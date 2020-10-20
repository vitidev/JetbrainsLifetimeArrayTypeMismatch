using System;
using System.Windows;
using JetBrains.Lifetimes;
using LifetimeWFPLissue.DI;
using LifetimeWFPLissue.MVVM;
using Microsoft.Extensions.DependencyInjection;

namespace LifetimeWFPLissue
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static IServiceProvider _serviceProvider;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            IServiceCollection services = new ServiceCollection();

            //register windows
            services.AddTransient<MainWindow>();
            services.AddTransient<ChildWindow>();

            // register lifetimes
            services.AddScoped<LifetimeHolder>();
            services.Add(new ServiceDescriptor(typeof(Lifetime),
                p => p.GetRequiredService<LifetimeHolder>().Lifetime,
                ServiceLifetime.Scoped));

            _serviceProvider = services.BuildServiceProvider();

            //start main window
            ShowNonModal(new MainViewModel(), typeof(MainWindow));
        }

        public static void ShowNonModal(ViewModelBase viewModel, Type windowType)
        {
            var view = CreateView(_serviceProvider, windowType);
            view.DataContext = viewModel;
            view.Show();
        }

        public static Window CreateView(IServiceProvider serviceProvider, Type windowType)
        {
            var windowLifetimeDef = Lifetime.Define();

            var scope = serviceProvider.CreateScope(windowLifetimeDef.Lifetime);
            var window = (Window)scope.ServiceProvider.GetRequiredService(windowType);

            window.Closed += (_, e) =>
            {
                windowLifetimeDef.Terminate();
            };

            return window;
        }
    }
}