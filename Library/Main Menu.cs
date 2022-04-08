using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library
{
    public partial class MainMenue : Form
    {

        //this part of the code handles the Form movement
        private bool mouseDown;
        private Point lastLocation;
        private void MainMenue_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }
        private void MainMenue_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }
        private void MainMenue_MouseUp_1(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }




        public MainMenue()
        {
            InitializeComponent();
        }


        //Adding Members Button.
        private void button2_Click(object sender, EventArgs e)
        {
            Form2 App = new Form2();            
            App.Location = this.Location;
            App.Show();
            this.Hide();
        }

     
        //Grant A Book Button.
        private void button3_Click(object sender, EventArgs e)
        {
            Form1 App = new Form1();
            App.Location = this.Location;
            App.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form3 App = new Form3();
            App.Location = this.Location;
            App.Show();
            this.Hide();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Members App = new Members();
            App.Location = this.Location;
            App.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Stored_Books App = new Stored_Books();
            App.Location = this.Location;
            App.Show();
            this.Hide();
        }
   

        private void button1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
