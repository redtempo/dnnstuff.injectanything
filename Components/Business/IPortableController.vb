'***************************************************************************/
'* Copyright (c) 2007 by DNNStuff.
'* All rights reserved.
'*
'* Date:        March 19,2007
'* Author:      Richard Edwards
'* Description: IPortable Support
'*************/

Imports System
Imports System.Configuration
Imports System.Data
Imports System.XML
Imports System.Web
Imports System.Collections.Generic

Imports DotNetNuke
Imports DotNetNuke.Common
Imports DotNetNuke.Common.Utilities.XmlUtils
Imports DotNetNuke.Common.Utilities
Imports DotNetNuke.Services.Search

Namespace DNNStuff.InjectAnything

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' The Controller class for InjectAnything
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Class IPortableController
        Implements Entities.Modules.IPortable

#Region "Optional Interfaces"

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' ExportModule implements the IPortable ExportModule Interface
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <param name="ModuleID">The Id of the module to be exported</param>
        ''' <history>
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public Function ExportModule(ByVal ModuleID As Integer) As String Implements DotNetNuke.Entities.Modules.IPortable.ExportModule
            Dim strXML As New Text.StringBuilder()
            Dim xmlsettings As New XmlWriterSettings()
            xmlsettings.Indent = True
            xmlsettings.OmitXmlDeclaration = True

            Dim Writer As XmlWriter = XmlWriter.Create(strXML, xmlsettings)
            Writer.WriteStartElement("InjectAnything")

            Dim ms As New ModuleSettings(ModuleID)
            Writer.WriteElementString("InsertionPoint", ms.InsertionPoint)
            Writer.WriteElementString("Source", ms.Source)
            Writer.WriteElementString("File", ms.File)
            Writer.WriteElementString("StandardFile", ms.StandardFile)
            Writer.WriteElementString("LinkFile", ms.LinkFile.ToString)
            Writer.WriteElementString("Text", ms.Text)
            Writer.WriteElementString("Debug", ms.Debug.ToString)
            Writer.WriteElementString("Enable", ms.Enable.ToString)

            ' tokens
            Dim ctrl As New DotNetNuke.Entities.Modules.ModuleController
            Dim settings As Hashtable = ctrl.GetModuleSettings(ModuleID)
            For Each key As String In settings.Keys
                If key.StartsWith("Token_") Then
                    Writer.WriteElementString(key, settings(key).ToString)
                End If
            Next

            Writer.WriteEndElement()

            Writer.Close()

            Return strXML.ToString()
        End Function
        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' ImportModule implements the IPortable ImportModule Interface
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <param name="ModuleID">The Id of the module to be imported</param>
        ''' <param name="Content">The content to be imported</param>
        ''' <param name="Version">The version of the module to be imported</param>
        ''' <param name="UserId">The Id of the user performing the import</param>
        ''' <history>
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public Sub ImportModule(ByVal ModuleID As Integer, ByVal Content As String, ByVal Version As String, ByVal UserId As Integer) Implements DotNetNuke.Entities.Modules.IPortable.ImportModule

            Dim xmlImport As XmlNode = GetContent(Content, "InjectAnything")

            Dim ms As New ModuleSettings(ModuleID)
            With ms
                .InsertionPoint = xmlImport.SelectSingleNode("InsertionPoint").InnerText
                .Source = xmlImport.SelectSingleNode("Source").InnerText
                .File = xmlImport.SelectSingleNode("File").InnerText
                .StandardFile = xmlImport.SelectSingleNode("StandardFile").InnerText
                .LinkFile = Convert.ToBoolean(xmlImport.SelectSingleNode("LinkFile").InnerText)
                .Text = xmlImport.SelectSingleNode("Text").InnerText
                .Debug = Convert.ToBoolean(xmlImport.SelectSingleNode("Debug").InnerText)
                .Enable = Convert.ToBoolean(xmlImport.SelectSingleNode("Enable").InnerText)
            End With
            ms.UpdateSettings()

            ' tokens
            Dim ctrl As New DotNetNuke.Entities.Modules.ModuleController
            For Each n As XmlNode In xmlImport.ChildNodes
                If n.Name.StartsWith("Token_") Then
                    ctrl.UpdateModuleSetting(ModuleID, n.Name, n.InnerText)
                End If
            Next
        End Sub

#End Region

    End Class
End Namespace
