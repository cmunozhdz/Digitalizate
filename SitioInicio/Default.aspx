<%@ Page Language="C#" %>

<!DOCTYPE html>
<%--Eventos del servidor--%>

<script runat="server">

	void page_init(Object Sender, EventArgs e) {
		string strHost = Request.Url.Host;


		ClassDefinicion ObjConfig = new ClassDefinicion(strHost);
		if (ObjConfig.ConfigItem != null) {
			this.Title = ObjConfig.ConfigItem.Titulo;
			H1L1.Text = ObjConfig.ConfigItem.Titulo;
			//Actualiza la liga para redireccionar 
			BtnRedirecciona.PostBackUrl = ObjConfig.ConfigItem.UrlRedirect;

			HtmlMeta keywords = new HtmlMeta();
			keywords.HttpEquiv = "refresh";
			keywords.Content = string.Format("4;URL='{0}'", ObjConfig.ConfigItem.UrlRedirect);
			this.Page.Header.Controls.Add(keywords);
			ImgWait.ImageUrl = ObjConfig.ConfigItem.ImageUrl;


			ImgWait.Visible = true;
			h2l2.Text = "Lo estamos conectando al sitio";
		}
		else
		{
			this.Title = "Configuración no encontrada";

			H1L1.Text = string.Format ("<i class=\"fa  fa-exclamation-triangle\"></i>Configuración no encontrada para servidor: {0:s} ",strHost);
			h2l2.Text = "Lo sentimos no podemos conectarlo";
			BtnRedirecciona.PostBackUrl = "#";
			ImgWait.Visible = false;


		}





	}

</script>


<html lang="es-mx" >
<head runat="server"  >
    <meta charset="utf-8" />
    <title></title>
	<!-- Bootstrap Core CSS -->
	<link href="vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet">

	<!-- MetisMenu CSS -->
	<link href="vendor/metisMenu/metisMenu.min.css" rel="stylesheet">

	
	<link href="dist/css/sb-admin-2.css" rel="stylesheet">
	
	<!-- Morris Charts CSS
	<link href="vendor/morrisjs/morris.css" rel="stylesheet">
	-->
	<!-- Custom Fonts -->
	<link href="vendor/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css">

	<!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
	<!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
	<!--[if lt IE 9]>
		<script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
		<script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
	<![endif]-->
	<!-- Personalizaciones Digitalizate-->
	<style type="text/css" >
		.no-js #loader {
			display: none;
		}

		.js #loader {
			display: block;
			position: absolute;
			left: 100px;
			top: 0;
		}

		.se-pre-con {
			position: fixed;
			left: 0px;
			top: 0px;
			width: 100%;
			height: 100%;
			z-index: 9999;
			background: url(images/loader-64x/Preloader_7.gif) center no-repeat #fff;
		}
	</style>
	<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.5.2/jquery.min.js"></script>
	<script src="http://cdnjs.cloudflare.com/ajax/libs/modernizr/2.8.2/modernizr.js"></script>
	
	<script  >
		//paste this code under the head tag or in a separate js file.
		// Wait for window load
		$(window).load(function() {
		// Animate loader off screen
		$(".se-pre-con").fadeOut("slow");;
		});

	</script>
	
</head>
<body class=""   >
<div class="se-pre-con">
	<%--Muestra icono de progreso mientras carga.--%>
</div>
 <form id="FrmDefault" runat="server" >

	   <div class="row" >
		   
		   <div class="panel panel-default ">
			   <div class=" col-lg-1 col-md-1 col-sm-1"  >
				   <img src="images/Logos/Digitalizate.png" height="100%" width="100%" alt="DigITalizate Logo" />
				   
			   </div>
			   <div  class="col-lg-9 col-md-9 col-sm-9" >
				   <h1><asp:Literal ID="H1L1" runat="server" ></asp:Literal></h1>
			   </div>
				
			</div> 

	   </div> 
	   <div class="col-lg-10 col-md-10">
                    <div class="panel panel-green">
                        <div class="panel-heading">
                            <div class="row">
                                <div class="col-xs-3">
									
                                    <%--<i class="fa fa-tasks fa-5x"></i>--%>
									<i class="fa fa-leaf fa-5x"></i>
									
                                </div>
								<div class="col-xs-9   text-left ">
                                    <%--<div class="huge"></div>
                                    <div>Espere...</div>--%>
									<div class="huge"><h2><asp:Literal ID="h2l2" runat="server" ></asp:Literal></h2></div>
									
                                </div>
                                <div class="col-xs-9 text-right">
                                    <div class="huge"></div>
                                    <div>Espere...</div>
                                </div>
                            </div>
                        </div>
						<asp:LinkButton ID="BtnRedirecciona" 
							 runat="server">
							<div class="panel-footer">
                                <span class="pull-left">Si no es conectado automáticamente </span>
                                <span class="pull-right"><i class="fa fa-arrow-circle-right"></i></span>
                                <div class="clearfix"></div>
                            </div>
						</asp:LinkButton>
                        
                    </div>
        </div>
	  <div id="WaitDiv" class="col-lg-10 col-md-10"  style="width: 100%; vertical-align: middle; text-align: center" runat="server" >
		  
		  <asp:Image  runat="server" Id="ImgWait" 
			   Width="50%" Height ="50%"  />

	  </div> 

 </form>
	
	

</body>
</html>