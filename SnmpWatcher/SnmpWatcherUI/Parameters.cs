using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WatcherUI
{
    public class Parameters
    {
        private static Parameters _parameters = null;
        public static Parameters Params
        {
            get
            {
                if (_parameters == null)
                {
                    GetPrams();
                }
                return _parameters;
            }
        }
        public string IP
        {
            get;
            set;
        }
        public int Port
        {
            get;
            set;
        }
        public string Content
        {
            get;
            set;
        }
        public string Oid
        {
            get;
            set;
        }
        public int Timeout
        {
            get;
            set;
        }


        public static Parameters GetPrams()
        {
            if (_parameters == null)
            {
                XmlSerializer deserializer = new XmlSerializer(typeof(Parameters));
                TextReader reader = null;
                try
                {
                    /// read all available parameters from defined file and deserialize it for further use
                    reader = new StreamReader("Parameters.xml");
                    object obj = deserializer.Deserialize(reader);
                    _parameters = (Parameters)obj;
                    reader.Close();
                }
                catch (Exception e)
                {
                    _parameters = new Parameters();
                    SaveToFile();
                }
            }
            return _parameters;
        }

        private Parameters()
        {

        }

        public static void SavePrams()
        {
            GetPrams();
            SaveToFile();
        }

        public static void SaveToFile()
        {
            /// Serializing values and write them to defined file
            XmlSerializer serializer = new XmlSerializer(typeof(Parameters));
            using (TextWriter writer = new StreamWriter("Parameters.xml"))
            {
                serializer.Serialize(writer, _parameters);
            }
        }
    }
}
