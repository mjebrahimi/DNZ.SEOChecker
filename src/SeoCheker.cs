#pragma warning disable S125 // Sections of code should not be commented out
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace DNZ.SEOChecker
{
    public static class SeoChecker
    {
        #region Constants
        private const int TitleMinLenght = 35;//**
        private const int TitleMaxLenght = 55;//**
        private const int TitleWordsMinCount = 3;
        private const int TitleWordsMaxCount = 12;
        //private const float TitleMinDensity = 1;
        //private const float TitleMaxDensity = 2;
        private const float TitleMatchContentDensity = 60;//**
        private const float TitleMatchMetaDescriptionDensity = 40;//**

        private const int MetaDescriptionMinLenght = 50;//**
        private const int MetaDescriptionMaxLenght = 155;//**
        private const int MetaDescriptionWordsMinCount = 10;
        private const int MetaDescriptionWordsMaxCount = 40;
        //private const float MetaDescriptionKeywordMinDensity = 10;
        //private const float MetaDescriptionKeywordMaxDensity = 20;
        private const float MetaDescriptionStopWordsMaxDensity = 35;//**
        //private const int MetaDescriptionSentencesWordsMinCount = 6;
        private const int MetaDescriptionSentencesWordsMaxCount = 20; //**
        private const float MetaDescriptionMatchContentDensity = 60;

        private const int TextMinLenght = 1500;//**
        private const int TextMaxLenght = 10000;
        private const int TextWordsMinCount = 300; //500 
        private const int TextWordsMaxCount = 2000; //1500
        private const float TextKeywordMinDensity = 1; //**
        private const float TextKeywordMaxDensity = 2.5F;//5
        private const float TextStopWordsMaxDensity = 30;//**
        //private const int TextSentencesMinCount = 10;
        //private const int TextSentencesMaxCount = 100;
        //private const int TextSentencesMinLenght = 10;
        //private const int TextSentencesMaxLenght = 100;
        //private const int TextSentencesWordsMinCount = 5;
        private const int TextSentencesWordsAvgCount = 20; //**
        private const int TextSentencesWordsMaxCount = 35;
        private const float TextSentencesWordsMaxCountDensity = 25; //**
        private const int TextParagraphWordsMinCount = 40; //**
        private const int TextParagraphWordsMaxCount = 150; //**
        //private const int TextParagraphMinCount = 1; //********************************
        //private const int TextParagraphMaxCount = 50; //********************************
        private const int TextLinksMinCount = 1;//**
        private const int TextLinksMaxCount = 100;//150
        private const int TextSubheadingMinLenght = 5;
        private const int TextSubheadingMaxLenght = 30; //**
        private const float TextSentencesTransitionWordsMinDensity = 20; //**

        private const int UrlMinLenght = 10; //**
        private const int UrlMaxLenght = 40; //**
        private const int UrlWordsMinCount = 2;
        private const int UrlWordsMaxCount = 10;
        private const float UrlStopWordsMaxDensity = 0; // max 20
        private const float UrlMatchContentDensity = 60;
        private const float UrlMatchMetaDescriptionDensity = 60;
        private const int MinValidScore = 70;

        private static readonly string[] StopWords = {
        "یک", "درباره", "بالا", "بعد", "مجدد", "مقابل", "همه", "هستم", "و", "هر", "هست", "مثل", "در", "است", "چون", "بوده", "قبل",
        "بودن", "پایین", "میان", "هردو", "اما", "ولی", "توسط", "می توانست", "کرد", "کردن", "کرده", "انجام دادن",
        "طی", "کمی", "برای", "از", "جلوتر", "داشت", "دارد", "داره", "داشتن", "او", "می خواهد", "اینجا",
        "اینجاست", "برای او", "خودش", "مال او", "چگونه", "چطور", "من", "می توانستم", "می خواهم", "دارن", "داریم", "دارند", "دارید",
        "دارم", "اگر", "داخل", "درون", "آن", "آن ها", "هستش", "بیایید", "بیشتر", "بیشترین", "خودم",
        "نه تنها", "یکبار", "فقط", "یا", "دیگر", "خواست", "ما", "برای ما", "خودمان", "بیرون", "روی", "خاصه",
        "باید", "بنابراین", "بعضی", "مانند", "نسبت به", "ان", "آنها", "مال آنها", "خودشون", "سپس",
        "اونها", "اون", "داشتند", "می خواهند", "هستند", "این", "از طریق", "به", "خیلی", "زیر", "تا", "بودیم", "می خوایم",
        "بود", "بودین", "بودید", "داشتیم", "می خواهیم", "هستیم", "داشتین", "داشتید", "بودند", "چی", "زمانی", "زمانی که",
        "جایی", "جایی که", "بطوری که", "هنگامی که", "وقتی که", "کس", "کسی", "کسی که", "چرا", "چرا که", "همراه", "می شد", "شما",
        "کردید", "کردین", "می خواهید", "هستید", "هستین", "مال شما", "خودتون", "خودتان", "با", "کی", "را", "رو", "که", "برابر",
        //"a", "about", "above", "after", "again", "against", "all", "am", "an", "and", "any", "are", "as", "at", "be", "because", 
        //"been", "before", "being", "below", "between", "both", "but", "by", "could", "did", "do", "does", "doing", "down", 
        //"during", "each", "few", "for", "from", "further", "had", "has", "have", "having", "he", "he'd", "he'll", "he's", "her", 
        //"here", "here's", "hers", "herself", "him", "himself", "his", "how", "how's", "i", "i'd", "i'll", "i'm", "i've", "if", "in", 
        //"into", "is", "it", "it's", "its", "itself", "let's", "me", "more", "most", "my", "myself", "nor", "of", "on", "once", "only", 
        //"or", "other", "ought", "our", "ours", "ourselves", "out", "over", "own", "same", "she", "she'd", "she'll", "she's", 
        //"should", "so", "some", "such", "than", "that", "that's", "the", "their", "theirs", "them", "themselves", "then", 
        //"there", "there's", "these", "they", "they'd", "they'll", "they're", "they've", "this", "those", "through", 
        //"to", "too", "under", "until", "up", "very", "was", "we", "we'd", "we'll", "we're", "we've", "were", "what", "what's", 
        //"when", "when's", "where", "where's", "which", "while", "who", "who's", "whom", "why", "why's", "with", "would", 
        //"you", "you'd", "you'll", "you're", "you've", "your", "yours", "yourself", "yourselves"
        };

        private static readonly string[] TransitionWords = {
            "اتفاقا", "از این جهت", "از این رو", "از جمله", "از سوی دیگر", "البته", "اگر", "اگر نه", "اگر چه", "با این حال",
            "با این وجود", "با توجه به", "با وجود آن", "با وجود این", "باز هم", "باید", "بایستی", "بر اساس آن", "بر اساس این",
            "بر این اساس", "برای مثال", "برای نمونه", "بعلاوه", "بلکه", "بنابراین", "به این دلیل", "به جهت", "به دلیل آن که",
            "به دلیل آنکه", "به رغم", "به شرطی که", "به طور خلاصه", "به طور مشابه", "به طوری که", "به عنوان مثال", "به عنوان نمونه",
            "به منظور", "به هر جهت", "به هر حال", "به همان ترتیب", "به همین ترتیب", "به ویژه", "تا آن که", "تا آنجائیکه",
            "تا آنجایی که", "تا آنکه", "تا این که", "تا اینکه", "تا زمانی که", "تا زمانیکه", "حتی", "حتی اگر", "خلاصه", "در آن صورت",
            "در ابتدا", "در انتها", "در این صورت", "در حالی است که", "در حالی که", "در حقیقت", "در صورتی که", "در ضمن", "در عین حال",
            "در غیر آن صورت", "در غیر این صورت", "در مجموع", "در مقابل", "در مورد", "در نتیجه", "در نهایت", "در هر صورت", "در واقع",
            "در پایان", "در کل", "در یک کلام", "راستی", "زیرا", "ضمنا", "علاوه بر", "علی ایحال", "لازم است", "لازم است که", "مانند",
            "مثال", "مثلا", "مسلما", "مهمتر از", "مهمتر از آن", "مهمتر از این", "مگر آن که", "مگر آنکه", "مگر این که", "مگر اینکه",
            "نه تنها", "هر چند", "همان طور که", "همانطور که", "همچنین", "همین طور که", "همینطور که", "هنوز هم", "پس از آن",
            "پس از این", "پیش از آن", "پیش از این", "چنان که", "چنانکه", "چنین که", "چون", "چون که", "چونکه", "گرچه", "یعنی", "به عبارت دیگر"
        };
        #endregion

        public static SeoResult Check(string title, string keyword, string url, string metadesc, string text, List<SeoTopic> topics = null)
        {
            #region Initialize
            var enums = Enum.GetValues(typeof(ErrorMessage)).Cast<ErrorMessage>();
            var result = new SeoResult
            {
                TitleMessages = new List<SeoMessage>(),
                KeywordMessages = new List<SeoMessage>(),
                UrlMessages = new List<SeoMessage>(),
                MetaDescriptionMessages = new List<SeoMessage>(),
                TextMessages = new List<SeoMessage>(),

                TotalTitleScore = enums.Count(p => p.GetGroupName() == "Title"),
                TotalKeywordScore = enums.Count(p => p.GetGroupName() == "Keyword"),
                TotalUrlScore = enums.Count(p => p.GetGroupName() == "Url"),
                TotalMetaDescriptionScore = enums.Count(p => p.GetGroupName() == "MetaDescription"),
                TotalTextScore = enums.Count(p => p.GetGroupName() == "Text"),
                TotalScore = enums.Count(),

                Title = title?.CleanString(),
                Keyword = keyword?.CleanString() ?? "",
                Url = url?.CleanString() ?? "",
                MetaDescription = metadesc?.CleanString() ?? "",
                Text = text?.CleanString() ?? ""
            };
            if (topics != null)
                result.Topics = topics;
            #endregion

            CheckKeyword(result);
            CheckTitle(result);
            CheckMetaDescription(result);
            CheckUrl(result);
            CheckText(result);
            result.SumScore = result.TitleScore + result.KeywordScore + result.UrlScore + result.MetaDescriptionScore + result.TextScore;
            result.SeoScore = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal((decimal)result.SumScore / result.TotalScore * 100)));
            var hasError = result.TitleMessages.Any(p => p.IsError)
                || result.KeywordMessages.Any(p => p.IsError)
                || result.UrlMessages.Any(p => p.IsError)
                || result.MetaDescriptionMessages.Any(p => p.IsError)
                || result.TextMessages.Any(p => p.IsError);
            result.IsAcceptable = !hasError && result.SeoScore > MinValidScore;

            return result;
        }

        #region Private Methods
        private static void CheckKeyword(SeoResult result)
        {
            // No focus keyword was set for this page. If you do not set a focus keyword, no score can be calculated.
            if (string.IsNullOrEmpty(result.Keyword))
            {
                result.KeywordMessages.Add(ErrorMessage.KeywordIsEmpty, true);
            }
            else
            {
                result.KeywordScore++;
                // You've used this focus keyword sonce before, be sure to make very clear which URL on your site is the most important for this keyword.
                //============================================"You've used this focus keyword times before, it's probably a good idea to read this post on cornerstone content and improve your keyword strategy.": ["شما این کلمه کلیدی کانونی را بار قبل از استفاده کرده اید . به نظر می رسد که بهتر است شما این مقاله را post on cornerstone content برای بهبود استراتژی کلمات کلیدی مطالعه کنید "],
                if ((result.Topics?.Any(p => p.Keyword == result.Keyword) == true) || result.KeywordDuplicated == true)
                    result.KeywordMessages.Add(ErrorMessage.KeywordDuplicated, false, result.Keyword); //{0}Keyword
                else
                    result.KeywordScore++;
                // The focus keyword for this page contains one or more, consider removing them. Found.
                var foundedStopWords = StopWords.Where(p => result.Keyword.ContainsIgroneCase(" " + p + " "));
                if (foundedStopWords.Any())
                    result.KeywordMessages.Add(ErrorMessage.KeywordContainStopWords, true, result.Keyword, foundedStopWords.GetSentence(), foundedStopWords.Count());//{0}Keyword - {1}StopWordsSentence - {2}StopWordsCount
                else
                    result.KeywordScore++;
            }
        }

        private static void CheckTitle(SeoResult result)
        {
            // Bad SEO score Please create a page title.
            if (string.IsNullOrEmpty(result.Title))
            {
                result.TitleMessages.Add(ErrorMessage.TitleIsEmpty, true);
            }
            else
            {
                result.TitleScore++;
                // Ok SEO score The page title contains 2 characters, which is less than the recommended minimum of 35 characters. Use the space to add keyword variations or create compelling call-to-action copy.
                if (result.Title.Length < TitleMinLenght)
                    result.TitleMessages.Add(ErrorMessage.TitleIsShort, true, result.Title, result.Title.Length, TitleMinLenght);//{0}Title - {1}TitleLenght - {2}TitleMinLenght
                else
                    result.TitleScore++;
                // Good SEO score The page title is between the 35 character minimum and the recommended 65 character maximum.
                // The page title contains x characters, which is more than the viewable limit of x characters; some words will not be visible to users in your listing.": ["عنوان صفحه شامل %3$d کاراکتر است که از ماکزیمم تعداد کاراکتر قابل مشاهده توسط کاربران یعنی  %2$d کاراکتر بیشتر است ; بعضی کلمات توسط کاربران مشاهده نخواهد شد."],
                if (result.Title.Length > TitleMaxLenght)
                    result.TitleMessages.Add(ErrorMessage.TitleIsLong, true, result.Title, result.Title.Length, TitleMaxLenght);//{0}Title - {1}TitleLenght - {2}TitleMaxLenght
                else
                    result.TitleScore++;
                //============================================
                var titleWords = result.Title.GetWords();
                if (titleWords.Length < TitleWordsMinCount)
                    result.TitleMessages.Add(ErrorMessage.TitleWordsAreFewerThan, false, result.Title, titleWords.Length, TitleWordsMinCount);//{0}Title - {1}TitleWordsCount - {2}TitleWordsMinCount
                else
                    result.TitleScore++;
                //============================================
                if (titleWords.Length > TitleWordsMaxCount)
                    result.TitleMessages.Add(ErrorMessage.TitleWordsAreMoreThan, false, result.Title, titleWords.Length, TitleWordsMaxCount);//{0}Title - {1}TitleWordsCount - {2}TitleWordsMaxCount
                else
                    result.TitleScore++;
                // Bad SEO score The focus keyword 'اپلیکیشن فروشگاهی' does not appear in the page title.
                if (!result.Title.ContainsIgroneCase(result.Keyword))
                {
                    result.TitleMessages.Add(ErrorMessage.TitleDoesNotContainFocusedKeyword, true, result.Title, result.Keyword);//{0}Title - {1}Keyword
                }
                else
                {
                    result.TitleScore++;
                    // Good SEO score The page title contains the focus keyword, at the beginning which is considered to improve rankings.
                    // The page title contains the focus keyword, but it does not appear at the beginning; try and move it to the beginning.
                    if (!result.Title.StartsWith(result.Keyword, StringComparison.OrdinalIgnoreCase))
                        result.TitleMessages.Add(ErrorMessage.TitleDoesNotStartingFocusedKeyword, false, result.Title, result.Keyword);//{0}Title - {1}Keyword
                    else
                        result.TitleScore++;
                }
                //============================================
                var pureWords = result.Title.TrimStopWords(StopWords).GetWords();
                var matchCount = pureWords.Count(p => result.MetaDescription.ContainsIgroneCase(p));
                var x = Convert.ToDecimal((decimal)matchCount / pureWords.Length * 100);
                var y = Convert.ToDecimal(TitleMatchMetaDescriptionDensity.ToString("0.00"));
                if (x < y)
                    result.TitleMessages.Add(ErrorMessage.TitleDoesNotMatchMetaDescription, false, result.Title, x.ToString("0.00"), y.ToString("0.00"));//{0}Title - {1}CurrentDensity - {2}MinDensity
                else
                    result.TitleScore++;
                //============================================
                if ((result.Topics?.Any(p => p.Title == result.Title) == true) || result.TitleDuplicated == true)
                    result.TitleMessages.Add(ErrorMessage.TitleDuplicated, true, result.Title);//{0}Title
                else
                    result.TitleScore++;
            }
        }

        private static void CheckMetaDescription(SeoResult result)
        {
            // Bad SEO score No meta description has been specified, search engines will display copy from the page instead.
            if (string.IsNullOrEmpty(result.MetaDescription))
            {
                result.MetaDescriptionMessages.Add(ErrorMessage.MetaDescriptionIsEmpty, true);
            }
            else
            {
                result.MetaDescriptionScore++;
                // Ok SEO score The meta description is under 120 characters, however up to 156 characters are available.
                if (result.MetaDescription.Length < MetaDescriptionMinLenght)
                    result.MetaDescriptionMessages.Add(ErrorMessage.MetaDescriptionIsShort, true, result.MetaDescription.Length, MetaDescriptionMinLenght);//{0}MetaDescLenght - {1}MetaDescriptionMinLenght
                else
                    result.MetaDescriptionScore++;
                // Ok SEO score The specified meta description is over 156 characters. Reducing it will ensure the entire description is visible.
                if (result.MetaDescription.Length > MetaDescriptionMaxLenght)
                    result.MetaDescriptionMessages.Add(ErrorMessage.MetaDescriptionIsLong, true, result.MetaDescription.Length, MetaDescriptionMaxLenght);//{0}MetaDescLenght - {1}MetaDescriptionMaxLenght
                else
                    result.MetaDescriptionScore++;
                //============================================
                var descWords = result.MetaDescription.GetWords();
                if (descWords.Length < MetaDescriptionWordsMinCount)
                    result.MetaDescriptionMessages.Add(ErrorMessage.MetaDescriptionWordsAreFewerThan, false, descWords.Length, MetaDescriptionWordsMinCount);//{0}MetaDescWordsCount - {1}MetaDescriptionWordsMinCount
                else
                    result.MetaDescriptionScore++;
                //============================================
                if (descWords.Length > MetaDescriptionWordsMaxCount)
                    result.MetaDescriptionMessages.Add(ErrorMessage.MetaDescriptionWordsAreMoreThan, false, descWords.Length, MetaDescriptionWordsMaxCount); //{0}MetaDescWordsCount - {1}MetaDescriptionWordsMaxCount
                else
                    result.MetaDescriptionScore++;
                // Bad SEO score A meta description has been specified, but it does not contain the focus keyword. 
                // Good SEO score The meta description contains the focus keyword.
                if (!result.MetaDescription.ContainsIgroneCase(result.Keyword))
                    result.MetaDescriptionMessages.Add(ErrorMessage.MetaDescriptionDoesNotContainFocusedKeyword, true, result.Keyword); //{0}Keywords
                else
                    result.MetaDescriptionScore++;
                // Good SEO score The meta description contains no sentences over 20 words.
                var metaDescSentences = result.MetaDescription.Split('.', '\n');
                var longSentences = metaDescSentences.Where(p => p.GetWords().Length > MetaDescriptionSentencesWordsMaxCount);
                if (longSentences.Any())
                    result.MetaDescriptionMessages.Add(ErrorMessage.MetaDescriptionContainSentencesMoreThanWords, false, metaDescSentences.Length, longSentences.Count(), MetaDescriptionSentencesWordsMaxCount);//{0}MetaDescSentencesCount - {1}MaxWordsSentencesCount - {2}MetaDescriptionSentencesWordsMaxCount
                else
                    result.MetaDescriptionScore++;
                //============================================
                var stopWordsRepeats = result.MetaDescription.GetWords().Except(result.MetaDescription.TrimStopWords(StopWords).GetWords());
                var x = Convert.ToDecimal((decimal)stopWordsRepeats.Count() / result.MetaDescription.GetWords().Length * 100);
                var y = Convert.ToDecimal(MetaDescriptionStopWordsMaxDensity.ToString("0.00"));
                if (x > y)
                    result.MetaDescriptionMessages.Add(ErrorMessage.MetaDescriptionContainMoreThanStopWords, false, x.ToString("0.00"), y, stopWordsRepeats.GetSentence());//{0}CurrentDensity - {1}MaxDensity - {2}StopWordsSentence
                else
                    result.MetaDescriptionScore++;
                //============================================
                if ((result.Topics?.Any(p => p.MetaDescription == result.MetaDescription) == true) || result.MetaDescriptionDuplicated == true)
                    result.MetaDescriptionMessages.Add(ErrorMessage.MetaDescriptionDuplicated, true);
                else
                    result.MetaDescriptionScore++;
            }
        }

        private static void CheckUrl(SeoResult result)
        {
            if (string.IsNullOrEmpty(result.Url))
            {
                result.UrlMessages.Add(ErrorMessage.UrlIsEmpty, true);
            }
            else
            {
                result.UrlScore++;
                //============================================
                if (result.Url.Length < UrlMinLenght)
                    result.UrlMessages.Add(ErrorMessage.UrlIsShort, true, result.Url, result.Url.Length, UrlMinLenght);//{0}UrlLenght - {1}UrlMinLenght
                else
                    result.UrlScore++;
                // The slug for this page is a bit long, consider shortening it
                if (result.Url.Length > UrlMaxLenght)
                    result.UrlMessages.Add(ErrorMessage.UrlIsLong, true, result.Url, result.Url.Length, UrlMaxLenght);//{0}UrlLenght - {1}UrlMaxLenght
                else
                    result.UrlScore++;
                //============================================
                var url = result.Url.Replace('-', ' ');
                var urlWords = url.GetWords();
                if (urlWords.Length < UrlWordsMinCount)
                    result.UrlMessages.Add(ErrorMessage.UrlWordsAreFewerThan, false, result.Url, urlWords.Length, UrlWordsMinCount);//{0}UrlWordsCount - {1}UrlWordsMinCount
                else
                    result.UrlScore++;
                //============================================
                if (urlWords.Length > UrlWordsMaxCount)
                    result.UrlMessages.Add(ErrorMessage.UrlWordsAreMoreThan, false, result.Url, urlWords.Length, UrlWordsMaxCount);//{0}UrlWordsCount - {1}UrlWordsMaxCount
                else
                    result.UrlScore++;
                // Good SEO score The focus keyword appears in the URL for this page.
                // Ok SEO score The focus keyword does not appear in the URL for this page. If you decide to rename the URL be sure to check the old URL 301 redirects to the new one!
                if (!url.ContainsIgroneCase(result.Keyword))
                    result.UrlMessages.Add(ErrorMessage.UrlDoesNotContainFocusKeyword, true, result.Url, result.Keyword);//{0}Url - {1}Keyword
                else
                    result.UrlScore++;
                // The slug for this page contains one or more stop words, consider removing them.
                {
                    var stopWordsRepeats = url.GetWords().Except(url.TrimStopWords(StopWords).GetWords());
                    var x = Convert.ToDecimal((decimal)stopWordsRepeats.Count() / urlWords.Length * 100);
                    var y = Convert.ToDecimal(UrlStopWordsMaxDensity.ToString("0.00"));
                    if (x > y)
                        result.UrlMessages.Add(ErrorMessage.UrlContainMoreThanStopWords, true, x.ToString("0.00"), y, stopWordsRepeats.GetSentence());//{0}CurrentDensity - {1}MaxDensity - {2}StopWordsSentence
                    else
                        result.UrlScore++;
                }
                //============================================
                {
                    var pureWords = url.TrimStopWords(StopWords).GetWords();
                    var matchCount = pureWords.Count(p => result.MetaDescription.ContainsIgroneCase(p));
                    var x = Convert.ToDecimal((decimal)matchCount / pureWords.Length * 100);
                    var y = Convert.ToDecimal(UrlMatchMetaDescriptionDensity.ToString("0.00"));
                    if (x < y)
                        result.UrlMessages.Add(ErrorMessage.UrlDoesNotMatchMetaDescription, false, result.Url, x.ToString("0.00"), y.ToString("0.00"));//{0}Url - {1}CurrentDensity - {2}MinDensity
                    else
                        result.UrlScore++;
                }
                //============================================
                if ((result.Topics?.Any(p => p.Url == result.Url) == true) || result.UrlDuplicated == true)
                    result.UrlMessages.Add(ErrorMessage.UrlDuplicated, true, result.Url);//{0}Url
                else
                    result.UrlScore++;
            }
        }

        private static void CheckText(SeoResult result)
        {
            if (string.IsNullOrEmpty(result.Text))
            {
                result.TextMessages.Add(ErrorMessage.TextIsEmpty, true);
            }
            else
            {
                result.TextScore++;
                var htmlDoc = new HtmlDocument()
                {
                    OptionFixNestedTags = true
                };
                htmlDoc.LoadHtml(result.Text);
                //============================================
                var errCount = (htmlDoc.ParseErrors?.Count()) ?? 0;
                if (errCount > 0 || htmlDoc.DocumentNode == null)
                {
                    result.TextMessages.Add(ErrorMessage.TextHasHtmlParseError, false, errCount);//{0}HtmlParseErrorCount
                }
                else
                {
                    result.TextScore++;
                    var innerText = htmlDoc.DocumentNode.InnerText;
                    if (string.IsNullOrEmpty(innerText))
                    {
                        result.TextMessages.Add(ErrorMessage.TextHasHtmlNotText, false);
                    }
                    else
                    {
                        result.TextScore++;
                        // Bad SEO score The text contains 49 words. This is far too low and should be increased.
                        if (innerText.Length < TextMinLenght)
                            result.TextMessages.Add(ErrorMessage.TextIsShort, true, innerText.Length, TextMinLenght); //{0}TextLenght - {1}TextMinLenght
                        else
                            result.TextScore++;
                        //============================================
                        if (innerText.Length > TextMaxLenght)
                            result.TextMessages.Add(ErrorMessage.TextIsLong, true, innerText.Length, TextMaxLenght); //{0}TextLenght - {1}TextMaxLenght
                        else
                            result.TextScore++;
                        // Good SEO score The text contains 323 words. This is more than the 300 word recommended minimum.
                        // The text contains words, this is slightly below the word recommended minimum. Add a bit more copy.
                        var textWords = innerText.GetWords();
                        if (textWords.Length < TextWordsMinCount)
                            result.TextMessages.Add(ErrorMessage.TextWordsAreFewerThan, false, textWords.Length, TextWordsMinCount); //{0}TextWordsCount - {1}TextWordsMinCount
                        else
                            result.TextScore++;
                        //============================================
                        if (textWords.Length > TextWordsMaxCount)
                            result.TextMessages.Add(ErrorMessage.TextWordsAreMoreThan, false, textWords.Length, TextWordsMaxCount); //{0}TextWordsCount - {1}TextWordsMaxCount
                        else
                            result.TextScore++;
                        //============================================
                        if (!innerText.ContainsIgroneCase(result.Keyword))
                        {
                            result.TextMessages.Add(ErrorMessage.TextDoesNotContainFocusKeyword, true, result.Keyword); //{0}Keyword
                        }
                        else
                        {
                            result.TextScore++;
                            //============================================
                            // The keyword density is 2.8%, which is over the advised 2.5% maximum; the focus keyword was found 144 times.
                            var keywordRepeats = Regex.Split(innerText, result.Keyword, RegexOptions.IgnoreCase).Length - 1;
                            var keywordDensity = Convert.ToDecimal((decimal)keywordRepeats / innerText.GetWords().Length * 100);
                            {
                                var y = Convert.ToDecimal(TextKeywordMinDensity.ToString("0.00"));
                                if (keywordDensity < y)
                                    result.TextMessages.Add(ErrorMessage.TextKeywordDensityIsLow, true, result.Keyword, keywordRepeats, keywordDensity.ToString("0.00"), y.ToString("0.00"));//{0}Keyword - {1}KeywordRepeatCount - {2}CurrentDensity - {3}MinDensity
                                else
                                    result.TextScore++;
                            }
                            //============================================
                            // Bad SEO score The keyword density is 0.0%, which is a bit low; the focus keyword was found 0 times.
                            {
                                var y = Convert.ToDecimal(TextKeywordMaxDensity.ToString("0.00"));
                                if (keywordDensity > y)
                                    result.TextMessages.Add(ErrorMessage.TextKeywordDensityIsHigh, true, result.Keyword, keywordRepeats, keywordDensity.ToString("0.00"), y.ToString("0.00"));//{0}Keyword - {1}KeywordRepeatCount - {2}CurrentDensity - {3}MaxDensity
                                else
                                    result.TextScore++;
                            }
                        }
                        //============================================
                        {
                            var stopWordsRepeats = innerText.GetWords().Except(innerText.TrimStopWords(StopWords).GetWords());
                            var x = Convert.ToDecimal((decimal)stopWordsRepeats.Count() / textWords.Length * 100);
                            var y = Convert.ToDecimal(TextStopWordsMaxDensity.ToString("0.00"));
                            if (x > y)
                                result.TextMessages.Add(ErrorMessage.TextContainMoreThanStopWords, false, x.ToString("0.00"), y, stopWordsRepeats.GetSentence()); //{0}CurrentDensity - {1}MaxDensity - {2}StopWordsSentence
                            else
                                result.TextScore++;
                        }
                        //============================================
                        // sure each page has a unique H1 tag
                        var headings = htmlDoc.DocumentNode.GetElemetns("h1");
                        if (!headings.Any())
                        {
                            result.TextMessages.Add(ErrorMessage.TextDoesNotContainHeading, true);
                        }
                        else
                        {
                            result.TextScore++;
                            //============================================
                            if (headings.Count() > 1)
                                result.TextMessages.Add(ErrorMessage.TextContainMultipleHeading, true, headings.Count()); //{0}H1Count
                            else
                                result.TextScore++;
                            //============================================
                            if (headings.FirstOrDefault()?.InnerText.ContainsIgroneCase(result.Keyword) == false)
                                result.TextMessages.Add(ErrorMessage.TextFirstHeadingDoesContainKeyword, false, result.Keyword); //{0}Keyword
                            else
                                result.TextScore++;
                        }
                        // Ok SEO score No subheading tags (like an H2) appear in the copy.
                        // Bad SEO score The text does not contain any subheadings. Add at least one subheading.
                        var subheadings = htmlDoc.DocumentNode.GetElemetns("h2", "h3", "h4", "h5", " h6");
                        if (!subheadings.Any())
                        {
                            result.TextMessages.Add(ErrorMessage.TextDoesNotContainAnySubheadings, true);
                        }
                        else
                        {
                            result.TextScore++;
                            if (!subheadings.Any(p => p.InnerText.ContainsIgroneCase(result.Keyword)))
                                result.TextMessages.Add(ErrorMessage.TextSubHeadingsDoesContainKeyword, false, result.Keyword); //{0}Keyword
                            else
                                result.TextScore++;
                            //============================================
                            var shortSubheadings = subheadings
                                .Where(p => p.InnerText.Length < TextSubheadingMinLenght).Select(p => p.InnerText)
                                .Concat(headings.Where(p => p.InnerText.Length < TextSubheadingMinLenght).Select(p => p.InnerText));
                            if (shortSubheadings.Any())
                                result.TextMessages.Add(ErrorMessage.TextSubHeadingsContainFewerThanCharacters, false, shortSubheadings.Count(), TextSubheadingMinLenght, shortSubheadings.GetSentence());//{0}HeadAndSubheadingsAreFewerLenght - {1}TextSubheadingMinLenght - {2}0Sentences
                            else
                                result.TextScore++;
                            //============================================
                            var longSubheadings = subheadings
                                .Where(p => p.InnerText.Length > TextSubheadingMaxLenght).Select(p => p.InnerText)
                                .Concat(headings.Where(p => p.InnerText.Length > TextSubheadingMaxLenght).Select(p => p.InnerText));
                            if (subheadings.Any(p => p.InnerText.Length > TextSubheadingMaxLenght) || headings.Any(p => p.InnerText.Length > TextSubheadingMaxLenght))
                                result.TextMessages.Add(ErrorMessage.TextSubHeadingsContainMoreThanCharacters, false, longSubheadings.Count(), TextSubheadingMaxLenght, longSubheadings.GetSentence());//{0}HeadAndSubheadingsAreMoreLenght - {1}TextSubheadingMaxLenght - {2}0Sentences
                            else
                                result.TextScore++;
                        }
                        //============================================
                        var pTags = htmlDoc.DocumentNode.GetElemetns("p"); //.SelectNodes("//p");
                        if (!pTags.Any())
                        {
                            result.TextMessages.Add(ErrorMessage.TextDoesNotContainAnyParagraph, false);
                        }
                        else
                        {
                            result.TextScore++;
                            // Bad SEO score The focus keyword doesn't appear in the first paragraph of the copy. Make sure the topic is clear immediately.
                            // The focus keyword appears in the first paragraph of the copy.
                            if (!pTags.First().InnerText.ContainsIgroneCase(result.Keyword))
                                result.TextMessages.Add(ErrorMessage.FirstParagraphDoesNotContainFocusedKeyword, false, result.Keyword);//{0}Keyword
                            else
                                result.TextScore++;
                            // Ok SEO score 5 of the paragraphs contain less than the recommended minimum of 40 words. Try to expand these paragraphs, or connect each of them to the previous or next paragraph.
                            var pWords = pTags.Select(p => p.InnerText.GetWords().Length).ToList();
                            var shortParagraphs = pWords.Where(p => p < TextParagraphWordsMinCount);
                            if (shortParagraphs.Any())
                                result.TextMessages.Add(ErrorMessage.ParagraphWordsAreFewerThan, false, shortParagraphs.Count(), TextParagraphWordsMinCount);//{0}ParagraphMoreWordsCount - {1}TextParagraphWordsMinCount
                            else
                                result.TextScore++;
                            //============================================
                            var longParagraphs = pWords.Where(p => p > TextParagraphWordsMaxCount);
                            if (longParagraphs.Any())
                                result.TextMessages.Add(ErrorMessage.ParagraphWordsAreMoreThan, false, longParagraphs.Count(), TextParagraphWordsMaxCount);//{0}ParagraphMoreWordsCount - {1}TextParagraphWordsMaxCount
                            else
                                result.TextScore++;
                        }
                        // Try to make shorter sentences to improve readability.
                        // Try to make shorter sentences, using less difficult words to improve readability.
                        var sentences = htmlDoc.DocumentNode.GetElemetns("p", "div").Select(p => p.InnerText.Split(new[] { ".", "<br>", "<br/>", "<br />" }, StringSplitOptions.None)).SelectMany(p => p);
                        var sentencesWords = sentences.Select(p => p.GetWords().Length).ToList();
                        //if (sentences.Where(p => p.Length < TextSentencesMinLenght).Any())
                        //    TextMessages.Add(SeoMessage.SentenceIsShort);
                        //else
                        //    TextScore++;
                        ////============================================
                        //if (sentences.Where(p => p.Length > TextSentencesMaxLenght).Any())
                        //    TextMessages.Add(SeoMessage.SentenceIsLong);
                        //else
                        //    TextScore++;
                        // 
                        var longSentences = sentencesWords.Where(p => p > TextSentencesWordsMaxCount);
                        if (longSentences.Any())
                            result.TextMessages.Add(ErrorMessage.TextSentencesContainMoreThanWords, false, longSentences.Count(), TextSentencesWordsMaxCount); //{0}MoreWordsSetencesCount - {1}TextSentencesWordsMaxCount
                        else
                            result.TextScore++;
                        // Bad SEO score 50% of the sentences contain more than 20 words, which is more than the recommended maximum of 25%. Try to shorten your sentences.
                        {
                            var scountmore = sentencesWords.Count(p => p > TextSentencesWordsAvgCount);
                            var x = Convert.ToDecimal((decimal)scountmore / sentencesWords.Count * 100);
                            var y = Convert.ToDecimal(TextSentencesWordsMaxCountDensity.ToString("0.00"));
                            if (sentencesWords.Count > 0 && x > y)
                                result.TextMessages.Add(ErrorMessage.TextAvgSentencesAreMoreThan, false, scountmore, TextSentencesWordsAvgCount, x.ToString("0.00"), y.ToString("0.00")); //{0}MoreWordsSetencesCount - {1}TextSentencesWordsAvgCount - {2}CurrentDensity - {3}MaxDensity
                            else
                                result.TextScore++;
                        }
                        // Bad SEO score 0.0% of the sentences contain a transition word or phrase, which is less than the recommended minimum of 20%.
                        {
                            var transitionSentences = sentences.Where(p => TransitionWords.Any(q => p.ContainsIgroneCase(q)));
                            var x = Convert.ToDecimal((decimal)transitionSentences.Count() / sentences.Count() * 100);
                            var y = Convert.ToDecimal(TextSentencesTransitionWordsMinDensity.ToString("0.00"));
                            if (sentences.Any() && x < y)
                                result.TextMessages.Add(ErrorMessage.TransitionSentencesAreFewerThan, false, transitionSentences.Count(), x.ToString("0.00"), y.ToString("0.00"));//{0}TransitionSentencesCount - {1}CurrentDensity - {2}MinDensity
                            else
                                result.TextScore++;
                        }
                        // Bad SEO score No images appear in this page, consider adding some as appropriate.
                        var images = htmlDoc.DocumentNode.GetElemetns("img").Where(p => p.Attributes["src"] != null).ToList();
                        if (images.Count == 0)
                        {
                            result.TextMessages.Add(ErrorMessage.TextDoesNotContainAnyImage, false);
                        }
                        else
                        {
                            result.TextScore++;
                            var altImages = images.Select(p => p.GetAttributeValue("alt", ""));
                            // Ok SEO score The images on this page are missing alt attributes.
                            var imagesWithNoAlt = altImages.Count(p => !p.HasValue());
                            if (imagesWithNoAlt > 0)
                                result.TextMessages.Add(ErrorMessage.ImagesDoesNotContainAltAttribute, true, imagesWithNoAlt, images.Count); //{0}NoAltCount - {1}ImageCount
                            else
                                result.TextScore++;
                            // The images on this page do not have alt tags containing your focus keyword.
                            if (!altImages.Any(p => p.ContainsIgroneCase(result.Keyword)))
                                result.TextMessages.Add(ErrorMessage.NoImageContainKeywordInAltAttribute, true, result.Keyword, images.Count); //{0}Keyword - {1}ImageCount
                            else
                                result.TextScore++;
                            // It's also a best practice to use keywords in the file names of images
                            if (!altImages.Any(p => Path.GetFileNameWithoutExtension(p ?? "").ContainsIgroneCase(result.Keyword)))
                                result.TextMessages.Add(ErrorMessage.NoImageContainKeywordInFielName, false, result.Keyword, images.Count); //{0}Keyword - {1}ImageCount
                            else
                                result.TextScore++;
                        }
                        // Ok SEO score No links appear in this page, consider adding some as appropriate.
                        var links = htmlDoc.DocumentNode.GetElemetns("a").Select(p => p.GetAttributeValue("href", "")).Where(p => p != "");
                        if (!links.Any())
                        {
                            result.TextMessages.Add(ErrorMessage.TextDoesNotContainAnyLinks, false);
                        }
                        else
                        {
                            result.TextScore++;
                            var linkCount = links.Count();
                            //============================================
                            if (linkCount < TextLinksMinCount)
                                result.TextMessages.Add(ErrorMessage.TextLinksAreFewerThan, false, linkCount, TextLinksMinCount); //{0}LinkCount - {1}MinCount
                            else
                                result.TextScore++;
                            //============================================
                            if (linkCount > TextLinksMaxCount)
                                result.TextMessages.Add(ErrorMessage.TextLinksAreMoreThan, false, linkCount, TextLinksMaxCount); //{0}LinkCount - {1}MaxCount
                            else
                                result.TextScore++;
                            //============================================
                            //Internal links on your own site pointing to page have keywords in anchor text, including breadcrumbs and navigational links (if possible)
                            if (!links.Any(p => p.UrlIsLocal()))
                                result.TextMessages.Add(ErrorMessage.TextDoesNotContainAnyInboundLinks, false, linkCount); //{0}LinkCount
                            else
                                result.TextScore++;
                            // No outbound links appear in this page, consider adding some as appropriate.
                            if (links.All(p => p.UrlIsLocal()))
                                result.TextMessages.Add(ErrorMessage.TextDoesNotContainAnyOutboundLinks, false, linkCount); //{0}LinkCount
                            else
                                result.TextScore++;
                        }
                        if (result.MetaDescription.HasValue())
                        {
                            //============================================
                            var pureWords = result.MetaDescription.TrimStopWords(StopWords).GetWords();
                            var matchCount = pureWords.Count(p => innerText.ContainsIgroneCase(p));
                            var x = Convert.ToDecimal((decimal)matchCount / pureWords.Length * 100);
                            var y = Convert.ToDecimal(MetaDescriptionMatchContentDensity.ToString("0.00"));
                            if (pureWords.Length > 0 && x < y)
                                result.MetaDescriptionMessages.Add(ErrorMessage.MetaDescriptionDoesNotMatchContent, true, x.ToString("0.00"), y.ToString("0.00")); //{0}CurrentDensity - {1}MinDensity
                            else
                                result.TextScore++;
                        }
                        if (result.Title.HasValue())
                        {
                            //============================================
                            var pureWords = result.Title.TrimStopWords(StopWords).GetWords();
                            var matchCount = pureWords.Count(p => innerText.ContainsIgroneCase(p));
                            var x = Convert.ToDecimal((decimal)matchCount / pureWords.Length * 100);
                            var y = Convert.ToDecimal(TitleMatchContentDensity.ToString("0.00"));
                            if (x < y)
                                result.TitleMessages.Add(ErrorMessage.TitleDoesNotMatchContent, true, result.Title, x.ToString("0.00"), y.ToString("0.00")); //{0}Title - {1}CurrentDensity - {2}MinDensity
                            else
                                result.TextScore++;
                        }
                        if (result.Url.HasValue())
                        {
                            //============================================
                            var pureWords = result.Url.Replace('-', ' ').TrimStopWords(StopWords).GetWords();
                            var matchCount = pureWords.Count(p => innerText.ContainsIgroneCase(p));
                            var x = Convert.ToDecimal((decimal)matchCount / pureWords.Length * 100);
                            var y = Convert.ToDecimal(UrlMatchContentDensity.ToString("0.00"));
                            if (x < y)
                                result.UrlMessages.Add(ErrorMessage.UrlDoesNotMatchContent, true, result.Url, x.ToString("0.00"), y.ToString("0.00")); //{0}Url - {1}CurrentDensity - {2}MinDensity
                            else
                                result.TextScore++;
                        }
                    }
                }
            }
        }
        #endregion
    }

    public class SeoTopic
    {
        public string Title { get; internal set; }
        public string Keyword { get; internal set; }
        public string MetaDescription { get; internal set; }
        public string Url { get; internal set; }
    }

    public class SeoMessage
    {
        public string Message { get; internal set; }
        public bool IsError { get; internal set; }
    }

    #region Comments
    //Search Engine Optimization (SEO) Starter Guide
    //https://support.google.com/webmasters/answer/7451184?hl=en
    //Similar Libraries
    //https://www.nuget.org/packages/SEOChecker
    //https://www.nuget.org/packages/SEOChecker.Core
    //https://www.nuget.org/packages/ErtiqaaSeoEditor/ [https://github.com/hasanShaddad/ShaddadSeoEditor]
    //https://www.nuget.org/packages/Definux.Seo/ [https://github.com/Definux/Seo]
    //https://www.nuget.org/packages/AspNetSeo.CoreMvc [https://github.com/sebnilsson/AspNetSeo]
    //https://www.nuget.org/packages/AspNetSeo [https://github.com/sebnilsson/AspNetSeo]
    //https://www.nuget.org/packages/Winton.AspNetCore.Seo [https://github.com/wintoncode/Winton.AspNetCore.Seo]
    //https://www.nuget.org/packages/SeoPack [https://github.com/oopanuga/seo-pack]
    //https://www.nuget.org/packages/SEOLib [https://github.com/TahaHachana/SEOLib]
    //https://www.nuget.org/packages/Netko.Common.Util.Seo.SeoFriendlyStringSanitizer/
    //https://www.nuget.org/packages/BioEngine.Core.Seo [https://github.com/BioWareRu/BioEngine.Core]
    //https://www.nuget.org/packages/Epiphany.SeoMetadata [https://github.com/ryanlewis/seo-metadata]
    //https://www.nuget.org/packages/Constellation.Sitecore.Seo [https://github.com/sitecorerick/constellation.sitecore.seo]
    //https://www.nuget.org/packages/UmbracoSeoVisualizer [https://github.com/enkelmedia/Umbraco-SeoVisualizer] [https://github.com/EtchUK/Umbraco7-SeoVisualizer]
    //https://www.nuget.org/packages/MarcelDigital.UmbracoExtensions.SEO [https://marceldigital.github.io/]
    //https://www.nuget.org/packages/Elision.Feature.Library.Seo [https://github.com/sitecore-elision/elision-seo]
    // 
    //Good SEO score The sentence variation score is 3.06, which is above the recommended minimum of 3. The text contains a nice combination of long and short sentences.
    //Good SEO score 0% of the words contain over 4 syllables, which is less than or equal to the recommended maximum of 10%.
    //Good SEO score The copy scores 100 in the Flesch Reading Ease test, which is considered very easy to read.
    // 
    //"This page has %1$s outbound link(s).": ["این صفحه شامل %1$s  لینک خروجی است."],
    //"This page has %2$s outbound link(s), all nofollowed.": [""],
    //"This page has %2$s nofollowed link(s) and %3$s normal outbound link(s).": ["این صفحه %2$s  لینک به صفحه بیرونی دارد که توسط موتور های جستجو دنبال نمی شوند."],
    //"You're linking to another page with the focus keyword you want this page to rank for. Consider changing that if you truly want this page to rank.": ["شما با کلمه کلیدی کانونی که می خواهید در این صفحه به خاطر آن رتبه داشته باشید ،به صفحه دیگری لینک داده اید. در صورتی که می خواهید در این صفحه رتبه خوبی داشته باشید آن را تغییر دهید."],
    //"Your keyphrase is over 10 words, a keyphrase should be shorter.": ["عبارت کلیدی بیش از 10 کلمه است ، عبارت کلیدی باید کوتاه تر باشد"],
    //Bad SEO score 1 of the subheadings is followed by less than the recommended minimum of 40 words. Consider deleting that particular subheading, or the following subheading.
    //Best practice is to open external links in a new tab/window
    //use anchor text
    #endregion
}
#pragma warning restore S125 // Sections of code should not be commented out