Private Sub ShowDrives()
	Dim d() As String
	d = System.IO.Directory.GetLogicalDrives()

	'·¨ 1
	Dim en As System.Collections.IEnumerator
	en = d.GetEnumerator
	While en.MoveNext
		Console.WriteLine(CStr(en.Current))
	End While

	Console.WriteLine("")
	'·¨ 2
	For Each e As String In d
		Console.WriteLine(e)
	Next

	Console.WriteLine("")
	'·¨ 3
	Dim i As Int32 = 0
	For i = 0 To d.Length - 1
		Console.WriteLine(d(i))
	Next
End Sub
