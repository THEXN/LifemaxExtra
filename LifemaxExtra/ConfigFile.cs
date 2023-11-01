using System;
using System.IO;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using TShockAPI;

namespace LifemaxExtra
{
    public class ConfigFile
    {
        public int 生命水晶最低使用值 = 400;
        public int 生命水晶最高使用值 = 600;
        public int 生命果最低使用值 = 600;
        public int 生命果最高使用值 = 1000;

        public static ConfigFile Read(string Path)//给定文件进行读  
        {
            if (!File.Exists(Path)) return new ConfigFile();
            using (var fs = new FileStream(Path, FileMode.Open, FileAccess.Read, FileShare.Read))
            { return Read(fs); }
        }
        public static ConfigFile Read(Stream stream)//给定流文件进行读取  
        {
            using (var sr = new StreamReader(stream))
            {
                var cf = JsonConvert.DeserializeObject<ConfigFile>(sr.ReadToEnd());
                if (ConfigR != null)
                    ConfigR(cf);
                return cf;
            }
        }
        public void Write(string Path) //给定路径进行写    
        {
            try
            {
                using (var fs = new FileStream(Path, FileMode.Create, FileAccess.Write, FileShare.None))
                { Write(fs); }
            }
            catch (IOException ex)
            {
                // 可以在这里添加自定义的异常处理逻辑    
                TShock.Log.ConsoleError("插件写入文件错误：" + ex.Message);
            }
        }
        public void Write(Stream stream) //给定流文件写    
        {
            var str = JsonConvert.SerializeObject(this, Formatting.Indented);
            try
            {
                using (var sw = new StreamWriter(stream))
                {
                    sw.Write(str);
                    sw.Flush(); // 刷新流，确保数据被完全写入  
                }
            }
            catch (Exception ex)
            {
                // 可以在这里添加自定义的异常处理逻辑    
                TShock.Log.ConsoleError("插件写入流错误：" + ex.Message);
            }
        }
        public static Action<ConfigFile> ConfigR;// 初始化 ConfigR 为 null，然后在需要的地方进行赋值。  
    }
}
