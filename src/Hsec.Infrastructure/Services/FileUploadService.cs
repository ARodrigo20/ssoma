using Hsec.Application.Common.Interfaces;
using Hsec.Application.Files.Commands.CreateFiles;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.Extensions.Configuration;
using System.Drawing;
using System;
using System.Drawing.Drawing2D;
using NReco.PdfRenderer;
using Hsec.Domain.Entities.PlanAccion;
using System.Diagnostics;
using Xabe.FFmpeg;
using System.Threading.Tasks;
using System.Reflection;

namespace Hsec.Infrastructure.Services
{
    public class FileService : IFileService
    {
        public static int minWidth;
        public static int minHeight;
        public IConfiguration Configuration { get; }

        public FileService(IConfiguration configuration) {
            Configuration = configuration; 
            minWidth = int.Parse(Configuration["appSettings:minWidth"]);
            minHeight = int.Parse(Configuration["appSettings:minHeight"]);
        }
        public CreateFileCommand imgUpload(IFormCollection form)
        {
            CreateFileCommand listaForm = new CreateFileCommand();

            foreach (var item in form)
            {
                if (item.Key == "NroDocReferencia")
                {
                    listaForm.NroDocReferencia = item.Value;
                }
                if (item.Key == "NroSubDocReferencia")
                {
                    listaForm.NroSubDocReferencia = item.Value;
                }
                if (item.Key == "CodTablaRef" || string.IsNullOrEmpty(item.Key))
                {
                    listaForm.CodTablaRef = item.Value;
                }
            }
            return listaForm;
        }

        public async Task<byte[]> getImagePreview(TFile file)
        {
            try
            {
                byte[] outPreview= null;
                bool topImage = false;
                if (file.TipoArchivo.Contains("video"))
                {
                    try {
                    found:
                        string nameMD5 = DateTime.Now.Ticks.GetHashCode().ToString("x").ToUpper();
                        var VideoPath = AppDomain.CurrentDomain.BaseDirectory + @"PreviewFile\Video_" + nameMD5 + "."+file.Nombre.Split('.')[file.Nombre.Split('.').Length-1];
                        if (File.Exists(VideoPath)) goto found;
                        File.WriteAllBytes(VideoPath, file.ArchivoData);
                        var imageFile = AppDomain.CurrentDomain.BaseDirectory + @"PreviewFile\VidImage_"+ nameMD5+".JPG";
                        var urlFile = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                        FFmpeg.SetExecutablesPath(urlFile+@"\MediaToolkit", "ffmpeg");
                        var infoVideo = await FFmpeg.GetMediaInfo(VideoPath);
                        var conversion=await FFmpeg.Conversions.FromSnippet.Snapshot(VideoPath,imageFile, infoVideo.Duration/2.0);
                        await conversion.Start();
                        outPreview = File.ReadAllBytes(imageFile);
                        File.Delete(VideoPath);
                        File.Delete(imageFile);
                    }
                    catch (Exception e) {
                        outPreview = new byte[0];
                    }
                }
                else if (file.TipoArchivo.Contains("pdf"))
                {
                    try
                    {
                    again:
                        string nameMD5 = DateTime.Now.Ticks.GetHashCode().ToString("x").ToUpper();
                        var pdfFile = AppDomain.CurrentDomain.BaseDirectory + @"PreviewFile\DocPdf_" + nameMD5+".pdf";
                        if (File.Exists(pdfFile)) goto again;
                        File.WriteAllBytes(pdfFile, file.ArchivoData);
                        var imageFile = AppDomain.CurrentDomain.BaseDirectory + @"PreviewFile\DocImg_" + nameMD5 + ".JPG";
                        var pdfToImg = new PdfToImageConverter();
                        pdfToImg.ScaleTo = 3000; // fit 200x200 box
                        pdfToImg.GenerateImage(pdfFile, 1, ImageFormat.Jpeg, imageFile);
                        outPreview = File.ReadAllBytes(imageFile);
                        File.Delete(pdfFile);
                        File.Delete(imageFile);
                        topImage = true;
                    }
                    catch (Exception e)
                    {
                        outPreview = new byte[0];
                    }
                }
                else outPreview = file.ArchivoData;
                Stream stream = new MemoryStream(outPreview);
                Image imageIn = Image.FromStream(stream);
                Bitmap bmPhoto = new Bitmap(imageIn);

                // recortar imagen 
                int sourceWidth = bmPhoto.Width;
                int sourceHeight = bmPhoto.Height;
                int cuadro = 0;
                int x = 0, y = 0;
//                double div = (minWidth * 1.0 / minHeight * 1.0);
                if (sourceWidth > sourceHeight)
                {
                    cuadro = sourceHeight;
                    x = sourceWidth / 2 - sourceHeight / 2;
                }
                else
                {
                    cuadro = sourceWidth;
                    if (topImage) y = 0;
                    else y = sourceHeight / 2 - sourceWidth / 2;
                }
                Rectangle cropArea = new Rectangle(x, y, cuadro, cuadro);

                Bitmap bmpCrop = bmPhoto.Clone(cropArea, bmPhoto.PixelFormat);

                //cambiar tamaño               
                Bitmap bmpRsize = resizeImage(bmpCrop);
                // cambiar resolucion
                bmpRsize.SetResolution(50, 50);
                MemoryStream ms = new MemoryStream();

                bmpRsize.Save(ms, imageIn.RawFormat);
                return ms.ToArray();
            }
            catch (Exception ex)
            {
                return new byte[0];
            }
        }

        public string GetThumbnailImage(string videoPath, string thumb)
        {
            try
            {               
                Process proc = new Process();
                proc.StartInfo.FileName = "C:\\Users\\Roger\\.nuget\\packages\\ffmpeg.nuget\\1.0.0\\protobuild\\Windows\\ffmpeg\\Windows\ffmpeg.exe";
                proc.StartInfo.Arguments = " -i \"" + videoPath + "\" -f image2 -ss 0:0:1 -vframes 1 -s 150x150 -an \"" + thumb + "\"";
                //The command which will be executed
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.RedirectStandardOutput = false;
                proc.Start();
                return thumb;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private static Bitmap resizeImage(Bitmap imgToResize)
        {
            int sourceWidth = imgToResize.Width;
            int sourceHeight = imgToResize.Height;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)minWidth / (float)sourceWidth);
            nPercentH = ((float)minHeight / (float)sourceHeight);

            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();

            return b;
        }
    }
}
