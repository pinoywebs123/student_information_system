using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace mycrud
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            student_list();
        }
        
        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Do you really want to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(dialog == DialogResult.Yes)
            {
                this.Dispose();
                Application.Exit();
            }
            else if(dialog == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
        string Gender = "";
        private void btnSave_Click(object sender, EventArgs e)
        {
            string student_id = this.txtID.Text;
            string lastname = this.txtLN.Text;
            string firstname = this.txtFN.Text;
            string middlename = this.txtMN.Text;
            string dob = this.dtpDOB.Text;
            string email = this.txtEmail.Text;
            string contact = this.txtContact.Text;
            string address = this.txtAddress.Text;
            string gender = this.Gender;

           

            if (string.IsNullOrEmpty(student_id) || string.IsNullOrEmpty(lastname) || string.IsNullOrEmpty(firstname) || string.IsNullOrEmpty(middlename) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(contact) || string.IsNullOrEmpty(address) || string.IsNullOrEmpty(gender))
            {
                MessageBox.Show("Please fill-up all fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
            {
                try
                {
                    string conString = "datasource=localhost;port=3306;username=root;password=admin;database=studentsystemdb";
                    MySqlConnection con = new MySqlConnection(conString);
                    string query = "insert into students values('"+Int32.Parse(student_id) + "','"+lastname+"','"+firstname+"','"+middlename+"','"+gender+"','"+dob+"','"+email+"','"+contact+"','"+address+"')";
                    MySqlCommand command = new MySqlCommand(query, con);
                    MySqlDataReader reader;
                    con.Open();
                    reader = command.ExecuteReader();
                    MessageBox.Show("Data Saved");
                    student_list();
                    con.Close();
                    
                }
                catch(Exception aw)
                {
                    MessageBox.Show("Error"+aw);
                }

            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Gender = "Male";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Gender = "Female";
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        void student_list()
        {
            string conString = "datasource=localhost;port=3306;username=root;password=admin;database=studentsystemdb";
            MySqlConnection con = new MySqlConnection(conString);
            string query = "select * from students";
            MySqlCommand command = new MySqlCommand(query, con);

            try
            {
                con.Open();
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = command;
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    BindingSource source = new BindingSource();
                    source.DataSource = dt;
                    studentList.DataSource = dt;
                    adapter.Update(dt);
                con.Close();
            }catch(Exception aw)
            {
                MessageBox.Show("Error" + aw);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtID.Text = "";
            txtLN.Text = "";
            txtFN.Text = "";
            txtMN.Text = "";
            txtEmail.Text = "";
            txtContact.Text = "";
            txtAddress.Text = "";
            this.btnSave.Enabled = true;
            this.panel1.Enabled = true;
        }

        private void studentList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.btnSave.Enabled = false;
            if(e.RowIndex >=0)
            {
                DataGridViewRow data = this.studentList.Rows[e.RowIndex];
                txtID.Text = data.Cells["student_id"].Value.ToString();
                txtLN.Text = data.Cells["lastname"].Value.ToString();
                txtFN.Text = data.Cells["firstname"].Value.ToString();
                txtMN.Text = data.Cells["middlename"].Value.ToString();
                txtEmail.Text = data.Cells["email"].Value.ToString();
                txtContact.Text = data.Cells["contact"].Value.ToString();
                txtAddress.Text = data.Cells["address"].Value.ToString();
                
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string conString = "datasource=localhost;port=3306;username=root;password=admin;database=studentsystemdb";
                MySqlConnection con = new MySqlConnection(conString);
                string query = "delete from students where student_id='" + Int32.Parse(txtID.Text) + "'";
                MySqlCommand command = new MySqlCommand(query, con);
                MySqlDataReader reader;
                con.Open();
                reader = command.ExecuteReader();
                MessageBox.Show("Data Deleted");
                student_list();
                con.Close();
                this.btnSave.Enabled = true;
            }catch(Exception aw)
            {
                MessageBox.Show("Error" + aw);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string conString = "datasource=localhost;port=3306;username=root;password=admin;database=studentsystemdb";
                MySqlConnection con = new MySqlConnection(conString);
                string query = "update students set student_id = '" + Int32.Parse(txtID.Text) + "', lastname='" + txtLN.Text + "',firstname='" + txtFN.Text + "', middlename='" + txtMN.Text + "', gender='" + Gender + "',dob='" + dtpDOB.Text + "',email='" + txtEmail.Text + "',contact='" + txtContact.Text + "', address='" + txtContact.Text + "', address='"+txtAddress.Text+"'";
                MySqlCommand command = new MySqlCommand(query, con);
                MySqlDataReader reader;
                con.Open();
                reader = command.ExecuteReader();
                MessageBox.Show("Data updated successfully!!");
                student_list();
                con.Close();
                this.btnSave.Enabled = true;
                this.panel1.Enabled = false;
            }
            catch(Exception aw)
            {
                MessageBox.Show("Error" + aw);
            }

        }
    }
}
