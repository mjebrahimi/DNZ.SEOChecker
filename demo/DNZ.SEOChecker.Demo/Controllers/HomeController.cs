using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DNZ.SEOChecker.Demo.Components;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;

namespace DNZ.SEOChecker.Demo.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            using var httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync("https://fa.wikipedia.org/wiki/%D8%A8%D9%87%DB%8C%D9%86%D9%87%E2%80%8C%D8%B3%D8%A7%D8%B2%DB%8C_%D9%85%D9%88%D8%AA%D9%88%D8%B1_%D8%AC%D8%B3%D8%AA%D8%AC%D9%88");

            var htmlDoc = new HtmlDocument() { OptionFixNestedTags = true };
            htmlDoc.LoadHtml(html);
            var content = htmlDoc.GetElementbyId("content").InnerHtml;

            var seoInput = new SeoInput
            {
                Title = "بهینه‌سازی موتور جستجو",
                Url = "بهینه‌سازی_موتور_جستجو",
                Keyword = "SEO",
                MetaDescription = "بهینه‌سازی موتور جستجو ؛ (به انگلیسی: Search Engine Optimization)، به‌اختصار سئو (به انگلیسی: SEO)، یک روند مناسب برای بهتر دیده‌شدن یک وب‌سایت یا یک صفحه وب در نتایج جستجو طبیعی یک موتور جستجو است.",
                Text = content
            };
            return View(seoInput);
        }
    }
}
