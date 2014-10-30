Imports DotNetNuke.Entities.Host
Imports DotNetNuke.Services.FileSystem

Module Compatibility

    Public Function ReplaceGenericTokens(ByVal thismodule As DNNStuff.InjectAnything.InjectAnything, ByVal text As String) As String
        Dim ret As String

        Dim objTokenReplace As New DotNetNuke.Services.Tokens.TokenReplace()
        objTokenReplace.ModuleId = thismodule.ModuleId
        ret = objTokenReplace.ReplaceEnvironmentTokens(text)

        objTokenReplace.User = thismodule.UserInfo
        objTokenReplace.AccessingUser = thismodule.UserInfo

        If thismodule.UserInfo.Profile.PreferredLocale IsNot Nothing Then ' will be nothing for anonymous users
            objTokenReplace.Language = thismodule.UserInfo.Profile.PreferredLocale
        End If
        ret = objTokenReplace.ReplaceEnvironmentTokens(ret)

        Return ret
    End Function

    Public Function IsAllowedExtension(ByVal Extension As String) As Boolean
        Return Host.AllowedExtensionWhitelist.IsAllowedExtension(Extension)
    End Function

    Public Function AllowedExtensions() As String
        Return Host.AllowedExtensionWhitelist.ToDisplayString()
    End Function

    Public Function GetFileContents(ByVal portalFilename As String, ByVal homeDirectory As String, ByVal portalId As Integer) As String
        Dim contents As String = ""
        ' inject using file
        If portalFilename.Length > 0 Then
            If portalFilename.StartsWith("FileID=") Then
                Dim fileId As Integer = Int32.Parse(portalFilename.Substring(7))
                Dim fileInfo As IFileInfo = FileManager.Instance.GetFile(fileId)
                Dim fileStream As IO.Stream = FileManager.Instance.GetFileContent(fileInfo)
                Using reader As New IO.StreamReader(fileStream)
                    contents = reader.ReadToEnd()
                End Using
            Else
                Dim fi As IO.FileInfo = New IO.FileInfo(homeDirectory & portalFilename)
                contents = DNNStuff.DNNUtilities.GetFileContents(fi)
            End If
        End If
        Return contents
    End Function

End Module
