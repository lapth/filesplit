using System;
using System.Collections.Generic;
using System.IO;

namespace FileSplit
{
    class FileSplitMain
    {
        static void Main(string[] args)
        {
            String fileName = "C:\\Users\\lapth\\Downloads\\VSCodeSetup-x64-1.19.1.exe";
            String outPath = "C:\\Users\\lapth\\Downloads\\Files";

            //Split.IFileSplit fileSplit = new Split.FileSplit();
            //fileSplit.SplitFile(fileName, outPath, 1024 * 1024 * 10);
            //IList<String> subFiles = Common.FileHelper.GetSubFiles("C:\\Users\\lapth\\Downloads\\Files\\VSCodeSetup-x64-1.19.1.exe.1");
            //((List<String>)subFiles).ForEach(file => Console.WriteLine(file));

            Join.IFileJoin fileJoin = new Join.FileJoin();
            fileJoin.JoinFile("C:\\Users\\lapth\\Downloads\\Files\\VSCodeSetup-x64-1.19.1.exe.1", "C:\\Users\\lapth\\Downloads\\Files\\Join");
        }

        private void printUsage()
        {
            Console.WriteLine("Program usage:");
            Console.WriteLine("   >> FileSplit");
        }
    }
}
