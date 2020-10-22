using System;
using System.Collections.Generic;

namespace DNZ.SEOChecker
{
    public class SeoResult
    {
        public string Title { get; internal set; }
        public string MetaDescription { get; internal set; }
        public string Url { get; internal set; }
        public string Text { get; internal set; }
        public string Keyword { get; internal set; }
        //public string[] Keywords { get; internal set; }

        public List<SeoMessage> TitleMessages { get; internal set; }
        public List<SeoMessage> KeywordMessages { get; internal set; }
        public List<SeoMessage> UrlMessages { get; internal set; }
        public List<SeoMessage> MetaDescriptionMessages { get; internal set; }
        public List<SeoMessage> TextMessages { get; internal set; }

        public int TitleScore { get; internal set; }
        public int KeywordScore { get; internal set; }
        public int UrlScore { get; internal set; }
        public int MetaDescriptionScore { get; internal set; }
        public int TextScore { get; internal set; }
        public int SumScore { get; internal set; }
        public int TotalScore { get; internal set; }
        public int TotalTitleScore { get; internal set; }
        public int TotalKeywordScore { get; internal set; }
        public int TotalUrlScore { get; internal set; }
        public int TotalMetaDescriptionScore { get; internal set; }
        public int TotalTextScore { get; internal set; }
        public int SeoScore { get; internal set; }
        public bool? KeywordDuplicated { get; internal set; }
        public bool? TitleDuplicated { get; internal set; }
        public bool? MetaDescriptionDuplicated { get; internal set; }
        public bool? UrlDuplicated { get; internal set; }
        public List<SeoTopic> Topics { get; internal set; }
        public bool IsAcceptable { get; internal set; }
        public string BackgroundColor
        {
            get
            {
                var red = (100 - SeoScore) * 255 / 100;
                var green = SeoScore * 255 / 100;
                var blue = Math.Abs(red - green + 1) / 10;
                var r = Math.Ceiling((decimal)red);
                var g = Math.Ceiling((decimal)green);
                var b = Math.Ceiling((decimal)blue);
                return $"rgb({r}, {g}, {b})";
            }
        }
    }
}