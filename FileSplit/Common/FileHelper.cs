using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Common
{
    public class FileHelper
    {
        public static IList<String> GetSubFileName(String orgFileName, String outPath, long subFileSize)
        {
            IList<String> lstSubFileName = new List<String>();

            if (string.IsNullOrEmpty(orgFileName)
                || string.IsNullOrEmpty(outPath)
                || !File.Exists(orgFileName)
                || !Directory.Exists(outPath)
                || subFileSize <= 0)
            {
                return lstSubFileName;
            }

            FileInfo orgFileInfo = new FileInfo(orgFileName);
            
            long orgFileSize = orgFileInfo.Length;

            int numSubFile = (int)(orgFileSize / subFileSize);
            if (orgFileSize % subFileSize > 0)
            {
                numSubFile++;
            }

            int subFileExtLen = numSubFile.ToString().Length;
            String subFileExt = "";
            for (int ind = 0; ind < subFileExtLen; ind ++)
            {
                subFileExt += "0";
            }

            outPath += "\\" + orgFileInfo.Name;

            for (int ind = 1; ind <= numSubFile; ind ++)
            {
                String subFileName = outPath + ind.ToString(subFileExt);
                lstSubFileName.Add(subFileName);            
            }

            return lstSubFileName;
        }

        public static void CopyToSubFile(FileStream fileStreamIn, String subFileName, long totalSubFileSize, int bufferSize)
        {
            if (null == fileStreamIn
                || string.IsNullOrEmpty(subFileName)
                || totalSubFileSize <= 0
                || bufferSize <= 0)
            {
                return;
            }

            FileStream fileStreamOut = File.Create(subFileName);
            
            fileStreamOut.SetLength(totalSubFileSize);

            long remainingSubFileSize = totalSubFileSize;
            int byteRead = -1;

            byte[] buffer = new byte[bufferSize];

            while (remainingSubFileSize > 0 || byteRead == 0)
            {
                if (bufferSize > remainingSubFileSize)
                {
                    bufferSize = (int) remainingSubFileSize;
                }

                byteRead = fileStreamIn.Read(buffer, 0, bufferSize);

                fileStreamOut.Write(buffer, 0, byteRead);                

                remainingSubFileSize -= byteRead;
            }

            fileStreamOut.Flush();
            fileStreamOut.Close();
        }
    }
}
