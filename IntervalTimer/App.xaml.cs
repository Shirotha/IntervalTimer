using System;
using System.Collections.ObjectModel;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Shirotha.IntervalTimer.View;
using Shirotha.IntervalTimer.ViewModel;
using Shirotha.Options;

namespace IntervalTimer
{
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Options options = new(e.Args);

            ConfigureServices();

            var window = Services!.GetRequiredService<TabataTimer>();

            window.DataContext = new TabataViewModel
            {
                Warmup = options.Get("-warmup", 60),
                Exercise = options.Get("-exercise", 30),
                Break = options.Get("-short-break", 10),
                LongBreak = options.Get("-long-break", 60),
                Rounds = options.Get("-rounds", 8),
                Cooldown = options.Get("-cooldown", 120),
                Excercises = new ObservableCollection<string>(options.Unnamed)
            };

            window.Show();
        }

        new public static App Current => (App)Application.Current;

        public IServiceProvider? Services { get; private set; }
        private void ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddTransient<TabataTimer>();

            Services = services.BuildServiceProvider();
        }
    }
}
