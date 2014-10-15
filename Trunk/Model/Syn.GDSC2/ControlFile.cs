/*
 * ******************************************************************************************************************
 * 一卡通2 控制文件结构
 * 
 * 
 * 作者：赵立仁
 * 最后更新日期：2004/04/23
 ********************************************************************************************************************
 */

using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Collections;

namespace Syn.GDSC2
{
	/*下面是控制文件的结构*/
	/*前8 + 30 * sizeof (struct tabCtrlHead)为文件头*/
	/* 前8字节为SYNTONG */
	/*表顺序如下：*/
	/*ID_CONTROL.BIN 文件头
	 位置 	      头结构 	      对应数据表名称
	================================================
	  #0	TControlHeadRec0	部门组织表	8 + 1 
	  #1	TControlHeadRec1	身份代码表
	  #2	TControlHeadRec2	证件类型表
	  #3	TControlHeadRec3	民族表
	  #4	TControlHeadRec4	国籍表
	  #5	TControlHeadRec5	政治面貌
	  #6	TControlHeadRec6	校/厂区代码表
	  #7	TControlHeadRec7    宿舍（苑/栋）
	  #8	TControlHeadRec8    文化程度
	  #9    TControlHeadRec9    卡样管理表
	  #10   TControlHeadRec10   技术职称
	  #11   TControlHeadRec11   行政职务
	================================================*/

	/// <summary>
	/// 控制文件
	/// </summary>
	public class ControlFile
	{
		#region 常量
		private 	 const string		 CONTROL_STR				=		"SYNTONG"	;/* 控制文件标识 */
		private		 const int   		 TABLECOUNT					=		30			;/* 结构个数 */
		private		 const int			 HEADER_STRUCT_LENGTH	    =		8			;/* 头结构大小*/
		private		 const int			 HEADERSIZE					=       22			;/* 文件头结构大小*/
		private		 const int			 DEPARTMENT_SIZE			=		100			;/* 部门结构大小*/
		private		 const int			 PID_SIZE				    =       73			;/*	身份机构大小*/	
		private		 const int			 AREA_SIZE					=		56			;/* 校区结构大小*/																			  
		private		 char[]				 TO_BE_TRIM					=		{'\0',' '}	;/* 需要被TRIM掉得字符*/
		#endregion

		private t_department[] _Department;
		public t_department[] Department
		{
			get{return this._Department;}
		}

		private t_pid[] _Pid;
		public t_pid[] Pid
		{
			get{return this._Pid;}
		}
		
		private t_area[] _Area;
		public t_area[] Area
		{
			get{return this._Area;}
		}

		
		public string GetSexString(string sexcode)
		{
			string result = "";
			switch(sexcode)
			{
				case "1":
					result = "男";
					break;
				case "2":
					result = "女";
					break;
				default:
					result = "未定义";
					break;
			}
			return result;
		}

		public string GetSexCode(string sexstring)
		{
			string result = "";
			switch(sexstring)
			{
				case "男":
					result = "1";
					break;
				case "女":
					result = "2";
					break;
				default:
					result = "0";
					break;
			}
			return result;
		}


		public string GetPidString(string pidcode)
		{
			for(int i = 0;i< this.Pid.Length;i++)
			{
				if(Pid[i].pid_code == pidcode.Trim(TO_BE_TRIM))
				{
					return Pid[i].pid_name.Trim(TO_BE_TRIM);
				}
			}
			return "";
		}

		public string GetAreaString(string areacode)
		{
			for(int i = 0;i < this.Area.Length;i++)
			{
				if(Area[i].area_code == areacode.Trim(TO_BE_TRIM))
				{
					return Area[i].area_name.Trim(TO_BE_TRIM);
				}
			}
			return "";
		}

		
		public string GetDepartmentString(string deptcode)
		{
			for(int i = 0;i < this.Department.Length;i++)
			{
				if(Department[i].dept_code == deptcode.Trim(TO_BE_TRIM))
				{
					return Department[i].dept_name.Trim(TO_BE_TRIM);
				}
			}
			return "";
		}

