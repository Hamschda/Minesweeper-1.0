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
        Button[,] btn = new Button[9, 9];
        int[] bmb_x = new Int32[5]; // Position der Bomben X-Koordinate
        int[] bmb_y = new Int32[5]; // Position der Bomben Y-Koordinate
        int bombenzahl = 5; // Anzahl der Bomben
                
        public Form1()
        {
            InitializeComponent();
            int size = 17; //Buttongröße
            
            Random random = new Random(); // Random zum erzeugen der Bombenpositionen
            // Erzeugen der Bombenpositionen
            for (int i = 0; i < bombenzahl; i++)
            {
                int pos_x = random.Next(0, 8);
                int pos_y = random.Next(0, 8);
                bmb_x[i] = pos_x;
                bmb_y[i] = pos_y;
            }
            
            
            //Erstellen und Konfigurieren der Buttons
            for (int x = 0; x < 9; x++)
            { 
                for (int y = 0; y < 9; y++)
                {
                    btn[x, y] = new Button();
                    btn[x, y].Name = "Button"+ x + y;
                    btn[x, y].Tag = x + "." + y;
                    btn[x, y].Size = new Size(size, size);
                    btn[x, y].Click += new EventHandler(this.button_Click);
                    btn[x, y].Location = new Point(x * (size - 1), y * (size - 1));
                 // btn[x, y].Text = Convert.ToString(x) + Convert.ToString(y); // Beschriftung der Buttons entfernt um Image besser sehen zu können
                    btn[x, y].Image = Properties.Resources.blank;
                    btn[x, y].TabStop = false;
                    btn[x, y].FlatStyle = FlatStyle.Flat;
                    btn[x, y].FlatAppearance.BorderSize = 0;
                    btn[x, y].FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
                    this.Controls.Add(btn[x, y]);
                }
            }
        }

        void button_Click(object sender, EventArgs e)
        {
            string curtentTag = (string)((Button)sender).Tag; //auslesen des Tags
            string[] coord = curtentTag.Split('.'); //zerteilen des Tags in die Koordinaten
            int x = Convert.ToInt32(coord[0]); 
            int y = Convert.ToInt32(coord[1]);
         // MessageBox.Show(x+ "," +y);
         // MessageBox.Show(Convert.ToString(uncover(x,y)));
            uncover(x, y); //aufdecken ohne MessageBox
            
            for (int i = 0; i < bombenzahl; i++) 
            {
                if (x == bmb_x[i] && y == bmb_y[i])
                {
                    btn[x, y].Image = Properties.Resources.num_2; // Hier Bombenimage reinsetzen
                }
            }
            
        }

        public int uncover(int x, int y)
        {
            int value = 0;
            btn[x, y].Image = Properties.Resources.uncover;          
            return value;
        }

        /*private void button1_Click(object sender, EventArgs e)
        {

        }*/
    }
}
