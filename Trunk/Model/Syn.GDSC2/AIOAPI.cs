/*
 * ******************************************************************************************************************
 * 第三方接口API声明文件
 * AIO_API.DLL 版本 2.1.0.1 最后更新日期：2004/03/26
 * 
 * 作者：赵立仁
 * 最后更新日期：2004/04/29
 ********************************************************************************************************************
 */


using System;
using System.Runtime.InteropServices;

namespace Syn.GDSC2
{
	public class AIOAPI
	{		
		/*
		**函数名称：TA_Init
		**函数功能：设置动态库以第三方方式运行
		**参数说明：IP-SIOS的IP地址, port-sios 的端口号 
		**			SysCode-系统代码,ProxyOffline-代理服务是否脱机
		**			MaxJnl-最大流水号
		**返回值：TRUE/FALSE
		**创建时间：2003-08-21
		**修改时间：2004-03-10
		**修改内容：去掉参数CardKey-卡片密钥
		**			改名TA_Init
		
		EXTC BOOL  WINAPI TA_Init(char *IP , short port , unsigned short SysCode, 
																	   unsigned short TerminalNo, bool *ProxyOffline, ULONG *MaxJnl);
		*/
		/// <summary>
		/// 设置动态库以第三方方式运行
		/// </summary>
		/// <param name="IP">SIOS的IP地址</param>
		/// <param name="port">sios 的端口号</param>
		/// <param name="SysCode">SysCode-系统代码</param>
		/// <param name="TerminalNo">终端号</param>
		/// <param name="ProxyOffline">ProxyOffline-代理服务是否脱机</param>
		/// <param name="MaxJnl">MaxJnl-最大流水号</param>
		/// <returns>TRUE/FALSE</returns>
		[DllImport("AIO_API.dll")]
		public static extern bool TA_Init(string IP,short port,ushort SysCode,ushort TerminalNo,out bool ProxyOffline,out uint MaxJnl);

		
		/*
		**函数名称：TA_CRInit
		**函数功能：初始化读卡器
		**参数列表：CardReaderType－输入参数，读卡器类型，0－usb读卡器，1－串口读卡器。
		**			port－输入参数，端口号，
		**			Baud_Rate－输入参数，波特率，
		**返回值:	见返回值列表
		**创建时间：2004-03-10
		
		EXTC int _stdcall TA_CRInit(char CardReaderType,int port,long Baud_Rate);
		*/
		/// <summary>
		/// 初始化读卡器
		/// </summary>
		/// <param name="CardReaderType">输入参数，读卡器类型，0－usb读卡器，1－串口读卡器。</param>
		/// <param name="port">输入参数，端口号</param>
		/// <param name="Baud_Rate">输入参数，波特率 建议使用19200</param>
		/// <returns>见返回值列表</returns>
		[DllImport("AIO_API.dll")]
		public static extern int TA_CRInit(byte CardReaderType,int port,long Baud_Rate);


		/*
		**函数名称：TA_CRClose
		**函数功能：关闭读卡器
		**参数列表：void
		**返回值:	true/false
		**创建时间：2004-03-10
		
		EXTC BOOL _stdcall TA_CRClose(void);
		*/
		/// <summary>
		/// 关闭读卡器
		/// </summary>
		/// <returns>true/false</returns>
		[DllImport("AIO_API.dll")]
		public static extern bool TA_CRClose();


		/*
		**函数名称：TA_FastReadCard
		**函数功能：快速读卡号，适用于检验是否有卡靠近读卡反应区
		**			如果只需要快速读取卡号，则不需要初始化动态库(TA_Init())
		**参数列表：CardNo－输出参数，从卡片中读出的卡号
		**返回值:	见返回值列表Errormsg.h
		**创建时间：2004-03-10
		
		EXTC int _stdcall TA_FastGetCardNo(unsigned int *CardNo);
		*/

