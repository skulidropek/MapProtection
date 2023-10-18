using Ionic.Zlib;
using MapUnlock.Core;
using MapUnlock.Extension;
using MapUnlock.Models;
using Newtonsoft.Json;
using Ookii.Dialogs.Wpf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MapUnlock.ViewModels
{
    internal class MapSettingViewModel : ViewModelBase
    {
        private WorldSerialization _worldSerialization = new WorldSerialization();
        private string _path;
        private Random _rnd = new Random();
        private int _size = 0;
        private readonly PrefabEntity _prefabEntity = new PrefabEntity();
        private readonly RustPlugin _rustPlugin = new RustPlugin();
        private List<RE> _addRE = new List<RE>();
        private List<PA> _addPrefabs = new List<PA>();
        private List<PD> _deletePrefabs = new List<PD>();

        private string _mapFile;
        private bool _isAddProtectionEnabled = true;
        private string _spamAmount = "5000";
        private bool _isREProtectChecked = true;
        private bool _isDeployProtectChecked = true;
        private bool _isEditProtectChecked = true;

        public string MapFile
        {
            get { return _mapFile; }
            set
            {
                _mapFile = value;
                OnPropertyChanged("MapFile");
            }
        }

        public bool IsAddProtectionEnabled
        {
            get { return _isAddProtectionEnabled; }
            set
            {
                _isAddProtectionEnabled = value;
                OnPropertyChanged("IsAddProtectionEnabled");
            }
        }

        public string SpamAmount
        {
            get { return _spamAmount; }
            set
            {
                _spamAmount = value;
                OnPropertyChanged("SpamAmount");
            }
        }

        public bool IsREProtectChecked
        {
            get { return _isREProtectChecked; }
            set
            {
                _isREProtectChecked = value;
                OnPropertyChanged("IsREProtectChecked");
            }
        }

        public bool IsDeployProtectChecked
        {
            get { return _isDeployProtectChecked; }
            set
            {
                _isDeployProtectChecked = value;
                OnPropertyChanged("IsDeployProtectChecked");
            }
        }

        public bool IsEditProtectChecked
        {
            get { return _isEditProtectChecked; }
            set
            {
                _isEditProtectChecked = value;
                OnPropertyChanged("IsEditProtectChecked");
            }
        }

        public ICommand SaveMapCommand { get; }
        public ICommand SelectMapCommand { get; }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public MapSettingViewModel()
        {
            SaveMapCommand = new RelayCommand(SaveMapCommandExecute);
            SelectMapCommand = new RelayCommand(SelectMapCommandExecute, (s) => !string.IsNullOrWhiteSpace(_path));
        }

        private void SelectMapCommandExecute(object obj)
        {
            _path = GetPathOpenFileDialog();

            if (string.IsNullOrEmpty(_path))
            {
                MessageBox.Show("Need choose path");
                return;
            }
        }

        private void SaveMapCommandExecute(object obj)
        {
            if (string.IsNullOrWhiteSpace(_path))
            {
                MessageBox.Show("Need select map");
                return;
            }

            LoadWorldData();
            ProcessProtection();
            ProcessPrefabs();
            ProcessPumpJackOverflow();
            ProcessMapDataOverflow();
            ProcessSpamPrefabs();
            ShufflePrefabList();
            PatchAndSavePluginFile();
        }

        private void LoadWorldData()
        {
            _worldSerialization.Load(_path);
            _size = (int)_worldSerialization.world.size;
        }

        private void ProcessProtection()
        {
            //Remove RustEditData (Maps started from client folder will have no rust edit extention data)
            for (int i = _worldSerialization.world.maps.Count - 1; i >= 0; i--)
            {
                if (System.Text.Encoding.Default.GetString(_worldSerialization.world.maps[i].data).StartsWith("<?xml version")) { _addRE.Add(new RE().New(_worldSerialization.world.maps[i].name, _worldSerialization.world.maps[i].data, _worldSerialization.world.prefabs.Count())); _worldSerialization.world.maps.RemoveAt(i); }
            }
        }

        private void ProcessPrefabs()
        {
            _worldSerialization.world.prefabs.RemoveAll(prefab =>
            {
                if (_prefabEntity.IsEntity(prefab.id))
                {
                    _addPrefabs.Add(new PA().New(prefab.id, prefab.category, prefab.position, prefab.rotation, prefab.scale));
                    return true;
                }
                if (prefab.id != 1724395471)
                {
                    prefab.category = $":\\\\test black:{_rnd.Next(0, Math.Min(_worldSerialization.world.prefabs.Count, 40))}:";
                }
                return false;
            });
        }

        private void ProcessPumpJackOverflow()
        {
            var pd = CreatePrefab(1599225199, new string('@', 200000000));
            _deletePrefabs.Add(new PD().New(pd.id, pd.position));
            _worldSerialization.world.prefabs.Add(pd);
        }

        private void ProcessMapDataOverflow()
        {
            if (_worldSerialization.GetMap("height") == null)
            {
                _worldSerialization.AddMap("height", new byte[200000000]);
            }

            var pd = CreatePrefab(1237378647, $":\\test black:1:");
            _deletePrefabs.Add(new PD().New(pd.id, pd.position));
            _worldSerialization.world.prefabs.Add(pd);
            _worldSerialization.world.size = (uint)_rnd.Next(111111111, int.MaxValue);

            for (int i = 0; i < Math.Min(_worldSerialization.world.prefabs.Count, 40); i++)
            {
                if (_worldSerialization.GetMap($":\\test black:{i}:") == null)
                {
                    _worldSerialization.AddMap($":\\test black:{i}:", new byte[0]);
                }
            }
        }

        private void ProcessSpamPrefabs()
        {
            if (int.TryParse(SpamAmount.Split(" ").Last(), out int spam))
            {
                int pref = 0;
                for (int i = 0; i < spam; i++)
                {
                    var pd = CreatePrefab(_prefabEntity.Esnts[pref], $":\\\\test black:{_rnd.Next(0, Math.Min(_worldSerialization.world.prefabs.Count, 40))}:");
                    _deletePrefabs.Add(new PD().New(pd.id, pd.position));
                    _worldSerialization.world.prefabs.Add(pd);
                    pref = (pref + 1) % _prefabEntity.Esnts.Count;
                }
            }
        }

        private void ShufflePrefabList()
        {
            _worldSerialization.world.prefabs = ShufflePrefabs(_worldSerialization.world.prefabs);
        }

        private void PatchAndSavePluginFile()
        {
            string pluginFilePath = Path.Combine(Path.GetDirectoryName(_path), "MapProtection.cs");
            string pluginContent = _rustPlugin.Plugin
                .Replace("%SIZE%", $"{_size}")
                .Replace("%PREFABKEY%", Convert.ToBase64String(Compression.Compress(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(_deletePrefabs))))
                .Replace("%ADDKEY%", Convert.ToBase64String(Compression.Compress(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(_addPrefabs))))
                .Replace("%REKEY%", Convert.ToBase64String(Compression.Compress(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(_addRE))))
                .Replace("\"", "\"\"")
                .Replace(@"""""", @""""))))
                ;

            File.WriteAllText(pluginFilePath, pluginContent);
            _worldSerialization.UpdatePassword();
            _worldSerialization.Save(_path + "protection.map");
        }

        private WorldSerialization.PrefabData CreatePrefab(uint PrefabID, string cat)
        {
            WorldSerialization.VectorData RandomVector = new WorldSerialization.VectorData();

            if (_worldSerialization.world.prefabs.Count < 420)
            {
                RandomVector = new WorldSerialization.VectorData(_rnd.Next((_size / 3) * -1, _size / 3), _rnd.Next(-1, 50), _rnd.Next((_size / 3) * -1, _size / 3));
            }
            else
            {
                try { RandomVector = _worldSerialization.world.prefabs[_rnd.Next(_worldSerialization.world.prefabs.Count - 1)].position; } catch { }
                RandomVector.x += _rnd.Next(-10, 10);
                RandomVector.y += _rnd.Next(-3, 3);
                RandomVector.z += _rnd.Next(-10, 10);
            }

            return new WorldSerialization.PrefabData()
            {
                category = cat,
                id = PrefabID,
                position = RandomVector,
                rotation = new WorldSerialization.VectorData(_rnd.Next(0, 359), _rnd.Next(0, 359), _rnd.Next(0, 359)),
                scale = new WorldSerialization.VectorData(1, 1, 1)
            };
        }

        public List<WorldSerialization.PrefabData> ShufflePrefabs(List<WorldSerialization.PrefabData> listToShuffle)
        {
            for (int i = listToShuffle.Count - 1; i > 0; i--)
            {
                int k = _rnd.Next(i + 1);
                var value = listToShuffle[k];
                listToShuffle[k] = listToShuffle[i];
                listToShuffle[i] = value;
            }
            return listToShuffle;
        }

        private string GetPathOpenFileDialog()
        {
            VistaOpenFileDialog openFileDialog = new VistaOpenFileDialog();
            openFileDialog.Filter = "Карты раст (*.map)|*.map";

            if (openFileDialog.ShowDialog() == false)
            {
                return "";
            }

            return openFileDialog.FileName;
        }
    }
}
