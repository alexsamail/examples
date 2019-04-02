using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.IO;
using System.IO.Compression;

namespace Enumerables
{
    class FileWork
    {
        static public string pathBinary = @"..\..\Data\testBinary.dat";
        static public string path = @"..\..\Data\test.txt";
        static public GameEnumerable BinaryLoad()
        {
            GameEnumerable catalog = new GameEnumerable();
            string[] nameArray = { "" };
            Array.Resize(ref nameArray, catalog.gameCatalog.Length);
            for (int i = 0; i < catalog.gameCatalog.Length; i++) nameArray[i] = catalog.gameCatalog[i].Name;
            for (int i = 0; i < nameArray.Length; i++) catalog.RemoveItem(nameArray[i]);
            try
            {
                using (BinaryReader reader = new BinaryReader(File.Open(pathBinary, FileMode.Open)))
                {
                    int size = reader.ReadInt32();
                    for (int i = 0; i < size; i++)
                    {
                        string name = reader.ReadString();
                        string image = reader.ReadString();
                        int num = reader.ReadInt32();
                        string desc = reader.ReadString();
                        string gen = reader.ReadString();
                        catalog.AddItem(name, image, num, gen, desc);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Файл с таким именем не существует.");
                catalog.AddItem("", "", 0, "", "");
            }
            return catalog;
        }
        static public void CompressFile(string sourceFile, string compressedFile)
        {
            // поток для чтения исходного файла
            using (FileStream sourceStream = new FileStream(sourceFile, FileMode.OpenOrCreate))
            {
                // поток для записи сжатого файла
                using (FileStream targetStream = File.Create(compressedFile))
                {
                    // поток архивации
                    using (GZipStream compressionStream = new GZipStream(targetStream, CompressionMode.Compress))
                    {
                        sourceStream.CopyTo(compressionStream); // копируем байты из одного потока в другой
                        MessageBox.Show("Сжатие файла завершено");
                    }
                }
            }
        }
        static public void DecompressFile(string compressedFile, string targetFile)
        {
            // поток для чтения из сжатого файла
            using (FileStream sourceStream = new FileStream(compressedFile, FileMode.OpenOrCreate))
            {
                // поток для записи восстановленного файла
                using (FileStream targetStream = File.Create(targetFile))
                {
                    // поток разархивации
                    using (GZipStream decompressionStream = new GZipStream(sourceStream, CompressionMode.Decompress))
                    {
                        decompressionStream.CopyTo(targetStream);
                        MessageBox.Show("Файл восстановлен.");
                    }
                }
            }
        }
        static public void BinarySave(GameEnumerable catalog)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(pathBinary, FileMode.Create)))
            {
                writer.Write(catalog.gameCatalog.Length);
                foreach (GameCatalog s in catalog.gameCatalog)
                {
                    writer.Write(s.Name);
                    writer.Write(s.Image);
                    writer.Write(s.Number);
                    writer.Write(s.Description);
                    writer.Write(s.Genre);
                }
            }
        }
        static public GameEnumerable LoadFromFile()
        {
            GameEnumerable catalog = new GameEnumerable();
            try
            {
                using (StreamReader sr = new StreamReader(File.Open(path, FileMode.Open), System.Text.Encoding.Default))
                {
                                        int size = Convert.ToInt32(sr.ReadLine());
                    string[] nameArray = { "" };
                    Array.Resize(ref nameArray, catalog.gameCatalog.Length);
                    for (int i = 0; i < catalog.gameCatalog.Length; i++) nameArray[i] = catalog.gameCatalog[i].Name;
                    for (int i = 0; i < nameArray.Length; i++) catalog.RemoveItem(nameArray[i]);
                    for (int i = 0; i < size; i++)
                    {
                        string name = sr.ReadLine();
                        string image = sr.ReadLine();
                        string desc = sr.ReadLine();
                        string gen = sr.ReadLine();
                        int num = Convert.ToInt32(sr.ReadLine());
                        catalog.AddItem(name, image, num, gen, desc);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Файл с таким именем не существует.");
                catalog.AddItem("", "", 0, "", "");
            }
            return catalog;
        }
        static public void SaveToFile(GameEnumerable catalog)
        {
            using (StreamWriter sr = new StreamWriter(File.Open(path, FileMode.Create)))
            {
                string line = Convert.ToString(catalog.gameCatalog.Length);
                sr.WriteLine(line);
                for (int i = 0; i < catalog.gameCatalog.Length; i++)
                {
                    line = catalog.gameCatalog[i].Name;
                    sr.WriteLine(line);
                    line = catalog.gameCatalog[i].Image;
                    sr.WriteLine(line);
                    line = catalog.gameCatalog[i].Description;
                    sr.WriteLine(line);
                    line = catalog.gameCatalog[i].Genre;
                    sr.WriteLine(line);
                    line = Convert.ToString(catalog.gameCatalog[i].Number);
                    sr.WriteLine(line);
                }

            }
        }
    }
}
