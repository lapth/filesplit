# filesplit
File Split console application for dotnot core 2.x

It support 2 mode: Split and Join

Tested with 100GB file to split to 100 sub files on Window 7, dot net core 2.1.3

Command to execute:

>> dotnet FileSplit.dll s/j [original file/the first sub file] [split files path/original file path] [size of sub file in byte]
>> Example 1: dotnet FileSplit.dll s "c:\path\file.data"  "c:\subfile_folder" 100000
>> Example 2: dotnet FileSplit.dll j "c:\subfile_folder\filename.ext.001" "c:\path\file.ext"
