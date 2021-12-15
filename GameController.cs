using MooGameRefactor.Games;
using MooGameRefactor.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;

namespace MooGameRefactor
{
    public class GameController
    {
        private readonly IUserInterface ui;
        private readonly MooGameLogic mooGameLogic;
        private readonly IStorage storage;
        private readonly GuessNumberGameLogic guessNumberGameLogic;

        public GameController(IUserInterface ui, MooGameLogic mooGameLogic, IStorage storage, GuessNumberGameLogic guessNumberGameLogic)
        {
            this.ui = ui;
            this.mooGameLogic = mooGameLogic;
            this.storage = storage;
            this.guessNumberGameLogic = guessNumberGameLogic;
        }

        public void Run()
        {
            bool start = true;
            ui.Output("Choose a game:\n");
            string[] menuChoices = { "[1] Guess a Number", "[2] Moo Game", "[Q]uit " };

            while (start)
            {
                int choice = ui.MultipleChoice(menuChoices);

                if (choice == 0)
                {
                    string fileName = "guessNumber.txt";
                    int nameIndex = ui.MultipleChoice(storage.ShowExistingNames(fileName));
                    string name = storage.ReturnName(nameIndex, storage.ShowExistingNames(fileName));
                    if (name == "Create a New Player")
                    {
                        ui.Output("Enter your user name:\n");
                        name = ui.Input();
                    }

                    bool running = true;
                    while (running)
                    {
                        int randomNumber = guessNumberGameLogic.RandomNumber();
                        ui.Output("Gissa ett tal mellan 1-99");
                        int nGuess = guessNumberGameLogic.ControlAnswer(randomNumber, ui);
                        storage.WriteToFile(name, nGuess, fileName);
                        storage.ShowTopList(fileName);
                        ui.Output("Correct, it took " + nGuess + " guesses\nContinue?");
                        running = ui.ContinueOrEnd(running);
                        if (!running)
                            start = false;
                    }
                }
                else if (choice == 1)
                {
                    string fileName = "moo.txt";
                    int nameIndex = ui.MultipleChoice(storage.ShowExistingNames(fileName));
                    string name = storage.ReturnName(nameIndex, storage.ShowExistingNames(fileName));
                    bool running = true;
                    if (name == "Create a New Player")
                    {
                        ui.Output("Enter your user name:\n");
                        name = ui.Input();
                    }

                    while (running)
                    {
                        string goal = mooGameLogic.CreateAnswer();
                        ui.Output("New game:\n");
                        //comment out or remove next line to play real games!
                        ui.Output("For practice, number is: " + goal + "\n");
                        int nGuess = mooGameLogic.CheckInput(ui, goal);
                        storage.WriteToFile(name, nGuess, fileName);
                        storage.ShowTopList(fileName);
                        ui.Output("Correct, it took " + nGuess + " guesses\nContinue?");
                        running = ui.ContinueOrEnd(running);
                        if (!running)
                            start = false;
                    }
                }
                else if (choice == 2)
                {
                    start = false;
                }
                else
                {
                    ui.Output("Choose 1, 2 or Q");
                }
            }
        }
    }

}
    

