namespace MooGameRefactor.Interfaces
{
    public interface IUserInterface
    {
        bool ContinueOrEnd(bool running);
        string Input();
        int MultipleChoice(params string[] options);
        void Output(string s);
    }
}