using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace Msi.UtilityKit.Json
{
    public static class JsonUtilities
    {

        public static JObject GetJObject(string filesDirectoryPath)
        {
            StringBuilder jsonDataSB = null;
            jsonDataSB = FileUtilities.ReadAllJsonFile(filesDirectoryPath);
            string jsonData = "";
            if (jsonDataSB != null)
            {
                jsonData = string.Format("{0}{1}{2}", "{", jsonDataSB.ToString(), "}");
            }
            var data = JsonConvert.DeserializeObject<dynamic>(jsonData);
            return JObject.Parse(jsonData);
        }

    }
}
