
 'ȷ��
   MessageBox.Show("����������Ϊ��", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
  
  '�Ƿ�
          If m_strUserType = "" OrElse m_strRateTypeName = "" OrElse m_strDayOfWeek = "" Then
            MessageBox.Show("��ѡ��Ҫ���õķ�������", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If