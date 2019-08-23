using GestaoTributos.Data.wsLoteContabil;
using GestaoTributos.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoTributos.Data.WebServices
{
    public class LoteContabilWS : BaseWebService, IDisposable
    {
        private wsLoteContabil.g5seniorservices _wsLoteContabil;
        internal wsLoteContabil.g5seniorservices WsLoteContabil
        {
            get
            {
                if (this._wsLoteContabil == null)
                    this._wsLoteContabil = new wsLoteContabil.g5seniorservices();
                return this._wsLoteContabil;
            }
        }

        public string UrlWsLoteContabil { get; set; }
        public int TimeoutWS { get; set; }

        public gerarlotectbGerar2Out EnviarLoteContabil(gerarlotectbGerar2In lote)
        {
            if (!string.IsNullOrEmpty(this.UrlWsLoteContabil))
                this.WsLoteContabil.Url = this.UrlWsLoteContabil;

            if (this.TimeoutWS > 0)
            {
                this.WsLoteContabil.Timeout = this.TimeoutWS;
            }

            var retorno = this.WsLoteContabil.Gerar_2(
                this.UsuarioWS,
                this.SenhaWS,
                this.EncryptionWS,
                lote);

            return retorno;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this._wsLoteContabil != null)
                this._wsLoteContabil.Dispose();
        }
    }
}
