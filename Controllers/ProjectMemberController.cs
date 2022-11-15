using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProjectManagement.Models;
using ProjectManagementTracker.Business.Manager.Contract;
using System.Collections.Generic;

namespace ProjectManagementTracker.Controllers
{
    [Route("projectmgmt/api/v1/member")]
    [ApiController]
    public class ProjectMemberController : ControllerBase
    {
        private readonly ILogger<ProjectMemberController> _logger;
        private readonly IProjectMemberManager _iProjectMemberManager;
        public ProjectMemberController(IProjectMemberManager iProjectMemberManager, ILogger<ProjectMemberController> logger)
        {
            this._iProjectMemberManager = iProjectMemberManager;
            _logger = logger;
        }

        /// <summary>
        ///Member can view assigned task. For that he need to pass his MemberId.
        /// </summary>
        /// <param name="memberId">2585</param>
        /// <param name="PageIndex">1</param>
        /// <param name="PageSize">5</param>
        /// <param name="SortByColumn">NoOfYearExperience</param>
        /// <param name="SortOrder">desc</param>
        /// <returns></returns>
        [HttpGet, Route("list/{memberId}/{PageIndex}/{PageSize}/{SortByColumn}/{SortOrder}")]
        public Response<List<MemberAssignedTaskDetail>> ViewAssignedTask(int memberId, int PageIndex, int PageSize, string SortByColumn, string SortOrder)
        {
            string currentNamespaceAndMethod = General.getCurrentNamespaceAndMethod();
            _logger.LogInformation(string.Format("{0} : {1} - {2}", currentNamespaceAndMethod, Constants.MsgPayload, JsonConvert.SerializeObject("MemberId:" + memberId + " PageIndex:" + PageIndex + " PageSize:" + PageSize + " SortByColumn:" + SortByColumn + " SortOrder:" + SortOrder)));
            if (memberId > 0)
            {
                var response = _iProjectMemberManager.GetAssignedTask(memberId, PageIndex, PageSize, SortByColumn, SortOrder);
                _logger.LogInformation(string.Format("{0} : {1} - {2}", currentNamespaceAndMethod, Constants.MsgResponse, JsonConvert.SerializeObject(response)));
                return response;
            }
            else
            {
                return General.GenerateResponse<List<MemberAssignedTaskDetail>>(true, Constants.MsgEmptyMemberId, null);
            }
        }
    }
}
