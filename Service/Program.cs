using System.Collections.Concurrent;
using System.Text.Json.Serialization;
using Service.Auth;
using Service.Cache;
using Service.Cache.LockerStore;
using Service.Contexts;
using Service.HostedService;
using Service.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<UserDbContext>();


builder.Services.AddSingleton(new ConcurrentDictionary<int, object>());
builder.Services.AddSingleton<IKeyLockerStore, DictionaryKeyLockerStore>();
builder.Services.AddSingleton<ICustomCache, CustomCache>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddHostedService<FetchDatabaseTimedHostedService>();
builder.Services.AddEndpointsApiExplorer();

builder.Services
    .AddControllers()
    .AddJsonOptions(opts =>
    {
        var enumConverter = new JsonStringEnumConverter();
        opts.JsonSerializerOptions.Converters.Add(enumConverter);
    })
    .AddXmlSerializerFormatters()
    .AddXmlDataContractSerializerFormatters();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseMiddleware<BasicAuthMiddleware>();

app.MapRazorPages();
app.MapControllers();

app.Run();

