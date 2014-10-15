/*
 * ******************************************************************************************************************
 * �������ӿ�API�����ļ�
 * AIO_API.DLL �汾 2.1.0.1 ���������ڣ�2004/03/26
 * 
 * ���ߣ�������
 * ���������ڣ�2004/04/29
 ********************************************************************************************************************
 */


using System;
using System.Runtime.InteropServices;

namespace Syn.GDSC2
{
	public class AIOAPI
	{		
		/*
		**�������ƣ�TA_Init
		**�������ܣ����ö�̬���Ե�������ʽ����
		**����˵����IP-SIOS��IP��ַ, port-sios �Ķ˿ں� 
		**			SysCode-ϵͳ����,ProxyOffline-��������Ƿ��ѻ�
		**			MaxJnl-�����ˮ��
		**����ֵ��TRUE/FALSE
		**����ʱ�䣺2003-08-21
		**�޸�ʱ�䣺2004-03-10
		**�޸����ݣ�ȥ������CardKey-��Ƭ��Կ
		**			����TA_Init
		
		EXTC BOOL  WINAPI TA_Init(char *IP , short port , unsigned short SysCode, 
																	   unsigned short TerminalNo, bool *ProxyOffline, ULONG *MaxJnl);
		*/
		/// <summary>
		/// ���ö�̬���Ե�������ʽ����
		/// </summary>
		/// <param name="IP">SIOS��IP��ַ</param>
		/// <param name="port">sios �Ķ˿ں�</param>
		/// <param name="SysCode">SysCode-ϵͳ����</param>
		/// <param name="TerminalNo">�ն˺�</param>
		/// <param name="ProxyOffline">ProxyOffline-��������Ƿ��ѻ�</param>
		/// <param name="MaxJnl">MaxJnl-�����ˮ��</param>
		/// <returns>TRUE/FALSE</returns>
		[DllImport("AIO_API.dll")]
		public static extern bool TA_Init(string IP,short port,ushort SysCode,ushort TerminalNo,out bool ProxyOffline,out uint MaxJnl);

		
		/*
		**�������ƣ�TA_CRInit
		**�������ܣ���ʼ��������
		**�����б�CardReaderType��������������������ͣ�0��usb��������1�����ڶ�������
		**			port������������˿ںţ�
		**			Baud_Rate����������������ʣ�
		**����ֵ:	������ֵ�б�
		**����ʱ�䣺2004-03-10
		
		EXTC int _stdcall TA_CRInit(char CardReaderType,int port,long Baud_Rate);
		*/
		/// <summary>
		/// ��ʼ��������
		/// </summary>
		/// <param name="CardReaderType">������������������ͣ�0��usb��������1�����ڶ�������</param>
		/// <param name="port">����������˿ں�</param>
		/// <param name="Baud_Rate">��������������� ����ʹ��19200</param>
		/// <returns>������ֵ�б�</returns>
		[DllImport("AIO_API.dll")]
		public static extern int TA_CRInit(byte CardReaderType,int port,long Baud_Rate);


		/*
		**�������ƣ�TA_CRClose
		**�������ܣ��رն�����
		**�����б�void
		**����ֵ:	true/false
		**����ʱ�䣺2004-03-10
		
		EXTC BOOL _stdcall TA_CRClose(void);
		*/
		/// <summary>
		/// �رն�����
		/// </summary>
		/// <returns>true/false</returns>
		[DllImport("AIO_API.dll")]
		public static extern bool TA_CRClose();


		/*
		**�������ƣ�TA_FastReadCard
		**�������ܣ����ٶ����ţ������ڼ����Ƿ��п�����������Ӧ��
		**			���ֻ��Ҫ���ٶ�ȡ���ţ�����Ҫ��ʼ����̬��(TA_Init())
		**�����б�CardNo������������ӿ�Ƭ�ж����Ŀ���
		**����ֵ:	������ֵ�б�Errormsg.h
		**����ʱ�䣺2004-03-10
		
		EXTC int _stdcall TA_FastGetCardNo(unsigned int *CardNo);
		*/

