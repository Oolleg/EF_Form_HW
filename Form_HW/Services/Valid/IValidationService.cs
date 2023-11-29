namespace EF_Form_HW.Services.Valid
{
    public interface IValidationService
    {

        bool IsNameValid(string Text);
        bool IsPhoneValid(string Text);

        bool IsEmailValid(string Text);

    }
}
