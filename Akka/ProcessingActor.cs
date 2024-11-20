using Akka.Actor;

namespace AkkaExample
{
    public class ProcessingActor : ReceiveActor
    {
        public ProcessingActor()
        {
            Receive<string>(message =>
            {
                Console.WriteLine($"ProcessingActor processing: {message}");
                // شبیه‌سازی ذخیره‌سازی در دیتابیس
            });
        }
    }
}
