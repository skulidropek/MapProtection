using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MapUnlock.Core
{
    internal class PrefabEntity
    {
        public List<uint> Esnts = new List<uint>();

        public PrefabEntity()
        {
            GetPrefabIDS();
        }

        public bool IsEntity(uint EntityID)
        {
            if (Esnts.Contains(EntityID)) { return true; }
            return false;
        }

        private void GetPrefabIDS()
        {
            WebClient wc = new WebClient();
            wc.Headers["User-Agent"] = Web.Useragent;
            wc.Headers[HttpRequestHeader.ContentType] = Web.Header;
            string[] prefabs = Encoding.UTF8.GetString(Compression.Uncompress(Convert.FromBase64String(wc.UploadString(Web.Uri, "prefabs=download")))).Split(',');
            wc.Dispose();
            foreach (string prefab in prefabs)
            {
                uint p = 0;
                if (uint.TryParse(prefab, out p)) { Esnts.Add(p); }
            }
        }
    }
}