		public string GetPidCode(string pidstring)
		{
			for(int i = 0;i< this.Pid.Length;i++)
			{
				if(Pid[i].pid_name == pidstring.Trim(TO_BE_TRIM))
				{
					return Pid[i].pid_code.Trim(TO_BE_TRIM);
				}
			}
			return "";
		}


		public string GetAreaAndDepartmentString(string departcode)
		{
			string result = "";
			string areaCode = "";
			string departstring = "";
			ArrayList deptCodes = new ArrayList();
			bool bRET = ParseDepartmentCode(departcode,ref areaCode,ref deptCodes);
			if(bRET)			
			{
				result = this.GetAreaString(areaCode);
				foreach(string s in deptCodes)
				{
					departstring += s;
					result += "/" + this.GetDepartmentString(departstring);
				}
			}
			return result;
		}

		private bool ParseDepartmentCode(string departcode,ref string areaCode,ref ArrayList deptCodes)
		{
			deptCodes.Clear();
			int codeLength = departcode.Length;
			if(((codeLength % 3) == 0) && (codeLength > 0))
			{
				areaCode = departcode.Substring(0,3);
				for(int i = 1;i < codeLength / 3;i++)
				{
					string s = departcode.Substring(i * 3,3);
					deptCodes.Add(s);
				}
				return true;
			}
			else
			{
				return false;
			}
		}

		private tabCtrlHead _PidHeader = new tabCtrlHead();

		/// <summary>
		/// 下载控制文件
		/// </summary>
		/// <returns>TRUE/FALSE</returns>
		public bool DownLoad()
		{			
			int iRET = AIOAPI.TA_DownControlFile(1);
			System.Console.WriteLine("读取控制文件返回：{0}",iRET);
			return (iRET > 0);			
		}

