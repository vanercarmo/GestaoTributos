using System;

namespace GestaoTributos.Logger
{
    public class ConsoleLogger : ILogger
    {
        private DateTime dtStart;
        private DateTime dtEnd;

        public void End()
        {
//#if DEBUG
            this.dtEnd = DateTime.Now;
            Console.WriteLine(Environment.NewLine + "Fim: " + this.dtEnd.ToString("dd/MM/yyyy HH:mm:ss"));
            Console.WriteLine("Tempo total: " + (this.dtEnd - this.dtStart).ToString());
//#endif
        }

        public void WriteLog(string message)
        {
//#if DEBUG
                Console.WriteLine(!string.IsNullOrEmpty(message) ? "[" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff") + "] - " + message : "");
//#endif
        }

        public void Start()
        {
//#if DEBUG
            this.dtStart = DateTime.Now;
            Console.WriteLine("Início: " + dtStart.ToString("dd/MM/yyyy HH:mm:ss") + Environment.NewLine + Environment.NewLine);
//#endif
        }
    }
}
