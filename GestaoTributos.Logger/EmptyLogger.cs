using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoTributos.Logger
{
    public class EmptyLogger : ILogger
    {
        public void End()
        {
        }

        public void Start()
        {
        }

        public void WriteLog(string message)
        {
        }
    }
}
