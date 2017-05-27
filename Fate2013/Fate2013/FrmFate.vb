﻿Imports System
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Math

'For add google search 經緯度
Imports System.Net
Imports System.Web
Imports System.Collections.Generic
Imports System.ComponentModel


'主要的操作表單(物件)
Public Class FrmFate
    '---------------------------------
    '  Public area 
    '---------------------------------
    Public str12Ja As String() = New String(12) {"節", _
           "立春", "驚蟄", "清明", "立夏", "芒種", "小暑", _
           "立秋", "白露", "寒露", "立冬", "大雪", "小寒"}    'str12Ja(0)不使用
    Public str24Jachi As String() = New String(24) {"節氣", _
        "立春", "雨水", "驚蟄", "春分", "清明", "穀雨", _
        "立夏", "小滿", "芒種", "夏至", "小暑", "大暑", _
        "立秋", "處暑", "白露", "秋分", "寒露", "霜降", _
        "立冬", "小雪", "大雪", "冬至", "小寒", "大寒"}       'str24Jachi(0)不使用
    Public str24HTsu As String() = New String(23) { _
        "子", "丑", "丑", "寅", "寅", "卯", "卯", "辰", "辰", "巳", "巳", "午", _
        "午", "未", "未", "申", "申", "酉", "酉", "戌", "戌", "亥", "亥", "子"}
    '--------------------------------
    '表單輸入欄位
    Dim txtFld(10) As String
    '宣告 ListBox 裡要用的字串
    Dim LStr(10) As String
    '萬年曆更新副程式用的輸入欄位
    Dim FldTx(12) As String
    '----------------------------------
    '宣告連線的資料庫
    Dim DBconn As SqlConnection
    '宣告連線的字串
    Dim DBsrc As String
    '宣告SQL string
    Dim SQLstr As String
    '宣告放SQL command的變數
    Dim SQLcmd As SqlCommand
    '宣告放SQL Data Reader
    Dim DBreader As SqlDataReader
    '---------------------------------------
    '宣告變數for目前customer資料表總筆數
    Dim CustomerCount As Integer
    '宣告變數for目前SAM資料表總筆數
    Dim SAMCount As Integer
    '宣告變數for目前Jachi資料表總筆數
    Dim JachiCount As Integer

    '配六十四卦的方法
    Public GwaMethod As String
    '過23Hour的算法選擇
    Public H23Method As String
    '星座西移宮的年份(預設是2000)
    Public MoveWestYear As String
    '命宮算法選項(1=old program, 2=Ajack New)
    Public MingKonMethod As String
    '星座B的第二第五柱修正選項
    Public StarBJu2Ju5Method As String

    '20140516Add, 農曆輸入時,需輸入是否閏月
    Public IntercalaryMonth As Boolean

    'Ajack memo
    'ExecuteNonQuery 
    'a)Used mainly for action queries(insert,delete,update,create,alter,drop). 
    'b)returns an int value indicating the number of affected rows 
    'ExecuteScalar 
    'a)Used in queries where we have to read a single value (totla count, rows) 
    'b)returns an object 
    'ExecuteReader 
    'a)Used in queries where we have to read a complete row of the data 
    'b)It initializes a SqlDataReader/OleDbDataReader variable which 
    '  can then be used to read the data returned.

    '工程模式密碼
    'Dim EngModPW As String = "空白姓名"
    Dim EngModPW As String = "94382761"

    Public Sub UserMsg(ByVal TextMsg As String)
        RichTextBox1.SelectedText = ""
        RichTextBox1.AppendText(TextMsg & vbCrLf)
        RichTextBox1.ScrollToCaret()
    End Sub

    '判斷節氣字串是否屬於十二個節
    Public Function Is12Ja(ByVal JC As String) As Byte
        Dim lop As Integer
        Is12Ja = 0
        For lop = 1 To 12
            '查看是否屬於12個"節"?
            If InStr(JC, str12Ja(lop)) <> 0 Then
                Is12Ja = lop
                Exit For
            End If
        Next
    End Function

    Private Sub FrmFate_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        DBsrc = "Data Source=.\SQLEXPRESS;" ' 伺服器
        DBsrc += "AttachDbFilename=|DataDirectory|\Fate2013.mdf;" ' 資料庫路徑與名稱
        DBsrc += "Integrated Security=True;" ' 登入的帳號認證
        DBsrc += "User Instance=True" ' 新使用者執行
        Me.Refresh()
        UserMsg("Fate2013 running......")

        'initial主要命造變數
        Bir.Initinal()

        Try
            DBconn = New SqlConnection(DBsrc) ' 連線
            DBconn.Open() '開啟資料庫
            If DBconn.State = ConnectionState.Open Then
                UserMsg("Fate2013資料庫備妥....")
                '取得目前資料庫JaChi總筆數
                SQLstr = "select Count(*) From JaChi"
                SQLcmd = New SqlCommand(SQLstr, DBconn)
                JachiCount = SQLcmd.ExecuteScalar()
                SQLcmd.Dispose()
                UserMsg("Fate2013資料庫的JaChi資料表有 " & JachiCount & " 筆資料....")
                '取得目前資料庫SAM總筆數
                SQLstr = "select Count(*) From SAM"
                SQLcmd = New SqlCommand(SQLstr, DBconn)
                SAMCount = SQLcmd.ExecuteScalar()
                SQLcmd.Dispose()
                UserMsg("Fate2013資料庫的SAM資料表有 " & SAMCount & " 筆資料....")
                '取得目前資料庫customer總筆數
                SQLstr = "select Count(*) From Customer"
                SQLcmd = New SqlCommand(SQLstr, DBconn)
                CustomerCount = SQLcmd.ExecuteScalar()
                SQLcmd.Dispose()
                UserMsg("Fate2013資料庫的Customer資料表有 " & CustomerCount & " 筆資料....")

                '讓編號欄位預設值為新編號
                'TBoxNumber.Text = CustomerCount + 1
            End If
        Catch ex As SqlException
            UserMsg(ex.Message)
        End Try
        '預設其他輸入欄位
        All_In_Cllear()
    End Sub

    Private Sub TBoxNumber_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TBoxNumber.TextChanged
        '輸入:編號
        If IsNumeric(TBoxNumber.Text) Then
            If TBoxNumber.Text = Int(TBoxNumber.Text) Then
                '檢查此編號是否已存在資料庫
                If TBoxNumber.Text <= CustomerCount Then
                    '若已在資料庫,則顯示提示訊息於listBox中
                    UserMsg("此編號已經存在....")
                Else
                    '若不在資料庫,則使用資料庫新編號
                    UserMsg("編號預設成資料庫新編號....")
                    TBoxNumber.Text = CustomerCount + 1
                End If
            Else
                MessageBox.Show("欄位輸入必須為整數! 重設成資料庫新編號")
                TBoxNumber.Text = CustomerCount + 1
            End If
        Else
            MessageBox.Show("欄位輸入必須為數值! 重設成資料庫新編號")
            TBoxNumber.Text = CustomerCount + 1
        End If
        txtFld(0) = TBoxNumber.Text
    End Sub

    Private Sub TBoxName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TBoxName.TextChanged
        '輸入:姓名
        txtFld(1) = TBoxName.Text
        '檢查是否為工程模式密碼
        If txtFld(1) = EngModPW Then
            UserMsg("進入工程模式....")
            Btn_Test.Visible = True
            Btn_LoopTest.Visible = True
            Btn_UpdateSAM.Visible = True
            BtnLBmsgClr.Visible = True
            Btn_Import.Visible = True
            Btn_Export.Visible = True
            BtnExportYear.Visible = True
            BtnGTsearch.Visible = True
            TBoxName.Text = ""
            Bir.EngMode = True
        End If
    End Sub

    Private Sub TBoxYear_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TBoxYear.TextChanged
        '輸入:年
        If IsNumeric(TBoxYear.Text) Then
            If TBoxYear.Text = Int(TBoxYear.Text) Then
                If TBoxYear.Text > 160 Or TBoxYear.Text < 1 Then
                    MessageBox.Show("只可計算到民國160年,數值超過,重設為60")
                    TBoxYear.Text = 60
                End If
            Else
                MessageBox.Show("欄位輸入必須為整數! 重設成60")
                TBoxYear.Text = 60
            End If
        Else
            MessageBox.Show("欄位輸入必須為數值! 重設成60")
            TBoxYear.Text = 60
        End If
        txtFld(3) = TBoxYear.Text
    End Sub

    Private Sub TBoxMonth_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TBoxMonth.TextChanged
        '輸入:月
        If IsNumeric(TBoxMonth.Text) Then
            If TBoxMonth.Text = Int(TBoxMonth.Text) Then
                If TBoxMonth.Text > 12 Or TBoxMonth.Text < 1 Then
                    MessageBox.Show("月份(1-12),數值超過,重設為1")
                    TBoxMonth.Text = 1
                End If
            Else
                MessageBox.Show("欄位輸入必須為整數! 重設成1")
                TBoxMonth.Text = 1
            End If
        Else
            MessageBox.Show("欄位輸入必須為數值! 重設成1")
            TBoxMonth.Text = 1
        End If
        txtFld(4) = TBoxMonth.Text
    End Sub

    Private Sub TBoxDay_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TBoxDay.TextChanged
        '輸入:日
        If IsNumeric(TBoxDay.Text) Then
            If TBoxDay.Text = Int(TBoxDay.Text) Then
                If TBoxDay.Text > 31 Or TBoxDay.Text < 1 Then
                    MessageBox.Show("每月日數(1-31),數值超過,重設為1")
                    TBoxDay.Text = 1
                End If
            Else
                MessageBox.Show("欄位輸入必須為整數! 重設成1")
                TBoxDay.Text = 1
            End If
        Else
            MessageBox.Show("欄位輸入必須為數值! 重設成1")
            TBoxDay.Text = 1
        End If
        txtFld(5) = TBoxDay.Text
    End Sub

    Private Sub TextBox5_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TBoxHour.TextChanged
        '輸入:時
        If IsNumeric(TBoxHour.Text) Then
            If TBoxHour.Text = Int(TBoxHour.Text) Then
                If TBoxHour.Text > 59 Or TBoxHour.Text < 0 Then
                    MessageBox.Show("24小時制(0-23),數值超過,重設為0")
                    TBoxHour.Text = 0
                End If
            Else
                MessageBox.Show("欄位輸入必須為整數! 重設成0")
                TBoxHour.Text = 0
            End If
        Else
            MessageBox.Show("欄位輸入必須為數值! 重設成0")
            TBoxHour.Text = 0
        End If
        txtFld(6) = TBoxHour.Text
    End Sub

    Private Sub TBoxMin_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TBoxMin.TextChanged
        '輸入:分
        If IsNumeric(TBoxMin.Text) Then
            If TBoxMin.Text = Int(TBoxMin.Text) Then
                If TBoxMin.Text > 59 Or TBoxMin.Text < 0 Then
                    MessageBox.Show("60分鐘(0-59),數值超過,重設為15")
                    TBoxMin.Text = 15
                End If
            Else
                MessageBox.Show("欄位輸入必須為整數! 重設成15")
                TBoxMin.Text = 15
            End If
        Else
            MessageBox.Show("欄位輸入必須為數值! 重設成15")
            TBoxMin.Text = 15
        End If
        txtFld(7) = TBoxMin.Text
    End Sub

    Private Sub TBoxPOS1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TBoxPOS1.TextChanged
        txtFld(8) = TBoxPOS1.Text
    End Sub

    Private Sub TBoxPOS2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TBoxPOS2.TextChanged
        txtFld(9) = TBoxPOS2.Text
    End Sub

    Private Sub txtAddress_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAddress.TextChanged
        txtFld(10) = txtAddress.Text
    End Sub

    Private Sub RadioButton_M_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton_M.CheckedChanged
        FM_check()
    End Sub

    Private Sub RadioButton_F_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton_F.CheckedChanged
        FM_check()
    End Sub

    Private Sub FM_check()
        '確保性別欄位的選項與字串一致
        If RadioButton_M.Checked Then
            txtFld(2) = RadioButton_M.Text
        ElseIf RadioButton_F.Checked Then
            txtFld(2) = RadioButton_F.Text
        End If
    End Sub

    Private Sub RB_Gwa1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RB_Gwa1.CheckedChanged
        Gwa_check()
    End Sub

    Private Sub RB_Gwa2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RB_Gwa2.CheckedChanged
        Gwa_check()
    End Sub

    Private Sub Gwa_check()
        '算卦法選項
        If RB_Gwa1.Checked Then
            GwaMethod = RB_Gwa1.Text
        ElseIf RB_Gwa2.Checked Then
            GwaMethod = RB_Gwa2.Text
        End If
    End Sub

    Private Sub RB_23hNext_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RB_23hNext.CheckedChanged
        Hour23_check()
    End Sub

    Private Sub RB_23hSame_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RB_23hSame.CheckedChanged
        Hour23_check()
    End Sub

    Private Sub Hour23_check()
        '過23時的選項
        If RB_23hNext.Checked Then
            H23Method = RB_23hNext.Text
        ElseIf RB_23hSame.Checked Then
            H23Method = RB_23hSame.Text
        End If
    End Sub

    Private Sub TBoxWestYear_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TBoxWestYear.TextChanged
        '輸入:星座西移年
        If IsNumeric(TBoxWestYear.Text) Then
            If TBoxWestYear.Text = Int(TBoxWestYear.Text) Then
                If TBoxWestYear.Text > 3000 Then
                    MessageBox.Show("只可小於3000年,數值超過,重設為2000")
                    TBoxWestYear.Text = 2000
                End If
            Else
                MessageBox.Show("欄位輸入必須為整數! 重設成2000")
                TBoxWestYear.Text = 2000
            End If
        Else
            MessageBox.Show("欄位輸入必須為數值! 重設成2000")
            TBoxWestYear.Text = 2000
        End If
        MoveWestYear = CInt(TBoxWestYear.Text)
    End Sub

    Private Sub RB_MingKon2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RB_MingKon2.CheckedChanged
        MKmethod_Check()
    End Sub

    Private Sub RB_MingKon1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RB_MingKon1.CheckedChanged
        MKmethod_Check()
    End Sub

    Private Sub RB_MingKon3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RB_MingKon3.CheckedChanged
        MKmethod_Check()
    End Sub

    Private Sub MKmethod_Check()
        '命宮算法的選項
        If RB_MingKon1.Checked Then
            MingKonMethod = RB_MingKon1.Text
        ElseIf RB_MingKon2.Checked Then
            MingKonMethod = RB_MingKon2.Text
        ElseIf RB_MingKon3.Checked Then
            MingKonMethod = RB_MingKon3.Text
        End If
    End Sub

    Private Sub RB_StarB1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RB_StarB1.CheckedChanged
        StarB_check()
    End Sub

    Private Sub RB_StarB2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RB_StarB2.CheckedChanged
        StarB_check()
    End Sub

    Private Sub StarB_check()
        '星座B的第二第五柱修正選項
        If RB_StarB1.Checked Then
            StarBJu2Ju5Method = RB_StarB1.Text
        ElseIf RB_StarB2.Checked Then
            StarBJu2Ju5Method = RB_StarB2.Text
        End If
    End Sub

    Private Sub Button_clr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_clr.Click
        '清除按鈕
        All_In_Cllear()
    End Sub

    Private Sub All_In_Cllear()
        '清除按鈕
        TBoxNumber.Text = CustomerCount + 1
        TBoxName.Text = "空白姓名"
        TBoxYear.Text = Now.Year - 1911
        TBoxMonth.Text = Now.Month
        TBoxDay.Text = Now.Day
        TBoxHour.Text = Now.Hour
        TBoxMin.Text = 15
        RadioButton_F.Checked = False
        RadioButton_M.Checked = True
        RB_23hSame.Checked = True    '2選1
        RB_23hNext.Checked = False
        RB_MingKon1.Checked = False  '3選1
        RB_MingKon2.Checked = False
        RB_MingKon3.Checked = True
        RB_Gwa1.Checked = False    '2選1
        RB_Gwa2.Checked = True
        CBcalender.SelectedIndex = 0 '預設為國曆輸入
        CheckBox_2ndMonth.Visible = False
        IntercalaryMonth = False
        CBox_Place.SelectedIndex = 1 '預設為台北市
        FM_check()
        Gwa_check()
        Hour23_check()
        MKmethod_Check()
        StarB_check()
        TBoxName.Focus()
        UserMsg("資料輸入欄位已經清成預設(現在日期時分),請重新輸入....")
    End Sub

    Private Sub Button_exit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_exit.Click
        '結束按鈕-------------
        DBconn.Close() '關閉資料庫
        '儲存log檔------------
        RichTextBox1.SaveFile("Fate2013.log", RichTextBoxStreamType.PlainText)
        '結束程式
        Application.Exit()
    End Sub

    Private Sub Btn_NameCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_NameCheck.Click
        Dim cn As Integer
        cn = 0
        SQLstr = " Select * from Customer where 姓名='" & TBoxName.Text & "'"
        SQLcmd = New SqlCommand(SQLstr, DBconn)
        DBreader = SQLcmd.ExecuteReader()
        If DBreader.HasRows Then
            Do While DBreader.Read()
                cn = cn + 1
                LStr(0) = System.Convert.ToString(DBreader("編號"))
                LStr(1) = System.Convert.ToString(DBreader("姓名"))
                LStr(2) = System.Convert.ToString(DBreader("性別"))
                LStr(3) = System.Convert.ToString(DBreader("民國年"))
                LStr(4) = System.Convert.ToString(DBreader("月"))
                LStr(5) = System.Convert.ToString(DBreader("日"))
                LStr(6) = System.Convert.ToString(DBreader("時"))
                LStr(7) = System.Convert.ToString(DBreader("分"))
                LStr(8) = System.Convert.ToString(DBreader("緯度"))
                LStr(9) = System.Convert.ToString(DBreader("經度"))
                LStr(10) = System.Convert.ToString(DBreader("出生地"))
                UserMsg("編號=" & LStr(0) & "-" & LStr(1) & "," & LStr(2) & ",民國" & LStr(3) & "-" & LStr(4) & "-" _
                        & LStr(5) & "," & LStr(6) & "時" & LStr(7) & "分; 緯度" & LStr(8) & "經度" & LStr(9))
            Loop
        Else
            UserMsg("資料庫裡沒有符合 姓名=" + TBoxName.Text + "的資料")
        End If
        DBreader.Close()
        TBoxNumber.Text = LStr(0)
        TBoxName.Text = LStr(1)
        TBoxYear.Text = LStr(3)
        TBoxMonth.Text = LStr(4)
        TBoxDay.Text = LStr(5)
        TBoxHour.Text = LStr(6)
        TBoxMin.Text = LStr(7)
        TBoxPOS1.Text = LStr(8)
        TBoxPOS2.Text = LStr(9)
        txtAddress.Text = LStr(10)
        CBox_Place.SelectedIndex = 0 '預設為無資料
        If LStr(2) = "男" Then
            RadioButton_F.Checked = False
            RadioButton_M.Checked = True
        Else
            RadioButton_F.Checked = True
            RadioButton_M.Checked = False
        End If
        FM_check()
        UserMsg("資料庫中符合 姓名=" + TBoxName.Text + "的資料共有 " & cn & " 筆資料如上....")
        SQLcmd.Dispose()
    End Sub


    Private Sub Btn_NumberGet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_NumberGet.Click
        SQLstr = " Select * from Customer where 編號='" & TBoxNumber.Text & "'"
        SQLcmd = New SqlCommand(SQLstr, DBconn)
        DBreader = SQLcmd.ExecuteReader()
        If DBreader.HasRows Then
            Do While DBreader.Read()
                LStr(0) = System.Convert.ToString(DBreader("編號"))
                LStr(1) = System.Convert.ToString(DBreader("姓名"))
                LStr(2) = System.Convert.ToString(DBreader("性別"))
                LStr(3) = System.Convert.ToString(DBreader("民國年"))
                LStr(4) = System.Convert.ToString(DBreader("月"))
                LStr(5) = System.Convert.ToString(DBreader("日"))
                LStr(6) = System.Convert.ToString(DBreader("時"))
                LStr(7) = System.Convert.ToString(DBreader("分"))
                LStr(8) = System.Convert.ToString(DBreader("緯度"))
                LStr(9) = System.Convert.ToString(DBreader("經度"))
                LStr(10) = System.Convert.ToString(DBreader("出生地"))
                UserMsg("編號=" & LStr(0) & "-" & LStr(1) & "," & LStr(2) & ",民國" & LStr(3) & "-" & LStr(4) & "-" _
                        & LStr(5) & "," & LStr(6) & "時" & LStr(7) & "分; 緯度" & LStr(8) & "經度" & LStr(9))
            Loop
        Else
            UserMsg("資料庫裡沒有符合 編號=" + TBoxNumber.Text + "的資料")
        End If
        DBreader.Close()
        TBoxNumber.Text = LStr(0)
        TBoxName.Text = LStr(1)
        TBoxYear.Text = LStr(3)
        TBoxMonth.Text = LStr(4)
        TBoxDay.Text = LStr(5)
        TBoxHour.Text = LStr(6)
        TBoxMin.Text = LStr(7)
        TBoxPOS1.Text = LStr(8)
        TBoxPOS2.Text = LStr(9)
        txtAddress.Text = LStr(10)
        CBox_Place.SelectedIndex = 0 '預設為無資料
        If LStr(2) = "男" Then
            RadioButton_F.Checked = False
            RadioButton_M.Checked = True
        Else
            RadioButton_F.Checked = True
            RadioButton_M.Checked = False
        End If
        FM_check()
        UserMsg("資料庫中符合 編號=" + TBoxNumber.Text + "的資料如上....")
        SQLcmd.Dispose()
    End Sub


    Private Sub Btn_Insert_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Insert.Click
        Dim cn As Integer
        Dim mbx As Integer
        Dim x As Integer
        Dim YesSameName As Boolean
        Dim IsLunar As Boolean
        'Deafult Values
        YesSameName = False
        IsLunar = False

        '姓名空白則不動作
        If TBoxName.Text = "" Then
            UserMsg("姓名空白! 操作無效....")
        Else
            '檢查是否輸入農曆, (農曆資料要先計算,換成國曆之後,才可儲存)
            If CBcalender.SelectedIndex = 1 Then
                UserMsg("農曆資料!!! 要先計算,換成國曆之後,才可儲存....")
                mbx = MsgBox("農曆資料要先按計算,換成國曆之後,才可儲存", vbOKOnly + vbInformation, "農曆資料!")
                IsLunar = True
            End If
            '檢查新增資料的姓名,避免重複輸入造成錯誤
            SQLstr = "select count(*) from Customer where 姓名 = '" & TBoxName.Text & "'"
            SQLcmd = New SqlCommand(SQLstr, DBconn)
            DBreader = SQLcmd.ExecuteReader()
            DBreader.Read()
            cn = DBreader.Item(0)
            DBreader.Close()
            SQLcmd.Dispose()
            '請user確認
            If cn <> 0 Then
                UserMsg("姓名重複! 請確認....")
                mbx = MsgBox("確定要新增此筆同名同姓資料?", vbYesNo + vbQuestion, "同名同姓")
                If mbx = vbYes Then
                    YesSameName = True
                End If
            End If
            'cn =0 means not found, insert the data 
            If (YesSameName Or cn = 0) And (Not IsLunar) Then
                SQLstr = "insert into Customer (姓名,性別,民國年,月,日,時,分,緯度,經度,出生地)values ('"
                SQLstr = SQLstr & txtFld(1) & "', '"
                SQLstr = SQLstr & txtFld(2) & "', "
                SQLstr = SQLstr & txtFld(3) & ","
                SQLstr = SQLstr & txtFld(4) & ","
                SQLstr = SQLstr & txtFld(5) & ","
                SQLstr = SQLstr & txtFld(6) & ","
                SQLstr = SQLstr & txtFld(7) & ", '"
                SQLstr = SQLstr & txtFld(8) & "', '"
                SQLstr = SQLstr & txtFld(9) & "', '"
                SQLstr = SQLstr & txtFld(10) & "')"
                SQLcmd = New SqlCommand(SQLstr, DBconn)
                x = SQLcmd.ExecuteNonQuery()
                If x <> 1 Then 'ExecuteNonQuery 傳回資料異動筆數
                    UserMsg("新增資料庫失敗! ")
                Else
                    UserMsg("新增一筆資料至資料庫成功! .....")
                    '設定資料表總筆數加一
                    CustomerCount = CustomerCount + 1
                    UserMsg("Fate2013資料庫的Customer資料表有 " & CustomerCount & " 筆資料....")
                End If
            Else
                UserMsg("放棄新增....")
            End If
            SQLcmd.Dispose()
        End If
    End Sub

    Private Sub Btn_Update_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Update.Click
        Dim mbx As Integer
        Dim x As Integer

        If txtFld(0) > CustomerCount Then
            '資料不存在
            UserMsg("目前編號並不存在. 操作無效....")
        Else
            '資料存在,請user確認要更新的資料
            UserMsg("所有內容正確嗎?確定更新此筆資料嗎? 請確認....")
            mbx = MsgBox("確定要更新此筆資料?", vbYesNo + vbQuestion, "更新")
            'Go! Update the data: 姓名,性別,民國年,月,日,時,分
            If mbx = vbYes Then
                SQLstr = "update Customer set 姓名='" & txtFld(1)
                SQLstr = SQLstr & "', 性別='" & txtFld(2)
                SQLstr = SQLstr & "', 民國年=" & txtFld(3)
                SQLstr = SQLstr & ",月=" & txtFld(4)
                SQLstr = SQLstr & ",日=" & txtFld(5)
                SQLstr = SQLstr & ",時=" & txtFld(6)
                SQLstr = SQLstr & ",分=" & txtFld(7)
                SQLstr = SQLstr & ",緯度='" & txtFld(8)
                SQLstr = SQLstr & "',經度='" & txtFld(9)
                SQLstr = SQLstr & "',出生地='" & txtFld(10)
                SQLstr = SQLstr & "' where 編號=" & txtFld(0)
                SQLcmd = New SqlCommand(SQLstr, DBconn)
                x = SQLcmd.ExecuteNonQuery()
                If x <> 1 Then 'ExecuteNonQuery 傳回資料異動筆數
                    UserMsg("更新資料庫失敗! ")
                Else
                    UserMsg("資料更新成功! .....")
                End If
            Else
                UserMsg("放棄更新....")
            End If
            SQLcmd.Dispose()
        End If
    End Sub

    Private Sub Btn_Delete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Delete.Click
        Dim mbx As Integer
        Dim x As Integer
        Dim DelNo As Integer

        '資料存在,請user確認要刪除的資料
        UserMsg("確定刪除此筆資料嗎? 請確認.....")
        mbx = MsgBox("確定要刪除此筆資料?", vbYesNo + vbQuestion, "刪除")
        'if yes then delete the data: 姓名,性別,民國年,月,日,時,分
        If mbx = vbYes Then
            DelNo = TBoxNumber.Text
            '修改後面資料的編號(Read-update-delete) ,確保編號連續
            'copy 後面資料向前一個編號,刪除最後一筆資料
            If DelNo < CustomerCount - 1 Then
                For index = DelNo + 1 To CustomerCount
                    '--------------Read (index)
                    SQLstr = " Select * from Customer where 編號='" & index & "'"
                    SQLcmd = New SqlCommand(SQLstr, DBconn)
                    DBreader = SQLcmd.ExecuteReader()
                    If DBreader.HasRows Then
                        Do While DBreader.Read()
                            LStr(0) = System.Convert.ToString(DBreader("編號"))
                            LStr(1) = System.Convert.ToString(DBreader("姓名"))
                            LStr(2) = System.Convert.ToString(DBreader("性別"))
                            LStr(3) = System.Convert.ToString(DBreader("民國年"))
                            LStr(4) = System.Convert.ToString(DBreader("月"))
                            LStr(5) = System.Convert.ToString(DBreader("日"))
                            LStr(6) = System.Convert.ToString(DBreader("時"))
                            LStr(7) = System.Convert.ToString(DBreader("分"))
                            LStr(8) = System.Convert.ToString(DBreader("緯度"))
                            LStr(9) = System.Convert.ToString(DBreader("經度"))
                            LStr(10) = System.Convert.ToString(DBreader("出生地"))
                            UserMsg("----移動後續編號=" & LStr(0))
                        Loop
                    Else
                        UserMsg("資料庫失敗!!!!!!!!!!!!!!!!! read 編號=" + Str(index) + "的資料")
                    End If
                    DBreader.Close()
                    '--------------Update (index-1)
                    SQLstr = "update Customer set 姓名='" & LStr(1)
                    SQLstr = SQLstr & "', 性別='" & LStr(2)
                    SQLstr = SQLstr & "', 民國年=" & LStr(3)
                    SQLstr = SQLstr & ",月=" & LStr(4)
                    SQLstr = SQLstr & ",日=" & LStr(5)
                    SQLstr = SQLstr & ",時=" & LStr(6)
                    SQLstr = SQLstr & ",分=" & LStr(7)
                    SQLstr = SQLstr & ",緯度='" & LStr(8)
                    SQLstr = SQLstr & "',經度='" & LStr(9)
                    SQLstr = SQLstr & "',出生地='" & LStr(10)
                    SQLstr = SQLstr & "' where 編號=" & index - 1
                    SQLcmd = New SqlCommand(SQLstr, DBconn)
                    x = SQLcmd.ExecuteNonQuery()
                    If x <> 1 Then 'ExecuteNonQuery 傳回資料異動筆數
                        UserMsg("資料庫失敗!!!!!!!!!!!!!!!!! update 編號=" + Str(index - 1) + "的資料")
                    End If
                Next
            End If
            'Real delete action ==> delete last one
            SQLstr = "delete from Customer where 編號=" & CustomerCount
            SQLcmd = New SqlCommand(SQLstr, DBconn)
            x = SQLcmd.ExecuteNonQuery()
            If x <> 1 Then 'ExecuteNonQuery 傳回資料異動筆數
                UserMsg("刪除資料庫失敗! ")
            Else
                UserMsg("資料刪除成功! .....")
            End If
            '總筆數--
            CustomerCount = CustomerCount - 1
            UserMsg("現在資料庫中有 " & CustomerCount & " 筆客戶資料....")
            '清除所有輸入欄位
            All_In_Cllear()
        Else
            UserMsg("放棄刪除....")
        End If
        SQLcmd.Dispose()
    End Sub

    Private Sub Btn_NoUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_NoUp.Click
        TBoxNumber.Text = TBoxNumber.Text - 1
        SQLstr = " Select * from Customer where 編號='" & TBoxNumber.Text & "'"
        SQLcmd = New SqlCommand(SQLstr, DBconn)
        DBreader = SQLcmd.ExecuteReader()
        If DBreader.HasRows Then
            Do While DBreader.Read()
                LStr(0) = System.Convert.ToString(DBreader("編號"))
                LStr(1) = System.Convert.ToString(DBreader("姓名"))
                LStr(2) = System.Convert.ToString(DBreader("性別"))
                LStr(3) = System.Convert.ToString(DBreader("民國年"))
                LStr(4) = System.Convert.ToString(DBreader("月"))
                LStr(5) = System.Convert.ToString(DBreader("日"))
                LStr(6) = System.Convert.ToString(DBreader("時"))
                LStr(7) = System.Convert.ToString(DBreader("分"))
                LStr(8) = System.Convert.ToString(DBreader("緯度"))
                LStr(9) = System.Convert.ToString(DBreader("經度"))
                LStr(10) = System.Convert.ToString(DBreader("出生地"))
                UserMsg("編號=" & LStr(0) & "-" & LStr(1) & "," & LStr(2) & ",民國" & LStr(3) & "-" & LStr(4) & "-" _
                        & LStr(5) & "," & LStr(6) & "時" & LStr(7) & "分; 緯度" & LStr(8) & "經度" & LStr(9))
            Loop
        Else
            UserMsg("資料庫裡沒有符合 編號=" + TBoxNumber.Text + "的資料")
        End If
        DBreader.Close()
        TBoxNumber.Text = LStr(0)
        TBoxName.Text = LStr(1)
        TBoxYear.Text = LStr(3)
        TBoxMonth.Text = LStr(4)
        TBoxDay.Text = LStr(5)
        TBoxHour.Text = LStr(6)
        TBoxMin.Text = LStr(7)
        TBoxPOS1.Text = LStr(8)
        TBoxPOS2.Text = LStr(9)
        txtAddress.Text = LStr(10)
        CBox_Place.SelectedIndex = 0 '預設為無資料
        If LStr(2) = "男" Then
            RadioButton_F.Checked = False
            RadioButton_M.Checked = True
        Else
            RadioButton_F.Checked = True
            RadioButton_M.Checked = False
        End If
        FM_check()
        UserMsg("資料庫中符合 編號=" + TBoxNumber.Text + "的資料如上....")
        SQLcmd.Dispose()
    End Sub

    Private Sub Btn_NoDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_NoDown.Click
        If TBoxNumber.Text = CustomerCount + 1 Then
            UserMsg("目前編號是新編號,無下一筆資料! 操作無效....")
        Else
            TBoxNumber.Text = TBoxNumber.Text + 1
            SQLstr = " Select * from Customer where 編號='" & TBoxNumber.Text & "'"
            SQLcmd = New SqlCommand(SQLstr, DBconn)
            DBreader = SQLcmd.ExecuteReader()
            If DBreader.HasRows Then
                Do While DBreader.Read()
                    LStr(0) = System.Convert.ToString(DBreader("編號"))
                    LStr(1) = System.Convert.ToString(DBreader("姓名"))
                    LStr(2) = System.Convert.ToString(DBreader("性別"))
                    LStr(3) = System.Convert.ToString(DBreader("民國年"))
                    LStr(4) = System.Convert.ToString(DBreader("月"))
                    LStr(5) = System.Convert.ToString(DBreader("日"))
                    LStr(6) = System.Convert.ToString(DBreader("時"))
                    LStr(7) = System.Convert.ToString(DBreader("分"))
                    LStr(8) = System.Convert.ToString(DBreader("緯度"))
                    LStr(9) = System.Convert.ToString(DBreader("經度"))
                    LStr(10) = System.Convert.ToString(DBreader("出生地"))
                    UserMsg("編號=" & LStr(0) & "-" & LStr(1) & "," & LStr(2) & ",民國" & LStr(3) & "-" & LStr(4) & "-" _
                            & LStr(5) & "," & LStr(6) & "時" & LStr(7) & "分; 緯度" & LStr(8) & "經度" & LStr(9))
                Loop
            Else
                UserMsg("資料庫裡沒有符合 編號=" + TBoxNumber.Text + "的資料")
            End If
            DBreader.Close()
            TBoxNumber.Text = LStr(0)
            TBoxName.Text = LStr(1)
            TBoxYear.Text = LStr(3)
            TBoxMonth.Text = LStr(4)
            TBoxDay.Text = LStr(5)
            TBoxHour.Text = LStr(6)
            TBoxMin.Text = LStr(7)
            TBoxPOS1.Text = LStr(8)
            TBoxPOS2.Text = LStr(9)
            txtAddress.Text = LStr(10)
            CBox_Place.SelectedIndex = 0 '預設為無資料
            If LStr(2) = "男" Then
                RadioButton_F.Checked = False
                RadioButton_M.Checked = True
            Else
                RadioButton_F.Checked = True
                RadioButton_M.Checked = False
            End If
            FM_check()
            UserMsg("資料庫中符合 編號=" + TBoxNumber.Text + "的資料如上....")
            SQLcmd.Dispose()
        End If
    End Sub

    Private Sub Btn_First_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_First.Click
        TBoxNumber.Text = 1
        SQLstr = " Select * from Customer where 編號='" & TBoxNumber.Text & "'"
        SQLcmd = New SqlCommand(SQLstr, DBconn)
        DBreader = SQLcmd.ExecuteReader()
        If DBreader.HasRows Then
            Do While DBreader.Read()
                LStr(0) = System.Convert.ToString(DBreader("編號"))
                LStr(1) = System.Convert.ToString(DBreader("姓名"))
                LStr(2) = System.Convert.ToString(DBreader("性別"))
                LStr(3) = System.Convert.ToString(DBreader("民國年"))
                LStr(4) = System.Convert.ToString(DBreader("月"))
                LStr(5) = System.Convert.ToString(DBreader("日"))
                LStr(6) = System.Convert.ToString(DBreader("時"))
                LStr(7) = System.Convert.ToString(DBreader("分"))
                LStr(8) = System.Convert.ToString(DBreader("緯度"))
                LStr(9) = System.Convert.ToString(DBreader("經度"))
                LStr(10) = System.Convert.ToString(DBreader("出生地"))
                UserMsg("編號=" & LStr(0) & "-" & LStr(1) & "," & LStr(2) & ",民國" & LStr(3) & "-" & LStr(4) & "-" _
                        & LStr(5) & "," & LStr(6) & "時" & LStr(7) & "分; 緯度" & LStr(8) & "經度" & LStr(9))
            Loop
        Else
            UserMsg("資料庫裡沒有符合 編號=" + TBoxNumber.Text + "的資料")
        End If
        DBreader.Close()
        TBoxNumber.Text = LStr(0)
        TBoxName.Text = LStr(1)
        TBoxYear.Text = LStr(3)
        TBoxMonth.Text = LStr(4)
        TBoxDay.Text = LStr(5)
        TBoxHour.Text = LStr(6)
        TBoxMin.Text = LStr(7)
        TBoxPOS1.Text = LStr(8)
        TBoxPOS2.Text = LStr(9)
        txtAddress.Text = LStr(10)
        CBox_Place.SelectedIndex = 0 '預設為無資料
        If LStr(2) = "男" Then
            RadioButton_F.Checked = False
            RadioButton_M.Checked = True
        Else
            RadioButton_F.Checked = True
            RadioButton_M.Checked = False
        End If
        FM_check()
        UserMsg("資料庫中符合 編號=" + TBoxNumber.Text + "的資料如上....")
        SQLcmd.Dispose()
    End Sub

    Private Sub Btn_Last_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Last.Click
        TBoxNumber.Text = CustomerCount
        SQLstr = " Select * from Customer where 編號='" & TBoxNumber.Text & "'"
        SQLcmd = New SqlCommand(SQLstr, DBconn)
        DBreader = SQLcmd.ExecuteReader()
        If DBreader.HasRows Then
            Do While DBreader.Read()
                LStr(0) = System.Convert.ToString(DBreader("編號"))
                LStr(1) = System.Convert.ToString(DBreader("姓名"))
                LStr(2) = System.Convert.ToString(DBreader("性別"))
                LStr(3) = System.Convert.ToString(DBreader("民國年"))
                LStr(4) = System.Convert.ToString(DBreader("月"))
                LStr(5) = System.Convert.ToString(DBreader("日"))
                LStr(6) = System.Convert.ToString(DBreader("時"))
                LStr(7) = System.Convert.ToString(DBreader("分"))
                LStr(8) = System.Convert.ToString(DBreader("緯度"))
                LStr(9) = System.Convert.ToString(DBreader("經度"))
                LStr(10) = System.Convert.ToString(DBreader("出生地"))
                UserMsg("編號=" & LStr(0) & "-" & LStr(1) & "," & LStr(2) & ",民國" & LStr(3) & "-" & LStr(4) & "-" _
                        & LStr(5) & "," & LStr(6) & "時" & LStr(7) & "分; 緯度" & LStr(8) & "經度" & LStr(9))
            Loop
        Else
            UserMsg("資料庫裡沒有符合 編號=" + TBoxNumber.Text + "的資料")
        End If
        DBreader.Close()
        TBoxNumber.Text = LStr(0)
        TBoxName.Text = LStr(1)
        TBoxYear.Text = LStr(3)
        TBoxMonth.Text = LStr(4)
        TBoxDay.Text = LStr(5)
        TBoxHour.Text = LStr(6)
        TBoxMin.Text = LStr(7)
        TBoxPOS1.Text = LStr(8)
        TBoxPOS2.Text = LStr(9)
        txtAddress.Text = LStr(10)
        CBox_Place.SelectedIndex = 0 '預設為無資料
        If LStr(2) = "男" Then
            RadioButton_F.Checked = False
            RadioButton_M.Checked = True
        Else
            RadioButton_F.Checked = True
            RadioButton_M.Checked = False
        End If
        FM_check()
        UserMsg("資料庫中符合 編號=" + TBoxNumber.Text + "的資料如上....")
        SQLcmd.Dispose()
    End Sub

    Private Sub Btn_LoopTest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_LoopTest.Click
        Dim datNow As Date
        Dim InputYear, inputMonth As Integer
        Dim LoopTimes As Integer
        Dim DisplayPage As Integer
        Dim WaitMs As Integer
        Dim mbx As Integer
        UserMsg("Loop test....")
        UserMsg("=======================================")
        UserMsg("輸入年月欄位: 起始日為該年的某月一日")
        UserMsg("輸入日欄位: loop次數= N * 365 (日)")
        UserMsg("輸入的小時數,為顯示停在第幾頁")
        UserMsg("輸入的分鐘數,為顯示停多少10ms, 0=等待")
        UserMsg("=======================================")
        mbx = MsgBox("Q:確定Loop test?", vbYesNo + vbQuestion, "Loop ?")
        If mbx <> vbYes Then
            UserMsg("Cancle loop test....")
            Exit Sub
        End If
        UserMsg("Loop test start....")
        If TBoxYear.Text < 30 Then
            UserMsg("出生年小於民國30年,超出資料庫範圍,無法計算...!!!")
        Else
            WaitMs = CInt(TBoxMin.Text)   '輸入的分鐘數,為顯示停多少10ms, 0=等待
            DisplayPage = CInt(TBoxHour.Text)       '輸入的小時數,為顯示停在第幾頁
            InputYear = CInt(TBoxYear.Text) + 1911
            inputMonth = CInt(TBoxMonth.Text)
            datNow = DateSerial(InputYear, inputMonth, 1)    '輸入該年某月一日開始
            LoopTimes = 365 * CInt(TBoxDay.Text)  'loop次數= N * 365 = 數年
            For RunIndex As Integer = 1 To LoopTimes
                '取得生辰
                TBoxYear.Text = Microsoft.VisualBasic.Year(datNow) - 1911
                TBoxMonth.Text = Microsoft.VisualBasic.Month(datNow)
                TBoxDay.Text = Microsoft.VisualBasic.Day(datNow)
                TBoxHour.Text = Int(Rnd() * 23) + 1  '生時取亂數
                TBoxMin.Text = Int(Rnd() * 59) + 1
                If (Int(Rnd() * 10) Mod 2) = 1 Then
                    RadioButton_F.Checked = False
                    RadioButton_M.Checked = True
                Else
                    RadioButton_F.Checked = True
                    RadioButton_M.Checked = False
                End If
                '計算與顯示
                CalcuLife()
                Result.Close()
                Result.Show()
                DelayTms(1)
                Result.ShowPage(DisplayPage)
                If WaitMs = 0 Then
                    '....wait key press
                    MsgBox("按任意見繼續...", , "keyPress")
                Else
                    DelayTms(10 * WaitMs)
                End If

                Bir.Clear()
                '下一筆計算資料
                datNow = DateAdd("d", 1, datNow)
            Next
            UserMsg("Loop tested " & LoopTimes & " 個生辰資料")
        End If
    End Sub

    Public Sub DelayTms(ByVal Tms As Integer)
        Dim Start As Integer = Environment.TickCount()
        Dim TimeLast As Integer = Tms * 10 '要延遲 t 秒,就設為 t *1000
        Do
            If Environment.TickCount() - Start > TimeLast Then Exit Do
            Application.DoEvents() ' 要記得寫這行，不然都在跑迴圈，畫面可能會不見
        Loop
    End Sub

    Private Sub Btn_Test_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Test.Click
        UserMsg("Debug 用途, Direct test....")
        'FileUpdateSAM("Year_old.txt", False) 'step1
        'FileUpdateSAM("Year_add.txt", True)  'step2
        'JachiUpdateSAM("Year_add24.txt")     'step3
        'ReMakeSAM()                          'step4
        'MakeJachi()                          'step5

        '直接測試生辰
        TBoxNumber.Text = CustomerCount + 1
        TBoxName.Text = "直接測試"
        CBcalender.SelectedIndex = 0 '預設為國曆輸入
        TBoxYear.Text = 60    '年
        TBoxMonth.Text = 6   '月
        TBoxDay.Text = 24     '日
        TBoxHour.Text = 15    '時
        TBoxMin.Text = 20     '分
        RadioButton_F.Checked = False
        RadioButton_M.Checked = True
        RB_23hSame.Checked = True    '2選1
        RB_23hNext.Checked = False
        RB_MingKon1.Checked = False  '3選1
        RB_MingKon2.Checked = False
        RB_MingKon3.Checked = True
        RB_Gwa1.Checked = False    '2選1
        RB_Gwa2.Checked = True
        FM_check()
        Gwa_check()
        Hour23_check()
        MKmethod_Check()
        StarB_check()
        '-----
        CalcuLife()
        Result.Show()
        Bir.Clear()
    End Sub

    Private Sub BtnExportYear_Click(sender As System.Object, e As System.EventArgs) Handles BtnExportYear.Click
        Dim mbx As Integer
        UserMsg("由萬年曆資料庫匯出干支年曆....")
        UserMsg("=======================================")
        UserMsg("匯出的干支年曆檔案= c:\5ArtSave\YeayGT.txt")
        UserMsg("內容欄位有國曆年月日干支,和年月日干支")
        UserMsg("輸出範圍=執行時間當年起,共三年")
        UserMsg("=======================================")
        mbx = MsgBox("Q1:確定由輸出檔案YeayGT.txt(年月日干支)?", vbYesNo + vbQuestion, "Export ?")
        If mbx = vbYes Then
            '可以重新insert/Update Year_old.txt 所有資料
            UserMsg("Operation : 輸出檔案YeayGT.txt(年月日干支)....")
            FileExportYeayGT("c:\5ArtSave\YeayGT.txt")
        End If
    End Sub

    Private Sub FileExportYeayGT(ByVal TxtFileName As String)
        Dim mbx As Integer
        Dim StartNo, EndNo As Integer
        Dim lop As Integer
        Dim YMD1, YMD2 As String
        '先取得起始編號和結束編號 ------------------------
        YMD1 = Now.Year & "/1/1"
        YMD2 = (Now.Year + 3) & "/1/1"
        UserMsg("...start date YMD=" & YMD1)
        UserMsg("...end date YMD=" & YMD2)
        SQLstr = " Select * from SAM where 西曆='" & YMD1 & "'"
        SQLcmd = New SqlCommand(SQLstr, DBconn)
        DBreader = SQLcmd.ExecuteReader()
        If DBreader.HasRows Then
            Do While DBreader.Read()
                StartNo = DBreader("編號")
            Loop
        Else
            UserMsg("資料庫裡沒有符合 日期=" & YMD1 & "的資料")
        End If
        DBreader.Close()
        SQLcmd.Dispose()
        '結束編號 -----
        SQLstr = " Select * from SAM where 西曆='" & YMD2 & "'"
        SQLcmd = New SqlCommand(SQLstr, DBconn)
        DBreader = SQLcmd.ExecuteReader()
        If DBreader.HasRows Then
            Do While DBreader.Read()
                EndNo = DBreader("編號")
            Loop
        Else
            UserMsg("資料庫裡沒有符合 日期=" & YMD2 & "的資料")
        End If
        UserMsg("...start date No=" & StartNo)
        UserMsg("...end date No=" & EndNo)
        DBreader.Close()
        SQLcmd.Dispose()

        '準備輸出檔案 -----------------
        If File.Exists(TxtFileName) = True Then
            mbx = MsgBox("檔案已存在,確定覆蓋舊的c:\5ArtSave\YeayGT.txt嗎?", vbYesNo + vbQuestion, "覆蓋")
            If mbx = vbYes Then
                UserMsg(TxtFileName & "檔案已存在,將會覆蓋....")
                FileOpen(2, TxtFileName, OpenMode.Output)
                For lop = StartNo To EndNo
                    RdYearGT_FileExport(Convert.ToString(lop))
                Next
                UserMsg("匯出完成....")
                FileClose(2)  '關閉檔案
            Else
                UserMsg(TxtFileName & "檔案已存在,放棄匯出!....")
            End If
        Else
            UserMsg(TxtFileName & "檔案不存在,產生新檔案....")
            FileOpen(2, TxtFileName, OpenMode.Output)
            For lop = StartNo To EndNo
                RdYearGT_FileExport(Convert.ToString(lop))
            Next
            UserMsg("匯出完成....")
            FileClose(2)  '關閉檔案
        End If
    End Sub

    Private Sub RdYearGT_FileExport(ByVal SAMNumber As String)
        Dim strTemp As String
        Dim YMD As String = ""      '暫存目前查詢出來的西曆生辰資料
        Dim Ju() As String = New String(8) {"", "", "", "", "", "", "", "", ""} 'Ju(0)不用 '暫存目前查詢出來的8柱
        SQLstr = " Select * from SAM where 編號='" & SAMNumber & "'"
        SQLcmd = New SqlCommand(SQLstr, DBconn)
        DBreader = SQLcmd.ExecuteReader()
        If DBreader.HasRows Then
            Do While DBreader.Read()
                YMD = DBreader("西曆")
                Ju(1) = DBreader("一柱")
                Ju(2) = DBreader("二柱")
                Ju(3) = DBreader("三柱")           
                strTemp = YMD & "," & Ju(1) & "," & Ju(2) & "," & Ju(3)
                UserMsg("編號=" & SAMNumber & ":" & strTemp)
                PrintLine(2, strTemp)
            Loop
        Else
            UserMsg("資料庫裡沒有符合 編號=" + SAMNumber + "的資料")
        End If
        DBreader.Close()
        SQLcmd.Dispose()
    End Sub

    Private Sub Btn_UpdateSAM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_UpdateSAM.Click
        Dim mbx As Integer
        UserMsg("更新萬年曆....")
        UserMsg("=======================================")
        UserMsg("檔案Year_old.txt,是2002舊版程式的萬年曆")
        UserMsg("內容含有農曆年月日干支,也已經有六柱干支")
        UserMsg("此處先輸入農曆年月日干支!")
        UserMsg("=======================================")
        mbx = MsgBox("Q1:確定由檔案Year_old.txt輸入陰陽曆資料嗎?", vbYesNo + vbQuestion, "Question1")
        If mbx = vbYes Then
            '可以重新insert/Update Year_old.txt 所有資料
            UserMsg("Operation-1 : 由檔案Year_old.txt, 輸入陰陽曆的資料,....")
            FileUpdateSAM("Year_old.txt", False)
        End If
        UserMsg("=======================================")
        UserMsg("檔案Year_add.txt,是2013Ajack新增萬年曆 ")
        UserMsg("內容是陰曆年月日干支,無六柱,節氣不精準.")
        UserMsg("=======================================")
        mbx = MsgBox("Q2:確定由檔案Year_add.txt輸入陰陽曆資料嗎?", vbYesNo + vbQuestion, "Question2")
        If mbx = vbYes Then
            UserMsg("Operation-2 : 由檔案Year1_add.txt, 輸入陰陽曆的資料,....")
            FileUpdateSAM("Year_add.txt", True)    '不輸入節氣
        End If
        UserMsg("=======================================")
        UserMsg("檔案Year_add24.txt,是2013Ajack新增檔案 ")
        UserMsg("依據網路上程式,再拷貝整理成文字檔,網址:")
        UserMsg("http://www.khoeiaumon.com/discuz/solar.php ")
        UserMsg("=======================================")
        mbx = MsgBox("Q3:確定由檔案Year_add24.txt輸入節氣資料嗎?", vbYesNo + vbQuestion, "Question3")
        If mbx = vbYes Then
            UserMsg("Operation-3 : 由檔案Year1_add24.txt, 輸入節氣資料,....")
            JachiUpdateSAM("Year_add24.txt")
        End If
        UserMsg("=======================================")
        UserMsg("萬年曆中的陰曆/農曆不同, 全部統一格式: ")
        UserMsg("西曆,農曆年月日干支,六柱是陰曆年月日!! ")
        UserMsg("=======================================")
        mbx = MsgBox("Q4:確定開始重整陰曆/農曆,以及產生六柱嗎?", vbYesNo + vbQuestion, "Question4")
        If mbx = vbYes Then
            '確定資料庫中是空白的資料表
            '可以重新insert Year_old.txt 所有資料
            UserMsg("Operation-4 : 重整萬年曆,也計算六柱,....")
            ReMakeSAM()
        End If
        UserMsg("=======================================")
        UserMsg("因為八字計算會需要前後三個節氣         ")
        UserMsg("所以需要建立,獨立的依序排列的資料庫    ")
        UserMsg("=======================================")
        mbx = MsgBox("Q5:確定開始重整節氣資料庫?", vbYesNo + vbQuestion, "Question5")
        If mbx = vbYes Then
            '確定資料庫中是空白的資料表
            '可以重新insert Year_old.txt 所有資料
            UserMsg("Operation-5 : 重整萬年曆,產生依序排列的節氣資料...")
            MakeJachi()
        End If
        UserMsg("更新萬年曆,全部OK... ")
    End Sub

    Private Sub BtnLBmsgClr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnLBmsgClr.Click
        '清除ListBox畫面
        RichTextBox1.Clear()
    End Sub

    '由檔案輸入,重建萬年曆資料Step1,Step2
    Private Sub FileUpdateSAM(ByVal TxtFileName As String, ByVal NoJachi As Boolean)
        Dim Aline As String
        Dim Ln As Integer
        Dim x As Integer
        Dim cn As Integer
        Dim HasItemErr As Boolean
        HasItemErr = False
        If File.Exists(TxtFileName) = True Then
            FileOpen(1, TxtFileName, OpenMode.Input)
            Ln = 0
            Do While (Not EOF(1))
                '檔案讀取一行,並且切割字串
                Aline = LineInput(1)
                'UserMsg(Ln & " " & Aline)
                FldTx = Split(Aline, vbTab)
                If Ln > 0 Then
                    '第一行為欄位名稱,第二行開始資料才有效
                    '檢查是否已有此資料,以第一欄日期查詢
                    SQLstr = "select count(*) from SAM where 西曆 = '" & FldTx(0) & "'"
                    SQLcmd = New SqlCommand(SQLstr, DBconn)
                    DBreader = SQLcmd.ExecuteReader()
                    DBreader.Read()
                    cn = DBreader.Item(0)
                    DBreader.Close()
                    SQLcmd.Dispose()
                    '準備 SQL 字串, Insert or Update
                    If cn <> 0 Then    ' 資料存在,故為Update
                        SQLstr = "update SAM set 農曆='" & FldTx(1)
                        SQLstr = SQLstr & "',年='" & FldTx(2)
                        SQLstr = SQLstr & "',月='" & FldTx(3)
                        SQLstr = SQLstr & "',日='" & FldTx(4)
                        If FldTx(5) = "" Then
                            SQLstr = SQLstr & "'"
                        Else
                            SQLstr = SQLstr & "',節氣='" & FldTx(5) & "'"
                        End If
                        SQLstr = SQLstr & " where 西曆='" & FldTx(0) & "'"
                    Else               '資料不存在,故而insert
                        SQLstr = "insert into SAM (西曆,農曆,年,月,日,節氣)values ('"
                        SQLstr = SQLstr & FldTx(0) & "', '"
                        SQLstr = SQLstr & FldTx(1) & "', '"
                        SQLstr = SQLstr & FldTx(2) & "', '"
                        SQLstr = SQLstr & FldTx(3) & "', '"
                        SQLstr = SQLstr & FldTx(4) & "', "
                        If (FldTx(5) = "") Or NoJachi Then
                            SQLstr = SQLstr & "NULL)"
                        Else
                            SQLstr = SQLstr & "'" & FldTx(5) & "')"
                        End If
                    End If
                    SQLcmd = New SqlCommand(SQLstr, DBconn)
                    'UserMsg(SQLstr)
                    x = SQLcmd.ExecuteNonQuery()
                    If x <> 1 Then 'ExecuteNonQuery 傳回資料異動筆數
                        UserMsg("新增資料庫失敗! ")
                        HasItemErr = True
                    Else
                        '成功!
                        UserMsg(Ln & "行成功! 現有 " & SAMCount & "筆:" & FldTx(0) & "," & FldTx(1) & "," & FldTx(2) & "," _
                                  & FldTx(3) & "." & FldTx(4) & "," & FldTx(5))
                        If cn = 0 Then
                            '若為insert設定資料表總筆數加一
                            SAMCount = SAMCount + 1
                        End If
                    End If
                    SQLcmd.Dispose()
                End If
                Ln = Ln + 1
            Loop
            ' ---------------------
            FileClose(1)  '關閉檔案
            UserMsg("File=" & TxtFileName & " 總共輸入了 " & Ln - 1 & " 行資料")
            UserMsg("Fate2013資料庫的SAM資料表有 " & SAMCount & " 筆資料....")
            If HasItemErr Then
                UserMsg("!!!!!!!請注意!! " & TxtFileName & " 輸入過程中有錯誤 !!!!!!!!!")
            End If
        Else
            UserMsg(TxtFileName & "檔案找不到!!,操作中斷....")
        End If
    End Sub

    '由檔案輸入節氣資料,重建萬年曆Step3
    Private Sub JachiUpdateSAM(ByVal TxtFileName As String)
        Dim Aline As String
        Dim Ln As Integer
        Dim strDate As String
        Dim strJachi As String
        Dim x As Integer
        Dim cn As Integer
        Dim HasItemErr As Boolean
        HasItemErr = False
        If File.Exists(TxtFileName) = True Then
            FileOpen(1, TxtFileName, OpenMode.Input)
            Ln = 0
            Do While (Not EOF(1))   'And Ln < 20  '測試時只用20筆資料
                '檔案讀取一行,並且切割字串
                Aline = LineInput(1)
                'UserMsg(Ln & " " & Aline)
                FldTx = Split(Aline, vbTab)
                If Ln > 0 Then
                    'UserMsg(Ln & "行:" & FldTx(0) & "," & FldTx(1) & "," & FldTx(2) & "," _
                    '            & FldTx(3) & "." & FldTx(4) & "," & FldTx(5) & "," & FldTx(6))
                    '重整日期資料
                    strDate = FldTx(1) & "/" & FldTx(2) & "/" & FldTx(3)
                    '重整節氣及時間資料
                    strJachi = "[" & FldTx(0)
                    If Len(FldTx(4)) = 1 Then
                        strJachi = strJachi & "0" & FldTx(4) & ":"
                    Else
                        strJachi = strJachi & FldTx(4) & ":"
                    End If
                    If Len(FldTx(5)) = 1 Then
                        strJachi = strJachi & "0" & FldTx(5) & "]"
                    Else
                        strJachi = strJachi & FldTx(5) & "]"
                    End If
                    'UserMsg(Ln & "行:" & strDate & ", " & strJachi)
                    '檢查是否已有此資料,以第一欄日期查詢
                    SQLstr = "select count(*) from SAM where 西曆 = '" & strDate & "'"
                    SQLcmd = New SqlCommand(SQLstr, DBconn)
                    DBreader = SQLcmd.ExecuteReader()
                    DBreader.Read()
                    cn = DBreader.Item(0)
                    DBreader.Close()
                    SQLcmd.Dispose()
                    '準備 SQL 字串, Insert or Update
                    If cn = 0 Then
                        UserMsg("資料不存在,有錯誤請檢查!!!!")
                        HasItemErr = True
                    Else
                        ' 資料存在,Update strJachi -------------
                        SQLstr = "update SAM set 節氣='" & strJachi & "' where 西曆='" & strDate & "'"
                        SQLcmd = New SqlCommand(SQLstr, DBconn)
                        'UserMsg(SQLstr)
                        x = SQLcmd.ExecuteNonQuery()
                        If x <> 1 Then 'ExecuteNonQuery 傳回資料異動筆數
                            UserMsg("新增資料庫失敗! ")
                            HasItemErr = True
                        Else
                            '成功!
                            UserMsg("成功Update節氣 " & strDate & ", " & strJachi)
                        End If
                        SQLcmd.Dispose()
                    End If
                End If
                Ln = Ln + 1
            Loop
            ' ---------------------
            FileClose(1)  '關閉檔案
            UserMsg("File=" & TxtFileName & " 總共輸入了 " & Ln - 1 & " 行資料")
            UserMsg("Fate2013資料庫的SAM資料表有 " & SAMCount & " 筆資料....")
            If HasItemErr Then
                UserMsg("!!!!!!!請注意!! " & TxtFileName & " 輸入過程中有錯誤 !!!!!!!!!")
            End If
        Else
            UserMsg(TxtFileName & "檔案找不到!!,操作中斷....")
        End If
    End Sub

    '重整萬年曆SAM, 產生123568柱, 重建萬年曆Step4
    Private Sub ReMakeSAM()
        Dim x As Integer
        Dim ProcNo As Integer  '目前正在處理的編號
        Dim P280No As Integer  '往前280天的編號
        Dim GanY As String   '程式重頭排農曆的年干
        Dim TsuY As String   '程式重頭排農曆的年支
        Dim GanM As String   '程式重頭排農曆的月干
        Dim TsuM As String   '程式重頭排農曆的月支
        Dim GanY1 As String  '程式排陰曆八柱的年干
        Dim TsuY1 As String  '程式排陰曆八柱的年支
        Dim GanM1 As String  '程式排陰曆八柱的月干
        Dim TsuM1 As String  '程式排陰曆八柱的月支
        Dim Rcalendar2 As String   '資料表讀出的農曆(Year_old.txt)或陰曆(Year_add.txt)
        Dim RC2 As String()
        Dim RC2_Y As Integer
        Dim RC2_M As Integer
        Dim RC2_D As Integer
        Dim Jachi As String        '資料表讀出的節氣
        Dim JuY As String    '準備要Update資料庫的年柱,也是資料表讀出的年柱
        Dim JuM As String    '準備要Update資料庫的月柱,也是資料表讀出的月柱
        Dim JuD As String    '準備要Update資料庫的日柱和三柱,也是資料表讀出的日柱
        Dim Ju1 As String    '準備要Update資料庫的一柱
        Dim Ju2 As String    '準備要Update資料庫的二柱
        Dim Ju5 As String    '準備要Update資料庫的五柱
        Dim Ju6 As String    '準備要Update資料庫的六柱
        Dim Ju8 As String    '準備要Update資料庫的八柱,也是資料表讀出280天前的年柱
        Dim intPrevMonth As Integer
        Dim HasItemErr As Boolean

        HasItemErr = False
        ProcNo = 1  '由資料表第一個編號開始處理 (西曆1929/12/26)
        P280No = 1
        GanY = "己"
        TsuY = "巳"
        GanM = "丙"
        TsuM = "子"
        GanY1 = "己"
        TsuY1 = "巳"
        GanM1 = "丙"
        TsuM1 = "子"
        intPrevMonth = 11
        JuY = "己巳"  '避免NULL,先給值
        JuM = "丙子"
        JuD = "乙巳"
        Jachi = ""
        Rcalendar2 = "1929/11/26"
        Ju8 = "戊辰"
        Do Until ProcNo > SAMCount
            If ProcNo > 280 Then
                '讀出280天之前的年柱----------------------------------
                SQLstr = " Select * from SAM where 編號='" & P280No & "'"
                SQLcmd = New SqlCommand(SQLstr, DBconn)
                DBreader = SQLcmd.ExecuteReader()
                If DBreader.HasRows Then
                    Do While DBreader.Read()
                        Ju8 = System.Convert.ToString(DBreader("年"))
                    Loop
                Else
                    UserMsg("資料庫裡沒有符合 編號= " & P280No & " 的資料")
                    HasItemErr = True
                End If
                DBreader.Close()
                SQLcmd.Dispose()
            End If
            '讀出目前編號的資料 ---------------------------------------
            SQLstr = " Select * from SAM where 編號='" & ProcNo & "'"
            SQLcmd = New SqlCommand(SQLstr, DBconn)
            DBreader = SQLcmd.ExecuteReader()
            If DBreader.HasRows Then
                Do While DBreader.Read()
                    Rcalendar2 = System.Convert.ToString(DBreader("農曆"))
                    JuY = System.Convert.ToString(DBreader("年"))
                    JuM = System.Convert.ToString(DBreader("月"))
                    JuD = System.Convert.ToString(DBreader("日"))
                    Jachi = System.Convert.ToString(DBreader("節氣"))
                Loop
                DBreader.Close()
                SQLcmd.Dispose()
                '整理農曆字串的不一致
                If Len(Trim(Rcalendar2)) <> 10 Then
                    RC2 = Split(Rcalendar2, "/")
                    RC2_Y = CInt(RC2(0))
                    RC2_M = CInt(RC2(1))
                    RC2_D = CInt(RC2(2))
                    Rcalendar2 = Convert.ToString(RC2_Y) & "/"
                    If RC2_M < 10 Then
                        Rcalendar2 = Rcalendar2 & "0" & Convert.ToString(RC2_M) & "/"
                    Else
                        Rcalendar2 = Rcalendar2 & Convert.ToString(RC2_M) & "/"
                    End If
                    If RC2_D < 10 Then
                        Rcalendar2 = Rcalendar2 & "0" & Convert.ToString(RC2_D)
                    Else
                        Rcalendar2 = Rcalendar2 & Convert.ToString(RC2_D)
                    End If
                End If
                '程式計算農曆的年柱月柱GanY,TsuY,GanM,TsuM
                JuYMChangeGJ(GanY, TsuY, GanM, TsuM, intPrevMonth, Rcalendar2)
                '檢查年柱月柱, ------------------------------------------
                '為了更正year_add.txt第二欄位為陰曆而非農曆()
                If (JuY <> (GanY & TsuY)) Then
                    UserMsg("編號= " & +ProcNo & " 的原始資料年非農曆")
                    JuY = GanY & TsuY
                End If
                If (JuM <> (GanM & TsuM)) Then
                    UserMsg("編號= " & ProcNo & " 的原始資料月非農曆")
                    JuM = GanM & TsuM
                End If
                '計算1256柱---------------------------------------------
                Ju1 = GanY1 & TsuY1
                Ju2 = GanM1 & TsuM1
                '五柱胎元月,速算為干移一位,支移三位
                Ju5 = GetJu5(GanM1, TsuM1)
                '六柱胎元日,用三柱之干支的五合六合
                Ju6 = GTMatch(JuD)
                'Update此編號到資料庫中--------------------------------
                SQLstr = "update SAM set 年='" & JuY
                SQLstr = SQLstr & "', 月='" & JuM
                SQLstr = SQLstr & "', 日='" & JuD
                SQLstr = SQLstr & "', 一柱='" & Ju1
                SQLstr = SQLstr & "', 二柱='" & Ju2
                SQLstr = SQLstr & "', 三柱='" & JuD
                SQLstr = SQLstr & "', 五柱='" & Ju5
                SQLstr = SQLstr & "', 六柱='" & Ju6
                SQLstr = SQLstr & "', 八柱='" & Ju8
                SQLstr = SQLstr & "', 農曆='" & Rcalendar2
                SQLstr = SQLstr & "' where 編號=" & ProcNo
                SQLcmd = New SqlCommand(SQLstr, DBconn)
                x = SQLcmd.ExecuteNonQuery()
                If x <> 1 Then 'ExecuteNonQuery 傳回資料異動筆數
                    UserMsg("編號= " & ProcNo & " 更新資料庫失敗! ")
                    HasItemErr = True
                Else
                    UserMsg("編號=" & ProcNo & "更新OK," & Rcalendar2 & JuY & JuM & JuD & Jachi & "," _
                              & Ju1 & Ju2 & JuD & "," & Ju5 & Ju6 & Ju8)
                End If
            Else
                UserMsg("資料庫裡沒有符合 編號= " & ProcNo & " 的資料")
                HasItemErr = True
            End If
            '---------Loop neding part
            SQLcmd.Dispose()
            '更新編號加一,下一天
            ProcNo = ProcNo + 1
            If ProcNo > 280 Then
                P280No = P280No + 1
            End If
            '先程式計算下一天的一柱二柱是否改變?
            '若今天剛好是節氣的節,下一天的一柱二柱要改變
            Ju12ChangeGJ(GanY1, TsuY1, GanM1, TsuM1, Jachi)

        Loop   'end of porcess number loop
        UserMsg("總共轉換了 " & ProcNo - 1 & " 筆資料")
        If HasItemErr Then
            UserMsg("!!!!!!!請注意!! 轉換過程中有錯誤 !!!!!!!!!")
        End If
    End Sub

    '重整萬年曆SAM, 產生Jachi資料表, 重建萬年曆Step5
    Private Sub MakeJachi()
        Dim x As Integer
        Dim cn As String
        Dim ProcSAMno As Integer  '目前正在處理的編號for SAM資料表
        Dim ProcJCno As Integer   '目前正在處理的編號for Jachi資料表
        Dim PreNo As Integer      '目前辦定的前節氣值,指向Jachi資料庫中的編號 
        Dim HasItemErr As Boolean
        Dim SAMCalendar1 As String 'SAM資料表讀出的農曆
        Dim SAMJachi As String     'SAM資料表讀出的節氣
        Dim JCname As String   '節氣名稱,為了JaChi資料表的寫入
        Dim JCtime As String   '節氣時間,為了JaChi資料表的寫入
        'Dim astrJachi(24) As String
        Dim lop As Integer

        JCname = ""
        SAMCalendar1 = ""
        SAMJachi = ""

        HasItemErr = False
        ProcSAMno = 1  '由資料表第一個編號開始處理 (西曆1929/12/26)
        ProcJCno = 1
        PreNo = 0    '前節氣欄位預設0,意為沒有錢節氣資料    
        Do Until ProcSAMno > SAMCount
            '讀出SAM資料表中目前編號的資料 ---------------------------------------
            SQLstr = " Select * from SAM where 編號='" & ProcSAMno & "'"
            SQLcmd = New SqlCommand(SQLstr, DBconn)
            DBreader = SQLcmd.ExecuteReader()
            If DBreader.HasRows Then
                Do While DBreader.Read()
                    SAMCalendar1 = System.Convert.ToString(DBreader("西曆"))
                    SAMJachi = System.Convert.ToString(DBreader("節氣"))
                    'UserMsg("SAM資料表中編號=" & ProcSAMno & "," & SAMCalendar1 & "," & SAMJachi)
                Loop
                DBreader.Close()
                SQLcmd.Dispose()

                'Update此編號的前節氣欄位,到SAM資料庫中--------------------------------
                SQLstr = "update SAM set 前節氣號='" & PreNo
                SQLstr = SQLstr & "' where 編號=" & ProcSAMno
                SQLcmd = New SqlCommand(SQLstr, DBconn)
                x = SQLcmd.ExecuteNonQuery()
                If x <> 1 Then 'ExecuteNonQuery 傳回資料異動筆數
                    UserMsg("SAM資料表中編號= " & ProcSAMno & " 更新失敗! ")
                    HasItemErr = True
                    'Else
                    'UserMsg("SAM資料表中編號= " & ProcSAMno & " 更新OK,")
                End If
                SQLcmd.Dispose()

                '判斷節氣欄位------------------------
                If SAMJachi <> "" Then     '有節氣資料
                    '在寫入之前,先確認JaChi當中無此資料,以西曆日期查詢
                    SQLstr = "select count(*) from JaChi where 西曆 = '" & SAMCalendar1 & "'"
                    SQLcmd = New SqlCommand(SQLstr, DBconn)
                    DBreader = SQLcmd.ExecuteReader()
                    DBreader.Read()
                    cn = DBreader.Item(0)
                    DBreader.Close()
                    SQLcmd.Dispose()
                    If cn <> 0 Then ' 資料存在,
                        UserMsg("JaChi已存在資料:" & SAMCalendar1)
                        ProcJCno = ProcJCno + 1
                    Else
                        ' 由節氣資料: [小寒巳時09:03]  抽出 09:03 的部份
                        SAMJachi = Trim(SAMJachi)
                        JCtime = Microsoft.VisualBasic.Right(SAMJachi, 6)
                        JCtime = Microsoft.VisualBasic.Left(JCtime, 5)
                        '由節氣資料: [小寒巳時09:03]  抽出 小寒 的部份
                        '注意有時有特例[十月大立冬巳時09:12]  
                        For lop = 1 To 24
                            If InStr(SAMJachi, str24Jachi(lop)) <> 0 Then
                                JCname = str24Jachi(lop)
                                Exit For
                            End If
                        Next
                        'SQL動作for JaChi新增一資料 --------
                        SQLstr = "insert into JaChi (西曆,節氣名,時分)values ('"
                        SQLstr = SQLstr & SAMCalendar1 & "', '"
                        SQLstr = SQLstr & JCname & "', '"
                        SQLstr = SQLstr & JCtime & "')"
                        SQLcmd = New SqlCommand(SQLstr, DBconn)
                        x = SQLcmd.ExecuteNonQuery()
                        If x <> 1 Then 'ExecuteNonQuery 傳回資料異動筆數
                            UserMsg("新增資料庫失敗! ")
                        Else
                            UserMsg("新增JaChi資料:" & SAMCalendar1 & JCname & JCtime)
                            '設定JaChi資料表總筆數加一
                            JachiCount = JachiCount + 1
                            'UserMsg("Fate2013資料庫的JaChi資料表有 " & JachiCount & " 筆資料....")
                        End If
                        ProcJCno = ProcJCno + 1
                    End If
                    SQLcmd.Dispose()
                    'PreNo++ 讓下一天使用
                    PreNo = PreNo + 1
                End If
            Else
                UserMsg("資料庫裡沒有符合 編號= " & ProcSAMno & " 的資料")
                HasItemErr = True
            End If
            '---------Loop neding part
            '更新編號加一(下一天) for SAM
            ProcSAMno = ProcSAMno + 1
        Loop   'end of porcess number loop
        UserMsg("總共轉換了 " & ProcSAMno - 1 & " 筆資料")
        UserMsg("Fate2013資料庫的JaChi資料表有 " & JachiCount & " 筆資料....")
        If HasItemErr Then
            UserMsg("!!!!!!!請注意!! 轉換過程中有錯誤 !!!!!!!!!")
        End If
    End Sub

    '五柱胎元月,速算為干移一位,支移三位 ---------------------------
    Private Function GetJu5(ByVal strGanM1 As String, ByVal strTsuM1 As String) As String
        Dim strOut As String
        strOut = NextTsu(strTsuM1)
        strOut = NextTsu(strOut)
        GetJu5 = NextGan(strGanM1) & NextTsu(strOut)
    End Function

    '依據初一,變化農曆的年月干支 ------------------------------------
    Private Sub JuYMChangeGJ(ByRef GanY As String, ByRef TsuY As String, _
                          ByRef GanM As String, ByRef TsuM As String, _
                          ByRef intPrevMonth As Integer, _
                          ByVal Rcalendar2 As String)
        Dim str005 As String
        Dim str002 As String
        str005 = Microsoft.VisualBasic.Right(Rcalendar2, 5)
        str002 = Microsoft.VisualBasic.Right(Rcalendar2, 2)
        ' 如果是1月1日，則年度也要換
        If str005 = "01/01" Then
            GanY = NextGan(GanY)
            TsuY = NextTsu(TsuY)
        End If
        ' 如果是1日，則月份也要換
        If str002 = "01" Then
            ' 如果上個月與本月份是相同的月份，代表是閏月，不須換月份
            If intPrevMonth <> CInt(Mid(Rcalendar2, 6, 2)) Then
                GanM = NextGan(GanM)
                TsuM = NextTsu(TsuM)
                intPrevMonth = CInt(Mid(Rcalendar2, 6, 2))
            End If
        End If
    End Sub

    '依據節氣,變化陰曆的年月干支(一柱二柱) ---------------------------
    Private Sub Ju12ChangeGJ(ByRef strGanY As String, ByRef strTsuY As String, _
                          ByRef strGanM As String, ByRef strTsuM As String, _
                          ByVal strJachi As String)
        Dim chk As Byte
        If Format(strJachi) <> "" Then
            chk = Is12Ja(strJachi)
            If chk <> 0 Then     '如果是十二個節之一, 回傳直非0
                strGanM = NextGan(strGanM)
                strTsuM = NextTsu(strTsuM)
                ' 如果是1月，則年度也要換
                If chk = 1 Then
                    strGanY = NextGan(strGanY)
                    strTsuY = NextTsu(strTsuY)
                End If
            End If
        End If
    End Sub

    Private Sub Button_cal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_cal.Click
        If TBoxYear.Text < 19 Then
            UserMsg("出生年小於民國19年,超出資料庫範圍,無法計算...!!!")
        Else
            Bir.Clear()  '預防舊資料累積下來
            CalcuLife()
            Result.Show()
        End If
    End Sub

    Private Sub CalcuLife()
        Dim YMD As String = ""       '暫存目前輸入的生辰資料
        '-----
        Dim YMD1 As String = ""      '暫存目前查詢出來的西曆生辰資料
        Dim YMD2 As String = ""      '暫存目前查詢出來的農曆生辰資料
        Dim CurJC As String = ""     '暫存目前查詢出來的生辰的節氣資料
        Dim Ju() As String = New String(8) {"", "", "", "", "", "", "", "", ""} 'Ju(0)不用 '暫存目前查詢出來的8柱
        Dim PreJCNo As Integer       '暫存目前查詢出來的前節氣號
        Dim SAM_No As Integer        '暫存目前查詢出來日期, 其在SAM中的編號
        '-----
        Dim tmpYMD1 As String = ""      '暫存目前查詢出來的西曆生辰資料
        Dim tmpYMD2 As String = ""      '暫存目前查詢出來的農曆生辰資料
        Dim tmpCurJC As String = ""     '暫存目前查詢出來的生辰的節氣資料
        Dim tmpJu() As String = New String(8) {"", "", "", "", "", "", "", "", ""} 'Ju(0)不用 '暫存目前查詢出來的8柱
        Dim tmpPreJCNo As Integer       '暫存目前查詢出來的前節氣號
        Dim tmpSAM_No As Integer        '暫存目前查詢出來日期, 其在SAM中的編號
        '-----
        Dim YMD_NextDay As String = ""
        '-----
        Dim CurTime As String = ""   '暫存目前生辰的時間資料
        Dim AnsRelatedJC As RelatedJC
        Dim StrLat, StrLon As String()
        Dim rCount As Integer

        Bir.No = txtFld(0)
        Bir.Name = txtFld(1)
        Bir.Sex = txtFld(2)
        Bir.BornPlaceAddr = txtFld(10)
        StrLat = Split(txtFld(8), ":")
        StrLon = Split(txtFld(9), ":")
        Bir.BornPlace.LatDeg = Val(StrLat(0))
        Bir.BornPlace.LatMin = Val(StrLat(1))
        Bir.BornPlace.LatSec = Val(StrLat(2))
        Bir.BornPlace.LonDeg = Val(StrLon(0))
        Bir.BornPlace.LonMin = Val(StrLon(1))
        Bir.BornPlace.LonSec = Val(StrLon(2))
        YMD = txtFld(3) + 1911 & "/" & txtFld(4) & "/" & txtFld(5)
        Bir.BornHour = txtFld(6)
        Bir.BornMinute = txtFld(7)
        CurTime = txtFld(6) & ":" & txtFld(7)
        If CBcalender.SelectedIndex = 0 Then
            '輸入資料為陽曆,
            Bir.Solar.Y = txtFld(3)
            Bir.Solar.M = txtFld(4)
            Bir.Solar.D = txtFld(5)
            'SQL查詢農曆
            SQLstr = " Select * from SAM where 西曆='" & YMD & "'"
        Else
            '輸入資料為農曆
            Bir.Lunar.Y = txtFld(3)
            Bir.Lunar.M = txtFld(4)
            Bir.Lunar.D = txtFld(5)
            Bir.Lunar_IntercalaryMonth = IntercalaryMonth
            '注意農曆資料於資料庫有特別格式
            YMD = Convert.ToString(Bir.Lunar.Y + 1911) & "/"
            If Bir.Lunar.M < 10 Then
                YMD = YMD & "0" & Convert.ToString(Bir.Lunar.M) & "/"
            Else
                YMD = YMD & Convert.ToString(Bir.Lunar.M) & "/"
            End If
            If Bir.Lunar.D < 10 Then
                YMD = YMD & "0" & Convert.ToString(Bir.Lunar.D)
            Else
                YMD = YMD & Convert.ToString(Bir.Lunar.D)
            End If
            'SQL查詢陽曆
            SQLstr = " Select * from SAM where 農曆='" & YMD & "'"
        End If
        '查詢資料庫
        SQLcmd = New SqlCommand(SQLstr, DBconn)
        DBreader = SQLcmd.ExecuteReader()
        If DBreader.HasRows Then
            rCount = 0
            Do While DBreader.Read()
                SAM_No = DBreader("編號")
                YMD1 = DBreader("西曆")
                YMD2 = DBreader("農曆")
                Ju(1) = DBreader("一柱")
                Ju(2) = DBreader("二柱")
                Ju(3) = DBreader("三柱")
                Ju(5) = DBreader("五柱")
                Ju(6) = DBreader("六柱")
                Ju(8) = DBreader("八柱")
                PreJCNo = DBreader("前節氣號")
                CurJC = System.Convert.ToString(DBreader("節氣"))
                rCount = rCount + 1
                '20140516,因為農曆有閏月機制,Do-loop完畢會得到第二個(即是閏月)
                '所以必須先記住第一個 (PS: if rCount>2, then 資料庫有錯)
                If rCount = 1 Then
                    tmpSAM_No = SAM_No
                    tmpYMD1 = YMD1
                    tmpYMD2 = YMD2
                    tmpJu(1) = Ju(1)
                    tmpJu(2) = Ju(2)
                    tmpJu(3) = Ju(3)
                    tmpJu(5) = Ju(5)
                    tmpJu(6) = Ju(6)
                    tmpJu(8) = Ju(8)
                    tmpPreJCNo = PreJCNo
                    tmpCurJC = CurJC
                End If
            Loop
            If rCount > 1 Then
                UserMsg("資料庫有" & rCount & "筆資料符合查詢條件,請注意農曆閏月與否!")
                MessageBox.Show("這個農曆日期有兩個,請注意農曆閏月與否!")
                If IntercalaryMonth = False Then
                    SAM_No = tmpSAM_No
                    YMD1 = tmpYMD1
                    YMD2 = tmpYMD2
                    Ju(1) = tmpJu(1)
                    Ju(2) = tmpJu(2)
                    Ju(3) = tmpJu(3)
                    Ju(5) = tmpJu(5)
                    Ju(6) = tmpJu(6)
                    Ju(8) = tmpJu(8)
                    PreJCNo = tmpPreJCNo
                    CurJC = tmpCurJC

                End If
            Else
                '所以rCount=1, 此農曆日期一定沒有閏月
                If IntercalaryMonth Then
                    MessageBox.Show("這個農曆日期,月份不會有閏月!")
                End If
                Bir.Lunar_IntercalaryMonth = False
            End If
        Else
                UserMsg("資料庫裡沒有符合 日期=" & YMD & "的資料")
        End If
        DBreader.Close()
        SQLcmd.Dispose()

        '先查次日,西曆日期
        SQLstr = " Select * from SAM where 編號='" & SAM_No + 1 & "'"
        SQLcmd = New SqlCommand(SQLstr, DBconn)
        DBreader = SQLcmd.ExecuteReader()
        If DBreader.HasRows Then
            Do While DBreader.Read()
                YMD_NextDay = DBreader("西曆")
            Loop
        Else
            UserMsg("資料庫裡沒有符合 編號=" & SAM_No + 1 & "的資料")
        End If
        DBreader.Close()
        SQLcmd.Dispose()

        '-----------------------------------------------------------------------------------------
        '整理日期資料
        If CBcalender.SelectedIndex = 0 Then    '輸入是西曆
            '查出農曆
            Bir.Lunar.Y = CInt(Mid(YMD2, 1, 4)) - 1911
            Bir.Lunar.M = CInt(Mid(YMD2, 6, 2))
            Bir.Lunar.D = CInt(Mid(YMD2, 9, 2))
            '要確定農曆日期是否屬於閏月  ---------------
            'SQL用農曆再查詢陽曆,若有兩個則要再辦別
            SQLstr = " Select * from SAM where 農曆='" & YMD2 & "'"
            '查詢資料庫
            SQLcmd = New SqlCommand(SQLstr, DBconn)
            DBreader = SQLcmd.ExecuteReader()
            If DBreader.HasRows Then
                rCount = 0
                Do While DBreader.Read()
                    tmpYMD1 = DBreader("西曆")
                    tmpYMD2 = DBreader("農曆")
                    rCount = rCount + 1
                Loop
            Else
                UserMsg("資料庫裡沒有符合 日期=" & YMD2 & "的資料")
            End If
            DBreader.Close()
            SQLcmd.Dispose()
            If (rCount > 1) Then
                UserMsg("需要判斷農曆閏月與否!")
                UserMsg("tmpYMD1 = " & tmpYMD1)
                UserMsg("YMD1 = " & YMD1)
                If tmpYMD1 = YMD1 Then
                    Bir.Lunar_IntercalaryMonth = True
                    UserMsg("是農曆閏月!")
                Else
                    Bir.Lunar_IntercalaryMonth = False
                    UserMsg("不是農曆閏月!")
                End If
            Else
                Bir.Lunar_IntercalaryMonth = False
            End If
            '------------------------------------
        Else
            '輸入是農曆,查出陽曆,
            Bir.Solar.Y = Microsoft.VisualBasic.Year(YMD1) - 1911
            Bir.Solar.M = Microsoft.VisualBasic.Month(YMD1)
            Bir.Solar.D = Microsoft.VisualBasic.Day(YMD1)
            '改變自動入欄位為陽曆資料
            UserMsg("輸入欄位更換成對應的國曆日期!!")
            TBoxYear.Text = Bir.Solar.Y
            TBoxMonth.Text = Bir.Solar.M
            TBoxDay.Text = Bir.Solar.D
            CBcalender.SelectedIndex = 0
            IntercalaryMonth = False  '陽曆無閏月!!
        End If

        '整理西曆次日資料
        Bir.SolarNextDay.Y = Microsoft.VisualBasic.Year(YMD_NextDay) - 1911
        Bir.SolarNextDay.M = Microsoft.VisualBasic.Month(YMD_NextDay)
        Bir.SolarNextDay.D = Microsoft.VisualBasic.Day(YMD_NextDay)
        '===================================================================
        '此處開始登記資料,以及計算資料
        For i As Integer = 1 To 8
            Bir.Ju(i).Gan = Microsoft.VisualBasic.Left(Ju(i), 1)
            Bir.Ju(i).Tsu = Microsoft.VisualBasic.Right(Ju(i), 1)
        Next

        ' 由年干辨陰陽
        If InStr("甲丙戊庚壬", Bir.Ju(1).Gan) Then
            Bir.SType = "陽"
        Else
            Bir.SType = "陰"
        End If

        ' 取得前後節氣
        AnsRelatedJC = GetRelatedJC(PreJCNo, CurJC, CurTime)
        Bir.PreJa = AnsRelatedJC.PreJa
        Bir.NxtJa = AnsRelatedJC.NxtJa
        Bir.PreJC = AnsRelatedJC.PreJC

        '若是需要換月份
        If AnsRelatedJC.changeMonth Then
            '移動月住
            Bir.Ju(2).Gan = NextGan(Bir.Ju(2).Gan)
            Bir.Ju(2).Tsu = NextTsu(Bir.Ju(2).Tsu)
            '移動5住, 
            Bir.Ju(5).Tsu = NextTsu(Bir.Ju(5).Tsu)
            Bir.Ju(5).Gan = NextGan(Bir.Ju(5).Gan)
        End If

        '依照User選項,若超過23時,可選擇算隔日,修正第三柱
        If (H23Method = "隔日") And (Bir.BornHour = 23) Then
            Bir.Ju(3).Gan = NextGan(Bir.Ju(3).Gan)
            Bir.Ju(3).Tsu = NextTsu(Bir.Ju(3).Tsu)
        End If

        ' 再來算第四與第七柱  
        Bir.Ju(4).Tsu = str24HTsu(Bir.BornHour)
        Bir.Ju(4).Gan = WuSu(Bir.Ju(4).Tsu, Bir.Ju(3).Gan)  ' 五鼠遁, 求時柱
        ' 求第七柱
        Bir.Ju(7).Gan = Gmatch(Bir.Ju(4).Gan)  '天干五合
        Bir.Ju(7).Tsu = Tmatch(Bir.Ju(4).Tsu)  '地支六合

        '求藏干
        GetHiddenGan()

        '排六神四輔(十神)
        SetSenFu()

        '排納音五行
        SetNaIng5Sing()

        '排各柱的五行
        Set5Sing()

        '' 排星座 (八柱天干)
        GetStar()

        ' 排星宿 (八柱地支,西洋,二十八星宿)
        GetStarC()

        ' 定64卦
        Get64Gwa()

        ' 宮次表~星際
        GonChi()

        ' 運線
        YuanShun()

        ' 繪星座圖
        StarSet()

        ' 運程
        YuanChan()

        ' 五行象(金囚、水旺...)
        GetWuSingState()

        ' 五行計數
        CountWuSing()

        '求8柱的神煞
        GetSenSa()

        '神輔計數
        CountSenFu()

        '---ending---
        DBreader.Close()
        UserMsg("編號=" & Bir.No & "姓名=" & Bir.Name & "," & Bir.Sex _
                & ", 民國" & Bir.Solar.Y & "年" & Bir.Solar.M & "月" & Bir.Solar.D & "日" _
                & "; 農曆" & Bir.Lunar.Y & "年" & Bir.Lunar.M & "月" & Bir.Lunar.D & "日" _
                & Bir.BornHour & "時" & Bir.BornMinute & "分")

        '=========================================
        'Ajack2014,add more 
        '取格
        Find8wordKan()
        '取用神 (依照窮通寶鑑,調候用神)
        FingUsingGod()
        '日主強弱
        FindStrongWeak()
        '空亡
        Bir.YearEmpty = FindEmpty(Bir.Ju(1).Gan, Bir.Ju(1).Tsu)
        Bir.DayEmpty = FindEmpty(Bir.Ju(3).Gan, Bir.Ju(3).Tsu)
    End Sub

    '取得前後節氣-----------------------------------------
    'Bir.PreJa = 生辰之前的節     (JC1)
    'Bir.NxtJa = 生辰之後的節     (JC2)
    'Bir.PreJC = 生辰之前的節或氣 (JC3)
    Private Function GetRelatedJC(ByVal PreJCNo As Integer, _
                                  ByVal curJC As String, _
                                  ByVal curTime As String) As RelatedJC
        Dim JC1 As JaChiDateTime
        Dim JC2 As JaChiDateTime
        Dim JC3 As JaChiDateTime
        Dim curHour As String
        Dim curMin As String
        Dim changeMonth As Boolean
        curHour = Mid(curTime, 1, 2)
        curMin = Mid(curTime, 4, 2)
        changeMonth = False
        If curJC = "" Then
            '生辰日不是節氣 {利用資料庫特性:curJC必定為"")
            JC1 = GetJCinDB(PreJCNo)
            If Is12Ja(JC1.Name) <> 0 Then '前一個節氣剛好為節
                JC2 = GetJCinDB(PreJCNo + 2)
                JC3 = JC1
            Else
                JC3 = JC1
                JC2 = GetJCinDB(PreJCNo + 1)
                JC1 = GetJCinDB(PreJCNo - 1)
            End If
        Else
            '生辰日是節氣, 要再細看時間
            '利用資料庫特性: 編號=PreJCNo+1 的即是生辰日節氣
            JC1 = GetJCinDB(PreJCNo + 1)
            If Val(curHour) * 100 + Val(curMin) < Val(JC1.Hour) * 100 + Val(JC1.Min) Then          '生時在節氣前
                If Is12Ja(JC1.Name) <> 0 Then '節氣剛好為節
                    JC2 = JC1                       'N+1
                    JC1 = GetJCinDB(PreJCNo - 1)    'N-1
                    JC3 = GetJCinDB(PreJCNo)        'N
                Else
                    JC1 = GetJCinDB(PreJCNo)      'N
                    JC2 = GetJCinDB(PreJCNo + 2)  'N+2
                    JC3 = JC1                     'N
                End If
            Else         '生時在節氣後
                If Is12Ja(JC1.Name) <> 0 Then '節氣剛好為節
                    changeMonth = True
                    JC1 = JC1                    'N+1
                    JC2 = GetJCinDB(PreJCNo + 3) 'N+ 3
                    JC3 = JC1                    'N+1
                Else
                    JC3 = JC1                    'N+1
                    JC1 = GetJCinDB(PreJCNo)     'N
                    JC2 = GetJCinDB(PreJCNo + 2) 'N+2
                End If
            End If
        End If
        GetRelatedJC.PreJa = JC1
        GetRelatedJC.NxtJa = JC2
        GetRelatedJC.PreJC = JC3
        GetRelatedJC.changeMonth = changeMonth
    End Function

    Private Function GetJCinDB(ByVal JCNo As Integer) As JaChiDateTime
        Dim YMD As String
        Dim JCname As String
        Dim JCtime As String
        YMD = ""
        JCname = ""
        JCtime = ""
        If JCNo < 1 Then
            JCNo = 1
            UserMsg("Error !!! 過早的生辰,資料庫無節氣資料!!!")
        End If
        '查詢節氣資料庫
        SQLstr = " Select * from JaChi where 編號='" & JCNo & "'"
        SQLcmd = New SqlCommand(SQLstr, DBconn)
        DBreader = SQLcmd.ExecuteReader()
        If DBreader.HasRows Then
            Do While DBreader.Read()
                YMD = DBreader("西曆")
                JCname = DBreader("節氣名")
                JCtime = DBreader("時分")
            Loop
        Else
            UserMsg("資料庫裡沒有符合 節氣編號=" + JCNo + "的資料")
        End If
        DBreader.Close()
        SQLcmd.Dispose()
        GetJCinDB.Name = JCname
        GetJCinDB.YMD.Y = Microsoft.VisualBasic.Year(YMD) - 1911
        GetJCinDB.YMD.M = Microsoft.VisualBasic.Month(YMD)
        GetJCinDB.YMD.D = Microsoft.VisualBasic.Day(YMD)
        GetJCinDB.Hour = Microsoft.VisualBasic.Left(JCtime, 2)
        GetJCinDB.Min = Microsoft.VisualBasic.Right(JCtime, 2)
    End Function

    '求藏干
    Private Sub GetHiddenGan()
        Dim str12T As String
        Dim HiddenGan As String()
        str12T = "子丑寅卯辰巳午未申酉戌亥"
        HiddenGan = {"癸", "己癸辛", "甲丙戊", "乙", "戊乙癸", "丙戊庚", "丁己", "己丁乙", "庚壬戊", "辛", "戊辛丁", "壬甲"}
        For i As Integer = 1 To 8
            Bir.Ju(i).hdG = HiddenGan(InStr(str12T, Bir.Ju(i).Tsu) - 1)
        Next
    End Sub

    ' 排神輔1
    Private Sub SetSenFu()
        '以第三柱天干為命元,查詢神輔
        For i As Integer = 1 To 8
            Bir.SenFu.Ju(i).Gan = FindSixSen(Bir.Ju(3).Gan, Bir.Ju(i).Gan)  '八柱天干 
            Bir.SenFu.Ju(i).Tsu = FindSixSen(Bir.Ju(3).Gan, Bir.Ju(i).Tsu)  '八柱地支
            '藏干....長度最少1最多3
            Bir.SenFu.Ju(i).HG1 = FindSixSen(Bir.Ju(3).Gan, Mid(Bir.Ju(i).hdG, 1, 1))
            Bir.SenFu.Ju(i).HG2 = FindSixSen(Bir.Ju(3).Gan, Mid(Bir.Ju(i).hdG, 2, 1))
            Bir.SenFu.Ju(i).HG3 = FindSixSen(Bir.Ju(3).Gan, Mid(Bir.Ju(i).hdG, 3, 1))
        Next
        '最後修改第三柱天干為天元
        Bir.SenFu.Ju(3).Gan = "天元"
    End Sub

    ' 求各柱的納音五行
    Private Sub SetNaIng5Sing()
        For i As Integer = 1 To 8
            Bir.NaIng.Ju(i) = FindNaIng5Sing(Bir.Ju(i).Gan, Bir.Ju(i).Tsu)
        Next
    End Sub

    '求各柱的五行
    Private Sub Set5Sing()
        For i As Integer = 1 To 8
            Bir.WuSing.Ju(i).Gan = Find5Sing(Bir.Ju(i).Gan)
            Bir.WuSing.Ju(i).Tsu = Find5Sing(Bir.Ju(i).Tsu)
            Bir.WuSing.Ju(i).HG1 = Find5Sing(Mid(Bir.Ju(i).hdG, 1, 1))
            Bir.WuSing.Ju(i).HG2 = Find5Sing(Mid(Bir.Ju(i).hdG, 2, 1))
            Bir.WuSing.Ju(i).HG3 = Find5Sing(Mid(Bir.Ju(i).hdG, 3, 1))
        Next

    End Sub

    ' 排星座 '所有八柱的星座 (天干)(行星)
    Private Sub GetStar()
        Dim aryStar As String() = New String(9) {"太陽", "太陰", _
                "火星", "水星", "木星", "土星", "金星", "天王", "海王", "冥王"}
        Dim str10G As String = "甲乙丙丁戊己庚辛壬癸"
        For i As Integer = 1 To 8
            Bir.Star.Ju(i) = aryStar(InStr(str10G, Bir.Ju(i).Gan) - 1)
        Next
    End Sub

    ' 排星宿 (八柱地支,西洋,二十八星宿)
    Private Sub GetStarC()
        Dim aryStarB As String() = New String(11) { _
            "白羊", "金牛", "雙子", "巨蟹", "獅子", "室女", _
            "天秤", "天蠍", "人馬", "摩羯", "寶瓶", "雙魚"}
        Dim aryStarC As String() = New String(11) { _
            "婁胄", "昴畢", "觜參井", "鬼柳", "星張翼軫", "角", _
            "亢氐", "房心尾", "箕斗", "牛女", "虛危", "室壁奎"}
        Dim strTsu1 As String      ' 順行
        Dim strTsu2 As String      ' 逆行
        Dim intAt As Integer
        strTsu1 = "子丑寅卯辰巳午未申酉戌亥子丑寅卯辰巳午未申酉戌亥"
        strTsu2 = "亥戌酉申未午巳辰卯寅丑子亥戌酉申未午巳辰卯寅丑子"

        '如果生年超過了星座西移年,先把星座表(字串)西移一位
        If Bir.Solar.Y > (MoveWestYear - 1911) Then
            aryStarB = {"雙魚", "白羊", "金牛", "雙子", "巨蟹", "獅子", _
                        "室女", "天秤", "天蠍", "人馬", "摩羯", "寶瓶"}
            aryStarC = {"室壁奎", "婁胄", "昴畢", "觜參井", "鬼柳", "星張翼軫", _
                        "角", "亢氐", "房心尾", "箕斗", "牛女", "虛危"}
        End If
        '-----------------------------------------------------------
        '以下撰寫註解,是以2000年以前為準, 2000年過後,星座西移一宮
        ' 第一、八柱由辰起白羊順行 ---------------------------------
        intAt = InStr(strTsu1, Bir.Ju(1).Tsu)
        If intAt <= 4 Then  ' 若已超過辰位，則取下一輪
            intAt = intAt + 12
        End If
        Bir.StarB.Ju(1) = aryStarB(intAt - 4 - 1) ' 辰排第4位
        Bir.StarC.Ju(1) = aryStarC(intAt - 4 - 1)
        intAt = InStr(strTsu1, Bir.Ju(8).Tsu)
        If intAt <= 4 Then  ' 若已超過辰位，則取下一輪
            intAt = intAt + 12
        End If
        Bir.StarB.Ju(8) = aryStarB(intAt - 4 - 1)
        Bir.StarC.Ju(8) = aryStarC(intAt - 4 - 1)

        'OLD Program, It's problem!:  第二、五柱由子起白羊順行
        intAt = InStr(strTsu1, Bir.Ju(2).Tsu)
        Bir.StarB.Ju(2) = aryStarB(intAt - 1)
        Bir.StarC.Ju(2) = aryStarC(intAt - 1)
        intAt = InStr(strTsu1, Bir.Ju(5).Tsu)
        Bir.StarB.Ju(5) = aryStarB(intAt - 1)
        Bir.StarC.Ju(5) = aryStarC(intAt - 1)

        'Ajack2014改寫,依照西林山人書==========
        If StarBJu2Ju5Method = "2" Then
            '第二柱由巳起白羊順行()
            intAt = InStr(strTsu1, Bir.Ju(2).Tsu)
            If intAt <= 5 Then  ' 若已超過巳位，則取下一輪
                intAt = intAt + 12
            End If
            Bir.StarB.Ju(2) = aryStarB(intAt - 5 - 1) ' 巳排第5位
            Bir.StarC.Ju(2) = aryStarC(intAt - 5 - 1)
            '第五柱由巳起白羊 *逆* 行
            intAt = InStr(strTsu2, Bir.Ju(5).Tsu)
            intAt = intAt - 7    ' 巳排第7位
            If intAt < 0 Then intAt = intAt + 12 ' 若已超過巳位，則取下一輪
            Bir.StarB.Ju(5) = aryStarB(intAt)
            Bir.StarC.Ju(5) = aryStarC(intAt)
        End If

        ' 第三、六柱由午起白羊 *逆* 行
        intAt = InStr(strTsu2, Bir.Ju(3).Tsu)
        intAt = intAt - 6    ' 午排第6位
        If intAt < 0 Then intAt = intAt + 12 ' 若已超過午位，則取下一輪
        Bir.StarB.Ju(3) = aryStarB(intAt)
        Bir.StarC.Ju(3) = aryStarC(intAt)
        intAt = InStr(strTsu2, Bir.Ju(6).Tsu)
        intAt = intAt - 6    ' 午排第6位
        If intAt < 0 Then intAt = intAt + 12 ' 若已超過午位，則取下一輪
        Bir.StarB.Ju(6) = aryStarB(intAt)
        Bir.StarC.Ju(6) = aryStarC(intAt)

        ' 第四、七柱由戌起白羊 *逆* 行
        intAt = InStr(strTsu2, Bir.Ju(4).Tsu)
        intAt = intAt - 2   ' 戌排第2位
        If intAt < 0 Then intAt = intAt + 12 ' 若已超過戌位，則取下一輪
        Bir.StarB.Ju(4) = aryStarB(intAt)
        Bir.StarC.Ju(4) = aryStarC(intAt)
        intAt = InStr(strTsu2, Bir.Ju(7).Tsu)
        intAt = intAt - 2   ' 戌排第2位
        If intAt < 0 Then intAt = intAt + 12 ' 若已超過戌位，則取下一輪
        Bir.StarB.Ju(7) = aryStarB(intAt)
        Bir.StarC.Ju(7) = aryStarC(intAt)
    End Sub

    Private Sub Get64Gwa()
        Dim aryGwa As String()
        Dim aryGwaNo As Integer()
        Dim aryGwaName As String()
        Dim strAllGan As String
        Dim strAllTsu As String
        Dim All60GT As String()
        Dim All60Gwa As String()
        strAllGan = "甲乙丙丁戊己庚辛壬癸"
        strAllTsu = "子丑寅卯辰巳午未申酉戌亥"
        aryGwa = {"乾", "兌", "離", "震", "巽", "坎", "艮", "坤", "乾", "兌", "離", "震"}
        aryGwaNo = {1, 2, 3, 4, 5, 6, 7, 8, 1, 2, 3, 4}
        aryGwaName = {"乾為天", "天澤履", "天火同人", "天雷無妄", "天風姤", "天水訟", "天山遯", "天地否", _
                  "澤天夬", "兌為澤", "澤火革", "澤雷隨", "澤風大過", "澤水困", "澤山咸", "澤地萃", _
                  "火天大有", "火澤睽", "離為火", "火雷噬嗑", "火風鼎", "火水未濟", "火山旅", "火地晉", _
                  "雷天大壯", "雷澤歸妹", "雷火丰", "震為雷", "雷風恆", "雷水解", "雷山小過", "雷地豫", _
                  "風天小畜", "風澤中孚", "風火家人", "風雷益", "巽為風", "風水渙", "風山漸", "風地觀", _
                  "水天需", "水澤節", "水火既濟", "水雷屯", "水風井", "坎為水", "水山蹇", "水地比", _
                  "山天大畜", "山澤損", "山火賁", "山雷頤", "山風蠱", "山水蒙", "艮為山", "山地剝", _
                  "地天泰", "地澤臨", "地火明夷", "地雷復", "地風升", "地水師", "地山謙", "坤為地"}
        All60GT = {"甲子", "乙丑", "丙寅", "丁卯", "戊辰", "己巳", "庚午", "辛未", "壬申", "癸酉", _
                   "甲戌", "乙亥", "丙子", "丁丑", "戊寅", "己卯", "庚辰", "辛巳", "壬午", "癸未", _
                   "甲申", "乙酉", "丙戌", "丁亥", "戊子", "己丑", "庚寅", "辛卯", "壬辰", "癸巳", _
                   "甲午", "乙未", "丙申", "丁酉", "戊戌", "己亥", "庚子", "辛丑", "壬寅", "癸卯", _
                   "甲辰", "乙巳", "丙午", "丁未", "戊申", "己酉", "庚戌", "辛亥", "壬子", "癸丑", _
                   "甲寅", "乙卯", "丙辰", "丁巳", "戊午", "己未", "庚申", "辛酉", "壬戌", "癸亥"}
        All60Gwa = {"地雷復坤", "火雷噬嗑", "風火家人", "山澤損", "天澤履", "雷天大壯", "雷風恒", "天水訟", "地水師", "風山漸", _
                    "水山蹇", "火地晉", "山雷頤", "澤雷隨", "雷火豐", "水澤節", "地天泰", "火天大有", "巽為風", "澤水困", _
                    "水火既濟", "天山遯", "艮為山", "雷地豫", "水雷屯", "天雷無妄", "澤火革離", "風澤中孚", "山天大畜", "澤天夬", _
                    "天風姤乾", "水風井", "雷水解", "澤山咸", "地山謙", "風地觀", "風雷益", "地火明夷", "天火同人", "雷澤歸妹", _
                    "火澤睽", "水天需", "澤風大過", "山風蠱", "風水渙", "火山旅", "天地否", "水地比", "震為雷", "山火賁", _
                    "水火既濟", "地澤臨", "兌為澤", "風天小畜", "火風鼎", "地風升", "山水蒙坎", "雷山小過", "澤地萃", "山地剝"}
        For i As Integer = 1 To 8
            '天干求上卦
            Bir.Gwa.Ju(i).UGwa = aryGwa(InStr(strAllGan, Bir.Ju(i).Gan) - 1)
            '上卦數字
            Bir.Gwa.Ju(i).UGwaNo = aryGwaNo(InStr(strAllGan, Bir.Ju(i).Gan) - 1)
            '地支求下卦
            Bir.Gwa.Ju(i).DGwa = aryGwa(InStr(strAllTsu, Bir.Ju(i).Tsu) - 1)
            '下卦數字
            Bir.Gwa.Ju(i).DGwaNo = aryGwaNo(InStr(strAllTsu, Bir.Ju(i).Tsu) - 1)
            '六十四卦卦名
            Bir.Gwa.Ju(i).GwaName = aryGwaName((Bir.Gwa.Ju(i).UGwaNo - 1) * 8 + (Bir.Gwa.Ju(i).DGwaNo - 1))
            'Ajack2013六十四卦,也標記乾坤坎離四卦,配六十甲子
            Bir.Gwa.Ju(i).GwaName2 = ""
            For m As Integer = 0 To 59
                If All60GT(m) = Bir.Ju(i).Gan & Bir.Ju(i).Tsu Then
                    Bir.Gwa.Ju(i).GwaName2 = All60Gwa(m)
                End If
            Next
        Next
    End Sub

    ' 宮次表~星際
    Private Sub GonChi()
        '宣告字串,以便用第二柱地支查出幾月
        Dim MonthIndex_Tsu As String = "地寅卯辰巳午未申酉戌亥子丑"
        'For 新法                        地  寅 卯  辰  巳 午 未 申 酉 戌 亥 子 丑
        Dim MonthIndex_no As Integer() = {0, 1, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2}
        '宣告字串,以便用幾月,依照子位起逆行,算月將(太陽立宮)(過中氣加一宮)
        Dim Month_Revise_Tsu As String = "地子亥戌酉申未午巳辰卯寅丑子"
        '月將(太陽立宮)
        Dim SunAt12Kon As String
        '宣告字串,以便用第四柱地支順乎到卯的距離
        Dim HourIndex_Tsu As String = "地子丑寅卯辰巳午未申酉戌亥"  '子丑寅卯"
        ' For 新法                      地  子 丑 寅 卯  辰  巳 午 未 申 酉 戌 亥"
        Dim HourIndex_No As Integer() = {0, 3, 2, 1, 12, 11, 10, 9, 8, 7, 6, 5, 4}
        Dim HourIndex_distance As Integer
        '宣告字串,以便依照距離,算出命宮地支
        Dim MingKon_long_Tsu As String = "地子丑寅卯辰巳午未申酉戌亥子丑寅卯辰巳午未申酉戌亥"
        '命宮
        Dim MingAt12Kon As String
        Dim SexType As String        '陽男陰女...
        Dim ALL12Tsu As String       '地支字串
        Dim AllYearKing As String()  '十二歲君
        Dim AllStar12 As String()    '十二宮星座
        Dim intAt As Integer
        Dim intAt1 As Integer
        Dim intAt2 As Integer
        Dim intMinKon As Integer
        Dim intChenPOS As Integer     'int辰位
        Dim ary12LongLife As String() 'ary長生
        Dim ary12KonName As String() = New String() {"命宮", "財帛", "兄弟", "田宅", "男女", _
                                            "奴僕", "配偶", "疾厄", "遷移", "官祿", "福德", "相貌"}

        '依照陽男陰女/陰男陽女,設定順行逆行
        SexType = Bir.SType & Bir.Sex
        If SexType = "陽男" Or SexType = "陰女" Then
            ALL12Tsu = "子丑寅卯辰巳午未申酉戌亥子丑寅卯辰巳午未申酉戌亥子丑寅卯辰巳午未申酉戌亥"  '順行
        Else
            ALL12Tsu = "亥戌酉申未午巳辰卯寅丑子亥戌酉申未午巳辰卯寅丑子亥戌酉申未午巳辰卯寅丑子"  '逆行
        End If

        'OLD program 找命宮==================
        '舊程式是使用第三柱與第四柱,符合書本(數運第一部,生命支玄機.page146)支敘述
        '但是,書本最後一頁列印的命例,卻不符合上述公式, 反而符合第二第四柱去計算
        '此處以第二第四柱計算
        '------------------------------------------------------
        ' 計算由第二柱地支順行至卯位是幾位
        intAt = InStr(ALL12Tsu, Bir.Ju(2).Tsu)
        intAt1 = InStr(intAt, ALL12Tsu, "卯") - intAt
        ' 所得位數，再由第四柱地支順數到之地支位，其位為命宮
        If intAt1 < 0 Then
            intAt1 = intAt1 + 12
        End If
        intMinKon = InStr(ALL12Tsu, Bir.Ju(4).Tsu) + intAt1
        MingAt12Kon = Mid(ALL12Tsu, intMinKon, 1)

        If MingKonMethod = "月將" Then
            'Ajack2014找命宮方法=============
            '先找月將(太陽立宮)
            '如果生辰已過中氣,則需加一宮
            If Is12Ja(Bir.PreJC.Name) <> 0 Then '未過中氣
                SunAt12Kon = Mid(Month_Revise_Tsu, InStr(MonthIndex_Tsu, Bir.Ju(2).Tsu), 1)
                UserMsg("未過中氣")
            Else    '過中氣
                SunAt12Kon = Mid(Month_Revise_Tsu, InStr(MonthIndex_Tsu, Bir.Ju(2).Tsu) + 1, 1)
                UserMsg("過中氣" & Bir.PreJC.Name)
            End If
            UserMsg("太陽立宮在:" & SunAt12Kon)
            '再找命宮
            HourIndex_distance = 4 - InStr(HourIndex_Tsu, Bir.Ju(4).Tsu) '算到卯(4)
            If HourIndex_distance < 0 Then
                HourIndex_distance = HourIndex_distance + 12
            End If
            MingAt12Kon = MingKon_long_Tsu(InStr(HourIndex_Tsu, SunAt12Kon) + HourIndex_distance)
            intMinKon = InStr(ALL12Tsu, MingAt12Kon)
            '------- end -----Ajack2014找命宮---------
        End If
        If MingKonMethod = "數" Then
            '找命宮新法=============
            '月支,時支計算數字
            Dim Month_IN As Integer = InStr(MonthIndex_Tsu, Bir.Ju(2).Tsu) - 1
            Dim Time_IN As Integer = InStr(HourIndex_Tsu, Bir.Ju(4).Tsu) - 1
            Dim Month_No As Integer = MonthIndex_no(Month_IN)
            Dim Time_No As Integer = HourIndex_No(Time_IN)
            Dim MK_no As Integer = Month_No + Time_No
            UserMsg("月支數字=" & Month_No & ", 時支數字=" & Time_No)
            If SexType = "陽男" Or SexType = "陰女" Then
                MK_no = MK_no - 1
            End If
            If MK_no > 12 Then
                MK_no = MK_no - 12
            End If
            MingAt12Kon = MingKon_long_Tsu(MK_no)
            intMinKon = InStr(ALL12Tsu, MingAt12Kon)
            '------- end -----找命宮新法---------
        End If
        UserMsg("命宮在:" & MingAt12Kon)

        '計算宮次 ===================================
        For i As Integer = intMinKon To (intMinKon + 11)
            Bir.Kon12(i - intMinKon + 1) = Mid(ALL12Tsu, i, 1)
            If Bir.Kon12(i - intMinKon + 1) = "辰" Then
                intChenPOS = i - intMinKon + 1
            End If
        Next
        'UserMsg("12宮次=" & Bir.Kon12(1) & Bir.Kon12(2) & Bir.Kon12(3) & Bir.Kon12(4) & Bir.Kon12(5) & Bir.Kon12(6) _
        '        & Bir.Kon12(7) & Bir.Kon12(8) & Bir.Kon12(9) & Bir.Kon12(10) & Bir.Kon12(11) & Bir.Kon12(12))

        '計算 限  =================================
        Bir.Shan12(1) = Bir.Kon12(1)
        For i As Integer = 2 To 12
            Bir.Shan12(i) = Bir.Kon12(14 - i)
        Next
        'UserMsg("12限=" & Bir.Shan12(1) & Bir.Shan12(2) & Bir.Shan12(3) & Bir.Shan12(4) & Bir.Shan12(5) & Bir.Shan12(6) _
        '       & Bir.Shan12(7) & Bir.Shan12(8) & Bir.Shan12(9) & Bir.Shan12(10) & Bir.Shan12(11) & Bir.Shan12(12))

        '準備好12長生的字串陣列,依照日主天干
        Select Case Bir.Ju(3).Gan
            Case "甲"
                ary12LongLife = {"沐浴", "冠帶", "臨官", "帝旺", "衰", "病", "死", "墓", "絕", "胎", "養", "長生", "沐浴", "冠帶", "臨官", "帝旺", "衰", "病", "死", "墓", "絕", "胎", "養", "長生"}
            Case "乙"
                ary12LongLife = {"病", "衰", "帝旺", "臨官", "冠帶", "沐浴", "長生", "養", "胎", "絕", "墓", "死", "病", "衰", "帝旺", "臨官", "冠帶", "沐浴", "長生", "養", "胎", "絕", "墓", "死"}
            Case "丙", "戊"
                ary12LongLife = {"胎", "養", "長生", "沐浴", "冠帶", "臨官", "帝旺", "衰", "病", "死", "墓", "絕", "胎", "養", "長生", "沐浴", "冠帶", "臨官", "帝旺", "衰", "病", "死", "墓", "絕"}
            Case "丁", "己"
                ary12LongLife = {"絕", "墓", "死", "病", "衰", "帝旺", "臨官", "冠帶", "沐浴", "長生", "養", "胎", "絕", "墓", "死", "病", "衰", "帝旺", "臨官", "冠帶", "沐浴", "長生", "養", "胎"}
            Case "庚"
                ary12LongLife = {"死", "墓", "絕", "胎", "養", "長生", "沐浴", "冠帶", "臨官", "帝旺", "衰", "病", "死", "墓", "絕", "胎", "養", "長生", "沐浴", "冠帶", "臨官", "帝旺", "衰", "病"}
            Case "辛"
                ary12LongLife = {"長生", "養", "胎", "絕", "墓", "死", "病", "衰", "帝旺", "臨官", "冠帶", "沐浴", "長生", "養", "胎", "絕", "墓", "死", "病", "衰", "帝旺", "臨官", "冠帶", "沐浴"}
            Case "壬"
                ary12LongLife = {"帝旺", "衰", "病", "死", "墓", "絕", "胎", "養", "長生", "沐浴", "冠帶", "臨官", "帝旺", "衰", "病", "死", "墓", "絕", "胎", "養", "長生", "沐浴", "冠帶", "臨官"}
            Case Else '  "癸"
                ary12LongLife = {"臨官", "冠帶", "沐浴", "長生", "養", "胎", "絕", "墓", "死", "病", "衰", "帝旺", "臨官", "冠帶", "沐浴", "長生", "養", "胎", "絕", "墓", "死", "病", "衰", "帝旺"}
        End Select

        '定十二宮的12長生 =================
        For i As Integer = 1 To 12
            If Bir.Kon12(i) = "子" Then
                intAt = 12 - i
                Exit For
            End If
        Next    '找出"子"的宮次
        'UserMsg("12長生=...")
        If SexType = "陽男" Or SexType = "陰女" Then
            For i As Integer = 1 To 12
                Bir.LongLife12.Kon(i) = ary12LongLife((i + intAt))
                'UserMsg(Bir.LongLife12.Kon(i))
            Next
        Else
            For i As Integer = 1 To 12
                Bir.LongLife12.Kon(i) = ary12LongLife(24 - (i + intAt))
                'UserMsg(Bir.LongLife12.Kon(i))
            Next
        End If

        '八柱的長生與宮位  ================
        For i As Integer = 1 To 8
            For k As Integer = 1 To 12
                If Bir.Kon12(k) = Bir.Ju(i).Tsu Then
                    Bir.LongLife12.Ju(i) = Bir.LongLife12.Kon(k)
                    Bir.MiKon.Ju(i) = ary12KonName(k - 1)
                End If
            Next
        Next

        '計算"運"============
        ' 1.查第一柱(年柱)之地支於十二命宮中.算出相同之地支所在宮位
        If SexType = "陽男" Or SexType = "陰女" Then
            ALL12Tsu = "子丑寅卯辰巳午未申酉戌亥子丑寅卯辰巳午未申酉戌亥"  '順行
        Else
            ALL12Tsu = "亥戌酉申未午巳辰卯寅丑子亥戌酉申未午巳辰卯寅丑子"  '逆行
        End If
        intAt = InStr(ALL12Tsu, Bir.Ju(1).Tsu)    ' 年柱地支是第幾位
        ' 計算應把 年柱地支 填到 宮次 的第幾位
        intAt1 = (12 - InStr(ALL12Tsu, Bir.Kon12(1)) + 1) + intAt
        If intAt1 > 12 Then intAt1 = intAt1 - 12 ' 填到 intAt1 位
        ' 2.查第四柱(時柱)之地支，以此地支為始填入與所在宮位之運位。
        intAt2 = InStr(ALL12Tsu, Bir.Ju(4).Tsu)   ' 時柱地支是第幾位
        For intLop = 1 To 12
            Bir.Yuan12(intAt1) = Mid(ALL12Tsu, intAt2, 1)  '運(十二宮)
            intAt1 = intAt1 + 1
            If intAt1 > 12 Then intAt1 = 1
            intAt2 = intAt2 + 1
        Next
        ' 神輔注入
        For intLop = 1 To 12
            Bir.SenFu12(intLop) = FindSixSen(Bir.Ju(3).Gan, Bir.Kon12(intLop))
        Next

        ' 星際--------------------------------------
        Call GetStarSky()

        ' 排12歲君------------
        ' 1.查第一柱(年柱)之地支與 命宮 算出相同之地支所在宮位為太歲
        ' 陽男、陰女 - 順行
        If SexType = "陽男" Or SexType = "陰女" Then
            ALL12Tsu = "子丑寅卯辰巳午未申酉戌亥子丑寅卯辰巳午未申酉戌亥"
            AllYearKing = {"太歲", "太陽", "喪門", "太陰", "五鬼", "死符", "歲破", "龍德", "白虎", "福德", "天狗", "病符"}
        Else  ' 陽女、陰男 - 逆行
            ALL12Tsu = "亥戌酉申未午巳辰卯寅丑子亥戌酉申未午巳辰卯寅丑子"
            AllYearKing = {"太歲", "病符", "天狗", "福德", "白虎", "龍德", "歲破", "死符", "五鬼", "太陰", "喪門", "太陽"}
        End If
        intAt = InStr(ALL12Tsu, Bir.Ju(1).Tsu)    ' 年柱地支是第幾位
        ' 計算應把 "太歲" 填到 歲君 的第幾位
        intAt1 = (12 - InStr(ALL12Tsu, Bir.Kon12(1)) + 1) + intAt
        If intAt1 > 12 Then intAt1 = intAt1 - 12 ' 填到 intAt1 位
        For intLop = 1 To 12
            Bir.YearKing(intAt1) = AllYearKing(intLop - 1)
            intAt1 = intAt1 + 1
            If intAt1 > 12 Then intAt1 = 1
        Next

        ' 排12星座
        ' 辰起白羊 (2000年西移年以前)
        ' 陽男、陰女 - 順行
        If SexType = "陽男" Or SexType = "陰女" Then
            If Bir.Solar.Y < (MoveWestYear - 1911) Then
                AllStar12 = {"白羊", "金牛", "雙子", "巨蟹", "獅子", "室女", "天秤", "天蠍", "人馬", "摩羯", "寶瓶", "雙魚"}
            Else    '如果生年超過了星座西移年,先把星座表(字串)西移一位
                AllStar12 = {"雙魚", "白羊", "金牛", "雙子", "巨蟹", "獅子", "室女", "天秤", "天蠍", "人馬", "摩羯", "寶瓶"}
            End If
        Else  ' 陽女、陰男 - 逆行
            If Bir.Solar.Y < (MoveWestYear - 1911) Then
                AllStar12 = {"白羊", "雙魚", "寶瓶", "摩羯", "人馬", "天蠍", "天秤", "室女", "獅子", "巨蟹", "雙子", "金牛"}
            Else    '如果生年超過了星座西移年,先把星座表(字串)西移一位
                AllStar12 = {"雙魚", "寶瓶", "摩羯", "人馬", "天蠍", "天秤", "室女", "獅子", "巨蟹", "雙子", "金牛", "白羊"}
            End If
        End If
        For intLop = 1 To 12
            If intChenPOS > 12 Then intChenPOS = intChenPOS - 12
            Bir.StarB2(intChenPOS) = AllStar12(intLop - 1)
            intChenPOS = intChenPOS + 1
        Next
    End Sub

    Private Sub GetStarSky()
        Dim lop As Integer
        Dim strGet As String
        Dim intAt As Integer
        Dim intCnt As Integer
        Dim strCHK As String = ""
        For lop = 1 To 12
            strGet = FindStarSky(Bir.Ju(1).Tsu, _
                                 Bir.Ju(2).Tsu, _
                                 Bir.Ju(3).Gan, _
                                 Bir.Kon12(lop))
            intCnt = 1
            While Len(strGet) > 1
                intAt = InStr(strGet, ",")
                If intAt = 0 Then
                    ' 把Parse出來的星名，放到Bir.星際 , 不過要先看看此星是否已重覆
                    Call AddToSinJi(lop, intCnt, strGet)
                    strGet = ""
                Else
                    ' 把Parse出來的星名，放到Bir.星際 , 不過要先看看此星是否已重覆
                    Call AddToSinJi(lop, intCnt, Microsoft.VisualBasic.Left(strGet, intAt - 1))
                    strGet = Mid(strGet, intAt + 1)
                End If
                intCnt = intCnt + 1
            End While
        Next
    End Sub

    ' 把Parse出來的星名，放到Bir.星際 , 不過要先看看此星是否已重覆
    Private Sub AddToSinJi(ByVal intFlag1 As Integer, _
                           ByRef intFlag2 As Integer, _
                           ByVal strStar As String)
        Dim intLop As Integer
        For intLop = 1 To intFlag2
            If Bir.StarSky(intFlag1, intLop) = strStar Then
                ' 把傳址而來的intCnt減一, 防止跳下一格
                intFlag2 = intFlag2 - 1
                Exit Sub
            End If
        Next
        Bir.StarSky(intFlag1, intLop - 1) = strStar
    End Sub

    ' 排60年運線'計算歲數
    Private Sub YuanShun()
        Dim intLop, intLop1, intLop2 As Integer
        Dim All_Gan, All_Tsu As String
        Dim TodayYMD As String
        Dim TodayGT As String
        Dim TodayYearIndex_1, TodayYearIndex_2 As Integer
        All_Gan = "甲乙丙丁戊己庚辛壬癸"
        All_Tsu = "子丑寅卯辰巳午未申酉戌亥"
        ' 年柱的支干所在，為運線起點
        intLop1 = InStr(All_Gan, Bir.Ju(1).Gan)
        intLop2 = InStr(All_Tsu, Bir.Ju(1).Tsu)
        For intLop = 1 To 60
            Bir.Y60state(intLop1, intLop2) = intLop
            ' 跳到下一年的干支
            intLop1 = intLop1 + 1
            If intLop1 = 11 Then intLop1 = 1
            intLop2 = intLop2 + 1
            If intLop2 = 13 Then intLop2 = 1
        Next
        '計算歲數
        TodayYMD = Now.Year & "/" & Now.Month & "/" & Now.Day
        TodayGT = GetDateYMDGT(TodayYMD)
        TodayYearIndex_1 = InStr(All_Gan, Mid(TodayGT, 1, 1))
        TodayYearIndex_2 = InStr(All_Tsu, Mid(TodayGT, 2, 1))
        Bir.Age = Bir.Y60state(TodayYearIndex_1, TodayYearIndex_2)
        'UserMsg("今年歲次=" & Mid(TodayGT, 1, 1) & ", 歲數=" & Bir.Age)
    End Sub

    ' 繪星座圖
    Private Sub StarSet()
        Dim intAngle As Integer
        Dim strBornDate As String
        Dim strPreJCDate As String
        Dim astrJachi As String()
        Dim aintAngle As Integer()
        Dim datD1 As Date
        Dim datD2 As Date
        Dim intLop As Integer
        astrJachi = {"立春", "雨水", "驚蟄", "春分", "清明", "穀雨", _
                     "立夏", "小滿", "芒種", "夏至", "小暑", "大暑", _
                     "立秋", "處暑", "白露", "秋分", "寒露", "霜降", _
                     "立冬", "小雪", "大雪", "冬至", "小寒", "大寒"}
        aintAngle = {315, 330, 345, 0, 15, 30, _
                     45, 60, 75, 90, 105, 120, _
                     135, 150, 165, 180, 195, 210, _
                     225, 240, 255, 270, 285, 300}
        ' 出生日
        strBornDate = Format(Bir.Solar.Y + 1911, "0000") & "/" & _
                      Format(Bir.Solar.M, "00") & "/" & _
                      Format(Bir.Solar.D, "00") & " " & _
                      Format(Bir.BornHour, "00") & ":" & _
                      Format(Bir.BornMinute, "00")
        datD1 = strBornDate
        '先前的節氣
        strPreJCDate = Format(Bir.PreJC.YMD.Y + 1911, "0000") & "/" & _
                       Format(Bir.PreJC.YMD.M, "00") & "/" & _
                       Format(Bir.PreJC.YMD.D, "00") & " " & _
                       Format(CInt(Bir.PreJC.Hour), "00") & ":" & _
                       Format(CInt(Bir.PreJC.Min), "00")
        datD2 = strPreJCDate
        'A.計算黃經
        ' 1.與上一節氣差幾天, 一日行一度
        intAngle = Abs(DateDiff("d", datD1, datD2))
        ' 2.加上上一節氣的度數
        For intLop = 0 To 23
            If astrJachi(intLop) = Bir.PreJC.Name Then
                Exit For
            End If
        Next
        intAngle = intAngle + aintAngle(intLop)
        Bir.YellowGing.BornDate = intAngle Mod 360
        'B.計算已行
        Bir.YellowGing.Passed = Abs(DateDiff("yyyy", Today, datD1))
        Bir.YellowGing.Passed = Bir.YellowGing.Passed Mod 360
        'C.計算現在
        Bir.YellowGing.NowDate = Bir.YellowGing.Passed + Bir.YellowGing.BornDate
        Bir.YellowGing.NowDate = Bir.YellowGing.NowDate Mod 360
        'D.象限
        If Bir.YellowGing.BornDate < 90 Then
            Bir.YellowGing.Quadrant = 1
        ElseIf Bir.YellowGing.BornDate >= 90 And Bir.YellowGing.BornDate < 180 Then
            Bir.YellowGing.Quadrant = 4
        ElseIf Bir.YellowGing.BornDate >= 180 And Bir.YellowGing.BornDate < 270 Then
            Bir.YellowGing.Quadrant = 3
        ElseIf Bir.YellowGing.BornDate >= 270 Then
            Bir.YellowGing.Quadrant = 2
        End If
    End Sub

    '算運程
    Private Sub YuanChan()
        Dim SexType As String   '陽男陰女
        Dim AllGan As String
        Dim AllTsu As String
        Dim Gan_pos As Integer
        Dim Tsu_pos As Integer
        Dim strBornDate As String
        Dim strJaDate As String
        Dim datD1 As Date
        Dim datD2 As Date
        Dim datD3 As Date
        Dim intDiffHour As Integer
        Dim tmpH As Byte
        Dim intD As Integer
        Dim intM As Integer
        Dim intY As Integer
        Dim intAt As Integer
        Dim YMD2 As String = ""
        Dim Y_GT As String = ""
        Dim YGan As String = ""
        Dim M_GT As String = ""
        Dim D_GT As String = ""
        Dim CurTime As String = ""
        Dim CurJC As String = ""
        Dim PreJCNo As Integer
        Dim AnsRelatedJC As RelatedJC
        Dim aryG As String()
        Dim aryStar As String()
        Dim UseNextDay As Boolean = False
        Dim strGet As String
        Dim intAAt As Integer
        Dim intCnt As Integer
        Dim strTmp As String

        '依照陽男陰女/陰男陽女,設定順行逆行
        SexType = Bir.SType & Bir.Sex
        If SexType = "陽男" Or SexType = "陰女" Then    ' 陽男、陰女 - 順行
            AllGan = "甲乙丙丁戊己庚辛壬癸甲乙丙丁戊己庚辛壬癸"
            AllTsu = "子丑寅卯辰巳午未申酉戌亥子丑寅卯辰巳午未申酉戌亥"
        Else   ' 陽女、陰男 - 逆行
            AllGan = "癸壬辛庚己戊丁丙乙甲癸壬辛庚己戊丁丙乙甲"
            AllTsu = "亥戌酉申未午巳辰卯寅丑子亥戌酉申未午巳辰卯寅丑子"
        End If
        '以月柱為準
        Gan_pos = InStr(AllGan, Bir.Ju(2).Gan)
        Tsu_pos = InStr(AllTsu, Bir.Ju(2).Tsu)

        '1.陽男陰女->順行; 陽女陰男->逆行，由生月干支排出運程干支
        For intLop = 1 To 10
            Gan_pos = Gan_pos + 1
            Tsu_pos = Tsu_pos + 1
            Bir.BigYuan(intLop).Gan = Mid(AllGan, Gan_pos, 1)
            Bir.BigYuan(intLop).Tsu = Mid(AllTsu, Tsu_pos, 1)
        Next

        '2.算上運=由出生日到最近的"入節"之日 
        '(陽男陰女->順行 取下一個節氣; 陽女陰男->逆行 取上一個節氣)
        ' 出生日
        tmpH = To12Hour(Bir.BornHour)
        If tmpH = 24 Then
            tmpH = 0
            UseNextDay = True
        Else
            UseNextDay = False
        End If
        If UseNextDay = False Then
            strBornDate = Format(Bir.Solar.Y + 1911, "0000") & "/" & _
                         Format(Bir.Solar.M, "00") & "/" & _
                         Format(Bir.Solar.D, "00") & " " & _
                         Format(tmpH, "00") & ":" & "00"
        Else
            strBornDate = Format(Bir.SolarNextDay.Y + 1911, "0000") & "/" & _
                         Format(Bir.SolarNextDay.M, "00") & "/" & _
                         Format(Bir.SolarNextDay.D, "00") & " " & _
                         Format(tmpH, "00") & ":" & "00"
        End If
        datD1 = strBornDate

        '先前的節氣或者往後的節氣
        ' 求出相差幾小時
        If SexType = "陽男" Or SexType = "陰女" Then    ' 陽男、陰女 - 順行
            tmpH = To12Hour(CInt(Bir.NxtJa.Hour))
            If tmpH = 24 Then
                tmpH = 0
                UseNextDay = True
            Else
                UseNextDay = False
            End If
            strJaDate = Format(Bir.NxtJa.YMD.Y + 1911, "0000") & "/" & _
                        Format(Bir.NxtJa.YMD.M, "00") & "/" & _
                        Format(Bir.NxtJa.YMD.D, "00") & " " & _
                        Format(tmpH, "00") & ":" & "00"
        Else   ' 陽女、陰男 - 逆行
            tmpH = To12Hour(CInt(Bir.PreJa.Hour))
            If tmpH = 24 Then
                tmpH = 0
                UseNextDay = True
            Else
                UseNextDay = False
            End If
            strJaDate = Format(Bir.PreJa.YMD.Y + 1911, "0000") & "/" & _
                        Format(Bir.PreJa.YMD.M, "00") & "/" & _
                        Format(Bir.PreJa.YMD.D, "00") & " " & _
                        Format(tmpH, "00") & ":" & "00"
        End If

        '求出總共相差多少小時
        datD2 = strJaDate
        intDiffHour = Abs(DateDiff("h", datD1, datD2))
        If SexType = "陽男" Or SexType = "陰女" Then    ' 陽男、陰女 - 順行
            If UseNextDay Then
                intDiffHour = intDiffHour + 24
            End If
        Else   ' 陽女、陰男 - 逆行
            If UseNextDay Then
                intDiffHour = intDiffHour - 24
            End If
        End If


        ' 求出相差幾天幾個時辰
        Bir.Ja2Born_D = Int(intDiffHour / 24)
        'UserMsg("天數(小時算)=" & Bir.Ja2Born_D)
        Bir.Ja2Born_H = Int(intDiffHour Mod 24) / 2
        'UserMsg("時辰數=" & Bir.Ja2Born_H)
        ' 每三天(72小時)換算一歲
        If intDiffHour > 72 Then
            intY = Int(intDiffHour / 72)
            intDiffHour = intDiffHour - 72 * intY
        End If
        ' 每一天(24小時)換算四個月
        If intDiffHour >= 24 Then
            intM = Int(intDiffHour / 24) * 4
            intDiffHour = intDiffHour - 24 * (intM / 4)
        End If
        ' 每一時辰(2小時)換算十天
        If intDiffHour >= 2 Then
            intD = Int(intDiffHour / 2) * 10
        End If
        If intD >= 30 Then
            intM = intM + Int(intD / 30)
            intD = intD Mod 30
        End If
        If intM >= 12 Then
            intY = intY + Int(intM / 12)
            intM = intM Mod 12
        End If
        Bir.YuanStart.Y = intY
        Bir.YuanStart.M = intM
        Bir.YuanStart.D = intD
        '---------------
        'datD3 = DateAdd(DateInterval.Year, intY, datD1)
        'datD3 = DateAdd(DateInterval.Month, intM, datD3)
        'datD3 = DateAdd(DateInterval.Day, intD, datD3)
        '2015/06/14, 應以農曆一年360天計算
        datD3 = DateAdd(DateInterval.Day, intY * 360, datD1)
        datD3 = DateAdd(DateInterval.Day, intM * 30, datD3)
        datD3 = DateAdd(DateInterval.Day, intD, datD3)
        Bir.YuanStartDate1 = FormatDateTime(datD3, vbShortDate)
        CurTime = FormatDateTime(datD3, vbShortTime)

        '取得日期的年月日干支
        SQLstr = " Select * from SAM where 西曆='" & Bir.YuanStartDate1 & "'"
        SQLcmd = New SqlCommand(SQLstr, DBconn)
        DBreader = SQLcmd.ExecuteReader()
        If DBreader.HasRows Then
            Do While DBreader.Read()
                YMD2 = DBreader("農曆")
                Y_GT = DBreader("一柱")
                M_GT = DBreader("二柱")
                D_GT = DBreader("三柱")
                PreJCNo = DBreader("前節氣號")
                CurJC = System.Convert.ToString(DBreader("節氣"))
            Loop
            DBreader.Close()
            SQLcmd.Dispose()
            Bir.YuanStartDate2 = YMD2
            Bir.YuanStart_YearGT = Y_GT
            '每逢?年干交脫
            YGan = Microsoft.VisualBasic.Left(Y_GT, 1)
            If YGan = "甲" Or YGan = "己" Then
                Bir.YuanChange_YG = "甲、己"
            ElseIf YGan = "乙" Or YGan = "庚" Then
                Bir.YuanChange_YG = "乙、庚"
            ElseIf YGan = "丙" Or YGan = "辛" Then
                Bir.YuanChange_YG = "丙、辛"
            ElseIf YGan = "丁" Or YGan = "壬" Then
                Bir.YuanChange_YG = "丁、壬"
            ElseIf YGan = "戊" Or YGan = "癸" Then
                Bir.YuanChange_YG = "戊、癸"
            End If
            '每逢交脫的月份
            Bir.YuanChange_M = Mid(YMD2, 6, 2)
            '每逢交脫的前一個節氣名
            AnsRelatedJC = GetRelatedJC(PreJCNo, CurJC, CurTime)
            Bir.YuanChange_JC = AnsRelatedJC.PreJa.Name
            '每逢交脫的節氣後天數
            datD1 = Format(AnsRelatedJC.PreJa.YMD.Y + 1911, "0000") & "/" & _
                    Format(AnsRelatedJC.PreJa.YMD.M, "00") & "/" & _
                    Format(AnsRelatedJC.PreJa.YMD.D, "00")
            datD2 = Bir.YuanStartDate1
            Bir.YuanChange_JCday = Abs(DateDiff("d", datD1, datD2))

            aryG = {"命宮", "財帛", "兄弟", "田宅", "男女", "奴僕", _
                    "配偶", "疾厄", "遷移", "官祿", "福德", "相貌"}
            AllGan = "甲乙丙丁戊己庚辛壬癸"
            aryStar = {"太陽", "太陰", "火星", "水星", "木星", "土星", _
                       "金星", "天王", "海王", "冥王"}

            '算大運(10年)相關資訊-------------------------------
            intY = DiffGT1Gt2(Mid(Bir.YuanStart_YearGT, 1, 1), _
                                  Mid(Bir.YuanStart_YearGT, 2, 1), _
                                  Bir.Ju(1).Gan, Bir.Ju(1).Tsu)
            For intLop = 1 To 10
                Bir.BigYuan(intLop).YearsOld = intY + 1

                intY = intY + 10
                ' 取得此運程的地支是對應到宮次的哪一宮
                intAt = 1
                For k As Integer = 1 To 12
                    If Bir.Kon12(k) = Bir.BigYuan(intLop).Tsu Then
                        intAt = k
                        Exit For
                    End If
                Next
                ' 把此宮的星煞、長生... 抄過來 
                Bir.BigYuan(intLop).LongLife = Bir.LongLife12.Kon(intAt)
                Bir.BigYuan(intLop).Kon = aryG(intAt - 1)
                '大運十神
                Bir.BigYuan(intLop).SenFu = SF1word(FindSixSen(Bir.Ju(3).Gan, Bir.BigYuan(intLop).Gan))
                Bir.BigYuan(intLop).SenFu = Bir.BigYuan(intLop).SenFu & _
                                            SF1word(FindSixSen(Bir.Ju(3).Gan, Bir.BigYuan(intLop).Tsu))
                Bir.BigYuan(intLop).SenFu12 = Bir.SenFu12(intAt)
                Bir.BigYuan(intLop).Naing = FindNaIng5Sing(Bir.BigYuan(intLop).Gan, Bir.BigYuan(intLop).Tsu)
                Bir.BigYuan(intLop).StarB2 = Bir.StarB2(intAt)
                Bir.BigYuan(intLop).Star = aryStar(InStr(AllGan, Bir.BigYuan(intLop).Gan) - 1)

                ' ==> 2016/01/20: 若是只有抄過來,似乎天德月德會少了月支碰天干!!
                'For intLop1 = 1 To 14
                '   Bir.BigYuanStarSky(intLop, intLop1) = Bir.StarSky(intAt, intLop1)
                'Next
                strGet = FindStarSky(Bir.Ju(1).Tsu, _
                                 Bir.Ju(2).Tsu, _
                                 Bir.Ju(3).Gan, _
                                 Bir.BigYuan(intLop).Tsu, _
                                 Bir.BigYuan(intLop).Gan)
                intCnt = 1
                While Len(strGet) > 1 And intCnt < 15
                    intAAt = InStr(strGet, ",")
                    If intAAt = 0 Then
                        strTmp = Mid(strGet, 1, 2)
                        Bir.BigYuanStarSky(intLop, intCnt) = Mid(strGet, 1, 2)
                        strGet = ""
                    Else
                        strTmp = Mid(Mid(strGet, 1, intAAt - 1), 1, 2)
                        Bir.BigYuanStarSky(intLop, intCnt) = Mid(Mid(strGet, 1, intAAt - 1), 1, 2)
                        strGet = Mid(strGet, intAAt + 1)
                    End If
                    strGet = Replace(strGet, Bir.BigYuanStarSky(intLop, intCnt), "")
                    strGet = Replace(strGet, ",,", ",")
                    intCnt = intCnt + 1
                End While
            Next
        Else
            UserMsg("資料庫裡沒有符合 西曆日期=" & Bir.YuanStartDate1 & "的資料")
        End If

    End Sub

    '取得某個西曆日期的年月日干支 ------------備用function
    Public Function GetDateYMDGT(ByVal inputDate As String) As String
        Dim Y As String = ""
        Dim M As String = ""
        Dim D As String = ""
        SQLstr = " Select * from SAM where 西曆='" & inputDate & "'"
        SQLcmd = New SqlCommand(SQLstr, DBconn)
        DBreader = SQLcmd.ExecuteReader()
        If DBreader.HasRows Then
            Do While DBreader.Read()
                Y = DBreader("一柱")
                M = DBreader("二柱")
                D = DBreader("三柱")
            Loop
        Else
            UserMsg("資料庫裡沒有符合 西曆日期=" & inputDate & "的資料")
        End If
        GetDateYMDGT = Y & M & D
        DBreader.Close()
        SQLcmd.Dispose()
    End Function

    Private Sub GetWuSingState()
        Select Case Bir.Ju(2).Tsu
            Case "寅", "卯"
                Bir.WuSingState = "死囚休相旺"
            Case "亥", "子"
                Bir.WuSingState = "囚休旺死相"
            Case "巳", "午"
                Bir.WuSingState = "相死囚旺休"
            Case "申", "酉"
                Bir.WuSingState = "休旺相囚死"
            Case "辰", "丑", "未", "戌"
                Bir.WuSingState = "旺相死休囚"
        End Select
    End Sub

    Private Sub CountWuSing()
        Dim strTmp As String = ""
        Dim intLop As Integer
        For intLop = 1 To 8
            strTmp = strTmp & Mid(Bir.WuSing.Ju(intLop).Gan, 1, 1)
            strTmp = strTmp & Mid(Bir.WuSing.Ju(intLop).Tsu, 1, 1)
            strTmp = strTmp & Mid(Bir.WuSing.Ju(intLop).HG1, 1, 1)
            strTmp = strTmp & Mid(Bir.WuSing.Ju(intLop).HG2, 1, 1)
            strTmp = strTmp & Mid(Bir.WuSing.Ju(intLop).HG3, 1, 1)
        Next
        For intLop = 1 To Len(strTmp)
            Select Case Mid(strTmp, intLop, 1)
                Case "土"
                    Bir.WuSingCount(1, 2) = Bir.WuSingCount(1, 2) + 1
                Case "火"
                    Bir.WuSingCount(2, 2) = Bir.WuSingCount(2, 2) + 1
                Case "水"
                    Bir.WuSingCount(3, 2) = Bir.WuSingCount(3, 2) + 1
                Case "木"
                    Bir.WuSingCount(4, 2) = Bir.WuSingCount(4, 2) + 1
                Case "金"
                    Bir.WuSingCount(5, 2) = Bir.WuSingCount(5, 2) + 1
            End Select
        Next
        '八字
        strTmp = ""
        For intLop = 1 To 4
            strTmp = strTmp & Mid(Bir.WuSing.Ju(intLop).Gan, 1, 1)
            strTmp = strTmp & Mid(Bir.WuSing.Ju(intLop).Tsu, 1, 1)
        Next
        For intLop = 1 To Len(strTmp)
            Select Case Mid(strTmp, intLop, 1)
                Case "土"
                    Bir.WuSingCount(1, 0) = Bir.WuSingCount(1, 0) + 1
                Case "火"
                    Bir.WuSingCount(2, 0) = Bir.WuSingCount(2, 0) + 1
                Case "水"
                    Bir.WuSingCount(3, 0) = Bir.WuSingCount(3, 0) + 1
                Case "木"
                    Bir.WuSingCount(4, 0) = Bir.WuSingCount(4, 0) + 1
                Case "金"
                    Bir.WuSingCount(5, 0) = Bir.WuSingCount(5, 0) + 1
            End Select
        Next
        For intLop = 1 To 8
            Select Case Microsoft.VisualBasic.Right(Bir.NaIng.Ju(intLop), 1)
                Case "土"
                    Bir.WuSingCount(1, 1) = Bir.WuSingCount(1, 1) + 1
                Case "火"
                    Bir.WuSingCount(2, 1) = Bir.WuSingCount(2, 1) + 1
                Case "水"
                    Bir.WuSingCount(3, 1) = Bir.WuSingCount(3, 1) + 1
                Case "木"
                    Bir.WuSingCount(4, 1) = Bir.WuSingCount(4, 1) + 1
                Case "金"
                    Bir.WuSingCount(5, 1) = Bir.WuSingCount(5, 1) + 1
            End Select
        Next
    End Sub

    Private Sub GetSenSa()
        Dim lop As Integer
        Dim strGet As String
        Dim intAt As Integer
        Dim intCnt As Integer
        Dim strTmp As String
        For lop = 1 To 8
            strGet = FindStarSky(Bir.Ju(1).Tsu, _
                                 Bir.Ju(2).Tsu, _
                                 Bir.Ju(3).Gan, _
                                 Bir.Ju(lop).Tsu, _
                                 Bir.Ju(lop).Gan)
            intCnt = 1
            While Len(strGet) > 1 And intCnt < 15
                intAt = InStr(strGet, ",")
                If lop = 4 Then
                    '                Beep
                End If
                If intAt = 0 Then
                    strTmp = Mid(strGet, 1, 2)
                    Bir.SenSa(lop, intCnt) = Mid(strGet, 1, 2)
                    strGet = ""
                Else
                    strTmp = Mid(Mid(strGet, 1, intAt - 1), 1, 2)
                    Bir.SenSa(lop, intCnt) = Mid(Mid(strGet, 1, intAt - 1), 1, 2)
                    strGet = Mid(strGet, intAt + 1)
                End If
                strGet = Replace(strGet, Bir.SenSa(lop, intCnt), "")
                strGet = Replace(strGet, ",,", ",")
                intCnt = intCnt + 1
            End While
        Next
    End Sub

    '神輔的計數
    Private Sub CountSenFu()
        Dim strTmp As String = ""
        For i As Integer = 1 To 8
            strTmp = strTmp & Bir.SenFu.Ju(i).Gan & Bir.SenFu.Ju(i).Tsu _
                & Bir.SenFu.Ju(i).HG1 & Bir.SenFu.Ju(i).HG2 & Bir.SenFu.Ju(i).HG3
        Next
        For intLop = 1 To Len(strTmp) Step 2
            Select Case Mid(strTmp, intLop, 2)
                Case "劫財"
                    Bir.SenFuCount(1) = Bir.SenFuCount(1) + 1
                Case "傷官"
                    Bir.SenFuCount(2) = Bir.SenFuCount(2) + 1
                Case "偏財"
                    Bir.SenFuCount(3) = Bir.SenFuCount(3) + 1
                Case "偏印"
                    Bir.SenFuCount(4) = Bir.SenFuCount(4) + 1
                Case "七殺"
                    Bir.SenFuCount(5) = Bir.SenFuCount(5) + 1
                Case "比肩"
                    Bir.SenFuCount(6) = Bir.SenFuCount(6) + 1
                Case "食神"
                    Bir.SenFuCount(7) = Bir.SenFuCount(7) + 1
                Case "正財"
                    Bir.SenFuCount(8) = Bir.SenFuCount(8) + 1
                Case "正印"
                    Bir.SenFuCount(9) = Bir.SenFuCount(9) + 1
                Case "正官"
                    Bir.SenFuCount(10) = Bir.SenFuCount(10) + 1
            End Select
        Next
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CBox_Place.SelectedIndexChanged
        Dim POS2 As String() = New String(27) {"", _
            "121:33:35", _
            "120:39:58", _
            "121:42:29", _
            "120:57:53", _
            "120:56:30", _
            "120:15:05", _
            "121:40:26", _
            "121:43:10", _
            "121:13:01", _
            "121:07:31", _
            "120:56:30", _
            "120:56:30", _
            "120:28:55", _
            "120:59:15", _
            "120:23:23", _
            "120:34:26", _
            "120:15:05", _
            "120:39:58", _
            "120:37:12", _
            "121:21:15", _
            "120:59:15", _
            "121:28:59", _
            "121:31:60", _
            "119:36:55", _
            "120:29:40", _
            "119:55:38", _
            "118:21:28"}
        Dim POS1 As String() = New String(27) {"", _
            "25:05:28", _
            "23:00:39", _
            "25:06:32", _
            "24:48:14", _
            "24:13:60", _
            "23:08:30", _
            "24:54:57", _
            "24:41:35", _
            "24:56:15", _
            "24:42:12", _
            "24:29:21", _
            "24:13:60", _
            "23:59:35", _
            "23:50:20", _
            "23:45:21", _
            "23:27:32", _
            "23:08:30", _
            "23:00:39", _
            "22:32:58", _
            "23:45:25", _
            "22:59:05", _
            "22:40:00", _
            "22:03:00", _
            "23:33:56", _
            "26:22:25", _
            "26:09:04", _
            "24:27:07"}
        Dim POSname As String() = New String(27) {"", _
            "台北市", _
            "高雄市", _
            "基隆市", _
            "新竹市", _
            "台中市", _
            "台南市", _
            "新北市", _
            "宜蘭縣市", _
            "桃園縣市", _
            "新竹縣", _
            "苗栗縣市", _
            "台中縣", _
            "彰化縣市", _
            "南投縣市", _
            "雲林縣", _
            "嘉義縣市", _
            "台南縣", _
            "高雄縣", _
            "屏東縣市", _
            "花蓮縣市", _
            "台東縣市", _
            "綠島", _
            "蘭嶼", _
            "澎湖縣", _
            "東引島", _
            "馬祖縣", _
            "金門縣"}
        If (CBox_Place.SelectedIndex <> 0) Then
            TBoxPOS1.Text = POS1(CBox_Place.SelectedIndex)
            TBoxPOS2.Text = POS2(CBox_Place.SelectedIndex)
            txtAddress.Text = POSname(CBox_Place.SelectedIndex)
        End If
        'UserMsg("出生地.... Index=" & CBox_Place.SelectedIndex)
    End Sub

    Private Sub Btn_GMap_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_GMap.Click
        Try
            Dim ll As New LatLon()
            ll = GetLatLon(txtAddress.Text)
            'TBoxPOS1.Text = ll.Latitude.ToString()
            'TBoxPOS2.Text = ll.Longitude.ToString()
            TBoxPOS1.Text = To60FormatStr(ll.Latitude)
            TBoxPOS2.Text = To60FormatStr(ll.Longitude)
            UserMsg("Google 緯度=" & TBoxPOS1.Text & " 經度=" & TBoxPOS2.Text)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "An error has occurred")
        End Try
    End Sub
    Public Function To60FormatStr(ByVal In10Number As Double) As String
        Dim TmpDouble As Double
        Dim TmpInt As Integer
        Dim TmpStr As String
        TmpDouble = In10Number
        TmpStr = ""
        If In10Number < 0 Then
            TmpDouble = Abs(In10Number)
            TmpStr = "-"
        End If
        '度數
        TmpInt = Math.Truncate(TmpDouble)
        TmpDouble = TmpDouble - TmpInt
        TmpStr = TmpStr & Format(TmpInt, "##0")
        '分
        TmpDouble = (TmpDouble * 60)
        TmpInt = Math.Truncate(TmpDouble)
        TmpDouble = TmpDouble - TmpInt
        TmpStr = TmpStr & ":" & Format(TmpInt, "00")
        '秒
        TmpDouble = (TmpDouble * 60)
        TmpInt = Math.Round(TmpDouble)
        'TmpDouble = TmpDouble - TmpInt
        TmpStr = TmpStr & ":" & Format(TmpInt, "00")
        'output
        To60FormatStr = TmpStr
    End Function
    Public Function GetLatLon(ByVal addr As String) As LatLon
        Dim url As String = "http://maps.googleapis.com/maps/api/geocode/json?address=" _
                            & addr & "&sensor=false&language=zh-tw"
        Dim request As System.Net.WebRequest = WebRequest.Create(url)
        Dim response As HttpWebResponse = request.GetResponse()
        If response.StatusCode = HttpStatusCode.OK Then
            Dim ms As New System.IO.MemoryStream()
            Dim responseStream As System.IO.Stream = response.GetResponseStream()
            Dim buffer(2048) As Byte
            Dim count As Integer = responseStream.Read(buffer, 0, buffer.Length)
            While count > 0
                ms.Write(buffer, 0, count)
                count = responseStream.Read(buffer, 0, buffer.Length)
            End While
            responseStream.Close()
            ms.Close()
            Dim responseBytes() As Byte = ms.ToArray()
            Dim encoding As New System.Text.ASCIIEncoding()
            Dim coords As String = encoding.GetString(responseBytes)
            Dim parts() As String = coords.Split(",")
            '找出location& lat在哪裡
            Dim part_no As Integer = 0
            Do Until InStr(parts(part_no), "location") <> 0 _
                 And InStr(parts(part_no), "lat") <> 0
                part_no = part_no + 1
            Loop
            parts(part_no) = Trim(parts(part_no))
            parts(part_no + 1) = Trim(parts(part_no + 1))
            'UserMsg("part" & part_no & "=[" & parts(part_no) & "]")
            'UserMsg("part" & part_no + 1 & "=[" & parts(part_no + 1) & "]")
            Dim latPos As Integer = InStr(parts(part_no), "lat")
            Dim lonPos As Integer = InStr(parts(part_no + 1), "lng")
            parts(part_no) = Mid(parts(part_no), latPos + 7, 9)
            parts(part_no + 1) = Mid(parts(part_no + 1), lonPos + 7, 9)
            'UserMsg("Google Map 準確緯度=" & parts(part_no) & ",準確經度=" & parts(part_no + 1))
            'Return New LatLon(1.0, 1.0)
            Return New LatLon(Convert.ToDouble(parts(part_no)), Convert.ToDouble(parts(part_no + 1)))
        End If
        Return Nothing
    End Function

    Private Sub CheckBox_2ndMonth_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox_2ndMonth.CheckedChanged
        If CheckBox_2ndMonth.Checked Then
            IntercalaryMonth = True
        Else
            IntercalaryMonth = False
        End If
    End Sub

    Private Sub CBcalender_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CBcalender.SelectedIndexChanged
        If CBcalender.SelectedIndex = 1 Then
            CheckBox_2ndMonth.Visible = True
        Else
            CheckBox_2ndMonth.Visible = False
        End If
        CheckBox_2ndMonth.Checked = False
        IntercalaryMonth = False
    End Sub

    Private Sub Btn_About_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_About.Click
        AboutBox1.Show()
    End Sub

    '匯出客戶資料
    Private Sub Btn_Export_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Export.Click
        Dim mbx As Integer
        UserMsg("=======================================")
        UserMsg("匯出資料庫內的客戶資料....")
        UserMsg("檔案名=c:\5ArtSave\Fate_customer_all.txt ")
        UserMsg("=======================================")
        mbx = MsgBox("確定匯出資料庫內的客戶資料到c:\5ArtSave\Fate_customer_all.txt嗎?", vbYesNo + vbQuestion, "匯出")
        If mbx = vbYes Then
            UserMsg("匯出中....")
            ExportCustomerAll("c:\5ArtSave\Fate_customer_all.txt")
        End If
    End Sub

    Private Sub ExportCustomerAll(ByVal TxtFileName As String)
        'Dim strTemp As String
        Dim mbx As Integer
        Dim lop As Integer
        If File.Exists(TxtFileName) = True Then
            mbx = MsgBox("檔案已存在,確定覆蓋舊的c:\5ArtSave\Fate_customer_all.txt嗎?", vbYesNo + vbQuestion, "覆蓋")
            If mbx = vbYes Then
                UserMsg(TxtFileName & "檔案已存在,將會覆蓋....")
                FileOpen(1, TxtFileName, OpenMode.Output)
                For lop = 1 To CustomerCount
                    RdCustr_FileExport(Convert.ToString(lop))
                Next
                UserMsg("匯出完成....")
                FileClose(1)  '關閉檔案
            Else
                UserMsg(TxtFileName & "檔案已存在,放棄匯出!....")
            End If
        Else
            UserMsg(TxtFileName & "檔案不存在,產生新檔案....")
            FileOpen(1, TxtFileName, OpenMode.Output)
            For lop = 1 To CustomerCount
                RdCustr_FileExport(Convert.ToString(lop))
            Next
            UserMsg("匯出完成....")
            FileClose(1)  '關閉檔案
        End If
    End Sub

    Private Sub RdCustr_FileExport(ByVal CustomerNumber As String)
        Dim strTemp As String
        SQLstr = " Select * from Customer where 編號='" & CustomerNumber & "'"
        SQLcmd = New SqlCommand(SQLstr, DBconn)
        DBreader = SQLcmd.ExecuteReader()
        If DBreader.HasRows Then
            Do While DBreader.Read()
                LStr(0) = System.Convert.ToString(DBreader("編號"))
                LStr(1) = System.Convert.ToString(DBreader("姓名"))
                LStr(2) = System.Convert.ToString(DBreader("性別"))
                LStr(3) = System.Convert.ToString(DBreader("民國年"))
                LStr(4) = System.Convert.ToString(DBreader("月"))
                LStr(5) = System.Convert.ToString(DBreader("日"))
                LStr(6) = System.Convert.ToString(DBreader("時"))
                LStr(7) = System.Convert.ToString(DBreader("分"))
                LStr(8) = System.Convert.ToString(DBreader("緯度"))
                LStr(9) = System.Convert.ToString(DBreader("經度"))
                LStr(10) = System.Convert.ToString(DBreader("出生地"))
                strTemp = LStr(0) & "," & LStr(1) & "," & LStr(2) & "," & LStr(3) & "," & LStr(4) & "," _
                        & LStr(5) & "," & LStr(6) & "," & LStr(7) & "," & LStr(8) & "," & LStr(9) & "," & LStr(10)
                UserMsg("編號=" & LStr(0) & "-" & LStr(1) & "," & LStr(2) & ",民國" & LStr(3) & "-" & LStr(4) & "-" & LStr(5) & "," _
                       & LStr(6) & "時" & LStr(7) & "分; 緯度" & LStr(8) & "經度" & LStr(9) & "出生地" & LStr(10))
                PrintLine(1, strTemp)
            Loop
        Else
            UserMsg("資料庫裡沒有符合 編號=" + CustomerNumber + "的資料")
        End If
        DBreader.Close()
        SQLcmd.Dispose()
    End Sub

    Private Sub Btn_Import_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Import.Click
        Dim mbx As Integer
        UserMsg("=======================================")
        UserMsg("匯入客戶資料至資料庫....")
        UserMsg("檔案名稱=c:\5ArtSave\Fate_customer_all.txt ")
        UserMsg("=======================================")
        mbx = MsgBox("確定匯入客戶資料到嗎?", vbYesNo + vbQuestion, "匯入")
        If mbx = vbYes Then
            UserMsg("匯入中....")
            ImportCustomerAll("c:\5ArtSave\Fate_customer_all.txt")
        End If
    End Sub
    Private Sub ImportCustomerAll(ByVal TxtFileName As String)
        Dim Aline As String
        Dim Ln As Integer
        Dim x As Integer
        Dim cn As Integer
        Dim HasItemErr As Boolean
        HasItemErr = False
        If File.Exists(TxtFileName) = True Then
            FileOpen(1, TxtFileName, OpenMode.Input)
            Ln = 0
            Do While (Not EOF(1))
                '檔案讀取一行,並且切割字串
                Aline = LineInput(1)
                UserMsg(Ln & " " & Aline)
                LStr = Split(Aline, ",")
                'UserMsg("編號=" & LStr(0) & "-" & LStr(1) & "," & LStr(2) & ",民國" & LStr(3) & "-" & LStr(4) & "-" & LStr(5) & "," _
                '       & LStr(6) & "時" & LStr(7) & "分; 緯度" & LStr(8) & "經度" & LStr(9) & "出生地" & LStr(10))
                '檢查是否已有此資料,以編號查詢
                SQLstr = "select count(*) from Customer where 編號 = '" & LStr(0) & "'"
                SQLcmd = New SqlCommand(SQLstr, DBconn)
                DBreader = SQLcmd.ExecuteReader()
                DBreader.Read()
                cn = DBreader.Item(0)
                DBreader.Close()
                SQLcmd.Dispose()
                '準備 SQL 字串, Insert or Update
                If cn <> 0 Then    ' 資料存在,故為Update
                    SQLstr = "update Customer set 姓名='" & LStr(1)
                    SQLstr = SQLstr & "', 性別='" & LStr(2)
                    SQLstr = SQLstr & "', 民國年=" & LStr(3)
                    SQLstr = SQLstr & ",月=" & LStr(4)
                    SQLstr = SQLstr & ",日=" & LStr(5)
                    SQLstr = SQLstr & ",時=" & LStr(6)
                    SQLstr = SQLstr & ",分=" & LStr(7)
                    SQLstr = SQLstr & ",緯度='" & LStr(8)
                    SQLstr = SQLstr & "',經度='" & LStr(9)
                    SQLstr = SQLstr & "',出生地='" & LStr(10)
                    SQLstr = SQLstr & "' where 編號=" & LStr(0)
                Else               '資料不存在,故而insert
                    SQLstr = "insert into Customer (姓名,性別,民國年,月,日,時,分,緯度,經度,出生地)values ('"
                    SQLstr = SQLstr & LStr(1) & "', '"
                    SQLstr = SQLstr & LStr(2) & "', "
                    SQLstr = SQLstr & LStr(3) & ","
                    SQLstr = SQLstr & LStr(4) & ","
                    SQLstr = SQLstr & LStr(5) & ","
                    SQLstr = SQLstr & LStr(6) & ","
                    SQLstr = SQLstr & LStr(7) & ", '"
                    SQLstr = SQLstr & LStr(8) & "', '"
                    SQLstr = SQLstr & LStr(9) & "', '"
                    SQLstr = SQLstr & LStr(10) & "')"
                End If
                SQLcmd = New SqlCommand(SQLstr, DBconn)
                'UserMsg(SQLstr)
                x = SQLcmd.ExecuteNonQuery()
                If x <> 1 Then 'ExecuteNonQuery 傳回資料異動筆數
                    UserMsg("新增資料庫失敗! ")
                    HasItemErr = True
                Else
                    '成功!
                    UserMsg(Ln & "行成功! 現有 " & CustomerCount & "筆:")
                    If cn = 0 Then
                        '若為insert設定資料表總筆數加一
                        CustomerCount = CustomerCount + 1
                    End If
                End If
                SQLcmd.Dispose()
                '----------
                Ln = Ln + 1
            Loop 'while loop end
            ' ---------------------
            FileClose(1)  '關閉檔案
            UserMsg("File=" & TxtFileName & " 總共輸入了 " & Ln & " 筆客戶資料")
            UserMsg("Fate2013資料庫的Customer資料表有 " & CustomerCount & " 筆客戶資料....")
            If HasItemErr Then
                UserMsg("!!!!!!!請注意!! " & TxtFileName & " 輸入過程中有錯誤 !!!!!!!!!")
            End If
        Else
            UserMsg(TxtFileName & "檔案找不到!!,操作中斷....")
        End If
    End Sub

    '八字取格
    Private Sub Find8wordKan()
        'Rule-1: 月支透月干為第一優先  (不取比肩劫財)
        'Rule-2: 月支透天干(年時)為第二優先  (不取比肩劫財)
        'Rule-3: 月干坐根(年日時)為第三優先  (不取比肩劫財)
        'Rule-4: 上述三優先之外,
        '     A> (年時)天干, 坐根(年日時)
        '     B> (年月時)天干, 藏根(地支藏干) , 天透地藏
        '     此時可能有雙格,又雙格裡面 
        '     BB> 三合三會, 
        '     ==> 正財偏財做偏財論;正印偏印做偏印論,正官偏官做偏官論,食神傷官做傷官論
        '     C> 同類相應, 年月日天干,找地支藏干
        '     ==> 正財偏財做偏財論;正印偏印做偏印論,正官偏官做偏官論,食神傷官做傷官論
        Dim CheckFirst1 As Boolean = False
        Dim CheckFirst2 As Boolean = False
        Dim CheckFirst3 As Boolean = False
        Dim Check4A1 As Boolean = False
        Dim Check4A2 As Boolean = False
        Dim Check4B1 As Boolean = False
        Dim Check4B2 As Boolean = False
        Dim Check4B3 As Boolean = False
        Dim Check4C1 As Boolean = False
        Dim Check4C2 As Boolean = False
        Dim Check4C3 As Boolean = False
        Dim Check4BB As Boolean = False
        Dim Check4BB_Month As Boolean = False
        Dim CF1Kan As String = ""
        Dim CF2Kan As String = ""
        Dim CF3Kan As String = ""
        Dim C4A1Kan As String = ""
        Dim C4A2Kan As String = ""
        Dim C4B1Kan As String = ""
        Dim C4B2Kan As String = ""
        Dim C4B3Kan As String = ""
        Dim C4C1Kan As String = ""
        Dim C4C2Kan As String = ""
        Dim C4C3Kan As String = ""
        Dim C4BBKan As String = ""
        Dim tmpStr As String = ""
        Dim ReCode As Integer
        '檢查優先,Rule-1 月支透月干
        If (Bir.SenFu.Ju(2).Tsu = Bir.SenFu.Ju(2).Gan) _
            And (Bir.SenFu.Ju(2).Tsu <> "比肩") And (Bir.SenFu.Ju(2).Tsu <> "劫財") Then
            CheckFirst1 = True
            CF1Kan = Bir.SenFu.Ju(2).Tsu & "格"
            If Bir.EngMode Then UserMsg("CF1Kan = " & CF1Kan)
        End If
        '檢查優先,Rule-2 月支透天干
        If ((Bir.SenFu.Ju(2).Tsu = Bir.SenFu.Ju(1).Gan) Or (Bir.SenFu.Ju(2).Tsu = Bir.SenFu.Ju(4).Gan)) _
            And (Bir.SenFu.Ju(2).Tsu <> "比肩") And (Bir.SenFu.Ju(2).Tsu <> "劫財") Then
            CheckFirst2 = True
            CF2Kan = Bir.SenFu.Ju(2).Tsu & "格"
            If Bir.EngMode Then UserMsg("CF2Kan = " & CF2Kan)
        End If
        '檢查優先,Rule-3 月干坐根
        If ((Bir.SenFu.Ju(2).Gan = Bir.SenFu.Ju(1).Tsu) _
          Or (Bir.SenFu.Ju(2).Gan = Bir.SenFu.Ju(3).Tsu) _
          Or (Bir.SenFu.Ju(2).Gan = Bir.SenFu.Ju(4).Tsu)) _
          And (Bir.SenFu.Ju(2).Gan <> "比肩") And (Bir.SenFu.Ju(2).Gan <> "劫財") Then
            CheckFirst3 = True
            CF3Kan = Bir.SenFu.Ju(2).Gan & "格"
            If Bir.EngMode Then UserMsg("CF3Kan = " & CF3Kan)
        End If
        '檢查Rule-4-A1 年干坐根
        If ((Bir.SenFu.Ju(1).Gan = Bir.SenFu.Ju(1).Tsu) _
            Or (Bir.SenFu.Ju(1).Gan = Bir.SenFu.Ju(3).Tsu) _
            Or (Bir.SenFu.Ju(1).Gan = Bir.SenFu.Ju(4).Tsu)) _
          And (Bir.SenFu.Ju(1).Gan <> "比肩") And (Bir.SenFu.Ju(1).Gan <> "劫財") Then
            Check4A1 = True
            C4A1Kan = Bir.SenFu.Ju(1).Gan & "格"
            If Bir.EngMode Then UserMsg("C4A1Kan = " & C4A1Kan)
        End If
        '檢查Rule-4-A2 時干坐根
        If ((Bir.SenFu.Ju(4).Gan = Bir.SenFu.Ju(1).Tsu) _
            Or (Bir.SenFu.Ju(4).Gan = Bir.SenFu.Ju(3).Tsu) _
            Or (Bir.SenFu.Ju(4).Gan = Bir.SenFu.Ju(4).Tsu)) _
           And (Bir.SenFu.Ju(4).Gan <> "比肩") And (Bir.SenFu.Ju(4).Gan <> "劫財") Then
            Check4A2 = True
            C4A2Kan = Bir.SenFu.Ju(4).Gan & "格"
            If Bir.EngMode Then UserMsg("C4A2Kan = " & C4A2Kan)
        End If
        '檢查Rule-4-B1 年干藏根
        If ((Bir.SenFu.Ju(1).Gan = Bir.SenFu.Ju(1).HG1) _
            Or (Bir.SenFu.Ju(1).Gan = Bir.SenFu.Ju(1).HG2) _
            Or (Bir.SenFu.Ju(1).Gan = Bir.SenFu.Ju(1).HG3) _
            Or (Bir.SenFu.Ju(1).Gan = Bir.SenFu.Ju(2).HG1) _
            Or (Bir.SenFu.Ju(1).Gan = Bir.SenFu.Ju(2).HG2) _
            Or (Bir.SenFu.Ju(1).Gan = Bir.SenFu.Ju(2).HG3) _
            Or (Bir.SenFu.Ju(1).Gan = Bir.SenFu.Ju(3).HG1) _
            Or (Bir.SenFu.Ju(1).Gan = Bir.SenFu.Ju(3).HG2) _
            Or (Bir.SenFu.Ju(1).Gan = Bir.SenFu.Ju(3).HG3) _
            Or (Bir.SenFu.Ju(1).Gan = Bir.SenFu.Ju(4).HG1) _
            Or (Bir.SenFu.Ju(1).Gan = Bir.SenFu.Ju(4).HG2) _
            Or (Bir.SenFu.Ju(1).Gan = Bir.SenFu.Ju(4).HG3)) _
           And (Bir.SenFu.Ju(1).Gan <> "比肩") And (Bir.SenFu.Ju(1).Gan <> "劫財") Then
            Check4B1 = True
            C4B1Kan = Bir.SenFu.Ju(1).Gan & "格"
            If Bir.EngMode Then UserMsg("C4B1Kan = " & C4B1Kan)
        End If
        '檢查Rule-4-B2 月干藏根
        If ((Bir.SenFu.Ju(2).Gan = Bir.SenFu.Ju(1).HG1) _
            Or (Bir.SenFu.Ju(2).Gan = Bir.SenFu.Ju(1).HG2) _
            Or (Bir.SenFu.Ju(2).Gan = Bir.SenFu.Ju(1).HG3) _
            Or (Bir.SenFu.Ju(2).Gan = Bir.SenFu.Ju(2).HG1) _
            Or (Bir.SenFu.Ju(2).Gan = Bir.SenFu.Ju(2).HG2) _
            Or (Bir.SenFu.Ju(2).Gan = Bir.SenFu.Ju(2).HG3) _
            Or (Bir.SenFu.Ju(2).Gan = Bir.SenFu.Ju(3).HG1) _
            Or (Bir.SenFu.Ju(2).Gan = Bir.SenFu.Ju(3).HG2) _
            Or (Bir.SenFu.Ju(2).Gan = Bir.SenFu.Ju(3).HG3) _
            Or (Bir.SenFu.Ju(2).Gan = Bir.SenFu.Ju(4).HG1) _
            Or (Bir.SenFu.Ju(2).Gan = Bir.SenFu.Ju(4).HG2) _
            Or (Bir.SenFu.Ju(2).Gan = Bir.SenFu.Ju(4).HG3)) _
           And (Bir.SenFu.Ju(2).Gan <> "比肩") And (Bir.SenFu.Ju(2).Gan <> "劫財") Then
            Check4B2 = True
            C4B2Kan = Bir.SenFu.Ju(2).Gan & "格"
            If Bir.EngMode Then UserMsg("C4B2Kan = " & C4B2Kan)
        End If
        '檢查Rule-4-B3 時干藏根
        If ((Bir.SenFu.Ju(4).Gan = Bir.SenFu.Ju(1).HG1) _
            Or (Bir.SenFu.Ju(4).Gan = Bir.SenFu.Ju(1).HG2) _
            Or (Bir.SenFu.Ju(4).Gan = Bir.SenFu.Ju(1).HG3) _
            Or (Bir.SenFu.Ju(4).Gan = Bir.SenFu.Ju(2).HG1) _
            Or (Bir.SenFu.Ju(4).Gan = Bir.SenFu.Ju(2).HG2) _
            Or (Bir.SenFu.Ju(4).Gan = Bir.SenFu.Ju(2).HG3) _
            Or (Bir.SenFu.Ju(4).Gan = Bir.SenFu.Ju(3).HG1) _
            Or (Bir.SenFu.Ju(4).Gan = Bir.SenFu.Ju(3).HG2) _
            Or (Bir.SenFu.Ju(4).Gan = Bir.SenFu.Ju(3).HG3) _
            Or (Bir.SenFu.Ju(4).Gan = Bir.SenFu.Ju(4).HG1) _
            Or (Bir.SenFu.Ju(4).Gan = Bir.SenFu.Ju(4).HG2) _
            Or (Bir.SenFu.Ju(4).Gan = Bir.SenFu.Ju(4).HG3)) _
           And (Bir.SenFu.Ju(4).Gan <> "比肩") And (Bir.SenFu.Ju(4).Gan <> "劫財") Then
            Check4B3 = True
            C4B3Kan = Bir.SenFu.Ju(4).Gan & "格"
            If Bir.EngMode Then UserMsg("C4B3Kan = " & C4B3Kan)
        End If
        '------------------------------------------------
        '檢查Rule-4-BB  三合三會
        tmpStr = Bir.Ju(1).Tsu & Bir.Ju(2).Tsu & Bir.Ju(3).Tsu & Bir.Ju(4).Tsu
        If Is3Group(tmpStr, ReCode, Bir.Tsu3GroupType, Bir.Tsu3Group5Sing) Then
            Check4BB = False : Check4BB_Month = False
            UserMsg(" 地支有:" & Bir.Tsu3GroupType & Bir.Tsu3Group5Sing)
            Select Case Bir.Tsu3Group5Sing
                Case "水"
                    If Bir.Ju(3).Gan <> "壬" And Bir.Ju(3).Gan <> "癸" Then
                        Check4BB = True
                        Select Case Bir.Ju(3).Gan
                            Case "甲", "乙"
                                C4BBKan = "偏印格"
                            Case "丙", "丁"
                                C4BBKan = "七殺格"
                            Case "戊", "己"
                                C4BBKan = "偏財格"
                            Case "庚", "辛"
                                C4BBKan = "傷官格"
                            Case Else
                                UserMsg(" 日干有誤 Error !...")
                        End Select
                        If InStr(Bir.Tsu3GroupType, Bir.Ju(2).Tsu) <> 0 Then Check4BB_Month = True
                    End If
                Case "火"
                    If Bir.Ju(3).Gan <> "丙" And Bir.Ju(3).Gan <> "丁" Then
                        Check4BB = True
                        Select Case Bir.Ju(3).Gan
                            Case "甲", "乙"
                                C4BBKan = "傷官格"
                            Case "戊", "己"
                                C4BBKan = "偏印格"
                            Case "庚", "辛"
                                C4BBKan = "七殺格"
                            Case "壬", "癸"
                                C4BBKan = "偏財格"
                            Case Else
                                UserMsg(" 日干有誤 Error !...")
                        End Select
                        If InStr(Bir.Tsu3GroupType, Bir.Ju(2).Tsu) <> 0 Then Check4BB_Month = True
                    End If
                Case "木"
                    If Bir.Ju(3).Gan <> "甲" And Bir.Ju(3).Gan <> "乙" Then
                        Check4BB = True
                        Select Case Bir.Ju(3).Gan
                            Case "丙", "丁"
                                C4BBKan = "偏印格"
                            Case "戊", "己"
                                C4BBKan = "七殺格"
                            Case "庚", "辛"
                                C4BBKan = "偏財格"
                            Case "壬", "癸"
                                C4BBKan = "傷官格"
                            Case Else
                                UserMsg(" 日干有誤 Error !...")
                        End Select
                        If InStr(Bir.Tsu3GroupType, Bir.Ju(2).Tsu) <> 0 Then Check4BB_Month = True
                    End If
                Case "金"
                    If Bir.Ju(3).Gan <> "庚" And Bir.Ju(3).Gan <> "辛" Then
                        Check4BB = True
                        Select Case Bir.Ju(3).Gan
                            Case "甲", "乙"
                                C4BBKan = "七殺格"
                            Case "丙", "丁"
                                C4BBKan = "偏財格"
                            Case "戊", "己"
                                C4BBKan = "傷官格"
                            Case "壬", "癸"
                                C4BBKan = "偏印格"
                            Case Else
                                UserMsg(" 日干有誤 Error !...")
                        End Select
                        If InStr(Bir.Tsu3GroupType, Bir.Ju(2).Tsu) <> 0 Then Check4BB_Month = True
                    End If
                Case Else
                    UserMsg(" Is3Group function has error !...")
            End Select
        End If
        '------------------------------------------------
        '檢查Rule-4-C1 年干藏根,同類相應
        If Bir.SenFu.Ju(1).Gan = "正財" And ( _
            Bir.SenFu.Ju(1).HG1 = "偏財" Or Bir.SenFu.Ju(1).HG2 = "偏財" Or Bir.SenFu.Ju(1).HG3 = "偏財" Or _
            Bir.SenFu.Ju(2).HG1 = "偏財" Or Bir.SenFu.Ju(2).HG2 = "偏財" Or Bir.SenFu.Ju(2).HG3 = "偏財" Or _
            Bir.SenFu.Ju(3).HG1 = "偏財" Or Bir.SenFu.Ju(3).HG2 = "偏財" Or Bir.SenFu.Ju(3).HG3 = "偏財" Or _
            Bir.SenFu.Ju(4).HG1 = "偏財" Or Bir.SenFu.Ju(4).HG2 = "偏財" Or Bir.SenFu.Ju(4).HG3 = "偏財") Then
            Check4C1 = True
            C4C1Kan = "偏財格"
        ElseIf Bir.SenFu.Ju(1).Gan = "偏財" And ( _
            Bir.SenFu.Ju(1).HG1 = "正財" Or Bir.SenFu.Ju(1).HG2 = "正財" Or Bir.SenFu.Ju(1).HG3 = "正財" Or _
            Bir.SenFu.Ju(2).HG1 = "正財" Or Bir.SenFu.Ju(2).HG2 = "正財" Or Bir.SenFu.Ju(2).HG3 = "正財" Or _
            Bir.SenFu.Ju(3).HG1 = "正財" Or Bir.SenFu.Ju(3).HG2 = "正財" Or Bir.SenFu.Ju(3).HG3 = "正財" Or _
            Bir.SenFu.Ju(4).HG1 = "正財" Or Bir.SenFu.Ju(4).HG2 = "正財" Or Bir.SenFu.Ju(4).HG3 = "正財") Then
            Check4C1 = True
            C4C1Kan = "偏財格"
        ElseIf Bir.SenFu.Ju(1).Gan = "正官" And ( _
            Bir.SenFu.Ju(1).HG1 = "七殺" Or Bir.SenFu.Ju(1).HG2 = "七殺" Or Bir.SenFu.Ju(1).HG3 = "七殺" Or _
            Bir.SenFu.Ju(2).HG1 = "七殺" Or Bir.SenFu.Ju(2).HG2 = "七殺" Or Bir.SenFu.Ju(2).HG3 = "七殺" Or _
            Bir.SenFu.Ju(3).HG1 = "七殺" Or Bir.SenFu.Ju(3).HG2 = "七殺" Or Bir.SenFu.Ju(3).HG3 = "七殺" Or _
            Bir.SenFu.Ju(4).HG1 = "七殺" Or Bir.SenFu.Ju(4).HG2 = "七殺" Or Bir.SenFu.Ju(4).HG3 = "七殺") Then
            Check4C1 = True
            C4C1Kan = "七殺格"
        ElseIf Bir.SenFu.Ju(1).Gan = "七殺" And ( _
            Bir.SenFu.Ju(1).HG1 = "正官" Or Bir.SenFu.Ju(1).HG2 = "正官" Or Bir.SenFu.Ju(1).HG3 = "正官" Or _
            Bir.SenFu.Ju(2).HG1 = "正官" Or Bir.SenFu.Ju(2).HG2 = "正官" Or Bir.SenFu.Ju(2).HG3 = "正官" Or _
            Bir.SenFu.Ju(3).HG1 = "正官" Or Bir.SenFu.Ju(3).HG2 = "正官" Or Bir.SenFu.Ju(3).HG3 = "正官" Or _
            Bir.SenFu.Ju(4).HG1 = "正官" Or Bir.SenFu.Ju(4).HG2 = "正官" Or Bir.SenFu.Ju(4).HG3 = "正官") Then
            Check4C1 = True
            C4C1Kan = "七殺格"
        ElseIf Bir.SenFu.Ju(1).Gan = "正印" And ( _
            Bir.SenFu.Ju(1).HG1 = "偏印" Or Bir.SenFu.Ju(1).HG2 = "偏印" Or Bir.SenFu.Ju(1).HG3 = "偏印" Or _
            Bir.SenFu.Ju(2).HG1 = "偏印" Or Bir.SenFu.Ju(2).HG2 = "偏印" Or Bir.SenFu.Ju(2).HG3 = "偏印" Or _
            Bir.SenFu.Ju(3).HG1 = "偏印" Or Bir.SenFu.Ju(3).HG2 = "偏印" Or Bir.SenFu.Ju(3).HG3 = "偏印" Or _
            Bir.SenFu.Ju(4).HG1 = "偏印" Or Bir.SenFu.Ju(4).HG2 = "偏印" Or Bir.SenFu.Ju(4).HG3 = "偏印") Then
            Check4C1 = True
            C4C1Kan = "偏印格"
        ElseIf Bir.SenFu.Ju(1).Gan = "偏印" And ( _
            Bir.SenFu.Ju(1).HG1 = "正印" Or Bir.SenFu.Ju(1).HG2 = "正印" Or Bir.SenFu.Ju(1).HG3 = "正印" Or _
            Bir.SenFu.Ju(2).HG1 = "正印" Or Bir.SenFu.Ju(2).HG2 = "正印" Or Bir.SenFu.Ju(2).HG3 = "正印" Or _
            Bir.SenFu.Ju(3).HG1 = "正印" Or Bir.SenFu.Ju(3).HG2 = "正印" Or Bir.SenFu.Ju(3).HG3 = "正印" Or _
            Bir.SenFu.Ju(4).HG1 = "正印" Or Bir.SenFu.Ju(4).HG2 = "正印" Or Bir.SenFu.Ju(4).HG3 = "正印") Then
            Check4C1 = True
            C4C1Kan = "偏印格"
        ElseIf Bir.SenFu.Ju(1).Gan = "食神" And ( _
            Bir.SenFu.Ju(1).HG1 = "傷官" Or Bir.SenFu.Ju(1).HG2 = "傷官" Or Bir.SenFu.Ju(1).HG3 = "傷官" Or _
            Bir.SenFu.Ju(2).HG1 = "傷官" Or Bir.SenFu.Ju(2).HG2 = "傷官" Or Bir.SenFu.Ju(2).HG3 = "傷官" Or _
            Bir.SenFu.Ju(3).HG1 = "傷官" Or Bir.SenFu.Ju(3).HG2 = "傷官" Or Bir.SenFu.Ju(3).HG3 = "傷官" Or _
            Bir.SenFu.Ju(4).HG1 = "傷官" Or Bir.SenFu.Ju(4).HG2 = "傷官" Or Bir.SenFu.Ju(4).HG3 = "傷官") Then
            Check4C1 = True
            C4C1Kan = "傷官格"
        ElseIf Bir.SenFu.Ju(1).Gan = "傷官" And ( _
            Bir.SenFu.Ju(1).HG1 = "食神" Or Bir.SenFu.Ju(1).HG2 = "食神" Or Bir.SenFu.Ju(1).HG3 = "食神" Or _
            Bir.SenFu.Ju(2).HG1 = "食神" Or Bir.SenFu.Ju(2).HG2 = "食神" Or Bir.SenFu.Ju(2).HG3 = "食神" Or _
            Bir.SenFu.Ju(3).HG1 = "食神" Or Bir.SenFu.Ju(3).HG2 = "食神" Or Bir.SenFu.Ju(3).HG3 = "食神" Or _
            Bir.SenFu.Ju(4).HG1 = "食神" Or Bir.SenFu.Ju(4).HG2 = "食神" Or Bir.SenFu.Ju(4).HG3 = "食神") Then
            Check4C1 = True
            C4C1Kan = "傷官格"
        Else
            Check4C1 = False
            C4C1Kan = ""
        End If
        If Bir.EngMode And Check4C1 Then UserMsg("C4C1Kan = " & C4C1Kan)

        '檢查Rule-4-C2 月干藏根,同類相應
        If Bir.SenFu.Ju(2).Gan = "正財" And ( _
            Bir.SenFu.Ju(1).HG1 = "偏財" Or Bir.SenFu.Ju(1).HG2 = "偏財" Or Bir.SenFu.Ju(1).HG3 = "偏財" Or _
            Bir.SenFu.Ju(2).HG1 = "偏財" Or Bir.SenFu.Ju(2).HG2 = "偏財" Or Bir.SenFu.Ju(2).HG3 = "偏財" Or _
            Bir.SenFu.Ju(3).HG1 = "偏財" Or Bir.SenFu.Ju(3).HG2 = "偏財" Or Bir.SenFu.Ju(3).HG3 = "偏財" Or _
            Bir.SenFu.Ju(4).HG1 = "偏財" Or Bir.SenFu.Ju(4).HG2 = "偏財" Or Bir.SenFu.Ju(4).HG3 = "偏財") Then
            Check4C2 = True
            C4C2Kan = "偏財格"
        ElseIf Bir.SenFu.Ju(2).Gan = "偏財" And ( _
            Bir.SenFu.Ju(1).HG1 = "正財" Or Bir.SenFu.Ju(1).HG2 = "正財" Or Bir.SenFu.Ju(1).HG3 = "正財" Or _
            Bir.SenFu.Ju(2).HG1 = "正財" Or Bir.SenFu.Ju(2).HG2 = "正財" Or Bir.SenFu.Ju(2).HG3 = "正財" Or _
            Bir.SenFu.Ju(3).HG1 = "正財" Or Bir.SenFu.Ju(3).HG2 = "正財" Or Bir.SenFu.Ju(3).HG3 = "正財" Or _
            Bir.SenFu.Ju(4).HG1 = "正財" Or Bir.SenFu.Ju(4).HG2 = "正財" Or Bir.SenFu.Ju(4).HG3 = "正財") Then
            Check4C2 = True
            C4C2Kan = "偏財格"
        ElseIf Bir.SenFu.Ju(2).Gan = "正官" And ( _
            Bir.SenFu.Ju(1).HG1 = "七殺" Or Bir.SenFu.Ju(1).HG2 = "七殺" Or Bir.SenFu.Ju(1).HG3 = "七殺" Or _
            Bir.SenFu.Ju(2).HG1 = "七殺" Or Bir.SenFu.Ju(2).HG2 = "七殺" Or Bir.SenFu.Ju(2).HG3 = "七殺" Or _
            Bir.SenFu.Ju(3).HG1 = "七殺" Or Bir.SenFu.Ju(3).HG2 = "七殺" Or Bir.SenFu.Ju(3).HG3 = "七殺" Or _
            Bir.SenFu.Ju(4).HG1 = "七殺" Or Bir.SenFu.Ju(4).HG2 = "七殺" Or Bir.SenFu.Ju(4).HG3 = "七殺") Then
            Check4C2 = True
            C4C2Kan = "七殺格"
        ElseIf Bir.SenFu.Ju(2).Gan = "七殺" And ( _
            Bir.SenFu.Ju(1).HG1 = "正官" Or Bir.SenFu.Ju(1).HG2 = "正官" Or Bir.SenFu.Ju(1).HG3 = "正官" Or _
            Bir.SenFu.Ju(2).HG1 = "正官" Or Bir.SenFu.Ju(2).HG2 = "正官" Or Bir.SenFu.Ju(2).HG3 = "正官" Or _
            Bir.SenFu.Ju(3).HG1 = "正官" Or Bir.SenFu.Ju(3).HG2 = "正官" Or Bir.SenFu.Ju(3).HG3 = "正官" Or _
            Bir.SenFu.Ju(4).HG1 = "正官" Or Bir.SenFu.Ju(4).HG2 = "正官" Or Bir.SenFu.Ju(4).HG3 = "正官") Then
            Check4C2 = True
            C4C2Kan = "七殺格"
        ElseIf Bir.SenFu.Ju(2).Gan = "正印" And ( _
            Bir.SenFu.Ju(1).HG1 = "偏印" Or Bir.SenFu.Ju(1).HG2 = "偏印" Or Bir.SenFu.Ju(1).HG3 = "偏印" Or _
            Bir.SenFu.Ju(2).HG1 = "偏印" Or Bir.SenFu.Ju(2).HG2 = "偏印" Or Bir.SenFu.Ju(2).HG3 = "偏印" Or _
            Bir.SenFu.Ju(3).HG1 = "偏印" Or Bir.SenFu.Ju(3).HG2 = "偏印" Or Bir.SenFu.Ju(3).HG3 = "偏印" Or _
            Bir.SenFu.Ju(4).HG1 = "偏印" Or Bir.SenFu.Ju(4).HG2 = "偏印" Or Bir.SenFu.Ju(4).HG3 = "偏印") Then
            Check4C2 = True
            C4C2Kan = "偏印格"
        ElseIf Bir.SenFu.Ju(2).Gan = "偏印" And ( _
            Bir.SenFu.Ju(1).HG1 = "正印" Or Bir.SenFu.Ju(1).HG2 = "正印" Or Bir.SenFu.Ju(1).HG3 = "正印" Or _
            Bir.SenFu.Ju(2).HG1 = "正印" Or Bir.SenFu.Ju(2).HG2 = "正印" Or Bir.SenFu.Ju(2).HG3 = "正印" Or _
            Bir.SenFu.Ju(3).HG1 = "正印" Or Bir.SenFu.Ju(3).HG2 = "正印" Or Bir.SenFu.Ju(3).HG3 = "正印" Or _
            Bir.SenFu.Ju(4).HG1 = "正印" Or Bir.SenFu.Ju(4).HG2 = "正印" Or Bir.SenFu.Ju(4).HG3 = "正印") Then
            Check4C2 = True
            C4C2Kan = "偏印格"
        ElseIf Bir.SenFu.Ju(2).Gan = "食神" And ( _
            Bir.SenFu.Ju(1).HG1 = "傷官" Or Bir.SenFu.Ju(1).HG2 = "傷官" Or Bir.SenFu.Ju(1).HG3 = "傷官" Or _
            Bir.SenFu.Ju(2).HG1 = "傷官" Or Bir.SenFu.Ju(2).HG2 = "傷官" Or Bir.SenFu.Ju(2).HG3 = "傷官" Or _
            Bir.SenFu.Ju(3).HG1 = "傷官" Or Bir.SenFu.Ju(3).HG2 = "傷官" Or Bir.SenFu.Ju(3).HG3 = "傷官" Or _
            Bir.SenFu.Ju(4).HG1 = "傷官" Or Bir.SenFu.Ju(4).HG2 = "傷官" Or Bir.SenFu.Ju(4).HG3 = "傷官") Then
            Check4C2 = True
            C4C2Kan = "傷官格"
        ElseIf Bir.SenFu.Ju(2).Gan = "傷官" And ( _
            Bir.SenFu.Ju(1).HG1 = "食神" Or Bir.SenFu.Ju(1).HG2 = "食神" Or Bir.SenFu.Ju(1).HG3 = "食神" Or _
            Bir.SenFu.Ju(2).HG1 = "食神" Or Bir.SenFu.Ju(2).HG2 = "食神" Or Bir.SenFu.Ju(2).HG3 = "食神" Or _
            Bir.SenFu.Ju(3).HG1 = "食神" Or Bir.SenFu.Ju(3).HG2 = "食神" Or Bir.SenFu.Ju(3).HG3 = "食神" Or _
            Bir.SenFu.Ju(4).HG1 = "食神" Or Bir.SenFu.Ju(4).HG2 = "食神" Or Bir.SenFu.Ju(4).HG3 = "食神") Then
            Check4C2 = True
            C4C2Kan = "傷官格"
        Else
            Check4C2 = False
            C4C2Kan = ""
        End If
        If Bir.EngMode And Check4C2 Then UserMsg("C4C2Kan = " & C4C2Kan)

        '檢查Rule-4-C3 時干藏根,同類相應
        If Bir.SenFu.Ju(4).Gan = "正財" And ( _
            Bir.SenFu.Ju(1).HG1 = "偏財" Or Bir.SenFu.Ju(1).HG2 = "偏財" Or Bir.SenFu.Ju(1).HG3 = "偏財" Or _
            Bir.SenFu.Ju(2).HG1 = "偏財" Or Bir.SenFu.Ju(2).HG2 = "偏財" Or Bir.SenFu.Ju(2).HG3 = "偏財" Or _
            Bir.SenFu.Ju(3).HG1 = "偏財" Or Bir.SenFu.Ju(3).HG2 = "偏財" Or Bir.SenFu.Ju(3).HG3 = "偏財" Or _
            Bir.SenFu.Ju(4).HG1 = "偏財" Or Bir.SenFu.Ju(4).HG2 = "偏財" Or Bir.SenFu.Ju(4).HG3 = "偏財") Then
            Check4C3 = True
            C4C3Kan = "偏財格"
        ElseIf Bir.SenFu.Ju(4).Gan = "偏財" And ( _
            Bir.SenFu.Ju(1).HG1 = "正財" Or Bir.SenFu.Ju(1).HG2 = "正財" Or Bir.SenFu.Ju(1).HG3 = "正財" Or _
            Bir.SenFu.Ju(2).HG1 = "正財" Or Bir.SenFu.Ju(2).HG2 = "正財" Or Bir.SenFu.Ju(2).HG3 = "正財" Or _
            Bir.SenFu.Ju(3).HG1 = "正財" Or Bir.SenFu.Ju(3).HG2 = "正財" Or Bir.SenFu.Ju(3).HG3 = "正財" Or _
            Bir.SenFu.Ju(4).HG1 = "正財" Or Bir.SenFu.Ju(4).HG2 = "正財" Or Bir.SenFu.Ju(4).HG3 = "正財") Then
            Check4C3 = True
            C4C3Kan = "偏財格"
        ElseIf Bir.SenFu.Ju(4).Gan = "正官" And ( _
            Bir.SenFu.Ju(1).HG1 = "七殺" Or Bir.SenFu.Ju(1).HG2 = "七殺" Or Bir.SenFu.Ju(1).HG3 = "七殺" Or _
            Bir.SenFu.Ju(2).HG1 = "七殺" Or Bir.SenFu.Ju(2).HG2 = "七殺" Or Bir.SenFu.Ju(2).HG3 = "七殺" Or _
            Bir.SenFu.Ju(3).HG1 = "七殺" Or Bir.SenFu.Ju(3).HG2 = "七殺" Or Bir.SenFu.Ju(3).HG3 = "七殺" Or _
            Bir.SenFu.Ju(4).HG1 = "七殺" Or Bir.SenFu.Ju(4).HG2 = "七殺" Or Bir.SenFu.Ju(4).HG3 = "七殺") Then
            Check4C3 = True
            C4C3Kan = "七殺格"
        ElseIf Bir.SenFu.Ju(4).Gan = "七殺" And ( _
            Bir.SenFu.Ju(1).HG1 = "正官" Or Bir.SenFu.Ju(1).HG2 = "正官" Or Bir.SenFu.Ju(1).HG3 = "正官" Or _
            Bir.SenFu.Ju(2).HG1 = "正官" Or Bir.SenFu.Ju(2).HG2 = "正官" Or Bir.SenFu.Ju(2).HG3 = "正官" Or _
            Bir.SenFu.Ju(3).HG1 = "正官" Or Bir.SenFu.Ju(3).HG2 = "正官" Or Bir.SenFu.Ju(3).HG3 = "正官" Or _
            Bir.SenFu.Ju(4).HG1 = "正官" Or Bir.SenFu.Ju(4).HG2 = "正官" Or Bir.SenFu.Ju(4).HG3 = "正官") Then
            Check4C3 = True
            C4C3Kan = "七殺格"
        ElseIf Bir.SenFu.Ju(4).Gan = "正印" And ( _
            Bir.SenFu.Ju(1).HG1 = "偏印" Or Bir.SenFu.Ju(1).HG2 = "偏印" Or Bir.SenFu.Ju(1).HG3 = "偏印" Or _
            Bir.SenFu.Ju(2).HG1 = "偏印" Or Bir.SenFu.Ju(2).HG2 = "偏印" Or Bir.SenFu.Ju(2).HG3 = "偏印" Or _
            Bir.SenFu.Ju(3).HG1 = "偏印" Or Bir.SenFu.Ju(3).HG2 = "偏印" Or Bir.SenFu.Ju(3).HG3 = "偏印" Or _
            Bir.SenFu.Ju(4).HG1 = "偏印" Or Bir.SenFu.Ju(4).HG2 = "偏印" Or Bir.SenFu.Ju(4).HG3 = "偏印") Then
            Check4C3 = True
            C4C3Kan = "偏印格"
        ElseIf Bir.SenFu.Ju(4).Gan = "偏印" And ( _
            Bir.SenFu.Ju(1).HG1 = "正印" Or Bir.SenFu.Ju(1).HG2 = "正印" Or Bir.SenFu.Ju(1).HG3 = "正印" Or _
            Bir.SenFu.Ju(2).HG1 = "正印" Or Bir.SenFu.Ju(2).HG2 = "正印" Or Bir.SenFu.Ju(2).HG3 = "正印" Or _
            Bir.SenFu.Ju(3).HG1 = "正印" Or Bir.SenFu.Ju(3).HG2 = "正印" Or Bir.SenFu.Ju(3).HG3 = "正印" Or _
            Bir.SenFu.Ju(4).HG1 = "正印" Or Bir.SenFu.Ju(4).HG2 = "正印" Or Bir.SenFu.Ju(4).HG3 = "正印") Then
            Check4C3 = True
            C4C3Kan = "偏印格"
        ElseIf Bir.SenFu.Ju(4).Gan = "食神" And ( _
            Bir.SenFu.Ju(1).HG1 = "傷官" Or Bir.SenFu.Ju(1).HG2 = "傷官" Or Bir.SenFu.Ju(1).HG3 = "傷官" Or _
            Bir.SenFu.Ju(2).HG1 = "傷官" Or Bir.SenFu.Ju(2).HG2 = "傷官" Or Bir.SenFu.Ju(2).HG3 = "傷官" Or _
            Bir.SenFu.Ju(3).HG1 = "傷官" Or Bir.SenFu.Ju(3).HG2 = "傷官" Or Bir.SenFu.Ju(3).HG3 = "傷官" Or _
            Bir.SenFu.Ju(4).HG1 = "傷官" Or Bir.SenFu.Ju(4).HG2 = "傷官" Or Bir.SenFu.Ju(4).HG3 = "傷官") Then
            Check4C3 = True
            C4C3Kan = "傷官格"
        ElseIf Bir.SenFu.Ju(4).Gan = "傷官" And ( _
            Bir.SenFu.Ju(1).HG1 = "食神" Or Bir.SenFu.Ju(1).HG2 = "食神" Or Bir.SenFu.Ju(1).HG3 = "食神" Or _
            Bir.SenFu.Ju(2).HG1 = "食神" Or Bir.SenFu.Ju(2).HG2 = "食神" Or Bir.SenFu.Ju(2).HG3 = "食神" Or _
            Bir.SenFu.Ju(3).HG1 = "食神" Or Bir.SenFu.Ju(3).HG2 = "食神" Or Bir.SenFu.Ju(3).HG3 = "食神" Or _
            Bir.SenFu.Ju(4).HG1 = "食神" Or Bir.SenFu.Ju(4).HG2 = "食神" Or Bir.SenFu.Ju(4).HG3 = "食神") Then
            Check4C3 = True
            C4C3Kan = "傷官格"
        Else
            Check4C3 = False
            C4C3Kan = ""
        End If
        If Bir.EngMode And Check4C3 Then UserMsg("C4C3Kan = " & C4C3Kan)
        '-########################################################################################################
        '最後判斷部分
        If CheckFirst1 Then
            Bir.Main8wordKan = CF1Kan
        ElseIf CheckFirst2 Then
            Bir.Main8wordKan = CF2Kan
        ElseIf CheckFirst3 Then
            Bir.Main8wordKan = CF3Kan
        ElseIf Check4A1 Or Check4A2 Then   '--------------4A
            If Check4A1 Then Bir.Main8wordKan = C4A1Kan
            If Check4A2 Then Bir.Main8wordKan = C4A2Kan
            If Check4A1 And Check4A2 Then
                '檢查雙格
                If (C4A1Kan = "偏印格" And C4A2Kan = "正印格") Or (C4A2Kan = "偏印格" And C4A1Kan = "正印格") Then
                    Bir.Main8wordKan = "偏印格"
                ElseIf (C4A1Kan = "偏財格" And C4A2Kan = "正財格") Or (C4A2Kan = "偏財格" And C4A1Kan = "正財格") Then
                    Bir.Main8wordKan = "偏財格"
                ElseIf (C4A1Kan = "七殺格" And C4A2Kan = "正官格") Or (C4A2Kan = "七殺格" And C4A1Kan = "正官格") Then
                    Bir.Main8wordKan = "七殺格"
                ElseIf (C4A1Kan = "食神格" And C4A2Kan = "傷官格") Or (C4A2Kan = "食神格" And C4A1Kan = "傷官格") Then
                    Bir.Main8wordKan = "傷官格"
                Else
                    Bir.Main8wordKan = C4A1Kan
                    Bir.Sub8wordKan1 = C4A2Kan
                End If
            End If
        ElseIf Check4B1 Or Check4B2 Or Check4B3 Or Check4BB Then '--------------4B
            'only have one ....
            If Check4B1 Then Bir.Main8wordKan = C4B1Kan
            If Check4B2 Then Bir.Main8wordKan = C4B2Kan
            If Check4B3 Then Bir.Main8wordKan = C4B3Kan
            If Check4BB Then Bir.Main8wordKan = C4BBKan
            '三合三會氣貫月支 ===================
            If Check4BB And Check4BB_Month Then
                Bir.Main8wordKan = C4BBKan
                If Check4B1 Then Bir.Sub8wordKan1 = C4B1Kan
                If Check4B2 Then Bir.Sub8wordKan1 = C4B2Kan
                If Check4B3 Then Bir.Sub8wordKan1 = C4B3Kan
                If Check4B1 And Check4B2 Then
                    'in BB 檢查雙格 -------------B1B2
                    If (C4B1Kan = "偏印格" And C4B2Kan = "正印格") Or (C4B2Kan = "偏印格" And C4B1Kan = "正印格") Then
                        Bir.Sub8wordKan1 = "偏印格"
                        If (Check4B3) Then Bir.Sub8wordKan2 = C4B3Kan
                    ElseIf (C4B1Kan = "偏財格" And C4B2Kan = "正財格") Or (C4B2Kan = "偏財格" And C4B1Kan = "正財格") Then
                        Bir.Sub8wordKan1 = "偏財格"
                        If (Check4B3) Then Bir.Sub8wordKan2 = C4B3Kan
                    ElseIf (C4B1Kan = "七殺格" And C4B2Kan = "正官格") Or (C4B2Kan = "七殺格" And C4B1Kan = "正官格") Then
                        Bir.Sub8wordKan1 = "七殺格"
                        If (Check4B3) Then Bir.Sub8wordKan2 = C4B3Kan
                    ElseIf (C4B1Kan = "食神格" And C4B2Kan = "傷官格") Or (C4B2Kan = "食神格" And C4B1Kan = "傷官格") Then
                        Bir.Sub8wordKan1 = "傷官格"
                        If (Check4B3) Then Bir.Sub8wordKan2 = C4B3Kan
                    Else
                        Bir.Sub8wordKan1 = C4B2Kan
                        Bir.Sub8wordKan2 = C4B1Kan
                        'If (Check4B3) Then Bir.Sub8wordKan3 = C4B3Kan
                    End If
                End If  'if B1B2
                If Check4B3 And Check4B2 Then
                    'in BB 檢查雙格 -------------B3B2
                    If (C4B3Kan = "偏印格" And C4B2Kan = "正印格") Or (C4B2Kan = "偏印格" And C4B3Kan = "正印格") Then
                        Bir.Sub8wordKan1 = "偏印格"
                        If (Check4B1) Then Bir.Sub8wordKan2 = C4B1Kan
                    ElseIf (C4B3Kan = "偏財格" And C4B2Kan = "正財格") Or (C4B2Kan = "偏財格" And C4B3Kan = "正財格") Then
                        Bir.Sub8wordKan1 = "偏財格"
                        If (Check4B1) Then Bir.Sub8wordKan2 = C4B1Kan
                    ElseIf (C4B3Kan = "七殺格" And C4B2Kan = "正官格") Or (C4B2Kan = "七殺格" And C4B3Kan = "正官格") Then
                        Bir.Sub8wordKan1 = "七殺格"
                        If (Check4B1) Then Bir.Sub8wordKan2 = C4B1Kan
                    ElseIf (C4B3Kan = "食神格" And C4B2Kan = "傷官格") Or (C4B2Kan = "食神格" And C4B3Kan = "傷官格") Then
                        Bir.Sub8wordKan1 = "傷官格"
                        If (Check4B1) Then Bir.Sub8wordKan2 = C4B1Kan
                    Else
                        Bir.Sub8wordKan1 = C4B2Kan
                        Bir.Sub8wordKan2 = C4B3Kan
                        'If (Check4B1) Then Bir.Sub8wordKan3 = C4B1Kan
                    End If
                End If   'if B2B3
                If Check4B1 And Check4B3 Then
                    '檢查雙格 -------------B1B3
                    If (C4B1Kan = "偏印格" And C4B3Kan = "正印格") Or (C4B3Kan = "偏印格" And C4B1Kan = "正印格") Then
                        Bir.Sub8wordKan1 = "偏印格"
                        If (Check4B2) Then Bir.Sub8wordKan2 = C4B2Kan
                    ElseIf (C4B1Kan = "偏財格" And C4B3Kan = "正財格") Or (C4B3Kan = "偏財格" And C4B1Kan = "正財格") Then
                        Bir.Sub8wordKan1 = "偏財格"
                        If (Check4B2) Then Bir.Sub8wordKan2 = C4B2Kan
                    ElseIf (C4B1Kan = "七殺格" And C4B3Kan = "正官格") Or (C4B3Kan = "七殺格" And C4B1Kan = "正官格") Then
                        Bir.Sub8wordKan1 = "七殺格"
                        If (Check4B2) Then Bir.Sub8wordKan2 = C4B2Kan
                    ElseIf (C4B1Kan = "食神格" And C4B3Kan = "傷官格") Or (C4B3Kan = "食神格" And C4B1Kan = "傷官格") Then
                        Bir.Sub8wordKan1 = "傷官格"
                        If (Check4B2) Then Bir.Sub8wordKan2 = C4B2Kan
                    Else
                        Bir.Sub8wordKan1 = C4B3Kan
                        Bir.Sub8wordKan2 = C4B1Kan
                        'If (Check4B2) Then Bir.Sub8wordKan3 = C4B2Kan
                    End If
                End If 'if B1B3
            End If
            '檢查雙格 ====================
            If Check4B1 And Check4B2 Then
                '檢查雙格 -------------B1B2
                If (C4B1Kan = "偏印格" And C4B2Kan = "正印格") Or (C4B2Kan = "偏印格" And C4B1Kan = "正印格") Then
                    Bir.Main8wordKan = "偏印格"
                    If (Check4B3) Then
                        Bir.Sub8wordKan1 = C4B3Kan
                        If Check4BB Then Bir.Sub8wordKan2 = C4BBKan
                    End If
                ElseIf (C4B1Kan = "偏財格" And C4B2Kan = "正財格") Or (C4B2Kan = "偏財格" And C4B1Kan = "正財格") Then
                    Bir.Main8wordKan = "偏財格"
                    If (Check4B3) Then
                        Bir.Sub8wordKan1 = C4B3Kan
                        If Check4BB Then Bir.Sub8wordKan2 = C4BBKan
                    End If
                ElseIf (C4B1Kan = "七殺格" And C4B2Kan = "正官格") Or (C4B2Kan = "七殺格" And C4B1Kan = "正官格") Then
                    Bir.Main8wordKan = "七殺格"
                    If (Check4B3) Then
                        Bir.Sub8wordKan1 = C4B3Kan
                        If Check4BB Then Bir.Sub8wordKan2 = C4BBKan
                    End If
                ElseIf (C4B1Kan = "食神格" And C4B2Kan = "傷官格") Or (C4B2Kan = "食神格" And C4B1Kan = "傷官格") Then
                    Bir.Main8wordKan = "傷官格"
                    If (Check4B3) Then
                        Bir.Sub8wordKan1 = C4B3Kan
                        If Check4BB Then Bir.Sub8wordKan2 = C4BBKan
                    End If
                Else
                    Bir.Main8wordKan = C4B2Kan
                    Bir.Sub8wordKan1 = C4B1Kan
                    If (Check4B3) Then Bir.Sub8wordKan2 = C4B3Kan
                    If Check4BB Then Bir.Sub8wordKan2 = C4BBKan
                End If
            End If
            If Check4B3 And Check4B2 Then
                '檢查雙格 -------------B3B2
                If (C4B3Kan = "偏印格" And C4B2Kan = "正印格") Or (C4B2Kan = "偏印格" And C4B3Kan = "正印格") Then
                    Bir.Main8wordKan = "偏印格"
                    If (Check4B1) Then
                        Bir.Sub8wordKan1 = C4B1Kan
                        If Check4BB Then Bir.Sub8wordKan2 = C4BBKan
                    End If
                ElseIf (C4B3Kan = "偏財格" And C4B2Kan = "正財格") Or (C4B2Kan = "偏財格" And C4B3Kan = "正財格") Then
                    Bir.Main8wordKan = "偏財格"
                    If (Check4B1) Then
                        Bir.Sub8wordKan1 = C4B1Kan
                        If Check4BB Then Bir.Sub8wordKan2 = C4BBKan
                    End If
                ElseIf (C4B3Kan = "七殺格" And C4B2Kan = "正官格") Or (C4B2Kan = "七殺格" And C4B3Kan = "正官格") Then
                    Bir.Main8wordKan = "七殺格"
                    If (Check4B1) Then
                        Bir.Sub8wordKan1 = C4B1Kan
                        If Check4BB Then Bir.Sub8wordKan2 = C4BBKan
                    End If
                ElseIf (C4B3Kan = "食神格" And C4B2Kan = "傷官格") Or (C4B2Kan = "食神格" And C4B3Kan = "傷官格") Then
                    Bir.Main8wordKan = "傷官格"
                    If (Check4B1) Then
                        Bir.Sub8wordKan1 = C4B1Kan
                        If Check4BB Then Bir.Sub8wordKan2 = C4BBKan
                    End If
                Else
                    Bir.Main8wordKan = C4B2Kan
                    Bir.Sub8wordKan1 = C4B3Kan
                    If (Check4B1) Then Bir.Sub8wordKan2 = C4B1Kan
                    If Check4BB Then Bir.Sub8wordKan2 = C4BBKan
                End If
            End If
            If Check4B1 And Check4B3 Then
                '檢查雙格 -------------B1B3
                If (C4B1Kan = "偏印格" And C4B3Kan = "正印格") Or (C4B3Kan = "偏印格" And C4B1Kan = "正印格") Then
                    Bir.Main8wordKan = "偏印格"
                    If (Check4B2) Then
                        Bir.Sub8wordKan1 = C4B2Kan
                        If Check4BB Then Bir.Sub8wordKan2 = C4BBKan
                    End If
                ElseIf (C4B1Kan = "偏財格" And C4B3Kan = "正財格") Or (C4B3Kan = "偏財格" And C4B1Kan = "正財格") Then
                    Bir.Main8wordKan = "偏財格"
                    If (Check4B2) Then
                        Bir.Sub8wordKan1 = C4B2Kan
                        If Check4BB Then Bir.Sub8wordKan2 = C4BBKan
                    End If
                ElseIf (C4B1Kan = "七殺格" And C4B3Kan = "正官格") Or (C4B3Kan = "七殺格" And C4B1Kan = "正官格") Then
                    Bir.Main8wordKan = "七殺格"
                    If (Check4B2) Then
                        Bir.Sub8wordKan1 = C4B2Kan
                        If Check4BB Then Bir.Sub8wordKan2 = C4BBKan
                    End If
                ElseIf (C4B1Kan = "食神格" And C4B3Kan = "傷官格") Or (C4B3Kan = "食神格" And C4B1Kan = "傷官格") Then
                    Bir.Main8wordKan = "傷官格"
                    If (Check4B2) Then
                        Bir.Sub8wordKan1 = C4B2Kan
                        If Check4BB Then Bir.Sub8wordKan2 = C4BBKan
                    End If
                Else
                    Bir.Main8wordKan = C4B3Kan
                    Bir.Sub8wordKan1 = C4B1Kan
                    If (Check4B2) Then Bir.Sub8wordKan2 = C4B2Kan
                    If Check4BB Then Bir.Sub8wordKan2 = C4BBKan
                End If
            End If
        ElseIf Check4C1 Or Check4C2 Or Check4C3 Then '--------------4C
            If Check4C1 Then
                Bir.Main8wordKan = C4C1Kan
                If Check4C2 And (C4C1Kan <> C4C2Kan) Then
                    Bir.Sub8wordKan1 = C4C2Kan
                    If Check4C3 And (C4C3Kan <> C4C1Kan) And (C4C3Kan <> C4C2Kan) Then Bir.Sub8wordKan2 = C4C3Kan
                Else
                    If Check4C3 Then Bir.Sub8wordKan1 = C4C3Kan
                End If
            End If
            If Check4C3 Then
                Bir.Main8wordKan = C4C3Kan
                If Check4C1 And (C4C3Kan <> C4C1Kan) Then
                    Bir.Sub8wordKan1 = C4C1Kan
                    If Check4C2 And (C4C2Kan <> C4C3Kan) And (C4C2Kan <> C4C1Kan) Then Bir.Sub8wordKan2 = C4C2Kan
                Else
                    If Check4C2 Then Bir.Sub8wordKan1 = C4C2Kan
                End If
            End If
            If Check4C2 Then
                Bir.Main8wordKan = C4C2Kan
                If Check4C3 And (C4C2Kan <> C4C3Kan) Then
                    Bir.Sub8wordKan1 = C4C3Kan
                    If Check4C1 And (C4C1Kan <> C4C3Kan) And (C4C1Kan <> C4C2Kan) Then Bir.Sub8wordKan2 = C4C1Kan
                Else
                    If Check4C1 Then Bir.Sub8wordKan1 = C4C1Kan
                End If
            End If
        Else
            '不在1,2,3,4A,4B,4C之中!!!
            UserMsg("八字取格時,有特殊狀況, 不在rule1,2,3,4A,4B之中!!")
        End If
        UserMsg("八字取格(主要) = " & Bir.Main8wordKan & ",(次要1) = " & Bir.Sub8wordKan1 & ",(次要2) = " & Bir.Sub8wordKan2)
    End Sub

    Private Sub BtnGTsearch_Click(sender As System.Object, e As System.EventArgs) Handles BtnGTsearch.Click
        Dim mbx As Integer
        UserMsg("=======================================")
        UserMsg("由干支找日子....")
        UserMsg("輸入姓名欄位:填入連續六字的年月日干支")
        UserMsg("=======================================")
        mbx = MsgBox("Q:確定由由干支找日子?", vbYesNo + vbQuestion, "GT search ?")
        If mbx = vbYes Then
            GanTsuSearchDate()
        End If
    End Sub

    Private Sub GanTsuSearchDate()
        Dim YGT, MGT, DGT As String
        Dim DisY, DisM, DisD As Integer
        Dim rCount As Integer = 0
        Dim YMD1 As String = ""      '暫存目前查詢出來的西曆生辰資料
        Dim Ju() As String = New String(8) {"", "", "", "", "", "", "", "", ""} 'Ju(0)不用 '暫存目前查詢出來的8柱
        '取得干支輸入
        YGT = Mid(TBoxName.Text, 1, 2)
        MGT = Mid(TBoxName.Text, 3, 2)
        DGT = Mid(TBoxName.Text, 5, 2)
        '檢查輸入值
        If (Not IsInGan(Mid(YGT, 1, 1))) Or (Not IsInGan(Mid(MGT, 1, 1))) Or (Not IsInGan(Mid(DGT, 1, 1))) Or _
            (Not IsInTsu(Mid(YGT, 2, 1))) Or (Not IsInTsu(Mid(MGT, 2, 1))) Or (Not IsInTsu(Mid(DGT, 2, 1))) Then
            UserMsg("輸入不是連續六字的干支! quit....")
            Exit Sub
        End If
        SQLstr = " Select * from SAM where 一柱='" & YGT & "'"
        SQLstr = SQLstr & " and 二柱='" & MGT & "'"
        SQLstr = SQLstr & " and 三柱='" & DGT & "'"
        SQLcmd = New SqlCommand(SQLstr, DBconn)
        DBreader = SQLcmd.ExecuteReader()
        If DBreader.HasRows Then
            Do While DBreader.Read()
                YMD1 = DBreader("西曆")
                Ju(1) = DBreader("一柱")
                Ju(2) = DBreader("二柱")
                Ju(3) = DBreader("三柱")
                rCount = rCount + 1
                DisY = Microsoft.VisualBasic.Year(YMD1) - 1911
                DisM = Microsoft.VisualBasic.Month(YMD1)
                DisD = Microsoft.VisualBasic.Day(YMD1)
                UserMsg("結果" & rCount & " 日期=民國" & DisY & "年" & DisM & "月" & DisD & "日" _
                        & ",干支=" & Ju(1) & Ju(2) & Ju(3))
            Loop
        Else
            UserMsg("資料庫裡沒有符合干支的資料")
        End If
        UserMsg("搜尋結束")
        DBreader.Close()
        SQLcmd.Dispose()
    End Sub

    Private Sub FingUsingGod()
        '根據日干與月令
        Select Case Bir.Ju(3).Gan
            Case "甲"
                Select Case Bir.Ju(2).Tsu
                    Case "子"
                        Bir.UsingGodMain = "丁" : Bir.UsingGodSub = "庚丙"
                    Case "丑"
                        Bir.UsingGodMain = "丁" : Bir.UsingGodSub = "庚丙"
                    Case "寅"
                        Bir.UsingGodMain = "丙" : Bir.UsingGodSub = "癸"
                    Case "卯"
                        Bir.UsingGodMain = "庚" : Bir.UsingGodSub = "丙丁戊己"
                    Case "辰"
                        Bir.UsingGodMain = "庚" : Bir.UsingGodSub = "丁壬"
                    Case "巳"
                        Bir.UsingGodMain = "癸" : Bir.UsingGodSub = "丁庚"
                    Case "午"
                        Bir.UsingGodMain = "癸" : Bir.UsingGodSub = "丁庚"
                    Case "未"
                        Bir.UsingGodMain = "癸" : Bir.UsingGodSub = "丁庚"
                    Case "申"
                        Bir.UsingGodMain = "庚" : Bir.UsingGodSub = "丁壬"
                    Case "酉"
                        Bir.UsingGodMain = "庚" : Bir.UsingGodSub = "丁丙"
                    Case "戌"
                        Bir.UsingGodMain = "庚" : Bir.UsingGodSub = "甲丁壬癸"
                    Case "亥"
                        Bir.UsingGodMain = "庚" : Bir.UsingGodSub = "丁丙戊"
                    Case Else
                        UserMsg("取用神時,發現月令有誤 ")
                End Select
            Case "乙"
                Select Case Bir.Ju(2).Tsu
                    Case "子"
                        Bir.UsingGodMain = "丙" : Bir.UsingGodSub = ""
                    Case "丑"
                        Bir.UsingGodMain = "丙" : Bir.UsingGodSub = ""
                    Case "寅"
                        Bir.UsingGodMain = "丙" : Bir.UsingGodSub = "癸"
                    Case "卯"
                        Bir.UsingGodMain = "丙" : Bir.UsingGodSub = "癸"
                    Case "辰"
                        Bir.UsingGodMain = "癸" : Bir.UsingGodSub = "丙戊"
                    Case "巳"
                        Bir.UsingGodMain = "癸" : Bir.UsingGodSub = ""
                    Case "午"
                        Bir.UsingGodMain = "癸" : Bir.UsingGodSub = "丙"
                    Case "未"
                        Bir.UsingGodMain = "癸" : Bir.UsingGodSub = "丙"
                    Case "申"
                        Bir.UsingGodMain = "丙" : Bir.UsingGodSub = "癸己"
                    Case "酉"
                        Bir.UsingGodMain = "癸" : Bir.UsingGodSub = "丙丁"
                    Case "戌"
                        Bir.UsingGodMain = "癸" : Bir.UsingGodSub = "辛"
                    Case "亥"
                        Bir.UsingGodMain = "丙" : Bir.UsingGodSub = "戊"
                    Case Else
                        UserMsg("取用神時,發現月令有誤 ")
                End Select
            Case "丙"
                Select Case Bir.Ju(2).Tsu
                    Case "子"
                        Bir.UsingGodMain = "壬" : Bir.UsingGodSub = "戊己"
                    Case "丑"
                        Bir.UsingGodMain = "壬" : Bir.UsingGodSub = "甲"
                    Case "寅"
                        Bir.UsingGodMain = "壬" : Bir.UsingGodSub = "庚"
                    Case "卯"
                        Bir.UsingGodMain = "壬" : Bir.UsingGodSub = "己"
                    Case "辰"
                        Bir.UsingGodMain = "壬" : Bir.UsingGodSub = "甲"
                    Case "巳"
                        Bir.UsingGodMain = "壬" : Bir.UsingGodSub = "庚癸"
                    Case "午"
                        Bir.UsingGodMain = "壬" : Bir.UsingGodSub = "庚"
                    Case "未"
                        Bir.UsingGodMain = "壬" : Bir.UsingGodSub = "庚"
                    Case "申"
                        Bir.UsingGodMain = "壬" : Bir.UsingGodSub = "戊"
                    Case "酉"
                        Bir.UsingGodMain = "壬" : Bir.UsingGodSub = "癸"
                    Case "戌"
                        Bir.UsingGodMain = "甲" : Bir.UsingGodSub = "壬"
                    Case "亥"
                        Bir.UsingGodMain = "甲" : Bir.UsingGodSub = "戊庚壬"
                    Case Else
                        UserMsg("取用神時,發現月令有誤 ")
                End Select
            Case "丁"
                Select Case Bir.Ju(2).Tsu
                    Case "子"
                        Bir.UsingGodMain = "甲" : Bir.UsingGodSub = "庚"
                    Case "丑"
                        Bir.UsingGodMain = "甲" : Bir.UsingGodSub = "庚"
                    Case "寅"
                        Bir.UsingGodMain = "甲" : Bir.UsingGodSub = "庚"
                    Case "卯"
                        Bir.UsingGodMain = "庚" : Bir.UsingGodSub = "甲"
                    Case "辰"
                        Bir.UsingGodMain = "甲" : Bir.UsingGodSub = "庚"
                    Case "巳"
                        Bir.UsingGodMain = "甲" : Bir.UsingGodSub = "庚"
                    Case "午"
                        Bir.UsingGodMain = "壬" : Bir.UsingGodSub = "庚癸"
                    Case "未"
                        Bir.UsingGodMain = "甲" : Bir.UsingGodSub = "壬庚"
                    Case "申"
                        Bir.UsingGodMain = "甲" : Bir.UsingGodSub = "庚丙戊"
                    Case "酉"
                        Bir.UsingGodMain = "甲" : Bir.UsingGodSub = "庚丙戊"
                    Case "戌"
                        Bir.UsingGodMain = "甲" : Bir.UsingGodSub = "庚戊"
                    Case "亥"
                        Bir.UsingGodMain = "甲" : Bir.UsingGodSub = "庚"
                    Case Else
                        UserMsg("取用神時,發現月令有誤 ")
                End Select
            Case "戊"
                Select Case Bir.Ju(2).Tsu
                    Case "子"
                        Bir.UsingGodMain = "丙" : Bir.UsingGodSub = "甲"
                    Case "丑"
                        Bir.UsingGodMain = "丙" : Bir.UsingGodSub = "甲"
                    Case "寅"
                        Bir.UsingGodMain = "丙" : Bir.UsingGodSub = "甲癸"
                    Case "卯"
                        Bir.UsingGodMain = "丙" : Bir.UsingGodSub = "甲癸"
                    Case "辰"
                        Bir.UsingGodMain = "甲" : Bir.UsingGodSub = "丙癸"
                    Case "巳"
                        Bir.UsingGodMain = "甲" : Bir.UsingGodSub = "丙癸"
                    Case "午"
                        Bir.UsingGodMain = "壬" : Bir.UsingGodSub = "甲丙"
                    Case "未"
                        Bir.UsingGodMain = "癸" : Bir.UsingGodSub = "丙甲"
                    Case "申"
                        Bir.UsingGodMain = "丙" : Bir.UsingGodSub = "癸甲"
                    Case "酉"
                        Bir.UsingGodMain = "丙" : Bir.UsingGodSub = "癸"
                    Case "戌"
                        Bir.UsingGodMain = "甲" : Bir.UsingGodSub = "丙癸"
                    Case "亥"
                        Bir.UsingGodMain = "甲" : Bir.UsingGodSub = "丙"
                    Case Else
                        UserMsg("取用神時,發現月令有誤 ")
                End Select
            Case "己"
                Select Case Bir.Ju(2).Tsu
                    Case "子"
                        Bir.UsingGodMain = "丙" : Bir.UsingGodSub = "甲戊"
                    Case "丑"
                        Bir.UsingGodMain = "丙" : Bir.UsingGodSub = "甲戊"
                    Case "寅"
                        Bir.UsingGodMain = "丙" : Bir.UsingGodSub = "庚甲"
                    Case "卯"
                        Bir.UsingGodMain = "甲" : Bir.UsingGodSub = "丙癸"
                    Case "辰"
                        Bir.UsingGodMain = "丙" : Bir.UsingGodSub = "癸甲"
                    Case "巳"
                        Bir.UsingGodMain = "癸" : Bir.UsingGodSub = "丙"
                    Case "午"
                        Bir.UsingGodMain = "癸" : Bir.UsingGodSub = "丙"
                    Case "未"
                        Bir.UsingGodMain = "癸" : Bir.UsingGodSub = "丙"
                    Case "申"
                        Bir.UsingGodMain = "丙" : Bir.UsingGodSub = "癸"
                    Case "酉"
                        Bir.UsingGodMain = "丙" : Bir.UsingGodSub = "癸"
                    Case "戌"
                        Bir.UsingGodMain = "甲" : Bir.UsingGodSub = "丙癸"
                    Case "亥"
                        Bir.UsingGodMain = "丙" : Bir.UsingGodSub = "甲戊"
                    Case Else
                        UserMsg("取用神時,發現月令有誤 ")
                End Select
            Case "庚"
                Select Case Bir.Ju(2).Tsu
                    Case "子"
                        Bir.UsingGodMain = "丁" : Bir.UsingGodSub = "甲丙"
                    Case "丑"
                        Bir.UsingGodMain = "丙" : Bir.UsingGodSub = "丁甲"
                    Case "寅"
                        Bir.UsingGodMain = "戊" : Bir.UsingGodSub = "甲丙壬丁"
                    Case "卯"
                        Bir.UsingGodMain = "丁" : Bir.UsingGodSub = "甲丙庚"
                    Case "辰"
                        Bir.UsingGodMain = "甲" : Bir.UsingGodSub = "丁壬癸"
                    Case "巳"
                        Bir.UsingGodMain = "壬" : Bir.UsingGodSub = "戊丙丁"
                    Case "午"
                        Bir.UsingGodMain = "壬" : Bir.UsingGodSub = "癸"
                    Case "未"
                        Bir.UsingGodMain = "丁" : Bir.UsingGodSub = "甲"
                    Case "申"
                        Bir.UsingGodMain = "丁" : Bir.UsingGodSub = "甲"
                    Case "酉"
                        Bir.UsingGodMain = "丁" : Bir.UsingGodSub = "甲丙"
                    Case "戌"
                        Bir.UsingGodMain = "甲" : Bir.UsingGodSub = "壬"
                    Case "亥"
                        Bir.UsingGodMain = "丁" : Bir.UsingGodSub = "丙"
                    Case Else
                        UserMsg("取用神時,發現月令有誤 ")
                End Select
            Case "辛"
                Select Case Bir.Ju(2).Tsu
                    Case "子"
                        Bir.UsingGodMain = "丙" : Bir.UsingGodSub = "戊壬甲"
                    Case "丑"
                        Bir.UsingGodMain = "丙" : Bir.UsingGodSub = "壬戊己"
                    Case "寅"
                        Bir.UsingGodMain = "己" : Bir.UsingGodSub = "壬庚"
                    Case "卯"
                        Bir.UsingGodMain = "壬" : Bir.UsingGodSub = "甲"
                    Case "辰"
                        Bir.UsingGodMain = "壬" : Bir.UsingGodSub = "甲"
                    Case "巳"
                        Bir.UsingGodMain = "壬" : Bir.UsingGodSub = "甲癸"
                    Case "午"
                        Bir.UsingGodMain = "壬" : Bir.UsingGodSub = "己癸"
                    Case "未"
                        Bir.UsingGodMain = "壬" : Bir.UsingGodSub = "庚甲"
                    Case "申"
                        Bir.UsingGodMain = "壬" : Bir.UsingGodSub = "甲戊"
                    Case "酉"
                        Bir.UsingGodMain = "壬" : Bir.UsingGodSub = "甲"
                    Case "戌"
                        Bir.UsingGodMain = "壬" : Bir.UsingGodSub = "甲"
                    Case "亥"
                        Bir.UsingGodMain = "壬" : Bir.UsingGodSub = "丙"
                    Case Else
                        UserMsg("取用神時,發現月令有誤 ")
                End Select
            Case "壬"
                Select Case Bir.Ju(2).Tsu
                    Case "子"
                        Bir.UsingGodMain = "戊" : Bir.UsingGodSub = "丙"
                    Case "丑"
                        Bir.UsingGodMain = "丙" : Bir.UsingGodSub = "丁甲"
                    Case "寅"
                        Bir.UsingGodMain = "庚" : Bir.UsingGodSub = "丙戊"
                    Case "卯"
                        Bir.UsingGodMain = "戊" : Bir.UsingGodSub = "辛庚"
                    Case "辰"
                        Bir.UsingGodMain = "甲" : Bir.UsingGodSub = "庚"
                    Case "巳"
                        Bir.UsingGodMain = "壬" : Bir.UsingGodSub = "辛庚癸"
                    Case "午"
                        Bir.UsingGodMain = "癸" : Bir.UsingGodSub = "庚辛"
                    Case "未"
                        Bir.UsingGodMain = "辛" : Bir.UsingGodSub = "甲"
                    Case "申"
                        Bir.UsingGodMain = "戊" : Bir.UsingGodSub = "丁"
                    Case "酉"
                        Bir.UsingGodMain = "甲" : Bir.UsingGodSub = "庚"
                    Case "戌"
                        Bir.UsingGodMain = "甲" : Bir.UsingGodSub = "丙"
                    Case "亥"
                        Bir.UsingGodMain = "戊" : Bir.UsingGodSub = "丙庚"
                    Case Else
                        UserMsg("取用神時,發現月令有誤 ")
                End Select
            Case "癸"
                Select Case Bir.Ju(2).Tsu
                    Case "子"
                        Bir.UsingGodMain = "丙" : Bir.UsingGodSub = "辛"
                    Case "丑"
                        Bir.UsingGodMain = "丙" : Bir.UsingGodSub = "丁"
                    Case "寅"
                        Bir.UsingGodMain = "辛" : Bir.UsingGodSub = "丙"
                    Case "卯"
                        Bir.UsingGodMain = "庚" : Bir.UsingGodSub = "辛"
                    Case "辰"
                        Bir.UsingGodMain = "丙" : Bir.UsingGodSub = "辛甲"
                    Case "巳"
                        Bir.UsingGodMain = "辛" : Bir.UsingGodSub = ""
                    Case "午"
                        Bir.UsingGodMain = "庚" : Bir.UsingGodSub = "壬癸"
                    Case "未"
                        Bir.UsingGodMain = "庚" : Bir.UsingGodSub = "辛壬癸"
                    Case "申"
                        Bir.UsingGodMain = "丁" : Bir.UsingGodSub = ""
                    Case "酉"
                        Bir.UsingGodMain = "辛" : Bir.UsingGodSub = "丙"
                    Case "戌"
                        Bir.UsingGodMain = "辛" : Bir.UsingGodSub = "甲壬癸"
                    Case "亥"
                        Bir.UsingGodMain = "庚" : Bir.UsingGodSub = "辛戊丁"
                    Case Else
                        UserMsg("取用神時,發現月令有誤 ")
                End Select
            Case Else
                UserMsg("取用神時,發現日干有誤 ")
        End Select
    End Sub
    Private Sub FindStrongWeak()
        '判別日主強弱
        'Rule1 :下面三者之一,即不算弱 
        '  1-A : 得令
        '  1-B : 得地
        '  1-C : 得黨
        'Rule2 : 再依照三干四支,做生扶/剋洩的投票
        Dim Check_1A As Boolean = False
        Dim Check_1B As Boolean = False
        Dim Check_1C As Boolean = False
        Dim check_postive As Integer = 0
        Dim check_negative As Integer = 0
        Dim StrongWeak As String = ""
        'cehck 1-A 得令
        Select Case Bir.Ju(3).Gan
            Case "甲", "乙"
                If Bir.Ju(2).Tsu = "寅" Or Bir.Ju(2).Tsu = "卯" Then
                    Check_1A = True
                End If
            Case "丙", "丁"
                If Bir.Ju(2).Tsu = "巳" Or Bir.Ju(2).Tsu = "午" Then
                    Check_1A = True
                End If
            Case "戊", "己"
                If Bir.Ju(2).Tsu = "巳" Or Bir.Ju(2).Tsu = "午" Or _
                    Bir.Ju(2).Tsu = "辰" Or Bir.Ju(2).Tsu = "戌" Or _
                    Bir.Ju(2).Tsu = "丑" Or Bir.Ju(2).Tsu = "未" Then
                    Check_1A = True
                End If
            Case "庚", "辛"
                If Bir.Ju(2).Tsu = "申" Or Bir.Ju(2).Tsu = "酉" Then
                    Check_1A = True
                End If
            Case "壬", "癸"
                If Bir.Ju(2).Tsu = "亥" Or Bir.Ju(2).Tsu = "子" Then
                    Check_1A = True
                End If
            Case Else
                UserMsg("判日主強弱時,發現日干有誤 ")
        End Select
        'cehck 1-B 得地
        Select Case Bir.Ju(3).Gan
            Case "甲", "乙"
                If (Bir.Ju(1).Tsu = "寅" Or Bir.Ju(1).Tsu = "卯" Or _
                    Bir.Ju(3).Tsu = "寅" Or Bir.Ju(3).Tsu = "卯" Or _
                    Bir.Ju(4).Tsu = "寅" Or Bir.Ju(4).Tsu = "卯") Then
                    Check_1B = True
                End If
            Case "丙", "丁"
                If (Bir.Ju(1).Tsu = "巳" Or Bir.Ju(1).Tsu = "午" Or _
                    Bir.Ju(3).Tsu = "巳" Or Bir.Ju(3).Tsu = "午" Or _
                    Bir.Ju(4).Tsu = "巳" Or Bir.Ju(4).Tsu = "午") Then
                    Check_1B = True
                End If
            Case "戊", "己"
                If (Bir.Ju(1).Tsu = "巳" Or Bir.Ju(1).Tsu = "午" Or _
                    Bir.Ju(3).Tsu = "巳" Or Bir.Ju(3).Tsu = "午" Or _
                    Bir.Ju(4).Tsu = "巳" Or Bir.Ju(4).Tsu = "午" Or _
                    Bir.Ju(1).Tsu = "辰" Or Bir.Ju(1).Tsu = "戌" Or _
                    Bir.Ju(3).Tsu = "辰" Or Bir.Ju(3).Tsu = "戌" Or _
                    Bir.Ju(4).Tsu = "辰" Or Bir.Ju(4).Tsu = "戌" Or _
                    Bir.Ju(1).Tsu = "丑" Or Bir.Ju(1).Tsu = "未" Or _
                    Bir.Ju(3).Tsu = "丑" Or Bir.Ju(3).Tsu = "未" Or _
                    Bir.Ju(4).Tsu = "丑" Or Bir.Ju(4).Tsu = "未") Then
                    Check_1B = True
                End If
            Case "庚", "辛"
                If (Bir.Ju(1).Tsu = "申" Or Bir.Ju(1).Tsu = "酉" Or _
                    Bir.Ju(3).Tsu = "申" Or Bir.Ju(3).Tsu = "酉" Or _
                    Bir.Ju(4).Tsu = "申" Or Bir.Ju(4).Tsu = "酉") Then
                    Check_1B = True
                End If
            Case "壬", "癸"
                If (Bir.Ju(1).Tsu = "亥" Or Bir.Ju(1).Tsu = "子" Or _
                    Bir.Ju(3).Tsu = "亥" Or Bir.Ju(3).Tsu = "子" Or _
                    Bir.Ju(4).Tsu = "亥" Or Bir.Ju(4).Tsu = "子") Then
                    Check_1B = True
                End If
            Case Else
                UserMsg("判日主強弱時,發現日干有誤 ")
        End Select
        'check 1-C 得黨
        If (Bir.SenFu.Ju(1).Gan = "比肩" Or Bir.SenFu.Ju(1).Gan = "劫財" Or _
            Bir.SenFu.Ju(2).Gan = "比肩" Or Bir.SenFu.Ju(2).Gan = "劫財" Or _
            Bir.SenFu.Ju(4).Gan = "比肩" Or Bir.SenFu.Ju(4).Gan = "劫財") Then
            Check_1C = True
        End If
        '計算 check_postive, check_negative
        check_postive = 0 : check_negative = 0
        If (Bir.SenFu.Ju(1).Gan = "比肩" Or Bir.SenFu.Ju(1).Gan = "劫財" Or _
            Bir.SenFu.Ju(1).Gan = "正印" Or Bir.SenFu.Ju(1).Gan = "偏印") Then
            check_postive = check_postive + 1
        Else
            check_negative = check_negative + 1
        End If
        If (Bir.SenFu.Ju(2).Gan = "比肩" Or Bir.SenFu.Ju(2).Gan = "劫財" Or _
            Bir.SenFu.Ju(2).Gan = "正印" Or Bir.SenFu.Ju(2).Gan = "偏印") Then
            check_postive = check_postive + 1
        Else
            check_negative = check_negative + 1
        End If
        If (Bir.SenFu.Ju(4).Gan = "比肩" Or Bir.SenFu.Ju(4).Gan = "劫財" Or _
            Bir.SenFu.Ju(4).Gan = "正印" Or Bir.SenFu.Ju(4).Gan = "偏印") Then
            check_postive = check_postive + 1
        Else
            check_negative = check_negative + 1
        End If
        If (Bir.SenFu.Ju(1).Tsu = "比肩" Or Bir.SenFu.Ju(1).Tsu = "劫財" Or _
            Bir.SenFu.Ju(1).Tsu = "正印" Or Bir.SenFu.Ju(1).Tsu = "偏印") Then
            check_postive = check_postive + 1
        Else
            check_negative = check_negative + 1
        End If
        If (Bir.SenFu.Ju(2).Tsu = "比肩" Or Bir.SenFu.Ju(2).Tsu = "劫財" Or _
            Bir.SenFu.Ju(2).Tsu = "正印" Or Bir.SenFu.Ju(2).Tsu = "偏印") Then
            check_postive = check_postive + 1
        Else
            check_negative = check_negative + 1
        End If
        If (Bir.SenFu.Ju(3).Tsu = "比肩" Or Bir.SenFu.Ju(3).Tsu = "劫財" Or _
            Bir.SenFu.Ju(3).Tsu = "正印" Or Bir.SenFu.Ju(3).Tsu = "偏印") Then
            check_postive = check_postive + 1
        Else
            check_negative = check_negative + 1
        End If
        If (Bir.SenFu.Ju(4).Tsu = "比肩" Or Bir.SenFu.Ju(4).Tsu = "劫財" Or _
            Bir.SenFu.Ju(4).Tsu = "正印" Or Bir.SenFu.Ju(4).Tsu = "偏印") Then
            check_postive = check_postive + 1
        Else
            check_negative = check_negative + 1
        End If
        If Bir.EngMode Then UserMsg("印比=" & check_postive & ", 剋洩=" & check_negative)
        '==================================================================================
        If Check_1A Then StrongWeak = "得令"
        If Check_1B Then StrongWeak = StrongWeak & "得地"
        If Check_1C Then StrongWeak = StrongWeak & "得黨"
        If check_postive > check_negative Then
            StrongWeak = StrongWeak & "-身強"
        Else
            StrongWeak = StrongWeak & "-身弱"
        End If
        Bir.DayStrongWeak = StrongWeak
    End Sub
End Class

'為了 GoogleMap 查經緯度需要
Public Class LatLon
    Public Property Latitude As Double
    Public Property Longitude As Double
    Public Sub New()
    End Sub
    Public Sub New(ByVal lat As Double, ByVal lon As Double)
        Me.Latitude = lat
        Me.Longitude = lon
    End Sub
End Class