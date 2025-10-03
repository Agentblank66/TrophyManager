using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace TrophyManager
{
    public class TrophiesRepository
    {
        /// <summary>
        /// Thiese are the instance fields.
        /// </summary>
        private List<Trophy> _trophies = new List<Trophy>
        {
            new Trophy(1, "Champions League", 2020),
            new Trophy(2, "Premier League", 2019),
            new Trophy(3, "FA Cup", 2018),
            new Trophy(4, "La Liga", 2021),
            new Trophy(5, "Serie A", 2022)
        };


        /// <summary>
        /// returns the list of trophies.
        /// </summary>
        /// <returns> _trophies </returns>
        public List<Trophy> Get()
        {
            return _trophies;
        }

        /// <summary>
        /// returns the list of trophies filtered by year.
        /// </summary>
        /// /// <param name="year"></param>
        /// <returns> _trophies filtered by year </returns>
        public List<Trophy> GetFilteredByYear(int year)
        {
            return _trophies.Where(t => t.Year == year).ToList();
        }

        /// <summary>
        /// Sorts the trophies based on the specified criteria (competition or year).
        /// </summary>
        /// <param name="sortby"></param>
        /// <returns></returns>
        public List<Trophy> GetSorted(string sortby)
        {
            var sortedTrophies = _trophies;
            return sortby.ToLower() switch
            {
                "competition" => sortedTrophies.OrderBy(t => t.Competition).ToList(),
                "year" => sortedTrophies.OrderBy(t => t.Year).ToList(),
                _ => sortedTrophies
            };
        }

        /// <summary>
        /// Gets a trophy by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> The wanted trophy </returns>
        /// <exception cref="ArgumentException"> if no trophy was found Exception is thrown </exception>
        public Trophy GetById(int id)
        {
            var trophy = _trophies.FirstOrDefault(t => t.Id == id);
            if (trophy == null)
            {
                throw new ArgumentException($"No trophy found with ID {id}.");
            }
            return trophy;
        }

        /// <summary>
        /// Adds the specified trophy to the collection.
        /// </summary>
        /// <param name="trophy"></param>
        /// <returns> The trophy that was added to the collection. </returns>
        public Trophy Add(Trophy trophy)
        {
            _trophies.Add(trophy);
            return trophy;
        }

        /// <summary>
        /// This method removes a trophy from the collection based on its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> The trophy that was removed from the collection. </returns>
        public Trophy Remove(int id)
        {
            var trophy = GetById(id);
            _trophies.Remove(trophy);
            return trophy;
        }

        /// <summary>
        /// This method updates the details of an existing trophy based on its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedTrophy"></param>
        /// <returns> The updated trophy </returns>
        public Trophy Update(int id, Trophy updatedTrophy)
        {
            var trophy = GetById(id);
            trophy.Competition = updatedTrophy.Competition;
            trophy.Year = updatedTrophy.Year;
            return trophy;
        }


    }
}
