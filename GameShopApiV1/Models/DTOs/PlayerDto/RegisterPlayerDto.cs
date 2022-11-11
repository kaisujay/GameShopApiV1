using System.ComponentModel.DataAnnotations;

namespace GameShopApiV1.Models.DTOs.PlayerDto
{
    public class RegisterPlayerDto
    {
        [Required(ErrorMessage ="Name is Required")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "User Name is Required")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        //[DataType(DataType.EmailAddress)]   //This one for screen DISPLAY
        [EmailAddress] //This is one Validates
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Confirmed Password is Required")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string? ConfirmedPassword { get; set; }

        [Required(ErrorMessage = "Date of Birth is Required")]
        public DateTime DateOfBirth { get; set; }
    }
}
