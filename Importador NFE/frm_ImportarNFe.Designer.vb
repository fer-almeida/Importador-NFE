<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_ImportarNFe
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.clb_NFEs = New System.Windows.Forms.CheckedListBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txt_PastaNFE = New System.Windows.Forms.TextBox()
        Me.btn_Selecionar = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.fb_PastaNFE = New System.Windows.Forms.FolderBrowserDialog()
        Me.lbl_Itens = New System.Windows.Forms.Label()
        Me.btn_Cancelar = New System.Windows.Forms.Button()
        Me.btn_Importar = New System.Windows.Forms.Button()
        Me.cb_Todas = New System.Windows.Forms.CheckBox()
        Me.panel_Selecao = New System.Windows.Forms.Panel()
        Me.btn_UpdFolder = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.panel_Importacao = New System.Windows.Forms.Panel()
        Me.btn_Fechar = New System.Windows.Forms.Button()
        Me.lb_Falhas = New System.Windows.Forms.ListBox()
        Me.lbl_Aviso = New System.Windows.Forms.Label()
        Me.pb_Importacao = New System.Windows.Forms.ProgressBar()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panel_Selecao.SuspendLayout()
        Me.panel_Importacao.SuspendLayout()
        Me.SuspendLayout()
        '
        'clb_NFEs
        '
        Me.clb_NFEs.CheckOnClick = True
        Me.clb_NFEs.FormattingEnabled = True
        Me.clb_NFEs.Location = New System.Drawing.Point(44, 100)
        Me.clb_NFEs.Name = "clb_NFEs"
        Me.clb_NFEs.Size = New System.Drawing.Size(506, 100)
        Me.clb_NFEs.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(82, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(66, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Importar"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.PictureBox2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(622, 89)
        Me.Panel1.TabIndex = 4
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = Global.Importador_NFE.My.Resources.Resources.invoice_64x64
        Me.PictureBox2.Location = New System.Drawing.Point(12, 12)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(64, 64)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox2.TabIndex = 2
        Me.PictureBox2.TabStop = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(41, 45)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(47, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Pasta:"
        '
        'txt_PastaNFE
        '
        Me.txt_PastaNFE.Enabled = False
        Me.txt_PastaNFE.Location = New System.Drawing.Point(94, 42)
        Me.txt_PastaNFE.Name = "txt_PastaNFE"
        Me.txt_PastaNFE.Size = New System.Drawing.Size(378, 21)
        Me.txt_PastaNFE.TabIndex = 6
        '
        'btn_Selecionar
        '
        Me.btn_Selecionar.Location = New System.Drawing.Point(475, 40)
        Me.btn_Selecionar.Name = "btn_Selecionar"
        Me.btn_Selecionar.Size = New System.Drawing.Size(75, 23)
        Me.btn_Selecionar.TabIndex = 7
        Me.btn_Selecionar.Text = "&Selecionar"
        Me.btn_Selecionar.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(41, 75)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(104, 13)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Nostas Fiscais:"
        '
        'lbl_Itens
        '
        Me.lbl_Itens.Location = New System.Drawing.Point(298, 279)
        Me.lbl_Itens.Name = "lbl_Itens"
        Me.lbl_Itens.Size = New System.Drawing.Size(252, 13)
        Me.lbl_Itens.TabIndex = 9
        Me.lbl_Itens.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'btn_Cancelar
        '
        Me.btn_Cancelar.Location = New System.Drawing.Point(523, 249)
        Me.btn_Cancelar.Name = "btn_Cancelar"
        Me.btn_Cancelar.Size = New System.Drawing.Size(75, 23)
        Me.btn_Cancelar.TabIndex = 10
        Me.btn_Cancelar.Text = "C&ancelar"
        Me.btn_Cancelar.UseVisualStyleBackColor = True
        '
        'btn_Importar
        '
        Me.btn_Importar.Enabled = False
        Me.btn_Importar.Location = New System.Drawing.Point(442, 249)
        Me.btn_Importar.Name = "btn_Importar"
        Me.btn_Importar.Size = New System.Drawing.Size(75, 23)
        Me.btn_Importar.TabIndex = 11
        Me.btn_Importar.Text = "&Importar"
        Me.btn_Importar.UseVisualStyleBackColor = True
        '
        'cb_Todas
        '
        Me.cb_Todas.AutoSize = True
        Me.cb_Todas.Enabled = False
        Me.cb_Todas.Location = New System.Drawing.Point(44, 206)
        Me.cb_Todas.Name = "cb_Todas"
        Me.cb_Todas.Size = New System.Drawing.Size(168, 17)
        Me.cb_Todas.TabIndex = 12
        Me.cb_Todas.Text = "Marcar/Desmarcar todas"
        Me.cb_Todas.UseVisualStyleBackColor = True
        '
        'panel_Selecao
        '
        Me.panel_Selecao.Controls.Add(Me.btn_UpdFolder)
        Me.panel_Selecao.Controls.Add(Me.Label7)
        Me.panel_Selecao.Controls.Add(Me.Label3)
        Me.panel_Selecao.Controls.Add(Me.cb_Todas)
        Me.panel_Selecao.Controls.Add(Me.btn_Cancelar)
        Me.panel_Selecao.Controls.Add(Me.btn_Importar)
        Me.panel_Selecao.Controls.Add(Me.clb_NFEs)
        Me.panel_Selecao.Controls.Add(Me.txt_PastaNFE)
        Me.panel_Selecao.Controls.Add(Me.btn_Selecionar)
        Me.panel_Selecao.Controls.Add(Me.lbl_Itens)
        Me.panel_Selecao.Controls.Add(Me.Label4)
        Me.panel_Selecao.Location = New System.Drawing.Point(12, 95)
        Me.panel_Selecao.Name = "panel_Selecao"
        Me.panel_Selecao.Size = New System.Drawing.Size(598, 276)
        Me.panel_Selecao.TabIndex = 13
        '
        'btn_UpdFolder
        '
        Me.btn_UpdFolder.Location = New System.Drawing.Point(442, 206)
        Me.btn_UpdFolder.Name = "btn_UpdFolder"
        Me.btn_UpdFolder.Size = New System.Drawing.Size(108, 23)
        Me.btn_UpdFolder.TabIndex = 13
        Me.btn_UpdFolder.Text = "&Atualizar Pasta"
        Me.btn_UpdFolder.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(41, 8)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(509, 30)
        Me.Label7.TabIndex = 5
        Me.Label7.Text = "Selecione a pasta onde os arquivos no formato XML das notas fiscais eletrônicas s" & _
    "e encontram:"
        '
        'panel_Importacao
        '
        Me.panel_Importacao.Controls.Add(Me.btn_Fechar)
        Me.panel_Importacao.Controls.Add(Me.lb_Falhas)
        Me.panel_Importacao.Controls.Add(Me.lbl_Aviso)
        Me.panel_Importacao.Controls.Add(Me.pb_Importacao)
        Me.panel_Importacao.Controls.Add(Me.Label5)
        Me.panel_Importacao.Location = New System.Drawing.Point(12, 95)
        Me.panel_Importacao.Name = "panel_Importacao"
        Me.panel_Importacao.Size = New System.Drawing.Size(598, 276)
        Me.panel_Importacao.TabIndex = 14
        Me.panel_Importacao.Visible = False
        '
        'btn_Fechar
        '
        Me.btn_Fechar.Location = New System.Drawing.Point(523, 249)
        Me.btn_Fechar.Name = "btn_Fechar"
        Me.btn_Fechar.Size = New System.Drawing.Size(75, 23)
        Me.btn_Fechar.TabIndex = 9
        Me.btn_Fechar.Text = "&Fechar"
        Me.btn_Fechar.UseVisualStyleBackColor = True
        '
        'lb_Falhas
        '
        Me.lb_Falhas.FormattingEnabled = True
        Me.lb_Falhas.Location = New System.Drawing.Point(44, 130)
        Me.lb_Falhas.Name = "lb_Falhas"
        Me.lb_Falhas.Size = New System.Drawing.Size(509, 108)
        Me.lb_Falhas.TabIndex = 8
        Me.lb_Falhas.Visible = False
        '
        'lbl_Aviso
        '
        Me.lbl_Aviso.Location = New System.Drawing.Point(38, 66)
        Me.lbl_Aviso.Name = "lbl_Aviso"
        Me.lbl_Aviso.Size = New System.Drawing.Size(538, 58)
        Me.lbl_Aviso.TabIndex = 7
        '
        'pb_Importacao
        '
        Me.pb_Importacao.Location = New System.Drawing.Point(41, 35)
        Me.pb_Importacao.Name = "pb_Importacao"
        Me.pb_Importacao.Size = New System.Drawing.Size(509, 23)
        Me.pb_Importacao.TabIndex = 6
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(38, 8)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(425, 13)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "Aguarde enquanto as notas fiscais selecionadas estão sendo importadas."
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(82, 30)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(516, 30)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Siga os passos abaixo para importar dados das notas fiscais eletrônicas para o ba" & _
    "nco de dados do sistema."
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(12, 384)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(598, 18)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "V1.03 - Desenvolvido por Fernando Almeida (fernando.almeida@me.com) para a CHG-Me" & _
    "ridian"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'frm_ImportarNFe
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(622, 409)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.panel_Selecao)
        Me.Controls.Add(Me.panel_Importacao)
        Me.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm_ImportarNFe"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Importar NFe"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panel_Selecao.ResumeLayout(False)
        Me.panel_Selecao.PerformLayout()
        Me.panel_Importacao.ResumeLayout(False)
        Me.panel_Importacao.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents clb_NFEs As System.Windows.Forms.CheckedListBox
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txt_PastaNFE As System.Windows.Forms.TextBox
    Friend WithEvents btn_Selecionar As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents fb_PastaNFE As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents lbl_Itens As System.Windows.Forms.Label
    Friend WithEvents btn_Cancelar As System.Windows.Forms.Button
    Friend WithEvents btn_Importar As System.Windows.Forms.Button
    Friend WithEvents cb_Todas As System.Windows.Forms.CheckBox
    Friend WithEvents panel_Selecao As System.Windows.Forms.Panel
    Friend WithEvents panel_Importacao As System.Windows.Forms.Panel
    Friend WithEvents pb_Importacao As System.Windows.Forms.ProgressBar
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lb_Falhas As System.Windows.Forms.ListBox
    Friend WithEvents lbl_Aviso As System.Windows.Forms.Label
    Friend WithEvents btn_Fechar As System.Windows.Forms.Button
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents btn_UpdFolder As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
End Class