		/// <summary>
		/// 快速读卡号，适用于检验是否有卡靠近读卡反应区
		/// 如果只需要快速读取卡号，则不需要初始化动态库(TA_Init())
		/// </summary>
		/// <param name="CardNo">输出参数，从卡片中读出的卡号</param>
		/// <returns>见返回值列表</returns>
		[DllImport("AIO_API.dll")]
		public static extern int TA_FastGetCardNo(out uint CardNo);


		/*
		**函数名称：TA_CRBeep
		**函数功能：读卡器峰鸣
		**参数列表：BeepSecond－输入参数，峰鸣的毫秒数。
		**返回值:	见返回值列表Errormsg.h
		**创建时间：2004-03-10
		
		EXTC int _stdcall TA_CRBeep(unsigned int BeepMSecond);
		*/

		/// <summary>
		/// 读卡器峰鸣
		/// </summary>
		/// <param name="BeepMSecond">输入参数，峰鸣的毫秒数。</param>
		/// <returns>见返回值列表</returns>
		[DllImport("AIO_API.dll")]
		public static extern int TA_CRBeep(uint BeepMSecond);


		/*
		**函数名称：TA_ReadCardSimple
		**函数功能：简单读卡信息，不检验白名单。
		**参数列表：pAccMsg－输出参数，从卡片中读出的卡片信息。
		**返回值:	见返回值列表Errormsg.h
		**创建时间：2004-03-10
		
		EXTC int _stdcall TA_ReadCardSimple(AccountMsg * pAccMsg);
		*/
		/// <summary>
		/// 简单读卡信息，不检验白名单
		/// </summary>
		/// <param name="pAccMsg">输出参数，从卡片中读出的卡片信息。</param>
		/// <returns>见返回值列表</returns>
		[DllImport("AIO_API.dll")]
		public static extern int TA_ReadCardSimple(out AccountMsg pAccMsg);
		
		/*
		**函数名称：TA_CheckWL
		**函数功能：第三方根据帐号和卡号检查白名单。
		**参数列表：AccountNo－输入参数，需要验证的帐号
		**			CardNo－输入参数，卡号
		**			CheckID－输入参数，是否检验身份开通关闭状态
		**返回值:	见返回值列表Errormsg.h
		**创建时间：2004-03-10
		
		EXTC int _stdcall TA_CheckWL (unsigned int AccountNo , unsigned int CardNo , bool CheckID=true);
		*/
		/// <summary>
		/// 第三方根据帐号和卡号检查白名单。
		/// </summary>
		/// <param name="AccountNo">输入参数，需要验证的帐号</param>
		/// <param name="CardNo">输入参数，卡号</param>
		/// <param name="CheckID">输入参数，是否检验身份开通关闭状态</param>
		/// <returns>见返回值列表</returns>
		[DllImport("AIO_API.dll")]
		public static extern int TA_CheckWL(uint AccountNo , uint CardNo , bool CheckID);

		/*
		**函数名称：TA_ReadCard
		**函数功能：读卡信息。读出卡信息并检验白名单，判断卡片的有效性。
		**参数列表：pAccMsg－帐户信息包,如果需要请求补助时必须填写pAccMsg->TerminalNo
		**			CheckID－输入参数，是否检验身份开通关闭状态
		**			CheckSub－输入参数，是否提取补助
		**返回值:	见返回值列表Errormsg.h
		**创建时间：2004-03-10
		
		EXTC int _stdcall TA_ReadCard(AccountMsg *pAccMsg,bool CheckID=true ,bool CheckSub= false);
		*/
		/// <summary>
		/// 读卡信息。读出卡信息并检验白名单，判断卡片的有效性。
		/// </summary>
		/// <param name="pAccMsg">帐户信息包,如果需要请求补助时必须填写pAccMsg->TerminalNo</param>
		/// <param name="CheckID">输入参数，是否检验身份开通关闭状态</param>
		/// <param name="CheckSub">输入参数，是否提取补助</param>
		/// <returns>见返回值列表</returns>
		[DllImport("AIO_API.dll")]
		public static extern int TA_ReadCard(out AccountMsg pAccMsg,bool CheckID ,bool CheckSub);

		
		/*
		**函数名称：TA_CardOpen
		**函数功能：开通
		**参数列表：pCardOper－第三方操作的整体数据包，在这里需要填写的参数是帐号和经手人。
		**			pCardOper->RetCode是后台交易的返回值
		**			TimeOut － 输入参数，超时时间（秒），缺省值为10秒。
		**返回值:	见返回值列表Errormsg.h
		**创建时间：2004-03-11
		
		EXTC int _stdcall TA_CardOpen(CardOperating *pCardOper, short TimeOut = 10);
		*/
		/// <summary>
		/// 开通
		/// </summary>
		/// <param name="pCardOper">第三方操作的整体数据包，在这里需要填写的参数是帐号和经手人。
		/// pCardOper->RetCode是后台交易的返回值</param>
		/// <param name="TimeOut">输入参数，超时时间（秒），缺省值为10秒。</param>
		/// <returns>见返回值列表</returns>
		[DllImport("AIO_API.dll")]
		public static extern int TA_CardOpen(ref CardOperating pCardOper, short TimeOut);

