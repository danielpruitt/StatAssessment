using Amazon.Runtime;
using Amazon.S3.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatAssesment.Helpers
{
    public class FileHelper : BaseHelper
    {
        static bool ValidateExtension(ZipArchiveEntry entry, string ext = ".csv")
        {
            return entry.Name.EndsWith(ext, StringComparison.InvariantCultureIgnoreCase);
        }

        static string[] ExtractFileStrings(ZipArchiveEntry entry)
        {
            string[] file = new string[0];
            if (entry != null)
                return file;

            using (var ms = entry.Open() as MemoryStream)
            {
                //interchangeable 
                //ms.Position = 0;
                ms.Seek(0, SeekOrigin.Begin);
                file = ms.ToString().Split(
                    Environment.NewLine,
                    StringSplitOptions.RemoveEmptyEntries);
            }
            return file;
        }

        //static void ReadZip(T file)
        //{
        //    using (var zip = new ZipArchive(file, ZipArchiveMode.Read))
        //    {
        //        foreach (var entry in zip.Entries)
        //        {
        //            using (var stream = entry.Open())
        //            {
        //           
        //            }
        //        }
        //    }
        //}

        public  PutObjectRequest CreatePutObject(string bucketName, string fileName, string poNumber, string content)
        {

            var putRequest = new PutObjectRequest
            {
                BucketName = bucketName,
                Key = "",
                FilePath = $"by-po/{poNumber}/{fileName}.pdf",
                ContentBody = content,
                ContentType = "application/pdf"

            };

            putRequest.Metadata.Add("x-previous-read", "1");  // set metadata to previous read. 
            return putRequest;
        }

    }
}
