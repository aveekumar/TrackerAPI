using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProjectManagement.Models;
using ProjectManagementTracker.Business.Manager.Contract;
using System.Collections.Generic;

namespace ProjectManagementTracker.Controllers
{
    [Route("projectmgmt/api/v1/project")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly ILogger<ProjectController> _logger;
        private readonly IProjectManager _iProjectManager;
        public ProjectController(IProjectManager iProjectManager, ILogger<ProjectController> logger)
        {
            this._iProjectManager = iProjectManager;
            _logger = logger;
        }

        /// <summary>
        /// Add new project. This will help us to track which team member is assigned to which project.
        /// </summary>
        /// <param name="ProjectName"></param>
        /// <returns></returns>
        [HttpPost, Route("add-project/{ProjectName}")]
        public Response<bool> AddNewProject(string ProjectName)
        {
            string currentNamespaceAndMethod = General.getCurrentNamespaceAndMethod();
            _logger.LogInformation(string.Format("{0} : {1} - {2}", currentNamespaceAndMethod, Constants.MsgPayload, JsonConvert.SerializeObject(ProjectName)));
            if (!string.IsNullOrWhiteSpace(ProjectName))
            {
                var response = _iProjectManager.AddNewProject(ProjectName);
                _logger.LogInformation(string.Format("{0} : {1} - {2}", currentNamespaceAndMethod, Constants.MsgResponse, JsonConvert.SerializeObject(response)));
                return response;
            }
            else
            {
                return General.GenerateResponse<bool>(false, Constants.MsgEmptyProjectName, false);
            }
        }

        /// <summary>
        /// Get list of projects. When Manager assign task to team member then he/she can select Project also.
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("get-all-project")]
        public Response<List<Project>> GetProject()
        {
            string currentNamespaceAndMethod = General.getCurrentNamespaceAndMethod();
            _logger.LogInformation(string.Format("{0} : {1} - {2}", currentNamespaceAndMethod, Constants.MsgPayload, string.Empty));
            var response = _iProjectManager.GetProject();
            _logger.LogInformation(string.Format("{0} : {1} - {2}", currentNamespaceAndMethod, Constants.MsgResponse, JsonConvert.SerializeObject(response)));
            return response;
        }
    }
}
