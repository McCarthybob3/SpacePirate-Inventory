using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SpacePirateInventory.Data.DapperRepo;
using SpacePirateInventory.Models.Queries;
using SpacePirateInventory.Models.Tables;

namespace SpacePirateInventory.Controllers
{
    public class ItemAPIController : ApiController
    {
        [Route("Inventory/Details")]
        [AcceptVerbs("GET")]
        public IHttpActionResult Search(decimal? minValue, decimal? maxValue, string itemName, string categoryName)
        {
            var repo = new PirateItemRepository();

            try
            {
                var parameters = new ItemSearchParameters()
                {
                    MinValue = minValue,
                    MaxValue = maxValue,
                    ItemName = itemName,
                    CategoryName = categoryName
                };
                var result = repo.Search(parameters);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("Inventory/remove/{ItemId}")]
        [AcceptVerbs("DELETE")]
        public IHttpActionResult RemoveItem(int ItemId)
        {
            var repo = new PirateItemRepository();

            try
            {
                repo.Delete(ItemId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Route("api/article/add")]
        [AcceptVerbs("Post")]
        public IHttpActionResult Add(string Title, string Body)
        {
            var repo = new ArticleRepository();

            try
            {


                var parameters = new Article()
                {
                    ArticleContent = Body,
                    ArticleTitle = Title
                };

                repo.AddArticle(parameters);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}