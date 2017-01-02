using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Nuevas
using Administrador_Service_Desk.WSDL;
using Administrador_Service_Desk.Modelo;
//XML
using System.Xml;
//DIAG
using System.Diagnostics;

namespace Administrador_Service_Desk.Modelo
{
    class pcat
    {
        private int id;
        private string persistent_id;
        private string sym;
        private int del;
        private string group_id;
        private string service_type;
        private int cr_flag;
        private int in_flag;
        private int pr_flag;
        private string ss_include;
        private string ss_sym;
        private string tenant;
        private string tabla = "Prob_Category";

        public pcat() { }

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public string Persistent_id
        {
            get
            {
                return persistent_id;
            }

            set
            {
                persistent_id = value;
            }
        }

        public string Sym
        {
            get
            {
                return sym;
            }

            set
            {
                sym = value;
            }
        }

        public int Del
        {
            get
            {
                return del;
            }

            set
            {
                del = value;
            }
        }

        public string Group_id
        {
            get
            {
                return group_id;
            }

            set
            {
                group_id = value;
            }
        }

        public string Service_type
        {
            get
            {
                return service_type;
            }

            set
            {
                service_type = value;
            }
        }

        public int Cr_flag
        {
            get
            {
                return cr_flag;
            }

            set
            {
                cr_flag = value;
            }
        }

        public int In_flag
        {
            get
            {
                return in_flag;
            }

            set
            {
                in_flag = value;
            }
        }

        public int Pr_flag
        {
            get
            {
                return pr_flag;
            }

            set
            {
                pr_flag = value;
            }
        }

        public string Ss_include
        {
            get
            {
                return ss_include;
            }

            set
            {
                ss_include = value;
            }
        }

        public string Ss_sym
        {
            get
            {
                return ss_sym;
            }

            set
            {
                ss_sym = value;
            }
        }

        public string Tenant
        {
            get
            {
                return tenant;
            }

            set
            {
                tenant = value;
            }
        }

        ////////////////////////////////////////////////
        //       UTILIDADES
        ////////////////////////////////////////////////
        public string utilUno()
        {


            return "";
        }


