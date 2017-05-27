﻿Imports System.Drawing
Imports System.Reflection
Imports System.IO

Public Class Result
    Dim bmpWidth As Integer = 1090 '圖形的寬度 
    Dim bmpHeight As Integer = 780 '圖形的高度 0rg=772
    Dim bmpWidth_v As Integer = bmpHeight '直式圖形的寬度 
    Dim bmpHeight_v As Integer = bmpWidth '直式圖形的高度 
    Dim BMP As Bitmap = New Bitmap(bmpWidth, bmpHeight)
    Dim BMPv As Bitmap = New Bitmap(bmpWidth_v, bmpHeight_v)
    Dim sCycleBMP As Bitmap
    Dim G As Graphics
    Dim Gv As Graphics

    '準備畫筆
    Dim BlackPen1 = New System.Drawing.Pen(Color.Black, 1)     '內文使用
    Dim BluePen1 = New System.Drawing.Pen(Color.DarkBlue, 1)   '表格使用
    Dim BluePen2 = New System.Drawing.Pen(Color.DarkBlue, 2)
    Dim BrownPen1 = New System.Drawing.Pen(Color.Brown, 1)     '星圖使用
    Dim BrownPen2 = New System.Drawing.Pen(Color.Brown, 2)
    Dim Black_Brush As New SolidBrush(Color.Black)    '內文使用
    Dim Blue_Brush As New SolidBrush(Color.DarkBlue)  '表格使用
    Dim Brown_Brush As New SolidBrush(Color.Brown)    '八柱和星圖使用
    Dim RedPen = New System.Drawing.Pen(Color.Red, 1) ' 手繪使用

    Public lPressed, lDraw As Boolean
    Public intX, intY As Single
    Dim PageNo As Integer = 0

    '主要的顯示資料副程式,直寫黑色文字,可選有或無藍框
    Private Sub MyText(ByVal X As Integer, ByVal Y As Integer, _
                       ByVal Width As Integer, ByVal Height As Integer, _
                       ByVal strText As String, ByVal intFontSize As Integer, _
                       Optional ByVal HasBoder As Boolean = True, _
                       Optional ByVal VirtalCenter As Boolean = True)
        Dim RecArea As New Rectangle(X, Y, Width, Height)
        '為了小方格內的文字,將受Padding影響,所以特別微調矩形,給DrawString使用
        Dim RecArea2 As New Rectangle(X, Y, Width + 4, Height)
        If HasBoder Then
            G.DrawRectangle(BluePen1, RecArea)
            HasBoder = False
        End If
        Dim DS_Font As New Font("新細明體", intFontSize)
        Dim DS_Format As New StringFormat(StringFormatFlags.DirectionVertical)
        DS_Format.LineAlignment = StringAlignment.Center
        If VirtalCenter Then
            DS_Format.Alignment = StringAlignment.Center
            VirtalCenter = False
        Else
            DS_Format.Alignment = StringAlignment.Near
        End If
        G.DrawString(strText, DS_Font, Black_Brush, RecArea2, DS_Format)
    End Sub

    '次要的顯示資料副程式,橫寫黑色文字,可選有或無藍框
    Private Sub MyTextH(ByVal X As Integer, ByVal Y As Integer, _
                        ByVal Width As Integer, ByVal Height As Integer, _
                        ByVal strText As String, ByVal intFontSize As Integer, _
                        Optional ByVal HasBoder As Boolean = True)
        Dim RecArea As New Rectangle(X, Y, Width, Height)
        '為了小方格內的文字,將受Padding影響,所以特別微調矩形,給DrawString使用
        Dim RecArea2 As New Rectangle(X - 2, Y + 1, Width + 4, Height + 4)
        If HasBoder Then
            G.DrawRectangle(BluePen1, RecArea)
            HasBoder = False
        End If
        Dim DS_Font As New Font("新細明體", intFontSize)
        Dim DS_Format As New StringFormat(StringFormatFlags.NoWrap)
        DS_Format.Alignment = StringAlignment.Center
        DS_Format.LineAlignment = StringAlignment.Center
        G.DrawString(strText, DS_Font, Black_Brush, RecArea2, DS_Format)
    End Sub

    '主要的顯示表格副程式,直寫藍色文字,可選有或無藍框, 可選加強框
    Private Sub TbText(ByVal X As Integer, ByVal Y As Integer, _
                       ByVal Width As Integer, ByVal Height As Integer, _
                       ByVal strText As String, ByVal intFontSize As Integer,
                       Optional ByVal HasBoder As Boolean = True, _
                       Optional ByVal L1Boder As Boolean = True)
        Dim RecArea As New Rectangle(X, Y, Width, Height)
        If HasBoder Then
            If L1Boder Then
                G.DrawRectangle(BluePen1, RecArea)
            Else
                G.DrawRectangle(BluePen2, RecArea)
                L1Boder = True
            End If
        Else
            HasBoder = True
        End If
        Dim DS_Font As New Font("標楷體", intFontSize)
        Dim DS_Format As New StringFormat(StringFormatFlags.DirectionVertical)
        DS_Format.Alignment = StringAlignment.Center
        DS_Format.LineAlignment = StringAlignment.Center
        G.DrawString(strText, DS_Font, Blue_Brush, RecArea, DS_Format)
    End Sub

    '主要的顯示表格副程式,橫寫藍色文字,固定藍框,標楷體字體
    Private Sub TbTextH(ByVal X As Integer, ByVal Y As Integer, _
                        ByVal Width As Integer, ByVal Height As Integer, _
                        ByVal strText As String, ByVal intFontSize As Integer, _
                        Optional ByVal HasBoder As Boolean = True)
        Dim RecArea As New Rectangle(X, Y, Width, Height)
        '為了方格內的文字,將受Padding影響,所以特別微調矩形,給DrawString使用
        Dim RecArea2 As New Rectangle(X - 2, Y + 1, Width + 4, Height + 4)
        If HasBoder Then
            G.DrawRectangle(BluePen1, RecArea)
            HasBoder = False
        End If
        Dim DS_Font As New Font("標楷體", intFontSize)
        Dim DS_Format As New StringFormat(StringFormatFlags.DirectionRightToLeft)
        DS_Format.Alignment = StringAlignment.Center
        DS_Format.LineAlignment = StringAlignment.Center
        G.DrawString(strText, DS_Font, Blue_Brush, RecArea2, DS_Format)
    End Sub

    '八柱凸顯字體,直寫棕色文字,固定藍框,標楷體字體
    Private Sub TblTextGu(ByVal X As Integer, ByVal Y As Integer, _
                          ByVal Width As Integer, ByVal Height As Integer, _
                          ByVal strText As String, ByVal intFontSize As Integer, _
                          Optional ByVal HasBoder As Boolean = True)
        Dim RecArea As New Rectangle(X, Y, Width, Height)
        If HasBoder Then
            G.DrawRectangle(BluePen1, RecArea)
            HasBoder = False
        End If
        Dim DS_Font As New Font("標楷體", intFontSize, FontStyle.Bold)
        Dim DS_Format As New StringFormat(StringFormatFlags.DirectionVertical)
        DS_Format.Alignment = StringAlignment.Center
        DS_Format.LineAlignment = StringAlignment.Center
        G.DrawString(strText, DS_Font, Brown_Brush, RecArea, DS_Format)
    End Sub

    '顯示標楷體資料,直寫黑色文字,可選有或無黑框(細)
    Private Sub MyBkBkText(ByVal X As Integer, ByVal Y As Integer, _
                       ByVal Width As Integer, ByVal Height As Integer, _
                       ByVal strText As String, ByVal intFontSize As Integer, _
                       Optional ByVal HasBoder As Boolean = True, _
                       Optional ByVal VirtalCenter As Boolean = True)
        Dim RecArea As New Rectangle(X, Y, Width, Height)
        '為了小方格內的文字,將受Padding影響,所以特別微調矩形,給DrawString使用
        Dim RecArea2 As New Rectangle(X, Y, Width + 4, Height)
        If HasBoder Then
            G.DrawRectangle(BlackPen1, RecArea) '黑框!!!
            HasBoder = False
        End If
        Dim DS_Font As New Font("標楷體", intFontSize)
        Dim DS_Format As New StringFormat(StringFormatFlags.DirectionVertical)
        DS_Format.LineAlignment = StringAlignment.Center
        If VirtalCenter Then
            DS_Format.Alignment = StringAlignment.Center
            VirtalCenter = False
        Else
            DS_Format.Alignment = StringAlignment.Near
        End If
        G.DrawString(strText, DS_Font, Black_Brush, RecArea2, DS_Format)
    End Sub

    '顯示橫寫黑色文字,可選有或無藍框
    Private Sub MyBkBkTextH(ByVal X As Integer, ByVal Y As Integer, _
                        ByVal Width As Integer, ByVal Height As Integer, _
                        ByVal strText As String, ByVal intFontSize As Integer, _
                        Optional ByVal HasBoder As Boolean = True)
        Dim RecArea As New Rectangle(X, Y, Width, Height)
        '為了小方格內的文字,將受Padding影響,所以特別微調矩形,給DrawString使用
        Dim RecArea2 As New Rectangle(X - 2, Y + 1, Width + 4, Height + 4)
        If HasBoder Then
            G.DrawRectangle(BlackPen1, RecArea)
            HasBoder = False
        End If
        Dim DS_Font As New Font("標楷體", intFontSize)
        Dim DS_Format As New StringFormat(StringFormatFlags.NoWrap)
        DS_Format.Alignment = StringAlignment.Center
        DS_Format.LineAlignment = StringAlignment.Center
        G.DrawString(strText, DS_Font, Black_Brush, RecArea2, DS_Format)
    End Sub
    '---------------------------------------------------------------------------------------------------------

    Private Sub Btn_Pan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Pan.Click
        Btn_Pan.Enabled = False
        Btn_Pen.Enabled = True
        Btn_Pen.Focus()
    End Sub

    Private Sub Btn_Pen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Pen.Click
        Btn_Pan.Enabled = True
        Btn_Pen.Enabled = False
        Btn_Pan.Focus()
    End Sub

    Private Sub Btn_Erase_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Erase.Click
        PicDisplay.Load("c:\5ArtSave\working_Fate00.bmp")
        'PicDisplay.Refresh()
    End Sub

    Private Sub Btn_Print_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Print.Click
        Me.PrintDialog1.Document = Me.PrintDocument1
        If PageAsVirtical Then
            PrintDocument1.DefaultPageSettings.Landscape = False
        Else
            PrintDocument1.DefaultPageSettings.Landscape = True
        End If
        If Me.PrintDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            PrintDocument1.Print()
        End If
    End Sub

    Private Sub Btn_Close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Close.Click
        Me.Close()
    End Sub

    ' By default, KeyDown does not fire for the ARROW keys
    Private Sub Btn_Print_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)
        Select Case (e.KeyCode)
            Case Keys.Escape
                Me.Close()
        End Select
    End Sub

    ' PreviewKeyDown is where you preview the key.
    ' Do not put any logic here, instead use the
    ' KeyDown event after setting IsInputKey to true.
    Private Sub Btn_Print_PreviewKeyDown(ByVal sender As Object, ByVal e As PreviewKeyDownEventArgs)
        Select Case (e.KeyCode)
            Case Keys.Escape
                e.IsInputKey = True
        End Select
    End Sub

    Public Sub Result_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '按鍵準備
        AddHandler Btn_Print.PreviewKeyDown, AddressOf Me.Btn_Print_PreviewKeyDown
        AddHandler Btn_Print.KeyDown, AddressOf Me.Btn_Print_KeyDown
        '畫布準備
        PicDisplay.Image = BMP
        G = Graphics.FromImage(BMP)
        G.SmoothingMode = Drawing2D.SmoothingMode.HighQuality '可以改善鋸齒狀的問題
        G.Clear(Color.White) '白色為底色

        '準備圓形圖
        If Bir.EngMode Then
            sCycleBMP = Global.Fate.My.Resources.star_cycle1_with_memo
        Else
            sCycleBMP = Global.Fate.My.Resources.star_cycle1
        End If

        '準備好預設的按鈕狀態
        Btn_Pan.Enabled = False
        Btn_Pen.Enabled = True

        '======================== Drawing 命造表 =================
        'Main Drwaing 
        StartDrawP00()
        '預先儲存命造表影像存檔
        PicDisplay.Image.Save("c:\5ArtSave\working_Fate00.bmp", System.Drawing.Imaging.ImageFormat.Bmp)
    End Sub

    Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Dim g As Graphics
        g = e.Graphics
        g.DrawImage(PicDisplay.Image, 0, 0)
        g.Dispose()
        e.HasMorePages = False
    End Sub

    'Main drawing page_0 here. (this one call all sub.)
    Private Sub StartDrawP00()
        PicDisplay.Image = BMP
        G = Graphics.FromImage(BMP)
        G.SmoothingMode = Drawing2D.SmoothingMode.HighQuality '可以改善鋸齒狀的問題
        G.Clear(Color.White) '白色為底色
        PicDisplay.Invalidate()
        '外邊的矩形邊框 
        Dim MyPictureBoder0 As New Rectangle(0, 0, (bmpWidth - 10), (bmpHeight - 14))
        G.DrawRectangle(BluePen2, MyPictureBoder0) '畫矩形
        ShowBasicData()
        ShowPartOne()
        ShowPartTwo()
        ShowPartTree()
        ShowPartFour()
        ShowPartFive()
        ShowPartSix()
        ShowPartSeven()
        PageNo = 0
        Label_page.Text = "P." & PageNo
    End Sub

    Private Sub ShowBasicData()
        Dim TmpStr As String
        Dim Xst As Integer = 1060
        Dim Yst As Integer = 40

        TmpStr = Trim(Bir.Name)
        If Bir.Sex = "男" Then
            TmpStr = TmpStr & " 先生福造"
        Else
            TmpStr = TmpStr & " 女士雅造"
        End If

        '1行----X----Y----W----H
        TbText(Xst - 20, Yst - 20, 40, 70, "姓名", 14)
        MyText(Xst - 20, Yst + 50, 40, 200, TmpStr, 14)
        TbText(Xst - 20, Yst + 250, 40, 50, "生辰", 14)
        MyText(Xst, Yst + 300, 20, 225, "陽　 年　   月　 日　 時　 分", 12)
        MyText(Xst - 20, Yst + 300, 20, 225, "農　 年　   月　 日　 時　 分", 12)
        MyTextH(Xst, Yst + 324, 20, 20, Bir.Solar.Y, 8, False) '小字型為了三位數的年數
        MyTextH(Xst - 20, Yst + 324, 20, 20, Bir.Lunar.Y, 8, False)
        MyTextH(Xst, Yst + 366, 20, 20, Bir.Solar.M, 9, False)
        If Bir.Lunar_IntercalaryMonth Then
            MyTextH(Xst - 20, Yst + 360, 20, 20, "閏", 9, False)
            MyTextH(Xst - 20, Yst + 371, 20, 20, Bir.Lunar.M, 9, False)
        Else
            MyTextH(Xst - 20, Yst + 366, 20, 20, Bir.Lunar.M, 9, False)
        End If
        MyTextH(Xst, Yst + 407, 20, 20, Bir.Solar.D, 9, False)
        MyTextH(Xst - 20, Yst + 407, 20, 20, Bir.Lunar.D, 9, False)
        MyTextH(Xst, Yst + 443, 20, 20, Bir.BornHour, 9, False)
        MyTextH(Xst - 20, Yst + 443, 20, 20, Bir.BornHour, 9, False)
        MyTextH(Xst, Yst + 479, 20, 20, Bir.BornMinute, 9, False)
        MyTextH(Xst - 20, Yst + 479, 20, 20, Bir.BornMinute, 9, False)
        TbText(Xst - 20, Yst + 525, 40, 55, "出生地", 13)
        MyText(Xst - 20, Yst + 580, 40, 147, Bir.BornPlaceAddr, 9)

        '2行---X----Y----W----H
        TbText(Xst - 60, Yst - 20, 40, 70, "編號", 14)
        MyText(Xst - 60, Yst + 50, 40, 100, Bir.No, 14)
        TbText(Xst - 60, Yst + 150, 40, 50, "性別", 14)
        MyText(Xst - 60, Yst + 200, 40, 50, Bir.SType & Bir.Sex, 14)
        TbText(Xst - 60, Yst + 250, 40, 50, "月令", 14)
        MyText(Xst - 40, Yst + 300, 20, 225, "", 12)   'Previous Ja
        MyText(Xst - 60, Yst + 300, 20, 225, "", 12)   'Next Ja
        MyText(Xst - 40, Yst + 300, 20, 150, Bir.PreJa.Name & "      -      -            :", 10, False)
        MyText(Xst - 60, Yst + 300, 20, 150, Bir.NxtJa.Name & "      -      -            :", 10, False)
        MyTextH(Xst - 40, Yst + 342, 20, 20, Bir.PreJa.YMD.Y, 8, False) '小字型為了三位數的年數
        MyTextH(Xst - 60, Yst + 342, 20, 20, Bir.NxtJa.YMD.Y, 8, False)
        MyTextH(Xst - 40, Yst + 364, 20, 20, Bir.PreJa.YMD.M, 9, False)
        MyTextH(Xst - 60, Yst + 364, 20, 20, Bir.NxtJa.YMD.M, 9, False)
        MyTextH(Xst - 40, Yst + 387, 20, 20, Bir.PreJa.YMD.D, 9, False)
        MyTextH(Xst - 60, Yst + 387, 20, 20, Bir.NxtJa.YMD.D, 9, False)
        MyTextH(Xst - 40, Yst + 415, 20, 20, Bir.PreJa.Hour, 9, False)
        MyTextH(Xst - 60, Yst + 415, 20, 20, Bir.NxtJa.Hour, 9, False)
        MyTextH(Xst - 40, Yst + 432, 20, 20, Bir.PreJa.Min, 9, False)
        MyTextH(Xst - 60, Yst + 432, 20, 20, Bir.NxtJa.Min, 9, False)
        TbText(Xst - 60, Yst + 525, 40, 45, "地標", 13)
        If Bir.BornPlace.LatDeg < 0 Then
            MyText(Xst - 40, Yst + 570, 20, 157, " 　南緯:    度    分    秒", 10, True, False)
        Else
            MyText(Xst - 40, Yst + 570, 20, 157, " 　北緯:    度    分    秒", 10, True, False)
        End If
        If Bir.BornPlace.LonDeg < 0 Then
            MyText(Xst - 60, Yst + 570, 20, 157, " 　西經:    度    分    秒", 10, True, False)
        Else
            MyText(Xst - 60, Yst + 570, 20, 157, " 　東經:    度    分    秒", 10, True, False)
        End If
        MyTextH(Xst - 40, Yst + 615, 20, 20, Math.Abs(Bir.BornPlace.LatDeg), 8, False) '小字型為了三位數的度數
        MyTextH(Xst - 60, Yst + 615, 20, 20, Math.Abs(Bir.BornPlace.LonDeg), 8, False) '小字型為了三位數的度數
        MyTextH(Xst - 40, Yst + 643, 20, 20, Bir.BornPlace.LatMin, 9, False)
        MyTextH(Xst - 60, Yst + 643, 20, 20, Bir.BornPlace.LonMin, 9, False)
        MyTextH(Xst - 40, Yst + 670, 20, 20, Bir.BornPlace.LatSec, 9, False)
        MyTextH(Xst - 60, Yst + 670, 20, 20, Bir.BornPlace.LonSec, 9, False)
    End Sub

    Private Sub ShowPartOne()
        Dim TopNumber As String() = New String(7) {"一", "二", "三", "四", "五", "六", "七", "八"}
        Dim intLop As Integer
        Dim lngGap As Integer = 32
        Dim Xst As Integer = 1000
        Dim Yst As Integer = 20
        '表頭數字
        For intLop = 0 To 7
            TbText(Xst - (lngGap * (intLop + 1)), Yst, lngGap, 20, TopNumber(intLop), 11)
        Next
        '八柱資料
        For i As Integer = 1 To 8
            TblTextGu(Xst - (lngGap * i), Yst + 20, lngGap, 50, Bir.Ju(i).Gan & Bir.Ju(i).Tsu, 14)
            '藏干
            MyText(Xst - (lngGap * i), Yst + 70, lngGap, 54, Bir.Ju(i).hdG, 12, True, False)
            '八柱神輔1.
            MyText(Xst - (lngGap * i), Yst + 124, lngGap, 18, SF1word(Bir.SenFu.Ju(i).Gan), 12)    '天元部分
            MyText(Xst - (lngGap * i), Yst + 142, lngGap, 18, SF1word(Bir.SenFu.Ju(i).Tsu), 12)    '地元部分
            MyText(Xst - (lngGap * i), Yst + 160, lngGap, 54, SF1word(Bir.SenFu.Ju(i).HG1), 12, True, False) '人元部分藏干123
            MyText(Xst - (lngGap * i), Yst + 178, lngGap, 18, SF1word(Bir.SenFu.Ju(i).HG2), 12, False)
            MyText(Xst - (lngGap * i), Yst + 196, lngGap, 18, SF1word(Bir.SenFu.Ju(i).HG3), 12, False)
            '八柱納音五行
            MyText(Xst - (lngGap * i), Yst + 214, lngGap, 45, Bir.NaIng.Ju(i), 10)
            '八柱本五行
            MyTextH(Xst - (lngGap * i), Yst + 259, lngGap, 15, Bir.WuSing.Ju(i).Gan, 10) '天元部分
            MyTextH(Xst - (lngGap * i), Yst + 274, lngGap, 15, Bir.WuSing.Ju(i).Tsu, 10) '地元部分
            MyTextH(Xst - (lngGap * i), Yst + 289, lngGap, 15, Bir.WuSing.Ju(i).HG1, 10) '人元部分
            MyTextH(Xst - (lngGap * i), Yst + 304, lngGap, 15, Bir.WuSing.Ju(i).HG2, 10)
            MyTextH(Xst - (lngGap * i), Yst + 319, lngGap, 15, Bir.WuSing.Ju(i).HG3, 10)
            '八柱所屬神煞 
            MyTextH(Xst - (lngGap * i), Yst + 334, lngGap, 201, "", 8)
            For j As Integer = 0 To 13
                MyTextH(Xst - (lngGap * i), Yst + 334 + (14 * j), lngGap, 14, Bir.SenSa(i, j + 1), 10, False)
            Next
            '八柱的所屬十二宮
            MyText(Xst - (lngGap * i), Yst + 535, lngGap, 16, Mid(Bir.MiKon.Ju(i), 1, 1), 11)
            '八柱的所屬十二長生
            MyText(Xst - (lngGap * i), Yst + 551, lngGap, 16, Mid(Bir.LongLife12.Ju(i), 1, 1), 11)
            '八柱配日月與行星
            MyText(Xst - (lngGap * i), Yst + 567, lngGap, 30, Bir.Star.Ju(i), 10)
            '八柱配十二星座
            MyText(Xst - (lngGap * i), Yst + 597, lngGap, 30, Bir.StarB.Ju(i), 10)
            '八柱配二八星宿
            MyText(Xst - (lngGap * i), Yst + 627, lngGap, 60, Bir.StarC.Ju(i), 10, True, False)
            '八柱配六十四卦
            If FrmFate.GwaMethod = "1" Then
                MyText(Xst - (lngGap * i), Yst + 687, lngGap, 58, Bir.Gwa.Ju(i).GwaName, 9, True, False)
            Else
                MyText(Xst - (lngGap * i), Yst + 687, lngGap, 58, Bir.Gwa.Ju(i).GwaName2, 9, True, False)
            End If
        Next
    End Sub

    Private Sub ShowPartTwo()
        Dim rWidth As Integer = 18
        Dim lWidth As Integer = 31
        Dim Xst As Integer = 744
        Dim Yst As Integer = 20
        Dim KonName As String() = New String(12) {"宮名", "命宮", "財帛", "兄弟", _
           "田宅", "男女", "奴僕", "配偶", "疾厄", "遷移", "官祿", "福壽", "相貌"}
        TbTextH(Xst - (rWidth * 4), Yst, rWidth * 4, 20, "次  宮", 12)
        TbTextH(Xst - (rWidth * 6), Yst, rWidth * 2, 20, "運限", 12)
        TbTextH(Xst - (rWidth * 23), Yst, rWidth * 17, 20, "際         星", 12)
        For i As Integer = 1 To 12
            MyTextH(Xst - rWidth, Yst + 20 + (lWidth * (i - 1)), rWidth, lWidth, Format(i), 11)
            '十二命宮
            TbText(Xst - (rWidth * 2), Yst + 20 + (lWidth * (i - 1)), rWidth, lWidth, "", 11)
            TbText(Xst - (rWidth * 2), Yst + 18 + (lWidth * (i - 1)), rWidth, lWidth + 4, KonName(i), 11, False)
            MyText(Xst - (rWidth * 3), Yst + 20 + (lWidth * (i - 1)), rWidth, lWidth, Bir.Kon12(i), 11)
            '十二長生
            MyText(Xst - (rWidth * 4), Yst + 20 + (lWidth * (i - 1)), rWidth, lWidth, "", 10)
            MyText(Xst - (rWidth * 4), Yst + 20 + (lWidth * (i - 1)), rWidth, lWidth + 2, Bir.LongLife12.Kon(i), 10, False)
            '限
            MyText(Xst - (rWidth * 5), Yst + 20 + (lWidth * (i - 1)), rWidth, lWidth, "", 10)
            MyText(Xst - (rWidth * 5), Yst + 20 + (lWidth * (i - 1)), rWidth, lWidth + 2, Bir.Shan12(i), 10, False)
            '運
            MyText(Xst - (rWidth * 6), Yst + 20 + (lWidth * (i - 1)), rWidth, lWidth, "", 10)
            MyText(Xst - (rWidth * 6), Yst + 20 + (lWidth * (i - 1)), rWidth, lWidth + 2, Bir.Yuan12(i), 10, False)
            '神輔
            MyText(Xst - (rWidth * 7), Yst + 20 + (lWidth * (i - 1)), rWidth, lWidth, "", 10)
            MyText(Xst - (rWidth * 7), Yst + 20 + (lWidth * (i - 1)), rWidth, lWidth + 2, Bir.SenFu12(i), 10, False)
            For j As Integer = 1 To 14
                MyText(Xst - (rWidth * (j + 7)), Yst + 20 + (lWidth * (i - 1)), rWidth, lWidth, "", 10)
                MyText(Xst - (rWidth * (j + 7)), Yst + 20 + (lWidth * (i - 1)), rWidth, lWidth + 2, Mid(Bir.StarSky(i, j), 1, 2), 10, False)
            Next
            MyText(Xst - (rWidth * 22), Yst + 20 + (lWidth * (i - 1)), rWidth, lWidth, "", 10)
            MyText(Xst - (rWidth * 22), Yst + 20 + (lWidth * (i - 1)), rWidth, lWidth + 2, Bir.YearKing(i), 10, False)
            MyText(Xst - (rWidth * 23), Yst + 20 + (lWidth * (i - 1)), rWidth, lWidth, "", 10)
            MyText(Xst - (rWidth * 23), Yst + 20 + (lWidth * (i - 1)), rWidth, lWidth + 2, Bir.StarB2(i), 10, False)
        Next
    End Sub

    Private Sub ShowPartTree()
        Dim allGan As String() = New String(10) {"天", _
            "甲", "乙", "丙", "丁", "戊", "己", "庚", "辛", "壬", "癸"}
        Dim allTsu As String() = New String(12) {"干支", _
            "子", "丑", "寅", "卯", "辰", "巳", "午", "未", "申", "酉", "戌", "亥"}
        Dim strTMP As String = ""
        Dim rWidth As Integer = 18
        Dim lWidth As Integer = 28
        Dim Xst As Integer = 330
        Dim Yst As Integer = 20
        TbTextH(Xst - (rWidth * 11), Yst, rWidth * 11, 20, "線     運", 12)
        For i As Integer = 1 To 10
            TbTextH(Xst - (rWidth * i), Yst + 20, rWidth, lWidth, allGan(11 - i), 12)
            For j As Integer = 1 To 12
                strTMP = (Bir.Y60state(11 - i, j))
                If strTMP = "0" Then
                    strTMP = ""
                End If
                MyTextH(Xst - (rWidth * i), Yst + 20 + (lWidth * j), rWidth, lWidth, strTMP, 11)
            Next
        Next
        For j As Integer = 1 To 12
            TbTextH(Xst - (rWidth * 11), Yst + 20 + (lWidth * j), rWidth, lWidth, allTsu(j), 12)
        Next
        TbTextH(Xst - (rWidth * 11), Yst + 20, rWidth, lWidth, allTsu(0), 9)
    End Sub

    Private Sub ShowPartFour()
        Dim ShowTxt1 As String = "注意：本表切勿遺失憑此察看命者運程        年      月      日 計算"
        Dim SexType As String
        Dim Ystr As String
        Dim Mstr As String
        Dim Dstr As String
        Dim Xst As Integer = 0
        Dim Yst As Integer = 20
        SexType = Bir.SType & Bir.Sex
        Ystr = DateTime.Today.Year() - 1911
        Mstr = DateTime.Today.Month()
        Dstr = DateTime.Today.Day()
        '-----------------
        TbTextH(Xst, Yst, 132, 20, "要  擇  交  運", 12)
        TbTextH(Xst, Yst + 20, 132, 20, "註      備", 12)
        TbText(Xst, Yst, 132, 384, "", 11)
        '-----------------
        MyText(Xst, Yst + 40, 20, 344, ShowTxt1, 9)
        MyText(Xst, Yst + 242, 20, 40, Ystr, 9, False)
        MyText(Xst, Yst + 275, 20, 40, Mstr, 9, False)
        MyText(Xst, Yst + 308, 20, 40, Dstr, 9, False)
        '------------------
        TbText(Xst + 110, Yst + 40, 20, 344, "每逢        年    月︵      ︶後   天交脫", 12, False)
        MyText(Xst + 110, Yst + 75, 20, 70, Bir.YuanChange_YG, 11, False)
        MyTextH(Xst + 110, Yst + 160, 20, 30, Bir.YuanChange_M, 11, False)
        MyText(Xst + 110, Yst + 210, 20, 70, Bir.YuanChange_JC, 11, False)
        MyTextH(Xst + 110, Yst + 305, 20, 30, Bir.YuanChange_JCday, 11, False)
        '------------------
        If (SexType = "陽男") Or (SexType = "陰女") Then
            TbText(Xst + 90, Yst + 40, 20, 244, "順數至下一節計    天     時辰", 12, False)
        Else
            TbText(Xst + 90, Yst + 40, 20, 244, "逆數至下一節計    天     時辰", 12, False)
        End If
        MyTextH(Xst + 90, Yst + 155, 20, 30, Bir.Ja2Born_D, 11, False)
        MyTextH(Xst + 90, Yst + 210, 20, 30, Bir.Ja2Born_H, 11, False)
        '-------------------
        TbText(Xst + 70, Yst + 40, 20, 344, "出生後   年   月又   天︵      ︶年開始行運", 12, False)
        MyTextH(Xst + 70, Yst + 90, 20, 30, Bir.YuanStart.Y, 11, False)
        MyTextH(Xst + 70, Yst + 130, 20, 30, Bir.YuanStart.M, 11, False)
        MyTextH(Xst + 70, Yst + 188, 20, 30, Bir.YuanStart.D, 11, False)
        MyText(Xst + 70, Yst + 240, 20, 70, Bir.YuanStart_YearGT, 11, False)
        '-------------------
        TbText(Xst + 20, Yst + 30, 20, 120, "土金水火木", 12, False)
        TbText(Xst + 30, Yst + 30, 20, 120, "：：：：：", 12, False)
        MyText(Xst + 42, Yst + 30, 20, 120, Bir.WuSingState, 11, False)
    End Sub

    Private Sub ShowPartFive()
        Dim sTitle As String() = New String(10) {"運", _
             "一", "二", "三", "四", "五", "六", "七", "八", "九", "十"}
        Dim Xst As Integer = 744
        Dim Yst As Integer = 412   'old=392
        Dim sqWidth As Integer = 36
        Dim sqHeight As Integer = 18
        Dim Y_Gap As Integer = 14
        Dim Y_Shift As Integer = 5
        Dim strTMP As String
        For i As Integer = 1 To 10
            TbText(Xst - (sqWidth * i), Yst, sqWidth, sqHeight, sTitle(i), 12)
            '干支
            strTMP = Bir.BigYuan(i).Gan & Bir.BigYuan(i).Tsu
            MyText(Xst - (sqWidth * i), Yst + sqHeight, sqWidth, sqHeight * 2 + Y_Shift - 2, strTMP, 12)
            '幾歲
            MyTextH(Xst - (sqWidth * i), Yst + Y_Shift + (sqHeight * 3) - 2, sqWidth, Y_Gap + 2, "", 10)
            MyTextH(Xst - (sqWidth * i), Yst + (sqHeight * 3), sqWidth, Y_Gap + 2, Bir.BigYuan(i).YearsOld, 10, False)
            '神輔
            MyText(Xst - (sqWidth * i), Yst + Y_Shift + (sqHeight * 3) + Y_Gap, sqWidth, Y_Gap * 2, Bir.BigYuan(i).SenFu, 9)
            '納音
            MyTextH(Xst - (sqWidth * i), Yst + Y_Shift + (sqHeight * 3) + (Y_Gap * 3), sqWidth, Y_Gap, Bir.BigYuan(i).Naing, 8)
            '星座B2
            MyTextH(Xst - (sqWidth * i), Yst + Y_Shift + (sqHeight * 3) + (Y_Gap * 4), sqWidth, Y_Gap, Bir.BigYuan(i).StarB2, 8)
            '星座Star
            MyTextH(Xst - (sqWidth * i), Yst + Y_Shift + (sqHeight * 3) + (Y_Gap * 5), sqWidth, Y_Gap, Bir.BigYuan(i).Star, 8)
            '十二長生與十二命宮
            strTMP = Mid(Bir.BigYuan(i).Kon, 1, 1) & "  " & Mid(Bir.BigYuan(i).LongLife, 1, 1)
            MyTextH(Xst - (sqWidth * i), Yst + Y_Shift + (sqHeight * 3) + (Y_Gap * 6), sqWidth, Y_Gap, strTMP, 8)
            MyTextH(Xst - (sqWidth * i), Yst + Y_Shift + (sqHeight * 3) + (Y_Gap * 6), sqWidth / 2, Y_Gap, "", 8)
            '星際
            For j As Integer = 1 To 14
                MyTextH(Xst - (sqWidth * i), Yst + Y_Shift + (sqHeight * 3) + (Y_Gap * (6 + j)), sqWidth, Y_Gap, Bir.BigYuanStarSky(i, j), 8)
            Next
        Next
    End Sub

    Private Sub ShowPartSix()
        Dim xWidth1 As Integer = 20
        Dim yHeigth1 As Integer = 20
        Dim yHeigth2 As Integer = 18
        Dim Xst As Integer = 0
        Dim Yst As Integer = 404  'old=384
        Dim all_WuSing As String() = New String(4) {"土", "火", "水", "木", "金"}
        Dim all_SenFu As String() = New String(9) {"劫 財", "傷 官", "偏 財", "偏 印", "七 殺", _
                                                   "比 肩", "食 神", "正 財", "正 印", "正 官"}
        '五行計數
        For i As Integer = 0 To 4
            TbText(Xst + (i * xWidth1), Yst, xWidth1, yHeigth1, all_WuSing(i), 12)
            MyTextH(Xst + (i * xWidth1), Yst + yHeigth1, xWidth1, yHeigth1 * 2, Bir.WuSingCount(i + 1, 1), 12)
            MyTextH(Xst + (i * xWidth1), Yst + (yHeigth1 * 3), xWidth1, yHeigth1 * 2, Bir.WuSingCount(i + 1, 0), 12)
            MyTextH(Xst + (i * xWidth1), Yst + (yHeigth1 * 5), xWidth1, yHeigth1 * 2, Bir.WuSingCount(i + 1, 2), 12)
            '神輔計數
            TbText(Xst + (i * xWidth1), Yst + (yHeigth1 * 7), xWidth1, yHeigth2 * 3, all_SenFu(i), 12)
            MyTextH(Xst + (i * xWidth1), Yst + (yHeigth1 * 7) + (yHeigth2 * 3), xWidth1, yHeigth2 * 3, Bir.SenFuCount(i + 1), 12)
            TbText(Xst + (i * xWidth1), Yst + (yHeigth1 * 7) + (yHeigth2 * 6), xWidth1, yHeigth2 * 3, all_SenFu(i + 5), 12)
            MyTextH(Xst + (i * xWidth1), Yst + (yHeigth1 * 7) + (yHeigth2 * 9), xWidth1, (yHeigth2 * 3) + 5, Bir.SenFuCount(i + 6), 12)
        Next
        TbText(Xst + (5 * xWidth1), Yst, xWidth1, yHeigth1 * 3, "納音五行", 12)
        TbText(Xst + (5 * xWidth1), Yst + (yHeigth1 * 3), xWidth1, yHeigth1 * 4, "本五行", 12)
        TbText(Xst + (5 * xWidth1), Yst + (yHeigth1 * 7), xWidth1, (yHeigth2 * 12) + 5, "神 輔 計 數", 12)
    End Sub
    Private Sub ShowPartSeven()
        Dim BMP_strX As Integer = 127
        Dim BMP_strY As Integer = 415  'old=395    
        Dim CenterX As Integer = BMP_strX + 127
        Dim CenterY As Integer = BMP_strY + 121
        Dim R0 As Integer = 10
        Dim R1 As Integer = 72
        Dim R2 As Integer = 65
        Dim Cycle1 As New Rectangle(CenterX - R0, CenterY - R0, 2 * R0, 2 * R0)
        Dim CentrP As Point
        Dim Line1P As Point
        Dim Line2P As Point
        G.DrawImage(sCycleBMP, BMP_strX, BMP_strY)
        If Bir.EngMode Then
            G.DrawEllipse(BlackPen1, Cycle1) '畫圓以確認中心點座標
        Else
            TbTextH(BMP_strX, BMP_strY, 30, 30, "IV", 13, False)
            TbTextH(BMP_strX + 210, BMP_strY, 30, 30, "I", 13, False)
            TbTextH(BMP_strX, BMP_strY + 200, 50, 60, "III", 13, False)
            TbTextH(BMP_strX + 210, BMP_strY + 200, 50, 60, "II", 13, False)
        End If

        '出生的黃經度
        CentrP = New Point(CenterX, CenterY)
        Line1P = New Point( _
                   Math.Round(Math.Cos(6.285 * Bir.YellowGing.BornDate / 360 - 1.57125) * R2) + CenterX, _
                   Math.Round(Math.Sin(6.285 * Bir.YellowGing.BornDate / 360 - 1.57125) * R2) + CenterY)
        G.DrawLine(BluePen2, CentrP, Line1P)
        '現在的黃經度
        Line2P = New Point( _
                   Math.Round(Math.Cos(6.285 * Bir.YellowGing.NowDate / 360 - 1.57125) * R1) + CenterX, _
                   Math.Round(Math.Sin(6.285 * Bir.YellowGing.NowDate / 360 - 1.57125) * R1) + CenterY)
        G.DrawLine(BrownPen2, CentrP, Line2P)
        '表列資料
        Dim Xst As Integer = 380
        Dim Yst As Integer = 660   'old=652
        Dim xWidth As Integer = 24
        TbText(Xst - 260, Yst, 264, 105, "", 9) '小外框
        TbText(Xst - xWidth + 3, Yst, xWidth, 87, "命標：黃經", 11, False)
        MyTextH(Xst - xWidth + 3, Yst + 80, xWidth, 13, Bir.YellowGing.BornDate, 11, False)
        TbText(Xst - (xWidth * 2), Yst, xWidth, 50, "象限：", 11, False)
        MyTextH(Xst - (xWidth * 2) + 2, Yst + 50, xWidth, 13, Bir.YellowGing.Quadrant, 12, False)
        TbText(Xst - (xWidth * 3), Yst, xWidth, 82, "命運：已行", 11, False)
        MyTextH(Xst - (xWidth * 3) + 2, Yst + 82, xWidth, 13, Bir.YellowGing.Passed, 11, False)
        TbText(Xst - (xWidth * 4), Yst, xWidth, 82, "現在：黃經", 11, False)
        MyTextH(Xst - (xWidth * 4) + 2, Yst + 82, xWidth, 13, Bir.YellowGing.NowDate, 11, False)
    End Sub


    Private Sub PicDisplay_MouseDown(sender As Object, e As MouseEventArgs) Handles PicDisplay.MouseDown
        intX = e.Location.X
        intY = e.Location.Y
        If Btn_Pan.Enabled Then
            lDraw = True
            PicDisplay.Cursor = System.Windows.Forms.Cursors.Hand
        Else
            lPressed = True
            PicDisplay.Cursor = System.Windows.Forms.Cursors.SizeAll
        End If

    End Sub

    Private Sub PicDisplay_MouseMove(sender As Object, e As MouseEventArgs) Handles PicDisplay.MouseMove
        If lPressed Then
            PicDisplay.Left = PicDisplay.Left + (e.Location.X - intX)
            PicDisplay.Top = PicDisplay.Top + (e.Location.Y - intY)
        End If
        If lDraw Then
            Using g As Graphics = Graphics.FromImage(PicDisplay.Image)
                g.DrawLine(RedPen, intX, intY, e.Location.X, e.Location.Y)
            End Using
            PicDisplay.Invalidate()
            intX = e.Location.X
            intY = e.Location.Y
        End If
    End Sub

    Private Sub PicDisplay_MouseUp(sender As Object, e As MouseEventArgs) Handles PicDisplay.MouseUp
        lPressed = False
        lDraw = False
        PicDisplay.Cursor = System.Windows.Forms.Cursors.Arrow
    End Sub

    Private Sub Btn_next_Click(sender As System.Object, e As System.EventArgs) Handles Btn_next.Click
        If PageNo = 5 Then
            Lbmsg.Text = "LastPage!"
            Lbmsg.Visible = True
        Else
            Lbmsg.Visible = False
            PageNo = PageNo + 1
            Label_page.Text = "P." & PageNo
        End If
        ShowPage(PageNo)
    End Sub

    Private Sub Btn_Prev_Click(sender As System.Object, e As System.EventArgs) Handles Btn_Prev.Click
        If PageNo = 0 Then
            Lbmsg.Text = "FirstPage!"
            Lbmsg.Visible = True
        Else
            Lbmsg.Visible = False
            PageNo = PageNo - 1
            Label_page.Text = "P." & PageNo
        End If
        ShowPage(PageNo)
    End Sub

    Private Sub Label_page_Click(sender As System.Object, e As System.EventArgs) Handles Label_page.Click
        ShowPage(PageNo)
    End Sub

    '------------------------------------------------------------------------------------------------------------
    'According to the value of PageNo, then Show (draw) page image
    Public Sub ShowPage(ByVal PageNumber As Integer)
        Select Case PageNumber
            Case 0
                GroupBox_VH.Visible = False
                PageAsVirtical = False
                RadioButton_H.Checked = True
                StartDrawP00()
                PicDisplay.Image.Save("c:\5ArtSave\working_Fate00.bmp", System.Drawing.Imaging.ImageFormat.Bmp)
            Case 1
                GroupBox_VH.Visible = True
                If PageAsVirtical Then
                    StartDrawP01_V()
                Else
                    StartDrawP01()
                End If
                PicDisplay.Image.Save("c:\5ArtSave\working_Fate01.bmp", System.Drawing.Imaging.ImageFormat.Bmp)
            Case 2
                GroupBox_VH.Visible = True
                If PageAsVirtical Then
                    StartDrawP01_V()
                Else
                    StartDrawP02()
                End If
                PicDisplay.Image.Save("c:\5ArtSave\working_Fate02.bmp", System.Drawing.Imaging.ImageFormat.Bmp)
            Case Else
        End Select
    End Sub
    '------------------------------------------------------------------------------------------------------------

    Dim Xmax As Integer = bmpWidth - 10
    Dim Ymax As Integer = bmpHeight - 14
    Private Sub StartDrawP01()
        PicDisplay.Image = BMP
        G = Graphics.FromImage(BMP)
        G.SmoothingMode = Drawing2D.SmoothingMode.HighQuality '可以改善鋸齒狀的問題
        G.Clear(Color.White) '白色為底色
        PicDisplay.Invalidate()

        Xmax = bmpWidth - 10
        Ymax = bmpHeight - 14

        '外邊的矩形邊框  (最後可不畫出)
        'Dim MyPictureBoder0 As New Rectangle(0, 0, Xmax, Ymax)
        'G.DrawRectangle(BluePen2, MyPictureBoder0) '畫矩形
        '---------------
        'ShowUpHoleSpace()  '固定上方留白,方便打孔 (最後可不畫出)
        ShowP01Title()
        ShowP01BasicData()
        ShowP01Ju4()
        ShowP01MingYung()
        ShowP01DaYung()
    End Sub

    Private Sub StartDrawP01_V()
        '畫布準備
        PicDisplay.Image = BMPv
        G = Graphics.FromImage(BMPv)
        G.SmoothingMode = Drawing2D.SmoothingMode.HighQuality '可以改善鋸齒狀的問題
        G.Clear(Color.White) '白色為底色
        PicDisplay.Invalidate()

        Xmax = bmpWidth_v - 5
        Ymax = bmpHeight_v - 5

        '外邊的矩形邊框  (最後可不畫出)
        Dim MyPictureBoder0 As New Rectangle(5, 5, Xmax, Ymax)
        G.DrawRectangle(BluePen2, MyPictureBoder0) '畫矩形
        '---------------
        ShowLeftHoleSpace()  '固定左方留白,方便打孔 (最後可不畫出)
        'ShowP01Title()

    End Sub

    Dim UpHoleSpaceH As Integer = 50
    Private Sub ShowUpHoleSpace()
        Dim UpHoleSpaceArea As New Rectangle(0, 0, Xmax, UpHoleSpaceH)
        G.DrawRectangle(BluePen2, UpHoleSpaceArea) '畫矩形
    End Sub

    Dim LeftHoleSpaceW As Integer = 50
    Private Sub ShowLeftHoleSpace()
        Dim LeftHoleSpaceArea As New Rectangle(0, 0, LeftHoleSpaceW, Ymax)
        G.DrawRectangle(BluePen2, LeftHoleSpaceArea) '畫矩形
    End Sub

    Dim TitleWidth As Integer = 50
    Dim PageHeight As Integer = Ymax - UpHoleSpaceH
    Private Sub ShowP01Title()
        Dim Xstr As Integer = Xmax - TitleWidth
        Dim Ystr As Integer = UpHoleSpaceH
        'MyBkBkText(Xstr, Ystr, TitleWidth, PageHeight, "", 24)  '劃出黑框
        MyBkBkText(Xstr, Ystr, TitleWidth, PageHeight / 2, "八字學 - 命造表", 24, False)
    End Sub

    Dim BasicDataWidth As Integer = 80
    Dim BasicDataHeight1 As Integer = 300
    Private Sub ShowP01BasicData()
        Dim TmpStr As String
        Dim Xstr As Integer = Xmax - TitleWidth - BasicDataWidth
        Dim Ystr As Integer = UpHoleSpaceH
        Dim CurrentY As Integer
        Dim CurrentM As Integer
        Dim Age As Integer
        TmpStr = Trim(Bir.Name)
        If Bir.Sex = "男" Then
            TmpStr = TmpStr & " 先生福造  " & Bir.SType & Bir.Sex
        Else
            TmpStr = TmpStr & " 女士雅造  " & Bir.SType & Bir.Sex
        End If
        MyBkBkText(Xstr, Ystr, BasicDataWidth, BasicDataHeight1, TmpStr, 18)
        MyBkBkText(Xstr, Ystr + BasicDataHeight1, BasicDataWidth, PageHeight - BasicDataHeight1, "", 18)
        TmpStr = "民國" & ToChineseNo(Bir.Solar.Y) & "年" & ToChineseNo(Bir.Solar.M) & "月" & ToChineseNo(Bir.Solar.D) & "日"
        TmpStr = TmpStr & Bir.Ju(4).Tsu & "時生"
        MyBkBkText(Xstr + BasicDataWidth / 2, Ystr + BasicDataHeight1, BasicDataWidth / 2, PageHeight - BasicDataHeight1, _
                   TmpStr, 17, False, False)
        CurrentY = DateTime.Today.Year() - 1911
        CurrentM = DateTime.Today.Month()
        Age = Bir.Age
        If Age < 0 Then
            TmpStr = "現年　歲。" & ToChNo(CurrentY) & "年" & ToChineseNo(CurrentM) & "月批"
        Else
            TmpStr = "現年" & ToChineseNo(Age) & "歲。" & ToChNo(CurrentY) & "年" & ToChineseNo(CurrentM) & "月批"
        End If
        MyBkBkText(Xstr, Ystr + BasicDataHeight1, BasicDataWidth / 2, PageHeight - BasicDataHeight1, _
                   TmpStr, 17, False, False)
    End Sub

    Dim JuNameFldWidth As Integer = 50
    Dim JuWidth As Integer = 70
    Dim JuFldHeight1 As Integer = 60
    Dim JuFldHeight2 As Integer = 80
    Dim JuFldHeight3 As Integer = 80
    Dim JuFldHeight4 As Integer = 80
    Dim JuFldHeight5 As Integer = 120
    Dim JuFldHeight6 As Integer = 80
    Dim JuFldHeight7 As Integer = PageHeight - JuFldHeight1 - JuFldHeight2 - JuFldHeight3 - JuFldHeight4 - JuFldHeight5 - JuFldHeight6
    Private Sub ShowP01Ju4()
        Dim Xstr As Integer = Xmax - TitleWidth - BasicDataWidth - JuNameFldWidth
        Dim Ystr As Integer = UpHoleSpaceH
        Dim TmpStr1 As String
        Dim TmpStr2 As String
        Dim TmpStr3 As String
        Dim TmpStr4 As String
        Dim Part7Width As Integer = JuWidth * 4 \ 6
        MyBkBkText(Xstr, Ystr, JuNameFldWidth, JuFldHeight1, "", 18)
        Ystr = Ystr + JuFldHeight1
        MyBkBkText(Xstr, Ystr, JuNameFldWidth, JuFldHeight2, "神輔", 18)
        Ystr = Ystr + JuFldHeight2
        MyBkBkText(Xstr, Ystr, JuNameFldWidth, JuFldHeight3, "干", 18)
        Ystr = Ystr + JuFldHeight3
        MyBkBkText(Xstr, Ystr, JuNameFldWidth, JuFldHeight4, "支", 18)
        Ystr = Ystr + JuFldHeight4
        MyBkBkText(Xstr, Ystr, JuNameFldWidth, JuFldHeight5, "藏干十神", 18)
        Ystr = Ystr + JuFldHeight5
        MyBkBkText(Xstr, Ystr, JuNameFldWidth, JuFldHeight6, "神輔", 18)
        Ystr = Ystr + JuFldHeight6
        MyBkBkText(Xstr, Ystr, JuNameFldWidth, JuFldHeight7, "命格記要", 18)
        '----------------------------
        Xstr = Xmax - TitleWidth - BasicDataWidth - JuNameFldWidth
        Ystr = UpHoleSpaceH
        MyBkBkText(Xstr - 1 * JuWidth, Ystr, JuWidth, JuFldHeight1, "年", 18)
        MyBkBkText(Xstr - 2 * JuWidth, Ystr, JuWidth, JuFldHeight1, "月", 18)
        MyBkBkText(Xstr - 3 * JuWidth, Ystr, JuWidth, JuFldHeight1, "日", 18)
        MyBkBkText(Xstr - 4 * JuWidth, Ystr, JuWidth, JuFldHeight1, "時", 18)
        Ystr = Ystr + JuFldHeight1
        MyBkBkText(Xstr - 1 * JuWidth, Ystr, JuWidth, JuFldHeight2, SF1word(Bir.SenFu.Ju(1).Gan), 18)
        MyBkBkText(Xstr - 2 * JuWidth, Ystr, JuWidth, JuFldHeight2, SF1word(Bir.SenFu.Ju(2).Gan), 18)
        MyBkBkText(Xstr - 3 * JuWidth, Ystr, JuWidth, JuFldHeight2, "日主", 18)
        MyBkBkText(Xstr - 4 * JuWidth, Ystr, JuWidth, JuFldHeight2, SF1word(Bir.SenFu.Ju(4).Gan), 18)
        Ystr = Ystr + JuFldHeight2
        MyBkBkText(Xstr - 1 * JuWidth, Ystr, JuWidth, JuFldHeight3, Bir.Ju(1).Gan, 18)
        MyBkBkText(Xstr - 2 * JuWidth, Ystr, JuWidth, JuFldHeight3, Bir.Ju(2).Gan, 18)
        MyBkBkText(Xstr - 3 * JuWidth, Ystr, JuWidth, JuFldHeight3, Bir.Ju(3).Gan, 18)
        MyBkBkText(Xstr - 4 * JuWidth, Ystr, JuWidth, JuFldHeight3, Bir.Ju(4).Gan, 18)
        Ystr = Ystr + JuFldHeight3
        MyBkBkText(Xstr - 1 * JuWidth, Ystr, JuWidth, JuFldHeight4, Bir.Ju(1).Tsu, 18)
        MyBkBkText(Xstr - 2 * JuWidth, Ystr, JuWidth, JuFldHeight4, Bir.Ju(2).Tsu, 18)
        MyBkBkText(Xstr - 3 * JuWidth, Ystr, JuWidth, JuFldHeight4, Bir.Ju(3).Tsu, 18)
        MyBkBkText(Xstr - 4 * JuWidth, Ystr, JuWidth, JuFldHeight4, Bir.Ju(4).Tsu, 18)
        Ystr = Ystr + JuFldHeight4
        TmpStr1 = SF1word(Bir.SenFu.Ju(1).HG1) & SF1word(Bir.SenFu.Ju(1).HG2) & SF1word(Bir.SenFu.Ju(1).HG3)
        TmpStr2 = SF1word(Bir.SenFu.Ju(2).HG1) & SF1word(Bir.SenFu.Ju(2).HG2) & SF1word(Bir.SenFu.Ju(2).HG3)
        TmpStr3 = SF1word(Bir.SenFu.Ju(3).HG1) & SF1word(Bir.SenFu.Ju(3).HG2) & SF1word(Bir.SenFu.Ju(3).HG3)
        TmpStr4 = SF1word(Bir.SenFu.Ju(4).HG1) & SF1word(Bir.SenFu.Ju(4).HG2) & SF1word(Bir.SenFu.Ju(4).HG3)
        MyBkBkText(Xstr - 1 * JuWidth, Ystr, JuWidth, JuFldHeight5, TmpStr1, 18)
        MyBkBkText(Xstr - 2 * JuWidth, Ystr, JuWidth, JuFldHeight5, TmpStr2, 18)
        MyBkBkText(Xstr - 3 * JuWidth, Ystr, JuWidth, JuFldHeight5, TmpStr3, 18)
        MyBkBkText(Xstr - 4 * JuWidth, Ystr, JuWidth, JuFldHeight5, TmpStr4, 18)
        Ystr = Ystr + JuFldHeight5
        MyBkBkText(Xstr - 1 * JuWidth, Ystr, JuWidth, JuFldHeight6, SF1word(Bir.SenFu.Ju(1).Tsu), 18)
        MyBkBkText(Xstr - 2 * JuWidth, Ystr, JuWidth, JuFldHeight6, SF1word(Bir.SenFu.Ju(2).Tsu), 18)
        MyBkBkText(Xstr - 3 * JuWidth, Ystr, JuWidth, JuFldHeight6, SF1word(Bir.SenFu.Ju(3).Tsu), 18)
        MyBkBkText(Xstr - 4 * JuWidth, Ystr, JuWidth, JuFldHeight6, SF1word(Bir.SenFu.Ju(4).Tsu), 18)
        '---Part7
        Ystr = Ystr + JuFldHeight6
        MyBkBkText(Xstr - 4 * JuWidth, Ystr, JuWidth * 4, JuFldHeight7, "", 18)
        TmpStr1 = "格局-" & Bir.Main8wordKan
        MyBkBkText(Xstr - 1 * Part7Width, Ystr, Part7Width, JuFldHeight7, TmpStr1, 18, False, False)
        TmpStr1 = "用神-" & Bir.UsingGodMain & "-" & Bir.UsingGodSub
        MyBkBkText(Xstr - 2 * Part7Width, Ystr, Part7Width, JuFldHeight7, TmpStr1, 18, False, False)
        TmpStr1 = Bir.DayStrongWeak
        MyBkBkText(Xstr - 3 * Part7Width, Ystr, Part7Width, JuFldHeight7, TmpStr1, 18, False, False)
        TmpStr1 = "空亡-" & Bir.YearEmpty & Bir.DayEmpty
        MyBkBkText(Xstr - 4 * Part7Width, Ystr, Part7Width, JuFldHeight7, TmpStr1, 18, False, False)
    End Sub

    Dim Tsu12W = 50
    Dim Tsu12Width = Tsu12W * 4
    Dim MingWidth = 65
    Dim YungWidth = Tsu12Width - MingWidth
    Private Sub ShowP01MingYung()
        Dim Xstr As Integer = Xmax - TitleWidth - BasicDataWidth - JuNameFldWidth - (4 * JuWidth) - MingWidth
        Dim Ystr As Integer = UpHoleSpaceH
        Dim Tmpstr As String
        Dim SexType As String
        MyBkBkText(Xstr, Ystr, MingWidth, JuFldHeight1, "", 18)
        MyBkBkText(Xstr, Ystr + JuFldHeight1 / 4, MingWidth, JuFldHeight1 / 2, "宮命", 18, False)
        Ystr = Ystr + JuFldHeight1
        MyBkBkText(Xstr, Ystr, MingWidth, JuFldHeight2 + JuFldHeight3 + JuFldHeight4 + JuFldHeight5 + JuFldHeight6, _
                   Bir.Kon12(1), 18)
        Xstr = Xmax - TitleWidth - BasicDataWidth - JuNameFldWidth - (4 * JuWidth) - MingWidth - YungWidth
        Ystr = UpHoleSpaceH
        MyBkBkText(Xstr, Ystr, YungWidth, JuFldHeight1, "", 18) '先畫外框
        MyBkBkText(Xstr, Ystr + JuFldHeight1 / 4, YungWidth, JuFldHeight1 / 2, "要擇交運", 18, False)
        Ystr = Ystr + JuFldHeight1
        MyBkBkText(Xstr, Ystr, YungWidth, JuFldHeight2 + JuFldHeight3 + JuFldHeight4 + JuFldHeight5 + JuFldHeight6, "", 18)
        Tmpstr = "每逢" & Bir.YuanChange_YG & "年" & ToChineseNo(Bir.YuanChange_M) & "月" & Bir.YuanChange_JC & "後" _
            & ToChineseNo(Bir.YuanChange_JCday) & "天交脫"
        MyBkBkText(Xstr + YungWidth * 3 / 4, Ystr, _
                   YungWidth / 4, JuFldHeight2 + JuFldHeight3 + JuFldHeight4 + JuFldHeight5 + JuFldHeight6, _
                   Tmpstr, 18, False, False)
        SexType = Bir.SType & Bir.Sex
        If (SexType = "陽男") Or (SexType = "陰女") Then
            Tmpstr = "順數至下一節計"
        Else
            Tmpstr = "逆數至下一節計"
        End If
        Tmpstr = Tmpstr & ToChineseNo(Bir.Ja2Born_D) & "天" & ToChineseNo(Bir.Ja2Born_H) & "時辰"
        MyBkBkText(Xstr + YungWidth * 2 / 4, Ystr, _
                   YungWidth / 4, JuFldHeight2 + JuFldHeight3 + JuFldHeight4 + JuFldHeight5 + JuFldHeight6, _
                   Tmpstr, 18, False, False)
        Tmpstr = "出生後" & ToChineseNo(Bir.YuanStart.Y) & "年" & ToChineseNo(Bir.YuanStart.M) & "月又" _
            & ToChineseNo(Bir.YuanStart.D) & "天"
        MyBkBkText(Xstr + YungWidth * 1 / 4, Ystr, _
                   YungWidth / 4, JuFldHeight2 + JuFldHeight3 + JuFldHeight4 + JuFldHeight5 + JuFldHeight6, _
                   Tmpstr, 18, False, False)
        Tmpstr = Bir.YuanStart_YearGT & "年開始行運"
        MyBkBkText(Xstr, Ystr, YungWidth / 4, JuFldHeight2 + JuFldHeight3 + JuFldHeight4 + JuFldHeight5 + JuFldHeight6, _
                   Tmpstr, 18, False, False)
        '畫十二支辰-----------------------------------------------------------------------------------------
        Xstr = Xmax - TitleWidth - BasicDataWidth - JuNameFldWidth - (4 * JuWidth) - MingWidth - YungWidth
        Ystr = UpHoleSpaceH + JuFldHeight1 + JuFldHeight2 + JuFldHeight3 + JuFldHeight4 + JuFldHeight5 + JuFldHeight6
        'MyBkBkText(Xstr, Ystr, Tsu12Width, JuFldHeight7, "", 18)
        MyBkBkText(Xstr + (0 * Tsu12W), Ystr, Tsu12W, JuFldHeight7 / 4, "", 18)
        MyBkBkText(Xstr + (1 * Tsu12W), Ystr, Tsu12W, JuFldHeight7 / 4, "", 18)
        MyBkBkText(Xstr + (2 * Tsu12W), Ystr, Tsu12W, JuFldHeight7 / 4, "", 18)
        MyBkBkText(Xstr + (3 * Tsu12W), Ystr, Tsu12W, JuFldHeight7 / 4, "", 18)
        MyBkBkText(Xstr + (0 * Tsu12W), Ystr + (JuFldHeight7 / 4), Tsu12W, JuFldHeight7 / 4, "", 18)
        MyBkBkText(Xstr + (3 * Tsu12W), Ystr + (JuFldHeight7 / 4), Tsu12W, JuFldHeight7 / 4, "", 18)
        MyBkBkText(Xstr + (0 * Tsu12W), Ystr + (2 * JuFldHeight7 / 4), Tsu12W, JuFldHeight7 / 4, "", 18)
        MyBkBkText(Xstr + (3 * Tsu12W), Ystr + (2 * JuFldHeight7 / 4), Tsu12W, JuFldHeight7 / 4, "", 18)
        MyBkBkText(Xstr + (0 * Tsu12W), Ystr + (3 * JuFldHeight7 / 4), Tsu12W, JuFldHeight7 / 4, "", 18)
        MyBkBkText(Xstr + (1 * Tsu12W), Ystr + (3 * JuFldHeight7 / 4), Tsu12W, JuFldHeight7 / 4, "", 18)
        MyBkBkText(Xstr + (2 * Tsu12W), Ystr + (3 * JuFldHeight7 / 4), Tsu12W, JuFldHeight7 / 4, "", 18)
        MyBkBkText(Xstr + (3 * Tsu12W), Ystr + (3 * JuFldHeight7 / 4), Tsu12W, JuFldHeight7 / 4, "", 18)
        '---
        MyBkBkText(Xstr + (0 * Tsu12W) + (Tsu12W / 2), Ystr + (Tsu12W / 2), Tsu12W / 2, JuFldHeight7 / 8, "巳", 16, False)
        MyBkBkText(Xstr + (1 * Tsu12W) + (Tsu12W / 2), Ystr + (Tsu12W / 2), Tsu12W / 2, JuFldHeight7 / 8, "午", 16, False)
        MyBkBkText(Xstr + (2 * Tsu12W) + (Tsu12W / 2), Ystr + (Tsu12W / 2), Tsu12W / 2, JuFldHeight7 / 8, "未", 16, False)
        MyBkBkText(Xstr + (3 * Tsu12W) + (Tsu12W / 2), Ystr + (Tsu12W / 2), Tsu12W / 2, JuFldHeight7 / 8, "申", 16, False)
        MyBkBkText(Xstr + (0 * Tsu12W) + (Tsu12W / 2), Ystr + (Tsu12W / 2) + (JuFldHeight7 / 4), Tsu12W / 2, JuFldHeight7 / 8, "辰", 16, False)
        MyBkBkText(Xstr + (3 * Tsu12W) + (Tsu12W / 2), Ystr + (Tsu12W / 2) + (JuFldHeight7 / 4), Tsu12W / 2, JuFldHeight7 / 8, "酉", 16, False)
        MyBkBkText(Xstr + (0 * Tsu12W) + (Tsu12W / 2), Ystr + (Tsu12W / 2) + (2 * JuFldHeight7 / 4), Tsu12W / 2, JuFldHeight7 / 8, "卯", 16, False)
        MyBkBkText(Xstr + (3 * Tsu12W) + (Tsu12W / 2), Ystr + (Tsu12W / 2) + (2 * JuFldHeight7 / 4), Tsu12W / 2, JuFldHeight7 / 8, "戌", 16, False)
        MyBkBkText(Xstr + (0 * Tsu12W) + (Tsu12W / 2), Ystr + (Tsu12W / 2) + (3 * JuFldHeight7 / 4), Tsu12W / 2, JuFldHeight7 / 8, "寅", 16, False)
        MyBkBkText(Xstr + (1 * Tsu12W) + (Tsu12W / 2), Ystr + (Tsu12W / 2) + (3 * JuFldHeight7 / 4), Tsu12W / 2, JuFldHeight7 / 8, "丑", 16, False)
        MyBkBkText(Xstr + (2 * Tsu12W) + (Tsu12W / 2), Ystr + (Tsu12W / 2) + (3 * JuFldHeight7 / 4), Tsu12W / 2, JuFldHeight7 / 8, "子", 16, False)
        MyBkBkText(Xstr + (3 * Tsu12W) + (Tsu12W / 2), Ystr + (Tsu12W / 2) + (3 * JuFldHeight7 / 4), Tsu12W / 2, JuFldHeight7 / 8, "亥", 16, False)
    End Sub

    Dim DaYungWidth As Integer = 40
    Dim DaYungHeight0 As Integer = 32 '大運Title
    Dim DaYungHeight1 As Integer = 32 '大運歲數
    Dim DaYungHeight2 As Integer = 50 '大運干支
    Dim DaYungHeight3 As Integer = 10 'space
    Dim DaYungHeight4 As Integer = 30 '小運title
    Dim DaYungHeight5 As Integer = 24 '小運歲數
    Dim DaYungHeight6 As Integer = 44 '干支
    Dim DaYungHeight7 As Integer = 0 'space
    Dim DaYungHeight8 As Integer = 30 '流年title
    Dim DaYungHeight9 As Integer = 24 '流年歲數
    Dim DaYungHeight10 As Integer = 44 '干支
    Private Sub ShowP01DaYung()
        Dim XWidth As Integer = Xmax - TitleWidth - BasicDataWidth - JuNameFldWidth - (4 * JuWidth) - MingWidth - YungWidth
        Dim Xstr As Integer = 10
        Dim Ystr As Integer = UpHoleSpaceH
        Dim strTMP As String
        Dim wGan As String  '記憶流年的干支
        Dim wTsu As String
        Dim sGan As String  '記憶小運的干支
        Dim sTsu As String
        Dim SexType As String
        MyBkBkText(Xstr, Ystr, DaYungWidth * 10, DaYungHeight0, "程　運　／　運　大", 16)
        '幾歲
        Ystr = Ystr + DaYungHeight0
        For i As Integer = 1 To 10
            MyBkBkText(Xstr + (i - 1) * DaYungWidth, Ystr, DaYungWidth, DaYungHeight1, "", 16)
            MyTextH(Xstr + (i - 1) * DaYungWidth, Ystr, DaYungWidth, DaYungHeight1, Bir.BigYuan(11 - i).YearsOld, 16, False)
        Next 'For loop i
        '大運干支
        Ystr = Ystr + DaYungHeight1
        For i = 1 To 10
            strTMP = Bir.BigYuan(11 - i).Gan & Bir.BigYuan(11 - i).Tsu
            MyBkBkText(Xstr + (i - 1) * DaYungWidth, Ystr, DaYungWidth, DaYungHeight2, strTMP, 16)
        Next 'For loop i
        Ystr = Ystr + DaYungHeight2 + DaYungHeight3
        MyBkBkText(Xstr, Ystr, DaYungWidth * 10, DaYungHeight4, "運　小", 16)
        '幾歲
        Ystr = Ystr + DaYungHeight4
        For i = 1 To 10
            MyBkBkText(Xstr + (10 - i) * DaYungWidth, Ystr, DaYungWidth, DaYungHeight5, "", 14)
            If i < Bir.BigYuan(1).YearsOld Then
                MyTextH(Xstr + (10 - i) * DaYungWidth, Ystr, DaYungWidth, DaYungHeight5, i, 14, False)
            End If
        Next 'For loop i
        '干支
        Ystr = Ystr + DaYungHeight5
        SexType = Bir.SType & Bir.Sex
        wGan = Bir.Ju(1).Gan  '記憶流年用的干支
        wTsu = Bir.Ju(1).Tsu
        sGan = Bir.Ju(4).Gan  '記憶小運用的干支 (0歲)
        sTsu = Bir.Ju(4).Tsu
        For i = 1 To 10
            MyBkBkText(Xstr + (10 - i) * DaYungWidth, Ystr, DaYungWidth, DaYungHeight6, "", 14)
            If i < Bir.BigYuan(1).YearsOld Then
                If (SexType = "陽男") Or (SexType = "陰女") Then
                    sGan = NextGan(sGan)
                    sTsu = NextTsu(sTsu)
                Else
                    sGan = PrevGan(sGan)
                    sTsu = PrevTsu(sTsu)
                End If
                strTMP = sGan & sTsu
                MyBkBkText(Xstr + (10 - i) * DaYungWidth, Ystr, DaYungWidth, DaYungHeight6, strTMP, 14, False)
                wGan = NextGan(wGan) '記憶流年用的干支
                wTsu = NextTsu(wTsu)
            End If
        Next 'For loop i
        Ystr = Ystr + DaYungHeight6 + DaYungHeight7
        MyBkBkText(Xstr, Ystr, DaYungWidth * 10, DaYungHeight8, "年 流", 16)
        '幾歲
        Ystr = Ystr + DaYungHeight8
        For i As Integer = 1 To 10
            MyBkBkText(Xstr + (i - 1) * DaYungWidth, Ystr, DaYungWidth, DaYungHeight9, "", 14)
            MyTextH(Xstr + (i - 1) * DaYungWidth, Ystr, DaYungWidth, DaYungHeight9, Bir.BigYuan(11 - i).YearsOld, 14, False)
        Next 'For loop i
        '干支
        Ystr = Ystr + DaYungHeight9
        For i = 1 To 10
            For j As Integer = 1 To 10
                MyBkBkText(Xstr + (10 - i) * DaYungWidth, Ystr + (j - 1) * DaYungHeight10, DaYungWidth, DaYungHeight10, "", 14)
                strTMP = wGan & wTsu
                MyBkBkText(Xstr + (10 - i) * DaYungWidth, Ystr + (j - 1) * DaYungHeight10, DaYungWidth, DaYungHeight10, strTMP, 14, False)
                wGan = NextGan(wGan)
                wTsu = NextTsu(wTsu)
            Next 'for loop j
        Next 'For loop i
    End Sub
    '------------------------------------------------------------
    Private Sub StartDrawP02()
        PicDisplay.Image = BMP
        G = Graphics.FromImage(BMP)
        G.SmoothingMode = Drawing2D.SmoothingMode.HighQuality '可以改善鋸齒狀的問題
        G.Clear(Color.White) '白色為底色
        PicDisplay.Invalidate()

        Xmax = bmpWidth - 10
        Ymax = bmpHeight - 14

        '外邊的矩形邊框  (最後可不畫出)
        'Dim MyPictureBoder0 As New Rectangle(0, 0, Xmax, Ymax)
        'G.DrawRectangle(BluePen2, MyPictureBoder0) '畫矩形
        '---------------
        'ShowUpHoleSpace()  '固定上方留白,方便打孔 (最後可不畫出)
        ShowP02BigYuan()
        ShowP02TenYear()
    End Sub

    Dim BigYunWidth = 125
    Dim BigYuan_Current As Integer = 0
    Private Sub ShowP02BigYuan()
        Dim Xstr As Integer = Xmax - BigYunWidth
        Dim Ystr As Integer = UpHoleSpaceH
        Dim FS As Integer = 16  'font size
        Dim DisplayString0 As String = ""
        Dim DisplayString0A As String = ""
        Dim DisplayString1 As String = ""
        Dim DisplayString2 As String = ""
        Dim DisplayString3 As String = ""
        Dim DisplayString4 As String = "一二三四五六七八九十一二三四五六七八九十一二三四五六七八九十"
        Dim DsiLine As Integer = 4
        Dim WordPerLine As Integer = 33
        Dim StartIndex As Integer = 0

        MyBkBkText(Xstr, Ystr, BigYunWidth, PageHeight, "", FS)  '劃出黑框

        DisplayString0 = Trim(Bir.Name)
        If Bir.Sex = "男" Then
            DisplayString0 = DisplayString0 & " 先生"
        Else
            DisplayString0 = DisplayString0 & " 女士"
        End If

        '取大運
        For i As Integer = 1 To 10
            If Bir.Age > Bir.BigYuan(i).YearsOld Then
                BigYuan_Current = i
            End If
        Next 'For loop i
        DisplayString0A = "現年" & ToChineseNo(Bir.Age) & "歲，日主為 " & Bir.Ju(3).Gan & Bir.Ju(3).Tsu & _
        " ，行 " & Bir.BigYuan(BigYuan_Current).Gan & Bir.BigYuan(BigYuan_Current).Tsu & _
        "（" & Bir.BigYuan(BigYuan_Current).SenFu & "）運程。"

        '大運批示
        DisplayString1 = FindYearsNote(FindSixSen(Bir.Ju(3).Gan, Bir.BigYuan(BigYuan_Current).Gan), _
                                       FindSixSen(Bir.Ju(3).Gan, Bir.BigYuan(BigYuan_Current).Tsu), DisplayString2)
        If DisplayString1 <> "" Then
            '去掉傳回DisplayString2中，前面的詩句
            StartIndex = InStr(DisplayString2, ")。")
            DisplayString2 = Mid(DisplayString2, StartIndex + 2, Len(DisplayString2) - StartIndex - 2)
            '先整合兩個字串,變成DisplayString3
            DisplayString3 = DisplayString1 & DisplayString2 & "，細部流年批示如下"
            '切割成字串,符合顯示長度
            If Len(DisplayString3) < (WordPerLine + 1) Then
                DisplayString1 = DisplayString3
                DisplayString2 = ""
            Else
                DisplayString1 = Mid(DisplayString3, 1, WordPerLine)
                DisplayString3 = Mid(DisplayString3, WordPerLine + 1, Len(DisplayString3) - WordPerLine)
                If Len(DisplayString3) < (WordPerLine + 1) Then
                    DisplayString2 = DisplayString3
                    DisplayString3 = ""
                Else
                    DisplayString2 = Mid(DisplayString3, 1, WordPerLine)
                    DisplayString3 = Mid(DisplayString3, WordPerLine + 1, Len(DisplayString3) - WordPerLine)
                    If Len(DisplayString3) > WordPerLine Then
                        FrmFate.UserMsg("顯示大運批示時有過多資訊錯誤!... ")
                    End If
                End If
            End If
        End If
        If DisplayString3 <> "" Then
            Xstr = Xmax - (BigYunWidth / DsiLine)
            MyBkBkText(Xstr, Ystr, BigYunWidth / DsiLine, PageHeight / 4, DisplayString0, FS, False, False)
            MyBkBkText(Xstr, Ystr + PageHeight / 4, BigYunWidth / DsiLine, PageHeight * 3 / 4, DisplayString0A, FS, False, False)
            Xstr = Xstr - (BigYunWidth / DsiLine)
            MyBkBkText(Xstr, Ystr, BigYunWidth / DsiLine, PageHeight, DisplayString1, FS, False, False)
            Xstr = Xstr - (BigYunWidth / DsiLine)
            MyBkBkText(Xstr, Ystr, BigYunWidth / DsiLine, PageHeight, DisplayString2, FS, False, False)
            Xstr = Xstr - (BigYunWidth / DsiLine)
            MyBkBkText(Xstr, Ystr, BigYunWidth / DsiLine, PageHeight, DisplayString3, FS, False, False)
        Else
            Xstr = Xmax - (BigYunWidth / (DsiLine - 1))
            MyBkBkText(Xstr, Ystr, BigYunWidth / (DsiLine - 1), PageHeight / 4, DisplayString0, FS, False, False)
            MyBkBkText(Xstr, Ystr + PageHeight / 4, BigYunWidth / (DsiLine - 1), PageHeight * 3 / 4, DisplayString0A, FS, False, False)
            Xstr = Xstr - (BigYunWidth / (DsiLine - 1))
            MyBkBkText(Xstr, Ystr, BigYunWidth / (DsiLine - 1), PageHeight, DisplayString1, FS, False, False)
            Xstr = Xstr - (BigYunWidth / (DsiLine - 1))
            MyBkBkText(Xstr, Ystr, BigYunWidth / (DsiLine - 1), PageHeight, DisplayString2, FS, False, False)
        End If
    End Sub

    Dim TableHeadWidth As Integer = 45
    Dim TenYearWidth As Integer = 900
    Dim Fild_1_height As Integer = 30 '大運十神
    Dim Fild_2_height As Integer = 30 '十年歲數
    Dim Fild_3_height As Integer = 30 '流年天干十神
    Dim Fild_4_height As Integer = 30 '流年天干
    Dim Fild_5_height As Integer = 30 '流年地支
    Dim Fild_6_height As Integer = 30 '流年地支十神
    Dim Fild_7_height As Integer = PageHeight - Fild_1_height - Fild_2_height - Fild_3_height - Fild_4_height - Fild_5_height - Fild_6_height
    Private Sub ShowP02TenYear()
        Dim Xstr As Integer = Xmax - BigYunWidth - TableHeadWidth
        Dim Ystr As Integer = UpHoleSpaceH
        Dim FS As Integer = 16   ' font size (Table)
        Dim FS2 As Integer = 14  'font size (歲數) 
        Dim FS3 As Integer = 14  'font size (流年批示) 
        Dim WordPerLine As Integer = 28
        Dim TmpStr As String
        Dim BigYuan_StartAge As Integer
        Dim All_Gan As String = "甲乙丙丁戊己庚辛壬癸"
        Dim All_Tsu As String = "子丑寅卯辰巳午未申酉戌亥"
        Dim Yuan_Index1, Yuan_Index2 As Integer
        Dim Years_Gan As String = ""
        Dim Years_Tsu As String = ""
        Dim Years_Gan10God As String = ""   '兩個字的十神名稱
        Dim Years_Tsu10God As String = ""
        Dim ThisYear_Gan As String = ""
        Dim ThisYear_Tsu As String = ""
        Dim ThisYear_Gan10God As String = ""   '兩個字的十神名稱
        Dim ThisYear_Tsu10God As String = ""
        Dim Age As Integer = 0
        Dim DisplayString0 As String = ""
        Dim DisplayString1 As String = ""
        Dim DisplayString2 As String = ""
        Dim DisplayString3 As String = ""
        Dim DisplayString4 As String = "一二三四五六七八九十一二三四五六七八九十一二三四五六七"
        '先看外框
        MyBkBkText(Xstr, Ystr, TableHeadWidth, PageHeight, "", 24)  '劃出黑框 (table head)
        Xstr = Xstr - TenYearWidth
        MyBkBkText(Xstr, Ystr, TenYearWidth, PageHeight, "", 24)  '劃出黑框 (table head)

        '取大運起始歲數, 以及干支
        BigYuan_StartAge = Bir.BigYuan(BigYuan_Current).YearsOld
        For i As Integer = 1 To 10
            For j As Integer = 1 To 12
                If Bir.Y60state(i, j) = BigYuan_StartAge Then
                    Yuan_Index1 = i : Yuan_Index2 = j
                End If
            Next
        Next
        Years_Gan = Mid(All_Gan, Yuan_Index1, 1)
        Years_Tsu = Mid(All_Tsu, Yuan_Index2, 1)

        '列印十年流年
        Xstr = Xmax - BigYunWidth - TableHeadWidth - (TenYearWidth / 10)
        Age = BigYuan_StartAge
        For i = 1 To 10
            '歲數
            Ystr = UpHoleSpaceH + Fild_1_height
            MyBkBkTextH(Xstr, Ystr, (TenYearWidth / 10), Fild_2_height, Age, FS2)
            '流年天干十神
            Ystr = Ystr + Fild_2_height
            Years_Gan10God = FindSixSen(Bir.Ju(3).Gan, Years_Gan)
            MyBkBkTextH(Xstr, Ystr, (TenYearWidth / 10), Fild_3_height, SF1word(Years_Gan10God), FS)
            '流年天干
            Ystr = Ystr + Fild_3_height
            MyBkBkTextH(Xstr, Ystr, (TenYearWidth / 10), Fild_4_height, Years_Gan, FS)
            '流年地支
            Ystr = Ystr + Fild_4_height
            MyBkBkTextH(Xstr, Ystr, (TenYearWidth / 10), Fild_5_height, Years_Tsu, FS)
            '流年地支十神
            Ystr = Ystr + Fild_5_height
            Years_Tsu10God = FindSixSen(Bir.Ju(3).Gan, Years_Tsu)
            MyBkBkTextH(Xstr, Ystr, (TenYearWidth / 10), Fild_6_height, SF1word(Years_Tsu10God), FS)
            '流年批示
            Ystr = Ystr + Fild_6_height
            DisplayString1 = FindYearsNote(FindSixSen(Bir.Ju(3).Gan, Years_Gan), FindSixSen(Bir.Ju(3).Gan, Years_Tsu), _
                                           DisplayString2)
            DisplayString3 = DisplayString1 & DisplayString2
            If DisplayString3 <> "" Then
                '切割成字串,符合顯示長度
                If Len(DisplayString3) < (WordPerLine + 1) Then
                    DisplayString0 = DisplayString3
                    DisplayString1 = ""
                Else
                    DisplayString0 = Mid(DisplayString3, 1, WordPerLine)
                    DisplayString3 = Mid(DisplayString3, WordPerLine + 1, Len(DisplayString3) - WordPerLine)
                    If Len(DisplayString3) < (WordPerLine + 1) Then
                        DisplayString1 = DisplayString3
                        DisplayString2 = ""
                        DisplayString3 = ""
                    Else
                        DisplayString1 = Mid(DisplayString3, 1, WordPerLine)
                        DisplayString3 = Mid(DisplayString3, WordPerLine + 1, Len(DisplayString3) - WordPerLine)
                        If Len(DisplayString3) < (WordPerLine + 1) Then
                            DisplayString2 = DisplayString3
                            DisplayString3 = ""
                        Else
                            DisplayString2 = Mid(DisplayString3, 1, WordPerLine)
                            DisplayString3 = Mid(DisplayString3, WordPerLine + 1, Len(DisplayString3) - WordPerLine)
                            If Len(DisplayString3) > WordPerLine Then
                                FrmFate.UserMsg("顯示流年批示時有過多資訊錯誤!... ")
                            End If '---3
                        End If '---2
                    End If '=---1
                End If   '----0
            End If '----- is not empty string
            MyBkBkText(Xstr, Ystr, (TenYearWidth / 10), Fild_7_height, "", FS3)
            If DisplayString3 <> "" Then
                MyBkBkText(Xstr + 3 * (TenYearWidth / 40), Ystr, (TenYearWidth / 40), Fild_7_height, DisplayString0, FS3, False, False)
                MyBkBkText(Xstr + 2 * (TenYearWidth / 40), Ystr, (TenYearWidth / 40), Fild_7_height, DisplayString1, FS3, False, False)
                MyBkBkText(Xstr + (TenYearWidth / 40), Ystr, (TenYearWidth / 40), Fild_7_height, DisplayString2, FS3, False, False)
                MyBkBkText(Xstr, Ystr, (TenYearWidth / 40), Fild_7_height, DisplayString3, FS3, False, False)
            Else
                If DisplayString2 <> "" Then
                    MyBkBkText(Xstr + 2 * (TenYearWidth / 30), Ystr, (TenYearWidth / 40), Fild_7_height, DisplayString0, FS3, False, False)
                    MyBkBkText(Xstr + 1 * (TenYearWidth / 30), Ystr, (TenYearWidth / 40), Fild_7_height, DisplayString1, FS3, False, False)
                    MyBkBkText(Xstr, Ystr, (TenYearWidth / 30), Fild_7_height, DisplayString2, FS3, False, False)
                Else
                    MyBkBkText(Xstr + 1 * (TenYearWidth / 20), Ystr, (TenYearWidth / 40), Fild_7_height, DisplayString0, FS3, False, False)
                    MyBkBkText(Xstr, Ystr, (TenYearWidth / 20), Fild_7_height, DisplayString1, FS3, False, False)
                End If
            End If
            '-----
            If Age = Bir.Age Then
                ThisYear_Gan = Years_Gan
                ThisYear_Tsu = Years_Tsu
                ThisYear_Gan10God = Years_Gan10God
                ThisYear_Tsu10God = Years_Tsu10God
            End If
            '-----
            Xstr = Xstr - (TenYearWidth / 10)
            Years_Gan = NextGan(Years_Gan)
            Years_Tsu = NextTsu(Years_Tsu)
            Age = Age + 1
        Next  '======================

        '寫Table head
        Xstr = Xmax - BigYunWidth - TableHeadWidth
        Ystr = UpHoleSpaceH
        MyBkBkText(Xstr, Ystr, TableHeadWidth, Fild_1_height + Fild_2_height, "  歲", FS)
        MyBkBkTextH(Xstr, Ystr, TableHeadWidth, Fild_1_height, Bir.Age, FS - 2, False)
        Ystr = Ystr + Fild_1_height + Fild_2_height
        MyBkBkText(Xstr, Ystr, TableHeadWidth, Fild_3_height, SF1word(FindSixSen(Bir.Ju(3).Gan, ThisYear_Gan)), FS)
        Ystr = Ystr + Fild_3_height
        MyBkBkText(Xstr, Ystr, TableHeadWidth, Fild_4_height, ThisYear_Gan, FS)
        Ystr = Ystr + Fild_4_height
        MyBkBkText(Xstr, Ystr, TableHeadWidth, Fild_5_height, ThisYear_Tsu, FS)
        Ystr = Ystr + Fild_5_height
        MyBkBkText(Xstr, Ystr, TableHeadWidth, Fild_6_height, SF1word(FindSixSen(Bir.Ju(3).Gan, ThisYear_Tsu)), FS)
        Ystr = Ystr + Fild_6_height
        MyBkBkText(Xstr, Ystr, TableHeadWidth, Fild_7_height, "流　　年　　批　　示", FS)
        Xstr = Xmax - BigYunWidth - TableHeadWidth - (TenYearWidth / 2)
        Ystr = UpHoleSpaceH
        MyBkBkText(Xstr, Ystr, TenYearWidth / 2, Fild_1_height, Mid(Bir.BigYuan(BigYuan_Current).SenFu, 1, 1), FS, True, False)
        Xstr = Xmax - BigYunWidth - TableHeadWidth - TenYearWidth
        MyBkBkText(Xstr, Ystr, TenYearWidth / 2, Fild_1_height, Mid(Bir.BigYuan(BigYuan_Current).SenFu, 2, 1), FS, True, False)

    End Sub

    Private Sub RadioButton_H_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles RadioButton_H.CheckedChanged
        VH_check()
    End Sub

    Private Sub RadioButton_V_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles RadioButton_V.CheckedChanged
        VH_check()
    End Sub

    Dim PageAsVirtical As Boolean = False
    Private Sub VH_check()
        '確保性別欄位的選項與字串一致
        If RadioButton_V.Checked Then
            PageAsVirtical = True
        ElseIf RadioButton_H.Checked Then
            PageAsVirtical = False
        End If
    End Sub

End Class