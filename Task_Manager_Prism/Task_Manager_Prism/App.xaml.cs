using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Prism.Unity.Windows;
using Taskmanager.DatabaseAccess;
using Microsoft.Practices.Unity;
using Task_Manager_Prism.ViewModels;
using Taskmanager.Views;

namespace Task_Manager_Prism
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : PrismUnityApplication
    {
        public App()
        {
            InitializeComponent();
            //  ExtendedSplashScreenFactory = (splashscreen) => new ExtendedSplashScreen(splashscreen);
        }

        protected override async Task OnLaunchApplicationAsync(LaunchActivatedEventArgs args)
        {
            if (args.PreviousExecutionState != ApplicationExecutionState.Running)
            {
                // Here we would load the application's resources.
                // await LoadAppResources();
            }

            NavigationService.Navigate("Main", null);
        }

        protected override Task OnInitializeAsync(IActivatedEventArgs args)
        {
            
            Container.RegisterInstance<IDatabaseAccessService>(new DatabaseAccessService());
      
            return base.OnInitializeAsync(args);
        }

        /// <summary>
        /// We use this method to simulate the loading of resources from different sources asynchronously.
        /// </summary>
        /// <returns></returns>
        private Task LoadAppResources()
        {
            return Task.Delay(1000);
        }
    }
}
