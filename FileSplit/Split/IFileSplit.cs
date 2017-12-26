using System;

namespace Split
{
    public interface IFileSplit
    {
        void SplitFile(String orgFileName, String outPath, long subFileSize);
    }
}