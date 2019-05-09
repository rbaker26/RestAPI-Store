using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using System.Runtime.Serialization.Json;
namespace REST_lib
{
    public static class JsonConverter
    {
        /// <summary>
        /// Converts an Object to JSON
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJason<T>(T obj)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));

                ser.WriteObject(ms, obj);

                ms.Position = 0;
                StreamReader sr = new StreamReader(ms);
                return sr.ReadToEnd();
            }
            catch(Exception)
            {
                return "[]";
            }

        }

        /// <summary>
        /// Converts JSON to an Object of type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T FromJson<T>(string json)
        {
            try
            {
                MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));

                ms.Position = 0;
                T t = (T)ser.ReadObject(ms);

                return t;
            }
            catch(Exception)
            {
                return default(T);
            }
        }


    }
}
