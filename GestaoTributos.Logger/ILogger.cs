namespace GestaoTributos.Logger
{
    public interface ILogger
    {
        void Start();
        void WriteLog(string message);
        void End();
    }
}
