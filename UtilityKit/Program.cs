using Msi.UtilityKit;
using System;
using System.Linq;

namespace UtilityKit
{
    class Program
    {
        static void Main(string[] args)
        {

            // find type
            var type = TypeUtil.FindByName<Program>("user");

            var address = new Address { Street = "Dhaka", Post = 1200 };
            var user = new User { Name = "Shahid", Address = address };
            var calc = new Calculator();

            #region get property value
            var name = ObjectUtil.GetValue(user, "name");
            #endregion

            #region get nested property value
            var post = ObjectUtil.GetValue(user, "address.post");
            #endregion

            #region set property value
            ObjectUtil.SetValue(user, "Shahidul Islam", "name");
            #endregion

            #region set nested property value
            ObjectUtil.SetValue(user, "Tangail", "address.street");
            #endregion

            #region invoke method
            var addr = ObjectUtil.InvokeMethod(user, "address.getaddress");
            #endregion

            #region invoke method with params
            var sum = ObjectUtil.InvokeMethod(calc, "add", 1, 2);
            #endregion

            #region dynamic where predicate
            int[] ints = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var expr = ExpressionUtil.New<int>(x => x > 1);
            expr = expr.And(x => x <= 5);
            var r = ints.Where(expr.Compile());
            #endregion

            Console.ReadLine();
        }
    }
}
