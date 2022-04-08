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

    public partial class Form1 : Form
    {
        SqlConnection ConnRef = new SqlConnection(@"Data Source = DESKTOP-A3SF5SI\SQL2019; Initial Catalog = Library; Integrated Security = True");
        AutoCompleteStringCollection coll = new AutoCompleteStringCollection();
        AutoCompleteStringCollection Book_coll = new AutoCompleteStringCollection();
        SqlDataAdapter da;
        int Mem_rows;
        int Book_rows;
        private bool mouseDown;
        private Point lastLocation;

        private void Grant_Click(object sender, EventArgs e)
        {
            Erorr_Window Ref = new Erorr_Window();
            if (isValid() == true&& isBookValid()==true&& DateTime.Compare(dateTimePicker1.Value, DateTime.Now) > 0)
            {
                int bookid = Convert.ToInt32(textBox1.Text.Substring(0, textBox1.Text.IndexOf('-')));
                int memid = Convert.ToInt32(textBox2.Text.Substring(0, textBox2.Text.IndexOf('-')));

                ConnRef.Open();
                SqlCommand Granting_Book = new SqlCommand(@"Update Books 
                Set Mem_Id='"+memid+"' ,Return_day ='"+ dateTimePicker1.Value + "',CheckOut_day= '"+DateTime.Now.ToString("M/d/yyyy")+"' " +
                "Where Book_Id='" +bookid +"'"  , ConnRef); 
                Granting_Book.ExecuteNonQuery();
                ConnRef.Close();
                Ref.ErrorMessage = "A Book Has Been Borrowed!";
                Ref.isAnError = false;
                Ref.Show();
                textBox1.Text = "";
            }
            else if(isValid()== false){

                Ref.ErrorMessage = "Member Name Not Found";
                textBox2.Text = "";
                Ref.Show();

            }else if(isBookValid()== false){

                Ref.ErrorMessage = "Book ID Not Found";

                Ref.Show();

            }else if(DateTime.Compare(dateTimePicker1.Value, DateTime.Now) < 0)
            {
                Ref.ErrorMessage = "Please Choose A Valid Return Date.";
                textBox1.Text = "";
                Ref.Show();
            }
        }

        private void button1_MouseDown_1(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }
        private void button1_MouseMove_1(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }
        private void button1_MouseUp_1(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            MainMenue App = new MainMenue();
            App.StartPosition = FormStartPosition.Manual;
            App.Location = this.Location;
            App.Show();
            this.Hide();
        }        
        public Form1()
        {
            InitializeComponent();
        }
        private void ClearInfo_Grant_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            textBox1.Clear();
            
        }   
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            AutoComplete(); 
        }


        //The logic for the auto complete textBox to select member names 
        private void AutoComplete()
        {
           
            da = new SqlDataAdapter("Select Mem_Id,Fname,Mname,Lname from Member order by Mem_Id asc", ConnRef);
            DataTable tbl = new DataTable();
            da.Fill(tbl);
           

            Mem_rows = tbl.Rows.Count;

            if (tbl.Rows.Count > 0){

                for (int i = 0; i < tbl.Rows.Count; i++){
                    coll.Add(tbl.Rows[i]["Mem_Id"].ToString()+ "-"+tbl.Rows[i]["Fname"].ToString() + tbl.Rows[i]["Mname"].ToString() + tbl.Rows[i]["Lname"].ToString());
                }
            }
            textBox2.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox2.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox2.AutoCompleteCustomSource = coll;

            da = new SqlDataAdapter("Select Book_Id, Title from Books order by Book_Id asc", ConnRef);
            DataTable BookID = new DataTable();
            da.Fill(BookID);
            Book_rows = BookID.Rows.Count;



            if (BookID.Rows.Count > 0)
            {

                for (int i = 0; i < BookID.Rows.Count; i++)
                {

                    Book_coll.Add(BookID.Rows[i]["Book_Id"].ToString() +"-"+ BookID.Rows[i]["Title"].ToString());

                }
            }
            textBox1.AutoCompleteMode = AutoCompleteMode.Suggest;

            textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;

            textBox1.AutoCompleteCustomSource = Book_coll;

        }

        private bool isValid()
        {
            if (Mem_rows > 0)
            {
                for (int i = 0; i < Mem_rows; i++)
                {
                    if (coll[i] == textBox2.Text)
                    {
                        return true;
                    }

                }
            }
            return false;
        }        
        private bool isBookValid()
        {
            if (Book_rows > 0)
            {
                for (int i = 0; i < Book_rows; i++)
                {
                    if (coll[i] == textBox2.Text)
                    {
                        return true;
                    }

                }
            }
            return false;
        }
    }
}