		public bool Read()
		{		
			//if (FileSize <= 0) return false;
			string filename = System.Environment.CurrentDirectory + @"\ControlFile\Control.bin";
			if(File.Exists(filename))
			{				
				
				System.IO.FileStream fs = null;
				try
				{
					fs = new FileStream(filename,FileMode.Open,FileAccess.Read,FileShare.ReadWrite);
					byte[] Control = new byte[fs.Length];
					int ret = fs.Read(Control,0,(int)fs.Length);
					if(ret != fs.Length) return false;

					
					#region 读取文件标记
					byte[] _Tag = new byte[8];
					Array.Copy(Control,0,_Tag,0,8);
					string Tag = System.Text.ASCIIEncoding.ASCII.GetString(_Tag).Trim('\0');					
					if( Tag != ControlFile.CONTROL_STR)			//判断文件标记是否是"SYNTONG"
						return false;
					#endregion		
						
					#region 读取控制文件头结构
					tabCtrlHead[] CtrlHeads = new tabCtrlHead[ControlFile.TABLECOUNT];
					byte[] tempCtrlHeads = new byte[ControlFile.TABLECOUNT * ControlFile.HEADER_STRUCT_LENGTH];
					
					Array.Copy(Control,8,tempCtrlHeads,0,ControlFile.TABLECOUNT * ControlFile.HEADER_STRUCT_LENGTH);
                    
					for(int i = 0;i < ControlFile.TABLECOUNT;i++)
					{
						CtrlHeads[i].structlength = BitConverter.ToUInt16(tempCtrlHeads,i * ControlFile.HEADER_STRUCT_LENGTH);
						CtrlHeads[i].count = BitConverter.ToUInt16(tempCtrlHeads,i * ControlFile.HEADER_STRUCT_LENGTH + 2);
						CtrlHeads[i].offset = BitConverter.ToUInt32(tempCtrlHeads,i * ControlFile.HEADER_STRUCT_LENGTH + 4);
					}
					#endregion

					#region 读取部门结构
					this._Department = new t_department[CtrlHeads[0].count];
					byte[] tempDepartment = new byte[CtrlHeads[0].count * ControlFile.DEPARTMENT_SIZE];
					Array.Copy(Control,CtrlHeads[0].offset,tempDepartment,0,CtrlHeads[0].count * ControlFile.DEPARTMENT_SIZE);

					for(int j = 0;j < CtrlHeads[0].count;j++)
					{
						int StartIndex =  j * ControlFile.DEPARTMENT_SIZE;						
						_Department[j].dept_code = System.Text.Encoding.Default.GetString(tempDepartment,StartIndex,16).Trim(TO_BE_TRIM);		//部门代码 						
						_Department[j].dept_name = System.Text.Encoding.Default.GetString(tempDepartment,StartIndex + 16,41).Trim(TO_BE_TRIM);		//部门名称						
						_Department[j].area_code = System.Text.Encoding.Default.GetString(tempDepartment,StartIndex + 16 + 41,4).Trim(TO_BE_TRIM);			//所属校/厂区
						_Department[j].dept_level = tempDepartment[StartIndex + 16 + 41 + 4];			//部门级别 						
						_Department[j].dept_easycode = System.Text.Encoding.Default.GetString(tempDepartment,StartIndex + 16 + 41 + 4 + 1,11).Trim(TO_BE_TRIM);	//助记码
						_Department[j].dept_type = tempDepartment[StartIndex + 16 + 41 + 4 + 1 + 11];				//部门类型 
						_Department[j].dept_flag = tempDepartment[StartIndex + 16 + 41 + 4 + 1 + 11 + 1];				//启用状态
						_Department[j].dept_desc = System.Text.Encoding.Default.GetString(tempDepartment,StartIndex + 16 + 41 + 4 + 1 + 11 + 1 + 1,21).Trim(TO_BE_TRIM);		//描述信息
						_Department[j].dept_opercode = System.Text.Encoding.Default.GetString(tempDepartment,StartIndex + 16 + 41 + 4 + 1 + 11 + 1 + 1 + 21,4).Trim(TO_BE_TRIM);		//操作员代码 
					}
					#endregion

					#region 读取身份结构
					this._Pid = new t_pid[CtrlHeads[1].count];
					byte[] tempPid = new byte[CtrlHeads[1].count * ControlFile.PID_SIZE];
					Array.Copy(Control,CtrlHeads[1].offset,tempPid,0,CtrlHeads[1].count * ControlFile.PID_SIZE);

					for(int i = 0;i < CtrlHeads[1].count;i++)
					{
//						char		pid_code[2+1];
//						char		class_code;
//						char		area_code[3+1];
//						char		pid_name[40+1];
//						char		pid_easycode[10+1];
//						char		pid_flag;
//						char		pid_opercode[3+1];
//						char        pid_cardtypeno[3+1];
//						int         pid_zkl;

						int StartIndex = i * ControlFile.PID_SIZE;
						this._Pid[i].pid_code = System.Text.Encoding.Default.GetString(tempPid,StartIndex,3).Trim(TO_BE_TRIM);
						this._Pid[i].class_code = tempPid[StartIndex + 3];
						this._Pid[i].area_code = System.Text.Encoding.Default.GetString(tempPid,StartIndex + 3 + 1,4).Trim(TO_BE_TRIM);
						this._Pid[i].pid_name = System.Text.Encoding.Default.GetString(tempPid,StartIndex + 3 + 1 + 4,41 ).Trim(TO_BE_TRIM);
						this._Pid[i].pid_easycode = System.Text.Encoding.Default.GetString(tempPid,StartIndex + 3 + 1 + 4 + 41,11 ).Trim(TO_BE_TRIM);
						this._Pid[i].pid_flag = tempPid[StartIndex + 3 + 1 + 4 + 41 + 11];
						this._Pid[i].pid_opercode = System.Text.Encoding.Default.GetString(tempPid,StartIndex + 3 + 1 + 4 + 41 + 11 + 1,4 ).Trim(TO_BE_TRIM);
						this._Pid[i].pid_cardtypeno = System.Text.Encoding.Default.GetString(tempPid,StartIndex + 3 + 1 + 4 + 41 + 11 + 1 + 4,4 ).Trim(TO_BE_TRIM);
						this._Pid[i].pid_zkl = BitConverter.ToInt32(tempPid,StartIndex + 3 + 1 + 4 + 41 + 11 + 1 + 4 + 4);
					}
					#endregion

					#region 读取校区结构
					this._Area = new t_area[CtrlHeads[6].count];
					byte[] tempArea = new byte[CtrlHeads[6].count * ControlFile.AREA_SIZE];
					Array.Copy(Control,CtrlHeads[6].offset,tempArea,0,CtrlHeads[6].count * ControlFile.AREA_SIZE);

					for(int i = 0;i < CtrlHeads[6].count;i++)
					{
						int StartIndex = i * ControlFile.AREA_SIZE;
						this._Area[i].area_code = System.Text.Encoding.Default.GetString(tempArea,StartIndex,4).Trim(TO_BE_TRIM);						
						this._Area[i].area_name = System.Text.Encoding.Default.GetString(tempArea,StartIndex + 4,41).Trim(TO_BE_TRIM);
						this._Area[i].area_easycode = System.Text.Encoding.Default.GetString(tempArea,StartIndex + 4 + 41,11 ).Trim(TO_BE_TRIM);
					}

					#endregion
					return true;
				}
				catch(Exception ex)
				{
					System.Console.WriteLine("读取控制文件失败：{0}",ex.ToString());
					return false;
				}
				finally
				{
					if (fs != null)
					fs.Close();
				}
			}
			else
				return false;
		}
        
	}

