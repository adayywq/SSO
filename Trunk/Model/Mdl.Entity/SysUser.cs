using System;
using System.Data;
using System.Text;
namespace Mdl.Entity
{
	/// <summary>
	/// 类USERINFO。
	/// </summary>
	[Serializable]
	public partial class SysUser
	{
		public SysUser()
		{}
		#region Model
		private int _userid;
		private int _uniteid;
		private string _logintype;
		private string _loginid;
		private string _name;
		private string _email;
		private string _mobile;
		private string _pwd;
		private string _initident;
		private string _datasource;
		private DateTime? _createdate;
		private int _state;
		/// <summary>
		/// 用户ID
		/// </summary>
		public int USERID
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 联合ID
		/// </summary>
		public int UNITEID
		{
			set{ _uniteid=value;}
			get{return _uniteid;}
		}
		/// <summary>
		/// 登录类型
		/// </summary>
		public string LOGINTYPE
		{
			set{ _logintype=value;}
			get{return _logintype;}
		}
		/// <summary>
		/// 登录ID
		/// </summary>
		public string LOGINID
		{
			set{ _loginid=value;}
			get{return _loginid;}
		}
		/// <summary>
		/// 姓名
		/// </summary>
		public string NAME
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// 电子邮箱
		/// </summary>
		public string EMAIL
		{
			set{ _email=value;}
			get{return _email;}
		}
		/// <summary>
		/// 手机
		/// </summary>
		public string MOBILE
		{
			set{ _mobile=value;}
			get{return _mobile;}
		}
		/// <summary>
		/// 密码
		/// </summary>
		public string PWD
		{
			set{ _pwd=value;}
			get{return _pwd;}
		}
		/// <summary>
		/// 默认身份
		/// </summary>
		public string INITIDENT
		{
			set{ _initident=value;}
			get{return _initident;}
		}
		/// <summary>
		/// 数据来源
		/// </summary>
		public string DATASOURCE
		{
			set{ _datasource=value;}
			get{return _datasource;}
		}
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime? CREATEDATE
		{
			set{ _createdate=value;}
			get{return _createdate;}
		}
		/// <summary>
		/// 状态值
		/// </summary>
		public int STATE
		{
			set{ _state=value;}
			get{return _state;}
		}
		#endregion Model		
	}
}

