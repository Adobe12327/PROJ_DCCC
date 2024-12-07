

using System.Data;
using System.Text;
using PROJ_DCCC.HTTP;

namespace PROJ_DCCC
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
            new Configuration();
            HTTPProcessor processor = new HTTPProcessor($"http://*:{Configuration.port.ToString()}/");
            processor.StartListening();
        }
    }
}