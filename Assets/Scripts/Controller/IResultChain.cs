namespace JGM.Controller
{
    public interface IResultChain
    {
        void SetNextChain(IResultChain nextChain);
        void Calculate(BoardController boardController);
    }
}