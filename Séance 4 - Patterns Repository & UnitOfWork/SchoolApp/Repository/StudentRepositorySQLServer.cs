using Microsoft.EntityFrameworkCore;
using Repository;
using SchoolApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Repository
{
    public class StudentRepositorySQLServer : BaseRepositorySQL<Student>, IStudentRepository
    {

        public StudentRepositorySQLServer(SchoolContext dbcontext) : base(dbcontext) { }

        public IList<Student> GetStudentBySectionOrderByYearResult()
        {
            IList<Student> students = (from Student s in _dbContext.Set<Student>()
                                       orderby s.Section.Name, s.YearResult descending
                                       select s).ToList();

            return students;
        }
    }

}
