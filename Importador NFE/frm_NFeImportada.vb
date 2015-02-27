Public Class frm_NFeImportada

    Private Sub frm_NFeImportada_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub dg_NFe_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dg_NFe.CellContentClick

    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        Dim rows = From row As DataGridViewRow In dg_NFe.Rows.Cast(Of DataGridViewRow)() Where Not row.IsNewRow Select Array.ConvertAll(row.Cells.Cast(Of DataGridViewCell).ToArray, Function(c) If(c.Value IsNot Nothing, c.Value.ToString, ""))
        Dim txt As String

        Using sw As New IO.StreamWriter("export.csv")
            For Each r In rows

                txt = Chr(34)
                For a As Integer = 0 To 62
                    txt = txt + r(a) + Chr(34) + ";" + Chr(34)
                Next
                txt = txt + r(63) + Chr(34)

                sw.WriteLine(txt)


                'sw.WriteLine(String.Join(",", r))
            Next
        End Using

        Process.Start("export.csv")
    End Sub
End Class