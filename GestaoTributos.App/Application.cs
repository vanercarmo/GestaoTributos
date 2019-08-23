using GestaoTributos.Logger;
using GestaoTributos.Data.Repositories;
using System;
using GestaoTributos.Domain;
using System.Threading;

namespace GestaoTributos.App
{
    public abstract class Application
    {
        internal Object ObjectLock = new Object();

        private ParametroRepository _ParametroRepository;
        internal ParametroRepository ParametroRepository
        {
            get
            {
                if (this._ParametroRepository == null)
                    this._ParametroRepository = new ParametroRepository();
                return this._ParametroRepository;
            }
        }

        private int _TimeoutWS = int.MinValue;
        internal int TimeoutWS
        {
            get
            {
                if (this._TimeoutWS == int.MinValue)
                {
                    var parametro = this.ParametroRepository.BuscarParametro(
                        EnumTipoParametro.TimeoutWS);

                    this._TimeoutWS = (parametro != null && parametro.ValorInt.HasValue && parametro?.ValorInt > 0)
                        ? parametro.ValorInt.Value
                        : 0;
                }

                return this._TimeoutWS;
            }
        }

        internal int? Empresa { get; set; }
        internal DatabaseLogger logger = new DatabaseLogger();

        public abstract void Run();
    }
}
