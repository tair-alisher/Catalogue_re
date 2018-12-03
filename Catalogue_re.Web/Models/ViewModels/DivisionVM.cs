using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Catalogue_re.Web.Models.ViewModels
{
    public class DivisionVM
    {
        public int Id { get; set; }

        [Display(Name = "Административное деление")]
        [RegularExpression(@"^[a-zA-ZЁёӨөҮүҢңА-Яа-я -]+$", ErrorMessage = "Ввод цифр запрещен")]
        [StringLength(100, ErrorMessage = "Длина строки не должна превышать 100 символов")]
        [Required(ErrorMessage = "Заполните поле!")]
        public string Name { get; set; }

        public ICollection<AdministrationVM> Administrations { get; set; }
    }
}