using System;
using System.Data;
using System.Text;
namespace Mdl.Entity
{
	/// <summary>
	/// 类APPLICATION。
	/// </summary>
	[Serializable]
	public partial class APPLICATION
	{
		public APPLICATION()
		{}
		#region Model
		private int _appid;
		private string _appname;
		private string _appcode;
        private int _pid;
        private int _ordernum;
		/// <summary>
		/// 应用ID
		/// </summary>
        public int APPID
		{
			set{ _appid=value;}
			get{return _appid;}
		}
		/// <summary>
		/// 应用名称
		/// </summary>
		public string APPNAME
		{
			set{ _appname=value;}
			get{return _appname;}
		}
		/// <summary>
		/// 应用编号
		/// </summary>
		public string APPCODE
		{
			set{ _appcode=value;}
			get{return _appcode;}
		}
		/// <summary>
		/// 父ID
		/// </summary>
		public int PID
		{
			set{ _pid=value;}
			get{return _pid;}
		}
		/// <summary>
		/// 排序编号
		/// </summary>
		public int ORDERNUM
		{
			set{ _ordernum=value;}
			get{return _ordernum;}
		}
		#endregion Model
	}
}

