#define EDITOR_VER

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Psimos.Core
{
    public class Beatmap
    {
        public readonly Sky sky;
        public readonly Ground ground;

        private Beatmap(Sky sky, Ground ground)
        {
            this.sky = sky;
            this.ground = ground;
        }

        public static Beatmap LoadFromPSIBF(byte[] psibf)
        {
            // 拿到谱面的基本数据
            byte[] key = psibf.Take(16) as byte[];
            string hash = Encoding.ASCII.GetString(psibf.Skip(16).Take(32) as byte[]);
            byte[] bBeatmap = AES.AESDecrypt(Encoding.UTF8.GetString(psibf.Skip(48) as byte[]), Encoding.UTF8.GetString(key));
            // 检查下谱面内容的SHA256对了没，没对那就是被魔改了，你玩NM，游戏都给你扬咯
            SHA256 sha256 = new SHA256Managed();
            if (BitConverter.ToString(sha256.ComputeHash(bBeatmap)).Replace("-", "").ToUpper() != hash)
            {
                // TODO: 想个办法通知下游戏主体，搞死玩这几把修改版游戏的
#if DEBUG
                File.AppendAllText(string.Format("{0}.log", DateTime.Now.ToString("M")), string.Format("[{1}]Incorrect beatmap: {0}\n", hash, DateTime.Now.ToString("F")));
#endif
                return null;
            }
            string beatmap = Encoding.UTF8.GetString(bBeatmap);
            return JsonConvert.DeserializeObject<Beatmap>(beatmap);
        }
#if EDITOR_VER
        public void SavePSIBF(string fileName, string key=null)
        {
            // PSIBF基本信息计算
            string beatmap = JsonConvert.SerializeObject(this);
            byte[] bBeatmap = Encoding.UTF8.GetBytes(beatmap);
            SHA256 sha256 = new SHA256Managed();
            byte[] hash = Encoding.UTF8.GetBytes(BitConverter.ToString(sha256.ComputeHash(bBeatmap)).Replace("-", "").ToUpper());
            string Key = key;
            if (Key == null)
            {
                // 生成随机密钥
                char[] serial = "ABCDEFGHIJKLMOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890-=!@#$%^&*()_+~`".ToCharArray();
                Random random = new Random();
                Key = new string(Enumerable.Repeat(serial, 16).Select(s => s[random.Next(s.Length)]).ToArray());
            }
            byte[] encrypted = Encoding.UTF8.GetBytes(AES.AESEncrypt(Convert.ToBase64String(bBeatmap), Key));
            // 把谱面信息全部拼在一起
            List<byte> bytes = new List<byte>();
            bytes.AddRange(Encoding.UTF8.GetBytes(Key));
            bytes.AddRange(hash);
            bytes.AddRange(encrypted);
            File.WriteAllBytes(fileName, bytes.ToArray());
        }
#endif
    }
}
