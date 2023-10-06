using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProductQRCodeReadWithDynamic.Configurations;
using ProductQRCodeReadWithDynamic.Data;
using ProductQRCodeReadWithDynamic.Hubs;
using ProductQRCodeReadWithDynamic.Mappers;
using ProductQRCodeReadWithDynamic.Repositories.Abstractions;
using ProductQRCodeReadWithDynamic.Repositories.Concretes;
using ProductQRCodeReadWithDynamic.Services;
using ProductQRCodeReadWithDynamic.Services.Abstractions;
using ProductQRCodeReadWithDynamic.Services.Abstractions.Hubs;
using ProductQRCodeReadWithDynamic.Services.Concretes;
using ProductQRCodeReadWithDynamic.Services.Concretes.Hubs;
using ProductQRCodeReadWithDynamic.Subscriptions.HubConnectionTableDependency;
using ProductQRCodeReadWithDynamic.Subscriptions.Middleware;
using ProductQRCodeReadWithDynamic.Subscriptions.ProductTableDependency;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnectionString")), ServiceLifetime.Singleton);

var emailConfig = builder.Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfig);

builder.Services.AddIdentity<IdentityUser<int>, IdentityRole<int>>(opt =>
{
    opt.Password.RequireDigit = false;
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequireLowercase = false;
    opt.Password.RequireUppercase = false;
    opt.SignIn.RequireConfirmedEmail = false;
    opt.SignIn.RequireConfirmedPhoneNumber = false;
    opt.User.RequireUniqueEmail = false;
})
    .AddEntityFrameworkStores<AppDbContext>()
    .AddSignInManager<SignInManager<IdentityUser<int>>>()
    .AddDefaultTokenProviders();

builder.Services.AddHttpContextAccessor();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession();

builder.Services.AddSignalR();

builder.Services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
builder.Services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));
builder.Services.AddScoped<IProductReadRepository, ProductReadRepository>();
builder.Services.AddScoped<IProductWriteRepository, ProductWriteRepository>();

builder.Services.AddSingleton<IHubConnectionReadRepository, HubConnectionReadRepository>();
builder.Services.AddSingleton<IHubConnectionWriteRepository, HubConnectionWriteRepository>();
builder.Services.AddSingleton<INotificationReadRepository, NotificationReadRepository>();
builder.Services.AddSingleton<INotificationWriteRepository, NotificationWriteRepository>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IMailService, MailService>();

builder.Services.AddTransient<IProductHubService, ProductHubService>();

builder.Services.AddSingleton<INotificationHubService, NotificationHubService>();
builder.Services.AddScoped<IQrCodeService, QrCodeService>();

builder.Services.AddSingleton<NotificationHub>();


builder.Services.AddScoped<ProductHub>();



builder.Services.AddSingleton<DatabaseSubscription>();
builder.Services.AddSingleton<NotificationDatabaseSubscription>();

var databaseSetting = new DbSettings(builder.Configuration);
builder.Configuration.Bind(nameof(DbSettings), databaseSetting);
builder.Services.AddSingleton(databaseSetting);

var mapperConfig = new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new ProductProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.MapHub<ProductHub>("/productHub");
app.MapHub<NotificationHub>("/notificationHub");

app.UseDatabaseSubscription<DatabaseSubscription>("Products");
app.UseDatabaseSubscription<NotificationDatabaseSubscription>("Notifications");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();

app.UseRouting();

app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
