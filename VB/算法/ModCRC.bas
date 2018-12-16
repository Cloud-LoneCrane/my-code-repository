Attribute VB_Name = "ModCRC"
Option Explicit

Private CRCTable(0 To 255) As Long
'========================================================================
'功能：获得文件的CRC32校验码
'参数：
'   FileName As String  '文件名
'   Optional ByVal IsFile As Boolean = True 是否是文件
'========================================================================
Public Function GetCRC32(FileName As String, Optional ByVal IsFile As Boolean = True) As String
    Dim FileDat() As Byte
    If Dir(FileName) <> "" And IsFile = True Then
        Open FileName For Binary As #1
        ReDim FileDat(FileLen(FileName) - 1)
        Get #1, , FileDat
        Close #1
        Call BuildTable
        GetCRC32 = Hex(CRC32(FileDat, UBound(FileDat())))
    Else
        GetCRC32 = 0
    End If
End Function
'========================================================================
'功能：获得文件的CRC32校验码
'参数：
'  ByRef bArrayIn() As Byte '需要获得CRC32校验码的数据
'========================================================================
Private Function CRC32(ByRef bArrayIn() As Byte, ByVal lLen As Long, Optional ByVal lcrc As Long = 0) As Long
    Dim lCurPos As Long
    Dim lTemp As Long
    
    lTemp = lcrc Xor &HFFFFFFFF
    
    For lCurPos = 0 To lLen
     lTemp = (((lTemp And &HFFFFFF00) \ &H100) And &HFFFFFF) Xor (CRCTable((lTemp And 255) Xor bArrayIn(lCurPos)))
    Next lCurPos
    
    CRC32 = lTemp Xor &HFFFFFFFF
End Function

'========================================================================
'功能：建立CRC32表
'参数：
'   无
'========================================================================
Private Function BuildTable() As Boolean
    Dim i As Long, x As Long, crc As Long
    Const Limit = &HEDB88320
    For i = 0 To 255
        crc = i
        For x = 0 To 7
            If crc And 1 Then
             crc = (((crc And &HFFFFFFFE) \ 2) And &H7FFFFFFF) Xor Limit
            Else
             crc = ((crc And &HFFFFFFFE) \ 2) And &H7FFFFFFF
            End If
        Next x
        CRCTable(i) = crc
    Next i
End Function