		/// <summary>
		/// ���ٶ����ţ������ڼ����Ƿ��п�����������Ӧ��
		/// ���ֻ��Ҫ���ٶ�ȡ���ţ�����Ҫ��ʼ����̬��(TA_Init())
		/// </summary>
		/// <param name="CardNo">����������ӿ�Ƭ�ж����Ŀ���</param>
		/// <returns>������ֵ�б�</returns>
		[DllImport("AIO_API.dll")]
		public static extern int TA_FastGetCardNo(out uint CardNo);


		/*
		**�������ƣ�TA_CRBeep
		**�������ܣ�����������
		**�����б�BeepSecond����������������ĺ�������
		**����ֵ:	������ֵ�б�Errormsg.h
		**����ʱ�䣺2004-03-10
		
		EXTC int _stdcall TA_CRBeep(unsigned int BeepMSecond);
		*/

		/// <summary>
		/// ����������
		/// </summary>
		/// <param name="BeepMSecond">��������������ĺ�������</param>
		/// <returns>������ֵ�б�</returns>
		[DllImport("AIO_API.dll")]
		public static extern int TA_CRBeep(uint BeepMSecond);


		/*
		**�������ƣ�TA_ReadCardSimple
		**�������ܣ��򵥶�����Ϣ���������������
		**�����б�pAccMsg������������ӿ�Ƭ�ж����Ŀ�Ƭ��Ϣ��
		**����ֵ:	������ֵ�б�Errormsg.h
		**����ʱ�䣺2004-03-10
		
		EXTC int _stdcall TA_ReadCardSimple(AccountMsg * pAccMsg);
		*/
		/// <summary>
		/// �򵥶�����Ϣ�������������
		/// </summary>
		/// <param name="pAccMsg">����������ӿ�Ƭ�ж����Ŀ�Ƭ��Ϣ��</param>
		/// <returns>������ֵ�б�</returns>
		[DllImport("AIO_API.dll")]
		public static extern int TA_ReadCardSimple(out AccountMsg pAccMsg);
		
		/*
		**�������ƣ�TA_CheckWL
		**�������ܣ������������ʺźͿ��ż���������
		**�����б�AccountNo�������������Ҫ��֤���ʺ�
		**			CardNo���������������
		**			CheckID������������Ƿ������ݿ�ͨ�ر�״̬
		**����ֵ:	������ֵ�б�Errormsg.h
		**����ʱ�䣺2004-03-10
		
		EXTC int _stdcall TA_CheckWL (unsigned int AccountNo , unsigned int CardNo , bool CheckID=true);
		*/
		/// <summary>
		/// �����������ʺźͿ��ż���������
		/// </summary>
		/// <param name="AccountNo">�����������Ҫ��֤���ʺ�</param>
		/// <param name="CardNo">�������������</param>
		/// <param name="CheckID">����������Ƿ������ݿ�ͨ�ر�״̬</param>
		/// <returns>������ֵ�б�</returns>
		[DllImport("AIO_API.dll")]
		public static extern int TA_CheckWL(uint AccountNo , uint CardNo , bool CheckID);

