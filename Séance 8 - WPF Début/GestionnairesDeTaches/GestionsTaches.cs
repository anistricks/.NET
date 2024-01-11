using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionnairesDeTaches
{
    public class GestionsTaches
    {
        private IList<Tache>?tachesList;
        public GestionsTaches()
        {

            Tache t1 = new Tache("super tâche");

            tachesList = new List<Tache>();
            tachesList.Add(t1);
        }

        public IList<Tache>? TachesList
        {
            get { return tachesList; }
        }


    }

    public class Tache
    {
        private string? _description;
        private int? _priorité;
        private DateTime? _date;
        private bool? _termine;

        public Tache(string? description)
        {
            _description = description;
            _priorité = 3;
            _date = DateTime.Now;
            _termine = false;
        }

        public string? Description { get => _description; set => _description = value; }
        public int? Priorité { get => _priorité; set => _priorité = value; }
        public DateTime? Date { get => _date; set => _date = value; }
        public bool? Termine { get => _termine; set => _termine = value; }
    }


}
