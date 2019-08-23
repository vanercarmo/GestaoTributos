using GestaoTributos.App;
using System;

namespace GestaoTributos.LancamentoContabil
{
    class Program
    {
        protected Program() { }

        static void Main(string[] args)
        {
            int? empresa = null;

            if (args != null && args.Length > 0)
            {
                empresa = int.Parse(args[0]);
            }

            using (var application = new LancamentoContabilApp(empresa))
            {
                application.Run();
            }

            Environment.Exit(0);
        }
    }
}
