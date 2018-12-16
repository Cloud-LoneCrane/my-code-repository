Imports System.Runtime.InteropServices
Imports System.MarshalByRefObject

Namespace YQSSocket
    ''' <summary>
    ''' 记录集传输类
    ''' </summary>
    ''' <remarks></remarks>
    Public Class clsDataSetStream

        ''' <summary>
        ''' 缓冲区
        ''' </summary>
        ''' <remarks></remarks>
        Dim m_bytBufTmp(65536 - 1) As Byte
        ''' <summary>
        ''' 未处理完的消息索引
        ''' </summary>
        ''' <remarks></remarks>
        Dim m_intUnCompleteIndex As Int32 = 0

        ''' <summary>
        ''' 结构体数组缓冲区
        ''' </summary>
        ''' <remarks>存放已经正确获取的结构体</remarks>
        Protected m_Buf(1024) As Object
        ''' <summary>
        '''有效记录个数
        ''' </summary>
        ''' <remarks>结构体数组缓冲区中 有效记录个数</remarks>
        Protected m_ItemCount As Int64 = 0

        ''' <summary>
        ''' 本次接收的结构体的类型
        ''' </summary>
        ''' <remarks>可以在enmStructType枚举定义里找到对应的说明</remarks>
        Private m_enmStructType As enmStructType = Nothing

        ''' <summary>
        ''' 本次接收完毕
        ''' </summary>
        ''' <remarks></remarks>
        Private bln_DisposedFlag As Boolean = False

        ''' <summary>
        ''' 获得随机数
        ''' </summary>
        ''' <returns>Int32</returns>
        ''' <remarks></remarks>
        Private Function GetRandNum() As Int32
            GetRandNum = 0
            Try

                Dim intTmp As Int32 = 0
                Dim RandGenerater As New Random '随机数生成器

                intTmp = RandGenerater.Next(500, 6000)
                GetRandNum = intTmp
            Catch ex As Exception
                ErrorHandler(ex.ToString)
            End Try
        End Function

        ''' <summary>
        ''' 发送结构体
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
                Dim msgStruct As strtDataSet '用来包装实际结构体的结构
                Dim structSize As Int32 = 0 '需要包装结构体大小
                Dim intRandNum As Int32 = 0 '随机数
                Dim RandGenerater As New Random '随机数生成器
                Dim intValidLength As Int32 = 0  '包装后的有效长度

                intRandNum = RandGenerater.Next(500, 6000) '随机数用于本次消息的验证

                '结构体转化为字节数组
                bytBuf = clsMsgComposer.RawSerialize(struct)
                With msgStruct
                    .StructType = structType
                    .CurIndex = 0
                    .Count = 1
                    .TotalCount = 1

                    '重新分配数据存储区的大小
                    ReDim .Data(DataBufFixedMax - 1)
                    Array.Copy(bytBuf, .Data, bytBuf.GetLength(0))
                End With

                '结构体的实际大小
                structSize = Marshal.SizeOf(struct)

                '包装后结构体的实际长度
                intValidLength = Marshal.SizeOf(msgStruct) - DataBufFixedMax + structSize

                ReDim bytBuf(0)

                bytBuf = clsMsgComposer.RawSerialize(msgStruct) '把自定义结构体转化为字节数组

                '封装为标准消息
                bytSend = clsMsgComposer.MakeCommonCmd(MsgType.DataSetStream, intRandNum, bytBuf, intValidLength)

                '发送
                Sock.Send(bytSend)

                SendStruct = True
            Catch ex As Exception
                ErrorHandler(ex.ToString)
                Return Nothing
            End Try
        End Function

        ''' <summary>
        ''' 接收结构体
        ''' </summary>
        ''' <remarks>因为接受的时候，可能存在一个完整的结构需要多次接收才能完全收完（必须使用缓冲），所以必须具有类的实例</remarks>
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

                        m_enmStructType = t.StructType '本次接收的结构的类型
                        AnyType = GetActualType(t.StructType)
                        Data = YQSSocket.clsMsgComposer.RawDeserialize(t.Data, AnyType)

                        '写入到缓冲区
                        m_Buf(m_ItemCount) = Data
                        m_ItemCount += 1 '更新当前已接收结构体个数       
                    End If
                Next

            Catch ex As Exception
                ErrorHandler(ex.ToString)
            End Try
        End Sub

        ''' <summary>
        ''' 获得已经接收到结构体
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
        ''' 发送结构体数组
        ''' </summary>
        ''' <param name="StructType"></param>
        ''' <param name="structArray"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function SendStructArray(Of T)(ByVal StructType As enmStructType, ByVal structArray() As T, ByVal Sock As clsSock) As Boolean
            SendStructArray = False
            Try
                '循环发送每一个结构体
                Dim i As Int32 = 0
                For i = 0 To structArray.Length - 1
                    SendStruct(StructType, structArray(i), Sock)
                Next

                Dim bytSend() As Byte

                '发送结构体已发送完毕的消息
                bytSend = clsMsgComposer.MakeCommonCmd(MsgType.RequestDisposed, GetRandNum, "") '封装为标准消息

                '发送
                Sock.Send(bytSend)

                SendStructArray = True
            Catch ex As Exception
                ErrorHandler(ex.ToString)
                SendStructArray = False
            End Try
        End Function

        ''' <summary>
        ''' 已经接收的记录条数
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
        ''' 本次数据是否接收完毕
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
        ''' 获得当前的结构体的Type
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