		/*
		**函数名称：TA_CardClose
		**函数功能：关闭
		**参数列表：pCardOper－第三方操作的整体数据包，在这里需要填写的参数是帐号和操作员。
		**			pCardOper->RetCode是后台交易的返回值
		**			TimeOut － 输入参数，超时时间（秒），缺省值为10秒。
		**返回值:	见返回值列表Errormsg.h
		**创建时间：2004-03-11
		
		EXTC int _stdcall TA_CardClose(CardOperating *pCardOper, short TimeOut=10);
		*/
		/// <summary>
		/// 关闭
		/// </summary>
		/// <param name="pCardOper">pCardOper－第三方操作的整体数据包，在这里需要填写的参数是帐号和操作员。
		///			pCardOper->RetCode是后台交易的返回值</param>
		/// <param name="TimeOut"></param>
		/// <returns></returns>
		[DllImport("AIO_API.dll")]
		public static extern int TA_CardClose(ref CardOperating pCardOper, short TimeOut);
			
		
		/*
		**函数名称：TA_CardLost
		**函数功能：挂失
		**参数列表：pCardOper－第三方操作的整体数据包，在这里需要填写的参数是帐号和操作员。
		**			pCardOper->RetCode是后台交易的返回值
		**			TimeOut － 输入参数，超时时间（秒），缺省值为10秒。
		**返回值:	见返回值列表Errormsg.h
		**创建时间：2004-03-11
		
		EXTC int _stdcall TA_CardLost(CardOperating *pCardOper, short TimeOut=10);
		*/
		/// <summary>
		/// 挂失
		/// </summary>
		/// <param name="pCardOper">pCardOper－第三方操作的整体数据包，在这里需要填写的参数是帐号和操作员。
		///			pCardOper->RetCode是后台交易的返回值</param>
		/// <param name="TimeOut">输入参数，超时时间（秒），缺省值为10秒。</param>
		/// <returns>见返回值列表</returns>
		[DllImport("AIO_API.dll")]
		public static extern int TA_CardLost(ref CardOperating pCardOper, short TimeOut);

		/*
		**函数名称：TA_Consume
		**函数功能：卡片消费(可以脱机)
		**参数列表：pCardCons－第三方操作的整体数据包,要求必须填入卡号
		**			pCardCons->RetCode是后台交易的返回值
		**			IsVerfy－是否验证累计消费额，如果超过累计消费额，则需要输入消费密码。
		**			TimeOut － 输入参数，超时时间（秒），缺省值为10秒。
		**返回值:	见返回值列表Errormsg.h
		**创建时间：2004-03-11
		
		EXTC int _stdcall TA_Consume(CardConsume *pCardCons, bool IsVerfy, short TimeOut=10);
		*/
		/// <summary>
		/// 卡片消费(可以脱机)
		/// </summary>
		/// <param name="pCardCons">第三方操作的整体数据包,要求必须填入卡号
		/// pCardCons->RetCode是后台交易的返回值</param>
		/// <param name="IsVerfy">是否验证累计消费额，如果超过累计消费额，则需要输入消费密码。</param>
		/// <param name="TimeOut">输入参数，超时时间（秒），缺省值为10秒。</param>
		/// <returns>见返回值列表</returns>
		[DllImport("AIO_API.dll")]
		public static extern int TA_Consume(ref CardConsume pCardCons, bool IsVerfy, short TimeOut);


