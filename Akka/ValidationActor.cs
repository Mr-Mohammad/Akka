using Akka.Actor;

namespace AkkaExample
{
    public class ValidationActor : ReceiveActor
    {
        public ValidationActor()
        {
            Receive<string>(message =>
            {
                Console.WriteLine($"ValidationActor validating: {message}");
                bool isValid = message.Contains("valid"); // شبیه‌سازی اعتبارسنجی
                Sender.Tell(isValid); // نتیجه اعتبارسنجی را برمی‌گرداند
            });
        }
    }
}
