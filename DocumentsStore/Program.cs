using DocumentStore.BL.Converters;
using DocumentStore.BL.Converters.Intefaces;
using DocumentStore.BL.Stores;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();
builder.Services.AddScoped<IDocumentRepository, InMemoryRepository>();
builder.Services.AddScoped<IXmlConverter, XmlConverter>();
builder.Services.AddScoped<IMessagePackConverter, MessagePackConverter>();
builder.Services.AddScoped<IJsonConverter, DocumentStore.BL.Converters.JsonConverter>();

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
app.UseExceptionMiddleware();

app.Run();
