using System.Collections.Generic;
using System.Web.Mvc;
using NUnit.Framework;
using Prospector.Domain.Contracts.AutoMapping;
using Prospector.Domain.Contracts.Repositories;
using Prospector.Domain.Entities;
using Prospector.Presentation.ViewModels;
using Prospector.Web.Controllers;

namespace Prospector.UnitTests.Web.Controllers.SettingsControllerSpecs
{
    public class WhenIGetTheIndexView : GivenA<SettingsController, ActionResult>
    {
        private readonly SettingData _mockSettingData = new SettingData();
        private readonly SettingViewModel _mockSettingViewModel = new SettingViewModel();

        protected override void Given()
        {
            base.Given();

            GetMock<ISettingRepository>()
                .Setup(m => m.Get())
                .Returns(new List<SettingData>
                {
                    _mockSettingData
                
                });

            GetMock<IAutoMapper>()
                .Setup(m => m.Map<SettingData, SettingViewModel>(_mockSettingData))
                .Returns(_mockSettingViewModel);
        }

        protected override void When()
        {
            base.When();

            Result = Target.Index();
        }

        [Then]
        public void TheSettingRepositoryGetsTheSettingsData()
        {
            Verify<ISettingRepository>(m => m.Get());
        }

        [Then]
        public void TheAutoMapperMapsTheDataToTheViewModel()
        {
            Verify<IAutoMapper>(m => m.Map<SettingData, SettingViewModel>(_mockSettingData));
        }

        [Then]
        public void TheResultIsAViewResult()
        {
            Assert.That(Result, Is.AssignableTo<ViewResult>());
        }

        [Then]
        public void TheViewModelModelHasBeenSet()
        {
            Assert.That((Result as ViewResult).Model as List<SettingViewModel>, Is.EquivalentTo(new List<SettingViewModel> {_mockSettingViewModel}));
        }
    }
}
