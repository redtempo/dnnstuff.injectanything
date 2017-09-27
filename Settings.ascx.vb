Imports DotNetNuke
Imports DotNetNuke.Common

Imports System.Configuration
Imports System.Text.RegularExpressions
Imports System.Xml
Imports System.Collections.Generic

Namespace DNNStuff.InjectAnything

    Partial Class Settings
        Inherits DotNetNuke.Entities.Modules.PortalModuleBase

        Protected WithEvents ctlfile As DotNetNuke.UI.UserControls.UrlControl

        Private _tokenList As Dictionary(Of String, DNNStuff.Utilities.RegularExpression.Token)
        Private _script As StandardScript = Nothing

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()

            If Not Page.IsPostBack Then
                Dim ms As New ModuleSettings(ModuleId, PortalSettings, Me)
                LoadModuleSettings(ms)
                _tokenList = InitTokenListFromModuleSettings(ms)
                RenderTokensAsControls()
                SetControlValuesFromTokens()
            Else
                _tokenList = DirectCast(Session("Tokens"), Dictionary(Of String, DNNStuff.Utilities.RegularExpression.Token))
                RenderTokensAsControls()
            End If

        End Sub

        Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If DNNUtilities.SafeDNNVersion().Major = 5 Then
                DNNUtilities.InjectCSS(Me.Page, ResolveUrl("Resources/Support/edit_5.css"))
            Else
                DNNUtilities.InjectCSS(Me.Page, ResolveUrl("Resources/Support/edit.css"))
            End If
            Page.ClientScript.RegisterClientScriptInclude(Me.GetType, "yeti", ResolveUrl("resources/support/yetii-min.js"))

            SetVisibilityBasedOnSource()
        End Sub

#End Region

#Region "Base Method Implementations"
        Public Sub UpdateSettings()
            Dim ms As New ModuleSettings(ModuleId)
            With ms
                .InsertionPoint = ddlInsertionPoint.SelectedValue
                .Source = ddlSource.SelectedValue
                .StandardFile = ddlStandard.SelectedValue
                .File = ctlfile.Url
                .LinkFile = chkLinkFile.Checked And .Source = "File"
                .Text = txtText.Text
                .Debug = chkDebug.Checked
                .Enable = chkEnable.Checked
            End With
            ms.UpdateSettings()
        End Sub

        Public Sub UpdateSettingsTokens()
            Dim ms As New ModuleSettings(ModuleId)
            ms.UpdateSettingsTokens(_tokenList)
        End Sub

        Private Sub SelectListItem(ByVal ctl As ListControl, ByVal value As String)
            Dim li As ListItem

            ctl.ClearSelection()
            li = ctl.Items.FindByValue(value)
            If li IsNot Nothing Then li.Selected = True

        End Sub

        Public Sub LoadModuleSettings(ByVal ms As ModuleSettings)

            SelectListItem(ddlInsertionPoint, ms.InsertionPoint)
            SelectListItem(ddlSource, ms.Source)

            ' bind standard scripts
            BindStandard(ddlStandard)

            Select Case ms.Source
                Case "Standard"
                    SelectListItem(ddlStandard, ms.StandardFile)
                Case "File"
                    ctlfile.Url = ms.File
            End Select

            chkLinkFile.Checked = ms.LinkFile
            txtText.Text = ms.Text
            chkDebug.Checked = ms.Debug
            chkEnable.Checked = ms.Enable

        End Sub

        Public Sub SetControlValuesFromTokens()
            ' set control values based on token values
            Dim ctrl As TextBox
            For Each key As String In _tokenList.Keys

                Dim t As DNNStuff.Utilities.RegularExpression.Token = _tokenList(key)

                ctrl = DirectCast(pnlToken.FindControl("Token_" & t.Name), TextBox)
                If Not ctrl Is Nothing Then
                    If t.Value IsNot Nothing Then
                        If t.Value.Length > 0 Then
                            ctrl.Text = t.Value
                        End If
                    End If
                End If
            Next
        End Sub
#End Region

        Private Sub SetVisibilityBasedOnSource()
            Select Case ddlSource.SelectedValue
                Case "File"
                    divStandard.Visible = False
                    divFile.Visible = True
                    divText.Visible = False
                Case "Standard"
                    divStandard.Visible = True
                    divFile.Visible = False
                    divText.Visible = False
                Case "Text"
                    divStandard.Visible = False
                    divFile.Visible = False
                    divText.Visible = True
            End Select
        End Sub

        Private Sub cmdUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdUpdate.Click
            RetrieveTokenValuesFromControls()
            UpdateSettings()
            UpdateSettingsTokens()
            ' Redirect back to the portal home page
            Response.Redirect(NavigateURL(), True)
        End Sub

        Private Sub cmdCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
            ' Redirect back to the portal home page
            Response.Redirect(NavigateURL(), True)
        End Sub

        Private Sub cmdRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdRefresh.Click
            UpdateSettings()
            UpdateSettingsTokens()
            Response.Redirect(EditUrl())
        End Sub

        Private Sub BindStandard(ByVal o As ListControl)
            Dim standardFolder As New IO.DirectoryInfo(Server.MapPath(ResolveUrl("Standard")))
            o.Items.Clear()
            For Each fi As IO.FileInfo In standardFolder.GetFiles("*.xml")

                o.Items.Add(New ListItem(fi.Name.Replace(".xml", ""), fi.Name))
            Next
        End Sub

