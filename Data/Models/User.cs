using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorManager.Data.Models
{
    public static class User
    {
        public static string? Login;
        public static string? Email;
        public static string? Password;
        public static string? JobTitle;
        public static string? PhoneNumber;
        public static bool IsAuthorizate = false;
        public static PossibleAction UserAction;
        
        public enum PossibleAction
        {
            Registration,
            RecoverPassword
        }
    }
}
