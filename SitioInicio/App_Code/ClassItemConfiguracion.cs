using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de ClassItemConfiguracion
/// </summary>
public class ClassItemConfiguracion
{
    private string mIdPagina;
    private string mUrlRedirect;
    private string mTitulo;
    private string mImgUrl;


    /// <summary>
    /// Establece la propiedad de la pagina.
    /// </summary>
    public string  Pagina
    {
        get
        {
            return mIdPagina ;
        }
        set
        {
            mIdPagina = value;
            
        }
    }

    public string UrlRedirect
    {
        get {
            return mUrlRedirect;

        }
        set
        {
            mUrlRedirect = value; 
        }
    }

    public string Titulo
    {
        get
        {
            return mTitulo;
        }
        set
        {
            mTitulo = value;

        }
    }
    public string ImageUrl {
        get
        {
            return mImgUrl;
        }
        set
        {
            mImgUrl = value;

        }
    }

    public ClassItemConfiguracion(string sPagina,  string StrUrl , string StrTitulo ,string UrlImg )
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
        this.Pagina = sPagina;
        this.UrlRedirect = StrUrl;
        this.Titulo = StrTitulo;
        
        this.ImageUrl = UrlImg;


    }
}