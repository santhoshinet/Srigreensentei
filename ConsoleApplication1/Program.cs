using System;
using System.Collections.Generic;
using System.Linq;
using CCAvenueIntegrationDL;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            // Database Seeding
            var scope = ObjectScopeProvider1.GetNewObjectScope();

            // Administrator declarations
            var administrators = new[] { "santhoshinet" };
            int count = (from c in scope.GetOqlQuery<User>().ExecuteEnumerable()
                         select c).Count();
            if (count > 0)
            {
                foreach (var administrator in administrators)
                {
                    string administrator1 = administrator;
                    List<User> users = (from c in scope.GetOqlQuery<User>().ExecuteEnumerable()
                                        where c.Username.ToLower().Equals(administrator1.ToLower())
                                        select c).ToList();
                    if (users.Count > 0)
                    {
                        foreach (var user in users)
                        {
                            scope.Transaction.Begin();
                            user.IsheAdmin = true;
                            scope.Add(user);
                            scope.Transaction.Commit();
                        }
                    }
                }
            }
            else
            {
                scope.Transaction.Begin();
                var user = new User();
                user.Username = "santhoshinet";
                user.Email = "santhoshonet@gmail.com";
                user.IsheAdmin = true;
                scope.Add(user);
                scope.Transaction.Commit();
            }

            var specialCategories = new[] { "Gift-Plants", "AllGardenNeeds", "ExoticPlants", "ImportExportPlants", "RentalPlants" };
            foreach (var specialCategory in specialCategories)
            {
                List<Category> categories = (from c in scope.GetOqlQuery<Category>().ExecuteEnumerable()
                                             where c.Name.ToLower().Equals(specialCategory.ToLower())
                                             select c).ToList();
                if (categories.Count == 0)
                {
                    // creating category for gift a plant
                    scope.Transaction.Begin();
                    var category = new Category();
                    category.Categorytype = Categorytype.Special;
                    category.Deletedstatus = DeleteStatus.Working;
                    category.Id = Guid.NewGuid();
                    category.Name = specialCategory;
                    scope.Add(category);
                    scope.Transaction.Commit();
                }
            }
        }
    }
}