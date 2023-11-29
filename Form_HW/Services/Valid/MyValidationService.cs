using System.Text.RegularExpressions;

namespace EF_Form_HW.Services.Valid
{
    public class MyValidationService : IValidationService
    {
        public bool IsEmailValid(string text)
        {
            if (text == "" || text == null)
            {
                return false;
            }

            string pattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

            return Regex.IsMatch(text, pattern, RegexOptions.IgnoreCase);
        }

        public bool IsNameValid(string text)
        {
            if (text == "" || text == null)
            {
                return true;
            }

            string pattern = @"^[A-ZА-ЯЁЇІЄҐ'][a-zА-Яа-яЁёЇїІіЄєҐґ']*$";

            return Regex.IsMatch(text, pattern);
        }

        public bool IsPhoneValid(string text)
        {
            if (text == "" || text == null)
            {
                return true;
            }

            string pattern = @"^0(\d{2})-?\d{3}(-?\d{2}){2}$";

            return Regex.IsMatch(text, pattern);

        }
    }
}
