using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Catalogue_re.Web.Models.ViewModels
{
    public class AdministrationVM
    {
        public int Id { get; set; }

        [Display(Name = "Орган управления")]
        [RegularExpression(@"^[a-zA-ZЁёӨөҮүҢңА-Яа-я -]+$", ErrorMessage = "Ввод цифр запрещен")]
        [StringLength(100, ErrorMessage = "Длина строки не должна превышать 100 символов")]
        [Required(ErrorMessage = "Заполните поле!")]
        public string Name { get; set; }

        [Display(Name = "Почтовый индекс")]
        [Range(700000, 729999, ErrorMessage = "Почтовый индекс должен быть в диапазоне 700000 - 729999")]
        public int? Post { get; set; }

        [Display(Name = "Адрес")]
        [StringLength(100, ErrorMessage = "Длина строки не должна превышать 100 символов")]
        public string Addres { get; set; }

        [Display(Name = "Факс")]
        [RegularExpression(@"^[0-9 -]+$", ErrorMessage = "Ввод цифр запрещен")]
        [StringLength(12, ErrorMessage = "Длина строки не должна превышать 12 символов")]
        public string Fax { get; set; }

        [Display(Name = "E-mail")]
        [StringLength(50, ErrorMessage = "Длина строки не должна превышать 50 символов")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Вы ввели некорректный E-mail")]
        public string Email { get; set; }

        [Display(Name = "Skype")]
        [StringLength(20, ErrorMessage = "Длина строки не должна превышать 20 символов")]
        public string Skype { get; set; }

        [Display(Name = "Код")]
        [Range(0, 09999, ErrorMessage = "Код городского телефона должен быть в диапазоне 0 - 09999")]
        public int? Code { get; set; }

        [Display(Name = "Административное деление")]
        [Required(ErrorMessage = "Необходимо выбрать административное деление!")]
        public int DivisionId { get; set; }

        public DivisionVM Division { get; set; }
        public ICollection<DepartmentVM> Departments { get; set; }
    }
}