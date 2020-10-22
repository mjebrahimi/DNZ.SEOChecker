using System;

namespace DNZ.SEOChecker
{
    internal enum ErrorMessage
    {
        //Title
        [MessageInfo(GroupName = "Title", Description = "لطفا یک عنوان برای صفحه وارد کنید.")]
        TitleIsEmpty,
        [MessageInfo(GroupName = "Title", Description = "عنوان صفحه {1} کاراکتر دارد که کمتر از مقدار پیشنهادی {2} کاراکتر است.")]//{0}Title - {1}TitleLenght - {2}TitleMinLenght
        TitleIsShort,
        [MessageInfo(GroupName = "Title", Description = "عنوان صفحه {1} کاراکتر دارد که بیشتر از مقدار پیشنهادی {2} کاراکتر است.")]//{0}Title - {1}TitleLenght - {2}TitleMaxLenght
        TitleIsLong,
        [MessageInfo(GroupName = "Title", Description = "کلمه کلیدی در عنوان صفحه لحاظ نشده است.")]//{0}Title - {1}Keyword
        TitleDoesNotContainFocusedKeyword,
        [MessageInfo(GroupName = "Title", Description = "عنوان صفحه شامل کلمه کلیدی هست، اما در ابتدای عنوان قرار نگرفته، لطفا آن را ابتدای عنوان قرار دهید.")]//{0}Title - {1}Keyword
        TitleDoesNotStartingFocusedKeyword,
        [MessageInfo(GroupName = "Title", Description = "تعداد کلمات عنوان {1} عدد است که این بیشتر از مقدار پیشنهادی {2} عدد است.")]//{0}Title - {1}TitleWordsCount - {2}TitleWordsMaxCount
        TitleWordsAreMoreThan,
        [MessageInfo(GroupName = "Title", Description = "تعداد کلمات عنوان {1} عدد است که این کمتر از مقدار پیشنهادی {2} عدد است.")]//{0}Title - {1}TitleWordsCount - {2}TitleWordsMinCount
        TitleWordsAreFewerThan,
        [MessageInfo(GroupName = "Title", Description = "عنوان صفحه تکراری است و در پست های قبلی استفاده شده است.")]//{0}Title
        TitleDuplicated,
        [MessageInfo(GroupName = "Title", Description = "میزان شباهت عنوان با متاتگ توضیحات {1}% است که این کمتر از مقدار پیشنهادی {2}% است.")]//{0}Title - {1}CurrentDensity - {2}MinDensity
        TitleDoesNotMatchMetaDescription,
        //TitleContainStopWords,
        [MessageInfo(GroupName = "Title", Description = "میزان شباهت عنوان با متن {1}% است که این کمتر از مقدار پیشنهادی {2}% است.")] //{0}Title - {1}CurrentDensity - {2}MinDensity
        TitleDoesNotMatchContent,

        //Keyword
        [MessageInfo(GroupName = "Keyword", Description = "کلمه کلیدی برای این صفحه تعیین نشده است. لطفا یک کلمه کلیدی پر مخاطب که در متن شما وجود دارد را انتخاب کنید.")]
        KeywordIsEmpty,
        [MessageInfo(GroupName = "Keyword", Description = "کلمه کلیدی \"{0}\" تکراری است و در پست های قبلی استفاده شده است.")]//{0}Keyword
        KeywordDuplicated,
        [MessageInfo(GroupName = "Keyword", Description = "کلمه کلیدی شامل {2} کلمه خیلی عمومی {1} است. آنها را پاک کنید.")]//{0}Keyword - {1}StopWordsSentence - {2}StopWordsCount
        KeywordContainStopWords,

