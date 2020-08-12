using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Send
{
    public class GetFileList
    {
        private string filePath = @"C:/Users/Konrad/Desktop/euvic/";
            
        private readonly List<string> _filePathList = new List<string>();

        public List<string> GetAllFilesToList()
        {
            var localFiles = Directory.GetFiles(filePath,
                "*.xml",
                SearchOption.TopDirectoryOnly).ToList();

            foreach (var link in localFiles)
            {
                _filePathList.Add(link);
            }

            return _filePathList;
        }
    }
}