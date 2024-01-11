using Microsoft.EntityFrameworkCore;
using Seance3.Models;
using System.Runtime.ConstrainedExecution;
using static System.Console;

WriteLine("Numéro d'exercice à exécuter (Exemple : B1)");
string? exerciceNumber = ReadLine();

if (exerciceNumber is not null)
{
    switch (exerciceNumber.ToUpper())
    {
        case "A1":
            ExA1();
            break
        case "B1":
            ExB1();
            break;
        case "B2":
            ExB2();
            break;
        case "B3":
            ExB3();
            break;
        case "B4":
            ExB4();
            break;
        case "B5":
            ExB5();
            break;
        case "B6":
            ExB6();
            break;
        case "B7":
            ExB7();
            break;
        case "C1":
            ExC1();
            break;
        case "C2":
            ExC2();
            break;
        case "D1":
            ExD1();
            break;
        case "E1":
            ExE1();
            break;
        case "E3":
            ExE3();
            break;
        case "E4":
            ExE4();
            break;


        default:
            WriteLine("numéro d'exerice inconnu");
            break;

    }
}

static void ExA1()
{
    Console.WriteLine("Ecrivez une ville : ");
  

   
        using (NorthwindContext context = new NorthwindContext())
        {
        var country = context.OrderDetails
                         .Select( od=>od.Product.Supplier.Country)
                         .Distinct().ToList();

        foreach(var country in context.OrderDetails)
        {
           var paysParProduit=context.OrderDetails
                .Where(od => od.Product.Supplier.County == country)
                .Distinct()
                .Count();


            Console.WriteLine(paysParProduit)
          

        }

                           

        
    }



}
static void ExB1()
{
    Console.WriteLine("Ecrivez une ville : ");
    string? ville = Console.ReadLine();

    if (ville is not null) {
        using (NorthwindContext context = new NorthwindContext())
        {
            var customers = from Customer c in context.Customers
                            where c.City == ville
                            select new { c.CustomerId, c.ContactName };

            foreach (var customer in customers) {
                Console.WriteLine("{0} : {1}", customer.CustomerId, customer.ContactName);
            }

        }
}

   

}

static void ExB2()
{
    using(NorthwindContext context = new NorthwindContext())
    {
         IQueryable<Category> categories  =  from Category c in context.Categories
                                              where c.CategoryName=="Beverages" || c.CategoryName=="Condiments" 
                                              select c;


        foreach (var c in categories) {
            
            WriteLine("Catégorie :  " + c.CategoryName);
            
            foreach (var ca in c.Products )
            {
                Console.WriteLine("{0}", ca.ProductName);
            }
            WriteLine("\n");
        }
    }

}

static void ExB3()
{
    using (NorthwindContext context = new NorthwindContext())
    {
        IQueryable<Category> categories = from Category c in context.Categories.Include(c => c.Products)
                                          where c.CategoryName == "Beverages" || c.CategoryName == "Condiments"
                                          select c;


        foreach (var c in categories)
        {

            WriteLine("Catégorie :  " + c.CategoryName);

            foreach (var ca in c.Products)
            {
                Console.WriteLine("{0}", ca.ProductName);
            }
            WriteLine("\n");
        }
    }

}

static void ExB4()
{
    Console.WriteLine("Entre l'ID du client");
    string? idclient = Console.ReadLine();

    if(idclient != null)
    {
        using(NorthwindContext context = new NorthwindContext())
        {
            var client = from Order o in context.Orders
                         where o.CustomerId == idclient && o.ShippedDate!!=null
                         orderby o.OrderDate descending
                         select new { o.CustomerId, o.OrderDate, o.ShippedDate };

            foreach(var c in client)
            {
                WriteLine("CustomerID:{0}    OrderDate:{1}   ShippedDate:{2}",c.CustomerId,c.OrderDate,c.ShippedDate);
            }

        }
    }



}


static void ExB5()
{
   using(NorthwindContext context=new NorthwindContext())
    {
        var ventes = from OrderDetail od in context.OrderDetails
                     orderby od.ProductId
                     group od by od.ProductId;

        foreach(IGrouping<int,OrderDetail> group in ventes)

        {
            Console.WriteLine(group.Key+"----> " + group.Sum(o => o.UnitPrice * o.Quantity));
        }
    }




}


static void ExB6()
{
    using(NorthwindContext context = new NorthwindContext())
    {
        IQueryable<Employee> emp =from Employee e in context.Employees
                where e.Territories.Any(t=>t.Region.RegionDescription.Equals("Western"))
                select e;
        foreach (Employee e in emp)
        {
            Console.WriteLine("{0} {1}",e.LastName,e.FirstName);
        }
    }
    


}

