Module Module1

    Sub Main(ByVal args() As String)
        Dim intDias As Integer
        Dim dtFechaIni As Date
        Dim dtFechaFin As Date
        Dim BolIsvalid As Boolean = False

        If args.Length > 0 Then

            Select Case args.Length
                Case 1
                    Select Case args(0).ToUpper()


                        Case "?"
                            Console.WriteLine("Parametros ?:Esta ayuda")
                            Console.WriteLine("Parametros d NN")
                            Console.WriteLine("Indica que se buscaran los documentos en los NN dias previos")
                            Console.WriteLine("Ejemplo: d 30")
                            Console.WriteLine("Parametros y ")
                            Console.WriteLine("Indica que se buscaran los documentos del año fiscal actual")
                            Console.WriteLine("Parametros c ")
                            Console.WriteLine("Muestra Configuración de los Webserbices.")
                            Console.WriteLine("Ejemplo: y")
                        Case "Y"
                            dtFechaIni = DateAndTime.DateSerial(Year(Now), 1, 1)
                            dtFechaFin = DateAndTime.DateSerial(Year(Now) + 1, 1, 1).AddSeconds(-1)
                            BolIsvalid = True
                        Case "C"
                            'dtFechaIni = DateAndTime.DateSerial(Year(Now), 1, 1)
                            'dtFechaFin = DateAndTime.DateSerial(Year(Now) + 1, 1, 1).AddSeconds(-1)
                            Dim mWs As String = My.Settings.CteWSPortalProveedores_ProxyWS_ProcWSValidaCDFIs
                            Console.WriteLine(mWs)

                            VerificarConexionURL(mWs)
                            Exit Sub

                            BolIsvalid = False


                    End Select

                Case 2
                    If args(0) = "d" And Integer.TryParse(args(1), intDias) Then
                        dtFechaFin = Today.AddDays(1).AddSeconds(-1)
                        dtFechaIni = Today.AddDays(-1 * intDias)
                        BolIsvalid = True
                    Else
                        If Date.TryParse(args(0), dtFechaIni) AndAlso Date.TryParse(args(1), dtFechaFin) Then
                            BolIsvalid = True
                        
                        End If


                    End If
                
            End Select
            If BolIsvalid Then
                Console.WriteLine("Fechas a Procesar Inicio:" & Format("{0:d}", dtFechaIni))
                Console.WriteLine("Fechas a Procesar Fin:" & Format("{0:d}", dtFechaFin))
                Try
                    Console.WriteLine("Procesando WebService")
                    Dim CteProxy As New ProxyWS.ProcWSValidaCDFIs
                    Dim MsgError As String = ""

                    Dim mBolRec As Boolean
                    mBolRec = CteProxy.Execute(dtFechaIni, dtFechaFin, MsgError)
                    Console.WriteLine("WebService Procesado!")
                    If mBolRec = False Then
                        Console.WriteLine(MsgError)

                    End If
                Catch ex As Exception

                    Console.WriteLine(ex.Message)
                End Try
            Else
                Console.WriteLine("Llamada invalida indique ? para ver la ayuda")

            End If
            
        Else
            Console.WriteLine("Llamada invalida indique ? para ver la ayuda")


        End If


        

    End Sub

    Public Function VerificarConexionURL(mURL As String) As Boolean

        Dim Peticion As System.Net.WebRequest

        'Peticion = default(System.Net.WebRequest);
        Dim Respuesta As System.Net.HttpWebResponse


        Try
            Console.WriteLine("Consultando:" & mURL & "?wsdl")
            Peticion = System.Net.WebRequest.Create(mURL)
            Respuesta = Peticion.GetResponse()
            Console.WriteLine(Respuesta.GetResponseStream.ToString)

            Return True
        Catch ex As System.Net.WebException
            If ex.Status = Net.WebExceptionStatus.NameResolutionFailure Then
                Return False
            End If
            Console.WriteLine("proceso fallo")
            Return False
        End Try
    End Function


End Module