		/*
		**�������ƣ�TA_ReadCard
		**�������ܣ�������Ϣ����������Ϣ��������������жϿ�Ƭ����Ч�ԡ�
		**�����б�pAccMsg���ʻ���Ϣ��,�����Ҫ������ʱ������дpAccMsg->TerminalNo
		**			CheckID������������Ƿ������ݿ�ͨ�ر�״̬
		**			CheckSub������������Ƿ���ȡ����
		**����ֵ:	������ֵ�б�Errormsg.h
		**����ʱ�䣺2004-03-10
		
		EXTC int _stdcall TA_ReadCard(AccountMsg *pAccMsg,bool CheckID=true ,bool CheckSub= false);
		*/
		/// <summary>
		/// ������Ϣ����������Ϣ��������������жϿ�Ƭ����Ч�ԡ�
		/// </summary>
		/// <param name="pAccMsg">�ʻ���Ϣ��,�����Ҫ������ʱ������дpAccMsg->TerminalNo</param>
		/// <param name="CheckID">����������Ƿ������ݿ�ͨ�ر�״̬</param>
		/// <param name="CheckSub">����������Ƿ���ȡ����</param>
		/// <returns>������ֵ�б�</returns>
		[DllImport("AIO_API.dll")]
		public static extern int TA_ReadCard(out AccountMsg pAccMsg,bool CheckID ,bool CheckSub);

		
		/*
		**�������ƣ�TA_CardOpen
		**�������ܣ���ͨ
		**�����б�pCardOper���������������������ݰ�����������Ҫ��д�Ĳ������ʺź;����ˡ�
		**			pCardOper->RetCode�Ǻ�̨���׵ķ���ֵ
		**			TimeOut �� �����������ʱʱ�䣨�룩��ȱʡֵΪ10�롣
		**����ֵ:	������ֵ�б�Errormsg.h
		**����ʱ�䣺2004-03-11
		
		EXTC int _stdcall TA_CardOpen(CardOperating *pCardOper, short TimeOut = 10);
		*/
		/// <summary>
		/// ��ͨ
		/// </summary>
		/// <param name="pCardOper">�������������������ݰ�����������Ҫ��д�Ĳ������ʺź;����ˡ�
		/// pCardOper->RetCode�Ǻ�̨���׵ķ���ֵ</param>
		/// <param name="TimeOut">�����������ʱʱ�䣨�룩��ȱʡֵΪ10�롣</param>
		/// <returns>������ֵ�б�</returns>
		[DllImport("AIO_API.dll")]
		public static extern int TA_CardOpen(ref CardOperating pCardOper, short TimeOut);

		/*
		**�������ƣ�TA_CardClose
		**�������ܣ��ر�
		**�����б�pCardOper���������������������ݰ�����������Ҫ��д�Ĳ������ʺźͲ���Ա��
		**			pCardOper->RetCode�Ǻ�̨���׵ķ���ֵ
		**			TimeOut �� �����������ʱʱ�䣨�룩��ȱʡֵΪ10�롣
		**����ֵ:	������ֵ�б�Errormsg.h
		**����ʱ�䣺2004-03-11
		
		EXTC int _stdcall TA_CardClose(CardOperating *pCardOper, short TimeOut=10);
		*/
		/// <summary>
		/// �ر�
		/// </summary>
		/// <param name="pCardOper">pCardOper���������������������ݰ�����������Ҫ��д�Ĳ������ʺźͲ���Ա��
		///			pCardOper->RetCode�Ǻ�̨���׵ķ���ֵ</param>
		/// <param name="TimeOut"></param>
		/// <returns></returns>
		[DllImport("AIO_API.dll")]
		public static extern int TA_CardClose(ref CardOperating pCardOper, short TimeOut);
			
		
		/*
		**�������ƣ�TA_CardLost
		**�������ܣ���ʧ
		**�����б�pCardOper���������������������ݰ�����������Ҫ��д�Ĳ������ʺźͲ���Ա��
		**			pCardOper->RetCode�Ǻ�̨���׵ķ���ֵ
		**			TimeOut �� �����������ʱʱ�䣨�룩��ȱʡֵΪ10�롣
		**����ֵ:	������ֵ�б�Errormsg.h
		**����ʱ�䣺2004-03-11
		
		EXTC int _stdcall TA_CardLost(CardOperating *pCardOper, short TimeOut=10);
		*/
		/// <summary>
		/// ��ʧ
		/// </summary>
		/// <param name="pCardOper">pCardOper���������������������ݰ�����������Ҫ��д�Ĳ������ʺźͲ���Ա��
		///			pCardOper->RetCode�Ǻ�̨���׵ķ���ֵ</param>
		/// <param name="TimeOut">�����������ʱʱ�䣨�룩��ȱʡֵΪ10�롣</param>
		/// <returns>������ֵ�б�</returns>
		[DllImport("AIO_API.dll")]
		public static extern int TA_CardLost(ref CardOperating pCardOper, short TimeOut);

