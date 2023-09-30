	using ITCanCook_DataAcecss;
	using Microsoft.EntityFrameworkCore;

	var builder = WebApplication.CreateBuilder(args);

	// Add services to the container.

	builder.Services.AddControllers();
	// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
	builder.Services.AddEndpointsApiExplorer();
	builder.Services.AddSwaggerGen();


	builder.Services.AddCors(options => options.AddDefaultPolicy(policy => 
	policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));// cho phép mọi người truy cập endpoint

// Đọc ConnectionString từ appsettings.json
builder.Configuration.AddJsonFile("appsettings.json");
builder.Services.AddDbContext<ITCanCookContext>(options =>
	{
		options.UseSqlServer(builder.Configuration.GetConnectionString("ITCanCook"));
	});


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
