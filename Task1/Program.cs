using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Security;

class Task1
{

    static void CleanCatalog(string CatalogPath)
    {
        DateTime dateTimeSet = DateTime.Now;

        ///  интервал в 30 минут
        TimeSpan timeInterval = TimeSpan.FromMinutes(30);


        foreach (string fileOrFolder in Directory.GetFileSystemEntries(CatalogPath))
        {
            var fileInfo = new FileInfo(fileOrFolder);
            var directoryInfo = new DirectoryInfo(fileOrFolder);

            if (fileInfo.LastAccessTime.Add(timeInterval) < dateTimeSet || directoryInfo.LastAccessTime.Add(timeInterval) < dateTimeSet)
            {
                if (fileInfo.Attributes.HasFlag(FileAttributes.Directory))
                {
                    //directoryInfo.Attributes &= ~(FileAttributes.Hidden | FileAttributes.ReadOnly);

                    ///пробегаемся по каталогам и удаляем содержимое 
                    CleanCatalog(fileOrFolder);
                    ///Console.WriteLine(fileOrFolder);
                    Directory.Delete(fileOrFolder, true);

                }
                else
                {
                    //fileInfo.Attributes &= ~(FileAttributes.Hidden | FileAttributes.ReadOnly);
                    //fileInfo.Attributes &= ~FileAttributes.Archive;
                    //Console.WriteLine(fileInfo);
                    File.Delete(fileOrFolder);
                }
            }
        }
    }
    static void Main(string[] args)
    {
        /// если путь небыл передан в программу, ввести в поле программы
        string PathCatalog = "";

        if (args.Length == 0)
        {

            Console.WriteLine("Пожалуйста, введите путь каталога:");
            PathCatalog = Console.ReadLine();
            // return;
        }
        else
        {
            if (!string.IsNullOrEmpty(args[0]))
            {
                PathCatalog = args[0];
            }
            else { 
                Console.WriteLine("Путь введен не корректно! Перезапустите программу и попробуйте снова!");
                return; 
            }
        }

        if (!Directory.Exists(PathCatalog))
        {
            Console.WriteLine("Путь введен не корректно! Перезапустите программу и попробуйте снова!");

            return;
        }

        try
        {
            CleanCatalog(PathCatalog);
            Console.WriteLine("Каталог успешно очищен от содержимого, которое не использовались более 30 минут.");
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Упс! Ошибка: {ex.Message}");
            Console.ReadKey();
        }
    }
   
}


