using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections.ObjectModel;
using System.Data;
using System.Runtime.CompilerServices;

namespace Syn.Utility.Extense
{

    internal enum Stgty : byte
    {
        StgtyInvalid = 0,
        StgtyStorage = 1,
        StgtyStream = 2,
        StgtyLockbytes = 3,
        StgtyProperty = 4,
        StgtyRoot = 5
    }

    internal enum Decolor : byte
    {
        DeRed = 0,
        DeBlack = 1
    }

    internal enum Fatmarkers : uint
    {
        FatEndOfChain = 0xFFFFFFFE,
        FatFreeSpace = 0xFFFFFFFF,
        FatFatSector = 0xFFFFFFFD,
        FatDifSector = 0xFFFFFFFC
    }

    internal enum Bifftype : ushort
    {
        WorkbookGlobals = 0x0005,
        VbModule = 0x0006,
        Worksheet = 0x0010,
        Chart = 0x0020,
        V4MacroSheet = 0x0040,
        V4WorkbookGlobals = 0x0100
    }

    internal enum Biffrecordtype : ushort
    {
        Interfacehdr = 0x00E1,
        Mms = 0x00C1,
        Interfaceend = 0x00E2,
        Writeaccess = 0x005C,
        Codepage = 0x0042,
        Dsf = 0x0161,
        Tabid = 0x013D,
        Fngroupcount = 0x009C,
        Windowprotect = 0x0019,
        Protect = 0x0012,
        Password = 0x0013,
        Prot4Rev = 0x01AF,
        Prot4Revpassword = 0x01BC,
        Window1 = 0x003D,
        Backup = 0x0040,
        Hideobj = 0x008D,
        Record1904 = 0x0022,
        Refreshall = 0x01B7,
        Bookbool = 0x00DA,

        Font = 0x0031,                  // Font record, BIFF2, 5 and later
        FontV34 = 0x0231,              // Font record, BIFF3, 4

        Format = 0x041E,                // Format record, BIFF4 and later
        Formatv23 = 0x001E,            // Format record, BIFF2, 3

        Xf = 0x00E0,                    // Extended format record, BIFF5 and later
        XfV4 = 0x0443,                 // Extended format record, BIFF4
        XfV3 = 0x0243,                 // Extended format record, BIFF3
        XfV2 = 0x0043,                 // Extended format record, BIFF2

        Style = 0x0293,
        Boundsheet = 0x0085,
        Country = 0x008C,
        Sst = 0x00FC,                   // Global string storage (for BIFF8)
        Continue = 0x003C,
        Extsst = 0x00FF,
        Bof = 0x0809,                   // BOF ID for BIFF5 and later
        BofV2 = 0x0009,                // BOF ID for BIFF2
        BofV3 = 0x0209,                // BOF ID for BIFF3
        BofV4 = 0x0409,                // BOF ID for BIFF5
        Eof = 0x000A,                   // End of block started with BOF
        Calccount = 0x000C,
        Calcmode = 0x000D,
        Precision = 0x000E,
        Refmode = 0x000F,
        Delta = 0x0010,
        Iteration = 0x0011,
        Saverecalc = 0x005F,
        Printheaders = 0x002A,
        Printgridlines = 0x002B,
        Guts = 0x0080,
        Wsbool = 0x0081,
        Gridset = 0x0082,
        Defaultrowheight = 0x0225,
        Header = 0x0014,
        Footer = 0x0015,
        Hcenter = 0x0083,
        Vcenter = 0x0084,
        Printsetup = 0x00A1,
        Dfaultcolwidth = 0x0055,
        Dimensions = 0x0200,            // Size of area used for data
        Row = 0x0208,                   // Row record
        Window2 = 0x023E,
        Selection = 0x001D,
        Index = 0x020B,                 // Index record, unsure about signature
        Dbcell = 0x00D7,                // DBCell record, unsure about signature
        Blank = 0x0201,                 // Empty cell
        BlankOld = 0x0001,             // Empty cell, old format
        Mulblank = 0x00BE,              // Equivalent of up to 256 blank cells
        Integer = 0x0202,               // Integer cell (0..65535)
        IntegerOld = 0x0002,           // Integer cell (0..65535), old format
        Number = 0x0203,                // Numeric cell
        NumberOld = 0x0003,            // Numeric cell, old format
        Label = 0x0204,                 // String cell (up to 255 symbols)
        LabelOld = 0x0004,             // String cell (up to 255 symbols), old format
        Labelsst = 0x00FD,              // String cell with value from SST (for BIFF8)
        Formula = 0x0406,               // Formula cell
        FormulaOld = 0x0006,           // Formula cell, old format
        Boolerr = 0x0205,               // Boolean or error cell
        BoolerrOld = 0x0005,           // Boolean or error cell, old format
        Array = 0x0221,                 // Range of cells for multi-cell formula
        Rk = 0x027E,                    // RK-format numeric cell
        Mulrk = 0x00BD,                 // Equivalent of up to 256 RK cells
        Rstring = 0x00D6,               // Rich-formatted string cell
        Shrfmla = 0x04BC,               // One more formula optimization element
        ShrfmlaOld = 0x00BC,           // One more formula optimization element, old format
        String = 0x0207,                // And one more, for string formula results
        Cf = 0x01B1,
        Codename = 0x01BA,
        Condfmt = 0x01B0,
        Dconbin = 0x01B5,
        Dv = 0x01BE,
        Dval = 0x01B2,
        Hlink = 0x01B8,
        Msodrawinggroup = 0x00EB,
        Msodrawing = 0x00EC,
        Msodrawingselection = 0x00ED,
        Paramqry = 0x00DC,
        Qsi = 0x01AD,
        Supbook = 0x01AE,
        Sxdb = 0x00C6,
        Sxdbex = 0x0122,
        Sxfdbtype = 0x01BB,
        Sxrule = 0x00F0,
        Sxex = 0x00F1,
        Sxfilt = 0x00F2,
        Sxname = 0x00F6,
        Sxselect = 0x00F7,
        Sxpair = 0x00F8,
        Sxfmla = 0x00F9,
        Sxformat = 0x00FB,
        Sxformula = 0x0103,
        Sxvdex = 0x0100,
        Txo = 0x01B6,
        Userbview = 0x01A9,
        Usersviewbegin = 0x01AA,
        Usersviewend = 0x01AB,
        Useselfs = 0x0160,
        Xl5Modify = 0x0162,
        Obj = 0x005D,
        Note = 0x001C,
        Sxext = 0x00DC,
        Verticalpagebreaks = 0x001A,
        Xct = 0x0059,

    }

    internal enum Formulaerror : byte
    {
        Null = 0x00,    // #NULL!
        Div0 = 0x07,    // #DIV/0!
        Value = 0x0F,   // #VALUE!
        Ref = 0x17,     // #REF!
        Name = 0x1D,    // #NAME?
        Num = 0x24,     // #NUM!
        Na = 0x2A,      // #N/A
    }

    public class InvalidHeaderException : Exception
    {
        public InvalidHeaderException()
        { }
        public InvalidHeaderException(string message)
            : base(message) { }
        public InvalidHeaderException(string message, Exception innerException)
            : base(message, innerException) { }
    }

    /// <summary>
    /// Represents Excel file header
    /// </summary>
    internal class XlsHeader
    {

        private readonly byte[] _mBytes;
        private readonly Stream _mFile;

        private XlsHeader(Stream file)
        {
            _mBytes = new byte[512];
            _mFile = file;
        }

        /// <summary>
        /// Reads Excel header from Stream
        /// </summary>
        /// <param name="file">Stream with Excel file</param>
        /// <returns>XlsHeader representing specified file</returns>
        public static XlsHeader ReadHeader(Stream file)
        {
            var hdr = new XlsHeader(file);
            lock (file)
            {
                file.Seek(0, SeekOrigin.Begin);
                file.Read(hdr._mBytes, 0, 512);
            }
            if (!hdr.IsSignatureValid)
                throw new InvalidHeaderException("Invalid file signature");
            if (hdr.ByteOrder != 0xFFFE)
                throw new FormatException("Invalid byte order specified");
            return hdr;
        }

        /// <summary>
        /// Returns file signature
        /// </summary>
        public ulong Signature
        {
            get { return BitConverter.ToUInt64(_mBytes, 0x0); }
        }

        /// <summary>
        /// Checks if file signature is valid
        /// </summary>
        public bool IsSignatureValid
        {
            get { return (Signature == 0xE11AB1A1E011CFD0); }
        }

        /// <summary>
        /// Typically filled with zeroes
        /// </summary>
        public Guid ClassId
        {
            get { var tmp = new byte[16]; Buffer.BlockCopy(_mBytes, 0x8, tmp, 0, 16); return new Guid(tmp); }
        }

