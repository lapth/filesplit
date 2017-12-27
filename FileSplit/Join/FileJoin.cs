using Common.Exception;
using System;
using System.Collections.Generic;
using System.IO;

namespace Join
{
    public class FileJoin : IFileJoin
    {
        private const int BUFFER_SIZE = 1024 * 1024;

        void IFileJoin.JoinFile(string firstSubFileName, string outPath)
        {
            if (string.IsNullOrEmpty(firstSubFileName)
               || string.IsNullOrEmpty(outPath)
               || !File.Exists(firstSubFileName)
               || !Directory.Exists(outPath))
            {
                throw new FileSplitException("File Join parameters wrong!", "002");
            }

            FileInfo fileInfo = new FileInfo(firstSubFileName);
            string orgFileName = fileInfo.Name.Substring(0, fileInfo.Name.LastIndexOf('.'));
            string targetFileName = outPath + "\\" + orgFileName;

            FileStream parrentFile = File.Create(targetFileName);

            IList<String> subFileNames = Common.FileHelper.GetSubFiles(firstSubFileName);
            for (int ind = 0; ind < subFileNames.Count; ind ++)
            {
                Common.FileHelper.JoinFile(parrentFile, subFileNames[ind], BUFFER_SIZE);
            }
            parrentFile.Flush();
            parrentFile.Close();
        }
    }
}