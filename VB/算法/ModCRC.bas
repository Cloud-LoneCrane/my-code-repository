Attribute VB_Name = "ModCRC"
Option Explicit

Private CRCTable(0 To 255) As Long
'========================================================================
'���ܣ�����ļ���CRC32У����
'������
'   FileName As String  '�ļ���
'   Optional ByVal IsFile As Boolean = True �Ƿ����ļ�
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
'���ܣ�����ļ���CRC32У����
'������
'  ByRef bArrayIn() As Byte '��Ҫ���CRC32У���������
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
'���ܣ�����CRC32��
'������
'   ��
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
