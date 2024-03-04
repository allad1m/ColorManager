using ColorManager.Data.Models;
using ColorManager.DataBase.Queries;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace ColorManager.Data.ViewModels
{
    public class AuthorizateViewModel : INotifyPropertyChanged
    {

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion


        #region Свойства ViewModel

        /* Свойства ViewModel
         * Эти данные используются лично для каждой View
         * и только после перехода на следующую страницу
         * происходит обмен данными с AuthorizateModel.
         * Соответственно все команды вызываются из 
         * AuthorizateModel с ёё данными
         */

        private string _login;
        private string _password;
        private string _email;
        private string _code;

        public string Login_TextBox
        {
            get { return _login; }
            set
            {
                if (_login != value)
                {
                    _login = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Password_TextBox
        {
            get { return _password; }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Email_TextBox
        {
            get { return _email; }
            set
            {
                if (_email != value)
                {
                    _email = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Code_TextBox
        {
            get { return _code; }
            set
            {
                if (_code != value)
                {
                    _code = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion


        #region Команды ViewModel

        private RelayCommand _signInButton;
        private RelayCommand _confirmAction;
        private RelayCommand _goToSignUpPage;
        private RelayCommand _goToConfirmPage;
        private RelayCommand _goToRecoverPage;
        private RelayCommand _goBackPage;


        public RelayCommand SignInButton
        {
            get
            {
                return _signInButton ??=
                    new RelayCommand(obj =>
                    {
                        if (AuthorizationQuery.SignIn(Login_TextBox, Password_TextBox))
                        {
                            Page page = obj as Page;
                            page.NavigationService.Navigate(new Views.HomePage());
                        }
                    }, 
                    obj =>
                    {
                        if (Login_TextBox != null && Password_TextBox != null)
                        {
                            if (Login_TextBox.Length > 0 && Password_TextBox.Length > 0)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    });
            }
        }

        public RelayCommand ConfirmAction
        {
            get
            {
                return _confirmAction ??=
                    new RelayCommand(obj =>
                    {
                        Page page = obj as Page;

                        if (User.UserAction == User.PossibleAction.Registration)
                        {
                            // Если пользователь проходил регистрацию, то вызываем метод для выполнения регистрации
                            // внутри этого метода вызываем команду для отправки почты, после чего проверяем код,
                            // который ввёл пользователь. Если коды совпадают, то выполняем регистрацию, иначе ошибка
                            if (AuthorizationQuery.SignUp(Code_TextBox))
                            {
                                page.NavigationService.Navigate(new Views.Authorization.SignInPage());
                            }
                        }
                        else if (User.UserAction == User.PossibleAction.RecoverPassword)
                        {
                            if (AuthorizationQuery.RecoverPassword(Code_TextBox))
                            {
                                page.NavigationService.Navigate(new Views.Authorization.SignInPage());
                            }
                        }
                        else
                        {
                            MessageBox.Show("Внутренняя ошибка", "Color Manager", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    },
                    obj =>
                    {
                        if (Code_TextBox != null)
                        {
                            if (Code_TextBox.Length > 0)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    });
            }
        }

        public RelayCommand GoToSignUpPage
        {
            get
            {
                return _goToSignUpPage ??=
                    new RelayCommand(obj =>
                    {
                        User.UserAction = User.PossibleAction.Registration;
                        Page page = obj as Page;
                        page.NavigationService.Navigate(new Views.Authorization.SignUpPage());
                    });
            }
        }

        public RelayCommand GoToRecoverPage
        {
            get
            {
                return _goToRecoverPage ??=
                    new RelayCommand(obj =>
                    {
                        User.UserAction = User.PossibleAction.RecoverPassword;
                        Page page = obj as Page;
                        page.NavigationService.Navigate(new Views.Authorization.RecoverPage());
                    });
            }
        }

        public RelayCommand GoToConfirmPage
        {
            get
            {
                return _goToConfirmPage ??=
                    new RelayCommand(obj =>
                    {
                        Page page = obj as Page;

                        if (User.UserAction == User.PossibleAction.Registration)
                        {
                            if (AuthorizationQuery.CanSignUp(Login_TextBox, Email_TextBox, Password_TextBox))
                            {
                                page.NavigationService.Navigate(new Views.Authorization.ConfirmationPage());
                            }
                        }
                        else if (User.UserAction == User.PossibleAction.RecoverPassword)
                        {
                            if (AuthorizationQuery.CanRecover(Login_TextBox, Email_TextBox, Password_TextBox))
                            {
                                page.NavigationService.Navigate(new Views.Authorization.ConfirmationPage());
                            }
                        }
                        else
                        {
                            MessageBox.Show("Внутренняя ошибка", "Color Manager: Регистрация", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    },
                    obj =>
                    {
                        if (Login_TextBox != null && Email_TextBox != null && Password_TextBox != null)
                        {
                            if (Login_TextBox.Length > 0 && Email_TextBox.Length > 0 && Password_TextBox.Length > 0)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    });
            }
        }

        public RelayCommand GoBackPage
        {
            get
            {
                return _goBackPage ??=
                    new RelayCommand(obj =>
                    {
                        Page page = obj as Page;
                        page.NavigationService.GoBack();
                    });
            }
        }

        #endregion

    }
}
