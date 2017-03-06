using System;
using System.Collections.Generic;
using System.Windows.Forms;
//using Library;

namespace DirSyncV2
{
    public class Program
    {


        //stores date when files are last modded
        enum FileState
        {
            lastModified,
            Directory,
        }

        // 
        //  MAIN METHOD
        //
        [STAThread]
        public static void Main(string[] args)
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Initialize Form
            Application.Run(new frmMain());

            //Initialize new Form instance, so I can control the inputs from the Form
            //Form form = new Form(); 

            //Get "from" and "to" directory path for copying, then set it to the strings "from" and "to"
            //string from = fromDirBox.Text;
            //string to = toDirBox.Text;

            //why don't i use an array to store the dir paths?? string[0] = fromDirBox, string[1] = toDirBox
            //string[] dirPaths = new string[2]; Yeah that's not going to be easy...
            //Trying List<T>
            
            

            
        }
        

    }
}

