Imports System.Net

Public Class ClassGetResponse
    Public Sub Ping(args As String)


        Dim mResp As HttpWebRequest = Nothing
        Try
            mResp = CType(WebRequest.Create(args), HttpWebRequest)
            mResp.Credentials = CredentialCache.DefaultNetworkCredentials
            Dim mResponse As HttpWebResponse = mResp.GetResponse()


            Console.WriteLine(mResponse.StatusCode.ToString)
            Console.WriteLine("El servicios está activo")
            mResponse.Close()
        Catch ex As Exception
            Console.WriteLine("Source:" & ex.Source & " Message:" & ex.Message)
            Console.WriteLine("************** Error al invocar el servicio ******************")
        End Try

    End Sub
End Class