        /// <summary>
        /// Must be 0x003E
        /// </summary>
        public ushort Version
        {
            get { return BitConverter.ToUInt16(_mBytes, 0x18); }
        }

        /// <summary>
        /// Must be 0x0003
        /// </summary>
        public ushort DllVersion
        {
            get { return BitConverter.ToUInt16(_mBytes, 0x1A); }
        }

        /// <summary>
        /// Must be 0xFFFE
        /// </summary>
        public ushort ByteOrder
        {
            get { return BitConverter.ToUInt16(_mBytes, 0x1C); }
        }

        /// <summary>
        /// Typically 512
        /// </summary>
        public int SectorSize
        {
            get { return (1 << BitConverter.ToUInt16(_mBytes, 0x1E)); }
        }

        /// <summary>
        /// Typically 64
        /// </summary>
        public int MiniSectorSize
        {
            get { return (1 << BitConverter.ToUInt16(_mBytes, 0x20)); }
        }

        /// <summary>
        /// Number of FAT sectors
        /// </summary>
        public int FatSectorCount
        {
            get { return BitConverter.ToInt32(_mBytes, 0x2C); }
        }

        /// <summary>
        /// Number of first Root Directory Entry (Property Set Storage, FAT Directory) sector
        /// </summary>
        public uint RootDirectoryEntryStart
        {
            get { return BitConverter.ToUInt32(_mBytes, 0x30); }
        }

        /// <summary>
        /// Transaction signature, 0 for Excel
        /// </summary>
        public uint TransactionSignature
        {
            get { return BitConverter.ToUInt32(_mBytes, 0x34); }
        }

        /// <summary>
        /// Maximum size for small stream, typically 4096 bytes
        /// </summary>
        public uint MiniStreamCutoff
        {
            get { return BitConverter.ToUInt32(_mBytes, 0x38); }
        }

        /// <summary>
        /// First sector of Mini FAT, FAT_EndOfChain if there's no one
        /// </summary>
        public uint MiniFatFirstSector
        {
            get { return BitConverter.ToUInt32(_mBytes, 0x3C); }
        }

        /// <summary>
        /// Number of sectors in Mini FAT, 0 if there's no one
        /// </summary>
        public int MiniFatSectorCount
        {
            get { return BitConverter.ToInt32(_mBytes, 0x40); }
        }

        /// <summary>
        /// First sector of DIF, FAT_EndOfChain if there's no one
        /// </summary>
        public uint DifFirstSector
        {
            get { return BitConverter.ToUInt32(_mBytes, 0x44); }
        }

        /// <summary>
        /// Number of sectors in DIF, 0 if there's no one
        /// </summary>
        public int DifSectorCount
        {
            get { return BitConverter.ToInt32(_mBytes, 0x48); }
        }

        public Stream FileStream
        {
            get { return _mFile; }
        }

        private XlsFat _mFat;

        /// <summary>
        /// Returns full FAT table, including DIF sectors
        /// </summary>
        public XlsFat Fat
        {
            get
            {
                if (_mFat != null)
                    return _mFat;

                uint value;
                int sectorSize = SectorSize;
                var sectors = new List<uint>(FatSectorCount);
                for (int i = 0x4C; i < sectorSize; i += 4)
                {
                    value = BitConverter.ToUInt32(_mBytes, i);
                    if (value == (uint)Fatmarkers.FatFreeSpace)
                        goto XlsHeader_Fat_Ready;
                    sectors.Add(value);
                }
                int difCount;
                if ((difCount = DifSectorCount) == 0)
                    goto XlsHeader_Fat_Ready;
                lock (_mFile)
                {
                    uint difSector = DifFirstSector;
                    var buff = new byte[sectorSize];
                    uint prevSector = 0;
                    while (difCount > 0)
                    {
                        sectors.Capacity += 128;
                        if (prevSector == 0 || (difSector - prevSector) != 1)
                            _mFile.Seek((difSector + 1) * sectorSize, SeekOrigin.Begin);
                        prevSector = difSector;
                        _mFile.Read(buff, 0, sectorSize);
                        for (int i = 0; i < 508; i += 4)
                        {
                            value = BitConverter.ToUInt32(buff, i);
                            if (value == (uint)Fatmarkers.FatFreeSpace)
                                goto XlsHeader_Fat_Ready;
                            sectors.Add(value);
                        }
                        value = BitConverter.ToUInt32(buff, 508);
                        if (value == (uint)Fatmarkers.FatFreeSpace)
                            break;
                        if ((difCount--) > 1)
                            difSector = value;
                        else
                            sectors.Add(value);
                    }
                }
            XlsHeader_Fat_Ready:
                _mFat = new XlsFat(this, sectors);
                return _mFat;
            }
        }

    }

    /// <summary>
    /// Represents Excel file FAT table
    /// </summary>
    internal class XlsFat
    {

        private readonly List<uint> _mFat;
        private readonly int _mSectorsForFat;
        private readonly int _mSectors;
        private readonly XlsHeader _mHdr;

        /// <summary>
        /// Constructs FAT table from list of sectors
        /// </summary>
        /// <param name="hdr">XlsHeader</param>
        /// <param name="sectors">Sectors list</param>
        public XlsFat(XlsHeader hdr, List<uint> sectors)
        {
            _mHdr = hdr;
            _mSectorsForFat = sectors.Count;
            uint prevSector = 0;
            int sectorSize = hdr.SectorSize;
            var buff = new byte[sectorSize];
            Stream file = hdr.FileStream;
            using (var ms = new MemoryStream(sectorSize * _mSectorsForFat))
            {
                lock (file)
                {
                    foreach (uint sector in sectors)
                    {
                        if (prevSector == 0 || (sector - prevSector) != 1)
                            file.Seek((sector + 1) * sectorSize, SeekOrigin.Begin);
                        prevSector = sector;
                        file.Read(buff, 0, sectorSize);
                        ms.Write(buff, 0, sectorSize);
                    }
                }
                ms.Seek(0, SeekOrigin.Begin);
                var rd = new BinaryReader(ms);
                _mSectors = (int)ms.Length / 4;
                _mFat = new List<uint>(_mSectors);
                for (int i = 0; i < _mSectors; i++)
                    _mFat.Add(rd.ReadUInt32());
                rd.Close();
                ms.Close();
            }
        }

        /// <summary>
        /// Returns next data sector using FAT
        /// </summary>
        /// <param name="sector">Current data sector</param>
        /// <returns>Next data sector</returns>
        public uint GetNextSector(uint sector)
        {
            if (_mFat.Count <= sector)
                throw new ArgumentOutOfRangeException("sector");
            uint value = _mFat[(int)sector];
            if (value == (uint)Fatmarkers.FatFatSector || value == (uint)Fatmarkers.FatDifSector)
                throw new InvalidOperationException("Oops! Trying to read stream from FAT area.");
            return value;
        }

        /// <summary>
        /// Resurns number of sectors used by FAT itself
        /// </summary>
        public int SectorsForFat
        {
            get { return _mSectorsForFat; }
        }

        /// <summary>
        /// Returns number of sectors described by FAT
        /// </summary>
        public int SectorsCount
        {
            get { return _mSectors; }
        }

        /// <summary>
        /// Returns underlying XlsHeader object
        /// </summary>
        public XlsHeader Header
        {
            get { return _mHdr; }
        }

    }

    /// <summary>
    /// Represents an Excel file stream
    /// </summary>
    internal class XlsStream
    {

        protected Stream MFile;
        protected XlsFat MFat;
        protected XlsHeader MHdr;
        protected uint MStartSector;

        public XlsStream(XlsHeader hdr, uint startSector)
        {
            MFile = hdr.FileStream;
            MFat = hdr.Fat;
            MHdr = hdr;
            MStartSector = startSector;
        }

        /// <summary>
        /// Returns offset of first stream sector
        /// </summary>
        public uint BaseOffset
        {
            get { return (uint)((MStartSector + 1) * MHdr.SectorSize); }
        }

        /// <summary>
        /// Returns number of first stream sector
        /// </summary>
        public uint BaseSector
        {
            get { return (MStartSector); }
        }

        /// <summary>
        /// Reads stream data from file
        /// </summary>
        /// <returns>Stream data</returns>
        public byte[] ReadStream()
        {
            uint sector = MStartSector, prevSector = 0;
            int sectorSize = MHdr.SectorSize;
            var buff = new byte[sectorSize];
            using (var ms = new MemoryStream(sectorSize * 8))
            {
                lock (MFile)
                {
                    do
                    {
                        if (prevSector == 0 || (sector - prevSector) != 1)
                            MFile.Seek((sector + 1) * sectorSize, SeekOrigin.Begin);
                        prevSector = sector;
                        MFile.Read(buff, 0, sectorSize);
                        ms.Write(buff, 0, sectorSize);
                    }
                    while ((sector = MFat.GetNextSector(sector)) != (uint)Fatmarkers.FatEndOfChain);
                }
                byte[] ret = ms.ToArray();
                ms.Close();
                return ret;
            }
        }

    }

