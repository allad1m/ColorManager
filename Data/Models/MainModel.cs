using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ColorManager.Data.Models
{
    public class MainModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion


        #region Synglton

        private static MainModel instance;
        private static object syncRoot = new Object();

        public static MainModel getInstance()
        {
            if (instance == null)
            {
                lock (syncRoot)
                {
                    if (instance == null)
                        instance = new MainModel();
                }
            }
            return instance;
        }

        #endregion


        #region Свойства класса

        private string _login;              // Логин авторизованного пользователя
        private int _amountOfRecipes;       // Количество рецептов
        private string _versionDataBase;    // Дата последнего обновления таблицы рецептов из Базы Данных
        private string _versionPriceList;   // Дата последнего обновления таблицы с прайс-листом из Базы Данных
        private string _currentProject;     // Название открытого на текущий момент проекта
        private Visibility _footerVisibility; // Видимость футера
        private Visibility _settingsVisibility; // Видимость кнопки настроек 
        private Visibility _profileVisibility;  // Видимость кнопки профиля

        public string Login
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

        public int AmountOfRecipes
        {
            get { return _amountOfRecipes; }
            set
            {
                if (_amountOfRecipes != value)
                {
                    _amountOfRecipes = value;
                    OnPropertyChanged();
                }
            }
        }

        public string VersionDataBase
        {
            get { return _versionDataBase; }
            set
            {
                if (_versionDataBase != value)
                {
                    _versionDataBase = value;
                    OnPropertyChanged();
                }
            }
        }

        public string VersionPriceList
        {
            get { return _versionPriceList; }
            set
            {
                if (_versionPriceList != value)
                {
                    _versionPriceList = value;
                    OnPropertyChanged();
                }
            }
        }

        public string CurrentProject
        {
            get { return _currentProject; }
            set
            {
                if (_currentProject != value)
                {
                    _currentProject = value;
                    OnPropertyChanged();
                }
            }
        }

        public Visibility FooterVisibility
        {
            get { return _footerVisibility; }
            set
            {
                if (_footerVisibility != value)
                {
                    _footerVisibility = value;
                    OnPropertyChanged();
                }
            }
        }

        public Visibility SettingsVisibility
        {
            get { return _settingsVisibility; }
            set
            {
                if (_settingsVisibility != value)
                {
                    _settingsVisibility = value;
                    OnPropertyChanged();
                }
            }
        }

        public Visibility ProfileVisibility
        {
            get { return _profileVisibility; }
            set
            {
                if (_profileVisibility != value)
                {
                    _profileVisibility = value;
                    OnPropertyChanged();
                }
            }
        }


        #endregion


        public MainModel()
        {
            AmountOfRecipes = 1273;
            VersionDataBase = "02.02.2024";
            VersionPriceList = "02.02.20224";
        }

    }
}
