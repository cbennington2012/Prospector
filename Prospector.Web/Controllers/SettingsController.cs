using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Prospector.Domain.Contracts.AutoMapping;
using Prospector.Domain.Contracts.Repositories;
using Prospector.Domain.Entities;
using Prospector.Presentation.ViewModels;

namespace Prospector.Web.Controllers
{
    public class SettingsController : Controller
    {
        private readonly IAutoMapper _autoMapper;
        private readonly ISettingRepository _settingRepository;

        public SettingsController(IAutoMapper autoMapper, ISettingRepository settingRepository)
        {
            _autoMapper = autoMapper;
            _settingRepository = settingRepository;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var data = _settingRepository.Get();
            var models = data.Select(item => _autoMapper.Map<SettingData, SettingViewModel>(item)).ToList();

            return View(models);
        }
    }
}