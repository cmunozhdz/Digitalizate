Imports System.Threading

Module Module1
    Dim mIndexTTotalThreads As Integer
    Dim mThread As Thread
    Dim MAX_TREADS As Integer
    Dim mTimeoutWs As Integer


    Sub Main(args As String())
        Try
            Dim mBolUsarThreads = False
            Dim pIdLote As Long = 0

            If args.Length = 0 Then
                'si no hay parametros toma los datos de la configuracion
                If Integer.TryParse(My.Settings("MAXTHREADS"), MAX_TREADS) Then
                    Console.WriteLine("Setting:MAXTHREADS Actual:" & MAX_TREADS)
                Else
                    Console.WriteLine("Setting:MAXTHREADS No Encontrado o invalido, tomando el valor predeterminado 10")
                    MAX_TREADS = 10

                End If
                mBolUsarThreads = My.Settings.UsarThreads




            Else
                Select Case args(0)
                    Case "-t"
                        Dim mObjTest As New ClassGetResponse

                        Console.WriteLine("Validando comunicacion con webservice")

                        Console.WriteLine("ProcWsGetArchivosProceso:" & My.Settings.CteValidacionMasiva_ProxyWsGetArchivos_ProcWsGetArchivosProceso)
                        mObjTest.Ping(My.Settings.CteValidacionMasiva_ProxyWsGetArchivos_ProcWsGetArchivosProceso)

                        Console.WriteLine("ProcWSGetFacturasCM     :" & My.Settings.CteValidacionMasiva_ProxyWSGetFacturasCM_ProcWSGetFacturasCM)
                        mObjTest.Ping(My.Settings.CteValidacionMasiva_ProxyWSGetFacturasCM_ProcWSGetFacturasCM)
                        Console.WriteLine("ProcWSUpdStatusCM       :" & My.Settings.CteValidacionMasiva_ProxyWSUpdStatusCM_ProcWSUpdStatusCM)
                        mObjTest.Ping(My.Settings.CteValidacionMasiva_ProxyWSUpdStatusCM_ProcWSUpdStatusCM)
                        End

                    Case "-h"
                        Console.WriteLine("-h Muestra está ayuda")
                        Console.WriteLine("-t Muestra parametros de conexion de webservices")
                        Console.WriteLine("-u Solo se ejecuta una tarea y hasta que llegue a su fin continua sin usar threads")
                        Console.WriteLine("-u idLote Solo se ejecuta una tarea y hasta que llegue a su fin continua sin usar threads par el lote IdLote")
                        Console.WriteLine("-s idLote Ejecuta el proceso usando varias  tareas  usando threads par el lote IdLote")
                        End

                    Case "-u"
                        mBolUsarThreads = False
                    Case "-s"
                        mBolUsarThreads = True 'Forza el uso de thread's sin tomarlo del archivo de configuracion


                    Case Else
                        Console.WriteLine("Parametrización Incorrecta -h para ver la ayuda")
                        End


                End Select

                Try
                    If Not String.IsNullOrEmpty(args(1)) Then
                        If Not Long.TryParse(args(1), pIdLote) Then
                            Console.WriteLine("Error Parametrización Incorrecta el lote suministrado debe ser numerico procesando todos los archivos")
                        End If
                    End If
                Catch ex As Exception
                    pIdLote = 0 'No hay lote a procesar

                End Try

            End If

            If ValidaTarea() Then
                mTimeoutWs = My.Settings.SegundosTimeOutWs

                If mBolUsarThreads Then
                    Console.WriteLine("Iniciando proceso de threads")
                    mIndexTTotalThreads = 0
                    BuscaMovimientos()

                Else
                    Console.WriteLine(String.Format("Iniciando proceso sin threads Lote: {0:d}", pIdLote))

                    MetodoInicioSinThreads(pIdLote)

                    'MetodoInicioSinThreads(120)


                End If
            End If


        Catch ex As Exception
            Console.WriteLine(String.Format("Excepcion no Controloada Origen {0} Descripción: {1} " & ex.Source, ex.Message))
        End Try



    End Sub

    Sub CrearThread(PindexTread As Integer, pIdLote As Long)
        Try
            Console.WriteLine(String.Format("{0:yyyy-MM-dd hh:mm:ss} Creando Thread  {1:d}.... Para el lote {2:d}", Date.Now, PindexTread, pIdLote))

            mThread = New Thread(AddressOf MetodoInicio)

            mThread.Start(pIdLote)
        Catch ex As Exception
            Console.WriteLine(String.Format("{0:yyyy-MM-dd hh:mm:ss} Error No controlado al crear Thread  {1:d}.... No Error {2:d} Descripcion:{3}", Date.Now, PindexTread, ex.Message))
        End Try



    End Sub
    ''' <summary>
    ''' Metodo a ejecutar durante la ejecucion del thread
    ''' </summary>
    ''' <param name="pIdArchivo">Id. de lote que se va ejecutar la validacion comercial</param>
    ''' 
    Sub MetodoInicioSinThreads(pIdArchivo As Long)
        Try
            Dim mBolContinuar As Boolean


            Console.WriteLine(String.Format("{0:yyyy-MM-dd hh:mm:ss} Iniciando el proceso .... Ejecutando WebService para Indentificar procesos", Date.Now))
            Dim ProxyWs As New ProxyWsGetArchivos.ProcWsGetArchivosProceso
            ProxyWs.Timeout = mTimeoutWs * 1000


            Dim mItem() As ProxyWsGetArchivos.SDTCMArchivoSDTCMArchivoItem = ProxyWs.Execute()
            'Ejecutará la validacion comercial del archivo encontrado.
            Console.WriteLine(String.Format("{0:yyyy-MM-dd hh:mm:ss} WebService Finalizado Elementos encontrados {1:d}", Date.Now, mItem.LongCount))

            For iItem As Long = 0 To mItem.LongCount - 1
                Dim pIdLote As Long = mItem(iItem).CMArchivoId
                If pIdArchivo = 0 Then
                    'Procesa todos los archivos 
                    mBolContinuar = True
                Else
                    'Procesar solo el archivo suministrado en los parametros.
                    mBolContinuar = pIdArchivo = pIdLote


                End If
                If mBolContinuar Then
                    Console.WriteLine(String.Format("{0:yyyy-MM-dd hh:mm:ss} Iniciando proceso de validación Comercial archivo {1:d}", Date.Now, pIdLote))
                    Dim mWsCte As New ProxyWSGetFacturasCM.ProcWSGetFacturasCM
                    mWsCte.Timeout = mTimeoutWs * 1000 'El tiempo de espera se calibra a mas milisegudos

                    mWsCte.Execute(pIdLote)
                    mWsCte = Nothing

                    Dim mCsCteSts As New ProxyWSUpdStatusCM.ProcWSUpdStatusCM
                    mCsCteSts.Timeout = mTimeoutWs * 1000
                    mCsCteSts.Execute(pIdLote)
                    mCsCteSts = Nothing

                    'Thread.Sleep(50000)
                    Console.WriteLine(String.Format("{0:yyyy-MM-dd hh:mm:ss} ******* Validación Comercial para Lote: {1:d} Finalizado *****", Date.Now, pIdLote))



                    If pIdArchivo <> 0 Then
                        Exit For 'Detiene el proceso ya que se pidido especificamente procesar un archivo


                    End If

                End If


            Next
            Console.WriteLine(String.Format("{0:yyyy-MM-dd hh:mm:ss} Proceso de carga masiva terminado", Date.Now))




        Catch ex As Exception
            Console.WriteLine(ex.Message)

            'Console.WriteLine(String.Format("Excepcion no Controloada Origen {0} Descripción: {1} " & ex.Source, ex.Message))

        End Try

    End Sub

    ''' <summary>
    ''' Metodo a ejecutar durante la ejecucion del thread
    ''' </summary>
    ''' <param name="pValue">Id. de lote que se va ejecutar la validacion comercial</param>
    Sub MetodoInicio(pValue As Long)
        Dim pIdLote As Long = pValue
        Try
            mTimeoutWs = My.Settings.SegundosTimeOutWs


            Console.WriteLine(String.Format("{0:yyyy-MM-dd hh:mm:ss} Iniciando proceso de validación Comercial archivo {1:d}", Date.Now, pIdLote))
            Dim mWsCte As New ProxyWSGetFacturasCM.ProcWSGetFacturasCM
            mWsCte.Timeout = mTimeoutWs * 1000 'El tiempo de espera se calibra a mas milisegudos

            mWsCte.Execute(pIdLote)

            Dim mCsCteSts As New ProxyWSUpdStatusCM.ProcWSUpdStatusCM
            mCsCteSts.Timeout = mTimeoutWs * 1000
            mCsCteSts.Execute(pIdLote)
            mCsCteSts = Nothing

            Console.WriteLine(String.Format("{0:yyyy-MM-dd hh:mm:ss} Validación Comercial para Archivo: {1:d} Finalizado", Date.Now, pIdLote))

        Catch ex As Exception
            'Console.WriteLine(String.Format("Excepcion no Controloada Origen {0} Descripción: {1} " & ex.Source, ex.Message))
            Dim mstrMsgError As String = ex.Message
            Console.WriteLine(String.Format("{0:yyyy-MM-dd hh:mm:ss} MetodoInicio({1:d})", Date.Now, pValue))
            Console.WriteLine(String.Format("{0:yyyy-MM-dd hh:mm:ss} Error {1:s}", Date.Now, mstrMsgError))
        Finally
            Console.WriteLine(String.Format("****** {0:yyyy-MM-dd hh:mm:ss} Método Inicio Finalizado para Archivo  {1:d} ****", Date.Now, pIdLote))
        End Try

    End Sub
    Sub ReiniciarThread()
        mIndexTTotalThreads = mIndexTTotalThreads - 1
    End Sub
    Function ValidaTarea() As Boolean
        Dim mStrAppName = My.Application.Info.AssemblyName
        Console.WriteLine("Validando Instancias de la aplicacion:" & mStrAppName)
        Dim aplicacioncorriendo As Process() = Process.GetProcessesByName(mStrAppName)
        If aplicacioncorriendo.LongLength > 1 Then
            Console.Write("La aplicacion está en ejecucion en otra instancia")
            End

        End If
        Return True

    End Function
    ''' <summary>
    ''' Consulta el web service para identificar los documentos que ya estan en proceso.
    ''' 
    ''' </summary>
    Sub BuscaMovimientos()
        'Dim mIdLoteValidacionGX As Long
        Try

            Dim mTotalThreads As Integer = My.Settings.MAXTHREADS
            Console.WriteLine("Iniciando el proceso .... Ejecutando WebService")
            Dim ProxyWs As New ProxyWsGetArchivos.ProcWsGetArchivosProceso
            Dim mItem() As ProxyWsGetArchivos.SDTCMArchivoSDTCMArchivoItem = ProxyWs.Execute()
            Console.WriteLine("WebService Finalizado Exitosamente")
            If mItem.Count > 0 Then
                If mItem.Count < mTotalThreads - 1 Then
                    mTotalThreads = mItem.Count - 1

                End If
                For iItem As Integer = 0 To mTotalThreads - 1
                    Console.WriteLine(String.Format("{0:yyyy-MM-dd hh:mm:ss} Creando Thread:{1}", Date.Now, iItem))
                    CrearThread(iItem, mItem(iItem).CMArchivoId)
                    Console.WriteLine(String.Format("{0:yyyy-MM-dd hh:mm:ss} Thread:{1} Creado exitosamente", Date.Now, iItem))
                Next

            Else
                Console.WriteLine(String.Format("{0:yyyy-MM-dd hh:mm:ss} No existen Archivos a procesar", Date.Now))

            End If

            Console.WriteLine(String.Format("{0:yyyy-MM-dd hh:mm:ss} BuscaMovimientos Proceso Finalizado", Date.Now))

        Catch ex As Exception
            Console.WriteLine(String.Format("{0:yyyy-MM-dd hh:mm:ss} Excepcion no controlada BuscaMovimiento  Descripcion:{1}", Date.Now, ex.Message))
        End Try

    End Sub
End Module