    /// <summary>
    /// Represents Root Directory in file
    /// </summary>
    internal class XlsRootDirectory
    {
        private readonly List<XlsDirectoryEntry> _mEntries;
        private readonly XlsDirectoryEntry _mRoot;

        /// <summary>
        /// Creates Root Directory catalog from XlsHeader
        /// </summary>
        /// <param name="hdr">XlsHeader object</param>
        public XlsRootDirectory(XlsHeader hdr)
        {
            var stream = new XlsStream(hdr, hdr.RootDirectoryEntryStart);
            byte[] array = stream.ReadStream();
            byte[] tmp;
            XlsDirectoryEntry entry;
            var entries = new List<XlsDirectoryEntry>();
            for (int i = 0; i < array.Length; i += XlsDirectoryEntry.Length)
            {
                tmp = new byte[XlsDirectoryEntry.Length];
                Buffer.BlockCopy(array, i, tmp, 0, tmp.Length);
                entries.Add(new XlsDirectoryEntry(tmp));
            }
            _mEntries = entries;
            foreach (XlsDirectoryEntry t in entries)
            {
                entry = t;
                if (_mRoot == null && entry.EntryType == Stgty.StgtyRoot)
                    _mRoot = entry;
                if (entry.ChildSid != (uint)Fatmarkers.FatFreeSpace)
                    entry.Child = entries[(int)entry.ChildSid];
                if (entry.LeftSiblingSid != (uint)Fatmarkers.FatFreeSpace)
                    entry.LeftSibling = entries[(int)entry.LeftSiblingSid];
                if (entry.RightSiblingSid != (uint)Fatmarkers.FatFreeSpace)
                    entry.RightSibling = entries[(int)entry.RightSiblingSid];
            }
        }

        /// <summary>
        /// Searches for first matching entry by its name
        /// </summary>
        /// <param name="entryName">String name of entry</param>
        /// <returns>Entry if found, null otherwise</returns>
        public XlsDirectoryEntry FindEntry(string entryName)
        {
            return _mEntries.FirstOrDefault(e => e.EntryName == entryName);
        }

        /// <summary>
        /// Returns all entries in Root Directory
        /// </summary>
        public ReadOnlyCollection<XlsDirectoryEntry> Entries
        {
            get { return _mEntries.AsReadOnly(); }
        }

        /// <summary>
        /// Returns the Root Entry
        /// </summary>
        public XlsDirectoryEntry RootEntry
        {
            get { return _mRoot; }
        }

    }

    /// <summary>
    /// Represents single Root Directory record
    /// </summary>
    internal class XlsDirectoryEntry
    {

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bytes">byte array representing current object</param>
        public XlsDirectoryEntry(byte[] bytes)
        {
            if (bytes.Length < Length)
                throw new InvalidDataException("Oops! Array is too small.");
            _mBytes = bytes;
        }

        private readonly byte[] _mBytes;

        /// <summary>
        /// Length of structure in bytes
        /// </summary>
        public const int Length = 0x80;

        /// <summary>
        /// Returns name of directory entry
        /// </summary>
        public string EntryName
        {
            get { return Encoding.Unicode.GetString(_mBytes, 0x0, EntryLength).TrimEnd('\0'); }
        }

        /// <summary>
        /// Returns size in bytes of entry's name (with terminating zero)
        /// </summary>
        public ushort EntryLength
        {
            get { return BitConverter.ToUInt16(_mBytes, 0x40); }
        }

        /// <summary>
        /// Returns entry type
        /// </summary>
        public Stgty EntryType
        {
            get { return (Stgty)Buffer.GetByte(_mBytes, 0x42); }
        }

        /// <summary>
        /// Retuns entry "color" in directory tree
        /// </summary>
        public Decolor EntryColor
        {
            get { return (Decolor)Buffer.GetByte(_mBytes, 0x43); }
        }

        /// <summary>
        /// Returns SID of left sibling
        /// </summary>
        /// <remarks>0xFFFFFFFF if there's no one</remarks>
        public uint LeftSiblingSid
        {
            get { return BitConverter.ToUInt32(_mBytes, 0x44); }
        }

        private XlsDirectoryEntry _mLeftSibling;

        /// <summary>
        /// Returns left sibling
        /// </summary>
        public XlsDirectoryEntry LeftSibling
        {
            get { return _mLeftSibling; }
            set { if (_mLeftSibling == null) _mLeftSibling = value; }
        }

        /// <summary>
        /// Returns SID of right sibling
        /// </summary>
        /// <remarks>0xFFFFFFFF if there's no one</remarks>
        public uint RightSiblingSid
        {
            get { return BitConverter.ToUInt32(_mBytes, 0x48); }
        }

        private XlsDirectoryEntry _mRightSibling;

        /// <summary>
        /// Returns right sibling
        /// </summary>
        public XlsDirectoryEntry RightSibling
        {
            get { return _mRightSibling; }
            set { if (_mRightSibling == null) _mRightSibling = value; }
        }

        /// <summary>
        /// Returns SID of first child (if EntryType is STGTY_STORAGE)
        /// </summary>
        /// <remarks>0xFFFFFFFF if there's no one</remarks>
        public uint ChildSid
        {
            get { return BitConverter.ToUInt32(_mBytes, 0x4C); }
        }

        private XlsDirectoryEntry _mChild;

        /// <summary>
        /// Returns child
        /// </summary>
        public XlsDirectoryEntry Child
        {
            get { return _mChild; }
            set { if (_mChild == null) _mChild = value; }
        }

        /// <summary>
        /// CLSID of container (if EntryType is STGTY_STORAGE)
        /// </summary>
        public Guid ClassId
        {
            get { var tmp = new byte[16]; Buffer.BlockCopy(_mBytes, 0x50, tmp, 0, 16); return new Guid(tmp); }
        }

        /// <summary>
        /// Returns user flags of container (if EntryType is STGTY_STORAGE)
        /// </summary>
        public uint UserFlags
        {
            get { return BitConverter.ToUInt32(_mBytes, 0x60); }
        }

        /// <summary>
        /// Returns creation time of entry
        /// </summary>
        public DateTime CreationTime
        {
            get { return DateTime.FromFileTime(BitConverter.ToInt64(_mBytes, 0x64)); }
        }

        /// <summary>
        /// Returns last modification time of entry
        /// </summary>
        public DateTime LastWriteTime
        {
            get { return DateTime.FromFileTime(BitConverter.ToInt64(_mBytes, 0x6C)); }
        }

        /// <summary>
        /// First sector of data stream (if EntryType is STGTY_STREAM)
        /// </summary>
        /// <remarks>if EntryType is STGTY_ROOT, this can be first sector of MiniStream</remarks>
        public uint StreamFirstSector
        {
            get { return BitConverter.ToUInt32(_mBytes, 0x74); }
        }

        /// <summary>
        /// Size of data stream (if EntryType is STGTY_STREAM)
        /// </summary>
        /// <remarks>if EntryType is STGTY_ROOT, this can be size of MiniStream</remarks>
        public uint StreamSize
        {
            get { return BitConverter.ToUInt32(_mBytes, 0x78); }
        }

        /// <summary>
        /// Reserved, must be 0
        /// </summary>
        public uint PropType
        {
            get { return BitConverter.ToUInt32(_mBytes, 0x7C); }
        }

    }

    /// <summary>
    /// Represents a BIFF stream
    /// </summary>
    internal class XlsBiffStream : XlsStream
    {
        private readonly byte[] _bytes;
        private int _mOffset;
        private readonly int _mSize;

        public XlsBiffStream(XlsHeader hdr, uint streamStart)
            : base(hdr, streamStart)
        {
            _bytes = base.ReadStream();
            _mSize = _bytes.Length;
            _mOffset = 0;
        }

        /// <summary>
        /// Always returns null, use biff-specific methods
        /// </summary>
        /// <returns></returns>
        [Obsolete("Use BIFF-specific methods for this stream")]
        public new byte[] ReadStream()
        {
            return _bytes;
        }

        /// <summary>
        /// Returns size of BIFF stream in bytes
        /// </summary>
        public int Size
        {
            get { return _mSize; }
        }

        /// <summary>
        /// Returns current position in BIFF stream
        /// </summary>
        public int Position
        {
            get { return _mOffset; }
        }

