using System.Collections.Generic;

namespace MooGameRefactor.Interfaces
{
    public interface IStorage
    {
        string ReturnName(int num, string[] arr);
        string[] ShowExistingNames(string? fileName);
        void ShowTopList(string? fileName);
        void WriteToFile(string name, int nGuess, string? filename);
    }
}