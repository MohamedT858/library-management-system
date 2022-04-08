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
    public partial class Form3 : Form
    {

        SqlConnection ConnRef = new SqlConnection(@"Data Source = DESKTOP-A3SF5SI\SQL2019; Initial Catalog = Library; Integrated Security = True");
        Erorr_Window Ref = new Erorr_Window();
        DataTable Tbl = new DataTable();
        private bool mouseDown;
        private Point lastLocation;
     
        public Form3()
        {
            InitializeComponent();
            dataGridContent();
        }
        private void dataGridContent()
        {
            ConnRef.Open();
            SqlDataAdapter adp = new SqlDataAdapter("select * from Currently_Borrowed", ConnRef);
            DataTable Tbl = new DataTable();
            adp.Fill(Tbl);
            ConnRef.Close();
            dataGridView1.DataSource = Tbl;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            MainMenue App = new MainMenue();
            App.StartPosition = FormStartPosition.Manual;
            App.Location = this.Location;
            App.Show();
            this.Hide();
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
        private void button1_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell != null&& IsInGrid() == true)
            {
                    ConnRef.Open();
                    SqlCommand Returned_Book = new SqlCommand(@"Update Books 
                    Set Mem_Id= NULL ,Return_day = NULL,CheckOut_day= Null Where Book_Id='" + dataGridView1.CurrentCell.Value + "'", ConnRef);
                    Returned_Book.ExecuteNonQuery();
                    ConnRef.Close();
                    Ref.ErrorMessage = "A Book Has Been Returned!";
                    Ref.isAnError = false;
                    Ref.Show();
                    dataGridContent();

            }
            else
            {
                    Ref.ErrorMessage = "Please Selcet The Id Of The Returned Book Before Pressing The Button.";
                    Ref.Show();
            }
        }
            
        private bool IsInGrid()
        {
         
                for (int rows = 0; rows < dataGridView1.Rows.Count; rows++)
                {
                if (dataGridView1.CurrentCell.Value.ToString() == dataGridView1.Rows[rows].Cells[0].Value.ToString())
                    {
                    return true;
                    }
                }
            
            return false;
        }
    }
}

