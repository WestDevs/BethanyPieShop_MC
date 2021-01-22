using BethanyPieShop.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BethanyPieShop
{
  public class Startup
  {
    private readonly IConfiguration _configuration;

    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public Startup(IConfiguration configuration)
    {
      _configuration = configuration;
    }
    public void ConfigureServices(IServiceCollection services)
    {
      //register services here through Dependency Injection
      services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));

      services.AddScoped<IPieRepository, PieRepository>();
      services.AddScoped<ICategoryRepository, CategoryRepository>();
      services.AddScoped<ShoppingCart>(sp => ShoppingCart.GetCart(sp));
      services.AddHttpContextAccessor();
      services.AddSession();
      //services.AddScoped<IPieRepository, MockPieRepository>();
      //services.AddScoped<ICategoryRepository, MockCategoryRepository>();
      services.AddControllersWithViews(); //AddMvc() in ASP.Net Core 2.1


    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseHttpsRedirection();
      app.UseStatusCodePages();
      app.UseStaticFiles(); //search any directory called wwwroot folder
      app.UseSession(); // call this before useRouting
      app.UseRouting();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllerRoute(
          name: "default",
          pattern: "{controller=Home}/{action=Index}/{id?}");
        //endpoints.MapGet("/", async context =>
        //      {
        //        await context.Response.WriteAsync("Hello World!");
        //      });
      });
    }
  }
}
