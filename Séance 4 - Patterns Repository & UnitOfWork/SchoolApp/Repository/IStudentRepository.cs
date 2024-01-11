using School.Repository;
using SchoolApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Repository
{
    public interface IStudentRepository : IRepository<Student>
    {
        IList<Student> GetStudentBySectionOrderByYearResult();
    }
}
