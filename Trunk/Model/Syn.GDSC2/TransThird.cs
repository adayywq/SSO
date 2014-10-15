/*
 * ******************************************************************************************************************
 * �������ӿ� �ṹ�����ļ� TransThird.h
 * AIO_API.DLL �汾 2.1.0.1 ���������ڣ�2004/03/26
 * 
 * ���ߣ�������
 * ���������ڣ�2004/04/23
 ********************************************************************************************************************
 */

using System;
using System.Runtime.InteropServices;

namespace Syn.GDSC2
{
	#region ����
	/// <summary>
	/// TransThird.h�ж����һЩ����
	/// </summary>
	public class TransThird
	{
		private TransThird(){}
		public const string	THIRD_TRCD_DOWN_FILE		=	"!01";			//�����ļ�
		public const string	THIRD_TRCD_DOWN_CON_FILE	=	"!02";			//���ؿ����ļ�
		public const string	THIRD_TRCD_CONSUME			=	"15\x0";		//����
		public const string	THIRD_TRCD_REFUND			=	"23\x0";		//�˷�
		public const string	THIRD_TRCD_OPEN				=	"46\x0";		//��ͨ
		public const string	THIRD_TRCD_CLOSE			=	"47\x0";		//�ر�
		public const string	THIRD_TRCD_LOST				=	"42\x0";		//��ʧ
		public const string	THIRD_TRCD_UNLOST			=	"43\x0";		//���
		public const string	TC_THIRD_GETMAXJN			=	"50\x0";		//ȡ�����ˮ��

		public const short SIOS_PORT = 8500;//����������˿ں�
	}

	#endregion

	#region �ʻ���Ϣ�ṹ:AccountMsg

	/*�ʻ���Ϣ��*/
		/* typedef struct
				{
					char      			Name[21]; 				//�����ĸ�����
					char      			SexNo[2]; 				//�Ա�
					char				DeptCode[19];			//���Ŵ���
					unsigned int		CardNo; 				//����
					unsigned int		AccountNo; 				//�ʺ�
					char				StudentCode[21]; 		//ѧ��
					char				IDCard[21]; 			//���֤��
					char				PID[3];					//��ݴ���
					char				IDNo[13]; 				//������
					int					Balance; 				//�����
					char				Password[7];			//��������
					char				ExpireDate[7];			//�˻���ֹ����
					unsigned short		SubSeq;					//������
					char				IsOpenInSys;			//�Ƿ��ڱ�ϵͳ�ڿ�ͨ
					short				TerminalNo;				//�ն˺���
					short				RetCode;				//��̨������ֵ
				} AccountMsg;
		*/

		/// <summary>
		/// �ʻ���Ϣ��
		/// </summary>
		[ StructLayout( LayoutKind.Sequential,CharSet=CharSet.Ansi ,Pack=1)]
		public struct AccountMsg 
		{
			/// <summary>
			/// �����ĸ�����
			/// </summary>
			[MarshalAs( UnmanagedType.ByValTStr, SizeConst=21)]
			public	string      		Name; 							

			/// <summary>
			/// �Ա�
			/// </summary>
			[MarshalAs( UnmanagedType.ByValTStr, SizeConst=2)]
			public	string      		SexNo; 							//�Ա�

			/// <summary>
			/// ���Ŵ���
			/// </summary>
			[MarshalAs( UnmanagedType.ByValTStr, SizeConst=19)]
			public	string				DeptCode;						//���Ŵ���
			
			/// <summary>
			/// ����
			/// </summary>
			public	uint				CardNo; 						//����
			
			/// <summary>
			/// �ʺ�
			/// </summary>
			public	uint				AccountNo; 						//�ʺ�

			/// <summary>
			/// ѧ��
			/// </summary>
			[MarshalAs( UnmanagedType.ByValTStr, SizeConst=21)]
			public	string				StudentCode; 					//ѧ��
			
			/// <summary>
			/// ���֤��
			/// </summary>
			[MarshalAs( UnmanagedType.ByValTStr, SizeConst=21)]
			public	string				IDCard; 						//���֤��

			/// <summary>
			/// ��ݴ���
			/// </summary>
			[MarshalAs( UnmanagedType.ByValTStr, SizeConst=3)]
			public	string				PID;							//��ݴ���

			/// <summary>
			/// ������
			/// </summary>
			[MarshalAs( UnmanagedType.ByValTStr, SizeConst=13)]
			public	string				IDNo; 							//������

