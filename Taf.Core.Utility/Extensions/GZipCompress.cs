// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GZipCompress.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   $Summary$
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.IO;
using System.IO.Compression;
using System.Text;

// 何翔华
// Taf.Utilty
// GZipCompress.cs

namespace Taf.Core.Utility{
    /// <summary>
    /// 字符串
    /// </summary>
    public static class GZipCompress{
        /// <summary>  
        /// 字节数组压缩  
        /// </summary>  
        /// <param name="data"></param>  
        /// <returns></returns>  
        private static byte[] Compress(byte[] data){
            try{
                var ms  = new MemoryStream();
                var   zip = new GZipStream(ms, CompressionMode.Compress, true);
                zip.Write(data, 0, data.Length);
                zip.Close();
                var buffer = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(buffer, 0, buffer.Length);
                ms.Close();
                return buffer;
            } catch(Exception e){
                throw new Exception(e.Message);
            }
        }

        /// <summary>  
        /// 字节数组解压缩  
        /// </summary>  
        /// <param name="data"></param>  
        /// <returns></returns>  
        private static byte[] Decompress(byte[] data){
            try{
                var ms       = new MemoryStream(data);
                var   zip      = new GZipStream(ms, CompressionMode.Decompress, true);
                var msreader = new MemoryStream();
                var       buffer   = new byte[0x1000];
                while(true){
                    var reader = zip.Read(buffer, 0, buffer.Length);
                    if(reader <= 0){
                        break;
                    }

                    msreader.Write(buffer, 0, reader);
                }

                zip.Close();
                ms.Close();
                msreader.Position = 0;
                buffer            = msreader.ToArray();
                msreader.Close();
                return buffer;
            } catch(Exception e){
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 字符串压缩
        /// </summary>
        /// <returns>The string.</returns>
        /// <param name="str">String.</param>
        public static string CompressString(string str){
            var compressString     = "";
            var compressBeforeByte = Encoding.UTF8.GetBytes(str);
            var compressAfterByte  = Compress(compressBeforeByte);
            //compressString = Encoding.GetEncoding("UTF-8").GetString(compressAfterByte);    
            compressString = Convert.ToBase64String(compressAfterByte);
            return compressString;
        }

        /// <summary>
        /// 字符串解压缩
        /// </summary>
        /// <returns>The string.</returns>
        /// <param name="str">String.</param>
        public static string DecompressString(string str){
            var compressString = "";
            //byte[] compressBeforeByte = Encoding.GetEncoding("UTF-8").GetBytes(str);    
            var compressBeforeByte = Convert.FromBase64String(str);
            var compressAfterByte  = Decompress(compressBeforeByte);
            compressString = Encoding.UTF8.GetString(compressAfterByte);
            return compressString;
        }
    }
}
