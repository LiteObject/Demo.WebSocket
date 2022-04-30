/*
 *  RFC 6455 WebSockets specification
 */

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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

// Configure the websocket middleware
var wsOptions = new WebSocketOptions
{
    // How frequently to send "ping" frames to the client to ensure
    // proxies keep the connection open. The default is two minutes.
    KeepAliveInterval = TimeSpan.FromMinutes(2)    
};

// list of allowed Origin header values for WebSocket requests. By default, all origins are allowed.
// wsOptions.AllowedOrigins.Add("http://...");

app.UseWebSockets(wsOptions);

app.Run();
