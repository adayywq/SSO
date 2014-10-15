using System;
using System.Data;
using System.Text;
namespace Mdl.Entity
{
	/// <summary>
	/// 类DEVELOPER。
	/// </summary>
	[Serializable]
	public partial class DEVELOPER
	{
		public DEVELOPER()
		{}
		#region Model
		private int _devid;
		private string _acccode;
		private string _devname;
		private string _devcode;
		private string _linkman;
		private string _mobile;
		private string _email;
		private string _siteurl;
		private string _callbackurl;
		private string _logouturl;
		private string _memo;
		private int _creator;
		private DateTime? _creatdate;
		private int _modifier;
		private DateTime? _modifydate;
		private int _state;
		/// <summary>
		/// 开发ID
		/// </summary>
		public int DEVID
		{
			set{ _devid=value;}
			get{return _devid;}
		}
		/// <summary>
		/// 授权编号
		/// </summary>
		public string ACCCODE
		{
			set{ _acccode=value;}
			get{return _acccode;}
		}
		/// <summary>
		/// 开发者
		/// </summary>
		public string DEVNAME
		{
			set{ _devname=value;}
			get{return _devname;}
		}
		/// <summary>
		/// 开发编号
		/// </summary>
		public string DEVCODE
		{
			set{ _devcode=value;}
			get{return _devcode;}
		}
		/// <summary>
		/// 联系人
		/// </summary>
		public string LINKMAN
		{
			set{ _linkman=value;}
			get{return _linkman;}
		}
		/// <summary>
		/// 联系电话
		/// </summary>
		public string MOBILE
		{
			set{ _mobile=value;}
			get{return _mobile;}
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
		/// 网站地址
		/// </summary>
		public string SITEURL
		{
			set{ _siteurl=value;}
			get{return _siteurl;}
		}
		/// <summary>
		/// 回调地址
		/// </summary>
		public string CALLBACKURL
		{
			set{ _callbackurl=value;}
			get{return _callbackurl;}
		}
		/// <summary>
		/// 退出地址
		/// </summary>
		public string LOGOUTURL
		{
			set{ _logouturl=value;}
			get{return _logouturl;}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string MEMO
		{
			set{ _memo=value;}
			get{return _memo;}
		}
		/// <summary>
		/// 创建人
		/// </summary>
		public int CREATOR
		{
			set{ _creator=value;}
			get{return _creator;}
		}
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime? CREATDATE
		{
			set{ _creatdate=value;}
			get{return _creatdate;}
		}
		/// <summary>
		/// 修改人
		/// </summary>
		public int MODIFIER
		{
			set{ _modifier=value;}
			get{return _modifier;}
		}
		/// <summary>
		/// 修改时间
		/// </summary>
		public DateTime? MODIFYDATE
		{
			set{ _modifydate=value;}
			get{return _modifydate;}
		}
		/// <summary>
		/// 状态
		/// </summary>
		public int STATE
		{
			set{ _state=value;}
			get{return _state;}
		}
		#endregion Model
	}
}