		/*
		**函数名称：TA_Refund
		**函数功能：卡片退费(联机交易)
		**参数列表：pCardCons－第三方操作的整体数据包,要求必须填入卡号
		**			pCardCons->RetCode是后台交易的返回值
		**			TimeOut － 输入参数，超时时间（秒），缺省值为10秒。
		**返回值:	见返回值列表Errormsg.h
		**创建时间：2004-03-15
		
		EXTC int _stdcall TA_Refund(CardConsume *pCardCons , short TimeOut=10);
		*/
		/// <summary>
		/// 卡片退费(联机交易)
		/// </summary>
		/// <param name="pCardCons">第三方操作的整体数据包,要求必须填入卡号
		/// pCardCons->RetCode是后台交易的返回值</param>
		/// <param name="TimeOut">输入参数，超时时间（秒），缺省值为10秒。</param>
		/// <returns>见返回值列表</returns>
		[DllImport("AIO_API.dll")]
		public static extern int TA_Refund(ref CardConsume pCardCons , short TimeOut);

		/*
		**函数名称：TA_Charge
		**函数功能：卡片收费
		**参数列表：pCardCharg－第三方操作的整体数据包,要求必须填入卡号
		**			pCardCharg->RetCode是后台交易的返回值
		**			IsVerfy－是否验证累计消费额，如果超过累计消费额，则需要输入消费密码。
		**			TimeOut － 输入参数，超时时间（秒），缺省值为10秒。
		**返回值:	见返回值列表Errormsg.h
		**创建时间：2004-03-11
		
		EXTC int _stdcall TA_Charge(CardCharge *pCardCharg, bool IsVerfy, short TimeOut=10);
		*/
		/// <summary>
		/// 卡片收费
		/// </summary>
		/// <param name="pCardCharg">第三方操作的整体数据包,要求必须填入卡号
		///			pCardCharg->RetCode是后台交易的返回值</param>
		/// <param name="IsVerfy">是否验证累计消费额，如果超过累计消费额，则需要输入消费密码。</param>
		/// <param name="TimeOut">输入参数，超时时间（秒），缺省值为10秒。</param>
		/// <returns>见返回值列表</returns>
		[DllImport("AIO_API.dll")]
		public static extern int TA_Charge(ref CardCharge pCardCharg, bool IsVerfy, short TimeOut);

		/*
		**函数名称：TA_InqAcc
		**函数功能：根据帐号/卡号/学工号/证件号精确查询帐户信息
		**参数列表：pAccMsg－第三方帐户信息的整体数据包
		**			(需要填写卡号或者是帐号或者是学工号或者是证件号)。
		**			输出参数，pAccMsg ->RetCode为后台处理的返回值
		**			TimeOut － 输入参数，超时时间（秒），缺省值为10秒。
		**返回值:	见返回值列表Errormsg.h
		**创建时间：2004-03-15
		
		EXTC int _stdcall TA_InqAcc(AccountMsg * pAccMsg, short TimeOut = 10);
		*/
		/// <summary>
		/// 根据帐号/卡号/学工号/证件号精确查询帐户信息
		/// </summary>
		/// <param name="pAccMsg">pAccMsg－第三方帐户信息的整体数据包
		///			(需要填写卡号或者是帐号或者是学工号或者是证件号)。
		///			输出参数，pAccMsg ->RetCode为后台处理的返回值</param>
		/// <param name="TimeOut">输入参数，超时时间（秒），缺省值为10秒。</param>
		/// <returns>见返回值列表</returns>
		[DllImport("AIO_API.dll")]
		public static extern int TA_InqAcc(ref AccountMsg pAccMsg, short TimeOut);