#Region " Token Support"

        Private Function InitTokenListFromModuleSettings(ByVal ms As ModuleSettings) As System.Collections.Generic.Dictionary(Of String, DNNStuff.Utilities.RegularExpression.Token)
            ' build list of tokens based on current module settings
            Return ms.LoadSettingsTokens(InitTokenListFromCurrentSettings(ms.Source, ms.StandardFile, ms.File, ms.Text))
        End Function

        Private Function InitTokenListFromCurrentSettings(ByVal source As String, ByVal standardFilename As String, ByVal portalFilename As String, ByVal text As String) As System.Collections.Generic.Dictionary(Of String, DNNStuff.Utilities.RegularExpression.Token)
            ' build list of tokens based on current module settings
            Dim list As New System.Collections.Generic.Dictionary(Of String, DNNStuff.Utilities.RegularExpression.Token)

            ' clear standard script
            _script = Nothing

            Select Case source
                Case "Standard"
                    ' inject using file
                    If standardFilename.Length > 0 Then
                        Dim filename As String = HttpContext.Current.Server.MapPath(Me.ResolveUrl("Standard/" & standardFilename))
                        Dim fi As IO.FileInfo = New IO.FileInfo(filename)
                        Dim script As StandardScript = DNNUtilities.GetStandardScript(fi)
                        list = InitTokenListFromString(script.Items(0).Content)
                        ' set script
                        _script = script
                    End If
                Case "File"
                    ' inject using file
                    list = InitTokenListFromString(Compatibility.GetFileContents(portalFilename, PortalSettings.HomeDirectoryMapPath, PortalId))
                Case Else
                    ' use the text
                    If text.Length > 0 Then list = InitTokenListFromString(Server.HtmlDecode(text))
            End Select
            Return list
        End Function

        Private Function InitTokenListFromString(ByVal s As String) As System.Collections.Generic.Dictionary(Of String, DNNStuff.Utilities.RegularExpression.Token)
            ' build list of tokens based on file or text
            Dim replacer As New DNNStuff.Utilities.RegularExpression.TokenReplacement(New Hashtable)

            Dim exclusions As New Generic.List(Of String)
            exclusions.Add("STANDARDFOLDER")
            exclusions.Add("ROLES")
            exclusions.Add("ROLESARRAY")

            ' add exclusions for some simple array indexes ie. myArray[i]
            exclusions.Add("I")
            exclusions.Add("J")
            exclusions.Add("K")

            Dim tokens As System.Collections.Generic.Dictionary(Of String, DNNStuff.Utilities.RegularExpression.Token)
            tokens = replacer.Tokens(s, exclusions)

            ' remove any tokens containing special characters (:) - this will catch all QS:, QUERYSTRING: and standard DNN tokens
            Dim loopTokens As New List(Of String)
            For Each key As String In tokens.Keys
                If key.Contains(":") Then
                    loopTokens.Add(key)
                End If
            Next
            For Each key As String In loopTokens
                tokens.Remove(key)
            Next

            Return tokens

        End Function

        Private Sub RenderTokensAsControls()
            ' renders tokens as textboxes based on tokenList

            pnlToken.Controls.Clear()

            ' render description and help
            If _script IsNot Nothing Then
                pnlToken.Controls.Add(New LiteralControl(String.Format("<span class=""SubHead"">Description</span>:<br />{0}<br />", _script.Description)))
                pnlToken.Controls.Add(New LiteralControl(String.Format("<span class=""SubHead"">Help</span>:<br />{0}", _script.Help)))
            End If

            For Each key As String In _tokenList.Keys


                Dim t As DNNStuff.Utilities.RegularExpression.Token = _tokenList(key)

                Dim tb As New WebControls.TextBox
                tb.ID = "Token_" & t.Name

                ' add label
                Dim d As New HtmlControls.HtmlGenericControl("div")
                d.Attributes.Add("class", "dnnFormItem")

                Dim l As New DotNetNuke.UI.WebControls.PropertyLabelControl
                l.ID = tb.ID & "_Help"
                l.Caption = t.Name.Replace("_", " ") & " :"
                If t.Parameters.ContainsKey("Caption") Then
                    l.Caption = t.Parameters("Caption")
                End If
                If t.Parameters.ContainsKey("Help") Then
                    l.HelpText = "<div class=""Help"">" & t.Parameters("Help") & "</div>"
                End If
                If t.Parameters.ContainsKey("Default") Then
                    tb.Text = t.Parameters("Default")
                End If
                tb.Columns = 40
                If t.Parameters.ContainsKey("Columns") Then
                    Int32.TryParse(t.Parameters("Columns"), tb.Columns)
                End If
                l.EditControl = tb
                d.Controls.Add(l)

                ' add textbox
                d.Controls.Add(tb)

                pnlToken.Controls.Add(d)

            Next
            Session("Tokens") = _tokenList

        End Sub


        Private Sub RetrieveTokenValuesFromControls()
            ' set tokenList values from token controls
            Dim ctrl As TextBox
            For Each key As String In _tokenList.Keys

                Dim t As DNNStuff.Utilities.RegularExpression.Token = _tokenList(key)

                ctrl = DirectCast(pnlToken.FindControl("Token_" & t.Name), TextBox)
                If Not ctrl Is Nothing Then
                    t.Value = ctrl.Text
                End If

            Next

        End Sub
#End Region

        Private Sub SourceChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSource.SelectedIndexChanged, ddlStandard.SelectedIndexChanged
            ' default link file to true
            If ddlSource.SelectedValue = "File" Then chkLinkFile.Checked = True

            _tokenList = InitTokenListFromCurrentSettings(ddlSource.SelectedValue, ddlStandard.SelectedValue, ctlfile.Url, txtText.Text)
            RenderTokensAsControls()
            SetVisibilityBasedOnSource()
        End Sub

    End Class

End Namespace
