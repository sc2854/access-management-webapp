using AccessManagementWebApp.Models;
using AccessManagementWebApp.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AccessManagementWebApp.Controllers
{
    /// <summary>
    /// Controller for managing user access requests, including creating, revoking, and changing permissions.
    /// </summary>
    public class AccessController : Controller
    {
        private readonly IUserAccessRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccessController"/> class.
        /// </summary>
        /// <param name="repository">The user access repository for database operations.</param>
        public AccessController(IUserAccessRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Displays the main index page for access management.
        /// </summary>
        /// <returns>The index view.</returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Displays the form for creating a new access request.
        /// Supports legacy ASPX URL for backward compatibility.
        /// </summary>
        /// <returns>The create access form view.</returns>
        [HttpGet("createAccessForm.aspx")]
        [HttpGet("Access/CreateAccessForm")]
        public IActionResult CreateAccessForm()
        {
            return View();
        }

        /// <summary>
        /// Handles the POST request to create a new access request.
        /// </summary>
        /// <param name="model">The model containing the access request details.</param>
        /// <returns>The view with success or error messages.</returns>
        [HttpPost]
        public async Task<IActionResult> RequestAccess(CreateAccessRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateAccessForm", model);
            }

            var existingAccess = await _repository.CheckUserAccessAsync(model.Username, model.FunctionName);

            if (existingAccess != null)
            {
                ModelState.AddModelError(string.Empty, "User already has access to this function.");
                return View("CreateAccessForm", model);
            }

            var created = await _repository.CreateAccessAsync(model.Username, model.FunctionName);
            if (!created)
            {
                ModelState.AddModelError(string.Empty, "Unable to create access request.");
                return View("CreateAccessForm", model);
            }

            ViewBag.SuccessMessage = "Access request created successfully.";
            return View("CreateAccessForm");
        }

        /// <summary>
        /// Displays the form for revoking user access.
        /// Supports legacy ASPX URL for backward compatibility.
        /// </summary>
        /// <returns>The revoke access form view.</returns>
        [HttpGet("Access/RevokeAccessForm")]
        [HttpGet("revokeAccessForm.aspx")]
        public IActionResult RevokeAccessForm()
        {
            return View();
        }

        /// <summary>
        /// Handles the POST request to revoke user access.
        /// </summary>
        /// <param name="model">The model containing the revoke request details.</param>
        /// <returns>The view with success or error messages.</returns>
        [HttpPost]
        public async Task<IActionResult> RevokeAccess(RevokeAccessRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("RevokeAccessForm", model);
            }

            var existingAccess = await _repository.CheckUserAccessAsync(model.Username, model.FunctionName);
            if (existingAccess == null)
            {
                ModelState.AddModelError(string.Empty, "User does not have access to this function.");
                return View("RevokeAccessForm", model);
            }

            var revoked = await _repository.RevokeAccessAsync(model.Username, model.FunctionName);
            if (!revoked)
            {
                ModelState.AddModelError(string.Empty, "Unable to revoke access.");
                return View("RevokeAccessForm", model);
            }

            ViewBag.SuccessMessage = "Access revoked successfully.";
            return View("RevokeAccessForm");
        }

        /// <summary>
        /// Displays the form for changing user permissions.
        /// Supports legacy ASPX URL for backward compatibility.
        /// </summary>
        /// <returns>The change permission form view.</returns>
        [HttpGet("Access/ChangePermissionForm")]
        [HttpGet("changePermissionForm.aspx")]
        public IActionResult ChangePermissionForm()
        {
            return View();
        }

        /// <summary>
        /// Handles the POST request to change user permissions.
        /// </summary>
        /// <param name="model">The model containing the change permission details.</param>
        /// <returns>The view with success or error messages.</returns>
        [HttpPost]
        public async Task<IActionResult> ChangePermission(ChangePermissionRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("ChangePermissionForm", model);
            }

            var existingAccess = await _repository.CheckUserAccessAsync(model.Username, model.FunctionName);
            if (existingAccess == null)
            {
                ModelState.AddModelError(string.Empty, "User does not have access to this function.");
                return View("ChangePermissionForm", model);
            }

            var updated = await _repository.UpdateAccessAsync(model.Username, model.FunctionName, model.NewPermission);
            if (!updated)
            {
                ModelState.AddModelError(string.Empty, "Unable to update permission.");
                return View("ChangePermissionForm", model);
            }

            ViewBag.SuccessMessage = "Access permissions updated successfully.";
            return View("ChangePermissionForm");
        }

        /// <summary>
        /// Displays the form for reactivating user access.
        /// Supports legacy ASPX URL for backward compatibility.
        /// </summary>
        /// <returns>The reactivate access form view.</returns>
        [HttpGet("reactivateAccessForm.aspx")]
        [HttpGet("Access/ReactivateAccessForm")]
        public IActionResult ReactivateAccessForm()
        {
            return View();
        }

        /// <summary>
        /// Handles the POST request to reactivate user access.
        /// </summary>
        /// <param name="model">The model containing the reactivate access details.</param>
        /// <returns>The view with success or error messages.</returns>
        [HttpPost]
        public async Task<IActionResult> ReactivateAccess(ReactivateAccessRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("ReactivateAccessForm", model);
            }

            var existingAccess = await _repository.CheckUserAccessAsync(model.Username, model.FunctionName);
            if (existingAccess != null)
            {
                ModelState.AddModelError(string.Empty, "User already has active access to this function.");
                return View("ReactivateAccessForm", model);
            }

            var reactivated = await _repository.ReactivateAccessAsync(model.Username, model.FunctionName);
            if (!reactivated)
            {
                ModelState.AddModelError(string.Empty, "Unable to reactivate access.");
                return View("ReactivateAccessForm", model);
            }

            ViewBag.SuccessMessage = "Access reactivated successfully.";
            return View("ReactivateAccessForm");
        }
    }
}
