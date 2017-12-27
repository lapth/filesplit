using System;
using System.Collections.Generic;
using System.IO;

namespace FileSplit
{
    class FileSplitMain
    {
        static void Main(string[] args)
        {
            if (null == args || args.Length == 0)
            {
                printUsage();
                return;
            }

            string orgFileName = "";
            string subFilePath = "";
            long subFileSize = 0;
            string firstFileName = "";
            string parrentFilePath = "";
            Boolean spliting = false;

            if ("S".Equals(args[0].ToUpper()))
            {
                orgFileName = args[1];
                subFilePath = args[2];
                subFileSize = long.Parse(args[3]);
                spliting = true;
            } else if ("J".Equals(args[0].ToUpper()))
            {
                firstFileName = args[1];
                parrentFilePath = args[2];
            }

            if (spliting)
            {
                Split.IFileSplit fileSplit = new Split.FileSplit();
                fileSplit.SplitFile(orgFileName, subFilePath, subFileSize);
            } else
            {
                Join.IFileJoin fileJoin = new Join.FileJoin();
                fileJoin.JoinFile(firstFileName, parrentFilePath);
            }            
        }

        private static void printUsage()
        {
            Console.WriteLine("Program usage:");
            Console.WriteLine("   >> FileSplit s/j [original file/the first sub file] [split files path/original file path] [size of sub file in byte]");
            Console.WriteLine("   >> Example 1: FileSplit s \"c:\\path\\file.data\"  \"c:\\subfile_folder\" 100000");
            Console.WriteLine("   >> Example 2: FileSplit j \"c:\\subfile_folder\\filename.ext.001\" \"c:\\path\\file.ext\"");
        }
    }
}
