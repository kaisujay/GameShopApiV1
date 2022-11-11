using System.ComponentModel.DataAnnotations;

namespace GameShopApiV1.Models.DTOs.PlayerDto
{
    public class LogInPlayerDto
    {
        [Required(ErrorMessage = "User Name is Required")]
        public string? UserName { get; set; }
        
        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
