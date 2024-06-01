
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using QLSV.Context;
using QLSV.Contracts;
using QLSV.Handler;
using QLSV.Repository;
using System.Text;

namespace QLSV
{

    public class Program
    {

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            //builder.Services.AddSingleton < AppDbContext >();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSingleton<DapperContext>();
            //Tạo lớp interface của Businessrepository để tách truy vấn và controller tăng an toàn
            builder.Services.AddScoped<ISinhVienRepository, SinhVienRepository>();
            builder.Services.AddScoped<ILopRepository, LopRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddAuthentication("BasicAuthentication").
                            AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>
                            ("BasicAuthentication", null);

            //JWT Authentication
            //builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuer = true,
            //        ValidateAudience = true,
            //        ValidateLifetime = true,
            //        ValidateIssuerSigningKey = true,
            //        ValidIssuer = builder.Configuration["Jwt:Issuer"],
            //        ValidAudience = builder.Configuration["Jwt:Audience"],
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
            //    };
            //});
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}