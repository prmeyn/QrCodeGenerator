var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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



app.MapGet("/renderqrcode", async (string data) =>
{
    var qrGenerator = new QRCoder.QRCodeGenerator();
    var qrCodeData = qrGenerator.CreateQrCode(data, QRCoder.QRCodeGenerator.ECCLevel.Q);
    var qrCode = new QRCoder.PngByteQRCode(qrCodeData);

    var qrCodeAsPngBytes = qrCode.GetGraphic(20);

    return Results.File(qrCodeAsPngBytes, "image/png");
}).WithName("RenderQRCode").WithOpenApi();





app.Run();
