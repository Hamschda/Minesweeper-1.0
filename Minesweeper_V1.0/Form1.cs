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
        static int bombenzahl = 9;
        Button[,] btn = new Button[9, 9];
        int[] bmb_x = new Int32[bombenzahl]; // Position der Bomben X-Koordinate
        int[] bmb_y = new Int32[bombenzahl]; // Position der Bomben Y-Koordinate
        //int bombenzahl = 5; // Anzahl der Bomben
        Bitmap blankBitmap = Properties.Resources.blank; // Abspeichern der Resourcen in Variablen
        Bitmap uncoverBitmap = Properties.Resources.uncover;
        Bitmap flagBitmap = Properties.Resources.flag;
        Bitmap mineBitmap = Properties.Resources.mine;
        Bitmap redMineBitmap = Properties.Resources.red_mine;
        Bitmap num1Bitmap = Properties.Resources.num_1;
        Bitmap num2Bitmap = Properties.Resources.num_2;
        Bitmap num3Bitmap = Properties.Resources.num_3;
        Bitmap num4Bitmap = Properties.Resources.num_4;
        Bitmap num5Bitmap = Properties.Resources.num_5;
        Bitmap num6Bitmap = Properties.Resources.num_6;
        Bitmap num7Bitmap = Properties.Resources.num_7;
        Bitmap num8Bitmap = Properties.Resources.num_8;
                
        public Form1()
        {
            InitializeComponent();
            int size = 17; //Buttongröße
            
            Random random = new Random(); // Random zum erzeugen der Bombenpositionen
            // Erzeugen der Bombenpositionen
            for (int i = 0; i < bombenzahl; i++)
            {
                int pos_x = random.Next(0, 8); //# Prüfung, ob Bombenposition bereits vergeben
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
                    btn[x, y].Tag = x + "." + y; //Koordinaten als Tag abspeichern
                    btn[x, y].Size = new Size(size, size);
                    btn[x, y].MouseDown += new MouseEventHandler(this.button_Click); //MouseDown -> ermöglicht Auswertung von Rechts-Klick
                    btn[x, y].Location = new Point(x * (size - 1), y * (size - 1)+40);
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

            if (e.Button == MouseButtons.Right) //Prüfen, ob Rechts-Klick //#Blockade, wenn gameover
            {
                if (btn[x, y].Image == blankBitmap) //#Anzahl an Flaggen beschränken
                {
                    btn[x, y].Image = flagBitmap;
                }
                else if (btn[x, y].Image == flagBitmap)
                {
                    btn[x, y].Image = blankBitmap;
                }
            }
            if (e.Button == MouseButtons.Left) //Prüfen, ob Links-Klick //#Blockade, wenn gameover
            {
                countMines(x, y); //umliegende Minen zählen

                for (int i = 0; i < bombenzahl; i++)
                {
                    if (x == bmb_x[i] && y == bmb_y[i])
                    {
                        btn[x, y].Image = redMineBitmap;
                        // #hier alle anderen Bomben aufdecken 
                    }
                }
            }  
        }

        public void countMines(int x, int y)
        {
            int value = 0;
            //Schleife zum Suchen und Zählen der umliegenden Minen
            for (int iy = -1; iy <= 1; iy++)
            {
                for (int ix = -1; ix <= 1; ix++)
                {
                   for (int i = 0; i < bombenzahl; i++)
                    {
                        if (x + ix == bmb_x[i] && y + iy  == bmb_y[i])
                        {
                            value++;
                        }
                    }
                }
            }
            uncover(x, y, value); //Weiterleiten des Zählwertes an die Uncover-Methode
        }

        public void uncover(int x, int y, int counts)
        {
            switch (counts)
            {
                case 0:
                    btn[x, y].Image = uncoverBitmap;
                    //Schleife zum Öffenen/Zählen der umliegenden Felder
                    for (int iy = -1; iy <= 1; iy++)
                    {
                        for (int ix = -1; ix <= 1; ix++)
                        {
                            if (ix == 0 && iy == 0)
                            {
                                ix++;
                            }
                            if (0 <= ix + x && ix + x <= 8 && 0 <= iy + y && iy + y <= 8) //Bedingung, um im Spielfeld zu bleiben
                            {
                                if(btn[ix + x, iy + y].Image == blankBitmap) //Bedinung, um nur blanke Felder zu betrachten
                                {
                                    countMines(ix + x, iy + y); //erneutes öffenen der Count-Methode mit neuer Feldkoordinate
                                }   
                            }
                        }
                    }
                    break;
                case 1:
                    btn[x, y].Image = num1Bitmap;
                    break;
                case 2:
                    btn[x, y].Image = num2Bitmap;
                    break;
                case 3:
                    btn[x, y].Image = num3Bitmap;
                    break;
                case 4:
                    btn[x, y].Image = num4Bitmap;
                    break;
                case 5:
                    btn[x, y].Image = num5Bitmap;
                    break;
                case 6:
                    btn[x, y].Image = num6Bitmap;
                    break;
                case 7:
                    btn[x, y].Image = num7Bitmap;
                    break;
                case 8:
                    btn[x, y].Image = num8Bitmap;
                    break;
            }
            //#Hier Zähler für aufgedeckte Felder einfügen
        }

        /*private void button1_Click(object sender, EventArgs e)
        {

        }*/
    }
}
