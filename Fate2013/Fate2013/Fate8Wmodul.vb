Module Fate8Wmodul
    '---------------------------------
    ' Ajack 2013 remake Fate program 
    ' Using VB express 2010 (free)
    '---------------------------------
    Public Structure GThG
        Public Gan As String
        Public Tsu As String
        Public hdG As String
        Public Sub clear()
            Gan = ""
            Tsu = ""
            hdG = ""
        End Sub
    End Structure

    Public Structure YearMonthDay
        Public Y As Integer
        Public M As Byte
        Public D As Byte
        Public Sub clear()
            Y = 0
            M = 0
            D = 0
        End Sub
    End Structure

    Public Structure PlaceLL
        Public LatDeg As Short
        Public LatMin As Byte
        Public LatSec As Byte
        Public LonDeg As Short
        Public LonMin As Byte
        Public LonSec As Byte
        Public Sub clear()
            'default 台灣台北市
            LatDeg = 25
            LatMin = 3
            LatSec = 23
            LonDeg = 121
            LonMin = 30
            LonSec = 15
        End Sub
    End Structure

    Public Structure JaChiDateTime
        Public Name As String
        Public YMD As YearMonthDay
        Public Hour As String
        Public Min As String
        Public Sub clear()
            Name = ""
            YMD.clear()
        End Sub
    End Structure

    Public Structure SenFu5Sing   '神輔與五行的各柱資料結構
        Public Gan As String
        Public Tsu As String
        Public HG1 As String
        Public HG2 As String
        Public HG3 As String
        Public Sub clear()
            Gan = ""
            Tsu = ""
            HG1 = ""
            HG2 = ""
            HG3 = ""
        End Sub
    End Structure

    Public Structure AllSenFu5Sing  '神輔與五行的八柱資料結構
        Public Ju As SenFu5Sing()
    End Structure

    Public Structure GwaInfo   '各柱的六十四卦資料
        Public UGwa As String
        Public DGwa As String
        Public UGwaNo As Integer
        Public DGwaNo As Integer
        Public GwaName As String
        Public GwaName2 As String
        Public Sub clear()
            UGwa = ""
            UGwaNo = 1
            DGwa = ""
            DGwaNo = 1
            GwaName = ""
            GwaName2 = ""
        End Sub
    End Structure

    Public Structure AllGwaInfo  '六十四卦的八柱資料結構
        Public Ju As GwaInfo()
    End Structure

    Public Structure All8Juinfo  '八柱資料結構
        Public Ju As String()
    End Structure

    Public Structure All8Ju12Koninfo  '八柱與十二宮資料結構
        Public Ju As String()
        Public Kon As String()
    End Structure

    Public Structure YellowGingType
        Public BornDate As Integer
        Public Passed As Integer
        Public NowDate As Integer
        Public Quadrant As Byte
        Public Sub clear()
            BornDate = 0
            Passed = 0
            NowDate = 0
            Quadrant = 1
        End Sub
    End Structure

    Public Structure typeBigYuan
        Public Gan As String           '干
        Public Tsu As String           '支
        Public YearsOld As Integer     '歲數
        Public Kon As String           '宮次
        Public LongLife As String      '長生
        Public SenFu As String         '神輔1
        Public SenFu12 As String       '神輔2
        Public Naing As String         '納音
        Public StarB2 As String        '星相
        Public Star As String          '星座
        Public Sub clear()
            Gan = ""
            Tsu = ""
            YearsOld = 0
            Kon = ""
            LongLife = ""
            SenFu = ""
            SenFu12 = ""
            Naing = ""
            StarB2 = ""
            Star = ""
        End Sub
    End Structure

    Public Structure Birth
        Public No As String            ' 編號
        Public EngMode As Boolean      ' 工程模式
        Public Age As Integer          ' 批命時的年齡
        Public Name As String          ' 姓名
        Public Sex As String           ' 姓別(男 or 女)
        Public Solar As YearMonthDay   ' 西曆出生年月日
        Public SolarNextDay As YearMonthDay   ' 西曆出生年月日的隔天
        Public Lunar As YearMonthDay   ' 農曆出生年月日
        Public Lunar_IntercalaryMonth As Boolean '農曆月份是否閏月
        Public BornHour As Byte        ' 出生時
        Public BornMinute As Byte      ' 出生分
        Public BornPlace As PlaceLL    ' 出生地經緯度
        Public BornPlaceAddr As String ' 出生地
        Public SType As String         ' 陽年或陰年生
        Public Ju() As GThG            '八柱干支藏干
        Public PreJa As JaChiDateTime  '生辰之前的節
        Public NxtJa As JaChiDateTime  '生辰之後的節
        Public PreJC As JaChiDateTime  '生辰之前的節或氣
        Public SenFu As AllSenFu5Sing  '所有八柱的神輔
        Public WuSing As AllSenFu5Sing '所有八柱的五行
        Public NaIng As All8Juinfo  '所有八柱的納音五行
        Public Star As All8Juinfo   '所有八柱的星座 (天干)
        Public StarB As All8Juinfo  '所有八柱的星座B1 (地支,西洋星座)
        Public StarC As All8Juinfo  '所有八柱的星座C  (二十八星宿)
        Public MiKon As All8Juinfo  '所有八柱的所屬十二命宮
        Public SenSa As String(,)   '所有八柱的神煞
        Public Gwa As AllGwaInfo
        Public Kon12 As String()    '宮次
        Public Shan12 As String()   '限
        Public Yuan12 As String()   '運
        Public SenFu12 As String()  '神輔
        Public LongLife12 As All8Ju12Koninfo  '長生
        Public StarSky As String(,) '星際
        Public YearKing As String() '歲君
        Public StarB2 As String()   '十二宮星座
        Public Y60state As Byte(,)  '60年運線
        Public YellowGing As YellowGingType '黃經度
        Public BigYuan As typeBigYuan()     '運程
        Public BigYuanStarSky As String(,)  '運程的星際
        Public Ja2Born_D As Integer         ' 數至上/下個節氣有幾天幾時辰
        Public Ja2Born_H As Integer
        Public YuanStart As YearMonthDay    '上運距離的年月日
        Public YuanStartDate1 As String     '上運的西曆日期
        Public YuanStartDate2 As String     '上運的農曆日期
        Public YuanStart_YearGT As String   '上運年的干支
        Public YuanChange_YG As String      '每逢交脫的年
        Public YuanChange_M As String       '每逢交脫的月
        Public YuanChange_JC As String      '每逢交脫的節氣
        Public YuanChange_JCday As Integer  '每逢交脫的節氣後幾天
        Public WuSingState As String        '五行的旺相休囚死
        Public WuSingCount As Integer(,)    '五行的計數
        Public SenFuCount As Integer()      '神輔的計數
        Public DayStrongWeak As String   '日主強弱
        Public Main8wordKan As String    '八字取格(主要)
        Public Sub8wordKan1 As String    '八字取格(副1)
        Public Sub8wordKan2 As String    '八字取格(副2)
        Public Tsu3GroupType As String       '四柱地支有三合三會
        Public Tsu3Group5Sing As String      '三合三會的五行
        Public UsingGodMain As String    '用神(主要)
        Public UsingGodSub As String     '用神(次要)
        Public UsingGodNote As String    '用神提要
        Public YearEmpty As String     '年空亡
        Public DayEmpty As String      '日空亡

        Public Sub Initinal()
            ReDim Ju(8)
            ReDim SenFu.Ju(8)
            ReDim WuSing.Ju(8)
            ReDim NaIng.Ju(8)
            ReDim Gwa.Ju(8)
            ReDim Star.Ju(8)
            ReDim StarB.Ju(8)
            ReDim StarC.Ju(8)
            ReDim MiKon.Ju(8)
            ReDim Sensa(8, 14)
            ReDim Kon12(12)
            ReDim Shan12(12)
            ReDim Yuan12(12)
            ReDim SenFu12(12)
            ReDim LongLife12.Ju(8)    '八柱的長生
            ReDim LongLife12.Kon(12)  '十二宮的長生
            ReDim StarSky(12, 14)
            ReDim YearKing(12)
            ReDim StarB2(12)
            ReDim Y60state(10, 12)
            ReDim BigYuan(10)
            ReDim BigYuanStarSky(10, 14)
            ReDim WuSingCount(5, 2)
            ReDim SenFuCount(10)
        End Sub
        Public Sub Clear()
            No = "0"
            Name = ""
            Sex = ""
            Age = 0
            Solar.clear()
            SolarNextDay.clear()
            Lunar.clear()
            BornHour = 0
            BornMinute = 0
            BornPlace.clear()
            BornPlaceAddr = ""
            SType = ""
            PreJa.clear()
            NxtJa.clear()
            PreJC.clear()
            Ja2Born_D = 0
            Ja2Born_H = 0
            YuanStart.clear()
            YuanStartDate1 = ""
            YuanStartDate2 = ""
            YuanStart_YearGT = ""
            YuanChange_YG = ""
            YuanChange_M = ""
            YuanChange_JC = ""
            YuanChange_JCday = 0
            WuSingState = ""
            DayStrongWeak = ""
            Main8wordKan = ""
            Sub8wordKan1 = ""
            Sub8wordKan2 = ""
            Tsu3GroupType = ""
            Tsu3Group5Sing = ""
            UsingGodMain = ""
            UsingGodSub = ""
            YearEmpty = ""
            DayEmpty = ""

            For i As Integer = 0 To 8   '------------
                Ju(i).clear()
                SenFu.Ju(i).clear()
                WuSing.Ju(i).clear()
                NaIng.Ju(i) = ""
                Star.Ju(i) = ""
                StarB.Ju(i) = ""
                StarC.Ju(i) = ""
                MiKon.Ju(i) = ""
                LongLife12.Ju(i) = ""
                Gwa.Ju(i).clear()
                For j As Integer = 0 To 14
                    SenSa(i, j) = ""
                Next
            Next
            For k As Integer = 0 To 12  '------------
                Kon12(k) = ""
                Shan12(k) = ""
                Yuan12(k) = ""
                SenFu12(k) = ""
                LongLife12.Kon(k) = ""
                YearKing(k) = ""
                StarB2(k) = ""
                For p As Integer = 0 To 10
                    Y60state(p, k) = 0
                Next
                For j As Integer = 0 To 14
                    StarSky(k, j) = ""
                Next
            Next
            For p As Integer = 0 To 10   '-----------
                BigYuan(p).clear()
                SenFuCount(p) = 0
                For j As Integer = 0 To 14
                    BigYuanStarSky(p, j) = ""
                Next
            Next
            For i As Integer = 0 To 5
                For j As Integer = 0 To 2
                    WuSingCount(i, j) = 0
                Next
            Next
        End Sub
    End Structure

    '宣告主要命造變數 PS:在主表單Load時,必須呼叫initinal()
    Public Bir As Birth

    Public Structure RelatedJC
        Public PreJa As JaChiDateTime  '日期之前的節
        Public NxtJa As JaChiDateTime  '日期之後的節
        Public PreJC As JaChiDateTime  '日期之前的節或氣
        Public changeMonth As boolean  '需要變換月份干支
    End Structure

    Public Function IsInGan(ByVal strG As String) As Boolean
        Dim strGan As String
        Dim pos As Integer
        strGan = "甲乙丙丁戊己庚辛壬癸"
        pos = InStr(strGan, strG)
        If pos = 0 Then
            IsInGan = False
        Else
            IsInGan = True
        End If
    End Function

    Public Function NextGan(ByVal strG As String) As String
        Dim strGan As String
        strGan = "甲乙丙丁戊己庚辛壬癸"
        ' 轉換下一個天干
        If strG = "癸" Then
            NextGan = "甲"
        Else
            NextGan = Mid(strGan, InStr(strGan, strG) + 1, 1)
        End If
    End Function

    Public Function PrevGan(ByVal strG As String) As String
        Dim strGan As String
        strGan = "癸壬辛庚己戊丁丙乙甲"
        ' 轉換前一個天干
        If strG = "甲" Then
            PrevGan = "癸"
        Else
            PrevGan = Mid(strGan, InStr(strGan, strG) + 1, 1)
        End If
    End Function

    Public Function IsInTsu(ByVal strT As String) As Boolean
        Dim strTsu As String
        Dim pos As Integer
        strTsu = "子丑寅卯辰巳午未申酉戌亥"
        pos = InStr(strTsu, strT)
        If pos = 0 Then
            IsInTsu = False
        Else
            IsInTsu = True
        End If
    End Function

    Public Function NextTsu(ByVal strT As String) As String
        Dim strTsu As String
        strTsu = "子丑寅卯辰巳午未申酉戌亥"
        ' 轉換下一個地支
        If strT = "亥" Then
            NextTsu = "子"
        Else
            NextTsu = Mid(strTsu, InStr(strTsu, strT) + 1, 1)
        End If
    End Function

    Public Function PrevTsu(ByVal strT As String) As String
        Dim strTsu As String
        strTsu = "亥戌酉申未午巳辰卯寅丑子"
        ' 轉換前一個地支
        If strT = "子" Then
            PrevTsu = "亥"
        Else
            PrevTsu = Mid(strTsu, InStr(strTsu, strT) + 1, 1)
        End If
    End Function

    Public Function DiffGT1Gt2(ByVal GT1_Gan As String, ByVal GT1_Tsu As String, _
                               ByVal GT2_Gan As String, ByVal GT2_Tsu As String) As Integer
        Dim Diff As Integer
        Diff = 0
        Do While GT2_Gan <> GT1_Gan Or GT2_Tsu <> GT1_Tsu
            'Move GT2 to next one
            GT2_Gan = NextGan(GT2_Gan)
            GT2_Tsu = NextTsu(GT2_Tsu)
            Diff = Diff + 1
        Loop

        DiffGT1Gt2 = Diff
    End Function

    '求干的五合------------------------------
    Public Function Gmatch(ByVal strGan As String) As String
        Select Case strGan
            Case "甲"
                Gmatch = "己"
            Case "乙"
                Gmatch = "庚"
            Case "丙"
                Gmatch = "辛"
            Case "丁"
                Gmatch = "壬"
            Case "戊"
                Gmatch = "癸"
            Case "己"
                Gmatch = "甲"
            Case "庚"
                Gmatch = "乙"
            Case "辛"
                Gmatch = "丙"
            Case "壬"
                Gmatch = "丁"
            Case "癸"
                Gmatch = "戊"
            Case Else        'dummy instuction
                Gmatch = "戊"
        End Select

    End Function

    '求支的六合------------------------------
    Public Function Tmatch(ByVal strTsu As String) As String
        Select Case strTsu
            Case "子"
                Tmatch = "丑"
            Case "丑"
                Tmatch = "子"
            Case "寅"
                Tmatch = "亥"
            Case "卯"
                Tmatch = "戌"
            Case "辰"
                Tmatch = "酉"
            Case "巳"
                Tmatch = "申"
            Case "午"
                Tmatch = "未"
            Case "未"
                Tmatch = "午"
            Case "申"
                Tmatch = "巳"
            Case "酉"
                Tmatch = "辰"
            Case "戌"
                Tmatch = "卯"
            Case "亥"
                Tmatch = "寅"
            Case Else         'dummy
                Tmatch = "寅"
        End Select
    End Function

    '求干支的五合六合(最合的干支)------------------------------
    Public Function GTMatch(ByVal strDayGanTsu As String) As String
        Dim strGan As String
        Dim strTsu As String
        strGan = Microsoft.VisualBasic.Left(strDayGanTsu, 1)
        strTsu = Microsoft.VisualBasic.Right(strDayGanTsu, 1)
        Select Case strGan
            Case "甲"
                strGan = "己"
            Case "乙"
                strGan = "庚"
            Case "丙"
                strGan = "辛"
            Case "丁"
                strGan = "壬"
            Case "戊"
                strGan = "癸"
            Case "己"
                strGan = "甲"
            Case "庚"
                strGan = "乙"
            Case "辛"
                strGan = "丙"
            Case "壬"
                strGan = "丁"
            Case "癸"
                strGan = "戊"
        End Select
        Select Case strTsu
            Case "子"
                strTsu = "丑"
            Case "丑"
                strTsu = "子"
            Case "寅"
                strTsu = "亥"
            Case "卯"
                strTsu = "戌"
            Case "辰"
                strTsu = "酉"
            Case "巳"
                strTsu = "申"
            Case "午"
                strTsu = "未"
            Case "未"
                strTsu = "午"
            Case "申"
                strTsu = "巳"
            Case "酉"
                strTsu = "辰"
            Case "戌"
                strTsu = "卯"
            Case "亥"
                strTsu = "寅"
        End Select
        GTMatch = strGan & strTsu
    End Function

    '五鼠遁
    Public Function WuSu(ByVal strTsu As String, ByVal strGan As String) As String
        Dim str10G As String
        Dim str12T As String
        Dim intAt As Integer
        str10G = "甲乙丙丁戊己庚辛壬癸甲乙丙丁戊己庚辛壬癸"
        str12T = "子丑寅卯辰巳午未申酉戌亥"
        intAt = InStr(str12T, strTsu)
        Select Case strGan
            Case "甲", "己"
                intAt = intAt
            Case "乙", "庚"
                intAt = intAt + 2
            Case "丙", "辛"
                intAt = intAt + 4
            Case "丁", "壬"
                intAt = intAt + 6
            Case "戊", "癸"
                intAt = intAt + 8
        End Select
        WuSu = Mid(str10G, intAt, 1)
    End Function

    ' 由 天干六神便訣、天干六神便訣 對照出的六神訣
    Public Function FindSixSen(ByVal strGan As String, ByVal strKey As String) As String
        Dim strMatch As String
        Dim arySixSen As String()
        Dim intAt As Integer
        strMatch = "甲乙丙丁戊己庚辛壬癸子丑寅卯辰巳午未申酉戌亥"
        Select Case strGan
            Case "甲"
                arySixSen = {"比肩", "劫財", "食神", "傷官", "偏財", "正財", "七殺", "正官", "偏印", "正印", "偏印", "正財", "比肩", "劫財", "偏財", "傷官", "食神", "正財", "七殺", "正官", "偏財", "正印"}
            Case "乙"
                arySixSen = {"劫財", "比肩", "傷官", "食神", "正財", "偏財", "正官", "七殺", "正印", "偏印", "正印", "偏財", "劫財", "比肩", "正財", "食神", "傷官", "偏財", "正官", "七殺", "正財", "偏印"}
            Case "丙"
                arySixSen = {"偏印", "正印", "比肩", "劫財", "食神", "傷官", "偏財", "正財", "七殺", "正官", "七殺", "傷官", "偏印", "正印", "食神", "劫財", "比肩", "傷官", "偏財", "正財", "食神", "正官"}
            Case "丁"
                arySixSen = {"正印", "偏印", "劫財", "比肩", "傷官", "食神", "正財", "偏財", "正官", "七殺", "正官", "食神", "正印", "偏印", "傷官", "比肩", "劫財", "食神", "正財", "偏財", "傷官", "七殺"}
            Case "戊"
                arySixSen = {"七殺", "正官", "偏印", "正印", "比肩", "劫財", "食神", "傷官", "偏財", "正財", "偏財", "劫財", "七殺", "正官", "比肩", "正印", "偏印", "劫財", "食神", "傷官", "比肩", "正財"}
            Case "己"
                arySixSen = {"正官", "七殺", "正印", "偏印", "劫財", "比肩", "傷官", "食神", "正財", "偏財", "正財", "比肩", "正官", "七殺", "劫財", "偏印", "正印", "比肩", "傷官", "食神", "劫財", "偏財"}
            Case "庚"
                arySixSen = {"偏財", "正財", "七殺", "正官", "偏印", "正印", "比肩", "劫財", "食神", "傷官", "食神", "正印", "偏財", "正財", "偏印", "正官", "七殺", "正印", "比肩", "劫財", "偏印", "傷官"}
            Case "辛"
                arySixSen = {"正財", "偏財", "正官", "七殺", "正印", "偏印", "劫財", "比肩", "傷官", "食神", "傷官", "偏印", "正財", "偏財", "正印", "七殺", "正官", "偏印", "劫財", "比肩", "正印", "食神"}
            Case "壬"
                arySixSen = {"食神", "傷官", "偏財", "正財", "七殺", "正官", "偏印", "正印", "比肩", "劫財", "比肩", "正官", "食神", "傷官", "七殺", "正財", "偏財", "正官", "偏印", "正印", "七殺", "劫財"}
            Case Else    '"癸"
                arySixSen = {"傷官", "食神", "正財", "偏財", "正官", "七殺", "正印", "偏印", "劫財", "比肩", "劫財", "七殺", "傷官", "食神", "正官", "偏財", "正財", "七殺", "正印", "偏印", "正官", "比肩"}
        End Select
        If strKey <> "" Then
            intAt = InStr(strMatch, strKey)
            FindSixSen = arySixSen(intAt - 1)
        Else
            FindSixSen = ""   '預設給空白的藏干
        End If
    End Function

    ' 把兩個字的神輔，轉為一個字的簡寫
    Public Function SF1word(ByVal strSenFu As String) As String
        Select Case strSenFu
            Case "比肩"
                SF1word = "比"
            Case "劫財"
                SF1word = "劫"
            Case "偏印"
                SF1word = "梟"
            Case "正印"
                SF1word = "印"
            Case "七殺"
                SF1word = "殺"
            Case "正官"
                SF1word = "官"
            Case "偏財"
                SF1word = "利"
            Case "正財"
                SF1word = "財"
            Case "食神"
                SF1word = "食"
            Case "傷官"
                SF1word = "傷"
            Case "天元"
                SF1word = "元"
            Case "元"
                SF1word = "元"
            Case Else   '若為空白輸出也空白
                SF1word = ""
        End Select
    End Function

    '求納音五行
    Public Function FindNaIng5Sing(ByVal str干 As String, ByVal str支 As String) As String
        Select Case str干 & str支
            Case "甲子", "乙丑"
                FindNaIng5Sing = "海中金"
            Case "丙寅", "丁卯"
                FindNaIng5Sing = "爐中火"
            Case "戊辰", "己巳"
                FindNaIng5Sing = "大林木"
            Case "庚午", "辛未"
                FindNaIng5Sing = "路旁土"
            Case "壬申", "癸酉"
                FindNaIng5Sing = "劍鋒金"
            Case "甲戌", "乙亥"
                FindNaIng5Sing = "山頭火"
            Case "丙子", "丁丑"
                FindNaIng5Sing = "澗下水"
            Case "戊寅", "己卯"
                FindNaIng5Sing = "城頭土"
            Case "庚辰", "辛巳"
                FindNaIng5Sing = "白臘金"
            Case "壬午", "癸未"
                FindNaIng5Sing = "楊柳木"
            Case "甲申", "乙酉"
                FindNaIng5Sing = "井泉水"
            Case "丙戌", "丁亥"
                FindNaIng5Sing = "屋上土"
            Case "戊子", "己丑"
                FindNaIng5Sing = "霹靂火"
            Case "庚寅", "辛卯"
                FindNaIng5Sing = "松柏木"
            Case "壬辰", "癸巳"
                FindNaIng5Sing = "長流水"
            Case "甲午", "乙未"
                FindNaIng5Sing = "沙中金"
            Case "丙申", "丁酉"
                FindNaIng5Sing = "山下火"
            Case "戊戌", "己亥"
                FindNaIng5Sing = "平地木"
            Case "庚子", "辛丑"
                FindNaIng5Sing = "壁上土"
            Case "壬寅", "癸卯"
                FindNaIng5Sing = "金箔金"
            Case "甲辰", "乙巳"
                FindNaIng5Sing = "覆燈火"
            Case "丙午", "丁未"
                FindNaIng5Sing = "天河水"
            Case "戊申", "己酉"
                FindNaIng5Sing = "大驛土"
            Case "庚戌", "辛亥"
                FindNaIng5Sing = "釵釧金"
            Case "壬子", "癸丑"
                FindNaIng5Sing = "桑拓木"
            Case "甲寅", "乙卯"
                FindNaIng5Sing = "大溪水"
            Case "丙辰", "丁巳"
                FindNaIng5Sing = "沙中土"
            Case "戊午", "己未"
                FindNaIng5Sing = "天上火"
            Case "庚申", "辛酉"
                FindNaIng5Sing = "石榴木"
            Case "壬戌", "癸亥"
                FindNaIng5Sing = "大海水"
            Case Else
                FindNaIng5Sing = "大海水"
        End Select
    End Function

    '求(各干支)的五行
    Public Function Find5Sing(ByVal StrGT As String) As String
        Dim strGTall As String = "甲乙丙丁戊己庚辛壬癸子丑寅卯辰巳午未申酉戌亥"
        Dim aryMatch As String() = New String() {"木+", "木-", "火+", "火-", "土+", "土-",
                    "金+", "金-", "水+", "水-", "水+", "土-", "木+", "木-", "土+", "火-", _
                         "火+", "土-", "金+", "金-", "土+", "水-"}
        If StrGT <> "" Then
            Find5Sing = aryMatch(InStr(strGTall, StrGT) - 1)
        Else
            Find5Sing = ""
        End If
    End Function

    '神煞總查詢 ----------------------------------------------
    '查詢一: 使用地支(十二宮次)查詢 strKey1=支
    '查詢二: 使用天干地支(八柱)查詢 strKey1=支 strKey2=干
    Public Function FindStarSky( _
        ByVal YearTsu As String, _
        ByVal MonthTsu As String, _
        ByVal DayGan As String, _
        ByVal strKey1 As String, Optional ByVal strKey2 As String = "") As String

        Dim strOut As String = ""
        Dim strKey As String
        Dim strKey_2 As String
        Dim aryTmp As String() = {"", ""}
        Dim ALLTsu As String
        Dim strTmp As String

        'Step1 比對特有干支 -------------------------------------------------
        If strKey2 <> "" Then
            strKey = (strKey2 & strKey1)
            Select Case strKey
                Case "甲子", "甲辰", "甲寅", "丙戌", "丙寅"
                    strOut = strOut & "," & "平頭殺"
            End Select
            Select Case strKey
                Case "戊子", "己丑", "戊午", "己未", "丁未", "丙午"
                    strOut = strOut & "," & "六秀"
            End Select
            Select Case strKey
                Case "壬辰", "庚戌", "庚辰", "戊戌"
                    strOut = strOut & "," & "魁罡"
            End Select
            Select Case strKey
                Case "甲子", "甲午", "己卯"
                    strOut = strOut & "," & "進神"
            End Select
            Select Case strKey
                Case "己巳", "癸酉", "乙丑"
                    strOut = strOut & "," & "金神"
            End Select
            Select Case strKey
                Case "戊寅", "甲午", "戊申", "甲子"
                    strOut = strOut & "," & "天赦"
            End Select
        End If

        '-----原程式提前判斷此二者
        Select Case (DayGan & strKey1)
            Case "丙申", "戊申", "丁酉", "己酉", "壬寅", "癸卯"
                strOut = strOut & "," & "文昌"
        End Select
        strTmp = DayGan & strKey1
        If strTmp = "甲亥" Or strTmp = "乙戌" Or strTmp = "丙申" Or _
           strTmp = "丁未" Or strTmp = "戊申" Or strTmp = "己未" Or _
           strTmp = "庚巳" Or strTmp = "辛辰" Or strTmp = "壬寅" Or strTmp = "癸丑" Then
            strOut = strOut & "," & "暗祿"
        End If


        ' 查對一般神煞, 年支表--------------------------------------------------
        ALLTsu = "子丑寅卯辰巳午未申酉戌亥"
        Select Case YearTsu
            Case "子"
                aryTmp = {"金匱,將星,擎天,劍鋒", _
                         "玉堂,歲合,晦氣,天空,陰殺", _
                         "驛馬,孤辰,地喪", _
                         "大隨,紅鸞,羊刃,卒暴,三刑,貫索,勾紋,六厄", _
                         "金匱,三臺,官符,華蓋,披頭,飛符,黃番", _
                         "劫殺,月德,的殺,小耗,破碎", _
                         "紅艷,月空,大殺,天哭,囚獄", _
                         "紫微,地解,暴敗,天耗,天殺,歲殺,天厄,闌干", _
                         "急劫殺,白衣,指背,地殺,飛廉,大殺", _
                         "天喜,天德,福星,流霞,披麻,飛刃,年殺,卷舌,咸池", _
                         "八座,天解,解神,血刃,吞陷,豹尾,弔客,月殺,浮沈", _
                         "陌越,亡神,吞陷"}
            Case "丑"
                aryTmp = {"陌越,歲合,玉堂,孤虛", _
                         "黃番,劍鋒,華蓋,伏尸", _
                         "孤辰,紅鸞,劫殺,吞陷,天空,晦氣", _
                         "披頭,災殺,地喪,囚獄", _
                         "羊刃,勾絞,天殺,歲殺,貫索,卒暴", _
                         "飛符,金匱,三臺,地殺,官符,天哭,指背", _
                         "月殺,月德,咸池,小耗,年殺", _
                         "地解,大耗,破碎,豹尾,闌干", _
                         "貴人,紫微,天喜,紅艷,亡神,天厄,暴敗,天官符", _
                         "八座,天解,將星,解神,金匱,飛廉,大殺,浮沈,血刃", _
                         "天德,福星,三型,板鞍,卷舌,寡宿", _
                         "天馬,弔客"}
            Case "寅"
                aryTmp = {"引客,災殺", _
                         "陌越,紅鸞,天醫,吞陷,寡宿,天殺", _
                         "指背,劍鋒,披麻,地殺", _
                         "天空,咸池,年殺", _
                         "地喪,天哭,豹尾,月殺", _
                         "勾絞,孤辰,貫索,亡神", _
                         "三臺,金匱,將星,官符,飛符", _
                         "月德,天喜,板鞍,小耗", _
                         "八座,天馬,天解,解神,地解,闌干,破碎,血刃,浮沈", _
                         "紫微,暴敗,天厄,六害", _
                         "黃番,華蓋,飛廉,大殺,", _
                         "歲合,天德,福星,劫殺,吞卷,絞煞"}
            Case "卯"
                aryTmp = {"天德,紅鸞,福星,咸池,卷舌,年刑", _
                         "寡宿,天解,八座,披頭,飛刃,月殺,弔客,", _
                         "陌越,亡神,天官符", _
                         "天殺,金匱,將星,天哭,劍鋒,伏日", _
                         "攀鞍,陰殺,天空,晦氣", _
                         "孤辰,驛馬,的殺,地喪,破碎", _
                         "天嘉,六厄,匱素,勾絞,六害", _
                         "黃番,天解,三臺,解神,華蓋,羊刃,浮沈,血刃,飛符", _
                         "月德,地解,小耗,釗殺", _
                         "歲殺,月空,闌干,災殺,大耗", _
                         "紫微,暴敗,天厄,天殺", _
                         "天殺,地殺"}
            Case "辰"
                aryTmp = {"飛刃,金匱,將星,飛廉,天殺,披頭", _
                         "福星,天德,的殺,卷吞,陰殺,寡宿", _
                         "驛馬,弔客,天哭", _
                         "陌越,六害", _
                         "吞陷,劍鋒,華蓋,黃番,伏尸", _
                         "天解,天喜,劫殺,天空,流霞,孤辰,晦氣", _
                         "災殺,八座,解神,浮沈,地喪,血刃,羊刃", _
                         "卒暴,歲殺,天殺,貫索,勾絞,囚獄", _
                         "披麻,三臺,飛符,官符,地殺,指背", _
                         "地殺,歲合,月德,咸池,年殺,小耗", _
                         "破碎,月空,闌干,月殺,豹尾,大耗", _
                         "紫微,地解,紅鸞,暴敗,天厄,亡神,天官符"}
            Case "巳"
                aryTmp = {"地解,玉堂,紫微,天厄,六害,暴敗,六厄", _
                         "飛廉,黃番,大殺,華蓋,羊刃", _
                         "天德,福星,卷舌,天哭,劫殺", _
                         "弔客,八座,天殺,吞陷,急腳殺", _
                         "陌越,天喜,天解,囚獄,天殺,寡宿,歲殺,紅艷", _
                         "伏尸,解神,浮沈,劍鋒,血刃,指背", _
                         "年殺,咸池,天空,晦氣,流霞", _
                         "月殺,豹尾,地喪,羊刃", _
                         "歲合,貴人,勾絞,歲刑,天官,卒暴,亡神,貫索,天官符", _
                         "將星,三臺,金匱,飛符,的殺,官符,陰殺", _
                         "紅鸞,月德,攀鞍,小耗", _
                         "破碎,驛馬,月空,披頭,大耗,闌干"}
            Case "午"
                aryTmp = {"破碎,月空,闌干,災殺,歲殺,天哭,大耗", _
                         "紫微,地解,吞陷,暴敗,天厄,歲殺,天殺,埋兒殺", _
                         "指背,大殺,地殺,飛廉,天雞", _
                         "天德,福星,天喜,咸池,年殺,卷吞,披麻", _
                         "解神,天解,八座,寡宿,豹尾,浮沈,月殺,弔客", _
                         "亡神,陌越,急腳,天官,腳殺,官符,破碎,的殺", _
                         "金匱,將星,劍鋒,伏尸", _
                         "歲合,攀鞍,天空,月殺,晦氣", _
                         "驛馬,孤辰,地喪", _
                         "紅鸞,卒暴,三刑,貫素,勾絞,六害,羊刃", _
                         "三臺,黃番,飛符,官符,華蓋,披頭", _
                         "月德,劫殺,小耗"}
            Case "未"
                aryTmp = {"月德,小耗,咸池,年殺", _
                         "闌干,月空,大耗,的殺,月殺,豹尾", _
                         "地解,紫微,天喜,暴敗,天官符,亡神,天厄,吞陷", _
                         "金匱,天解,解神,將神,飛廉,大殺,浮沈,血刃", _
                         "福星,天德,攀鞍,飛刃,陰殺,卷舌,披頭,寡宿", _
                         "八座,驛馬,弔客", _
                         "陌越,玉堂,歲合,六合", _
                         "華蓋,黃番,劍鋒,伏尸", _
                         "紅鸞,孤辰,劫殺,天空", _
                         "披頭,災殺,地殺,囚獄", _
                         "羊刃,勾紋,貫索", _
                         "三臺,飛符,地殺,官符,天哭,指背"}
            Case "申"
                aryTmp = {"將星,三臺,金匱,飛符,官符", _
                         "月德,天喜,攀鞍,陰殺,小耗", _
                         "驛馬,月空,天解,解神,歲刑,破碎,大耗,浮沈,羊刃", _
                         "紫微,玉堂,地解,天殺,六害,闌干,暴敗,天厄", _
                         "飛廉,大殺,華蓋,黃番", _
                         "天德,貴人,歲合,福星,劫殺,卷舌,披麻", _
                         "八座,弔客,囚獄,災殺", _
                         "陌越,紅鸞,歲殺,天殺,宿寡", _
                         "劍鋒,指背,地殺,披頭,伏尸", _
                         "咸池,晦氣,天空", _
                         "豹尾,天哭,月殺,地喪,吞陷", _
                         "貫索,地解,孤辰,勾絞,亡神,卒暴,官符"}
            Case "酉"
                aryTmp = {"六害,天喜,貫索,勾絞,卒暴,六厄", _
                         "天解,解神,三臺,浮沈,飛符,黃番,官符,華蓋,血刃", _
                         "月德,劫殺,死殺,小耗", _
                         "月空,闌干,大耗,破碎,災殺,囚獄", _
                         "歲合,紫微,地解,天厄,歲殺,天殺,暴敗", _
                         "飛廉,地殺,大殺", _
                         "紅鸞,福星,天德,咸池,年殺,卷舌,披麻", _
                         "披頭,八座,寡宿,月殺,弔客,豹尾", _
                         "陌越,天官符,亡神", _
                         "將星,金匱,劍鋒,天哭,伏尸", _
                         "披頭,陰殺,吞陷,天空,晦氣", _
                         "驛馬,孤辰,地喪"}
            Case "戌"
                aryTmp = {"天解,八座,解神,災殺,地喪,浮沈,羊刃", _
                         "年殺,天殺,卒暴,勾絞", _
                         "吞陷,三臺,飛符,地殺,官符,指背", _
                         "月德,歲合,咸池,年殺,小耗", _
                         "闌干,月空,破碎,豹尾,月殺,大耗", _
                         "紅鸞,紫微,天官符,亡神,天厄,暴敗", _
                         "地解,將星,金匱,大殺,披頭", _
                         "天德,福星,寡宿,歲刑,卷舌", _
                         "天馬,弔客,天哭", _
                         "陌越,六害", _
                         "伏尸,劍鋒,黃番,華蓋", _
                         "天喜,孤辰,天空,劫殺"}
            Case "亥"
                aryTmp = {"咸池,年殺,天空,晦氣,擎天", _
                         "卒暴,豹尾,月殺,地喪,貫索", _
                         "歲合,孤辰,勾絞,亡神,天官符", _
                         "金匱,三臺,將星,飛廉,披頭,官符", _
                         "紅鸞,月德,攀鞍,陰殺,小耗", _
                         "驛馬,月空,大耗,披頭,破碎", _
                         "紫微,遊奕,六害,天厄,暴敗", _
                         "華蓋,飛廉,黃番,天哭,大殺", _
                         "天德,福星,劫殺,披麻,卷舌", _
                         "天解,八座,囚獄,弔客,夾殺", _
                         "陌越,天喜,歲殺,天殺,寡宿", _
                         "指背,解神,血刃,浮沈,地殺,伏尸"}
        End Select
        strOut = strOut & "," & aryTmp(InStr(ALLTsu, strKey1) - 1)

        ' 日干表 ------------------------------------------------------------------
        strKey = DayGan & strKey1
        Select Case strKey
            Case "甲寅", "乙卯", "丙巳", "丁午", "戊巳", "己午", "庚申", "辛酉", "壬亥", "癸子"
                strOut = strOut & "," & "祿神"
        End Select
        Select Case strKey
            Case "甲丑", "乙子", "丙亥", "丁亥", "戊丑", "己子", "庚丑", "辛午", "壬巳", "癸巳"
                strOut = strOut & "," & "天乙貴人"
        End Select
        Select Case strKey
            Case "甲未", "乙申", "丙酉", "丁酉", "戊未", "己申", "庚未", "辛寅", "壬卯", "癸卯"
                strOut = strOut & "," & "天乙貴人"
        End Select
        Select Case strKey
            Case "甲卯", "乙辰", "丙午", "丁未", "戊午", "己未", "庚酉", "辛戌", "壬子", "癸丑"
                strOut = strOut & "," & "陽刃"
        End Select
        Select Case strKey
            Case "甲亥", "乙午", "丙寅", "丁酉", "戊寅", "己酉", "庚巳", "辛子", "壬申", "癸卯"
                strOut = strOut & "," & "學堂"
        End Select
        Select Case strKey
            Case "甲午", "甲申", "乙午", "乙申", "丙寅", "丁未", "戊辰", "己辰", "庚申", "庚戌", "辛酉", "壬子", "癸申"
                strOut = strOut & "," & "紅豔"
        End Select
        Select Case strKey
            Case "甲酉", "乙戌", "丙未", "丁申", "戊巳", "己午", "庚辰", "辛卯", "壬亥", "癸寅"
                strOut = strOut & "," & "流霞"
        End Select
        Select Case strKey
            Case "甲辰", "乙己", "丙未", "丁申", "戊未", "己申", "庚戌", "辛亥", "壬丑", "癸寅"
                strOut = strOut & "," & "金與"
        End Select
        Select Case strKey
            Case "甲酉", "乙戌", "丙子", "丁丑", "戊子", "己丑", "庚卯", "辛辰", "壬午", "癸未"
                strOut = strOut & "," & "飛刃"
        End Select

        If Right(strOut, 1) = "," Then
            strOut = Microsoft.VisualBasic.Left(strOut, Len(strOut) - 1)
        End If

        ' 月支表-------------------------------------------------------------------------
        strKey = MonthTsu & strKey1
        strKey_2 = MonthTsu & strKey2
        Select Case strKey
            Case "寅丁", "卯申", "辰壬", "巳辛", "午亥", "未甲", "申癸", "酉寅", "戌丙", "亥乙", "子己", "丑庚"
                strOut = strOut & "," & "天德"
        End Select
        Select Case strKey
            Case "寅丙", "卯甲", "辰壬", "巳庚", "午丙", "未甲", "申壬", "酉庚", "戌丙", "亥甲", "子壬", "丑庚"
                strOut = strOut & "," & "月德"
        End Select
        Select Case strKey_2
            Case "寅丁", "卯申", "辰壬", "巳辛", "午亥", "未甲", "申癸", "酉寅", "戌丙", "亥乙", "子己", "丑庚"
                strOut = strOut & "," & "天德"
        End Select
        Select Case strKey_2
            Case "寅丙", "卯甲", "辰壬", "巳庚", "午丙", "未甲", "申壬", "酉庚", "戌丙", "亥甲", "子壬", "丑庚"
                strOut = strOut & "," & "月德"
        End Select
        Select Case strKey
            Case "寅丑", "卯未", "辰寅", "巳申", "午卯", "未酉", "申辰", "酉戌", "戌巳", "亥亥", "子午", "丑子"
                strOut = strOut & "," & "血刃"
        End Select

        ' 年支表
        strKey = YearTsu & strKey1
        Select Case strKey
            Case "子辰", "丑丑", "寅戌", "卯未", "辰辰", "己丑", "午戌", "未未", "申辰", "酉丑", "戌戌", "亥未"
                strOut = strOut & "," & "華蓋"
        End Select
        Select Case strKey
            Case "子子", "丑酉", "寅午", "卯卯", "辰子", "己酉", "午午", "未卯", "申子", "酉酉", "戌午", "亥卯"
                strOut = strOut & "," & "將星"
        End Select
        Select Case strKey
            Case "子寅", "丑亥", "寅申", "卯巳", "辰寅", "己亥", "午申", "未巳", "申寅", "酉亥", "戌申", "亥巳"
                strOut = strOut & "," & "驛馬"
        End Select
        Select Case strKey
            Case "子酉", "丑午", "寅卯", "卯子", "辰酉", "己午", "午卯", "未子", "申酉", "酉午", "戌卯", "亥子"
                strOut = strOut & "," & "桃花"
        End Select
        Select Case strKey
            Case "子戌", "丑酉", "寅申", "卯未", "辰午", "己巳", "午辰", "未卯", "申寅", "酉丑", "戌子", "亥亥"
                strOut = strOut & "," & "血刃"
        End Select
        Select Case strKey
            Case "子寅", "丑寅", "寅巳", "卯巳", "辰巳", "己申", "午申", "未申", "申亥", "酉亥", "戌亥", "亥寅"
                strOut = strOut & "," & "孤神"
        End Select
        Select Case strKey
            Case "子戌", "丑戌", "寅丑", "卯丑", "辰丑", "己辰", "午辰", "未辰", "申未", "酉未", "戌未", "亥戌"
                strOut = strOut & "," & "寡宿"
        End Select
        Select Case strKey
            Case "子寅", "丑卯", "寅辰", "卯巳", "辰午", "己未", "午申", "未酉", "申戌", "酉亥", "戌子", "亥丑"
                strOut = strOut & "," & "喪門"
        End Select
        Select Case strKey
            Case "子戌", "丑亥", "寅子", "卯丑", "辰寅", "己卯", "午辰", "未巳", "申午", "酉未", "戌申", "亥酉"
                strOut = strOut & "," & "弔客"
        End Select
        Select Case strKey
            Case "子巳", "丑寅", "寅亥", "卯申", "辰巳", "己寅", "午亥", "未申", "申巳", "酉寅", "戌亥", "亥申"
                strOut = strOut & "," & "劫煞"
        End Select
        Select Case strKey
            Case "子戌", "丑酉", "寅申", "卯未", "辰午", "己巳", "午辰", "未卯", "申寅", "酉丑", "戌子", "亥亥"
                strOut = strOut & "," & "血支"
        End Select
        Select Case strKey
            Case "子巳", "丑丑", "寅酉", "卯巳", "辰丑", "己酉", "午巳", "未丑", "申酉", "酉巳", "戌丑", "亥酉"
                strOut = strOut & "," & "太白凶星"
        End Select

        If Left(strOut, 1) = "," Then
            strOut = Mid(strOut, 2)
        End If

        '=================== end ==================
        FindStarSky = strOut
        '巳戌巳戌寅戌寅戌酉寅子辰子 丑丙丁寅
        '丑酉寅亥卯戌寅酉午亥酉丑丑 未甲申卯
        '酉申亥子辰丑巳申卯申午戌寅 寅壬壬辰
        '巳未申丑巳丑巳未子巳卯未卯 申庚辛巳
        '丑午巳寅午丑巳午酉寅子辰辰 卯丙亥午
        '酉巳寅卯未辰申巳午亥酉丑己 酉甲甲未
        '巳辰亥辰申辰申辰卯申午戌午 辰壬癸申
        '丑卯申巳酉辰申卯子巳卯未未 戌庚寅酉
        '酉寅巳午戌未亥寅酉寅子辰申 巳丙丙戌
        '巳丑寅未亥未亥丑午亥酉丑酉 亥甲乙亥
        '丑子亥申子未亥子卯申午戌戌 午壬己子
        '酉亥申酉丑戌寅亥子巳卯未亥 子庚庚丑
    End Function
    Public Function To12Hour(ByVal In24Hour As Byte) As Byte
        If (In24Hour Mod 2) = 0 Then
            '偶數
            To12Hour = In24Hour
        Else
            '奇數
            To12Hour = In24Hour + 1
        End If
    End Function

    '整數轉換成中文數字, 至多3位數, 不含百與十
    Public Function ToChNo(ByVal IntNo As Integer) As String
        Dim strChNoAll As String() = {"零", "一", "二", "三", "四", "五", "六", "七", "八", "九"}
        Dim Dvr As Integer
        Dim work As Integer
        Dim AnsStr As String = ""
        Dim ThreeNum As Boolean = False
        work = IntNo
        If work > 100 Then
            ThreeNum = True
            Dvr = work \ 100
            work = work - (Dvr * 100)
            AnsStr = AnsStr & strChNoAll(Dvr)
        End If
        If ThreeNum Then
            If work > 10 Then
                Dvr = work \ 10
                work = work - (Dvr * 10)
                AnsStr = AnsStr & strChNoAll(Dvr)
            Else
                AnsStr = AnsStr & strChNoAll(0)
            End If
        Else
            'only two digit
            If work > 10 Then
                Dvr = work \ 10
                work = work - (Dvr * 10)
                AnsStr = AnsStr & strChNoAll(Dvr)
            End If
        End If
        AnsStr = AnsStr & strChNoAll(work)
        ToChNo = AnsStr
    End Function
    Public Function ToChineseNo(ByVal IntNo As Integer) As String
        Dim strChNoAll As String() = {"零", "一", "二", "三", "四", "五", "六", "七", "八", "九", "十", "百"}
        Dim Dvr As Integer
        Dim work As Integer
        Dim AnsStr As String = ""
        Dim ThreeNum As Boolean = False
        work = IntNo
        If work > 100 Then
            ThreeNum = True
            Dvr = work \ 100
            work = work - (Dvr * 100)
            AnsStr = AnsStr & strChNoAll(Dvr) & strChNoAll(11)
        End If
        If ThreeNum Then
            If work > 10 Then
                Dvr = work \ 10
                work = work - (Dvr * 10)
                AnsStr = AnsStr & strChNoAll(Dvr) & strChNoAll(10)
            Else
                AnsStr = AnsStr & strChNoAll(0)
            End If
        Else
            'only two digit
            If work > 10 Then
                Dvr = work \ 10
                work = work - (Dvr * 10)
                If (Dvr > 1) Then
                    AnsStr = AnsStr & strChNoAll(Dvr) & strChNoAll(10)
                Else
                    AnsStr = AnsStr & strChNoAll(10)
                End If
            End If
        End If
        AnsStr = AnsStr & strChNoAll(work)
        ToChineseNo = AnsStr
    End Function

    '檢查三合三會, 輸入為地支四字
    ' Input : InString 為地支四字
    ' Function return 是不是三合或三會 (False/True)
    ' Extra return:
    '         ReCode = 0  : 輸入有非地支之字
    '                = 1  : 輸入無錯誤
    '                = 21,22,23,24 : 三合水,火,木,金
    '                = 31,32,33,34 : 三會水,火,木,金
    '                = 4  : 沖破
    Public Function Is3Group(ByVal InString As String, _
                            ByRef ReCode As Integer, _
                            ByRef GroupType As String, _
                            ByRef Group5Sing As String) As Boolean
        '三合
        Dim Gstring21 As String = "申子辰"
        Dim Gstring22 As String = "寅午戌"
        Dim Gstring23 As String = "亥卯未"
        Dim Gstring24 As String = "巳酉丑"
        '三會
        Dim Gstring31 As String = "亥子丑"
        Dim Gstring32 As String = "巳午未"
        Dim Gstring33 As String = "寅卯辰"
        Dim Gstring34 As String = "申酉戌"

        '檢查輸入
        For i As Integer = 1 To Len(InString)
            If IsInTsu(Mid(InString, i, 1)) = False Then
                ReCode = 0 : GroupType = "" : Group5Sing = ""
                Is3Group = False
                Exit Function
            End If
        Next
        '----------------
        ReCode = 1 : GroupType = "" : Group5Sing = ""
        Is3Group = False
        'check 21 -------  "申子辰"
        If InStr(InString, Mid(Gstring21, 1, 1)) Then  '有第一字
            If InStr(InString, Mid(Gstring21, 2, 1)) Then  '有第二字
                If InStr(InString, Mid(Gstring21, 3, 1)) Then  '有第三字
                    GroupType = "申子辰" : Group5Sing = "水"
                    '是否沖破?
                    If InStr(InString, Mid(Gstring22, 1, 1)) Or _
                        InStr(InString, Mid(Gstring22, 2, 1)) Or _
                        InStr(InString, Mid(Gstring22, 3, 1)) Then
                        ReCode = 4
                        Is3Group = False
                    Else
                        ReCode = 21
                        Is3Group = True
                    End If  '沖破
                End If   '3
            End If    '2
        End If    '1
        'check 22 ------- "寅午戌"
        If InStr(InString, Mid(Gstring22, 1, 1)) Then  '有第一字
            If InStr(InString, Mid(Gstring22, 2, 1)) Then  '有第二字
                If InStr(InString, Mid(Gstring22, 3, 1)) Then  '有第三字
                    GroupType = "寅午戌" : Group5Sing = "火"
                    '是否沖破?
                    If InStr(InString, Mid(Gstring21, 1, 1)) Or _
                        InStr(InString, Mid(Gstring21, 2, 1)) Or _
                        InStr(InString, Mid(Gstring21, 3, 1)) Then
                        ReCode = 4
                        Is3Group = False
                    Else
                        ReCode = 22
                        Is3Group = True
                    End If  '沖破
                End If   '3
            End If    '2
        End If    '1
        'check 23 -------  "亥卯未"
        If InStr(InString, Mid(Gstring23, 1, 1)) Then  '有第一字
            If InStr(InString, Mid(Gstring23, 2, 1)) Then  '有第二字
                If InStr(InString, Mid(Gstring23, 3, 1)) Then  '有第三字
                    GroupType = "亥卯未" : Group5Sing = "木"
                    '是否沖破?
                    If InStr(InString, Mid(Gstring24, 1, 1)) Or _
                        InStr(InString, Mid(Gstring24, 2, 1)) Or _
                        InStr(InString, Mid(Gstring24, 3, 1)) Then
                        ReCode = 4
                        Is3Group = False
                    Else
                        ReCode = 23
                        Is3Group = True
                    End If  '沖破
                End If   '3
            End If    '2
        End If    '1
        'check 24 ------- "巳酉丑"
        If InStr(InString, Mid(Gstring24, 1, 1)) Then  '有第一字
            If InStr(InString, Mid(Gstring24, 2, 1)) Then  '有第二字
                If InStr(InString, Mid(Gstring24, 3, 1)) Then  '有第三字
                    GroupType = "巳酉丑" : Group5Sing = "金"
                    '是否沖破?
                    If InStr(InString, Mid(Gstring23, 1, 1)) Or _
                        InStr(InString, Mid(Gstring23, 2, 1)) Or _
                        InStr(InString, Mid(Gstring23, 3, 1)) Then
                        ReCode = 4
                        Is3Group = False
                    Else
                        ReCode = 24
                        Is3Group = True
                    End If  '沖破
                End If   '3
            End If    '2
        End If    '1
        '===========================
        'check 31 -------  "亥子丑"
        If InStr(InString, Mid(Gstring31, 1, 1)) Then  '有第一字
            If InStr(InString, Mid(Gstring31, 2, 1)) Then  '有第二字
                If InStr(InString, Mid(Gstring31, 3, 1)) Then  '有第三字
                    GroupType = "亥子丑" : Group5Sing = "水"
                    '是否沖破?
                    If InStr(InString, Mid(Gstring32, 1, 1)) Or _
                        InStr(InString, Mid(Gstring32, 2, 1)) Or _
                        InStr(InString, Mid(Gstring32, 3, 1)) Then
                        ReCode = 4
                        Is3Group = False
                    Else
                        ReCode = 31
                        Is3Group = True
                    End If  '沖破
                End If   '3
            End If    '2
        End If    '1
        'check 32 ------- "巳午未"
        If InStr(InString, Mid(Gstring32, 1, 1)) Then  '有第一字
            If InStr(InString, Mid(Gstring32, 2, 1)) Then  '有第二字
                If InStr(InString, Mid(Gstring32, 3, 1)) Then  '有第三字
                    GroupType = "巳午未" : Group5Sing = "火"
                    '是否沖破?
                    If InStr(InString, Mid(Gstring31, 1, 1)) Or _
                        InStr(InString, Mid(Gstring31, 2, 1)) Or _
                        InStr(InString, Mid(Gstring31, 3, 1)) Then
                        ReCode = 4
                        Is3Group = False
                    Else
                        ReCode = 32
                        Is3Group = True
                    End If  '沖破
                End If   '3
            End If    '2
        End If    '1
        'check 33 -------  "寅卯辰"
        If InStr(InString, Mid(Gstring33, 1, 1)) Then  '有第一字
            If InStr(InString, Mid(Gstring33, 2, 1)) Then  '有第二字
                If InStr(InString, Mid(Gstring33, 3, 1)) Then  '有第三字
                    GroupType = "寅卯辰" : Group5Sing = "木"
                    '是否沖破?
                    If InStr(InString, Mid(Gstring34, 1, 1)) Or _
                        InStr(InString, Mid(Gstring34, 2, 1)) Or _
                        InStr(InString, Mid(Gstring34, 3, 1)) Then
                        ReCode = 4
                        Is3Group = False
                    Else
                        ReCode = 33
                        Is3Group = True
                    End If  '沖破
                End If   '3
            End If    '2
        End If    '1
        'check 34 ------- "申酉戌"
        If InStr(InString, Mid(Gstring34, 1, 1)) Then  '有第一字
            If InStr(InString, Mid(Gstring34, 2, 1)) Then  '有第二字
                If InStr(InString, Mid(Gstring34, 3, 1)) Then  '有第三字
                    GroupType = "申酉戌" : Group5Sing = "金"
                    '是否沖破?
                    If InStr(InString, Mid(Gstring33, 1, 1)) Or _
                        InStr(InString, Mid(Gstring33, 2, 1)) Or _
                        InStr(InString, Mid(Gstring33, 3, 1)) Then
                        ReCode = 4
                        Is3Group = False
                    Else
                        ReCode = 34
                        Is3Group = True
                    End If  '沖破
                End If   '3
            End If    '2
        End If    '1
    End Function

    '求空亡
    Public Function FindEmpty(ByVal str干 As String, ByVal str支 As String) As String
        Select Case str干 & str支
            Case "甲子", "乙丑", "丙寅", "丁卯", "戊辰", "己巳", "庚午", "辛未", "壬申", "癸酉"
                FindEmpty = "戌亥"
            Case "甲戌", "乙亥", "丙子", "丁丑", "戊寅", "己卯", "庚辰", "辛巳", "壬午", "癸未"
                FindEmpty = "申酉"
            Case "甲申", "乙酉", "丙戌", "丁亥", "戊子", "己丑", "庚寅", "辛卯", "壬辰", "癸巳"
                FindEmpty = "午未"
            Case "甲午", "乙未", "丙申", "丁酉", "戊戌", "己亥", "庚子", "辛丑", "壬寅", "癸卯"
                FindEmpty = "辰巳"
            Case "甲辰", "乙巳", "丙午", "丁未", "戊申", "己酉", "庚戌", "辛亥", "壬子", "癸丑"
                FindEmpty = "寅卯"
            Case "甲寅", "乙卯", "丙辰", "丁巳", "戊午", "己未", "庚申", "辛酉", "壬戌", "癸亥"
                FindEmpty = "子丑"
            Case Else
                FindEmpty = "錯誤"
        End Select
    End Function

    '流年批示
    Public Function FindYearsNote(ByVal Gan10God As String, ByVal Tsu10God As String, ByRef YearsNote2 As String) As String
        Dim S1 As String = ""
        Dim S2 As String = ""
        Select Case Gan10God
            Case "比肩"  '---------------------------------------
                Select Case Tsu10God
                    Case "比肩"
                        S1 = "平吉。無主權、多爲昆仲朋友而盡力、父緣薄，有死別之象，多者，自我孤獨成癖，男命多遲婚。"
                        S2 = "(千里風雲培玉樹、十分雨露發荊化)。對敵多、阻害、諸事難如意、三思而行。"
                    Case "劫財"
                        S1 = "大凶。本身爲親反蒙受損害，或合作事業中逢解散，兄弟多齟齬，或與妻子不和。"
                        S2 = "(路逢蜀道而經險、水入黃河便不清)。多煩、憂悶、周轉不靈、從事反逆、遭遇不幸。"
                    Case "食神"
                        S1 = "大吉兆。萬事順利、食祿豐厚，有創業之才。"
                        S2 = "(雲收自然星月朗、春回依舊草花香)。勤儉、努力、保持經濟、貴人提拔、進展鴻運。"
                    Case "傷官"
                        S1 = "不吉。助人不利反被忌棄，為金錢諸事擾親友，婚後恐家庭風波不絕。"
                        S2 = "(夜色似年難得曉、燈光如豆不成紅)。家庭不和、勞而無功、小人之災、破財、婚姻變態。"
                    Case "正財"
                        S1 = "大吉。得財力、娶良妻、諸事順利。"
                        S2 = "(梅知運到添春色、鳥覺時來報好音)。利潤、勞碌、多忙、戀愛、結婚、外有利益。"
                    Case "偏財"
                        S1 = "不吉。因父或情婦之故，在營業上多發生紛擾，且有疾病及色情之災。"
                        S2 = "(昨夜不知何處雨、今朝花開損精神)。多事多難、舊守即安、不幸病危、色情、煩惱。"
                    Case "正官"
                        S1 = "半吉凶。爭權奪利，是非滋生不絕，但主正論得人信仰。"
                        S2 = "(雷鳴大地聲千里、霞繞千山色萬里)。爭鬥、競選、合夥、其他變動之機、考慮而行。"
                    Case "七殺"
                        S1 = "大凶。ㄧ生勞心，昆仲朋友反目多煩惱，更有盜詐、或患病之災，若為女命則多代夫勞苦。"
                        S2 = "(夜雨無情驚醒夢、西風有意折飛花)。口舌、多端、災難、被騙、破財、反睦、多磨。"
                    Case "正印"
                        S1 = "大吉。經商或經營副業、副業可獲利，且能得貴人扶持，日趨繁榮。"
                        S2 = "(人逢美事精神爽、月到中天分外明)。求謀順調、兄朋幸運、貴人提拔、獲得利潤。"
                    Case "偏印"
                        S1 = "大凶。勞苦損失，枉費心機、萬事不順，居無定所，工作更無固定崗位。"
                        S2 = "(荒江葉亂露初冷、獨槕孤舟夜上流)。勿擔保作證、不可貪、守舊事須細慮、反被陷害。"
                End Select
            Case "劫財"  '---------------------------------------
                Select Case Tsu10God
                    Case "比肩"
                        S1 = "不吉。家事、婚事不順，為兄弟親友勞苦，若為女命，則為丈夫品性不正而煩惱。"
                        S2 = "(東風有意添楊柳、西風無情損海棠)。意見不和、被人連累、破財、失意、多端、病疾。"
                    Case "劫財"
                        S1 = "大凶。父非早亡即病疾纏綿床弟日，合作事業必遭解散，謀事難成，男命尤凶、剋妻或婚變。"
                        S2 = "(流水有情歸故澗、夕陽無話下空停)。變動、家庭風波、事業分離、口角、反睦、病疾。"
                    Case "食神"
                        S1 = "大吉。有貴人扶持，可得意外之財，物質享受豐富，但女命恐有二次結婚芳獲得幸福。"
                        S2 = "花開月朗風光好、人樂家安喜氣濃)。幸運連來、事事如意、昇進、注意週到、保持品行。"
                    Case "傷官"
                        S1 = "大凶。品行不端、枉法犯紀，身敗名裂，遺恨終身，在時柱則必傷妻損兒。"
                        S2 = "(分明月在梅枝上、尋到梅遂月又無)。吉凶參平、凡事忍讓、不可衝突、耐勞苦、保信用。"
                    Case "正財"
                        S1 = " 大凶。剋妻、遭詐欺、破財或多為朋友事而煩惱。"
                        S2 = "(巫江水亦鳴孤憤、蜀道山多帶不平)。人情似鬼、防小人計、病危、別離、孝服留意。"
                    Case "偏財"
                        S1 = "大凶。破財不能積蓄，再婚或因妾而散盡家財，女命則敗財不貞。"
                        S2 = "(雲際樵子速歸路、水大漁翁失釣磯)。周轉不靈、事業失敗、交加、美人關、先凶後吉。"
                    Case "正官"
                        S1 = "吉凶、浮沉。雖能發達，但與部屬不和，諸多不如意，並有免職、疾病、破財，若女命有被夫凌虐之苦。"
                        S2 = "(漫清碧桃開口笑、須知綠柳帶愁根)。忍氣和財、怒氣破財、尊從人意、過勞致病。"
                    Case "七殺"
                        S1 = "浮沉、多成多敗，雖俱才能，惜不得遇，卽成功亦敗北。"
                        S2 = "(順風江上滿揚帆、不料江頭有石灘)。欺騙、合作被離、進財、守吉、事應忍耐、候時。"
                    Case "正印"
                        S1 = "不吉。表面繁榮、內裏空虛，獲小失大，被冷落，得部屬助力而好轉。"
                        S2 = "(好似曉雲初出關、恰為江日正東昇)。一事急水、周轉用術、合作不利、以柔待人。"
                    Case "偏印"
                        S1 = "大凶。職業不穩定、生活不安定，劫財過多、內心冷漠無情，易招妻子離去。"
                        S2 = "(分明似喜非為喜、恍惚聞香不是香)。不三不四、須是謹慎、交易細慮、恍惚失敗。"
                End Select
            Case "食神"  '---------------------------------------
                Select Case Tsu10God
                    Case "比肩"
                        S1 = "吉兆。可成富家之養子，又俱有經濟才能，事是可貴人助。"
                        S2 = "(何須執意求佳景、自有奇逢應早春)。大路行車、貴人提拔、人氣極旺、結婚有益。"
                    Case "劫財"
                        S1 = "吉。愈凶而可從凶中獲利，遭破壞反得財力，得繼承之命。"
                        S2 = "(燕子支飛繞晝堂、春風幾度斷肝腸)。風雨交襲、樂極生悲、勞心苦戰、勿貪、舊守。"
                    Case "食神"
                        S1 = "吉。福祿豐厚、共同事業可獲大利，但不宜任官吏，恐生是非。"
                        S2 = "(浮雲捲盡碧天空、春風融合瑞氣多)。財喜雙全、合作事多、利潤可觀、須事三思。"
                    Case "傷官"
                        S1 = "半吉。雖可發達，但中運有障礙，且會為子女、配偶事起苦惱。"
                        S2 = "(滿面春風人運好、多少不足自家和)。天官賜福、轉變、昇級、但無益處、家庭風波。"
                    Case "正財"
                        S1 = "吉。蒙長輩或長官之愛護，而獲利，福份甚大。"
                        S2 = "(掃開天上雲千丈、捧出波心月ㄧ輪)。蜻蜓出網、拾舊就新、保持信用、後有果得。"
                    Case "偏財"
                        S1 = "大富之兆，得財帛，上下親愛，艷福佳，凡事進取有功。"
                        S2 = "(五風十雨皆為瑞、萬紫千紅總是春)。添丁發財、買賣利潤、任事而行、注意對方。"
                    Case "正官"
                        S1 = "吉。操行端正、安分守己，獲眾人信任、福力日增，女可配良夫得幸福。"
                        S2 = "(芳草春回依舊綠、梅花時到自然香)。一石二鳥、如意、機會、戀愛成功、順調、發達。"
                    Case "七殺"
                        S1 = "凶。易生禍殃諸事不順，半世為奴婢，勤勉、ㄧ生勞碌窮困、且易怒、脾氣不佳。"
                        S2 = "(衡門之下可棲遲、今日徒勞枉費思)。無中生有、凶多吉少、反睦衝突、不滿事端。"
                    Case "正印"
                        S1 = "吉。性誠實，信用好，得貴人支持，業務繁榮、順利發達。"
                        S2 = "(樓閣重重明月滿、蘭干曲曲好花扶)。一帆風順、貴人引進、財益、幸運、希望可達。"
                    Case "偏印"
                        S1 = "凶。紛爭、損害、疾病多災。(要養岳家人)"
                        S2 = "(雲深有月光難見、海闊有風浪自生)。馬渡竹橋、不吉利、破財、離散、病厄、不和。"
                End Select
            Case "傷官"  '---------------------------------------
                Select Case Tsu10God
                    Case "比肩"
                        S1 = "不吉。自招家庭不和，親屬不睦，婚姻不順，婚後亦生口舌。"
                        S2 = "(石燕拂雲晴亦雨、江豚吹浪海還風)。事有生煩、坐立不安、不睦、苦惱、心憂。"
                    Case "劫財"
                        S1 = "不吉。娶妻以財為對象，為人心胸橫霸，太勢利。有破財、離別親人、身心勞苦、合作事業恐解散。"
                        S2 = "(花枝零落怕東風、秋雨吹殘滿地中)。一場困難、安分守己、做保連累、破財。"
                    Case "食神"
                        S1 = "與長輩意見衝突，但卻受漲倍之影響而獲利。"
                        S2 = "(六陰極處水凝冰、造物分明未有形)。吉凶參半、不可自作聰明、以和待人。"
                    Case "傷官"
                        S1 = "凶。有痼疾短命之兆，生涯勞碌不絕，卽富貴亦不長久，女命財被丈夫冷落，再日祝則被妻及其人輕視。"
                        S2 = "(紅顏命薄ㄧ樹花、春風已抱曲琵琶)。半天ㄧ箭、失業、失職、失戀、被害、破財。"
                    Case "正財"
                        S1 = "他人破財，反可成全自己之利，又得妻助。"
                        S2 = "(良馬始因離路險、梅花今已發陽春)。半途走馬、內助之蔭、須事三思、美景重來。"
                    Case "偏財"
                        S1 = "可理財，能騰達，但精神不爽，宜慎戒色情之禍。"
                        S2 = "(曉望山前霄色開、俄然風起雨又來)。無風生浪、多勞風波、色情、不如意。"
                    Case "正官"
                        S1 = "凶。有惡作劇及諷刺罵人之惡癖，萬事受阻，得而後失，夫妻別離。"
                        S2 = "(謀要未通不如意、行船又遇打頭風)。愁眉難展、未得順調、被害、衝突、變動、病危。"
                    Case "七殺"
                        S1 = "凶。終身辛苦，有恩亦無功，且遭人非議、毀謗甚至含冤莫白、夫妻別離之兆。"
                        S2 = "(一池新水今朝雨、滿地桃花昨夜風)。半天下雨、誹謗盡端、官訟連累、須防小人。"
                    Case "正印"
                        S1 = "長壽，且多得人助。"
                        S2 = "(盼附東君好護持、奇花遭雨壞籬坡)。假戲、假風、勞苦、不和、住地變動、困難。"
                    Case "偏印"
                        S1 = "本業成功則副業失敗，或長輩之失敗，累至自己之破滅。"
                        S2 = "(多因風雨花零落、領渡清溪欠便船)。蟲入目中、破兆、考慮損利、反睦、婚變。"
                End Select
            Case "正財"  '---------------------------------------
                Select Case Tsu10God
                    Case "比肩"
                        S1 = "吉。得財之象，財緣甚佳，有女人緣，雖有兄弟及親友之拖累，但大體順遂，又有分廠分店之暗示。"
                        S2 = "(水滿池塘花滿枝、青鮮葉裡自聞香)。無中生有、幫助者多、轉變、財益、勿保證。"
                    Case "劫財"
                        S1 = "不吉。父早亡或父業衰微。"
                        S2 = "(乍雨乍晴春不定、花開花落雨無情)。大有奇觀、交易小心、被騙、破財、婚變、反睦。"
                    Case "食神"
                        S1 = "吉。得妻助力，子嗣賢孝、幸福發達之兆。"
                        S2 = "(天賜禎祥納慶多、花前月下聽高歌)。十分清泰、好機會、應時進行、大有利益。"
                    Case "傷官"
                        S1 = "半吉凶。他人失敗，損失、反為自已獲利，外表似衰微，內實充足。但因浮沉轉變多，家運不吉。"
                        S2 = "(雖有浮雲掩月光、僬然風捲雲收藏)。以石似虎、家庭風波、警戒、忍耐、古物商有利。"
                    Case "正財"
                        S1 = "吉。商業興隆，信用好，財源茂盛，但有剋母之兆，男命婚姻美滿。女命則子息稀。"
                        S2 = "(南方轉過北方地、一生衣祿永無憂)。七古八怪、被騙破財、倒閉、生別、相剋。"
                    Case "偏財"
                        S1 = "平吉。利益雖多 開支亦繁。不滿現狀，兼管數業，徒增心勞、切記色情之災。"
                        S2 = "(走遍江山千萬里、徒勞心事想東西)。愁思不安、勞而無助、多忙無益、閒事勿管。"
                    Case "正官"
                        S1 = "吉。本性良德，喜獲眾望，蒙上提攜、得下援助，職位高升、獲取財利，女命尤甚、必配佳婿。"
                        S2 = "(筆走龍蛇高萬丈、一舉揚名壓富豪)。家門光彩、利益權利、地位皆好、財喜雙進。"
                    Case "七殺"
                        S1 = "吉。恩澤於下，所費雖多，但從商可獲巨利，女命尤吉。"
                        S2 = "(五行秀氣色生財、天光霄月樂悠悠)。宜人之景、有外財、女出資、男經營有利益。"
                    Case "正印"
                        S1 = "凶。志願難達，與人競爭易招禍，母妻不睦忍耐解之，切記勿貪圖非份之財，安份守己。"
                        S2 = "(釣得魚兒已上鉤、俄然躍起水中遊)。一場困苦、須事檢討、失敗、損利、損精神。"
                    Case "偏印"
                        S1 = "半吉凶。欠耐力、缺誠實，但廣交、人緣佳，可從事副業。"
                        S2 = "(有祿有財過晚景、無憂無慮樂升平)。春光換彩、冒利事業可受人提拔成功。"
                End Select
            Case "偏財"  '---------------------------------------
                Select Case Tsu10God
                    Case "比肩"
                        S1 = "不吉。父飄零，難開運、終死異鄉，與父不睦或父有疾病，若繼承祖業，必有紛爭，男命易生色情風波，若原命有偏官出干者不剋父。"
                        S2 = "(正駕高帆遊大海、忽然ㄧ陣打頭風)。順中反逆、勞費、暗箭、迫害、破財、欺騙。"
                    Case "劫財"
                        S1 = "凶。陽干之偏財坐劫財者，父必破財，男命則為色情招致家庭紛擾，女命則有婚姻突變之兆，財破人奪。"
                        S2 = "(七夕有雲天淡淡、中秋無月夜朦朦)。 須事謹慎、美人關、病危、變緣、不利。"
                    Case "食神"
                        S1 = "吉。財遇食神增福力，多利多益，為官宦運享通，商務興隆。"
                        S2 = "(時來與發如川至、運到財源似水流)。大有良機、得利、名譽、希望到達、平安。"
                    Case "傷官"
                        S1 = "吉。他人失敗、反變為自己之利。"
                        S2 = "(財源好似春江水、滾滾流來日夜長)。左作右中、財益利潤、注意桃花、醫術繁榮。"
                    Case "正財"
                        S1 = "吉。經營商工業可獲厚利，並有兼營二種職業之傾向，但原局若見偏財、正財，則只外表盛大、內心空虛。"
                        S2 = "(改換門閭事更新、錦衣玉食福乃臻)。人形光彩、變動、事業廣振、節約開支。"
                    Case "偏財"
                        S1 = "吉。富有經濟手腕，發達於他鄉，尤其月干欠偏財更是。"
                        S2 = "(財如春水源源進、福似朝花朵朵新)。順水推舟、投機、新規有利、結婚美滿。"
                    Case "正官"
                        S1 = "吉。本性良德，喜獲眾望，蒙上提攜、得下援助，職位高升、獲取財利，女命尤甚、必配佳婿。"
                        S2 = "(嫩逐出支風勢惡、全憑老竹傍其身)。光天化下、分居、離鄉、從人意、衝突、保守。"
                    Case "七殺"
                        S1 = "經商之業可獲利，但辛勞備至，但為女人而散財，若為女命則有婚配錯誤，招致再嫁之慮。"
                        S2 = "(牡丹原是百花王，開向人間分外香)。大有餘慶、榮轉、工商多利、發達調順。"
                    Case "正印"
                        S1 = "半吉。天賜大福，享受平生。"
                        S2 = "(數注紫衣人指人，財祿功名自有期)。人事利達、行而三思、合不利、不可急、安泰。"
                    Case "偏印"
                        S1 = "凶。能得小利，但勞苦不絕，且易受他人拖累而受損失，不得父愛，反與伯父投機。"
                        S2 = "(靜坐有經清白日，閒來無事伴紅塵)。守之即安、動不利、換機會、被劫、反背。"
                End Select
            Case "正官"  '---------------------------------------
                Select Case Tsu10God
                    Case "比肩"
                        S1 = "吉。性端莊嚴，有手領之器，可承繼祖業。若為女命屬良婦，配賢婿，且助夫成家，但稍有凌夫之嫌。"
                        S2 = "(須事三思而後行、不可自作誤聰明)。心上心下、暗箭當射、勿管閒事、守靜。"
                    Case "劫財"
                        S1 = "不吉。兄弟不睦，有被人連累受害及色情之災。"
                        S2 = "(不料雲來蔽明月、誰知冷雪透衣襟)。半天ㄧ箭、失敗、勿保、色難、破財。"
                    Case "食神"
                        S1 = "吉。信用好，上下和睦，子必顯榮，女命可獲美滿良緣。"
                        S2 = "(旱年發財如飲水、中年得利似雲鳴)。十分美景、財益名譽多、防有惹火燒身危。"
                    Case "傷官"
                        S1 = "凶。被小人傷害，配偶病弱，男命與妻子別離、女命防夫、不得寵。"
                        S2 = "(南樓惟報三更月、半夜子規尚且啼)。人情反覆、反對者多、夫妻反睦、災難、病危失業。"
                    Case "正財"
                        S1 = "吉。信譽佳，社會上可得崇位，物質享受豐裕。"
                        S2 = "(時難運轉人得爽、處處春風草不榮)。事應三思、好機歪曲、禍胎、勿嬌、守信。"
                    Case "偏財"
                        S1 = "吉。信用好，得長輩扶持，經營工商昌榮發達。"
                        S2 = "(克難克儉稱淑女、內助持家數有方)。天光化日、愛強求事、忽然得福、出行有利。"
                    Case "正官"
                        S1 = "男命大吉，女命不吉，有婚變再嫁之嫌。"
                        S2 = "(貴並劉寬富石崇、富貴能有幾人閒)。利人利己、名譽、地位、尊敬、達成、幸運。"
                    Case "七殺"
                        S1 = "凶。少逸多勞，遭人排斥，誹謗、災異不絕、易陷迷惘，而多損害，女命尤凶，有色情之災。"
                        S2 = "(山高水深多險處、果有舟車不易行)。守己安分、自重、守舊、不測、官祿、災危。"
                    Case "正印"
                        S1 = "吉。名利亨通，可成就偉業，名利雙收之象。"
                        S2 = "(橘井泉香丹現色、杏花春暖玉生輝)。良辰美景、名譽、昇格、財益、行進。"
                    Case "偏印"
                        S1 = "凶。代人謀事不成，反受打擊，工商業失利，擔任公職或靜態之學術機構，能揚名獲利。"
                        S2 = "(老之將至君不知、有災有難侵人危)。曲路行車、無功、曲折、變動、災難、不知。"
                End Select
            Case "七殺"  '---------------------------------------
                Select Case Tsu10God
                    Case "比肩"
                        S1 = "不吉。為昆仲朋友招損失，又暗示有陷盜被奪、詐謀不和、疾病。若為女命，為夫招惹痛苦。"
                        S2 = "(叩門聲急是非多，一見官非病又難)。人形似鬼、不測風雲、爭事、官惹、疾病。"
                    Case "劫財"
                        S1 = "平庸。能獲權柄，但為俠義而招破損，又夫妻之間苦情甚多，女命有再婚之虞。"
                        S2 = "(天上紫燕兩分飛、夫君相隨命先歸)。平地風波、變動、移徒、改變、變緣、婚破。"
                    Case "食神"
                        S1 = "平靜安穩之命，但經商多成敗，女命婚姻不順。"
                        S2 = "(一朝煙雨一朝晴、晴不多時雨又淋)。半晴半雨、暗害、損益、勞苦、失職、病危。"
                    Case "傷官"
                        S1 = "凶。為長輩勞苦且受他人連累而受災，有破家之兆。"
                        S2 = "(風雨凄凄正愁人、窗前寂寞有難知)。半天下雨、連累、破產、錯誤、失敗、失意。"
                    Case "正財"
                        S1 = "吉。商工業順利，男命得佳兒，女得夫寵。"
                        S2 = "(黃金久淹在泥沙、有日開藏世共誇)。見有食無、外觀好內虛、忍耐候機會、守舊。"
                    Case "偏財"
                        S1 = "父緣薄，凡事不遂心，為金錢而失信用。"
                        S2 = "(ㄧ日愁來百事多、心中煩惱亂如麻)。愁腸百結、色難、職業變動、麻煩事多。"
                    Case "正官"
                        S1 = "凶。所謂『露殺坐官、禍端最大』。表面不錯，內心空虛、逢事多愁，缺乏決斷力、女命甚尤。"
                        S2 = "(玉兔催人投宿處、金雞喚客東行裝)。悲喜交集、迷惘、誘惑、失財、苦澀。"
                    Case "七殺"
                        S1 = "凶。進退不利，諸事苦惱，為子女辛勞，憂苦不絕，女命必受夫累，且翁姑緣薄。"
                        S2 = "(如花開時便凋殘、恨殺無情風水催)。風雲蔽日、身病、迫害、過失、防止未然。"
                    Case "正印"
                        S1 = "吉。業務茂盛，女命得佳偶，得翁姑寵愛。"
                        S2 = "(無端夜雨迷秋月、不意狂風折好花)。氣薪維新、多事、多磨、須事三思、求和平。"
                    Case "偏印"
                        S1 = "平庸。行商異鄉，苦樂盡嘗，易抗上，女命凶論。"
                        S2 = "(飛符為患又為災、啾唧無端水破財)。木之經霜、週轉不靈、內有病苦、喪事。"
                End Select
            Case "正印"  '---------------------------------------
                Select Case Tsu10God
                    Case "比肩"
                        S1 = "吉。對兄弟朋友善於盡力，萬事順利發達，但利潤有限。"
                        S2 = "(後運正與家業盛、風高月明樂悠悠)。平運走馬、上半年不宜、下半年轉好、計謀而行。"
                    Case "劫財"
                        S1 = "吉。業務興隆，雖因慈善而為親朋易招損失，但無妨。"
                        S2 = "(夫妻本是同林鳥、宿在林中離去光)。一事急水、生煩、注重、從意、勿急行事。"
                    Case "食神"
                        S1 = "吉。受人尊敬、信任，得提拔高昇，業務繁榮大獲財利。"
                        S2 = "(財源交接聲名重、家業興隆氣象新)。大有餘慶、廣大、利潤、信用、如意吉祥。"
                    Case "傷官"
                        S1 = "凶。有名敗利破，百事挫折之凶兆、且與母親意見相左。"
                        S2 = "(維持屋裡春風滿、織棉機中夜月寒)。無故生煩、損失、破財、不吉、難順、凶運。"
                    Case "正財"
                        S1 = "凶。諸事難遇良機、疾病、勞苦、金錢缺乏、憂愁又母妻不睦，或母妻ㄧ方因久病而憂慮。"
                        S2 = "(花放隨風靜切切、水流葉落恨汪汪)。一喜一憂、興盡告敗、別離、風波、改革方針。"
                    Case "偏財"
                        S1 = "吉。家庭美滿、業務繁榮、利益豐厚。"
                        S2 = "(南北東西皆享通、往來有利得相從)。川流不息、圓滿、繁榮、順調、快樂、利潤。"
                    Case "正官"
                        S1 = "吉。交友廣闊，為人慈善，正直誠實，信譽佳，逢印有官十有七貴，女命如此丈夫最佳賢內助。"
                        S2 = "(好似花開當午日、猶如嫩柳遇春風)。謀事易機、昇格、新進、得利、技藝成功。"
                    Case "七殺"
                        S1 = "吉。為人慈善正直，信用卓越，勤勉、能忍能讓、家內和睦。印居煞地、煞助仁德。"
                        S2 = "(文質彬彬君子志、趨庭詩禮習儒宗)。財喜雙至、添丁發財、婚事如意、得利良機。"
                    Case "正印"
                        S1 = "不吉。自尊心過強，反遭失敗，欲望高失望大、職業不定，因此勞苦不絕，與子緣薄，女命尤凶。"
                        S2 = "(口唸彌陀不脫塵、在家修道敬鬼神)。一牛兩尾、家口不安、勞苦被害、孝服凶災。"
                    Case "偏印"
                        S1 = "不吉。不滿足ㄧ業、無決斷力、常失敗、多愁善感，子緣薄，女命尤凶。"
                        S2 = "(風漾柳枝無氣力、淡時月影未分明)。一枝三葉、靜待、守舊、勿貪、失利、破財。"
                End Select
            Case "偏印"  '---------------------------------------
                Select Case Tsu10God
                    Case "比肩"
                        S1 = "貴人暗而不明，多為人作養子或有繼母，一生勞碌心酸，常遇波折，經營副業每多損失。"
                        S2 = "(終日持竿不得魚、江中風雨草蔞蔞)。蟲入木中、害身、災難、病難、謹慎、安份。"
                    Case "劫財"
                        S1 = "凶。辛勞不絕，合作事業雙方皆無收獲，婚姻不順。"
                        S2 = "(走過江山又是關、道途跋涉事艱難)。浮雲暗月、恩怨、錯誤、守舊、無功。"
                    Case "食神"
                        S1 = "會受長輩限制，不得自由，進退失據，大成大敗，終破家，若女命有難產之厄，暗示與子女離別。"
                        S2 = "(花開遍雨無顏色、月明遇雲無光亮)。為花失色、刑剋、病弱、誤食、反背、遇害。"
                    Case "傷官"
                        S1 = "凶。經濟不佳生技困難，家庭風波，有家破人散之象，女命則破夫運，又剋子女之慮。"
                        S2 = "(虛名虛利而不成、浪跡萍蹤已半生)。翻來覆去、失職、不利、婚變、病厄。"
                    Case "正財"
                        S1 = "吉。貴人明顯、如奮發努力，能獲長輩提拔，藝術揚名。"
                        S2 = "(龍下碧潭添秀色、虎逢山谷轉精準)。風光自得、提拔、貴人、新規事業、謀事更吉。"
                    Case "偏財"
                        S1 = "可能逃災禍，得平安，萬一不測，亦有人代。"
                        S2 = "(處處雉鳥兩分飛、折斷佳人不濟眉)。美而未良、舊守、麻煩、失戀、失敗、不和、不幸。"
                    Case "正官"
                        S1 = "凶中吉。外表地位權力俱重，但內實則空虛不足，女命常被丈夫傷心，且有分離之兆。"
                        S2 = "(風吹翠竹流珠淚、雪壓梅花帶素冠)。花逢夜攻擊、誹謗、失敗、不和、不幸。"
                    Case "七殺"
                        S1 = "凶。開支多，災少，入不敷出，易被人利用，不但無功，反遭損害，至多成多敗。"
                        S2 = "(當年之曲不堪聞、彈出新聲男有聲)。善中之求、不可強求、終告失敗、守舊、待機。"
                    Case "正印"
                        S1 = "吉中凶。有兼營兩種事業，凡事多迷惑，易招損失。"
                        S2 = "(風出不堪雲淡淡、花開無奈雨淋淋)。一火三煙、困苦事難、勞苦被害、徒勞不安。"
                    Case "偏印"
                        S1 = "生活不固定，易招盜賊、火災、有失權、長期暗疾，每離家鄉外出，辛勞受欺，女命會欺凌丈夫且子息緣薄。"
                        S2 = "(風弄竹聲驚犬吠、月移花影惹雞鳴)。凶多吉少、災難、病苦、產難、死別、不順、失敗。"
                End Select
        End Select
        'final output
        FindYearsNote = S1
        YearsNote2 = S2
    End Function
End Module
