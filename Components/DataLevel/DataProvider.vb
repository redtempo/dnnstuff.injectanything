Imports System
Imports DotNetNuke

Namespace DNNStuff.InjectAnything

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' An abstract class for the data access layer
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public MustInherit Class DataProvider

#Region "Shared/Static Methods"

        ' singleton reference to the instantiated object 
        Private Shared objProvider As DataProvider = Nothing

        ' constructor
        Shared Sub New()
            CreateProvider()
        End Sub

        ' dynamically create provider
        Private Shared Sub CreateProvider()
            objProvider = CType(Framework.Reflection.CreateObject("data", "DNNStuff.InjectAnything", ""), DataProvider)
        End Sub

        ' return the provider
        Public Shared Shadows Function Instance() As DataProvider
            Return objProvider
        End Function

#End Region

#Region "Injection Abstract Methods"

        Public MustOverride Function GetInjection(ByVal ModuleId As Integer) As IDataReader
        Public MustOverride Function UpdateInjection(ByVal InjectionId As Integer, ByVal ModuleId As Integer, ByVal FreeFormText As String) As IDataReader
        Public MustOverride Sub DeleteInjection(ByVal InjectionId As Integer)

#End Region

    End Class

End Namespace