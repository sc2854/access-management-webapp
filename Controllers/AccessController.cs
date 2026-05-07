using AccessManagementWebApp.Models;
using AccessManagementWebApp.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AccessManagementWebApp.Controllers
{
    public class AccessController : Controller
    {
        private readonly IUserAccessRepository _repository;

        public AccessController(IUserAccessRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("createAccessForm.aspx")]
        [HttpGet("Access/CreateAccessForm")]
        public IActionResult CreateAccessForm()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RequestAccess(CreateAccessRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateAccessForm", model);
            }

            var existingAccess = await _repository.CheckUserAccessAsync(model.Username, model.ApplicationId);

            if (existingAccess != null)
            {
                ModelState.AddModelError(string.Empty, "User already has access to this application.");
                return View("CreateAccessForm", model);
            }

            var created = await _repository.CreateAccessAsync(model.Username, model.ApplicationId);
            if (!created)
            {
                ModelState.AddModelError(string.Empty, "Unable to create access request.");
                return View("CreateAccessForm", model);
            }

            ViewBag.SuccessMessage = "Access request created successfully.";
            return View("CreateAccessForm");
        }

        [HttpGet("Access/RevokeAccessForm")]
        [HttpGet("revokeAccessForm.aspx")]
        public IActionResult RevokeAccessForm()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RevokeAccess(RevokeAccessRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("RevokeAccessForm", model);
            }

            var existingAccess = await _repository.CheckUserAccessAsync(model.Username, model.ApplicationId);
            if (existingAccess == null)
            {
                ModelState.AddModelError(string.Empty, "User does not have access to this application.");
                return View("RevokeAccessForm", model);
            }

            var revoked = await _repository.RevokeAccessAsync(model.Username, model.ApplicationId);
            if (!revoked)
            {
                ModelState.AddModelError(string.Empty, "Unable to revoke access.");
                return View("RevokeAccessForm", model);
            }

            ViewBag.SuccessMessage = "Access revoked successfully.";
            return View("RevokeAccessForm");
        }

        [HttpGet("Access/ChangePermissionForm")]
        [HttpGet("changePermissionForm.aspx")]
        public IActionResult ChangePermissionForm()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePermission(ChangePermissionRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("ChangePermissionForm", model);
            }

            var existingAccess = await _repository.CheckUserAccessAsync(model.Username, model.ApplicationId);
            if (existingAccess == null)
            {
                ModelState.AddModelError(string.Empty, "User does not have access to this application.");
                return View("ChangePermissionForm", model);
            }

            var updated = await _repository.UpdateAccessAsync(model.Username, model.ApplicationId, model.NewPermission);
            if (!updated)
            {
                ModelState.AddModelError(string.Empty, "Unable to update permission.");
                return View("ChangePermissionForm", model);
            }

            ViewBag.SuccessMessage = "Access permissions updated successfully.";
            return View("ChangePermissionForm");
        }

        [HttpGet("reactivateAccessForm.aspx")]
        [HttpGet("Access/ReactivateAccessForm")]
        public IActionResult ReactivateAccessForm()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ReactivateAccess(ReactivateAccessRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("ReactivateAccessForm", model);
            }

            var existingAccess = await _repository.CheckUserAccessAsync(model.Username, model.ApplicationId);
            if (existingAccess != null)
            {
                ModelState.AddModelError(string.Empty, "User already has active access to this application.");
                return View("ReactivateAccessForm", model);
            }

            var reactivated = await _repository.ReactivateAccessAsync(model.Username, model.ApplicationId);
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
