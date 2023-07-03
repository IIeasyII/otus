using System.Diagnostics;
using System.Linq;
using System.Net;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var currentDirectory = Environment.CurrentDirectory;
        
        #region 1
        var path1 = @"\Otus\TestDir1";
        var directoryInfo1 = new DirectoryInfo($"{currentDirectory}{path1}");
        directoryInfo1.Create();

        var path2 = @"\Otus\TestDir2";
        var directoryInfo2 = new DirectoryInfo($"{currentDirectory}{path2}");
        directoryInfo2.Create();
        #endregion

        #region 2
        var fileName1 = "file1.txt";
        var fileName2 = "file2.txt";
        File.Create($"{directoryInfo1.FullName}\\{fileName1}");
        File.Create($"{directoryInfo1.FullName}\\{fileName2}");

        var fileName3 = "file3.txt";
        var fileName4 = "file4.txt";
        File.Create($"{directoryInfo2.FullName}\\{fileName3}");
        File.Create($"{directoryInfo2.FullName}\\{fileName4}");
        #endregion

        #region 3
        var fileStream1 = File.OpenWrite($"{directoryInfo1.FullName}\\{fileName1}");
        fileStream1.BeginWrite();
        #endregion
    }
}