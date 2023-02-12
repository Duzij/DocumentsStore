using DocumentStore.BL.Converters;
using DocumentStore.BL.Converters.Intefaces;
using DocumentStore.BL.Stores;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();
builder.Services.AddScoped<IDocumentRepository, InMemoryRepository>();
builder.Services.AddScoped<IXmlConverter, XmlConverter>();
builder.Services.AddScoped<IMessagePackConverter, MessagePackConverter>();
builder.Services.AddScoped<IJsonConverter, JsonConverter>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionMiddleware();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();
