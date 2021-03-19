using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace FileSplitter
{
    class Program
    {
        public static int Maxlength = 10000000;
        static void Main(string[] args)
        {
            Console.WriteLine("Input file name to be split");
            var fileName = Console.ReadLine();
            Console.WriteLine("Input chunk size (In MB)");
            var checkIn = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(checkIn))
            {
                Maxlength = Int32.Parse(checkIn);
            }
            Maxlength *= 1000000;
            var fileInfo = new FileInfo(Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + fileName);
            var fileLength = fileInfo.Length;
            var chunkNumbers = fileLength / Maxlength;
            if (chunkNumbers > 100)
            {
                Console.WriteLine("Too small a chunk size");
                Console.ReadLine();
                return;
            }
            var root = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + $"SeperatedFilesFromFileSplitter";
            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
            }

            Split(root, fileInfo, chunkNumbers);
            Console.WriteLine("done");
            Console.ReadLine();
        }

        private static void Split(string savePath, FileInfo fileInfo, long chunkSize)
        {
            var lines = File.ReadAllLines(fileInfo.DirectoryName + Path.DirectorySeparatorChar + fileInfo.Name).ToList();
            var chunks = lines.ChunkBy(lines.Count / chunkSize);
            int x = 0;
            foreach (var chunk in chunks)
            {
                File.WriteAllLines(savePath + Path.DirectorySeparatorChar + x + fileInfo.Name, chunk);
                x++;
            }
        }

        private static long FileLength(string file)
        {
            long length = new System.IO.FileInfo(file).Length;
            return length;
        }
    }
}