        /// <summary>
        /// Sets stream pointer to the specified offset
        /// </summary>
        /// <param name="offset">Offset value</param>
        /// <param name="origin">Offset origin</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Seek(int offset, SeekOrigin origin)
        {
            switch (origin)
            {
                case SeekOrigin.Begin:
                    _mOffset = offset;
                    break;
                case SeekOrigin.Current:
                    _mOffset += offset;
                    break;
                case SeekOrigin.End:
                    _mOffset = _mSize - offset;
                    break;
            }
            if (_mOffset < 0)
                throw new IndexOutOfRangeException("Oops! Moving before stream start");
            if (_mOffset > _mSize)
                throw new IndexOutOfRangeException("Oops! Moving after stream end");
        }

        /// <summary>
        /// Reads record under cursor and advances cursor position to next record
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public XlsBiffRecord Read()
        {
            XlsBiffRecord rec = XlsBiffRecord.GetRecord(_bytes, (uint)_mOffset);
            _mOffset += rec.Size;
            if (_mOffset > _mSize)
                return null;
            return rec;
        }

        /// <summary>
        /// Reads record at specified offset, does not change cursor position
        /// </summary>
        /// <param name="offset"></param>
        /// <returns></returns>
        public XlsBiffRecord ReadAt(int offset)
        {
            XlsBiffRecord rec = XlsBiffRecord.GetRecord(_bytes, (uint)offset);
            if (_mOffset + rec.Size > _mSize)
                return null;
            return rec;
        }

    }

    /// <summary>
    /// Represents basic BIFF record
    /// Base class for all BIFF record types
    /// </summary>
    internal class XlsBiffRecord
    {
        protected byte[] MBytes;
        protected int MReadoffset;

        /// <summary>
        /// Basic entry constructor
        /// </summary>
        /// <param name="bytes">array representing this entry</param>
        protected XlsBiffRecord(byte[] bytes) : this(bytes, 0) { }

        protected XlsBiffRecord(byte[] bytes, uint offset)
        {
            if (bytes.Length - offset < 4)
                throw new InvalidDataException("Oops! Buffer size is less than minimum BIFF record size");
            MBytes = bytes;
            MReadoffset = (int)(4 + offset);
            if (bytes.Length < offset + Size)
                throw new InvalidDataException("Oops! Buffer size is less than entry length.");
        }

        /// <summary>
        /// Returns record at specified offset
        /// </summary>
        /// <param name="bytes">byte array</param>
        /// <param name="offset">position in array</param>
        /// <returns></returns>
        public static XlsBiffRecord GetRecord(byte[] bytes, uint offset)
        {
            uint id = BitConverter.ToUInt16(bytes, (int)offset);
            switch ((Biffrecordtype)id)
            {
                case Biffrecordtype.BofV2:
                case Biffrecordtype.BofV3:
                case Biffrecordtype.BofV4:
                case Biffrecordtype.Bof:
                    return new XlsBiffBof(bytes, offset);
                case Biffrecordtype.Eof:
                    return new XlsBiffEof(bytes, offset);
                case Biffrecordtype.Interfacehdr:
                    return new XlsBiffInterfaceHdr(bytes, offset);

                case Biffrecordtype.Sst:
                    return new XlsBiffSst(bytes, offset);

                case Biffrecordtype.Index:
                    return new XlsBiffIndex(bytes, offset);
                case Biffrecordtype.Row:
                    return new XlsBiffRow(bytes, offset);
                case Biffrecordtype.Dbcell:
                    return new XlsBiffDbCell(bytes, offset);

                case Biffrecordtype.Blank:
                case Biffrecordtype.BlankOld:
                    return new XlsBiffBlankCell(bytes, offset);
                case Biffrecordtype.Mulblank:
                    return new XlsBiffMulBlankCell(bytes, offset);
                case Biffrecordtype.Label:
                case Biffrecordtype.LabelOld:
                case Biffrecordtype.Rstring:
                    return new XlsBiffLabelCell(bytes, offset);
                case Biffrecordtype.Labelsst:
                    return new XlsBiffLabelSstCell(bytes, offset);
                case Biffrecordtype.Integer:
                case Biffrecordtype.IntegerOld:
                    return new XlsBiffIntegerCell(bytes, offset);
                case Biffrecordtype.Number:
                case Biffrecordtype.NumberOld:
                    return new XlsBiffNumberCell(bytes, offset);
                case Biffrecordtype.Rk:
                    return new XlsBiffRkCell(bytes, offset);
                case Biffrecordtype.Mulrk:
                    return new XlsBiffMulRkCell(bytes, offset);
                case Biffrecordtype.Formula:
                case Biffrecordtype.FormulaOld:
                    return new XlsBiffFormulaCell(bytes, offset);
                case Biffrecordtype.String:
                    return new XlsBiffFormulaString(bytes, offset);
                case Biffrecordtype.Continue:
                    return new XlsBiffContinue(bytes, offset);
                case Biffrecordtype.Dimensions:
                    return new XlsBiffDimensions(bytes, offset);
                case Biffrecordtype.Boundsheet:
                    return new XlsBiffBoundSheet(bytes, offset);
                case Biffrecordtype.Window1:
                    return new XlsBiffWindow1(bytes, offset);
                case Biffrecordtype.Codepage:
                    return new XlsBiffSimpleValueRecord(bytes, offset);
                case Biffrecordtype.Fngroupcount:
                    return new XlsBiffSimpleValueRecord(bytes, offset);
                case Biffrecordtype.Record1904:
                    return new XlsBiffSimpleValueRecord(bytes, offset);
                case Biffrecordtype.Bookbool:
                    return new XlsBiffSimpleValueRecord(bytes, offset);
                case Biffrecordtype.Backup:
                    return new XlsBiffSimpleValueRecord(bytes, offset);
                case Biffrecordtype.Hideobj:
                    return new XlsBiffSimpleValueRecord(bytes, offset);
                case Biffrecordtype.Useselfs:
                    return new XlsBiffSimpleValueRecord(bytes, offset);

                default:
                    return new XlsBiffRecord(bytes, offset);
            }

        }

        internal byte[] Bytes
        {
            get { return MBytes; }
        }

        internal int Offset
        {
            get { return MReadoffset - 4; }
        }

        /// <summary>
        /// Returns type ID of this entry
        /// </summary>
        public Biffrecordtype Id
        {
            get { return (Biffrecordtype)BitConverter.ToUInt16(MBytes, MReadoffset - 4); }
        }

        /// <summary>
        /// Returns data size of this entry
        /// </summary>
        public ushort RecordSize
        {
            get { return BitConverter.ToUInt16(MBytes, MReadoffset - 2); }
        }

        /// <summary>
        /// Returns whole size of structure
        /// </summary>
        public int Size
        {
            get { return 4 + RecordSize; }
        }

        public byte ReadByte(int offset)
        {
            return Buffer.GetByte(MBytes, MReadoffset + offset);
        }

        public ushort ReadUInt16(int offset)
        {
            return BitConverter.ToUInt16(MBytes, MReadoffset + offset);
        }

        public uint ReadUInt32(int offset)
        {
            return BitConverter.ToUInt32(MBytes, MReadoffset + offset);
        }

        public ulong ReadUInt64(int offset)
        {
            return BitConverter.ToUInt64(MBytes, MReadoffset + offset);
        }

        public short ReadInt16(int offset)
        {
            return BitConverter.ToInt16(MBytes, MReadoffset + offset);
        }

        public int ReadInt32(int offset)
        {
            return BitConverter.ToInt32(MBytes, MReadoffset + offset);
        }

        public long ReadInt64(int offset)
        {
            return BitConverter.ToInt64(MBytes, MReadoffset + offset);
        }

        public byte[] ReadArray(int offset, int size)
        {
            var tmp = new byte[size];
            Buffer.BlockCopy(MBytes, MReadoffset + offset, tmp, 0, size);
            return tmp;
        }

        public float ReadFloat(int offset)
        {
            return BitConverter.ToSingle(MBytes, MReadoffset + offset);
        }

        public double ReadDouble(int offset)
        {
            return BitConverter.ToDouble(MBytes, MReadoffset + offset);
        }

    }

    /// <summary>
    /// Represents BIFF BOF record
    /// </summary>
    internal class XlsBiffBof : XlsBiffRecord
    {

        internal XlsBiffBof(byte[] bytes) : this(bytes, 0) { }
        internal XlsBiffBof(byte[] bytes, uint offset) : base(bytes, offset) { }

        /// <summary>
        /// Version
        /// </summary>
        public ushort Version
        {
            get { return ReadUInt16(0x0); }
        }

        /// <summary>
        /// Type of BIFF block
        /// </summary>
        public Bifftype Type
        {
            get { return (Bifftype)ReadUInt16(0x2); }
        }

        /// <summary>
        /// Creation ID
        /// </summary>
        /// <remarks>Not used before BIFF5</remarks>
        public ushort CreationId
        {
            get
            {
                if (RecordSize < 6) return 0;
                return ReadUInt16(0x4);
            }
        }

