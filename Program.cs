
using FluentMigrator.Runner;
using System.Reflection;

namespace BizBorrowDiary
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
          
            builder.Services.AddControllers();
            //Add migrations
            builder.Services.AddFluentMigratorCore()
                .ConfigureRunner(config =>
                config.AddMySql8()
                .WithGlobalConnectionString("Server=localhost;Database=BizBorrowDiarydev;Uid=Sachin;Pwd=Sac@123")
            .ScanIn(typeof(BizBorrowDiary.Database.Migrations.InitialMigration).Assembly)
               .For.All());
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

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();
            var scope= app.Services.CreateScope();
            var migrator = scope.ServiceProvider.GetService<IMigrationRunner>();
            migrator.MigrateUp();
            app.Run();
        }
    }
}
