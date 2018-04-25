using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SpacePirateInventory.Data.Dapper;
using SpacePirateInventory.Data.DapperRepo;
using SpacePirateInventory.Models.Queries;
using SpacePirateInventory.Models.Tables;

namespace SpacePirateInventory.Tests.IntegrationTests
{
    [TestFixture]
    public class DapperTests
    {
        [SetUp]
        public void Init()
        {
            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                var cmd = new SqlCommand();
                cmd.CommandText = "DbReset";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //Might not need the below code for dapper?
                cmd.Connection = cn;
                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        [Test]
        public void CanLoadCategory()
        {
            var repo = new CategoryRepository();

            var categories = repo.GetAll().ToList();

            Assert.AreEqual(5, categories.Count());

            Assert.AreEqual("Treasure", categories[0].CategoryName);
        }

        [Test]
        public void CanLoadCurrency()
        {
            var repo = new CurrencyRepository();

            var currencies = repo.GetAll().ToList();

            Assert.AreEqual(2, currencies.Count());

            Assert.AreEqual("World Dollars", currencies[0].CurrencyName);
        }

        [Test]
        public void CanLoadUsers()
        {
            var repo = new UserRepository();

            var user = repo.GetAll().ToList();

            Assert.AreEqual(2, user.Count());

            Assert.AreEqual("mary", user[0].UserName);
            Assert.AreEqual("Admin", user[0].Name);
        }

        [Test]
        public void CanLoadArticleDetails()
        {
            var repo = new ArticleRepository();

            var articles = repo.GetAll().ToList();

            Assert.AreEqual(9, articles.Count());

            Assert.AreEqual("Space and you", articles[2].ArticleTitle);

        }

        [Test]
        public void CanEditArticle()
        {
            var repo = new ArticleRepository();
            var articleToUpdate = repo.GetById(3);

            articleToUpdate.ArticleTitle = "Space Gluten Exterminator";
            repo.UpdateArticle(articleToUpdate);

            var updatedArticle = repo.GetById(3);
            Assert.AreEqual("Space Gluten Exterminator", updatedArticle.ArticleTitle);
        }

        [Test]
        public void CanInsertCategory()
        {
            Category categoryToInsert = new Category();
            var repo = new CategoryRepository();

            categoryToInsert.CategoryId = 6;
            categoryToInsert.CategoryName = "Florbies";

            repo.AddCategory(categoryToInsert);

            Assert.AreEqual(6, categoryToInsert.CategoryId);
            Assert.AreEqual("Florbies", categoryToInsert.CategoryName);
        }

        [Test]
        public void CanInsertArticle()
        {
            Article articleToInsert = new Article();
            var repo = new ArticleRepository();

            articleToInsert.ArticleId = 5;
            articleToInsert.ArticleTitle = "How to survive Namek.";
            articleToInsert.ArticleContent = "A guide Namekians are DYING to read.";
            articleToInsert.DateAdded = DateTime.Today;
            articleToInsert.UserId = "00000000-0000-0000-0000-000000000000";
            articleToInsert.Approved = true;

            repo.AddArticle(articleToInsert);

            Assert.AreEqual(5, articleToInsert.ArticleId);
            Assert.AreEqual(DateTime.Today, articleToInsert.DateAdded);

        }

        [Test]
        public void CanDeleteCategory()
        {
            Category categoryToInsert = new Category();
            var repo = new CategoryRepository();

            categoryToInsert.CategoryId = 6;
            categoryToInsert.CategoryName = "Florbies";

            repo.AddCategory(categoryToInsert);

            var loaded = repo.GetById(6);
            Assert.IsNotNull(loaded);

            repo.DeleteCategory(6);
            loaded = repo.GetById(6);

            Assert.IsNull(loaded);
        }
        
        [Test]
        public void CanReadFeaturedShortItem()
        {
            var repo = new PirateItemRepository();

            List<ItemShort> itemToRead = repo.GetAllFeaturedShortItems().ToList();

            Assert.AreEqual(8, itemToRead.Count());

            Assert.AreEqual("Space Gold", itemToRead[0].ItemName);
        }

        [Test]
        public void CanReadAllShortItems()
        {
            var repo = new PirateItemRepository();

            List<ItemShort> itemToRead = repo.GetAllShortItems().ToList();

            Assert.AreEqual(8, itemToRead.Count());

            Assert.AreEqual("Space Gold", itemToRead[0].ItemName);
        }

        [Test]
        public void CanAddItem()
        {
            Item itemToAdd = new Item();
            var repo = new PirateItemRepository();

            itemToAdd.ItemName = "Test Item";
            itemToAdd.RealValue = 15000;
            itemToAdd.DisplayValue = 20000;
            itemToAdd.Description = "This is a test item";
            itemToAdd.Favorite = true;
            itemToAdd.Featured = true;
            itemToAdd.CategoryName = "Space Goats";
            itemToAdd.CurrencyName = "Space Bucks";
            itemToAdd.ItemPictureURL = "placeholder.jpg";

            repo.Insert(itemToAdd);

            Assert.AreEqual(9, itemToAdd.ItemId);
        }

        [Test]
        public void CanDeleteItem()
        {
            Item itemToAdd = new Item();
            var repo = new PirateItemRepository();

            itemToAdd.ItemName = "Test Item";
            itemToAdd.RealValue = 15000;
            itemToAdd.DisplayValue = 20000;
            itemToAdd.Description = "This is a test item";
            itemToAdd.Favorite = true;
            itemToAdd.Featured = true;
            itemToAdd.CategoryName = "Space Goats";
            itemToAdd.CurrencyName = "Space Bucks";
            itemToAdd.ItemPictureURL = "placeholder.jpg";

            repo.Insert(itemToAdd);
            Assert.AreEqual(9, itemToAdd.ItemId);

            var loaded = repo.GetItemById(9);
            Assert.IsNotNull(loaded);
            Assert.AreEqual(9, loaded.ItemId);

            repo.Delete(9);
            loaded = repo.GetItemById(9);

            Assert.IsNull(loaded);
        }


    }
}
