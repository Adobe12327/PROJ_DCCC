using Newtonsoft.Json;
using System.Text;
using System.Security.Cryptography;

namespace PROJ_DCCC.HTTP.DTO
{
    class HTTPResponce
    {
        public bool success;
        public string? errorCode;

        /// <summary>
        /// Json 문자열을 UTF8 바이트 배열로
        /// </summary>
        /// <param name="aes">null이 아닌 경우 해당 Aes 인스턴스를 사용해 암호화된 base64 문자열을 반환</param>
        /// <returns></returns>
        public byte[] ToUTF8ByteArray(Aes aes = null)
        {
            string json = JsonConvert.SerializeObject(this, Formatting.None);
            Console.WriteLine(json);
            if (aes != null)
            {
                using (ICryptoTransform encryptor = aes.CreateEncryptor())
                {
                    byte[] plainBytes = Encoding.UTF8.GetBytes(json);
                    byte[] encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
                    string base64 = Convert.ToBase64String(encryptedBytes);
                    return Encoding.ASCII.GetBytes(base64);
                }
            }
            return Encoding.UTF8.GetBytes(json);
        }

        public virtual void Process(HTTPRequest request) { }
    }
}
