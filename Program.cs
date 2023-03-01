using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using System.IO.Compression;
using StatAssesment.Managers;
using Console = Colorful.Console;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
public class Program
{
    //TODO: NULL OUT BEFORE CHECK IN! 
    private static string region = "";
    private static string bucket = "";
    private static string accessKeyId = "";
    private static string secret = "";

    private static async Task Main(string[] args)
    {
        //don't need this, apparently doesn't work the way I thought
        //using (BucketConfig bc = new BucketConfig())
        //{
        //    bc.WriteProfile("danielpruitt6@gmail.com", accessKeyId, secret);
        //}

        var client = new AmazonS3Client(accessKeyId, secret, RegionEndpoint.USEast2);

        Console.WriteLine("Listing Objects in Bucket");
        var result = await client.ListObjectsAsync(bucket); // gets the 11 zips

        if (result != null)
        {
            var putObjectRequests = new List<PutObjectRequest>();
            //might could optimize this with some parallel.foreach but may have some consequences
            foreach (var key in result.S3Objects.Select(x => x.Key).ToList()) // open's the zip 
            {
                var request = new GetObjectRequest { BucketName = bucket, Key = key };
                using var response = await client.GetObjectAsync(request); // get just one of 
                using var zip = new ZipArchive(response.ResponseStream, ZipArchiveMode.Read);

                //for this exercise, we're provide 1 csv per zip. if there's more we stop. 
                //real world if there's more we may just need to check on various cases then merge them together
                if (zip.Entries.Where(x => x.FullName.EndsWith(".csv")).ToList().Count > 1)
                    throw new Exception(message:"too many csv");

                var csvEntry = zip.Entries.Where(x => x.FullName.EndsWith(".csv")).FirstOrDefault() ?? null;
                var poNumFromCsv = new List<string>(); // read fromcsv
                var foundPoNumbers = new List<string>(); // this holds the pdf files that we need to flag
                Console.WriteLine("=================SEARCH FOR PO NUMBERS====================", Color.Brown);

                if (csvEntry != null)
                {
                    using (var c = new AwsClientManager(client, bucket))
                    {
                        (poNumFromCsv, foundPoNumbers) = c.GetPONumbers(csvEntry, zip.Entries.Select(x => x.FullName).ToList());
                    }
                 
                }
                
                Console.WriteLine("=================PROCESS ENTRIES===========================", Color.Brown);

                Regex rgx = new Regex("[^0-9 _]");

                var entriesToUpload = new List<ZipArchiveEntry>();
                var putObjectRequestList = new List<PutObjectRequest>();
                using (var c = new AwsClientManager(client, bucket))
                {
                    entriesToUpload = c.ProcessEntries(zip.Entries.ToList(), foundPoNumbers);
                    //download files to MS 
                    foreach (var entry in entriesToUpload)
                    {
                        string poNumber = rgx.Replace(entry.Name, "");
                        //Don't actually need this since entry.Open is a stream itself
                        //Stream stream = await c.DownloadFileAsync(entry);
                        Stream stream = entry.Open();
                        bool hasTag = c.CheckMetaData(stream);

                        if (!hasTag) // if hasTag that means there's metadata and it has been processed
                        {
                            await c.UploadDocumentToS3(stream, poNumber, entry.Name);
                        }
                    }
                }
                
                //zip write metadata
            
            }

        }

    }
}


//using (LocalFileHelper fileHelper = new LocalFileHelper())
//{
//    string fullPath = fileHelper.GetWorkingFileDirectory("Zips");
//    string[] dirs = Directory.GetDirectories(fullPath, "*", SearchOption.AllDirectories);
//    Dictionary<string,List<string>> zipList = new Dictionary<string,List<string>>();
//    Parallel.ForEach(dirs, dir =>
//    {
//        bool containsConfigFile = fileHelper.CheckForFiles(dir, "csv", 1, false);
//        if (containsConfigFile)
//        {
//            bool hasFiles = fileHelper.CheckForFiles(dir, "pdf", 1, true);
//            int pdfCount = Directory.GetFiles(dir, "*pdf", SearchOption.TopDirectoryOnly).Length;
//            //Console.WriteLine($"{dir} has {pdfCount}(s)");
//            string[] files = Directory.GetFiles(dir);
//            foreach (string file in files)
//                Console.WriteLine(Path.GetFileName(file));

//            //var csv = fileHelper.ReadCsv(dir);

//        }

//    });

//}
