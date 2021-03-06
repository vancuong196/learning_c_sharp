﻿
using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using Taskmanager.DatabaseAccess;

namespace Taskmanager.ViewModels
{
    public class ViewModelLocator
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

        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<IDatabaseAccessService,DatabaseAccessService>();
            SimpleIoc.Default.Register<MainPageViewModel>();
        }
    }
}