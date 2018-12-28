using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;

namespace CalculateMathExpression.ViewModels
{
    class ViewModelLocator
    {
        public MainPageViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainPageViewModel>();
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
                return ServiceLocator.Current.GetInstance<CalculateTabModel>();
            }
            set
            {
                SecondTabViewModel = value;
            }
        }


        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
           // SimpleIoc.Default.Register<IMessageService, MessageService>();
            SimpleIoc.Default.Register<MainPageViewModel>();
            SimpleIoc.Default.Register<CalculateTabModel>();
        }

            
    }
}