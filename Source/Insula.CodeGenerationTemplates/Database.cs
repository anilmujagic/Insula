/////////////////////////////////////////////////////////////////////////////////////////////
//  This code was generated from a template. Do NOT change it, edit the template instead.  //
/////////////////////////////////////////////////////////////////////////////////////////////

using System;
using Insula.Data.Orm;

namespace MyApp.Data
{
    public partial class MyAppDatabase : Database
    {
        public MyAppDatabase()
            : base(System.Configuration.ConfigurationManager.ConnectionStrings["MyAppDB"].ConnectionString)
        {
        }

        public MyAppDatabase(bool keepConnectionOpen)
            : base(System.Configuration.ConfigurationManager.ConnectionStrings["MyAppDB"].ConnectionString, keepConnectionOpen)
        {
        }

        #region Repositories

        private AuthorRepository _authorRepository;
        public AuthorRepository AuthorRepository
        {
            get
            {
                if (_authorRepository == null)
                    _authorRepository = new AuthorRepository(this);
    
                return _authorRepository;
            }
        }

        private BookRepository _bookRepository;
        public BookRepository BookRepository
        {
            get
            {
                if (_bookRepository == null)
                    _bookRepository = new BookRepository(this);
    
                return _bookRepository;
            }
        }

        private BookAuthorRepository _bookAuthorRepository;
        public BookAuthorRepository BookAuthorRepository
        {
            get
            {
                if (_bookAuthorRepository == null)
                    _bookAuthorRepository = new BookAuthorRepository(this);
    
                return _bookAuthorRepository;
            }
        }

        private BookTagRepository _bookTagRepository;
        public BookTagRepository BookTagRepository
        {
            get
            {
                if (_bookTagRepository == null)
                    _bookTagRepository = new BookTagRepository(this);
    
                return _bookTagRepository;
            }
        }

        private RatingRepository _ratingRepository;
        public RatingRepository RatingRepository
        {
            get
            {
                if (_ratingRepository == null)
                    _ratingRepository = new RatingRepository(this);
    
                return _ratingRepository;
            }
        }

        private RatingSourceRepository _ratingSourceRepository;
        public RatingSourceRepository RatingSourceRepository
        {
            get
            {
                if (_ratingSourceRepository == null)
                    _ratingSourceRepository = new RatingSourceRepository(this);
    
                return _ratingSourceRepository;
            }
        }

        private ReminderRepository _reminderRepository;
        public ReminderRepository ReminderRepository
        {
            get
            {
                if (_reminderRepository == null)
                    _reminderRepository = new ReminderRepository(this);
    
                return _reminderRepository;
            }
        }

        private ReviewRepository _reviewRepository;
        public ReviewRepository ReviewRepository
        {
            get
            {
                if (_reviewRepository == null)
                    _reviewRepository = new ReviewRepository(this);
    
                return _reviewRepository;
            }
        }

        private SuggestedBookRepository _suggestedBookRepository;
        public SuggestedBookRepository SuggestedBookRepository
        {
            get
            {
                if (_suggestedBookRepository == null)
                    _suggestedBookRepository = new SuggestedBookRepository(this);
    
                return _suggestedBookRepository;
            }
        }

        private SuggestedBookSubscriptionRepository _suggestedBookSubscriptionRepository;
        public SuggestedBookSubscriptionRepository SuggestedBookSubscriptionRepository
        {
            get
            {
                if (_suggestedBookSubscriptionRepository == null)
                    _suggestedBookSubscriptionRepository = new SuggestedBookSubscriptionRepository(this);
    
                return _suggestedBookSubscriptionRepository;
            }
        }

        private TagRepository _tagRepository;
        public TagRepository TagRepository
        {
            get
            {
                if (_tagRepository == null)
                    _tagRepository = new TagRepository(this);
    
                return _tagRepository;
            }
        }

        #endregion
    }
}