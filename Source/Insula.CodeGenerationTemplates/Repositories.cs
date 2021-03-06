﻿/////////////////////////////////////////////////////////////////////////////////////////////
//  This code was generated from a template. Do NOT change it, edit the template instead.  //
/////////////////////////////////////////////////////////////////////////////////////////////

using System;
using Insula.Data.Orm;

namespace MyApp.Data
{
    public partial class AuthorRepository : Repository<Author>
    {
        public AuthorRepository(MyAppDatabaseContext databaseContext)
            : base(databaseContext)
        {
        }
        
        public Author GetByKey(int authorID)
        {
            return this.Query().Where(new { AuthorID = authorID }).GetSingle();  
        }
        
        public void DeleteByKey(int authorID)
        {
            this.Delete(new Author() { AuthorID = authorID });
        }
    }


    public partial class BookRepository : Repository<Book>
    {
        public BookRepository(MyAppDatabaseContext databaseContext)
            : base(databaseContext)
        {
        }
        
        public Book GetByKey(int bookID)
        {
            return this.Query().Where(new { BookID = bookID }).GetSingle();  
        }
        
        public void DeleteByKey(int bookID)
        {
            this.Delete(new Book() { BookID = bookID });
        }
    }


    public partial class BookAuthorRepository : Repository<BookAuthor>
    {
        public BookAuthorRepository(MyAppDatabaseContext databaseContext)
            : base(databaseContext)
        {
        }
        
        public BookAuthor GetByKey(int bookID, int authorID)
        {
            return this.Query().Where(new { BookID = bookID, AuthorID = authorID }).GetSingle();  
        }
        
        public void DeleteByKey(int bookID, int authorID)
        {
            this.Delete(new BookAuthor() { BookID = bookID, AuthorID = authorID });
        }
    }


    public partial class BookTagRepository : Repository<BookTag>
    {
        public BookTagRepository(MyAppDatabaseContext databaseContext)
            : base(databaseContext)
        {
        }
        
        public BookTag GetByKey(int bookID, short tagID)
        {
            return this.Query().Where(new { BookID = bookID, TagID = tagID }).GetSingle();  
        }
        
        public void DeleteByKey(int bookID, short tagID)
        {
            this.Delete(new BookTag() { BookID = bookID, TagID = tagID });
        }
    }


    public partial class RatingRepository : Repository<Rating>
    {
        public RatingRepository(MyAppDatabaseContext databaseContext)
            : base(databaseContext)
        {
        }
        
        public Rating GetByKey(int bookID, byte ratingSourceID)
        {
            return this.Query().Where(new { BookID = bookID, RatingSourceID = ratingSourceID }).GetSingle();  
        }
        
        public void DeleteByKey(int bookID, byte ratingSourceID)
        {
            this.Delete(new Rating() { BookID = bookID, RatingSourceID = ratingSourceID });
        }
    }


    public partial class RatingSourceRepository : Repository<RatingSource>
    {
        public RatingSourceRepository(MyAppDatabaseContext databaseContext)
            : base(databaseContext)
        {
        }
        
        public RatingSource GetByKey(byte ratingSourceID)
        {
            return this.Query().Where(new { RatingSourceID = ratingSourceID }).GetSingle();  
        }
        
        public void DeleteByKey(byte ratingSourceID)
        {
            this.Delete(new RatingSource() { RatingSourceID = ratingSourceID });
        }
    }


    public partial class ReminderRepository : Repository<Reminder>
    {
        public ReminderRepository(MyAppDatabaseContext databaseContext)
            : base(databaseContext)
        {
        }
        
        public Reminder GetByKey(int reminderID)
        {
            return this.Query().Where(new { ReminderID = reminderID }).GetSingle();  
        }
        
        public void DeleteByKey(int reminderID)
        {
            this.Delete(new Reminder() { ReminderID = reminderID });
        }
    }


    public partial class ReviewRepository : Repository<Review>
    {
        public ReviewRepository(MyAppDatabaseContext databaseContext)
            : base(databaseContext)
        {
        }
        
        public Review GetByKey(int reviewID)
        {
            return this.Query().Where(new { ReviewID = reviewID }).GetSingle();  
        }
        
        public void DeleteByKey(int reviewID)
        {
            this.Delete(new Review() { ReviewID = reviewID });
        }
    }


    public partial class SuggestedBookRepository : Repository<SuggestedBook>
    {
        public SuggestedBookRepository(MyAppDatabaseContext databaseContext)
            : base(databaseContext)
        {
        }
        
        public SuggestedBook GetByKey(int suggestedBookID)
        {
            return this.Query().Where(new { SuggestedBookID = suggestedBookID }).GetSingle();  
        }
        
        public void DeleteByKey(int suggestedBookID)
        {
            this.Delete(new SuggestedBook() { SuggestedBookID = suggestedBookID });
        }
    }


    public partial class SuggestedBookSubscriptionRepository : Repository<SuggestedBookSubscription>
    {
        public SuggestedBookSubscriptionRepository(MyAppDatabaseContext databaseContext)
            : base(databaseContext)
        {
        }
        
        public SuggestedBookSubscription GetByKey(int suggestedBookSubscriptionID)
        {
            return this.Query().Where(new { SuggestedBookSubscriptionID = suggestedBookSubscriptionID }).GetSingle();  
        }
        
        public void DeleteByKey(int suggestedBookSubscriptionID)
        {
            this.Delete(new SuggestedBookSubscription() { SuggestedBookSubscriptionID = suggestedBookSubscriptionID });
        }
    }


    public partial class TagRepository : Repository<Tag>
    {
        public TagRepository(MyAppDatabaseContext databaseContext)
            : base(databaseContext)
        {
        }
        
        public Tag GetByKey(short tagID)
        {
            return this.Query().Where(new { TagID = tagID }).GetSingle();  
        }
        
        public void DeleteByKey(short tagID)
        {
            this.Delete(new Tag() { TagID = tagID });
        }
    }


}