using System;
using System.Threading.Tasks;
using Grpc.Net.Client;
using gRPCContagem;

namespace ClientgRPCContagem
{
    class Program
    {
        private const string SERVER_GRPC = "https://localhost:5001";
        //private const string SERVER_GRPC = "http://localhost:25001";
        //private const string SERVER_GRPC = "http://52.186.82.56";

        public static async Task Main()
        {
            if (!SERVER_GRPC.StartsWith("https"))
            {
                AppContext.SetSwitch(
                    "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            }

            var channel = GrpcChannel.ForAddress(SERVER_GRPC);
            var client = new ContadorSvc.ContadorSvcClient(channel);

            while (true)
            {
                var resultado = await client.GerarValorAsync(
                    new ContadorRequest()
                    {
                        Nome = "Renato"
                    });
                Console.WriteLine(
                    $"| Mensagem: {resultado.Mensagem} " +
                    $"| Valor Atual: {resultado.ValorAtual} " +
                    $"| Local: {resultado.LocalSvc} " +
                    $"| Target Framework: {resultado.TargetFramework} |");

                Console.ReadKey();
            }
        }
    }
}