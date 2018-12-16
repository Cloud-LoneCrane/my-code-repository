
 '确定
   MessageBox.Show("区域名不能为空", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
  
  '是否
          If m_strUserType = "" OrElse m_strRateTypeName = "" OrElse m_strDayOfWeek = "" Then
            MessageBox.Show("请选择要设置的费率类型", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If