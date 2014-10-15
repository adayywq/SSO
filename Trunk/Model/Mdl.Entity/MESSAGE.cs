using System;
using System.Data;
using System.Text;
namespace Mdl.Entity
{
	/// <summary>
	/// 类MESSAGE。
	/// </summary>
	[Serializable]
	public partial class MESSAGE
	{
		public MESSAGE()
		{}
		#region Model
		private int _msgid;
		private string _title;
		private string _content;
		private string _sender;
		private int _system;
		private string _creator;
		private DateTime? _createdate;
		private string _modifier;
		private DateTime? _modifydate;
		private int _state;
		/// <summary>
		/// MsgID
		/// </summary>
		public int MSGID
		{
			set{ _msgid=value;}
			get{return _msgid;}
		}
		/// <summary>
		/// 发布标题
		/// </summary>
		public string TITLE
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
		/// 发布内容
		/// </summary>
		public string CONTENT
		{
			set{ _content=value;}
			get{return _content;}
		}
		/// <summary>
		/// 发送者
		/// </summary>
		public string SENDER
		{
			set{ _sender=value;}
			get{return _sender;}
		}
		/// <summary>
		/// 发送系统
		/// </summary>
		public int SYSTEM
		{
			set{ _system=value;}
			get{return _system;}
		}
		/// <summary>
		/// 发布用户
		/// </summary>
		public string CREATOR
		{
			set{ _creator=value;}
			get{return _creator;}
		}
		/// <summary>
		/// 发布时间
		/// </summary>
		public DateTime? CREATEDATE
		{
			set{ _createdate=value;}
			get{return _createdate;}
		}
		/// <summary>
		/// 修改人
		/// </summary>
		public string MODIFIER
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

