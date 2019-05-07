using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using System.Runtime.Serialization.Json;
namespace REST_lib
{
    class JsonConverter
    {
        public static string ToJason<T>(T t)
        {
            MemoryStream ms = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));

            ser.WriteObject(ms,t);

            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            return sr.ReadToEnd();
        }

        public static T FromJson<T>(string s)
        {
            T t;

            MemoryStream ms = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));


            ms.Position = 0;
            t = (T)ser.ReadObject(ms);

            return t;
        }


    }
}
