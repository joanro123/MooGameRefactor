using MooGameRefactor.Interfaces;
using MooGameRefactor.Models;
using MooGameRefactor.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MooGameRefactor
{
    public class SaveToFile : IStorage
    {
        private readonly IUserInterface ui = new SolutionIO();

        public void WriteToFile(string name, int nGuess, string? fileName)
        {
            StreamWriter output = new StreamWriter(fileName, append: true);
            output.WriteLine(name + "#&#" + nGuess);
            output.Close();
        }

        public void ShowTopList(string fileName)
        {
            StreamReader input = new StreamReader(fileName);
            List<PlayerData> results = new List<PlayerData>();
            string line;
            while ((line = input.ReadLine()) != null)
            {
                string[] nameAndScore = line.Split(new string[] { "#&#" }, StringSplitOptions.None);
                string name = nameAndScore[0];
                int guesses = Convert.ToInt32(nameAndScore[1]);
                PlayerData pd = new PlayerData(name, guesses);
                int pos = results.IndexOf(pd);
                if (pos < 0)
                {
                    results.Add(pd);
                }
                else
                {
                    results[pos].Update(guesses);
                }


            }
            results.Sort((p1, p2) => p1.Average().CompareTo(p2.Average()));
            ui.Output("Player   games average");
            foreach (PlayerData p in results)
            {
                ui.Output(string.Format("{0,-9}{1,5:D}{2,9:F2}", p.Name, p.NGames, p.Average()));
            }
            input.Close();
        }
        public string[] ShowExistingNames(string fileName)
        {
          //  const string fileName = "result.txt";
            List<string> lines = new();

            using (StreamReader reader = new(fileName))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] nameAndScore = line.Split(new string[] { "#&#" }, StringSplitOptions.None);
                    string name = nameAndScore[0];
                    lines.Add(name);
                }
            }

            var removeDuplicates = lines.Distinct().ToList();

            string[] arr = new string[removeDuplicates.Count + 1];
            removeDuplicates.CopyTo(arr, 0);
            arr[arr.Length - 1] = "Create a New Player";

            return arr;
        }

        public string ReturnName(int num, string[] arr)
        {
            string name = arr[num];
            return name;
        }
    }
}
