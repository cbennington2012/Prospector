using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Moq;
using NUnit.Framework;
using Prospector.Presentation.Extensions;

namespace Prospector.UnitTests.Presentation.Extensions.HtmlDropDownExtensionsSpecs
{
    public class GivenAnHtmlDropDownExtensions
    {
    }

    public class WhenIGetAnEnumDropDownForAModel : GivenAnHtmlDropDownExtensions
    {
        private MvcHtmlString _result;

        [SetUp]
        public void Setup()
        {
            var vd = new ViewDataDictionary(new TestViewModel());

            var controllerContext =
                new ControllerContext(
                    new HttpContextWrapper(
                        new HttpContext(new HttpRequest(string.Empty, "http://www.notimportant.com", string.Empty),
                            new HttpResponse(new StringWriter()))),
                    new RouteData(),
                    new Mock<ControllerBase>().Object);

            var viewContext = new ViewContext(controllerContext, new Mock<IView>().Object, vd, new TempDataDictionary(),
                new Mock<TextWriter>().Object);

            var mockViewDataContainer = new Mock<IViewDataContainer>();
            mockViewDataContainer.Setup(v => v.ViewData).Returns(vd);

            var htmlHelper = new HtmlHelper<TestViewModel>(viewContext, mockViewDataContainer.Object);
            htmlHelper.EnableClientValidation(false);
            htmlHelper.EnableUnobtrusiveJavaScript(false);

            _result = htmlHelper.EnumDescriptionDropDownListFor(m => m.TestEnumValue, "--");
        }

        [Then]
        public void TheResultContainsTheEnumList()
        {
            Assert.That(_result.ToHtmlString().Contains("--"));
            Assert.That(_result.ToHtmlString().Contains("Value One"));
            Assert.That(_result.ToHtmlString().Contains("Value Two"));
        }
    }

    internal class TestViewModel
    {
        public TestEnums TestEnumValue { get; set; }
        public int TestNumericValue { get; set; }
    }

    internal enum TestEnums
    {
        [System.ComponentModel.Description("Value One")]
        ValueOne,

        [System.ComponentModel.Description("Value Two")]
        ValueTwo
    }
}