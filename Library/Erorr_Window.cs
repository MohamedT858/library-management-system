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
    public partial class Erorr_Window : Form
    {
        
        public string ErrorMessage="";
        public bool isAnError = true;

        public Erorr_Window()
        {
            InitializeComponent();
           
        }


        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Erorr_Window_Load(object sender, EventArgs e)
        {
            label1.Text = ErrorMessage;

            pictureBox2.Visible = false;

            if (isAnError == false) {
                pictureBox1.Visible = false;
                pictureBox2.Visible = true;

            }

        }



        private bool mouseDown;
        private Point lastLocation;
        private void Erorr_Window_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }
        private void Erorr_Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point((this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }
        private void Erorr_Window_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }
    }
}