//  7.	Quels sont les territoires gérés par le supérieur de « Suyama Michael »
static void ExB7()
{
    using (NorthwindContext context = new NorthwindContext())
    {
        var ter = (from Employee e in context.Employees
                   where e.LastName.Equals("Suyama")
                   select e.ReportsToNavigation.Territories).SingleOrDefault();
        foreach (Territory t in ter)
        {
            WriteLine(t.TerritoryDescription);
        }
    }
   


}


//8 


static void ExC1()
{
    using (NorthwindContext contexte=new NorthwindContext())
    {
        IQueryable<Customer> cust = from Customer c in contexte.Customers
                                 select c;

        foreach(Customer c in cust)
        {
            c.ContactName = c.ContactName.ToUpper();
        }
        try
        {
            contexte.SaveChanges();

        }catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        WriteLine("Done");
    }


}
static void ExC2()
{
    using (NorthwindContext context = new NorthwindContext())
    {
        WriteLine("Vérifier que tous les noms des clients sont en majuscule");


        IQueryable<Customer> custom = (from c in context.Customers
                                       select c);


        foreach (Customer cust in custom)
        {
            WriteLine(cust.ContactName);
        }
    }


}



static void ExD1()
{
    WriteLine("Ecrivez la catégorie a ajouté");
    string? cat = Console.ReadLine();
    try
    {


        using (NorthwindContext context = new NorthwindContext())
        {
            Category? categ = (from Category c in context.Categories
                               where c.CategoryName == cat
                               select c).SingleOrDefault<Category>();
            if (categ == null && cat is not null)
            {
                categ = new Category { CategoryName = cat };
                context.Categories.Add(categ);
                context.SaveChanges();
            }
            else
            {
                WriteLine("Une categorie exite deja ");
            }

            foreach (var c in context.Categories)
            {
                WriteLine(c.CategoryName);
            }
        }
    }
      catch (Exception e)
      {
        Console.WriteLine(e);
      }
}


static void ExE1()
{
    WriteLine("Ecrivez la catégorie a supprimé");
    string? cat = Console.ReadLine();
    try
    {


        using (NorthwindContext context = new NorthwindContext())
        {
            Category? categ = (from Category c in context.Categories
                               where c.CategoryName == cat
                               select c).SingleOrDefault<Category>();
            if (categ != null && cat is not null)
            {
                
                context.Categories.Remove(categ);
                context.SaveChanges();
            }
            else
            {
                WriteLine("la categorie existe pas  ");
            }

            foreach (var c in context.Categories)
            {
                WriteLine(c.CategoryName);
            }
        }
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
    }
}



static void ExE3()
{
    WriteLine("Entrez l'ID de l'employé à supprimer");
    string? emp1 = ReadLine();
    WriteLine("Entrez l'ID de l'employé qui reprend les Orders de celui à supprimer");
    string? emp2 = ReadLine();

    if (!int.TryParse(emp1, out int e1))
    {
        WriteLine("Employé 1 inconnu");
    }
    if (!int.TryParse(emp2, out int e2))
    {
        WriteLine("Employé 2 inconnu");
    }

    using (NorthwindContext context = new NorthwindContext())
    {

        Employee employee1 = (from e in context.Employees.Include("Territories").Include("InverseReportsToNavigation")
                              where e.EmployeeId == e1
                              select e).Single<Employee>();


        Employee employee2 = (from e in context.Employees
                              where e.EmployeeId == e2
                              select e).Single<Employee>();

        IQueryable<Order> employee1Orders = (from o in context.Orders
                                             where o.EmployeeId == e1
                                             select o);


        foreach (Order o in employee1Orders)
        {
            employee2.Orders.Add(o);
            employee1.Orders.Remove(o);

        }

        employee1.Territories.Clear();
        employee1.InverseReportsToNavigation.Clear();



        context.Employees.Remove(employee1);
        int affected = context.SaveChanges();
        WriteLine("Nombre de lignes affectées " + affected);








    }





   


}






static void ExE4()
{



    using (NorthwindContext context = new NorthwindContext())
    {
        IQueryable<Employee> e = from Employee ec in context.Employees
                                 where ec.EmployeeId == 2
                                 select ec;

        foreach (Employee emp in e)
        {
            WriteLine(emp.InverseReportsToNavigation.ToString());
        }

    }
}


