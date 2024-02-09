
using QLSV.Context;
using QLSV.Contracts;
using QLSV.Repository;

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