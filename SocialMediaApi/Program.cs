using SocialMedia.Infrastructure.Extensions;
using SocialMedia.Infrastructure.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.AddDbContexts();

builder.AddOptions();

builder.AddServices();

builder.AddJwtAuthentication();

builder.AddMVCServices();

builder.Services.AddControllers(options => options.Filters.Add<GlobalExceptionFilters>());

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddSwaggerGen();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
}).ConfigureApiBehaviorOptions(options =>{});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
