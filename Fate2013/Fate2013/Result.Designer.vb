<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Result
    Inherits System.Windows.Forms.Form

    'Form 覆寫 Dispose 以清除元件清單。
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

    '為 Windows Form 設計工具的必要項
    Private components As System.ComponentModel.IContainer

    '注意: 以下為 Windows Form 設計工具所需的程序
    '可以使用 Windows Form 設計工具進行修改。
    '請不要使用程式碼編輯器進行修改。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Result))
        Me.Btn_Pan = New System.Windows.Forms.Button()
        Me.Btn_Pen = New System.Windows.Forms.Button()
        Me.Btn_Erase = New System.Windows.Forms.Button()
        Me.Btn_Print = New System.Windows.Forms.Button()
        Me.Btn_Close = New System.Windows.Forms.Button()
        Me.PicDisplay = New System.Windows.Forms.PictureBox()
        Me.PrintDialog1 = New System.Windows.Forms.PrintDialog()
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.Label_page = New System.Windows.Forms.Label()
        Me.Btn_next = New System.Windows.Forms.Button()
        Me.Btn_Prev = New System.Windows.Forms.Button()
        Me.Lbmsg = New System.Windows.Forms.Label()
        Me.GroupBox_VH = New System.Windows.Forms.GroupBox()
        Me.RadioButton_H = New System.Windows.Forms.RadioButton()
        Me.RadioButton_V = New System.Windows.Forms.RadioButton()
        CType(Me.PicDisplay, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox_VH.SuspendLayout()
        Me.SuspendLayout()
        '
        'Btn_Pan
        '
        Me.Btn_Pan.Image = Global.Fate.My.Resources.Resources.move
        Me.Btn_Pan.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.Btn_Pan.Location = New System.Drawing.Point(3, 113)
        Me.Btn_Pan.Name = "Btn_Pan"
        Me.Btn_Pan.Size = New System.Drawing.Size(45, 40)
        Me.Btn_Pan.TabIndex = 1
        Me.Btn_Pan.Text = "移動"
        Me.Btn_Pan.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.Btn_Pan.UseVisualStyleBackColor = True
        '
        'Btn_Pen
        '
        Me.Btn_Pen.Image = Global.Fate.My.Resources.Resources.pen
        Me.Btn_Pen.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.Btn_Pen.Location = New System.Drawing.Point(3, 159)
        Me.Btn_Pen.Name = "Btn_Pen"
        Me.Btn_Pen.Size = New System.Drawing.Size(45, 40)
        Me.Btn_Pen.TabIndex = 2
        Me.Btn_Pen.Text = "紅筆"
        Me.Btn_Pen.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.Btn_Pen.UseVisualStyleBackColor = True
        '
        'Btn_Erase
        '
        Me.Btn_Erase.Image = CType(resources.GetObject("Btn_Erase.Image"), System.Drawing.Image)
        Me.Btn_Erase.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.Btn_Erase.Location = New System.Drawing.Point(3, 205)
        Me.Btn_Erase.Name = "Btn_Erase"
        Me.Btn_Erase.Size = New System.Drawing.Size(45, 40)
        Me.Btn_Erase.TabIndex = 3
        Me.Btn_Erase.Text = "還原"
        Me.Btn_Erase.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.Btn_Erase.UseVisualStyleBackColor = True
        '
        'Btn_Print
        '
        Me.Btn_Print.Image = Global.Fate.My.Resources.Resources.printer
        Me.Btn_Print.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.Btn_Print.Location = New System.Drawing.Point(3, 48)
        Me.Btn_Print.Name = "Btn_Print"
        Me.Btn_Print.Size = New System.Drawing.Size(45, 40)
        Me.Btn_Print.TabIndex = 4
        Me.Btn_Print.Text = "列印"
        Me.Btn_Print.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.Btn_Print.UseVisualStyleBackColor = True
        '
        'Btn_Close
        '
        Me.Btn_Close.Location = New System.Drawing.Point(3, 2)
        Me.Btn_Close.Name = "Btn_Close"
        Me.Btn_Close.Size = New System.Drawing.Size(45, 40)
        Me.Btn_Close.TabIndex = 5
        Me.Btn_Close.Text = "關閉"
        Me.Btn_Close.UseVisualStyleBackColor = True
        '
        'PicDisplay
        '
        Me.PicDisplay.Location = New System.Drawing.Point(73, 2)
        Me.PicDisplay.Name = "PicDisplay"
        Me.PicDisplay.Size = New System.Drawing.Size(404, 195)
        Me.PicDisplay.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PicDisplay.TabIndex = 7
        Me.PicDisplay.TabStop = False
        '
        'PrintDialog1
        '
        Me.PrintDialog1.UseEXDialog = True
        '
        'PrintDocument1
        '
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Font = New System.Drawing.Font("PMingLiU", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(153, 26)
        '
        'Label_page
        '
        Me.Label_page.AutoSize = True
        Me.Label_page.Location = New System.Drawing.Point(12, 308)
        Me.Label_page.Name = "Label_page"
        Me.Label_page.Size = New System.Drawing.Size(30, 13)
        Me.Label_page.TabIndex = 8
        Me.Label_page.Text = "Page"
        '
        'Btn_next
        '
        Me.Btn_next.Location = New System.Drawing.Point(3, 336)
        Me.Btn_next.Name = "Btn_next"
        Me.Btn_next.Size = New System.Drawing.Size(45, 23)
        Me.Btn_next.TabIndex = 9
        Me.Btn_next.Text = "下頁"
        Me.Btn_next.UseVisualStyleBackColor = True
        '
        'Btn_Prev
        '
        Me.Btn_Prev.Location = New System.Drawing.Point(3, 272)
        Me.Btn_Prev.Name = "Btn_Prev"
        Me.Btn_Prev.Size = New System.Drawing.Size(45, 23)
        Me.Btn_Prev.TabIndex = 10
        Me.Btn_Prev.Text = "上頁"
        Me.Btn_Prev.UseVisualStyleBackColor = True
        '
        'Lbmsg
        '
        Me.Lbmsg.AutoSize = True
        Me.Lbmsg.Font = New System.Drawing.Font("PMingLiU", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Lbmsg.Location = New System.Drawing.Point(3, 377)
        Me.Lbmsg.Name = "Lbmsg"
        Me.Lbmsg.Size = New System.Drawing.Size(45, 13)
        Me.Lbmsg.TabIndex = 11
        Me.Lbmsg.Text = "Label1"
        Me.Lbmsg.Visible = False
        '
        'GroupBox_VH
        '
        Me.GroupBox_VH.Controls.Add(Me.RadioButton_H)
        Me.GroupBox_VH.Controls.Add(Me.RadioButton_V)
        Me.GroupBox_VH.Location = New System.Drawing.Point(6, 409)
        Me.GroupBox_VH.Name = "GroupBox_VH"
        Me.GroupBox_VH.Size = New System.Drawing.Size(42, 50)
        Me.GroupBox_VH.TabIndex = 31
        Me.GroupBox_VH.TabStop = False
        Me.GroupBox_VH.Text = "V/H"
        Me.GroupBox_VH.Visible = False
        '
        'RadioButton_H
        '
        Me.RadioButton_H.AutoSize = True
        Me.RadioButton_H.Checked = True
        Me.RadioButton_H.Location = New System.Drawing.Point(4, 13)
        Me.RadioButton_H.Name = "RadioButton_H"
        Me.RadioButton_H.Size = New System.Drawing.Size(38, 17)
        Me.RadioButton_H.TabIndex = 1
        Me.RadioButton_H.TabStop = True
        Me.RadioButton_H.Text = "橫"
        Me.RadioButton_H.UseVisualStyleBackColor = True
        '
        'RadioButton_V
        '
        Me.RadioButton_V.AutoSize = True
        Me.RadioButton_V.Location = New System.Drawing.Point(4, 30)
        Me.RadioButton_V.Name = "RadioButton_V"
        Me.RadioButton_V.Size = New System.Drawing.Size(38, 17)
        Me.RadioButton_V.TabIndex = 0
        Me.RadioButton_V.Text = "直"
        Me.RadioButton_V.UseVisualStyleBackColor = True
        '
        'Result
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.AutoSize = True
        Me.BackgroundImage = Global.Fate.My.Resources.Resources.Back001
        Me.ClientSize = New System.Drawing.Size(489, 471)
        Me.Controls.Add(Me.GroupBox_VH)
        Me.Controls.Add(Me.Lbmsg)
        Me.Controls.Add(Me.Btn_Prev)
        Me.Controls.Add(Me.Btn_next)
        Me.Controls.Add(Me.Label_page)
        Me.Controls.Add(Me.Btn_Close)
        Me.Controls.Add(Me.Btn_Print)
        Me.Controls.Add(Me.Btn_Pan)
        Me.Controls.Add(Me.PicDisplay)
        Me.Controls.Add(Me.Btn_Erase)
        Me.Controls.Add(Me.Btn_Pen)
        Me.Font = New System.Drawing.Font("PMingLiU", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(200, 0)
        Me.Name = "Result"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "命造表"
        CType(Me.PicDisplay, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox_VH.ResumeLayout(False)
        Me.GroupBox_VH.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Btn_Pan As System.Windows.Forms.Button
    Friend WithEvents Btn_Pen As System.Windows.Forms.Button
    Friend WithEvents Btn_Erase As System.Windows.Forms.Button
    Friend WithEvents Btn_Print As System.Windows.Forms.Button
    Friend WithEvents Btn_Close As System.Windows.Forms.Button
    Friend WithEvents PicDisplay As System.Windows.Forms.PictureBox
    Friend WithEvents PrintDialog1 As System.Windows.Forms.PrintDialog
    Friend WithEvents PrintDocument1 As System.Drawing.Printing.PrintDocument
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents Label_page As System.Windows.Forms.Label
    Friend WithEvents Btn_next As System.Windows.Forms.Button
    Friend WithEvents Btn_Prev As System.Windows.Forms.Button
    Friend WithEvents Lbmsg As System.Windows.Forms.Label
    Friend WithEvents GroupBox_VH As System.Windows.Forms.GroupBox
    Friend WithEvents RadioButton_H As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton_V As System.Windows.Forms.RadioButton
End Class
