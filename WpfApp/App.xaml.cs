using DataAccessLayer.DAO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repositories.Interface;
using Repositories;
using Services.Interface;
using Services;
using System.Configuration;
using System.Data;
using System.Windows;
using WPFApp;
using Microsoft.EntityFrameworkCore;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IConfiguration Configuration { get; }
        public IServiceProvider ServiceProvider { get; set; }

        public App()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            Configuration = builder.Build();

            var services = new ServiceCollection();
            ConfigServices(services);
            ServiceProvider = services.BuildServiceProvider();
        }

        private void ConfigServices(IServiceCollection services)
        {
            //Register Services here
            services.AddSingleton<ICustomerService, CustomerService>();
            services.AddSingleton<IBookingHistoryService, BookingHistoryService>();
            services.AddSingleton<IRoomService, RoomService>();

            //Register Repository here
            services.AddSingleton<ICustomerRepository, CustomerRepository>();
            services.AddSingleton<IBookingHistoryRepository, BookingHistoryRepository>();
            services.AddSingleton<IRoomRepository, RoomRepository>();

            //Register Dbcontext
            services.AddDbContext<FuminiHotelManagementContext>(options =>
                     options.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionString")));

            //Register Window
            services.AddTransient<LoginWindow>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var loginWindow = ServiceProvider.GetRequiredService<LoginWindow>();
            loginWindow.Show();
        }
    }

}
