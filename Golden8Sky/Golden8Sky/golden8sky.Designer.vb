<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Golden8sky
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Golden8sky))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.PicDisplay = New System.Windows.Forms.PictureBox()
        Me.ButtonBye = New System.Windows.Forms.Button()
        Me.ButtonAbout = New System.Windows.Forms.Button()
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.ListBox3 = New System.Windows.Forms.ListBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonTest = New System.Windows.Forms.Button()
        Me.Label_Msg = New System.Windows.Forms.Label()
        Me.ListBox2 = New System.Windows.Forms.ListBox()
        Me.Label2 = New System.Windows.Forms.Label()
        CType(Me.PicDisplay, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.ControlLight
        Me.Label1.Font = New System.Drawing.Font("PMingLiU", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label1.Location = New System.Drawing.Point(6, 25)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(82, 24)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "紫金運"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("PMingLiU", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label3.Location = New System.Drawing.Point(257, 14)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(34, 48)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "24" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "山"
        '
        'PicDisplay
        '
        Me.PicDisplay.BackColor = System.Drawing.Color.White
        Me.PicDisplay.Location = New System.Drawing.Point(12, 92)
        Me.PicDisplay.Name = "PicDisplay"
        Me.PicDisplay.Size = New System.Drawing.Size(450, 450)
        Me.PicDisplay.TabIndex = 6
        Me.PicDisplay.TabStop = False
        '
        'ButtonBye
        '
        Me.ButtonBye.Font = New System.Drawing.Font("PMingLiU", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.ButtonBye.Location = New System.Drawing.Point(65, 545)
        Me.ButtonBye.Name = "ButtonBye"
        Me.ButtonBye.Size = New System.Drawing.Size(144, 33)
        Me.ButtonBye.TabIndex = 8
        Me.ButtonBye.Text = "結束"
        Me.ButtonBye.UseVisualStyleBackColor = True
        '
        'ButtonAbout
        '
        Me.ButtonAbout.Font = New System.Drawing.Font("PMingLiU", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.ButtonAbout.Location = New System.Drawing.Point(268, 545)
        Me.ButtonAbout.Name = "ButtonAbout"
        Me.ButtonAbout.Size = New System.Drawing.Size(123, 33)
        Me.ButtonAbout.TabIndex = 9
        Me.ButtonAbout.Text = "關於"
        Me.ButtonAbout.UseVisualStyleBackColor = True
        '
        'ListBox1
        '
        Me.ListBox1.Font = New System.Drawing.Font("PMingLiU", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.ItemHeight = 16
        Me.ListBox1.Items.AddRange(New Object() {"一運紫白一運金龍", "一運紫白二運金龍", "二運紫白二運金龍", "三運紫白二運金龍", "三運紫白三運金龍", "四運紫白三運金龍", "四運紫白四運金龍", "五運紫白四運金龍", "五運紫白六運金龍", "六運紫白六運金龍", "六運紫白七運金龍", "七運紫白七運金龍", "七運紫白八運金龍", "八運紫白八運金龍", "八運紫白九運金龍", "九運紫白九運金龍"})
        Me.ListBox1.Location = New System.Drawing.Point(91, 10)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(158, 52)
        Me.ListBox1.TabIndex = 12
        '
        'ListBox3
        '
        Me.ListBox3.Font = New System.Drawing.Font("PMingLiU", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.ListBox3.FormattingEnabled = True
        Me.ListBox3.ItemHeight = 16
        Me.ListBox3.Items.AddRange(New Object() {"壬", "子", "癸", "丑", "艮", "寅", "甲", "卯", "乙", "辰", "巽", "巳", "丙", "午", "丁", "未", "坤", "申", "庚", "酉", "辛", "戌", "乾", "亥"})
        Me.ListBox3.Location = New System.Drawing.Point(297, 10)
        Me.ListBox3.Name = "ListBox3"
        Me.ListBox3.Size = New System.Drawing.Size(48, 52)
        Me.ListBox3.TabIndex = 14
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.ControlLight
        Me.Panel1.Controls.Add(Me.ListBox2)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.ListBox3)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.ListBox1)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Location = New System.Drawing.Point(12, 12)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(451, 69)
        Me.Panel1.TabIndex = 15
        '
        'ButtonTest
        '
        Me.ButtonTest.Font = New System.Drawing.Font("PMingLiU", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.ButtonTest.Location = New System.Drawing.Point(397, 545)
        Me.ButtonTest.Name = "ButtonTest"
        Me.ButtonTest.Size = New System.Drawing.Size(39, 32)
        Me.ButtonTest.TabIndex = 16
        Me.ButtonTest.Text = "全部掃描"
        Me.ButtonTest.UseVisualStyleBackColor = True
        Me.ButtonTest.Visible = False
        '
        'Label_Msg
        '
        Me.Label_Msg.AutoSize = True
        Me.Label_Msg.Location = New System.Drawing.Point(12, 81)
        Me.Label_Msg.Name = "Label_Msg"
        Me.Label_Msg.Size = New System.Drawing.Size(30, 12)
        Me.Label_Msg.TabIndex = 17
        Me.Label_Msg.Text = "Note:"
        Me.Label_Msg.Visible = False
        '
        'ListBox2
        '
        Me.ListBox2.Font = New System.Drawing.Font("PMingLiU", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.ListBox2.FormattingEnabled = True
        Me.ListBox2.ItemHeight = 16
        Me.ListBox2.Items.AddRange(New Object() {"一", "二", "三", "四", "五", "六", "七", "八", "九"})
        Me.ListBox2.Location = New System.Drawing.Point(393, 10)
        Me.ListBox2.Name = "ListBox2"
        Me.ListBox2.Size = New System.Drawing.Size(48, 52)
        Me.ListBox2.TabIndex = 16
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("PMingLiU", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label2.Location = New System.Drawing.Point(353, 14)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(34, 48)
        Me.Label2.TabIndex = 15
        Me.Label2.Text = "年" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "白"
        '
        'Golden8sky
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.Golden8Sky.My.Resources.Resources.Back001
        Me.ClientSize = New System.Drawing.Size(475, 580)
        Me.Controls.Add(Me.Label_Msg)
        Me.Controls.Add(Me.ButtonTest)
        Me.Controls.Add(Me.ButtonAbout)
        Me.Controls.Add(Me.ButtonBye)
        Me.Controls.Add(Me.PicDisplay)
        Me.Controls.Add(Me.Panel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(900, 80)
        Me.Name = "Golden8sky"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "紫金八宮"
        CType(Me.PicDisplay,System.ComponentModel.ISupportInitialize).EndInit
        Me.Panel1.ResumeLayout(false)
        Me.Panel1.PerformLayout
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents PicDisplay As System.Windows.Forms.PictureBox
    Friend WithEvents ButtonBye As System.Windows.Forms.Button
    Friend WithEvents ButtonAbout As System.Windows.Forms.Button
    Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
    Friend WithEvents ListBox3 As System.Windows.Forms.ListBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonTest As System.Windows.Forms.Button
    Friend WithEvents Label_Msg As System.Windows.Forms.Label
    Friend WithEvents ListBox2 As System.Windows.Forms.ListBox
    Friend WithEvents Label2 As System.Windows.Forms.Label

End Class
