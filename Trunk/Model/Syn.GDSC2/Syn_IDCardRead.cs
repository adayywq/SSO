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
        //    char Name[32]; //����       
        //    char Sex[4];   //�Ա�
        //    char Nation[6]; //����
        //    char Born[18]; //��������
        //    char Address[72]; //סַ
        //    char IDCardNo[38]; //���֤��
        //    char GrantDept[32]; //��֤����
        //    char UserLifeBegin[18]; //��Ч��ʼ����
        //    char UserLifeEnd[18];  //��Ч��ֹ����
        //    char reserved[38]; //����
        //    char PhotoFileName[255]; //��Ƭ·��
        //}
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct IDCardData
        {
            /// <summary>
            /// ����
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string Name;
            /// <summary>
            /// �Ա�
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
            public string SexNo; 							//�Ա�
            /// <summary>
            /// ����
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 6)]
            public string Nation;						//����
            /// <summary>
            /// ��������
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 18)]
            public string Born; 					//��������
            /// <summary>
            /// סַ
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 72)]
            public string Address;
            /// <summary>
            /// ���֤��
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 38)]
            public string IDCardNo;
            /// <summary>
            /// ��֤����
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string GrantDept;
            /// <summary>
            /// ��Ч��ʼ����
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 18)]
            public string UserLifeBegin;
            /// <summary>
            /// ��Ч��ֹ����
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 18)]
            public string UserLifeEnd;
            /// <summary>
            /// �����ֶ�
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 38)]
            public string reserved;
            /// <summary>
            /// ��Ƭ·��
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 255)]
            public string PhotoFileName;
        }

    }
}
