using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Tony.Util;

namespace Tony.Application.Code
{
   public class VerifyCode
    {
        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <returns></returns>
        public byte[] GetVerifyCode()
        {
            var codeW = 80;
            var codeH = 30;
            var fontSize = 16;
            var chkCode = string.Empty;
            //颜色列表，用于验证码、噪线、噪点 
            Color[] color = { Color.Black, Color.Red, Color.Blue, Color.Green, Color.Orange, Color.Brown, Color.Brown, Color.DarkBlue };
            //字体列表，用于验证码 
            string[] font = { "Times New Roman" };
            //验证码的字符集，去掉了一些容易混淆的字符 
            char[] character = { '2', '3', '4', '5', '6', '8', '9', 'a', 'b', 'd', 'e', 'f', 'h', 'k', 'm', 'n', 'r', 'x', 'y', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'R', 'S', 'T', 'W', 'X', 'Y' };
            var rnd = new Random();
            //生成验证码字符串 
            for (var i = 0; i < 4; i++)
            {
                chkCode += character[rnd.Next(character.Length)];
            }
            //写入Session、验证码加密
            WebHelper.WriteSession("session_verifycode", Md5Helper.MD5(chkCode.ToLower(), 16));
            //创建画布
            var bmp = new Bitmap(codeW, codeH);
            var g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
            //画噪线 
            for (var i = 0; i < 1; i++)
            {
                var x1 = rnd.Next(codeW);
                var y1 = rnd.Next(codeH);
                var x2 = rnd.Next(codeW);
                var y2 = rnd.Next(codeH);
                var clr = color[rnd.Next(color.Length)];
                g.DrawLine(new Pen(clr), x1, y1, x2, y2);
            }
            //画验证码字符串 
            for (var i = 0; i < chkCode.Length; i++)
            {
                var fnt = font[rnd.Next(font.Length)];
                var ft = new Font(fnt, fontSize);
                var clr = color[rnd.Next(color.Length)];
                g.DrawString(chkCode[i].ToString(), ft, new SolidBrush(clr), (float)i * 18, 0);
            }
            //将验证码图片写入内存流，并将其以 "image/Png" 格式输出 
            var ms = new MemoryStream();
            try
            {
                bmp.Save(ms, ImageFormat.Png);
                return ms.ToArray();
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                g.Dispose();
                bmp.Dispose();
            }
        }
    }
}
