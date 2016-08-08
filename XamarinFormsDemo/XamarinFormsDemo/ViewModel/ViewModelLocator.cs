/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:XamarinFormsDemo"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace XamarinFormsDemo.ViewModel
{
    using Constants;
    using GalaSoft.MvvmLight.Messaging;
    using GalaSoft.MvvmLight.Views;
    using Services;
    
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            if (!ServiceLocator.IsLocationProviderSet)
            {
                this.SetLocatorProvider();
            }
        }

        public void SetLocatorProvider()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            var nav = this.ConfigureNavigationPages();

            SimpleIoc.Default.Register<INavigationService>(() => nav);
            SimpleIoc.Default.Register<IMessenger, Messenger>();
            SimpleIoc.Default.Register<IDialogService, DialogService>();

            SimpleIoc.Default.Register<MainViewModel>();
        }

        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }

        public ExtendedNavigationService ConfigureNavigationPages()
        {
            var nav = new ExtendedNavigationService();
            nav.Configure(AppConstants.NavigationPages.MainPage, typeof(MainPage));

            return nav;
        }
    }
}