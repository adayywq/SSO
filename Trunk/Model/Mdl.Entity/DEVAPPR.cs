using System;
using System.Data;
using System.Text;
namespace Mdl.Entity
{
	/// <summary>
	/// 类DEVAPPR。
	/// </summary>
	[Serializable]
	public partial class DEVAPPR
	{
		public DEVAPPR()
		{}
		#region Model
		private int _daid;
		private int _devid;
		private int _appid;
		/// <summary>
		/// ID
		/// </summary>
		public int DAID
		{
			set{ _daid=value;}
			get{return _daid;}
		}
		/// <summary>
		/// 开发者ID
		/// </summary>
		public int DEVID
		{
			set{ _devid=value;}
			get{return _devid;}
		}
		/// <summary>
		/// 应用ID
		/// </summary>
		public int APPID
		{
			set{ _appid=value;}
			get{return _appid;}
		}
		#endregion Model
	}
}

