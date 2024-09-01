using Advogados.Application;

var builder = WebApplication.CreateBuilder(args);

// Adiciona a infraestrutura
builder.Services.AddInfrastructure(builder.Configuration);

// Adiciona os serviços de aplicação
builder.Services.AddApplication();

// Configuração padrão do ASP.NET Core
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuração de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        builder => builder
            .WithOrigins("http://localhost:3000") // Permitir somente este domínio
            .AllowAnyMethod()                      // Permitir todos os métodos (GET, POST, etc.)
            .AllowAnyHeader()                      // Permitir todos os cabeçalhos
            .AllowCredentials());                  // Permitir envio de credenciais
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Aplicar a política de CORS
app.UseCors("AllowReactApp");

app.UseAuthorization();

app.MapControllers();

app.Run();
