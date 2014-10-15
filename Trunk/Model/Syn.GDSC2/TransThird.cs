/*
 * ******************************************************************************************************************
 * 第三方接口 结构声明文件 TransThird.h
 * AIO_API.DLL 版本 2.1.0.1 最后更新日期：2004/03/26
 * 
 * 作者：赵立仁
 * 最后更新日期：2004/04/23
 ********************************************************************************************************************
 */

using System;
using System.Runtime.InteropServices;

namespace Syn.GDSC2
{
	#region 常量
	/// <summary>
	/// TransThird.h中定义的一些常量
	/// </summary>
	public class TransThird
	{
		private TransThird(){}
		public const string	THIRD_TRCD_DOWN_FILE		=	"!01";			//下载文件
		public const string	THIRD_TRCD_DOWN_CON_FILE	=	"!02";			//下载控制文件
		public const string	THIRD_TRCD_CONSUME			=	"15\x0";		//消费
		public const string	THIRD_TRCD_REFUND			=	"23\x0";		//退费
		public const string	THIRD_TRCD_OPEN				=	"46\x0";		//开通
		public const string	THIRD_TRCD_CLOSE			=	"47\x0";		//关闭
		public const string	THIRD_TRCD_LOST				=	"42\x0";		//挂失
		public const string	THIRD_TRCD_UNLOST			=	"43\x0";		//解挂
		public const string	TC_THIRD_GETMAXJN			=	"50\x0";		//取最大流水号

		public const short SIOS_PORT = 8500;//代理服务器端口号
	}

	#endregion

	#region 帐户信息结构:AccountMsg

	/*帐户信息包*/
		/* typedef struct
				{
					char      			Name[21]; 				//姓名四个汉字
					char      			SexNo[2]; 				//性别
					char				DeptCode[19];			//部门代码
					unsigned int		CardNo; 				//卡号
					unsigned int		AccountNo; 				//帐号
					char				StudentCode[21]; 		//学号
					char				IDCard[21]; 			//身份证号
					char				PID[3];					//身份代码
					char				IDNo[13]; 				//身份序号
					int					Balance; 				//现余额
					char				Password[7];			//消费密码
					char				ExpireDate[7];			//账户截止日期
					unsigned short		SubSeq;					//补助戳
					char				IsOpenInSys;			//是否在本系统内开通
					short				TerminalNo;				//终端号码
					short				RetCode;				//后台处理返回值
				} AccountMsg;
		*/

		/// <summary>
		/// 帐户信息包
		/// </summary>
		[ StructLayout( LayoutKind.Sequential,CharSet=CharSet.Ansi ,Pack=1)]
		public struct AccountMsg 
		{
			/// <summary>
			/// 姓名四个汉字
			/// </summary>
			[MarshalAs( UnmanagedType.ByValTStr, SizeConst=21)]
			public	string      		Name; 							

			/// <summary>
			/// 性别
			/// </summary>
			[MarshalAs( UnmanagedType.ByValTStr, SizeConst=2)]
			public	string      		SexNo; 							//性别

			/// <summary>
			/// 部门代码
			/// </summary>
			[MarshalAs( UnmanagedType.ByValTStr, SizeConst=19)]
			public	string				DeptCode;						//部门代码
			
			/// <summary>
			/// 卡号
			/// </summary>
			public	uint				CardNo; 						//卡号
			
			/// <summary>
			/// 帐号
			/// </summary>
			public	uint				AccountNo; 						//帐号

			/// <summary>
			/// 学号
			/// </summary>
			[MarshalAs( UnmanagedType.ByValTStr, SizeConst=21)]
			public	string				StudentCode; 					//学号
			
			/// <summary>
			/// 身份证号
			/// </summary>
			[MarshalAs( UnmanagedType.ByValTStr, SizeConst=21)]
			public	string				IDCard; 						//身份证号

			/// <summary>
			/// 身份代码
			/// </summary>
			[MarshalAs( UnmanagedType.ByValTStr, SizeConst=3)]
			public	string				PID;							//身份代码

