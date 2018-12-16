Imports System.Runtime.InteropServices
Imports System.MarshalByRefObject

Namespace YQSSocket
    ''' <summary>
    ''' ��¼��������
    ''' </summary>
    ''' <remarks></remarks>
    Public Class clsDataSetStream

        ''' <summary>
        ''' ������
        ''' </summary>
        ''' <remarks></remarks>
        Dim m_bytBufTmp(65536 - 1) As Byte
        ''' <summary>
        ''' δ���������Ϣ����
        ''' </summary>
        ''' <remarks></remarks>
        Dim m_intUnCompleteIndex As Int32 = 0

        ''' <summary>
        ''' �ṹ�����黺����
        ''' </summary>
        ''' <remarks>����Ѿ���ȷ��ȡ�Ľṹ��</remarks>
        Protected m_Buf(1024) As Object
        ''' <summary>
        '''��Ч��¼����
        ''' </summary>
        ''' <remarks>�ṹ�����黺������ ��Ч��¼����</remarks>
        Protected m_ItemCount As Int64 = 0

        ''' <summary>
        ''' ���ν��յĽṹ�������
        ''' </summary>
        ''' <remarks>������enmStructTypeö�ٶ������ҵ���Ӧ��˵��</remarks>
        Private m_enmStructType As enmStructType = Nothing

        ''' <summary>
        ''' ���ν������
        ''' </summary>
        ''' <remarks></remarks>
        Private bln_DisposedFlag As Boolean = False

        ''' <summary>
        ''' ��������
        ''' </summary>
        ''' <returns>Int32</returns>
        ''' <remarks></remarks>
        Private Function GetRandNum() As Int32
            GetRandNum = 0
            Try

                Dim intTmp As Int32 = 0
                Dim RandGenerater As New Random '�����������

                intTmp = RandGenerater.Next(500, 6000)
                GetRandNum = intTmp
            Catch ex As Exception
                ErrorHandler(ex.ToString)
            End Try
        End Function

        ''' <summary>
        ''' ���ͽṹ��
        ''' </summary>
        ''' <param name="structType"></param>
        ''' <param name="struct"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function SendStruct(ByVal structType As enmStructType, ByVal struct As Object, ByVal Sock As clsSock)
            SendStruct = False
            Try

                Dim bytBuf() As Byte
                Dim bytSend() As Byte
                Dim msgStruct As strtDataSet '������װʵ�ʽṹ��Ľṹ
                Dim structSize As Int32 = 0 '��Ҫ��װ�ṹ���С
                Dim intRandNum As Int32 = 0 '�����
                Dim RandGenerater As New Random '�����������
                Dim intValidLength As Int32 = 0  '��װ�����Ч����

                intRandNum = RandGenerater.Next(500, 6000) '��������ڱ�����Ϣ����֤

                '�ṹ��ת��Ϊ�ֽ�����
                bytBuf = clsMsgComposer.RawSerialize(struct)
                With msgStruct
                    .StructType = structType
                    .CurIndex = 0
                    .Count = 1
                    .TotalCount = 1

                    '���·������ݴ洢���Ĵ�С
                    ReDim .Data(DataBufFixedMax - 1)
                    Array.Copy(bytBuf, .Data, bytBuf.GetLength(0))
                End With

                '�ṹ���ʵ�ʴ�С
                structSize = Marshal.SizeOf(struct)

                '��װ��ṹ���ʵ�ʳ���
                intValidLength = Marshal.SizeOf(msgStruct) - DataBufFixedMax + structSize

                ReDim bytBuf(0)

                bytBuf = clsMsgComposer.RawSerialize(msgStruct) '���Զ���ṹ��ת��Ϊ�ֽ�����

                '��װΪ��׼��Ϣ
                bytSend = clsMsgComposer.MakeCommonCmd(MsgType.DataSetStream, intRandNum, bytBuf, intValidLength)

                '����
                Sock.Send(bytSend)

                SendStruct = True
            Catch ex As Exception
                ErrorHandler(ex.ToString)
                Return Nothing
            End Try
        End Function

        ''' <summary>
        ''' ���սṹ��
        ''' </summary>
        ''' <remarks>��Ϊ���ܵ�ʱ�򣬿��ܴ���һ�������Ľṹ��Ҫ��ν��ղ�����ȫ���꣨����ʹ�û��壩�����Ա���������ʵ��</remarks>
        Public Sub ReciveStruct(ByVal skRecv As YQSSocket.clsSock, _
                         ByVal bytBuf() As Byte, ByVal intRecved As Integer)
            Try
                Dim arTmp As New ArrayList
                Dim i As Int32 = 0
                Dim Msg As modTypes.MsgCommonCmd
                Dim t As strtDataSet
                Dim Data As Object
                Dim AnyType As Type

                arTmp = YQSSocket.clsMsgComposer.PreprocessMsg(m_bytBufTmp, m_intUnCompleteIndex, bytBuf, intRecved)


                For i = 0 To arTmp.Count - 1

                    Msg = YQSSocket.clsMsgComposer.RawDeserialize(arTmp(i), GetType(modTypes.MsgCommonCmd))

                    If Msg.Header.PacketType = MsgType.RequestDisposed Then
                        bln_DisposedFlag = True
                        Exit For
                    Else
                        t = YQSSocket.clsMsgComposer.RawDeserialize(Msg.Command.Data, GetType(strtDataSet))

                        m_enmStructType = t.StructType '���ν��յĽṹ������
                        AnyType = GetActualType(t.StructType)
                        Data = YQSSocket.clsMsgComposer.RawDeserialize(t.Data, AnyType)

                        'д�뵽������
                        m_Buf(m_ItemCount) = Data
                        m_ItemCount += 1 '���µ�ǰ�ѽ��սṹ�����       
                    End If
                Next

            Catch ex As Exception
                ErrorHandler(ex.ToString)
            End Try
        End Sub

        ''' <summary>
        ''' ����Ѿ����յ��ṹ��
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetStructArray() As Object()
            GetStructArray = Nothing
            Try
                Dim arObj() As Object

                If m_ItemCount > 0 Then
                    ReDim arObj(m_ItemCount - 1)
                    Array.Copy(m_Buf, 0, arObj, 0, m_ItemCount)

                    Return arObj
                Else
                    Return Nothing
                End If

            Catch ex As Exception
                ErrorHandler(ex.ToString)
                Return Nothing
            End Try
        End Function

        ''' <summary>
        ''' ���ͽṹ������
        ''' </summary>
        ''' <param name="StructType"></param>
        ''' <param name="structArray"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function SendStructArray(Of T)(ByVal StructType As enmStructType, ByVal structArray() As T, ByVal Sock As clsSock) As Boolean
            SendStructArray = False
            Try
                'ѭ������ÿһ���ṹ��
                Dim i As Int32 = 0
                For i = 0 To structArray.Length - 1
                    SendStruct(StructType, structArray(i), Sock)
                Next

                Dim bytSend() As Byte

                '���ͽṹ���ѷ�����ϵ���Ϣ
                bytSend = clsMsgComposer.MakeCommonCmd(MsgType.RequestDisposed, GetRandNum, "") '��װΪ��׼��Ϣ

                '����
                Sock.Send(bytSend)

                SendStructArray = True
            Catch ex As Exception
                ErrorHandler(ex.ToString)
                SendStructArray = False
            End Try
        End Function

        ''' <summary>
        ''' �Ѿ����յļ�¼����
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ReadOnly Property ItemCount() As Int32
            Get
                Return m_ItemCount
            End Get
        End Property

        ''' <summary>
        ''' ���������Ƿ�������
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ReadOnly Property DisposedFlag() As Boolean
            Get
                Return bln_DisposedFlag
            End Get
        End Property

        ''' <summary>
        ''' ��õ�ǰ�Ľṹ���Type
        ''' </summary>
        ''' <param name="StructType"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetActualType(ByVal StructType As enmStructType) As Type

            Select Case StructType
                Case enmStructType.AccInfo
                    GetActualType = GetType(strtZhxx)
                Case enmStructType.CertType
                    GetActualType = GetType(strtCertType)
                Case Else
                    GetActualType = Nothing
            End Select

        End Function

        Protected Overrides Sub Finalize()
            ReDim m_bytBufTmp(0)
            ReDim m_Buf(0)

            MyBase.Finalize()
        End Sub
    End Class

End Namespace
