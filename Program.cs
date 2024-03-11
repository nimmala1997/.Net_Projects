using FirstApp;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddTransient(typeof(DoAuthorization));
builder.Services.AddTransient<DoAuthorization>(sp =>
{
    return new DoAuthorization();
});
var app = builder.Build();

//app.MapGet("/", () => "Hello World! Nimmala");



app.Use(async (HttpContext context, RequestDelegate next) =>
{

    context.Response.Headers["MyKey"] = "My Value";
    context.Response.StatusCode = 200;
    await context.Response.WriteAsync("Hello");
    await context.Response.WriteAsync("World");
    await next(context);
});

app.UseMiddleware<DoAuthorization>();

app.Run(async (HttpContext context) =>
{

    //context.Response.Headers["MyKey"] = "My Value";
    //context.Response.StatusCode = 200;
    await context.Response.WriteAsync("Hello");
    await context.Response.WriteAsync("World");
});

app.Run();

