using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.CustomEntities;
using SocialMedia.Core.Data;
using SocialMedia.Core.Interfaces;
using SocialMedia.Core.Services;
using SocialMedia.Infrastructure.Filters;
using SocialMedia.Infrastructure.Interfaces;
using SocialMedia.Infrastructure.Repositories;
using SocialMedia.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
})
.ConfigureApiBehaviorOptions(options =>
{
    //options.SuppressModelStateInvalidFilter = true;
});
builder.Services.Configure<PaginationOptions>(builder.Configuration.GetSection("Pagination"));

builder.Services.AddMvc(options =>
{
    options.Filters.Add<ValidationFilter>();
}).AddFluentValidation(options =>
{
    options.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
});
builder.Services.AddControllers(options => options.Filters.Add<GlobalExceptionFilters>());
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IPostService, PostService>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
//builder.Services.AddSingleton<IUriService, UriService>();
builder.Services.AddSingleton<IUriService>(provider =>
{
    var accesor = provider.GetRequiredService<IHttpContextAccessor>();
    var request = accesor.HttpContext.Request;
    var absoluteUri= string.Concat(request.Scheme, "://",request.Host.ToUriComponent());
    return new UriService(absoluteUri);
});
builder.Services.AddScoped(typeof(SocialMedia.Core.Interfaces.IRepository<>),typeof(BaseRepository<>));
builder.Services.AddDbContext<SocialMediaContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("SocialMedia")));
//builder.Services.Configure<PaginationOptions>(Configuration.GetSection("Pagination"));



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
