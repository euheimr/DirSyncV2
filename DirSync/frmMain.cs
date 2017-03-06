using System;
using System.Windows.Forms;
using Library;
using System.IO;
using System.Diagnostics;


namespace DirSyncV2
{
    public partial class frmMain : System.Windows.Forms.Form
    {
        public frmMain()
        {
            InitializeComponent();
            Engine dir = new Engine();
            //Read from the file if it exists
            dir.ReadFromFile();

        }

        // This method is to recursively look and copy directories
        private void DirCopy(string fromDir, string toDir, bool copySubDir)
        {
            //find sub-directories
            DirectoryInfo dir = new DirectoryInfo(fromDir);

            // if the dir doesn't exist, 
            if (!dir.Exists) 
            {
                //Just show a message box and stop the method
                MessageBox.Show("Invalid directory in: " + fromDir);
            }
            else
            {
                DirectoryInfo[] dirs = dir.GetDirectories();
                //if the directory doesn't exist, create it
                if (!Directory.Exists(toDir))
                {
                    Directory.CreateDirectory(toDir);
                }

                //if the destination Dir exists
                if (Directory.Exists(toDir))
                {
                    FileInfo[] files = dir.GetFiles();
                    //Grab the files in the directory
                    foreach (FileInfo file in files)
                    {
                        //get the WriteTime on fromDir, and toDir.
                        DateTime dtFrom = File.GetLastWriteTime(fromDir);
                        DateTime dtTo = File.GetLastWriteTime(toDir);

                        //if destination directory is older than the original "from" directory, 
                        // recursively copy from "fromDir" to "toDir"
                        try 
                        {
                            if (dtFrom >= dtTo)
                            {
                                // temppath is the string of destination directory + the file name
                                string temppath = Path.Combine(toDir, file.Name);
                                file.CopyTo(temppath, true);
                            }
                            /*  I don't need this, as there is no point to it. 
                            else
                            {
                                // if the files in the destination are newer, do not copy from
                                // the original directory
                                copySubDir = false;
                            }
                            */
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message);
                        }

                    }    

                    // If copying dirs, do the full copying
                    if (copySubDir)
                    {
                        foreach (DirectoryInfo subdir in dirs)
                        {
                            string temppath = Path.Combine(toDir, subdir.Name);
                            DirCopy(subdir.FullName, temppath, copySubDir);
                        }

                    }
                    // Writes to a file storing the "from" and "to" strings for dir location
                    // this file will be checked at the beginning of the program launch too
                    Engine instance = new Engine();
                    instance.PermsPath();
                    instance.WriteToFile(fromDir, toDir);
                }
            }

        }




        //On button click from the form, compares files in "From" to "To" dir, only copies
        // & overwrites "To" files..
        private void okButton_Click(object sender, EventArgs e)
        {
            


            //check if file paths are valid
            string result = "";

            Engine.IsValidPath(fromTextBox.Text);
            Engine.IsValidPath(toTextBox.Text);
            Engine.CheckPath(fromTextBox.Text, out result);
            Engine.CheckPath(toTextBox.Text, out result);

            //execute dircopy method on okButton click
            DirCopy(fromTextBox.Text, toTextBox.Text, true);

            //if all of this is done, it will show the success message
            MessageBox.Show("Donezo!");

            /*
            string result = "";

            //For storing directory strings
            List<string> list = new List<string>();
            list.Add(fromTextBox.Text);
            list.Add(toTextBox.Text);
            */
            //showing box text from fromDirBox

            //MessageBox.Show(fromTextBox.Text);
            


        }

        private void fromDirBox_TextChanged(object sender, EventArgs e)
        {
            fromTextBox.Text = Convert.ToString(fromTextBox.Text);

        }

        private void toDirBox_TextChanged(object sender, EventArgs e)
        {
            toTextBox.Text = Convert.ToString(toTextBox.Text);

        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private void folderBrowserDialog2_HelpRequest(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
        }

        private void textbox1_Click(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            Engine file = new Engine();
            // creating an instance so i can use ReadFromFile method from Engine
            //This will be run at runtime of frmMain

                file.PermsPath();
                file.ReadFromFile();
                //if this works, it will save the file with those directories, then 
                //display a success message box
                MessageBox.Show("Saved the directory locations!");


        }
    }
}
