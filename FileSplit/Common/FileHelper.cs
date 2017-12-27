using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Common
{
    public class FileHelper
    {
        /// <summary>
        /// Generate sub file name based on the sub file size.
        /// </summary>
        /// <param name="orgFileName">The path to the original file</param>
        /// <param name="outPath">The folder contains the sub files</param>
        /// <param name="subFileSize">The size of each sub file</param>
        /// <returns>List of sub files</returns>
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

            // Get posible number of sub files
            int numSubFile = (int)(orgFileSize / subFileSize);
            if (orgFileSize % subFileSize > 0)
            {
                numSubFile++;
            }

            // Sub file extention pattern
            int subFileExtLen = numSubFile.ToString().Length;
            String subFileExt = "";
            for (int ind = 0; ind < subFileExtLen; ind ++)
            {
                subFileExt += "0";
            }

            // List of sub files
            outPath += "\\" + orgFileInfo.Name + ".";

            for (int ind = 1; ind <= numSubFile; ind ++)
            {
                String subFileName = outPath + ind.ToString(subFileExt);
                lstSubFileName.Add(subFileName);            
            }

            return lstSubFileName;
        }

        /// <summary>
        /// Get the list of continous sub file based of the first sub file.
        /// </summary>
        /// <param name="firstSubFilePath">The first splited file</param>
        /// <returns>List of splited file name</returns>
        public static IList<String> GetSubFiles(String firstSubFilePath)
        {
            IList<String> lstSubFiles = new List<String>();

            if (string.IsNullOrEmpty(firstSubFilePath)
                || !File.Exists(firstSubFilePath))
            {
                return lstSubFiles;
            }

            FileInfo fileInfo = new FileInfo(firstSubFilePath);

            string firstSubFileName = fileInfo.Name;
            string orgFileName = firstSubFileName.Substring(0, firstSubFileName.LastIndexOf('.'));
            string firstSubFileNumber = firstSubFileName.Substring(firstSubFileName.LastIndexOf('.') + 1);
            string subFilePath = fileInfo.DirectoryName;

            if (int.Parse(firstSubFileNumber) != 1)
            {
                throw new Exception.FileSplitException("Wrong selection, you have to choose the first sub file!", "003");
            }

            // Sub file number partern
            int subFileExtLen = firstSubFileNumber.Length;
            String subFileExt = "";
            for (int ind = 0; ind < subFileExtLen; ind++)
            {
                subFileExt += "0";
            }

            // Get continous sub file
            subFilePath = subFilePath + "\\" + orgFileName + ".";
            int copyingFile = 1;
            do
            {
                String subFileName = subFilePath + copyingFile.ToString(subFileExt);

                if (!File.Exists(subFileName))
                {
                    break;
                }

                lstSubFiles.Add(subFileName);
                copyingFile++;
            } while (true);

            return lstSubFiles;
        }

        /// <summary>
        /// Copy a specified bytes from original file to a sub file.
        /// </summary>
        /// <param name="fileStreamIn">The original file stream</param>
        /// <param name="subFileName">The sub file will be copied to</param>
        /// <param name="totalSubFileSize">The sub file size</param>
        /// <param name="bufferSize">The posible buffer size when copying</param>
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
            // Copy to sub file
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

        /// <summary>
        /// Copy the sub file back to the splited file.
        /// </summary>
        /// <param name="parrentFileStream">The original file to copy back</param>
        /// <param name="subFileName">Name of sub file</param>
        /// <param name="bufferSize">The posible buffer size</param>
        public static void JoinFile(FileStream parrentFileStream, string subFileName, int bufferSize)
        {
            if (string.IsNullOrEmpty(subFileName)
               || !File.Exists(subFileName)
               || null == parrentFileStream)
            {
                return;
            }

            FileStream subFileStream = new FileStream(subFileName, FileMode.Open, FileAccess.Read);

            int byteRead = -1;

            byte[] buffer = new byte[bufferSize];
            // Copy back to original file
            while (byteRead != 0)
            {
                byteRead = subFileStream.Read(buffer, 0, bufferSize);

                parrentFileStream.Write(buffer, 0, byteRead);
            }

            parrentFileStream.Flush();
        }
    }
}
