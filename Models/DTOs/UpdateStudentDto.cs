using System.ComponentModel.DataAnnotations;

namespace BackEnd_student.Models.DTOs;

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