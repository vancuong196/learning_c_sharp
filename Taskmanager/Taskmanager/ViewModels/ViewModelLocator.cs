
using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using Taskmanager.DatabaseAccess;

namespace Taskmanager.ViewModels
{
    public class ViewModelLocator
    {
        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
            set
            {
                Main = value;
            }
        }

        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<IDatabaseAccessService, FakeDatabaseAccessService>();
            SimpleIoc.Default.Register<MainViewModel>();
        }
    }
}