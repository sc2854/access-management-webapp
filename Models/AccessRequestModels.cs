using System.ComponentModel.DataAnnotations;

namespace AccessManagementWebApp.Models
{
    public class CreateAccessRequestModel
    {
        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Functions")]
        public string FunctionName { get; set; } = string.Empty;
    }

    public class RevokeAccessRequestModel
    {
        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Functions")]
        public string FunctionName { get; set; } = string.Empty;
    }

    public class ChangePermissionRequestModel
    {
        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Functions")]
        public string FunctionName { get; set; } = string.Empty;

        [Required]
        public string NewPermission { get; set; } = string.Empty;
    }

    public class ReactivateAccessRequestModel
    {
        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Functions")]
        public string FunctionName { get; set; } = string.Empty;
    }
}
