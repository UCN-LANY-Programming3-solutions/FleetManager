using FleetManager.Desktop.Data;
using FleetManager.Desktop.Model;
using FleetManager.Desktop.Presenter;
using FleetManager.Desktop.Data.Memory;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace FleetManager.Desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            // adding a service collection to utilize dependency injection
            ServiceCollection services = new();
            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(ServiceCollection services)
        {
            // adding daos to service collection
            _ = services.AddSingleton(DaoFactory.Create<Car>(DaoFactory.DaoType.REST));
            _ = services.AddSingleton(DaoFactory.Create<Location>(DaoFactory.DaoType.REST));
            // adding presenters to service collection
            _ = services.AddSingleton<CarPresenter>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Window main = new MainWindow(_serviceProvider.GetService<CarPresenter>());
            main.Show();
        }
    }
}
