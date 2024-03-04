using ColorManager.Data.Models;
using System;
using System.Linq;
using System.Net;
using System.Windows;

namespace ColorManager.DataBase.Queries
{
    public class AuthorizationQuery
    {
        // IP-адрес
        public static string host = Dns.GetHostName();
        public static IPAddress[]? address = Dns.GetHostAddresses(host);
        // Одноразовый код подтверждения
        public static string securityCode;


        /// <summary>
        /// Выполняет отправку сообщения на почту
        /// </summary>
        /// <param name="email">Почта пользователя</param>
        /// <returns>Возвращает отправленный одноразовый код</returns>
        public static void SendCodeToEmail(string email)
        {
            // Генерируем одноразовый код и отправляем его на почту
            Random random = new Random();
            securityCode = random.Next(100000, 999999).ToString();

            // Код для отправки письма на почту
            string theme = "ColorBase: Одноразовый код подтверждения";
            string text = $"Код: {securityCode}. Если это письмо пришло вам случайным образом - рекомендуем сменить пароль и написать в службу технической поддержки";
            EmailSender.Send("colorbasesender@gmail.com", User.Email, theme, text);
        }

        /// <summary>
        /// Выполняет выход из текущего профиля пользователя
        /// </summary>
        /// <returns>true при успешном выходе с аккаунта/ false при не нахождении пользователя с указанным IP </returns>
        public static bool LogOut()
        {
            try
            {
                using (var db = new ApplicationContext())
                {
                    // Ищет пользователя с полученным IP и возвращает null или экземпляр класса Users
                    var user = db.Users.FirstOrDefault(u => u.IP == address[0].ToString());

                    if (user != null)
                    {
                        // Убираем из базы данных информацию об IP у нашего пользователя
                        user.IP = null;
                        User.IsAuthorizate = false;
                        db.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Color Manager: Выход из профиля", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        /// <summary>
        /// Выполняет автоматизированный вход в профиль
        /// </summary>
        /// <returns>true при успешном входе в профиль/ false при не нахождении пользователя с указанным IP</returns>
        public static bool AutoSignIn()
        {
            try
            {
                using (var db = new ApplicationContext())
                {
                    // Ищет пользователя с полученным IP и возвращает null или экземпляр класса Users
                    var user = db.Users.FirstOrDefault(u => u.IP == address[0].ToString());

                    //Если IP у пользователя null просто возвращаем false
                    if(address[0].ToString() == null) 
                        return false;
                    // Если пользователь по указанному IP найден
                    else if (user != null)
                    {
                        MainModel.getInstance().Login = user.Login;
                        User.Login = user.Login;
                        User.Email = user.Email;
                        User.IsAuthorizate = true;
                        return true;
                    }
                    // Если пользователь по указанному IP не найден
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Color Manager: Авторизация", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <param name="password">Пароль пользователя</param>
        /// <returns>true при успешной авторизации / false при неудаче. Меняет состояние отображение футера и статус авторизации пользователя</returns>
        public static bool SignIn(string login, string password)
        {
            try
            {
                using (var db = new ApplicationContext())
                {
                    // Ищет пользователя с полученным Login и возвращает null или экземпляр класса Users
                    var user = db.Users.FirstOrDefault(u => u.Login == login);

                    // Если пользователь по указанному логину найден
                    if (user != null)
                    {
                        // Проверяем пароль
                        if (user.Password == password)
                        {
                            // Передаём информацию в статический класс
                            MainModel.getInstance().Login = user.Login;
                            User.Login = user.Login;
                            User.Email = user.Email;
                            User.IsAuthorizate = true;
                            // Передаём в базу данных текущий IP-адрес
                            user.IP = address[0].ToString();
                            db.SaveChanges();
                            // Возвращаем результат авторизации
                            return true;
                        }
                        else
                        {
                            MessageBox.Show("Неверный пароль", "Color Manager: Авторизация", MessageBoxButton.OK, MessageBoxImage.Error);
                            return false;
                        }
                    }
                    // Если пользователь по указанному логину не найден
                    else
                    {
                        MessageBox.Show("Пользователя с указанным Логином не существует", "Color Manager: Авторизация", MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Color Manager: Авторизация", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        /// <summary>
        /// Выполняет проверку на возможность регистрации пользователя
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <param name="email">Почта пользователя</param>
        /// <param name="password">Пароль пользователя</param>
        /// <returns>true при возможности зарегестрироваться / false при занятых логине или почте</returns>
        public static bool CanSignUp(string login, string email, string password)
        {
            try
            {
                using (var db = new ApplicationContext())
                {
                    // Ищет пользователя у котрого Login и Email аналогичны
                    var user = db.Users.FirstOrDefault(u => u.Login == login || u.Email == email);

                    // Если пользователи найдены
                    // Возможность регистрации - отсутствует
                    if (user != null)
                    {
                        MessageBox.Show("Указанные Логин и Почта заняты", "Color Manager: Регистрация", MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }
                    // Если пользователи не найдены
                    // Возможность регистрации - присутствует
                    else
                    {
                        // Подготавливаем данные для регистрации
                        User.Login = login;
                        User.Email = email;
                        User.Password = password;
                        // Отправляем одноразовый код на почту
                        SendCodeToEmail(email);

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Color Manager: Регистрация", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        /// <summary>
        /// Выполняет проверку на возможность восстановления пароля пользователя
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <param name="email">Почта пользователя</param>
        /// <param name="password">Пароль пользователя</param>
        /// <returns>true при возможности восстановить пароль / false при не совпадающем логине</returns>
        public static bool CanRecover(string login, string email, string password)
        {
            try
            {
                using (var db = new ApplicationContext())
                {
                    var user = db.Users.FirstOrDefault(u => u.Login == login && u.Email == email);

                    if (user != null)
                    {
                        // Подготавливаем данные для регистрации
                        User.Login = login;
                        User.Email = email;
                        User.Password = password;
                        // Отправляем одноразовый код на почту
                        SendCodeToEmail(email);

                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Пользователя с указанным Логином не существует", "Color Manager: Восстановление пароля", MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Color Manager: Восстановление пароля", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        /// <summary>
        /// Выполняет регистрацию пользователя в приложении
        /// </summary>
        /// <param name="code">Одноразовый код подтверждения</param>
        public static bool SignUp(string code)
        {
            if (securityCode == code)
            {
                try
                {
                    using (var db = new ApplicationContext())
                    {
                        //Создаем пользователя
                        Users user = new Users() { Login = User.Login, Email = User.Email, Password = User.Password, Name = User.Login };
                        //Отправляем его в БД
                        db.Add(user);
                        // Сохраняем изменения
                        db.SaveChanges();

                        MessageBox.Show("Регистрация пройдена успешно. Авторизуйтесь чтобы начать работу в приложении", "Color Manager: Регистрация", MessageBoxButton.OK, MessageBoxImage.Information);

                        return true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Color Manager: Регистрация", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Неверный одноразовый код", "Color Manager: Регистрация", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        /// <summary>
        /// Выполняет Изменение почты пользователя
        /// </summary>
        /// <param name="code">Одноразовый код подтверждения</param>
        public static bool RecoverPassword(string code)
        {
            if (securityCode == code)
            {
                try
                {
                    using (var db = new ApplicationContext())
                    {
                        // Ищем пользователя
                        var user = db.Users.FirstOrDefault(u => u.Login == User.Login && u.Email == User.Email);
                        // Присваиваем новый пароль
                        user.Password = User.Password;
                        // Сохраняем изменения
                        db.SaveChanges();

                        MessageBox.Show("Пароль изменён успешно. Авторизуйтесь чтобы начать работу в приложении", "Color Manager: Восстановление пароля", MessageBoxButton.OK, MessageBoxImage.Information);

                        return true;
                    } 
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Color Manager: Восстановление пароля", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Неверный одноразовый код", "Color Manager: Восстановление пароля", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
    }
}
