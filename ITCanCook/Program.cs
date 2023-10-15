using ITCanCook_BusinessObject;
using ITCanCook_BusinessObject.Service.Implement;
using ITCanCook_BusinessObject.Service.Interface;
using ITCanCook_BusinessObject.Helper.Implement;
using ITCanCook_DataAcecss;
using ITCanCook_DataAcecss.Entities;
using ITCanCook_DataAcecss.Repository.Interface;
using ITCanCook_DataAcecss.Repository.Implement;
using ITCanCook_DataAcecss.UnitOfWork;
using ITCanCook_BusinessObject.ServiceModel.Momo;
using ITCanCook.Mapping;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
	policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod())); // Cho phép mọi người truy cập endpoint

// Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
	.AddEntityFrameworkStores<ITCanCookContext>()
	.AddDefaultTokenProviders();


// Đọc ConnectionString từ appsettings.json
builder.Configuration.AddJsonFile("appsettings.json");
builder.Services.AddDbContext<ITCanCookContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("ITCanCook"));
});

builder.Services.AddAuthentication(op =>
{
	op.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	op.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
	op.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(op =>
{
	op.SaveToken = true;
	op.RequireHttpsMetadata = false;
	op.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
	{
		ValidateIssuer = true,
		ValidateAudience = true,
		ValidAudience = builder.Configuration["JWT:ValidAudience"],
		ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:IssuerSigningKey"]))
	};
});

builder.Services.AddSwaggerGen(options =>
{
	options.SwaggerDoc("v1", new OpenApiInfo { Title = "Auth API", Version = "v1" });
	options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		In = ParameterLocation.Header,
		Description = "Please enter a valid token",
		Name = "Authorization",
		Type = SecuritySchemeType.Http,
		BearerFormat = "JWT",
		Scheme = "Bearer"
	});
	options.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type = ReferenceType.SecurityScheme,
					Id = "Bearer"
				}
			},
			new string[] { }
		}
	});
    var xmlCommentFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var cmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentFile);
    options.IncludeXmlComments(cmlCommentsFullPath);
});



builder.Services.AddScoped<DbContext, ITCanCookContext>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEmailService, SendMail>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
//base repo
builder.Services.AddScoped<IBaseRepository<dynamic>, BaseRepository<dynamic>>();
//cooking hobby repo
builder.Services.AddScoped<ICookingHobbyRepo, CookingHobbyRepository>();
builder.Services.AddScoped<ICookingHobbyService, CookingHobbyService>();

builder.Services.AddScoped<IEquipmentRepo, EquipmentRepository>();
builder.Services.AddScoped<IEquipmentService, EquipmentService>();

builder.Services.AddScoped<IHealthConditionCategoryRepo, HealthConditionCategoryRepo>();
builder.Services.AddScoped<IHealthConditionCategoryService, HealthCategoryService>();

builder.Services.AddScoped<IHealthConditionRepo, HealthConditionRepository>();
builder.Services.AddScoped<IHealthConditionService, HealthConditionService>();

builder.Services.AddScoped<IIngredientCategoryRepo, IngredientCategoryRepository>();
builder.Services.AddScoped<IIngredientCategoryService, IngredientCategoryService>();

builder.Services.AddScoped<IIngredientRepo, IngredientRepository>();
builder.Services.AddScoped<IIngredientService, IngredientService>();

builder.Services.AddScoped<IRecipeRepo, RecipeRepository>();
builder.Services.AddScoped<IRecipeService, RecipeService>();

builder.Services.AddScoped<IRecipeAmountRepo, RecipeAmountRepository>();
builder.Services.AddScoped<IRecipeAmountService, RecipeAmountService>();

builder.Services.AddScoped<IRecipeStepRepo, RecipeStepRepository>();
builder.Services.AddScoped<IRecipeStepService, RecipeStepService>();



//Payment
builder.Services.Configure<MomoOptionModel>(builder.Configuration.GetSection("MomoAPI"));
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<ITransactionService, TransactionService>();

// Thêm dịch vụ Hangfire
builder.Services.AddHangfire(configuration => configuration
	.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
	.UseSimpleAssemblyNameTypeSerializer()
	.UseRecommendedSerializerSettings()
	.UseSqlServerStorage(builder.Configuration.GetConnectionString("ITCanCook")));


// Đăng ký mapper
builder.Services.AddAutoMapper(typeof(ModelMappers));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
// Khởi tạo Hangfire
app.UseHangfireServer();
app.UseHangfireDashboard();


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
