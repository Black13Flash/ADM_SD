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
                        //int acum = 0;

                        int ini = 0;
                        int fin = 9;

                        for (int i = 0; i < vueltasEntero; i++)
                        {
                            string query_xml = usd.getListValues(sid, listaHandle, ini, fin, att);
                            int acum = 0;
                            //Debug.WriteLine("XML==============>"+query_xml);

                            XmlDocument xmlDoc = new XmlDocument();

                            if (query_xml != null)
                            {
                                xmlDoc.LoadXml(query_xml);

                                XmlNodeList listaXml = xmlDoc.GetElementsByTagName("AttrValue");

                                Debug.WriteLine("listaXML:::::::::::::::::::::::::::: " + listaXml.Count);

                                if (listaXml.Count > 0)
                                {
                                    int cantAtt = att.Length;
                                    Debug.WriteLine("cantAtt:::::::::::::::: " + cantAtt);

                                    for (int zz = 0; zz < listaXml.Count; zz++)
                                    {
                                        Debug.WriteLine("zz[" + zz + "]==" + listaXml[zz].InnerXml);
                                    }

                                    //aqui el for

                                    for (int y = 1; y < cantAtt; y++)
                                    {
                                        pcat categoria = new pcat();
                                        Debug.WriteLine("y:::::::::::::::: LISTA[" + y+"]");

                                        categoria.Id = Convert.ToInt32(listaXml[acum].InnerXml);
                                        Debug.WriteLine("categoria.Id :::::::"+acum+"::::::::: " + categoria.Id);
                                        acum++;
                                        categoria.Persistent_id = listaXml[acum].InnerXml;
                                        Debug.WriteLine("categoria.Pers ::::::::" + acum + "::::::::: " + categoria.Persistent_id);
                                        acum++;
                                        categoria.Sym = listaXml[acum].InnerXml;
                                        Debug.WriteLine("categoria.sym :::::::" + acum + ":::::::::: " + categoria.Sym);
                                        acum++;
                                        categoria.Del = Convert.ToInt32(listaXml[acum].InnerXml);
                                        Debug.WriteLine("categoria.Del ::::::" + acum + "::::::::::: " + categoria.Del);
                                        acum++;
                                        categoria.Group_id = listaXml[acum].InnerXml;
                                        Debug.WriteLine("categoria.Group_id :::::::" + acum + ":::::::::: " + categoria.Group_id);
                                        acum++;
                                        categoria.Service_type = listaXml[acum].InnerXml;
                                        Debug.WriteLine("categoria.Service_type ::::::" + acum + "::::::::::: " + categoria.Service_type);
                                        acum++;
                                        categoria.Cr_flag = Convert.ToInt32(listaXml[acum].InnerXml);
                                        Debug.WriteLine("categoria.Cr_flag ::::::::" + acum + "::::::::: " + categoria.Cr_flag);
                                        acum++;
                                        categoria.In_flag = Convert.ToInt32(listaXml[acum].InnerXml);
                                        Debug.WriteLine("categoria.In_flag ::::::" + acum + "::::::::::: " + categoria.In_flag);
                                        acum++;
                                        categoria.Pr_flag = Convert.ToInt32(listaXml[acum].InnerXml);
                                        Debug.WriteLine("categoria.Pr_flag :::::::" + acum + ":::::::::: " + categoria.Pr_flag);
                                        acum++;
                                        categoria.Ss_include = listaXml[acum].InnerXml;
                                        Debug.WriteLine("categoria.Ss_include :::::" + acum + ":::::::::::: " + categoria.Ss_include);
                                        acum++;
                                        categoria.Ss_sym = listaXml[acum].InnerXml;
                                        Debug.WriteLine("categoria.Ss_sym ::::::" + acum + "::::::::::: " + categoria.Ss_sym);
                                        acum++;
                                        listaPcat.Add(categoria);

                                        Debug.WriteLine("acum::::::::::::::::: " + acum);
                                    }
                                }
                            }

                            ini += 10;
                            fin += 10;
                        }
                        
                    }
                    else
                    {

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
            Conexion con = Conexion.Instance();

            try
            {
                USD_WebServiceSoapClient usd = new USD_WebServiceSoapClient();
                int sid = con.abreConexion();

                if (sid > 0)
                {
                    string[] attributos = new string[12];
                    attributos[0] = "id";
                    //attributos[1] = "persistent_id";
                    //attributos[2] = "sym";
                    //attributos[3] = "delete_flag";
                    //attributos[4] = "group";
                    //attributos[5] = "service_type";
                    //attributos[6] = "cr_flag";
                    //attributos[7] = "in_flag";
                    //attributos[8] = "pr_flag";
                    //attributos[9] = "ss_include";
                    //attributos[10] = "ss_sym";
                    //attributos[11] = "tenant";

                    //string query_xml = usd.doSelect(sid, "pcat", "sym like 'ACCESOS.APLICACIONES.CDC%' ", -1, attributos);
                    string query_xml = usd.doSelect(sid, "pcat", "", 1, attributos);


                    // -----------------------
                    //      VALIDAR QUERY
                    // -----------------------

                    if (query_xml == null)
                    {
                        con.cierraConexion();
                        return null;
                    }
                    else
                    {
                        XmlDocument xmlDoc = new XmlDocument();
                        try
                        {
                            xmlDoc.LoadXml(query_xml);
                            XmlNodeList lista = xmlDoc.GetElementsByTagName("AttrValue");

                            List<pcat> listaCategorias;

                            if (lista.Count > 0)
                            {
                                listaCategorias = new List<pcat>();

                                for (int i = 0; i < lista.Count; i++)
                                {
                                    pcat categoria = new pcat();
                                    categoria.Id = Convert.ToInt32(lista[i].InnerXml);
                                    //i++;
                                    //categoria.Persistent_id = lista[i].InnerXml;
                                    //i++;
                                    //categoria.Sym = lista[i].InnerXml;
                                    //i++;
                                    //categoria.Del = Convert.ToInt32(lista[i].InnerXml);
                                    //i++;
                                    //categoria.Group_id = lista[i].InnerXml;
                                    //i++;
                                    //categoria.Service_type = lista[i].InnerXml;
                                    //i++;
                                    //categoria.Cr_flag = Convert.ToInt32(lista[i].InnerXml);
                                    //i++;
                                    //categoria.In_flag = Convert.ToInt32(lista[i].InnerXml);
                                    //i++;
                                    //categoria.Pr_flag = Convert.ToInt32(lista[i].InnerXml);
                                    //i++;
                                    //categoria.Ss_include = lista[i].InnerXml;
                                    //i++;
                                    //categoria.Ss_sym = lista[i].InnerXml;
                                    //i++;
                                    //categoria.Tenant = lista[i].InnerXml;

                                    listaCategorias.Add(categoria);
                                }
                                return listaCategorias;
                            }
                            else
                            {
                                return null;
                            }
                        }
                        catch
                        {
                            con.cierraConexion();
                            return null;
                        }
                    }
                }
                else
                {
                    con.cierraConexion();
                    return null;
                }
            }
            catch
            {
                return null;
            }
            finally
            {
                con.cierraConexion();
            }
        }
    }
}