        //MetaDescription
        [MessageInfo(GroupName = "MetaDescription", Description = "متای توضیحات مشخص نشده است. موتور های جستجو قسمتی از متن صفحه را به جای آن نشان خواهند داد.")]
        MetaDescriptionIsEmpty,
        [MessageInfo(GroupName = "MetaDescription", Description = "متا تگ توضیحات تعریف شده است اما کلمه کلیدی \"{0}\" در آن دیده نمی شود.")] //{0}Keywords
        MetaDescriptionDoesNotContainFocusedKeyword,
        [MessageInfo(GroupName = "MetaDescription", Description = "{1} جمله در متاتگ توضیحات بیش از {2} کلمه دارد. لطفا آن را کاهش دهید.")]//{0}MetaDescSentencesCount - {1}MaxWordsSentencesCount - {2}MetaDescriptionSentencesWordsMaxCount
        MetaDescriptionContainSentencesMoreThanWords,
        [MessageInfo(GroupName = "MetaDescription", Description = "متاتگ توضیحات {0} کاراکتر دارد که این کمتر از مقدار پیشنهادی {1} کاراکتر است.")]//{0}MetaDescLenght - {1}MetaDescriptionMinLenght
        MetaDescriptionIsShort,
        [MessageInfo(GroupName = "MetaDescription", Description = "متاتگ توضیحات {0} کاراکتر دارد که این بیشتر از مقدار پیشنهادی {1} کاراکتر است.")]//{0}MetaDescLenght - {1}MetaDescriptionMaxLenght
        MetaDescriptionIsLong,
        [MessageInfo(GroupName = "MetaDescription", Description = "متاتگ توضیحات شامل {0} کلمه است که این بیشتر از مقدار پیشنهادی {1} است.")]//{0}MetaDescWordsCount - {1}MetaDescriptionWordsMaxCount
        MetaDescriptionWordsAreMoreThan,
        [MessageInfo(GroupName = "MetaDescription", Description = "متاتگ توضیحات شامل {0} کلمه است که این کمتر از مقدار پیشنهادی {1} است.")]//{0}MetaDescWordsCount - {1}MetaDescriptionWordsMinCount
        MetaDescriptionWordsAreFewerThan,
        [MessageInfo(GroupName = "MetaDescription", Description = "تراکم کلمات خیلی عمومی {2} متاتگ توضیحات {0}% است که این مقدار بیش از مقدار پیشنهادی {1}% است")]//{0}CurrentDensity - {1}MaxDensity - {2}StopWordsSentence
        MetaDescriptionContainMoreThanStopWords,
        [MessageInfo(GroupName = "MetaDescription", Description = "میزان شباهت متاتگ توضیحات با متن صفحه {0}% است که این کمتر از مقدار پیشنهادی {1}% است.")] //{0}CurrentDensity - {1}MinDensity
        MetaDescriptionDoesNotMatchContent,
        [MessageInfo(GroupName = "MetaDescription", Description = "متاتگ توضیحات تکراری است و در پست های قبلی استفاده شده است")]
        MetaDescriptionDuplicated,

