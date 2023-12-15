// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Encrypt.cs" company="">
//   
// </copyright>
// <summary>
//   The encrypt.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using KYSharp.SM;
using System;
using System.ComponentModel;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Taf.Core.Utility{
    /// <summary>
    /// The encrypt.
    /// </summary>
    public static class Encrypt{
        /// <summary>
        /// 加密字符串长度应该大于8
        /// </summary>
        public const string EncrKey = "P@ssw0rd+";
        /// <summary>
        /// 国密SM4加密密钥
        /// </summary>
        private const string Sm4SecretKey = "j5vmjdLczPJgs3IM";
        /// <summary>
        /// 国密SM4加密向量
        /// </summary>
        private const string Sm4IvKey = "JGh6O3ohb6IxME7O";
        /// <summary>
        /// 国密SM2加密公钥
        /// </summary>
        private const string Sm2PublicKey = "04499F83EBF37604B775597B67E3A0EA90488E0AF8A162601EC2730472A5B16E9344A9082883F08523DBA21472962F8458F5E382F819DE7887AD7E5D560DE13A10";
        /// <summary>
        /// 国密SM2加密私钥
        /// </summary>
        private const string Sm2PrivateKey = "00DAA773A775704C8A9A3B11D0D39E14A5628F367882A823E0B4AC36D28C34B1E7";

        /// <summary>
        /// 获取新密码
        /// </summary>
        /// <param name="pwdlen">
        /// 密码长度
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetNewPassword(int pwdlen){
            const string randomchars = "abcdefghijklmnopqrstuvwxyz0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var          tmpstr      = string.Empty;
            var          rnd         = new Random();
            for(var i = 0; i < pwdlen; i++){
                var iRandNum = rnd.Next(randomchars.Length);
                tmpstr += randomchars[iRandNum];
            }

            return tmpstr;
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="strText">
        /// </param>
        /// <param name="encrKey">盐</param>
        /// <param name="version">v2版本使用国密</param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string DesEncrypt(string strText, SecurityVersion version = SecurityVersion.V2, string? encrKey = null){
            if(version == SecurityVersion.V1){
                byte[] iv ={ 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

                var byKey = encrKey == null
                    ? Encoding.UTF8.GetBytes(EncrKey.Substring(0, 8))
                    : Encoding.UTF8.GetBytes(encrKey.Substring(0, 8));
                var des            = new DESCryptoServiceProvider();
                var inputByteArray = Encoding.UTF8.GetBytes(strText);
                var ms             = new MemoryStream();
                var cs             = new CryptoStream(ms, des.CreateEncryptor(byKey, iv), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }

            return Sm4Encrypt(strText, encrKey); //使用国密SM4加密
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="strText">
        /// </param>
        /// <param name="encrKey">盐</param>
        /// <param name="version">v2版本使用国密</param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string DesDecrypt(string strText, SecurityVersion version = SecurityVersion.V2, string? encrKey = null){
            try{
                if(version == SecurityVersion.V1){
                    byte[] iv ={ 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
                    var byKey = encrKey == null
                        ? Encoding.UTF8.GetBytes(EncrKey.Substring(0, 8))
                        : Encoding.UTF8.GetBytes(encrKey.Substring(0, 8));
                    var    des = new DESCryptoServiceProvider();
                    var    inputByteArray = Convert.FromBase64String(strText);
                    var    ms = new MemoryStream();
                    var    cs = new CryptoStream(ms, des.CreateDecryptor(byKey, iv), CryptoStreamMode.Write);
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    Encoding encoding = new UTF8Encoding();
                    return encoding.GetString(ms.ToArray());
                }

                return Sm4Decrypt(strText, encrKey); //使用国密SM4解密 
            } catch{
                return strText;
            }
        }

        /// <summary>
        /// 生成MD5
        /// </summary>
        /// <param name="input">
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetMd5Hash(string input){
            var md5        = MD5.Create();
            var inputBytes = Encoding.ASCII.GetBytes(input);
            var hash       = md5.ComputeHash(inputBytes);
            var sb         = new StringBuilder();
            foreach(var t in hash){
                sb.Append(t.ToString("X2"));
            }

            return sb.ToString();
        }

        /// <summary>
        /// Md5加密，返回16位结果
        /// </summary>
        /// <param name="text">
        /// 待加密字符串
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string Md5By16(string text) => Md5By16(text, Encoding.UTF8);
        
        /// <summary>
        /// 获取对象的Md5值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string Md5(object obj) => Md5By32(JsonSerializer.Serialize(obj));

        /// <summary>
        /// Md5加密，返回16位结果
        /// </summary>
        /// <param name="text">
        /// 待加密字符串
        /// </param>
        /// <param name="encoding">
        /// 字符编码
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string Md5By16(string text, Encoding encoding) => Md5(text, encoding, 4, 8);

        /// <summary>
        /// Md5加密
        /// </summary>
        /// <param name="text">
        /// The text.
        /// </param>
        /// <param name="encoding">
        /// The encoding.
        /// </param>
        /// <param name="startIndex">
        /// The start Index.
        /// </param>
        /// <param name="length">
        /// The length.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private static string Md5(string text, Encoding encoding, int? startIndex, int? length){
            if(string.IsNullOrWhiteSpace(text)){
                return string.Empty;
            }

            var    md5 = new MD5CryptoServiceProvider();
            string result;
            try{
                result = startIndex == null
                    ? BitConverter.ToString(md5.ComputeHash(encoding.GetBytes(text)))
                    : BitConverter.ToString(
                        md5.ComputeHash(encoding.GetBytes(text)),
                        startIndex.SafeValue(),
                        length.SafeValue());
            } finally{
                md5.Clear();
            }

            return result.Replace("-", string.Empty);
        }

        /// <summary>
        /// Md5加密，返回32位结果
        /// </summary>
        /// <param name="text">
        /// 待加密字符串
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string Md5By32(string text) => Md5By32(text, Encoding.UTF8);

        /// <summary>
        /// Md5加密，返回32位结果
        /// </summary>
        /// <param name="text">
        /// 待加密字符串
        /// </param>
        /// <param name="encoding">
        /// 字符编码
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string Md5By32(string text, Encoding encoding) => Md5(text, encoding, null, null);

        /// <summary> 
        /// SHA1加密字符串 
        /// </summary> 
        /// <param name="text">源字符串</param> 
        /// <returns>加密后的字符串</returns> 
        public static string Sha1(string text){
            var           strRes = Encoding.Default.GetBytes(text);
            HashAlgorithm iSha   = new SHA1CryptoServiceProvider();
            strRes = iSha.ComputeHash(strRes);
            var enText = new StringBuilder();
            foreach(var iByte in strRes){
                enText.Append($"{iByte:x2}");
            }

            return enText.ToString();
        }

    #region SM加密解密

        /// <summary>
        /// SM4加密,使用CBC模式
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="secretKey"></param>
        /// <returns></returns>
        public static string Sm4Encrypt(string plainText, string? secretKey = null){
            Fx.If(!string.IsNullOrWhiteSpace(secretKey) && secretKey.Length < 16)
              .Then(() => secretKey = secretKey.PadRight(16, '0'));
            var sm4            = new SM4Utils{ secretKey = secretKey ?? Sm4SecretKey ,iv = Sm4IvKey,hexString = false};
            var originalString = sm4.Encrypt_ECB( plainText);
            var bytesToEncode  = Encoding.UTF8.GetBytes(originalString);
            return Convert.ToBase64String(bytesToEncode);
        }

        /// <summary>
        /// SM4解密,使用CBC模式
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="secretKey"></param>
        /// <returns></returns>
        public static string Sm4Decrypt(string plainText, string? secretKey = null){
            Fx.If(!string.IsNullOrWhiteSpace(secretKey) && secretKey.Length < 16)
              .Then(() => secretKey = secretKey.PadRight(16, '0'));
            var decodedBytes = Convert.FromBase64String(plainText);
            var targetText   = Encoding.UTF8.GetString(decodedBytes);
            var sm4          = new SM4Utils{ secretKey = secretKey ?? Sm4SecretKey ,hexString = false};
            return sm4.Decrypt_ECB(targetText);
        }
        
        /// <summary>
        /// SM2加密,使用CBC模式
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="publicKey"></param>
        /// <returns></returns>
        public static string Sm2Encrypt(string plainText, string? publicKey = null){
            var originalString = SM2Utils.Encrypt_Hex(publicKey ?? Sm2PublicKey, plainText, Encoding.UTF8);
            var bytesToEncode  = Encoding.UTF8.GetBytes(originalString);
            return Convert.ToBase64String(bytesToEncode);
        }

        /// <summary>
        /// SM2解密,使用CBC模式
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="privateKey"></param>
        /// <returns></returns>
        public static string Sm2Decrypt(string plainText, string? privateKey = null){
            var decodedBytes = Convert.FromBase64String(plainText);
            var targetText   = Encoding.UTF8.GetString(decodedBytes);
            return SM2Utils.Decrypt_Hex(privateKey ?? Sm2PrivateKey, targetText, Encoding.UTF8);
        }
        
    #endregion
        
    }
    
    public enum SecurityVersion{
        [Description("V1版本使用SHA1CryptoServiceProvider")]V1,
        [Description("V2版本使用国密Sm4")]V2
    }
}
