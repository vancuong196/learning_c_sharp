using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Prism.Unity.Windows;
using Microsoft.Practices.Unity;
using Task_Manager_Prism.DatabaseAccess;
using Task_Manager_Prism.DAL;
using Task_Manager_Prism.Utils;

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

            NavigationService.Navigate("Login", null);
        }

        protected override Task OnInitializeAsync(IActivatedEventArgs args)
        {

            Container.RegisterInstance<IDatabaseAccessService>(new DatabaseAccessServiceProxy());
            Container.RegisterInstance<IMessageService>(new MessageService());
            Container.RegisterInstance<ILoginService>(new LoginService());

            return base.OnInitializeAsync(args);
        }

    }
}