			/// <summary>
			/// 身份序号
			/// </summary>
			[MarshalAs( UnmanagedType.ByValTStr, SizeConst=13)]
			public	string				IDNo; 							//身份序号

			/// <summary>
			/// 现余额
			/// </summary>
			public	int					Balance; 						//现余额

			/// <summary>
			/// 消费密码
			/// </summary>
			[MarshalAs( UnmanagedType.ByValTStr, SizeConst=7)]
			public	string				Password;						//消费密码

			/// <summary>
			/// 账户截止日期
			/// </summary>
			[MarshalAs( UnmanagedType.ByValTStr, SizeConst=7)]
			public	string				ExpireDate;						//账户截止日期
	
			/// <summary>
			/// 补助戳
			/// </summary>
			public	ushort				SubSeq;							//补助戳
			/// <summary>
			/// 是否在本系统内开通
			/// </summary>
			public	byte				IsOpenInSys;					//是否在本系统内开通

			/// <summary>
			/// 终端号码
			/// </summary>
			public	short				TerminalNo;						//终端号码

			/// <summary>
			/// 后台处理返回
			/// </summary>
			public	short				RetCode;						//后台处理返回			

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
			public	string Flag;			//状态(2004-08-26增加)  16

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 84)]
            public string Pad;				//预留字段          84  
		}
	#endregion

	#region 卡操作的包:CardOperating
	//	/*卡操作的包*/
	//	typedef struct 
	//			{
	//				unsigned int		AccountNo;		/*帐号*/
	//				char				StudentNo[21];	/*学号*/
	//				char				inqPassword[7];	/*查询密码*/
	//				char				Operator[3];	/*操作员*/
	//				short				RetCode;		/*后台处理返回值*/
	//			} CardOperating;

	/// <summary>
	/// 卡操作的包
	/// </summary>
	[ StructLayout( LayoutKind.Sequential, CharSet=CharSet.Ansi ,Pack=1)]
	public struct CardOperating
	{
		/// <summary>
		/// 帐号
		/// </summary>
		public uint		AccountNo;		

		/// <summary>
		/// 学号
		/// </summary>
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst=21)]
		string				StudentNo;

		/// <summary>
		/// 查询密码
		/// </summary>
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst=7)]
		string				inqPassword;

		/// <summary>
		/// 操作员
		/// </summary>
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst=3)]
		string				Operator;

		/// <summary>
		/// 后台处理返回值
		/// </summary>
		short				RetCode;

	}
	#endregion

	#region 卡片消/退费的包:CardConsume
	//	卡片消/退费的包
	//	typedef struct 
	//			{
	//				unsigned int		AccountNo;			/*帐号*/	
	//				unsigned int		CardNo;				/*卡号*/	
	//				char				FinancingType[3];	/*财务类型*/
	//				int					CardBalance; 		/*卡余额,精确至分*/
	//				int					TranAmt; 			/*交易额,精确至分*/
	//				unsigned short		UseCardNum;			/*用卡次数，交易前*/
	//				unsigned short 		TerminalNo;			/*终端编号*/
	//				char				PassWord[7];		/*卡密码*/
	//				char				Operator[3];		/*操作员*/
	//				char				Abstract[129];		/*摘要*/
	//				unsigned int		TranJnl;			/*交易流水号*/
	//				unsigned int		BackJnl;			/*后台交易流水号*/
	//				short				RetCode;			/*后台处理返回值*/
	//			} CardConsume;


	/// <summary>
	/// 卡片消/退费的包
	/// </summary>
	[ StructLayout( LayoutKind.Sequential, CharSet=CharSet.Ansi ,Pack=1)]
	public struct CardConsume
	{
		/// <summary>
		/// 帐号
		/// </summary>
		public uint		AccountNo;			/*帐号*/	

		/// <summary>
		/// 卡号
		/// </summary>
		public uint		CardNo;				/*卡号*/	

		/// <summary>
		/// 财务类型
		/// </summary>
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst=3)]
		public string				FinancingType;	/*财务类型*/

		/// <summary>
		/// 卡余额，精确到分
		/// </summary>
		public int					CardBalance; 		/*卡余额,精确至分*/

		/// <summary>
		/// 交易额,精确至分
		/// </summary>
		public int					TranAmt; 			/*交易额,精确至分*/

		/// <summary>
		/// 用卡次数，交易前
		/// </summary>
		public ushort		UseCardNum;			/*用卡次数，交易前*/

		/// <summary>
		/// 终端编号
		/// </summary>
		public ushort 		TerminalNo;			/*终端编号*/
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst=7)]
		public string				PassWord;		/*卡密码*/

		[MarshalAs( UnmanagedType.ByValTStr, SizeConst=3)]
		public string				Operator;		/*操作员*/

		/// <summary>
		/// 摘要
		/// </summary>
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst=129)]
		public string				Abstract;		/*摘要*/

		/// <summary>
		/// 交易流水号
		/// </summary>
		public uint		TranJnl;			/*交易流水号*/

		/// <summary>
		/// 后台交易流水号
		/// </summary>
		public uint		BackJnl;			/*后台交易流水号*/

		/// <summary>
		/// 后台处理返回值
		/// </summary>
		public short				RetCode;			/*后台处理返回值*/
	}
	#endregion

	#region 卡片收费的包:CardCharge
	//	/*卡片收费的包*/
	//	typedef struct 
	//			{
	//				char				Operator[4];		/*操作员*/
	//				unsigned int		AccountNo;			/*帐号*/	
	//				unsigned int		CardNo;				/*卡号*/	
	//				unsigned int 		FeeID;   			//收费ID号
	//				int					TranAmt; 			/*交易额,精确至分*/
	//				char				ConsumeType[4]; 	//收费类型
	//				char				FeeFlag[6];			/*FeeFlag[0]：0_自助交费 1_自动交费*/
	//				/*FeeFlag[1]：0_校园卡交费 1_银行卡交费 2_现金交费 3_银行代收 */
	//				/*FeeFlag[2]：0_已交费 1_未交费 2_已对帐 3_已核销 4_已作废*/
	//				/*FeeFlag[3]:  0_一次交清	1_分期交费*/
	//				char				FeeDesc[31];		//费用描述
	//				int					CardBalance; 		/*卡余额,精确至分*/
	//				unsigned short  	TerminalNo;			/*终端编号*/
	//				char				FeeTerm[11];		/*费用时序*/
	//				char				BankAcc[21];		/*银行卡号*/
	//				char				Cname[31];			/*中文名姓名*/
	//				char				IdentityCode[21];	/*身份证号*/
	//				int					LateFeeAmt;			/*滞纳金额 精确至分*/
	//				int					LateFeeRate;		/* 滞纳金率 */
	//				char				LateFeeStDate[15];	/*滞纳金起计日期 YYYYMMDD*/
	//				char				ExpDate[15];		/* 必交费有效期  */
	//				char				BillNo[51];			/* 票据编号 */
	//				unsigned int		TranJnl;			/*交易流水号*/
	//				unsigned int		BackJnl;			/*后台交易流水号*/
	//				short				RetCode;			/*后台处理返回值*/
	//			} CardCharge;

	/// <summary>
	/// 卡片收费的包
	/// </summary>
	[ StructLayout( LayoutKind.Sequential, CharSet=CharSet.Ansi ,Pack=1)]
	public struct CardCharge
	{
		/// <summary>
		/// 操作员
		/// </summary>
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 4)]
		string				Operator;			/*操作员*/
		/// <summary>
		/// 帐号
		/// </summary>
		uint				AccountNo;			/*帐号*/	
		/// <summary>
		/// 卡号
		/// </summary>
		uint				CardNo;				/*卡号*/
		/// <summary>
		/// 收费ID号
		/// </summary>
		uint 				FeeID;   			//收费ID号
		/// <summary>
		/// 交易额,精确至分
		/// </summary>
		int					TranAmt; 			/*交易额,精确至分*/
		
		/// <summary>
		/// 收费类型
		/// </summary>
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 4)]
		string				ConsumeType; 	//收费类型
		
		/// <summary>
		/// FeeFlag[0]：0_自助交费 1_自动交费*/
		/// FeeFlag[1]：0_校园卡交费 1_银行卡交费 2_现金交费 3_银行代收 */
		/// FeeFlag[2]：0_已交费 1_未交费 2_已对帐 3_已核销 4_已作废*/
		/// FeeFlag[3]：0_一次交清	1_分期交费
		/// </summary>
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 6)]
		string				FeeFlag;			/*FeeFlag[0]：0_自助交费 1_自动交费*/
		/*FeeFlag[1]：0_校园卡交费 1_银行卡交费 2_现金交费 3_银行代收 */
		/*FeeFlag[2]：0_已交费 1_未交费 2_已对帐 3_已核销 4_已作废*/
		/*FeeFlag[3]:  0_一次交清	1_分期交费*/

		/// <summary>
		/// 费用描述
		/// </summary>
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 31)]
		string				FeeDesc;		//费用描述
		/// <summary>
		/// 卡余额,精确至分
		/// </summary>
		int					CardBalance; 		/*卡余额,精确至分*/

		/// <summary>
		/// 终端编号
		/// </summary>
		ushort			  	TerminalNo;			/*终端编号*/
		/// <summary>
		/// 费用时序
		/// </summary>
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 11)]
		string				FeeTerm;		/*费用时序*/

		/// <summary>
		/// 银行卡号
		/// </summary>
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 21)]
		string				BankAcc;		/*银行卡号*/

		/// <summary>
		/// 中文名姓名
		/// </summary>
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 31)]
		string				Cname;			/*中文名姓名*/

		/// <summary>
		/// 身份证号
		/// </summary>
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 21)]
		string				IdentityCode;	/*身份证号*/
		/// <summary>
		/// 滞纳金额 精确至分
		/// </summary>
		int					LateFeeAmt;			/*滞纳金额 精确至分*/
		/// <summary>
		/// 滞纳金率
		/// </summary>
		int					LateFeeRate;		/* 滞纳金率 */

		/// <summary>
		/// 滞纳金起计日期 YYYYMMDD
		/// </summary>
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 15)]
		string				LateFeeStDate;	/*滞纳金起计日期 YYYYMMDD*/

		/// <summary>
		/// 必交费有效期
		/// </summary>
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 15)]
		string				ExpDate;		/* 必交费有效期  */

		/// <summary>
		/// 票据编号
		/// </summary>
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 51)]
		string				BillNo;			/* 票据编号 */

		/// <summary>
		/// 交易流水号
		/// </summary>
		uint		TranJnl;			/*交易流水号*/
		/// <summary>
		/// 后台交易流水号
		/// </summary>
		uint		BackJnl;			/*后台交易流水号*/
		/// <summary>
		/// 后台处理返回值
		/// </summary>
		short		RetCode;			/*后台处理返回值*/
	}
	#endregion

	#region 查询交易流水的数据包:InqTranFlow
	/*查询交易流水的数据包*/