		/*
		**函数名称：TA_HazyInqAcc
		**函数功能：按部门代码模糊查询持卡人信息。
		**参数列表：pAccMsg－输入参数，查询条件(可以根据Name,DeptCode,SexNo,StudentNo,PID查询)
		**			查询的文件放到RecvTemp目录下，文件名写入到FileName中
		**			FileName－输出参数，返回的文件名称，最少64个字节
		**			RecNum-输出参数，查询到的记录数目
		**			TimeOut － 输入参数，超时时间（秒），缺省值为10秒。
		**返回值:	见返回值列表Errormsg.h
		**创建时间：2004-03-15
		
		EXTC int _stdcall TA_HazyInqAcc(AccountMsg *pAccMsg, int *RecNum , char *FileName,short TimeOut = 10);
		*/
		/// <summary>
		/// 按部门代码模糊查询持卡人信息。
		/// </summary>
		/// <param name="pAccMsg">pAccMsg－输入参数，查询条件(可以根据Name,DeptCode,SexNo,StudentNo,PID查询)
		///			查询的文件放到RecvTemp目录下，文件名写入到FileName中</param>
		/// <param name="RecNum"></param>
		/// <param name="FileName">FileName－输出参数，返回的文件名称，最少64个字节</param>
		/// <param name="TimeOut"></param>
		/// <returns></returns>
		[DllImport("AIO_API.dll")]
		public static extern int TA_HazyInqAcc(ref AccountMsg pAccMsg, out int RecNum , byte[] FileName,short TimeOut);
		//public static extern int TA_HazyInqAcc(byte[] accmsg, out int RecNum , byte[] FileName,short TimeOut);

		/*
		**函数名称：TA_InqTranFlow
		**函数功能：交易流水查询。
		**参数列表：pInqTranFlow－输入参数,可以根据持卡人帐号、商户帐号、
		**			终端号码组合查询当天的或者历史的交易流水。
		**			查询的文件放到RecvTemp目录下，文件名写入到pInqTranFlow->FileName中。	
		**			pInqTranFlow->RecNum-输出参数，查询到的记录数目
		**			TimeOut － 输入参数，超时时间（秒），缺省值为10秒。
		**返回值:	见返回值列表Errormsg.h
		**创建时间：2004-03-15
		
		EXTC int _stdcall TA_InqTranFlow(InqTranFlow *pInqTranFlow, short TimeOut = 10);
		*/
		/// <summary>
		/// 交易流水查询
		/// </summary>
		/// <param name="pInqTranFlow">pInqTranFlow－输入参数,可以根据持卡人帐号、商户帐号、
		///			终端号码组合查询当天的或者历史的交易流水。
		///			查询的文件放到RecvTemp目录下，文件名写入到pInqTranFlow->FileName中。	
		///			pInqTranFlow->RecNum-输出参数，查询到的记录数目</param>
		/// <param name="TimeOut">输入参数，超时时间（秒），缺省值为10秒。</param>
		/// <returns>见返回值列表</returns>
		[DllImport("AIO_API.dll")]
		public static extern int TA_InqTranFlow(ref InqTranFlow pInqTranFlow, short TimeOut);

