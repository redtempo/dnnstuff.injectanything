#If DNNVERSION = "DNN4" Then

Module Compatibility
    ' this module will provide compatibility between DNN versions

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

End Module

#End If
