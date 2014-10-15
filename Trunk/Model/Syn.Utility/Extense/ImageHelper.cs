using System;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Web;
using Syn.Utility.Function;

namespace Syn.Utility.Extense
{
    /// <summary>
    /// 图片操作的通用方法
    /// </summary>
    public class ImageHelper
    {
        private Image _srcImage;
        private string _srcFileName;

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="fileName">原始图片路径</param>
        public bool SetImage(string fileName)
        {
            _srcFileName = FileHelper.GetMapPath(fileName);
            try
            {
                _srcImage = Image.FromFile(_srcFileName);
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 回调
        /// </summary>
        /// <returns></returns>
        public bool ThumbnailCallback()
        {
            return false;
        }

        /// <summary>
        /// 生成缩略图,返回缩略图的Image对象
        /// </summary>
        /// <param name="width">缩略图宽度</param>
        /// <param name="height">缩略图高度</param>
        /// <returns>缩略图的Image对象</returns>
        public Image GetImage(int width, int height)
        {
            var callb = new Image.GetThumbnailImageAbort(ThumbnailCallback);
            Image img = _srcImage.GetThumbnailImage(width, height, callb, IntPtr.Zero);
            return img;
        }

        /// <summary>
        /// 保存缩略图
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void SaveThumbnailImage(int width, int height)
        {
            switch (Path.GetExtension(_srcFileName).ToLower())
            {
                case ".png":
                    SaveImage(width, height, ImageFormat.Png);
                    break;
                case ".gif":
                    SaveImage(width, height, ImageFormat.Gif);
                    break;
                default:
                    SaveImage(width, height, ImageFormat.Jpeg);
                    break;
            }
        }

        /// <summary>
        /// 生成缩略图并保存
        /// </summary>
        /// <param name="width">缩略图的宽度</param>
        /// <param name="height">缩略图的高度</param>
        /// <param name="imgformat">保存的图像格式</param>
        /// <returns>缩略图的Image对象</returns>
        public void SaveImage(int width, int height, ImageFormat imgformat)
        {
            if ((_srcImage.Width > width) || (_srcImage.Height > height))
            {
                var callb = new Image.GetThumbnailImageAbort(ThumbnailCallback);
                Image img = _srcImage.GetThumbnailImage(width, height, callb, IntPtr.Zero);
                _srcImage.Dispose();
                img.Save(_srcFileName, imgformat);
                img.Dispose();
            }
        }

        #region Helper

        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="image">Image 对象</param>
        /// <param name="savePath">保存路径</param>
        /// <param name="ici">指定格式的编解码参数</param>
        private static void SaveImage(Image image, string savePath, ImageCodecInfo ici)
        {
            //设置 原图片 对象的 EncoderParameters 对象
            var parameters = new EncoderParameters(1);
            parameters.Param[0] = new EncoderParameter(Encoder.Quality, ((long)100));
            image.Save(savePath, ici, parameters);
            parameters.Dispose();
        }

        /// <summary>
        /// 获取图像编码解码器的所有相关信息
        /// </summary>
        /// <param name="mimeType">包含编码解码器的多用途网际邮件扩充协议 (MIME) 类型的字符串</param>
        /// <returns>返回图像编码解码器的所有相关信息</returns>
        private static ImageCodecInfo GetCodecInfo(string mimeType)
        {
            ImageCodecInfo[] codecInfo = ImageCodecInfo.GetImageEncoders();
            return codecInfo.FirstOrDefault(ici => ici.MimeType == mimeType);
        }

        /// <summary>
        /// 计算新尺寸
        /// </summary>
        /// <param name="width">原始宽度</param>
        /// <param name="height">原始高度</param>
        /// <param name="maxWidth">最大新宽度</param>
        /// <param name="maxHeight">最大新高度</param>
        /// <returns></returns>
        private static Size ResizeImage(int width, int height, int maxWidth, int maxHeight)
        {
            var MAX_WIDTH = (decimal)maxWidth;
            var MAX_HEIGHT = (decimal)maxHeight;
            var ASPECT_RATIO = MAX_WIDTH / MAX_HEIGHT;

            int newWidth, newHeight;

            var originalWidth = (decimal)width;
            var originalHeight = (decimal)height;

            if (originalWidth > MAX_WIDTH || originalHeight > MAX_HEIGHT)
            {
                decimal factor;
                // determine the largest factor 
                if (originalWidth / originalHeight > ASPECT_RATIO)
                {
                    factor = originalWidth / MAX_WIDTH;
                    newWidth = Convert.ToInt32(originalWidth / factor);
                    newHeight = Convert.ToInt32(originalHeight / factor);
                }
                else
                {
                    factor = originalHeight / MAX_HEIGHT;
                    newWidth = Convert.ToInt32(originalWidth / factor);
                    newHeight = Convert.ToInt32(originalHeight / factor);
                }
            }
            else
            {
                newWidth = width;
                newHeight = height;
            }

            return new Size(newWidth, newHeight);

        }

        /// <summary>
        /// 得到图片格式
        /// </summary>
        /// <param name="name">文件名称</param>
        /// <returns></returns>
        public static ImageFormat GetFormat(string name)
        {
            string ext = name.Substring(name.LastIndexOf(".") + 1);
            switch (ext.ToLower())
            {
                case "jpg":
                case "jpeg":
                    return ImageFormat.Jpeg;
                case "bmp":
                    return ImageFormat.Bmp;
                case "png":
                    return ImageFormat.Png;
                case "gif":
                    return ImageFormat.Gif;
                default:
                    return ImageFormat.Jpeg;
            }
        }
        #endregion

        /// <summary>
        /// 制作小正方形
        /// </summary>
        /// <param name="fileName">原图的文件路径</param>
        /// <param name="newFileName">新地址</param>
        /// <param name="newSize">长度或宽度</param>
        public static void MakeSquareImage(string fileName, string newFileName, int newSize)
        {
            Image image = Image.FromFile(fileName);

            int width = image.Width;
            int height = image.Height;
            if (width > height)
            {
            }
            var b = new Bitmap(newSize, newSize);

            try
            {
                Graphics g = Graphics.FromImage(b);
                g.InterpolationMode = InterpolationMode.High;
                g.SmoothingMode = SmoothingMode.HighQuality;

                //清除整个绘图面并以透明背景色填充
                g.Clear(Color.Transparent);
                g.DrawImage(image, new Rectangle(0, 0, newSize, newSize),
                            width < height
                                ? new Rectangle(0, (height - width)/2, width, width)
                                : new Rectangle((width - height)/2, 0, height, height), GraphicsUnit.Pixel);

                SaveImage(b, newFileName, GetCodecInfo("image/" + GetFormat(fileName).ToString().ToLower()));
            }
            finally
            {
                image.Dispose();
                b.Dispose();
            }

        }


        /// <summary>
        /// 制作缩略图
        /// </summary>
        /// <param name="fileName">原图路径</param>
        /// <param name="newFileName">新图路径</param>
        /// <param name="maxWidth">最大宽度</param>
        /// <param name="maxHeight">最大高度</param>
        public static void MakeThumbnailImage(string fileName, string newFileName, int maxWidth, int maxHeight)
        {
            Image original = Image.FromFile(fileName);

            Size newSize = ResizeImage(original.Width, original.Height, maxWidth, maxHeight);

            Image displayImage = new Bitmap(original, newSize);

            try
            {
                displayImage.Save(newFileName, GetFormat(fileName));
            }
            finally
            {
                original.Dispose();
            }

        }
        /// <summary>
        /// 判断图片的格式是否为CMYK
        /// </summary>
        /// <param name="img">System.Drawing.Image img</param>
        /// <returns>true or false</returns>
        public static bool IsCmyk(Image img)
        {
            string flagVals = GetImageFlags(img);
            return (flagVals.IndexOf("Ycck") > -1) || (flagVals.IndexOf("Cmyk") > -1);
        }
        public static string GetImageFlags(Image img)
        {
            var flagVals = (ImageFlags)Enum.Parse(typeof(ImageFlags), img.Flags.ToString());
            return flagVals.ToString();
        }

        public static Bitmap ConvertCmyk(Bitmap bmp)
        {
            var tmpBmp = new Bitmap(bmp.Width, bmp.Height, PixelFormat.Format24bppRgb);

            Graphics g = Graphics.FromImage(tmpBmp);
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            var rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            // 将CMYK图片重绘一遍,此时GDI+自动将CMYK格式转换为RGB了
            g.DrawImage(bmp, rect);

            var returnBmp = new Bitmap(tmpBmp);

            g.Dispose();
            tmpBmp.Dispose();
            bmp.Dispose();

            return returnBmp;
        }



        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="originalImagePath">源图路径（物理路径）</param>
        /// <param name="thumbnailPath">缩略图路径（物理路径）</param>
        /// <param name="width">缩略图宽度</param>
        /// <param name="height">缩略图高度</param>
        /// <param name="mode">
        /// 生成缩略图的方式\r\n
        /// HW --- 指定高宽缩放（可能变形）   
        /// W  --- 指定宽，高按比例 
        /// H  --- 指定高，宽按比例
        /// Cut --- 指定高宽裁减（不变形）   
        /// WORH --- 指定宽和高，原图按比例缩小，其余部分留白，不变形
        /// </param>    originalImage img bmp
        public static string MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, string mode)
        {
            originalImagePath = originalImagePath.Replace("&#39;", "\'").Replace("&#34;", "\"");
            thumbnailPath = thumbnailPath.Replace("&#39;", "\'").Replace("&#34;", "\"");
            // thumbnailPath = Utils.CutString(thumbnailPath.Replace("&#39;", "\'").Replace("&#34;", "\""), 0, thumbnailPath.Replace("&#39;", "\'").Replace("&#34;", "\"").LastIndexOf('.') + 1)+"gif";
            string result = string.Empty;
            Image originalImage = Image.FromFile(originalImagePath);
            Image img = new Bitmap(originalImagePath);
            //if (IsCMYK(img))
            //{
            //    Bitmap bmp = new Bitmap(originalImagePath);
            //    ConvertCMYK(bmp);
            //    bmp.Dispose();
            //}
            int towidth = width;
            int toheight = height;
            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;
            switch (mode)
            {
                case "HW"://指定高宽缩放（可能变形）                
                    break;
                case "W"://指定宽，高按比例                    
                    toheight = originalImage.Height * width / originalImage.Width;
                    break;
                case "H"://指定高，宽按比例
                    towidth = originalImage.Width * height / originalImage.Height;
                    break;
                case "Cut"://指定高宽裁减（不变形）                
                    if (originalImage.Width / (double)originalImage.Height > towidth / (double)toheight)
                    {
                        oh = originalImage.Height;
                        ow = originalImage.Height * towidth / toheight;
                        y = 0;
                        x = (originalImage.Width - ow) / 2;
                    }
                    else
                    {
                        ow = originalImage.Width;
                        oh = originalImage.Width * height / towidth;
                        x = 0;
                        y = (originalImage.Height - oh) / 2;
                    }
                    break;
                case "WORH":
                    {
                        if (originalImage.Width * height > width * originalImage.Height)
                            toheight = originalImage.Height * width / originalImage.Width;
                        else
                        {
                            towidth = originalImage.Width * height / originalImage.Height;
                        }
                    }
                    break;
                default:
                    break;
            }
            //新建一个bmp图片
            Image bitmap = new Bitmap(width, height);
            //bitmap.Palette = 
            //新建一个画板
            Graphics g = Graphics.FromImage(bitmap);

            //设置高质量插值法
            g.InterpolationMode = InterpolationMode.High;

            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = SmoothingMode.HighQuality;

            //清空画布并以透明背景色填充
            g.Clear(Color.White);
            g.DrawImage(originalImage, new Rectangle(0, 0, towidth, toheight),
               new Rectangle(x, y, ow, oh),
               GraphicsUnit.Pixel);
            try
            {
                //以jpg格式保存缩略图
                bitmap.Save(thumbnailPath, ImageFormat.Jpeg);
            }
            catch (Exception)
            {
                result = originalImagePath;
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
                img.Dispose();

            }

            return result;
        }
        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="newFile"></param>
        /// <param name="maxWidth"></param>
        /// <param name="maxHeight"></param>
        /// <param name="backgroundcolor"></param>
        /// <returns></returns>
        public static string ThumbnailImage(string fileName, string newFile, int maxWidth, int maxHeight, string backgroundcolor)
        {
            DateTime dt1 = DateTime.Now;

            string result;
            Image img = Image.FromFile(fileName);
            ImageFormat thisFormat = img.RawFormat;
            //缩略图尺寸
            double w;
            double h;
            double sw = Convert.ToDouble(img.Width);
            double sh = Convert.ToDouble(img.Height);
            double mw = Convert.ToDouble(maxWidth);
            double mh = Convert.ToDouble(maxHeight);
            if (sw < mw && sh < mh)
            {
                w = sw;
                h = sh;
            }
            else if ((sw / sh) > (mw / mh))
            {
                w = maxWidth;
                h = (w * sh) / sw;
            }
            else
            {
                h = maxHeight;
                w = (h * sw) / sh;
            }
            var newSize = new Size(Convert.ToInt32(w), Convert.ToInt32(h));
            //Bitmap outBmp = new Bitmap(newSize.Width, newSize.Height);
            var outBmp = new Bitmap(maxWidth, maxHeight);
            Graphics g = Graphics.FromImage(outBmp);
            //设置画布的描绘质量
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            //清空画布并以透明背景色填充
            g.Clear(ColorTranslator.FromHtml(backgroundcolor));//#E7F3F3  #fff
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(img, new Rectangle(0, 0, newSize.Width, newSize.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel);
            
            //以下代码为保存图片时,设置压缩质量
            var encoderParams = new EncoderParameters();
            var quality = new long[1];
            quality[0] = 100;
            var encoderParam = new EncoderParameter(Encoder.Quality, quality);
            encoderParams.Param[0] = encoderParam;
            //获得包含有关内置图像编码解码器的信息的ImageCodecInfo 对象.
            ImageCodecInfo[] arrayIci = ImageCodecInfo.GetImageEncoders();
            ImageCodecInfo jpegIci = arrayIci.FirstOrDefault(t => t.FormatDescription.Equals("JPEG"));
            try
            {
                if (jpegIci != null)
                {
                    outBmp.Save(newFile, jpegIci, encoderParams);
                }
                else
                {
                    outBmp.Save(newFile, thisFormat);
                }
                result = newFile;
            }
            catch(Exception)
            {                
                //string strRemark = "\n源路径：" + fileName;
                //strRemark += "\n目标路径：" + newFile;
                //Log.WriteErrorLog(ex, "", "公用的生成缩略图", strRemark, false);                
                
                result = "error";
            }
            finally
            {
                img.Dispose();
                g.Dispose();
                outBmp.Dispose();
            }
            DateTime dt2 = DateTime.Now;
            (dt2 - dt1).Milliseconds.Tostring();
            return result;
        }

        public static string MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, string mode, string backgroundcolor)
        {
            originalImagePath = originalImagePath.Replace("&#39;", "\'").Replace("&#34;", "\"");
            thumbnailPath = thumbnailPath.Replace("&#39;", "\'").Replace("&#34;", "\"");
            // thumbnailPath = Utils.CutString(thumbnailPath.Replace("&#39;", "\'").Replace("&#34;", "\""), 0, thumbnailPath.Replace("&#39;", "\'").Replace("&#34;", "\"").LastIndexOf('.') + 1)+"gif";
            string result = string.Empty;

            Image originalImage = Image.FromFile(originalImagePath);

            //System.Drawing.Image img = new Bitmap(originalImagePath);
            //if (IsCMYK(img))
            //{
            //    Bitmap bmp = new Bitmap(originalImagePath);
            //    ConvertCMYK(bmp);
            //    bmp.Dispose();
            //}
            int towidth = width;
            int toheight = height;
            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;
            switch (mode)
            {
                case "HW"://指定高宽缩放（可能变形）                
                    break;
                case "W"://指定宽，高按比例                    
                    toheight = originalImage.Height * width / originalImage.Width;
                    break;
                case "H"://指定高，宽按比例
                    towidth = originalImage.Width * height / originalImage.Height;
                    break;
                case "Cut"://指定高宽裁减（不变形）                
                    if (originalImage.Width / (double)originalImage.Height > towidth / (double)toheight)
                    {
                        oh = originalImage.Height;
                        ow = originalImage.Height * towidth / toheight;
                        y = 0;
                        x = (originalImage.Width - ow) / 2;
                    }
                    else
                    {
                        ow = originalImage.Width;
                        oh = originalImage.Width * height / towidth;
                        x = 0;
                        y = (originalImage.Height - oh) / 2;
                    }
                    break;
                case "WORH":
                    {
                        if (originalImage.Width * height > width * originalImage.Height)
                            toheight = originalImage.Height * width / originalImage.Width;
                        else
                        {
                            towidth = originalImage.Width * height / originalImage.Height;
                        }
                    }
                    break;
                default:
                    break;
            }
            //新建一个bmp图片
            Image bitmap = new Bitmap(width, height);

            //新建一个画板
            Graphics g = Graphics.FromImage(bitmap);

            //设置高质量插值法
            g.InterpolationMode = InterpolationMode.High;

            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = SmoothingMode.HighQuality;

            //清空画布并以透明背景色填充
            g.Clear(ColorTranslator.FromHtml(backgroundcolor));//#E7F3F3  #fff
            //在指定位置并且按指定大小绘制原图片的指定部分
            //g.DrawImage(originalImage, new Rectangle(0, , 
            //g.DrawImage(originalImage, new Rectangle((width - towidth) / 2, (height - toheight) / 2, towidth, toheight),
            //new Rectangle(x, y, ow, oh),
            //GraphicsUnit.Pixel);

            g.DrawImage(originalImage, new Rectangle(0, 0, towidth, toheight),
               new Rectangle(x, y, ow, oh),
               GraphicsUnit.Pixel);

            try
            {
                //以jpg格式保存缩略图
                bitmap.Save(thumbnailPath, ImageFormat.Jpeg);
            }
            catch (Exception)
            {
                result = originalImagePath;
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
                //img.Dispose();

            }

            return result;
        }

        /// <summary>
        /// 加图片水印
        /// </summary>
        /// <param name="filePath">需要加水印的图片路径</param>
        /// <param name="type">水印位置   1左上;2上中;3右上;4中左;5中;6中右;7左下;8下中;9右下</param>
        /// <param name="waterFile">水印图片地址</param>
        /// <returns></returns>
        public string MarkWater(string filePath, int type, string waterFile)
        {
            try
            {
                string filename1 = "t" + filePath.CutString(filePath.LastIndexOf('/') + 1, filePath.Length);
                string filepaths = filePath.CutString(0, filePath.LastIndexOf('/') + 1);
                Image image = Image.FromFile(HttpContext.Current.Request.MapPath(filePath));
                Image shuiyinImg = Image.FromFile(waterFile);
                Graphics g = Graphics.FromImage(image);
                int rectX = 0;
                int rectY = 0;

                #region 判断水印位置
                switch (type)
                {
                    case 1:
                        rectX = 0;
                        break;
                    case 2:
                        rectX = (image.Width - shuiyinImg.Width) / 2;
                        break;
                    case 3:
                        rectX = image.Width - shuiyinImg.Width;
                        break;
                    case 4:
                        rectY = (image.Height - shuiyinImg.Height) / 2;
                        break;
                    case 5:
                        rectX = (image.Width - shuiyinImg.Width) / 2;
                        rectY = (image.Height - shuiyinImg.Height) / 2;
                        break;
                    case 6:
                        rectX = image.Width - shuiyinImg.Width;
                        rectY = (image.Height - shuiyinImg.Height) / 2;
                        break;
                    case 7:
                        rectY = image.Height - shuiyinImg.Height;
                        break;
                    case 8:
                        rectX = (image.Width - shuiyinImg.Width) / 2;
                        rectY = image.Height - shuiyinImg.Height;
                        break;
                    case 9:
                        rectX = image.Width - shuiyinImg.Width;
                        rectY = image.Height - shuiyinImg.Height;
                        break;
                    default:
                        rectX = 0;
                        break;
                }
                #endregion

                g.DrawImage(shuiyinImg, new Rectangle(rectX, rectY, shuiyinImg.Width, shuiyinImg.Height), 0, 0, shuiyinImg.Width, shuiyinImg.Height, GraphicsUnit.Pixel);
                g.Dispose();
                image.Save(FileHelper.GetMapPath(filepaths + filename1));//保存的地址
                image.Dispose();
                return filepaths + filename1;
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 加文字水印
        /// </summary>
        /// <param name="fileName">需要加水印的图片路径</param>
        /// <param name="type">水印位置   1左上;2上中;3右上;4中左;5中;6中右;7左下;8下中;9右下</param>
        /// <param name="text">水印文字内容</param>
        /// <returns></returns>
        public string AddTextToImg(string fileName, int type, string text)
        {
            try{

                if (text == string.Empty)
                {
                    return "";
                }
                //还需要判断文件类型是否为图像类型
                Image image = Image.FromFile("");//图片地址
                var bitmap = new Bitmap(image, image.Width, image.Height);
                Graphics g = Graphics.FromImage(bitmap);
                const float fontSize = 12.0f;
                //下面定义一个矩形区域，以后在这个矩形里画上白底黑字
                float rectX = 0;
                float rectY = 0;
                float rectWidth = text.Length * (fontSize + 8);
                const float rectHeight = fontSize + 8;
                //声明矩形域
                float heighs = image.Height;
                float widths = image.Width;
                #region 判断水印位置
                switch (type)
                {
                    case 1:
                        rectX = 0;
                        break;
                    case 2:
                        rectX = (widths - rectWidth) / 2;
                        break;
                    case 3:
                        rectX = widths - rectWidth - 8;
                        break;
                    case 4:
                        rectY = heighs / 2;
                        break;
                    case 5:
                        rectX = widths / 2 - rectWidth / 2;
                        rectY = heighs / 2;
                        break;
                    case 6:
                        rectX = widths - rectWidth - 8;
                        rectY = heighs / 2 - fontSize / 2;
                        break;
                    case 7:
                        rectY = heighs - fontSize - 8;
                        break;
                    case 8:
                        rectX = widths / 2;
                        rectY = heighs - fontSize - 8;
                        break;
                    case 9:
                        rectX = widths - rectWidth - 8;
                        rectY = heighs - fontSize - 8;
                        break;
                    default:
                        rectX = 0;
                        break;
                }
                #endregion
                var textArea = new RectangleF(rectX, rectY, rectWidth, rectHeight);
                var font = new Font("宋体", fontSize);   //定义字体
                Brush whiteBrush = new SolidBrush(Color.Red);   //白笔刷，画文字用
                g.DrawString(text, font, whiteBrush, textArea);
                var ms = new MemoryStream();
                //保存为Jpg类型
                bitmap.Save(ms, ImageFormat.Jpeg);
                string filename1 = "w" + fileName.CutString(fileName.LastIndexOf('/') + 1, fileName.Length);
                string filepaths = fileName.CutString(0, fileName.LastIndexOf('/') + 1);
                bitmap.Save(FileHelper.GetMapPath(filepaths + filename1));
                g.Dispose();
                bitmap.Dispose();
                image.Dispose();
                return filepaths + filename1;
            }
            catch
            {
                return "";
            }
        }
    }
}
