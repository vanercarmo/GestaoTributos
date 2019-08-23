using GestaoTributos.Data.Repositories;
using GestaoTributos.Data.WebServices;
using GestaoTributos.Data.wsLoteContabil;
using GestaoTributos.Domain;
using GestaoTributos.Logger;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestaoTributos.App
{
    public class LoteContabilApp : Application, IDisposable
    {
        private LoteContabilRepository _LoteContabilRepository;
        internal LoteContabilRepository LoteContabilRepository
        {
            get
            {
                if (this._LoteContabilRepository == null)
                    this._LoteContabilRepository = new LoteContabilRepository();
                return this._LoteContabilRepository;
            }
        }

        private string _UrlWsLoteContabil;
        internal string UrlWsLoteContabil
        {
            get
            {
                if (string.IsNullOrEmpty(this._UrlWsLoteContabil))
                {
                    var parametro = this.ParametroRepository.BuscarParametro(
                        EnumTipoParametro.UrlWsLoteContabil);

                    if (parametro != null && !string.IsNullOrEmpty(parametro.ValorTexto))
                        this._UrlWsLoteContabil = parametro.ValorTexto;
                }

                return this._UrlWsLoteContabil;
            }
        }

        private int _QtdeThreadsLoteContabil = int.MinValue;
        internal int QtdeThreadsLoteContabil
        {
            get
            {
                if (this._QtdeThreadsLoteContabil == int.MinValue)
                {
                    var parametro = this.ParametroRepository.BuscarParametro(
                        EnumTipoParametro.QtdeThreadsLoteContabil);

                    this._QtdeThreadsLoteContabil = (parametro != null && parametro.ValorInt.HasValue && parametro.ValorInt > 0)
                         ? parametro.ValorInt.Value
                         : 0;
                }

                return this._QtdeThreadsLoteContabil;
            }
        }

        public LoteContabilApp(int? empresa)
        {
            this.Empresa = empresa;
        }

        public override void Run()
        {
            this.LoteContabilRepository.Empresa = this.Empresa;

            if (this.QtdeThreadsLoteContabil == 0)
            {
                this.EnviarLoteContabil();
            }
            else
            {
                var tasks = new List<Task>();
                for (var i = 0; i < this.QtdeThreadsLoteContabil; i++)
                {
                    tasks.Add(Task.Run(() => this.EnviarLoteContabil()));
                }

                Task.WaitAll(tasks.ToArray());
            }
        }

        public void EnviarLoteContabil()
        {
            LoteContabil lote = null;

            using (var loteContabilWS = new LoteContabilWS())
            {
                loteContabilWS.UrlWsLoteContabil = this.UrlWsLoteContabil;
                loteContabilWS.TimeoutWS = this.TimeoutWS;

                lock (this.ObjectLock)
                    lote = this.LoteContabilRepository.BuscarLoteContabil();

                while (lote != null)
                {
                    var retornoWS = loteContabilWS.EnviarLoteContabil(this.ConverterLoteContabil(lote));

                if (retornoWS != null)
                    {
                        lock (this.ObjectLock)
                            this.LoteContabilRepository.InserirNumeroLote(lote, retornoWS.erroExecucao, retornoWS.numLot);
                    }

                    lock (this.ObjectLock)
                        lote = this.LoteContabilRepository.BuscarLoteContabil();
                }
            }
        }

        private gerarlotectbGerar2In ConverterLoteContabil(LoteContabil lote)
        {
            return new gerarlotectbGerar2In()
            {
                codEmp = lote.CODEMP,
                codFil = lote.CODFIL,
                oriLot = lote.ORILCT,
                desLot = lote.DESLOT,
                totLot = lote.TOTINFS,
                datLot = lote.DATLOT,
                tipLot = "MAN",
                codEmpSpecified = true,
                codFilSpecified = true,
                totLotSpecified = true
            };
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
        }
    }
}
