// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");
using LINQDataContext;
using System.Linq;
using System.Runtime.InteropServices;

DataContext dc = new DataContext();



Student? jdepp = (from student in dc.Students
                  where student.Login == "jdepp"
                  select student).SingleOrDefault();

if (jdepp != null)
{
    Console.WriteLine(jdepp.Last_Name + jdepp.First_Name);
}



// Exercices 2.1  version 1

var studentViewVersion1= from Student s in dc.Students
                select new {s.Last_Name,s.BirthDate,s.Login,s.Year_Result} ;
// Exercices 2.1  version 2
var studentViewVersion2 = dc.Students.Select(s => new { s.Last_Name, s.BirthDate, s.Login, s.Year_Result });

foreach (var student in studentViewVersion2)
{ Console.WriteLine("{0} est né le {1}  avec le login {2} et a obtenu {3}",
    student.Last_Name,student.BirthDate,student.Login,student.Year_Result); }


// Exercices 2.2 

var studentView = dc.Students.Select(s => new { Fullname=s.First_Name+" "+s.Last_Name,s.Student_ID,s.BirthDate});

foreach (var student in studentView)
{
    Console.WriteLine("{0} id:{1} anniversaire:{2} ", student.Fullname, student.Student_ID, student.BirthDate);
}

//Exercices 2.3 
var studenView = dc.Students.Select(s => new {FullInfoStudent=s.Last_Name+" | "+s.First_Name+" | "+s.Student_ID+" | "+s.BirthDate});

foreach (var student in studenView)
{
    Console.WriteLine("{0}",student.FullInfoStudent);
}

//////////////////////////////////////////////////////////////////////////////////////////////////////////////

//Exercices3.1

var stud31 = from Student s in dc.Students
             where s.BirthDate.Year < 1995
             select new { s.Last_Name, s.Year_Result, status = (s.Year_Result >= 12) ? "OK":"KO"};
Console.WriteLine("Exo 3.1");
foreach (var student in stud31)
{
    Console.WriteLine("{0}",student);
}

//Exercices3.2
var stud32 = from Student s in dc.Students
             where s.BirthDate.Year >= 1955 && s.BirthDate.Year <= 1965
             select new { s.Last_Name,s.Year_Result,
             category=(s.Year_Result < 10)
             ?"inférieur":(s.Year_Result==10)?"neutre":"supérieur"};
Console.WriteLine("Ex 3.2");
foreach (var student in stud32)
{
    Console.WriteLine("{0}", student);
}

//Exercices3.3

var stud33=from Student s in dc.Students
           where s.Last_Name != null && s.Last_Name.EndsWith("r")
           select new
           {
               s.Last_Name,
               s.Year_Result,
               category = (s.Year_Result < 10)
             ? "inférieur" : (s.Year_Result == 10) ? "neutre" : "supérieur"
           };
Console.WriteLine("Ex 3.3");
foreach (var student in stud33)
{
    Console.WriteLine("{0}", student);
}

//Exercices3.4

var stud34 = dc.Students.Where(s => s.Year_Result <= 3).OrderByDescending(s=>s.Year_Result).Select(s => new {
    s.Last_Name,
    s.Year_Result,
    category = (s.Year_Result < 10)
             ? "inférieur" : (s.Year_Result == 10) ? "neutre" : "supérieur"
});
Console.WriteLine("Ex 3.4");
foreach (var student in stud34)
{
    Console.WriteLine("{0}", student);
}

// 3.5

var stud35 = dc.Students.Where(s => s.Section_ID == 1110).OrderBy(s=>s.Last_Name)
    .Select(s => new { Fullname = s.Last_Name + " " + s.First_Name, s.Year_Result });
Console.WriteLine("Ex 3.5");
foreach (var stud in stud35)
{
    Console.WriteLine(stud);
}

// 3.6

var stud36 = dc.Students.Where(s => s.Section_ID == 1010 || s.Section_ID==1020 && s.Year_Result<12 && s.Year_Result>18)
    .OrderBy(s => s.Section_ID)
    .Select(s => new { Fullname = s.Last_Name + " " + s.Section_ID, s.Year_Result });
Console.WriteLine("Ex 3.6");
foreach (var stud in stud36)
{
    Console.WriteLine(stud);
}

//3.7

var stud37 = dc.Students.Where(s =>s.Section_ID.ToString().StartsWith("13") && s.Year_Result * 5 <= 60)
    .OrderByDescending(s => (s.Year_Result*5))
    .Select(s => new { Fullname = s.Last_Name + " " + s.Section_ID, result_100 = (s.Year_Result*5) });
Console.WriteLine("Ex 3.7");
foreach (var stud in stud37)
{
    Console.WriteLine(stud);
}


//4.1

var stud41 = dc.Students.Average(s => s.Year_Result);
Console.WriteLine("Ex 4.1");
Console.WriteLine("Moyenne :"+stud41);

//4.2
var stud42 = dc.Students.Max(s => s.Year_Result);
Console.WriteLine("Ex 4.2");
Console.WriteLine("Max =" + stud42);

//4.3
var stud43 = dc.Students.Sum(s => s.Year_Result);
//var stud43 = dc.Students.Sum(s => (int?)s.Year_Result ?? 0);
Console.WriteLine("Ex 4.3");
Console.WriteLine("Sum =" + stud43);

//4.4
var stud44 = dc.Students.Min(s => s.Year_Result);

Console.WriteLine("Ex 4.4");
Console.WriteLine("Min =" + stud44);

//4.5

var stud45 = dc.Students.Count();
Console.WriteLine("count =" + stud45);



////////////////////////////////////////////// EXO 5 \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\


//5.1