		/*
		**�������ƣ�TA_Consume
		**�������ܣ���Ƭ����(�����ѻ�)
		**�����б�pCardCons���������������������ݰ�,Ҫ��������뿨��
		**			pCardCons->RetCode�Ǻ�̨���׵ķ���ֵ
		**			IsVerfy���Ƿ���֤�ۼ����Ѷ��������ۼ����Ѷ����Ҫ�����������롣
		**			TimeOut �� �����������ʱʱ�䣨�룩��ȱʡֵΪ10�롣
		**����ֵ:	������ֵ�б�Errormsg.h
		**����ʱ�䣺2004-03-11
		
		EXTC int _stdcall TA_Consume(CardConsume *pCardCons, bool IsVerfy, short TimeOut=10);
		*/
		/// <summary>
		/// ��Ƭ����(�����ѻ�)
		/// </summary>
		/// <param name="pCardCons">�������������������ݰ�,Ҫ��������뿨��
		/// pCardCons->RetCode�Ǻ�̨���׵ķ���ֵ</param>
		/// <param name="IsVerfy">�Ƿ���֤�ۼ����Ѷ��������ۼ����Ѷ����Ҫ�����������롣</param>
		/// <param name="TimeOut">�����������ʱʱ�䣨�룩��ȱʡֵΪ10�롣</param>
		/// <returns>������ֵ�б�</returns>
		[DllImport("AIO_API.dll")]
		public static extern int TA_Consume(ref CardConsume pCardCons, bool IsVerfy, short TimeOut);


		/*
		**�������ƣ�TA_Refund
		**�������ܣ���Ƭ�˷�(��������)
		**�����б�pCardCons���������������������ݰ�,Ҫ��������뿨��
		**			pCardCons->RetCode�Ǻ�̨���׵ķ���ֵ
		**			TimeOut �� �����������ʱʱ�䣨�룩��ȱʡֵΪ10�롣
		**����ֵ:	������ֵ�б�Errormsg.h
		**����ʱ�䣺2004-03-15
		
		EXTC int _stdcall TA_Refund(CardConsume *pCardCons , short TimeOut=10);
		*/
		/// <summary>
		/// ��Ƭ�˷�(��������)
		/// </summary>
		/// <param name="pCardCons">�������������������ݰ�,Ҫ��������뿨��
		/// pCardCons->RetCode�Ǻ�̨���׵ķ���ֵ</param>
		/// <param name="TimeOut">�����������ʱʱ�䣨�룩��ȱʡֵΪ10�롣</param>
		/// <returns>������ֵ�б�</returns>
		[DllImport("AIO_API.dll")]
		public static extern int TA_Refund(ref CardConsume pCardCons , short TimeOut);

		/*
		**�������ƣ�TA_Charge
		**�������ܣ���Ƭ�շ�
		**�����б�pCardCharg���������������������ݰ�,Ҫ��������뿨��
		**			pCardCharg->RetCode�Ǻ�̨���׵ķ���ֵ
		**			IsVerfy���Ƿ���֤�ۼ����Ѷ��������ۼ����Ѷ����Ҫ�����������롣
		**			TimeOut �� �����������ʱʱ�䣨�룩��ȱʡֵΪ10�롣
		**����ֵ:	������ֵ�б�Errormsg.h
		**����ʱ�䣺2004-03-11
		
		EXTC int _stdcall TA_Charge(CardCharge *pCardCharg, bool IsVerfy, short TimeOut=10);
		*/
		/// <summary>
		/// ��Ƭ�շ�
		/// </summary>
		/// <param name="pCardCharg">�������������������ݰ�,Ҫ��������뿨��
		///			pCardCharg->RetCode�Ǻ�̨���׵ķ���ֵ</param>
		/// <param name="IsVerfy">�Ƿ���֤�ۼ����Ѷ��������ۼ����Ѷ����Ҫ�����������롣</param>
		/// <param name="TimeOut">�����������ʱʱ�䣨�룩��ȱʡֵΪ10�롣</param>
		/// <returns>������ֵ�б�</returns>
		[DllImport("AIO_API.dll")]
		public static extern int TA_Charge(ref CardCharge pCardCharg, bool IsVerfy, short TimeOut);

