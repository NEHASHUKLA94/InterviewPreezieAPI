using System.ComponentModel.DataAnnotations;

namespace InterviewPreezieAPI
{
    public class Users
    {

        //      public long ID { get; set; }
        [Required(ErrorMessage = "EmailId is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "DisplayName is required")]
        public string DisplayName { get; set; }
    }

}