using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using System.Globalization;

namespace Syn.Utility.Extense
{

    /// <summary>
    /// 将Excel数据导入
    /// </summary>
    public class ExcelHelper
    {
        private readonly Stream _mFile;
        private readonly XlsHeader _mHdr;
        private readonly XlsBiffStream _mStream;
        private XlsWorkbookGlobals _mGlobals;
        private readonly List<XlsWorksheet> _mSheets = new List<XlsWorksheet>();
        private readonly DataSet _mWorkbookData;
        private ushort _mVersion = 0x0600;
        private Encoding _mEncoding = Encoding.Default;

        /// <summary>
        /// 默认构造器
        /// </summary>
        /// <param name="file">源文件流</param>
        public ExcelHelper(Stream file)
        {
            _mFile = file; // new BufferedStream(file);
            _mHdr = XlsHeader.ReadHeader(_mFile);
            var dir = new XlsRootDirectory(_mHdr);
            XlsDirectoryEntry workbookEntry = dir.FindEntry("Workbook") ?? dir.FindEntry("Book");
            if (workbookEntry == null)
                throw new FileNotFoundException("Oops! Neither stream 'Workbook' nor 'Book' was found in file");
            if (workbookEntry.EntryType != Stgty.StgtyStream)
                throw new FormatException("Oops! Workbook directory entry is not a Stream");
            _mStream = new XlsBiffStream(_mHdr, workbookEntry.StreamFirstSector);
            ReadWorkbookGlobals();
            GC.Collect();
            _mWorkbookData = new DataSet();
            foreach (XlsWorksheet t in _mSheets)
                if (ReadWorksheet(t))
                    _mWorkbookData.Tables.Add(t.Data);
            _mGlobals.Sst = null;
            _mGlobals = null;
            _mSheets = null;
            _mStream = null;
            _mHdr = null;
            GC.Collect();
        }

        /// <summary>        
        /// DataSet表示工作簿，Tables表示Excel中的Sheets
        /// </summary>
        public DataSet WorkbookData
        {
            get { return _mWorkbookData; }
        }

        /// <summary>
        /// 私有方法，读取工作簿中的所有sheet
        /// </summary>
        private void ReadWorkbookGlobals()
        {
            _mGlobals = new XlsWorkbookGlobals();
            _mStream.Seek(0, SeekOrigin.Begin);
            XlsBiffRecord rec = _mStream.Read();
            var bof = rec as XlsBiffBof;
            if (bof == null || bof.Type != Bifftype.WorkbookGlobals)
                throw new InvalidDataException("Oops! Stream has invalid data");
            _mVersion = bof.Version;
            _mEncoding = Encoding.Unicode;
            bool isV8 = (_mVersion >= 0x600);
            bool sst = false;
            while ((rec = _mStream.Read()) != null)
            {
                switch (rec.Id)
                { 
                    case Biffrecordtype.Interfacehdr:
                        _mGlobals.InterfaceHdr = (XlsBiffInterfaceHdr)rec;
                        break;
                    case Biffrecordtype.Boundsheet:
                        var sheet = (XlsBiffBoundSheet)rec;
                        if (sheet.Type != XlsBiffBoundSheet.SheetType.Worksheet) break;
                        sheet.IsV8 = isV8;
                        sheet.UseEncoding = _mEncoding;
                        _mSheets.Add(new XlsWorksheet(_mGlobals.Sheets.Count, sheet));
                        _mGlobals.Sheets.Add(sheet);
                        break;
                    case Biffrecordtype.Mms:
                        _mGlobals.Mms = rec;
                        break;
                    case Biffrecordtype.Country:
                        _mGlobals.Country = rec;
                        break;
                    case Biffrecordtype.Codepage:
                        _mGlobals.CodePage = (XlsBiffSimpleValueRecord)rec;
                        _mEncoding = Encoding.GetEncoding(_mGlobals.CodePage.Value);
                        break;
                    case Biffrecordtype.Font:
                    case Biffrecordtype.FontV34:
                        _mGlobals.Fonts.Add(rec);
                        break;
                    case Biffrecordtype.Format:
                    case Biffrecordtype.Formatv23:
                        _mGlobals.Formats.Add(rec);
                        break;
                    case Biffrecordtype.Xf:
                    case Biffrecordtype.XfV4:
                    case Biffrecordtype.XfV3:
                    case Biffrecordtype.XfV2:
                        _mGlobals.ExtendedFormats.Add(rec);
                        break;
                    case Biffrecordtype.Sst:
                        _mGlobals.Sst = (XlsBiffSst)rec;
                        sst = true;
                        break;
                    case Biffrecordtype.Continue:
                        if (!sst) break;
                        var contSst = (XlsBiffContinue)rec;
                        _mGlobals.Sst.Append(contSst);
                        break;
                    case Biffrecordtype.Extsst:
                        _mGlobals.ExtSst = rec;
                        sst = false;
                        break;
                    case Biffrecordtype.Eof:
                        if (_mGlobals.Sst != null)
                            _mGlobals.Sst.ReadStrings();
                        return;
                    default:
                        continue;
                }
            }
        }

