Imports DotNetNuke
Imports DNNStuff.Utilities.RegularExpression

Namespace DNNStuff.InjectAnything

    Public Class ModuleSettings
        Private _ModuleId As Integer = 0
        Private _PortalSettings As DotNetNuke.Entities.Portals.PortalSettings = Nothing
        Private _Ctl As Control = Nothing

#Region " Properties"

        Private _InsertionPoint As String
        Private Const SETTING_INSERTIONPOINT As String = "InsertionPoint"
        Public Property InsertionPoint() As String
            Get
                Return _InsertionPoint
            End Get
            Set(ByVal value As String)
                _InsertionPoint = value
            End Set
        End Property

        Private _Source As String = "Standard"
        Private Const SETTING_SOURCE As String = "Source"
        Public Property Source() As String
            Get
                Return _Source
            End Get
            Set(ByVal value As String)
                _Source = value
            End Set
        End Property

        Private _File As String = ""
        Private Const SETTING_FILE As String = "File"
        Public Property File() As String
            Get
                Return _File
            End Get
            Set(ByVal Value As String)
                _File = Value
            End Set
        End Property

        Private _StandardFile As String = ""
        Private Const SETTING_STANDARDFILE As String = "StandardFile"
        Public Property StandardFile() As String
            Get
                Return _StandardFile
            End Get
            Set(ByVal Value As String)
                _StandardFile = Value
            End Set
        End Property

        Private _LinkFile As Boolean = True
        Private Const SETTING_LINKFILE As String = "LinkFile"
        Public Property LinkFile() As Boolean
            Get
                Return _LinkFile
            End Get
            Set(ByVal value As Boolean)
                _LinkFile = value
            End Set
        End Property

        '        Private Const SETTING_TEXT As String = "Text"
        Private _Text As String = ""
        Public Property Text() As String
            Get
                Return _Text
            End Get
            Set(ByVal Value As String)
                _Text = Value
            End Set
        End Property

        Private Const SETTING_DEBUG As String = "Debug"
        Private _Debug As Boolean = False
        Public Property Debug() As Boolean
            Get
                Return _Debug
            End Get
            Set(ByVal Value As Boolean)
                _Debug = Value
            End Set
        End Property

        Private Const SETTING_ENABLE As String = "Enable"
        Private _Enable As Boolean = False
        Public Property Enable() As Boolean
            Get
                Return _Enable
            End Get
            Set(ByVal Value As Boolean)
                _Enable = Value
            End Set
        End Property
#End Region
#Region " Helpers"
        Public ReadOnly Property FullFilePath() As String
            Get
                Dim filename As String = ""
                Select Case _Source
                    Case "Standard"
                        filename = HttpContext.Current.Server.MapPath(_Ctl.ResolveUrl("Standard/" & _StandardFile))
                    Case "File"
                        filename = _PortalSettings.HomeDirectoryMapPath & _File
                End Select

                Return filename
            End Get
        End Property
#End Region
#Region "Methods"
        Public Sub New(ByVal moduleId As Integer, ByVal portalsettings As DotNetNuke.Entities.Portals.PortalSettings, ByVal ctl As Control)
            _ModuleId = moduleId
            _PortalSettings = portalsettings
            _Ctl = ctl
            LoadSettings()
        End Sub
        Public Sub New(ByVal moduleId As Integer)
            _ModuleId = moduleId
            LoadSettings()
        End Sub

        Private Sub LoadSettings()
            Dim ctrl As New DotNetNuke.Entities.Modules.ModuleController
            Dim settings As Hashtable = ctrl.GetModuleSettings(_ModuleId)

            _InsertionPoint = DNNUtilities.GetSetting(settings, SETTING_INSERTIONPOINT, "")
            _Source = DNNUtilities.GetSetting(settings, SETTING_SOURCE, "")
            _File = DNNUtilities.GetSetting(settings, SETTING_FILE, "")
            _StandardFile = DNNUtilities.GetSetting(settings, SETTING_STANDARDFILE, "")
            _LinkFile = Convert.ToBoolean(DNNUtilities.GetSetting(settings, SETTING_LINKFILE, "False"))
            _Debug = Convert.ToBoolean(DNNUtilities.GetSetting(settings, SETTING_DEBUG, "False"))
            _Enable = Convert.ToBoolean(DNNUtilities.GetSetting(settings, SETTING_ENABLE, "True"))

            '            _Text = DNNUtilities.GetSetting(settings, SETTING_TEXT, "")

            ' using controller for text
            Dim controller As New InjectionController
            Dim obj As InjectionInfo = controller.GetInjection(_ModuleId)
            If obj IsNot Nothing Then
                _Text = obj.FreeFormText
            End If
            controller = Nothing

        End Sub

        Public Function LoadSettingsTokens(ByVal _tokenList As System.Collections.Generic.Dictionary(Of String, Token)) As System.Collections.Generic.Dictionary(Of String, Token)
            Dim ctrl As New DotNetNuke.Entities.Modules.ModuleController
            Dim settings As Hashtable = ctrl.GetModuleSettings(_ModuleId)
            For Each key As String In _tokenList.Keys
                Dim t As Token = _tokenList(key)
                t.Value = DNNUtilities.GetSetting(settings, "Token_" & key, "")
            Next
            Return _tokenList
        End Function

        Public Sub UpdateSettings()
            Dim ctrl As New DotNetNuke.Entities.Modules.ModuleController
            With ctrl
                .UpdateModuleSetting(_ModuleId, SETTING_INSERTIONPOINT, _InsertionPoint)
                .UpdateModuleSetting(_ModuleId, SETTING_SOURCE, _Source)
                .UpdateModuleSetting(_ModuleId, SETTING_FILE, _File)
                .UpdateModuleSetting(_ModuleId, SETTING_STANDARDFILE, _StandardFile)
                .UpdateModuleSetting(_ModuleId, SETTING_LINKFILE, _LinkFile.ToString)
                '                .UpdateModuleSetting(_ModuleId, SETTING_TEXT, _Text)
                .UpdateModuleSetting(_ModuleId, SETTING_DEBUG, _Debug.ToString)
                .UpdateModuleSetting(_ModuleId, SETTING_ENABLE, _Enable.ToString)
            End With

            ' using controller for text
            Dim controller As New InjectionController
            Dim obj As New InjectionInfo
            With obj
                .FreeFormText = _Text
                .ModuleId = _ModuleId
            End With
            controller.UpdateInjection(obj)
            controller = Nothing
        End Sub

        Public Sub UpdateSettingsTokens(ByVal _tokenList As System.Collections.Generic.Dictionary(Of String, Token))
            Dim ctrl As New DotNetNuke.Entities.Modules.ModuleController
            For Each key As String In _tokenList.Keys
                Dim t As Token = _tokenList(key)
                ctrl.UpdateModuleSetting(_ModuleId, "Token_" & key, t.Value)
            Next
        End Sub
#End Region

    End Class
End Namespace
