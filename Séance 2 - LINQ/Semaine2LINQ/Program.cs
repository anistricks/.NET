using LINQDataContext;

DataContext dc=new DataContext();


Student? jdepp = (from student in dc.Students
                  where student.Login == "jdepp"
                  select student).SingleOrDefault();

if (jdepp != null)
{
    Console.WriteLine(jdepp.Last_Name + jdepp.First_Name);
}



Console.WriteLine("\n");



var students = from Student s in dc.Students
                    select new { s.First_Name,s.Last_Name,s.BirthDate,s.Login,
                    s.Year_Result};

foreach(var student in students)
{
    Console.WriteLine("{0} {1} est née le {2} et a  comme login {3} et resultat :{4}",
    student.First_Name,student.Last_Name,student.BirthDate,student.Login,student.Year_Result);
}
