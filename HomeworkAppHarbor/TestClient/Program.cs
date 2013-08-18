using CodeJewelData;
using CodeJewelData.Migrations;
using CodeJewelModels;
using System;
using System.Data.Entity;
using System.Linq;

namespace TestClient
{
    class Program
    {
        static void Main()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<CodeContext, Configuration>());
            var context = new CodeContext();
            var cat = new Category
            {
                Name = "Category 1"
            };
            context.Categories.Add(cat);
            context.SaveChanges();

            var cats = from c in context.Categories
                       select c;
            foreach (var c in cats)
            {
                Console.WriteLine(c.Id);
                Console.WriteLine(c.Name);
            }
        }
    }
}