		/*
		**�������ƣ�TA_InqAcc
		**�������ܣ������ʺ�/����/ѧ����/֤���ž�ȷ��ѯ�ʻ���Ϣ
		**�����б�pAccMsg���������ʻ���Ϣ���������ݰ�
		**			(��Ҫ��д���Ż������ʺŻ�����ѧ���Ż�����֤����)��
		**			���������pAccMsg ->RetCodeΪ��̨����ķ���ֵ
		**			TimeOut �� �����������ʱʱ�䣨�룩��ȱʡֵΪ10�롣
		**����ֵ:	������ֵ�б�Errormsg.h
		**����ʱ�䣺2004-03-15
		
		EXTC int _stdcall TA_InqAcc(AccountMsg * pAccMsg, short TimeOut = 10);
		*/
		/// <summary>
		/// �����ʺ�/����/ѧ����/֤���ž�ȷ��ѯ�ʻ���Ϣ
		/// </summary>
		/// <param name="pAccMsg">pAccMsg���������ʻ���Ϣ���������ݰ�
		///			(��Ҫ��д���Ż������ʺŻ�����ѧ���Ż�����֤����)��
		///			���������pAccMsg ->RetCodeΪ��̨����ķ���ֵ</param>
		/// <param name="TimeOut">�����������ʱʱ�䣨�룩��ȱʡֵΪ10�롣</param>
		/// <returns>������ֵ�б�</returns>
		[DllImport("AIO_API.dll")]
		public static extern int TA_InqAcc(ref AccountMsg pAccMsg, short TimeOut);

		/*
		**�������ƣ�TA_HazyInqAcc
		**�������ܣ������Ŵ���ģ����ѯ�ֿ�����Ϣ��
		**�����б�pAccMsg�������������ѯ����(���Ը���Name,DeptCode,SexNo,StudentNo,PID��ѯ)
		**			��ѯ���ļ��ŵ�RecvTempĿ¼�£��ļ���д�뵽FileName��
		**			FileName��������������ص��ļ����ƣ�����64���ֽ�
		**			RecNum-�����������ѯ���ļ�¼��Ŀ
		**			TimeOut �� �����������ʱʱ�䣨�룩��ȱʡֵΪ10�롣
		**����ֵ:	������ֵ�б�Errormsg.h
		**����ʱ�䣺2004-03-15
		
		EXTC int _stdcall TA_HazyInqAcc(AccountMsg *pAccMsg, int *RecNum , char *FileName,short TimeOut = 10);
		*/
		/// <summary>
		/// �����Ŵ���ģ����ѯ�ֿ�����Ϣ��
		/// </summary>
		/// <param name="pAccMsg">pAccMsg�������������ѯ����(���Ը���Name,DeptCode,SexNo,StudentNo,PID��ѯ)
		///			��ѯ���ļ��ŵ�RecvTempĿ¼�£��ļ���д�뵽FileName��</param>
		/// <param name="RecNum"></param>
		/// <param name="FileName">FileName��������������ص��ļ����ƣ�����64���ֽ�</param>
		/// <param name="TimeOut"></param>
		/// <returns></returns>
		[DllImport("AIO_API.dll")]
		public static extern int TA_HazyInqAcc(ref AccountMsg pAccMsg, out int RecNum , byte[] FileName,short TimeOut);
		//public static extern int TA_HazyInqAcc(byte[] accmsg, out int RecNum , byte[] FileName,short TimeOut);

