using System;
using System.Data;
using System.Text;
namespace Mdl.Entity
{
	/// <summary>
	/// 类LOG。
	/// </summary>
	[Serializable]
	public partial class LOG
	{
		public LOG()
		{}
		#region Model
		private int _logid;
		private int _userid;
		private string _username;
		private string _typea;
		private string _typeb;
		private string _action;
		private string _content;
		private string _clientip;
		private DateTime? _createdate;
		/// <summary>
		/// 主键ID
		/// </summary>
		public int LOGID
		{
			set{ _logid=value;}
			get{return _logid;}
		}
		/// <summary>
		/// 用户ID
		/// </summary>
		public int USERID
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 用户名
		/// </summary>
		public string USERNAME
		{
			set{ _username=value;}
			get{return _username;}
		}
		/// <summary>
		/// 操作类别A
		/// </summary>
		public string TYPEA
		{
			set{ _typea=value;}
			get{return _typea;}
		}
		/// <summary>
		/// 操作类别B
		/// </summary>
		public string TYPEB
		{
			set{ _typeb=value;}
			get{return _typeb;}
		}
		/// <summary>
		/// 动作
		/// </summary>
		public string ACTION
		{
			set{ _action=value;}
			get{return _action;}
		}
		/// <summary>
		/// 操作内容
		/// </summary>
		public string CONTENT
		{
			set{ _content=value;}
			get{return _content;}
		}
		/// <summary>
		/// 记录IP
		/// </summary>
		public string CLIENTIP
		{
			set{ _clientip=value;}
			get{return _clientip;}
		}
		/// <summary>
		/// 操作时间
		/// </summary>
		public DateTime? CREATEDATE
		{
			set{ _createdate=value;}
			get{return _createdate;}
		}
		#endregion Model
	}
}

