using System;
using System.IO;
using System.Security;
using System.Text;
using System.Windows.Forms;
using System.Security.Permissions;

namespace Library
{
    public class Engine
    {

        //This string is used for the WriteToFile method, which is located below
        string saveDirLocation = @"C:\DirSync";

        //file location
        string fileLoc = @"C:\DirSync\userDirectories.txt";


        /// <summary>
        /// Gets a value that tells whether dirPath is a valid path
        /// </summary>
        /// <param name="dirPath"></param>
        /// <returns></returns>
        /// 
        public static bool IsValidPath(string dirPath)
        {
            string result;
            return CheckPath(dirPath, out result);
        }

        //TODO
        public static void process()
        {



        }
        // This method is to recursively look and copy directories
        public static void DirCopy(string fromDir, string toDir, bool copySubDir)
        {
            //find sub-directories
            DirectoryInfo dirInfo = new DirectoryInfo(fromDir);
            DirectoryInfo[] fromDirs = dirInfo.GetDirectories();

            // if the destination directory doesn't exist, create it
            if (!Directory.Exists(toDir))
            {
                Directory.CreateDirectory(toDir);
            }

            if (Directory.Exists(toDir))
            {
                FileInfo[] files = dirInfo.GetFiles();

                //Grab the files in the directory
                foreach (FileInfo file in files)
                {
                    //get the WriteTime on fromDir, and toDir.
                    DateTime dtFrom = File.GetLastWriteTime(fromDir);
                    DateTime dtTo = File.GetLastWriteTime(toDir);
                    FileInfo toFileInfo = new FileInfo(toDir);

                    //if destination directory is older than the original "from" directory, 
                    // recursively copy from "fromDir" to "toDir"
                    string toFilePath = Path.Combine(toDir, file.Name);
                    file.CopyTo(toFilePath, true);

                }


                foreach (DirectoryInfo subdir in fromDirs)
                {
                    string temppath = Path.Combine(toDir, subdir.Name);
                    DirCopy(subdir.FullName, temppath, copySubDir);
                }

            }
            //TODO add SQL functions   

        }
        /// <summary>
        /// Returns absolute path for dirPath (user defined) string. A
        /// return value will indicate if the path is valid or not.
        /// </summary>
        /// <param name="dirPath"></param> User specified path
        /// <param name="result"></param> 
        /// <returns>true</returns> if path is correct, otherwise false
        /// 
        public static bool CheckPath(string dirPath, out string result)
        {
            result = String.Empty;
            if (String.IsNullOrWhiteSpace(dirPath))
            {
                return false;
            }
            bool status = false;

            try
            {
                result = Path.GetFullPath(dirPath);
                status = true;
            }

            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return status;
        }



    }
}
