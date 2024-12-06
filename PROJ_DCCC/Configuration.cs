using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace PROJ_DCCC
{
    public class Configuration
    {
        public static byte[] aesKey;
        public static byte[] aesIV;
        public static int port;
        public Configuration()
        {
            XmlDocument XmlFile = new XmlDocument();
            XmlFile.Load("Config.xml");

            var config = XmlFile.GetElementsByTagName("Config")[0];
            var crypto = config.SelectSingleNode("AesCrypto");
            aesKey = Convert.FromBase64String(crypto.SelectSingleNode("Key").InnerText);
            aesIV = Convert.FromBase64String(crypto.SelectSingleNode("IV").InnerText);
            port = int.Parse(config.SelectSingleNode("Port").InnerText);
        }
    }
}
