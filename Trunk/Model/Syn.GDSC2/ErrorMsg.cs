using System;

namespace Syn.GDSC2
{
	/// <summary>
	/// ErrorMsg ��ժҪ˵����
	/// </summary>
	public class ErrorMsg
	{
		#region �������
		public const 	int	ERR_EXCEPTION		=	-9999;		//�ػ��쳣
		public const 	int	ERR_OK				=	0;			//���׳ɹ�
		public const 	int	ERR_VER				=	-1;			//�汾����
		public const 	int	ERR_RETCODE			=	-2;			//�����벻��
		public const 	int	ERR_LENGTH			=	-3;			//���ݳ��Ȳ���
		public const 	int	ERR_FILENAME		=	-4;			//�ļ����Ƿ�
		public const 	int	ERR_FILESTAT		=	-5;			//�ļ�����״̬�Ƿ�
		public const 	int	ERR_FAIL			=	-6;			//����ʧ��	
                
		
		/*sios error : ERR_SIOS_ */
		public const 	int	ERR_SIOS_NOREC		=	-100;		//ָ���ļ�¼����
		public const 	int	ERR_SIOS_DOWNLOAD	=	-101;		//�����ļ�ʧ��
		
		
		/*net error : ERR_NET_ */
		public const  	int	ERR_NET_CONNECT		=	-200;		//�������Ӳ�ͨ
		public const  	int	ERR_NET_SEND		=	-201;		//���ݷ��ͳ���
		public const  	int	ERR_NET_RECV		=	-202;		//���ݽ��ճ���
		public const  	int	ERR_NET_RECVFILE	=	-203;		//�����ļ�����
		public const  	int	ERR_NET_SENDFILE	=	-204;		//�����ļ�����
                
                
		/*trans errot : ERR_TRN_ */
		public const  	int	ERR_TRN_SUBCODE		=	-300;		//��Ч����ϵͳ����
		public const  	int	ERR_TRN_STATION		=	-301;		//��Ч��վ���
		
		
		/*EN_CARD error : ERR_ENCARD_ */
		public const  	int	ERR_ENCARD_RHEAD	=	-500;		//�����ܿ�ͷ��
		public const  	int	ERR_ENCARD_CONFIG	=	-501;		//������������
		public const  	int	ERR_ENCARD_RKEY		=	-502;		//����Կ��
		public const  	int	ERR_ENCARD_OPEN		=	-503;		//�򿪼��ܿ�ʧ��
		
		 
		/*DLL error : ERR_DLL_ from -1000 */
		public const  	int	ERR_DLL_SIOS		=	-1001;		//SIOSû����������
		public const  	int	ERR_DLL_DSQL		=	-1002;		//DSQL��������
		public const  	int	ERR_DLL_BUF_MIN		=	-1003;		//����Ļ�����̫С�����ܿ���
		public const  	int	ERR_DLL_UNPACK		=	-1004;		//�������
		public const  	int	ERR_DLL_REDO		=	-1005;		//����ҵ��2003-09-05
		public const  	int	ERR_DLL_NOPHOTO		=	-1006;		//û����Ƭ�ļ�
		public const  	int	ERR_DLL_NOFILE		=	-1007;		//ָ���ļ�������
                
                 
		/*������������ֵ from 1100*/
		public const  	int	ERR_FILEEXIST		=	-1100;		//�ļ��Ѿ�����
		public const  	int	ERR_REFUSE			=	-1101;		//�������ܾ�
		public const  	int	ERR_NO_FILE			=	-1102;		//û���ļ�
		public const  	int	ERR_DEL_FAIL		=	-1103;		//ɾ���ļ�ʧ��
		public const  	int	ERR_COMM_FAIL		=	-1104;		//ͨѶʧ��
		 
                 
		/*����������ֵ���� from 1200*/
		public const  	int	ERR_TA_TRANAMT		=	-1200;		//���׶����
		public const  	int	ERR_TA_NOT_INIT		=	-1201;		//������APIû�г�ʼ��
		public const  	int	ERR_TA_CARDREADER	=	-1202;		//����������
		public const  	int	ERR_TA_READCARD		=	-1203;		//����ʧ��
		public const  	int	ERR_TA_WRITECARD	=	-1204;		//д��ʧ��
		public const  	int	ERR_TA_LIMIT_FUNC	=	-1205;		//�������ù�������
		public const  	int	ERR_TA_CARDTYPE		=	-1206;		//�������ѿ�
		public const  	int	ERR_TA_SNO			=	-1207;		//�Ǳ�ԺУ��
		public const  	int	ERR_TA_EXPIRECARD	=	-1208;		//���ڿ�
		public const  	int	ERR_TA_FAIL_CHGUT	=	-1209;		//�޸��ÿ�����ʧ��
		public const  	int	ERR_TA_NOT_SAMECARD	=	-1210;		//д��ʱ���Ų���
		public const  	int	ERR_TA_WRONG_PWD	=	-1211;		//������ʱ�����������
		public const  	int	ERR_TA_LOW_BALAN	=	-1212;		//��������
		public const  	int	ERR_TA_EXCEED_QUOTA	=	-1213;		//���������޶�
		public const  	int	ERR_TA_LOST_CARD	=	-1214;		//��ʧ��
		public const  	int	ERR_TA_FREEZE_CARD	=	-1215;		//���Ῠ
		public const  	int	ERR_TA_CARDNO		=	-1216;		//�����ʺŲ���
		public const  	int	ERR_TA_ID_CLOSE		=	-1217;		//��ݹر�
		public const  	int	ERR_TA_CR_DLL		=	-1218;		//���ض�������̬���ӿ�ʧ��
		public const  	int	ERR_TA_CR_INIT		=	-1219;		//��������ʼ��ʧ��
		public const  	int	ERR_TA_PARA			=	-1220;		//��������
		public const  	int	ERR_TA_NOREC		=	-1221;		//û������ʻ�
		public const  	int	ERR_TA_SUB_SUCC		=	-1222;		//�����ɹ�,Ҳ����ȷ�ķ�����Ϣ
		public const  	int	ERR_TA_SUB_FAIL		=	-1223;		//����ʧ��,Ҳ����ȷ�ķ�����Ϣ
		public const  	int	ERR_TA_INITED		=	-1224;		//�������Ѿ���ʼ������ر�
		#endregion
		private ErrorMsg(){}	//��ֹ�౻ʵ����

