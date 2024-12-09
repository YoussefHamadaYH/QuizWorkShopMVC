using QuizWorkShopMVC.ClientServices;

namespace QuizWorkShopMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Register HttpClient as a service for dependency injection
            builder.Services.AddHttpClient<HttpClientService>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
                 
            app.MapControllerRoute(
                name: "default",
                //https://localhost:7211/quiz/quizlist
                pattern: "{controller=quiz}/{action=quizlist}/{id?}");

            app.Run();
        }
    }
}
