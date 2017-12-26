using System;
using System.Collections.Generic;
using System.IO;
using Common.Exception;

namespace Split
{
    public class FileSplit : IFileSplit
    {
        private const int BUFFER_SIZE = 1024 * 1024;

        public void SplitFile(string orgFileName, string outPath, long subFileSize)
        {
            if (string.IsNullOrEmpty(orgFileName)
               || string.IsNullOrEmpty(outPath)
               || !File.Exists(orgFileName)
               || !Directory.Exists(outPath)
               || subFileSize <= 0)
            {
                throw new FileSplitException("File Split parameter wrong!", "001");
            }

            Console.WriteLine("Spliting the file [{0}] to [{1}] with file size [{2}] bytes.", orgFileName, outPath, subFileSize);

            IList<String> lstSubFiles = Common.FileHelper.GetSubFileName(orgFileName, outPath, subFileSize);

            Console.WriteLine("Spliting to [{0}] sub files.", lstSubFiles.Count);

            FileStream orgFile = new FileStream(orgFileName, FileMode.Open, FileAccess.Read);

            long remainingFileSize = orgFile.Length;

            foreach (String subFile in lstSubFiles)
            {
                Console.WriteLine("Spliting [{0}].", subFile);

                if (subFileSize > remainingFileSize)
                {
                    subFileSize = remainingFileSize;
                }

                Common.FileHelper.CopyToSubFile(orgFile, subFile, subFileSize, BUFFER_SIZE);

                remainingFileSize -= subFileSize;
            }
        }
    }
}
