using StatAssesment.Helpers;
using StatAssesment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatAssesment.Managers
{
    public class LocalFileHelper : BaseHelper
    {
        public LocalFileHelper() { }

        public void UnZipFile(string startPath, string zipPath, string extractPath)
        {
            System.IO.Compression.ZipFile.CreateFromDirectory(startPath, zipPath);
            System.IO.Compression.ZipFile.ExtractToDirectory(zipPath, extractPath);
        }

        public string GetWorkingFileDirectory(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return string.Empty;

            string workingDirectory = Environment.CurrentDirectory;
            return $"{Directory.GetParent(workingDirectory)?.Parent?.Parent?.FullName}/{filePath}";

        }

        public List<string> ReadFile(string direPath) => File.ReadAllLines(direPath, Encoding.UTF8).ToList();

        public bool CheckForFiles(string fullPath, string fileExtension, int quantity, bool hasAdditional)
        {
            //could check for specified file extensions but there are a lot so i'm doing a small list 
            List<string> validFileExtensions = new List<string> { "csv", "xls", "xlsx", "pdf", "doc", "docx" };
            if (!validFileExtensions.Contains(fileExtension))
                return false;

            string[] files = System.IO.Directory.GetFiles(fullPath, $"*.{fileExtension}");
            if (hasAdditional ? files.Length >= quantity : files.Length == quantity) // only need one. 
                return true;
            return false;
        }
        //public List<CsvMap> ExtractCsv(string path)
        //{
        //    List<CsvMap> myExtraction = new List<CsvMap>();
        //    foreach (string line in temp)
        //    {
        //        var delimitedLine = line.Split('\t'); //set ur separator, in this case tab

        //        myExtraction.Add(new MyMappedCSVFile(delimitedLine[0], delimitedLine[3]));
        //    }
        //}

        public (List<string>, List<string>) ReadCsv(string path)
        {

            List<string> listA = new List<string>();
            List<string> listB = new List<string>();
            using (var reader = new StreamReader(path))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');

                    listA.Add(values[0]);
                    listB.Add(values[1]);
                }
            }
            return (listA, listB) ;
        }

    }
}
