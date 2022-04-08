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
    public partial class Stored_Books : Form
    {
        SqlConnection ConnRef = new SqlConnection(@"Data Source = DESKTOP-A3SF5SI\SQL2019; Initial Catalog = Library; Integrated Security = True");
        private bool mouseDown;
        private Point lastLocation;


        public Stored_Books()
        {
            InitializeComponent();
            ConnRef.Open();
            SqlDataAdapter adp = new SqlDataAdapter("Select * From Stored_Books", ConnRef);
            DataTable Tbl = new DataTable();
            adp.Fill(Tbl);
            ConnRef.Close();
            dataGridView1.DataSource = Tbl;
        }


        private void button4_Click(object sender, EventArgs e)
        {
            MainMenue App = new MainMenue();
            App.StartPosition = FormStartPosition.Manual;
            App.Location = this.Location;
            App.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void panel2_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }
    }
}
