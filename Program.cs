using System;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.Runtime.CredentialManagement;
using System.Collections.Generic;
using StatAssesment.Configuration;
using StatAssesment.Helpers;
using System.IO.Compression;
using System.Security.Cryptography.X509Certificates;
using StatAssesment.Managers;
using System.IO;
using StatAssesment.Models;
using CsvHelper;
using System.Globalization;
using System.Text;
using Console = Colorful.Console;
using System.Drawing;
using Amazon.Runtime.Internal.Transform;

public class Program
{
    //TODO: NULL OUT BEFORE CHECK IN! 
    
    private static async Task Main(string[] args)
    {
        //don't need this, apparently doesn't work the way I thought
        //using (BucketConfig bc = new BucketConfig())
        //{
        //    bc.WriteProfile("danielpruitt6@gmail.com", accessKeyId, secret);
        //}

        var client = new AmazonS3Client(accessKeyId, secret, RegionEndpoint.USEast2);


        Console.WriteLine("Listing Objects in Bucket");
        var result = await client.ListObjectsAsync(bucket);

        if (result != null)
        {
            var putObjectRequests = new List<PutObjectRequest>();
            //might could optimize this with some parallel.foreach but may have some consequences
            foreach (var key in result.S3Objects.Select(x => x.Key).ToList())
            {
                var request = new GetObjectRequest { BucketName = bucket, Key = key };
                using var response = await client.GetObjectAsync(request);
                using var zip = new ZipArchive(response.ResponseStream, ZipArchiveMode.Read);
                var maps = new Dictionary<string, List<PONumbertModel>>();
                List<ZipArchiveEntry> csvEntries = zip.Entries.Where(x => x.FullName.EndsWith(".csv")).ToList();

                using (var c = new AwsClientManager(client, bucket))
                {
                    maps = c.GetPoNumbersByEntry(csvEntries);
                }

                foreach (var item in maps)
                {
                    Console.WriteLine("===============================================", Color.Pink);
                    Console.WriteLine($"CSV File {item.Key}");
                    Console.WriteLine($"Contains PO Numbers: {item.Value.Count}");
                    Console.WriteLine("\n");
                    Console.WriteLine("===============================================", Color.Pink);
                }


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