        /// <summary>
        /// 私有方法，读取工作簿中指定的sheet
        /// </summary>
        /// <param name="sheet">要读取的Sheet对象</param>
        /// <returns>True表示读取成功，否则为False</returns>
        private bool ReadWorksheet(XlsWorksheet sheet)
        {
            _mStream.Seek((int)sheet.DataOffset, SeekOrigin.Begin);
            var bof = _mStream.Read() as XlsBiffBof;
            if (bof == null || bof.Type != Bifftype.Worksheet)
                return false;
            var idx = _mStream.Read() as XlsBiffIndex;
            bool isV8 = (_mVersion >= 0x600);
            if (idx != null)
            {
                idx.IsV8 = isV8;
                var dt = new DataTable(sheet.Name);

                XlsBiffRecord trec;
                XlsBiffDimensions dims = null;
                do
                {
                    trec = _mStream.Read();
                    if (trec.Id == Biffrecordtype.Dimensions)
                    {
                        dims = (XlsBiffDimensions)trec;
                        break;
                    }
                }
                while (trec.Id != Biffrecordtype.Row);
                int maxCol = 256;
                if (dims != null)
                {
                    dims.IsV8 = isV8;
                    maxCol = dims.LastColumn;
                    sheet.Dimensions = dims;
                }
                for (int i = 0; i < maxCol; i++)
                    dt.Columns.Add("Column" + (i + 1), typeof(string));
                sheet.Data = dt;
                uint maxRow = idx.LastExistingRow;
                if (idx.LastExistingRow <= idx.FirstExistingRow)
                    return true;
                dt.BeginLoadData();
                for (int i = 0; i <= maxRow; i++)
                    dt.Rows.Add(dt.NewRow());
                uint[] dbCellAddrs = idx.DbCellAddresses;
                foreach (uint t in dbCellAddrs)
                {
                    var dbCell = (XlsBiffDbCell)_mStream.ReadAt((int)t);
                    XlsBiffRow row;
                    var offs = dbCell.RowAddress;
                    do
                    {
                        row = _mStream.ReadAt(offs) as XlsBiffRow;
                        if (row == null) break;
                        offs += row.Size;
                    }
                    while (true);
                    while (true)
                    {
                        XlsBiffRecord rec = _mStream.ReadAt(offs);
                        offs += rec.Size;
                        if (rec is XlsBiffDbCell) break;
                        if (rec is XlsBiffEof) break;
                        var cell = rec as XlsBiffBlankCell;
                        if (cell == null)
                        {
                            continue;
                        }
                        if (cell.ColumnIndex >= maxCol) continue;
                        if (cell.RowIndex > maxRow) continue;
                        switch (cell.Id)
                        {
                            case Biffrecordtype.Integer:
                            case Biffrecordtype.IntegerOld:
                                dt.Rows[cell.RowIndex][cell.ColumnIndex] = ((XlsBiffIntegerCell)cell).Value.ToString();
                                break;
                            case Biffrecordtype.Number:
                            case Biffrecordtype.NumberOld:
                                dt.Rows[cell.RowIndex][cell.ColumnIndex] = FormatNumber(((XlsBiffNumberCell)cell).Value);
                                break;
                            case Biffrecordtype.Label:
                            case Biffrecordtype.LabelOld:
                            case Biffrecordtype.Rstring:
                                dt.Rows[cell.RowIndex][cell.ColumnIndex] = ((XlsBiffLabelCell)cell).Value;
                                break;
                            case Biffrecordtype.Labelsst:
                                {
                                    string tmp = _mGlobals.Sst.GetString(((XlsBiffLabelSstCell)cell).SstIndex);
                                    dt.Rows[cell.RowIndex][cell.ColumnIndex] = tmp;
                                }
                                break;
                            case Biffrecordtype.Rk:
                                dt.Rows[cell.RowIndex][cell.ColumnIndex] = FormatNumber(((XlsBiffRkCell)cell).Value);
                                break;
                            case Biffrecordtype.Mulrk:
                                for (ushort j = cell.ColumnIndex; j <= ((XlsBiffMulRkCell)cell).LastColumnIndex; j++)
                                    dt.Rows[cell.RowIndex][j] = FormatNumber(((XlsBiffMulRkCell)cell).GetValue(j));
                                break;
                            case Biffrecordtype.Blank:
                            case Biffrecordtype.BlankOld:
                            case Biffrecordtype.Mulblank:
                                // Skip blank cells
                                break;
                            case Biffrecordtype.Formula:
                            case Biffrecordtype.FormulaOld:
                                ((XlsBiffFormulaCell)cell).UseEncoding = _mEncoding; 
                                object val = ((XlsBiffFormulaCell)cell).Value;
                                if (val == null)
                                    val = string.Empty;
                                else if (val is Formulaerror)
                                    val = "#" + ((Formulaerror)val);
                                else if (val is double)
                                    val = FormatNumber((double)val);
                                dt.Rows[cell.RowIndex][cell.ColumnIndex] = val.ToString();
                                break;
                            default:
                                break;
                        }
                    }
                }
                dt.EndLoadData();
            }
            else
            {
                return false;
            }

            return true;
        }

        private static string FormatNumber(double x)
        {
            if (Math.Floor(x) == x)
                return Math.Floor(x).ToString();
            return Math.Round(x, 2).ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// 将Excel中的Sheet导出到DataTable中
        /// </summary>
        /// <param name="dataname">表名</param>
        /// <param name="filePath">Excel的路径</param>
        /// <returns>DataTable</returns>
        public DataTable LoadDataFromExcel(string dataname, string filePath)
        {
            var ds = new DataSet();
            DataTable dt;
            string conn = "Provider=Microsoft.Jet.Oledb.4.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=1;'";
            var thisconnection = new System.Data.OleDb.OleDbConnection(conn);
            thisconnection.Open();
            string sql = "select   *   from   [" + dataname + "$]";
            var mycommand = new System.Data.OleDb.OleDbDataAdapter(sql, thisconnection);
            try
            {
                mycommand.Fill(ds, "[" + dataname + "$]");
                dt = ds.Tables[0];
            }
            catch
            {
                dt = null;
                //InsertLog(dataname + "页签——————没有导入成功\r\n", null, 0);
            }
            finally
            {
                mycommand.Dispose();
                thisconnection.Close();
            }
            return dt;
        }

    }
}
