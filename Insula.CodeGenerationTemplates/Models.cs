/////////////////////////////////////////////////////////////////////////////////////////////
//  This code was generated from a template. Do NOT change it, edit the template instead.  //
/////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.ComponentModel.DataAnnotations;
using Insula.DataAnnotations.Schema;

namespace MyApp.Data
{
    public partial class Author
    {
        [Mapped]
        [Key]                
        [Identity]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ModelValidationMessages))]
        //[Display(Name = "Author_AuthorID_LABEL", Description = "Author_AuthorID_DESCRIPTION", ResourceType = typeof(ModelDisplayNamesAndDescriptions))]
        public int AuthorID { get; set; }

        [Mapped]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ModelValidationMessages))]
        //[StringLength(100, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(ModelValidationMessages))]
        //[Display(Name = "Author_Name_LABEL", Description = "Author_Name_DESCRIPTION", ResourceType = typeof(ModelDisplayNamesAndDescriptions))]
        public string Name { get; set; }

    }


    public partial class Book
    {
        [Mapped]
        [Key]                
        [Identity]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ModelValidationMessages))]
        //[Display(Name = "Book_BookID_LABEL", Description = "Book_BookID_DESCRIPTION", ResourceType = typeof(ModelDisplayNamesAndDescriptions))]
        public int BookID { get; set; }

        [Mapped]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ModelValidationMessages))]
        //[StringLength(300, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(ModelValidationMessages))]
        //[Display(Name = "Book_Title_LABEL", Description = "Book_Title_DESCRIPTION", ResourceType = typeof(ModelDisplayNamesAndDescriptions))]
        public string Title { get; set; }

        [Mapped]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ModelValidationMessages))]
        //[StringLength(20, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(ModelValidationMessages))]
        //[Display(Name = "Book_Edition_LABEL", Description = "Book_Edition_DESCRIPTION", ResourceType = typeof(ModelDisplayNamesAndDescriptions))]
        public string Edition { get; set; }

        [Mapped]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ModelValidationMessages))]
        //[Display(Name = "Book_Pages_LABEL", Description = "Book_Pages_DESCRIPTION", ResourceType = typeof(ModelDisplayNamesAndDescriptions))]
        public short Pages { get; set; }

        [Mapped]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ModelValidationMessages))]
        //[StringLength(100, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(ModelValidationMessages))]
        //[Display(Name = "Book_Publisher_LABEL", Description = "Book_Publisher_DESCRIPTION", ResourceType = typeof(ModelDisplayNamesAndDescriptions))]
        public string Publisher { get; set; }

        [Mapped]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ModelValidationMessages))]
        //[Display(Name = "Book_PublicationDate_LABEL", Description = "Book_PublicationDate_DESCRIPTION", ResourceType = typeof(ModelDisplayNamesAndDescriptions))]
        [DisplayFormat(DataFormatString = "g")]
        public DateTime PublicationDate { get; set; }

        [Mapped]
        //[StringLength(20, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(ModelValidationMessages))]
        //[Display(Name = "Book_ISBN10_LABEL", Description = "Book_ISBN10_DESCRIPTION", ResourceType = typeof(ModelDisplayNamesAndDescriptions))]
        [DisplayFormat(ConvertEmptyStringToNull = true)]
        public string ISBN10 { get; set; }

        [Mapped]
        //[StringLength(20, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(ModelValidationMessages))]
        //[Display(Name = "Book_ISBN13_LABEL", Description = "Book_ISBN13_DESCRIPTION", ResourceType = typeof(ModelDisplayNamesAndDescriptions))]
        [DisplayFormat(ConvertEmptyStringToNull = true)]
        public string ISBN13 { get; set; }

        [Mapped]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ModelValidationMessages))]
        //[StringLength(20, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(ModelValidationMessages))]
        //[Display(Name = "Book_ASIN_LABEL", Description = "Book_ASIN_DESCRIPTION", ResourceType = typeof(ModelDisplayNamesAndDescriptions))]
        public string ASIN { get; set; }

        [Mapped]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ModelValidationMessages))]
        //[StringLength(2000, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(ModelValidationMessages))]
        //[Display(Name = "Book_ImageLink_LABEL", Description = "Book_ImageLink_DESCRIPTION", ResourceType = typeof(ModelDisplayNamesAndDescriptions))]
        public string ImageLink { get; set; }

        [Mapped]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ModelValidationMessages))]
        //[StringLength(2000, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(ModelValidationMessages))]
        //[Display(Name = "Book_DetailsLink_LABEL", Description = "Book_DetailsLink_DESCRIPTION", ResourceType = typeof(ModelDisplayNamesAndDescriptions))]
        public string DetailsLink { get; set; }

        [Mapped]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ModelValidationMessages))]
        //[Display(Name = "Book_Visible_LABEL", Description = "Book_Visible_DESCRIPTION", ResourceType = typeof(ModelDisplayNamesAndDescriptions))]
        public bool Visible { get; set; }

        [Mapped]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ModelValidationMessages))]
        //[Display(Name = "Book_AverageRatingScore_LABEL", Description = "Book_AverageRatingScore_DESCRIPTION", ResourceType = typeof(ModelDisplayNamesAndDescriptions))]
        [DisplayFormat(DataFormatString = "N1")]
        public decimal AverageRatingScore { get; set; }

        [Mapped]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ModelValidationMessages))]
        //[Display(Name = "Book_TotalVoteCount_LABEL", Description = "Book_TotalVoteCount_DESCRIPTION", ResourceType = typeof(ModelDisplayNamesAndDescriptions))]
        public int TotalVoteCount { get; set; }

        [Mapped]
        //[Display(Name = "Book_TweetedAt_LABEL", Description = "Book_TweetedAt_DESCRIPTION", ResourceType = typeof(ModelDisplayNamesAndDescriptions))]
        [DisplayFormat(ConvertEmptyStringToNull = true)]
        public DateTime? TweetedAt { get; set; }

    }


    public partial class BookAuthor
    {
        [Mapped]
        [Key]                
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ModelValidationMessages))]
        //[Display(Name = "BookAuthor_BookID_LABEL", Description = "BookAuthor_BookID_DESCRIPTION", ResourceType = typeof(ModelDisplayNamesAndDescriptions))]
        public int BookID { get; set; }

        [Mapped]
        [Key]                
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ModelValidationMessages))]
        //[Display(Name = "BookAuthor_AuthorID_LABEL", Description = "BookAuthor_AuthorID_DESCRIPTION", ResourceType = typeof(ModelDisplayNamesAndDescriptions))]
        public int AuthorID { get; set; }

    }


    public partial class BookTag
    {
        [Mapped]
        [Key]                
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ModelValidationMessages))]
        //[Display(Name = "BookTag_BookID_LABEL", Description = "BookTag_BookID_DESCRIPTION", ResourceType = typeof(ModelDisplayNamesAndDescriptions))]
        public int BookID { get; set; }

        [Mapped]
        [Key]                
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ModelValidationMessages))]
        //[Display(Name = "BookTag_TagID_LABEL", Description = "BookTag_TagID_DESCRIPTION", ResourceType = typeof(ModelDisplayNamesAndDescriptions))]
        public short TagID { get; set; }

    }


    public partial class Rating
    {
        [Mapped]
        [Key]                
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ModelValidationMessages))]
        //[Display(Name = "Rating_BookID_LABEL", Description = "Rating_BookID_DESCRIPTION", ResourceType = typeof(ModelDisplayNamesAndDescriptions))]
        public int BookID { get; set; }

        [Mapped]
        [Key]                
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ModelValidationMessages))]
        //[Display(Name = "Rating_RatingSourceID_LABEL", Description = "Rating_RatingSourceID_DESCRIPTION", ResourceType = typeof(ModelDisplayNamesAndDescriptions))]
        public byte RatingSourceID { get; set; }

        [Mapped]
        //[StringLength(2000, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(ModelValidationMessages))]
        //[Display(Name = "Rating_Link_LABEL", Description = "Rating_Link_DESCRIPTION", ResourceType = typeof(ModelDisplayNamesAndDescriptions))]
        [DisplayFormat(ConvertEmptyStringToNull = true)]
        public string Link { get; set; }

        [Mapped]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ModelValidationMessages))]
        //[Display(Name = "Rating_Score_LABEL", Description = "Rating_Score_DESCRIPTION", ResourceType = typeof(ModelDisplayNamesAndDescriptions))]
        [DisplayFormat(DataFormatString = "N1")]
        public decimal Score { get; set; }

        [Mapped]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ModelValidationMessages))]
        //[Display(Name = "Rating_VoteCount_LABEL", Description = "Rating_VoteCount_DESCRIPTION", ResourceType = typeof(ModelDisplayNamesAndDescriptions))]
        public int VoteCount { get; set; }

        [Mapped]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ModelValidationMessages))]
        //[Display(Name = "Rating_UpdatedAt_LABEL", Description = "Rating_UpdatedAt_DESCRIPTION", ResourceType = typeof(ModelDisplayNamesAndDescriptions))]
        [DisplayFormat(DataFormatString = "G")]
        public DateTime UpdatedAt { get; set; }

    }


    public partial class RatingSource
    {
        [Mapped]
        [Key]                
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ModelValidationMessages))]
        //[Display(Name = "RatingSource_RatingSourceID_LABEL", Description = "RatingSource_RatingSourceID_DESCRIPTION", ResourceType = typeof(ModelDisplayNamesAndDescriptions))]
        public byte RatingSourceID { get; set; }

        [Mapped]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ModelValidationMessages))]
        //[StringLength(100, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(ModelValidationMessages))]
        //[Display(Name = "RatingSource_Name_LABEL", Description = "RatingSource_Name_DESCRIPTION", ResourceType = typeof(ModelDisplayNamesAndDescriptions))]
        public string Name { get; set; }

        [Mapped]
        //[StringLength(2000, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(ModelValidationMessages))]
        //[Display(Name = "RatingSource_LinkPattern_LABEL", Description = "RatingSource_LinkPattern_DESCRIPTION", ResourceType = typeof(ModelDisplayNamesAndDescriptions))]
        [DisplayFormat(ConvertEmptyStringToNull = true)]
        public string LinkPattern { get; set; }

        [Mapped]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ModelValidationMessages))]
        //[Display(Name = "RatingSource_RefreshInterval_LABEL", Description = "RatingSource_RefreshInterval_DESCRIPTION", ResourceType = typeof(ModelDisplayNamesAndDescriptions))]
        public short RefreshInterval { get; set; }

    }


    public partial class Reminder
    {
        [Mapped]
        [Key]                
        [Identity]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ModelValidationMessages))]
        //[Display(Name = "Reminder_ReminderID_LABEL", Description = "Reminder_ReminderID_DESCRIPTION", ResourceType = typeof(ModelDisplayNamesAndDescriptions))]
        public int ReminderID { get; set; }

        [Mapped]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ModelValidationMessages))]
        //[Display(Name = "Reminder_BookID_LABEL", Description = "Reminder_BookID_DESCRIPTION", ResourceType = typeof(ModelDisplayNamesAndDescriptions))]
        public int BookID { get; set; }

        [Mapped]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ModelValidationMessages))]
        //[StringLength(300, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(ModelValidationMessages))]
        //[Display(Name = "Reminder_Email_LABEL", Description = "Reminder_Email_DESCRIPTION", ResourceType = typeof(ModelDisplayNamesAndDescriptions))]
        public string Email { get; set; }

        [Mapped]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ModelValidationMessages))]
        //[Display(Name = "Reminder_Days_LABEL", Description = "Reminder_Days_DESCRIPTION", ResourceType = typeof(ModelDisplayNamesAndDescriptions))]
        public short Days { get; set; }

        [Mapped]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ModelValidationMessages))]
        //[Display(Name = "Reminder_SubscribedAt_LABEL", Description = "Reminder_SubscribedAt_DESCRIPTION", ResourceType = typeof(ModelDisplayNamesAndDescriptions))]
        [DisplayFormat(DataFormatString = "G")]
        public DateTime SubscribedAt { get; set; }

        [Mapped]
        //[Display(Name = "Reminder_RemindedAt_LABEL", Description = "Reminder_RemindedAt_DESCRIPTION", ResourceType = typeof(ModelDisplayNamesAndDescriptions))]
        [DisplayFormat(ConvertEmptyStringToNull = true)]
        public DateTime? RemindedAt { get; set; }

        [Mapped]
        //[Display(Name = "Reminder_Token_LABEL", Description = "Reminder_Token_DESCRIPTION", ResourceType = typeof(ModelDisplayNamesAndDescriptions))]
        [DisplayFormat(ConvertEmptyStringToNull = true)]
        public Guid? Token { get; set; }

        [Mapped]
        //[Display(Name = "Reminder_VisitedAt_LABEL", Description = "Reminder_VisitedAt_DESCRIPTION", ResourceType = typeof(ModelDisplayNamesAndDescriptions))]
        [DisplayFormat(ConvertEmptyStringToNull = true)]
        public DateTime? VisitedAt { get; set; }

        [Mapped]
        //[Display(Name = "Reminder_ReviewID_LABEL", Description = "Reminder_ReviewID_DESCRIPTION", ResourceType = typeof(ModelDisplayNamesAndDescriptions))]
        [DisplayFormat(ConvertEmptyStringToNull = true)]
        public int? ReviewID { get; set; }

    }


    public partial class Review
    {
        [Mapped]
        [Key]                
        [Identity]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ModelValidationMessages))]
        //[Display(Name = "Review_ReviewID_LABEL", Description = "Review_ReviewID_DESCRIPTION", ResourceType = typeof(ModelDisplayNamesAndDescriptions))]
        public int ReviewID { get; set; }

        [Mapped]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ModelValidationMessages))]
        //[Display(Name = "Review_BookID_LABEL", Description = "Review_BookID_DESCRIPTION", ResourceType = typeof(ModelDisplayNamesAndDescriptions))]
        public int BookID { get; set; }

        [Mapped]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ModelValidationMessages))]
        //[Display(Name = "Review_RatingScore_LABEL", Description = "Review_RatingScore_DESCRIPTION", ResourceType = typeof(ModelDisplayNamesAndDescriptions))]
        public byte RatingScore { get; set; }

        [Mapped]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ModelValidationMessages))]
        //[StringLength(50, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(ModelValidationMessages))]
        //[Display(Name = "Review_Name_LABEL", Description = "Review_Name_DESCRIPTION", ResourceType = typeof(ModelDisplayNamesAndDescriptions))]
        public string Name { get; set; }

        [Mapped]
        //[StringLength(2000, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(ModelValidationMessages))]
        //[Display(Name = "Review_Link_LABEL", Description = "Review_Link_DESCRIPTION", ResourceType = typeof(ModelDisplayNamesAndDescriptions))]
        [DisplayFormat(ConvertEmptyStringToNull = true)]
        public string Link { get; set; }

        [Mapped]
        //[Display(Name = "Review_Pros_LABEL", Description = "Review_Pros_DESCRIPTION", ResourceType = typeof(ModelDisplayNamesAndDescriptions))]
        [DisplayFormat(ConvertEmptyStringToNull = true)]
        public string Pros { get; set; }

        [Mapped]
        //[Display(Name = "Review_Cons_LABEL", Description = "Review_Cons_DESCRIPTION", ResourceType = typeof(ModelDisplayNamesAndDescriptions))]
        [DisplayFormat(ConvertEmptyStringToNull = true)]
        public string Cons { get; set; }

        [Mapped]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ModelValidationMessages))]
        //[Display(Name = "Review_SubmittedAt_LABEL", Description = "Review_SubmittedAt_DESCRIPTION", ResourceType = typeof(ModelDisplayNamesAndDescriptions))]
        [DisplayFormat(DataFormatString = "G")]
        public DateTime SubmittedAt { get; set; }

        [Mapped]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ModelValidationMessages))]
        //[Display(Name = "Review_Visible_LABEL", Description = "Review_Visible_DESCRIPTION", ResourceType = typeof(ModelDisplayNamesAndDescriptions))]
        public bool Visible { get; set; }

        [Mapped]
        //[Display(Name = "Review_ApprovedAt_LABEL", Description = "Review_ApprovedAt_DESCRIPTION", ResourceType = typeof(ModelDisplayNamesAndDescriptions))]
        [DisplayFormat(ConvertEmptyStringToNull = true)]
        public DateTime? ApprovedAt { get; set; }

        [Mapped]
        //[StringLength(50, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(ModelValidationMessages))]
        //[Display(Name = "Review_IpAddress_LABEL", Description = "Review_IpAddress_DESCRIPTION", ResourceType = typeof(ModelDisplayNamesAndDescriptions))]
        [DisplayFormat(ConvertEmptyStringToNull = true)]
        public string IpAddress { get; set; }

    }


    public partial class SuggestedBook
    {
        [Mapped]
        [Key]                
        [Identity]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ModelValidationMessages))]
        //[Display(Name = "SuggestedBook_SuggestedBookID_LABEL", Description = "SuggestedBook_SuggestedBookID_DESCRIPTION", ResourceType = typeof(ModelDisplayNamesAndDescriptions))]
        public int SuggestedBookID { get; set; }

        [Mapped]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ModelValidationMessages))]
        //[StringLength(20, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(ModelValidationMessages))]
        //[Display(Name = "SuggestedBook_ISBN_LABEL", Description = "SuggestedBook_ISBN_DESCRIPTION", ResourceType = typeof(ModelDisplayNamesAndDescriptions))]
        public string ISBN { get; set; }

        [Mapped]
        //[Display(Name = "SuggestedBook_AddedAt_LABEL", Description = "SuggestedBook_AddedAt_DESCRIPTION", ResourceType = typeof(ModelDisplayNamesAndDescriptions))]
        [DisplayFormat(ConvertEmptyStringToNull = true)]
        public DateTime? AddedAt { get; set; }

        [Mapped]
        //[Display(Name = "SuggestedBook_BookID_LABEL", Description = "SuggestedBook_BookID_DESCRIPTION", ResourceType = typeof(ModelDisplayNamesAndDescriptions))]
        [DisplayFormat(ConvertEmptyStringToNull = true)]
        public int? BookID { get; set; }

    }


    public partial class SuggestedBookSubscription
    {
        [Mapped]
        [Key]                
        [Identity]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ModelValidationMessages))]
        //[Display(Name = "SuggestedBookSubscription_SuggestedBookSubscriptionID_LABEL", Description = "SuggestedBookSubscription_SuggestedBookSubscriptionID_DESCRIPTION", ResourceType = typeof(ModelDisplayNamesAndDescriptions))]
        public int SuggestedBookSubscriptionID { get; set; }

        [Mapped]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ModelValidationMessages))]
        //[Display(Name = "SuggestedBookSubscription_SuggestedBookID_LABEL", Description = "SuggestedBookSubscription_SuggestedBookID_DESCRIPTION", ResourceType = typeof(ModelDisplayNamesAndDescriptions))]
        public int SuggestedBookID { get; set; }

        [Mapped]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ModelValidationMessages))]
        //[StringLength(300, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(ModelValidationMessages))]
        //[Display(Name = "SuggestedBookSubscription_Email_LABEL", Description = "SuggestedBookSubscription_Email_DESCRIPTION", ResourceType = typeof(ModelDisplayNamesAndDescriptions))]
        public string Email { get; set; }

        [Mapped]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ModelValidationMessages))]
        //[Display(Name = "SuggestedBookSubscription_SubscribedAt_LABEL", Description = "SuggestedBookSubscription_SubscribedAt_DESCRIPTION", ResourceType = typeof(ModelDisplayNamesAndDescriptions))]
        [DisplayFormat(DataFormatString = "G")]
        public DateTime SubscribedAt { get; set; }

        [Mapped]
        //[Display(Name = "SuggestedBookSubscription_NotifiedAt_LABEL", Description = "SuggestedBookSubscription_NotifiedAt_DESCRIPTION", ResourceType = typeof(ModelDisplayNamesAndDescriptions))]
        [DisplayFormat(ConvertEmptyStringToNull = true)]
        public DateTime? NotifiedAt { get; set; }

        [Mapped]
        //[Display(Name = "SuggestedBookSubscription_NotificationToken_LABEL", Description = "SuggestedBookSubscription_NotificationToken_DESCRIPTION", ResourceType = typeof(ModelDisplayNamesAndDescriptions))]
        [DisplayFormat(ConvertEmptyStringToNull = true)]
        public Guid? NotificationToken { get; set; }

        [Mapped]
        //[Display(Name = "SuggestedBookSubscription_VisitedAt_LABEL", Description = "SuggestedBookSubscription_VisitedAt_DESCRIPTION", ResourceType = typeof(ModelDisplayNamesAndDescriptions))]
        [DisplayFormat(ConvertEmptyStringToNull = true)]
        public DateTime? VisitedAt { get; set; }

    }


    public partial class Tag
    {
        [Mapped]
        [Key]                
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ModelValidationMessages))]
        //[Display(Name = "Tag_TagID_LABEL", Description = "Tag_TagID_DESCRIPTION", ResourceType = typeof(ModelDisplayNamesAndDescriptions))]
        public short TagID { get; set; }

        [Mapped]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ModelValidationMessages))]
        //[StringLength(50, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(ModelValidationMessages))]
        //[Display(Name = "Tag_Name_LABEL", Description = "Tag_Name_DESCRIPTION", ResourceType = typeof(ModelDisplayNamesAndDescriptions))]
        public string Name { get; set; }

        [Mapped]
        //[Display(Name = "Tag_ParentTagID_LABEL", Description = "Tag_ParentTagID_DESCRIPTION", ResourceType = typeof(ModelDisplayNamesAndDescriptions))]
        [DisplayFormat(ConvertEmptyStringToNull = true)]
        public short? ParentTagID { get; set; }

    }


}