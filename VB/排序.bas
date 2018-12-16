Sub QuickSort(List() As Double)
'   Sorts an array using Quick Sort algorithm
'   Adapted from "Visual Basic Developers Guide"
'   By D.F. Scott

    Dim i As Double, j As Double, b As Double
    Dim l As Double, t As Double, r As Double, d As Double

    Dim p(1 To 100) As Double
    Dim w(1 To 100) As Double

    k = 1
    p(k) = LBound(List)
    w(k) = UBound(List)
    l = 1
    d = 1
    r = UBound(List)
    Do
toploop:
        If r - l < 9 Then GoTo bubsort
        i = l
        j = r
        While j > i
           comp = comp + 1
           If List(i) > List(j) Then
               swic = swic + 1
               t = List(j)
               oldx1 = List(j)
               oldy1 = j
               List(j) = List(i)
               oldx2 = List(i)
               oldy2 = i
               newx1 = List(j)
               newy1 = j
               List(i) = t
               newx2 = List(i)
               newy2 = i
               d = -d
           End If
           If d = -1 Then
               j = j - 1
                Else
                    i = i + 1
           End If
       Wend
           j = j + 1
           k = k + 1
            If i - l < r - j Then
                p(k) = j
                w(k) = r
                r = i
                Else
                    p(k) = l
                    w(k) = i
                    l = j
            End If
            d = -d
            GoTo toploop
bubsort:
    If r - l > 0 Then
        For i = l To r
            b = i
            For j = b + 1 To r
                comp = comp + 1
                If List(j) <= List(b) Then b = j
            Next j
            If i <> b Then
                swic = swic + 1
                t = List(b)
                oldx1 = List(b)
                oldy1 = b
                List(b) = List(i)
                oldx2 = List(i)
                oldy2 = i
                newx1 = List(b)
                newy1 = b
                List(i) = t
                newx2 = List(i)
                newy2 = i
            End If
        Next i
    End If
    l = p(k)
    r = w(k)
    k = k - 1
    Loop Until k = 0
End Sub
Sub BubbleSort(List() As Double)
'   Sorts an array using bubble sort algorithm

    Dim First As Double, Last As Double
    Dim i As Integer, j As Integer
    Dim Temp As Double
    
    First = LBound(List)
    Last = UBound(List)
    For i = First To Last - 1
        For j = i + 1 To Last
            If List(i) > List(j) Then
                Temp = List(j)
                List(j) = List(i)
                List(i) = Temp
            End If
        Next j
    Next i
End Sub
