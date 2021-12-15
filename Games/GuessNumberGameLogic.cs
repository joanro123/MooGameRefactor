using MooGameRefactor.Interfaces;
using System;

namespace MooGameRefactor.Games
{
    public class GuessNumberGameLogic
    {
        public int RandomNumber()
        {
            Random r = new Random();
            int number = r.Next(1, 100);
            return number;
        }

        public int ControlAnswer(int randomNumber, IUserInterface ui)
        {
            int guesses = 0;
            int answer = Convert.ToInt32(ui.Input());

            while (answer != randomNumber)
            {

                if (answer < randomNumber)
                {
                    ui.Output("För lågt, gissa på nytt!");
                    answer = Convert.ToInt32(ui.Input());
                    guesses++;
                }
                else if (answer > randomNumber)
                {
                    ui.Output("För högt, gissa på nytt!");
                    answer = Convert.ToInt32(ui.Input());
                    guesses++;
                }

            }
            return guesses;
        }
    }
}
