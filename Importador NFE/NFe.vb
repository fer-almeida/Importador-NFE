Imports System.Xml
Imports System.Xml.XPath

Public Class NFe
    Dim _XML As New XmlDocument
    Dim DocNode As XmlNode

    Dim _ArquivoXML As String
    Dim _Id As String
    Dim _Versao As String

    Dim _Identificacao_UF As String 'cUF
    'cNF
    Dim _Identificacao_NaturezaOperacao As String 'NatOp

    Dim _Identificacao_Numero As String 'nNF
    

    Public Sub LerNFe()
        If (_ArquivoXML <> "") Then
            _XML.Load(_ArquivoXML)
        Else
            MsgBox("Propriedade ArquivoXML não configurada", MsgBoxStyle.Critical, "Erro")
            Return
        End If

        Dim manager As XmlNamespaceManager = New XmlNamespaceManager(_XML.NameTable)
        manager.AddNamespace("nf", "http://www.portalfiscal.inf.br/nfe")

        DocNode = _XML.SelectSingleNode("nf:nfeProc/nf:NFe/nf:infNFe", manager)
        _Id = DocNode.Attributes("Id").Value.ToString
        _Versao = DocNode.Attributes("versao").Value.ToString

        DocNode = _XML.SelectSingleNode("nf:nfeProc/nf:NFe/nf:infNFe/nf:ide", manager)
        _Identificacao_UF = DocNode.Item("cUF").InnerText
        _Identificacao_Numero = DocNode.Item("cNF").InnerText
        _Identificacao_NaturezaOperacao = DocNode.Item("natOp").InnerText

    End Sub

    Public Property ArquivoXML As String
        Get
            Return _ArquivoXML
        End Get
        Set(value As String)
            _ArquivoXML = value
        End Set
    End Property

    Public ReadOnly Property Id As String
        Get
            Return _Id
        End Get
    End Property
    Public ReadOnly Property Versao As String
        Get
            Return _Versao
        End Get
    End Property

    Public ReadOnly Property Identificacao_UF As String
        Get
            Return _Identificacao_UF
        End Get
    End Property

    Public ReadOnly Property Identificacao_Numero As String
        Get
            Return _Identificacao_Numero
        End Get
    End Property

    Public ReadOnly Property Identificacao_NaturezaOperacao As String
        Get
            Return _Identificacao_NaturezaOperacao
        End Get
    End Property
End Class
