<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmFate
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmFate))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TBoxNumber = New System.Windows.Forms.TextBox()
        Me.TBoxName = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.CBcalender = New System.Windows.Forms.ComboBox()
        Me.TBoxYear = New System.Windows.Forms.TextBox()
        Me.TBoxMonth = New System.Windows.Forms.TextBox()
        Me.TBoxDay = New System.Windows.Forms.TextBox()
        Me.TBoxHour = New System.Windows.Forms.TextBox()
        Me.TBoxMin = New System.Windows.Forms.TextBox()
        Me.Button_clr = New System.Windows.Forms.Button()
        Me.Btn_NameCheck = New System.Windows.Forms.Button()
        Me.Button_cal = New System.Windows.Forms.Button()
        Me.Btn_Insert = New System.Windows.Forms.Button()
        Me.Button_exit = New System.Windows.Forms.Button()
        Me.Btn_Test = New System.Windows.Forms.Button()
        Me.Btn_LoopTest = New System.Windows.Forms.Button()
        Me.GroupBox_FM = New System.Windows.Forms.GroupBox()
        Me.RadioButton_F = New System.Windows.Forms.RadioButton()
        Me.RadioButton_M = New System.Windows.Forms.RadioButton()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Btn_NumberGet = New System.Windows.Forms.Button()
        Me.Btn_Update = New System.Windows.Forms.Button()
        Me.Btn_NoUp = New System.Windows.Forms.Button()
        Me.Btn_NoDown = New System.Windows.Forms.Button()
        Me.Btn_First = New System.Windows.Forms.Button()
        Me.Btn_Last = New System.Windows.Forms.Button()
        Me.Btn_Delete = New System.Windows.Forms.Button()
        Me.Btn_UpdateSAM = New System.Windows.Forms.Button()
        Me.BtnLBmsgClr = New System.Windows.Forms.Button()
        Me.RichTextBox1 = New System.Windows.Forms.RichTextBox()
        Me.GroupBox_Gwa = New System.Windows.Forms.GroupBox()
        Me.RB_Gwa2 = New System.Windows.Forms.RadioButton()
        Me.RB_Gwa1 = New System.Windows.Forms.RadioButton()
        Me.GroupBox_23H = New System.Windows.Forms.GroupBox()
        Me.RB_23hNext = New System.Windows.Forms.RadioButton()
        Me.RB_23hSame = New System.Windows.Forms.RadioButton()
        Me.GroupBox_West = New System.Windows.Forms.GroupBox()
        Me.TBoxWestYear = New System.Windows.Forms.TextBox()
        Me.GroupBox_MingKon = New System.Windows.Forms.GroupBox()
        Me.RB_MingKon3 = New System.Windows.Forms.RadioButton()
        Me.RB_MingKon2 = New System.Windows.Forms.RadioButton()
        Me.RB_MingKon1 = New System.Windows.Forms.RadioButton()
        Me.GroupBox_StarB = New System.Windows.Forms.GroupBox()
        Me.RB_StarB2 = New System.Windows.Forms.RadioButton()
        Me.RB_StarB1 = New System.Windows.Forms.RadioButton()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TBoxPOS1 = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TBoxPOS2 = New System.Windows.Forms.TextBox()
        Me.Btn_GMap = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.CBox_Place = New System.Windows.Forms.ComboBox()
        Me.txtAddress = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.CheckBox_2ndMonth = New System.Windows.Forms.CheckBox()
        Me.Btn_About = New System.Windows.Forms.Button()
        Me.Btn_Import = New System.Windows.Forms.Button()
        Me.Btn_Export = New System.Windows.Forms.Button()
        Me.BtnExportYear = New System.Windows.Forms.Button()
        Me.BtnGTsearch = New System.Windows.Forms.Button()
        Me.GroupBox_FM.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox_Gwa.SuspendLayout()
        Me.GroupBox_23H.SuspendLayout()
        Me.GroupBox_West.SuspendLayout()
        Me.GroupBox_MingKon.SuspendLayout()
        Me.GroupBox_StarB.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.Label1.Font = New System.Drawing.Font("新細明體", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label1.Location = New System.Drawing.Point(28, 35)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(52, 21)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "編號"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.Label2.Font = New System.Drawing.Font("新細明體", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label2.Location = New System.Drawing.Point(28, 65)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 21)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "姓名"
        '
        'TBoxNumber
        '
        Me.TBoxNumber.Location = New System.Drawing.Point(91, 35)
        Me.TBoxNumber.MaxLength = 10
        Me.TBoxNumber.Name = "TBoxNumber"
        Me.TBoxNumber.Size = New System.Drawing.Size(104, 22)
        Me.TBoxNumber.TabIndex = 0
        '
        'TBoxName
        '
        Me.TBoxName.Location = New System.Drawing.Point(91, 65)
        Me.TBoxName.Name = "TBoxName"
        Me.TBoxName.Size = New System.Drawing.Size(104, 22)
        Me.TBoxName.TabIndex = 6
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.Label7.Font = New System.Drawing.Font("新細明體", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label7.Location = New System.Drawing.Point(244, 98)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(31, 21)
        Me.Label7.TabIndex = 8
        Me.Label7.Text = "年"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.Label8.Font = New System.Drawing.Font("新細明體", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label8.Location = New System.Drawing.Point(346, 97)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(31, 21)
        Me.Label8.TabIndex = 9
        Me.Label8.Text = "月"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.Label9.Font = New System.Drawing.Font("新細明體", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label9.Location = New System.Drawing.Point(404, 97)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(31, 21)
        Me.Label9.TabIndex = 10
        Me.Label9.Text = "日"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.Label10.Font = New System.Drawing.Font("新細明體", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label10.Location = New System.Drawing.Point(463, 97)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(31, 21)
        Me.Label10.TabIndex = 11
        Me.Label10.Text = "時"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.Label11.Font = New System.Drawing.Font("新細明體", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label11.Location = New System.Drawing.Point(519, 97)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(31, 21)
        Me.Label11.TabIndex = 12
        Me.Label11.Text = "分"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.Label12.Font = New System.Drawing.Font("新細明體", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label12.Location = New System.Drawing.Point(28, 98)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(52, 21)
        Me.Label12.TabIndex = 13
        Me.Label12.Text = "生辰"
        '
        'CBcalender
        '
        Me.CBcalender.DisplayMember = "1"
        Me.CBcalender.FormattingEnabled = True
        Me.CBcalender.Items.AddRange(New Object() {"陽曆", "陰曆"})
        Me.CBcalender.Location = New System.Drawing.Point(91, 97)
        Me.CBcalender.Name = "CBcalender"
        Me.CBcalender.Size = New System.Drawing.Size(68, 20)
        Me.CBcalender.TabIndex = 16
        '
        'TBoxYear
        '
        Me.TBoxYear.Location = New System.Drawing.Point(208, 98)
        Me.TBoxYear.Name = "TBoxYear"
        Me.TBoxYear.Size = New System.Drawing.Size(38, 22)
        Me.TBoxYear.TabIndex = 17
        '
        'TBoxMonth
        '
        Me.TBoxMonth.Location = New System.Drawing.Point(313, 97)
        Me.TBoxMonth.Name = "TBoxMonth"
        Me.TBoxMonth.Size = New System.Drawing.Size(33, 22)
        Me.TBoxMonth.TabIndex = 18
        '
        'TBoxDay
        '
        Me.TBoxDay.Location = New System.Drawing.Point(374, 97)
        Me.TBoxDay.Name = "TBoxDay"
        Me.TBoxDay.Size = New System.Drawing.Size(31, 22)
        Me.TBoxDay.TabIndex = 19
        '
        'TBoxHour
        '
        Me.TBoxHour.Location = New System.Drawing.Point(432, 97)
        Me.TBoxHour.Name = "TBoxHour"
        Me.TBoxHour.Size = New System.Drawing.Size(32, 22)
        Me.TBoxHour.TabIndex = 20
        '
        'TBoxMin
        '
        Me.TBoxMin.Location = New System.Drawing.Point(492, 97)
        Me.TBoxMin.Name = "TBoxMin"
        Me.TBoxMin.Size = New System.Drawing.Size(27, 22)
        Me.TBoxMin.TabIndex = 21
        '
        'Button_clr
        '
        Me.Button_clr.Location = New System.Drawing.Point(18, 191)
        Me.Button_clr.Name = "Button_clr"
        Me.Button_clr.Size = New System.Drawing.Size(64, 30)
        Me.Button_clr.TabIndex = 23
        Me.Button_clr.Text = "清　除"
        Me.Button_clr.UseVisualStyleBackColor = True
        '
        'Btn_NameCheck
        '
        Me.Btn_NameCheck.Location = New System.Drawing.Point(298, 225)
        Me.Btn_NameCheck.Name = "Btn_NameCheck"
        Me.Btn_NameCheck.Size = New System.Drawing.Size(64, 30)
        Me.Btn_NameCheck.TabIndex = 24
        Me.Btn_NameCheck.Text = "姓名查詢"
        Me.Btn_NameCheck.UseVisualStyleBackColor = True
        '
        'Button_cal
        '
        Me.Button_cal.Location = New System.Drawing.Point(372, 191)
        Me.Button_cal.Name = "Button_cal"
        Me.Button_cal.Size = New System.Drawing.Size(82, 64)
        Me.Button_cal.TabIndex = 25
        Me.Button_cal.Text = "計　算"
        Me.Button_cal.UseVisualStyleBackColor = True
        '
        'Btn_Insert
        '
        Me.Btn_Insert.Location = New System.Drawing.Point(18, 225)
        Me.Btn_Insert.Name = "Btn_Insert"
        Me.Btn_Insert.Size = New System.Drawing.Size(64, 30)
        Me.Btn_Insert.TabIndex = 26
        Me.Btn_Insert.Text = "新增資料"
        Me.Btn_Insert.UseVisualStyleBackColor = True
        '
        'Button_exit
        '
        Me.Button_exit.Location = New System.Drawing.Point(463, 191)
        Me.Button_exit.Name = "Button_exit"
        Me.Button_exit.Size = New System.Drawing.Size(87, 64)
        Me.Button_exit.TabIndex = 27
        Me.Button_exit.Text = "結束"
        Me.Button_exit.UseVisualStyleBackColor = True
        '
        'Btn_Test
        '
        Me.Btn_Test.Font = New System.Drawing.Font("新細明體", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Btn_Test.Location = New System.Drawing.Point(557, 70)
        Me.Btn_Test.Name = "Btn_Test"
        Me.Btn_Test.Size = New System.Drawing.Size(36, 30)
        Me.Btn_Test.TabIndex = 28
        Me.Btn_Test.Text = "直接測試"
        Me.Btn_Test.UseVisualStyleBackColor = True
        Me.Btn_Test.Visible = False
        '
        'Btn_LoopTest
        '
        Me.Btn_LoopTest.Font = New System.Drawing.Font("新細明體", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Btn_LoopTest.Location = New System.Drawing.Point(556, 99)
        Me.Btn_LoopTest.Name = "Btn_LoopTest"
        Me.Btn_LoopTest.Size = New System.Drawing.Size(36, 30)
        Me.Btn_LoopTest.TabIndex = 29
        Me.Btn_LoopTest.Text = "迴圈測試"
        Me.Btn_LoopTest.UseVisualStyleBackColor = True
        Me.Btn_LoopTest.Visible = False
        '
        'GroupBox_FM
        '
        Me.GroupBox_FM.Controls.Add(Me.RadioButton_F)
        Me.GroupBox_FM.Controls.Add(Me.RadioButton_M)
        Me.GroupBox_FM.Location = New System.Drawing.Point(207, 35)
        Me.GroupBox_FM.Name = "GroupBox_FM"
        Me.GroupBox_FM.Size = New System.Drawing.Size(43, 50)
        Me.GroupBox_FM.TabIndex = 30
        Me.GroupBox_FM.TabStop = False
        Me.GroupBox_FM.Text = "性別"
        '
        'RadioButton_F
        '
        Me.RadioButton_F.AutoSize = True
        Me.RadioButton_F.Location = New System.Drawing.Point(4, 31)
        Me.RadioButton_F.Name = "RadioButton_F"
        Me.RadioButton_F.Size = New System.Drawing.Size(35, 16)
        Me.RadioButton_F.TabIndex = 1
        Me.RadioButton_F.Text = "女"
        Me.RadioButton_F.UseVisualStyleBackColor = True
        '
        'RadioButton_M
        '
        Me.RadioButton_M.AutoSize = True
        Me.RadioButton_M.Checked = True
        Me.RadioButton_M.Location = New System.Drawing.Point(4, 14)
        Me.RadioButton_M.Name = "RadioButton_M"
        Me.RadioButton_M.Size = New System.Drawing.Size(35, 16)
        Me.RadioButton_M.TabIndex = 0
        Me.RadioButton_M.TabStop = True
        Me.RadioButton_M.Text = "男"
        Me.RadioButton_M.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.PictureBox1.Location = New System.Drawing.Point(18, 21)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(556, 168)
        Me.PictureBox1.TabIndex = 31
        Me.PictureBox1.TabStop = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.Label3.Font = New System.Drawing.Font("新細明體", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label3.Location = New System.Drawing.Point(157, 98)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(52, 21)
        Me.Label3.TabIndex = 32
        Me.Label3.Text = "民國"
        '
        'Btn_NumberGet
        '
        Me.Btn_NumberGet.Location = New System.Drawing.Point(298, 191)
        Me.Btn_NumberGet.Name = "Btn_NumberGet"
        Me.Btn_NumberGet.Size = New System.Drawing.Size(64, 30)
        Me.Btn_NumberGet.TabIndex = 35
        Me.Btn_NumberGet.Text = "編號查詢"
        Me.Btn_NumberGet.UseVisualStyleBackColor = True
        '
        'Btn_Update
        '
        Me.Btn_Update.Location = New System.Drawing.Point(88, 191)
        Me.Btn_Update.Name = "Btn_Update"
        Me.Btn_Update.Size = New System.Drawing.Size(64, 30)
        Me.Btn_Update.TabIndex = 36
        Me.Btn_Update.Text = "修正資料"
        Me.Btn_Update.UseVisualStyleBackColor = True
        '
        'Btn_NoUp
        '
        Me.Btn_NoUp.Location = New System.Drawing.Point(158, 191)
        Me.Btn_NoUp.Name = "Btn_NoUp"
        Me.Btn_NoUp.Size = New System.Drawing.Size(64, 30)
        Me.Btn_NoUp.TabIndex = 38
        Me.Btn_NoUp.Text = "上一編號"
        Me.Btn_NoUp.UseVisualStyleBackColor = True
        '
        'Btn_NoDown
        '
        Me.Btn_NoDown.Location = New System.Drawing.Point(158, 225)
        Me.Btn_NoDown.Name = "Btn_NoDown"
        Me.Btn_NoDown.Size = New System.Drawing.Size(64, 30)
        Me.Btn_NoDown.TabIndex = 39
        Me.Btn_NoDown.Text = "下一編號"
        Me.Btn_NoDown.UseVisualStyleBackColor = True
        '
        'Btn_First
        '
        Me.Btn_First.Location = New System.Drawing.Point(228, 191)
        Me.Btn_First.Name = "Btn_First"
        Me.Btn_First.Size = New System.Drawing.Size(64, 30)
        Me.Btn_First.TabIndex = 40
        Me.Btn_First.Text = "第一編號"
        Me.Btn_First.UseVisualStyleBackColor = True
        '
        'Btn_Last
        '
        Me.Btn_Last.Location = New System.Drawing.Point(228, 225)
        Me.Btn_Last.Name = "Btn_Last"
        Me.Btn_Last.Size = New System.Drawing.Size(64, 30)
        Me.Btn_Last.TabIndex = 41
        Me.Btn_Last.Text = "最後編號"
        Me.Btn_Last.UseVisualStyleBackColor = True
        '
        'Btn_Delete
        '
        Me.Btn_Delete.Location = New System.Drawing.Point(88, 225)
        Me.Btn_Delete.Name = "Btn_Delete"
        Me.Btn_Delete.Size = New System.Drawing.Size(64, 30)
        Me.Btn_Delete.TabIndex = 37
        Me.Btn_Delete.Text = "刪除資料"
        Me.Btn_Delete.UseVisualStyleBackColor = True
        '
        'Btn_UpdateSAM
        '
        Me.Btn_UpdateSAM.Font = New System.Drawing.Font("新細明體", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Btn_UpdateSAM.Location = New System.Drawing.Point(557, 43)
        Me.Btn_UpdateSAM.Name = "Btn_UpdateSAM"
        Me.Btn_UpdateSAM.Size = New System.Drawing.Size(36, 30)
        Me.Btn_UpdateSAM.TabIndex = 42
        Me.Btn_UpdateSAM.Text = "換萬年曆"
        Me.Btn_UpdateSAM.UseVisualStyleBackColor = True
        Me.Btn_UpdateSAM.Visible = False
        '
        'BtnLBmsgClr
        '
        Me.BtnLBmsgClr.Font = New System.Drawing.Font("新細明體", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.BtnLBmsgClr.Location = New System.Drawing.Point(556, -1)
        Me.BtnLBmsgClr.Name = "BtnLBmsgClr"
        Me.BtnLBmsgClr.Size = New System.Drawing.Size(36, 30)
        Me.BtnLBmsgClr.TabIndex = 43
        Me.BtnLBmsgClr.Text = "清除訊息"
        Me.BtnLBmsgClr.UseVisualStyleBackColor = True
        Me.BtnLBmsgClr.Visible = False
        '
        'RichTextBox1
        '
        Me.RichTextBox1.BackColor = System.Drawing.SystemColors.Window
        Me.RichTextBox1.Location = New System.Drawing.Point(18, 261)
        Me.RichTextBox1.Name = "RichTextBox1"
        Me.RichTextBox1.Size = New System.Drawing.Size(556, 106)
        Me.RichTextBox1.TabIndex = 44
        Me.RichTextBox1.Text = ""
        '
        'GroupBox_Gwa
        '
        Me.GroupBox_Gwa.Controls.Add(Me.RB_Gwa2)
        Me.GroupBox_Gwa.Controls.Add(Me.RB_Gwa1)
        Me.GroupBox_Gwa.Location = New System.Drawing.Point(512, 35)
        Me.GroupBox_Gwa.Name = "GroupBox_Gwa"
        Me.GroupBox_Gwa.Size = New System.Drawing.Size(42, 51)
        Me.GroupBox_Gwa.TabIndex = 45
        Me.GroupBox_Gwa.TabStop = False
        Me.GroupBox_Gwa.Text = "卦"
        '
        'RB_Gwa2
        '
        Me.RB_Gwa2.AutoSize = True
        Me.RB_Gwa2.Checked = True
        Me.RB_Gwa2.Location = New System.Drawing.Point(7, 31)
        Me.RB_Gwa2.Name = "RB_Gwa2"
        Me.RB_Gwa2.Size = New System.Drawing.Size(29, 16)
        Me.RB_Gwa2.TabIndex = 1
        Me.RB_Gwa2.TabStop = True
        Me.RB_Gwa2.Text = "2"
        Me.RB_Gwa2.UseVisualStyleBackColor = True
        '
        'RB_Gwa1
        '
        Me.RB_Gwa1.AutoSize = True
        Me.RB_Gwa1.Location = New System.Drawing.Point(7, 14)
        Me.RB_Gwa1.Name = "RB_Gwa1"
        Me.RB_Gwa1.Size = New System.Drawing.Size(29, 16)
        Me.RB_Gwa1.TabIndex = 0
        Me.RB_Gwa1.Text = "1"
        Me.RB_Gwa1.UseVisualStyleBackColor = True
        '
        'GroupBox_23H
        '
        Me.GroupBox_23H.Controls.Add(Me.RB_23hNext)
        Me.GroupBox_23H.Controls.Add(Me.RB_23hSame)
        Me.GroupBox_23H.Location = New System.Drawing.Point(253, 35)
        Me.GroupBox_23H.Name = "GroupBox_23H"
        Me.GroupBox_23H.Size = New System.Drawing.Size(61, 50)
        Me.GroupBox_23H.TabIndex = 46
        Me.GroupBox_23H.TabStop = False
        Me.GroupBox_23H.Text = "過23時"
        '
        'RB_23hNext
        '
        Me.RB_23hNext.AutoSize = True
        Me.RB_23hNext.Checked = True
        Me.RB_23hNext.Location = New System.Drawing.Point(7, 31)
        Me.RB_23hNext.Name = "RB_23hNext"
        Me.RB_23hNext.Size = New System.Drawing.Size(47, 16)
        Me.RB_23hNext.TabIndex = 1
        Me.RB_23hNext.TabStop = True
        Me.RB_23hNext.Text = "隔日"
        Me.RB_23hNext.UseVisualStyleBackColor = True
        '
        'RB_23hSame
        '
        Me.RB_23hSame.AutoSize = True
        Me.RB_23hSame.Location = New System.Drawing.Point(7, 13)
        Me.RB_23hSame.Name = "RB_23hSame"
        Me.RB_23hSame.Size = New System.Drawing.Size(47, 16)
        Me.RB_23hSame.TabIndex = 0
        Me.RB_23hSame.Text = "同日"
        Me.RB_23hSame.UseVisualStyleBackColor = True
        '
        'GroupBox_West
        '
        Me.GroupBox_West.Controls.Add(Me.TBoxWestYear)
        Me.GroupBox_West.Location = New System.Drawing.Point(408, 35)
        Me.GroupBox_West.Name = "GroupBox_West"
        Me.GroupBox_West.Size = New System.Drawing.Size(59, 50)
        Me.GroupBox_West.TabIndex = 47
        Me.GroupBox_West.TabStop = False
        Me.GroupBox_West.Text = "星座　西移年"
        '
        'TBoxWestYear
        '
        Me.TBoxWestYear.Location = New System.Drawing.Point(11, 23)
        Me.TBoxWestYear.Name = "TBoxWestYear"
        Me.TBoxWestYear.Size = New System.Drawing.Size(35, 22)
        Me.TBoxWestYear.TabIndex = 0
        Me.TBoxWestYear.Text = "2000"
        '
        'GroupBox_MingKon
        '
        Me.GroupBox_MingKon.Controls.Add(Me.RB_MingKon3)
        Me.GroupBox_MingKon.Controls.Add(Me.RB_MingKon2)
        Me.GroupBox_MingKon.Controls.Add(Me.RB_MingKon1)
        Me.GroupBox_MingKon.Location = New System.Drawing.Point(317, 35)
        Me.GroupBox_MingKon.Name = "GroupBox_MingKon"
        Me.GroupBox_MingKon.Size = New System.Drawing.Size(88, 50)
        Me.GroupBox_MingKon.TabIndex = 48
        Me.GroupBox_MingKon.TabStop = False
        Me.GroupBox_MingKon.Text = "命宮算法"
        '
        'RB_MingKon3
        '
        Me.RB_MingKon3.AutoSize = True
        Me.RB_MingKon3.Checked = True
        Me.RB_MingKon3.Location = New System.Drawing.Point(50, 14)
        Me.RB_MingKon3.Name = "RB_MingKon3"
        Me.RB_MingKon3.Size = New System.Drawing.Size(35, 16)
        Me.RB_MingKon3.TabIndex = 2
        Me.RB_MingKon3.TabStop = True
        Me.RB_MingKon3.Text = "數"
        Me.RB_MingKon3.UseVisualStyleBackColor = True
        '
        'RB_MingKon2
        '
        Me.RB_MingKon2.AutoSize = True
        Me.RB_MingKon2.Location = New System.Drawing.Point(7, 30)
        Me.RB_MingKon2.Name = "RB_MingKon2"
        Me.RB_MingKon2.Size = New System.Drawing.Size(47, 16)
        Me.RB_MingKon2.TabIndex = 1
        Me.RB_MingKon2.Text = "月將"
        Me.RB_MingKon2.UseVisualStyleBackColor = True
        '
        'RB_MingKon1
        '
        Me.RB_MingKon1.AutoSize = True
        Me.RB_MingKon1.Location = New System.Drawing.Point(7, 14)
        Me.RB_MingKon1.Name = "RB_MingKon1"
        Me.RB_MingKon1.Size = New System.Drawing.Size(35, 16)
        Me.RB_MingKon1.TabIndex = 0
        Me.RB_MingKon1.Text = "書"
        Me.RB_MingKon1.UseVisualStyleBackColor = True
        '
        'GroupBox_StarB
        '
        Me.GroupBox_StarB.Controls.Add(Me.RB_StarB2)
        Me.GroupBox_StarB.Controls.Add(Me.RB_StarB1)
        Me.GroupBox_StarB.Location = New System.Drawing.Point(469, 35)
        Me.GroupBox_StarB.Name = "GroupBox_StarB"
        Me.GroupBox_StarB.Size = New System.Drawing.Size(41, 51)
        Me.GroupBox_StarB.TabIndex = 49
        Me.GroupBox_StarB.TabStop = False
        Me.GroupBox_StarB.Text = "星B"
        '
        'RB_StarB2
        '
        Me.RB_StarB2.AutoSize = True
        Me.RB_StarB2.Checked = True
        Me.RB_StarB2.Location = New System.Drawing.Point(7, 31)
        Me.RB_StarB2.Name = "RB_StarB2"
        Me.RB_StarB2.Size = New System.Drawing.Size(29, 16)
        Me.RB_StarB2.TabIndex = 1
        Me.RB_StarB2.TabStop = True
        Me.RB_StarB2.Text = "2"
        Me.RB_StarB2.UseVisualStyleBackColor = True
        '
        'RB_StarB1
        '
        Me.RB_StarB1.AutoSize = True
        Me.RB_StarB1.Location = New System.Drawing.Point(7, 16)
        Me.RB_StarB1.Name = "RB_StarB1"
        Me.RB_StarB1.Size = New System.Drawing.Size(29, 16)
        Me.RB_StarB1.TabIndex = 0
        Me.RB_StarB1.Text = "1"
        Me.RB_StarB1.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.Label4.Font = New System.Drawing.Font("新細明體", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label4.Location = New System.Drawing.Point(28, 128)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(73, 21)
        Me.Label4.TabIndex = 50
        Me.Label4.Text = "出生地"
        '
        'TBoxPOS1
        '
        Me.TBoxPOS1.Location = New System.Drawing.Point(247, 129)
        Me.TBoxPOS1.MaxLength = 10
        Me.TBoxPOS1.Name = "TBoxPOS1"
        Me.TBoxPOS1.Size = New System.Drawing.Size(66, 22)
        Me.TBoxPOS1.TabIndex = 51
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.Label5.Font = New System.Drawing.Font("新細明體", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label5.Location = New System.Drawing.Point(323, 129)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(52, 21)
        Me.Label5.TabIndex = 52
        Me.Label5.Text = "經度"
        '
        'TBoxPOS2
        '
        Me.TBoxPOS2.Location = New System.Drawing.Point(376, 129)
        Me.TBoxPOS2.MaxLength = 10
        Me.TBoxPOS2.Name = "TBoxPOS2"
        Me.TBoxPOS2.Size = New System.Drawing.Size(66, 22)
        Me.TBoxPOS2.TabIndex = 53
        '
        'Btn_GMap
        '
        Me.Btn_GMap.Location = New System.Drawing.Point(448, 125)
        Me.Btn_GMap.Name = "Btn_GMap"
        Me.Btn_GMap.Size = New System.Drawing.Size(64, 30)
        Me.Btn_GMap.TabIndex = 55
        Me.Btn_GMap.Text = "找經緯度"
        Me.Btn_GMap.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.Label6.Font = New System.Drawing.Font("新細明體", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label6.Location = New System.Drawing.Point(196, 129)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(52, 21)
        Me.Label6.TabIndex = 56
        Me.Label6.Text = "緯度"
        '
        'CBox_Place
        '
        Me.CBox_Place.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.CBox_Place.DisplayMember = "1"
        Me.CBox_Place.FormattingEnabled = True
        Me.CBox_Place.Items.AddRange(New Object() {"(輸入...)", "台北市", "高雄市", "基隆市", "新竹市", "台中市", "台南市", "新北市", "宜蘭縣市", "桃園縣市", "新竹縣", "苗栗縣市", "台中縣", "彰化縣市", "南投縣市", "雲林縣", "嘉義縣市", "台南縣", "高雄縣", "屏東縣市", "花蓮縣市", "台東縣市", "綠島", "蘭嶼", "澎湖縣", "東引島", "馬祖縣", "金門縣"})
        Me.CBox_Place.Location = New System.Drawing.Point(107, 129)
        Me.CBox_Place.Name = "CBox_Place"
        Me.CBox_Place.Size = New System.Drawing.Size(88, 20)
        Me.CBox_Place.TabIndex = 57
        '
        'txtAddress
        '
        Me.txtAddress.BackColor = System.Drawing.SystemColors.Window
        Me.txtAddress.Location = New System.Drawing.Point(91, 158)
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.Size = New System.Drawing.Size(421, 22)
        Me.txtAddress.TabIndex = 58
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.Label13.Font = New System.Drawing.Font("新細明體", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label13.Location = New System.Drawing.Point(30, 159)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(52, 21)
        Me.Label13.TabIndex = 59
        Me.Label13.Text = "地址"
        '
        'CheckBox_2ndMonth
        '
        Me.CheckBox_2ndMonth.AutoSize = True
        Me.CheckBox_2ndMonth.Font = New System.Drawing.Font("新細明體", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.CheckBox_2ndMonth.Location = New System.Drawing.Point(274, 100)
        Me.CheckBox_2ndMonth.Name = "CheckBox_2ndMonth"
        Me.CheckBox_2ndMonth.Size = New System.Drawing.Size(36, 16)
        Me.CheckBox_2ndMonth.TabIndex = 60
        Me.CheckBox_2ndMonth.Text = "閏"
        Me.CheckBox_2ndMonth.UseVisualStyleBackColor = True
        Me.CheckBox_2ndMonth.Visible = False
        '
        'Btn_About
        '
        Me.Btn_About.Location = New System.Drawing.Point(519, 129)
        Me.Btn_About.Name = "Btn_About"
        Me.Btn_About.Size = New System.Drawing.Size(44, 50)
        Me.Btn_About.TabIndex = 61
        Me.Btn_About.Text = "關於"
        Me.Btn_About.UseVisualStyleBackColor = True
        '
        'Btn_Import
        '
        Me.Btn_Import.Location = New System.Drawing.Point(557, 236)
        Me.Btn_Import.Name = "Btn_Import"
        Me.Btn_Import.Size = New System.Drawing.Size(37, 30)
        Me.Btn_Import.TabIndex = 62
        Me.Btn_Import.Text = "匯入"
        Me.Btn_Import.UseVisualStyleBackColor = True
        Me.Btn_Import.Visible = False
        '
        'Btn_Export
        '
        Me.Btn_Export.Location = New System.Drawing.Point(557, 264)
        Me.Btn_Export.Name = "Btn_Export"
        Me.Btn_Export.Size = New System.Drawing.Size(37, 30)
        Me.Btn_Export.TabIndex = 63
        Me.Btn_Export.Text = "匯出"
        Me.Btn_Export.UseVisualStyleBackColor = True
        Me.Btn_Export.Visible = False
        '
        'BtnExportYear
        '
        Me.BtnExportYear.Font = New System.Drawing.Font("新細明體", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.BtnExportYear.Location = New System.Drawing.Point(557, 143)
        Me.BtnExportYear.Name = "BtnExportYear"
        Me.BtnExportYear.Size = New System.Drawing.Size(36, 30)
        Me.BtnExportYear.TabIndex = 64
        Me.BtnExportYear.Text = "匯出年曆"
        Me.BtnExportYear.UseVisualStyleBackColor = True
        Me.BtnExportYear.Visible = False
        '
        'BtnGTsearch
        '
        Me.BtnGTsearch.Font = New System.Drawing.Font("新細明體", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.BtnGTsearch.Location = New System.Drawing.Point(556, 191)
        Me.BtnGTsearch.Name = "BtnGTsearch"
        Me.BtnGTsearch.Size = New System.Drawing.Size(36, 30)
        Me.BtnGTsearch.TabIndex = 65
        Me.BtnGTsearch.Text = "干支尋日"
        Me.BtnGTsearch.UseVisualStyleBackColor = True
        Me.BtnGTsearch.Visible = False
        '
        'FrmFate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.Fate.My.Resources.Resources.Back001
        Me.ClientSize = New System.Drawing.Size(592, 385)
        Me.Controls.Add(Me.BtnGTsearch)
        Me.Controls.Add(Me.BtnExportYear)
        Me.Controls.Add(Me.Btn_Export)
        Me.Controls.Add(Me.Btn_Import)
        Me.Controls.Add(Me.Btn_About)
        Me.Controls.Add(Me.CheckBox_2ndMonth)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.txtAddress)
        Me.Controls.Add(Me.CBox_Place)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Btn_GMap)
        Me.Controls.Add(Me.TBoxPOS2)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.TBoxPOS1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.GroupBox_StarB)
        Me.Controls.Add(Me.GroupBox_MingKon)
        Me.Controls.Add(Me.GroupBox_West)
        Me.Controls.Add(Me.GroupBox_23H)
        Me.Controls.Add(Me.GroupBox_Gwa)
        Me.Controls.Add(Me.RichTextBox1)
        Me.Controls.Add(Me.BtnLBmsgClr)
        Me.Controls.Add(Me.Btn_UpdateSAM)
        Me.Controls.Add(Me.Btn_Last)
        Me.Controls.Add(Me.Btn_First)
        Me.Controls.Add(Me.Btn_NoDown)
        Me.Controls.Add(Me.Btn_NoUp)
        Me.Controls.Add(Me.Btn_Delete)
        Me.Controls.Add(Me.Btn_Update)
        Me.Controls.Add(Me.Btn_NumberGet)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.GroupBox_FM)
        Me.Controls.Add(Me.Btn_LoopTest)
        Me.Controls.Add(Me.Btn_Test)
        Me.Controls.Add(Me.Button_exit)
        Me.Controls.Add(Me.Btn_Insert)
        Me.Controls.Add(Me.Button_cal)
        Me.Controls.Add(Me.Btn_NameCheck)
        Me.Controls.Add(Me.Button_clr)
        Me.Controls.Add(Me.TBoxMin)
        Me.Controls.Add(Me.TBoxHour)
        Me.Controls.Add(Me.TBoxDay)
        Me.Controls.Add(Me.TBoxMonth)
        Me.Controls.Add(Me.TBoxYear)
        Me.Controls.Add(Me.CBcalender)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.TBoxName)
        Me.Controls.Add(Me.TBoxNumber)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.PictureBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "FrmFate"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "八字命造"
        Me.GroupBox_FM.ResumeLayout(False)
        Me.GroupBox_FM.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox_Gwa.ResumeLayout(False)
        Me.GroupBox_Gwa.PerformLayout()
        Me.GroupBox_23H.ResumeLayout(False)
        Me.GroupBox_23H.PerformLayout()
        Me.GroupBox_West.ResumeLayout(False)
        Me.GroupBox_West.PerformLayout()
        Me.GroupBox_MingKon.ResumeLayout(False)
        Me.GroupBox_MingKon.PerformLayout()
        Me.GroupBox_StarB.ResumeLayout(False)
        Me.GroupBox_StarB.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TBoxNumber As System.Windows.Forms.TextBox
    Friend WithEvents TBoxName As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents CBcalender As System.Windows.Forms.ComboBox
    Friend WithEvents TBoxYear As System.Windows.Forms.TextBox
    Friend WithEvents TBoxMonth As System.Windows.Forms.TextBox
    Friend WithEvents TBoxDay As System.Windows.Forms.TextBox
    Friend WithEvents TBoxHour As System.Windows.Forms.TextBox
    Friend WithEvents TBoxMin As System.Windows.Forms.TextBox
    Friend WithEvents Button_clr As System.Windows.Forms.Button
    Friend WithEvents Btn_NameCheck As System.Windows.Forms.Button
    Friend WithEvents Button_cal As System.Windows.Forms.Button
    Friend WithEvents Btn_Insert As System.Windows.Forms.Button
    Friend WithEvents Button_exit As System.Windows.Forms.Button
    Friend WithEvents Btn_Test As System.Windows.Forms.Button
    Friend WithEvents Btn_LoopTest As System.Windows.Forms.Button
    Friend WithEvents GroupBox_FM As System.Windows.Forms.GroupBox
    Friend WithEvents RadioButton_F As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton_M As System.Windows.Forms.RadioButton
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Btn_NumberGet As System.Windows.Forms.Button
    Friend WithEvents Btn_Update As System.Windows.Forms.Button
    Friend WithEvents Btn_NoUp As System.Windows.Forms.Button
    Friend WithEvents Btn_NoDown As System.Windows.Forms.Button
    Friend WithEvents Btn_First As System.Windows.Forms.Button
    Friend WithEvents Btn_Last As System.Windows.Forms.Button
    Friend WithEvents Btn_Delete As System.Windows.Forms.Button
    Friend WithEvents Btn_UpdateSAM As System.Windows.Forms.Button
    Friend WithEvents BtnLBmsgClr As System.Windows.Forms.Button
    Friend WithEvents RichTextBox1 As System.Windows.Forms.RichTextBox
    Friend WithEvents GroupBox_Gwa As System.Windows.Forms.GroupBox
    Friend WithEvents RB_Gwa2 As System.Windows.Forms.RadioButton
    Friend WithEvents RB_Gwa1 As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox_23H As System.Windows.Forms.GroupBox
    Friend WithEvents RB_23hNext As System.Windows.Forms.RadioButton
    Friend WithEvents RB_23hSame As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox_West As System.Windows.Forms.GroupBox
    Friend WithEvents TBoxWestYear As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox_MingKon As System.Windows.Forms.GroupBox
    Friend WithEvents RB_MingKon2 As System.Windows.Forms.RadioButton
    Friend WithEvents RB_MingKon1 As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox_StarB As System.Windows.Forms.GroupBox
    Friend WithEvents RB_StarB2 As System.Windows.Forms.RadioButton
    Friend WithEvents RB_StarB1 As System.Windows.Forms.RadioButton
    Friend WithEvents RB_MingKon3 As System.Windows.Forms.RadioButton
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TBoxPOS1 As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TBoxPOS2 As System.Windows.Forms.TextBox
    Friend WithEvents Btn_GMap As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents CBox_Place As System.Windows.Forms.ComboBox
    Friend WithEvents txtAddress As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents CheckBox_2ndMonth As System.Windows.Forms.CheckBox
    Friend WithEvents Btn_About As System.Windows.Forms.Button
    Friend WithEvents Btn_Import As System.Windows.Forms.Button
    Friend WithEvents Btn_Export As System.Windows.Forms.Button
    Friend WithEvents BtnExportYear As System.Windows.Forms.Button
    Friend WithEvents BtnGTsearch As System.Windows.Forms.Button
End Class
