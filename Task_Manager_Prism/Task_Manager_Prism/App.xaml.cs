using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Prism.Unity.Windows;
using Microsoft.Practices.Unity;
using Task_Manager_Prism.DatabaseAccess;

namespace Task_Manager_Prism
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : PrismUnityApplication
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            
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
