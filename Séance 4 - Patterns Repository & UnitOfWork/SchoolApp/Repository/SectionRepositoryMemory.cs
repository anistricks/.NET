using SchoolApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Repository
{
     class SectionRepositoryMemory
    {
        private List<Section> _sections;

        public SectionRepositoryMemory()
        {
            this._sections = new List<Section>();
        }

        public void Delete(Section entity)
        {
            this._sections.Remove(entity);
        }

        public IList<Section> GetAll()
        {
            return this._sections;
        }

        public Section GetById(int id)
        {
            return this._sections.Where(s => s.SectionId == id).SingleOrDefault();
        }

        public void Insert(Section entity)
        {
            this._sections.Add(entity);
        }

        public bool Save(Section entity, Expression<Func<Section, bool>> predicate)
        {
            Section s = SearchFor(predicate).SingleOrDefault();

            if (s == null)
            {
                Insert(entity);

            }
            return true;
        }

        public IList<Section> SearchFor(Expression<Func<Section, bool>> predicate)
        {
            return this._sections.AsQueryable().Where(predicate).ToList();
        }
    }


}

