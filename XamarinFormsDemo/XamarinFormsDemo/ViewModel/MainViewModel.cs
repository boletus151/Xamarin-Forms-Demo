namespace XamarinFormsDemo.ViewModel
{
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using GalaSoft.MvvmLight.Views;
    using Model;

    /// <summary>
    ///     This class contains properties that the main View can data bind to.
    ///     <para>
    ///         Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    ///     </para>
    ///     <para>
    ///         You can also use Blend to data bind with the tool's support.
    ///     </para>
    ///     <para>
    ///         See http://www.galasoft.ch/mvvm
    ///     </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        #region Private Fields

        private ObservableCollection<MyColor> _colorsList;
        private MyColor _selectedColor;
        private string _selectedValue;
        private ICommand _tryDebugCommand;
        private ICommand _onAppearingCommand;
        private readonly IDialogService dialogService;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IDialogService dialogService)
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}

            this.dialogService = dialogService;

            var c1 = new MyColor("White", "#FFFFFF");
            var c2 = new MyColor("Black", "#000000");

            this.ColorsList = new ObservableCollection<MyColor>
            {
                c1,
                c2
            };
            this.SelectedColor = this.ColorsList.First();
        }

        #endregion

        #region Public Properties

        public ObservableCollection<MyColor> ColorsList
        {
            get { return this._colorsList; }
            set
            {
                this._colorsList = value;
                this.RaisePropertyChanged();
            }
        }

        public ICommand OnAppearingCommand => this._onAppearingCommand ?? (this._onAppearingCommand = new RelayCommand(async () => await this.OnAppearing()));

        private async Task OnAppearing()
        {
            await this.dialogService.ShowMessage("OnAppearing", string.Empty);
        }

        public MyColor SelectedColor
        {
            get { return this._selectedColor; }
            set
            {
                this._selectedColor = value;
                this.RaisePropertyChanged();
            }
        }

        public string SelectedValue
        {
            get { return this._selectedValue; }
            set
            {
                this._selectedValue = value;
                this.RaisePropertyChanged();
            }
        }

        public ICommand TryDebugCommand => this._tryDebugCommand ?? (this._tryDebugCommand = new RelayCommand(this.TryDebug));

        #endregion

        #region Private Methods

        private void TryDebug()
        {
            Debug.WriteLine($"������The Selected Item is: {this.SelectedColor}");
        }

        #endregion
    }
}