		/*
		**�������ƣ�TA_InqTranFlow
		**�������ܣ�������ˮ��ѯ��
		**�����б�pInqTranFlow���������,���Ը��ݳֿ����ʺš��̻��ʺš�
		**			�ն˺�����ϲ�ѯ����Ļ�����ʷ�Ľ�����ˮ��
		**			��ѯ���ļ��ŵ�RecvTempĿ¼�£��ļ���д�뵽pInqTranFlow->FileName�С�	
		**			pInqTranFlow->RecNum-�����������ѯ���ļ�¼��Ŀ
		**			TimeOut �� �����������ʱʱ�䣨�룩��ȱʡֵΪ10�롣
		**����ֵ:	������ֵ�б�Errormsg.h
		**����ʱ�䣺2004-03-15
		
		EXTC int _stdcall TA_InqTranFlow(InqTranFlow *pInqTranFlow, short TimeOut = 10);
		*/
		/// <summary>
		/// ������ˮ��ѯ
		/// </summary>
		/// <param name="pInqTranFlow">pInqTranFlow���������,���Ը��ݳֿ����ʺš��̻��ʺš�
		///			�ն˺�����ϲ�ѯ����Ļ�����ʷ�Ľ�����ˮ��
		///			��ѯ���ļ��ŵ�RecvTempĿ¼�£��ļ���д�뵽pInqTranFlow->FileName�С�	
		///			pInqTranFlow->RecNum-�����������ѯ���ļ�¼��Ŀ</param>
		/// <param name="TimeOut">�����������ʱʱ�䣨�룩��ȱʡֵΪ10�롣</param>
		/// <returns>������ֵ�б�</returns>
		[DllImport("AIO_API.dll")]
		public static extern int TA_InqTranFlow(ref InqTranFlow pInqTranFlow, short TimeOut);

		/*
		**�������ƣ�TA_InqOpenFlow
		**�������ܣ���ͨ��ˮ��ѯ��
		**�����б�pInqOpenFlow���������,���Ը��ݳֿ����ʺš��̻��ʺš�
		**			�ն˺�����ϲ�ѯ����Ļ�����ʷ�Ľ�����ˮ��
		**			��ѯ���ļ��ŵ�RecvTempĿ¼�£��ļ���д�뵽pInqOpenFlow->FileName�С�	
		**			pInqOpenFlow->RecNum-�����������ѯ���ļ�¼��Ŀ
		**			TimeOut �� �����������ʱʱ�䣨�룩��ȱʡֵΪ10�롣
		**����ֵ:	������ֵ�б�Errormsg.h
		**����ʱ�䣺2004-03-15
		
		EXTC int _stdcall TA_InqOpenFlow(InqOpenFlow *pInqOpenFlow, short TimeOut = 10);
		*/
		/// <summary>
		/// ��ͨ��ˮ��ѯ
		/// </summary>
		/// <param name="pInqOpenFlow">pInqOpenFlow���������,���Ը��ݳֿ����ʺš��̻��ʺš�
		///			�ն˺�����ϲ�ѯ����Ļ�����ʷ�Ľ�����ˮ��
		///			��ѯ���ļ��ŵ�RecvTempĿ¼�£��ļ���д�뵽pInqOpenFlow->FileName�С�	
		///			pInqOpenFlow->RecNum-�����������ѯ���ļ�¼��Ŀ</param>
		/// <param name="TimeOut">�����������ʱʱ�䣨�룩��ȱʡֵΪ10��</param>
		/// <returns>������ֵ�б�</returns>
		[DllImport("AIO_API.dll")]
		public static extern int TA_InqOpenFlow(ref InqOpenFlow pInqOpenFlow, short TimeOut);

		/*������:TA_DownControlFile
		*����:���ؿ����ļ��������ļ���Ŀ¼��\ControlFile
		*�������:timeout����ʱʱ��
		*�������:��
		*����ֵ:����0��ʾ���ص��ļ��Ĵ�С��С��0��ʾʧ��
		
		EXTC int _stdcall TA_DownControlFile(short timeOut=10);
		*/
		/// <summary>
		/// ���ؿ����ļ��������ļ���Ŀ¼��\ControlFile
		/// </summary>
		/// <param name="timeOut">��ʱʱ��</param>
		/// <returns>����0��ʾ���ص��ļ��Ĵ�С��С��0��ʾʧ��</returns>
		[DllImport("AIO_API.dll")]
		public static extern int TA_DownControlFile(short timeOut);

