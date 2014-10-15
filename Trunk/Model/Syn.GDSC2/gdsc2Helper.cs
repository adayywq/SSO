using System;
using System.Configuration;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;
using System.Xml;

namespace Syn.GDSC2
{
    public class gdsc2Helper
    {
        public const string FINANCING_TYPE_CARDREADER = "555";
        public const string FINANCING_TYPE_DOORPOS = "556";

        public const string COMMERCE_ABSTRACT_CARDREADER = "注册会员费用";
        public const string COMMERCE_ABSTRACT_DOORPOS = "门禁通道消费";
        public const string COMMERCE_ABSTRACT_HAND = "手工收费";

        public const short TIME_OUT = 10;

        public static string ProxyServerIP = "10.128.0.191";
        public static short ProxyServerPort = 8500;
        public static ushort SysCode = 147;
        public static ushort TerminalNo = 1;

        public static short TimeOut = TIME_OUT;

        public static short FileDownloadTimeOut = 1200;

        public static string Operator = "SZ";

        public static bool CheckPIDTag = true;

        public static string Department = "新中新电子";

        public static bool ProxyOffline;

        private static uint MaxJnl;

        /// <summary>
        /// 获取流水号
        /// </summary>
        /// <returns>ulong流水号</returns>
        public static uint GetJnl()
        {
            bool dd = gdsc2Helper.InitGDSC2();
            if (dd)
            {
                MaxJnl++;
                return (uint)(MaxJnl + 1);
            }
            else
                return 1;
        }

        private static bool InitCardReaderOK = false;

        /// <summary>
        /// 初始化读卡器
        /// </summary>
        /// <returns></returns>
        public static bool InitCardReader()
        {
            try
            {
                if (InitCardReaderOK)
                    return true;
                else
                {
                    bool bret = gdsc2Helper.InitGDSC2();
                    if (bret)
                    {
                        int RET2 = Syn.GDSC2.AIOAPI.TA_CRInit(0, 0, 0); //尝试初始化USB读卡器
                        if (RET2 == 0)
                        {
                            InitCardReaderOK = true;
                            return true;
                        }
                        else
                        {
                            int RET3 = Syn.GDSC2.AIOAPI.TA_CRInit(1, 0, 19200);	//尝试初始化串口读卡器
                            if (RET3 == 0)
                            {
                                InitCardReaderOK = true;
                                return true;
                            }
                            else
                            {
                                int RET4 = Syn.GDSC2.AIOAPI.TA_CRInit(1, 1, 19200);	//尝试初始化串口读卡器
                                if (RET4 == 0)
                                {
                                    InitCardReaderOK = true;
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        private static bool InitGDSC2OK = false;

        /// <summary>
        /// 初始化第三方接口
        /// </summary>
        /// <returns></returns>
        public static bool InitGDSC2()
        {
            if (InitGDSC2OK)
                return true;
            else
            {
                bool ret = Syn.GDSC2.AIOAPI.TA_Init(ProxyServerIP, ProxyServerPort, SysCode, TerminalNo, out ProxyOffline, out MaxJnl);
                InitGDSC2OK = ret;
                return ret;
            }
        }

        /// <summary>
        /// 读卡成功鸣叫
        /// </summary>
        public static void CardReaderOKBeep()
        {
            Syn.GDSC2.AIOAPI.TA_CRBeep(50);
        }

        /// <summary>
        /// 读卡失败鸣叫
        /// </summary>
        public static void CardReaderFaildBeep()
        {
            Syn.GDSC2.AIOAPI.TA_CRBeep(50);
            System.Threading.Thread.Sleep(100);
            Syn.GDSC2.AIOAPI.TA_CRBeep(200);
            System.Threading.Thread.Sleep(100);
            Syn.GDSC2.AIOAPI.TA_CRBeep(300);
        }

        private static Syn.GDSC2.ControlFile _controlFile = null;

        /// <summary>
        /// 获取控制文件
        /// </summary>
        /// <returns></returns>
        public static Syn.GDSC2.ControlFile GetControlFile()
        {
            InitGDSC2();
            if (_controlFile == null)
            {
                Syn.GDSC2.ControlFile cf = new Syn.GDSC2.ControlFile();
                cf.DownLoad(); //下载控制文件
                if (cf.Read())
                {
                    _controlFile = cf;
                    return _controlFile;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return _controlFile;
            }
        }

        /// <summary>
        /// 下载照片文件
        /// </summary>
        /// <param name="AccountID">一卡通帐号</param>
        /// <returns></returns>
        public static bool DownPhotoFile(uint AccountID)
        {
            try
            {
                if (InitGDSC2())
                {
                    Syn.GDSC2.AccountMsg acc = new Syn.GDSC2.AccountMsg();
                    acc.AccountNo = AccountID;

                    int RET = Syn.GDSC2.AIOAPI.TA_InqAcc(ref acc, TIME_OUT); //查询账户信息
                    if (RET == 0)
                    {
                        byte[] idno = System.Text.Encoding.Default.GetBytes(acc.IDNo);
                        byte[] filename = System.Text.Encoding.Default.GetBytes(acc.AccountNo + ".jpg");
                        int RET2 = Syn.GDSC2.AIOAPI.TA_DownPhotoFile(idno, filename, TIME_OUT);
                        return (RET2 == 0);
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 消费
        /// </summary>
        /// <param name="AccountID">一卡通帐号</param>
        /// <param name="CardNo">一卡通卡号</param>
        /// <param name="Fee">消费金额</param>
        /// <param name="Err">错误信息</param>
        /// <param name="TranJnl">流水号</param>
        /// <returns></returns>
        public static bool VIPRegistFee(uint AccountID, uint CardNo, uint Fee, ref string Err, ref uint TranJnl)
        {
            Syn.GDSC2.CardConsume cc = new CardConsume();
            cc.AccountNo = AccountID;
            cc.CardNo = CardNo;
            cc.Operator = gdsc2Helper.Operator;
            cc.TranAmt = (int)(-1 * Fee);
            cc.TerminalNo = gdsc2Helper.TerminalNo;
            cc.TranJnl = Syn.GDSC2.gdsc2Helper.GetJnl();
            cc.Abstract = gdsc2Helper.COMMERCE_ABSTRACT_CARDREADER;
            cc.FinancingType = gdsc2Helper.FINANCING_TYPE_CARDREADER;

            int RET = Syn.GDSC2.AIOAPI.TA_Consume(ref cc, false, TIME_OUT);
            if (RET == 0)
            {
                Err = "";
                TranJnl = cc.TranJnl;
                return true;
            }
            else
            {
                Err = "收取注册费用:" + Syn.GDSC2.ErrorMsg.GetErrorMessage(RET);
                TranJnl = 0;
                return false;
            }
        }

        /// <summary>
        /// 下载账户信息
        /// </summary>
        /// <param name="RecNum">记录数</param>
        /// <param name="FileName">文件名</param>
        /// <param name="sexno">性别</param>
        /// <param name="error">错误</param>
        /// <returns></returns>
        public static bool DownloadAccountInfo(out int RecNum, byte[] FileName, string sexno, ref string error)
        {
            Syn.GDSC2.AccountMsg pAccMsg = new AccountMsg();
            pAccMsg.SexNo = sexno;


            int RET = Syn.GDSC2.AIOAPI.TA_HazyInqAcc(ref pAccMsg, out RecNum, FileName, FileDownloadTimeOut);
            if (RET == 0)
            {
                return true;
            }
            else
            {
                error = "错误:" + Syn.GDSC2.ErrorMsg.GetErrorMessage(RET);
                return false;
            }
        }
    }
}
