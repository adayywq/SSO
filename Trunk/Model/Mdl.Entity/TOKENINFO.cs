using System;
using System.Data;
using System.Text;
namespace Mdl.Entity
{
	/// <summary>
	/// 类TOKENINFO。
	/// </summary>
	[Serializable]
	public partial class TOKENINFO
	{
		public TOKENINFO()
		{}
		#region Model
		private int _tokenid;
		private string _token;
		private string _loginid;
		private string _logintype;
		private string _usesys;
		private string _datasource;
		private string _clientip;
		private string _clientmac;
		private DateTime? _lastupdate;
		/// <summary>
		/// SKID
		/// </summary>
		public int TOKENID
		{
			set{ _tokenid=value;}
			get{return _tokenid;}
		}
		/// <summary>
		/// SessionKey
		/// </summary>
		public string TOKEN
		{
			set{ _token=value;}
			get{return _token;}
		}
		/// <summary>
		/// 登录名
		/// </summary>
		public string LOGINID
		{
			set{ _loginid=value;}
			get{return _loginid;}
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
		/// 使用方
		/// </summary>
		public string USESYS
		{
			set{ _usesys=value;}
			get{return _usesys;}
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
		/// ClientIP
		/// </summary>
		public string CLIENTIP
		{
			set{ _clientip=value;}
			get{return _clientip;}
		}
		/// <summary>
		/// ClientMac
		/// </summary>
		public string CLIENTMAC
		{
			set{ _clientmac=value;}
			get{return _clientmac;}
		}
		/// <summary>
		/// LastUpdate
		/// </summary>
		public DateTime? LASTUPDATE
		{
			set{ _lastupdate=value;}
			get{return _lastupdate;}
		}
		#endregion Model
	}
}