        //Text
        [MessageInfo(GroupName = "Text", Description = "متن خالی است، لطفا متن را وارد کنید.")]
        TextIsEmpty,
        [MessageInfo(GroupName = "Text", Description = "متن {0} خطای html ایی دارد.")]//{0}HtmlParseErrorCount
        TextHasHtmlParseError,
        [MessageInfo(GroupName = "Text", Description = "متن شامل تگ های html است ولی محتوا ندارد.")]
        TextHasHtmlNotText,
        [MessageInfo(GroupName = "Text", Description = "متن {0} کاراکتر دارد که این کمتر از حداقل مقدار پیشنهادی {1} است.")]//{0}TextLenght - {1}TextMinLenght
        TextIsShort,
        [MessageInfo(GroupName = "Text", Description = "متن {0} کاراکتر دارد که این بیشتر از مقدار پیشنهادی {1} است.")] //{0}TextLenght - {1}TextMaxLenght
        TextIsLong,
        [MessageInfo(GroupName = "Text", Description = "متن شامل کلمه کلیدی کانونی نیست.")] //{0}Keyword
        TextDoesNotContainFocusKeyword,
        [MessageInfo(GroupName = "Text", Description = "متن شامل تگ تیتری(h1) نیست.")]
        TextDoesNotContainHeading,
        [MessageInfo(GroupName = "Text", Description = "متن شامل {0} تگ تیتری(h1) است، آنرا به یک عدد کاهش دهید.")] //{0}H1Count
        TextContainMultipleHeading,
        [MessageInfo(GroupName = "Text", Description = "هیچ تگ زیرتیتری مانند (H2, H3, ...) در نوشته شما نیست.")]
        TextDoesNotContainAnySubheadings,
        [MessageInfo(GroupName = "Text", Description = "اولین زیر تیتر شامل کلمه کلیدی \"{0}\" نیست")] //{0}Keyword
        TextFirstHeadingDoesContainKeyword,
        [MessageInfo(GroupName = "Text", Description = "شما در هیچ کدام از زیرتیتر ها (H2,H3,...) .کلمه کلیدی \"{0}\" استفاده نکرده اید")] //{0}Keyword
        TextSubHeadingsDoesContainKeyword,
        [MessageInfo(GroupName = "Text", Description = "{0} تیتر/زیرتیتر در متن وجود دارد که کمتر از {1} کارکتر دارد.")]//{0}HeadAndSubheadingsAreFewerLenght - {1}TextSubheadingMinLenght - {2}0Sentences
        TextSubHeadingsContainFewerThanCharacters,
        [MessageInfo(GroupName = "Text", Description = "{0} تیتر/زیرتیتر در متن وجود دارد که بیشتر از {1} کارکتر دارد.")]//{0}HeadAndSubheadingsAreMoreLenght - {1}TextSubheadingMaxLenght - {2}0Sentences
        TextSubHeadingsContainMoreThanCharacters,
        [MessageInfo(GroupName = "Text", Description = "هیچ پاراگرافی (تگ p یا div) در متن دیده نمی شود.")]
        TextDoesNotContainAnyParagraph,
        [MessageInfo(GroupName = "Text", Description = "هیچ لینکی (تگ a) در متن دیده نمی شود.")]
        TextDoesNotContainAnyLinks,
        [MessageInfo(GroupName = "Text", Description = "{0} لینک در متن وجود دارد که این مقدار کمتر از حداقل مقدار پیشنهادی {1} است.")]//{0}LinkCount - {1}MinCount
        TextLinksAreFewerThan,
        [MessageInfo(GroupName = "Text", Description = "{0} لینک در متن وجود دارد که این مقدار بیشتر از مقدار پیشنهادی {1} است.")]//{0}LinkCount - {1}MaxCount
        TextLinksAreMoreThan,
        [MessageInfo(GroupName = "Text", Description = "در این متن، لینک داخلی (لینک به سایت خودتان) وجود ندارد! یک یا چندتا لینک داخلی مرتبط با پست های سایت به نوشته اضافه کنید.")]//{0}LinkCount
        TextDoesNotContainAnyInboundLinks,
        [MessageInfo(GroupName = "Text", Description = "در این متن، لینک خارجی (لینک به سایت دیگری) وجود ندارد! یک یا چند لینک خارجی مرتبط با محتوای نوشته به سایت های معتبر اضافه کنید.")]//{0}LinkCount
        TextDoesNotContainAnyOutboundLinks,
        [MessageInfo(GroupName = "Text", Description = "تراکم کلمه کلیدی در متن {2}% است که این مقدار کمتر از مقدار پیشنهادی {3}% است. کلمه کلیدی شما {1} بار در متن تکرار شده است")]//{0}Keyword - {1}KeywordRepeatCount - {2}CurrentDensity - {3}MinDensity
        TextKeywordDensityIsLow,
        [MessageInfo(GroupName = "Text", Description = "تراکم کلمه کلیدی در متن {2}% است که این مقدار بیشتر از مقدار پیشنهادی {3}% است. کلمه کلیدی شما {1} بار در متن تکرار شده است")]//{0}Keyword - {1}KeywordRepeatCount - {2}CurrentDensity - {3}MaxDensity
        TextKeywordDensityIsHigh,
        [MessageInfo(GroupName = "Text", Description = "تعداد کلمات این متن {0} عدد است که این مقدار کمتر از حداقل  مقدار پیشنهادی {1} عدد است.")]//{0}TextWordsCount - {1}TextWordsMinCount
        TextWordsAreFewerThan,
        [MessageInfo(GroupName = "Text", Description = "تعداد کلمات این متن {0} عدد است که این مقدار بیشتر از حداکثر  مقدار پیشنهادی {1} عدد است.")]//{0}TextWordsCount - {1}TextWordsMaxCount
        TextWordsAreMoreThan,
        [MessageInfo(GroupName = "Text", Description = "تراکم کلمات خیلی عمومی ({2}) این متن {0} عدد است که این مقدار بیشتر از حداکثر  مقدار پیشنهادی {1} عدد است.")]//{0}CurrentDensity - {1}MaxDensity - {2}StopWordsSentence
        TextContainMoreThanStopWords,
        [MessageInfo(GroupName = "Text", Description = "تراکم کلمات گذار (Transition Words) موجود در متن {1}% است که این مقدار کمتر از حداقل پیشنهادی {2}% است. {0} جمله در متن شامل کلمه گذار می باشد.")]//{0}TransitionSentencesCount - {1}CurrentDensity - {2}MinDensity
        TransitionSentencesAreFewerThan,
        //TextContainIllegalCharacters

