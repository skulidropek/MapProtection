using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapProtection
{
    internal static class Data
    {
        public static Dictionary<string, string> Value = new Dictionary<string, string>();

        static Data()
        {
            if (File.Exists("Data.json"))
            {
                Value = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText("Data.json"));
                return;
            }

            Value = new Dictionary<string, string>()
            {
                { "2857304752", "assets/bundled/prefabs/radtown/crate_normal.prefab" },
                { "1071933290", "assets/bundled/prefabs/radtown/crate_mine.prefab" },
                { "1546200557", "assets/bundled/prefabs/radtown/crate_normal_2.prefab" },
                { "2896170989", "assets/bundled/prefabs/radtown/foodbox.prefab" },
                { "356724277", "assets/bundled/prefabs/radtown/vehicle_parts.prefab" },
                { "1603759333", "assets/bundled/prefabs/radtown/crate_basic.prefab" },
                { "1729604075", "assets/bundled/prefabs/static/recycler_static.prefab" },
                { "754638672", "assets/bundled/prefabs/static/hobobarrel_static.prefab" },
                { "1027662705", "assets/bundled/prefabs/static/door.hinged.industrial_a_c.prefab" },
                { "3408144370", "assets/bundled/prefabs/static/door.hinged.industrial_a_b.prefab" },
                { "3899244415", "assets/bundled/prefabs/static/door.hinged.garage_a.prefab" },
                { "629849447", "assets/bundled/prefabs/static/chair.static.prefab" }
            };

            Save();
        }

        public static void Save()
        {
            File.WriteAllText("Data.json", JsonConvert.SerializeObject(Value));
        }
    }
}
