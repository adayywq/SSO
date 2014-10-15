using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Syn.Utility.Extense
{ 
    /// <summary>
    /// 读CSV文件类,读取指定的CSV文件，可以导出DataTable
    /// </summary>
    public class CsvStreamReader
    {
        private readonly ArrayList _rowAl; //行链表,CSV文件的每一行就是一个链

        private Encoding _encoding; //编码
        private string _fileName; //文件名

        public CsvStreamReader()
        {
            _rowAl = new ArrayList();
            _fileName = "";
            _encoding = Encoding.Default;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName">文件名,包括文件路径</param>
        public CsvStreamReader(string fileName)
        {
            _rowAl = new ArrayList();
            _fileName = fileName;
            _encoding = Encoding.Default;
            LoadCsvFile();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName">文件名,包括文件路径</param>
        /// <param name="encoding">文件编码</param>
        public CsvStreamReader(string fileName, Encoding encoding)
        {
            _rowAl = new ArrayList();
            _fileName = fileName;
            _encoding = encoding;
            LoadCsvFile();
        }

        /// <summary>
        /// 文件名,包括文件路径
        /// </summary>
        public string FileName
        {
            set
            {
                _fileName = value;
                LoadCsvFile();
            }
        }

        /// <summary>
        /// 文件编码
        /// </summary>
        public Encoding FileEncoding
        {
            set { _encoding = value; }
        }

        /// <summary>
        /// 获取行数
        /// </summary>
        public int RowCount
        {
            get { return _rowAl.Count; }
        }

        /// <summary>
        /// 获取列数
        /// </summary>
        public int ColCount
        {
            get
            {
                return _rowAl.Cast<ArrayList>().Aggregate(0, (current, colAl) => (current > colAl.Count) ? current : colAl.Count);
            }
        }

        /// <summary>
        /// 获取某行某列的数据
        /// </summary>
        /// <param name="row">行,row = 1代表第一行</param>
        /// <param name="col">列,col = 1代表第一列</param>
        /// <returns></returns>
        public string this[int row, int col]
        {
            get
            {
                //数据有效性验证
                CheckRowValid(row);
                CheckColValid(col);
                var colAl = (ArrayList) _rowAl[row - 1];
                //如果请求列数据大于当前行的列时,返回空值
                return colAl.Count < col ? "" : colAl[col - 1].ToString();
            }
        }

        /// <summary>
        /// 根据最小行，最大行，最小列，最大列，来生成一个DataTable类型的数据
        /// </summary>
        /// <param name="minRow">行等于1代表第一行</param>
        /// <param name="maxRow"> -1代表最大行</param>
        /// <param name="minCol">列等于1代表第一列</param>
        /// <param name="maxCol"> -1代表最大列</param>
        /// <returns></returns>
        public DataTable this[int minRow, int maxRow, int minCol, int maxCol]
        {
            get
            {
                //数据有效性验证

                CheckRowValid(minRow);
                CheckMaxRowValid(maxRow);
                CheckColValid(minCol);
                CheckMaxColValid(maxCol);
                if (maxRow == -1)
                {
                    maxRow = RowCount;
                }
                if (maxCol == -1)
                {
                    maxCol = ColCount;
                }
                if (maxRow < minRow)
                {
                    throw new Exception("最大行数不能小于最小行数");
                }
                if (maxCol < minCol)
                {
                    throw new Exception("最大列数不能小于最小列数");
                }
                var csvDt = new DataTable();
                int i;
                int row;

                //增加列

                for (i = minCol; i <= maxCol; i++)
                {
                    csvDt.Columns.Add(i.ToString());
                }
                DataRow csvDr = csvDt.NewRow();
                for (row = minRow; row <= maxRow; row++)
                {
                    i = 0;
                    int col;
                    for (col = minCol; col <= maxCol; col++)
                    {
                        csvDr[i] = this[row, col];
                        i++;
                    }
                    csvDt.Rows.Add(csvDr);
                }

                return csvDt;
            }
        }

        /// <summary>
        /// 检查行数是否是有效的
        /// </summary>
        /// <param name="row"></param>
        private void CheckRowValid(int row)
        {
            if (row <= 0)
            {
                throw new Exception("行数不能小于0");
            }
            if (row > RowCount)
            {
                throw new Exception("没有当前行的数据");
            }
        }

        /// <summary>
        /// 检查最大行数是否是有效的
        /// </summary>
        /// <param name="maxRow"></param>  
        private void CheckMaxRowValid(int maxRow)
        {
            if (maxRow <= 0 && maxRow != -1)
            {
                throw new Exception("行数不能等于0或小于-1");
            }
            if (maxRow > RowCount)
            {
                throw new Exception("没有当前行的数据");
            }
        }

        /// <summary>
        /// 检查列数是否是有效的
        /// </summary>
        /// <param name="col"></param>  
        private void CheckColValid(int col)
        {
            if (col <= 0)
            {
                throw new Exception("列数不能小于0");
            }
            if (col > ColCount)
            {
                throw new Exception("没有当前列的数据");
            }
        }

        /// <summary>
        /// 检查检查最大列数是否是有效的
        /// </summary>
        /// <param name="maxCol"></param>  
        private void CheckMaxColValid(int maxCol)
        {
            if (maxCol <= 0 && maxCol != -1)
            {
                throw new Exception("列数不能等于0或小于-1");
            }
            if (maxCol > ColCount)
            {
                throw new Exception("没有当前列的数据");
            }
        }

        /// <summary>
        /// 载入CSV文件
        /// </summary>
        private void LoadCsvFile()
        {
            //对数据的有效性进行验证

            if (_fileName == null)
            {
                throw new Exception("请指定要载入的CSV文件名");
            }
            if (!File.Exists(_fileName))
            {
                throw new Exception("指定的CSV文件不存在");
            }
            if (_encoding == null)
            {
                _encoding = Encoding.Default;
            }

            var sr = new StreamReader(_fileName, _encoding);

            string csvDataLine = "";
            string fileDataLine = sr.ReadLine();
            while (true)
            {
                if (fileDataLine == null)
                {
                    break;
                }
                if (csvDataLine == "")
                {
                    csvDataLine = fileDataLine; //GetDeleteQuotaDataLine(fileDataLine);
                }
                else
                {
                    csvDataLine += "\r\n" + fileDataLine; //GetDeleteQuotaDataLine(fileDataLine);
                }
                //如果包含偶数个引号，说明该行数据中出现回车符或包含逗号
                if (IfOddQuota(csvDataLine)) continue;
                AddNewDataLine(csvDataLine);
                csvDataLine = "";
            }
            sr.Close();
            //数据行出现奇数个引号
            if (csvDataLine.Length > 0)
            {
                throw new Exception("CSV文件的格式有错误");
            }
        }

        /// <summary>
        /// 判断字符串是否包含奇数个引号
        /// </summary>
        /// <param name="dataLine">数据行</param>
        /// <returns>为奇数时，返回为真；否则返回为假</returns>
        private static bool IfOddQuota(IEnumerable<char> dataLine)
        {
            int quotaCount = dataLine.Count(t => t == '\"');

            bool oddQuota = false;
            if (quotaCount%2 == 1)
            {
                oddQuota = true;
            }

            return oddQuota;
        }

        /// <summary>
        /// 判断是否以奇数个引号开始
        /// </summary>
        /// <param name="dataCell"></param>
        /// <returns></returns>
        private static bool IfOddStartQuota(IEnumerable<char> dataCell)
        {
            int quotaCount = 0;
            foreach (char t in dataCell)
            {
                if (t == '\"')
                {
                    quotaCount++;
                }
                else
                {
                    break;
                }
            }

            bool oddQuota = false;
            if (quotaCount%2 == 1)
            {
                oddQuota = true;
            }

            return oddQuota;
        }

        /// <summary>
        /// 判断是否以奇数个引号结尾
        /// </summary>
        /// <param name="dataCell"></param>
        /// <returns></returns>
        private static bool IfOddEndQuota(string dataCell)
        {
            int quotaCount = 0;
            for (int i = dataCell.Length - 1; i >= 0; i--)
            {
                if (dataCell[i] == '\"')
                {
                    quotaCount++;
                }
                else
                {
                    break;
                }
            }

            bool oddQuota = false;
            if (quotaCount%2 == 1)
            {
                oddQuota = true;
            }

            return oddQuota;
        }

        /// <summary>
        /// 加入新的数据行
        /// </summary>
        /// <param name="newDataLine">新的数据行</param>
        private void AddNewDataLine(string newDataLine)
        {
            Debug.WriteLine("NewLine:" + newDataLine);
            //return;
            var colAl = new ArrayList();
            string[] dataArray = newDataLine.Split(',');

            bool oddStartQuota = false;
            string cellData = "";
            foreach (string t in dataArray)
            {
                if (oddStartQuota)
                {
                    //因为前面用逗号分割,所以要加上逗号
                    cellData += "," + t;
                    //是否以奇数个引号结尾
                    if (IfOddEndQuota(t))
                    {
                        colAl.Add(GetHandleData(cellData));
                        oddStartQuota = false;
                        continue;
                    }
                }
                else
                {
                    //是否以奇数个引号开始

                    if (IfOddStartQuota(t))
                    {
                        //是否以奇数个引号结尾,不能是一个双引号,并且不是奇数个引号

                        if (IfOddEndQuota(t) && t.Length > 2 && !IfOddQuota(t))
                        {
                            colAl.Add(GetHandleData(t));
                            continue;
                        }
                        oddStartQuota = true;
                        cellData = t;
                        continue;
                    }
                    colAl.Add(GetHandleData(t));
                }
            }
            if (oddStartQuota)
            {
                throw new Exception("数据格式有问题");
            }
            _rowAl.Add(colAl);
        }


        /// <summary>
        /// 去掉格子的首尾引号，把双引号变成单引号
        /// </summary>
        /// <param name="fileCellData"></param>
        /// <returns></returns>
        private static string GetHandleData(string fileCellData)
        {
            if (fileCellData == "")
            {
                return "";
            }
            if (IfOddStartQuota(fileCellData))
            {
                if (IfOddEndQuota(fileCellData))
                {
                    return fileCellData.Substring(1, fileCellData.Length - 2).Replace("\"\"", "\"");
                        //去掉首尾引号，然后把双引号变成单引号
                }
                throw new Exception("数据引号无法匹配" + fileCellData);
            }
            //考虑形如""    """"      """"""    
            if (fileCellData.Length > 2 && fileCellData[0] == '\"')
            {
                fileCellData = fileCellData.Substring(1, fileCellData.Length - 2).Replace("\"\"", "\"");
                //去掉首尾引号，然后把双引号变成单引号
            }

            return fileCellData;
        }
    }
}