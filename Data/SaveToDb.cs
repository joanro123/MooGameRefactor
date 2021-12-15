using MooGameRefactor.Interfaces;
using MooGameRefactor.Models;
using MooGameRefactor.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MooGameRefactor
{
    public class SaveToDb : IStorage
    {
        private readonly IUserInterface ui = new SolutionIO();
        ResultContext _context = new();

        public void WriteToFile(string name, int nGuesses, string? fileName)
        {
            using (var db = new ResultContext())
            {
                var result = new Result()
                {
                    Name = name,
                    Guesses = nGuesses
                };
                db.Results.Add(result);
                db.SaveChanges();
            }
       //  ui.Output("Correct, it took " + nGuesses + " guesses.");
        }

        public void ShowTopList(string? fileName)
        {
            var numberOfGames = _context.Results.GroupBy(g => g.Name).ToList();

            foreach (var item in numberOfGames)
            {
                ui.Output(item.Key + "\tnumber of games: " + CountNames(item.Key) + "\tAverage number of guesses: " + AverageGuesses(item.Key));               
            }
            ui.Output("\n");
        }

        private int CountNames(string name)
        {
            var q = _context.Results.Where(w => w.Name == name).Count();
            return q;
        }

        private double AverageGuesses(string name)
        {
            var q = _context.Results.Where(w => w.Name == name).Select(s => s.Guesses).Average();
            q = Math.Round(q, 1);
            return q;
        }

        public string[] ShowExistingNames(string? fileName)
        {
            var names = _context.Results.GroupBy(g => g.Name).ToArray();
            string[] arr = new string[names.Length];

            for (int i = 0; i < names.Length; i++)
            {
                arr[i] = names[i].Key;
            }

            string[] arr2 = new string[arr.Length + 1];
            arr.CopyTo(arr2, 0);
            arr2[arr2.Length - 1] = "Create a New Player";

            return arr2;

            //foreach (var item in names)
            //{
            //    ui.Output(item.Key);
            //}
        }

        public string ReturnName(int num, string[] arr)
        {
            string name = arr[num];
            return name;
        }
    }
}