        /// <summary>
        /// Creation year
        /// </summary>
        /// <remarks>Not used before BIFF5</remarks>
        public ushort CreationYear
        {
            get
            {
                if (RecordSize < 8) return 0;
                return ReadUInt16(0x6);
            }
        }

        /// <summary>
        /// File history flag
        /// </summary>
        /// <remarks>Not used before BIFF8</remarks>
        public uint HistoryFlag
        {
            get
            {
                if (RecordSize < 12) return 0;
                return ReadUInt32(0x8);
            }
        }

        /// <summary>
        /// Minimum Excel version to open this file
        /// </summary>
        /// <remarks>Not used before BIFF8</remarks>
        public uint MinVersionToOpen
        {
            get
            {
                if (RecordSize < 16) return 0;
                return ReadUInt32(0xC);
            }
        }

    }

    /// <summary>
    /// Represents BIFF EOF resord
    /// </summary>
    internal class XlsBiffEof : XlsBiffRecord
    {
        internal XlsBiffEof(byte[] bytes, uint offset) : base(bytes, offset) { }
        internal XlsBiffEof(byte[] bytes) : this(bytes, 0) { }
    }

    /// <summary>
    /// Represents record with the only two-bytes value
    /// </summary>
    internal class XlsBiffSimpleValueRecord : XlsBiffRecord
    {
        internal XlsBiffSimpleValueRecord(byte[] bytes, uint offset) : base(bytes, offset) { }
        internal XlsBiffSimpleValueRecord(byte[] bytes) : this(bytes, 0) { }

        /// <summary>
        /// Returns value
        /// </summary>
        public ushort Value
        {
            get { return ReadUInt16(0x0); }
        }
    }

    /// <summary>
    /// Represents InterfaceHdr record in Wokrbook Globals
    /// </summary>
    internal class XlsBiffInterfaceHdr : XlsBiffRecord
    {
        internal XlsBiffInterfaceHdr(byte[] bytes, uint offset) : base(bytes, offset) { }
        internal XlsBiffInterfaceHdr(byte[] bytes) : this(bytes, 0) { }

        /// <summary>
        /// Returns CodePage for Interface Header
        /// </summary>
        public ushort CodePage
        {
            get { return ReadUInt16(0x0); }
        }
    }

    /// <summary>
    /// Represents Dimensions of worksheet
    /// </summary>
    internal class XlsBiffDimensions : XlsBiffRecord
    {
        internal XlsBiffDimensions(byte[] bytes, uint offset) : base(bytes, offset) { }
        internal XlsBiffDimensions(byte[] bytes) : this(bytes, 0) { }

        private bool _isV8 = true;

        /// <summary>
        /// Gets or sets if BIFF8 addressing is used
        /// </summary>
        public bool IsV8
        {
            get { return _isV8; }
            set { _isV8 = value; }
        }

        /// <summary>
        /// Index of first row
        /// </summary>
        public uint FirstRow
        {
            get { return (_isV8) ? ReadUInt32(0x0) : ReadUInt16(0x0); }
        }

        /// <summary>
        /// Index of last row + 1
        /// </summary>
        public uint LastRow
        {
            get { return (_isV8) ? ReadUInt32(0x4) : ReadUInt16(0x2); }
        }

        /// <summary>
        /// Index of first column
        /// </summary>
        public ushort FirstColumn
        {
            get { return (_isV8) ? ReadUInt16(0x8) : ReadUInt16(0x4); }
        }

        /// <summary>
        /// Index of last column + 1
        /// </summary>
        public ushort LastColumn
        {
            get { return (_isV8) ? ReadUInt16(0x10) : ReadUInt16(0x6); }
        }

    }

    /// <summary>
    /// Represents a worksheet index
    /// </summary>
    internal class XlsBiffIndex : XlsBiffRecord
    {
        internal XlsBiffIndex(byte[] bytes, uint offset) : base(bytes, offset) { }
        internal XlsBiffIndex(byte[] bytes) : this(bytes, 0) { }

        private bool _isV8 = true;

        /// <summary>
        /// Gets or sets if BIFF8 addressing is used
        /// </summary>
        public bool IsV8
        {
            get { return _isV8; }
            set { _isV8 = value; }
        }

        /// <summary>
        /// Returns zero-based index of first existing row
        /// </summary>
        public uint FirstExistingRow
        {
            get { return (_isV8) ? ReadUInt32(0x4) : ReadUInt16(0x4); }
        }

        /// <summary>
        /// Returns zero-based index of last existing row
        /// </summary>
        public uint LastExistingRow
        {
            get { return (_isV8) ? ReadUInt32(0x8) : ReadUInt16(0x6); }
        }

        /// <summary>
        /// Returns addresses of DbCell records
        /// </summary>
        public uint[] DbCellAddresses
        {
            get
            {
                int size = RecordSize;
                int firstIdx = (_isV8) ? 16 : 12;
                if (size <= firstIdx)
                    return new uint[0];
                var cells = new List<uint>((size - firstIdx) / 4);
                for (int i = firstIdx; i < size; i += 4)
                    cells.Add(ReadUInt32(i));
                return cells.ToArray();
            }
        }

    }

    /// <summary>
    /// Represents a Shared String Table in BIFF8 format
    /// </summary>
    internal class XlsBiffSst : XlsBiffRecord
    {
        private readonly List<string> _mStrings;
        private uint _mSize;
        private readonly List<uint> _continues = new List<uint>();

        internal XlsBiffSst(byte[] bytes, uint offset)
            : base(bytes, offset)
        {
            _mSize = RecordSize;
            _mStrings = new List<string>((int)Count);
        }
        internal XlsBiffSst(byte[] bytes) : this(bytes, 0) { }

        /// <summary>
        /// Returns count of strings in SST
        /// </summary>
        public uint Count
        {
            get { return ReadUInt32(0x0); }
        }

        /// <summary>
        /// Returns count of unique strings in SST
        /// </summary>
        public uint UniqueCount
        {
            get { return ReadUInt32(0x4); }
        }

        /// <summary>
        /// Reads strings from BIFF stream into SST array
        /// </summary>
        public void ReadStrings()
        {
            uint offset = (uint)MReadoffset + 8;
            uint last = (uint)MReadoffset + RecordSize;
            int lastcontinue = 0;
            uint count = UniqueCount;
            while (offset < last)
            {
                var str = new XlsFormattedUnicodeString(MBytes, offset);
                uint prefix = str.HeadSize;
                uint postfix = str.TailSize;
                uint len = str.CharacterCount;
                uint size = prefix + postfix + len + ((str.IsMultiByte) ? len : 0);
                if (offset + size > last)
                {
                    if (lastcontinue >= _continues.Count)
                        break;
                    uint contoffset = _continues[lastcontinue];
                    byte encoding = Buffer.GetByte(MBytes, (int)contoffset + 4);
                    var buff = new byte[size * 2];
                    Buffer.BlockCopy(MBytes, (int)offset, buff, 0, (int)(last - offset));
                    if (encoding == 0 && str.IsMultiByte)
                    {
                        len -= (last - prefix - offset) / 2;
                        string temp = Encoding.Default.GetString(MBytes,
                                                                (int)contoffset + 5,
                                                                (int)len);
                        byte[] tempbytes = Encoding.Unicode.GetBytes(temp);
                        Buffer.BlockCopy(tempbytes, 0, buff, (int)(last - offset), tempbytes.Length);
                        Buffer.BlockCopy(MBytes, (int)(contoffset + 5 + len), buff, (int)(last - offset + len + len), (int)postfix);
                        offset = contoffset + 5 + len + postfix;
                    }
                    else if (encoding == 1 && str.IsMultiByte == false)
                    {
                        len -= (last - offset - prefix);
                        string temp = Encoding.Unicode.GetString(MBytes,
                                                                (int)contoffset + 5,
                                                                (int)(len + len));
                        byte[] tempbytes = Encoding.Default.GetBytes(temp);
                        Buffer.BlockCopy(tempbytes, 0, buff, (int)(last - offset), tempbytes.Length);
                        Buffer.BlockCopy(MBytes, (int)(contoffset + 5 + len + len), buff, (int)(last - offset + len), (int)postfix);
                        offset = contoffset + 5 + len + len + postfix;
                    }
                    else
                    {
                        Buffer.BlockCopy(MBytes, (int)contoffset + 5, buff, (int)(last - offset), (int)(size - last + offset));
                        offset = contoffset + 5 + size - last + offset;
                    }
                    last = contoffset + 4 + BitConverter.ToUInt16(MBytes, (int)contoffset + 2);
                    lastcontinue++;

                    str = new XlsFormattedUnicodeString(buff, 0);
                }
                else
                {
                    offset += size;
                    if (offset == last)
                    {
                        if (lastcontinue < _continues.Count)
                        {
                            uint contoffset = _continues[lastcontinue];
                            offset = contoffset + 4;
                            last = offset + BitConverter.ToUInt16(MBytes, (int)contoffset + 2);
                            lastcontinue++;
                        }
                        else
                            count = 1;
                    }
                }
                _mStrings.Add(str.Value);
                count--;
                if (count == 0)
                    break;
            }
        }