        ////////////////////////////////////////////////
        //       OBTENER LISTA DE CATEGORIAS
        ////////////////////////////////////////////////
        public List<pcat> listaCategorias()
        {
            List<pcat> listaPcat = null;

            Conexion con = Conexion.Instance();
            try
            {
                USD_WebServiceSoapClient usd = new USD_WebServiceSoapClient();
                int sid = con.abreConexion();

                if (sid > 0)
                {
                    ListResult lista = new ListResult();
                    lista = usd.doQuery(sid, "pcat", "sym is not null");

                    int listaHandle = lista.listHandle;
                    int listaLength = lista.listLength;
                    
                    string[] att = new string[11];
                    att[0] = "id";
                    att[1] = "persistent_id";
                    att[2] = "sym";
                    att[3] = "delete_flag";
                    att[4] = "group";
                    att[5] = "service_type";
                    att[6] = "cr_flag";
                    att[7] = "in_flag";
                    att[8] = "pr_flag";
                    att[9] = "ss_include";
                    att[10] = "ss_sym";
                    //attributos[11] = "tenant";

                    ////////////////////////////////////////////////
                    //
                    //        TRABAJAR CON LA LISTAS
                    //
                    ////////////////////////////////////////////////
                    
                    if (listaLength > 0)
                    {
                        // EXISTEN CATEGORIAS
                        listaPcat = new List<pcat>();

                        float cantidad = listaLength;
                        float vueltas = cantidad / 10;
                        int vueltasEntero = (int)vueltas;
                        int acum = 0;
                        int segFin = 0;

                        if (vueltasEntero >= 1)
                        {
                            int ini = 0;
                            int fin = 9;

                            for (int i = 0; i < vueltasEntero; i++)
                            {
                                string query_xml = usd.getListValues(sid, listaHandle, ini, fin, att);
                                acum = 0;

                                XmlDocument xmlDoc = new XmlDocument();

                                if (query_xml != null)
                                {
                                    xmlDoc.LoadXml(query_xml);

                                    XmlNodeList listaXml = xmlDoc.GetElementsByTagName("AttrValue");

                                    Debug.WriteLine("::::::::::::::::::::::::::::::::::::::::::");
                                    Debug.WriteLine(" Cantidad Categorías    : " + listaLength);
                                    Debug.WriteLine(" Cantidad Elementos XML : " + listaXml.Count);
                                    Debug.WriteLine("::::::::::::::::::::::::::::::::::::::::::");

                                    if (listaXml.Count > 0)
                                    {
                                        int cantAtt = att.Length;

                                        for (int y = 0; y < 10; y++)
                                        {
                                            pcat categoria = new pcat();
                                            Debug.WriteLine(":::::::::::::::: OBJETO[" + y + "]");

                                            categoria.Id = Convert.ToInt32(listaXml[acum].InnerXml);

                                            acum++;
                                            categoria.Persistent_id = listaXml[acum].InnerXml;

                                            acum++;
                                            categoria.Sym = listaXml[acum].InnerXml;

                                            acum++;
                                            categoria.Del = Convert.ToInt32(listaXml[acum].InnerXml);

                                            acum++;
                                            categoria.Group_id = listaXml[acum].InnerXml;

                                            acum++;
                                            categoria.Service_type = listaXml[acum].InnerXml;

                                            acum++;
                                            categoria.Cr_flag = Convert.ToInt32(listaXml[acum].InnerXml);

                                            acum++;
                                            categoria.In_flag = Convert.ToInt32(listaXml[acum].InnerXml);

                                            acum++;
                                            categoria.Pr_flag = Convert.ToInt32(listaXml[acum].InnerXml);

                                            acum++;
                                            categoria.Ss_include = listaXml[acum].InnerXml;

                                            acum++;
                                            categoria.Ss_sym = listaXml[acum].InnerXml;

                                            acum++;
                                            listaPcat.Add(categoria);

                                            Debug.WriteLine("["+acum+"]=('" + categoria.Id + "','" +
                                                                 categoria.persistent_id + "','" +
                                                                 categoria.sym + "','" +
                                                                 categoria.del + "','" +
                                                                 categoria.group_id + "','" +
                                                                 categoria.service_type + "','" +
                                                                 categoria.cr_flag + "','" +
                                                                 categoria.in_flag + "','" +
                                                                 categoria.pr_flag + "','" +
                                                                 categoria.ss_include + "','" +
                                                                 categoria.ss_sym + ")");
                                        }
                                    }
                                }

                                ini += 10;
                                fin += 10;
                            }
                            segFin = fin;
                        }

                        int otraVuelta = (int)((vueltas - (float)vueltasEntero) * 10);

                        if (otraVuelta >= 1)
                        {
                            for (int i = 0; i < 1; i++)
                            {
                                int ini = 0;
                                int fin = otraVuelta;

                                segFin -= 10;

                                if (acum > 0)
                                {
                                    ini = segFin + 1;
                                    fin = segFin + otraVuelta;
                                }

                                acum = 0;


                                string query_xml = usd.getListValues(sid, listaHandle, ini, fin, att);

                                XmlDocument xmlDoc = new XmlDocument();

                                if (query_xml != null)
                                {
                                    xmlDoc.LoadXml(query_xml);

                                    XmlNodeList listaXml = xmlDoc.GetElementsByTagName("AttrValue");

                                    Debug.WriteLine("::::::::::::::::::::::::::::::::::::::::::");
                                    Debug.WriteLine(" Cantidad Elementos XML : " + listaXml.Count);
                                    Debug.WriteLine("::::::::::::::::::::::::::::::::::::::::::");

                                    if (listaXml.Count > 0)
                                    {
                                        int cantAtt = att.Length;

                                        for (int y = 0; y < otraVuelta; y++)
                                        {
                                            pcat categoria = new pcat();
                                            Debug.WriteLine(":::::::::::::::: OBJETO[" + y + "]");

                                            categoria.Id = Convert.ToInt32(listaXml[acum].InnerXml);

                                            acum++;
                                            categoria.Persistent_id = listaXml[acum].InnerXml;

                                            acum++;
                                            categoria.Sym = listaXml[acum].InnerXml;

                                            acum++;
                                            categoria.Del = Convert.ToInt32(listaXml[acum].InnerXml);

                                            acum++;
                                            categoria.Group_id = listaXml[acum].InnerXml;

                                            acum++;
                                            categoria.Service_type = listaXml[acum].InnerXml;

                                            acum++;
                                            categoria.Cr_flag = Convert.ToInt32(listaXml[acum].InnerXml);

                                            acum++;
                                            categoria.In_flag = Convert.ToInt32(listaXml[acum].InnerXml);

                                            acum++;
                                            categoria.Pr_flag = Convert.ToInt32(listaXml[acum].InnerXml);

                                            acum++;
                                            categoria.Ss_include = listaXml[acum].InnerXml;

                                            acum++;
                                            categoria.Ss_sym = listaXml[acum].InnerXml;

                                            acum++;
                                            listaPcat.Add(categoria);

                                            Debug.WriteLine("[" + acum + "]=('" + categoria.Id + "','" +
                                                                 categoria.persistent_id + "','" +
                                                                 categoria.sym + "','" +
                                                                 categoria.del + "','" +
                                                                 categoria.group_id + "','" +
                                                                 categoria.service_type + "','" +
                                                                 categoria.cr_flag + "','" +
                                                                 categoria.in_flag + "','" +
                                                                 categoria.pr_flag + "','" +
                                                                 categoria.ss_include + "','" +
                                                                 categoria.ss_sym + ")");
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        //SIN CATEGORIAS
                    }

                }

                return listaPcat;
            }
            catch (Exception)
            {
                return null;
                throw;
            }
            finally
            {
                con.cierraConexion();
            }

            
        }

        // ---------------------------------------
        //      TODAS LAS CATEGORIAS EN LISTA
        // ---------------------------------------
        public List<pcat> buscaTodas()
        {
            List<pcat> listaPcat = null;

            Conexion con = Conexion.Instance();
            try
            {
                USD_WebServiceSoapClient usd = new USD_WebServiceSoapClient();
                int sid = con.abreConexion();

                if (sid > 0)
                {
                    ListResult lista = new ListResult();
                    lista = usd.doQuery(sid, "pcat", "sym is not null");

                    int listaHandle = lista.listHandle;
                    int listaLength = lista.listLength;

                    string[] att = new string[11];
                    att[0] = "id";
                    att[1] = "persistent_id";
                    att[2] = "sym";
                    att[3] = "delete_flag";
                    att[4] = "group";
                    att[5] = "service_type";
                    att[6] = "cr_flag";
                    att[7] = "in_flag";
                    att[8] = "pr_flag";
                    att[9] = "ss_include";
                    att[10] = "ss_sym";
                    //attributos[11] = "tenant";

                    ////////////////////////////////////////////////
                    //
                    //        TRABAJAR CON LA LISTAS
                    //
                    ////////////////////////////////////////////////

                    if (listaLength > 0)
                    {
                        float cantidad = listaLength;
                        float vueltas = cantidad / 10;
                        float otraVuelta = vueltas - ((float)((int)vueltas));
                        bool resto = false;

                        if (otraVuelta > 0)
                        {
                            resto = true;
                        }

                        listaPcat = new List<pcat>();

                        int vueltasEntero = (int)vueltas;

                        int ini = 0;
                        int fin = 9;

                        for (int i = 0; i < vueltasEntero; i++)
                        {
                            string query_xml = usd.getListValues(sid, listaHandle, ini, fin, att);
                            int acum = 0;

                            XmlDocument xmlDoc = new XmlDocument();

                            if (query_xml != null)
                            {
                                xmlDoc.LoadXml(query_xml);

                                XmlNodeList listaXml = xmlDoc.GetElementsByTagName("AttrValue");

                                Debug.WriteLine("::::::::::::::::::::::::::::::::::::::::::");
                                Debug.WriteLine(" Cantidad Elementos XML : " + listaXml.Count);
                                Debug.WriteLine("::::::::::::::::::::::::::::::::::::::::::");

                                if (listaXml.Count > 0)
                                {
                                    int cantAtt = att.Length;

                                    for (int y = 0; y < 10; y++)
                                    {
                                        pcat categoria = new pcat();
                                        Debug.WriteLine("y:::::::::::::::: LISTA[" + y + "]");

                                        categoria.Id = Convert.ToInt32(listaXml[acum].InnerXml);

                                        acum++;
                                        categoria.Persistent_id = listaXml[acum].InnerXml;

                                        acum++;
                                        categoria.Sym = listaXml[acum].InnerXml;

                                        acum++;
                                        categoria.Del = Convert.ToInt32(listaXml[acum].InnerXml);

                                        acum++;
                                        categoria.Group_id = listaXml[acum].InnerXml;

                                        acum++;
                                        categoria.Service_type = listaXml[acum].InnerXml;

                                        acum++;
                                        categoria.Cr_flag = Convert.ToInt32(listaXml[acum].InnerXml);

                                        acum++;
                                        categoria.In_flag = Convert.ToInt32(listaXml[acum].InnerXml);

                                        acum++;
                                        categoria.Pr_flag = Convert.ToInt32(listaXml[acum].InnerXml);

                                        acum++;
                                        categoria.Ss_include = listaXml[acum].InnerXml;

                                        acum++;
                                        categoria.Ss_sym = listaXml[acum].InnerXml;

                                        acum++;
                                        listaPcat.Add(categoria);

                                        Debug.WriteLine("('" + categoria.Id + "','" +
                                                             categoria.persistent_id + "','" +
                                                             categoria.sym + "','" +
                                                             categoria.del + "','" +
                                                             categoria.group_id + "','" +
                                                             categoria.service_type + "','" +
                                                             categoria.cr_flag + "','" +
                                                             categoria.in_flag + "','" +
                                                             categoria.pr_flag + "','" +
                                                             categoria.ss_include + "','" +
                                                             categoria.ss_sym + ")"
                                                             );
                                        //Debug.WriteLine("categoria.Id :::::::" + acum + "::::::::: " + categoria.Id);
                                        //Debug.WriteLine("categoria.Pers ::::::::" + acum + "::::::::: " + categoria.Persistent_id);
                                        //Debug.WriteLine("categoria.sym :::::::" + acum + ":::::::::: " + categoria.Sym);
                                        //Debug.WriteLine("categoria.Del ::::::" + acum + "::::::::::: " + categoria.Del);
                                        //Debug.WriteLine("categoria.Group_id :::::::" + acum + ":::::::::: " + categoria.Group_id);
                                        //Debug.WriteLine("categoria.Service_type ::::::" + acum + "::::::::::: " + categoria.Service_type);
                                        //Debug.WriteLine("categoria.Cr_flag ::::::::" + acum + "::::::::: " + categoria.Cr_flag);
                                        //Debug.WriteLine("categoria.In_flag ::::::" + acum + "::::::::::: " + categoria.In_flag);
                                        //Debug.WriteLine("categoria.Pr_flag :::::::" + acum + ":::::::::: " + categoria.Pr_flag);
                                        //Debug.WriteLine("categoria.Ss_include :::::" + acum + ":::::::::::: " + categoria.Ss_include);
                                        //Debug.WriteLine("categoria.Ss_sym ::::::" + acum + "::::::::::: " + categoria.Ss_sym);

                                    }
                                }
                            }

                            ini += 10;
                            fin += 10;
                        }

                    }
                    else
                    {
                        // NO HAY CATEGORIAS
                    }

                }

                return listaPcat;
            }
            catch (Exception)
            {
                return null;
                throw;
            }
            finally
            {
                con.cierraConexion();
            }
        }
    }
}
