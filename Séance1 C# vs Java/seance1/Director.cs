﻿using seance1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace seance1;

// implements Serializable 
// [] -> annotations
[Serializable]
internal class Director : Person
{
    private List<Movie> _directedMovies;

    public Director(string name, string firstname, DateTime birthDate) : base(name, firstname, birthDate)
    {
        _directedMovies = new List<Movie>();
    }

    public bool AddMovie(Movie movie)
    {

        if (_directedMovies.Contains(movie))
            return false;

        if (movie.Director == null)
            movie.Director = this;

        _directedMovies.Add(movie);

        return true;

    }

    public IEnumerator<Movie> Movies()
    {
        return _directedMovies.GetEnumerator();
    }


}
