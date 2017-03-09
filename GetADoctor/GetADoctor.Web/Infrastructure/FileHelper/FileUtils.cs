using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace GetADoctor.Web.Infrastructure.FileHelper
{
    public class FileUtils
    {
        public static readonly string UPLOAD_PATH = "~/Content/Uploads/Account/";

        public static string UploadFile(HttpPostedFileBase file)
        {
            string filename = null;

            if (file != null && file.ContentLength > 0)
            {
                filename = GetFileName(file.FileName);
                string path = Path.Combine(HostingEnvironment.MapPath(UPLOAD_PATH), filename);
                file.SaveAs(path);
            }

            return filename;
        }

        public static string UploadFile(HttpPostedFileBase file, string fileFolder)
        {
            string filename = null;
            string filePath = UPLOAD_PATH + fileFolder;

            if (file != null && file.ContentLength > 0)
            {
                filename = GetFileName(file.FileName);
                bool exists = Directory.Exists(HostingEnvironment.MapPath((filePath)));
                if (!exists)
                {
                    Directory.CreateDirectory(HostingEnvironment.MapPath(filePath));
                }

                string path = Path.Combine(HostingEnvironment.MapPath(filePath), filename);
                file.SaveAs(path);
            }

            return filename;
        }

        public static string GetFileName(string uploadedFileName)
        {
            string filename = Guid.NewGuid() + Path.GetExtension(uploadedFileName);
            return filename;
        }
    }
}