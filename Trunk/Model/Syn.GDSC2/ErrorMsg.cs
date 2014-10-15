using System;

namespace Syn.GDSC2
{
	/// <summary>
	/// ErrorMsg 的摘要说明。
	/// </summary>
	public class ErrorMsg
	{
		#region 错误代码
		public const 	int	ERR_EXCEPTION		=	-9999;		//截获异常
		public const 	int	ERR_OK				=	0;			//交易成功
		public const 	int	ERR_VER				=	-1;			//版本不符
		public const 	int	ERR_RETCODE			=	-2;			//返回码不对
		public const 	int	ERR_LENGTH			=	-3;			//数据长度不对
		public const 	int	ERR_FILENAME		=	-4;			//文件名非法
		public const 	int	ERR_FILESTAT		=	-5;			//文件访问状态非法
		public const 	int	ERR_FAIL			=	-6;			//操作失败	
                
		
		/*sios error : ERR_SIOS_ */
		public const 	int	ERR_SIOS_NOREC		=	-100;		//指定的记录不存
		public const 	int	ERR_SIOS_DOWNLOAD	=	-101;		//下载文件失败
		
		
		/*net error : ERR_NET_ */
		public const  	int	ERR_NET_CONNECT		=	-200;		//网络连接不通
		public const  	int	ERR_NET_SEND		=	-201;		//数据发送出错
		public const  	int	ERR_NET_RECV		=	-202;		//数据接收出错
		public const  	int	ERR_NET_RECVFILE	=	-203;		//接收文件出错
		public const  	int	ERR_NET_SENDFILE	=	-204;		//发送文件出错
                
                
		/*trans errot : ERR_TRN_ */
		public const  	int	ERR_TRN_SUBCODE		=	-300;		//无效的子系统代码
		public const  	int	ERR_TRN_STATION		=	-301;		//无效的站点号
		
		
		/*EN_CARD error : ERR_ENCARD_ */
		public const  	int	ERR_ENCARD_RHEAD	=	-500;		//读加密卡头错
		public const  	int	ERR_ENCARD_CONFIG	=	-501;		//读配置区出错
		public const  	int	ERR_ENCARD_RKEY		=	-502;		//读密钥错
		public const  	int	ERR_ENCARD_OPEN		=	-503;		//打开加密卡失败
		
		 
		/*DLL error : ERR_DLL_ from -1000 */
		public const  	int	ERR_DLL_SIOS		=	-1001;		//SIOS没有正常运行
		public const  	int	ERR_DLL_DSQL		=	-1002;		//DSQL操作错误
		public const  	int	ERR_DLL_BUF_MIN		=	-1003;		//分配的缓冲区太小，不能拷贝
		public const  	int	ERR_DLL_UNPACK		=	-1004;		//解包出错
		public const  	int	ERR_DLL_REDO		=	-1005;		//重做业务2003-09-05
		public const  	int	ERR_DLL_NOPHOTO		=	-1006;		//没有相片文件
		public const  	int	ERR_DLL_NOFILE		=	-1007;		//指定文件不存在
                
                 
		/*定义升级返回值 from 1100*/
		public const  	int	ERR_FILEEXIST		=	-1100;		//文件已经存在
		public const  	int	ERR_REFUSE			=	-1101;		//操作被拒绝
		public const  	int	ERR_NO_FILE			=	-1102;		//没有文件
		public const  	int	ERR_DEL_FAIL		=	-1103;		//删除文件失败
		public const  	int	ERR_COMM_FAIL		=	-1104;		//通讯失败
		 
                 
		/*第三方返回值定义 from 1200*/
		public const  	int	ERR_TA_TRANAMT		=	-1200;		//交易额错误
		public const  	int	ERR_TA_NOT_INIT		=	-1201;		//第三方API没有初始化
		public const  	int	ERR_TA_CARDREADER	=	-1202;		//读卡器错误
		public const  	int	ERR_TA_READCARD		=	-1203;		//读卡失败
		public const  	int	ERR_TA_WRITECARD	=	-1204;		//写卡失败
		public const  	int	ERR_TA_LIMIT_FUNC	=	-1205;		//函数调用功能限制
		public const  	int	ERR_TA_CARDTYPE		=	-1206;		//不是消费卡
		public const  	int	ERR_TA_SNO			=	-1207;		//非本院校卡
		public const  	int	ERR_TA_EXPIRECARD	=	-1208;		//过期卡
		public const  	int	ERR_TA_FAIL_CHGUT	=	-1209;		//修改用卡次数失败
		public const  	int	ERR_TA_NOT_SAMECARD	=	-1210;		//写卡时卡号不符
		public const  	int	ERR_TA_WRONG_PWD	=	-1211;		//卡消费时输入密码错误
		public const  	int	ERR_TA_LOW_BALAN	=	-1212;		//卡内余额不足
		public const  	int	ERR_TA_EXCEED_QUOTA	=	-1213;		//超过消费限额
		public const  	int	ERR_TA_LOST_CARD	=	-1214;		//挂失卡
		public const  	int	ERR_TA_FREEZE_CARD	=	-1215;		//冻结卡
		public const  	int	ERR_TA_CARDNO		=	-1216;		//卡号帐号不符
		public const  	int	ERR_TA_ID_CLOSE		=	-1217;		//身份关闭
		public const  	int	ERR_TA_CR_DLL		=	-1218;		//加载读卡器动态链接库失败
		public const  	int	ERR_TA_CR_INIT		=	-1219;		//读卡器初始化失败
		public const  	int	ERR_TA_PARA			=	-1220;		//参数错误
		public const  	int	ERR_TA_NOREC		=	-1221;		//没有这个帐户
		public const  	int	ERR_TA_SUB_SUCC		=	-1222;		//补助成功,也是正确的返回信息
		public const  	int	ERR_TA_SUB_FAIL		=	-1223;		//补助失败,也是正确的返回信息
		public const  	int	ERR_TA_INITED		=	-1224;		//读卡器已经初始化，请关闭
		#endregion
		private ErrorMsg(){}	//防止类被实例化

