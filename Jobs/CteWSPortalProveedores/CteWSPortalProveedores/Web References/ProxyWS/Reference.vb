﻿'------------------------------------------------------------------------------
' <auto-generated>
'     Este código fue generado por una herramienta.
'     Versión de runtime:4.0.30319.42000
'
'     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
'     se vuelve a generar el código.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On

Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Xml.Serialization

'
'Microsoft.VSDesigner generó automáticamente este código fuente, versión=4.0.30319.42000.
'
Namespace ProxyWS
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2053.0"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Web.Services.WebServiceBindingAttribute(Name:="ProcWSValidaCDFIsSoapBinding", [Namespace]:="KbComprasPortal")>  _
    Partial Public Class ProcWSValidaCDFIs
        Inherits System.Web.Services.Protocols.SoapHttpClientProtocol
        
        Private ExecuteOperationCompleted As System.Threading.SendOrPostCallback
        
        Private useDefaultCredentialsSetExplicitly As Boolean
        
        '''<remarks/>
        Public Sub New()
            MyBase.New
            Me.Url = Global.CteWSPortalProveedores.My.MySettings.Default.CteWSPortalProveedores_ProxyWS_ProcWSValidaCDFIs
            If (Me.IsLocalFileSystemWebService(Me.Url) = true) Then
                Me.UseDefaultCredentials = true
                Me.useDefaultCredentialsSetExplicitly = false
            Else
                Me.useDefaultCredentialsSetExplicitly = true
            End If
        End Sub
        
        Public Shadows Property Url() As String
            Get
                Return MyBase.Url
            End Get
            Set
                If (((Me.IsLocalFileSystemWebService(MyBase.Url) = true)  _
                            AndAlso (Me.useDefaultCredentialsSetExplicitly = false))  _
                            AndAlso (Me.IsLocalFileSystemWebService(value) = false)) Then
                    MyBase.UseDefaultCredentials = false
                End If
                MyBase.Url = value
            End Set
        End Property
        
        Public Shadows Property UseDefaultCredentials() As Boolean
            Get
                Return MyBase.UseDefaultCredentials
            End Get
            Set
                MyBase.UseDefaultCredentials = value
                Me.useDefaultCredentialsSetExplicitly = true
            End Set
        End Property
        
        '''<remarks/>
        Public Event ExecuteCompleted As ExecuteCompletedEventHandler
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("KbComprasPortalaction/APROCWSVALIDACDFIS.Execute", RequestElementName:="ProcWSValidaCDFIs.Execute", RequestNamespace:="KbComprasPortal", ResponseElementName:="ProcWSValidaCDFIs.ExecuteResponse", ResponseNamespace:="KbComprasPortal", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function Execute(ByRef Fechainicio As Date, ByRef Fechafin As Date, ByRef Msgerror As String) As <System.Xml.Serialization.XmlElementAttribute("Retstatus")> Boolean
            Dim results() As Object = Me.Invoke("Execute", New Object() {Fechainicio, Fechafin})
            Fechainicio = CType(results(1),Date)
            Fechafin = CType(results(2),Date)
            Msgerror = CType(results(3),String)
            Return CType(results(0),Boolean)
        End Function
        
        '''<remarks/>
        Public Overloads Sub ExecuteAsync(ByVal Fechainicio As Date, ByVal Fechafin As Date)
            Me.ExecuteAsync(Fechainicio, Fechafin, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub ExecuteAsync(ByVal Fechainicio As Date, ByVal Fechafin As Date, ByVal userState As Object)
            If (Me.ExecuteOperationCompleted Is Nothing) Then
                Me.ExecuteOperationCompleted = AddressOf Me.OnExecuteOperationCompleted
            End If
            Me.InvokeAsync("Execute", New Object() {Fechainicio, Fechafin}, Me.ExecuteOperationCompleted, userState)
        End Sub
        
        Private Sub OnExecuteOperationCompleted(ByVal arg As Object)
            If (Not (Me.ExecuteCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent ExecuteCompleted(Me, New ExecuteCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        Public Shadows Sub CancelAsync(ByVal userState As Object)
            MyBase.CancelAsync(userState)
        End Sub
        
        Private Function IsLocalFileSystemWebService(ByVal url As String) As Boolean
            If ((url Is Nothing)  _
                        OrElse (url Is String.Empty)) Then
                Return false
            End If
            Dim wsUri As System.Uri = New System.Uri(url)
            If ((wsUri.Port >= 1024)  _
                        AndAlso (String.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) = 0)) Then
                Return true
            End If
            Return false
        End Function
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2053.0")>  _
    Public Delegate Sub ExecuteCompletedEventHandler(ByVal sender As Object, ByVal e As ExecuteCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2053.0"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class ExecuteCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As Boolean
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),Boolean)
            End Get
        End Property
        
        '''<remarks/>
        Public ReadOnly Property Fechainicio() As Date
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(1),Date)
            End Get
        End Property
        
        '''<remarks/>
        Public ReadOnly Property Fechafin() As Date
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(2),Date)
            End Get
        End Property
        
        '''<remarks/>
        Public ReadOnly Property Msgerror() As String
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(3),String)
            End Get
        End Property
    End Class
End Namespace
