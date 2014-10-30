'* ClassesInfo.vb
'*
'* Copyright (c) 2007 by DNNStuff.
'* All rights reserved.
'*
'* Date:        March 19,2007
'* Author:      Richard Edwards
'* Description: Data classes
'*************/

Imports System
Imports System.Configuration
Imports System.Data

Namespace DNNStuff.InjectAnything

    Public Class InjectionInfo

#Region "Private Members"
        Private _InjectionId As Integer
        Private _ModuleId As Integer
        Private _FreeFormText As String
#End Region

#Region "Constructors"
        Public Sub New()
        End Sub
#End Region

#Region "Public Properties"
        Public Property InjectionId() As Integer
            Get
                Return _InjectionId
            End Get
            Set(ByVal value As Integer)
                _InjectionId = value
            End Set
        End Property

        Public Property ModuleId() As Integer
            Get
                Return _ModuleId
            End Get
            Set(ByVal value As Integer)
                _ModuleId = value
            End Set
        End Property

        Public Property FreeFormText() As String
            Get
                Return _FreeFormText
            End Get
            Set(ByVal Value As String)
                _FreeFormText = Value
            End Set
        End Property
#End Region

    End Class

End Namespace
