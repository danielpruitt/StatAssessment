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

public class Program
{
    //TODO: NULL OUT BEFORE CHECK IN! 
    private static string region = "us-east-2";
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

        using (AwsClientManager clientManager = new AwsClientManager())
        {
            Console.WriteLine("Listing Objects in bucket");
            var result = await client.ListObjectsAsync(bucket);

            if (result != null)
            {
                List<PutObjectRequest> putObjectRequests = new List<PutObjectRequest>();

               
                foreach (var key in result.S3Objects.Select(x => x.Key).ToList())
                {
                    //clientManager.OpenZipFile(client, key);
                    var request = new GetObjectRequest { BucketName = "stat-coding-amanzfwaxlh", Key = key };
                    using var response = await client.GetObjectAsync(request);
                    using var zip = new ZipArchive(response.ResponseStream, ZipArchiveMode.Read);
                    foreach (var entry in zip.Entries)
                    {
                        Console.WriteLine(entry.FullName);

                        //Check for csv 
                        

                        //using var stream = entry.Open();
                        // stream will contain decompressed data, do whatever you want with it
                    }
                }

                //process object in other methods 
                //parse csv 
                //extract pdfs that have not been processed

                //needs way more logic above 
                //await clientManager.ProcessPutObject(client);


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
