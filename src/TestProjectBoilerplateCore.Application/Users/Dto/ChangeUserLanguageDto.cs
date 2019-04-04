using System.ComponentModel.DataAnnotations;

namespace TestProjectBoilerplateCore.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}