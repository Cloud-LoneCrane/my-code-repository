Imports System
Imports System.Net
Imports System.IO
Imports System.Text
Imports System.Threading
Imports System.Net.Sockets
Imports System.Runtime.InteropServices

Public Class clsSock
    Public Declare Unicode Function send Lib "ws2_32.dll" (ByVal s As Int32, <MarshalAs(UnmanagedType.LPArray)> ByVal buf() As Byte, ByVal len As Int32, ByVal flags As Int32) As Integer

    Public Structure ConnectionInfo
        Public skAccept As clsSock
        Public bytBuf() As Byte
    End Structure

    Private m_skMine As Socket                          'Socket对象

    Private m_ciSocks As New List(Of ConnectionInfo)    '侦听Socket所接受的连接列表

    Private m_bytBuf(DefaultSockBufSize - 1) As Byte                      '接收数据缓冲区
    Private m_epRemote As IPEndPoint                    '暂存远端主机地址和端口

    Private m_blnOpened As Boolean                      'Socket是否打开

    Private m_strRemoteHost As String                   '远端(对方)地址
    Private m_intRemotePort As Integer                  '远端(对方)端口
    Private m_enmProtocol As ProtocolType               '协议类型 一般是TCP或UDP
    Private m_strLocalHost As String                    '本地地址
    Private m_intLocalPort As Integer                   '本地端口

    Private m_arIncompleteBuf(65535) As Byte           'Sock接收时未处理完的数据
    Private m_intIncompleteLen As Integer               'Sock接收时未处理完的数据的长度

    Private m_intError As Integer

    ''' <summary>
    ''' 侦听时接受到连接后的通知事件
    ''' </summary>
    ''' <param name="skAccept">接受连接后分配的clsSock对象</param>
    ''' <remarks></remarks>
    Event OnAccept(ByVal skAccept As clsSock)
    ''' <summary>
    ''' 发送数据后的通知事件
    ''' </summary>
    ''' <param name="skSend">发送数据的clsSock对象</param>
    ''' <param name="intSent">发送出去的字节数</param>
    ''' <remarks></remarks>
    Event OnSent(ByVal skSend As clsSock, ByVal intSent As Integer)
    ''' <summary>
    ''' 接收到数据后的通知事件
    ''' </summary>
    ''' <param name="skRecv">接收数据的clsSock对象</param>
    ''' <param name="bytBuf">接收到的数据</param>
    ''' <param name="intRecved">接收到的字节数</param>
    ''' <remarks></remarks>
    Event OnReceive(ByVal skRecv As clsSock, ByVal bytBuf() As Byte, ByVal intRecved As Integer)
    ''' <summary>
    ''' 连接上对方的通知事件
    ''' </summary>
    ''' <param name="skConnect">发出连接的clsSock对象</param>
    ''' <remarks></remarks>
    Event OnConnected(ByVal skConnect As clsSock)
    ''' <summary>
    ''' 对方关闭的通知事件
    ''' </summary>
    ''' <param name="skClose"></param>
    ''' <remarks></remarks>
    Event OnPeerClose(ByVal skClose As clsSock)
    ''' <summary>
    ''' 文件发送完毕的通知事件
    ''' </summary>
    ''' <param name="skSend"></param>
    ''' <param name="strFile"></param>
    ''' <remarks></remarks>
    Event OnFileSent(ByVal skSend As clsSock, ByVal strFile As String)
    ''' <summary>
    ''' Socket出错的通知事件
    ''' </summary>
    ''' <param name="skSock">出错的Socket,(Socket创建成功前出错)有可能为nothing</param>
    ''' <param name="nError"></param>
    ''' <remarks></remarks>
    Event OnError(ByVal skSock As clsSock, ByVal nError As Integer)


    ''' <summary>
    ''' clsSock类的默认构造函数
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()

    End Sub
    ''' <summary>
    ''' clsSock类的构造函数,打开socket
    ''' </summary>
    ''' <param name="enmProtocol">socket类型</param>
    ''' <param name="strAddress">本地IP地址或域名</param>
    ''' <param name="intPort">本地端口</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal enmProtocol As ProtocolType, ByVal strAddress As String, ByVal intPort As Integer)
        Try
            m_blnOpened = False

            Dim epLocal As New IPEndPoint(IPAddress.Parse(strAddress), intPort)

            If enmProtocol = Sockets.ProtocolType.Tcp Then
                m_skMine = New Socket(AddressFamily.InterNetwork, SocketType.Stream, enmProtocol)
                m_skMine.Bind(epLocal)
                m_skMine.NoDelay = True
                m_blnOpened = True
            ElseIf enmProtocol = Sockets.ProtocolType.Udp Then
                m_epRemote = New IPEndPoint(0, 0)

                m_skMine = New Socket(AddressFamily.InterNetwork, SocketType.Dgram, enmProtocol)
                m_skMine.Bind(epLocal)

                Array.Clear(m_bytBuf, 0, DefaultSockBufSize)
                m_skMine.BeginReceiveFrom(m_bytBuf, 0, DefaultSockBufSize, SocketFlags.None, m_epRemote, New AsyncCallback(AddressOf ReceiveCallback), Me)

                m_blnOpened = True
            Else
            End If

            m_skMine.ReceiveBufferSize = &H20000

            If m_blnOpened Then
                m_enmProtocol = enmProtocol
                m_strLocalHost = strAddress
                m_intLocalPort = intPort
            End If
        Catch ex As SocketException
            m_intError = ex.ErrorCode
            RaiseEvent OnError(Nothing, m_intError)
        End Try
    End Sub

    ''' <summary>
    ''' 拷贝构造函数
    ''' </summary>
    ''' <param name="objSrc">被拷贝的对象</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal objSrc As clsSock)
        m_skMine = objSrc.m_skMine
        m_blnOpened = objSrc.m_blnOpened
        m_strRemoteHost = objSrc.m_strRemoteHost
        m_intRemotePort = objSrc.m_intRemotePort
        m_enmProtocol = objSrc.m_enmProtocol
        m_strLocalHost = objSrc.m_strLocalHost
        m_intLocalPort = objSrc.m_intLocalPort
        m_bytBuf = objSrc.m_bytBuf
        m_epRemote = objSrc.m_epRemote
        m_ciSocks = objSrc.m_ciSocks
    End Sub

    ''' <summary>
    ''' clsSock类的析构函数,关闭socket
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub Finalize()
        If Not m_skMine Is Nothing Then
            If m_blnOpened Then
                m_skMine.Close()
            End If
        End If
    End Sub

    ''' <summary>
    ''' 打开socket,通常对New Socket()构造的实例在设置属性后调用
    ''' </summary>
    ''' <returns>True 成功 False 失败</returns>
    ''' <remarks></remarks>
    Public Function Open() As Boolean
        Open = False

        If m_blnOpened Then
            Exit Function
        End If

        Try
            m_blnOpened = False

            Dim epLocal As New IPEndPoint(IPAddress.Parse(m_strLocalHost), m_intLocalPort)

            If m_enmProtocol = Sockets.ProtocolType.Tcp Then
                m_skMine = New Socket(AddressFamily.InterNetwork, SocketType.Stream, m_enmProtocol)
                m_skMine.Bind(epLocal)
                m_skMine.NoDelay = True
                m_blnOpened = True
            ElseIf m_enmProtocol = Sockets.ProtocolType.Udp Then
                m_epRemote = New IPEndPoint(0, 0)

                m_skMine = New Socket(AddressFamily.InterNetwork, SocketType.Dgram, m_enmProtocol)
                m_skMine.Bind(epLocal)
                m_skMine.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.NoDelay, True)

                Array.Clear(m_bytBuf, 0, DefaultSockBufSize)
                m_skMine.BeginReceiveFrom(m_bytBuf, 0, DefaultSockBufSize, SocketFlags.None, m_epRemote, New AsyncCallback(AddressOf ReceiveCallback), Me)

                m_blnOpened = True
            Else
            End If

            Open = m_blnOpened
        Catch ex As SocketException
            m_intError = ex.ErrorCode
            RaiseEvent OnError(Nothing, m_intError)
        End Try
    End Function

    ''' <summary>
    ''' 关闭Socket
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Close()
        If Not m_skMine Is Nothing Then
            While m_ciSocks.Count > 0
                Dim ciInfo = m_ciSocks.Item(0)
                CloseConnection(ciInfo)
            End While

            If m_blnOpened Then
                m_blnOpened = False
                m_skMine.Close()
            End If
        End If
    End Sub

    ''' <summary>
    ''' 关闭一个Accept的连接
    ''' </summary>
    ''' <param name="ciInfo"></param>
    ''' <remarks></remarks>
    Private Sub CloseConnection(ByVal ciInfo As ConnectionInfo)
        If Not ciInfo.skAccept Is Nothing Then
            ciInfo.skAccept.Close()
        End If

        SyncLock GetType(ConnectionInfo)
            m_ciSocks.Remove(ciInfo)
        End SyncLock
    End Sub

    ''' <summary>
    ''' Socket异步接收数据的处理函数,针对TCP Accept后的socket
    ''' </summary>
    ''' <param name="result"></param>
    ''' <remarks></remarks>
    Private Sub AcceptRecvCallback(ByVal result As IAsyncResult)
        Dim nRecved As Integer = 0
        Dim ciInfo As ConnectionInfo = CType(result.AsyncState, ConnectionInfo)

        If ciInfo.skAccept Is Nothing Or _
            ciInfo.skAccept.m_blnOpened = False Then
            Exit Sub
        End If

        Try
            nRecved = ciInfo.skAccept.SocketObj.EndReceive(result)

            RaiseEvent OnReceive(ciInfo.skAccept, ciInfo.bytBuf, nRecved)

            Array.Clear(ciInfo.bytBuf, 0, DefaultSockBufSize)
            ciInfo.skAccept.SocketObj.BeginReceive(ciInfo.bytBuf, 0, DefaultSockBufSize, SocketFlags.None, New AsyncCallback(AddressOf AcceptRecvCallback), ciInfo)
        Catch ex As SocketException
            m_intError = ex.ErrorCode

            If ex.ErrorCode = 10054 Then
                If nRecved > 0 Then
                    RaiseEvent OnReceive(ciInfo.skAccept, m_bytBuf, nRecved)
                End If

                RaiseEvent OnPeerClose(ciInfo.skAccept)
                CloseConnection(ciInfo)
            Else
                RaiseEvent OnError(ciInfo.skAccept, m_intError)
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Socket异步接受连接的处理函数
    ''' </summary>
    ''' <param name="result"></param>
    ''' <remarks></remarks>
    Private Sub AcceptCallback(ByVal result As IAsyncResult)
        Dim ciInfo As New ConnectionInfo
        Dim skListener As clsSock = CType(result.AsyncState, clsSock)

        If Not skListener.m_blnOpened Then
            Exit Sub
        End If

        Try
            Dim skAccept As Socket = skListener.SocketObj.EndAccept(result)

            ciInfo.skAccept = New clsSock()
            ciInfo.skAccept.SocketObj = skAccept
            ReDim ciInfo.bytBuf(DefaultSockBufSize)
            SyncLock GetType(ConnectionInfo)
                m_ciSocks.Add(ciInfo)
            End SyncLock

            ciInfo.skAccept.m_strLocalHost = CType(skAccept.LocalEndPoint, IPEndPoint).Address.ToString()
            ciInfo.skAccept.m_intLocalPort = CType(skAccept.LocalEndPoint, IPEndPoint).Port
            ciInfo.skAccept.m_strRemoteHost = CType(skAccept.RemoteEndPoint, IPEndPoint).Address.ToString()
            ciInfo.skAccept.m_intRemotePort = CType(skAccept.RemoteEndPoint, IPEndPoint).Port
            ciInfo.skAccept.m_enmProtocol = m_enmProtocol
            ciInfo.skAccept.m_blnOpened = True

            RaiseEvent OnAccept(ciInfo.skAccept)

            skListener.SocketObj.BeginAccept(New AsyncCallback(AddressOf AcceptCallback), skListener)

            Array.Clear(ciInfo.bytBuf, 0, DefaultSockBufSize)
            ciInfo.skAccept.SocketObj.BeginReceive(ciInfo.bytBuf, 0, DefaultSockBufSize, SocketFlags.None, New AsyncCallback(AddressOf AcceptRecvCallback), ciInfo)
        Catch ex As SocketException
            m_intError = ex.ErrorCode

            RaiseEvent OnError(ciInfo.skAccept, m_intError)
            CloseConnection(ciInfo)
        End Try
    End Sub

    ''' <summary>
    ''' Socket异步进行连接的处理函数
    ''' </summary>
    ''' <param name="result"></param>
    ''' <remarks></remarks>
    Private Sub ConnectCallback(ByVal result As IAsyncResult)
        Dim skConnect As clsSock = CType(result.AsyncState, clsSock)

        Try
            skConnect.SocketObj.EndConnect(result)

            skConnect.m_strRemoteHost = CType(skConnect.SocketObj.RemoteEndPoint, IPEndPoint).Address.ToString()
            skConnect.m_intRemotePort = CType(skConnect.SocketObj.RemoteEndPoint, IPEndPoint).Port

            RaiseEvent OnConnected(skConnect)

            Array.Clear(m_bytBuf, 0, DefaultSockBufSize)
            skConnect.SocketObj.BeginReceive(m_bytBuf, 0, DefaultSockBufSize, SocketFlags.None, New AsyncCallback(AddressOf ReceiveCallback), skConnect)
        Catch ex As SocketException
            m_intError = ex.ErrorCode

            RaiseEvent OnError(skConnect, m_intError)
        End Try
    End Sub

    ''' <summary>
    ''' Socket异步接受数据的处理函数
    ''' </summary>
    ''' <param name="result"></param>
    ''' <remarks></remarks>
    Private Sub ReceiveCallback(ByVal result As IAsyncResult)
        Dim nRecved As Integer = 0
        Dim skRecv As clsSock = CType(result.AsyncState, clsSock)

        If skRecv Is Nothing Or _
            skRecv.m_blnOpened = False Then
            Exit Sub
        End If

        Try
            If skRecv.Protocol = Sockets.ProtocolType.Tcp Then
                nRecved = skRecv.SocketObj.EndReceive(result)

                RaiseEvent OnReceive(skRecv, m_bytBuf, nRecved)

                Array.Clear(m_bytBuf, 0, DefaultSockBufSize)
                skRecv.SocketObj.BeginReceive(m_bytBuf, 0, DefaultSockBufSize, SocketFlags.None, New AsyncCallback(AddressOf ReceiveCallback), skRecv)
            ElseIf skRecv.Protocol = Sockets.ProtocolType.Udp Then
                nRecved = skRecv.SocketObj.EndReceiveFrom(result, m_epRemote)

                m_strRemoteHost = m_epRemote.Address.ToString()
                m_intRemotePort = m_epRemote.Port

                RaiseEvent OnReceive(skRecv, m_bytBuf, nRecved)

                Array.Clear(m_bytBuf, 0, DefaultSockBufSize)
                skRecv.SocketObj.BeginReceiveFrom(m_bytBuf, 0, DefaultSockBufSize, SocketFlags.None, m_epRemote, New AsyncCallback(AddressOf ReceiveCallback), skRecv)
            End If
        Catch ex As SocketException
            m_intError = ex.ErrorCode

            If ex.ErrorCode = 10054 Then
                If nRecved > 0 Then
                    RaiseEvent OnReceive(skRecv, m_bytBuf, nRecved)
                End If

                RaiseEvent OnPeerClose(skRecv)
            Else
                RaiseEvent OnError(skRecv, m_intError)
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Socket异步发送数据的处理函数
    ''' </summary>
    ''' <param name="result"></param>
    ''' <remarks></remarks>
    Private Sub SendCallback(ByVal result As IAsyncResult)
        Dim skSend As clsSock = CType(result.AsyncState, clsSock)

        Try
            Dim nSent As Integer

            If skSend.Protocol = Sockets.ProtocolType.Udp Then
                nSent = skSend.SocketObj.EndSendTo(result)
            ElseIf skSend.Protocol = Sockets.ProtocolType.Tcp Then
                nSent = skSend.SocketObj.EndSend(result)
            End If

            RaiseEvent OnSent(skSend, nSent)
        Catch ex As SocketException
            m_intError = ex.ErrorCode

            RaiseEvent OnError(skSend, m_intError)
        End Try
    End Sub

    Private Sub SendFileCallback(ByVal result As IAsyncResult)
        Dim skSend As clsSock = CType(result.AsyncState, clsSock)

        skSend.SocketObj.EndSendFile(result)

        RaiseEvent OnFileSent(skSend, "sent!!!")
    End Sub

    ''' <summary>
    ''' 开始TCP侦听
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Listen() As Boolean
        Listen = False

        If Not m_blnOpened Then
            Exit Function
        End If

        If m_enmProtocol <> Sockets.ProtocolType.Tcp Then
            Exit Function
        End If

        If m_skMine.Connected Then
            Exit Function
        End If

        Listen = False
        Try
            m_skMine.Listen(100)
            m_skMine.BeginAccept(New AsyncCallback(AddressOf AcceptCallback), Me)

            Listen = True
        Catch ex As SocketException
            m_intError = ex.ErrorCode

            RaiseEvent OnError(Me, m_intError)
        End Try
    End Function

    ''' <summary>
    ''' 开始连接
    ''' </summary>
    ''' <param name="strRemoteHost"></param>
    ''' <param name="intRemotePort"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Connect(ByVal strRemoteHost As String, ByVal intRemotePort As Integer) As Boolean
        Connect = False

        If Not m_blnOpened Then
            Exit Function
        End If

        If m_enmProtocol <> Sockets.ProtocolType.Tcp Then
            Exit Function
        End If

        Try
            Dim epRemote As New IPEndPoint(IPAddress.Parse(strRemoteHost), intRemotePort)
            m_skMine.BeginConnect(epRemote, New AsyncCallback(AddressOf ConnectCallback), Me)
        Catch ex As SocketException
            m_intError = ex.ErrorCode
            RaiseEvent OnError(Me, m_intError)
        End Try
    End Function

    ''' <summary>
    ''' 发送数据
    ''' </summary>
    ''' <param name="bytBuf">字节数组类型的 要发送的数据</param>
    ''' <returns></returns>
    ''' <remarks>TCP UDP都可使用</remarks>
    Public Overloads Function SendData(ByVal bytBuf() As Byte) As Boolean
        Dim iar As IAsyncResult

        SendData = False

        If bytBuf Is Nothing Or bytBuf.GetLength(0) = 0 Then
            Exit Function
        End If

        Try
            If m_enmProtocol = Sockets.ProtocolType.Udp Then
                Dim epRemote As New IPEndPoint(IPAddress.Parse(m_strRemoteHost), m_intRemotePort)

                iar = m_skMine.BeginSendTo(bytBuf, 0, bytBuf.GetLength(0), SocketFlags.None, epRemote, New AsyncCallback(AddressOf SendCallback), Me)
                'While iar.IsCompleted = False
                'Thread.Sleep(10)
                'End While

            ElseIf m_enmProtocol = Sockets.ProtocolType.Tcp Then
                If Not m_skMine.Connected Then
                    Exit Function
                End If

                'send(m_skMine.Handle, bytBuf, bytBuf.GetLength(0), 0)
                iar = m_skMine.BeginSend(bytBuf, 0, bytBuf.GetLength(0), SocketFlags.None, New AsyncCallback(AddressOf SendCallback), Me)
                'While iar.IsCompleted = False
                'Thread.Sleep(10)
                'End While
            End If

        Catch ex As SocketException
            m_intError = ex.ErrorCode

            RaiseEvent OnError(Me, m_intError)
        End Try
    End Function

    ''' <summary>
    ''' 发送数据
    ''' </summary>
    ''' <param name="bytBuf">字节数组类型的 要发送的数据</param>
    ''' <param name="intLen">要发送的数据的长度</param>
    ''' <returns></returns>
    ''' <remarks>TCP UDP都可使用</remarks>
    Public Overloads Function SendData(ByVal bytBuf() As Byte, ByVal intLen As Integer) As Boolean
        Dim iar As IAsyncResult

        SendData = False

        If bytBuf Is Nothing Or _
            bytBuf.GetLength(0) = 0 Or _
            intLen > bytBuf.GetLength(0) Then
            Exit Function
        End If

        Try
            If m_enmProtocol = Sockets.ProtocolType.Udp Then
                Dim epRemote As New IPEndPoint(IPAddress.Parse(m_strRemoteHost), m_intRemotePort)

                iar = m_skMine.BeginSendTo(bytBuf, 0, intLen, SocketFlags.None, epRemote, New AsyncCallback(AddressOf SendCallback), Me)
                'While iar.IsCompleted = False
                'Thread.Sleep(10)
                'End While

                SendData = True
            ElseIf m_enmProtocol = Sockets.ProtocolType.Tcp Then
                If Not m_skMine.Connected Then
                    Exit Function
                End If

                iar = m_skMine.BeginSend(bytBuf, 0, intLen, SocketFlags.None, New AsyncCallback(AddressOf SendCallback), Me)
                'While iar.IsCompleted = False
                'Thread.Sleep(10)
                'End While

                SendData = True
            End If
        Catch ex As SocketException
            m_intError = ex.ErrorCode

            RaiseEvent OnError(Me, m_intError)
        End Try
    End Function


    ''' <summary>
    ''' 发送数据
    ''' </summary>
    ''' <param name="bytBuf">字节数组类型的 要发送的数据</param>
    ''' <returns></returns>
    ''' <remarks>仅UDP使用</remarks>
    Public Overloads Function SendData(ByVal strRemoteHost As String, ByVal intRemotePort As Integer, ByVal bytBuf() As Byte) As Boolean
        Dim iar As IAsyncResult

        SendData = False

        'If m_skMine Is Nothing Then
        'Exit Function
        'End If

        If bytBuf Is Nothing Or bytBuf.GetLength(0) = 0 Then
            Exit Function
        End If

        Try
            If m_enmProtocol = Sockets.ProtocolType.Udp Then
                Dim epRemote As New IPEndPoint(IPAddress.Parse(strRemoteHost), intRemotePort)

                iar = m_skMine.BeginSendTo(bytBuf, 0, bytBuf.GetLength(0), SocketFlags.None, epRemote, New AsyncCallback(AddressOf SendCallback), Me)
                'While iar.IsCompleted = False
                'Thread.Sleep(10)
                'End While

                SendData = True
            ElseIf m_enmProtocol = Sockets.ProtocolType.Tcp Then
                If Not m_skMine.Connected Then
                    Exit Function
                End If

                iar = m_skMine.BeginSend(bytBuf, 0, bytBuf.GetLength(0), SocketFlags.None, New AsyncCallback(AddressOf SendCallback), Me)
                'While iar.IsCompleted = False
                'Thread.Sleep(10)
                'End While

                SendData = True
            End If
        Catch ex As SocketException
            m_intError = ex.ErrorCode

            RaiseEvent OnError(Me, m_intError)
        End Try
    End Function

    ''' <summary>
    ''' 发送数据
    ''' </summary>
    ''' <param name="bytBuf">字节数组类型的 要发送的数据</param>
    ''' <param name="intLen">要发送的数据的长度</param>
    ''' <returns></returns>
    ''' <remarks>仅UDP使用</remarks>
    Public Overloads Function SendData(ByVal strRemoteHost As String, ByVal intRemotePort As Integer, ByVal bytBuf() As Byte, ByVal intLen As Integer) As Boolean
        Dim iar As IAsyncResult

        SendData = False

        If bytBuf Is Nothing Or _
            bytBuf.GetLength(0) = 0 Or _
            intLen > bytBuf.GetLength(0) Then
            Exit Function
        End If

        Try
            If m_enmProtocol = Sockets.ProtocolType.Udp Then
                Dim epRemote As New IPEndPoint(IPAddress.Parse(strRemoteHost), intRemotePort)

                iar = m_skMine.BeginSendTo(bytBuf, 0, intLen, SocketFlags.None, epRemote, New AsyncCallback(AddressOf SendCallback), Me)
                'While iar.IsCompleted = False
                'Thread.Sleep(10)
                'End While

                SendData = True
            ElseIf m_enmProtocol = Sockets.ProtocolType.Tcp Then
                If Not m_skMine.Connected Then
                    Exit Function
                End If

                iar = m_skMine.BeginSend(bytBuf, 0, intLen, SocketFlags.None, New AsyncCallback(AddressOf SendCallback), Me)
                'While iar.IsCompleted = False
                'Thread.Sleep(10)
                'End While

                SendData = True
            End If
        Catch ex As SocketException
            m_intError = ex.ErrorCode

            RaiseEvent OnError(Me, m_intError)
        End Try
    End Function

    ''' <summary>
    ''' 发送数据
    ''' </summary>
    ''' <param name="strMsg">字符串类型的 要发送的数据</param>
    ''' <returns></returns>
    ''' <remarks>TCP UDP都可使用</remarks>
    Public Overloads Function SendData(ByVal strMsg As String) As Boolean
        Dim iar As IAsyncResult
        Dim arMsg() As Byte = Encoding.Default.GetBytes(strMsg)

        SendData = False

        If strMsg Is Nothing Or strMsg.Length = 0 Then
            Exit Function
        End If

        Try
            If m_enmProtocol = Sockets.ProtocolType.Udp Then
                Dim epRemote As New IPEndPoint(IPAddress.Parse(m_strRemoteHost), m_intRemotePort)

                iar = m_skMine.BeginSendTo(arMsg, 0, arMsg.GetLength(0), SocketFlags.None, epRemote, New AsyncCallback(AddressOf SendCallback), Me)
                'While iar.IsCompleted = False
                'Thread.Sleep(10)
                'End While

                SendData = True
            ElseIf m_enmProtocol = Sockets.ProtocolType.Tcp Then
                If Not m_skMine.Connected Then
                    Exit Function
                End If

                iar = m_skMine.BeginSend(arMsg, 0, arMsg.GetLength(0), SocketFlags.None, New AsyncCallback(AddressOf SendCallback), Me)
                'While iar.IsCompleted = False
                'Thread.Sleep(10)
                'End While

                SendData = True
            End If
        Catch ex As SocketException
            m_intError = ex.ErrorCode

            RaiseEvent OnError(Me, m_intError)
        End Try
    End Function

    ''' <summary>
    ''' 发送数据
    ''' </summary>
    ''' <param name="strMsg">字符串类型的 要发送的数据</param>
    ''' <returns></returns>
    ''' <remarks>仅UDP可使用</remarks>
    Public Overloads Function SendData(ByVal strRemoteHost As String, ByVal intRemotePort As Integer, ByVal strMsg As String) As Boolean
        Dim iar As IAsyncResult
        Dim arMsg() As Byte = Encoding.Default.GetBytes(strMsg)

        SendData = False

        If strMsg Is Nothing Or strMsg.Length = 0 Then
            Exit Function
        End If

        Try
            If m_enmProtocol = Sockets.ProtocolType.Udp Then
                Dim epRemote As New IPEndPoint(IPAddress.Parse(strRemoteHost), intRemotePort)

                iar = m_skMine.BeginSendTo(arMsg, 0, arMsg.GetLength(0), SocketFlags.None, epRemote, New AsyncCallback(AddressOf SendCallback), Me)
                'While iar.IsCompleted = False
                'Thread.Sleep(10)
                'End While

                SendData = True
            ElseIf m_enmProtocol = Sockets.ProtocolType.Tcp Then
                If Not m_skMine.Connected Then
                    Exit Function
                End If

                iar = m_skMine.BeginSend(arMsg, 0, arMsg.GetLength(0), SocketFlags.None, New AsyncCallback(AddressOf SendCallback), Me)
                'While iar.IsCompleted = False
                'Thread.Sleep(10)
                'End While

                SendData = True
            End If
        Catch ex As SocketException
            m_intError = ex.ErrorCode

            RaiseEvent OnError(Me, m_intError)
        End Try
    End Function

    Public Function SendFile(ByVal strPath As String) As Boolean
        SendFile = False
        Try
            If m_enmProtocol = Sockets.ProtocolType.Tcp Then
                If Not m_skMine.Connected Then
                    Exit Function
                End If

                m_skMine.BeginSendFile(strPath, New AsyncCallback(AddressOf SendFileCallback), Me)
                SendFile = True
            End If
        Catch ex As SocketException
            m_intError = ex.ErrorCode

            RaiseEvent OnError(Me, m_intError)
        End Try
    End Function

    ''' <summary>
    ''' 对从Socket收到的消息进行预处理,主要是处理大包或粘包情况
    ''' </summary>
    ''' <param name="bytBuf">这次又收到的数据</param>
    ''' <param name="intRecved">这次又收到的数据长度</param>
    ''' <returns>返回分离好的单个消息的数组表,每个消息是字节数组的形式</returns>
    ''' <remarks></remarks>
    Public Function PreprocessMsg(ByVal bytBuf() As Byte, ByVal intRecved As Integer) As ArrayList
        Dim arMsgs As New ArrayList
        Dim strtHeader As MsgHeader
        Dim arOneMsg() As Byte
        Dim intOneMsgLen As Integer

        PreprocessMsg = arMsgs

        Dim intPos As Integer = 0
        Dim intSizeOfHeader = Marshal.SizeOf(GetType(MsgHeader))
        Dim arTmpHeader(intSizeOfHeader - 1) As Byte

        'Debug.Print("preprocessmsg bytes count:" & intRecved & " incompletelen:" & intIncompleteLen)
        While intPos < intRecved
            If m_intIncompleteLen > 0 Then  '有未处理完的数据,一般应当intPos=0
                If intPos = 0 Then
                    If m_intIncompleteLen < intSizeOfHeader Then  '未处理完数据长度不够一个消息头的长度!!!
                        '先读够一个消息头的数据,计算包长度
                        Array.Copy(m_arIncompleteBuf, arTmpHeader, m_intIncompleteLen)
                        Array.Copy(bytBuf, 0, arTmpHeader, m_intIncompleteLen, intSizeOfHeader - m_intIncompleteLen)

                        strtHeader = clsMsgComposer.RawDeserialize(arTmpHeader, GetType(MsgHeader))
                        intOneMsgLen = intSizeOfHeader + strtHeader.DataLength

                        If ((intSizeOfHeader - m_intIncompleteLen) + strtHeader.DataLength) > intRecved Then '仍凑不够一个完整包
                            '拷贝到公用未处理缓冲区,更新指针
                            Array.Copy(bytBuf, 0, m_arIncompleteBuf, m_intIncompleteLen, intRecved - intPos)
                            m_intIncompleteLen += (intRecved - intPos)

                            Exit While '退出
                        Else
                            ReDim arOneMsg(intOneMsgLen - 1)

                            Array.Copy(arTmpHeader, 0, arOneMsg, 0, intSizeOfHeader)
                            Array.Copy(bytBuf, intSizeOfHeader - m_intIncompleteLen, arOneMsg, intSizeOfHeader, strtHeader.DataLength)

                            If clsMsgComposer.ValidateMsg(strtHeader, intOneMsgLen) Then
                                arMsgs.Add(arOneMsg)
                            End If

                            intPos += (intSizeOfHeader - m_intIncompleteLen) + strtHeader.DataLength '增加intPos,处理下一个
                            m_intIncompleteLen = 0
                        End If
                    Else
                        '取出消息头,计算消息长度
                        strtHeader = clsMsgComposer.RawDeserialize(m_arIncompleteBuf, GetType(MsgHeader))
                        intOneMsgLen = intSizeOfHeader + strtHeader.DataLength

                        If (intOneMsgLen - m_intIncompleteLen) > intRecved Then '仍凑不够一个完整包
                            Array.Copy(bytBuf, 0, m_arIncompleteBuf, m_intIncompleteLen, intRecved - intPos)
                            m_intIncompleteLen += (intRecved - intPos)

                            Exit While '退出
                        Else
                            ReDim arOneMsg(intOneMsgLen - 1)

                            Array.Copy(m_arIncompleteBuf, 0, arOneMsg, 0, m_intIncompleteLen)
                            Array.Copy(bytBuf, 0, arOneMsg, m_intIncompleteLen, intOneMsgLen - m_intIncompleteLen)

                            If clsMsgComposer.ValidateMsg(strtHeader, intOneMsgLen) Then
                                arMsgs.Add(arOneMsg)
                            End If

                            intPos += (intOneMsgLen - m_intIncompleteLen)   '增加intPos,处理下一个
                            m_intIncompleteLen = 0
                        End If
                    End If
                Else    '如果intPos>0,可视为错误,退出
                    Exit While
                End If
            Else
                If (intRecved - intPos) >= intSizeOfHeader Then  '所剩数据至少大于一个包头大小
                    '取出消息头,计算消息长度
                    Array.Copy(bytBuf, intPos, arTmpHeader, 0, intSizeOfHeader)
                    strtHeader = clsMsgComposer.RawDeserialize(arTmpHeader, GetType(MsgHeader))
                    intOneMsgLen = intSizeOfHeader + strtHeader.DataLength

                    If (intPos + intOneMsgLen) > intRecved Then '是一个不完整包
                        Array.Copy(bytBuf, intPos, m_arIncompleteBuf, 0, intRecved - intPos)
                        m_intIncompleteLen = intRecved - intPos

                        intPos += intRecved - intPos    '增加intPos, 一般再次循环就退出
                    Else    '是一个完整包
                        ReDim arOneMsg(intOneMsgLen - 1)

                        Array.Copy(bytBuf, intPos, arOneMsg, 0, intOneMsgLen)

                        If clsMsgComposer.ValidateMsg(strtHeader, intOneMsgLen) Then
                            arMsgs.Add(arOneMsg)
                        End If

                        intPos += intOneMsgLen '增加intPos,处理下一个
                        m_intIncompleteLen = 0
                    End If
                Else    '所剩数据小于一个包头大小
                    Array.Copy(bytBuf, intPos, m_arIncompleteBuf, 0, intRecved - intPos)
                    m_intIncompleteLen = intRecved - intPos
                    intPos += (intRecved - intPos)
                End If
            End If
        End While
        'Debug.Print("preprocessmsgs recved to array:" & arMsgs.Count & " incompletelen:" & intIncompleteLen)
    End Function

    ''' <summary>
    ''' socket类型
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Protocol() As ProtocolType
        Get
            Return m_enmProtocol
        End Get
        Set(ByVal value As ProtocolType)
            If Not m_blnOpened Then
                m_enmProtocol = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' 本地主机地址
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property LocalHost() As String
        Get
            Return m_strLocalHost
        End Get
        Set(ByVal value As String)
            If Not m_blnOpened Then
                m_strLocalHost = value
            End If
        End Set
    End Property

    Property LocalPort() As Integer
        Get
            Return m_intLocalPort
        End Get
        Set(ByVal value As Integer)
            If Not m_blnOpened Then
                m_intLocalPort = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' 远端(对方)主机的地址
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property RemoteHost() As String
        Get
            Return m_strRemoteHost
        End Get

        Set(ByVal value As String)
            m_strRemoteHost = value
        End Set
    End Property

    ''' <summary>
    ''' 远端(对方)主机的端口
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property RemotePort() As Integer
        Get
            Return m_intRemotePort
        End Get
        Set(ByVal value As Integer)
            m_intRemotePort = value
        End Set
    End Property

    ''' <summary>
    ''' socket是否打开
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property IsOpened() As Boolean
        Get
            Return m_blnOpened
        End Get
    End Property

    ''' <summary>
    ''' socket是否处于连接状态
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property IsConnected() As Boolean
        Get
            Return m_skMine.Connected
        End Get
    End Property

    Property SocketObj() As Socket
        Get
            Return m_skMine
        End Get
        Set(ByVal value As Socket)
            If m_blnOpened = False Then
                m_skMine = value
            End If
        End Set
    End Property

    ReadOnly Property LastError() As Boolean
        Get
            Return m_intError
        End Get
    End Property
End Class
