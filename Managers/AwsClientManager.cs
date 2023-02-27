using Amazon.S3.Model;
using Amazon.S3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatAssesment.Helpers;
using System.IO.Compression;

namespace StatAssesment.Managers
{
    public class AwsClientManager: BaseHelper
    {
        public AwsClientManager() { }
        public static string _bucketName = "stat-coding-amanzfwaxlh";

        public async Task<List<S3Object>> ListingObjectsAsync(AmazonS3Client client)
        {

            try
            {
                ListObjectsRequest request = new ListObjectsRequest
                {
                    BucketName = _bucketName,
                    MaxKeys = 50
                };

                ListObjectsResponse response = await client.ListObjectsAsync(request);

                return response.S3Objects;

            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine("Error encountered on server. Message:'{0}' when writing an object", e.Message);
                return new List<S3Object>();
            }
            catch (Exception e)
            {
                Console.WriteLine("Unknown encountered on server. Message:'{0}' when writing an object", e.Message);
                return new List<S3Object>();
            }
        }

        public async Task OpenZipFile(AmazonS3Client client, string key)
        {
            var request = new GetObjectRequest { BucketName = _bucketName, Key = $"{key}.zip" };
            using var response =  await client.GetObjectAsync(request);
            using var zip = new ZipArchive(response.ResponseStream, ZipArchiveMode.Read);
            foreach (var entry in zip.Entries)
            {
                Console.WriteLine(entry.FullName);

                //using var stream = entry.Open();
                // stream will contain decompressed data, do whatever you want with it
            }
        }

        public async Task ProcessPutObject(AmazonS3Client client)
        {
            return;
            if (false) // this will be a check on x-previously-read metadata to start processing the object 
            {
                string fileName = "";
                string content = "";
                string poNumber = "";
                //upload back to bucket 
                using (FileHelper fh = new FileHelper())
                {
                    try
                    {
                        await client.PutObjectAsync(fh.CreatePutObject(_bucketName, fileName, poNumber, content));
                    }
                    catch (AmazonS3Exception e)
                    {
                        Console.WriteLine(
                                "Error encountered ***. Message:'{0}' when writing an object"
                                , e.Message);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(
                            "Unknown encountered on server. Message:'{0}' when writing an object"
                            , e.Message);
                    }
                }
            }

        }
    }
}
