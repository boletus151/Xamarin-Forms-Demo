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

namespace XamarinFormsDemo.ViewModel
{
    using CommonServiceLocator;
    using Constants;
    using GalaSoft.MvvmLight.Ioc;
    using GalaSoft.MvvmLight.Messaging;
    using GalaSoft.MvvmLight.Views;
    using Services;
    using View;

    /// <summary>
    /// This class contains static references to all the view models in the application and provides
    /// an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            SetLocatorProvider();
        }

        #endregion

        #region Public Properties

        public DynamicListViewScrollingViewModel DynamicList => ServiceLocator.Current.GetInstance<DynamicListViewScrollingViewModel>();

        public FirstViewModel Main => ServiceLocator.Current.GetInstance<FirstViewModel>();

        public CarouselViewModel Carousel => ServiceLocator.Current.GetInstance<CarouselViewModel>();

        public PickersViewModel Pickers => ServiceLocator.Current.GetInstance<PickersViewModel>();

        public RadioButtonViewModel RadioButtonVm => ServiceLocator.Current.GetInstance<RadioButtonViewModel>();

        public HorizontalListViewModel HorizontalListVm => ServiceLocator.Current.GetInstance<HorizontalListViewModel>();

        public RegexViewModel RegexVm => ServiceLocator.Current.GetInstance<RegexViewModel>();

        #endregion

        #region Public Methods

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }

        public static ExtendedNavigationService ConfigureNavigationPages()
        {
            var nav = new ExtendedNavigationService();

            nav.Configure(AppConstants.NavigationPages.MainPage, typeof(MainPage));
            nav.Configure(AppConstants.NavigationPages.ControlTemplatePage, typeof(ControlTemplatePage));
            nav.Configure(AppConstants.NavigationPages.InfiniteScrollingPage, typeof(DynamicListViewScrollingPage));
            nav.Configure(AppConstants.NavigationPages.CarouselPage, typeof(CarouselPage));
            nav.Configure(AppConstants.NavigationPages.ObjectBindablePickerPage, typeof(ObjectBindablePickerPage));
            nav.Configure(AppConstants.NavigationPages.ToolbarWithPickerPage, typeof(ToolbarWithPickerPage));
            nav.Configure(AppConstants.NavigationPages.RadioButtonPage, typeof(RadioButtonPage));
            nav.Configure(AppConstants.NavigationPages.HorizontalListViewPage, typeof(HorizontalListViewPage));
            nav.Configure(AppConstants.NavigationPages.RegexPage, typeof(RegexPage));
            nav.Configure(AppConstants.NavigationPages.Test01Page, typeof(test01));

            return nav;
        }

        public static void RegisterInIocContainer()
        {
            SimpleIoc.Default.Register<IMessenger, Messenger>();
            SimpleIoc.Default.Register<IDialogService, DialogService>();
            var nav = ConfigureNavigationPages();
            SimpleIoc.Default.Register<INavigationService>(() => nav);

            SimpleIoc.Default.Register<CarouselViewModel>();
            SimpleIoc.Default.Register<DynamicListViewScrollingViewModel>(true);
            SimpleIoc.Default.Register<FirstViewModel>();
            SimpleIoc.Default.Register<ParentViewModel>(true);
            SimpleIoc.Default.Register<PickersViewModel>(true);
            SimpleIoc.Default.Register<RadioButtonViewModel>();
            SimpleIoc.Default.Register<HorizontalListViewModel>();
            SimpleIoc.Default.Register<RegexViewModel>();
            SimpleIoc.Default.Register<Test01ViewModel>();
        }

        public static void SetLocatorProvider()
        {
            if (!ServiceLocator.IsLocationProviderSet)
            {
                ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
                RegisterInIocContainer();
            }
        }

        #endregion
    }
}