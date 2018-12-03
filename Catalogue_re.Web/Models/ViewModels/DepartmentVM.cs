using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Catalogue_re.Web.Models.ViewModels
{
    public class DepartmentVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Заполните поле!")]
        [StringLength(255, ErrorMessage = "Длина строки не должна превышать 255 символов")]
        [RegularExpression(@"^[a-zA-ZЁёӨөҮүҢңА-Яа-я , -]+$", ErrorMessage = "Ввод цифр запрещен")]
        [Display(Name = "Наименование отдела")]
        public string Name { get; set; }

        [Display(Name = "Почтовый индекс")]
        [Range(700000, 729999, ErrorMessage = "Почтовый индекс должен быть в диапазоне 700000 - 729999")]
        public int? Post { get; set; }


        [Display(Name = "Адрес")]
        [StringLength(100, ErrorMessage = "Длина строки не должна превышать 100 символов")]
        public string Address { get; set; }

        [Display(Name = "Факс")]
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

        [Display(Name = "Орган управления")]
        [Required(ErrorMessage = "Необходимо выбрать орган управления!")]
        public int AdministrationId { get; set; }

        public AdministrationVM Administration { get; set; }
        public ICollection<EmployeeVM> Employees { get; set; }
    }
}