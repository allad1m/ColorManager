using ColorManager.Data.Models;
using ColorManager.Data.Views;
using ColorManager.DataBase.Queries;
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
    public class CalculateViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        public SettingsModel Model { get; set; }

        public CalculateViewModel()
        {
            Model = new SettingsModel();
            SettingsQuery.GetUserInfo(Model);
        }

        #region Команды ViewModel

        private RelayCommand _goBack;
        private RelayCommand _goToColorSelection;

        public RelayCommand GoBack
        {
            get
            {
                return _goBack ??=
                    new RelayCommand(obj =>
                    {
                        Page page = obj as Page;
                        page.NavigationService.GoBack();
                    });
            }
        }


        public RelayCommand GoToColorSelection
        {
            get
            {
                return _goToColorSelection ??=
                    new RelayCommand(obj =>
                    {
                        ColorSelection colorSelection = new ColorSelection();
                        colorSelection.ShowDialog();   
                    });
            }
        }
        #endregion


    }
}
