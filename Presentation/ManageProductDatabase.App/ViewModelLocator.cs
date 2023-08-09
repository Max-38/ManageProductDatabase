using ManageProductDatabase.App.ViewModels;
using ManageProductDatabase.NsApplication.Interfaces;
using ManageProductDatabase.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Configuration;
using ManageProductDatabase.NsApplication;

namespace ManageProductDatabase.App
{
    public class ViewModelLocator
    {
        private static IHost host;

        public static void Init()
        {
            host = Host.CreateDefaultBuilder().ConfigureServices(services =>
            {
                services.AddSingleton<MainWindowVM>();

                services.AddSingleton<ProductRepositoryHandler>();
                services.AddSingleton<IProductRepository>(x => new ProductRepository(ConfigurationManager.ConnectionStrings["ProductDbConnectionString"].ToString()));
            }).Build();
        }

        public MainWindowVM MainWindowVM => host.Services.GetRequiredService<MainWindowVM>();
    }
}
