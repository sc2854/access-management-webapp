using System.ComponentModel.DataAnnotations;

namespace AccessManagementWebApp.Models
{
    /// <summary>
    /// Model for creating a new access request.
    /// </summary>
    public class CreateAccessRequestModel
    {
        /// <summary>
        /// Gets or sets the username for the access request.
        /// </summary>
        [Required]
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the function name for which access is requested.
        /// </summary>
        [Required]
        [Display(Name = "Functions")]
        public string FunctionName { get; set; } = string.Empty;
    }

    /// <summary>
    /// Model for revoking user access.
    /// </summary>
    public class RevokeAccessRequestModel
    {
        /// <summary>
        /// Gets or sets the username for the revoke request.
        /// </summary>
        [Required]
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the function name for which access is to be revoked.
        /// </summary>
        [Required]
        [Display(Name = "Functions")]
        public string FunctionName { get; set; } = string.Empty;
    }

    /// <summary>
    /// Model for changing user permissions.
    /// </summary>
    public class ChangePermissionRequestModel
    {
        /// <summary>
        /// Gets or sets the username for the permission change.
        /// </summary>
        [Required]
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the function name for which permission is to be changed.
        /// </summary>
        [Required]
        [Display(Name = "Functions")]
        public string FunctionName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the new permission level.
        /// </summary>
        [Required]
        public string NewPermission { get; set; } = string.Empty;
    }

    /// <summary>
    /// Model for reactivating user access.
    /// </summary>
    public class ReactivateAccessRequestModel
    {
        /// <summary>
        /// Gets or sets the username for the reactivation request.
        /// </summary>
        [Required]
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the function name for which access is to be reactivated.
        /// </summary>
        [Required]
        [Display(Name = "Functions")]
        public string FunctionName { get; set; } = string.Empty;
    }
}
