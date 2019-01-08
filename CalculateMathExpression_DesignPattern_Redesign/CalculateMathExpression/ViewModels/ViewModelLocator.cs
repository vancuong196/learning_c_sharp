#define UI_MESSAGE
using CalculateMathExpression.DAL;
using CalculateMathExpression.Models;
using CalculateMathExpression.Utils;
using System.Collections.Generic;

namespace CalculateMathExpression.ViewModels
{
    class ViewModelLocator
    {
        public MainPageViewModel Main
        {
            get
            {
                // return ServiceLocator.Current.GetInstance<MainPageViewModel>();
                return ObjectLocatorService.Current().GetInstance<MainPageViewModel>();

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
                return ObjectLocatorService.Current().GetInstance<CalculateTabModel>();
            }
            set
            {
                SecondTabViewModel = value;
            }
        }
        public PermissionTabViewModel PermissiontabViewModel
        {
            get
            {
                //return ServiceLocator.Current.GetInstance<CalculateTabModel>();
                return ObjectLocatorService.Current().GetInstance<PermissionTabViewModel>();
            }
            set
            {
                PermissiontabViewModel = value;
            }
        }


        static ViewModelLocator()
        {
#if LOG_ONLY
            IInfomationService messageService = new InformationServiceFactory().GetInformationService("LogService");
#endif

#if UI_MESSAGE
            IInfomationService messageService = new InformationServiceFactory().GetInformationService("MessageService");
#endif
#if !LOG_ONLY && !UI_MESSAGE
            IInfomationService messageService = new InformationServiceFactory().GetInformationService("");
#endif
            IDataAccessService dataAccessService = new DataAccessService();
            MainPageViewModel mainPageViewModel = new MainPageViewModel(messageService);
            CalculateTabModel calculateTabModel = new CalculateTabModel(messageService, new ThirdPartyCalculator());
            // register Observers of MainPageViewModel.
            mainPageViewModel.AddOnDataChangeListener(calculateTabModel);
            mainPageViewModel.AddOnDataChangeListener(new InformationServiceFactory().GetInformationService("LogService") as MainPageDataChangedListener);
            // add Instance to ObjectLocator.
            ObjectLocatorService.Current().RegisterInstance<MainPageViewModel>(mainPageViewModel);
            ObjectLocatorService.Current().RegisterInstance<CalculateTabModel>(calculateTabModel);
            ObjectLocatorService.Current().RegisterInstance<IDataAccessService>(dataAccessService);
            ObjectLocatorService.Current().RegisterInstance<PermissionTabViewModel>(new PermissionTabViewModel(messageService, dataAccessService));

            
            
        



            

        }

            
    }
}