using Amazon.S3.Model;
using Amazon.S3;
using StatAssesment.Helpers;
using System.IO.Compression;
using Amazon.S3.Transfer;
using StatAssesment.Models;

using Console = Colorful.Console;

using iText.Kernel.Pdf;
using System.IO;
using Amazon.Runtime.Documents.Internal.Transform;

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
        public string ConvertCSVToLine(string line) => line.Split('~')[9];
      
        public PONumbertModel ConvertCSVToEntity(string line, string[] csvMetada)
        {
            var values = line.Split('~');
            Console.WriteLine(line);
            PONumbertModel newRecord = new PONumbertModel()
            {
                //PONumber = values[15]
                PONumber = values[9]
                //PONumber = values[10]
            };
            //need to fix mapper for scalability
            //using (ConverterHelper ch = new ConverterHelper())
            //{
            //    newRecord = ch.ConvertCSVToEntity<CsvMap>(values, csvMetada);

            //}
            return newRecord;
        }

        public List<string> ReadCSVTosModels(ZipArchiveEntry entry)
        {
            var maps = new List<PONumbertModel>();
            var poNumbers = new List<string>();
            using (var reader = new StreamReader(entry.Open()))
            {
                Console.WriteLine(reader.ToString());
                //read first line with headers
                var metaDataLine = reader.ReadLine() ?? "";
               
                //get array with headers
                string[] csvMetada = metaDataLine.Split('~');
                while (!reader.EndOfStream)
                {
                    // create model based on string data and columns metadata with columns                      
                    //PONumbertModel newModel = ConvertCSVToEntity(reader.ReadLine() ?? "", csvMetada);
                    //string poNumber = ConvertCSVToLine(reader.ReadLine());
                    poNumbers.Add(ConvertCSVToLine(reader.ReadLine()));
                    //maps.Add(newModel);
                }
            }
            return poNumbers;
            //return maps;
        }

        // this is searching a lot of long strings. could parse better but it would be best to map the CSV to a model. 
        public List<string> GetListFoundPONumbers(List<string> entryNames, List<string> poNumbers)
        {
            var foundPoNumbers = new List<string>();
            foreach (var entry in entryNames)
            {
                var name = entry;
                var foundNumber = poNumbers.Where(x => x.Contains(name)).FirstOrDefault();
                if (foundNumber != null)
                {
                    foundPoNumbers.Add(name);
                    //Console.WriteLine("PoNumber found " + name, Color.Sienna);
                }
            }

            return foundPoNumbers;
        }


        public(List<string> poNumFromCsv, List<string> foundPONumbers) GetPONumbers(ZipArchiveEntry csvEntry, List<string> pdfFileNames)
        {

            var poNumFromCsv = new List<string>(); // read fromcsv
            var foundPoNumbers = new List<string>(); // this holds the pdf files that we need to flag

            poNumFromCsv = ReadCSVTosModels(csvEntry); //send off the csv entry to be read and mapped to see all ponumbers

            foundPoNumbers = GetListFoundPONumbers(pdfFileNames, poNumFromCsv); // this holds the pdf files that we need to flag

            return (poNumFromCsv, foundPoNumbers);
        }

        public List<ZipArchiveEntry> ProcessEntries(List<ZipArchiveEntry> allEntries, List<string> poNumbers)
        {
            var processedEntries = new List<ZipArchiveEntry>();

            foreach (var item in poNumbers)
            {
                var entry = allEntries.Where(x => x.FullName.Contains(item)).FirstOrDefault();
                //TODO: need to uncomment the real condition to check 
                //TODO: actually look into metadata on the opened pdf 
                //if (entry != null && !entry.Comment.Contains("- processed;")) // this check looks to see if it was processed before ideally we would be writing something to a db table to compare against a list of those entries 
                if (entry != null) // this check looks to see if it was processed before ideally we would be writing something to a db table to compare against a list of those entries 
                {
                    entry.Comment = $"{DateTime.UtcNow} - processed;";
                    Console.WriteLine(entry.Comment);
                  

                    processedEntries.Add(entry);
                }
            }

            return processedEntries;
        }
      
        public async Task<Stream> DownloadFileAsync(ZipArchiveEntry entry) => entry.Open();
      

        public async Task UploadDocumentToS3(Stream stream, string poNumber, string fileName)
        {
            PutObjectRequest fileRequest = new PutObjectRequest();

            fileRequest.BucketName = $"by-po/{poNumber}/{fileName}";
            fileRequest.CannedACL = S3CannedACL.Private;
            fileRequest.StorageClass = S3StorageClass.Standard;

            //adding to metadata to read if processed. If this has a value beign a date it's been processed
            fileRequest.Metadata.Add("x-previous-read-date", DateTime.UtcNow.ToString());
            fileRequest.InputStream = stream;
            
            //don't want to post at the moment, just want to look at the object
            //await _client.PutObjectAsync(fileRequest);

        }
        public bool CheckMetaData(Stream stream)
        {
            bool hasPrevDate = false;
          
            byte[] bytes;
            using (var ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                bytes = ms.ToArray();
            }
            using var inputStream = new MemoryStream(bytes);
            using var reader = new  PdfReader(inputStream);
            using var outputStream = new MemoryStream();
            using var writer = new PdfWriter(outputStream);
            using (var document = new PdfDocument(reader, writer))
            {
                var metadata = document.GetDocumentInfo();
                if (metadata == null)
                    return true;
                //documentInfo.SetTitle(title);
            }

            return hasPrevDate;
        }
    }
}
