
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();


var app = builder.Build();



app.MapPost("type", async Task<string> (HttpContext context, CancellationToken cancellationToken) =>
{

    var user = await context.Request.ReadFromJsonAsync<User>();
    var result = await Task.FromResult("Hello World");

    if (user is null)
    {
        return result;
    }

    Console.WriteLine(user);
    return result;

});


app.MapPost("json_serializer", async Task<string> (HttpContext context, CancellationToken cancellationToken) =>
{

    var data = await JsonSerializer.DeserializeAsync<Dictionary<string, object>>(context.Request.Body);
    var result = await Task.FromResult("Hello World");

    if (data is null)
    {
        return result;
    }

    Console.WriteLine($"Email :  {data["email"]}");
    Console.WriteLine($"Person :  {data["person"]}");
    return result;

});
app.MapPost("dictionary", async Task<string> (HttpContext context, CancellationToken cancellationToken) =>
{

    var data = await context.Request.ReadFromJsonAsync<IDictionary<string, object>>();
    var result = await Task.FromResult("Hello World");

    if (data is null)
    {
        return result;
    }

   Console.WriteLine($"Email :  {data["email"]}");
   Console.WriteLine($"Person :  {data["person"]}");
    return result;

});

app.UseCors((options)=> options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.Run();

public record Person(string Name, string LastName);
public record User(string Email, Person Person);


