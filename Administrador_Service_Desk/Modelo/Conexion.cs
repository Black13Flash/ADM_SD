using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using Administrador_Service_Desk.WSDL;

namespace Administrador_Service_Desk.Modelo
{
    class Conexion
    {
        private static Conexion instance;
        public USD_WebServiceSoapClient usd;
        public int sid;
        

        public Conexion()
        {
            usd = new USD_WebServiceSoapClient();
        }

        public static Conexion Instance()
        {
            if (instance == null)
                instance = new Conexion();
            return instance;
        }

        public int abreConexion()
        {
            sid = usd.login("usuario", "contrasena");

            return sid;
        }

        public void cierraConexion()
        {
            try
            {
                usd.logout(sid);
            }
            catch
            {
                //HACER ALGO ?
            }
            
        }

    }
}