        /// <summary>
        /// Returns string at specified index
        /// </summary>
        /// <param name="sstIndex">Index of string to get</param>
        /// <returns>string value if it was found, empty string otherwise</returns>
        public string GetString(uint sstIndex)
        {
            if (sstIndex < _mStrings.Count)
                return _mStrings[(int)sstIndex];
            return "NOT FOUND #" + sstIndex;// string.Empty;
        }

        /// <summary>
        /// Appends Continue record to SST
        /// </summary>
        /// <param name="fragment">Continue record</param>
        public void Append(XlsBiffContinue fragment)
        {
            _continues.Add((uint)fragment.Offset);
            _mSize += (uint)fragment.Size;
        }

    }

    /// <summary>
    /// Represents formatted unicode string in SST
    /// </summary>
    internal class XlsFormattedUnicodeString
    {
        protected byte[] MBytes;
        protected uint MOffset;

        [Flags]
        public enum FormattedUnicodeStringFlags : byte
        {
            MultiByte = 0x01,
            HasExtendedString = 0x04,
            HasFormatting = 0x08,
        }

        public XlsFormattedUnicodeString(byte[] bytes, uint offset)
        {
            MBytes = bytes;
            MOffset = offset;
        }

        /// <summary>
        /// Count of characters in string
        /// </summary>
        public ushort CharacterCount
        {
            get { return BitConverter.ToUInt16(MBytes, (int)MOffset); }
        }

        /// <summary>
        /// String flags
        /// </summary>
        public FormattedUnicodeStringFlags Flags
        {
            get { return (FormattedUnicodeStringFlags)Buffer.GetByte(MBytes, (int)MOffset + 2); }
        }

        /// <summary>
        /// Checks if string has Extended record
        /// </summary>
        public bool HasExtString
        {
            get { return false; } // ((Flags & FormattedUnicodeStringFlags.HasExtendedString) == FormattedUnicodeStringFlags.HasExtendedString); }
        }

        /// <summary>
        /// Checks if string has formatting
        /// </summary>
        public bool HasFormatting
        {
            get { return ((Flags & FormattedUnicodeStringFlags.HasFormatting) == FormattedUnicodeStringFlags.HasFormatting); }
        }

        /// <summary>
        /// Checks if string is unicode
        /// </summary>
        public bool IsMultiByte
        {
            get { return ((Flags & FormattedUnicodeStringFlags.MultiByte) == FormattedUnicodeStringFlags.MultiByte); }
        }

        /// <summary>
        /// Returns length of string in bytes
        /// </summary>
        private uint ByteCount
        {
            get { return (uint)(CharacterCount * ((IsMultiByte) ? 2 : 1)); }
        }

        /// <summary>
        /// Returns number of formats used for formatting (0 if string has no formatting)
        /// </summary>
        public ushort FormatCount
        {
            get
            {
                return (HasFormatting) ? BitConverter.ToUInt16(MBytes, (int)MOffset + 3) : (ushort)0;
            }
        }

        /// <summary>
        /// Returns size of extended string in bytes, 0 if there is no one
        /// </summary>
        public uint ExtendedStringSize
        {
            get { return (HasExtString) ? (uint)BitConverter.ToUInt16(MBytes, (int)MOffset + ((HasFormatting) ? 5 : 3)) : 0; }
        }

        /// <summary>
        /// Returns head (before string data) size in bytes
        /// </summary>
        public uint HeadSize
        {
            get { return (uint)((HasFormatting) ? 2 : 0) + (uint)((HasExtString) ? 4 : 0) + 3; }
        }

        /// <summary>
        /// Returns tail (after string data) size in bytes
        /// </summary>
        public uint TailSize
        {
            get { return (uint)((HasFormatting) ? 4 * FormatCount : 0) + ((HasExtString) ? ExtendedStringSize : 0); }
        }

        /// <summary>
        /// Returns size of whole record in bytes
        /// </summary>
        public uint Size
        {
            get
            {
                uint extraSize = (uint)((HasFormatting) ? (2 + FormatCount * 4) : 0) + ((HasExtString) ? (4 + ExtendedStringSize) : 0) + 3;
                if (!IsMultiByte)
                    return extraSize + CharacterCount;
                return extraSize + (uint)CharacterCount * 2;
            }
        }

        /// <summary>
        /// Returns string represented by this instance
        /// </summary>
        public string Value
        {
            get
            {
                if (IsMultiByte)
                    return Encoding.Unicode.GetString(MBytes, (int)(MOffset + HeadSize), (int)ByteCount);
                return Encoding.Default.GetString(MBytes, (int)(MOffset + HeadSize), (int)ByteCount);
            }
        }

    }

    /// <summary>
    /// Represents additional space for very large records
    /// </summary>
    internal class XlsBiffContinue : XlsBiffRecord
    {
        internal XlsBiffContinue(byte[] bytes, uint offset) : base(bytes, offset) { }
        internal XlsBiffContinue(byte[] bytes) : this(bytes, 0) { }
    }

    /// <summary>
    /// Represents row record in table
    /// </summary>
    internal class XlsBiffRow : XlsBiffRecord
    {
        internal XlsBiffRow(byte[] bytes, uint offset) : base(bytes, offset) { }
        internal XlsBiffRow(byte[] bytes) : this(bytes, 0) { }

        /// <summary>
        /// Zero-based index of row described
        /// </summary>
        public ushort RowIndex
        {
            get { return ReadUInt16(0x0); }
        }

        /// <summary>
        /// Index of first defined column
        /// </summary>
        public ushort FirstDefinedColumn
        {
            get { return ReadUInt16(0x2); }
        }

        /// <summary>
        /// Index of last defined column
        /// </summary>
        public ushort LastDefinedColumn
        {
            get { return ReadUInt16(0x4); }
        }

        /// <summary>
        /// Returns row height
        /// </summary>
        public uint RowHeight
        {
            get { return ReadUInt16(0x6); }
        }

        /// <summary>
        /// Returns row flags
        /// </summary>
        public ushort Flags
        {
            get { return ReadUInt16(0xC); }
        }

        /// <summary>
        /// Returns default format for this row
        /// </summary>
        public ushort XFormat
        {
            get { return ReadUInt16(0xE); }
        }

    }

    /// <summary>
    /// Represents cell-indexing record, finishes each row values block
    /// </summary>
    internal class XlsBiffDbCell : XlsBiffRecord
    {
        internal XlsBiffDbCell(byte[] bytes, uint offset) : base(bytes, offset) { }
        internal XlsBiffDbCell(byte[] bytes) : this(bytes, 0) { }

        /// <summary>
        /// Offset of first row linked with this record
        /// </summary>
        public int RowAddress
        {
            get { return (Offset - ReadInt32(0x0)); }
        }

        /// <summary>
        /// Addresses of cell values
        /// </summary>
        public uint[] CellAddresses
        {
            get
            {
                int a = RowAddress - 20;    // 20 assumed to be row structure size
                var tmp = new List<uint>();
                for (int i = 0x4; i < RecordSize; i += 4)
                    tmp.Add((uint)a + ReadUInt16(i));
                return tmp.ToArray();
            }
        }

    }

    /// <summary>
    /// Represents blank cell
    /// Base class for all cell types
    /// </summary>
    internal class XlsBiffBlankCell : XlsBiffRecord
    {
        internal XlsBiffBlankCell(byte[] bytes, uint offset) : base(bytes, offset) { }
        internal XlsBiffBlankCell(byte[] bytes) : this(bytes, 0) { }

        /// <summary>
        /// Zero-based index of row containing this cell
        /// </summary>
        public ushort RowIndex
        {
            get { return ReadUInt16(0x0); }
        }

        /// <summary>
        /// Zero-based index of column containing this cell
        /// </summary>
        public ushort ColumnIndex
        {
            get { return ReadUInt16(0x2); }
        }

        /// <summary>
        /// Format used for this cell
        /// </summary>
        public ushort XFormat
        {
            get { return ReadUInt16(0x4); }
        }

    }

    /// <summary>
    /// Represents a constant integer number in range 0..65535
    /// </summary>
    internal class XlsBiffIntegerCell : XlsBiffBlankCell
    {
        internal XlsBiffIntegerCell(byte[] bytes) : this(bytes, 0) { }
        internal XlsBiffIntegerCell(byte[] bytes, uint offset) : base(bytes, offset) { }

        /// <summary>
        /// Returns value of this cell
        /// </summary>
        public uint Value
        {
            get { return ReadUInt16(0x6); }
        }

    }