		/*
		**函数名称：TA_InqOpenFlow
		**函数功能：开通流水查询。
		**参数列表：pInqOpenFlow－输入参数,可以根据持卡人帐号、商户帐号、
		**			终端号码组合查询当天的或者历史的交易流水。
		**			查询的文件放到RecvTemp目录下，文件名写入到pInqOpenFlow->FileName中。	
		**			pInqOpenFlow->RecNum-输出参数，查询到的记录数目
		**			TimeOut － 输入参数，超时时间（秒），缺省值为10秒。
		**返回值:	见返回值列表Errormsg.h
		**创建时间：2004-03-15
		
		EXTC int _stdcall TA_InqOpenFlow(InqOpenFlow *pInqOpenFlow, short TimeOut = 10);
		*/
		/// <summary>
		/// 开通流水查询
		/// </summary>
		/// <param name="pInqOpenFlow">pInqOpenFlow－输入参数,可以根据持卡人帐号、商户帐号、
		///			终端号码组合查询当天的或者历史的交易流水。
		///			查询的文件放到RecvTemp目录下，文件名写入到pInqOpenFlow->FileName中。	
		///			pInqOpenFlow->RecNum-输出参数，查询到的记录数目</param>
		/// <param name="TimeOut">输入参数，超时时间（秒），缺省值为10秒</param>
		/// <returns>见返回值列表</returns>
		[DllImport("AIO_API.dll")]
		public static extern int TA_InqOpenFlow(ref InqOpenFlow pInqOpenFlow, short TimeOut);

		/*函数名:TA_DownControlFile
		*功能:下载控制文件，控制文件的目录是\ControlFile
		*输入参数:timeout－超时时间
		*输出参数:无
		*返回值:大于0表示下载的文件的大小，小于0表示失败
		
		EXTC int _stdcall TA_DownControlFile(short timeOut=10);
		*/
		/// <summary>
		/// 下载控制文件，控制文件的目录是\ControlFile
		/// </summary>
		/// <param name="timeOut">超时时间</param>
		/// <returns>大于0表示下载的文件的大小，小于0表示失败</returns>
		[DllImport("AIO_API.dll")]
		public static extern int TA_DownControlFile(short timeOut);

		/*
		**函数名称：TA_DownPhotoFile
		**函数功能：下载相片文件
		**输入参数：IDNo-身份序号，PhotoFn-相片文件的名称
		**输出参数: 无
		**返回值:	大于0表示上送的文件的大小，小于0表示失败
		**创建时间：2004-03-19
		
		EXTC int  _stdcall TA_DownPhotoFile(char * IDNo, char *PhotoFn, short TimeOut=10);
		*/
		/// <summary>
		/// 下载相片文件
		/// </summary>
		/// <param name="IDNo">身份序号</param>
		/// <param name="PhotoFn">相片文件的名称</param>
		/// <param name="TimeOut">超时时间</param>
		/// <returns>大于0表示上送的文件的大小，小于0表示失败</returns>
		[DllImport("AIO_API.dll")]
		public static extern int TA_DownPhotoFile(byte[] IDNo, byte[] PhotoFn, short TimeOut);

        /*
		**函数名称：TA_UpJournal
		**函数功能：上传流水帐(可以脱机)
		**参数列表：pCardCons－第三方操作的整体数据包,要求必须填入卡号
		**			pCardCons->RetCode是后台交易的返回值
		**			TimeOut － 输入参数，超时时间（秒），缺省值为10秒。
		**返回值:	见返回值列表Errormsg.h
		**创建时间：2004-04-26
		
		EXTC int _stdcall TA_UpJournal(CardConsume *pCardCons, short TimeOut)
		*/
        [DllImport("AIO_API.dll")]
        public static extern int TA_UpJournal(ref CardConsume pCardCons, short TimeOut);

        /*
        **函数名称：TA_GetCK
        **函数功能：游泳馆系统取得卡片密钥
        **参数列表：pwd-取得密钥的密码
        **			CardKey-卡片密钥 8卡密钥2厂商4院校
        **返回值:	见返回值列表Errormsg.h
        **创建时间：2004-04-26
		
        EXTC BOOL _stdcall TA_GetCK(char *pwd , char * CardKey)  密码"swimming"					
        */
        [DllImport("AIO_API.dll")]
        public static extern bool TA_GetCK(byte[] pwd, byte[] CardKey);

        /*
        **功能：一卡通加密函数
        **	   参数说明：
        **iBuf：要加密的缓冲区
        **		 iLen：加密缓冲区的长度（长度1－8）
        **oBuf：加密后的缓冲区（长度大于iBuf）
        **返回值：布尔值
		
        EXTC BOOL WINAPI G_PW_Encrypt( char *iBuf, int iLen, char *oBuf );
        */
        [DllImport("AIO_API.dll")]
        public static extern int G_PW_Encrypt(byte[] inBuf, int iLen, byte[] outBuf);

