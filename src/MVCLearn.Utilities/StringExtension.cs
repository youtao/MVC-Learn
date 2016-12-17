using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace MVCLearn.Utilities
{
    public static class StringExtension
    {
        /// <summary>
        /// 获取字符串的MD5
        /// </summary>
        /// <param name="source"></param>
        /// <exception cref="NullReferenceException">字符串为空</exception>
        /// <returns></returns>
        public static string MD5(this string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                throw new NullReferenceException("字符串为空");
            }
            using (MD5 md5 = new MD5CryptoServiceProvider())
            {
                byte[] sourceBytes = Encoding.Default.GetBytes(source);
                byte[] md5Bytes = md5.ComputeHash(sourceBytes);
                StringBuilder sb = new StringBuilder();
                foreach (byte b in md5Bytes)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        }

        /// <summary>
        /// 字符串Sha1哈希值
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string Sha1(this string source)
        {
            var sourceBytes = Encoding.Default.GetBytes(source);
            var hashedBytes = SHA1.Create().ComputeHash(sourceBytes);
            return BitConverter.ToString(hashedBytes).Replace("-", string.Empty);
        }

        /// <summary>
        /// 剔除字符串中的HTML标签
        /// </summary>
        /// <param name="source">html</param>
        /// <param name="length">要获取的长度</param>
        /// <returns></returns>
        public static string RemoveHtmlTag(this string source, int length = 200)
        {
            string temp = Regex.Replace(source, "[<].*?[>]", "");
            temp = Regex.Replace(temp, "&[^;]+;", "");
            if (length > 0 && temp.Length > length)
                return temp.Substring(0, length);
            return temp;
        }

        /// <summary>
        /// 获取Html内容里图片路径
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public static List<string> HtmlImageUrls(this string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return new List<string>();
            }
            var reg = new Regex(
                @"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<url>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>",
                RegexOptions.IgnoreCase);
            var matches = reg.Matches(source);
            var list = (from Match item in matches select item.Groups["url"].Value).ToList();
            return list;
        }
    }
}