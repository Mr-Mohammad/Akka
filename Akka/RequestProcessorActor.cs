using Akka.Actor;

namespace AkkaExample
{
    public class RequestProcessorActor : ReceiveActor
    {
        public RequestProcessorActor()
        {
            Receive<string>(message =>
            {
                Console.WriteLine($"Processing request: {message}");
                // شبیه‌سازی پردازش (مثلاً ذخیره‌سازی در دیتابیس)
                Sender.Tell($"Request '{message}' processed successfully.");
            });
        }
    }
}