			/// <summary>
			/// �����
			/// </summary>
			public	int					Balance; 						//�����

			/// <summary>
			/// ��������
			/// </summary>
			[MarshalAs( UnmanagedType.ByValTStr, SizeConst=7)]
			public	string				Password;						//��������

			/// <summary>
			/// �˻���ֹ����
			/// </summary>
			[MarshalAs( UnmanagedType.ByValTStr, SizeConst=7)]
			public	string				ExpireDate;						//�˻���ֹ����
	
			/// <summary>
			/// ������
			/// </summary>
			public	ushort				SubSeq;							//������
			/// <summary>
			/// �Ƿ��ڱ�ϵͳ�ڿ�ͨ
			/// </summary>
			public	byte				IsOpenInSys;					//�Ƿ��ڱ�ϵͳ�ڿ�ͨ

			/// <summary>
			/// �ն˺���
			/// </summary>
			public	short				TerminalNo;						//�ն˺���

			/// <summary>
			/// ��̨������
			/// </summary>
			public	short				RetCode;						//��̨������			

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
			public	string Flag;			//״̬(2004-08-26����)  16

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 84)]
            public string Pad;				//Ԥ���ֶ�          84  
		}
	#endregion

	#region �������İ�:CardOperating
	//	/*�������İ�*/
	//	typedef struct 
	//			{
	//				unsigned int		AccountNo;		/*�ʺ�*/
	//				char				StudentNo[21];	/*ѧ��*/
	//				char				inqPassword[7];	/*��ѯ����*/
	//				char				Operator[3];	/*����Ա*/
	//				short				RetCode;		/*��̨������ֵ*/
	//			} CardOperating;

	/// <summary>
	/// �������İ�
	/// </summary>
	[ StructLayout( LayoutKind.Sequential, CharSet=CharSet.Ansi ,Pack=1)]
	public struct CardOperating
	{
		/// <summary>
		/// �ʺ�
		/// </summary>
		public uint		AccountNo;		

		/// <summary>
		/// ѧ��
		/// </summary>
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst=21)]
		string				StudentNo;

		/// <summary>
		/// ��ѯ����
		/// </summary>
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst=7)]
		string				inqPassword;

		/// <summary>
		/// ����Ա
		/// </summary>
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst=3)]
		string				Operator;

		/// <summary>
		/// ��̨������ֵ
		/// </summary>
		short				RetCode;

	}
	#endregion

	#region ��Ƭ��/�˷ѵİ�:CardConsume
	//	��Ƭ��/�˷ѵİ�
	//	typedef struct 
	//			{
	//				unsigned int		AccountNo;			/*�ʺ�*/	
	//				unsigned int		CardNo;				/*����*/	
	//				char				FinancingType[3];	/*��������*/
	//				int					CardBalance; 		/*�����,��ȷ����*/
	//				int					TranAmt; 			/*���׶�,��ȷ����*/
	//				unsigned short		UseCardNum;			/*�ÿ�����������ǰ*/
	//				unsigned short 		TerminalNo;			/*�ն˱��*/
	//				char				PassWord[7];		/*������*/
	//				char				Operator[3];		/*����Ա*/
	//				char				Abstract[129];		/*ժҪ*/
	//				unsigned int		TranJnl;			/*������ˮ��*/
	//				unsigned int		BackJnl;			/*��̨������ˮ��*/
	//				short				RetCode;			/*��̨������ֵ*/
	//			} CardConsume;


	/// <summary>
	/// ��Ƭ��/�˷ѵİ�
	/// </summary>
	[ StructLayout( LayoutKind.Sequential, CharSet=CharSet.Ansi ,Pack=1)]
	public struct CardConsume
	{
		/// <summary>
		/// �ʺ�
		/// </summary>
		public uint		AccountNo;			/*�ʺ�*/	

		/// <summary>
		/// ����
		/// </summary>
		public uint		CardNo;				/*����*/	

		/// <summary>
		/// ��������
		/// </summary>
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst=3)]
		public string				FinancingType;	/*��������*/

		/// <summary>
		/// ������ȷ����
		/// </summary>
		public int					CardBalance; 		/*�����,��ȷ����*/

		/// <summary>
		/// ���׶�,��ȷ����
		/// </summary>
		public int					TranAmt; 			/*���׶�,��ȷ����*/

		/// <summary>
		/// �ÿ�����������ǰ
		/// </summary>
		public ushort		UseCardNum;			/*�ÿ�����������ǰ*/

		/// <summary>
		/// �ն˱��
		/// </summary>
		public ushort 		TerminalNo;			/*�ն˱��*/
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst=7)]
		public string				PassWord;		/*������*/

		[MarshalAs( UnmanagedType.ByValTStr, SizeConst=3)]
		public string				Operator;		/*����Ա*/

		/// <summary>
		/// ժҪ
		/// </summary>
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst=129)]
		public string				Abstract;		/*ժҪ*/

		/// <summary>
		/// ������ˮ��
		/// </summary>
		public uint		TranJnl;			/*������ˮ��*/

		/// <summary>
		/// ��̨������ˮ��
		/// </summary>
		public uint		BackJnl;			/*��̨������ˮ��*/

		/// <summary>
		/// ��̨������ֵ
		/// </summary>
		public short				RetCode;			/*��̨������ֵ*/
	}
	#endregion

	#region ��Ƭ�շѵİ�:CardCharge
	//	/*��Ƭ�շѵİ�*/
	//	typedef struct 
	//			{
	//				char				Operator[4];		/*����Ա*/
	//				unsigned int		AccountNo;			/*�ʺ�*/	
	//				unsigned int		CardNo;				/*����*/	
	//				unsigned int 		FeeID;   			//�շ�ID��
	//				int					TranAmt; 			/*���׶�,��ȷ����*/
	//				char				ConsumeType[4]; 	//�շ�����
	//				char				FeeFlag[6];			/*FeeFlag[0]��0_�������� 1_�Զ�����*/
	//				/*FeeFlag[1]��0_У԰������ 1_���п����� 2_�ֽ𽻷� 3_���д��� */
	//				/*FeeFlag[2]��0_�ѽ��� 1_δ���� 2_�Ѷ��� 3_�Ѻ��� 4_������*/
	//				/*FeeFlag[3]:  0_һ�ν���	1_���ڽ���*/
	//				char				FeeDesc[31];		//��������
	//				int					CardBalance; 		/*�����,��ȷ����*/
	//				unsigned short  	TerminalNo;			/*�ն˱��*/
	//				char				FeeTerm[11];		/*����ʱ��*/
	//				char				BankAcc[21];		/*���п���*/
	//				char				Cname[31];			/*����������*/
	//				char				IdentityCode[21];	/*���֤��*/
	//				int					LateFeeAmt;			/*���ɽ�� ��ȷ����*/
	//				int					LateFeeRate;		/* ���ɽ��� */
	//				char				LateFeeStDate[15];	/*���ɽ�������� YYYYMMDD*/
	//				char				ExpDate[15];		/* �ؽ�����Ч��  */
	//				char				BillNo[51];			/* Ʊ�ݱ�� */
	//				unsigned int		TranJnl;			/*������ˮ��*/
	//				unsigned int		BackJnl;			/*��̨������ˮ��*/
	//				short				RetCode;			/*��̨������ֵ*/
	//			} CardCharge;

	/// <summary>
	/// ��Ƭ�շѵİ�
	/// </summary>
	[ StructLayout( LayoutKind.Sequential, CharSet=CharSet.Ansi ,Pack=1)]
	public struct CardCharge
	{
		/// <summary>
		/// ����Ա
		/// </summary>
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 4)]
		string				Operator;			/*����Ա*/
		/// <summary>
		/// �ʺ�
		/// </summary>
		uint				AccountNo;			/*�ʺ�*/	
		/// <summary>
		/// ����
		/// </summary>
		uint				CardNo;				/*����*/
		/// <summary>
		/// �շ�ID��
		/// </summary>
		uint 				FeeID;   			//�շ�ID��
		/// <summary>
		/// ���׶�,��ȷ����
		/// </summary>
		int					TranAmt; 			/*���׶�,��ȷ����*/
		
		/// <summary>
		/// �շ�����
		/// </summary>
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 4)]
		string				ConsumeType; 	//�շ�����
		
		/// <summary>
		/// FeeFlag[0]��0_�������� 1_�Զ�����*/
		/// FeeFlag[1]��0_У԰������ 1_���п����� 2_�ֽ𽻷� 3_���д��� */
		/// FeeFlag[2]��0_�ѽ��� 1_δ���� 2_�Ѷ��� 3_�Ѻ��� 4_������*/
		/// FeeFlag[3]��0_һ�ν���	1_���ڽ���
		/// </summary>
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 6)]
		string				FeeFlag;			/*FeeFlag[0]��0_�������� 1_�Զ�����*/
		/*FeeFlag[1]��0_У԰������ 1_���п����� 2_�ֽ𽻷� 3_���д��� */
		/*FeeFlag[2]��0_�ѽ��� 1_δ���� 2_�Ѷ��� 3_�Ѻ��� 4_������*/
		/*FeeFlag[3]:  0_һ�ν���	1_���ڽ���*/

		/// <summary>
		/// ��������
		/// </summary>
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 31)]
		string				FeeDesc;		//��������
		/// <summary>
		/// �����,��ȷ����
		/// </summary>
		int					CardBalance; 		/*�����,��ȷ����*/

		/// <summary>
		/// �ն˱��
		/// </summary>
		ushort			  	TerminalNo;			/*�ն˱��*/
		/// <summary>
		/// ����ʱ��
		/// </summary>
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 11)]
		string				FeeTerm;		/*����ʱ��*/

		/// <summary>
		/// ���п���
		/// </summary>
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 21)]
		string				BankAcc;		/*���п���*/

		/// <summary>
		/// ����������
		/// </summary>
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 31)]
		string				Cname;			/*����������*/

		/// <summary>
		/// ���֤��
		/// </summary>
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 21)]
		string				IdentityCode;	/*���֤��*/
		/// <summary>
		/// ���ɽ�� ��ȷ����
		/// </summary>
		int					LateFeeAmt;			/*���ɽ�� ��ȷ����*/
		/// <summary>
		/// ���ɽ���
		/// </summary>
		int					LateFeeRate;		/* ���ɽ��� */

		/// <summary>
		/// ���ɽ�������� YYYYMMDD
		/// </summary>
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 15)]
		string				LateFeeStDate;	/*���ɽ�������� YYYYMMDD*/

		/// <summary>
		/// �ؽ�����Ч��
		/// </summary>
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 15)]
		string				ExpDate;		/* �ؽ�����Ч��  */

		/// <summary>
		/// Ʊ�ݱ��
		/// </summary>
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 51)]
		string				BillNo;			/* Ʊ�ݱ�� */

		/// <summary>
		/// ������ˮ��
		/// </summary>
		uint		TranJnl;			/*������ˮ��*/
		/// <summary>
		/// ��̨������ˮ��
		/// </summary>
		uint		BackJnl;			/*��̨������ˮ��*/
		/// <summary>
		/// ��̨������ֵ
		/// </summary>
		short		RetCode;			/*��̨������ֵ*/
	}
	#endregion

	#region ��ѯ������ˮ�����ݰ�:InqTranFlow
	/*��ѯ������ˮ�����ݰ�*/
