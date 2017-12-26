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

            Split.IFileSplit fileSplit = new Split.FileSplit();
            fileSplit.SplitFile(fileName, outPath, 1024 * 1024 * 10);


        }
    }
}
