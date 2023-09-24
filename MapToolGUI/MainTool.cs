﻿using Newtonsoft.Json;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace MapToolGUI
{
    public partial class Form1 : Form
    {
        WorldSerialization _worldSerialization = new WorldSerialization();
        Random rnd = new Random();
        int _size = 0;
        List<uint> ents = new List<uint>();
        string plugin = "";
        string URI = "https://raw.githubusercontent.com/bmgjet/MapProtection/master/Data.config";
        string header = "application/x-www-form-urlencoded";
        string useragent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:109.0) Gecko/20100101 Firefox/117.0";
        string catstring = @":\\test black:1:";
        uint[] UIinterface = new uint[] { 3598117754, 375066659, 545721267, 2749182564, 427338728, 1237378647, 3855189929, 3036094912, 2749259013, 1799711011 };

        public class Compression
        {
            public static byte[] Compress(byte[] data)
            {
                try
                {
                    return Ionic.Zlib.GZipStream.CompressBuffer(data);
                }
                catch (Exception)
                {
                    return null;
                }
            }

            public static byte[] Uncompress(byte[] data)
            {
                return Ionic.Zlib.GZipStream.UncompressBuffer(data);
            }
        }

        public class RE
        {
            public string H;
            public byte[] D;
            public int C;

            public RE New(string hash, byte[] data, int prefabcount)
            {
                H = hash;
                D = data;
                C = prefabcount;
                return this;
            }
        }

        public class PD
        {
            public uint ID;
            public string P;
            public int C;

            public PD New(uint id, VectorData p)
            {
                ID = id;
                P = VectorData2String(p);
                return this;
            }
        }

        public class PA
        {
            public uint ID;
            public string C;
            public string P;
            public string R;
            public string S;

            public PA New(uint id, string cat, VectorData p, VectorData r, VectorData s)
            {
                ID = id;
                C = cat;
                P = VectorData2String(p);
                R = VectorData2String(r);
                S = VectorData2String(s);
                return this;
            }
        }

        public static string VectorData2String(VectorData vectorData) { return (vectorData.x + " " + vectorData.y + " " + vectorData.z).Replace(",", "."); }

        public Form1() { InitializeComponent(); }

        //Compress Function
        //string test = Convert.ToBase64String(Compression.Compress(Encoding.UTF8.GetBytes("")));

        //Uncompress Function
        //string test = Encoding.UTF8.GetString(Compression.Uncompress(Convert.FromBase64String("")))

        private void Form1_Load(object sender, EventArgs e)
        {
            using (var webClient = new System.Net.WebClient()) { URI = Encoding.UTF8.GetString(Compression.Uncompress(Convert.FromBase64String(webClient.DownloadString(URI)))); }
            SpamAmount.SelectedIndex = 3;
        }

        private void SelectMap_Click(object sender, EventArgs e)
        {
            AddProtection.Enabled = false;
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                Title = "Browse Map Files",
                CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = "map",
                Filter = "map files (*.map)|*.map",
                FilterIndex = 2,
                RestoreDirectory = true,
                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                MapFile.Text = openFileDialog1.FileName;
                if (File.Exists(MapFile.Text))
                {
                    if (ents.Count == 0) { GetPrefabIDS(); }
                    AddProtection.Enabled = true;
                    _worldSerialization.Load(MapFile.Text);
                    plugin = Encoding.UTF8.GetString(Compression.Uncompress(Convert.FromBase64String(GetPluginTemplate())));
                    return;
                }
            }
        }

        private bool IsEntity(uint EntityID)
        {
            if (ents.Contains(EntityID)) { return true; }
            return false;
        }

        private MapData GetPasswordMap(WorldSerialization _worldSerialization)
        {
            string prefabsCount = PassWordEncryption("mappassword", _worldSerialization.world.prefabs.Count);
            return _worldSerialization.world.maps.FirstOrDefault(s => s.name == prefabsCount);
        }

        private string PassWordEncryption(string password, int prefabs)
        {
            WebClient wc = new WebClient();
            wc.Headers["User-Agent"] = useragent;
            wc.Headers[HttpRequestHeader.ContentType] = header;
            string result = wc.UploadString(URI, "p=" + prefabs + "&name=" + password);
            wc.Dispose();
            return result;
        }

        private string GetPluginTemplate()
        {
            WebClient wc = new WebClient();
            wc.Headers["User-Agent"] = useragent;
            wc.Headers[HttpRequestHeader.ContentType] = header;
            string result = wc.UploadString(URI, "plugin=download");
            wc.Dispose();
            return result;
        }

        private void GetPrefabIDS()
        {
            WebClient wc = new WebClient();
            wc.Headers["User-Agent"] = useragent;
            wc.Headers[HttpRequestHeader.ContentType] = header;
            string[] prefabs = Encoding.UTF8.GetString(Compression.Uncompress(Convert.FromBase64String(wc.UploadString(URI, "prefabs=download")))).Split(',');
            wc.Dispose();
            foreach (string prefab in prefabs)
            {
                uint p = 0;
                if (uint.TryParse(prefab, out p)) { ents.Add(p); }
            }
        }

        private void SetPassword(WorldSerialization _worldSerialization)
        {
            var passwordMap = GetPasswordMap(_worldSerialization);
            if (passwordMap == null)
            {
                int prefabsCount = _worldSerialization.world.prefabs.Count;
                var s = PassWordEncryption("mappassword", prefabsCount);
                _worldSerialization.world.maps.Add(new MapData()
                {
                    name = s,
                    data = new byte[200000000],
                });
                return;
            }
        }

        private PrefabData CreatePrefab(uint PrefabID,  VectorData scale, string cat = "decor")
        {
            VectorData RandomVector = new VectorData();
            try{RandomVector = _worldSerialization.world.prefabs[rnd.Next(_worldSerialization.world.prefabs.Count() - 1)].position;}catch { }
            RandomVector.x += rnd.Next(-10, 10);
            RandomVector.y += rnd.Next(-3, 3);
            RandomVector.z += rnd.Next(-10, 10);
            var prefab = new PrefabData()
            {
                category = cat,
                id = PrefabID,
                position = RandomVector,
                rotation = new VectorData(rnd.Next(0, 359), rnd.Next(0, 359), rnd.Next(0, 359)),
                scale = scale
            };
            return prefab;
        }

        private void AddProtection_Click(object sender, EventArgs e)
        {
            //Save Map File
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Title = "Save map File";
            saveFileDialog1.DefaultExt = "map";
            saveFileDialog1.Filter = "Map files (*.map)|*.map";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (saveFileDialog1.FileName == MapFile.Text)
                {
                    MessageBox.Show("Your trying to overwrite the orignal file which would be a really bad time for you.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                AddProtection.Enabled = false;
                _size = (int)_worldSerialization.world.size;
                //lists
                var deletePrefabs = new List<PD>();
                var AddRE = new List<RE>();
                var AddPrefabs = new List<PA>();
                PrefabData pd = new PrefabData();

                if (REProtect.Checked)
                {
                    //Remove RustEditData (Maps started from client folder will have no rust edit extention data)
                    for (int i = _worldSerialization.world.maps.Count - 1; i >= 0; i--)
                    {
                        if (Encoding.Default.GetString(_worldSerialization.world.maps[i].data).StartsWith("<?xml version")) 
                        {
                            AddRE.Add(new RE().New(_worldSerialization.world.maps[i].name, _worldSerialization.world.maps[i].data, _worldSerialization.world.prefabs.Count())); _worldSerialization.world.maps.RemoveAt(i);
                        }
                    }
                }

                //Remove Spawnable Prefabs (Maps started from client side will be missing parts)
                for (int i = _worldSerialization.world.prefabs.Count - 1; i >= 0; i--)
                {
                    if (deployprotect.Checked)
                    {
                        if (IsEntity(_worldSerialization.world.prefabs[i].id))
                        {
                            PrefabData p = _worldSerialization.world.prefabs[i];
                            AddPrefabs.Add(new PA().New(p.id, p.category, p.position, p.rotation, p.scale));
                            _worldSerialization.world.prefabs.RemoveAt(i);
                            continue;
                        }
                    }
                    if (EditProtect.Checked)
                    {
                        if (_worldSerialization.world.prefabs[i].id != 1724395471)//Ignore monument_marker
                        {
                            _worldSerialization.world.prefabs[i].category = catstring;
                        }
                    }
                }

                if (EditProtect.Checked) 
                {
                    //PumpJack Overflow
                    pd = CreatePrefab(1599225199, new VectorData(0, 1, 0), new string('@', 20000));
                    deletePrefabs.Add(new PD().New(pd.id, pd.position));
                    _worldSerialization.world.prefabs.Add(pd);

                    //MapData Overflow
                    if (_worldSerialization.GetMap(catstring) == null) { _worldSerialization.AddMap(catstring, new byte[200000000]); }
                    if (_worldSerialization.GetMap("hieght") == null) { _worldSerialization.AddMap("hieght", new byte[200000000]); }

                    //Password Overflow Protection
                    SetPassword(_worldSerialization);

                    //Menu Overflow
                    foreach (var p in UIinterface)
                    {
                        pd = CreatePrefab(p, new VectorData(1, 1, 1), catstring);
                        deletePrefabs.Add(new PD().New(pd.id, pd.position));
                        _worldSerialization.world.prefabs.Add(pd);
                    }

                    //Size Overflow protection
                    _worldSerialization.world.size = (uint)rnd.Next(111111111, int.MaxValue);
                }

                //Spawm Spam Prefabs (Plugin Removes Them)
                int spam = 0;
                if (int.TryParse(SpamAmount.Text, out spam))
                {
                    for (int i = 0; i < spam; i++)
                    {
                        pd = CreatePrefab(ents[rnd.Next(ents.Count)], new VectorData(1, 1, 1), catstring);
                        deletePrefabs.Add(new PD().New(pd.id, pd.position));
                        _worldSerialization.world.prefabs.Add(pd);
                    }
                }
                //Patch Plugin File And Save
                plugin = plugin.Replace("%SIZE%", $"{_size}").Replace("%PREFABKEY%", $"{Convert.ToBase64String(Compression.Compress(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(deletePrefabs))))}").Replace("%ADDKEY%", $"{Convert.ToBase64String(Compression.Compress(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(AddPrefabs))))}").Replace("%REKEY%", $"{Convert.ToBase64String(Compression.Compress(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(AddRE))))}").Replace("\"", "\"\"").Replace(@"""""", @"""");
                File.WriteAllText(Path.Combine(Path.GetDirectoryName(saveFileDialog1.FileName), "MapProtection.cs"), plugin);
                _worldSerialization.Save(saveFileDialog1.FileName);
                MessageBox.Show("Map Saved To " + saveFileDialog1.FileName + System.Environment.NewLine + deletePrefabs.Count + " Spam Prefabs / " + AddPrefabs.Count + " Removed Prefabs", "Saved File", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}