#define UI_MESSAGE
using CalculateMathExpression.Utils;
namespace CalculateMathExpression.ViewModels
{
    class ViewModelLocator
    {
        public MainPageViewModel Main
        {
            get
            {
                // return ServiceLocator.Current.GetInstance<MainPageViewModel>();
                return ObjectLocator.Current().GetInstance<MainPageViewModel>();

            }
            set
            {
                Main = value;
            }
        }
        public CalculateTabModel SecondTabViewModel
        {
            get
            {
                //return ServiceLocator.Current.GetInstance<CalculateTabModel>();
                return ObjectLocator.Current().GetInstance<CalculateTabModel>();
            }
            set
            {
                SecondTabViewModel = value;
            }
        }


        static ViewModelLocator()
        {
            // ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            // SimpleIoc.Default.Register<IMessageService, MessageService>();
            // SimpleIoc.Default.Register<MainPageViewModel>();
            //  SimpleIoc.Default.Register<CalculateTabModel>();
#if LOG_ONLY
            IInfomationService messageService = new InformationServiceFactory().GetInformationService("LogService");
#endif

#if UI_MESSAGE
            IInfomationService messageService = new InformationServiceFactory().GetInformationService("MessageService");
#endif
#if !LOG_ONLY&&!UI_MESSAGE
            IInfomationService messageService = new InformationServiceFactory().GetInformationService("");
#endif

            MainPageViewModel mainPageViewModel = new MainPageViewModel(messageService);
            CalculateTabModel calculateTabModel = new CalculateTabModel(messageService);
            // register Observers of MainPageViewModel.
            mainPageViewModel.AddOnDataChangeListener(calculateTabModel);
            mainPageViewModel.AddOnDataChangeListener(LogService.GetLogger());
            // add Instance to ObjectLocator.
            ObjectLocator.Current().RegisterInstance<MainPageViewModel>(mainPageViewModel);
            ObjectLocator.Current().RegisterInstance<CalculateTabModel>(calculateTabModel);
        }

            
    }
}