/////////////////////////////////////////////////////////////////////////////////////////////
//  This code was generated from a template. Do NOT change it, edit the template instead.  //
/////////////////////////////////////////////////////////////////////////////////////////////

using System;
using Insula.Data.Orm;

namespace MyApp.Data
{
    public partial class AuthorRepository : Repository<Author>
    {
        public AuthorRepository(MyAppDatabaseContext context)
            : base(context)
        {
        }
        
        public Author GetByKey(int authorID)
        {
            return this.Query().Where("[AuthorID] = @0", authorID).GetSingle();  
        }
        
        public void DeleteByKey(int authorID)
        {
            this.Delete(new Author() { AuthorID = authorID });
        }
    }


    public partial class BookRepository : Repository<Book>
    {
        public BookRepository(MyAppDatabaseContext context)
            : base(context)
        {
        }
        
        public Book GetByKey(int bookID)
        {
            return this.Query().Where("[BookID] = @0", bookID).GetSingle();  
        }
        
        public void DeleteByKey(int bookID)
        {
            this.Delete(new Book() { BookID = bookID });
        }
    }


    public partial class BookAuthorRepository : Repository<BookAuthor>
    {
        public BookAuthorRepository(MyAppDatabaseContext context)
            : base(context)
        {
        }
        
        public BookAuthor GetByKey(int bookID, int authorID)
        {
            return this.Query().Where("[BookID] = @0 AND [AuthorID] = @1", bookID, authorID).GetSingle();  
        }
        
        public void DeleteByKey(int bookID, int authorID)
        {
            this.Delete(new BookAuthor() { BookID = bookID, AuthorID = authorID });
        }
    }


    public partial class BookTagRepository : Repository<BookTag>
    {
        public BookTagRepository(MyAppDatabaseContext context)
            : base(context)
        {
        }
        
        public BookTag GetByKey(int bookID, short tagID)
        {
            return this.Query().Where("[BookID] = @0 AND [TagID] = @1", bookID, tagID).GetSingle();  
        }
        
        public void DeleteByKey(int bookID, short tagID)
        {
            this.Delete(new BookTag() { BookID = bookID, TagID = tagID });
        }
    }


    public partial class RatingRepository : Repository<Rating>
    {
        public RatingRepository(MyAppDatabaseContext context)
            : base(context)
        {
        }
        
        public Rating GetByKey(int bookID, byte ratingSourceID)
        {
            return this.Query().Where("[BookID] = @0 AND [RatingSourceID] = @1", bookID, ratingSourceID).GetSingle();  
        }
        
        public void DeleteByKey(int bookID, byte ratingSourceID)
        {
            this.Delete(new Rating() { BookID = bookID, RatingSourceID = ratingSourceID });
        }
    }


    public partial class RatingSourceRepository : Repository<RatingSource>
    {
        public RatingSourceRepository(MyAppDatabaseContext context)
            : base(context)
        {
        }
        
        public RatingSource GetByKey(byte ratingSourceID)
        {
            return this.Query().Where("[RatingSourceID] = @0", ratingSourceID).GetSingle();  
        }
        
        public void DeleteByKey(byte ratingSourceID)
        {
            this.Delete(new RatingSource() { RatingSourceID = ratingSourceID });
        }
    }


    public partial class ReminderRepository : Repository<Reminder>
    {
        public ReminderRepository(MyAppDatabaseContext context)
            : base(context)
        {
        }
        
        public Reminder GetByKey(int reminderID)
        {
            return this.Query().Where("[ReminderID] = @0", reminderID).GetSingle();  
        }
        
        public void DeleteByKey(int reminderID)
        {
            this.Delete(new Reminder() { ReminderID = reminderID });
        }
    }


    public partial class ReviewRepository : Repository<Review>
    {
        public ReviewRepository(MyAppDatabaseContext context)
            : base(context)
        {
        }
        
        public Review GetByKey(int reviewID)
        {
            return this.Query().Where("[ReviewID] = @0", reviewID).GetSingle();  
        }
        
        public void DeleteByKey(int reviewID)
        {
            this.Delete(new Review() { ReviewID = reviewID });
        }
    }


    public partial class SuggestedBookRepository : Repository<SuggestedBook>
    {
        public SuggestedBookRepository(MyAppDatabaseContext context)
            : base(context)
        {
        }
        
        public SuggestedBook GetByKey(int suggestedBookID)
        {
            return this.Query().Where("[SuggestedBookID] = @0", suggestedBookID).GetSingle();  
        }
        
        public void DeleteByKey(int suggestedBookID)
        {
            this.Delete(new SuggestedBook() { SuggestedBookID = suggestedBookID });
        }
    }


    public partial class SuggestedBookSubscriptionRepository : Repository<SuggestedBookSubscription>
    {
        public SuggestedBookSubscriptionRepository(MyAppDatabaseContext context)
            : base(context)
        {
        }
        
        public SuggestedBookSubscription GetByKey(int suggestedBookSubscriptionID)
        {
            return this.Query().Where("[SuggestedBookSubscriptionID] = @0", suggestedBookSubscriptionID).GetSingle();  
        }
        
        public void DeleteByKey(int suggestedBookSubscriptionID)
        {
            this.Delete(new SuggestedBookSubscription() { SuggestedBookSubscriptionID = suggestedBookSubscriptionID });
        }
    }


    public partial class TagRepository : Repository<Tag>
    {
        public TagRepository(MyAppDatabaseContext context)
            : base(context)
        {
        }
        
        public Tag GetByKey(short tagID)
        {
            return this.Query().Where("[TagID] = @0", tagID).GetSingle();  
        }
        
        public void DeleteByKey(short tagID)
        {
            this.Delete(new Tag() { TagID = tagID });
        }
    }


}