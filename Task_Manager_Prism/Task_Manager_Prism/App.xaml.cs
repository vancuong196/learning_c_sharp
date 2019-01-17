using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Prism.Unity.Windows;
using Microsoft.Practices.Unity;
using Task_Manager_Prism.DatabaseAccess;
using Task_Manager_Prism.DAL;
using Task_Manager_Prism.Utils;
using Windows.UI.Core;
using Prism.Windows.AppModel;

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
            //var navMgr = SystemNavigationManager.GetForCurrentView();
            //navMgr.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
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

          //  Container.RegisterInstance<IDatabaseAccessService>(new DatabaseAccessServiceProxy());
            
            Container.RegisterInstance<IMessageService>(new MessageService());
            Container.RegisterInstance<ILoginService>(new LoginService());
            Container.RegisterInstance<IRegisterService>(new RegisterService());
            Container.RegisterType<IDatabaseAccessService, DatabaseAccessServiceProxy>();

            return base.OnInitializeAsync(args);
        }

        protected override IDeviceGestureService OnCreateDeviceGestureService()
        {
            var svc = base.OnCreateDeviceGestureService();
            svc.UseTitleBarBackButton = false;
            return svc;
        }
    }
}
