using System.Net.WebSockets;
using System.Text;

namespace Demo.WebSocket.Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            CancellationTokenSource sourceCancellationToken = new CancellationTokenSource();
            sourceCancellationToken.CancelAfter(50000);

            using var wsClient = new ClientWebSocket();
            await wsClient.ConnectAsync(new Uri("wss://socketsbay.com/wss/v2/2/demo/"), CancellationToken.None);

            byte[] buffer = new byte[256];

            while (wsClient.State == WebSocketState.Open)
            {
                var result = await wsClient.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await wsClient.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
                }
                else
                {
                    HandleMessage(buffer, result.Count);
                }

                if (result.EndOfMessage)
                {
                    // break;
                }
            }
        }

        private static void HandleMessage(byte[] buffer, int count)
        {
            // Console.WriteLine($">>> Received {BitConverter.ToString(buffer, 0, count)}");
            Console.WriteLine(">>> Data:{0}", Encoding.UTF8.GetString(buffer, 0, count));
        }
    }
}