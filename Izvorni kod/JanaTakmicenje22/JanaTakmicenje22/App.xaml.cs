using JanaTakmicenje22.Core.Data;
using JanaTakmicenje22.Servisi;
using JanaTakmicenje22.ViewModels;
using JanaTakmicenje22.Views;
using JanaTakmicenje22.Views.Pages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Navigation;

namespace JanaTakmicenje22
{
    public partial class App : Application
    {
        public static IServiceProvider Services { get; private set; } = null!;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var services = new ServiceCollection();
            ConfigureServices(services);
            Services = services.BuildServiceProvider();
            using (var scope = Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                db.Database.EnsureCreated();
            }
            var mainWindow = Services.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(ServiceLifetime.Scoped);
            services.AddSingleton<AuthServis>();
            services.AddSingleton<NavigationServis>();
            services.AddSingleton<ChallengeServis>();

            services.AddSingleton<NoteServis>();
            services.AddSingleton<TikChatServis>();

            services.AddTransient<LoginViewModel>();
            services.AddTransient<RegisterViewModel>();
            services.AddTransient<HomeViewModel>();
            services.AddTransient<ChallengesViewModel>();
            services.AddTransient<NotesViewModel>();
            services.AddTransient<TikChatViewModel>();
            services.AddTransient<ShellViewModel>();

            services.AddTransient<LoginPage>();
            services.AddTransient<RegisterPage>();
            services.AddTransient<HomePage>();
            services.AddTransient<ChallengesPage>();
            services.AddTransient<NotesPage>();
            services.AddTransient<TikChatPage>();
            services.AddTransient<ShellView>();
            services.AddTransient<MainWindow>();
        }
    }

}
