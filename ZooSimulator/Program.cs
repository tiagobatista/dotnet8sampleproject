using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer("Server=localhost;Database=master;User Id=sa;Password=YourStrong!Passw0rd;Encrypt=True;TrustServerCertificate=True"));
/*
Dependency Injection (DI) is a design pattern and a core principle in software engineering, particularly in object-oriented programming, 
to achieve Inversion of Control (IoC) between classes and their dependencies. 
In simple terms, DI allows for injecting dependencies (services, objects) into a class rather than the class creating them internally.
This leads to more modular, testable, and maintainable code.

Core Concepts of Dependency Injection
Dependency: A dependency is any object that another object requires to function.
 For example, if a class Car needs an instance of Engine to operate, Engine is a dependency of Car.
Injection: Injection is the process of passing the dependencies to a class instead of the class creating them. This can be done via:
Constructor Injection: Dependencies are provided through the class constructor.
Property Injection: Dependencies are set through public properties of the class.
Method Injection: Dependencies are passed through methods of the class.
Benefits of Dependency Injection
Decoupling: DI reduces the tight coupling between a class and its dependencies, making the code more flexible and easier to change.
Testability: Since dependencies can be injected, it becomes easier to swap real implementations with mock objects or stubs during unit testing.
Maintainability: DI promotes single responsibility and adherence to the SOLID principles, particularly the Dependency Inversion Principle,
 making the codebase easier to maintain.

Three Main Lifecycles
Transient (Short-Lived)
What it means: A new instance of the service is created every time it's needed.
When to use: When the service doesn’t need to remember anything (stateless). For example, a service that just does a quick calculation.
Example: Think of a disposable cup. You use it once and then throw it away.
Scoped (Lives During a Request)
What it means: The service is created once per request and reused within that request.
When to use: When you want the service to keep information while handling one request but throw it away afterward.
 Useful in web applications where you want to keep track of user data during a single page load.
Example: Imagine a water bottle you use throughout the day but replace the next day.
Singleton (Lives as Long as the Program)
What it means: The service is created once when the program starts and stays the same for the entire life of the program.
When to use: When the service needs to remember things or is expensive to create, 
like a settings manager that loads configuration data once and reuses it.
Example: Think of a refrigerator that you use all the time and it keeps working for a long time.
How to Decide Which to Use?
Transient: Use when you don’t need to keep data, like a quick helper.
Scoped: Use when you need to share data temporarily, like during a single task.
Singleton: Use when you need to keep data or share a resource for the entire program’s life.

*/

builder.Services.AddTransient<IAnimalRepo, AnimalRepo>(); //order doesn't matter
builder.Services.AddTransient<IZooService, ZooService>();

var app = builder.Build();

//Refer middlewares. Here, we use to log - Middleware is software that bridges the gap between applications and operating systems (OSes) by providing a method for communication and data management.
//This capability is useful for applications that otherwise do not have any way to exchange data with other software tools or databases.
//Usage: This middleware will log every request made to the API, helping in debugging and monitoring.
app.Use(async (context, next) =>
{
    Console.WriteLine($"Request: {context.Request.Method} {context.Request.Path}");
    await next();
    Console.WriteLine($"Response: {context.Response.StatusCode}");
});

/*
We could also do this for custom middlewares

public class RequestResponseLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public RequestResponseLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Log the incoming request details
        Console.WriteLine($"Incoming Request: {context.Request.Method} {context.Request.Path}{context.Request.QueryString}");

        // Call the next middleware in the pipeline
        await _next(context);

        // Log the outgoing response details
        Console.WriteLine($"Outgoing Response: {context.Response.StatusCode}");
    }
}

and register like this 
app.UseMiddleware<RequestResponseLoggingMiddleware>();
*/

/*
.NET Minimal API is a feature introduced in .NET 6 and improved in later versions, designed to simplify the development of APIs by reducing boilerplate code and streamlining the setup process. It provides a lightweight approach to building HTTP APIs, focusing on simplicity and ease of use. Here’s a comprehensive yet simple explanation of what Minimal API is and how it works:

What is Minimal API?
Minimal API is a simplified way to create web APIs using .NET without the need for a lot of configuration or boilerplate code. It’s particularly useful for small to medium-sized applications where you want to quickly set up endpoints with minimal overhead.

Key Features
Concise Syntax: Minimal APIs use a more concise syntax, allowing you to define endpoints and request handling logic directly in the Program.cs file or a similar startup file.
Reduced Boilerplate: You don’t need to set up controllers, action methods, or models unless you need them. This reduces the amount of code and configuration required to get an API up and running.
Improved Startup: The Minimal API approach simplifies the startup configuration by reducing the need for multiple files and classes.
How Does Minimal API Work?
Here’s a step-by-step overview of how to create a Minimal API:

Setup: You start by creating a new .NET project (e.g., an ASP.NET Core Web API project).
Define Endpoints: In the Program.cs file, you define your HTTP endpoints using minimal syntax. You use methods like MapGet, MapPost, etc., to specify routes and request handling.
Run the Application: The application runs with the minimal configuration, and you can use it to handle HTTP requests directly.
*/

var zooAPI = app.MapGroup("/animals");
zooAPI.MapGet("/", async (IZooService zooService) => await zooService.GetAllAnimalsFromZooAsync());
zooAPI.MapGet("/{movementType}", (string movementType, IZooService zooService) =>
{
    MovementType? movementTypeResult = movementType switch
    {
        "flying" => MovementType.Flying,
        "walking" => MovementType.Walking,
        _ => null
    };

    if (movementType is null)
    {
        return Results.NotFound();
    }

    var animals = zooService.GetAnimalsByMovementType(movementTypeResult.Value);

    return animals.Count > 0 ? Results.Ok(animals) : Results.NotFound(animals);
});

zooAPI.MapPost("/{name}/add", (string name, IZooService zooService) =>
{
    Animal? newAnimal = name.ToLower() switch
    {
        "lion" => new Lion(MovementType.Walking),
        "eagle" => new Eagle(MovementType.Flying),
        "tiger" => new Tiger(MovementType.Walking),
        _ => null
    };

    if (newAnimal is null)
    {
        return Results.BadRequest("Animal not needed to be saved!");
    }

    var wasAdded = zooService.AddAnimalToZoo(newAnimal);

    return Results.Created($"/animals/{name}", newAnimal);
});

app.Run();

[JsonSerializable(typeof(List<Animal>))]
[JsonSerializable(typeof(Animal))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{

}
