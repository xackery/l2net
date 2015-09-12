using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace L2_login
{
    /// <summary>
    /// Bot Configuration for loading/saving from new flat file system, replaces the old BotOptions
    /// </summary>
    class BotConfig
    {
        public volatile FollowOption Follow = new FollowOption();
        public string PlayerName = "";

        private static volatile BotConfig instance;
        /// <summary>
        /// BotConfig is a singleton instance, so call this to get the current instance settings.
        /// </summary>
        /// <returns></returns>
        public static BotConfig Instance()
        {
            if (instance == null)
            {
                instance = new BotConfig();
            }
            return instance;
        }

        public static void Save(string path)
        {
            if (instance == null)
            {
                instance = new BotConfig();
            }

            JsonTextWriter writer = new JsonTextWriter(new StreamWriter(path));

            JsonSerializer serializer = new JsonSerializer();
            serializer.Formatting = Formatting.Indented;
            serializer.Serialize(writer, instance);
            writer.Close();

        }
        public static void Load(string path)
        {
            
            JsonTextReader reader = new JsonTextReader(new StreamReader(path));

            JsonSerializer serializer = new JsonSerializer();
            instance = serializer.Deserialize<BotConfig>(reader);
            reader.Close();
        }
        

        public static void Clear()
        {
            instance = new BotConfig();
        }
        
    }
}
