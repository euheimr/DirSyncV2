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

            //TODO 


        }

 
        //On button click from the form, compares files in "From" to "To" dir, only copies
        // & overwrites "To" files..
        private void okButton_Click(object sender, EventArgs e)
        {

            Engine.process();
            //check if file paths are valid
            string result = "";

            Engine.IsValidPath(fromTextBox.Text);
            Engine.IsValidPath(toTextBox.Text);
            Engine.CheckPath(fromTextBox.Text, out result);
            Engine.CheckPath(toTextBox.Text, out result);

            //execute dircopy method on okButton click
            Engine.DirCopy(fromTextBox.Text, toTextBox.Text, true);

            //if all of this is done, it will show the success message
            MessageBox.Show("Donezo!");


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
                //if this works, it will save the file with those directories, then 
                //display a success message box
                MessageBox.Show("Saved the directory locations!");


        }
    }
}
