using System;
using System.Collections.Generic;
using System.Text;

namespace Syn.GDSC2
{
  public class IdentityErrorMsg
    {
      public static string GetErrorMsg(int iRet)
      {
          string result = "���֤����:";
          switch (iRet)
          {
              case -1: result = "�˿ڴ�ʧ��"; break;
              case -2: result = "֤/���д���������"; break;
              case -3: result = "PC���ճ�ʱ���ڹ涨��ʱ����δ���յ��涨���ȵ�����"; break;
              case -4: result = " ���ݴ������"; break;
              case -5: result = "��SAM_V���ڲ����ã�ֻ��SDT_GetCOMBaudʱ���п��ܷ���"; break;
              case -6: result = " ����ҵ���ն����ݵ�У��ʹ�"; break;
              case -7: result = "����ҵ���ն����ݵĳ��ȴ�"; break;
              case -8: result = "����ҵ���ն˵�������󣬰��������еĸ�����ֵ���߼��������"; break;
              case -9: result = "ԽȨ����"; break;
              case -10: result = "�޷�ʶ��Ĵ���"; break;
              case -11: result = "Ѱ��֤/��ʧ��"; break;
              case -12: result = " ѡȡ֤/��ʧ��"; break;
              case -13: result = "����sdtapi.dll����"; break;
              case -14: result = "��Ƭ�������"; break;
              case -15: result = "��Ȩ�ļ�������"; break;
              case -16: result = "�豸���Ӵ���"; break;
              default: result = "δ����ķ���ֵ"; break;
          }
          return iRet.ToString()+result;
      }
    }
}
