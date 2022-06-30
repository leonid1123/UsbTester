using System.Diagnostics;
Console.WriteLine("Insert USB drive and press 'Enter'");
ConsoleKeyInfo start = Console.ReadKey();
if (start.Key == ConsoleKey.Enter)
{
    List<DriveInfo> usb = ScanDrives();
    Console.WriteLine("Drives detected:");
    foreach (DriveInfo d in usb)
    {
        Console.WriteLine("Drive letter:{0}",d.Name);
        Console.WriteLine("Drive label:{0}",d.VolumeLabel);
        Console.WriteLine("---");
    }
} else
{
    Console.WriteLine("ENTER!!!!");
}
Console.WriteLine("Ready to test?");
Console.WriteLine("If so press Enter");
start = Console.ReadKey();
if (start.Key == ConsoleKey.Enter)
{
    TestSpeed(ScanDrives());
} else
{
    Console.WriteLine("ENTER!!!!");
}

    List<DriveInfo> ScanDrives()
{
    DriveInfo[] allDrives = DriveInfo.GetDrives();
   List<DriveInfo> drives = new List<DriveInfo>();
    for (int k = 0; k < allDrives.Length; k++)
    {
        if (allDrives[k].DriveType.ToString() == "Removable")
        {
            drives.Add(allDrives[k]);            
        }
    }
    return drives;
}

void TestSpeed(List<DriveInfo> locatedDrives)
{
    Random random = new Random();
    byte newByte = 0;
    foreach (DriveInfo d in locatedDrives)// сделать выбор, запоминать результат, добавить возможность подключать
    {
        Console.WriteLine("Test Results:");
        Console.WriteLine("Drive {0}", d.Name);
        //Console.WriteLine("Root dir {0}", d.RootDirectory);
        Stopwatch stopWatch = new Stopwatch();
        stopWatch.Start();
        using (FileStream fs = new FileStream( //переписать на новый конструктор
            d.RootDirectory.ToString() + "tst.bin",
            FileMode.Create,
            FileAccess.Write,
            FileShare.None,
            18,
            FileOptions.None
            )
            )
        {
            for (int i = 0; i < 2 << 22; i++)
            {
                random.Next(newByte);
                fs.WriteByte(newByte);
            }
        }
        stopWatch.Stop();
        TimeSpan ts = stopWatch.Elapsed;
        Console.WriteLine("Write time:{0}", ts);
        File.Delete(d.RootDirectory + "tst.bin");
    }
}
/*void FileWrite()
{

}*/



