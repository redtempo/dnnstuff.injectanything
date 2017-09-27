Imports DotNetNuke.Common.Utilities

Namespace DNNStuff.InjectAnything

    Public Class InjectionController

#Region "Injection Public Methods"

        Public Function GetInjection(ByVal InjectionId As Integer) As InjectionInfo
            Return CType(CBO.FillObject(DataProvider.Instance().GetInjection(InjectionId), GetType(InjectionInfo)), InjectionInfo)
        End Function

        Public Sub UpdateInjection(ByVal objInfo As InjectionInfo)
            DataProvider.Instance().UpdateInjection(objInfo.InjectionId, objInfo.ModuleId, objInfo.FreeFormText)
        End Sub

        Public Sub DeleteInjection(ByVal InjectionId As Integer)
            DataProvider.Instance().DeleteInjection(InjectionId)
        End Sub

#End Region

    End Class

End Namespace
