namespace WebApplication.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WebApplication.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<WebApplication.Data.SimpleShoppingListContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        //the seed method will be used to insert data in the database tables as soon as we create them.
        protected override void Seed(WebApplication.Data.SimpleShoppingListContext context)
        {
            context.ShoppingLists.AddOrUpdate(
                new ShoppingList
                {
                    Name = "Groceriers",
                    Items =
                    {
                        new Item { Name = "milk"},
                        new Item { Name = "conrflakes"},
                        new Item { Name = "stawberries"}
                    }
                },

                new ShoppingList
                {
                    Name = "Hardware"
                }
                );
        }
    }
}
