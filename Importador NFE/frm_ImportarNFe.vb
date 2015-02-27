Imports System.IO

Public Class frm_ImportarNFe
    Public ClienteDestinoNome As String = ""
    Public ClienteDestinoCNPJ As String = ""

    Private Sub btn_Selecionar_Click(sender As Object, e As EventArgs) Handles btn_Selecionar.Click
        Dim i As Integer = 0

        fb_PastaNFE.ShowDialog()
        cb_Todas.Enabled = False
        cb_Todas.Checked = False

        If fb_PastaNFE.SelectedPath = "" Then Return
        clb_NFEs.Items.Clear()


        txt_PastaNFE.Text = fb_PastaNFE.SelectedPath

        Dim strFileSize As String = ""
        Dim di As New IO.DirectoryInfo(txt_PastaNFE.Text)
        Dim aryFi As IO.FileInfo() = di.GetFiles("*.xml")
        Dim fi As IO.FileInfo

        For Each fi In aryFi
            i += 1
            clb_NFEs.Items.Add(fi.Name)
        Next

        Select Case i
            Case 0
                lbl_Itens.Text = "Nenhum item encontrado"
                btn_Importar.Enabled = False
            Case 1
                lbl_Itens.Text = i.ToString + " item encontrado"
                btn_Importar.Enabled = True
            Case Else
                lbl_Itens.Text = i.ToString + " itens encontrados"
                btn_Importar.Enabled = True
        End Select

        SaveSetting("NFE Importador", "NFEs", "UltimoCaminho", txt_PastaNFE.Text)

        cb_Todas.Enabled = True
        cb_Todas.Checked = True
    End Sub

    Private Sub btn_Cancelar_Click(sender As Object, e As EventArgs) Handles btn_Cancelar.Click
        Me.Close()
    End Sub

    Private Sub cb_Todas_CheckedChanged(sender As Object, e As EventArgs) Handles cb_Todas.CheckedChanged
        For i As Integer = 0 To clb_NFEs.Items.Count - 1
            clb_NFEs.SetItemChecked(i, cb_Todas.Checked)
        Next
    End Sub

    Private Sub btn_Importar_Click(sender As Object, e As EventArgs) Handles btn_Importar.Click
        panel_Selecao.Visible = False
        panel_Importacao.Visible = True
        Me.Refresh()

        pb_Importacao.Minimum = 0
        pb_Importacao.Maximum = clb_NFEs.CheckedIndices.Count
        lb_Falhas.Items.Clear()

        Dim Result As New frm_NFeImportada
        Result.dg_NFe.ColumnCount = 64
        Result.dg_NFe.Rows.Add(New String() {"Id",
                                             "CodigoUFEmitente",
                                             "ChaveAcesso",
                                             "DigitoVerificador",
                                             "NaturezaOperacao",
                                             "FormaPagamento",
                                             "ModeloDocumentoFiscal",
                                             "Serie",
                                             "Numero",
                                             "DataEmissao",
                                             "TipoOperacao",
                                             "CodigoCidadeFatoGerador",
                                             "TipoEmissao",
                                             "Finalidade",
                                             "CNPJEmitente",
                                             "NomeEmitente",
                                             "CodigoProduto",
                                             "DescricaoProduto",
                                             "NCMProduto",
                                             "CFOPProduto",
                                             "UnidadeComercialProduto",
                                             "QuantidadeComercialProduto",
                                             "ValorUnitarioProduto",
                                             "TOTAIS_ValorProdutos",
                                             "ICMS_OrigemProduto",
                                             "ICMS_TributacaoProduto",
                                             "ICMS_ModalidadeBC",
                                             "ICMS_ModalidadeBCST",
                                             "ICMS_Aliquota",
                                             "ICMS_Valor",
                                             "ICMS_ValorBC",
                                             "ICMS_ValorBCST",
                                             "ICMS_PercentualMargemBCST",
                                             "ICMS_PercentualReducaoBCST",
                                             "ICMS_AliquotaICMSST",
                                             "ICMS_ValorICMSST",
                                             "IPI_Enquadramento",
                                             "IPI_CNPJProdutor",
                                             "IPI_SeloControle",
                                             "IPI_SeloQuantidade",
                                             "IPI_CodigoEnquadramento",
                                             "IPI_CodigoSituacaoTributaria",
                                             "IPI_ValorBC",
                                             "IPI_Aliquota",
                                             "IPI_Valor",
                                             "IPI_Unidade",
                                             "IPI_ValorUnitario",
                                             "II_ValorBC",
                                             "II_ValorDespesasAduaneiras",
                                             "II_Valor",
                                             "II_ValorIOF",
                                             "PIS_CodigoSituacaoTributaria",
                                             "PIS_ValorBC",
                                             "PIS_Aliquota",
                                             "PIS_Valor",
                                             "PIS_QuantidadeVendida",
                                             "PIS_ValorAliquota",
                                             "COFINS_CodigoSituacaoTributaria",
                                             "COFINS_ValorBC",
                                             "COFINS_Aliquota",
                                             "COFINS_Valor",
                                             "COFINS_QuantidadeVendida",
                                             "COFINS_ValorAliquota",
                                             "TOTAIS_ValorProEIPI"
                                            })

        For Each nfe In clb_NFEs.CheckedItems
            pb_Importacao.Value = pb_Importacao.Value + 1
            If NFeValida(txt_PastaNFE.Text + "\" + nfe) Then
                Dim nf As DataTable = LerNFe(txt_PastaNFE.Text + "\" + nfe)

                For Each item In nf.Rows
                    Result.dg_NFe.Rows.Add(New String() {item("Id"),
                                                         item("CodigoUFEmitente"),
                                                         item("ChaveAcesso"),
                                                         item("DigitoVerificador"),
                                                         item("NaturezaOperacao"),
                                                         item("FormaPagamento"),
                                                         item("ModeloDocumentoFiscal"),
                                                         item("Serie"),
                                                         item("Numero"),
                                                         item("DataEmissao"),
                                                         item("TipoOperacao"),
                                                         item("CodigoCidadeFatoGerador"),
                                                         item("TipoEmissao"),
                                                         item("Finalidade"),
                                                         item("CNPJEmitente"),
                                                         item("NomeEmitente"),
                                                         item("CodigoProduto"),
                                                         item("DescricaoProduto"),
                                                         item("NCMProduto"),
                                                         item("CFOPProduto"),
                                                         item("UnidadeComercialProduto"),
                                                         item("QuantidadeComercialProduto"),
                                                         item("ValorUnitarioProduto"),
                                                         item("TOTAIS_ValorProdutos"),
                                                         item("ICMS_OrigemProduto"),
                                                         item("ICMS_TributacaoProduto"),
                                                         item("ICMS_ModalidadeBC"),
                                                         item("ICMS_ModalidadeBCST"),
                                                         item("ICMS_Aliquota"),
                                                         item("ICMS_Valor"),
                                                         item("ICMS_ValorBC"),
                                                         item("ICMS_ValorBCST"),
                                                         item("ICMS_PercentualMargemBCST"),
                                                         item("ICMS_PercentualReducaoBCST"),
                                                         item("ICMS_AliquotaICMSST"),
                                                         item("ICMS_ValorICMSST"),
                                                         item("IPI_Enquadramento"),
                                                         item("IPI_CNPJProdutor"),
                                                         item("IPI_SeloControle"),
                                                         item("IPI_SeloQuantidade"),
                                                         item("IPI_CodigoEnquadramento"),
                                                         item("IPI_CodigoSituacaoTributaria"),
                                                         item("IPI_ValorBC"),
                                                         item("IPI_Aliquota"),
                                                         item("IPI_Valor"),
                                                         item("IPI_Unidade"),
                                                         item("IPI_ValorUnitario"),
                                                         item("II_ValorBC"),
                                                         item("II_ValorDespesasAduaneiras"),
                                                         item("II_Valor"),
                                                         item("II_ValorIOF"),
                                                         item("PIS_CodigoSituacaoTributaria"),
                                                         item("PIS_ValorBC"),
                                                         item("PIS_Aliquota"),
                                                         item("PIS_Valor"),
                                                         item("PIS_QuantidadeVendida"),
                                                         item("PIS_ValorAliquota"),
                                                         item("COFINS_CodigoSituacaoTributaria"),
                                                         item("COFINS_ValorBC"),
                                                         item("COFINS_Aliquota"),
                                                         item("COFINS_Valor"),
                                                         item("COFINS_QuantidadeVendida"),
                                                         item("COFINS_ValorAliquota"),
                                                         item("TOTAIS_ValorProEIPI")
                                                         })

                Next
            Else
                lb_Falhas.Items.Add(nfe)
            End If
        Next

        If lb_Falhas.Items.Count > 0 Then
            lb_Falhas.Visible = True
            lbl_Aviso.Text = "Ocorreram alguns erros ao importas as notas fiscais. Isto pode ocorrer caso o arquivo seja inválido ou a NF já tenha sido importada anteriormente." + vbCrLf + vbCrLf + "Abaixo a lista de arquivos que não puderam ser importadas:"
        Else
            lb_Falhas.Visible = False
            lbl_Aviso.Text = "Todas as notas fiscais selecionadas foram importadas com êxito."
        End If

        Result.Show()
    End Sub

    Private Sub btn_Fechar_Click(sender As Object, e As EventArgs) Handles btn_Fechar.Click
        Me.Close()
    End Sub

    Private Sub btn_Pesquisar_Click(sender As Object, e As EventArgs)
        'Dim rdDBc As SqlClient.SqlDataReader
        'Dim SelecionarCliente As New frm_SelecionarCliente

        'SelecionarCliente.ShowDialog()
        'SelecionarCliente = Nothing

        'txt_Cliente.Text = ClienteDestinoNome

        'cb_Convenios.Items.Clear()
        'cb_Contratos.Items.Clear()

        'If ClienteDestinoNome <> "" Then
        '    cb_Convenios.Items.Clear()
        '    cb_Convenios.Items.Add("Novo Convênio")

        '    With comDB
        '        .CommandText = "SELECT * FROM Convenios WHERE id_cliente = " + ClienteIDporCNPJ(RemoverFormatacao(ClienteDestinoCNPJ)) + " ORDER BY Numero ASC"

        '        rdDBc = .ExecuteReader
        '    End With

        '    Do While rdDBc.Read
        '        cb_Convenios.Items.Add(rdDBc("Numero").ToString())
        '        My.Application.DoEvents()
        '    Loop

        '    rdDBc.Close()

        '    If cb_Convenios.Items.Count > 1 Then
        '        cb_Convenios.SelectedIndex = 1

        '        cb_Contratos.Items.Clear()
        '        cb_Contratos.Items.Add("Novo Contrato")

        '        With comDB
        '            .CommandText = "SELECT * FROM Contratos WHERE id_convenio = " + ConvenioIDporNumero(cb_Convenios.Items(1).ToString)

        '            rdDBc = .ExecuteReader
        '        End With

        '        Do While rdDBc.Read
        '            cb_Contratos.Items.Add(rdDBc("id").ToString())
        '            My.Application.DoEvents()
        '        Loop

        '        rdDBc.Close()

        '        If cb_Contratos.Items.Count > 1 Then
        '            cb_Contratos.SelectedIndex = 1
        '        Else
        '            cb_Contratos.SelectedIndex = 0
        '        End If
        '    Else
        '        cb_Convenios.SelectedIndex = 0
        '    End If
        '    btn_Continuar.Enabled = True
        'Else
        '    btn_Continuar.Enabled = False
        'End If
        ''rdDBc.Close()
    End Sub

    Private Sub frm_ImportarNFe_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'panel_SelecaoClienteContratoPedido.Visible = True
        'panel_Importacao.Visible = False
        'panel_Selecao.Visible = False

        'txt_Cliente.Text = ""
        'txt_NroNovoConvenio.Text = ""
        'txt_NroNovoConvenio.Visible = False
        'cb_Convenios.Items.Clear()
        'cb_Contratos.Items.Clear()
        txt_PastaNFE.Text = GetSetting("NFE Importador", "NFEs", "UltimoCaminho", "")
        lbl_Itens.Text = ""
        cb_Todas.Checked = False
        cb_Todas.Enabled = False
        clb_NFEs.Items.Clear()
        pb_Importacao.Minimum = 0
        pb_Importacao.Value = 0
        lb_Falhas.Items.Clear()
        fb_PastaNFE.SelectedPath = ""
        clb_NFEs.Items.Clear()
        lb_Falhas.Visible = False
        'btn_Continuar.Enabled = False
        btn_Importar.Enabled = False
        lbl_Aviso.Text = ""

    End Sub

    Private Sub cb_Contratos_SelectedIndexChanged(sender As Object, e As EventArgs)
        'If cb_Convenios.SelectedItem.ToString <> "Novo Contrato" Then
        '    txt_NroNovoConvenio.Visible = False

        '    If cb_Convenios.Items.Count > 1 Then
        '        Dim rdDBc As SqlClient.SqlDataReader

        '        cb_Contratos.Items.Clear()
        '        cb_Contratos.Items.Add("Novo Pedido")

        '        With comDB
        '            .CommandText = "SELECT * FROM Contratos WHERE id_convenio = " + ConvenioIDporNumero(cb_Convenios.SelectedItem.ToString)

        '            rdDBc = .ExecuteReader
        '        End With

        '        Do While rdDBc.Read
        '            cb_Contratos.Items.Add(rdDBc("id").ToString())
        '            My.Application.DoEvents()
        '        Loop

        '        rdDBc.Close()

        '        If cb_Contratos.Items.Count > 1 Then
        '            cb_Contratos.SelectedIndex = 1
        '        Else
        '            cb_Contratos.SelectedIndex = 0
        '        End If

        '        rdDBc.Close()
        '    Else
        '        cb_Contratos.Items.Clear()
        '        cb_Contratos.Items.Add("Novo Contrato")
        '    End If
        'Else
        '    txt_NroNovoConvenio.Visible = True
        '    cb_Contratos.Items.Clear()
        '    cb_Contratos.Items.Add("Novo Contrato")
        '    cb_Contratos.SelectedIndex = 0
        'End If
    End Sub

    Private Sub btn_Continuar_Click(sender As Object, e As EventArgs)
        'If cb_Convenios.Text = "Novo Convênio" And txt_NroNovoConvenio.Text = "" Then
        '    MsgBox("Digite um novo número de convênio para continuar.", MsgBoxStyle.Information)
        '    Exit Sub
        'End If

        'If cb_Convenios.Text = "Novo Convênio" Then
        '    If ExisteConvenio(RemoverFormatacao(ClienteDestinoCNPJ), Val(txt_NroNovoConvenio.Text)) Then
        '        MsgBox("Convênio já existente. Digite outro número de convênio.", MsgBoxStyle.Exclamation)
        '        txt_NroNovoConvenio.Focus()
        '        Exit Sub
        '    End If
        'End If

        'panel_Selecao.Visible = True
        'panel_SelecaoClienteContratoPedido.Visible = False
    End Sub

    Private Sub btn_Cancel_Click(sender As Object, e As EventArgs)
        btn_Cancelar_Click(sender, e)
    End Sub

    Private Sub btn_UpdFolder_Click(sender As Object, e As EventArgs) Handles btn_UpdFolder.Click
        Dim i As Integer = 0

        If txt_PastaNFE.Text = "" Then Return
        clb_NFEs.Items.Clear()

        Dim strFileSize As String = ""
        Dim di As New IO.DirectoryInfo(txt_PastaNFE.Text)
        Dim aryFi As IO.FileInfo() = di.GetFiles("*.xml")
        Dim fi As IO.FileInfo

        For Each fi In aryFi
            i += 1
            clb_NFEs.Items.Add(fi.Name)
        Next

        Select Case i
            Case 0
                lbl_Itens.Text = "Nenhum item encontrado"
                btn_Importar.Enabled = False
            Case 1
                lbl_Itens.Text = i.ToString + " item encontrado"
                btn_Importar.Enabled = True
            Case Else
                lbl_Itens.Text = i.ToString + " itens encontrados"
                btn_Importar.Enabled = True
        End Select

        SaveSetting("NFE Importador", "NFEs", "UltimoCaminho", txt_PastaNFE.Text)

        cb_Todas.Enabled = True
        cb_Todas.Checked = True
    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click

    End Sub
End Class