using Msi.UtilityKit.Search;
using Msi.UtilityKit;
using System;
using System.Collections.Generic;
using System.Linq;
using Msi.UtilityKit.Security;

namespace UtilityKit
{
    class Program
    {
        static void Main(string[] args)
        {

            var users = new List<User>
            {
                new User { Id = 1, Name = "A" },
                new User { Id = 2, Name = "B" },
                new User { Id = 3, Name = "B" },
            }.AsQueryable();

            SecurityUtilities.ConfigureStaticAesOptions(x =>
            {
                x.Key = "";
                x.Secret = "";
            });

            string str = "Normal String";
            string encryptedString = str.Encrypt();

            var searchOptions = new SearchOptions
            {
                Search = new string[] { "name eq B" }
            };

            var result = users.ApplySearch(searchOptions).ToList();

            var r = result.IsEnumerable();

            #region Old

            //// find type
            //var type = TypeUtilities.FindByName<Program>("user");

            //var address = new Address { Street = "Dhaka", Post = 1200 };
            //var user = new User { Name = "Shahid", Address = address };
            //var calc = new Calculator();

            //#region get property value
            //var name = user.GetValue<string>("name");
            //#endregion

            //#region get nested property value
            //var post = user.GetValue("address.post");
            //#endregion

            //#region set property value
            //user.SetValue("Shahidul Islam", "name");
            //#endregion

            //user.SetValue(1300, "address.post");

            //#region set nested property value
            //ObjectUtilities.SetValue(user, "Tangail", "address.street");
            //#endregion

            //#region invoke method
            //var addr = ObjectUtilities.InvokeMethod(user, "address.getaddress");
            //#endregion

            //#region invoke method with params
            //var sum = ObjectUtilities.InvokeMethod(calc, "add", 1, 2);
            //#endregion

            //#region dynamic where predicate
            //int[] ints = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            //var expr = ExpressionUtilities.New<int>(x => x > 1);
            //expr = expr.And(x => x <= 5);
            //var r = ints.Where(expr.Compile());
            //#endregion

            #endregion

            Console.ReadLine();
        }
    }
}
