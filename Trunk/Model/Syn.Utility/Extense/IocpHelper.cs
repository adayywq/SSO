using System;
using System.Threading;
using System.Runtime.InteropServices;

namespace Syn.Utility.Extense
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public unsafe struct OVERLAPPED
    {
        UInt32* ulpInternal;
        UInt32* ulpInternalHigh;
        Int32 lOffset;
        Int32 lOffsetHigh;
        UInt32 hEvent;
    }

    public sealed class IocpHelper
    {
        #region IOCP四个方法
        /// <summary> Win32Func: Create an IO Completion Port Thread Pool </summary>
        [DllImport("Kernel32", CharSet = CharSet.Auto)]
        private unsafe static extern UInt32 CreateIoCompletionPort(UInt32 FileHandle, UInt32 ExistingCompletionPort, UInt32* CompletionKey, UInt32 NumberOfConcurrentThreads);

        /// <summary> Win32Func: Closes an IO Completion Port Thread Pool </summary>
        [DllImport("Kernel32", CharSet = CharSet.Auto)]
        private unsafe static extern Boolean CloseIoCompletionPort(UInt32 hObject);

        /// <summary> Win32Func: Posts a context based event into an IO Completion Port Thread Pool </summary>
        [DllImport("Kernel32", CharSet = CharSet.Auto)]
        private unsafe static extern Boolean PostQueuedCompletionStatus(UInt32 hCompletionPort, UInt32 uiSizeOfArgument, UInt32* puiUserArg, OVERLAPPED* pOverlapped);

        /// <summary> Win32Func: Waits on a context based event from an IO Completion Port Thread Pool.
        [DllImport("Kernel32", CharSet = CharSet.Auto)]
        private unsafe static extern Boolean GetQueuedCompletionStatus(UInt32 hCompletionPort, UInt32* pSizeOfArgument, UInt32* puiUserArg, OVERLAPPED** ppOverlapped, UInt32 uiMilliseconds);

        #endregion

        #region IOCP四个常量
        /// <summary> SimTypeConst: This represents the Win32 Invalid Handle Value Macro </summary>
        private const UInt32 INVALID_HANDLE_VALUE = 0xffffffff;

        /// <summary> SimTypeConst: This represents the Win32 INFINITE Macro </summary>
        private const UInt32 INIFINITE = 0xffffffff;

        /// <summary> SimTypeConst: This tells the IOCP Function to shutdown </summary>
        private const Int32 SHUTDOWN_IOCPTHREAD = 0x7fffffff;

        /// <summary> DelType: This is the type of user function to be supplied for the thread pool </summary>
        public delegate bool USER_FUNCTION(Object iValue);
        #endregion

        #region IOCP属性方法
        private UInt32 m_cpHandle;//IOCP线程处理实例
        private Int32  m_uiMaxConcurrency;//允许同时运行的最大线程数
        private Int32  m_iMinThreadsInPool;//维持的最小线程池数量
        private Int32  m_iMaxThreadsInPool;//维持的最大线程池数量
        private Object m_pCriticalSection;

        private Int32 m_iCurThreadsInPool;
        private Int32 m_iActThreadsInPool;
        private Int32 m_iCurWorkInPool;

        /// <summary> 
        /// 说明：IOCP线程处理实例
        /// </summary>
        private UInt32 GetCompletionPort { get { return m_cpHandle; } set { m_cpHandle = value; } }

        /// <summary> 
        /// 说明：允许同时运行的最大线程数
        /// </summary>
        private Int32 GetMaxConcurrency { get { return m_uiMaxConcurrency; } set { m_uiMaxConcurrency = value; } }

        /// <summary> 
        /// 说明：维持的最小线程池数量
        /// </summary>
        private Int32 GetMinThreadsInPool { get { return m_iMinThreadsInPool; } set { m_iMinThreadsInPool = value; } }

        /// <summary> 
        /// SimType: The maximum number of threads the thread pool maintains 
        /// 说明：维持的最大线程池数量
        /// </summary>
        private Int32 GetMaxThreadsInPool { get { return m_iMaxThreadsInPool; } set { m_iMaxThreadsInPool = value; } }

        /// <summary> RefType: A serialization object to protect the class state </summary>
        private Object GetCriticalSection { get { return m_pCriticalSection; } set { m_pCriticalSection = value; } }

        /// <summary> DelType: A reference to a user specified function to be call by the thread pool </summary>
        private USER_FUNCTION m_pfnUserFunction;
        private USER_FUNCTION GetUserFunction { get { return m_pfnUserFunction; } set { m_pfnUserFunction = value; } }

        /// <summary> SimType: Flag to indicate if the class is disposing </summary>
        private Boolean m_bDisposeFlag;
        private Boolean IsDisposed { get { return m_bDisposeFlag; } set { m_bDisposeFlag = value; } }

        // Public Properties
        /// <summary> SimType: The current number of threads in the thread pool </summary>
        public Int32 GetCurThreadsInPool { get { return m_iCurThreadsInPool; } set { m_iCurThreadsInPool = value; } }
        /// <summary> SimType: Increment current number of threads in the thread pool </summary>
        private Int32 IncCurThreadsInPool() { return Interlocked.Increment(ref m_iCurThreadsInPool); }
        /// <summary> SimType: Decrement current number of threads in the thread pool </summary>
        private Int32 DecCurThreadsInPool() { return Interlocked.Decrement(ref m_iCurThreadsInPool); }
        
        /// <summary> SimType: The current number of active threads in the thread pool </summary>
        public Int32 GetActThreadsInPool { get { return m_iActThreadsInPool; } set { m_iActThreadsInPool = value; } }
        /// <summary> SimType: Increment current number of active threads in the thread pool </summary>
        private Int32 IncActThreadsInPool() { return Interlocked.Increment(ref m_iActThreadsInPool); }
        /// <summary> SimType: Decrement current number of active threads in the thread pool </summary>
        private Int32 DecActThreadsInPool() { return Interlocked.Decrement(ref m_iActThreadsInPool); }
        
        /// <summary> SimType: The current number of Work posted in the thread pool </summary>
        public Int32 GetCurWorkInPool { get { return m_iCurWorkInPool; } set { m_iCurWorkInPool = value; } }
        /// <summary> SimType: Increment current number of Work posted in the thread pool </summary>
        private Int32 IncCurWorkInPool() { return Interlocked.Increment(ref m_iCurWorkInPool); }
        /// <summary> SimType: Decrement current number of Work posted in the thread pool </summary>
        private Int32 DecCurWorkInPool() { return Interlocked.Decrement(ref m_iCurWorkInPool); }

        #endregion

        #region 构造函数
        /// <summary> 构造函数 </summary>
        /// <param name = "iMaxConcurrency">最大允许运行线程数</param>
        /// <param name = "iMinThreadsInPool">线程池最小线程数</param>
        /// <param name = "iMaxThreadsInPool">线程池最大线程数 </param>
        /// <param name = "pfnUserFunction">用户方法</param>
        /// <exception cref = "Exception">异常</exception>
        public IocpHelper(Int32 iMaxConcurrency, Int32 iMinThreadsInPool, Int32 iMaxThreadsInPool, USER_FUNCTION pfnUserFunction)
        {
            try
            {
                // 设置线程属性
                GetMaxConcurrency = iMaxConcurrency;
                GetMinThreadsInPool = iMinThreadsInPool;
                GetMaxThreadsInPool = iMaxThreadsInPool;
                GetUserFunction = pfnUserFunction;

                // 初始化线程计数器
                GetCurThreadsInPool = 0;
                GetActThreadsInPool = 0;
                GetCurWorkInPool = 0;
                GetCriticalSection = new Object();
                IsDisposed = false;

                // 创建完成端口
                unsafe
                {
                    GetCompletionPort = CreateIoCompletionPort(INVALID_HANDLE_VALUE, 0, null, (UInt32)GetMaxConcurrency);
                }
                // 判断是否创建成功
                if (GetCompletionPort == 0)
                    throw new Exception("Unable To Create IO Completion Port");

                //分配并且启动实际线程的最小数量
                //Int32 iStartingCount = GetCurThreadsInPool;
                //ThreadStart tsThread = new ThreadStart(IOCPFunction);
                //for (Int32 iThread = 0; iThread < GetMinThreadsInPool; ++iThread)
                //{
                //    Thread thThread = new Thread(tsThread);
                //    thThread.Name = "IOCP " + thThread.GetHashCode();
                //    thThread.Start();// 创建-启动线程
                //    IncCurThreadsInPool();//放入线程池
                //}
            }
            catch
            {
                throw new Exception("Unhandled Exception");
            }
        }
        #endregion

        #region 析构函数
        /// <summary> 
        /// 析构函数 
        /// </summary>
        ~IocpHelper()
        {
            if (!IsDisposed) Dispose();
        }
        #endregion

        #region 关闭完成端口
        /// <summary>
        /// 关闭完成端口
        /// </summary>
        public void Dispose()
        {
            try
            {
                IsDisposed = true;//是否释放对象

                // 获取线程池现有线程数量
                Int32 iCurThreadsInPool = GetCurThreadsInPool;

                // 关闭所有线程
                for (Int32 iThread = 0; iThread < iCurThreadsInPool; ++iThread)
                {
                    unsafe
                    {
                        bool bret = PostQueuedCompletionStatus(GetCompletionPort, 4, (UInt32*)SHUTDOWN_IOCPTHREAD, null);
                    }
                }
                // 等待获取所有线程
                while (GetCurThreadsInPool != 0) Thread.Sleep(100);
                unsafe
                {
                    // 关闭完成端口
                    CloseIoCompletionPort(GetCompletionPort);
                }
            }
            catch
            {
            }
        }
        #endregion

        #region IOCP执行方法
        /// <summary> IOCP执行方法</summary>
        private void IOCPFunction()
        {
            UInt32 uiNumberOfBytes;
            Int32 iValue;
            try
            {
                while (true)
                {
                    unsafe
                    {
                        OVERLAPPED* pOv;
                        // 等待客户端事件
                        GetQueuedCompletionStatus(GetCompletionPort, &uiNumberOfBytes, (UInt32*)&iValue, &pOv, INIFINITE);
                    }

                    // 线程休眠
                    DecCurWorkInPool();
                    // 线程关闭
                    if (iValue == SHUTDOWN_IOCPTHREAD)
                        break;
                    // 激活线程
                    IncActThreadsInPool();
                    try
                    {
                        // 调用用户方法
                        GetUserFunction(iValue);
                    }
                    catch
                    {
                    }
                    // 设置排他锁
                    Monitor.Enter(GetCriticalSection);
                    try
                    {
                        if (GetCurThreadsInPool < GetMaxThreadsInPool)
                        {
                            if (GetActThreadsInPool == GetCurThreadsInPool)
                            {
                                if (IsDisposed == false)
                                {
                                    ThreadStart tsThread = new ThreadStart(IOCPFunction);
                                    Thread thThread = new Thread(tsThread);
                                    thThread.Name = "IOCP " + thThread.GetHashCode();
                                    thThread.Start();
                                    IncCurThreadsInPool();
                                }
                            }
                        }
                    }
                    catch
                    {
                    }
                    // 释放排他锁
                    Monitor.Exit(GetCriticalSection);
                    DecActThreadsInPool();
                }
            }
            catch
            {
            }

            DecCurThreadsInPool();
        }

        #endregion

        #region IOCP响应用户方法
        /// <summary> IOCP响应用户方法</summary>
        /// <param name="iValue"></param>
        /// <exception cref = "Exception"></exception>
        public void PostEvent(Int32 iValue)
        {
            try
            {
                if (IsDisposed == false)
                {
                    unsafe
                    {
                        // 提交一个事件到完成端口线程池
                        PostQueuedCompletionStatus(GetCompletionPort, 4, (UInt32*)iValue, null);
                    }
                    IncCurWorkInPool();
                    Monitor.Enter(GetCriticalSection);
                    try
                    {
                        if (GetCurThreadsInPool < GetMaxThreadsInPool)
                        {
                            if (GetActThreadsInPool == GetCurThreadsInPool)
                            {
                                if (IsDisposed == false)
                                {
                                    ThreadStart tsThread = new ThreadStart(IOCPFunction);
                                    Thread thThread = new Thread(tsThread);
                                    thThread.Name = "IOCP " + thThread.GetHashCode();
                                    thThread.Start();
                                    IncCurThreadsInPool();
                                }
                            }
                        }
                    }
                    catch
                    {
                    }
                    Monitor.Exit(GetCriticalSection);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            catch
            {
                throw new Exception("Unhandled Exception");
            }
        }
        /// <summary> IOCP响应用户方法 </summary>
        /// <exception cref = "Exception"></exception>
        public void PostEvent()
        {
            try
            {
                if (IsDisposed == false)
                {
                    unsafe
                    {
                        PostQueuedCompletionStatus(GetCompletionPort, 0, null, null);
                    }
                    IncCurWorkInPool();
                    Monitor.Enter(GetCriticalSection);
                    try
                    {
                        if (GetCurThreadsInPool < GetMaxThreadsInPool)
                        {
                            if (GetActThreadsInPool == GetCurThreadsInPool)
                            {
                                if (IsDisposed == false)
                                {
                                    ThreadStart tsThread = new ThreadStart(IOCPFunction);
                                    Thread thThread = new Thread(tsThread);
                                    thThread.Name = "IOCP " + thThread.GetHashCode();
                                    thThread.Start();
                                    IncCurThreadsInPool();
                                }
                            }
                        }
                    }
                    catch
                    {
                    }
                    Monitor.Exit(GetCriticalSection);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            catch
            {
                throw new Exception("Unhandled Exception");
            }
        }

        #endregion 
    }

}
