using Taf.Core.Web;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.AddLocalConfiguration();//增加本地加密配置

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment()){
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.UseHealthCheck();//使用健康检查
app.Run();
