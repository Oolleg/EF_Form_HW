using EF_Form_HW.Models.Home;

namespace EF_Form_HW.Services.Valid
{
    public class expandingMethods
    {
      public bool validModelFildsChecker(FormValidModel validModel)
        {
            if(validModel.IsNameValid == true && validModel.IsEmailValid == true 
                && validModel.IsPhoneValid == true && validModel.IsLastameValid == true) 
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
