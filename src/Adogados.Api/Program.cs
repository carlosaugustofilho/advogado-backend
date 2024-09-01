using Advogados.Application;

var builder = WebApplication.CreateBuilder(args);

// Adiciona a infraestrutura
builder.Services.AddInfrastructure(builder.Configuration);

// Adiciona os servi�os de aplica��o
builder.Services.AddApplication();

// Configura��o padr�o do ASP.NET Core
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configura��o de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        builder => builder
            .WithOrigins("http://localhost:3000") // Permitir somente este dom�nio
            .AllowAnyMethod()                      // Permitir todos os m�todos (GET, POST, etc.)
            .AllowAnyHeader()                      // Permitir todos os cabe�alhos
            .AllowCredentials());                  // Permitir envio de credenciais
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Aplicar a pol�tica de CORS
app.UseCors("AllowReactApp");

app.UseAuthorization();

app.MapControllers();

app.Run();
