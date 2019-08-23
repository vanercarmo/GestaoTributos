using GestaoTributos.Logger;
using GestaoTributos.Data.Repositories;
using GestaoTributos.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GestaoTributos.Data.wsLancamentoContabil;
using GestaoTributos.Data.WebServices;
using System.Linq;
using System.Threading;
using System.Runtime.InteropServices;

namespace GestaoTributos.App
{
    public class LancamentoContabilApp : Application, IDisposable
    {
        private LancamentoContabilRepository _LancamentoContabilRepository;
        internal LancamentoContabilRepository LancamentoContabilRepository
        {
            get
            {
                if (this._LancamentoContabilRepository == null)
                    this._LancamentoContabilRepository = new LancamentoContabilRepository();
                return this._LancamentoContabilRepository;
            }
        }

        private string _UrlWsLancamentoContabil;
        internal string UrlWsLancamentoContabil
        {
            get
            {
                if (string.IsNullOrEmpty(this._UrlWsLancamentoContabil))
                {
                    var parametro = this.ParametroRepository.BuscarParametro(
                        EnumTipoParametro.UrlWsLancamentoContabil);

                    if (parametro != null && !string.IsNullOrEmpty(parametro.ValorTexto))
                        this._UrlWsLancamentoContabil = parametro.ValorTexto;
                }
                return this._UrlWsLancamentoContabil;
            }
        }

        private int _QtdeThreadsLancamentoContabil = int.MinValue;
        internal int QtdeThreadsLancamentoContabil
        {
            get
            {
                if (this._QtdeThreadsLancamentoContabil == int.MinValue)
                {
                    var parametro = this.ParametroRepository.BuscarParametro(
                        EnumTipoParametro.QtdeThreadsLancamentoContabil);

                    this._QtdeThreadsLancamentoContabil = (parametro != null && parametro.ValorInt.HasValue && parametro.ValorInt > 0)
                         ? parametro.ValorInt.Value
                         : 0;
                }

                return this._QtdeThreadsLancamentoContabil;
            }
        }

        private bool? _Assincrono = null;
        internal bool Assincrono
        {
            get
            {
                if (!this._Assincrono.HasValue)
                {
                    var parametro = this.ParametroRepository.BuscarParametro(
                        EnumTipoParametro.EnviarLancamentoContabilAssincrono);

                    this._Assincrono = (parametro != null
                        && parametro.ValorBooleano.HasValue
                        && parametro.ValorBooleano.Value);
                }

                return this._Assincrono.Value;
            }
        }

        public LancamentoContabilApp(int? empresa)
        {
            this.Empresa = empresa;
        }

        public override void Run()
        {
            try
            {
                this.LancamentoContabilRepository.Empresa = this.Empresa;

                if (this.QtdeThreadsLancamentoContabil == 0)
                {
                    this.EnviarLancamentoContabil();
                }
                else
                {
                    var tasks = new List<Task>();

                    for (var i = 0; i < this.QtdeThreadsLancamentoContabil; i++)
                    {
                        tasks.Add(Task.Run(() => this.EnviarLancamentoContabil()));
                    }

                    Task.WaitAll(tasks.ToArray());
                }
            }
            catch (Exception ex)
            {
                logger.WriteLog(ex.ToString());
            }
        }

        private void EnviarLancamentoContabil()
        {
            IEnumerable<LancamentoContabil> retorno;
            var taskList = new List<Task>();

            lock (this.ObjectLock)
                retorno = this.LancamentoContabilRepository.BuscarLancamentoContabil();

            using (var lancamentoContabilWS = new LancamentoContabilWS())
            {
                lancamentoContabilWS.UrlWsLancamentoContabil = this.UrlWsLancamentoContabil;
                lancamentoContabilWS.TimeoutWS = this.TimeoutWS;
                lancamentoContabilWS.Assincrono = this.Assincrono;

                while (retorno != null && retorno.Any())
                {
                    var retornoWS = lancamentoContabilWS.EnviarLancamentoContabil(this.ConverterLancamentosContabeis(retorno));

                    if (retornoWS != null)
                    {
                        taskList.Add(this.LancamentoContabilRepository.GravarLogExecucaoAsync(retornoWS));
                    }

                    lock (this.ObjectLock)
                        retorno = this.LancamentoContabilRepository.BuscarLancamentoContabil();
                }

                if (taskList.Count > 0)
                    Task.WaitAll(taskList.ToArray());
            }
        }

        private importacaolctctbImportar3In ConverterLancamentosContabeis(IEnumerable<LancamentoContabil> lancamentos)
        {
            var retorno = new importacaolctctbImportar3In();

            if (lancamentos != null && lancamentos.Any())
            {
                var items = lancamentos.ToList();

                retorno.gridLct = new importacaolctctbImportar3InGridLct[items.Count];

                for (var i = 0; i < items.Count; i++)
                {
                    retorno.gridLct[i] = new importacaolctctbImportar3InGridLct()
                    {
                        codEmp = items[i].CODEMP,
                        codFil = items[i].CODFIL,
                        cplLct = items[i].CPLLCT,
                        ctaCre = items[i].CTACRE,
                        ctaDeb = items[i].CTADEB,
                        datLct = items[i].DATLCT,
                        docLct = items[i].DOCLCT,
                        numLot = items[i].NUMLOT,
                        vlrLct = items[i].VLRLCT,
                        obsCpl = items[i].OBSCPL,
                        oriLct = "MAN",
                        codEmpSpecified = true,
                        codFilSpecified = true,
                        ctaCreSpecified = true,
                        ctaDebSpecified = true,
                        numLotSpecified = true,
                        vlrLctSpecified = true
                    };
                }
            }

            return retorno;
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            this.LancamentoContabilRepository.Dispose();
            this.ParametroRepository.Dispose();
        }
    }
}
