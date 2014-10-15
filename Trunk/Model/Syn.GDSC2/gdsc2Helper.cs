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

        public const string COMMERCE_ABSTRACT_CARDREADER = "ע���Ա����";
        public const string COMMERCE_ABSTRACT_DOORPOS = "�Ž�ͨ������";
        public const string COMMERCE_ABSTRACT_HAND = "�ֹ��շ�";

        public const short TIME_OUT = 10;

        public static string ProxyServerIP = "10.128.0.191";
        public static short ProxyServerPort = 8500;
        public static ushort SysCode = 147;
        public static ushort TerminalNo = 1;

        public static short TimeOut = TIME_OUT;

        public static short FileDownloadTimeOut = 1200;

        public static string Operator = "SZ";

        public static bool CheckPIDTag = true;

        public static string Department = "�����µ���";

        public static bool ProxyOffline;

        private static uint MaxJnl;

        /// <summary>
        /// ��ȡ��ˮ��
        /// </summary>
        /// <returns>ulong��ˮ��</returns>
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
        /// ��ʼ��������
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
                        int RET2 = Syn.GDSC2.AIOAPI.TA_CRInit(0, 0, 0); //���Գ�ʼ��USB������
                        if (RET2 == 0)
                        {
                            InitCardReaderOK = true;
                            return true;
                        }
                        else
                        {
                            int RET3 = Syn.GDSC2.AIOAPI.TA_CRInit(1, 0, 19200);	//���Գ�ʼ�����ڶ�����
                            if (RET3 == 0)
                            {
                                InitCardReaderOK = true;
                                return true;
                            }
                            else
                            {
                                int RET4 = Syn.GDSC2.AIOAPI.TA_CRInit(1, 1, 19200);	//���Գ�ʼ�����ڶ�����
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
        /// ��ʼ���������ӿ�
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
        /// �����ɹ�����
        /// </summary>
        public static void CardReaderOKBeep()
        {
            Syn.GDSC2.AIOAPI.TA_CRBeep(50);
        }

        /// <summary>
        /// ����ʧ������
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
        /// ��ȡ�����ļ�
        /// </summary>
        /// <returns></returns>
        public static Syn.GDSC2.ControlFile GetControlFile()
        {
            InitGDSC2();
            if (_controlFile == null)
            {
                Syn.GDSC2.ControlFile cf = new Syn.GDSC2.ControlFile();
                cf.DownLoad(); //���ؿ����ļ�
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
        /// ������Ƭ�ļ�
        /// </summary>
        /// <param name="AccountID">һ��ͨ�ʺ�</param>
        /// <returns></returns>
        public static bool DownPhotoFile(uint AccountID)
        {
            try
            {
                if (InitGDSC2())
                {
                    Syn.GDSC2.AccountMsg acc = new Syn.GDSC2.AccountMsg();
                    acc.AccountNo = AccountID;

                    int RET = Syn.GDSC2.AIOAPI.TA_InqAcc(ref acc, TIME_OUT); //��ѯ�˻���Ϣ
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
        /// ����
        /// </summary>
        /// <param name="AccountID">һ��ͨ�ʺ�</param>
        /// <param name="CardNo">һ��ͨ����</param>
        /// <param name="Fee">���ѽ��</param>
        /// <param name="Err">������Ϣ</param>
        /// <param name="TranJnl">��ˮ��</param>
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
                Err = "��ȡע�����:" + Syn.GDSC2.ErrorMsg.GetErrorMessage(RET);
                TranJnl = 0;
                return false;
            }
        }

        /// <summary>
        /// �����˻���Ϣ
        /// </summary>
        /// <param name="RecNum">��¼��</param>
        /// <param name="FileName">�ļ���</param>
        /// <param name="sexno">�Ա�</param>
        /// <param name="error">����</param>
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
                error = "����:" + Syn.GDSC2.ErrorMsg.GetErrorMessage(RET);
                return false;
            }
        }
    }
}