//	typedef struct
//			{
//				char				InqType;			/*��ѯ����,0-��ѯ������ˮ;1-��ʷ��ˮ*/
//				unsigned int		Account;			/*�ֿ����ʺ�*/
//				unsigned int		MercAcc;			/*�̻��ʺ�*/
//				short				TerminalNo;			/*�ն˺���*/
//				char				StartTime[15];		/*��ʼʱ��,YYYYMMDDHHMMSS*/
//				char				EndTime[15];		/*����ʱ��,YYYYMMDDHHMMSS*/
//				char				FileName[64];		/*���յ����ļ�����*/
//				int					RecNum;				/*��ѯ���ļ�¼��Ŀ*/
//			}InqTranFlow;

	/// <summary>
	/// ��ѯ������ˮ�����ݰ�
	/// </summary>
	[ StructLayout( LayoutKind.Sequential, CharSet=CharSet.Ansi ,Pack=1)]
	public struct InqTranFlow
	{
		/// <summary>
		/// ��ѯ����,0-��ѯ������ˮ;1-��ʷ��ˮ
		/// </summary>
		byte				InqType;			/*��ѯ����,0-��ѯ������ˮ;1-��ʷ��ˮ*/
		/// <summary>
		/// �ֿ����ʺ�
		/// </summary>
		uint		Account;			/*�ֿ����ʺ�*/
		/// <summary>
		/// �̻��ʺ�
		/// </summary>
		uint		MercAcc;			/*�̻��ʺ�*/
		/// <summary>
		/// �ն˺���
		/// </summary>
		short				TerminalNo;			/*�ն˺���*/
		/// <summary>
		/// ��ʼʱ��,YYYYMMDDHHMMSS
		/// </summary>
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 15)]
		string				StartTime;		/*��ʼʱ��,YYYYMMDDHHMMSS*/
		/// <summary>
		/// ����ʱ��,YYYYMMDDHHMMSS
		/// </summary>
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 15)]
		string				EndTime;		/*����ʱ��,YYYYMMDDHHMMSS*/
		/// <summary>
		/// ���յ����ļ�����
		/// </summary>
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 64)]
		string				FileName;		/*���յ����ļ�����*/
		/// <summary>
		/// ��ѯ���ļ�¼��Ŀ
		/// </summary>
		int					RecNum;				/*��ѯ���ļ�¼��Ŀ*/
	}
	#endregion

	#region ��ѯ��ͨ��ˮ :InqOpenFlow
	/*��ѯ��ͨ��ˮ*/
	//	typedef struct
	//			{
	//				char				InqType;			/*��ѯ����,0-��ѯ������ˮ;1-��ʷ��ˮ*/
	//				unsigned int		Account;			/*�ֿ����ʺ�*/
	//				int					SysCode;			/*ϵͳ����*/
	//				char				OpenDate[9];		/*����ʱ��,YYYYMMDD*/
	//				char				OperCode[3];		/*����Ա����*/
	//				char				FileName[64];		/*���յ����ļ�����*/
	//				int					RecNum;				/*��ѯ���ļ�¼��Ŀ*/
	//			}InqOpenFlow;

	/// <summary>
	/// ��ѯ��ͨ��ˮ
	/// </summary>
	[ StructLayout( LayoutKind.Sequential, CharSet=CharSet.Ansi ,Pack=1)]
	public struct InqOpenFlow
	{
		byte				InqType;			/*��ѯ����,0-��ѯ������ˮ;1-��ʷ��ˮ*/
		uint				Account;			/*�ֿ����ʺ�*/
		int					SysCode;			/*ϵͳ����*/
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 9)]
		string				OpenDate;		/*����ʱ��,YYYYMMDD*/
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 3)]
		string				OperCode;		/*����Ա����*/
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 64)]
		string				FileName;		/*���յ����ļ�����*/
		int					RecNum;				/*��ѯ���ļ�¼��Ŀ*/
	}
	#endregion

	#region ģ����ѯʱ���ص���ˮ�ļ���ʽ:TrjnFlowFile
	//ģ����ѯʱ���ص���ˮ�ļ���ʽ:
	//
	//��ˮ�ļ��ṹ
	//	typedef struct
	//			{
	//				long Account; /*�ʺ�*/
	//				long BackJnl; /*��̨��ˮ��*/
	//				long MercAccount; /*�̻��ʺ�*/
	//				long TerminalNo; /*�ն˱��*/
	//				char OperCode[4]; /*����Ա���*/
	//				char TranCode[3]; /*�¼�����*/
	//				char JnlStatus[2]; /*��ˮ״̬*/
	//				char JnDateTime[15]; /*��ˮ��������ʱ��YYYYMMDDHH24MISS*/
	//				char EffectDate [9]; /*��ˮ��Ч����YYYYMMDD*/
	//				long Balance; /*���*/
	//				long TranAmt; /*���׶�*/
	//				char ConsumeType[4]; /*��������*/
	//				char Resume[129]; /*��ע*/
	//			}TrjnFlowFile;
	//
		/// <summary>
		/// ģ����ѯʱ���ص���ˮ�ļ���ʽ
		/// </summary>
		[ StructLayout( LayoutKind.Sequential, CharSet=CharSet.Ansi ,Pack=1)]
	public struct TrjnFlowFile
	{
		/// <summary>
		/// �ʺ�
		/// </summary>
		long Account; /*�ʺ�*/
		/// <summary>
		/// ��̨��ˮ��
		/// </summary>
		long BackJnl; /*��̨��ˮ��*/
		/// <summary>
		/// �̻��ʺ�
		/// </summary>
		long MercAccount; /*�̻��ʺ�*/
		/// <summary>
		/// �ն˱��
		/// </summary>
		long TerminalNo; /*�ն˱��*/
		/// <summary>
		/// ����Ա���
		/// </summary>
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 4)]
		string OperCode; /*����Ա���*/
		/// <summary>
		/// �¼�����
		/// </summary>
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 3)]
		string TranCode; /*�¼�����*/
		/// <summary>
		/// ��ˮ״̬
		/// </summary>
		string JnlStatus; /*��ˮ״̬*/
		/// <summary>
		/// ��ˮ��������ʱ��YYYYMMDDHH24MISS
		/// </summary>
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 15)]
		string JnDateTime; /*��ˮ��������ʱ��YYYYMMDDHH24MISS*/
		/// <summary>
		/// ��ˮ��Ч����YYYYMMDD
		/// </summary>
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 9)]
		string EffectDate; /*��ˮ��Ч����YYYYMMDD*/
		/// <summary>
		/// ���*
		/// </summary>
		long Balance; /*���*/
		/// <summary>
		/// ���׶�
		/// </summary>
		long TranAmt; /*���׶�*/
		/// <summary>
		/// ��������
		/// </summary>
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 4)]
		string ConsumeType; /*��������*/
		/// <summary>
		/// ��ע
		/// </summary>
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 129)]
		string Resume; /*��ע*/
	}
	#endregion

	#region ģ����ѯʱ���صĿ�ͨ��ˮ���ļ��ṹ:OpenFlowFile
	/*ģ����ѯʱ���صĿ�ͨ��ˮ���ļ��ṹ*/

	//	typedef struct
	//			{
	//				long Account; /*�ʺ�*/
	//				long SysCode; /*ϵͳ����*/
	//				char OpenDate[9]; /*��ͨ����*/
	//				char OperCode[4]; /*����Ա����*/
	//			}OpenFlowFile

	/// <summary>
	/// ģ����ѯʱ���صĿ�ͨ��ˮ���ļ��ṹ
	/// </summary>
	[ StructLayout( LayoutKind.Sequential, CharSet=CharSet.Ansi ,Pack=1)]
	public struct OpenFlowFile
	{
		/// <summary>
		/// �ʺ�
		/// </summary>
		long Account; /*�ʺ�*/
		/// <summary>
		/// ͳ����
		/// </summary>
		long SysCode; /*ϵͳ����*/
		/// <summary>
		/// ��ͨ����
		/// </summary>
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 9)]
		string OpenDate; /*��ͨ����*/
		/// <summary>
		/// ����Ա����
		/// </summary>
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 4)]
		string OperCode; /*����Ա����*/
	}
	#endregion

	#region ģ����ѯʱ���ص��ʻ���Ϣ���ļ��ṹ:HazyInqAccMsg
	//	typedef struct
	//			{
	//				char      		Name[31]; 			/*����*/
	//				char      		SexNo[2]; 			/*�Ա�*/
	//				char			DeptCode[19];		/*���Ŵ���*/
	//				unsigned int	CardNo; 			/*����*/
	//				unsigned int	AccountNo; 			/*�ʺ�*/
	//				char			StudentCode[21];	/*ѧ��*/
	//				char			IDCard[21]; 		/*���֤��*/
	//				char			PID[3];				/*��ݴ���*/
	//				char			IDNo[13]; 			/*������*/
	//				int			    Balance; 			/*�����*/
	//				char			ExpireDate[7];		/*�˻���ֹ����*/
	//				unsigned int	SubSeq;			    /*������*/
	//				char                        Flag[16]; 
	//			}HazyInqAccMsg;
	/// <summary>
	/// ģ����ѯʱ���ص��ʻ���Ϣ���ļ��ṹ
	/// </summary>
	[ StructLayout( LayoutKind.Sequential, CharSet=CharSet.Ansi ,Pack=1)]
	public struct HazyInqAccMsg
	{
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 31)]
		public string      		Name; 								/*����*/
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 2)]
		public string      		SexNo; 								/*�Ա�*/
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 19)]
		public string				DeptCode;							/*���Ŵ���*/
		public uint				CardNo; 							/*����*/
		public uint				AccountNo; 							/*�ʺ�*/
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 21)]
		public string				StudentCode;						/*ѧ��*/
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 21)]
		public string				IDCard; 							/*���֤��*/
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 3)]
		public string				PID;								/*��ݴ���*/
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 13)]
		public string				IDNo; 								/*������*/
		public int			    	Balance; 							/*�����*/
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 7)]
		public string				ExpireDate;							/*�˻���ֹ����*/
		public uint				SubSeq;										/*������*/
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 16)]
		public string				Flag;										/*������*/
	}
	#endregion
}