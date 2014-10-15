using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Syn.GDSC2
{
    public class Syn_IDCardRead
    {
        [DllImport("Syn_IDCardRead.dll")]
        public static extern  int  Syn_OpenPort(int iPortID);
        [DllImport("Syn_IDCardRead.dll")]
        public static extern  int Syn_ClosePort(int iPortID);
        [DllImport("Syn_IDCardRead.dll")]
        public static extern unsafe int  Syn_StartFindIDCard(int iPortID, byte[] pucManaInfo,int iIfOpen);
        [DllImport("Syn_IDCardRead.dll")]
        public static extern unsafe int  Syn_SelectIDCard(int iPortID, byte[] pucManaMsg,int iIfOpen);
        [DllImport("Syn_IDCardRead.dll")]
        public static extern unsafe int Syn_ReadMsg(int iPortID,int iIfOpen,ref IDCardData pIDCardData);
        [DllImport("Syn_IDCardRead.dll")]
        public static extern unsafe int Syn_GetSAMStatus(int iPortID,int iIfOpen);
        [DllImport("Syn_IDCardRead.dll")]
        public static extern unsafe int Syn_ResetSAM(int iPortID,int iIfOpen);
        [DllImport("Syn_IDCardRead.dll")]
        public static extern unsafe int Syn_GetSAMID(int iPortID, byte[] pucSAMID,int iIfOpen);
        [DllImport("Syn_IDCardRead.dll")]
        public static extern unsafe int Syn_GetSAMIDToStr(int iPortID, ref string pcSAMID,int iIfOpen);
        [DllImport("Syn_IDCardRead.dll")]
        public static extern unsafe void Syn_DelPhotoFile();
        
        //    typedef struct tagIDCardData{
        //    char Name[32]; //姓名       
        //    char Sex[4];   //性别
        //    char Nation[6]; //名族
        //    char Born[18]; //出生日期
        //    char Address[72]; //住址
        //    char IDCardNo[38]; //身份证号
        //    char GrantDept[32]; //发证机关
        //    char UserLifeBegin[18]; //有效开始日期
        //    char UserLifeEnd[18];  //有效截止日期
        //    char reserved[38]; //保留
        //    char PhotoFileName[255]; //照片路径
        //}
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct IDCardData
        {
            /// <summary>
            /// 姓名
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string Name;
            /// <summary>
            /// 性别
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
            public string SexNo; 							//性别
            /// <summary>
            /// 民族
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 6)]
            public string Nation;						//民族
            /// <summary>
            /// 出生日期
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 18)]
            public string Born; 					//出生日期
            /// <summary>
            /// 住址
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 72)]
            public string Address;
            /// <summary>
            /// 身份证号
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 38)]
            public string IDCardNo;
            /// <summary>
            /// 发证机关
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string GrantDept;
            /// <summary>
            /// 有效开始日期
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 18)]
            public string UserLifeBegin;
            /// <summary>
            /// 有效截止日期
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 18)]
            public string UserLifeEnd;
            /// <summary>
            /// 保留字段
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 38)]
            public string reserved;
            /// <summary>
            /// 照片路径
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 255)]
            public string PhotoFileName;
        }

    }
}
