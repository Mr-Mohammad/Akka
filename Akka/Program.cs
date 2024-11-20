using Akka.Actor;
using Akka.DependencyInjection;
using AkkaExample;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton(provider =>
{
    var bootstrap = BootstrapSetup.Create();
    var diSetup = DependencyResolverSetup.Create(provider);
    var actorSystemSetup = bootstrap.And(diSetup);
    return ActorSystem.Create("MyActorSystem", actorSystemSetup);
});


builder.Services.AddSingleton<IActorRef>(provider =>
{
    var actorSystem = provider.GetRequiredService<ActorSystem>();

    var validationActor = actorSystem.ActorOf(Props.Create<ValidationActor>(), "validationActor");
    var processingActor = actorSystem.ActorOf(Props.Create<ProcessingActor>(), "processingActor");
    return actorSystem.ActorOf(Props.Create(() => new OrderActor(validationActor,processingActor)), "orderActor");
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
