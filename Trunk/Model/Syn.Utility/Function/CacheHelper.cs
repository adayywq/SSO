using System;
using System.Collections;
using System.Collections.Specialized;
using System.Web;
using System.Web.Caching;

namespace Syn.Utility.Function
{
    /// <summary>
    ///CacheHelper 的摘要说明
    ///缓存名称 建议不使用下划线（"_"）
    /// </summary>
    public class CacheHelper
    {
        #region 缓存时间

        /// <summary>
        /// 缓存时间,默认30分钟
        /// </summary>
        private static string _cacheTime = "30";

        #endregion

        public CacheHelper(string cachetime)
        {
            _cacheTime = cachetime;
        }

        #region 缓存名称

        /// <summary>
        /// 获取缓存名称
        /// </summary>
        /// <param name="mainName"></param>
        /// <param name="subName"></param>
        /// <returns></returns>
        public static string GetCacheName(string mainName, string subName)
        {
            mainName = mainName.ToLower();
            subName = subName.ToLower();
            if (subName.Trim() == "")
            {
                subName = "&nbsp;";
            }
            string getName = mainName + "_" + subName;

            var keyname = new NameValueCollection();

            // 获取用户被缓存的访问记录
            var value = Get<object>(mainName);
            if (value != null)
            {
                keyname = (NameValueCollection) value;
            }

            // 根据参数值判断dt中是否有相应的数据			
            bool addNewValue = false;

            if (keyname.Count > 0)
            {
                if (keyname[getName] != null)
                {
                    if (keyname[getName].ToLower() != getName.ToLower())
                    {
                        addNewValue = true;
                        keyname.Add(subName, getName);
                    }
                }
                else
                {
                    addNewValue = true;
                    keyname.Add(subName, getName);
                }
            }
            else
            {
                addNewValue = true;
                keyname.Add(subName, getName);
            }
            if (addNewValue)
            {
                Add(mainName, keyname as object);
            }

            keyname.Clear();

            if (getName.Trim() != "")
            {
                getName = getName.Replace("&nbsp;", "");
            }

            return getName;
        }

        #endregion

        #region 判断Cache

        public static bool IsExist(string cachename)
        {
            cachename = cachename.ToLower();
            return HttpRuntime.Cache[cachename] != null;
        }

        #endregion

        #region 添加Cache

        /// <summary>
        /// 添加Cache
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="mainName">缓存主键</param>
        /// <param name="o">缓存对象</param>
        public static void Add<T>(string mainName, T o)
        {
            string cacheName = mainName.ToLower();
            HttpRuntime.Cache.Insert(cacheName, o, null, DateTime.Now.AddMinutes(Convert.ToDouble(_cacheTime)),
                                     Cache.NoSlidingExpiration);
        }

        public static void Add<T>(string mainName, string subName, T o)
        {
            string cacheName = GetCacheName(mainName.ToLower(), subName.ToLower());
            HttpRuntime.Cache.Insert(cacheName, o, null, DateTime.Now.AddMinutes(Convert.ToDouble(_cacheTime)),
                                     Cache.NoSlidingExpiration);
        }

        /// <summary>
        /// 添加Cache-带过期时间--
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="mainName">缓存键</param>
        /// <param name="o">缓存对象</param>
        /// <param name="cacheTime">缓存时间，单位分钟</param>
        public static void Add<T>(string mainName, T o, double cacheTime)
        {
            string cacheName = mainName.ToLower();
            HttpRuntime.Cache.Insert(cacheName, o, null, DateTime.Now.AddMinutes(cacheTime), Cache.NoSlidingExpiration);
        }

        public static void Add<T>(string mainName, string subName, T o, double cacheTime)
        {
            string cacheName = GetCacheName(mainName.ToLower(), subName.ToLower());
            HttpRuntime.Cache.Insert(cacheName, o, null, DateTime.Now.AddMinutes(cacheTime), Cache.NoSlidingExpiration);
        }

        /// <summary>
        /// 添加Cache-依赖文件--
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="o">缓存对象</param>
        /// <param name="mainName">缓存键</param>
        /// <param name="absoluteFilePath">依赖文件绝对路径</param>
        public static void Add<T>(string mainName, T o, string absoluteFilePath)
        {
            string cacheName = mainName.ToLower();
            HttpRuntime.Cache.Insert(cacheName, o, new CacheDependency(absoluteFilePath), Cache.NoAbsoluteExpiration,
                                     Cache.NoSlidingExpiration, CacheItemPriority.High, null);
        }

        #endregion

        #region 获取Cache

        /// <summary>
        /// 取得Cache值，带类型 T
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="mainName">缓存键</param>
        /// <returns></returns>
        public static T Get<T>(string mainName)
        {
            string cacheName = mainName.ToLower();
            T t;
            try
            {
                if (!IsExist(cacheName))
                {
                    return default(T); //
                }
                t = (T) HttpRuntime.Cache[cacheName];
            }
            catch
            {
                t = default(T);
            }

            return t;
        }

        public static T Get<T>(string mainName, string subName)
        {
            string cacheName = mainName.ToLower() + "_" + subName.ToLower();
            T t;
            try
            {
                if (!IsExist(cacheName))
                {
                    return default(T); //
                }
                t = (T) HttpRuntime.Cache[cacheName];
            }
            catch
            {
                t = default(T);
            }

            return t;
        }

        #endregion

        #region 删除Cache

        /// <summary>
        /// 删除指定key的Cache
        /// </summary>
        /// <param name="mainName">Cache的key</param>
        public static void Remove(string mainName)
        {
            NameValueCollection keyname;
            var value = Get<object>(mainName);
            if (value != null)
            {
                keyname = (NameValueCollection) value;
                for (int i = 0; i < keyname.Count; i++)
                {
                    HttpRuntime.Cache.Remove(mainName + "_" + keyname[i]);
                }
            }

            HttpRuntime.Cache.Remove(mainName);
        }

        public static void Remove(string mainName, string subName)
        {
            string cacheName = mainName.ToLower() + "_" + subName.ToLower();
            NameValueCollection keyname;
            var value = Get<object>(mainName);
            if (value != null)
            {
                keyname = (NameValueCollection) value;
                keyname.Remove(subName);
                Add(mainName, keyname as object);
            }

            HttpRuntime.Cache.Remove(cacheName);
        }

        #endregion

        #region 清除所有Cache

        /// <summary>
        /// 清除所有缓存
        /// </summary>
        public static void RemoveAll()
        {
            Cache cache = HttpRuntime.Cache;
            IDictionaryEnumerator cacheEnum = cache.GetEnumerator();
            var al = new ArrayList();
            while (cacheEnum.MoveNext())
            {
                al.Add(cacheEnum.Key);
            }

            foreach (string key in al)
            {
                cache.Remove(key);
            }
        }

        #endregion
    }
}