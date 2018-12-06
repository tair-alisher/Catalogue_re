using System.ComponentModel.DataAnnotations;

namespace Catalogue_re.Web.Models.Account
{
    public class ChangePasswordModel
    {
        public string UserId { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Старый пароль")]
        public string OldPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Новый пароль")]
        public string NewPassword { get; set; }
    }
}