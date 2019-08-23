using System.Configuration;

namespace GestaoTributos.Data.WebServices
{
    public class BaseWebService
    {
        internal string UsuarioWS
        {
            get
            {
                return ConfigurationManager.AppSettings["USUARIO_WS"];
            }
        }

        internal string SenhaWS
        {
            get
            {
                return ConfigurationManager.AppSettings["SENHA_WS"];
            }
        }

        internal int EncryptionWS
        {
            get
            {
                int encryption = 0;
                int.TryParse(ConfigurationManager.AppSettings["ENCRYPTION_WS"], out encryption);

                return encryption;
            }
        }
    }
}