		private static string MessageHeader = "";
		
		/// <summary>
		/// 根据一卡通第三方接口函数返回值或者信息
		/// </summary>
		/// <param name="RET">第三方接口返回值</param>
		/// <returns>字符串：返回值信息</returns>
		public  static string GetErrorMessage(int RET)
		{
			string result = "一卡通返回:";
			switch(RET)
			{
				case	ERR_EXCEPTION		:	result		=	"截获异常";       break;
				case	ERR_VER				:	result		=	"版本不符";       break;
				case	ERR_RETCODE			:	result		=	"返回码不对";       break;
				case	ERR_LENGTH			:	result		=	"数据长度不对";       break;
				case	ERR_FILENAME		:	result		=	"文件名非法";       break;
				case	ERR_FILESTAT		:	result		=	"文件访问状态非法";       break;
				case	ERR_FAIL			:	result		=	"操作失败";       break;
				case	ERR_SIOS_NOREC		:	result		=	"指定的记录不存";       break;
				case	ERR_SIOS_DOWNLOAD	:	result		=	"下载文件失败";       break;
				case	ERR_NET_CONNECT		:	result		=	"网络连接不通";       break;
				case	ERR_NET_SEND		:	result		=	"数据发送出错";       break;
				case	ERR_NET_RECV		:	result		=	"数据接收出错";       break;
				case	ERR_NET_RECVFILE	:	result		=	"接收文件出错";       break;
				case	ERR_NET_SENDFILE	:	result		=	"发送文件出错";       break;
				case	ERR_TRN_SUBCODE		:	result		=	"无效的子系统代码";       break;
				case	ERR_TRN_STATION		:	result		=	"无效的站点号";       break;
				case	ERR_ENCARD_RHEAD	:	result		=	"读加密卡头错";       break;
				case	ERR_ENCARD_CONFIG	:	result		=	"读配置区出错";       break;
				case	ERR_ENCARD_RKEY		:	result		=	"读密钥错";       break;
				case	ERR_ENCARD_OPEN		:	result		=	"打开加密卡失败";       break;
				case	ERR_DLL_SIOS		:	result		=	"SIOS没有正常运行";       break;
				case	ERR_DLL_DSQL		:	result		=	"DSQL操作错误";       break;
				case	ERR_DLL_BUF_MIN		:	result		=	"分配的缓冲区太小，不能拷贝";       break;
				case	ERR_DLL_UNPACK		:	result		=	"解包出错";       break;
				case	ERR_DLL_REDO		:	result		=	"重做业务2003-09-05";       break;
				case	ERR_DLL_NOPHOTO		:	result		=	"没有相片文件";       break;
				case	ERR_DLL_NOFILE		:	result		=	"指定文件不存在";       break;
				case	ERR_FILEEXIST		:	result		=	"文件已经存在";       break;
				case	ERR_REFUSE			:	result		=	"操作被拒绝";       break;
				case	ERR_NO_FILE			:	result		=	"没有文件";       break;
				case	ERR_DEL_FAIL		:	result		=	"删除文件失败";       break;
				case	ERR_COMM_FAIL		:	result		=	"通讯失败";       break;
				case	ERR_TA_TRANAMT		:	result		=	"交易额错误";       break;
				case	ERR_TA_NOT_INIT		:	result		=	"第三方API没有初始化";       break;
				case	ERR_TA_CARDREADER	:	result		=	"读卡器错误";       break;
				case	ERR_TA_READCARD		:	result		=	"读卡失败";       break;
				case	ERR_TA_WRITECARD	:	result		=	"写卡失败";       break;
				case	ERR_TA_LIMIT_FUNC	:	result		=	"函数调用功能限制";       break;
				case	ERR_TA_CARDTYPE		:	result		=	"不是消费卡";       break;
				case	ERR_TA_SNO			:	result		=	"非本院校卡";       break;
				case	ERR_TA_EXPIRECARD	:	result		=	"过期卡";       break;
				case	ERR_TA_FAIL_CHGUT	:	result		=	"修改用卡次数失败";       break;
				case	ERR_TA_NOT_SAMECARD	:	result		=	"写卡时卡号不符";       break;
				case	ERR_TA_WRONG_PWD	:	result		=	"卡消费时输入密码错误";       break;
				case	ERR_TA_LOW_BALAN	:	result		=	"卡内余额不足";       break;
				case	ERR_TA_EXCEED_QUOTA	:	result		=	"超过消费限额";       break;
				case	ERR_TA_LOST_CARD	:	result		=	"挂失卡";       break;
				case	ERR_TA_FREEZE_CARD	:	result		=	"冻结卡";       break;
				case	ERR_TA_CARDNO		:	result		=	"卡号帐号不符";       break;
				case	ERR_TA_ID_CLOSE		:	result		=	"身份关闭";       break;
				case	ERR_TA_CR_DLL		:	result		=	"加载读卡器动态链接库失败";       break;
				case	ERR_TA_CR_INIT		:	result		=	"读卡器初始化失败";       break;
				case	ERR_TA_PARA			:	result		=	"参数错误";       break;
				case	ERR_TA_NOREC		:	result		=	"没有这个帐户";       break;
				case	ERR_TA_SUB_SUCC		:	result		=	"补助成功,也是正确的返回信息";       break;
				case	ERR_TA_SUB_FAIL		:	result		=	"补助失败,也是正确的返回信息";       break;
				case	ERR_TA_INITED		:	result		=	"读卡器已经初始化，请关闭";       break;
				default:	result = ":未定义的返回值"; break;
			}
			return MessageHeader + RET.ToString() + ":" + result;
		}
		
	}
}