        /*
        **功能：一卡通解密函数
        **参数说明：
        **iBuf：要解密的缓冲区
        **iLen：解密缓冲区的长度
        **oBuf：解密后的缓冲区
        **返回值：布尔值
		
        EXTC BOOL WINAPI G_PW_Decrypt( char *iBuf, int iLen, char *oBuf );
        */
        [DllImport("AIO_API.dll")]
        public static extern int G_PW_Decrypt(byte[] inBuf, int iLen, byte[] outBuf);

        /*
         *函数名:TA_CheckQpwd
         *功能:根据账号验证查询密码
         *输入参数:accountno-持卡人账号,qpwd:持卡人查询密码
         *输出参数:无
         *返回值:0-验证成功 / 1-验证失败 / -1-查询交易失败
         *备注:为武汉理工网站增加
         *创建时间:2004-10-14
         
        EXTC int _stdcall TA_CheckQpwd(unsigned int accountno , char * qpwd)
        */
        [DllImport("AIO_API.dll")]
        public static extern int TA_CheckQpwd(uint accountNo, byte[] qpwd);

        /*
        **函数名称：EC_GetEnCardCfg
        **函数功能：取得加密卡中的全局配置,为白栋照片系统添加，不读取共享内存，每次都读加密卡
        *输出参数:pec－全局配置结构指针
        *输入参数:无
        *返回值:RET_OK/ERR_READ_ENCARD
        *备注：2007-12-18
        **修改内容：去掉参数CardKey-卡片密钥		
        EXTC int WINAPI EC_GetEnCardCfg( ENCARD_CONFIG_ALL *pec)
        */
        [DllImport("AIO_API.dll")]
        public static extern int EC_GetEnCardCfg(out ENCARD_CONFIG_ALL ENCARD_CONFIG_ALL);

        //EXTC int _stdcall TA_GetNodeByType( int systypecode, unsigned short* pnodeid );
        /*
        **函数名称：TA_GetNodeByType
        **函数功能：第三方从综合前置机下载全部可用的节点信息，并查询某个类型的第一个节点号
        **参数列表：systypecode-[in]系统类型代码；pnodeid-[OUT]查询到节点号；如果节点号为0，表示没有查到
        **返回值:	int
        **创建时间：2012-04-17
        */
        [DllImport("AIO_API.dll")]
        public static extern int TA_GetNodeByType(int SysTypeCode, out ushort pNodeID);

	}
    #region 查询加密卡内部信息
    //typedef struct
    //{
    //    char 	ip_p[20];	//钱包
    //    char	ip_f[20];	//前置机
    //    char	ip_i[20];	//身份
    //    USHORT	port_p;
    //    USHORT	port_f;
    //    USHORT	port_i;
    //    USHORT	port_pftp;	//钱包FTP端口
    //    USHORT	port_iftp;	//前置机FTP端口
    //    USHORT	NodeID;
    //    UCHAR	CardKey[8];
    //    UCHAR	CmpMark[2];
    //    UCHAR	SchoolCode[4];
    //    UCHAR	StaticKey[16];
    //    UCHAR	UserName[20];		//用户名称
    //    UCHAR	BankName[20];		//银行名称
    //    ULONG	UserCapacity;		//用户(帐号)容量
    //    UCHAR	SysType;			//处理机类型
    //}ENCARD_CONFIG_ALL;
    /// <summary>
    /// 查询加密卡内部信息
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
    //    public ushort port_pftp;          /*钱包FTP端口*/
    //    public ushort port_iftp;        /*前置机FTP端口*/
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
    //    public string UserName;   							/*用户名称*/
    //    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
    //    public string BankName; 								/*银行名称*/

    //    public uint UserCapacity;								/*用户(帐号)容量*/
    //    public uint SysType; 								/*处理机类型*/
    //}
    #endregion
}