    /// <summary>
    /// Represents multiple Blank cell
    /// </summary>
    internal class XlsBiffMulBlankCell : XlsBiffBlankCell
    {
        internal XlsBiffMulBlankCell(byte[] bytes) : this(bytes, 0) { }
        internal XlsBiffMulBlankCell(byte[] bytes, uint offset) : base(bytes, offset) { }

        /// <summary>
        /// Returns format forspecified column, column must be between ColumnIndex and LastColumnIndex
        /// </summary>
        /// <param name="columnIdx">Index of column</param>
        /// <returns>Format</returns>
        public ushort GetXf(ushort columnIdx)
        {
            int ofs = 4 + 6 * (columnIdx - ColumnIndex);
            if (ofs > RecordSize - 2)
                return 0;
            return ReadUInt16(ofs);
        }

        /// <summary>
        /// Zero-based index of last described column
        /// </summary>
        public ushort LastColumnIndex
        {
            get { return ReadUInt16(RecordSize - 2); }
        }

    }

    /// <summary>
    /// Represents a floating-point number 
    /// </summary>
    internal class XlsBiffNumberCell : XlsBiffBlankCell
    {
        internal XlsBiffNumberCell(byte[] bytes) : this(bytes, 0) { }
        internal XlsBiffNumberCell(byte[] bytes, uint offset) : base(bytes, offset) { }

        /// <summary>
        /// Returns value of this cell
        /// </summary>
        public double Value
        {
            get { return ReadDouble(0x6); }
        }

    }

    /// <summary>
    /// Represents a string (max 255 bytes)
    /// </summary>
    internal class XlsBiffLabelCell : XlsBiffBlankCell
    {
        internal XlsBiffLabelCell(byte[] bytes) : this(bytes, 0) { }
        internal XlsBiffLabelCell(byte[] bytes, uint offset) : base(bytes, offset) { }

        private Encoding _mUseEncoding = Encoding.Default;

        /// <summary>
        /// Encoding used to deal with strings
        /// </summary>
        public Encoding UseEncoding
        {
            get { return _mUseEncoding; }
            set { _mUseEncoding = value; }
        }

        /// <summary>
        /// Length of string value
        /// </summary>
        public byte Length
        {
            get { return ReadByte(0x6); }
        }

        /// <summary>
        /// Returns value of this cell
        /// </summary>
        public string Value
        {
            get { return _mUseEncoding.GetString(ReadArray(0x8, Length * ((_mUseEncoding.IsSingleByte) ? 1 : 2))); }
        }

    }

    /// <summary>
    /// Represents an RK number cell
    /// </summary>
    internal class XlsBiffRkCell : XlsBiffBlankCell
    {
        internal XlsBiffRkCell(byte[] bytes) : this(bytes, 0) { }
        internal XlsBiffRkCell(byte[] bytes, uint offset) : base(bytes, offset) { }

        /// <summary>
        /// Returns value of this cell
        /// </summary>
        public double Value
        {
            get { return NumFromRk(ReadUInt32(0x6)); }
        }

        /// <summary>
        /// Decodes RK-encoded number
        /// </summary>
        /// <param name="rk">Encoded number</param>
        /// <returns></returns>
        public static double NumFromRk(uint rk)
        {
            double num;
            if ((rk & 0x2) == 0x2)
            {
                // int
                num = (int)(rk >> 2);
            }
            else
            {
                // hi words of IEEE num
                num = BitConverter.Int64BitsToDouble(((long)(rk & 0xfffffffc) << 32));
            }
            if ((rk & 0x1) == 0x1)
                num /= 100; // divide by 100
            return num;
        }

    }

    /// <summary>
    /// Represents multiple RK number cells
    /// </summary>
    internal class XlsBiffMulRkCell : XlsBiffBlankCell
    {
        internal XlsBiffMulRkCell(byte[] bytes) : this(bytes, 0) { }
        internal XlsBiffMulRkCell(byte[] bytes, uint offset) : base(bytes, offset) { }

        /// <summary>
        /// Returns format for specified column
        /// </summary>
        /// <param name="columnIdx">Index of column, must be between ColumnIndex and LastColumnIndex</param>
        /// <returns></returns>
        public ushort GetXf(ushort columnIdx)
        {
            int ofs = 4 + 6 * (columnIdx - ColumnIndex);
            if (ofs > RecordSize - 2)
                return 0;
            return ReadUInt16(ofs);
        }

        /// <summary>
        /// Returns value for specified column
        /// </summary>
        /// <param name="columnIdx">Index of column, must be between ColumnIndex and LastColumnIndex</param>
        /// <returns></returns>
        public double GetValue(ushort columnIdx)
        {
            int ofs = 6 + 6 * (columnIdx - ColumnIndex);
            if (ofs > RecordSize)
                return 0;
            return XlsBiffRkCell.NumFromRk(ReadUInt32(ofs));
        }

        /// <summary>
        /// Returns zero-based index of last described column
        /// </summary>
        public ushort LastColumnIndex
        {
            get { return ReadUInt16(RecordSize - 2); }
        }

    }

    /// <summary>
    /// Represents a string stored in SST
    /// </summary>
    internal class XlsBiffLabelSstCell : XlsBiffBlankCell
    {
        internal XlsBiffLabelSstCell(byte[] bytes) : this(bytes, 0) { }
        internal XlsBiffLabelSstCell(byte[] bytes, uint offset) : base(bytes, offset) { }

        /// <summary>
        /// Index of string in Shared String Table
        /// </summary>
        public uint SstIndex
        {
            get { return ReadUInt32(0x6); }
        }

        /// <summary>
        /// Returns text using specified SST
        /// </summary>
        /// <param name="sst">Shared String Table record</param>
        /// <returns></returns>
        public string Text(XlsBiffSst sst)
        {
            return sst.GetString(SstIndex);
        }

    }

    /// <summary>
    /// Represents a boolean or error value
    /// </summary>
    internal class XlsBiffBoolErrCell : XlsBiffBlankCell
    {
        internal XlsBiffBoolErrCell(byte[] bytes) : this(bytes, 0) { }
        internal XlsBiffBoolErrCell(byte[] bytes, uint offset) : base(bytes, offset) { }

        /// <summary>
        /// Gets code of error, if IsError is True
        /// </summary>
        public Formulaerror ErrorCode
        {
            get { return (Formulaerror)ReadByte(0x6); }
        }

        /// <summary>
        /// Gets boolean value, if IsError is False
        /// </summary>
        public bool Value
        {
            get { return ReadByte(0x6) != 0; }
        }

        /// <summary>
        /// Checks if value is error
        /// </summary>
        public bool IsError
        {
            get { return ReadByte(0x7) != 0; }
        }

    }

    /// <summary>
    /// Represents a string value of formula
    /// </summary>
    internal class XlsBiffFormulaString : XlsBiffRecord
    {
        internal XlsBiffFormulaString(byte[] bytes) : this(bytes, 0) { }
        internal XlsBiffFormulaString(byte[] bytes, uint offset) : base(bytes, offset) { }

        private Encoding _mUseEncoding = Encoding.Default;

        /// <summary>
        /// Encoding used to deal with strings
        /// </summary>
        public Encoding UseEncoding
        {
            get { return _mUseEncoding; }
            set { _mUseEncoding = value; }
        }

        /// <summary>
        /// Length of the string
        /// </summary>
        public ushort Length
        {
            get { return ReadUInt16(0x0); }
        }

        /// <summary>
        /// String text
        /// </summary>
        public string Value
        {
            get { return _mUseEncoding.GetString(MBytes, MReadoffset + ((_mUseEncoding.IsSingleByte) ? 2 : 3), Length * ((_mUseEncoding.IsSingleByte) ? 1 : 2)); }
        }
    }

    /// <summary>
    /// Represents a cell containing formula
    /// </summary>
    internal class XlsBiffFormulaCell : XlsBiffNumberCell
    {
        internal XlsBiffFormulaCell(byte[] bytes) : this(bytes, 0) { }
        internal XlsBiffFormulaCell(byte[] bytes, uint offset) : base(bytes, offset) { }

        [Flags]
        public enum FormulaFlags : ushort
        {
            AlwaysCalc = 0x0001,
            CalcOnLoad = 0x0002,
            SharedFormulaGroup = 0x0008
        }

        private Encoding _mUseEncoding = Encoding.Default;

        /// <summary>
        /// Encoding used to deal with strings
        /// </summary>
        public Encoding UseEncoding
        {
            get { return _mUseEncoding; }
            set { _mUseEncoding = value; }
        }

        /// <summary>
        /// Formula flags
        /// </summary>
        public FormulaFlags Flags
        {
            get { return (FormulaFlags)(ReadUInt16(0xE)); }
        }

        /// <summary>
        /// Length of formula string
        /// </summary>
        public byte FormulaLength
        {
            get { return ReadByte(0xF); }
        }