		/*
		**�������ƣ�TA_DownPhotoFile
		**�������ܣ�������Ƭ�ļ�
		**���������IDNo-�����ţ�PhotoFn-��Ƭ�ļ�������
		**�������: ��
		**����ֵ:	����0��ʾ���͵��ļ��Ĵ�С��С��0��ʾʧ��
		**����ʱ�䣺2004-03-19
		
		EXTC int  _stdcall TA_DownPhotoFile(char * IDNo, char *PhotoFn, short TimeOut=10);
		*/
		/// <summary>
		/// ������Ƭ�ļ�
		/// </summary>
		/// <param name="IDNo">������</param>
		/// <param name="PhotoFn">��Ƭ�ļ�������</param>
		/// <param name="TimeOut">��ʱʱ��</param>
		/// <returns>����0��ʾ���͵��ļ��Ĵ�С��С��0��ʾʧ��</returns>
		[DllImport("AIO_API.dll")]
		public static extern int TA_DownPhotoFile(byte[] IDNo, byte[] PhotoFn, short TimeOut);

        /*
		**�������ƣ�TA_UpJournal
		**�������ܣ��ϴ���ˮ��(�����ѻ�)
		**�����б�pCardCons���������������������ݰ�,Ҫ��������뿨��
		**			pCardCons->RetCode�Ǻ�̨���׵ķ���ֵ
		**			TimeOut �� �����������ʱʱ�䣨�룩��ȱʡֵΪ10�롣
		**����ֵ:	������ֵ�б�Errormsg.h
		**����ʱ�䣺2004-04-26
		
		EXTC int _stdcall TA_UpJournal(CardConsume *pCardCons, short TimeOut)
		*/
        [DllImport("AIO_API.dll")]
        public static extern int TA_UpJournal(ref CardConsume pCardCons, short TimeOut);

        /*
        **�������ƣ�TA_GetCK
        **�������ܣ���Ӿ��ϵͳȡ�ÿ�Ƭ��Կ
        **�����б�pwd-ȡ����Կ������
        **			CardKey-��Ƭ��Կ 8����Կ2����4ԺУ
        **����ֵ:	������ֵ�б�Errormsg.h
        **����ʱ�䣺2004-04-26
		
        EXTC BOOL _stdcall TA_GetCK(char *pwd , char * CardKey)  ����"swimming"					
        */
        [DllImport("AIO_API.dll")]
        public static extern bool TA_GetCK(byte[] pwd, byte[] CardKey);

        /*
        **���ܣ�һ��ͨ���ܺ���
        **	   ����˵����
        **iBuf��Ҫ���ܵĻ�����
        **		 iLen�����ܻ������ĳ��ȣ�����1��8��
        **oBuf�����ܺ�Ļ����������ȴ���iBuf��
        **����ֵ������ֵ
		
        EXTC BOOL WINAPI G_PW_Encrypt( char *iBuf, int iLen, char *oBuf );
        */
        [DllImport("AIO_API.dll")]
        public static extern int G_PW_Encrypt(byte[] inBuf, int iLen, byte[] outBuf);

        /*
        **���ܣ�һ��ͨ���ܺ���
        **����˵����
        **iBuf��Ҫ���ܵĻ�����
        **iLen�����ܻ������ĳ���
        **oBuf�����ܺ�Ļ�����
        **����ֵ������ֵ
		
        EXTC BOOL WINAPI G_PW_Decrypt( char *iBuf, int iLen, char *oBuf );
        */
        [DllImport("AIO_API.dll")]
        public static extern int G_PW_Decrypt(byte[] inBuf, int iLen, byte[] outBuf);

        /*
         *������:TA_CheckQpwd
         *����:�����˺���֤��ѯ����
         *�������:accountno-�ֿ����˺�,qpwd:�ֿ��˲�ѯ����
         *�������:��
         *����ֵ:0-��֤�ɹ� / 1-��֤ʧ�� / -1-��ѯ����ʧ��
         *��ע:Ϊ�人����վ����
         *����ʱ��:2004-10-14
         
        EXTC int _stdcall TA_CheckQpwd(unsigned int accountno , char * qpwd)
        */
        [DllImport("AIO_API.dll")]
        public static extern int TA_CheckQpwd(uint accountNo, byte[] qpwd);

