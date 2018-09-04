using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;




/// <summary>
/// Descripción breve de ClassDefinicion
/// </summary>
public class ClassDefinicion
{
    private ClassItemConfiguracion ObjItem;
    /// <summary>
    /// Devuelve el item de la configuracion actual
    /// </summary>
    public ClassItemConfiguracion  ConfigItem {
        get
        {
            return ObjItem;

        }
    }

    /// <summary>
    /// Consulta la configuracion del archivo xml e inicializa el Objitem
    /// </summary>
    /// <param name="pStrHost">Host de la pagina actual.</param>
    public ClassDefinicion(string pStrHost)
    {

        //
        // TODO: Agregar aquí la lógica del constructor
        //
        try {
            //Inicializa la lista de objetos con sus llaves.
            Boolean ExistConexion = false;
            

 
            XmlTextReader reader = new XmlTextReader(HttpContext.Current.Server.MapPath("rsc/XMLConfig.xml"));
            while (( reader.Read() ) && (ExistConexion==false) )
            {
                if ( (reader.NodeType == XmlNodeType.Element )  && (reader.Name.ToString().Equals("Conexion")) ) 

                {
                    string sIdKey = reader.GetAttribute("IdKey");
                    if (sIdKey.Equals(pStrHost)) {
                        ObjItem = new ClassItemConfiguracion(sIdKey, reader.GetAttribute("Destino"), reader.GetAttribute("Titulo"), reader.GetAttribute("Imagen"));
                         ExistConexion = true;

                    }

                 }


            }
            reader.Close();


        }
        catch (Exception e)
        {
            HttpContext.Current.Response.Write(string.Format("Error {0:s}", e.Message  ));

        }
  

            

    }
   
}