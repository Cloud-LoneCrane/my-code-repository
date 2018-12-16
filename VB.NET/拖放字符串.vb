 Sub Button1Click(sender As Object, e As EventArgs)
    	Me.textBox1.AllowDrop = True
    	Me.textBox2.AllowDrop = True
    End Sub
    
    Sub TextBox1_MouseDown(ByVal sender As System.Object,ByVal e As System.Windows.Forms.MouseEventArgs ) Handles textBox1.MouseDown
    	If e.Button = MouseButtons.Left Then 
    		Me.textBox1.SelectAll()
    		Me.textBox1.DoDragDrop(Me.textBox1.SelectedText, _
    			DragDropEffects.Move Or DragDropEffects.Copy)
    	End If
    End Sub
    
    '当用户在拖放操作过程中首次将鼠标光标拖到控件上时发生
    Sub TextBox2_DragEnter(sender As System.Object,ByVal e As System.Windows.Forms.DragEventArgs) Handles textBox2.DragEnter
    	If e.Data.GetDataPresent(DataFormats.Text) Then 
    		e.Effect = DragDropEffects.Copy
    	End If
    End Sub
    
    '在完成TextBox1中的文字拖放到TextBox2时发生 
    Sub TextBox2_DragDrop(ByVal sender As Object,ByVal e As System.Windows.Forms.DragEventArgs) Handles TextBox2.DragDrop
 Me.textBox2.Text = e.Data.GetData(DataFormats.Text).ToString() 
   	
   	'判断是否按下Ctrl
   	If (e.KeyState & 8) <> 8 Then 
   		Me.textBox1.Text = ""
   	End If
  	
    End Sub Sub Button1Click(sender As Object, e As EventArgs)
    	Me.textBox1.AllowDrop = True
    	Me.textBox2.AllowDrop = True
    End Sub
    
    Sub TextBox1_MouseDown(ByVal sender As System.Object,ByVal e As System.Windows.Forms.MouseEventArgs ) Handles textBox1.MouseDown
    	If e.Button = MouseButtons.Left Then 
    		Me.textBox1.SelectAll()
    		Me.textBox1.DoDragDrop(Me.textBox1.SelectedText, _
    			DragDropEffects.Move Or DragDropEffects.Copy)
    	End If
    End Sub
    
    '当用户在拖放操作过程中首次将鼠标光标拖到控件上时发生
    Sub TextBox2_DragEnter(sender As System.Object,ByVal e As System.Windows.Forms.DragEventArgs) Handles textBox2.DragEnter
    	If e.Data.GetDataPresent(DataFormats.Text) Then 
    		e.Effect = DragDropEffects.Copy
    	End If
    End Sub
    
    '在完成TextBox1中的文字拖放到TextBox2时发生 
    Sub TextBox2_DragDrop(ByVal sender As Object,ByVal e As System.Windows.Forms.DragEventArgs) Handles TextBox2.DragDrop
 Me.textBox2.Text = e.Data.GetData(DataFormats.Text).ToString() 
   	
   	'判断是否按下Ctrl
   	If (e.KeyState & 8) <> 8 Then 
   		Me.textBox1.Text = ""
   	End If
  	
    End Sub