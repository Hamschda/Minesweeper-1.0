using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Button[,] btn = new Button[9,9]; 
            int size = 17; //Buttongröße
            
            //Erstellen und Konfigurieren Buttons
            for (int x = 0; x < 9; x++)
            {
                
                for (int y = 0; y < 9; y++)
                {
                    btn[x, y] = new Button();
                    btn[x, y].Name = "Button" + x + y;
                    btn[x, y].Size = new Size(size, size);
                    btn[x, y].Location = new Point(x * (size - 1), y * (size - 1));
                    btn[x, y].Text = Convert.ToString(x) + Convert.ToString(y);
                    btn[x, y].Image = Properties.Resources.blank;
                    btn[x, y].TabStop = false;
                    btn[x, y].FlatStyle = FlatStyle.Flat;
                    btn[x, y].FlatAppearance.BorderSize = 0;
                    btn[x, y].FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
                    this.Controls.Add(btn[x, y]);
                }
            }
            
        }

        public int uncover(int x, int y)
        {
            int value = 0;
            return value;
        }

        /*private void button1_Click(object sender, EventArgs e)
        {

        }*/
    }
}
