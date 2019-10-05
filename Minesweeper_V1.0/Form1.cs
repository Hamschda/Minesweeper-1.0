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
        static int bombenzahl = 9; // Anzahl der Bomben
        int flaggenzahl = bombenzahl; // Anzahl der Flaggen
        bool gameover = false;
        int width = 9; //Spielfeldbreie
        int height = 9; //Spielfeldhöhe
        Button[,] btn = new Button[100, 100];
        Button picEmojy = new Button();
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
            int size = 16; //Buttongröße
            this.Size = new Size(36 + width * size, 100 + height * size);
            this.MaximizeBox = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.BackColor = Color.FromArgb(192, 192, 192);
            
            Random random = new Random(); // Random zum erzeugen der Bombenpositionen
            // Erzeugen der Bombenpositionen
            for (int i = 0; i < bombenzahl; i++)
            {
                int pos_x = random.Next(0, width); //# Prüfung, ob Bombenposition bereits vergeben
                int pos_y = random.Next(0, height);
                bmb_x[i] = pos_x;
                bmb_y[i] = pos_y;
            }
            //Konfiguration des Emojy-Buttons
            picEmojy.Size = new Size(26, 26);
            picEmojy.Image = Properties.Resources.emojy_happy;
            picEmojy.Location = new Point(10 + width * size / 2 - 13, 13);
            picEmojy.TabStop = false;
            picEmojy.FlatStyle = FlatStyle.Flat;
            picEmojy.FlatAppearance.BorderSize = 0;
            picEmojy.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
            picEmojy.MouseClick += new MouseEventHandler(picEmojy_Click); // Klicken auf Emojy
            this.Controls.Add(picEmojy);

            //Konfiguration der oberen Eckteile
            PictureBox picHL = new PictureBox();
            picHL.Size = new Size(58, 52);
            picHL.Image = Properties.Resources.head_left;
            picHL.Location = new Point(0, 0);
            this.Controls.Add(picHL);

            PictureBox picHR = new PictureBox();
            picHR.Size = new Size(58, 52);
            picHR.Image = Properties.Resources.head_right;
            picHR.Location = new Point(width * 16 + 20 - 58, 0);
            this.Controls.Add(picHR);

            //Erstellen und Konfigurieren der Buttons
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    //Konfiguration der unteren Rahmenteile
                    if (x < (width - 2))
                    {
                        Bitmap FrameBitmap = Properties.Resources.frame;
                        FrameBitmap.RotateFlip(RotateFlipType.Rotate270FlipY);
                        PictureBox picFrB = new PictureBox();
                        picFrB.Size = new Size(16, 10);
                        picFrB.Image = FrameBitmap;
                        picFrB.Location = new Point(x * size + 26, height * size + 52);
                        this.Controls.Add(picFrB);
                    }
                    //Konfiguration der oberen Rahmenteile
                    if (x < (width - 6))
                    {
                        PictureBox picHM = new PictureBox();
                        picHM.Size = new Size(16, 52);
                        picHM.Image = Properties.Resources.head_mid;
                        picHM.Location = new Point(x * size + 10 + size * 3, 0);
                        this.Controls.Add(picHM);
                    }
                    //Konfiguration der seitlichen Rahmenteile
                    if (x == 0)
                    {
                        PictureBox picFrR = new PictureBox();
                        picFrR.Size = new Size(10, 16);
                        picFrR.Image = Properties.Resources.frame;
                        picFrR.Location = new Point(width * size + 20 - 10, y * size + 52);
                        this.Controls.Add(picFrR);

                        PictureBox picFrL = new PictureBox();
                        picFrL.Size = new Size(10, 16);
                        picFrL.Image = Properties.Resources.frame;
                        picFrL.Location = new Point(0, y * size + 52);
                        this.Controls.Add(picFrL);
                    }

                    btn[x, y] = new Button();
                    btn[x, y].Name = "Button" + x + y;
                    btn[x, y].Tag = x + "." + y; //Koordinaten als Tag abspeichern
                    btn[x, y].Size = new Size(size + 1, size + 1);
                    btn[x, y].MouseDown += new MouseEventHandler(this.button_Click); //MouseDown -> ermöglicht Auswertung von Rechts-Klick
                    btn[x, y].Location = new Point(x * size + 9, y * size + 51);
                    btn[x, y].Image = blankBitmap;
                    btn[x, y].TabStop = false;
                    btn[x, y].FlatStyle = FlatStyle.Flat;
                    btn[x, y].FlatAppearance.BorderSize = 0;
                    btn[x, y].FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
                    this.Controls.Add(btn[x, y]);
                }
            }
            PictureBox picBL = new PictureBox();
            picBL.Size = new Size(26, 26);
            picBL.Image = Properties.Resources.bottom_left;
            picBL.Location = new Point(0, 52 + (height - 1) * size);
            this.Controls.Add(picBL);

            PictureBox picBR = new PictureBox();
            picBR.Size = new Size(26, 26);
            picBR.Image = Properties.Resources.bottom_right;
            picBR.Location = new Point((width - 1) * size + 10, 52 + (height - 1) * size);
            this.Controls.Add(picBR);
    }

        void picEmojy_Click(object sender, MouseEventArgs e)
        {
            // Auswertung ob Spiel verloren ist wenn Emojy geklickt wird
            if (gameover == true)
            {
                MessageBox.Show("Restart");
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

            if (e.Button == MouseButtons.Right && gameover == false) //Prüfen, ob Rechts-Klick 
            {
                if (btn[x, y].Image == blankBitmap && flaggenzahl >= 1)
                {
                    btn[x, y].Image = flagBitmap;
                    flaggenzahl--; //Flaggenzahl minimieren bei gesetzter Flagge
                }
                else if (btn[x, y].Image == flagBitmap)
                {
                    btn[x, y].Image = blankBitmap;
                    flaggenzahl++; //Flaggenzahl erhöhen bei gesetzter Flagge
                }
            }
            if (e.Button == MouseButtons.Left && gameover == false) //Prüfen, ob Links-Klick
            {
                if (btn[x, y].Image != flagBitmap) // Prüfen ob eine Flagge auf dem Feld vorhanden ist
                {
                    countMines(x, y); //umliegende Minen zählen

                    for (int i = 0; i < bombenzahl; i++)
                    {
                        if (x == bmb_x[i] && y == bmb_y[i])
                        {
                            btn[x, y].Image = redMineBitmap;
                            gameover = true; // Für Blockade der Clicks
                            picEmojy.Image = Properties.Resources.emojy_sad;
                        }
                    }
                }
            }
            // Aufdecken der Bomben im Spiel
            if (gameover == true)
            {
                for (int ix = 0; ix < width; ix++) // Durchsuchen des Spielfeldes in x-Richtung
                {
                    for (int iy = 0; iy < height; iy++) // Durchsuchen des Spielfeldes in y-Richtung
                    {
                        for (int i = 0; i < bombenzahl; i++)
                        {
                            x = ix;
                            y = iy;
                            if (x == bmb_x[i] && y == bmb_y[i])
                            {
                                if (btn[x, y].Image != redMineBitmap && btn[x,y].Image != flagBitmap) // Abfrage ob Bombe die Aufgedeckt werden soll die erste Bombe war bzw. ob Bombe durch Fahne deaktiviert ist
                                {
                                    btn[x, y].Image = mineBitmap;
                                }
                            }
                        }
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
                            if (0 <= ix + x && ix + x < width && 0 <= iy + y && iy + y < height) //Bedingung, um im Spielfeld zu bleiben
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
