using System;
using System.Net.Mail;
using System.Net;
using System.Windows;

namespace ColorManager.DataBase.Queries
{
    public class EmailSender
    {
        public static void Send(string fromAddress, string toAddress, string messageTheme, string messageText, string? filePath = null)
        {
            try
            {
                // Создаем объект сообщения. 1.Отправитель письма; 2.Получатель письма
                MailMessage message = new MailMessage(new MailAddress($"{fromAddress}"), new MailAddress(toAddress));
                // Тема письма
                message.Subject = messageTheme;
                // Текст письма
                message.Body = messageText;
                // Добавление файла в письмо при наличии пути к файлу
                if (filePath != null)
                {
                    message.Attachments.Add(new Attachment(filePath));
                }
                // Письмо представляет код html
                message.IsBodyHtml = true;
                // Адрес smtp-сервера и порт, с которого будем отправлять письмо
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                // Логин и пароль отправителя
                smtp.Credentials = new NetworkCredential("colorbasesender@gmail.com", "cfof hjse wiwj crxh");
                smtp.EnableSsl = true;
                smtp.Send(message);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Отправка письма не удалась\nОшибка: {ex}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }  
}
