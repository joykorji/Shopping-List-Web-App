using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class ItemController : ApiController
    {
       

        // POST: api/Item
        public IHttpActionResult Post([FromBody]Item item)
        {
            ShoppingList shoppingList =
                ShoppingListController.shoppingLists.Where(s => s.Id == item.ShoppingListId).FirstOrDefault();

            if(shoppingList == null)
            {
                return NotFound(); 
            }

            if(shoppingList.Items.Count == 0)
            {
                item.Id = 0;
            }
            else
            {
                item.Id = shoppingList.Items.Max(i => i.Id) + 1;
            }
            

            shoppingList.Items.Add(item);

            return Ok(shoppingList);
        }

        // PUT: api/Item/5
        public IHttpActionResult Put(int id, [FromBody]Item item)
        {
            ShoppingList shoppingList =
                ShoppingListController.shoppingLists
                .Where(s => s.Id == item.ShoppingListId)
                .FirstOrDefault();

            if (shoppingList == null)
            {
                return NotFound();
            }

            Item ChangedItem = shoppingList.Items.Where(i => i.Id == id).FirstOrDefault();

            if(ChangedItem == null)
            {
                return NotFound();
            }

            ChangedItem.Checked = item.Checked;

            return Ok(shoppingList);
        }

        // DELETE: api/Item/5
        public IHttpActionResult Delete(int id)
        {
            ShoppingList shoppingList = ShoppingListController.shoppingLists[0]; // here we pretend that all the items  we want to delete 
                                                                                 // are in the first shopping list ( because we dont have entity framework yet ) 
            Item item = shoppingList.Items.FirstOrDefault(i => i.Id == id);

            if(item == null)
            {
                return NotFound();
            }

            shoppingList.Items.Remove(item);

            return Ok(shoppingList);

        }
    }
}