        /*
        **�������ƣ�EC_GetEnCardCfg
        **�������ܣ�ȡ�ü��ܿ��е�ȫ������,Ϊ�׶���Ƭϵͳ��ӣ�����ȡ�����ڴ棬ÿ�ζ������ܿ�
        *�������:pec��ȫ�����ýṹָ��
        *�������:��
        *����ֵ:RET_OK/ERR_READ_ENCARD
        *��ע��2007-12-18
        **�޸����ݣ�ȥ������CardKey-��Ƭ��Կ		
        EXTC int WINAPI EC_GetEnCardCfg( ENCARD_CONFIG_ALL *pec)
        */
        [DllImport("AIO_API.dll")]
        public static extern int EC_GetEnCardCfg(out ENCARD_CONFIG_ALL ENCARD_CONFIG_ALL);

        //EXTC int _stdcall TA_GetNodeByType( int systypecode, unsigned short* pnodeid );
        /*
        **�������ƣ�TA_GetNodeByType
        **�������ܣ����������ۺ�ǰ�û�����ȫ�����õĽڵ���Ϣ������ѯĳ�����͵ĵ�һ���ڵ��
        **�����б�systypecode-[in]ϵͳ���ʹ��룻pnodeid-[OUT]��ѯ���ڵ�ţ�����ڵ��Ϊ0����ʾû�в鵽
        **����ֵ:	int
        **����ʱ�䣺2012-04-17
        */
        [DllImport("AIO_API.dll")]
        public static extern int TA_GetNodeByType(int SysTypeCode, out ushort pNodeID);

	}
    #region ��ѯ���ܿ��ڲ���Ϣ
    //typedef struct
    //{
    //    char 	ip_p[20];	//Ǯ��
    //    char	ip_f[20];	//ǰ�û�
    //    char	ip_i[20];	//���
    //    USHORT	port_p;
    //    USHORT	port_f;
    //    USHORT	port_i;
    //    USHORT	port_pftp;	//Ǯ��FTP�˿�
    //    USHORT	port_iftp;	//ǰ�û�FTP�˿�
    //    USHORT	NodeID;
    //    UCHAR	CardKey[8];
    //    UCHAR	CmpMark[2];
    //    UCHAR	SchoolCode[4];
    //    UCHAR	StaticKey[16];
    //    UCHAR	UserName[20];		//�û�����
    //    UCHAR	BankName[20];		//��������
    //    ULONG	UserCapacity;		//�û�(�ʺ�)����
    //    UCHAR	SysType;			//���������
    //}ENCARD_CONFIG_ALL;
    /// <summary>
    /// ��ѯ���ܿ��ڲ���Ϣ
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct ENCARD_CONFIG_ALL
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string ip_p;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string ip_f;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string ip_i;

        public ushort port_p;
        public ushort port_f;
        public ushort port_i;
        public ushort port_pftp;
        public ushort port_iftp;
        public ushort NodeID;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)]
        public string CardKey;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 2)]
        public string CmpMark;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string SchoolCode;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string StaticKey;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string UserName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string BankName;
        public uint UserCapacity;
        public uint SysType;
    }

    //[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    //public struct ENCARD_CONFIG_ALL
    //{
    //    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
    //    public string ip_p;
    //    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
    //    public string ip_f;
    //    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
    //    public string ip_i;

    //    public ushort port_p;
    //    public ushort port_f;
    //    public ushort port_i;
    //    public ushort port_pftp;          /*Ǯ��FTP�˿�*/
    //    public ushort port_iftp;        /*ǰ�û�FTP�˿�*/
    //    public ushort NodeID;

    //    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)]
    //    public string CardKey;
    //    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 2)]
    //    public string CmpMark;
    //    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
    //    public string SchoolCode;
    //    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
    //    public string StaticKey;
    //    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
    //    public string UserName;   							/*�û�����*/
    //    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
    //    public string BankName; 								/*��������*/

    //    public uint UserCapacity;								/*�û�(�ʺ�)����*/
    //    public uint SysType; 								/*���������*/
    //}
    #endregion
}