        /// <summary>
        /// Returns type-dependent value of formula
        /// </summary>
        public new object Value
        {
            get
            {
                long val = ReadInt64(0x6);
                if (((ulong)val & 0xFFFF000000000000) == 0xFFFF000000000000)
                {
                    var type = (byte)(val & 0xFF);
                    var code = (byte)((val >> 16) & 0xFF);
                    switch (type)
                    {
                        case 0:     // String
                            var str = GetRecord(MBytes, (uint)(Offset + Size)) as XlsBiffFormulaString;
                            if (str == null)
                                return string.Empty;
                            str.UseEncoding = _mUseEncoding;
                            return str.Value;
                        case 1:     // Boolean
                            return code != 0;
                        case 2:     // Error
                            return (Formulaerror)code;
                        default:
                            return null;
                    }
                }
                return BitConverter.Int64BitsToDouble(val);
            }
        }

        public string Formula
        {
            get { return Encoding.Default.GetString(ReadArray(0x10, FormulaLength)); }
        }

    }

    /// <summary>
    /// Represents Workbook's global window description
    /// </summary>
    internal class XlsBiffWindow1 : XlsBiffRecord
    {
        [Flags]
        public enum Window1Flags : ushort
        {
            Hidden = 0x1,
            Minimized = 0x2,
            //(Reserved) = 0x4,
            HScrollVisible = 0x8,
            VScrollVisible = 0x10,
            WorkbookTabs = 0x20
            //(Other bits are reserved)
        }

        internal XlsBiffWindow1(byte[] bytes, uint offset) : base(bytes, offset) { }
        internal XlsBiffWindow1(byte[] bytes) : this(bytes, 0) { }

        /// <summary>
        /// Returns X position of a window
        /// </summary>
        public ushort Left
        {
            get { return ReadUInt16(0x0); }
        }

        /// <summary>
        /// Returns Y position of a window
        /// </summary>
        public ushort Top
        {
            get { return ReadUInt16(0x2); }
        }

        /// <summary>
        /// Returns width of a window
        /// </summary>
        public ushort Width
        {
            get { return ReadUInt16(0x4); }
        }

        /// <summary>
        /// Returns height of a window
        /// </summary>
        public ushort Height
        {
            get { return ReadUInt16(0x6); }
        }

        /// <summary>
        /// Returns window flags
        /// </summary>
        public Window1Flags Flags
        {
            get { return (Window1Flags)ReadUInt16(0x8); }
        }

        /// <summary>
        /// Returns active workbook tab (zero-based)
        /// </summary>
        public ushort ActiveTab
        {
            get { return ReadUInt16(0xA); }
        }

        /// <summary>
        /// Returns first visible workbook tab (zero-based)
        /// </summary>
        public ushort FirstVisibleTab
        {
            get { return ReadUInt16(0xC); }
        }

        /// <summary>
        /// Returns number of selected workbook tabs
        /// </summary>
        public ushort SelectedTabCount
        {
            get { return ReadUInt16(0xE); }
        }

        /// <summary>
        /// Returns workbook tab width to horizontal scrollbar width
        /// </summary>
        public ushort TabRatio
        {
            get { return ReadUInt16(0x10); }
        }

    }

    /// <summary>
    /// Represents Sheet record in Workbook Globals
    /// </summary>
    internal class XlsBiffBoundSheet : XlsBiffRecord
    {
        public enum SheetType : byte
        {
            Worksheet = 0x0,
            MacroSheet = 0x1,
            Chart = 0x2,
            VbModule = 0x6
        }

        public enum SheetVisibility : byte
        {
            Visible = 0x0,
            Hidden = 0x1,
            VeryHidden = 0x2
        }

        internal XlsBiffBoundSheet(byte[] bytes, uint offset) : base(bytes, offset) { }
        internal XlsBiffBoundSheet(byte[] bytes) : this(bytes, 0) { }

        /// <summary>
        /// Worksheet data start offset
        /// </summary>
        public uint StartOffset
        {
            get { return ReadUInt32(0x0); }
        }

        /// <summary>
        /// Type of worksheet
        /// </summary>
        public SheetType Type
        {
            get { return (SheetType)ReadByte(0x4); }
        }

        /// <summary>
        /// Visibility of worksheet
        /// </summary>
        public SheetVisibility VisibleState
        {
            get { return (SheetVisibility)(ReadByte(0x5) & 0x3); }
        }

        /// <summary>
        /// Name of worksheet
        /// </summary>
        public string SheetName
        {
            get
            {
                ushort len = ReadByte(0x6);
                const int start = 0x8;
                if (_isV8)
                    return ReadByte(0x7) == 0 ? Encoding.Default.GetString(MBytes, MReadoffset + start, len) : _mUseEncoding.GetString(MBytes, MReadoffset + start, (_mUseEncoding.IsSingleByte) ? len : len * 2);
                return Encoding.Default.GetString(MBytes, MReadoffset + start - 1, len);
            }
        }

        private Encoding _mUseEncoding = Encoding.Default;

        /// <summary>
        /// Encoding used to deal with strings
        /// </summary>
        public Encoding UseEncoding
        {
            get { return _mUseEncoding; }
            set { _mUseEncoding = value; }
        }

        private bool _isV8 = true;

        /// <summary>
        /// Specifies if BIFF8 format should be used
        /// </summary>
        public bool IsV8
        {
            get { return _isV8; }
            set { _isV8 = value; }
        }

    }

    /// <summary>
    /// Represents Globals section of workbook
    /// </summary>
    internal class XlsWorkbookGlobals
    {
        public XlsBiffInterfaceHdr InterfaceHdr { get; set; }

        public XlsBiffRecord Mms { get; set; }

        public XlsBiffRecord WriteAccess { get; set; }

        public XlsBiffSimpleValueRecord CodePage { get; set; }

        public XlsBiffRecord Dsf { get; set; }

        public XlsBiffRecord Country { get; set; }

        public XlsBiffSimpleValueRecord Backup { get; set; }

        private readonly List<XlsBiffRecord> _mFonts = new List<XlsBiffRecord>();

        public List<XlsBiffRecord> Fonts
        {
            get { return _mFonts; }
        }

        private readonly List<XlsBiffRecord> _mFormats = new List<XlsBiffRecord>();

        public List<XlsBiffRecord> Formats
        {
            get { return _mFormats; }
        }

        private readonly List<XlsBiffRecord> _mExtendedFormats = new List<XlsBiffRecord>();

        public List<XlsBiffRecord> ExtendedFormats
        {
            get { return _mExtendedFormats; }
        }

        private readonly List<XlsBiffRecord> _mStyles = new List<XlsBiffRecord>();

        public List<XlsBiffRecord> Styles
        {
            get { return _mStyles; }
        }

        private readonly List<XlsBiffBoundSheet> _mSheets = new List<XlsBiffBoundSheet>();

        public List<XlsBiffBoundSheet> Sheets
        {
            get { return _mSheets; }
        }

        /// <summary>
        /// Shared String Table of workbook
        /// </summary>
        public XlsBiffSst Sst { get; set; }

        public XlsWorkbookGlobals()
        {
            Backup = null;
            Dsf = null;
            CodePage = null;
            WriteAccess = null;
            Mms = null;
            InterfaceHdr = null;
        }

        public XlsBiffRecord ExtSst { get; set; }
    }

    /// <summary>
    /// Represents Worksheet section in workbook
    /// </summary>
    internal class XlsWorksheet
    {
        private readonly int _mIndex;
        private readonly string _mName = string.Empty;
        private readonly uint _mDataOffset;

        public XlsWorksheet(int index, XlsBiffBoundSheet refSheet)
        {
            _mIndex = index;
            _mName = refSheet.SheetName;
            _mDataOffset = refSheet.StartOffset;
        }

        /// <summary>
        /// Name of worksheet
        /// </summary>
        public string Name
        {
            get { return _mName; }
        }

        /// <summary>
        /// Zero-based index of worksheet
        /// </summary>
        public int Index
        {
            get { return _mIndex; }
        }

        /// <summary>
        /// Offset of worksheet data
        /// </summary>
        public uint DataOffset
        {
            get { return _mDataOffset; }
        }

        /// <summary>
        /// DataTable with worksheet data
        /// </summary>
        public DataTable Data { get; set; }

        public XlsBiffSimpleValueRecord CalcMode { get; set; }

        public XlsBiffSimpleValueRecord CalcCount { get; set; }

        public XlsBiffSimpleValueRecord RefMode { get; set; }

        public XlsBiffSimpleValueRecord Iteration { get; set; }

        public XlsBiffRecord Delta { get; set; }

        /// <summary>
        /// Dimensions of worksheet
        /// </summary>
        public XlsBiffDimensions Dimensions { get; set; }

        public XlsBiffRecord Window2 { get; set; }
    }

}
