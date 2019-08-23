using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestaoTributos.Data.wsLancamentoContabil;
using GestaoTributos.Data.Repositories;
using GestaoTributos.Domain;

namespace GestaoTributos.Data.WebServices
{
    public class LancamentoContabilWS : BaseWebService, IDisposable
    {
        private g5seniorservices _wsLancamentoContabil;
        protected g5seniorservices WsLancamentoContabil
        {
            get
            {
                if (this._wsLancamentoContabil == null)
                    this._wsLancamentoContabil = new g5seniorservices();
                return this._wsLancamentoContabil;
            }
        }

        public string UrlWsLancamentoContabil { get; set; }
        public int TimeoutWS { get; set; }

        public bool Assincrono { get; set; }

        public importacaolctctbImportar3Out EnviarLancamentoContabil(importacaolctctbImportar3In item)
        {
            if (!string.IsNullOrEmpty(this.UrlWsLancamentoContabil))
                this.WsLancamentoContabil.Url = this.UrlWsLancamentoContabil;

            if (this.TimeoutWS > 0)
            {
                this.WsLancamentoContabil.Timeout = this.TimeoutWS;
            }

            if (this.Assincrono)
            {
                this.WsLancamentoContabil.Importar_3Async(
                    this.UsuarioWS,
                    this.SenhaWS,
                    this.EncryptionWS,
                    item);

                return null;
            }
            else
            {
                var retorno = this.WsLancamentoContabil.Importar_3(
                    this.UsuarioWS,
                    this.SenhaWS,
                    this.EncryptionWS,
                    item);

                return retorno;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this._wsLancamentoContabil != null)
                this._wsLancamentoContabil.Dispose();
        }
    }
}
