using System.ComponentModel.DataAnnotations;

public class UpdateStudentDto
{
    [Required(ErrorMessage = "Ошибка! Введите ФИО")]
    public string FullName { get; set; } = "";
    
    [Required(ErrorMessage = "Ошибка! Введите группу")]
    public string Group { get; set; } = "";
    
    [Required(ErrorMessage = "Ошибка! Введите возраст")]
    [Range(16, 100, ErrorMessage = "Ошибка! Возраст должен быть от 16 до 100")]
    public int Age { get; set; }
}