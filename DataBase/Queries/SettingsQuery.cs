using ColorManager.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ColorManager.DataBase.Queries
{
    public class SettingsQuery
    {


        /// <summary>
        /// Получение данных пользователя
        /// </summary>
        /// <returns>Возвращает пользователя</returns>
        public static async Task GetUserInfo(SettingsModel model)
        {
            try
            {
                using (var db = new ApplicationContext())
                {
                    var user = db.Users.FirstOrDefault(u => u.IP == AuthorizationQuery.address[0].ToString());

                    if (user != null)
                    {
                        model.Name = user.Name;
                        model.Status = user.JobTitle;
                        model.Number = user.PhoneNumber;
                        model.Email = user.Email;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "ColorsManager: Профиль", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        /// <summary>
        /// Сохранение измененных данных
        /// </summary>
        /// <param name="name">Имя пользователя</param>
        /// <param name="jobTitle">Статус пользователя</param>
        /// <param name="number">Номер телефона пользователя</param>
        /// <param name="email">Электронная почта пользователя</param>
        public static void SaveData(SettingsModel model)
        {
            try
            {
                using (var db = new ApplicationContext())
                {
                    var user = db.Users.FirstOrDefault(u => u.IP == AuthorizationQuery.address[0].ToString());

                    if (user != null)
                    {
                        user.Name = model.Name;
                        user.JobTitle = model.Status;
                        user.PhoneNumber = model.Number;
                        user.Email = model.Email;
                        db.SaveChanges();
                    }
                    else
                    {
                        return;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "ColorsManager: Профиль", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
    }
}
