using ColorManager.Data.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ColorManager.Data.ViewModels
{
    public class HomePageViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion


        private RelayCommand _goToColorStation;
        private RelayCommand _goToCalculator;


        public RelayCommand GoToColorStation
        {
            get
            {
                return _goToColorStation ??=
                    new RelayCommand(obj =>
                    {
                        Page page = obj as Page;
                        page.NavigationService.Navigate(new Views.ColorStantion());
                    });
            }
        }

        public RelayCommand GoToCalculator
        {
            get
            {
                return _goToCalculator ??=
                    new RelayCommand(obj =>
                    {
                        Page page = obj as Page;
                        page.NavigationService.Navigate(new Views.Calculator());
                    });
            }
        }



    }
}