	/// <summary>
	/// 变量长度
	/// FROM 叶性名 varlen.h
	/// </summary>
	public class VarLength
	{
		public const int SchCodeLen		=2;
		public const int IdentityIdLen		=12;
		public const int NameLen		=30;
		public const int PidLen			=2;
		public const int DeptCodeLen		=18;
		public const int PasswordLen		=16;
		public const int BankAccLen		=20;
		public const int IdentityLen		=20;
		public const int CertifyTypeLen		=3;
		public const int SnoLen			=20;
		public const int PhoneLen		=20;
		public const int DateLen		=8;
		public const int TimeLen		=6;
		public const int CardTypeLen		=3;
		public const int DatetimeLen		=14;
		public const int DigitalSignLen		=8;
		public const int SubjectCodeLen		=8;
		public const int MercNameLen		=40;
		public const int LinkmanLen		=20;
		public const int MercCodeLen		=10;
		public const int AddressLen		=40;
		public const int ZipLen			=6;
		public const int OperCodeLen		=3;
		public const int SchNameLen		=40;
		public const int SubjectNameLen		=18;
		public const int ShortNameLen		=12;
		public const int FeeCodeLen		=3;
		public const int FeeNameLen		=20;
		public const int TranCodeLen		=2;
		public const int CodeLen		=3;
		public const int AssistantCodeLen	=10;
		public const int SysNameLen		=40;
		public const int SysTypeLen		=3;
		public const int CompCodeLen		=3;
		public const int IpLen			=15;
		public const int OperNameLen		=20;
		public const int OperLevelLen		=100;
		public const int BardianLen		=40;
		public const int TranNameLen		=40;
		public const int MsgCodeLen		=4;
		public const int ConsumeTypeLen		=3;
		public const int SubTypeLen		=3;
	}


	#region 控制文件结构
	//控制文件头
	[ StructLayout( LayoutKind.Sequential, CharSet=CharSet.Ansi ,Pack = 1)]
	public struct tabCtrlHead
	{
		public ushort structlength;      //每条记录长度
		public ushort count;				//记录数量
		public long offset;			//从文件头开始，该表第一条记录偏移量
	}
	#endregion

	#region 部门组织结构
	/// <summary>
	/// 部门组织结构
	/// </summary>
	///[ StructLayout( LayoutKind.Sequential, CharSet=CharSet.Ansi ,Pack = 1)]
	public  struct	t_department
	{
		public string		dept_code;			//部门代码  
		public string		dept_name;			//部门名称
		public string		area_code;			//所属校/厂区
		public byte			dept_level;			//部门级别   
		public string		dept_easycode;		//助记码
		public byte			dept_type;			//部门类型 
		public byte			dept_flag;			//启用状态
		public string		dept_desc;			//描述信息
		public string		dept_opercode;		//操作员代码  
	}
	#endregion

