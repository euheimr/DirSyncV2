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

            catch (ArgumentException)
            {
                MessageBox.Show("Bad Arguments");
            }

            catch (SecurityException)
            {
                MessageBox.Show("File is read only.");
            }

            catch (NotSupportedException)
            {
                MessageBox.Show("Not supported.");
            }

            catch (PathTooLongException)
            {
                MessageBox.Show("Path is too long.");
            }
            return status;
        }

        //This method, when invoked, will write a tvt file to:
        // C:\Users\%USER_NAME%\%APPDATA%\Roaming\DirSync
        // this will be used at the end of the okButton click method
        // (refer to frmMain.cs)
        public void WriteToFile(string fromDirSave, string toDirSave)
        {
            
            //create an array which will be used to write to a file
            string[] DirSave = { fromDirSave, toDirSave };

            DirectoryInfo dir = new DirectoryInfo(saveDirLocation);

            // checks the directory to see if %APPDATA%\DirSync exists
            checkSaveDir();

            DirectoryInfo[] dirs = dir.GetDirectories();
            // Writes the two strings to userDirectories.txt
            PermsPath();
            File.Open(saveDirLocation + "userDirectories.txt", FileMode.Create);
            File.WriteAllLines(saveDirLocation + "userDirectories.txt", DirSave);
        }

        public void ReadFromFile()
        {
            //check if dirsync directory exists
            checkSaveDir();
            //checks to see if the directory locations have been saved
            if (File.Exists(fileLoc))
            {
                try
                {
                    PermsPath();
                    string[] DirSave = File.ReadAllLines(fileLoc, Encoding.UTF8);
                    
                }
                catch (UnauthorizedAccessException e)
                {
                    MessageBox.Show(e.Message);
                }

            }
            else
            {
                File.Create(fileLoc);
            }
 
        }


        //check if save directory exists
        public void checkSaveDir()
        {
            DirectoryInfo dir = new DirectoryInfo(saveDirLocation);
            if (!Directory.Exists(saveDirLocation))
            {
                Directory.CreateDirectory(saveDirLocation);
            }
        }

        //TODO: FIX THE PERMISSIONS FOR FILE EDIT
        // permissions for reading and writing to saveDirLocation
        public void PermsPath()
        {
            try
            {
                FileIOPermission perms = new FileIOPermission(PermissionState.Unrestricted);
                perms.AddPathList(FileIOPermissionAccess.AllAccess, fileLoc);
                checkSaveDir();
            }
            catch (UnauthorizedAccessException e)
            {
                MessageBox.Show(e.Message);
            }
        }

    }
}
