Imports System.Xml
Imports System.Xml.XPath
Imports System.IO

Module NFE_Util
    Public Function NFeValida(ArquivoNFe As String) As Boolean
        Dim XML As New XmlDocument
        Dim DocNode As XmlNode

        Try
            XML.Load(ArquivoNFe)
        Catch ex As Exception
            Return False
        End Try

        Dim manager As XmlNamespaceManager = New XmlNamespaceManager(XML.NameTable)
        manager.AddNamespace("nf", "http://www.portalfiscal.inf.br/nfe")

        DocNode = XML.SelectSingleNode("nf:nfeProc/nf:NFe/nf:infNFe", manager)
        If DocNode Is Nothing Then DocNode = XML.SelectSingleNode("nf:enviNFe/nf:NFe/nf:infNFe", manager)

        If DocNode IsNot Nothing Then Return True Else Return False
    End Function
    'Public Function LerNFes(PastaNFe As String) As DataTable
    '    Dim Index As Integer = 0

    '    Dim ds As DataSet = New DataSet("NFes")
    '    Dim dt As DataTable = ds.Tables.Add("Dados")

    '    dt.Columns.Add("Index", Type.GetType("System.Int32"))
    '    CriarColunas(dt)

    '    Dim dr As DataRow

    '    Dim strFileSize As String = ""
    '    Dim di As New IO.DirectoryInfo(PastaNFe)
    '    Dim aryFi As IO.FileInfo() = di.GetFiles("*.xml")
    '    Dim fi As IO.FileInfo

    '    For Each fi In aryFi
    '        Dim nf As DataTable = LerNFe(fi.FullName)
    '        For Each item In nf.Rows
    '            Index = Index + 1
    '            dr = ds.Tables("Dados").NewRow

    '            dr("Index") = Index

    '            For i As Integer = 0 To (nf.Columns.Count - 1)
    '                dr(i + 1) = item(i)
    '            Next

    '            ds.Tables("Dados").Rows.Add(dr)
    '        Next

    '    Next

    '    Return dt
    'End Function
    Public Function LerNFe(ArquivoNFe As String) As DataTable
        'Criar um novo dataset temporário
        Dim ds As DataSet = New DataSet("NFe")

        'Criar um novo datatable para infNFe
        Dim dt As DataTable = ds.Tables.Add("Dados")

        'Criar colunas
        CriarColunas(dt)

        Dim Versao, Id, CodigoUFEmitente, UFEmitente, ChaveAcesso, DigitoVerificador, NaturezaOperacao, FormaPagamento, ModeloDocumentoFiscal, Serie, Numero, DataEmissao, TipoOperacao, CodigoCidadeFatoGerador, TipoEmissao, Finalidade As String
        Dim CNPJEmitente, NomeEmitente, EndLogradouroEmitente, EndNumeroEmitente, EndBairroEmitente, EndCodigoCidadeEmitente, EndCidadeEmitente, EndUFEmitente, EndCEPEmitente, EndCodigoPaisEmitente, EndPaisEmitente, EndTelefoneEmitente, IEEmitente, CodigoRegimeTributarioEmitente As String
        Dim CNPJDestinatario, NomeDestinatario, EndLogradouroDestinatario, EndNumeroDestinatario, EndBairroDestinatario, EndCodigoCidadeDestinatario, EndCidadeDestinatario, EndUFDestinatario, EndCEPDestinatario, EndCodigoPaisDestinatario, EndPaisDestinatario, EndTelefoneDestinatario, IEDestinatario As String
        Dim CodigoProduto = "", DescricaoProduto = "", NCMProduto = "", CFOPProduto = "", UnidadeComercialProduto, QuantidadeComercialProduto As String

        Dim XML As New XmlDocument
        Dim DocNode As XmlNode

        If Not NFeValida(ArquivoNFe) Then
            MsgBox("NF invalida: " + ArquivoNFe)
            Return dt
        End If

        XML.Load(ArquivoNFe)

        Dim manager As XmlNamespaceManager = New XmlNamespaceManager(XML.NameTable)
        manager.AddNamespace("nf", "http://www.portalfiscal.inf.br/nfe")

        Dim nfeNode As String = "nf:nfeProc/nf:NFe/nf:infNFe"
        If XML.SelectSingleNode(nfeNode, manager) Is Nothing Then nfeNode = "nf:enviNFe/nf:NFe/nf:infNFe"

        'Ler dados
        Dim dr As DataRow

        DocNode = XML.SelectSingleNode(nfeNode, manager)

        Id = DocNode.Attributes("Id").Value.ToString
        Versao = DocNode.Attributes("versao").Value.ToString

        DocNode = XML.SelectSingleNode(nfeNode + "/nf:ide", manager)
        CodigoUFEmitente = DocNode.Item("cUF").InnerText
        UFEmitente = UFPorCodigo(DocNode.Item("cUF").InnerText)
        ChaveAcesso = DocNode.Item("cNF").InnerText
        DigitoVerificador = DocNode.Item("cDV").InnerText
        NaturezaOperacao = DocNode.Item("natOp").InnerText

        FormaPagamento = ""
        Select Case DocNode.Item("indPag").InnerText
            Case "0"
                FormaPagamento = "0" '"Pagamento à Vista"
            Case "1"
                FormaPagamento = "1" '"Pagamento à Prazo"
            Case "2"
                FormaPagamento = "2" '"Outros"
        End Select

        ModeloDocumentoFiscal = DocNode.Item("mod").InnerText
        Serie = DocNode.Item("serie").InnerText
        Numero = DocNode.Item("nNF").InnerText

        If DocNode.Item("dEmi") Is Nothing Then
            DataEmissao = DocNode.Item("dhEmi").InnerText.Substring(8, 2) + "/" + DocNode.Item("dhEmi").InnerText.Substring(5, 2) + "/" + DocNode.Item("dhEmi").InnerText.Substring(0, 4)
        Else
            DataEmissao = DocNode.Item("dEmi").InnerText.Substring(8, 2) + "/" + DocNode.Item("dEmi").InnerText.Substring(5, 2) + "/" + DocNode.Item("dEmi").InnerText.Substring(0, 4)
        End If

        Select Case DocNode.Item("tpNF").InnerText
            Case "0"
                TipoOperacao = "0" '"Entrada"
            Case "1"
                TipoOperacao = "1" '"Saida"
        End Select

        CodigoCidadeFatoGerador = DocNode.Item("cMunFG").InnerText

        Select Case DocNode.Item("tpEmis").InnerText
            Case "1"
                TipoEmissao = "1" '"Normal"
            Case "2"
                TipoEmissao = "2" 'Contingência FS"
            Case "3"
                TipoEmissao = "3" '"Contingência SCAN"
            Case "4"
                TipoEmissao = "4" '"Contingência DPEC"
            Case "5"
                TipoEmissao = "5" '"Contingência FS-DA"
        End Select

        Finalidade = DocNode.Item("finNFe").InnerText

        DocNode = XML.SelectSingleNode(nfeNode + "/nf:emit", manager)
        CNPJEmitente = FormatarCNPJ(DocNode.Item("CNPJ").InnerText)
        NomeEmitente = DocNode.Item("xNome").InnerText
        IEEmitente = DocNode.Item("IE").InnerText

        Select Case DocNode.Item("CRT").InnerText
            Case "1"
                CodigoRegimeTributarioEmitente = "1 - Simples Nacional"
            Case "2"
                CodigoRegimeTributarioEmitente = "2 - Simples Nacional - Excesso de Sublimite de Receita Bruta"
            Case "3"
                CodigoRegimeTributarioEmitente = "3 - Regime Normal"
        End Select

        DocNode = XML.SelectSingleNode(nfeNode + "/nf:emit/nf:enderEmit", manager)
        If DocNode.Item("xLgr") IsNot Nothing Then EndLogradouroEmitente = DocNode.Item("xLgr").InnerText Else EndLogradouroEmitente = "---"
        If DocNode.Item("nro") IsNot Nothing Then EndNumeroEmitente = DocNode.Item("nro").InnerText Else EndNumeroEmitente = "---"
        If DocNode.Item("xBairro") IsNot Nothing Then EndBairroEmitente = DocNode.Item("xBairro").InnerText Else EndBairroEmitente = "---"
        If DocNode.Item("cMun") IsNot Nothing Then EndCodigoCidadeEmitente = DocNode.Item("cMun").InnerText Else EndCodigoCidadeEmitente = "---"
        If DocNode.Item("xMun") IsNot Nothing Then EndCidadeEmitente = DocNode.Item("xMun").InnerText Else EndCidadeEmitente = "---"
        If DocNode.Item("UF") IsNot Nothing Then EndCidadeEmitente = DocNode.Item("UF").InnerText Else EndCidadeEmitente = "---"
        If DocNode.Item("CEP") IsNot Nothing Then EndCEPEmitente = DocNode.Item("CEP").InnerText Else EndCEPEmitente = "---"
        If DocNode.Item("cPais") IsNot Nothing Then EndCodigoPaisEmitente = DocNode.Item("cPais").InnerText Else EndCodigoPaisEmitente = "---"
        If DocNode.Item("xPais") IsNot Nothing Then EndPaisEmitente = DocNode.Item("xPais").InnerText Else EndPaisEmitente = "---"
        If DocNode.Item("fone") IsNot Nothing Then EndTelefoneEmitente = DocNode.Item("fone").InnerText Else EndTelefoneEmitente = "---"

        DocNode = XML.SelectSingleNode(nfeNode + "/nf:dest", manager)
        CNPJDestinatario = DocNode.Item("CNPJ").InnerText
        NomeDestinatario = DocNode.Item("xNome").InnerText
        If DocNode.Item("IE") IsNot Nothing Then IEDestinatario = DocNode.Item("IE").InnerText Else IEDestinatario = "Isento"

        DocNode = XML.SelectSingleNode(nfeNode + "/nf:dest/nf:enderDest", manager)
        EndLogradouroDestinatario = DocNode.Item("xLgr").InnerText
        EndNumeroDestinatario = DocNode.Item("nro").InnerText
        EndBairroDestinatario = DocNode.Item("xBairro").InnerText
        EndCodigoCidadeDestinatario = DocNode.Item("cMun").InnerText
        EndCidadeDestinatario = DocNode.Item("xMun").InnerText
        EndUFDestinatario = DocNode.Item("UF").InnerText
        EndCEPDestinatario = DocNode.Item("CEP").InnerText
        EndCodigoPaisDestinatario = DocNode.Item("cPais").InnerText
        EndPaisDestinatario = DocNode.Item("xPais").InnerText
        If DocNode.Item("fone") IsNot Nothing Then EndTelefoneDestinatario = DocNode.Item("fone").InnerText Else EndTelefoneDestinatario = "---"

        Dim nodes As XmlNodeList = XML.SelectNodes(nfeNode + "/nf:det", manager)
        Dim Index As Integer = 0

        For Each node As XmlNode In nodes
            Index = Index + 1

            dr = ds.Tables("Dados").NewRow
            dr("IndexProduto") = Index.ToString
            dr("Id") = Id
            dr("Versao") = Versao
            dr("CodigoUFEmitente") = CodigoUFEmitente
            dr("UFEmitente") = UFEmitente
            dr("ChaveAcesso") = ChaveAcesso
            dr("DigitoVerificador") = DigitoVerificador
            dr("NaturezaOperacao") = NaturezaOperacao
            dr("FormaPagamento") = FormaPagamento
            dr("ModeloDocumentoFiscal") = ModeloDocumentoFiscal
            dr("Serie") = Serie
            dr("Numero") = Numero
            dr("DataEmissao") = DataEmissao
            dr("TipoOperacao") = TipoOperacao
            dr("CodigoCidadeFatoGerador") = CodigoCidadeFatoGerador
            dr("TipoEmissao") = TipoEmissao
            dr("Finalidade") = Finalidade
            dr("CNPJEmitente") = CNPJEmitente
            dr("NomeEmitente") = NomeEmitente
            dr("IEEmitente") = IEEmitente
            dr("CodigoRegimeTributarioEmitente") = CodigoRegimeTributarioEmitente
            dr("EndLogradouroEmitente") = EndLogradouroEmitente
            dr("EndNumeroEmitente") = EndNumeroEmitente
            dr("EndBairroEmitente") = EndBairroEmitente
            dr("EndCodigoCidadeEmitente") = EndCodigoCidadeEmitente
            dr("EndCidadeEmitente") = EndCidadeEmitente
            dr("EndUFEmitente") = EndUFEmitente
            dr("EndCEPEmitente") = EndCEPEmitente
            dr("EndCodigoPaisEmitente") = EndCodigoPaisEmitente
            dr("EndPaisEmitente") = EndPaisEmitente
            dr("EndTelefoneEmitente") = EndTelefoneEmitente
            dr("CNPJDestinatario") = CNPJDestinatario
            dr("NomeDestinatario") = NomeDestinatario
            dr("IEDestinatario") = IEDestinatario
            dr("EndLogradouroDestinatario") = EndLogradouroDestinatario
            dr("EndNumeroDestinatario") = EndNumeroDestinatario
            dr("EndBairroDestinatario") = EndBairroDestinatario
            dr("EndCodigoCidadeDestinatario") = EndCodigoCidadeDestinatario
            dr("EndCidadeDestinatario") = EndCidadeDestinatario
            dr("EndUFDestinatario") = EndUFDestinatario
            dr("EndCEPDestinatario") = EndCEPDestinatario
            dr("EndCodigoPaisDestinatario") = EndCodigoPaisDestinatario
            dr("EndPaisDestinatario") = EndPaisDestinatario
            dr("EndTelefoneDestinatario") = EndTelefoneDestinatario

            ' Produtos
            DocNode = XML.SelectSingleNode(nfeNode + "/nf:det[" + node.Attributes("nItem").Value.ToString + "]/nf:prod", manager)
            dr("CodigoProduto") = DocNode.Item("cProd").InnerText
            dr("DescricaoProduto") = DocNode.Item("xProd").InnerText
            dr("NCMProduto") = DocNode.Item("NCM").InnerText
            dr("CFOPProduto") = DocNode.Item("CFOP").InnerText
            UnidadeComercialProduto = DocNode.Item("uCom").InnerText
            dr("UnidadeComercialProduto") = UnidadeComercialProduto

            QuantidadeComercialProduto = DocNode.Item("qCom").InnerText.Replace(".", ",")

            dr("QuantidadeComercialProduto") = QuantidadeComercialProduto
            dr("ValorUnitarioProduto") = DocNode.Item("vUnCom").InnerText.Replace(".", ",")
            Dim ValorProdutoMult As String = (CInt(QuantidadeComercialProduto) * CDbl(DocNode.Item("vUnCom").InnerText.Replace(".", ","))).ToString.Replace(".", ",")
            dr("TOTAIS_ValorProdutos") = ValorProdutoMult

            'Impostos

            'ICMS
            Dim ICMSTipo() As String = {"00", "10", "20", "30", "40", "51", "60", "70", "90", "Part", "ST", "SN101", "SN102", "SN201", "SN500", "SN900"}

            For Each tipo In ICMSTipo
                DocNode = XML.SelectSingleNode(nfeNode + "/nf:det[" + node.Attributes("nItem").Value.ToString + "]/nf:imposto/nf:ICMS/nf:ICMS" + tipo, manager)
                If DocNode IsNot Nothing Then

                    'ICMS_OrigemProduto             orig
                    'ICMS_TributacaoProduto         CST
                    'ICMS_TributacaoDescricao       Descrição CST
                    'ICMS_ModalidadeBC              modBC
                    'ICMS_PercentualReducaoBC       pRedBC
                    'ICMS_ValorBC                   vBC
                    'ICMS_Aliquota                  pICMS
                    'ICMS_Valor                     vICMS
                    'ICMS_ModalidadeBCST            modBCST
                    'ICMS_PercentualMargemBCST      pMVAST
                    'ICMS_PercentualReducaoBCST     pRedBCST
                    'ICMS_ValorBCST                 vBCST
                    'ICMS_AliquotaICMSST            pICMSST
                    'ICMS_ValorICMSST               vICMSST

                    If DocNode.Item("orig") IsNot Nothing Then dr("ICMS_OrigemProduto") = DocNode.Item("orig").InnerText Else dr("ICMS_OrigemProduto") = "0"
                    If DocNode.Item("CST") IsNot Nothing Then dr("ICMS_TributacaoProduto") = DocNode.Item("CST").InnerText Else dr("ICMS_TributacaoProduto") = "0"
                    If DocNode.Item("CST") IsNot Nothing Then dr("ICMS_TributacaoDescricao") = TributoPorCodigo(DocNode.Item("CST").InnerText) Else dr("ICMS_TributacaoDescricao") = "Nenhum"
                    If DocNode.Item("modBC") IsNot Nothing Then dr("ICMS_ModalidadeBC") = DocNode.Item("modBC").InnerText Else dr("ICMS_ModalidadeBC") = "0"
                    If DocNode.Item("pRedBC") IsNot Nothing Then dr("ICMS_PercentualReducaoBC") = DocNode.Item("pRedBC").InnerText.Replace(".", ",") Else dr("ICMS_PercentualReducaoBC") = "0,00"
                    If DocNode.Item("vBC") IsNot Nothing Then dr("ICMS_ValorBC") = Convert.ToDouble(DocNode.Item("vBC").InnerText.Replace(".", ",")).ToString Else dr("ICMS_ValorBC") = "0,00"
                    If DocNode.Item("pICMS") IsNot Nothing Then dr("ICMS_Aliquota") = DocNode.Item("pICMS").InnerText.Replace(".", ",") Else dr("ICMS_Aliquota") = "0,00"
                    If DocNode.Item("vICMS") IsNot Nothing Then dr("ICMS_Valor") = Convert.ToDouble(DocNode.Item("vICMS").InnerText.Replace(".", ",")).ToString Else dr("ICMS_Valor") = "0,00"
                    If DocNode.Item("modBCST") IsNot Nothing Then dr("ICMS_ModalidadeBCST") = DocNode.Item("modBCST").InnerText Else dr("ICMS_ModalidadeBCST") = "0"
                    If DocNode.Item("pMVAST") IsNot Nothing Then dr("ICMS_PercentualMargemBCST") = DocNode.Item("pMVAST").InnerText.Replace(".", ",") Else dr("ICMS_PercentualMargemBCST") = "0,00"
                    If DocNode.Item("pRedBCST") IsNot Nothing Then dr("ICMS_PercentualReducaoBCST") = DocNode.Item("pRedBCST").InnerText.Replace(".", ",") Else dr("ICMS_PercentualReducaoBCST") = "0,00"
                    If DocNode.Item("vBCST") IsNot Nothing Then dr("ICMS_ValorBCST") = Convert.ToDouble(DocNode.Item("vBCST").InnerText.Replace(".", ",")).ToString Else dr("ICMS_ValorBCST") = "0,00"
                    If DocNode.Item("pICMSST") IsNot Nothing Then dr("ICMS_AliquotaICMSST") = DocNode.Item("pICMSST").InnerText.Replace(".", ",") Else dr("ICMS_AliquotaICMSST") = "0,00"
                    If DocNode.Item("vICMSST") IsNot Nothing Then dr("ICMS_ValorICMSST") = Convert.ToDouble(DocNode.Item("vICMS").InnerText.Replace(".", ",")).ToString Else dr("ICMS_ValorICMSST") = "0,00"
                    Exit For
                End If
            Next

            'IPI
            DocNode = XML.SelectSingleNode(nfeNode + "/nf:det[" + node.Attributes("nItem").Value.ToString + "]/nf:imposto/nf:IPI", manager)
            Dim vIPI As String ' = "0,00"

            Try
                dr("IPI_Enquadramento") = DocNode.Item("cEnq").InnerText

                Dim IPITipo() As String = {"Trib", "NT"}
                For Each tipo In IPITipo
                    DocNode = XML.SelectSingleNode(nfeNode + "/nf:det[" + node.Attributes("nItem").Value.ToString + "]/nf:imposto/nf:IPI/nf:IPI" + tipo, manager)

                    If DocNode IsNot Nothing Then

                        'IPI_Enquadramento                  clEnq
                        'IPI_CNPJProdutor                   CNPJProd
                        'IPI_SeloControle                   cSelo
                        'IPI_SeloQuantidade                 qSelo
                        'IPI_CodigoEnquadramento            cEnq
                        'IPI_CodigoSituacaoTributaria       CST
                        'IPI_ValorBC                        vBC
                        'IPI_Aliquota                       pIPI
                        'IPI_Valor                          vIPI
                        'IPI_Unidade                        qUnid
                        'IPI_ValorUnitario                  vUnid

                        If DocNode.Item("clEnq") IsNot Nothing Then dr("IPI_Enquadramento") = IPI_SituacaoPorCodigo(DocNode.Item("clEnq").InnerText) Else dr("IPI_Enquadramento") = "0"
                        If DocNode.Item("CNPJProd") IsNot Nothing Then dr("IPI_CNPJProdutor") = IPI_SituacaoPorCodigo(DocNode.Item("CNPJProd").InnerText) Else dr("IPI_CNPJProdutor") = "Nao informado"
                        If DocNode.Item("cSelo") IsNot Nothing Then dr("IPI_SeloControle") = IPI_SituacaoPorCodigo(DocNode.Item("cSelo").InnerText) Else dr("IPI_SeloControle") = "0"
                        If DocNode.Item("qSelo") IsNot Nothing Then dr("IPI_SeloQuantidade") = IPI_SituacaoPorCodigo(DocNode.Item("qSelo").InnerText) Else dr("IPI_SeloQuantidade") = "0"
                        If DocNode.Item("cEnq") IsNot Nothing Then dr("IPI_CodigoEnquadramento") = IPI_SituacaoPorCodigo(DocNode.Item("cEnq").InnerText) Else dr("IPI_CodigoEnquadramento") = "0"
                        If DocNode.Item("CST") IsNot Nothing Then dr("IPI_CodigoSituacaoTributaria") = DocNode.Item("CST").InnerText Else dr("IPI_CodigoSituacaoTributaria") = "0"
                        If DocNode.Item("CST") IsNot Nothing Then dr("IPI_CodigoSituacaoTributariaDescricao") = IPI_SituacaoPorCodigo(DocNode.Item("CST").InnerText) Else dr("IPI_CodigoSituacaoTributariaDescricao") = "Nenhum"
                        If DocNode.Item("vBC") IsNot Nothing Then dr("IPI_ValorBC") = Convert.ToDouble(DocNode.Item("vBC").InnerText.Replace(".", ",")).ToString Else dr("IPI_ValorBC") = "0,00"
                        If DocNode.Item("pIPI") IsNot Nothing Then dr("IPI_Aliquota") = DocNode.Item("pIPI").InnerText.Replace(".", ",") Else dr("IPI_Aliquota") = "0,00"

                        If DocNode.Item("vIPI") IsNot Nothing Then
                            vIPI = Convert.ToDouble(DocNode.Item("vIPI").InnerText.Replace(".", ",")).ToString
                        Else
                            vIPI = "0,00"
                        End If
                        dr("IPI_Valor") = vIPI

                        If DocNode.Item("qUnid") IsNot Nothing Then dr("IPI_Unidade") = DocNode.Item("qUnid").InnerText Else dr("IPI_Unidade") = "0"
                        If DocNode.Item("vUnid") IsNot Nothing Then dr("IPI_ValorUnitario") = Convert.ToDouble(DocNode.Item("vUnid").InnerText.Replace(".", ",")).ToString Else dr("IPI_ValorUnitario") = "0,00"
                        Exit For
                    End If
                Next
            Catch ex As Exception
                dr("IPI_Enquadramento") = "Nenhum"

                dr("IPI_CNPJProdutor") = "Nao informado"
                dr("IPI_SeloControle") = "0"
                dr("IPI_SeloQuantidade") = "0"
                dr("IPI_CodigoEnquadramento") = "0"
                dr("IPI_CodigoSituacaoTributaria") = "0"
                dr("IPI_CodigoSituacaoTributariaDescricao") = "Nenhum"
                dr("IPI_ValorBC") = "0,00"
                dr("IPI_Aliquota") = "0,00"
                dr("IPI_Valor") = "0,00"
                dr("IPI_Unidade") = "0"
                dr("IPI_ValorUnitario") = "0,00"
            End Try

            'II
            DocNode = XML.SelectSingleNode(nfeNode + "/nf:det[" + node.Attributes("nItem").Value.ToString + "]/nf:imposto/nf:II", manager)
            If DocNode IsNot Nothing Then

                'II_ValorBC                     vBC
                'II_ValorDespesasAduaneiras     vDespAdu
                'II_Valor                       vII
                'II_ValorIOF                    vIOF

                If DocNode.Item("vBC") IsNot Nothing Then dr("II_ValorBC") = Convert.ToDouble(DocNode.Item("vBC").InnerText.Replace(".", ",")).ToString Else dr("II_ValorBC") = "0,00"
                If DocNode.Item("vDespAdu") IsNot Nothing Then dr("II_ValorDespesasAduaneiras") = Convert.ToDouble(DocNode.Item("vDespAdu").InnerText.Replace(".", ",")).ToString Else dr("II_ValorDespesasAduaneiras") = "0,00"
                If DocNode.Item("vII") IsNot Nothing Then dr("II_Valor") = Convert.ToDouble(DocNode.Item("vII").InnerText.Replace(".", ",")).ToString Else dr("II_Valor") = "0,00"
                If DocNode.Item("vIOF") IsNot Nothing Then dr("II_ValorIOF") = Convert.ToDouble(DocNode.Item("vIOF").InnerText.Replace(".", ",")).ToString Else dr("II_ValorIOF") = "0,00"
            Else
                dr("II_ValorBC") = "0,00"
                dr("II_ValorDespesasAduaneiras") = "0,00"
                dr("II_Valor") = "0,00"
                dr("II_ValorIOF") = "0,00"
            End If

            'PIS
            Dim PISTipo() As String = {"Aliq", "Qtde", "NT", "ST", "Outr"}
            For Each tipo In PISTipo
                DocNode = XML.SelectSingleNode(nfeNode + "/nf:det[" + node.Attributes("nItem").Value.ToString + "]/nf:imposto/nf:PIS/nf:PIS" + tipo, manager)

                If DocNode IsNot Nothing Then

                    'PIS_CodigoSituacaoTributaria   CST
                    'PIS_ValorBC                    vBC
                    'PIS_Aliquota                   pPIS
                    'PIS_Valor                      vPIS
                    'PIS_QuantidadeVendida          qBCProd
                    'PIS_ValorAliquota              vAliqProd

                    If DocNode.Item("CST") IsNot Nothing Then dr("PIS_CodigoSituacaoTributaria") = DocNode.Item("CST").InnerText Else dr("PIS_CodigoSituacaoTributaria") = "0"
                    If DocNode.Item("CST") IsNot Nothing Then dr("PIS_CodigoSituacaoTributariaDescricao") = PIS_SituacaoPorCodigo(DocNode.Item("CST").InnerText) Else dr("PIS_CodigoSituacaoTributariaDescricao") = "Nenhum"
                    If DocNode.Item("vBC") IsNot Nothing Then dr("PIS_ValorBC") = Convert.ToDouble(DocNode.Item("vBC").InnerText.Replace(".", ",")).ToString Else dr("PIS_ValorBC") = "0,00"
                    If DocNode.Item("pPIS") IsNot Nothing Then dr("PIS_Aliquota") = DocNode.Item("pPIS").InnerText.Replace(".", ",") Else dr("PIS_Aliquota") = "0,00"
                    If DocNode.Item("vPIS") IsNot Nothing Then dr("PIS_Valor") = Convert.ToDouble(DocNode.Item("vPIS").InnerText.Replace(".", ",")).ToString Else dr("PIS_Valor") = "0,00"
                    If DocNode.Item("qBCProd") IsNot Nothing Then dr("PIS_QuantidadeVendida") = DocNode.Item("qBCProd").InnerText Else dr("PIS_QuantidadeVendida") = "0"
                    If DocNode.Item("vAliqProd") IsNot Nothing Then dr("PIS_ValorAliquota") = Convert.ToDouble(DocNode.Item("vAliqProd").InnerText.Replace(".", ",")).ToString Else dr("PIS_ValorAliquota") = "0,00"
                End If
            Next

            'COFINS
            Dim COFINSTipo() As String = {"Aliq", "Qtde", "NT", "ST", "Outr"}
            For Each tipo In COFINSTipo
                DocNode = XML.SelectSingleNode(nfeNode + "/nf:det[" + node.Attributes("nItem").Value.ToString + "]/nf:imposto/nf:COFINS/nf:COFINS" + tipo, manager)

                If DocNode IsNot Nothing Then

                    'COFINS_CodigoSituacaoTributaria            CST
                    'COFINS_ValorBC                             vBC
                    'COFINS_Aliquota                            pCOFINS
                    'COFINS_Valor                               vCOFINS
                    'COFINS_QuantidadeVendida                   qBCProd
                    'COFINS_ValorAliquota                       vAliqProd

                    If DocNode.Item("CST") IsNot Nothing Then dr("COFINS_CodigoSituacaoTributaria") = DocNode.Item("CST").InnerText Else dr("COFINS_CodigoSituacaoTributaria") = "0"
                    If DocNode.Item("CST") IsNot Nothing Then dr("COFINS_CodigoSituacaoTributariaDescricao") = COFINS_SituacaoPorCodigo(DocNode.Item("CST").InnerText) Else dr("COFINS_CodigoSituacaoTributariaDescricao") = "Nenhum"
                    If DocNode.Item("vBC") IsNot Nothing Then dr("COFINS_ValorBC") = Convert.ToDouble(DocNode.Item("vBC").InnerText.Replace(".", ",")).ToString Else dr("COFINS_ValorBC") = "0,00"
                    If DocNode.Item("pCOFINS") IsNot Nothing Then dr("COFINS_Aliquota") = DocNode.Item("pCOFINS").InnerText.Replace(".", ",") Else dr("COFINS_Aliquota") = "0,00"
                    If DocNode.Item("vCOFINS") IsNot Nothing Then dr("COFINS_Valor") = Convert.ToDouble(DocNode.Item("vCOFINS").InnerText.Replace(".", ",")).ToString Else dr("COFINS_Valor") = "0,00"
                    If DocNode.Item("qBCProd") IsNot Nothing Then dr("COFINS_QuantidadeVendida") = DocNode.Item("qBCProd").InnerText Else dr("COFINS_QuantidadeVendida") = "0"
                    If DocNode.Item("vAliqProd") IsNot Nothing Then dr("COFINS_ValorAliquota") = Convert.ToDouble(DocNode.Item("vAliqProd").InnerText.Replace(".", ",")).ToString Else dr("COFINS_ValorAliquota") = "0,00"
                End If
            Next

            'Totais
            DocNode = XML.SelectSingleNode(nfeNode + "/nf:total/nf:ICMSTot", manager)

            If DocNode IsNot Nothing Then

                'TOTAIS_ValorBC         vBC
                'TOTAIS_ValorICMS       vICMS
                'TOTAIS_ValorBCST       vBCST
                'TOTAIS_ValorST         vST
                'TOTAIS_ValorProdutos   vProd
                'TOTAIS_ValorFrete      vFrete
                'TOTAIS_ValorSeguro     vSeg
                'TOTAIS_ValorDescontos  vDesc
                'TOTAIS_ValorII         vII
                'TOTAIS_ValorIPI        vIPI
                'TOTAIS_ValorPIS        vPIS
                'TOTAIS_ValorCOFINS     vCOFINS
                'TOTAIS_ValorOutros     vOutro
                'TOTAIS_ValorNF         vNF
                'TOTAIS_ValorProEIPI    vProd+vIPI

                If DocNode.Item("vBC") IsNot Nothing Then dr("TOTAIS_ValorBC") = Convert.ToDouble(DocNode.Item("vBC").InnerText.Replace(".", ",")).ToString Else dr("TOTAIS_ValorBC") = "0.00"
                If DocNode.Item("vICMS") IsNot Nothing Then dr("TOTAIS_ValorICMS") = Convert.ToDouble(DocNode.Item("vICMS").InnerText.Replace(".", ",")).ToString Else dr("TOTAIS_ValorICMS") = "0.00"
                If DocNode.Item("vBCST") IsNot Nothing Then dr("TOTAIS_ValorBCST") = Convert.ToDouble(DocNode.Item("vBCST").InnerText.Replace(".", ",")).ToString Else dr("TOTAIS_ValorBCST") = "0.00"
                If DocNode.Item("vST") IsNot Nothing Then dr("TOTAIS_ValorST") = Convert.ToDouble(DocNode.Item("vST").InnerText.Replace(".", ",")).ToString Else dr("TOTAIS_ValorST") = "0.00"
                'If DocNode.Item("vProd") IsNot Nothing Then dr("TOTAIS_ValorProdutos") = Convert.ToDouble(DocNode.Item("vProd").InnerText).ToString Else dr("TOTAIS_ValorProdutos") = "0.00"
                If DocNode.Item("vFrete") IsNot Nothing Then dr("TOTAIS_ValorFrete") = Convert.ToDouble(DocNode.Item("vFrete").InnerText.Replace(".", ",")).ToString Else dr("TOTAIS_ValorFrete") = "0.00"
                If DocNode.Item("vSeg") IsNot Nothing Then dr("TOTAIS_ValorSeguro") = Convert.ToDouble(DocNode.Item("vSeg").InnerText.Replace(".", ",")).ToString Else dr("TOTAIS_ValorSeguro") = "0.00"
                If DocNode.Item("vDesc") IsNot Nothing Then dr("TOTAIS_ValorDescontos") = Convert.ToDouble(DocNode.Item("vII").InnerText.Replace(".", ",")).ToString Else dr("TOTAIS_ValorDescontos") = "0.00"
                If DocNode.Item("vII") IsNot Nothing Then dr("TOTAIS_ValorII") = Convert.ToDouble(DocNode.Item("vBC").InnerText.Replace(".", ",")).ToString Else dr("TOTAIS_ValorII") = "0.00"
                If DocNode.Item("vIPI") IsNot Nothing Then dr("TOTAIS_ValorIPI") = Convert.ToDouble(DocNode.Item("vIPI").InnerText.Replace(".", ",")).ToString Else dr("TOTAIS_ValorIPI") = "0.00"
                If DocNode.Item("vPIS") IsNot Nothing Then dr("TOTAIS_ValorPIS") = Convert.ToDouble(DocNode.Item("vPIS").InnerText.Replace(".", ",")).ToString Else dr("TOTAIS_ValorPIS") = "0.00"
                If DocNode.Item("vCOFINS") IsNot Nothing Then dr("TOTAIS_ValorCOFINS") = Convert.ToDouble(DocNode.Item("vCOFINS").InnerText.Replace(".", ",")).ToString Else dr("TOTAIS_ValorCOFINS") = "0.00"
                If DocNode.Item("vOutro") IsNot Nothing Then dr("TOTAIS_ValorOutros") = Convert.ToDouble(DocNode.Item("vOutro").InnerText.Replace(".", ",")).ToString Else dr("TOTAIS_ValorOutros") = "0.00"
                If DocNode.Item("vNF") IsNot Nothing Then dr("TOTAIS_ValorNF") = Convert.ToDouble(DocNode.Item("vNF").InnerText.Replace(".", ",")).ToString Else dr("TOTAIS_ValorNF") = "0.00"

                dr("TOTAIS_ValorProEIPI") = CDbl(ValorProdutoMult) + Convert.ToDouble(vIPI).ToString

            End If


            'Salvar na tabela
            ds.Tables("Dados").Rows.Add(dr)
        Next

        Return ds.Tables("Dados")

    End Function

    Public Function UFPorCodigo(Codigo As String) As String
        Select Case Codigo
            Case "11"
                Return "Rondônia"
            Case "12"
                Return "Acre"
            Case "13"
                Return "Amazonas"
            Case "14"
                Return "Roraima"
            Case "15"
                Return "Pará"
            Case "16"
                Return "Amapá"
            Case "17"
                Return "Tocantins"
            Case "21"
                Return "Maranhão"
            Case "22"
                Return "Piauí"
            Case "23"
                Return "Ceará"
            Case "24"
                Return "Rio Grande do Norte"
            Case "25"
                Return "Paraíba"
            Case "26"
                Return "Pernambuco"
            Case "27"
                Return "Alagoas"
            Case "28"
                Return "Sergipe"
            Case "29"
                Return "Bahia"
            Case "31"
                Return "Minas Gerais"
            Case "32"
                Return "Espírito Santo"
            Case "33"
                Return "Rio de Janeiro"
            Case "35"
                Return "São Paulo"
            Case "41"
                Return "Paraná"
            Case "42"
                Return "Santa Catarina"
            Case "43"
                Return "Rio Grande do Sul"
            Case "50"
                Return "Mato Grosso do Sul"
            Case "51"
                Return "Mato Grosso"
            Case "52"
                Return "Goiás"
            Case "53"
                Return "Distrito Federal"
        End Select
        Return ""
    End Function
    Public Function TributoPorCodigo(Codigo As String) As String
        Select Case Codigo
            Case "00"
                Return "Tributada Integralmente"
            Case "10"
                Return "Tributada e com cobrança do ICMS por substituição tributária"
            Case "20"
                Return "Com redução de base de cálculo"
            Case "30"
                Return "Isenta ou não tributada e com cobrança do ICMS por substituição tributária"
            Case "40"
                Return "Isenta"
            Case "41"
                Return "Não tributada"
            Case "50"
                Return "Suspensão"
            Case "51"
                Return "Diferimento"
            Case "60"
                Return "ICMS cobrado anteriormente por substituição tributária"
            Case "70"
                Return "Com redução de base de cálculo e cobrança do ICMS por substituição tributária ICMS por substituição tributária"
            Case "90"
                Return "Outros"
        End Select
        Return ""
    End Function
    Public Function IPI_SituacaoPorCodigo(Codigo As String) As String
        Select Case Codigo
            Case "00"
                Return "Entrada com recuperação de crédito"
            Case "01"
                Return "Entrada tributada com alíquota zero"
            Case "02"
                Return "Entrada isenta"
            Case "03"
                Return "Entrada não-tributada"
            Case "04"
                Return "Entrada imune"
            Case "05"
                Return "Entrada com suspensão"
            Case "49"
                Return "Outras entradas"
            Case "50"
                Return "Saída tributada"
            Case "51"
                Return "Saída tributada com alíquota zero"
            Case "52"
                Return "Saída isenta"
            Case "53"
                Return "Saída não-tributada"
            Case "54"
                Return "Saída imune"
            Case "55"
                Return "Saída com suspensão"
            Case "99"
                Return "Entrada com recuperação de crédito"
        End Select
        Return ""
    End Function
    Public Function PIS_SituacaoPorCodigo(Codigo As String) As String
        Select Case Codigo
            Case "01"
                Return "Operação Tributável (base de cálculo = valor da operação alíquota normal (cumulativo/não cumulativo))"
            Case "02"
                Return "Operação Tributável (base de cálculo = valor da operação (alíquota diferenciada))"
            Case "03"
                Return "Operação Tributável (base de cálculo = quantidade vendida x alíquota por unidade de produto)"
            Case "04"
                Return "Operação Tributável (tributação monofásica (alíquota zero))"
            Case "06"
                Return "Operação Tributável (alíquota zero)"
            Case "07"
                Return "Operação Isenta da Contribuição"
            Case "08"
                Return "Operação Sem Incidência da Contribuição"
            Case "09"
                Return "Operação com Suspensão da Contribuição"
            Case "99"
                Return "Outras Operações"
        End Select
        Return ""
    End Function
    Public Function COFINS_SituacaoPorCodigo(Codigo As String) As String
        Select Case Codigo
            Case "01"
                Return "Operação Tributável (base de cálculo = valor da operação alíquota normal (cumulativo/não cumulativo))"
            Case "02"
                Return "Operação Tributável (base de cálculo = valor da operação (alíquota diferenciada))"
            Case "03"
                Return "Operação Tributável (base de cálculo = quantidade vendida x alíquota por unidade de produto)"
            Case "04"
                Return "Operação Tributável (tributação monofásica (alíquota zero))"
            Case "06"
                Return "Operação Tributável (alíquota zero)"
            Case "07"
                Return "Operação Isenta da Contribuição"
            Case "08"
                Return "Operação Sem Incidência da Contribuição"
            Case "09"
                Return "Operação com Suspensão da Contribuição"
            Case "99"
                Return "Outras Operações"
        End Select
        Return ""
    End Function

    Public Sub CriarColunas(dt As DataTable)
        Dim campos() As String = {"IndexProduto", _
                                  "Versao", _
                                  "Id", _
                                  "CodigoUFEmitente", _
                                  "UFEmitente", _
                                  "ChaveAcesso", _
                                  "DigitoVerificador", _
                                  "NaturezaOperacao", _
                                  "FormaPagamento", _
                                  "ModeloDocumentoFiscal", _
                                  "Serie", _
                                  "Numero", _
                                  "DataEmissao", _
                                  "TipoOperacao", _
                                  "CodigoCidadeFatoGerador", _
                                  "TipoEmissao", _
                                  "Finalidade", _
                                  "CNPJEmitente", _
                                  "NomeEmitente", _
                                  "EndLogradouroEmitente", _
                                  "EndNumeroEmitente", _
                                  "EndBairroEmitente", _
                                  "EndCodigoCidadeEmitente", _
                                  "EndCidadeEmitente", _
                                  "EndUFEmitente", _
                                  "EndCEPEmitente", _
                                  "EndCodigoPaisEmitente", _
                                  "EndPaisEmitente", _
                                  "EndTelefoneEmitente", _
                                  "IEEmitente", _
                                  "CodigoRegimeTributarioEmitente", _
                                  "CNPJDestinatario", _
                                  "NomeDestinatario", _
                                  "EndLogradouroDestinatario", _
                                  "EndNumeroDestinatario", _
                                  "EndBairroDestinatario", _
                                  "EndCodigoCidadeDestinatario", _
                                  "EndCidadeDestinatario", _
                                  "EndUFDestinatario", _
                                  "EndCEPDestinatario", _
                                  "EndCodigoPaisDestinatario", _
                                  "EndPaisDestinatario", _
                                  "EndTelefoneDestinatario", _
                                  "IEDestinatario", _
                                  "CodigoProduto", _
                                  "DescricaoProduto", _
                                  "NCMProduto", _
                                  "CFOPProduto", _
                                  "UnidadeComercialProduto", _
                                  "QuantidadeComercialProduto", _
                                  "ValorUnitarioProduto", _
                                  "ICMS_OrigemProduto", _
                                  "ICMS_TributacaoProduto", _
                                  "ICMS_TributacaoDescricao", _
                                  "ICMS_ModalidadeBC", _
                                  "ICMS_PercentualReducaoBC", _
                                  "ICMS_ValorBC", _
                                  "ICMS_Aliquota", _
                                  "ICMS_Valor", _
                                  "ICMS_ModalidadeBCST", _
                                  "ICMS_PercentualMargemBCST", _
                                  "ICMS_PercentualReducaoBCST", _
                                  "ICMS_ValorBCST", _
                                  "ICMS_AliquotaICMSST", _
                                  "ICMS_ValorICMSST", _
                                  "IPI_Enquadramento", _
                                  "IPI_CNPJProdutor", _
                                  "IPI_SeloControle", _
                                  "IPI_SeloQuantidade", _
                                  "IPI_CodigoEnquadramento", _
                                  "IPI_CodigoSituacaoTributaria", _
                                  "IPI_CodigoSituacaoTributariaDescricao", _
                                  "IPI_ValorBC", _
                                  "IPI_Aliquota", _
                                  "IPI_Valor", _
                                  "IPI_Unidade", _
                                  "IPI_ValorUnitario", _
                                  "II_ValorBC", _
                                  "II_ValorDespesasAduaneiras", _
                                  "II_Valor", _
                                  "II_ValorIOF", _
                                  "PIS_CodigoSituacaoTributaria", _
                                  "PIS_CodigoSituacaoTributariaDescricao", _
                                  "PIS_ValorBC", _
                                  "PIS_Aliquota", _
                                  "PIS_Valor", _
                                  "PIS_QuantidadeVendida", _
                                  "PIS_ValorAliquota", _
                                  "COFINS_CodigoSituacaoTributaria", _
                                  "COFINS_CodigoSituacaoTributariaDescricao", _
                                  "COFINS_ValorBC", _
                                  "COFINS_Aliquota", _
                                  "COFINS_Valor", _
                                  "COFINS_QuantidadeVendida", _
                                  "COFINS_ValorAliquota", _
                                  "TOTAIS_ValorBC", _
                                  "TOTAIS_ValorICMS", _
                                  "TOTAIS_ValorBCST", _
                                  "TOTAIS_ValorST", _
                                  "TOTAIS_ValorProdutos", _
                                  "TOTAIS_ValorFrete", _
                                  "TOTAIS_ValorSeguro", _
                                  "TOTAIS_ValorDescontos", _
                                  "TOTAIS_ValorII", _
                                  "TOTAIS_ValorIPI", _
                                  "TOTAIS_ValorPIS", _
                                  "TOTAIS_ValorCOFINS", _
                                  "TOTAIS_ValorOutros", _
                                  "TOTAIS_ValorNF", _
                                  "TOTAIS_ValorProEIPI"
                                  }

        For Each campo In campos
            dt.Columns.Add(campo, Type.GetType("System.String"))
        Next
    End Sub
    Public Function FormatarCNPJ(CNPJ As String) As String
        Return CNPJ.Substring(0, 2) + "." + CNPJ.Substring(2, 3) + "." + CNPJ.Substring(5, 3) + "/" + CNPJ.Substring(8, 4) + "-" + CNPJ.Substring(12, 2)
    End Function
    Public Function RemoverFormatacao(Numero As String) As String
        Dim formatacao() As String = {".", ",", "-", "/"}
        Dim remove As Boolean = False
        Dim tmpStr As String = ""

        Dim i As Integer = 0
        For i = 0 To (Numero.Length - 1)
            For Each item In formatacao
                If Numero.Substring(i, 1) = item Then
                    remove = True
                    Exit For
                Else
                    remove = False
                End If
            Next
            If Not remove Then tmpStr = tmpStr + Numero.Substring(i, 1)
        Next

        Return tmpStr
    End Function
End Module
