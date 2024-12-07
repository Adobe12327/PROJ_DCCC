using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PROJ_DCCC.HTTP.DTO;

namespace PROJ_DCCC.HTTP
{
    public class HTTPProcessor
    {
        static HttpListener listener = new HttpListener();
        static Thread thread = new Thread(new ParameterizedThreadStart(WorkerThread));
        public static Aes aes;

        public HTTPProcessor(string prefix)
        {
            listener.Prefixes.Add(string.Format($"{prefix}"));
        }

        public static void InitEncryption()
        {
            aes = Aes.Create();

            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            aes.KeySize = 128;
            aes.BlockSize = 128;

            // 키와 IV 설정
            aes.Key = Configuration.aesKey;
            aes.IV = Configuration.aesIV;

            Console.WriteLine("AES CBC 인스턴스가 성공적으로 생성되었습니다.");
        }

        public void StartListening()
        {
            listener.Start();  //서버 스레드 시작
            if (!thread.IsAlive)
                thread.Start(listener);
            Console.WriteLine(string.Format("Server Started at {0}", listener.Prefixes.ToArray()[0]));
            InitEncryption();
        }

        private static void WorkerThread(object arg)
        {
            try
            {
                while (listener.IsListening)
                {
                    HttpListenerContext ctx = listener.GetContext();
                    ProcessRequest(ctx);
                }
            }
            catch (ThreadAbortException)
            {
                //frm.textBox1.AppendText("Normal Stopping Service");
            }
        }

        private static byte[] ReadFully(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }

        private static void ResponceProcessBinary(HttpListenerContext ctx, byte[] data, HttpStatusCode statusCode)
        {

            //HttpListenerRequest request = ctx.Request;
            HttpListenerResponse response = ctx.Response;
            ctx.Response.StatusCode = (int)statusCode;
            //헤더 설정        
            response.Headers.Add("Accept-Encoding", "none"); //gzip 처리하기 귀찮으므로 비압축
            response.Headers.Add("Content-Type", "application/text");
            response.Headers.Add("Server", "DCCC-Server");
            response.Headers.Add("Access-Control-Allow-Origin", "*");
            //스트림 쓰기
            response.ContentLength64 = data.Length;
            Console.WriteLine(string.Format("Responce Length: {0}", data.Length));
            Stream output = response.OutputStream;
            output.Write(data, 0, data.Length);
        }

        public static T GetDecryptedDTO<T>(byte[] ClientRequestData, bool noEncryption)
        {
            string plainJson;

            if (!noEncryption)
            {
                using (ICryptoTransform decryptor = aes.CreateDecryptor())
                {
                    byte[] encryptedBytes = Convert.FromBase64String(Encoding.ASCII.GetString(ClientRequestData));
                    byte[] plainBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
                    plainJson = Encoding.UTF8.GetString(plainBytes);
                }
                Console.WriteLine(plainJson);
            }
            else
                plainJson = Encoding.UTF8.GetString(ClientRequestData);

            return JsonConvert.DeserializeObject<T>(plainJson);
        }

        public static void ProcessRequest(HttpListenerContext ctx)
        {
            string urlPath = ctx.Request.Url.LocalPath;
            byte[] Client_Req_data = new byte[0];
            if (ctx.Request.HttpMethod == "POST")
            {
                Client_Req_data = ReadFully(ctx.Request.InputStream);
            }

            Console.WriteLine(string.Format("Request: {0} , Methood:{2}, body: {1} byte", urlPath, Client_Req_data.Length, ctx.Request.HttpMethod));
            Console.WriteLine(Encoding.UTF8.GetString(Client_Req_data));

            if (HTTPPath.PathList.ContainsKey(urlPath))
            {
                var path = HTTPPath.PathList[urlPath];
                HTTPResponce response = (HTTPResponce)Activator.CreateInstance(path.responce);
                HTTPRequest request = null;
                if (path.request != null)
                {
                    var method = typeof(HTTPProcessor).GetMethod("GetDecryptedDTO");
                    var genericMethod = method.MakeGenericMethod(path.request);
                    request = (HTTPRequest)genericMethod.Invoke(null, new object[] { Client_Req_data, path.noEncryption });
                }
                response.Process(request);

                if (path.noEncryption == true)
                    ResponceProcessBinary(ctx, response.ToUTF8ByteArray(null), HttpStatusCode.OK);
                else
                    ResponceProcessBinary(ctx, response.ToUTF8ByteArray(aes), HttpStatusCode.OK);
                return;
            }
            else
            {
                Console.WriteLine("URL " + urlPath + " Not Exists!");
            }

        }
    }
}
