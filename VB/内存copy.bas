'����Ĭ����ByRef ����ַ����
Public Declare Sub CopyMemory Lib "kernel32" Alias "RtlMoveMemory" _
(Destination As Any, Source As Any, ByVal Length As Long)

Declare Function VarPtrArray Lib "msvbvm60.dll" Alias "VarPtr" _
(Var() As Any) As Long
