﻿using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace Syn.Utility.Extense
{ 
    /// <summary>
    /// 写CSV文件类,首先给CSV文件赋值,最后通过Save方法进行保存操作
    /// </summary>
    public class CsvStreamWriter
    {
        private readonly ArrayList _rowAl; //行链表,CSV文件的每一行就是一个链
        private Encoding _encoding; //编码
        private string _fileName; //文件名

        public CsvStreamWriter()
        {
            _rowAl = new ArrayList();
            _fileName = "";
            _encoding = Encoding.Default;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName">文件名,包括文件路径</param>
        public CsvStreamWriter(string fileName)
        {
            _rowAl = new ArrayList();
            _fileName = fileName;
            _encoding = Encoding.Default;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName">文件名,包括文件路径</param>
        /// <param name="encoding">文件编码</param>
        public CsvStreamWriter(string fileName, Encoding encoding)
        {
            _rowAl = new ArrayList();
            _fileName = fileName;
            _encoding = encoding;
        }

        /// <summary>
        /// 得到第row行，第col列
        /// </summary>
        /// <param name="row">行,row = 1代表第一行</param>
        /// <param name="col">列,col = 1代表第一列</param>
        /// <returns></returns>
        public string this[int row, int col]
        {
            set
            {
                //对行进行判断
                if (row <= 0)
                {
                    throw new Exception("行数不能小于0");
                }
                if (row > _rowAl.Count) //如果当前列链的行数不够，要补齐
                {
                    for (int i = _rowAl.Count + 1; i <= row; i++)
                    {
                        _rowAl.Add(new ArrayList());
                    }
                }
                //对列进行判断
                if (col <= 0)
                {
                    throw new Exception("列数不能小于0");
                }
                var colTempAl = (ArrayList) _rowAl[row - 1];

                //扩大长度
                if (col > colTempAl.Count)
                {
                    for (int i = colTempAl.Count; i <= col; i++)
                    {
                        colTempAl.Add("");
                    }
                }
                _rowAl[row - 1] = colTempAl;
                //赋值
                var colAl = (ArrayList) _rowAl[row - 1];

                colAl[col - 1] = value;
                _rowAl[row - 1] = colAl;
            }
        }


        /// <summary>
        /// 文件名,包括文件路径
        /// </summary>
        public string FileName
        {
            set { _fileName = value; }
        }

        /// <summary>
        /// 文件编码
        /// </summary>
        public Encoding FileEncoding
        {
            set { _encoding = value; }
        }

        /// <summary>
        /// 获取当前最大行
        /// </summary>
        public int CurMaxRow
        {
            get { return _rowAl.Count; }
        }

        /// <summary>
        /// 获取最大列
        /// </summary>
        public int CurMaxCol
        {
            get
            {
                return _rowAl.Cast<ArrayList>().Aggregate(0, (current, colAl) => (current > colAl.Count) ? current : colAl.Count);
            }
        }

        /// <summary>
        /// 添加表数据到CSV文件中
        /// </summary>
        /// <param name="dataDt">表数据</param>
        /// <param name="beginCol">从第几列开始,beginCol = 1代表第一列</param>
        public void AddData(DataTable dataDt, int beginCol)
        {
            if (dataDt == null)
            {
                throw new Exception("需要添加的表数据为空");
            }

            int curMaxRow = _rowAl.Count;
            for (int i = 0; i < dataDt.Rows.Count; i++)
            {
                for (int j = 0; j < dataDt.Columns.Count; j++)
                {
                    this[curMaxRow + i + 1, beginCol + j] = dataDt.Rows[i][j].ToString();
                }
            }
        }

        /// <summary>
        /// 保存数据,如果当前硬盘中已经存在文件名一样的文件，将会覆盖
        /// </summary>
        public void Save()
        {
            //对数据的有效性进行判断
            if (_fileName == null)
            {
                throw new Exception("缺少文件名");
            }
            if (File.Exists(_fileName))
            {
                File.Delete(_fileName);
            }
            if (_encoding == null)
            {
                _encoding = Encoding.Default;
            }
            var sw = new StreamWriter(_fileName, false, _encoding);

            foreach (object t in _rowAl)
            {
                sw.WriteLine(ConvertToSaveLine((ArrayList) t));
            }

            sw.Close();
        }

        /// <summary>
        /// 保存数据,如果当前硬盘中已经存在文件名一样的文件，将会覆盖
        /// </summary>
        /// <param name="fileName">文件名,包括文件路径</param>
        public void Save(string fileName)
        {
            _fileName = fileName;
            Save();
        }

        /// <summary>
        /// 保存数据,如果当前硬盘中已经存在文件名一样的文件，将会覆盖
        /// </summary>
        /// <param name="fileName">文件名,包括文件路径</param>
        /// <param name="encoding">文件编码</param>
        public void Save(string fileName, Encoding encoding)
        {
            _fileName = fileName;
            _encoding = encoding;
            Save();
        }


        /// <summary>
        /// 转换成保存行
        /// </summary>
        /// <param name="colAl">一行</param>
        /// <returns></returns>
        private static string ConvertToSaveLine(ArrayList colAl)
        {
            string saveLine = "";
            for (int i = 0; i < colAl.Count; i++)
            {
                saveLine += ConvertToSaveCell(colAl[i].ToString());
                //格子间以逗号分割
                if (i < colAl.Count - 1)
                {
                    saveLine += ",";
                }
            }

            return saveLine;
        }

        /// <summary>
        /// 字符串转换成CSV中的格子
        /// 双引号转换成两个双引号，然后首尾各加一个双引号
        /// 这样就不需要考虑逗号及换行的问题
        /// </summary>
        /// <param name="cell">格子内容</param>
        /// <returns></returns>
        private static string ConvertToSaveCell(string cell)
        {
            cell = cell.Replace("\"", "\"\"");

            return "\"" + cell + "\"";
        }
    }
}