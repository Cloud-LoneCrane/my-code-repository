'
' Created by SharpDevelop.
' User: jiftle
' Date: 2010-7-3
' Time: 15:14 - 17:53 
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'

''' <summary>
''' 颜色选择组合框
''' </summary>
Public Class ColorComboBox

	''' <summary>
	'''  组合框数组
	''' </summary>
	Private m_arCbo() As ComboBox
	
	Public Sub New()
		
	End Sub
	
	''' <summary>
	''' 初始化
	''' </summary>
	Public Sub  New(byVal cboTmp As ComboBox)
		Try
			If cboTmp IsNot Nothing Then
				ReDim m_arCbo(0)
				
				m_arCbo(0) = cboTmp
				AddHandler m_arCbo(0).DrawItem,AddressOf DrawItem
				AddHandler m_arCbo(0).MeasureItem,AddressOf MeasureItem
				'Add All Known Color in Drawing.Brushes
			AddItems
		End If
	
			Catch ex As Exception
		#If Debug Then
			MsgBox(Ex.ToString)
		#Else
		#End If
	End Sub
	
	Public Sub New (ByVal arCbo() As ComboBox)
		try
		If arCbo IsNot Nothing Then
			ReDim m_arCbo(Ubound(arCbo))
			
			Dim i As Integer = 0
			For i = 0 To UBound(arCbo)
				If arCbo(i) IsNot Nothing Then
					m_arCbo(i) = arCbo(i)
					AddHandler m_arCbo(i).DrawItem, AddressOf DrawItem
					AddHandler m_arCbo(i).MeasureItem,AddressOf MeasureItem
				End If
			Next
			
			'Add All Known Color in Drawing.Brushes
			AddItems
		End If
		
			Catch ex As Exception
		#If Debug Then
			MsgBox(Ex.ToString)
		#Else
		#End If
	End Sub
	
	Public Sub ItemsAdd(ByVal cboTmp As ComboBox)
		Try
		If cboTmp IsNot Nothing Then
			If cboTmp.GetType Is GetType(ComboBox) Then
				If m_arCbo Is Nothing Then
					Redim m_arCbo(0)
					
					m_arCbo(0) = cboTmp
					AddHandler m_arCbo(0).DrawItem,AddressOf DrawItem
					AddHandler m_arCbo(0).MeasureItem,AddressOf MeasureItem
					AddItems
				Else
					ReDim Preserve m_arCbo(Ubound(m_arCbo) + 1)
					 m_arCbo(Ubound(m_arCbo)) = cboTmp
					AddHandler m_arCbo(Ubound(m_arCbo)).DrawItem,AddressOf DrawItem
					AddHandler m_arCbo(Ubound(m_arCbo)).MeasureItem,AddressOf MeasureItem
					AddItems
				End If
			
			End If
		End If
		
			Catch ex As Exception
		#If Debug Then
			MsgBox(Ex.ToString)
		#Else
		#End If
	End Sub
	
	
	Private Sub AddItems()
		Try
		If m_arCbo Is Nothing Then
			Return
		End If
		For Each tmp As ComboBox In m_arCbo
			If tmp IsNot Nothing Then
				If tmp.GetType() is GetType(ComboBox) Then
					With tmp
						.DropDownStyle =  ComboBoxStyle.DropDownList
						.DrawMode =DrawMode.OwnerDrawFixed
						.Items.Clear
						
						'Add All system known Color
						.Items.Add(System.Drawing.Brushes.Transparent)
						.Items.Add(System.Drawing.Brushes.AliceBlue)
						.Items.Add(System.Drawing.Brushes.AntiqueWhite)
						.Items.Add(System.Drawing.Brushes.Aqua)
						.Items.Add(System.Drawing.Brushes.Aquamarine)
						.Items.Add(System.Drawing.Brushes.Azure)
						.Items.Add(System.Drawing.Brushes.Beige)
						.Items.Add(System.Drawing.Brushes.Bisque)
						.Items.Add(System.Drawing.Brushes.Black)
						.Items.Add(System.Drawing.Brushes.BlanchedAlmond)
						.Items.Add(System.Drawing.Brushes.Blue)
						.Items.Add(System.Drawing.Brushes.BlueViolet)
						.Items.Add(System.Drawing.Brushes.Brown)
						.Items.Add(System.Drawing.Brushes.BurlyWood)
						.Items.Add(System.Drawing.Brushes.CadetBlue)
						.Items.Add(System.Drawing.Brushes.Chartreuse)
						.Items.Add(System.Drawing.Brushes.Chocolate)
						.Items.Add(System.Drawing.Brushes.Coral)
						.Items.Add(System.Drawing.Brushes.CornflowerBlue)
						.Items.Add(System.Drawing.Brushes.Cornsilk)
						.Items.Add(System.Drawing.Brushes.Crimson)
						.Items.Add(System.Drawing.Brushes.Cyan)
						.Items.Add(System.Drawing.Brushes.DarkBlue)
						.Items.Add(System.Drawing.Brushes.DarkCyan)
						.Items.Add(System.Drawing.Brushes.DarkGoldenrod)
						.Items.Add(System.Drawing.Brushes.DarkGray)
						.Items.Add(System.Drawing.Brushes.DarkGreen)
						.Items.Add(System.Drawing.Brushes.DarkKhaki)
						.Items.Add(System.Drawing.Brushes.DarkMagenta)
						.Items.Add(System.Drawing.Brushes.DarkOliveGreen)
						.Items.Add(System.Drawing.Brushes.DarkOrange)
						.Items.Add(System.Drawing.Brushes.DarkOrchid)
						.Items.Add(System.Drawing.Brushes.DarkRed)
						.Items.Add(System.Drawing.Brushes.DarkSalmon)
						.Items.Add(System.Drawing.Brushes.DarkSeaGreen)
						.Items.Add(System.Drawing.Brushes.DarkSlateBlue)
						.Items.Add(System.Drawing.Brushes.DarkSlateGray)
						.Items.Add(System.Drawing.Brushes.DarkTurquoise)
						.Items.Add(System.Drawing.Brushes.DarkViolet)
						.Items.Add(System.Drawing.Brushes.DeepPink)
						.Items.Add(System.Drawing.Brushes.DeepSkyBlue)
						.Items.Add(System.Drawing.Brushes.DimGray)
						.Items.Add(System.Drawing.Brushes.DodgerBlue)
						.Items.Add(System.Drawing.Brushes.Firebrick)
						.Items.Add(System.Drawing.Brushes.FloralWhite)
						.Items.Add(System.Drawing.Brushes.ForestGreen)
						.Items.Add(System.Drawing.Brushes.Fuchsia)
						.Items.Add(System.Drawing.Brushes.Gainsboro)
						.Items.Add(System.Drawing.Brushes.GhostWhite)
						.Items.Add(System.Drawing.Brushes.Gold)
						.Items.Add(System.Drawing.Brushes.Goldenrod)
						.Items.Add(System.Drawing.Brushes.Gray)
						.Items.Add(System.Drawing.Brushes.Green)
						.Items.Add(System.Drawing.Brushes.GreenYellow)
						.Items.Add(System.Drawing.Brushes.Honeydew)
						.Items.Add(System.Drawing.Brushes.HotPink)
						.Items.Add(System.Drawing.Brushes.IndianRed)
						.Items.Add(System.Drawing.Brushes.Indigo)
						.Items.Add(System.Drawing.Brushes.Ivory)
						.Items.Add(System.Drawing.Brushes.Khaki)
						.Items.Add(System.Drawing.Brushes.Lavender)
						.Items.Add(System.Drawing.Brushes.LavenderBlush)
						.Items.Add(System.Drawing.Brushes.LawnGreen)
						.Items.Add(System.Drawing.Brushes.LemonChiffon)
						.Items.Add(System.Drawing.Brushes.LightBlue)
						.Items.Add(System.Drawing.Brushes.LightCoral)
						.Items.Add(System.Drawing.Brushes.LightCyan)
						.Items.Add(System.Drawing.Brushes.LightGoldenrodYellow)
						.Items.Add(System.Drawing.Brushes.LightGreen)
						.Items.Add(System.Drawing.Brushes.LightGray)
						.Items.Add(System.Drawing.Brushes.LightPink)
						.Items.Add(System.Drawing.Brushes.LightSalmon)
						.Items.Add(System.Drawing.Brushes.LightSeaGreen)
						.Items.Add(System.Drawing.Brushes.LightSkyBlue)
						.Items.Add(System.Drawing.Brushes.LightSlateGray)
						.Items.Add(System.Drawing.Brushes.LightSteelBlue)
						.Items.Add(System.Drawing.Brushes.LightYellow)
						.Items.Add(System.Drawing.Brushes.Lime)
						.Items.Add(System.Drawing.Brushes.LimeGreen)
						.Items.Add(System.Drawing.Brushes.Linen)
						.Items.Add(System.Drawing.Brushes.Magenta)
						.Items.Add(System.Drawing.Brushes.Maroon)
						.Items.Add(System.Drawing.Brushes.MediumAquamarine)
						.Items.Add(System.Drawing.Brushes.MediumBlue)
						.Items.Add(System.Drawing.Brushes.MediumOrchid)
						.Items.Add(System.Drawing.Brushes.MediumPurple)
						.Items.Add(System.Drawing.Brushes.MediumSeaGreen)
						.Items.Add(System.Drawing.Brushes.MediumSlateBlue)
						.Items.Add(System.Drawing.Brushes.MediumSpringGreen)
						.Items.Add(System.Drawing.Brushes.MediumTurquoise)
						.Items.Add(System.Drawing.Brushes.MediumVioletRed)
						.Items.Add(System.Drawing.Brushes.MidnightBlue)
						.Items.Add(System.Drawing.Brushes.MintCream)
						.Items.Add(System.Drawing.Brushes.MistyRose)
						.Items.Add(System.Drawing.Brushes.Moccasin)
						.Items.Add(System.Drawing.Brushes.NavajoWhite)
						.Items.Add(System.Drawing.Brushes.Navy)
						.Items.Add(System.Drawing.Brushes.OldLace)
						.Items.Add(System.Drawing.Brushes.Olive)
						.Items.Add(System.Drawing.Brushes.OliveDrab)
						.Items.Add(System.Drawing.Brushes.Orange)
						.Items.Add(System.Drawing.Brushes.OrangeRed)
						.Items.Add(System.Drawing.Brushes.Orchid)
						.Items.Add(System.Drawing.Brushes.PaleGoldenrod)
						.Items.Add(System.Drawing.Brushes.PaleGreen)
						.Items.Add(System.Drawing.Brushes.PaleTurquoise)
						.Items.Add(System.Drawing.Brushes.PaleVioletRed)
						.Items.Add(System.Drawing.Brushes.PapayaWhip)
						.Items.Add(System.Drawing.Brushes.PeachPuff)
						.Items.Add(System.Drawing.Brushes.Peru)
						.Items.Add(System.Drawing.Brushes.Pink)
						.Items.Add(System.Drawing.Brushes.Plum)
						.Items.Add(System.Drawing.Brushes.PowderBlue)
						.Items.Add(System.Drawing.Brushes.Purple)
						.Items.Add(System.Drawing.Brushes.Red)
						.Items.Add(System.Drawing.Brushes.RosyBrown)
						.Items.Add(System.Drawing.Brushes.RoyalBlue)
						.Items.Add(System.Drawing.Brushes.SaddleBrown)
						.Items.Add(System.Drawing.Brushes.Salmon)
						.Items.Add(System.Drawing.Brushes.SandyBrown)
						.Items.Add(System.Drawing.Brushes.SeaGreen)
						.Items.Add(System.Drawing.Brushes.SeaShell)
						.Items.Add(System.Drawing.Brushes.Sienna)
						.Items.Add(System.Drawing.Brushes.Silver)
						.Items.Add(System.Drawing.Brushes.SkyBlue)
						.Items.Add(System.Drawing.Brushes.SlateBlue)
						.Items.Add(System.Drawing.Brushes.SlateGray)
						.Items.Add(System.Drawing.Brushes.Snow)
						.Items.Add(System.Drawing.Brushes.SpringGreen)
						.Items.Add(System.Drawing.Brushes.SteelBlue)
						.Items.Add(System.Drawing.Brushes.Tan)
						.Items.Add(System.Drawing.Brushes.Teal)
						.Items.Add(System.Drawing.Brushes.Thistle)
						.Items.Add(System.Drawing.Brushes.Tomato)
						.Items.Add(System.Drawing.Brushes.Turquoise)
						.Items.Add(System.Drawing.Brushes.Violet)
						.Items.Add(System.Drawing.Brushes.Wheat)
						.Items.Add(System.Drawing.Brushes.White)
						.Items.Add(System.Drawing.Brushes.WhiteSmoke)
						.Items.Add(System.Drawing.Brushes.Yellow)
						.Items.Add(System.Drawing.Brushes.YellowGreen)
						
					End With
				End If
			End If
		Next
		
		Catch ex As Exception
		#If Debug Then
			MsgBox(Ex.ToString)
		#Else
		#End If
			
		End Try
		
	End Sub
	
	Sub MeasureItem(sender As Object ,e As MeasureItemEventArgs)
		Try
			if sender isNot Nothing
				e.ItemHeight = CType(sender,Combobox).ItemHeight -2
		End If
			Catch ex As Exception
		#If Debug Then
			MsgBox(Ex.ToString)
		#Else
		#End If
	End Sub
	
	
	''' <summary>
	''' 绘制Item
	''' </summary>
	Private Sub DrawItem(sender As Object,e As System.Windows.Forms.DrawItemEventArgs)
		try
		If e.Index = -1 Then
			Return
		End If
		
		If Sender Is Nothing Then
			return
		End If
		
		Dim cboTmp As ComboBox = CType(sender,ComboBox)
		Dim BrushTmp As System.Drawing.SolidBrush = _
			CType(cboTmp.Items(e.Index),System.Drawing.SolidBrush)
			
		Dim g As Graphics = e.Graphics
		
		'if the items is selected ,then draw background and frame
		e.DrawBackground()
		e.DrawFocusRectangle()
		
		'绘制颜色预览框
		Dim rectTmp As Rectangle = e.Bounds
		
		With rectTmp
			.Offset(2,2)
			.Width = 25
			.Height -= 4
		End With
		
		g.DrawRectangle(New Pen(e.ForeColor),rectTmp)
		
		With rectTmp
			.Offset(1,1)
			.Width -= 2
			.Height -=2
			
		End With
		
		g.FillRectangle(brushTmp,rectTmp)
		g.DrawString(brushTmp.Color.Name.ToString(),Ctype(sender,Control).Font, _
			New SolidBrush(e.ForeColor),e.Bounds.X+35,e.Bounds.Y +1)
		
		Catch ex As Exception
		#If Debug Then 
			MsgBox(Ex.ToString)
		#Else
		#end if 
	End Sub
End Class