var stud51 = dc.Students.GroupBy(s => s.Section_ID).Select( g => new { SectionID = g.Key,Max_Result=g.Max(s=>s.Year_Result)} );
Console.WriteLine("Ex 5.1");
foreach (var section in stud51)
{
    Console.WriteLine("Section ID: " + section.SectionID + ", Max Result: " + section.Max_Result);
}


var sectionsResult = from Student s in dc.Students
                     group s by s.Section_ID;



Console.WriteLine("Ex 5.11");
foreach (IGrouping<Int32, Student> section in sectionsResult)
{
    Console.WriteLine("Le max de la section {0} est {1}", section.Key, section.Max(s => s.Year_Result));

}


//5.2
var stud52 = dc.Students.GroupBy(s => s.Section_ID).Select(g => new { SectionID = g.Key, Average_Result = g.Average(s => s.Year_Result) });
Console.WriteLine("Ex 5.2");
foreach (var section in stud52)
{
    Console.WriteLine("Section ID: " + section.SectionID + ", Average Result: " + section.Average_Result);
}
//5.3

var stud53 = dc.Students.Where(s=>s.BirthDate.Year >=1970 && s.BirthDate.Year <= 1985).GroupBy(e=>e.BirthDate.Month)
 .Select(g => new { date=g.Key,SectionID = g.Key, Average_Result = g.Average(s => s.Year_Result) });
Console.WriteLine("Ex 5.3");
foreach (var section in stud53)
{
    Console.WriteLine("Mois: " + section.date + ", Average Result: " + section.Average_Result);
};

var resultMoy = from Student s in dc.Students
                where (s.BirthDate.Year >= 1970
                && s.BirthDate.Year <= 1985)
                group s by s.BirthDate.Month;

Console.WriteLine("Ex 5.3");

foreach (IGrouping<Int32, Student> res in resultMoy)
{
    Console.WriteLine("Mois : " + res.Key);
    Console.WriteLine("result moyen : " + res.Average(s => s.Year_Result));
}

//5.4
var avgResultsForSections = dc.Students
    .GroupBy(s => s.Section_ID)
    .Where(g => g.Count() > 3)
    .Select(g => new {
        SectionID = g.Key,
        AVG_Results = g.Average(s => s.Year_Result)
    });

Console.WriteLine("Ex 5.4");
foreach (var section in avgResultsForSections)
{
    Console.WriteLine("Section ID: " + section.SectionID + ", Average Result: " + section.AVG_Results);
}


//5.5

var prof55 = from Cours in dc.Courses
             join prof in dc.Professors on Cours.Professor_ID equals prof.Professor_ID
             join sec in dc.Sections on prof.Section_ID equals sec.Section_ID
             select new { Cours.Course_Name,sec.Section_Name,prof.Professor_Name};

Console.WriteLine("Ex 5.5");
foreach (var prof in prof55)
{
    Console.WriteLine("coursename:{0}  | sectionname:{1}   | profname:{2} " ,prof.Course_Name ,prof.Section_Name,prof.Professor_Name );
}

var query = from Cours in dc.Courses
            join prof in dc.Professors on Cours.Professor_ID equals prof.Professor_ID
            join sect in dc.Sections on prof.Section_ID equals sect.Section_ID
            select new { Cours.Course_Name, prof.Professor_Name, sect.Section_Name };

Console.WriteLine("5.5");
foreach (var res in query)
{
    Console.WriteLine(res.Course_Name + " " + res.Section_Name + " " + res.Professor_Name);
}


//5.6

var sectionDelegatesQuery = from Section s  in dc.Sections
                            join stud in dc.Students on s.Delegate_ID equals stud.Student_ID
                            orderby s.Section_ID descending
                            select new { s.Section_ID, s.Section_Name, DelegateLastName=stud.Last_Name };

Console.WriteLine("Ex 5.6");
foreach (var sect in sectionDelegatesQuery)
{
    Console.WriteLine("Section ID: {0}, Section Name: {1}, Delegate Last Name: {2}",
                      sect.Section_ID,
                      sect.Section_Name,
                      sect.DelegateLastName);
}



/*
var sectionDelegates = dc.Sections
    .Join(dc.Students,
          sect => sect.Delegate_ID,
          stud => stud.Student_ID,
          (sect, stud) => new {
              SectionID = sect.Section_ID,
              SectionName = sect.Section_Name,
              DelegateLastName = stud.Last_Name
          })
    .OrderByDescending(sect => sect.SectionID);

Console.WriteLine("Ex 5.6");
foreach (var section in sectionDelegates)
{
    Console.WriteLine($"Section ID: {section.SectionID}, Name: {section.SectionName}, Delegate: {section.DelegateLastName}");
}*/


//5.7
Console.WriteLine("5.7");

var prof57 = from Section s in dc.Sections
             join prof in dc.Professors on s.Section_ID equals prof.Section_ID into secProfs
             select new { nomSection = s.Section_Name,nomsProf=secProfs };

foreach(var jointure in prof57)
{
    Console.WriteLine("Section : " + jointure.nomSection);
    foreach(var prof in jointure.nomsProf)
    {
        Console.WriteLine("Prof=" + prof.Professor_Name);
    }
}


//5.11
Console.WriteLine("5.11");
var prof511 = from Course c in dc.Courses
              group c by c.Professor_ID into secCourses
              join prof in dc.Professors on secCourses.Key equals prof.Professor_ID
              select new { proffeseurID = secCourses.Key, ECTSTOT = secCourses.Sum(cg => cg.Course_Ects) } into results
              orderby results.ECTSTOT descending
              select results;

foreach (var item in prof511)
{
    Console.WriteLine("prof:{0} etcs:{1}",item.proffeseurID,item.ECTSTOT);

}
