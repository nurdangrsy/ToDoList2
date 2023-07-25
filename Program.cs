using Microsoft.OpenApi;
using ToDoList.Models;
using ToDoList.Services;
using System.Reflection;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<TodoListDatabaseSettings>(builder.Configuration.GetSection("TodoListDatabase"));
builder.Services.AddSingleton<TodosService>();
builder.Services.AddRazorPages();

builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "TodoList Api",
        Description="",
        TermsOfService=new Uri("https://example.com/terms"),
        Contact=new OpenApiContact
        {
            Name="Example Contact",
            Url= new Uri("https://exampla.com/contact")
        },
        License= new OpenApiLicense
        {
            Name = "License",
            Url= new Uri("https://example.com/licence")
        }
    });
});

var app = builder.Build();


app.UseDeveloperExceptionPage();
app.UseSwagger(options => { options.SerializeAsV2 = true; });
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    //options.RoutePrefix = string.Empty;
});


//app.UseHttpsRedirection();
//app.UseAuthorization();

app.MapControllers();
app.MapRazorPages();
app.Run();
