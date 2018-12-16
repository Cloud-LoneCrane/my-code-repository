
'* ******************************************************* *
'*    程序名称：DelMe.bas
'*    程序功能：在VB中实现应用程序自删除
'*    作者：lyserver
'*    联系方式：http://blog.csdn.net/lyserver
'* ******************************************************* *
Public Sub DelMe()
    Dim fn As Integer
    If App.LogMode Then
        fn = FreeFile()
        Open "del.tmp.vbs" For Output As fn
        Print #fn, "Dim FSO,WMI"
        Print #fn, "Set WMI=GetObject(" & Chr(34) & "winmgmts:\\." & Chr(34) & ")"
        Print #fn, "Set FSO=CreateObject(" & Chr(34) & "Scripting.FileSystemObject" & Chr(34) & ")"
        Print #fn, "Do While WMI.ExecQuery(" & Chr(34) & _
                "SELECT * FROM WIN32_PROCESS WHERE NAME='" & App.EXEName & ".EXE'" & Chr(34) & ").Count"
        Print #fn, "WScript.Sleep 1"
        Print #fn, "Loop"
        Print #fn, "FSO.DeleteFile " & Chr(34) & App.Path & "\"; App.EXEName & ".EXE" & Chr(34)
        Print #fn, "FSO.DeleteFile " & Chr(34) & App.Path & "\del.tmp.vbs" & Chr(34)
        Print #fn, "Set FSO=Nothing"
        Print #fn, "Set WMI=Nothing"
        Close #fn
        Shell "WScript.Exe del.tmp.vbs", vbHide
    End If
End Sub