	#region 身份类型
	/// <summary>
	/// 身份类型
	/// </summary>
	///[ StructLayout( LayoutKind.Sequential, CharSet=CharSet.Ansi ,Pack =1)]
	public struct	t_pid
	{
		//[MarshalAs( UnmanagedType.ByValTStr, SizeConst=3)]
		public string		pid_code;			//身份代码
		public byte		class_code;				//持卡人类型（1学生，2教工，3校外人员）
		//[MarshalAs( UnmanagedType.ByValTStr, SizeConst=4)]
		public string		area_code;			//校/厂区代码
		//[MarshalAs( UnmanagedType.ByValTStr, SizeConst=41)]
		public string		pid_name;			//身份名称
		//[MarshalAs( UnmanagedType.ByValTStr, SizeConst=11)]
		public string		pid_easycode;		//助记码
		public byte		pid_flag;				//启用状态
		//[MarshalAs( UnmanagedType.ByValTStr, SizeConst=4)]
		public string		pid_opercode;		//操作员代码
		//[MarshalAs( UnmanagedType.ByValTStr, SizeConst=4)]
		public string      pid_cardtypeno;	//卡样式号
		public int         pid_zkl;				//折扣率
	};
	#endregion

	#region 证件类型表
	/// <summary>
	/// 证件类型表
	/// </summary>
	[ StructLayout( LayoutKind.Sequential, CharSet=CharSet.Ansi ,Pack = 1)]
	public struct	t_idtype
	{
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst=4)]
		string		idtype_code;
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst=17)]
		string		idtype_name;
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst=11)]
		string		idtype_easycode;
	}
	#endregion

	#region 民族表
	/// <summary>
	/// 民族表
	/// </summary>
	[ StructLayout( LayoutKind.Sequential, CharSet=CharSet.Ansi ,Pack = 1)]
	public struct	t_people
	{
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst=3)]
		string		people_code;
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst=11)]
		string		people_name;
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst=11)]
		string		people_easycode;
		byte		people_flag;
	}
	#endregion

	#region 国籍表
	/// <summary>
	/// 国籍表
	/// </summary>
	[ StructLayout( LayoutKind.Sequential, CharSet=CharSet.Ansi ,Pack = 1)]
	public struct	t_nation
	{
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst=3)]
		string		nation_code;
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst=11)]
		string		nation_name;
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst=11)]
		string		nation_easycode;
		byte		nation_flag;
	}
	#endregion

	#region 政治面貌
	/// <summary>
	/// 政治面貌
	/// </summary>
	[ StructLayout( LayoutKind.Sequential, CharSet=CharSet.Ansi ,Pack = 1)]
	public struct	t_zzmm
	{
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst=3)]
		string		zzmm_code;
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst=31)]
		string		zzmm_name;
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst=11)]
		string		zzmm_easycode;
		byte		zzmm_flag;
	}
	#endregion

	#region 校/厂区代码表
	/// <summary>
	/// 校/厂区代码表
	/// </summary>
	//[ StructLayout( LayoutKind.Sequential, CharSet=CharSet.Ansi ,Pack = 1)]
	public struct	t_area
	{
		public string		area_code;
		public string		area_name;
		public string		area_easycode;
	}
	#endregion

	/// <summary>
	/// 宿舍（苑/栋）
	/// </summary>
	[ StructLayout( LayoutKind.Sequential, CharSet=CharSet.Ansi ,Pack = 1)]
	public struct t_ss_yuan 
	{
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst=4)]
		string  code;
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst=21)]
		string  name;
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst=11)]
		string  easycode;
		byte  type;
	}

	[ StructLayout( LayoutKind.Sequential, CharSet=CharSet.Ansi ,Pack = 1)]
	public struct t_whcd 
	{
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst=3)]
		string  code;
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst=21)]
		string  name;
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst=11)]
		string  easycode;
		byte  useflag;
	}

	[ StructLayout( LayoutKind.Sequential, CharSet=CharSet.Ansi ,Pack = 1)]
	public struct t_cardtype 
	{
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst=3)]
		string cardtypeno;
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst=21)]
		string cardtypename;
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst=101)]
		string cardcontent;
		byte useflag;
	}

	[ StructLayout( LayoutKind.Sequential, CharSet=CharSet.Ansi ,Pack = 1)]
	public struct t_jszc 
	{
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst=4)]
		string code;
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst=31)]
		string name;
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst=11)]
		string easycode;
		byte useflag;
	}

	[ StructLayout( LayoutKind.Sequential, CharSet=CharSet.Ansi ,Pack = 1)]
	public struct t_xzzw 
	{
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst=4)]
		string code;
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst=31)]
		string name;
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst=11)]
		string easycode;
		byte useflag;
	}

	[ StructLayout( LayoutKind.Sequential, CharSet=CharSet.Ansi ,Pack = 1)]
	public struct t_schcode
	{
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst=VarLength.SchCodeLen+1)]
		string			SchCode;
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst=VarLength.SchNameLen+1)]
		string            Name;
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst=81)]
		string            EnglishName;
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst=81)]
		string            Address;
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst=41)]
		string            Remark;
	}

	[ StructLayout( LayoutKind.Sequential, CharSet=CharSet.Ansi ,Pack = 1)]
	public struct t_subject
	{
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst=VarLength.SubjectCodeLen+1)]
		string			  SubjectCode;
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst=VarLength.SubjectNameLen+1)]
		string            SubjectName;
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst=VarLength.ShortNameLen+1)]
		string            ShortName;
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst=VarLength.SubjectCodeLen+1)]
		string            SuperSubject;
		int               Sequence;
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst=11)]
		string            flag;
	}

	[ StructLayout( LayoutKind.Sequential, CharSet=CharSet.Ansi ,Pack = 1)]
	public struct t_fee
	{
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst=VarLength.FeeCodeLen+1)]
		char			FeeCode;
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst=VarLength.FeeNameLen+1)]
		char            FeeName;
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst=VarLength.SchCodeLen+1)]
		char            SchCode;
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst=VarLength.TranCodeLen+1)]
		char            TranCode;
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst=VarLength.PidLen+1)]
		char            PID;
		int             FeeAmt;
		int             FeeAmtRate;
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst=3)]
		string            FeeFlag;
	}

	[ StructLayout( LayoutKind.Sequential, CharSet=CharSet.Ansi ,Pack = 1)]
	public struct t_configinfo
	{
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst=VarLength.CodeLen+1)]
		string			Code;
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst=41)]
		string            Name;
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst=VarLength.AssistantCodeLen+1)]
		string            AssistantCode;
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst=3)]
		string            Type;
	}

	[ StructLayout( LayoutKind.Sequential, CharSet=CharSet.Ansi ,Pack = 1)]
	public struct t_branch
	{
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst=4)]
		string			DeptCode;
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst=VarLength.AssistantCodeLen+1)]
		string            AssistantCode;
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst=31)]
		string            Name;
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst=4)]
		string            SuDeptCode;
		byte            DeptLevel;
		byte            DeptType;
	}

	[ StructLayout( LayoutKind.Sequential, CharSet=CharSet.Ansi ,Pack = 1)]
	struct t_cszd
	{
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst=4)]
		char			Code;
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst=21)]
		char            Name;
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst=21)]
		char            Value;
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst=41)]
		char            Remark;
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst=3)]
		char            Flag;
	}

	[ StructLayout( LayoutKind.Sequential, CharSet=CharSet.Ansi ,Pack = 1)]
	struct t_trcd
	{
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst=VarLength.TranCodeLen+1)]
		char		TranCode;
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst=VarLength.TranNameLen+1)]
		char		TranName;
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst=11)]
		char		Flag;
	}

	[ StructLayout( LayoutKind.Sequential, CharSet=CharSet.Ansi ,Pack = 1)]
	struct t_message
	{
		short		MsgCode;
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst=251)]
		char		Describe;
	}
}
