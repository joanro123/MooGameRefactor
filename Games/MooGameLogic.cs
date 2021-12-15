using MooGameRefactor.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MooGameRefactor.Games
{
    public class MooGameLogic
    {
		public string CreateAnswer()
		{
			Random randomGenerator = new Random();
			string goal = "";
			for (int i = 0; i < 4; i++)
			{
				int random = randomGenerator.Next(10);
				string randomDigit = "" + random;
				while (goal.Contains(randomDigit))
				{
					random = randomGenerator.Next(10);
					randomDigit = "" + random;
				}
				goal = goal + randomDigit;
			}
			return goal;
		}

        public int CheckInput(IUserInterface ui, string goal)
        {
            string guess = ui.Input();

            int nGuess = 1;
            string bbcc = FindMatches(goal, guess);
            ui.Output(bbcc + "\n");
            while (bbcc != "BBBB,")
            {
                nGuess++;
                guess = ui.Input();
                ui.Output(guess + "\n");
                bbcc = FindMatches(goal, guess);
                ui.Output(bbcc + "\n");
            }

            return nGuess;
        }

		public string FindMatches(string goal, string guess)
		{
			int cows = 0, bulls = 0;
			guess += "    ";     // if player entered less than 4 chars
			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					if (goal[i] == guess[j])
					{
						if (i == j)
						{
							bulls++;
						}
						else
						{
							cows++;
						}
					}
				}
			}
			return "BBBB".Substring(0, bulls) + "," + "CCCC".Substring(0, cows);
		}
	}
}
