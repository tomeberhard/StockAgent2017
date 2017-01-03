using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using System.Linq;

namespace StockAgent2017
{
    /// <summary>
    /// Class to read text files.
    /// </summary>
    class FileIO
    {
        /// <summary>
        /// Gobbles up the whole file as a string.
        /// </summary>
        /// <param name="fullPath"></param>
        /// <returns></returns>
        public static string Load(string fullPath)
        {
            FileStream fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(fs);

            string fileData = reader.ReadToEnd();

            reader.Close();
            fs.Close();

            return fileData;
        }

        /// <summary>
        /// Gobbles up the whole file, returning an ArrayList of the lines.
        /// </summary>
        /// <param name="fullPath"></param>
        /// <returns></returns>
        public static ArrayList LoadLines(string fullPath)
        {
            ArrayList lines = new ArrayList();

            try
            {
                FileStream fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(fs);

                while (!reader.EndOfStream)
                {
                    lines.Add(reader.ReadLine());
                }
                lines.TrimToSize();

                reader.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                String s = "Exception in FileIO.LoadLines(): " + ex.ToString();
            }

            return lines;
        }

        /// <summary>
        /// Saves the entire string into a file
        /// </summary>
        /// <param name="fullPath"></param>
        /// <param name="data"></param>
        public static void Save(string fullPath, string data)
        {
            FileStream fs = new FileStream(fullPath, FileMode.Append, FileAccess.Write);
            StreamWriter writer = new StreamWriter(fs);

            writer.WriteLine(data);

            writer.Close();
            fs.Close();
        }

        /// <summary>
        /// Returns a list of n files, sorted in reverse chronological order.
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="fileCount">Number of trading days to go back.</param>
        /// <returns></returns>
        public static ArrayList GetLatestFiles(string folder, int fileCount)
        {
            ArrayList fileList = new ArrayList();

            // get a list of all the files
            DirectoryInfo dirInfo = new DirectoryInfo(folder);
            foreach (FileInfo fileInfo in dirInfo.EnumerateFiles())
            {
                fileList.Add(fileInfo.Name);
            }

            // sort alphabetically
            fileList.Sort();
            fileList.Reverse();

            // return only fileCount
            if ( fileCount < fileList.Count )
            {
                int count = fileList.Count - fileCount;
                fileList.RemoveRange(fileCount, count);
            }
            return fileList;
        }

        public static List<FileInfo> GetFileList(string path)
        {
            List<FileInfo> files = new List<FileInfo>();

            DirectoryInfo dirInfo = new DirectoryInfo(path);
            FileInfo[] filesArray = dirInfo.GetFiles("*.txt");

            foreach (FileInfo file in filesArray)
            {
                files.Add(file);
            }

            return files;
        }


    }
}

