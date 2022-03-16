using System.Diagnostics;
using System.Reflection;

namespace SharpIceCer
{
    static class SharpIceCerMain
    {
        static string TmepFile = System.IO.Path.GetTempPath() + "SharpIceCer" + System.Guid.NewGuid();
        static void FreedFile(string resource, string path) //提取文件,代码:https://juejin.cn/post/6989143365862293534
        {
            BufferedStream input = new BufferedStream(Assembly.GetExecutingAssembly().GetManifestResourceStream(resource));
            FileStream output = new FileStream(path, FileMode.Create);
            byte[] data = new byte[1024];
            int lengthEachRead;
            while ((lengthEachRead = input.Read(data, 0, data.Length)) > 0)
            {
                output.Write(data, 0, lengthEachRead);
            }
            output.Flush();
            output.Close();
        }

        static void Main()
        {
            Console.Title = "SharpIce Windows代码签名证书过白程序";
            Process process = new Process();

            Directory.CreateDirectory(TmepFile);
            FreedFile("SharpIceCer.Resources.certmgr.exe", TmepFile + @"\certmgr.exe");
            FreedFile("SharpIceCer.Resources.SharpIce.cer", TmepFile + @"\SharpIce.cer");
            process.StartInfo = new ProcessStartInfo(TmepFile + @"\certmgr.exe", "/c /add " + TmepFile + @"\SharpIce.cer" + " /s root");
            process.Start();
            process.WaitForExit();
            new DirectoryInfo(TmepFile).Delete(true);
        }
    }
}