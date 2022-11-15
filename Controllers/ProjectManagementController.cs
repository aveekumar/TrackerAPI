using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProjectManagement.Models;
using ProjectManagementTracker.Business.Manager.Contract;
using System.Collections.Generic;

namespace ProjectManagementTracker.Controllers
{
    [Route("projectmgmt/api/v1/manager")]
    [ApiController]
    public class ProjectManagementController : ControllerBase
    {
        private readonly ILogger<ProjectManagementController> _loggerset;
        private readonly IProjectManagementManager _iProjectManagementManager;
        public ProjectManagementController(IProjectManagementManager iProjectManagementManager, ILogger<ProjectManagementController> loggerset)
        {
            this._iProjectManagementManager = iProjectManagementManager;
            _loggerset = loggerset;
        }

        /// <summary>
        /// Add a new member in a team.
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("add-member")]
        public Response<bool> AddNewTeamMember(TeamMember teamMember)
        {
            string currentInfo = General.getCurrentNamespaceAndMethod();
            _loggerset.LogInformation(string.Format("{0} : {1} - {2}", currentInfo, Constants.MsgPayload, JsonConvert.SerializeObject(teamMember)));
            var response = _iProjectManagementManager.AddNewTeamMember(teamMember);
            _loggerset.LogInformation(string.Format("{0} : {1} - {2}", currentInfo, Constants.MsgResponse, JsonConvert.SerializeObject(response)));
            return response;
        }

        /// <summary>
        /// By passing Manager Id user get the details.
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("list/{managerId}")]
        public Response<List<TeamMember>> GetTeamMembersByManagerId(string managerId)
        {
            string currentInfo = General.getCurrentNamespaceAndMethod();
            _loggerset.LogInformation(string.Format("{0} : {1} - {2}", currentInfo, Constants.MsgPayload, JsonConvert.SerializeObject(managerId)));

            if (!(string.IsNullOrWhiteSpace(managerId)))
            {
                var response = _iProjectManagementManager.GetTeamMembers(managerId);
                _loggerset.LogInformation(string.Format("{0} : {1} - {2}", currentInfo, Constants.MsgResponse, JsonConvert.SerializeObject(response)));
                return response;
            }
            else
            {
                return General.GenerateResponse<List<TeamMember>>(false, Constants.MsgEmptyManagerId, null);
            }
        }

        /// <summary>
        /// Assign a task to member in project.
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("assign-task")]
        public Response<bool> AssignTask(TaskDetail taskDetail)
        {
            string currentInfo = General.getCurrentNamespaceAndMethod();
            _loggerset.LogInformation(string.Format("{0} : {1} - {2}", currentInfo, Constants.MsgPayload, JsonConvert.SerializeObject(taskDetail)));
            var response = _iProjectManagementManager.AssignTask(taskDetail);
            _loggerset.LogInformation(string.Format("{0} : {1} - {2}", currentInfo, Constants.MsgResponse, JsonConvert.SerializeObject(response)));
            return response;
        }

        /// <summary>
        /// Update the allocation percentage to member
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("update/{projectId}/{memberId}/{allocationPercentage}")]
        public Response<bool> UpdateMemberAllocation(string projectId, string memberId, string allocationPercentage)
        {
            string currentInfo = General.getCurrentNamespaceAndMethod();
            _loggerset.LogInformation(string.Format("{0} : {1} - {2}", currentInfo, Constants.MsgPayload, JsonConvert.SerializeObject("projectId: " + projectId + " memberId: " + memberId + " allocationPercentage:" + allocationPercentage)));
            var response = _iProjectManagementManager.UpdateTeamMemberAllocation(projectId, memberId, allocationPercentage);
            _loggerset.LogInformation(string.Format("{0} : {1} - {2}", currentInfo, Constants.MsgResponse, JsonConvert.SerializeObject(response)));
            return response;
        }
    }
}
