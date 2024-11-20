using Akka.Actor;

namespace AkkaExample
{
    public class OrderActor : ReceiveActor
    {
        private readonly IActorRef _validationActor;
        private readonly IActorRef _processingActor;

        public OrderActor(IActorRef validationActor, IActorRef processingActor)
        {
            _validationActor = validationActor;
            _processingActor = processingActor;

            Receive<string>(message =>
            {
                Console.WriteLine($"OrderActor received: {message}");
                _validationActor.Tell(message); // پیام را برای ValidationActor ارسال می‌کند
            });

            Receive<bool>(isValid =>
            {
                if (isValid)
                {
                    Console.WriteLine("Order is valid. Sending to ProcessingActor...");
                    _processingActor.Tell("Process Order");
                }
                else
                {
                    Console.WriteLine("Order is invalid.");
                }
            });
        }
    }
}
