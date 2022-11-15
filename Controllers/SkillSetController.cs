using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProjectManagement.Models;
using ProjectManagementTracker.Business.Manager.Contract;
using System.Collections.Generic;

namespace ProjectManagementTracker.Controllers
{
    [Route("api/skills")]
    [ApiController]
    public class SkillSetController : ControllerBase
    {
        private readonly ILogger<SkillSetController> _logger;
        private readonly ISkillSetManager _iSkillSetManager;
        public SkillSetController(ISkillSetManager iSkillSetManager, ILogger<SkillSetController> logger)
        {
            this._iSkillSetManager = iSkillSetManager;
            _logger = logger;
        }

        /// <summary>
        /// Add new Skill. This is used during adding new team member by Manager.
        /// Manager needs to select atleast 3 skill set.
        /// </summary>
        /// <param name="SkillName"></param>
        /// <returns></returns>
        [HttpGet, Route("add-skill/{SkillName}")]
        public Response<bool> AddNewSkill(string SkillName)
        {
            string currentNamespaceAndMethod = General.getCurrentNamespaceAndMethod();
            _logger.LogInformation(string.Format("{0} : {1} - {2}", currentNamespaceAndMethod, Constants.MsgPayload, JsonConvert.SerializeObject(SkillName)));
            if (!(string.IsNullOrWhiteSpace(SkillName)))
            {
                var response = _iSkillSetManager.AddNewSkill(SkillName);
                _logger.LogInformation(string.Format("{0} : {1} - {2}", currentNamespaceAndMethod, Constants.MsgResponse, JsonConvert.SerializeObject(response)));
                return response;
            }
            else
            {
                return General.GenerateResponse<bool>(false, Constants.MsgEmptySkillName, false);
            }
        }

        /// <summary>
        /// Get list of Skills.
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("get-skills")]
        public Response<List<Skillset>> GetSkillSets()
        {
            string currentNamespaceAndMethod = General.getCurrentNamespaceAndMethod();
            _logger.LogInformation(string.Format("{0} : {1} - {2}", currentNamespaceAndMethod, Constants.MsgPayload, string.Empty));
            var response = _iSkillSetManager.GetSkillsets();
            _logger.LogInformation(string.Format("{0} : {1} - {2}", currentNamespaceAndMethod, Constants.MsgResponse, JsonConvert.SerializeObject(response)));
            return response;
        }
    }
}
