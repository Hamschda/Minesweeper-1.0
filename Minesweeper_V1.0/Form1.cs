﻿using System;
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
        Bitmap blankBitmap = Properties.Resources.blank;
        Bitmap uncoverBitmap = Properties.Resources.uncover;
        Bitmap flagBitmap = Properties.Resources.flag;
        Bitmap mineBitmap = Properties.Resources.mine;
        Bitmap redMineBitmap = Properties.Resources.red_mine;
        Bitmap num1Bitmap = Properties.Resources.num_1;
        Bitmap num2Bitmap = Properties.Resources.num_2;
                
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
                    btn[x, y].MouseDown += new MouseEventHandler(this.button_Click);
                    btn[x, y].Location = new Point(x * (size - 1), y * (size - 1));
                 // btn[x, y].Text = Convert.ToString(x) + Convert.ToString(y); // Beschriftung der Buttons entfernt um Image besser sehen zu können
                    btn[x, y].Image = blankBitmap;
                    btn[x, y].TabStop = false;
                    btn[x, y].FlatStyle = FlatStyle.Flat;
                    btn[x, y].FlatAppearance.BorderSize = 0;
                    btn[x, y].FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
                    this.Controls.Add(btn[x, y]);
                }
            }
        }

        void button_Click(object sender, MouseEventArgs e)
        {
            string curtentTag = (string)((Button)sender).Tag; //auslesen des Tags
            string[] coord = curtentTag.Split('.'); //zerteilen des Tags in die Koordinaten
            int x = Convert.ToInt32(coord[0]); 
            int y = Convert.ToInt32(coord[1]);
         // MessageBox.Show(x+ "," +y);
         // MessageBox.Show(Convert.ToString(uncover(x,y)));

            if (e.Button == MouseButtons.Right)
            {
                if (btn[x, y].Image == blankBitmap)
                {
                    btn[x, y].Image = flagBitmap;
                }
                else if (btn[x, y].Image == flagBitmap)
                {
                    btn[x, y].Image = blankBitmap;
                }
            }
            if (e.Button == MouseButtons.Left)
            {
                int counts = countMines(x, y); //umliegende Minen zählen

                switch (counts)
                {
                    case 0:
                        btn[x, y].Image = uncoverBitmap;
                        break;
                    case 1:
                        btn[x, y].Image = num1Bitmap;
                        break;
                    case 2:
                        btn[x, y].Image = num2Bitmap;
                        break;
                }

                for (int i = 0; i < bombenzahl; i++)
                {
                    if (x == bmb_x[i] && y == bmb_y[i])
                    {
                        btn[x, y].Image = mineBitmap; 
                    }
                }
            }  
        }

        public int countMines(int x, int y)
        {
            int value = 0;
            for (int iy = -1; iy <= 1; iy++)
            {
                for (int ix = -1; ix <= 1; ix++)
                {
                    if (ix == 0 && iy == 0)
                    {
                        ix++;
                    }
                    for (int i = 0; i < bombenzahl; i++)
                    {
                        if (x + ix == bmb_x[i] && y + iy  == bmb_y[i])
                        {
                            value++;
                        }
                    }
                }
            }
            btn[x, y].Image = uncoverBitmap;          
            return value;
        }

        /*private void button1_Click(object sender, EventArgs e)
        {

        }*/
    }
}
