using System;
using System.Collections.Generic;
using System.Data;
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
        public static string ip;
        public static int port;
        public static string db_server;
        public static string db_database;
        public static string db_user;
        public static string db_password;
        public static string connStr;
        public static (string, bool)[] featureList;
        public Configuration()
        {
            XmlDocument XmlFile = new XmlDocument();
            XmlFile.Load("Config.xml");

            var config = XmlFile.GetElementsByTagName("Config")[0];
            var crypto = config.SelectSingleNode("AesCrypto");
            aesKey = Convert.FromBase64String(crypto.SelectSingleNode("Key").InnerText);
            aesIV = Convert.FromBase64String(crypto.SelectSingleNode("IV").InnerText);
            port = int.Parse(config.SelectSingleNode("Port").InnerText);
            ip = config.SelectSingleNode("IP").InnerText;

            var db = config.SelectSingleNode("DataBase");
            db_server = db.SelectSingleNode("Server").InnerText;
            db_database = db.SelectSingleNode("DataBase").InnerText;
            db_user = db.SelectSingleNode("UserID").InnerText;
            db_password = db.SelectSingleNode("Password").InnerText;

            connStr = string.Format("Server={0};Database={1};User ID={2};Password={3};", db_server, db_database, db_user, db_password);

            var features = config.SelectSingleNode("Features");
            featureList = new (string, bool)[features.ChildNodes.Count];
            for (int i = 0; i < features.ChildNodes.Count; i++)
            {
                XmlNode n = features.ChildNodes[i];
                featureList[i] = new(n.Attributes["name"].Value, bool.Parse(n.Attributes["enabled"].Value));
            }
        }
    }
}
