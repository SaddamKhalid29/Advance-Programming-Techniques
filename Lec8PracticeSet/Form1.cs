using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Lec8PracticeSet
{
    public partial class Form1 : Form
    {
        List<Student> students = new List<Student>();   
        Student currStudent=null;
        int currIndex = -1;
        public Form1()
        {
            InitializeComponent();
        }
        // At Step1.Form Load event will call
        private void Form1_Load(object sender, EventArgs e)
        {
            if(File.Exists("temp.txt"))
                ReadDataFromFile();

            if (students.Count > 0)
            { 
                currIndex = students.Count - 1;
                currStudent = students[currIndex];
                
            }
            if(currStudent!=null)
                FromDataToUI();
            EnableControls();
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            currStudent = new Student();
            currIndex = students.Count;
            EnableControls();
            FromDataToUI();
        }

        private void FromDataToUI()
        {
            txtID.Text = Convert.ToString(currStudent.ID);
            txtName.Text=currStudent.Name;
            txtAge.Text=Convert.ToString(currStudent.Age);
            txtGender.SelectedIndex=currStudent.Gender==Student.GENDER.Male?0:1;
            txtMail.Text = currStudent.Gmail;
            txtDOB.Value = currStudent.DOB;
            
        }

        private void EnableControls()
        {
            if (currStudent == null)
            {
                txtID.Enabled = false;
                txtName.Enabled=false;
                txtAge.Enabled = false;
                txtMail.Enabled = false;
                txtGender.Enabled = false;
                txtDOB.Enabled=false;
                btnSave.Enabled=false;
                btnPrev.Enabled=false;
                btnNext.Enabled=false;
            }
            else
            {
                txtID.Enabled =true;
                txtName.Enabled =true;
                txtAge.Enabled = true;
                txtMail.Enabled = true;
                txtGender.Enabled = true;
                txtDOB.Enabled = true;
                btnSave.Enabled = true;
            }
            if (currIndex > 0)
            {
                btnPrev.Enabled = true;
            }
            else
            {
                btnPrev.Enabled = false;

            }
            if (currIndex < students.Count - 1)
            {
                btnNext.Enabled = true;
            }
            else
            {
                btnNext.Enabled= false;
            }
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            FromUIToData();
            if(currIndex == students.Count)
            {
                students.Add(currStudent);
            }
            lbloutput.Text = students.Count.ToString();

            WriteDataToFile();
        }

        private void FromUIToData()
        {
            currStudent.ID=Convert.ToInt32(txtID.Text);
            currStudent.Name=txtName.Text;
            currStudent.Age= Convert.ToInt32(txtAge.Text);
            currStudent.Gender = txtGender.SelectedIndex == 0 ? Student.GENDER.Male : Student.GENDER.female;
            currStudent.Gmail= txtMail.Text;
           // currStudent.selec
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (currIndex <= 0) {
                return;
            }
            currIndex--;
            currStudent=students[currIndex];
            FromDataToUI();
            EnableControls();

        }
        public void ReadDataFromFile()
        {


            FileStream filestream = new FileStream("temp.txt", FileMode.OpenOrCreate);
        TextReader textReader = new StreamReader(filestream);

        XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Student>));
        students = (List<Student>) xmlSerializer.Deserialize(textReader);


        textReader.Close();
        filestream.Close();
    }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (currIndex >= students.Count-1) {

                return;
            }
            currIndex++;
            currStudent= students[currIndex];
            FromDataToUI();
            EnableControls();
        }
        public void WriteDataToFile()
        {
            FileStream filestream = new FileStream("temp.txt", FileMode.OpenOrCreate);
            TextWriter textWriter = new StreamWriter(filestream);

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Student>));
            xmlSerializer.Serialize(textWriter, students);


            textWriter.Close();
            filestream.Close();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnNew_Click(sender, e);
        }
    }
   
}

