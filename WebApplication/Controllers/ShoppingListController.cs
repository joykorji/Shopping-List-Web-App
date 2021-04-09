using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class ShoppingListController : ApiController
    {

        public static List<ShoppingList> shoppingLists = new List<ShoppingList>
        {

            // this is a dommy shopping list will be used to test before creating the database 
            new ShoppingList() { Id = 0, Name = "Groceries", Items = {
                new Item { Id =0,  Name = "Milk", ShoppingListId =0 },
                new Item { Id =1,  Name = "conrflakes", ShoppingListId =0  },
                new Item { Id =2,  Name = "strawberries", ShoppingListId =0  }
                }
            },
            new ShoppingList() { Id = 1, Name = "Hareware" }
        };


        // this function will be called from the app.js "getShoppingListById function"
        // GET: api/ShoppingList/5
        public IHttpActionResult Get(int id)
        {
            ShoppingList result =
                shoppingLists.FirstOrDefault(s => s.Id == id);
            if(result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // this function will be called from the app.js "createShoppingList function"
        // POST: api/ShoppingList
        public IEnumerable Post([FromBody]ShoppingList newList) // FromBody here tells that the data for the request is 
        {                                                 // from the body and there is nothing is the url of the request
            newList.Id = shoppingLists.Count;
            shoppingLists.Add(newList);

            return shoppingLists; // this will send an object to our backend and this object is the new shoppinglist 
        }

       
    }
}
