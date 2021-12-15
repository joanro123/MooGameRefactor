using MooGameRefactor.Games;
using MooGameRefactor.Interfaces;
using MooGameRefactor.Models;
using MooGameRefactor.View;
using System.Data.Entity;

namespace MooGameRefactor
{
    public class Program
    {
        public static void Main()
        {
            IUserInterface ui = new SolutionIO();
            MooGameLogic gameLogic = new();
            GuessNumberGameLogic secondGameLogic = new();
            IStorage storage = new SaveToFile();
            GameController controller = new(ui, gameLogic, storage, secondGameLogic);
            controller.Run();
        }
    }

    public class ResultContext : DbContext
    {
        public DbSet<Result> Results { get; set; }
    }
}
