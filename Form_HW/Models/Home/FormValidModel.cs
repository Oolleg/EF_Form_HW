namespace EF_Form_HW.Models.Home
{
    public class FormValidModel
    {
        public bool? IsNameValid { get; set; }
        public bool? IsLastameValid { get; set; }
        public bool? IsEmailValid { get; set; }
        public bool? IsPhoneValid { get; set; }

        public FormModel FormData { get; set; } = null!;
    }
}
