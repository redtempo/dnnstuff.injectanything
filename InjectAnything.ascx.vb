Imports DotNetNuke
Imports System.Text.RegularExpressions

Namespace DNNStuff.InjectAnything

    Partial Class InjectAnything
        Inherits DotNetNuke.Entities.Modules.PortalModuleBase
        Implements DotNetNuke.Entities.Modules.IActionable

        Dim _ms As ModuleSettings

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()

            Try
                ' process file or text
                Process()

            Catch ex As Exception
                DotNetNuke.Services.Exceptions.ProcessModuleLoadException(Me, ex, True)
            End Try
        End Sub

#End Region

#Region " Injection"
        Private Sub Process()
            ' get settings
            _ms = New ModuleSettings(ModuleId, PortalSettings, Me)

            ' disabled?
            If Not _ms.Enable Then Exit Sub

            Dim injectedText As String = ""

            Select Case _ms.Source
                Case "Standard"
                    Dim filename As String = _ms.FullFilePath
                    If filename.Length = 0 Then Exit Sub

                    Dim fi As IO.FileInfo = New IO.FileInfo(filename)
                    If _ms.LinkFile Then
                        injectedText = GetFileLink(fi)
                    Else
                        Dim script As StandardScript = DNNUtilities.GetStandardScript(fi)
                        injectedText = script.Items(0).Content
                    End If
                Case "File"
                    Dim filename As String = _ms.FullFilePath
                    If filename.Length = 0 Then Exit Sub

                    Dim fi As IO.FileInfo = New IO.FileInfo(filename)
                    If _ms.LinkFile Then
                        injectedText = GetFileLink(fi)
                    Else
                        injectedText = GetFileContents(fi)
                    End If
                Case Else
                    ' use the text
                    injectedText = _ms.Text
            End Select
            If injectedText.Length > 0 Then InjectText(injectedText)

        End Sub

        Private Function GetFileLink(ByVal fi As IO.FileInfo) As String
            ' returns html to link file as is
            If Not fi.Exists Then Return ""

            Select Case fi.Extension.ToLower
                Case ".css"
                    Return String.Format("<link href=""{0}"" rel=""stylesheet"" type=""text/css"" />", ResolveUrl(MapURL(fi.FullName)))
                Case ".js"
                    Return String.Format("<script src=""{0}"" type=""text/javascript"" />", ResolveUrl(MapURL(fi.FullName)))
                Case ".vbs"
                    Return String.Format("<script src=""{0}"" type=""text/vbscript"" />", ResolveUrl(MapURL(fi.FullName)))
                Case Else
                    Return GetFileContents(fi)
            End Select
            Return ""
        End Function

        Private Function GetFileContents(ByVal fi As IO.FileInfo) As String
            ' retrieve contents of a file as a string
            Dim fileStream As IO.StreamReader = Nothing
            Dim fileContents As String

            Try
                fileStream = IO.File.OpenText(fi.FullName)
                fileContents = fileStream.ReadToEnd

                Return fileContents

            Finally
                If fileStream IsNot Nothing Then
                    fileStream.Close()
                End If
            End Try

            Return ""
        End Function

        Public Function MapURL(ByVal Path As String) As String
            Dim AppPath As String = _
            HttpContext.Current.Server.MapPath("~")
            Dim url As String = String.Format("~{0}" _
            , Path.Replace(AppPath, "").Replace("\", "/"))
            Return url
        End Function

        Private Sub InjectText(ByVal s As String)
            ' inject text into page at desired point
            Dim replacementText As String = MakeReplacements(Server.HtmlDecode(s))

            If _ms.Debug Then
                Dim injectInto As Control = Me
                injectInto = New HtmlGenericControl("pre")
                InjectAtPoint(injectInto)
                injectInto.Controls.Add(New LiteralControl(Server.HtmlEncode(replacementText)))
            Else
                InjectAtPoint(New LiteralControl(replacementText))
            End If
        End Sub

        Private Sub InjectAtPoint(ByVal ctl As Control)
            ' inject control at the proper insertion point
            Dim point As Control

            Select Case _ms.InsertionPoint
                Case "Module"
                    Me.Controls.Add(ctl)
                Case "Head"
                    point = Page.FindControl("head")
                    If point IsNot Nothing Then
                        point.Controls.Add(ctl)
                    End If
                Case "AfterForm"
                    point = Page.FindControl("form")
                    If point IsNot Nothing Then
                        point.Controls.AddAt(0, ctl)
                    End If
                Case "BeforeForm"
                    point = Page.FindControl("form")
                    If point IsNot Nothing Then
                        point.Controls.Add(ctl)
                    End If
                Case "AfterBody"
                    point = Page.FindControl("body")
                    If point IsNot Nothing Then
                        point.Controls.AddAt(0, ctl)
                    End If
                Case "BeforeBody"
                    point = Page.FindControl("body")
                    If point IsNot Nothing Then
                        point.Controls.Add(ctl)
                    End If
            End Select
        End Sub
        Private Function UserRoles() As String
            Dim roleController As New Security.Roles.RoleController
            Return String.Join(",", roleController.GetRolesByUser(UserId, PortalId))
        End Function
        Private Function UserRolesArray() As String
            Return "'" & UserRoles.Replace(",", "','") & "'"
        End Function

        Private Function MakeReplacements(ByVal s As String) As String
            ' returns string with all token replacements made
            Dim ctrl As New DotNetNuke.Entities.Modules.ModuleController
            Dim settings As Hashtable = ctrl.GetModuleSettings(ModuleId)

            Dim ret As String = s

            ' define tokens
            ' NOTE: make sure to add to exclusions in the Settings.ascx.vb method, InitTokenListFromString
            Dim justTokens As Hashtable = JustTokenSettings()
            justTokens.Add("STANDARDFOLDER", ResolveUrl("standard"))
            justTokens.Add("ROLES", UserRoles())
            justTokens.Add("ROLESARRAY", UserRolesArray())

            ' merge in querystring
            Dim qsTokens As Hashtable = DNNStuff.Common.StandardVariables.QueryString(Me.Request)
            justTokens = DNNStuff.Common.HashTableHelpers.Merge(justTokens, qsTokens)

            ' merge in servervars
            Dim svTokens As Hashtable = DNNStuff.Common.StandardVariables.ServerVars(Me.Request)
            justTokens = DNNStuff.Common.HashTableHelpers.Merge(justTokens, svTokens)

            ' merge in formvars
            Dim fvTokens As Hashtable = DNNStuff.Common.StandardVariables.FormVars(Me.Request)
            justTokens = DNNStuff.Common.HashTableHelpers.Merge(justTokens, fvTokens)

            ' logic replacement
            Dim logicReplacer As New DNNStuff.Utilities.RegularExpression.IfDefinedTokenReplacement(justTokens)
            ret = logicReplacer.Replace(ret)

            ' then built in replacement
            Dim replacer As New DNNStuff.Utilities.RegularExpression.TokenReplacement(justTokens)
            replacer.ReplaceIfNotFound = False
            ret = replacer.Replace(ret)

            ' standard dnn replacement first
            ret = Compatibility.ReplaceGenericTokens(Me, ret)

            ' replace escaped characters
            ret = ReplaceEscapedCharacters(ret)

            Return ret
        End Function

        Public Shared Function ReplaceEscapedCharacters(ByVal text As String) As String
            text = Regex.Replace(text, "0x5B", "[", RegexOptions.IgnoreCase)
            text = Regex.Replace(text, "0x5D", "]", RegexOptions.IgnoreCase)

            Return text
        End Function
        Private Function JustTokenSettings() As Hashtable
            Const STR_TOKEN_PREFIX As String = "Token_"

            Dim justTokens As New Hashtable
            For Each key As String In Settings.Keys
                If key.StartsWith(STR_TOKEN_PREFIX) Then
                    justTokens.Add(key.Substring(STR_TOKEN_PREFIX.Length, key.Length - STR_TOKEN_PREFIX.Length), Settings(key))
                End If
            Next
            Return justTokens
        End Function
#End Region

#Region " Optional Interfaces"

        Public ReadOnly Property ModuleActions() As Entities.Modules.Actions.ModuleActionCollection Implements Entities.Modules.IActionable.ModuleActions
            Get
                Dim Actions As New Entities.Modules.Actions.ModuleActionCollection
                Actions.Add(GetNextActionID, DotNetNuke.Services.Localization.Localization.GetString(Entities.Modules.Actions.ModuleActionType.ContentOptions, LocalResourceFile), Entities.Modules.Actions.ModuleActionType.EditContent, "", "", EditUrl(), False, Security.SecurityAccessLevel.Edit, True, False)
                Actions.Add(GetNextActionID, DotNetNuke.Services.Localization.Localization.GetString("UploadScript", LocalResourceFile), Entities.Modules.Actions.ModuleActionType.ImportModule, "", "", EditUrl("UploadScript"), False, Security.SecurityAccessLevel.Host, True, False)
                Return Actions
            End Get
        End Property

#End Region
    End Class

End Namespace
