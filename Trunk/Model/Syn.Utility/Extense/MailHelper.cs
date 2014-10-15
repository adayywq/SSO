using Syn.Utility.Function;

namespace Syn.Utility.Extense
{
    /// <summary>
    /// 邮件发送(需预先指定)
    /// </summary>
    public class MailHelper
    {
        //邮件发送端口
        private readonly int _mailPort = Utils.GetConfigParam("MailPort", "25").ToInt32();
        //邮件发送超时时间(毫秒)
        private readonly int _mailTimeOut = Utils.GetConfigParam("MailTimeOut","50000").ToInt32();
        //发送邮件服务器  smtp.sina.net;smtp.163.com
        private readonly string _mailHost = Utils.GetConfigParam("EmailSmtp", string.Empty);
                
        // 发件人名称
        private readonly string _fromMailName = Utils.GetConfigParam("FromMailName", string.Empty);
        //发件人Email
        private readonly string _fromMailAdd = Utils.GetConfigParam("FromMailAdd", string.Empty);
        //发件箱密码
        private readonly string _fromMailPwd = Utils.GetConfigParam("FromMailPwd", string.Empty);

        /// <summary>
        /// 发送邮件(邮件发送端口:"EmailSmtp";邮件发送超时时间(毫秒):"EmailSmtp";发送邮件服务器:"EmailSmtp";发件人名称:"FromMailName";发件人Email"FromMailAdd";发件箱密码:"FromMailPwd"需要从web.config中定义节点)
        /// </summary>
        /// <param name="toMailName">收件人名称</param>
        /// <param name="toMailAdd">收件人地址</param>
        /// <param name="mailSubject">邮件主题</param>
        /// <param name="mailBody">邮件正文</param>
        /// <returns></returns>
        public bool SendMail(string toMailName, string toMailAdd, string mailSubject, string mailBody)
        {
            try
            {
                var mailServer = new System.Net.Mail.SmtpClient
                                     {
                                         Credentials = new System.Net.NetworkCredential(_fromMailAdd, _fromMailPwd),
                                         EnableSsl = false,
                                         DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                                         Host = _mailHost,
                                         Port = _mailPort,
                                         Timeout = _mailTimeOut
                                     };

                var mail = new System.Net.Mail.MailMessage();
                var mFrom = new System.Net.Mail.MailAddress(_fromMailAdd, _fromMailName, System.Text.Encoding.Default);
                mail.From = mFrom;
                var mTo = new System.Net.Mail.MailAddress(toMailAdd, toMailName, System.Text.Encoding.Default);
                mail.To.Add(mTo);
                mail.Subject = mailSubject;
                mail.Body = mailBody;
                mail.SubjectEncoding = System.Text.Encoding.Default;
                mail.BodyEncoding = System.Text.Encoding.Default;
                mail.IsBodyHtml = true;
                mailServer.Send(mail);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 发送邮件(全部自定义，发送邮件的参数)
        /// </summary>
        /// 发件人名称/param>
        /// <param name="toMailAdd">收件人地址</param>
        /// <param name="mailSubject">邮件主题</param>
        /// <param name="mailBody">邮件正文</param>
        /// <param name="fromMailName">发件人名称</param>
        /// <param name="fromMailAdd">发件人地址</param>
        /// <param name="fromMailPwd">发件人密码</param>
        /// <param name="toMailName">收件人名称</param>
        /// <returns></returns>
        public bool SendMail(string fromMailName, string fromMailAdd, string fromMailPwd, string toMailName, string toMailAdd, string mailSubject, string mailBody)
        {
            try
            {
                var mailServer = new System.Net.Mail.SmtpClient
                                     {
                                         Credentials = new System.Net.NetworkCredential(fromMailAdd, fromMailPwd),
                                         EnableSsl = false,
                                         DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                                         Host = _mailHost,
                                         Port = _mailPort,
                                         Timeout = _mailTimeOut
                                     };

                var mail = new System.Net.Mail.MailMessage();
                var mFrom = new System.Net.Mail.MailAddress(fromMailAdd, fromMailName, System.Text.Encoding.Default);
                mail.From = mFrom;
                var mTo = new System.Net.Mail.MailAddress(toMailAdd, toMailName, System.Text.Encoding.Default);
                mail.To.Add(mTo);
                mail.Subject = mailSubject;
                mail.Body = mailBody;
                mail.SubjectEncoding = System.Text.Encoding.Default;
                mail.BodyEncoding = System.Text.Encoding.Default;
                mail.IsBodyHtml = true;
                mailServer.Send(mail);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
