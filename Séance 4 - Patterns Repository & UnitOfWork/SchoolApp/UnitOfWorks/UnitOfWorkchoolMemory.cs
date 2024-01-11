
using School.Repository;
using SchoolApp.Models;
using SchoolApp.Repository;


namespace SchoolApp.UnitOfWorks
{
    class UnitOfWorkSchoolMemory : IUnitOfWorkSchool
    {
        private SectionRepositoryMemory _sectionsRepository;

        private StudentRepositoryMemory _studentsRepository;

        public UnitOfWorkSchoolMemory()
        {
            this._sectionsRepository = new SectionRepositoryMemory();
            this._studentsRepository = new StudentRepositoryMemory();
        }

        public IRepository<Section> SectionsRepository
        {
            get { return (IRepository<Section>)this._sectionsRepository; }
        }

        public IStudentRepository StudentsRepository
        {
            get { return (IStudentRepository)this._studentsRepository; }
        }
    }

}
