﻿using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace Blog.Frontend.Common
{
    public static class JsonHelper
    {
        public static string SerializeJson<T>(T obj)
        {
            if (obj == null)
                return "";

            string r = "";
            try
            {
                var serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(obj.GetType());
                using (var ms = new MemoryStream())
                {
                    serializer.WriteObject(ms, obj);
                    r = Encoding.UTF8.GetString(ms.ToArray());
                    ms.Close();
                }
            }
            catch (Exception ex)
            {

                System.Diagnostics.Debug.WriteLine("#ERR SerializeJson:\t" + ex.Message);
            }
            return r;
        }

        public static T DeserializeJson<T>(string json)
        {
            T obj = default(T);
            try
            {
                obj = JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("#ERR DeserializeJson:\t" + ex.Message);
            }
            return obj;
        }
    }
}