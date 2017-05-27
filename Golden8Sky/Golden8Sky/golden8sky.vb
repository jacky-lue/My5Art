Public Class Golden8sky
    Dim bmpWidth As Integer = 450 '圖形的寬度 
    Dim bmpHeight As Integer = 450 '圖形的高度 
    Dim BMP As Bitmap = New Bitmap(bmpWidth, bmpHeight)
    Dim G As Graphics

    '定位-----------------------------------------------
    Dim mainRec_startX As Integer = 10    '整個範圍
    Dim mainRec_startY As Integer = 10
    Dim mainRec_width As Integer = 425
    '方向箭頭
    Dim arrowTop_X As Integer = mainRec_startX + (mainRec_width / 2)
    Dim arrowTop_Y As Integer = mainRec_startY
    Dim arrowMlength As Integer = 20
    Dim arrow_dx As Integer = 10
    Dim arrow_dy As Integer = 6
    Dim arrowBottom_X As Integer = mainRec_startX + (mainRec_width / 2)
    Dim arrowBottom_Y As Integer = mainRec_startY + mainRec_width
    Dim arrowLeft_X As Integer = mainRec_startX
    Dim arrowLeft_Y As Integer = mainRec_startY + (mainRec_width / 2)
    Dim arrowRight_X As Integer = mainRec_startX + mainRec_width
    Dim arrowRight_Y As Integer = mainRec_startY + (mainRec_width / 2)
    '外圍起第1圓
    Dim CirRec_0_width = 380
    Dim CirRec_0_startX = mainRec_startX + ((mainRec_width - CirRec_0_width) / 2)
    Dim CirRec_0_startY = mainRec_startY + ((mainRec_width - CirRec_0_width) / 2)
    '外圍起第2圓
    Dim CirRec_1_width = 310
    Dim CirRec_1_startX = mainRec_startX + ((mainRec_width - CirRec_1_width) / 2)
    Dim CirRec_1_startY = mainRec_startY + ((mainRec_width - CirRec_1_width) / 2)
    '外圍起第3圓
    Dim CirRec_2_width = 230
    Dim CirRec_2_startX = mainRec_startX + ((mainRec_width - CirRec_2_width) / 2)
    Dim CirRec_2_startY = mainRec_startY + ((mainRec_width - CirRec_2_width) / 2)
    '外圍起第4圓(中心)
    Dim CirRec_3_width = 90
    Dim CirRec_3_startX = mainRec_startX + ((mainRec_width - CirRec_3_width) / 2)
    Dim CirRec_3_startY = mainRec_startY + ((mainRec_width - CirRec_3_width) / 2)
    
    '準備畫筆
    Dim GrayPen1 = New System.Drawing.Pen(Color.LightGray, 1) '方位out
    Dim GrayPen2 = New System.Drawing.Pen(Color.Gray, 2) '方位
    Dim BlackPen1 = New System.Drawing.Pen(Color.Black, 1) '方位
    Dim BluePen1 = New System.Drawing.Pen(Color.DarkBlue, 1)   '運數
    Dim BluePen2 = New System.Drawing.Pen(Color.Blue, 2)   '飛星
    Dim PurplePen1 = New System.Drawing.Pen(Color.Purple, 1)   '八宮

    '準備筆刷
    Dim Dragon_BrushB As New SolidBrush(Color.Gold) '大金龍
    Dim Dragon_BrushS As New SolidBrush(Color.PeachPuff) '小金龍
    Dim Dragon_Brush0 As New SolidBrush(Color.LightYellow) '零神
    Dim Dragon_Brush1 As New SolidBrush(Color.Gainsboro) '正神
    Dim White_Brush As New SolidBrush(Color.White) '內層
    Dim Black_Brush As New SolidBrush(Color.Black) '文字
    Dim Blue_Brush As New SolidBrush(Color.Blue) '文字
    Dim Red_Brush As New SolidBrush(Color.Red) '文字
    Dim Purple_Brush As New SolidBrush(Color.Purple) '文字
    Dim My_Brush1 As New SolidBrush(Color.FromArgb(252, 230, 246)) '兩元
    Dim My_Brush2 As New SolidBrush(Color.FromArgb(215, 252, 221)) '兩元

    '八宮角度 壬山順行坎宮起順行, ---"坎",   "艮",  "震",  "巽",  "離",  "坤", "兌",  "乾"
    Dim Kon8_startAng As Single(,) = _
                   New Single(23, 7) {{82.5, 127.5, 172.5, 217.5, 262.5, 307.5, 352.5, 37.5}, _
                                      {67.5, 112.5, 157.5, 202.5, 247.5, 292.5, 337.5, 22.5}, _
                                      {52.5, 97.5, 142.5, 187.5, 232.5, 277.5, 322.5, 7.5}, _
                                      {37.5, 82.5, 127.5, 172.5, 217.5, 262.5, 307.5, 352.5}, _
                                      {22.5, 67.5, 112.5, 157.5, 202.5, 247.5, 292.5, 337.5}, _
                                      {7.5, 52.5, 97.5, 142.5, 187.5, 232.5, 277.5, 322.5}, _
                                      {352.5, 37.5, 82.5, 127.5, 172.5, 217.5, 262.5, 307.5}, _
                                      {337.5, 22.5, 67.5, 112.5, 157.5, 202.5, 247.5, 292.5}, _
                                      {322.5, 7.5, 52.5, 97.5, 142.5, 187.5, 232.5, 277.5}, _
                                      {307.5, 352.5, 37.5, 82.5, 127.5, 172.5, 217.5, 262.5}, _
                                      {292.5, 337.5, 22.5, 67.5, 112.5, 157.5, 202.5, 247.5}, _
                                      {277.5, 322.5, 7.5, 52.5, 97.5, 142.5, 187.5, 232.5}, _
                                      {262.5, 307.5, 352.5, 37.5, 82.5, 127.5, 172.5, 217.5}, _
                                      {247.5, 292.5, 337.5, 22.5, 67.5, 112.5, 157.5, 202.5}, _
                                      {232.5, 277.5, 322.5, 7.5, 52.5, 97.5, 142.5, 187.5}, _
                                      {217.5, 262.5, 307.5, 352.5, 37.5, 82.5, 127.5, 172.5}, _
                                      {202.5, 247.5, 292.5, 337.5, 22.5, 67.5, 112.5, 157.5}, _
                                      {187.5, 232.5, 277.5, 322.5, 7.5, 52.5, 97.5, 142.5}, _
                                      {172.5, 217.5, 262.5, 307.5, 352.5, 37.5, 82.5, 127.5}, _
                                      {157.5, 202.5, 247.5, 292.5, 337.5, 22.5, 67.5, 112.5}, _
                                      {142.5, 187.5, 232.5, 277.5, 322.5, 7.5, 52.5, 97.5}, _
                                      {127.5, 172.5, 217.5, 262.5, 307.5, 352.5, 37.5, 82.5}, _
                                      {112.5, 157.5, 202.5, 247.5, 292.5, 337.5, 22.5, 67.5}, _
                                      {97.5, 142.5, 187.5, 232.5, 277.5, 322.5, 7.5, 52.5}}

    '八宮宮名標示位置
    '   index=0: 坎宮起順行
    Dim Kon8name_Pos0 As Integer(,) = New Integer(7, 1) { _
        {CirRec_2_startX + (CirRec_2_width / 4) + 10, CirRec_2_startY + CirRec_2_width + 5}, _
        {CirRec_2_startX - 18, CirRec_2_startY + (3 * CirRec_2_width / 4)}, _
        {CirRec_2_startX - 28, CirRec_2_startY + (CirRec_2_width / 4) + 13}, _
        {CirRec_2_startX + (CirRec_2_width / 4) - 22, CirRec_2_startY - 12}, _
        {CirRec_2_startX + (CirRec_2_width / 2) + 25, CirRec_2_startY - 25}, _
        {CirRec_2_startX + CirRec_2_width - 5, CirRec_2_startY + 42}, _
        {CirRec_2_startX + CirRec_2_width, CirRec_2_startY + (CirRec_2_width / 2) + 25}, _
        {CirRec_2_startX + CirRec_2_width - 58, CirRec_2_startY + CirRec_2_width - 10}}
    Dim Kon8name_Pos1 As Integer(,) = New Integer(7, 1) { _
        {CirRec_2_startX + (CirRec_2_width / 2) - 14, CirRec_2_startY + CirRec_2_width + 10}, _
        {CirRec_2_startX + 4, CirRec_2_startY + CirRec_2_width - 32}, _
        {CirRec_2_startX - 30, CirRec_2_startY + (CirRec_2_width / 2) - 14}, _
        {CirRec_2_startX + 5, CirRec_2_startY + 12}, _
        {CirRec_2_startX + (CirRec_2_width / 2) - 14, CirRec_2_startY - 28}, _
        {CirRec_2_startX + CirRec_2_width - 30, CirRec_2_startY + 10}, _
        {CirRec_2_startX + CirRec_2_width + 3, CirRec_2_startY + (CirRec_2_width / 2) - 14}, _
        {CirRec_2_startX + CirRec_2_width - 30, CirRec_2_startY + CirRec_2_width - 30}}
    Dim Kon8name_Pos2 As Integer(,) = New Integer(7, 1) { _
        {CirRec_2_startX + (CirRec_2_width / 2) + 25, CirRec_2_startY + CirRec_2_width + 5}, _
        {CirRec_2_startX + (CirRec_2_width / 4) - 20, CirRec_2_startY + CirRec_2_width - 8}, _
        {CirRec_2_startX - 26, CirRec_2_startY + (CirRec_2_width / 2) + 26}, _
        {CirRec_2_startX - 15, CirRec_2_startY + 37}, _
        {CirRec_2_startX + (CirRec_2_width / 4) + 10, CirRec_2_startY - 23}, _
        {CirRec_2_startX + (3 * CirRec_2_width / 4), CirRec_2_startY - 10}, _
        {CirRec_2_startX + CirRec_2_width + 3, CirRec_2_startY + (CirRec_2_width / 2) - 37}, _
        {CirRec_2_startX + CirRec_2_width - 10, CirRec_2_startY + (3 * CirRec_2_width / 4)}}
    '八宮宮名 index=0=坎,其餘順行.(保持此順序,變動時改變上述其他繪圖座標)
    Dim Kon8name As String() = New String(7) {"坎", "艮", "震", "巽", "離", "坤", "兌", "乾"}
    '八宮金龍顏色
    '  "B" : 大金龍
    '  "S" : 小金龍
    '  "0" : 零神
    '  "1" : 正神
    'index=元運數, 一二三四六七八九
    '             =0 1 2 3 4 5 6 7
    '金龍,Defaut show 八運,坎宮起順行--"坎","艮","震","巽","離","坤","兌","乾"
    Dim K8DrganColor As String(,) = _
                    New String(7, 7) {{"1", "0", "1", "1", "B", "1", "S", "0"}, _
                                      {"S", "1", "0", "0", "1", "B", "1", "1"}, _
                                      {"1", "S", "1", "1", "0", "1", "B", "0"}, _
                                      {"0", "1", "0", "B", "1", "S", "1", "1"}, _
                                      {"1", "0", "1", "1", "S", "1", "0", "B"}, _
                                      {"0", "1", "B", "S", "1", "0", "1", "1"}, _
                                      {"1", "B", "1", "1", "0", "1", "0", "S"}, _
                                      {"B", "1", "S", "0", "1", "0", "1", "1"}}
    '四正四隅名(all)第一個為坐山,順時針排
    Dim Pol8name As String(,) = New String(23, 7) _
       {{"壬", "丑", "甲", "辰", "丙", "未", "庚", "戌"}, _
        {"子", "艮", "卯", "巽", "午", "坤", "酉", "乾"}, _
        {"癸", "寅", "乙", "巳", "丁", "申", "辛", "亥"}, _
        {"丑", "甲", "辰", "丙", "未", "庚", "戌", "壬"}, _
        {"艮", "卯", "巽", "午", "坤", "酉", "乾", "子"}, _
        {"寅", "乙", "巳", "丁", "申", "辛", "亥", "癸"}, _
        {"甲", "辰", "丙", "未", "庚", "戌", "壬", "丑"}, _
        {"卯", "巽", "午", "坤", "酉", "乾", "子", "艮"}, _
        {"乙", "巳", "丁", "申", "辛", "亥", "癸", "寅"}, _
        {"辰", "丙", "未", "庚", "戌", "壬", "丑", "甲"}, _
        {"巽", "午", "坤", "酉", "乾", "子", "艮", "卯"}, _
        {"巳", "丁", "申", "辛", "亥", "癸", "寅", "乙"}, _
        {"丙", "未", "庚", "戌", "壬", "丑", "甲", "辰"}, _
        {"午", "坤", "酉", "乾", "子", "艮", "卯", "巽"}, _
        {"丁", "申", "辛", "亥", "癸", "寅", "乙", "巳"}, _
        {"未", "庚", "戌", "壬", "丑", "甲", "辰", "丙"}, _
        {"坤", "酉", "乾", "子", "艮", "卯", "巽", "午"}, _
        {"申", "辛", "亥", "癸", "寅", "乙", "巳", "丁"}, _
        {"庚", "戌", "壬", "丑", "甲", "辰", "丙", "未"}, _
        {"酉", "乾", "子", "艮", "卯", "巽", "午", "坤"}, _
        {"辛", "亥", "癸", "寅", "乙", "巳", "丁", "申"}, _
        {"戌", "壬", "丑", "甲", "辰", "丙", "未", "庚"}, _
        {"乾", "子", "艮", "卯", "巽", "午", "坤", "酉"}, _
        {"亥", "癸", "寅", "乙", "巳", "丁", "申", "辛"}}

    '四正四隅position 由六點鐘起順時針, 二十四山只填八方
    Dim Pol8name_Pos As Integer(,) = New Integer(7, 1) { _
        {CirRec_1_startX + (CirRec_1_width / 2) - 14, CirRec_1_startY + CirRec_1_width + 7}, _
        {CirRec_1_startX + 17, CirRec_1_startY + CirRec_1_width - 47}, _
        {CirRec_1_startX - 30, CirRec_1_startY + (CirRec_1_width / 2) - 12}, _
        {CirRec_1_startX + 15, CirRec_1_startY + 30}, _
        {CirRec_1_startX + (CirRec_1_width / 2) - 14, CirRec_1_startY - 28}, _
        {CirRec_1_startX + CirRec_1_width - 46, CirRec_1_startY + 22}, _
        {CirRec_1_startX + CirRec_1_width + 5, CirRec_1_startY + (CirRec_1_width / 2) - 14}, _
        {CirRec_1_startX + CirRec_1_width - 44, CirRec_1_startY + CirRec_1_width - 44}}

    '紫白飛星
    '  (0) 三元九運 一二三四五六七八九
    '         input=0,1,2,3,4,5,6,7,8
    '  (1) 元旦盤,洛書數  (2) index for position 
    '      四,九,二   巽離坤    8, 4, 6,
    '      三,五,七   震　兌    7, 0, 2,  (0=中宮)
    '      八,一,六   艮坎乾    3, 5, 1
    '  (3)position 固定(但隨八宮位置),運星山星水星飛完之後,再寫入

    '紫白飛星.運星名A (first index means 哪個運星入中宮, 運星一律順飛)
    Dim sYung_Name As String(,) = _
              New String(8, 8) {{"一", "二", "三", "四", "五", "六", "七", "八", "九"}, _
                                {"二", "三", "四", "五", "六", "七", "八", "九", "一"}, _
                                {"三", "四", "五", "六", "七", "八", "九", "一", "二"}, _
                                {"四", "五", "六", "七", "八", "九", "一", "二", "三"}, _
                                {"五", "六", "七", "八", "九", "一", "二", "三", "四"}, _
                                {"六", "七", "八", "九", "一", "二", "三", "四", "五"}, _
                                {"七", "八", "九", "一", "二", "三", "四", "五", "六"}, _
                                {"八", "九", "一", "二", "三", "四", "五", "六", "七"}, _
                                {"九", "一", "二", "三", "四", "五", "六", "七", "八"}}
    '紫白飛星.運星名B (另一紀錄)
    '以後天數看卦,分陰陽, 陽星一律順飛. 陰星一律逆飛 
    '陽星, 1638(坎乾震艮), 順飛: 宮位順著次序, 數字也順次序
    '陰星, 2749(坤兌巽離), 逆飛: 宮位順著次序, 數字為逆次序
    Dim sYung_Name_2Way As String(,) = _
              New String(8, 8) {{"一", "二", "三", "四", "五", "六", "七", "八", "九"}, _
                                {"二", "一", "九", "八", "七", "六", "五", "九", "一"}, _
                                {"三", "四", "五", "六", "七", "八", "九", "一", "二"}, _
                                {"四", "三", "二", "一", "九", "八", "七", "六", "五"}, _
                                {"五", "六", "七", "八", "九", "一", "二", "三", "四"}, _
                                {"六", "七", "八", "九", "一", "二", "三", "四", "五"}, _
                                {"七", "六", "五", "四", "三", "二", "一", "九", "八"}, _
                                {"八", "九", "一", "二", "三", "四", "五", "六", "七"}, _
                                {"九", "八", "七", "六", "五", "四", "三", "二", "一"}}

    '查山星用表格
    '查表兩次, 先看 sMonutain_MW_Pos(23,2) 
    '    first index here, means 0~23 二十四山
    '    second index here, 0 for 山星所在宮位, content is the index of position
    '                       1 for 水星所在宮位, content is the index of position
    '                       2 for 此山在宮中第幾位(0,1,2)
    '    山/水星所在宮位(content is the index of position):
    '     8, 4, 6,
    '     7, 0, 2,  (0=中宮)
    '     3, 5, 1
    '再來查表 sYung_Name(天運入中順飛結果), 所得內容,文字轉數字,即是山星水星
    '--------
    '24組數字, 為24山, 壬山起順數
    '每組三個數字: 山星所在宮位,水星所在宮位, 此山在宮中第幾位
    Dim sMonutain_MW_Pos As Integer(,) = New Integer(23, 2) { _
         {5, 4, 0}, _
         {5, 4, 1}, _
         {5, 4, 2}, _
         {3, 6, 0}, _
         {3, 6, 1}, _
         {3, 6, 2}, _
         {7, 2, 0}, _
         {7, 2, 1}, _
         {7, 2, 2}, _
         {8, 1, 0}, _
         {8, 1, 1}, _
         {8, 1, 2}, _
         {4, 5, 0}, _
         {4, 5, 1}, _
         {4, 5, 2}, _
         {6, 3, 0}, _
         {6, 3, 1}, _
         {6, 3, 2}, _
         {2, 7, 0}, _
         {2, 7, 1}, _
         {2, 7, 2}, _
         {1, 8, 0}, _
         {1, 8, 1}, _
         {1, 8, 2}}

    '再查山星水星順逆飛 index 為八宮洛數,(沒有0,5)
    '===
    '      四,九,二   巽離坤    
    '      三,五,七   震　兌   
    '      八,一,六   艮坎乾    
    '===
    '  零(缺)
    '  一坎 壬子癸
    '  二坤 未坤申
    '  三震 甲卯乙
    '  四巽 辰巺巳
    '  五(代入運星)
    '  六乾 戌乾亥
    '  七兌 庚酉辛
    '  八艮 丑艮寅
    '  九離 丙午丁
    Dim FlyDir As String(,) = New String(9, 2) { _
        {"無", "無", "無"}, _
        {"順", "逆", "逆"}, _
        {"逆", "順", "順"}, _
        {"順", "逆", "逆"}, _
        {"逆", "順", "順"}, _
        {"無", "無", "無"}, _
        {"逆", "順", "順"}, _
        {"順", "逆", "逆"}, _
        {"逆", "順", "順"}, _
        {"順", "逆", "逆"}}
    '紫白飛星.運星position
    '  index=0 : center position
    '  index for position 
    '     8, 4, 6,
    '     7, 0, 2,  (0=中宮)
    '     3, 5, 1
    Dim sYung_Name_Pos0 As Integer(,) = New Integer(8, 1) { _
       {CirRec_3_startX + (CirRec_3_width / 2) - 14, CirRec_3_startY + (CirRec_3_width / 2) - 20}, _
       {CirRec_3_startX + (CirRec_3_width / 2) + 25, CirRec_3_startY + CirRec_3_width + 7}, _
       {CirRec_3_startX + CirRec_3_width + 14, CirRec_3_startY + (CirRec_3_width / 2)}, _
       {CirRec_3_startX - 40, CirRec_3_startY + (CirRec_3_width / 2) + 20}, _
       {CirRec_3_startX + (CirRec_3_width / 2) + 10, CirRec_3_startY - 50}, _
       {CirRec_3_startX + (CirRec_3_width / 2) - 32, CirRec_3_startY + CirRec_3_width + 10}, _
       {CirRec_3_startX + CirRec_3_width + 14, CirRec_3_startY - 20}, _
       {CirRec_3_startX - 50, CirRec_3_startY}, _
       {CirRec_3_startX - 4, CirRec_3_startY - 45}}
    Dim sYung_Name_Pos1 As Integer(,) = New Integer(8, 1) { _
       {CirRec_3_startX + (CirRec_3_width / 2) - 14, CirRec_3_startY + (CirRec_3_width / 2) - 20}, _
       {CirRec_3_startX + CirRec_3_width - 2, CirRec_3_startY + CirRec_3_width - 7}, _
       {CirRec_3_startX + CirRec_3_width + 20, CirRec_3_startY + (CirRec_3_width / 2) - 20}, _
       {CirRec_3_startX - 24, CirRec_3_startY + CirRec_3_width - 4}, _
       {CirRec_3_startX + (CirRec_3_width / 2) - 14, CirRec_3_startY - 50}, _
       {CirRec_3_startX + (CirRec_3_width / 2) - 14, CirRec_3_startY + CirRec_3_width + 13}, _
       {CirRec_3_startX + CirRec_3_width, CirRec_3_startY - 32}, _
       {CirRec_3_startX - 46, CirRec_3_startY + (CirRec_3_width / 2) - 20}, _
       {CirRec_3_startX - 28, CirRec_3_startY - 30}}
    Dim sYung_Name_Pos2 As Integer(,) = New Integer(8, 1) { _
       {CirRec_3_startX + (CirRec_3_width / 2) - 14, CirRec_3_startY + (CirRec_3_width / 2) - 20}, _
       {CirRec_3_startX + CirRec_3_width + 10, CirRec_3_startY + CirRec_3_width - 20}, _
       {CirRec_3_startX + CirRec_3_width + 20, CirRec_3_startY}, _
       {CirRec_3_startX - 4, CirRec_3_startY + CirRec_3_width + 10}, _
       {CirRec_3_startX + (CirRec_3_width / 2) - 32, CirRec_3_startY - 53}, _
       {CirRec_3_startX + (CirRec_3_width / 2) + 8, CirRec_3_startY + CirRec_3_width + 13}, _
       {CirRec_3_startX + CirRec_3_width - 14, CirRec_3_startY - 47}, _
       {CirRec_3_startX - 46, CirRec_3_startY + (CirRec_3_width / 2)}, _
       {CirRec_3_startX - 32, CirRec_3_startY - 18}}
    '--------------------------------------------------------------------
    '  index for position 
    '     8, 4, 6,
    '     7, 0, 2,  (0=中宮)
    '     3, 5, 1
    '======>24山依序看,原來位置逆轉(12648735)========
    '  元旦盤,巽離坤 index 0  1  2  3  4  5  6  7  8
    '  　　　 震　兌  對應 中 乾 兌 艮 離 坎 坤 震 巽
    '  　　　 艮坎乾  座標 ０ １ ２ ３ ４ ５ ６ ７ ８
    '         ---------------------------------------
    '  元旦盤,離坤兌 index 0  1  2  3  4  5  6  7  8
    '  逆一　 巽　乾  對應 中 乾 兌 艮 離 坎 坤 震 巽
    '  　　　 震艮坎  座標 ０ ２ ６ ５ ８ １ ４ ３ ７
    '         ---------------------------------------
    '  元旦盤,坤兌乾 index 0  1  2  3  4  5  6  7  8
    '  逆二　 離　坎  對應 中 乾 兌 艮 離 坎 坤 震 巽
    '  　　　 巽震艮  座標 ０ ６ ４ １ ７ ２ ８ ５ ３
    '         ---------------------------------------
    '  元旦盤,兌乾坎 index 0  1  2  3  4  5  6  7  8
    '  逆三　 坤　艮  對應 中 乾 兌 艮 離 坎 坤 震 巽
    '  　　　 離巽震  座標 ０ ４ ８ ２ ３ ６ ７ １ ５
    '         ---------------------------------------
    '  元旦盤,乾坎艮 index 0  1  2  3  4  5  6  7  8
    '  逆四　 兌　震  對應 中 乾 兌 艮 離 坎 坤 震 巽
    '  　　　 坤離巽  座標 ０ ８ ７ ６ ５ ４ ３ ２ １
    '         ---------------------------------------
    '  元旦盤,坎艮震 index 0  1  2  3  4  5  6  7  8
    '  逆五　 乾　巽  對應 中 乾 兌 艮 離 坎 坤 震 巽
    '  　　　 兌坤離  座標 ０ ７ ３ ４ １ ８ ５ ６ ２
    '         ---------------------------------------
    '  元旦盤,艮震巽 index 0  1  2  3  4  5  6  7  8
    '  逆六　 坎　離  對應 中 乾 兌 艮 離 坎 坤 震 巽
    '  　　　 乾兌坤  座標 ０ ３ ５ ８ ２ ７ １ ４ ６
    '         ---------------------------------------
    '  元旦盤,震巽離 index 0  1  2  3  4  5  6  7  8
    '  逆七　 艮　坤  對應 中 乾 兌 艮 離 坎 坤 震 巽
    '  　　　 坎乾兌  座標 ０ ５ １ ７ ６ ３ ２ ８ ４
    '         ---------------------------------------
    '運星座標轉換
    Dim sYungNamePos_Tranform As Integer(,) = New Integer(7, 8) { _
        {0, 1, 2, 3, 4, 5, 6, 7, 8}, _
        {0, 2, 6, 5, 8, 1, 4, 3, 7}, _
        {0, 6, 4, 1, 7, 2, 8, 5, 3}, _
        {0, 4, 8, 2, 3, 6, 7, 1, 5}, _
        {0, 8, 7, 6, 5, 4, 3, 2, 1}, _
        {0, 7, 3, 4, 1, 8, 5, 6, 2}, _
        {0, 3, 5, 8, 2, 7, 1, 4, 6}, _
        {0, 5, 1, 7, 6, 3, 2, 8, 4}}
    'for display font area size
    Dim Fdx As Integer = 20 'font area dx
    Dim Fdy As Integer = 20 'font area dy

    '紫白是三元九運共180年,美運20年
    '金龍是二元八運也180年,長短不一
    '  sYungNo   天運 = 0,1,~~8 = 一二三四五六七八九
    '  eYungNo   地運 = 0,1,~~7 = 一二三四六七八九
    '真正天地運組合只有16種
    Dim SkyEarth As Integer(,) = New Integer(15, 1) {
        {0, 0}, {0, 1}, _
        {1, 1}, {2, 1}, _
        {2, 2}, {3, 2}, _
        {3, 3}, {4, 3}, _
        {4, 4}, {5, 4}, _
        {5, 5}, {6, 5}, _
        {6, 6}, {7, 6}, _
        {7, 7}, {8, 7}}

    '=========================================================================================================
    '顯示文字資料副程式
    Private Sub RedTextH(ByVal X As Integer, ByVal Y As Integer, _
                       ByVal Width As Integer, ByVal Height As Integer, _
                       ByVal strText As String, ByVal intFontSize As Integer)
        Dim RecArea As New Rectangle(X, Y, Width, Height)
        '為了小方格內的文字,將受Padding影響,所以特別微調矩形,給DrawString使用
        Dim RecArea2 As New Rectangle(X, Y, Width + 4, Height)
        Dim DS_Font As New Font("新細明體", intFontSize, FontStyle.Bold)
        Dim DS_Format As New StringFormat(StringFormatFlags.DirectionRightToLeft)
        DS_Format.LineAlignment = StringAlignment.Center
        DS_Format.Alignment = StringAlignment.Center
        G.DrawString(strText, DS_Font, Red_Brush, RecArea2, DS_Format)
    End Sub
    Private Sub BlueTextH(ByVal X As Integer, ByVal Y As Integer, _
                       ByVal Width As Integer, ByVal Height As Integer, _
                       ByVal strText As String, ByVal intFontSize As Integer)
        Dim RecArea As New Rectangle(X, Y, Width, Height)
        '為了小方格內的文字,將受Padding影響,所以特別微調矩形,給DrawString使用
        Dim RecArea2 As New Rectangle(X, Y, Width + 4, Height)
        Dim DS_Font As New Font("新細明體", intFontSize, FontStyle.Bold)
        Dim DS_Format As New StringFormat(StringFormatFlags.DirectionRightToLeft)
        DS_Format.LineAlignment = StringAlignment.Center
        DS_Format.Alignment = StringAlignment.Center
        G.DrawString(strText, DS_Font, Blue_Brush, RecArea2, DS_Format)
    End Sub
    Private Sub PurpleText(ByVal X As Integer, ByVal Y As Integer, _
                       ByVal Width As Integer, ByVal Height As Integer, _
                       ByVal strText As String, ByVal intFontSize As Integer)
        Dim RecArea As New Rectangle(X, Y, Width, Height)
        '為了小方格內的文字,將受Padding影響,所以特別微調矩形,給DrawString使用
        Dim RecArea2 As New Rectangle(X, Y, Width + 4, Height)
        Dim DS_Font As New Font("新細明體", intFontSize, FontStyle.Bold)
        Dim DS_Format As New StringFormat(StringFormatFlags.DirectionVertical)
        DS_Format.LineAlignment = StringAlignment.Center
        DS_Format.Alignment = StringAlignment.Center
        G.DrawString(strText, DS_Font, Purple_Brush, RecArea2, DS_Format)
    End Sub

    Private Sub BlackText(ByVal X As Integer, ByVal Y As Integer, _
                       ByVal Width As Integer, ByVal Height As Integer, _
                       ByVal strText As String, ByVal intFontSize As Integer)
        Dim RecArea As New Rectangle(X, Y, Width, Height)
        '為了小方格內的文字,將受Padding影響,所以特別微調矩形,給DrawString使用
        Dim RecArea2 As New Rectangle(X, Y, Width + 4, Height)
        Dim DS_Font As New Font("新細明體", intFontSize, FontStyle.Bold)
        Dim DS_Format As New StringFormat(StringFormatFlags.DirectionVertical)
        DS_Format.LineAlignment = StringAlignment.Center
        DS_Format.Alignment = StringAlignment.Center
        G.DrawString(strText, DS_Font, Black_Brush, RecArea2, DS_Format)
    End Sub

    Private Sub BlackTextH(ByVal X As Integer, ByVal Y As Integer, _
                       ByVal Width As Integer, ByVal Height As Integer, _
                       ByVal strText As String, ByVal intFontSize As Integer)
        Dim RecArea As New Rectangle(X, Y, Width, Height)
        '為了小方格內的文字,將受Padding影響,所以特別微調矩形,給DrawString使用
        Dim RecArea2 As New Rectangle(X, Y, Width + 4, Height)
        Dim DS_Font As New Font("新細明體", intFontSize, FontStyle.Bold)
        Dim DS_Format As New StringFormat(StringFormatFlags.DirectionRightToLeft)
        DS_Format.LineAlignment = StringAlignment.Center
        DS_Format.Alignment = StringAlignment.Center
        G.DrawString(strText, DS_Font, Black_Brush, RecArea2, DS_Format)
    End Sub

    Private Sub BlueText(ByVal X As Integer, ByVal Y As Integer, _
                       ByVal Width As Integer, ByVal Height As Integer, _
                       ByVal strText As String, ByVal intFontSize As Integer)
        Dim RecArea As New Rectangle(X, Y, Width, Height)
        '為了小方格內的文字,將受Padding影響,所以特別微調矩形,給DrawString使用
        Dim RecArea2 As New Rectangle(X, Y, Width + 4, Height)
        Dim DS_Font As New Font("新細明體", intFontSize, FontStyle.Bold)
        Dim DS_Format As New StringFormat(StringFormatFlags.DirectionVertical)
        DS_Format.LineAlignment = StringAlignment.Center
        DS_Format.Alignment = StringAlignment.Center
        G.DrawString(strText, DS_Font, Blue_Brush, RecArea2, DS_Format)
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Default 天運八(7),地運九(7),子山(1)
        ListBox1.SelectedIndex = 14
        ListBox3.SelectedIndex = 2
        ListBox2.SelectedIndex = 0
    End Sub

    'Main function ========================================================
    '  sYungNo   天運 = 0,1,~~8 = 一二三四五六七八九
    '  eYungNo   地運 = 0,1,~~7 = 一二三四六七八九
    '  sMountain 坐山 = 0,1,~~23 = 壬子癸丑....亥
    Private Sub DrawAll(ByVal sYungNo As Integer,
                        ByVal eYungNo As Integer,
                        ByVal sMountain As Integer,
                        ByVal yearFlow As Integer)
        Dim Kon8name_Pos As Integer(,) = New Integer(7, 1) {
            {0, 0}, {0, 0}, {0, 0}, {0, 0}, {0, 0}, {0, 0}, {0, 0}, {0, 0}}
        Dim sYung_Name_Pos As Integer(,) = New Integer(8, 1) {
            {0, 0}, {0, 0}, {0, 0}, {0, 0}, {0, 0}, {0, 0}, {0, 0}, {0, 0}, {0, 0}}

        '畫布準備
        PicDisplay.Image = BMP
        G = Graphics.FromImage(BMP)
        G.SmoothingMode = Drawing2D.SmoothingMode.HighQuality '可以改善鋸齒狀的問題
        G.Clear(Color.White) '白色為底色

        'check outside boundary
        Dim RecArea_main As New Rectangle(mainRec_startX,
                                          mainRec_startY,
                                          mainRec_width, mainRec_width)
        G.DrawRectangle(BluePen1, RecArea_main) 'check 整個畫圖範圍
        '各個圓圈範圍
        Dim CirRec_0 As New Rectangle(CirRec_0_startX,
                                      CirRec_0_startY,
                                      CirRec_0_width, CirRec_0_width)
        Dim CirRec_1 As New Rectangle(CirRec_1_startX,
                                      CirRec_1_startY,
                                      CirRec_1_width, CirRec_1_width)
        Dim CirRec_2 As New Rectangle(CirRec_2_startX,
                                      CirRec_2_startY,
                                      CirRec_2_width, CirRec_2_width)
        Dim CirRec_3 As New Rectangle(CirRec_3_startX,
                                      CirRec_3_startY,
                                      CirRec_3_width, CirRec_3_width)
        '外圓0八宮顏色--------------------金龍!!
        For i As Integer = 0 To 7
            If K8DrganColor(eYungNo, i) = "B" Then
                G.FillPie(Dragon_BrushB, CirRec_0, Kon8_startAng(sMountain, i), 45)
            ElseIf K8DrganColor(eYungNo, i) = "S" Then
                G.FillPie(Dragon_BrushS, CirRec_0, Kon8_startAng(sMountain, i), 45)
            ElseIf K8DrganColor(eYungNo, i) = "0" Then
                G.FillPie(Dragon_Brush0, CirRec_0, Kon8_startAng(sMountain, i), 45)
            Else
                G.FillPie(Dragon_Brush1, CirRec_0, Kon8_startAng(sMountain, i), 45)
            End If
        Next
        '外圓0八宮邊線----
        For i As Integer = 0 To 7
            G.DrawPie(GrayPen1, CirRec_0, Kon8_startAng(sMountain, i), 45)
        Next
        '圓1八宮塗色--------------------兩元!!
        For i As Integer = 0 To 7
            Select Case i
                Case 3, 4, 5, 6
                    G.FillPie(My_Brush1, CirRec_1, Kon8_startAng(sMountain, i), 45)
                Case 0, 1, 2, 7
                    G.FillPie(My_Brush2, CirRec_1, Kon8_startAng(sMountain, i), 45)
            End Select
        Next
        '圓1八宮邊線----
        For i As Integer = 0 To 7
            G.DrawPie(GrayPen2, CirRec_1, Kon8_startAng(sMountain, i), 45)
        Next
        '圓2八宮塗色--------------------
        For i As Integer = 0 To 7
            G.FillPie(White_Brush, CirRec_2, Kon8_startAng(sMountain, i), 45)
        Next
        '圓2八宮邊線----
        For i As Integer = 0 To 7
            G.DrawPie(BlackPen1, CirRec_2, Kon8_startAng(sMountain, i), 45)
        Next
        '圓3八宮塗色--------------------
        G.FillEllipse(White_Brush, CirRec_3)
        '圓3八宮邊線----
        G.DrawEllipse(BluePen1, CirRec_3)

        '方向箭頭與四正----------------------------
        G.DrawLine(GrayPen2, arrowTop_X, arrowTop_Y, arrowTop_X, arrowTop_Y + arrowMlength)
        G.DrawLine(GrayPen2, arrowTop_X, arrowTop_Y, arrowTop_X + arrow_dx, arrowTop_Y + arrowMlength - arrow_dy)
        G.DrawLine(GrayPen2, arrowTop_X, arrowTop_Y, arrowTop_X - arrow_dx, arrowTop_Y + arrowMlength - arrow_dy)
        G.DrawLine(GrayPen2, arrowBottom_X, arrowBottom_Y, arrowBottom_X, arrowBottom_Y - arrowMlength)
        G.DrawLine(GrayPen2, arrowLeft_X, arrowLeft_Y, arrowLeft_X + arrowMlength, arrowLeft_Y)
        G.DrawLine(GrayPen2, arrowRight_X, arrowRight_Y, arrowRight_X - arrowMlength, arrowRight_Y)

        'Transform 八宮名position, 飛星position
        Dim j As Integer
        Select Case sMountain
            Case 0    '壬山 
                For i As Integer = 0 To 7
                    Kon8name_Pos(i, 0) = Kon8name_Pos0(i, 0)
                    Kon8name_Pos(i, 1) = Kon8name_Pos0(i, 1)
                Next
                For i As Integer = 0 To 8
                    sYung_Name_Pos(i, 0) = sYung_Name_Pos0(sYungNamePos_Tranform(0, i), 0)
                    sYung_Name_Pos(i, 1) = sYung_Name_Pos0(sYungNamePos_Tranform(0, i), 1)
                Next
            Case 1     '子山
                For i As Integer = 0 To 7
                    Kon8name_Pos(i, 0) = Kon8name_Pos1(i, 0)
                    Kon8name_Pos(i, 1) = Kon8name_Pos1(i, 1)
                Next
                For i As Integer = 0 To 8
                    sYung_Name_Pos(i, 0) = sYung_Name_Pos1(sYungNamePos_Tranform(0, i), 0)
                    sYung_Name_Pos(i, 1) = sYung_Name_Pos1(sYungNamePos_Tranform(0, i), 1)
                Next
            Case 2  '癸山
                For i As Integer = 0 To 7
                    Kon8name_Pos(i, 0) = Kon8name_Pos2(i, 0)
                    Kon8name_Pos(i, 1) = Kon8name_Pos2(i, 1)
                Next
                For i As Integer = 0 To 8
                    sYung_Name_Pos(i, 0) = sYung_Name_Pos2(sYungNamePos_Tranform(0, i), 0)
                    sYung_Name_Pos(i, 1) = sYung_Name_Pos2(sYungNamePos_Tranform(0, i), 1)
                Next
            Case 3   '丑山
                For i As Integer = 0 To 7
                    j = i - 1
                    If j < 0 Then
                        j = j + 8
                    End If
                    Kon8name_Pos(i, 0) = Kon8name_Pos0(j, 0)
                    Kon8name_Pos(i, 1) = Kon8name_Pos0(j, 1)
                Next
                For i As Integer = 0 To 8
                    sYung_Name_Pos(i, 0) = sYung_Name_Pos0(sYungNamePos_Tranform(1, i), 0)
                    sYung_Name_Pos(i, 1) = sYung_Name_Pos0(sYungNamePos_Tranform(1, i), 1)
                Next
            Case 4   '艮山
                For i As Integer = 0 To 7
                    j = i - 1
                    If j < 0 Then
                        j = j + 8
                    End If
                    Kon8name_Pos(i, 0) = Kon8name_Pos1(j, 0)
                    Kon8name_Pos(i, 1) = Kon8name_Pos1(j, 1)
                Next
                For i As Integer = 0 To 8
                    sYung_Name_Pos(i, 0) = sYung_Name_Pos1(sYungNamePos_Tranform(1, i), 0)
                    sYung_Name_Pos(i, 1) = sYung_Name_Pos1(sYungNamePos_Tranform(1, i), 1)
                Next
            Case 5   '寅山
                For i As Integer = 0 To 7
                    j = i - 1
                    If j < 0 Then
                        j = j + 8
                    End If
                    Kon8name_Pos(i, 0) = Kon8name_Pos2(j, 0)
                    Kon8name_Pos(i, 1) = Kon8name_Pos2(j, 1)
                Next
                For i As Integer = 0 To 8
                    sYung_Name_Pos(i, 0) = sYung_Name_Pos2(sYungNamePos_Tranform(1, i), 0)
                    sYung_Name_Pos(i, 1) = sYung_Name_Pos2(sYungNamePos_Tranform(1, i), 1)
                Next
            Case 6   '甲山
                For i As Integer = 0 To 7
                    j = i - 2
                    If j < 0 Then
                        j = j + 8
                    End If
                    Kon8name_Pos(i, 0) = Kon8name_Pos0(j, 0)
                    Kon8name_Pos(i, 1) = Kon8name_Pos0(j, 1)
                Next
                For i As Integer = 0 To 8
                    sYung_Name_Pos(i, 0) = sYung_Name_Pos0(sYungNamePos_Tranform(2, i), 0)
                    sYung_Name_Pos(i, 1) = sYung_Name_Pos0(sYungNamePos_Tranform(2, i), 1)
                Next
            Case 7   '卯山
                For i As Integer = 0 To 7
                    j = i - 2
                    If j < 0 Then
                        j = j + 8
                    End If
                    Kon8name_Pos(i, 0) = Kon8name_Pos1(j, 0)
                    Kon8name_Pos(i, 1) = Kon8name_Pos1(j, 1)
                Next
                For i As Integer = 0 To 8
                    sYung_Name_Pos(i, 0) = sYung_Name_Pos1(sYungNamePos_Tranform(2, i), 0)
                    sYung_Name_Pos(i, 1) = sYung_Name_Pos1(sYungNamePos_Tranform(2, i), 1)
                Next
            Case 8   '乙山
                For i As Integer = 0 To 7
                    j = i - 2
                    If j < 0 Then
                        j = j + 8
                    End If
                    Kon8name_Pos(i, 0) = Kon8name_Pos2(j, 0)
                    Kon8name_Pos(i, 1) = Kon8name_Pos2(j, 1)
                Next
                For i As Integer = 0 To 8
                    sYung_Name_Pos(i, 0) = sYung_Name_Pos2(sYungNamePos_Tranform(2, i), 0)
                    sYung_Name_Pos(i, 1) = sYung_Name_Pos2(sYungNamePos_Tranform(2, i), 1)
                Next
            Case 9   '辰山
                For i As Integer = 0 To 7
                    j = i - 3
                    If j < 0 Then
                        j = j + 8
                    End If
                    Kon8name_Pos(i, 0) = Kon8name_Pos0(j, 0)
                    Kon8name_Pos(i, 1) = Kon8name_Pos0(j, 1)
                Next
                For i As Integer = 0 To 8
                    sYung_Name_Pos(i, 0) = sYung_Name_Pos0(sYungNamePos_Tranform(3, i), 0)
                    sYung_Name_Pos(i, 1) = sYung_Name_Pos0(sYungNamePos_Tranform(3, i), 1)
                Next
            Case 10   '巽山
                For i As Integer = 0 To 7
                    j = i - 3
                    If j < 0 Then
                        j = j + 8
                    End If
                    Kon8name_Pos(i, 0) = Kon8name_Pos1(j, 0)
                    Kon8name_Pos(i, 1) = Kon8name_Pos1(j, 1)
                Next
                For i As Integer = 0 To 8
                    sYung_Name_Pos(i, 0) = sYung_Name_Pos1(sYungNamePos_Tranform(3, i), 0)
                    sYung_Name_Pos(i, 1) = sYung_Name_Pos1(sYungNamePos_Tranform(3, i), 1)
                Next
            Case 11   '巳山
                For i As Integer = 0 To 7
                    j = i - 3
                    If j < 0 Then
                        j = j + 8
                    End If
                    Kon8name_Pos(i, 0) = Kon8name_Pos2(j, 0)
                    Kon8name_Pos(i, 1) = Kon8name_Pos2(j, 1)
                Next
                For i As Integer = 0 To 8
                    sYung_Name_Pos(i, 0) = sYung_Name_Pos2(sYungNamePos_Tranform(3, i), 0)
                    sYung_Name_Pos(i, 1) = sYung_Name_Pos2(sYungNamePos_Tranform(3, i), 1)
                Next
            Case 12   '丙山
                For i As Integer = 0 To 7
                    j = i - 4
                    If j < 0 Then
                        j = j + 8
                    End If
                    Kon8name_Pos(i, 0) = Kon8name_Pos0(j, 0)
                    Kon8name_Pos(i, 1) = Kon8name_Pos0(j, 1)
                Next
                For i As Integer = 0 To 8
                    sYung_Name_Pos(i, 0) = sYung_Name_Pos0(sYungNamePos_Tranform(4, i), 0)
                    sYung_Name_Pos(i, 1) = sYung_Name_Pos0(sYungNamePos_Tranform(4, i), 1)
                Next
            Case 13   '午山
                For i As Integer = 0 To 7
                    j = i - 4
                    If j < 0 Then
                        j = j + 8
                    End If
                    Kon8name_Pos(i, 0) = Kon8name_Pos1(j, 0)
                    Kon8name_Pos(i, 1) = Kon8name_Pos1(j, 1)
                Next
                For i As Integer = 0 To 8
                    sYung_Name_Pos(i, 0) = sYung_Name_Pos1(sYungNamePos_Tranform(4, i), 0)
                    sYung_Name_Pos(i, 1) = sYung_Name_Pos1(sYungNamePos_Tranform(4, i), 1)
                Next
            Case 14  '丁山
                For i As Integer = 0 To 7
                    j = i - 4
                    If j < 0 Then
                        j = j + 8
                    End If
                    Kon8name_Pos(i, 0) = Kon8name_Pos2(j, 0)
                    Kon8name_Pos(i, 1) = Kon8name_Pos2(j, 1)
                Next
                For i As Integer = 0 To 8
                    sYung_Name_Pos(i, 0) = sYung_Name_Pos2(sYungNamePos_Tranform(4, i), 0)
                    sYung_Name_Pos(i, 1) = sYung_Name_Pos2(sYungNamePos_Tranform(4, i), 1)
                Next
            Case 15   '未山
                For i As Integer = 0 To 7
                    j = i - 5
                    If j < 0 Then
                        j = j + 8
                    End If
                    Kon8name_Pos(i, 0) = Kon8name_Pos0(j, 0)
                    Kon8name_Pos(i, 1) = Kon8name_Pos0(j, 1)
                Next
                For i As Integer = 0 To 8
                    sYung_Name_Pos(i, 0) = sYung_Name_Pos0(sYungNamePos_Tranform(5, i), 0)
                    sYung_Name_Pos(i, 1) = sYung_Name_Pos0(sYungNamePos_Tranform(5, i), 1)
                Next
            Case 16   '坤山
                For i As Integer = 0 To 7
                    j = i - 5
                    If j < 0 Then
                        j = j + 8
                    End If
                    Kon8name_Pos(i, 0) = Kon8name_Pos1(j, 0)
                    Kon8name_Pos(i, 1) = Kon8name_Pos1(j, 1)
                Next
                For i As Integer = 0 To 8
                    sYung_Name_Pos(i, 0) = sYung_Name_Pos1(sYungNamePos_Tranform(5, i), 0)
                    sYung_Name_Pos(i, 1) = sYung_Name_Pos1(sYungNamePos_Tranform(5, i), 1)
                Next
            Case 17  '申山
                For i As Integer = 0 To 7
                    j = i - 5
                    If j < 0 Then
                        j = j + 8
                    End If
                    Kon8name_Pos(i, 0) = Kon8name_Pos2(j, 0)
                    Kon8name_Pos(i, 1) = Kon8name_Pos2(j, 1)
                Next
                For i As Integer = 0 To 8
                    sYung_Name_Pos(i, 0) = sYung_Name_Pos2(sYungNamePos_Tranform(5, i), 0)
                    sYung_Name_Pos(i, 1) = sYung_Name_Pos2(sYungNamePos_Tranform(5, i), 1)
                Next
            Case 18   '庚山
                For i As Integer = 0 To 7
                    j = i - 6
                    If j < 0 Then
                        j = j + 8
                    End If
                    Kon8name_Pos(i, 0) = Kon8name_Pos0(j, 0)
                    Kon8name_Pos(i, 1) = Kon8name_Pos0(j, 1)
                Next
                For i As Integer = 0 To 8
                    sYung_Name_Pos(i, 0) = sYung_Name_Pos0(sYungNamePos_Tranform(6, i), 0)
                    sYung_Name_Pos(i, 1) = sYung_Name_Pos0(sYungNamePos_Tranform(6, i), 1)
                Next
            Case 19   '酉山
                For i As Integer = 0 To 7
                    j = i - 6
                    If j < 0 Then
                        j = j + 8
                    End If
                    Kon8name_Pos(i, 0) = Kon8name_Pos1(j, 0)
                    Kon8name_Pos(i, 1) = Kon8name_Pos1(j, 1)
                Next
                For i As Integer = 0 To 8
                    sYung_Name_Pos(i, 0) = sYung_Name_Pos1(sYungNamePos_Tranform(6, i), 0)
                    sYung_Name_Pos(i, 1) = sYung_Name_Pos1(sYungNamePos_Tranform(6, i), 1)
                Next
            Case 20   '辛山
                For i As Integer = 0 To 7
                    j = i - 6
                    If j < 0 Then
                        j = j + 8
                    End If
                    Kon8name_Pos(i, 0) = Kon8name_Pos2(j, 0)
                    Kon8name_Pos(i, 1) = Kon8name_Pos2(j, 1)
                Next
                For i As Integer = 0 To 8
                    sYung_Name_Pos(i, 0) = sYung_Name_Pos2(sYungNamePos_Tranform(6, i), 0)
                    sYung_Name_Pos(i, 1) = sYung_Name_Pos2(sYungNamePos_Tranform(6, i), 1)
                Next
            Case 21   '戌山
                For i As Integer = 0 To 7
                    j = i - 7
                    If j < 0 Then
                        j = j + 8
                    End If
                    Kon8name_Pos(i, 0) = Kon8name_Pos0(j, 0)
                    Kon8name_Pos(i, 1) = Kon8name_Pos0(j, 1)
                Next
                For i As Integer = 0 To 8
                    sYung_Name_Pos(i, 0) = sYung_Name_Pos0(sYungNamePos_Tranform(7, i), 0)
                    sYung_Name_Pos(i, 1) = sYung_Name_Pos0(sYungNamePos_Tranform(7, i), 1)
                Next
            Case 22   '乾山()
                For i As Integer = 0 To 7
                    j = i - 7
                    If j < 0 Then
                        j = j + 8
                    End If
                    Kon8name_Pos(i, 0) = Kon8name_Pos1(j, 0)
                    Kon8name_Pos(i, 1) = Kon8name_Pos1(j, 1)
                Next
                For i As Integer = 0 To 8
                    sYung_Name_Pos(i, 0) = sYung_Name_Pos1(sYungNamePos_Tranform(7, i), 0)
                    sYung_Name_Pos(i, 1) = sYung_Name_Pos1(sYungNamePos_Tranform(7, i), 1)
                Next
            Case 23    '亥山 
                For i As Integer = 0 To 7
                    j = i - 7
                    If j < 0 Then
                        j = j + 8
                    End If
                    Kon8name_Pos(i, 0) = Kon8name_Pos2(j, 0)
                    Kon8name_Pos(i, 1) = Kon8name_Pos2(j, 1)
                Next
                For i As Integer = 0 To 8
                    sYung_Name_Pos(i, 0) = sYung_Name_Pos2(sYungNamePos_Tranform(7, i), 0)
                    sYung_Name_Pos(i, 1) = sYung_Name_Pos2(sYungNamePos_Tranform(7, i), 1)
                Next
        End Select
        '寫後天八卦八宮名
        For i As Integer = 0 To 7
            BlueText(Kon8name_Pos(i, 0), Kon8name_Pos(i, 1), Fdx, Fdy, Kon8name(i), 16)
        Next
        '寫四正四隅名
        For i As Integer = 0 To 7
            PurpleText(Pol8name_Pos(i, 0), Pol8name_Pos(i, 1), Fdx, Fdy, Pol8name(sMountain, i), 16)
        Next
        '寫紫白飛星.運星
        For i As Integer = 0 To 8
            'A  運星一律順飛
            BlackText(sYung_Name_Pos(i, 0), sYung_Name_Pos(i, 1), Fdx, Fdy, sYung_Name(sYungNo, i), 16)
            'B  運星分陰陽順逆飛 ??
            'BlackText(sYung_Name_Pos(i, 0), sYung_Name_Pos(i, 1), Fdx, Fdy, sYung_Name_2Way(sYungNo, i), 16)
        Next
        Dim Mstar As Integer   '山星
        Dim Wstar As Integer   '水星
        Dim MstarFly As String   '山星順逆飛
        Dim WstarFly As String   '水星順逆飛
        Dim MstarAll As Integer() = New Integer(8) {1, 2, 3, 4, 5, 6, 7, 8, 9}  '山星all (default)(將儲存飛完的結果)
        Dim WstarAll As Integer() = New Integer(8) {6, 5, 4, 3, 2, 1, 9, 8, 7}  '水星all (default)(將儲存飛完的結果)
        '查表找山星水星 ---------------------
        'step1 山星
        Select Case sYung_Name(sYungNo, sMonutain_MW_Pos(sMountain, 0))
            Case "一"
                Mstar = 1
            Case "二"
                Mstar = 2
            Case "三"
                Mstar = 3
            Case "四"
                Mstar = 4
            Case "五"
                Mstar = 5
            Case "六"
                Mstar = 6
            Case "七"
                Mstar = 7
            Case "八"
                Mstar = 8
            Case "九"
                Mstar = 9
        End Select
        'step2 水星
        Select Case sYung_Name(sYungNo, sMonutain_MW_Pos(sMountain, 1))
            Case "一"
                Wstar = 1
            Case "二"
                Wstar = 2
            Case "三"
                Wstar = 3
            Case "四"
                Wstar = 4
            Case "五"
                Wstar = 5
            Case "六"
                Wstar = 6
            Case "七"
                Wstar = 7
            Case "八"
                Wstar = 8
            Case "九"
                Wstar = 9
        End Select
        'step3 順逆飛
        If Mstar <> 5 Then  'check山星, 逢五代入運星
            MstarFly = FlyDir(Mstar, sMonutain_MW_Pos(sMountain, 2))
        Else
            MstarFly = FlyDir(sYungNo + 1, sMonutain_MW_Pos(sMountain, 2))
        End If
        If Wstar <> 5 Then  'check水星, 逢五代入運星
            WstarFly = FlyDir(Wstar, sMonutain_MW_Pos(sMountain, 2))
        Else
            WstarFly = FlyDir(sYungNo + 1, sMonutain_MW_Pos(sMountain, 2))
        End If
        'step4 排山星水星
        For i As Integer = 0 To 8
            If MstarFly = "順" Then
                MstarAll(i) = Mstar + i
                If MstarAll(i) > 9 Then
                    MstarAll(i) = MstarAll(i) - 9
                End If
            Else
                MstarAll(i) = Mstar - i
                If MstarAll(i) < 1 Then
                    MstarAll(i) = MstarAll(i) + 9
                End If
            End If
        Next
        For i As Integer = 0 To 8
            If WstarFly = "順" Then
                WstarAll(i) = Wstar + i
                If WstarAll(i) > 9 Then
                    WstarAll(i) = WstarAll(i) - 9
                End If
            Else
                WstarAll(i) = Wstar - i
                If WstarAll(i) < 1 Then
                    WstarAll(i) = WstarAll(i) + 9
                End If
            End If
        Next
        '寫紫白飛星.山水星, 流年飛星
        For i As Integer = 0 To 8
            BlueTextH(sYung_Name_Pos(i, 0) - 7, sYung_Name_Pos(i, 1) + 24, Fdx + 5, Fdy, MstarAll(i), 14)
            RedTextH(sYung_Name_Pos(i, 0) + 7, sYung_Name_Pos(i, 1) + 24, Fdx + 5, Fdy, WstarAll(i), 14)
        Next

        Dim Ystar As Integer   '年白
        If yearFlow >= 0 Then
            For i As Integer = 0 To 8
                '年白入中, 流年飛星
                Select Case sYung_Name(yearFlow, i)
                    Case "一"
                        Ystar = 1
                    Case "二"
                        Ystar = 2
                    Case "三"
                        Ystar = 3
                    Case "四"
                        Ystar = 4
                    Case "五"
                        Ystar = 5
                    Case "六"
                        Ystar = 6
                    Case "七"
                        Ystar = 7
                    Case "八"
                        Ystar = 8
                    Case "九"
                        Ystar = 9
                End Select
                BlackTextH(sYung_Name_Pos(i, 0) - 19, sYung_Name_Pos(i, 1) + 19, Fdx + 5, Fdy, Ystar, 14)
            Next
        End If

        '寫紫白飛星.山水星, 流年飛星
        For i As Integer = 0 To 8
            BlueTextH(sYung_Name_Pos(i, 0) - 7, sYung_Name_Pos(i, 1) + 24, Fdx + 5, Fdy, MstarAll(i), 14)
            RedTextH(sYung_Name_Pos(i, 0) + 7, sYung_Name_Pos(i, 1) + 24, Fdx + 5, Fdy, WstarAll(i), 14)
        Next
    End Sub

    Private Sub ButtonBye_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonBye.Click
        '結束程式
        Application.Exit()
    End Sub

    Private Sub UpdateScreen()
        Dim sMountain As Integer
        Dim eYungNo As Integer
        Dim sYungNo As Integer
        Dim yearFlow As Integer
        '坐山
        sMountain = ListBox3.SelectedIndex
        '地運  一二三四六七八九     
        eYungNo = SkyEarth(ListBox1.SelectedIndex, 1)
        '天運
        sYungNo = SkyEarth(ListBox1.SelectedIndex, 0)
        '年白
        yearFlow = ListBox2.SelectedIndex
        '計算
        DrawAll(sYungNo, eYungNo, sMountain, yearFlow)
    End Sub

    Private Sub ButtonAbout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonAbout.Click
        AboutBox1.Show()
    End Sub

    Public Sub DelayTms(ByVal Tms As Integer)
        Dim Start As Integer = Environment.TickCount()
        Dim TimeLast As Integer = Tms * 10 '要延遲 t 秒,就設為 t *1000
        Do
            If Environment.TickCount() - Start > TimeLast Then Exit Do
            Application.DoEvents() ' 要記得寫這行，不然都在跑迴圈，畫面可能會不見
        Loop
    End Sub

    Private Sub ButtonTest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonTest.Click
        Dim SaveFileName As String
        Dim Sky, Earth As Integer
        For i As Integer = 0 To 15
            Sky = SkyEarth(i, 0)
            Earth = SkyEarth(i, 1)
            For k As Integer = 0 To 23
                ListBox1.SelectedIndex = i
                ListBox3.SelectedIndex = k
                DrawAll(Sky, Earth, k, -1)
                '------------
                Label_Msg.Text = "圖檔存入目錄 C:\5ArtSave\  ...."
                SaveFileName = "C:\5ArtSave\" & ListBox1.SelectedItem & _
                                "運_" & ListBox3.SelectedIndex & ListBox3.SelectedItem & "山.jpg"
                PicDisplay.Refresh()
                PicDisplay.Image.Save(SaveFileName, System.Drawing.Imaging.ImageFormat.Jpeg)
                DelayTms(8)
            Next
        Next
    End Sub

    Private Sub ListBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox3.SelectedIndexChanged
        If (ListBox1.SelectedIndex >= 0) And (ListBox2.SelectedIndex >= 0) Then
            UpdateScreen()
        End If
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        If (ListBox3.SelectedIndex >= 0) And (ListBox2.SelectedIndex >= 0) Then
            UpdateScreen()
        End If
    End Sub

    Private Sub ListBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox2.SelectedIndexChanged
        If (ListBox1.SelectedIndex >= 0) And (ListBox3.SelectedIndex >= 0) Then
            UpdateScreen()
        End If
    End Sub
End Class