using System;
using System.Collections.Generic;
using System.Text;

namespace Syn.GDSC2
{
  public class IdentityErrorMsg
    {
      public static string GetErrorMsg(int iRet)
      {
          string result = "身份证返回:";
          switch (iRet)
          {
              case -1: result = "端口打开失败"; break;
              case -2: result = "证/卡中次项无内容"; break;
              case -3: result = "PC接收超时，在规定的时间内未接收到规定长度的数据"; break;
              case -4: result = " 数据传输错误"; break;
              case -5: result = "该SAM_V串口不可用，只在SDT_GetCOMBaud时才有可能返回"; break;
              case -6: result = " 接收业务终端数据的校验和错"; break;
              case -7: result = "接收业务终端数据的长度错"; break;
              case -8: result = "接收业务终端的命令错误，包括命令中的各种数值或逻辑搭配错误"; break;
              case -9: result = "越权操作"; break;
              case -10: result = "无法识别的错误"; break;
              case -11: result = "寻找证/卡失败"; break;
              case -12: result = " 选取证/卡失败"; break;
              case -13: result = "调用sdtapi.dll错误"; break;
              case -14: result = "相片解码错误"; break;
              case -15: result = "授权文件不存在"; break;
              case -16: result = "设备连接错误"; break;
              default: result = "未定义的返回值"; break;
          }
          return iRet.ToString()+result;
      }
    }
}
