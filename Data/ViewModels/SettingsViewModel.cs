using ColorManager.Data.Models;
using ColorManager.DataBase;
using ColorManager.DataBase.Queries;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using System.Windows.Controls;

namespace ColorManager.Data.ViewModels
{
    public class SettingsViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        public SettingsModel Model { get; set; }

        public SettingsViewModel()
        {
           Model = new SettingsModel();
           SettingsQuery.GetUserInfo(Model);
        }

        #region Kоманды ViewModel

        private RelayCommand _saveData;
        private RelayCommand _goBack;
        private RelayCommand _logOut;
        private RelayCommand _rashet;
        private RelayCommand _izgotovka;
        private RelayCommand _podbor;
        private RelayCommand _zapros;

        public RelayCommand LogOut
        {
            get
            {
                return _logOut ??=
                    new RelayCommand(obj =>
                    {
                        if (AuthorizationQuery.LogOut())
                        {
                            User.IsAuthorizate = false;
                            Page page = obj as Page;
                            page.NavigationService.Navigate(new Views.Authorization.SignInPage());
                        }
                    });
            }
        }

        public RelayCommand SaveData
        {
            get
            {
                return _saveData ??=
                    new RelayCommand(obj =>
                    {
                        //Реализовать функцию сохранения данных
                        SettingsQuery.SaveData(Model);
                    });
            }
        }

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

        //public RelayCommand Rashet
        //{
        //    get
        //    {
        //        return _pageLoad ??=
        //            new RelayCommand(obj =>
        //            {
                       
        //            });
        //    }
        //}


        //public RelayCommand Izgotovka
        //{
        //    get
        //    {
        //        return _pageLoad ??=
        //            new RelayCommand(obj =>
        //            {
                       
        //            });
        //    }
        //}

       // public RelayCommand Podbor
        //{
        //    get
        //    {
        //        return _pageLoad ??=
        //            new RelayCommand(obj =>
        //            {
                       
        //            });
        //    }
        //}

        //public RelayCommand Zapros
        //{
        //    get
        //    {
        //        return _pageLoad ??=
        //            new RelayCommand(obj =>
        //            {
                       
        //            });
        //    }
        //}




        #endregion
    }
}
