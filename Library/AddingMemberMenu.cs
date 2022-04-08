using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace Library
{
    public partial class Form2 : Form
    {

        String Fname;
        String Mname;
        String Lname;
        String Address;
        string age;        
        private bool mouseDown;
        private Point lastLocation;
        SqlConnection ConnRef = new SqlConnection(@"Data Source = DESKTOP-A3SF5SI\SQL2019; Initial Catalog = Library; Integrated Security = True");

        public Form2()
        {
            InitializeComponent();
        }

        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }
        private void button1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }
        private void button1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }
        private void button3_Click(object sender, EventArgs e)
        { 
            Erorr_Window Ref = new Erorr_Window();

            int count = 0;
            for(int i=0; i < textBox2.Text.Length;i++)
            {
                if (textBox2.Text[i] == ' ')
                {
                    count++;
                }
                
            }

            if (count == 2 && textBox3.Text.Length > 2)
            {
                Fname = textBox2.Text.Substring(0, textBox2.Text.IndexOf(" "));
                Mname = textBox2.Text.Substring(Fname.Length, textBox2.Text.IndexOf(" ", Fname.Length));
                Lname = textBox2.Text.Substring(Mname.Length + Fname.Length);
                Address = textBox3.Text;
                age = textBox1.Text;

                ConnRef.Open();
                SqlCommand Add_Mem = new SqlCommand(@"Insert into Member(Fname,Mname,Lname,Address,Age)
                Values('" + Fname + "','" + Mname + "','" + Lname + "','" + Address + "','" + age + "')", ConnRef);
                Add_Mem.ExecuteNonQuery();
                ConnRef.Close();
                
                Ref.ErrorMessage = "A Member Has Been Added";
                Ref.isAnError = false;
                Ref.Show();
            }else if (count < 2||count>2)
            {
                Ref.ErrorMessage = "Please Enter Their First, Middle And Last Name Seprated By Spaces";
                textBox2.Text = "";
                Ref.Show();

            }
            else if (textBox3.Text.Length < 2)
            {
                Ref.ErrorMessage = "Please Enter Their Address"; 
                textBox3.Text = "";
                Ref.Show();

            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            MainMenue App = new MainMenue();
            App.StartPosition = FormStartPosition.Manual;
            App.Location = this.Location;
            App.Show();
            this.Hide();
        }
        private void button5_Click_1(object sender, EventArgs e)
        {
            textBox2.Clear();
            textBox1.Clear();
            textBox3.Clear();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
