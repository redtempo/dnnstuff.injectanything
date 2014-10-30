Imports System.Xml

<Serialization.XmlRoot("script")> _
Public Class StandardScript
    Private _description As String = ""
    Private _help As String = ""
    Private _items As Collections.Generic.List(Of StandardScriptItem)

    <Serialization.XmlElement("description")> _
    Public Property Description() As String
        Get
            Return _description
        End Get
        Set(ByVal value As String)
            _description = value
        End Set
    End Property

    <Serialization.XmlElement("help")> _
    Public Property Help() As String
        Get
            Return _help
        End Get
        Set(ByVal value As String)
            _help = value
        End Set
    End Property

    <Serialization.XmlArray("items"), _
    Serialization.XmlArrayItem("item", GetType(StandardScriptItem))> _
    Public Property Items() As Collections.Generic.List(Of StandardScriptItem)
        Get
            Return _items
        End Get
        Set(ByVal value As Collections.Generic.List(Of StandardScriptItem))
            _items = value
        End Set
    End Property
End Class

<Serialization.XmlRoot("item")> _
Public Class StandardScriptItem
    Private _content As String = ""

    <Serialization.XmlElement("content")> _
    Public Property Content() As String
        Get
            Return _content
        End Get
        Set(ByVal value As String)
            _content = value
        End Set
    End Property
End Class