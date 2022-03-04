using System.ComponentModel.DataAnnotations;

namespace KartSpace.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}