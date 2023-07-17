using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;

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
        var fs1 = File.Create($"{directoryInfo1.FullName}\\{fileName1}");
        fs1.Close();
        var fs2 = File.Create($"{directoryInfo1.FullName}\\{fileName2}");
        fs2.Close();

        var fileName3 = "file3.txt";
        var fileName4 = "file4.txt";
        var fs3 = File.Create($"{directoryInfo2.FullName}\\{fileName3}");
        fs3.Close();
        File.Create($"{directoryInfo2.FullName}\\{fileName4}");
        #endregion

        #region 3
        var files = new[]
        {
                $"{directoryInfo1.FullName}\\{fileName1}",
                $"{directoryInfo1.FullName}\\{fileName2}",
                $"{directoryInfo2.FullName}\\{fileName3}",
                $"{directoryInfo2.FullName}\\{fileName4}",
            };
        try
        {
            foreach (var file in files)
            {
                using (FileStream fs = File.OpenWrite(file))
                {
                    Byte[] info = System.Text.Encoding.UTF8.GetBytes(file);
                    fs.Write(info, 0, info.Length);
                }
            }
        }
        catch (IOException ex)
        {
            System.Console.WriteLine(ex.ToString());
        }
        catch (UnauthorizedAccessException ex)
        {
            System.Console.WriteLine(ex.ToString());
        }
        #endregion

        #region 4
        foreach (var file in files)
        {
            try
            {
                var content = new[] { DateTime.Now.ToString() };
                await File.AppendAllLinesAsync(file, content);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.ToString());
            }
        }
        #endregion

        #region 5
        foreach (var file in files)
        {
            try
            {
                using (FileStream fs = File.OpenRead(file))
                {
                    byte[] b = new byte[1024];
                    UTF8Encoding temp = new UTF8Encoding(true);

                    while (fs.Read(b, 0, b.Length) > 0)
                    {
                        Console.WriteLine(temp.GetString(b));
                    }
                }
            }
            catch { }
        }
        #endregion
    }
}