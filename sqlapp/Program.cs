using sqlapp.Services;

var builder = WebApplication.CreateBuilder(args);

//
builder.Services.AddTransient<IProductService, ProductService>();
// Add services to the container.
builder.Services.AddRazorPages();

//Value from the Azure App config access key
var connectionString = "Endpoint=https://jjwebappconfig.azconfig.io;Id=Ouhu-l8-s0:yKpzLdiGwSGCAyGjd+rY;Secret=1Re3bIDGKc1ivXLb0JzhLa+L/7zSg6kk2kwYCJkeMIQ=";

builder.Host.ConfigureAppConfiguration(builder =>
{
    builder.AddAzureAppConfiguration(connectionString);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
