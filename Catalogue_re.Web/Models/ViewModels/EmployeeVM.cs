using System;
using System.ComponentModel.DataAnnotations;

namespace Catalogue_re.Web.Models.ViewModels
{
    public class EmployeeVM
    {
        public int Id { get; set; }

        [Display(Name = "ФИО")]
        [RegularExpression(@"^[a-zA-ZЁёӨөҮүҢңА-Яа-я ]+$", ErrorMessage = "Ввод цифр запрещен")]
        [StringLength(100, ErrorMessage = "Длина строки не должна превышать 100 символов")]
        [Required(ErrorMessage = "Заполните поле!")]
        public string FullName { get; set; }

        [Display(Name = "Кабинет")]
        [StringLength(10, ErrorMessage = "Длина строки не должна превышать 10 символов")]
        public string Room { get; set; }

        [Display(Name = "Адрес")]
        [StringLength(100, ErrorMessage = "Длина строки не должна превышать 100 символов")]
        public string Address { get; set; }

        [Display(Name = "Телефон")]
        [StringLength(100, ErrorMessage = "Длина строки не должна превышать 100 символов")]
        [Required(ErrorMessage = "Заполните поле!")]
        public string Phone { get; set; }

        [Display(Name = "Мобильный телефон")]
        [StringLength(100, ErrorMessage = "Длина строки не должна превышать 100 символов")]
        public string PersonalPhone { get; set; }

        [Display(Name = "E-mail")]
        [StringLength(50, ErrorMessage = "Длина строки не должна превышать 50 символов")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Вы ввели некорректный E-mail")]
        public string Email { get; set; }

        [Display(Name = "Skype")]
        [StringLength(30, ErrorMessage = "Длина строки не должна превышать 30 символов")]
        public string Skype { get; set; }

        [Display(Name = "Фото сотрудника")]
        [RegularExpression(@"([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.jpeg|.PNG|.JPG|.JPEG)$", ErrorMessage = "Формат файла должен быть .jpg, .png, .jpeg")]
        public string Photo { get; set; }

        [Display(Name = "Дата принятия")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DateAdoption { get; set; }

        [Display(Name = "Дата увольнения")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DateDismissal { get; set; }

        [Display(Name = "Должность")]
        [Required(ErrorMessage = "Необходимо выбрать должность!")]
        public int PositionId { get; set; }

        [Display(Name = "Отдел")]
        [Required(ErrorMessage = "Необходимо выбрать отдел!")]
        public int DepartmentId { get; set; }

        public PositionVM Position { get; set; }
        public DepartmentVM Department { get; set; }
    }
}