        //Sentence
        //SentenceIsShort,
        //[MessageInfo(GroupName = "Text", Description = "سعی کنید برای خوانایی بیشتر جمله ها رو کوتاه تر کنید.")]
        //SentenceIsLong,
        [MessageInfo(GroupName = "Text", Description = "{0} جمله در متن شامل بیش از {1} کلمه است که یعنی {2}% از جملات متن، و این بیش از حداکثر مقدار پیشنهادی {3}% است.")]//{0}MoreWordsSetencesCount - {1}TextSentencesWordsAvgCount - {2}CurrentDensity - {3}MaxDensity
        TextAvgSentencesAreMoreThan,
        [MessageInfo(GroupName = "Text", Description = "{0} جمله در متن شامل بیش از حداکثر مقدار پیشنهادی {1} کلمه است.")]//{0}MoreWordsSetencesCount - {1}TextSentencesWordsMaxCount
        TextSentencesContainMoreThanWords,
        //SentenceVariationIsBad, ********************
        //combination of long and short sentences

        //Paragraph
        [MessageInfo(GroupName = "Text", Description = "{0} از پاراگراف (تگ p یا div) های موجود در متن بیشتر از حداکثر {1} کلمه پیشنهادی است.")]//{0}ParagrapMoreWordsCount - {1}TextParagrapWordsMaxCount
        ParagraphWordsAreMoreThan,
        [MessageInfo(GroupName = "Text", Description = "{0} از پاراگراف (تگ p یا div) های موجود در متن کمتر از حداقل {1} کلمه پیشنهادی است.")]//{0}ParagrapMoreWordsCount - {1}TextParagrapWordsMinCount
        ParagraphWordsAreFewerThan,
        [MessageInfo(GroupName = "Text", Description = "کلمه کلیدی \"{0}\" در پاراگراف اول دیده نمی شود.")]//{0}Keyword
        FirstParagraphDoesNotContainFocusedKeyword,

