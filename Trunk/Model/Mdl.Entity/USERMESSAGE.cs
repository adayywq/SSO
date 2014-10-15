using System;
using System.Data;
using System.Text;
namespace Mdl.Entity
{
	/// <summary>
	/// 类USERMESSAGE。
	/// </summary>
	[Serializable]
	public partial class USERMESSAGE
	{
		public USERMESSAGE()
		{}
		#region Model
		private int _umid;
		private int _msgid;
		private string _recipient;
		private string _rtype;
		private DateTime? _readdate;
		private DateTime? _deldate;
		private int _state;
		/// <summary>
		/// 主键ID
		/// </summary>
		public int UMID
		{
			set{ _umid=value;}
			get{return _umid;}
		}
		/// <summary>
		/// MsgID
		/// </summary>
		public int MSGID
		{
			set{ _msgid=value;}
			get{return _msgid;}
		}
		/// <summary>
		/// Recipient
		/// </summary>
		public string RECIPIENT
		{
			set{ _recipient=value;}
			get{return _recipient;}
		}
		/// <summary>
		/// Rtype
		/// </summary>
		public string RTYPE
		{
			set{ _rtype=value;}
			get{return _rtype;}
		}
		/// <summary>
		/// 阅读时间
		/// </summary>
		public DateTime? READDATE
		{
			set{ _readdate=value;}
			get{return _readdate;}
		}
		/// <summary>
		/// 删除时间
		/// </summary>
		public DateTime? DELDATE
		{
			set{ _deldate=value;}
			get{return _deldate;}
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