		private static string MessageHeader = "";
		
		/// <summary>
		/// ����һ��ͨ�������ӿں�������ֵ������Ϣ
		/// </summary>
		/// <param name="RET">�������ӿڷ���ֵ</param>
		/// <returns>�ַ���������ֵ��Ϣ</returns>
		public  static string GetErrorMessage(int RET)
		{
			string result = "һ��ͨ����:";
			switch(RET)
			{
				case	ERR_EXCEPTION		:	result		=	"�ػ��쳣";       break;
				case	ERR_VER				:	result		=	"�汾����";       break;
				case	ERR_RETCODE			:	result		=	"�����벻��";       break;
				case	ERR_LENGTH			:	result		=	"���ݳ��Ȳ���";       break;
				case	ERR_FILENAME		:	result		=	"�ļ����Ƿ�";       break;
				case	ERR_FILESTAT		:	result		=	"�ļ�����״̬�Ƿ�";       break;
				case	ERR_FAIL			:	result		=	"����ʧ��";       break;
				case	ERR_SIOS_NOREC		:	result		=	"ָ���ļ�¼����";       break;
				case	ERR_SIOS_DOWNLOAD	:	result		=	"�����ļ�ʧ��";       break;
				case	ERR_NET_CONNECT		:	result		=	"�������Ӳ�ͨ";       break;
				case	ERR_NET_SEND		:	result		=	"���ݷ��ͳ���";       break;
				case	ERR_NET_RECV		:	result		=	"���ݽ��ճ���";       break;
				case	ERR_NET_RECVFILE	:	result		=	"�����ļ�����";       break;
				case	ERR_NET_SENDFILE	:	result		=	"�����ļ�����";       break;
				case	ERR_TRN_SUBCODE		:	result		=	"��Ч����ϵͳ����";       break;
				case	ERR_TRN_STATION		:	result		=	"��Ч��վ���";       break;
				case	ERR_ENCARD_RHEAD	:	result		=	"�����ܿ�ͷ��";       break;
				case	ERR_ENCARD_CONFIG	:	result		=	"������������";       break;
				case	ERR_ENCARD_RKEY		:	result		=	"����Կ��";       break;
				case	ERR_ENCARD_OPEN		:	result		=	"�򿪼��ܿ�ʧ��";       break;
				case	ERR_DLL_SIOS		:	result		=	"SIOSû����������";       break;
				case	ERR_DLL_DSQL		:	result		=	"DSQL��������";       break;
				case	ERR_DLL_BUF_MIN		:	result		=	"����Ļ�����̫С�����ܿ���";       break;
				case	ERR_DLL_UNPACK		:	result		=	"�������";       break;
				case	ERR_DLL_REDO		:	result		=	"����ҵ��2003-09-05";       break;
				case	ERR_DLL_NOPHOTO		:	result		=	"û����Ƭ�ļ�";       break;
				case	ERR_DLL_NOFILE		:	result		=	"ָ���ļ�������";       break;
				case	ERR_FILEEXIST		:	result		=	"�ļ��Ѿ�����";       break;
				case	ERR_REFUSE			:	result		=	"�������ܾ�";       break;
				case	ERR_NO_FILE			:	result		=	"û���ļ�";       break;
				case	ERR_DEL_FAIL		:	result		=	"ɾ���ļ�ʧ��";       break;
				case	ERR_COMM_FAIL		:	result		=	"ͨѶʧ��";       break;
				case	ERR_TA_TRANAMT		:	result		=	"���׶����";       break;
				case	ERR_TA_NOT_INIT		:	result		=	"������APIû�г�ʼ��";       break;
				case	ERR_TA_CARDREADER	:	result		=	"����������";       break;
				case	ERR_TA_READCARD		:	result		=	"����ʧ��";       break;
				case	ERR_TA_WRITECARD	:	result		=	"д��ʧ��";       break;
				case	ERR_TA_LIMIT_FUNC	:	result		=	"�������ù�������";       break;
				case	ERR_TA_CARDTYPE		:	result		=	"�������ѿ�";       break;
				case	ERR_TA_SNO			:	result		=	"�Ǳ�ԺУ��";       break;
				case	ERR_TA_EXPIRECARD	:	result		=	"���ڿ�";       break;
				case	ERR_TA_FAIL_CHGUT	:	result		=	"�޸��ÿ�����ʧ��";       break;
				case	ERR_TA_NOT_SAMECARD	:	result		=	"д��ʱ���Ų���";       break;
				case	ERR_TA_WRONG_PWD	:	result		=	"������ʱ�����������";       break;
				case	ERR_TA_LOW_BALAN	:	result		=	"��������";       break;
				case	ERR_TA_EXCEED_QUOTA	:	result		=	"���������޶�";       break;
				case	ERR_TA_LOST_CARD	:	result		=	"��ʧ��";       break;
				case	ERR_TA_FREEZE_CARD	:	result		=	"���Ῠ";       break;
				case	ERR_TA_CARDNO		:	result		=	"�����ʺŲ���";       break;
				case	ERR_TA_ID_CLOSE		:	result		=	"��ݹر�";       break;
				case	ERR_TA_CR_DLL		:	result		=	"���ض�������̬���ӿ�ʧ��";       break;
				case	ERR_TA_CR_INIT		:	result		=	"��������ʼ��ʧ��";       break;
				case	ERR_TA_PARA			:	result		=	"��������";       break;
				case	ERR_TA_NOREC		:	result		=	"û������ʻ�";       break;
				case	ERR_TA_SUB_SUCC		:	result		=	"�����ɹ�,Ҳ����ȷ�ķ�����Ϣ";       break;
				case	ERR_TA_SUB_FAIL		:	result		=	"����ʧ��,Ҳ����ȷ�ķ�����Ϣ";       break;
				case	ERR_TA_INITED		:	result		=	"�������Ѿ���ʼ������ر�";       break;
				default:	result = ":δ����ķ���ֵ"; break;
			}
			return MessageHeader + RET.ToString() + ":" + result;
		}
		
	}
}
