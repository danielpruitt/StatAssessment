using Amazon.S3.Model;
using Amazon.S3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatAssesment.Helpers;
using System.IO.Compression;
using Amazon.S3.Transfer;
using StatAssesment.Models;
using System.Reflection;
using System.Net.Sockets;

namespace StatAssesment.Managers
{
    public class AwsClientManager : BaseHelper
    {
        private AmazonS3Client _client;
        private string _bucketName = "stat-coding-amanzfwaxlh";

        public AwsClientManager(AmazonS3Client client, string bucketName)
        {
            _client = client;
            _bucketName = bucketName;
        }

        public async Task OpenZipFile(string key)
        {
            var request = new GetObjectRequest { BucketName = _bucketName, Key = key };
            using var response = await _client.GetObjectAsync(request);
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

        public async Task CreateZipFile(List<List<KeyVersion>> keyVersions)
        {

            using MemoryStream zipMS = new MemoryStream();
            using (ZipArchive zipArchive = new ZipArchive(zipMS, ZipArchiveMode.Create, true))
            {
                foreach (var key in keyVersions)
                {
                    foreach (var fileToZip in key)
                    {
                        GetObjectRequest request = new GetObjectRequest
                        {
                            BucketName = _bucketName,
                            Key = fileToZip.Key
                        };
                        using GetObjectResponse response = await _client.GetObjectAsync(request);
                        using Stream responseStream = response.ResponseStream;

                        ZipArchiveEntry zipFileEntry = zipArchive.CreateEntry(fileToZip.Key);

                        //add the file contents
                        using Stream zipEntryStream = zipFileEntry.Open();
                        await responseStream.CopyToAsync(zipEntryStream);

                    }
                }
                zipArchive.Dispose();
            }
            zipMS.Seek(0, SeekOrigin.Begin);
            var fileTxfr = new TransferUtility(_client);
            await fileTxfr.UploadAsync(zipMS, "dev-s3-zip-bucket", "test.zip");
        }

        public PONumbertModel ConvertCSVToEntity(string line, string[] csvMetada)
        {
            var values = line.Split('~');
            PONumbertModel newRecord = new PONumbertModel()
            {
                PONumber = values[15]
            };
            //need to fix mapper for scalability
            //using (ConverterHelper ch = new ConverterHelper())
            //{
            //    newRecord = ch.ConvertCSVToEntity<CsvMap>(values, csvMetada);

            //}
            return newRecord;
        }

        public Dictionary<string, List<PONumbertModel>> GetPoNumbersByEntry(List<ZipArchiveEntry> entries)
        {
            var maps = new Dictionary<string, List<PONumbertModel>>();

            foreach (var csv in entries)
            {
               
                    maps.Add(csv.FullName, this.ReadCSVTosModels(csv));
                
            }
            return maps;
        }
        public List<PONumbertModel> ReadCSVTosModels(ZipArchiveEntry entry)
        {
            var maps = new List<PONumbertModel>();
            using (var reader = new StreamReader(entry.Open()))
            {
                //read first line with headers
                var metaDataLine = reader.ReadLine() ?? "";
                //get array with headers
                string[] csvMetada = metaDataLine.Split('~');
                while (!reader.EndOfStream)
                {
                    // create model based on string data and columns metadata with columns                      
                    PONumbertModel newModel = ConvertCSVToEntity(reader.ReadLine() ?? "", csvMetada);
                    maps.Add(newModel);
                }
            }
            return maps;
        }



    }
}
