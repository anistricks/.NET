using School.Repository;
using SchoolApp.Models;
using SchoolApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.UnitOfWorks
{
    interface IUnitOfWorkSchool
    {
        IRepository<Section> SectionsRepository { get; }

        IStudentRepository StudentsRepository { get; }
    }

}
