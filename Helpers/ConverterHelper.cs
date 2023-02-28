using Amazon.Runtime;
using Amazon.S3.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StatAssesment.Helpers
{
    public class ConverterHelper : BaseHelper
    {
        public ConverterHelper() { }
        public static T ConvertCSVToEntity<T>(string[] csvData, string[] csvMetada) where T : new()
        {
            T returnEntity = new T();
            var properties = returnEntity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var eachProperty in properties)
            {
                var csvAttribute = eachProperty.GetCustomAttribute(typeof(CSVColumn)) as CSVColumn;
                if (csvAttribute != null)
                {
                    int csvIndex = Array.IndexOf(csvMetada, csvAttribute.ImportName);
                    if (csvIndex > -1)
                    {
                        var csvValue = csvData[csvIndex];

                        object setValue = new object();
                        try
                        {
                            //this needs to actually map to returnEntity
                            setValue = csvValue;// string.IsNullOrEmpty(csvValue) && eachProperty.PropertyType != typeof(string) ? Activator.CreateInstance(eachProperty.PropertyType) : Convert.ChangeType(csvValue, eachProperty.PropertyType);
                        }
                        catch (Exception e)
                        {
                        }
                    }
                }

            }
            return returnEntity;

        }
    }
}
