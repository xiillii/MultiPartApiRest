using documentsapi.logic;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddTransient<IStorageRepository>((provider) =>
{
    var storageAccountName = builder.Configuration.GetValue<string>("StorageAccount:Name") ?? "";
    var accessKey = builder.Configuration.GetValue<string>("StorageAccount:AccessKey") ?? "";
    var container = builder.Configuration.GetValue<string>("StorageAccount:ContainerBase") ?? "";

    return new AzureStorageAccount(storageAccountName, accessKey, container);
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


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
