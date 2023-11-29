using Microsoft.AspNetCore.Mvc;

namespace EF_Form_HW.Models.Home
{
    public class FormModel
    {
        [FromForm(Name = "user-fistname")]
        public string FistName { get; set; } = null!;

        [FromForm(Name = "user-lastname")]
        public string LastName { get; set; } = null!;

        [FromForm(Name ="user-email")]
        public string Email { get; set; } = null!;

        [FromForm(Name = "user-phone")]
        public string Phone { get; set; } = null!;

    }
}
