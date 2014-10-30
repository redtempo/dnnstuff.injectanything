Imports DotNetNuke.Entities.Host
Imports DotNetNuke.Entities.Portals

#If DNNVERSION = "DNN5" Then
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
        Return InStr(1, "," & Host.FileExtensions.ToLower, "," & Extension.ToLower) <> 0
    End Function

    Public Function AllowedExtensions() As String
        Return Replace(Host.FileExtensions, ",", ", *.")
    End Function

    Public Function GetFileContents(ByVal portalFilename As String, ByVal homeDirectory As String, ByVal portalID As Integer) As String
        Dim contents As String = ""
        ' inject using file
        If portalFilename.Length > 0 Then
            Dim fi As IO.FileInfo
            If portalFilename.Contains("FileID=") Then
                Dim fileController As New DotNetNuke.Services.FileSystem.FileController
                Dim fileInfo As DotNetNuke.Services.FileSystem.FileInfo = fileController.GetFileById(Int32.Parse(portalFilename.Replace("FileID=", "")), portalID)
                fi = New IO.FileInfo(fileInfo.PhysicalPath)
            Else
                fi = New IO.FileInfo(homeDirectory & portalFilename)
            End If
            contents = DNNStuff.DNNUtilities.GetFileContents(fi)
        End If
        Return contents
    End Function

End Module
#End If