        //Image
        [MessageInfo(GroupName = "Text", Description = "در این نوشته تصویری مشاهده نمی شود. شاید بهتر باشد تصویری اضافه نمایید.")]
        TextDoesNotContainAnyImage,
        //ImageSrcIsEmptyOrNotFound,
        //ImageNotOptimized,
        //ImageSizeIsOverThan,
        [MessageInfo(GroupName = "Text", Description = "{0} تصویر از مجموع {1} تصویر موجود در متن، برچسب (alt) ندارد.")]//{0}NoAltCount - {1}ImageCount
        ImagesDoesNotContainAltAttribute,
        [MessageInfo(GroupName = "Text", Description = "هیچکدام از {1} تصویر استفاده شده در این متن، شامل کلمه کلیدی \"{0}\" در برچسب (alt) نیستند.")]//{0}Keyword - {1}ImageCount
        NoImageContainKeywordInAltAttribute,
        [MessageInfo(GroupName = "Text", Description = "هیچکدام از {1} تصویر استفاده شده در این متن، شامل کلمه کلیدی \"{0}\" در نام فایل (src) نیستند.")]//{0}Keyword - {1}ImageCount
        NoImageContainKeywordInFielName,

        //Url
        [MessageInfo(GroupName = "Url", Description = "آدرس صفحه خالی است، لطفا یک آدرس مناسب وارد کنید.")]
        UrlIsEmpty,
        [MessageInfo(GroupName = "Url", Description = "آدرس صفحه شامل {0} کاراکتر است که این مقدار کمتر از حداقل مقدار پیشنهادی {1} کاراکتر است")]//{0}UrlLenght - {1}UrlMinLenght
        UrlIsShort,
        [MessageInfo(GroupName = "Url", Description = "آدرس صفحه شامل {0} کاراکتر است که این مقدار بیشتر از حداکثر مقدار پیشنهادی {1} کاراکتر است")]//{0}UrlLenght - {1}UrlMaxLenght
        UrlIsLong,
        [MessageInfo(GroupName = "Url", Description = "کلمه کلیدی \"{1}\" در آدرس این صفحه لحاظ نشده است.")]//{0}Url - {1}Keyword
        UrlDoesNotContainFocusKeyword,
        [MessageInfo(GroupName = "Url", Description = "آدرس صفحه شامل {0} کلمه است که این مقدار بیشتر از حداکثر مقدار پیشنهادی {1} کلمه است")]//{0}UrlWordsCount - {1}UrlWordsMaxCount
        UrlWordsAreMoreThan,
        [MessageInfo(GroupName = "Url", Description = "آدرس صفحه شامل {0} کلمه است که این مقدار کمتر از حداقل مقدار پیشنهادی {1} کلمه است")]//{0}UrlWordsCount - {1}UrlWordsMinCount
        UrlWordsAreFewerThan,
        [MessageInfo(GroupName = "Url", Description = "آدرس این صفحه شامل یک یا بیشتر کلمه خیلی عمومی {2} است، لطفا آنها را حذف کنید.")]//{0}CurrentDensity - {1}MaxDensity - {2}StopWordsSentence "آدرس این صفحه شامل {0}% تراکم کلمه خیلی عمومی {2} است ، لطفا آنها را حذف کنید."
        UrlContainMoreThanStopWords,
        [MessageInfo(GroupName = "Url", Description = "میزان شباهت آدرس با متاتگ توضیحات {1}% است که این کمتر از مقدار پیشنهادی {2}% است.")]//{0}Url - {1}CurrentDensity - {2}MinDensity
        UrlDoesNotMatchMetaDescription,
        [MessageInfo(GroupName = "Url", Description = "آدرس صفحه تکراری است و در پست های قبلی استفاده شده است")]//{0}Url
        UrlDuplicated,
        [MessageInfo(GroupName = "Url", Description = "میزان شباهت آدرس با متن {1}% است که این کمتر از مقدار پیشنهادی {2}% است.")]//{0}Url - {1}CurrentDensity - {2}MinDensity
        UrlDoesNotMatchContent,
    }

    [AttributeUsage(AttributeTargets.Field)]
    internal sealed class MessageInfoAttribute : Attribute
    {
        public string GroupName { get; set; }
        public string Description { get; set; }
    }
}