//	typedef struct
//			{
//				char				InqType;			/*查询类型,0-查询当日流水;1-历史流水*/
//				unsigned int		Account;			/*持卡人帐号*/
//				unsigned int		MercAcc;			/*商户帐号*/
//				short				TerminalNo;			/*终端号码*/
//				char				StartTime[15];		/*起始时间,YYYYMMDDHHMMSS*/
//				char				EndTime[15];		/*结束时间,YYYYMMDDHHMMSS*/
//				char				FileName[64];		/*接收到的文件名称*/
//				int					RecNum;				/*查询到的记录数目*/
//			}InqTranFlow;

	/// <summary>
	/// 查询交易流水的数据包
	/// </summary>
	[ StructLayout( LayoutKind.Sequential, CharSet=CharSet.Ansi ,Pack=1)]
	public struct InqTranFlow
	{
		/// <summary>
		/// 查询类型,0-查询当日流水;1-历史流水
		/// </summary>
		byte				InqType;			/*查询类型,0-查询当日流水;1-历史流水*/
		/// <summary>
		/// 持卡人帐号
		/// </summary>
		uint		Account;			/*持卡人帐号*/
		/// <summary>
		/// 商户帐号
		/// </summary>
		uint		MercAcc;			/*商户帐号*/
		/// <summary>
		/// 终端号码
		/// </summary>
		short				TerminalNo;			/*终端号码*/
		/// <summary>
		/// 起始时间,YYYYMMDDHHMMSS
		/// </summary>
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 15)]
		string				StartTime;		/*起始时间,YYYYMMDDHHMMSS*/
		/// <summary>
		/// 结束时间,YYYYMMDDHHMMSS
		/// </summary>
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 15)]
		string				EndTime;		/*结束时间,YYYYMMDDHHMMSS*/
		/// <summary>
		/// 接收到的文件名称
		/// </summary>
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 64)]
		string				FileName;		/*接收到的文件名称*/
		/// <summary>
		/// 查询到的记录数目
		/// </summary>
		int					RecNum;				/*查询到的记录数目*/
	}
	#endregion

	#region 查询开通流水 :InqOpenFlow
	/*查询开通流水*/
	//	typedef struct
	//			{
	//				char				InqType;			/*查询类型,0-查询当日流水;1-历史流水*/
	//				unsigned int		Account;			/*持卡人帐号*/
	//				int					SysCode;			/*系统代码*/
	//				char				OpenDate[9];		/*结束时间,YYYYMMDD*/
	//				char				OperCode[3];		/*操作员代码*/
	//				char				FileName[64];		/*接收到的文件名称*/
	//				int					RecNum;				/*查询到的记录数目*/
	//			}InqOpenFlow;

	/// <summary>
	/// 查询开通流水
	/// </summary>
	[ StructLayout( LayoutKind.Sequential, CharSet=CharSet.Ansi ,Pack=1)]
	public struct InqOpenFlow
	{
		byte				InqType;			/*查询类型,0-查询当日流水;1-历史流水*/
		uint				Account;			/*持卡人帐号*/
		int					SysCode;			/*系统代码*/
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 9)]
		string				OpenDate;		/*结束时间,YYYYMMDD*/
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 3)]
		string				OperCode;		/*操作员代码*/
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 64)]
		string				FileName;		/*接收到的文件名称*/
		int					RecNum;				/*查询到的记录数目*/
	}
	#endregion

	#region 模糊查询时返回的流水文件格式:TrjnFlowFile
	//模糊查询时返回的流水文件格式:
	//
	//流水文件结构
	//	typedef struct
	//			{
	//				long Account; /*帐号*/
	//				long BackJnl; /*后台流水号*/
	//				long MercAccount; /*商户帐号*/
	//				long TerminalNo; /*终端编号*/
	//				char OperCode[4]; /*操作员编号*/
	//				char TranCode[3]; /*事件代码*/
	//				char JnlStatus[2]; /*流水状态*/
	//				char JnDateTime[15]; /*流水发生日期时间YYYYMMDDHH24MISS*/
	//				char EffectDate [9]; /*流水生效日期YYYYMMDD*/
	//				long Balance; /*余额*/
	//				long TranAmt; /*交易额*/
	//				char ConsumeType[4]; /*财务类型*/
	//				char Resume[129]; /*备注*/
	//			}TrjnFlowFile;
	//
		/// <summary>
		/// 模糊查询时返回的流水文件格式
		/// </summary>
		[ StructLayout( LayoutKind.Sequential, CharSet=CharSet.Ansi ,Pack=1)]
	public struct TrjnFlowFile
	{
		/// <summary>
		/// 帐号
		/// </summary>
		long Account; /*帐号*/
		/// <summary>
		/// 后台流水号
		/// </summary>
		long BackJnl; /*后台流水号*/
		/// <summary>
		/// 商户帐号
		/// </summary>
		long MercAccount; /*商户帐号*/
		/// <summary>
		/// 终端编号
		/// </summary>
		long TerminalNo; /*终端编号*/
		/// <summary>
		/// 操作员编号
		/// </summary>
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 4)]
		string OperCode; /*操作员编号*/
		/// <summary>
		/// 事件代码
		/// </summary>
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 3)]
		string TranCode; /*事件代码*/
		/// <summary>
		/// 流水状态
		/// </summary>
		string JnlStatus; /*流水状态*/
		/// <summary>
		/// 流水发生日期时间YYYYMMDDHH24MISS
		/// </summary>
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 15)]
		string JnDateTime; /*流水发生日期时间YYYYMMDDHH24MISS*/
		/// <summary>
		/// 流水生效日期YYYYMMDD
		/// </summary>
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 9)]
		string EffectDate; /*流水生效日期YYYYMMDD*/
		/// <summary>
		/// 余额*
		/// </summary>
		long Balance; /*余额*/
		/// <summary>
		/// 交易额
		/// </summary>
		long TranAmt; /*交易额*/
		/// <summary>
		/// 财务类型
		/// </summary>
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 4)]
		string ConsumeType; /*财务类型*/
		/// <summary>
		/// 备注
		/// </summary>
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 129)]
		string Resume; /*备注*/
	}
	#endregion

	#region 模糊查询时返回的开通流水的文件结构:OpenFlowFile
	/*模糊查询时返回的开通流水的文件结构*/

	//	typedef struct
	//			{
	//				long Account; /*帐号*/
	//				long SysCode; /*系统代码*/
	//				char OpenDate[9]; /*开通日期*/
	//				char OperCode[4]; /*操作员代码*/
	//			}OpenFlowFile

	/// <summary>
	/// 模糊查询时返回的开通流水的文件结构
	/// </summary>
	[ StructLayout( LayoutKind.Sequential, CharSet=CharSet.Ansi ,Pack=1)]
	public struct OpenFlowFile
	{
		/// <summary>
		/// 帐号
		/// </summary>
		long Account; /*帐号*/
		/// <summary>
		/// 统代码
		/// </summary>
		long SysCode; /*系统代码*/
		/// <summary>
		/// 开通日期
		/// </summary>
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 9)]
		string OpenDate; /*开通日期*/
		/// <summary>
		/// 操作员代码
		/// </summary>
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 4)]
		string OperCode; /*操作员代码*/
	}
	#endregion

	#region 模糊查询时返回的帐户信息的文件结构:HazyInqAccMsg
	//	typedef struct
	//			{
	//				char      		Name[31]; 			/*姓名*/
	//				char      		SexNo[2]; 			/*性别*/
	//				char			DeptCode[19];		/*部门代码*/
	//				unsigned int	CardNo; 			/*卡号*/
	//				unsigned int	AccountNo; 			/*帐号*/
	//				char			StudentCode[21];	/*学号*/
	//				char			IDCard[21]; 		/*身份证号*/
	//				char			PID[3];				/*身份代码*/
	//				char			IDNo[13]; 			/*身份序号*/
	//				int			    Balance; 			/*现余额*/
	//				char			ExpireDate[7];		/*账户截止日期*/
	//				unsigned int	SubSeq;			    /*补助戳*/
	//				char                        Flag[16]; 
	//			}HazyInqAccMsg;
	/// <summary>
	/// 模糊查询时返回的帐户信息的文件结构
	/// </summary>
	[ StructLayout( LayoutKind.Sequential, CharSet=CharSet.Ansi ,Pack=1)]
	public struct HazyInqAccMsg
	{
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 31)]
		public string      		Name; 								/*姓名*/
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 2)]
		public string      		SexNo; 								/*性别*/
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 19)]
		public string				DeptCode;							/*部门代码*/
		public uint				CardNo; 							/*卡号*/
		public uint				AccountNo; 							/*帐号*/
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 21)]
		public string				StudentCode;						/*学号*/
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 21)]
		public string				IDCard; 							/*身份证号*/
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 3)]
		public string				PID;								/*身份代码*/
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 13)]
		public string				IDNo; 								/*身份序号*/
		public int			    	Balance; 							/*现余额*/
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 7)]
		public string				ExpireDate;							/*账户截止日期*/
		public uint				SubSeq;										/*补助戳*/
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 16)]
		public string				Flag;										/*补助戳*/
	}
	#endregion
}