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
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MapProtection
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _plugin = @"
// Reference: 0Harmony
using Harmony;
using Newtonsoft.Json;
using Oxide.Core;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Oxide.Plugins
{
    [Info(""MapProtection"", ""https://discord.gg/4G5s2eepu8"", ""1.3.11"")]
    [Description(""MapProtection"")]
    class MapProtection : RustPlugin
    {
        private HarmonyInstance _harmony; //Reference to harmony
        string json = @""%JSON%"";
        List<PrefabData> prefabDatas = new List<PrefabData>();

        private void Init()
        {
            prefabDatas = JsonConvert.DeserializeObject<List<PrefabData>>(json);
            _harmony = HarmonyInstance.Create(Name + ""PATCH"");
            Type[] patchType ={AccessTools.Inner(typeof(MapProtection), ""OnWorldLoad_hook""),};
            foreach (var t in patchType) { new PatchProcessor(_harmony, t, HarmonyMethod.Merge(t.GetHarmonyMethods())).Patch(); } //Patch Harmony Code In
        }

        private void OnServerInitialized()
        {
            foreach (var baseEntity in BaseNetworkable.serverEntities.entityList.Values)
            {
                if (baseEntity is BaseEntity)
                {
                    var prefabData = prefabDatas.FirstOrDefault(s =>
                            s.position == baseEntity.transform.position &&
                            s.rotation == baseEntity.transform.rotation &&
                            s.id == baseEntity.prefabID);

                    if (prefabData != null)
                    {
                        baseEntity.Kill(BaseNetworkable.DestroyMode.Gib);
                    }
                }
            }
        }

        private void Unload() { _harmony.UnpatchAll(Name + ""PATCH""); }//Remove harmony

        [HarmonyPatch(typeof(WorldSerialization), nameof(WorldSerialization.Load),typeof(string))] 
        internal class OnWorldLoad_hook
        {
            [HarmonyPostfix] //Postfix runs after orignal code
            static void Postfix(WorldSerialization __instance)
            {
               Interface.CallHook(""OnWorldLoad"", __instance); //Calls the hook
            }
        }

        private void OnWorldPrefabSpawned(GameObject obj, string category)
        {
            BaseEntity baseEntity = obj.GetComponent<BaseEntity>();
            if (baseEntity == null)
                return;

            var prefabData = prefabDatas.FirstOrDefault(s =>
                   s.position == obj.transform.position &&
                   s.rotation == obj.transform.rotation &&
                   s.id == baseEntity.prefabID
                   );

            if (prefabData != null)
            {
                baseEntity.Kill(BaseNetworkable.DestroyMode.Gib);
            }
        }

        private void OnWorldLoad()
        {
            WorldSerialization worldSerialization = World.Serialization;
            worldSerialization.world.size = %SIZE%;
        }

        [Serializable]
        [ProtoContract]
        public class PrefabData
        {
            [ProtoMember(1)] public string category;
            [ProtoMember(2)] public uint id;
            [ProtoMember(3)] public VectorData position;
            [ProtoMember(4)] public VectorData rotation;
            [ProtoMember(5)] public VectorData scale;


            public PrefabData() { }
            public PrefabData(string category, uint id, VectorData position, VectorData rotation, VectorData scale)
            {
                this.category = category;
                this.id = id;
                this.position = position;
                this.rotation = rotation;
                this.scale = scale;
            }
        }

        [Serializable]
        [ProtoContract]
        public class VectorData
        {
            [ProtoMember(1)] public float x;
            [ProtoMember(2)] public float y;
            [ProtoMember(3)] public float z;

            public VectorData(float x, float y, float z)
            {
                this.x = x;
                this.y = y;
                this.z = z;
            }

            public static implicit operator VectorData(Vector3 v)
            {
                return new VectorData(v.x, v.y, v.z);
            }

            public static implicit operator VectorData(Quaternion q)
            {
                return q.eulerAngles;
            }

            public static implicit operator Vector3(VectorData v)
            {
                return new Vector3(v.x, v.y, v.z);
            }

            public static implicit operator Quaternion(VectorData v)
            {
                return Quaternion.Euler(v);
            }
        }
    }
}
                        ";
        private static List<string> _spanwedPrefabIds = new List<string>()
        {
            "3598117754",
            "375066659",
            "545721267",
            "2749182564",
            "427338728",
            "1237378647",
            "3855189929",
            "3036094912",
            "2749259013",
            "1799711011",
        };

        public MainWindow()
        {
            InitializeComponent();
        }

        private WorldSerialization _worldSerialization = new WorldSerialization();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string path = GetPathOpenFileDialog();

            if (string.IsNullOrEmpty(path))
            {
                MessageBox.Show("Need choose path");
                return;
            }

            _worldSerialization.Load(path);

            var deletePrefabs = new List<WorldSerialization.PrefabData>();

            var prefabMiningPumjack = new WorldSerialization.PrefabData()
            {
                category = ":\\test black:1:",
                id = 1599225199,
                position = new WorldSerialization.VectorData(),
                rotation = new WorldSerialization.VectorData(),
                scale = new WorldSerialization.VectorData(0, 1, 0)
            };

            _worldSerialization.world.prefabs.Add(prefabMiningPumjack);

            deletePrefabs.Add(prefabMiningPumjack);

            _plugin = _plugin.Replace("%SIZE%", $"{_worldSerialization.world.size}");

            _worldSerialization.world.size = 999999999;

            foreach (var prefab in _spanwedPrefabIds)
            {
                _worldSerialization.world.prefabs.Add(new WorldSerialization.PrefabData()
                {
                    category = "menu",
                    id = uint.Parse(prefab),
                    position = new WorldSerialization.VectorData(),
                    rotation = new WorldSerialization.VectorData(),
                    scale = new WorldSerialization.VectorData(),
                });
            }

            Random random = new Random();

            if (_worldSerialization.GetMap(":\\test black:1:") == null)
                _worldSerialization.AddMap(":\\test black:1:", new byte[0]);

            foreach (var prefab in _worldSerialization.world.prefabs)
                prefab.category = ":\\test black:1:";

            var prefabs = _worldSerialization.world.prefabs.ToList();

            foreach (var prefab in Data.Value)
            {
                if (random.Next(0, 10) != 2)
                    continue;

                foreach (var worldPrefab in _worldSerialization.world.prefabs)
                {
                    if (random.Next(0, 2) != 1)
                        continue;

                    var prefabSpawn = new WorldSerialization.PrefabData(":\\test black:1:", uint.Parse(prefab.Key), worldPrefab.position, worldPrefab.rotation, worldPrefab.scale);
                    prefabs.Add(prefabSpawn);
                    deletePrefabs.Add(prefabSpawn);
                }
            }

            _plugin = _plugin.Replace("%JSON%", JsonConvert.SerializeObject(deletePrefabs).Replace("\"", "\"\""));

            File.WriteAllText(path + "MapProtection.cs", _plugin);

            _worldSerialization.world.prefabs = prefabs;

            SetPassword("1");

            var data = new byte[200000000];

            GetPasswordMap().data = data;//2146437092];

            _worldSerialization.Save(path + "PROTECTION.map");
        }

        private WorldSerialization.MapData GetPasswordMap()
        {
            int prefabsCount = _worldSerialization.world.prefabs.Count;
            return _worldSerialization.world.maps.FirstOrDefault(s => s.name == OPIPGDGHLCP("mappassword", prefabsCount) || s.name == EIGDJDKLLED("mappassword", prefabsCount));
        }

        private void SetPassword(string password)
        {
            if (string.IsNullOrEmpty(password)) return;

            var passwordMap = GetPasswordMap();

            if (passwordMap == null)
            {
                int prefabsCount = _worldSerialization.world.prefabs.Count;
                var s = EIGDJDKLLED("mappassword", prefabsCount);

                _worldSerialization.world.maps.Add(new WorldSerialization.MapData()
                {
                    name = s,
                    data = new byte[0],
                });

                SetPassword(password);
                return;
            }

            passwordMap.data = Encoding.UTF8.GetBytes(password);
        }

        private string EIGDJDKLLED(string KBJLCHGLCPN, int FNCNKFNPJAB)
        {
            string password = FNCNKFNPJAB.ToString();
            byte[] bytes = Encoding.Unicode.GetBytes(KBJLCHGLCPN);
            using (Aes aes = Aes.Create())
            {
                Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, new byte[]
                {
                73,
                118,
                97,
                110,
                32,
                77,
                101,
                100,
                118,
                101,
                100,
                101,
                118
                });
                aes.Key = rfc2898DeriveBytes.GetBytes(32);
                aes.IV = rfc2898DeriveBytes.GetBytes(16);
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(bytes, 0, bytes.Length);
                        cryptoStream.Close();
                    }
                    KBJLCHGLCPN = Convert.ToBase64String(memoryStream.ToArray());
                }
            }
            return KBJLCHGLCPN;
        }

        private string OPIPGDGHLCP(string JKNFEDNIELG, int FNCNKFNPJAB)
        {
            StringBuilder stringBuilder = new StringBuilder(JKNFEDNIELG);
            StringBuilder stringBuilder2 = new StringBuilder(JKNFEDNIELG.Length);
            for (int i = 0; i < JKNFEDNIELG.Length; i++)
            {
                char c = stringBuilder[i];
                c = (char)((int)c ^ FNCNKFNPJAB);
                stringBuilder2.Append(c);
            }
            return stringBuilder2.ToString();
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
