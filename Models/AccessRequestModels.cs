using System.ComponentModel.DataAnnotations;

namespace AccessManagementWebApp.Models
{
    public class CreateAccessRequestModel
    {
        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public int ApplicationId { get; set; }
    }

    public class RevokeAccessRequestModel
    {
        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public int ApplicationId { get; set; }
    }

    public class ChangePermissionRequestModel
    {
        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public int ApplicationId { get; set; }

        [Required]
        public string NewPermission { get; set; } = string.Empty;
    }

    public class ReactivateAccessRequestModel
    {
        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public int ApplicationId { get; set; }
    }
}
