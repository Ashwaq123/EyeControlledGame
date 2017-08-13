using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Tarkizi1 
{
    public partial class AddUsers : UserControl
    {
        public String cName;
        public int CID;

        public AddUsers(String CoachName, int coachID)
        {
            InitializeComponent();
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent; 
            cName = CoachName;
            CID = coachID;
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            //textBox1.Focus();
 
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Form parentForm = (this.Parent as Form);
            parentForm.Controls.Clear();
            parentForm.Text = "تركيزي- عرض قائمة المستخدمين";
            User_List UserList_Control = new User_List(cName, CID);
            parentForm.Controls.Add(UserList_Control);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            DialogResult myDialogResult = MessageBox.Show("هل أنت متأكد من أنك تريد إنهاء البرنامج؟", "إنهاء البرنامج", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
        MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
            if (myDialogResult == DialogResult.Yes)
            {
                Form parentForm = (this.Parent as Form);
                parentForm.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            bool empty = false;
            if (string.IsNullOrWhiteSpace(this.textBox1.Text))
            {
                MessageBox.Show("أدخل الاسم الأول من فضلك",
        "تنبيه",
        MessageBoxButtons.OK,
        MessageBoxIcon.Warning,
        MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
                empty = true;
            }
            if (string.IsNullOrWhiteSpace(this.textBox2.Text))
            {
                MessageBox.Show("أدخل الاسم الأخير من فضلك",
        "تنبيه",
        MessageBoxButtons.OK,
        MessageBoxIcon.Warning,
        MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
                empty = true;
            }
            if (empty == false && string.IsNullOrWhiteSpace(this.textBox3.Text))
            {
                MessageBox.Show("أدخل العمر من فضلك",
        "تنبيه",
        MessageBoxButtons.OK,
        MessageBoxIcon.Warning,
        MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
                empty = true;
            }
            if (empty == false && string.IsNullOrWhiteSpace(this.textBox4.Text))
            {
                MessageBox.Show("أدخل رقم الهاتف من فضلك",
        "تنبيه",
        MessageBoxButtons.OK,
        MessageBoxIcon.Warning,
        MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
                empty = true;
            }
            if (empty == false && string.IsNullOrWhiteSpace(this.textBox5.Text))
            {
                MessageBox.Show("أدخل العنوان من فضلك",
        "تنبيه",
        MessageBoxButtons.OK,
        MessageBoxIcon.Warning,
        MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
                empty = true;
            }

            if (empty == false)
            {
                int selectedStatus = 0, selectedOthersDisorders = 0;

                selectedStatus = Convert.ToInt32(comboBox1.SelectedIndex);
                selectedOthersDisorders = Convert.ToInt32(comboBox2.SelectedIndex);
                try
                {
                    Char sex;
                    if (radioButton1.Checked == true)
                        sex = 'M';
                    else sex = 'F';

                    String FirstName = textBox1.Text.ToString().Replace("  ", string.Empty);
                    String LastName = textBox2.Text.ToString().Replace("  ", string.Empty);
                    string sql = "SELECT *  FROM users where FName='" + FirstName + "' and LName ='" + LastName + "' and CID= '" + CID + "';";
                    f1.objMySqlCon = new MySqlConnection(f1.cs);
                    MySqlCommand cmdSel = new MySqlCommand(sql, f1.objMySqlCon);
                    f1.objMySqlCon.Open();
                    f1.rdr = cmdSel.ExecuteReader();


                    if (!(f1.rdr.Read())) 
                    {
                        string insertSQL = "INSERT INTO users(FName, LName, Age, PhoneNo, Address, Status, OtherDisorders, CID, sex) VALUES ('" + FirstName + "', '" + LastName + "', '" + textBox3.Text.ToString() + "', '" + textBox4.Text.ToString() + "', '" + textBox5.Text.ToString() + "','" + selectedStatus + "','" + selectedOthersDisorders + "','" + CID + "','" + sex + "')";
                    f1.objMySqlCon = new MySqlConnection(f1.cs);
                    MySqlCommand cmd = new MySqlCommand(insertSQL, f1.objMySqlCon);
                    f1.objMySqlCon.Open();
                    int executed = 0;
                    executed = cmd.ExecuteNonQuery();
                    f1.objMySqlCon.Close();

                    if (executed == 1)
                    {
                        MessageBox.Show("تمت عملية الإضافة",
        "تم!");
                        textBox1.Text = "";
                        textBox2.Text = "";
                        textBox3.Text = "";
                        textBox4.Text = "";
                        Form parentForm = (this.Parent as Form);
                        parentForm.Controls.Clear();
                        parentForm.Text = "تركيزي";
                        User_List User_List_control = new User_List(cName, CID);
                        parentForm.Controls.Add(User_List_control);
                    }
                    else
                    {
                        MessageBox.Show("حدث خطأ أثناء عملية الإضافة، حاول مرة أخرى",
        "تنبيه",
        MessageBoxButtons.OK,
        MessageBoxIcon.Error,
        MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
                    }

                }
                    else
                        MessageBox.Show("هذا المستخدم موجود مسبقاً!",
        "تنبيه",
        MessageBoxButtons.OK,
        MessageBoxIcon.Warning,
        MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Error: {0}", ex.ToString());

                }

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form parentForm = (this.Parent as Form);
            parentForm.Controls.Clear();
            parentForm.Text = "تركيزي- عرض قائمة المستخدمين";
            User_List User_List_control = new User_List(cName, CID);
            parentForm.Controls.Add(User_List_control);
        }

        private void AddUsers_Load(object sender, EventArgs e)
        {
            textBox1.Focus();
            radioButton1.Checked = true;
        }

        private void label9_Click(object sender, EventArgs e)
        {
            Form parentForm = (this.Parent as Form);
            parentForm.Controls.Clear();
            parentForm.Text = "تركيزي- عرض قائمة المستخدمين";
            User_List UserList_Control = new User_List(cName, CID);
            parentForm.Controls.Add(UserList_Control);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}
