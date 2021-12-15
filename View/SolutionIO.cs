using MooGameRefactor.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MooGameRefactor.View
{
    public class SolutionIO : IUserInterface
    {
        public string Input()
        {
            string s = Console.ReadLine();
            return s;
        }

        public void Output(string s)
        {
            Console.WriteLine(s);
        }

        public bool ContinueOrEnd(bool running)
        {
            string answer = Console.ReadLine().ToLower();
            if (answer != null && answer != "" && answer.Substring(0, 1) == "n")
            {
                running = false;
            }

            return running;
        }

        public int MultipleChoice(params string[] options)
        {
            const int startX = 0;
            const int startY = 0;

            int currentSelection = 0;

            ConsoleKey key;

            Console.CursorVisible = false;

            do
            {
                Console.Clear();

                for (int i = 0; i < options.Length; i++)
                {
                    Console.SetCursorPosition(startX, startY + i);

                    if (i == currentSelection)
                        Console.ForegroundColor = ConsoleColor.Red;

                    Console.Write(options[i]);

                    Console.ResetColor();
                }

                key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        {
                            if (currentSelection > 0)
                                currentSelection--;
                            break;
                        }
                    case ConsoleKey.DownArrow:
                        {
                            if (currentSelection < options.Length - 1)
                                currentSelection++;
                            break;
                        }
                   
                }

            } while (key != ConsoleKey.Enter);

            Console.CursorVisible = true;
            Console.Clear();
            return currentSelection;
        }
    }
}
