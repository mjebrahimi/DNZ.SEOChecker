using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DNZ.SEOChecker.Demo.Components
{
    public class SeoViewComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(SeoInput input)
        {
            var result = SeoChecker.Check(input.Title, input.Keyword, input.Url, input.MetaDescription, input.Text);
            return Task.FromResult((IViewComponentResult)View(result));
        }
    }

    public class SeoInput
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string Keyword { get; set; }
        public string MetaDescription { get; set; }
        public string Text { get; set; }
    }
}
