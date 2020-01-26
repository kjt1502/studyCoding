using LinqKit;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            #region

            //int x = 5;
            //int y = 6;
            //TestOut(x, y, out int c, out int d);

            //Console.WriteLine(x);
            //Console.WriteLine(y);

            //Swap(ref x, ref y);

            //Console.WriteLine(x);
            //Console.WriteLine(y);

            //ArrayList x = null;
            //Console.WriteLine(x?[0]);
            //Console.WriteLine(x?[1]);
            //Console.WriteLine(x?[2]);

            //x = new ArrayList();
            //x.Add("123");
            //x.Add("456");
            //x.Add("789");
            //Console.WriteLine(x?[0]);
            //Console.WriteLine(x?[1]);
            //Console.WriteLine(x?[2]);


            //int a = 10, 
            //    b = 20;

            //Console.WriteLine($"before change {a}, {b}");

            //Change(ref a, ref b);

            //Console.WriteLine($"after change {a}, {b}");



            //Console.WriteLine(refValue);
            //refValue = ReturnRef();
            //Console.WriteLine(refValue);


            //Product pr = new Product();
            //int normal = pr.GetPrice();
            //ref int refValue = ref pr.GetPrice();

            //pr.PrintPrice();

            //Console.WriteLine(normal);
            //Console.WriteLine(refValue);

            //refValue = 200;

            //pr.PrintPrice();

            //Console.WriteLine(normal);
            //Console.WriteLine(refValue);

            #endregion

            #region MyRegion

            //Class1 c1 = new Class1 ();

            //string normal = c1.returnParam();
            //ref string refString = ref c1.returnParam();

            //Console.WriteLine(normal);
            //Console.WriteLine(refString);

            //c1.printString();

            //refString = "changed";

            //Console.WriteLine(normal);
            //Console.WriteLine(refString);

            //c1.printString();

            //string fullUrl = "asefjose!=/ase  rf=";
            //string encodedFullUrl = HttpUtility.UrlDecode(fullUrl);
            //byte[] byteFullUrl = Encoding.UTF8.GetBytes(fullUrl);

            //string base64FullUrl = Convert.ToBase64String(byteFullUrl);

            //Console.WriteLine(fullUrl);
            //Console.WriteLine(encodedFullUrl);
            //Console.WriteLine(base64FullUrl);

            //for (int i = 0; i < 10; i++)
            //{
            //    Console.WriteLine(i);
            //}
            //DateTime oldTime = DateTime.UtcNow;
            //DateTime now = DateTime.UtcNow;
            //Console.WriteLine("now.ToString()           " + now.ToString());
            //Console.WriteLine("now.ToLongDateString()   " + now.ToLongDateString());
            //Console.WriteLine("now.ToLongTimeString()   " + now.ToLongTimeString());
            //Console.WriteLine("now.ToShortDateString()  " + now.ToShortDateString());
            //Console.WriteLine("now.ToShortTimeString()  " + now.ToShortTimeString());
            //now.AddHours(2.0);

            //string nowString = now.ToString();

            //bool isParsed = DateTime.TryParse(nowString, out DateTime parsedNow);
            //if (isParsed)
            //{
            //    int comp = parsedNow.CompareTo(oldTime);
            //    Console.WriteLine(comp);
            //    comp = oldTime.CompareTo(parsedNow);
            //    Console.WriteLine(comp);
            //    comp = now.CompareTo(now);
            //    Console.WriteLine(comp);
            //    Console.WriteLine(isParsed);
            //}
            #endregion

            Console.WriteLine("freewords");
            string keywords = Console.ReadLine();

            Console.WriteLine("gender");
            string gender = Console.ReadLine();

            Console.WriteLine("taller than ");
            string height = Console.ReadLine();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            using (Users_Entity context = new Users_Entity())
            {
                #region CreateRow

                //for (int i = 10; i < 10000; i++)
                //{
                //    context.Students.Add(new Students
                //    {
                //        id = $"{i}_id",
                //        age = $"{i}_age",
                //        country = $"{i}_country",
                //        gender = $"{i}_gender",
                //        height = $"{i}_height",
                //        name = $"{i}_name",
                //    });
                //    context.SaveChanges();
                //}
                #endregion

                var outter = PredicateBuilder.True<Students>();
                if (!string.IsNullOrEmpty(keywords))
                {
                    #region option1
                    //predicate = predicate.And(i => i.country.Contains(keyword) || i.id.Contains(keyword) || i.name.Contains(keyword));
                    #endregion

                    #region option2


                    //ParameterExpression pe = Expression.Parameter(typeof(Students), "i");

                    //Expression left = Expression.Property(pe, typeof(Students).GetProperty("country"));
                    //Expression right = Expression.Constant(keyword);
                    //Expression e1 = Expression.Equal(left, right);

                    //left = Expression.Property(pe, typeof(Students).GetProperty("gender"));
                    //right = Expression.Constant(keyword);
                    //Expression e2 = Expression.Equal(left, right);

                    //Expression predicateBody = Expression.OrElse(e1, e2);

                    //left = Expression.Property(pe, typeof(Students).GetProperty("id"));
                    //right = Expression.Constant(keyword);
                    //Expression e3 = Expression.Equal(left, right);
                    //predicateBody = Expression.OrElse(predicateBody, e3);

                    //whereCallExpression = Expression.Call(
                    //    typeof(Queryable),
                    //    "Where",
                    //    new Type[] { queryableData.ElementType },
                    //    queryableData.Expression,
                    //    Expression.Lambda<Func<Students, bool>>(predicateBody, new ParameterExpression[] { pe }));
                    #endregion

                    var inner = PredicateBuilder.False<Students>();
                    keywords = keywords.Replace("　", " ");
                    foreach (var keyword in keywords.Split(' '))
                    {
                        inner = inner.Or(i => i.country.Contains(keyword));
                        inner = inner.Or(i => i.id.Contains(keyword));
                        inner = inner.Or(i => i.name.Contains(keyword));
                    }
                    outter = outter.And(inner);
                }

                if (!string.IsNullOrEmpty(gender))
                {
                    var inner = PredicateBuilder.True<Students>();
                    inner = inner.Or(i => i.gender.Contains(gender));
                    outter = outter.And(inner);
                }

                var results = context.Students.AsExpandable().Where(outter).ToList(); ;

                if (!string.IsNullOrEmpty(height))
                {

                    int zz = (int.TryParse(height, out int z)) ? z : 0;
                    List<Students> xx = new List<Students>();
                    foreach (var item in results)
                    {
                        if (int.TryParse(item.height, out int x) && x > zz)
                        {
                            xx.Add(item);
                        }
                    }

                    results = xx;
                    //outter = outter.And(i => Convert.ToUInt32(i.height) > 10);
                }
                //IQueryable<Students> results = queryableData.Provider.CreateQuery<Students>(whereCallExpression);


                //students = students.Where(predicate);
                foreach (var student in results)
                {
                    Console.WriteLine("Student {0}", JsonConvert.SerializeObject(student));
                }

                stopwatch.Stop();
                // Get the elapsed time as a TimeSpan value.
                TimeSpan ts = stopwatch.Elapsed;

                // Format and display the TimeSpan value.
                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    ts.Hours, ts.Minutes, ts.Seconds,
                    ts.Milliseconds / 10);
                Console.WriteLine("RunTime " + elapsedTime);

                //string[] companies = { "Consolidated Messenger", "Alpine Ski House", "Southridge Video", "City Power & Light",
                //   "Coho Winery", "Wide World Importers", "Graphic Design Institute", "Adventure Works",
                //   "Humongous Insurance", "Woodgrove Bank", "Margie's Travel", "Northwind Traders",
                //   "Blue Yonder Airlines", "Trey Research", "The Phone Company",
                //   "Wingtip Toys", "Lucerne Publishing", "Fourth Coffee" };

                //// The IQueryable data to query.  
                //IQueryable<String> queryableData = companies.AsQueryable<string>();

                //// Compose the expression tree that represents the parameter to the predicate.  
                //ParameterExpression pe = Expression.Parameter(typeof(string), "company");

                //// ***** Where(company => (company.ToLower() == "coho winery" || company.Length > 16)) *****  
                //// Create an expression tree that represents the expression 'company.ToLower() == "coho winery"'.  
                //Expression left = Expression.Call(pe, typeof(string).GetMethod("ToLower", System.Type.EmptyTypes));
                //Expression right = Expression.Constant("coho winery");
                //Expression e1 = Expression.Equal(left, right);

                //// Create an expression tree that represents the expression 'company.Length > 16'.  
                //left = Expression.Property(pe, typeof(string).GetProperty("Length"));
                //right = Expression.Constant(16, typeof(int));
                //Expression e2 = Expression.GreaterThan(left, right);

                //// Combine the expression trees to create an expression tree that represents the  
                //// expression '(company.ToLower() == "coho winery" || company.Length > 16)'.  
                //Expression predicateBody = Expression.OrElse(e1, e2);

                //// Create an expression tree that represents the expression  
                //// 'queryableData.Where(company => (company.ToLower() == "coho winery" || company.Length > 16))'  
                //MethodCallExpression whereCallExpression = Expression.Call(
                //    typeof(Queryable),
                //    "Where",
                //    new Type[] { queryableData.ElementType },
                //    queryableData.Expression,
                //    Expression.Lambda<Func<string, bool>>(predicateBody, new ParameterExpression[] { pe }));
                //// ***** End Where *****  

                //// ***** OrderBy(company => company) *****  
                //// Create an expression tree that represents the expression  
                //// 'whereCallExpression.OrderBy(company => company)'  
                //MethodCallExpression orderByCallExpression = Expression.Call(
                //    typeof(Queryable),
                //    "OrderBy",
                //    new Type[] { queryableData.ElementType, queryableData.ElementType },
                //    whereCallExpression,
                //    Expression.Lambda<Func<string, string>>(pe, new ParameterExpression[] { pe }));
                //// ***** End OrderBy *****  

                //// Create an executable query from the expression tree.  
                //IQueryable<string> results = queryableData.Provider.CreateQuery<string>(orderByCallExpression);

                //// Enumerate the results.  
                //foreach (string company in results)
                //    Console.WriteLine(company);
            }
        }




        #region MyRegion

        public static void TestOut(int a, int b, out int c, out int d)
        {
            c = a + b;
            d = a * b;
        }

        public static void Swap(ref int a, ref int b)
        {
            //int tmp = a;
            //a = b;
            //b = tmp;
        }

        public static void Change (ref int first, ref int second)
        {
            int tmp = first;
            first = second;
            second = tmp;
        }

        #endregion
    }

    #region MyRegion2

        class Product
        {
            private int price = 100;

            public ref int GetPrice()
            {

                return ref price;
            }

            public void PrintPrice ()
            {
                Console.WriteLine($"Price = {price}");
            }
        }
    #endregion

}
