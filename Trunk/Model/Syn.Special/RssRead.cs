using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Xml;
using System.Net;
using System.IO;

namespace Syn.Special
{
    public class RssRead
    {
        XmlDocument doc;
        List<Hashtable> list;
        public RssRead() { }

        public RssRead(XmlDocument doc)
        {
            this.doc = doc;
            Load();
        }

        /// <summary>
        /// 获取网络资源
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="timeOut">timeout（单位秒）</param>
        /// <param name="useProxy">是否用代理</param>
        public static XmlDocument GetDoc(string url, int timeOut, bool useProxy)
        {
            XmlDocument _doc = new XmlDocument();
            try
            {
                WebRequest req = WebRequest.Create(url);
                if (useProxy)
                {
                    WebProxy proxy = new WebProxy("http://63.149.98.16:80/", true);
                    req.Proxy = proxy;
                }
                req.Timeout = timeOut * 1000;
                WebResponse res = req.GetResponse();
                Stream rssStream = res.GetResponseStream();
                _doc.Load(rssStream);
                rssStream.Dispose();
                res.Close();
            }
            catch
            {
                _doc = null;
            }
            return _doc;
        }

        private void Load()
        {
            list = new List<Hashtable>();

            XmlNodeList nodes = doc.GetElementsByTagName("item");
            if (nodes == null || nodes.Count == 0)
                nodes = doc.GetElementsByTagName("entry");
            if (nodes == null || nodes.Count == 0)
                return;

            Hashtable ht;
            XmlNodeList ns;
            string name;
            string date;
            foreach (XmlNode node in nodes)
            {
                ht = new Hashtable();
                ns = node.ChildNodes;
                try
                {
                    foreach (XmlNode n in ns)
                    {
                        name = n.Name.ToLower();
                        if (name.Contains("link"))
                        {
                            if (n.Attributes["href"] != null)
                                ht["link"] = n.Attributes["href"].Value.Trim();
                            else
                                ht["link"] = n.InnerText.Trim();
                            continue;
                        }
                        if (name.Contains("title"))
                        {
                            ht["title"] = n.InnerText.Trim();
                            continue;
                        }
                        if (name.Contains("category"))
                        {
                            if (ht["category"] == null)
                                ht["category"] = n.InnerText.Trim();
                            else
                                ht["category"] = ht["category"].ToString() + "," + n.InnerText.Trim();
                            continue;
                        }
                        if (name.Contains("date"))
                        {
                            date = n.InnerText;
                            if (date != "")
                            {
                                if (date.Contains(","))
                                    date = date.Substring(date.IndexOf(",") + 1);
                                date = date.Trim();
                                if (date.Split(' ').Length > 4)
                                    date = date.Replace(date.Split(' ')[4], "");
                                if (date.Contains("."))
                                    date = date.Split('.')[0].Trim();
                                date = date.Replace("T", " ");
                                if (date.Substring(date.LastIndexOf(":") + 1).Length > 2)
                                    date = date.Substring(0, date.LastIndexOf(":") + 3);
                                try
                                {
                                    date = DateTime.Parse(date.Trim()).ToString();
                                }
                                catch
                                {
                                    date = DateTime.Now.ToString();
                                }
                            }
                            else
                                date = DateTime.Now.ToString();
                            ht["pubdate"] = date;
                            continue;
                        }
                        if (name.Contains("author"))
                        {
                            ht["author"] = n.InnerText.Trim();
                            continue;
                        }
                        if (name.Contains("description"))
                        {
                            ht["description"] = n.InnerText.Trim();
                            continue;
                        }
                        if (name.Contains("content"))
                        {
                            ht["description"] = n.InnerText.Trim();
                            continue;
                        }
                        if (name.Contains("summary"))
                        {
                            if (ht["description"] == null)
                                ht["description"] = n.InnerText.Trim();
                        }
                    }
                }
                catch
                {
                    continue;
                }
                if (ht["link"] == null) ht["link"] = "";
                if (ht["title"] == null || ht["title"].ToString() == "") ht["title"] = ht["link"].ToString();
                if (ht["category"] == null) ht["category"] = "";
                if (ht["pubdate"] == null) ht["pubdate"] = DateTime.Now.ToString();
                if (ht["description"] == null) ht["description"] = "";
                if (ht["author"] == null) ht["author"] = "";

                list.Add(ht);
            }
        }

        public List<Hashtable> Items
        {
            get
            {
                return list;
            }
        }
    }    
}
