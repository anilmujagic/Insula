using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using MyApp.Data;

namespace Insula.Data.Orm.Tests
{
    public class SqlQueryTests
    {
        public class Where
        {
            [Fact]
            public void OnObjectWithoutProperties_DoesNotCreateWhereClause()
            {
                using (var db = new MyAppDatabaseContext())
                {
                    var query = db.AuthorRepository.Query()
                        .Where(new {})
                        .ToString();

                    Assert.DoesNotContain("WHERE", query);
                }
            }

            [Fact]
            public void OnAnonymousObject_CreateWhereClauseForEachProperty()
            {
                using (var db = new MyAppDatabaseContext())
                {
                    var query = db.AuthorRepository.Query()
                        .Where(new { 
                            FirstName = "Anil", 
                            LastName = "Mujagic" 
                        })
                        .ToString();

                    Assert.Contains("WHERE", query);
                    Assert.Contains("@FirstName", query);
                    Assert.Contains("@LastName", query);
                }
            }

            [Fact]
            public void OnTypedObject_CreateWhereClauseForEachPropertyWithNonDefaultValue()
            {
                using (var db = new MyAppDatabaseContext())
                {
                    var query = db.AuthorRepository.Query()
                        .Where(new Author
                        {
                            Name = "Anil Mujagic"
                        })
                        .ToString();

                    Assert.Contains("WHERE", query);
                    Assert.Contains("@Name", query);
                    Assert.DoesNotContain("@AuthorID", query);
                }
            }

            [Fact]
            public void OnTypedObject_CreateWhereClauseForEachProperty()
            {
                using (var db = new MyAppDatabaseContext())
                {
                    var query = db.AuthorRepository.Query()
                        .Where(new Author
                        {
                            Name = "Anil Mujagic"
                        }, true)
                        .ToString();

                    Assert.Contains("WHERE", query);
                    Assert.Contains("@Name", query);
                    Assert.Contains("@AuthorID", query);
                }
            }
        }
    }
}
