using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        int bombenzahl; // Anzahl der Bomben
        int flaggenzahl; // Anzahl der Flaggen
        bool gameover = false;
        bool gamewin = false;
        int width; //Spielfeldbreie
        int height; //Spielfeldhöhe
        int aufgedecktefelder = 0;
        int timer = 0;
        int saveRadioBtn = 0;
        Button[,] btn = new Button[30, 24];
        Button[] picEmojy = new Button[1]; //Definition alls Array, um die Variable nicht mit Controls.Clear() zu löschen
        PictureBox[] counter1 = new PictureBox[1]; //Definition alls Array, um die Variable nicht mit Controls.Clear() zu löschen
        PictureBox[] counter2 = new PictureBox[1]; //Definition alls Array, um die Variable nicht mit Controls.Clear() zu löschen
        PictureBox[] counter3 = new PictureBox[1]; //Definition alls Array, um die Variable nicht mit Controls.Clear() zu löschen
        PictureBox[] timer1 = new PictureBox[1]; //Definition alls Array, um die Variable nicht mit Controls.Clear() zu löschen
        PictureBox[] timer2 = new PictureBox[1]; //Definition alls Array, um die Variable nicht mit Controls.Clear() zu löschen
        PictureBox[] timer3 = new PictureBox[1]; //Definition alls Array, um die Variable nicht mit Controls.Clear() zu löschen
        int[] bmb_x = new Int32[667]; // Position der Bomben X-Koordinate
        int[] bmb_y = new Int32[667]; // Position der Bomben Y-Koordinate
        
        Bitmap blankBitmap = Properties.Resources.blank; // Abspeichern der Resourcen in Variablen
        Bitmap uncoverBitmap = Properties.Resources.uncover;
        Bitmap flagBitmap = Properties.Resources.flag;
        Bitmap mineBitmap = Properties.Resources.mine;
        Bitmap redMineBitmap = Properties.Resources.red_mine;
        Bitmap crossedMineBitmap = Properties.Resources.crossed_mine;
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
                bombenzahl = 10;
                width = 8;
                height = 8;

                StartGame(width, height, bombenzahl);
                radioButton1.Checked = true;
        }

        public void StartGame(int width, int height, int bombenzahl)
        {
            InitializeComponent(); //Initialisieren der über den Designer erstellten Componenten (Menü)
            timer4.Start();
            int y_corr = 23; //y-Korrektur, um Platz für Menü zu haben
            int size = 16; //Buttongröße
            this.MaximizeBox = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.BackColor = Color.FromArgb(192, 192, 192);
            this.Size = new Size(25 + width * size, 113 + height * size);
            Random random = new Random(); // Random zum erzeugen der Bombenpositionen
            int pos_x;
            int pos_y;

            // Erzeugen der Bombenpositionen
            for (int i = 0; i < bombenzahl; i++)
            {
                pos_x = random.Next(0, width);
                pos_y = random.Next(0, height);
                bmb_x[i] = pos_x;
                bmb_y[i] = pos_y;
                for (int j = 0; j < i; j++)
                {
                    if (pos_x == bmb_x[j] && pos_y == bmb_y[j]) // Prüfen, ob Mine bereits vorhanden
                    {
                        i--; //Array-Platz wird erneut beschrieben
                    }
                } 
            }

            //Konfiguration des Emojy-Buttons
            picEmojy[0] = new Button();
            picEmojy[0].Size = new Size(26, 26);
            picEmojy[0].Image = Properties.Resources.emojy_happy;
            picEmojy[0].Location = new Point(10 + width * size / 2 - 13, 13 + y_corr);
            picEmojy[0].TabStop = false;
            picEmojy[0].FlatStyle = FlatStyle.Flat;
            picEmojy[0].FlatAppearance.BorderSize = 0;
            picEmojy[0].FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
            picEmojy[0].MouseClick += new MouseEventHandler(picEmojy_Click); // Klicken auf Emojy
            this.Controls.Add(picEmojy[0]);

            //Konfiguration des Counters
            counter1[0] = new PictureBox();
            counter1[0].Size = new Size(13, 23);
            counter1[0].Image = Properties.Resources._7S_0;
            counter1[0].Location = new Point(16+13*2, 14 + y_corr);
            this.Controls.Add(counter1[0]);

            counter2[0] = new PictureBox();
            counter2[0].Size = new Size(13, 23);
            counter2[0].Image = Properties.Resources._7S_0;
            counter2[0].Location = new Point(16 + 13, 14 + y_corr);
            this.Controls.Add(counter2[0]);

            counter3[0] = new PictureBox();
            counter3[0].Size = new Size(13, 23);
            counter3[0].Image = Properties.Resources._7S_0;
            counter3[0].Location = new Point(16, 14 + y_corr);
            this.Controls.Add(counter3[0]);

            //Konfiguration des Timers
            timer1[0] = new PictureBox();
            timer1[0].Size = new Size(13, 23);
            timer1[0].Image = Properties.Resources._7S_0;
            timer1[0].Location = new Point(width * size - 9, 14 + y_corr);
            this.Controls.Add(timer1[0]);

            timer2[0] = new PictureBox();
            timer2[0].Size = new Size(13, 23);
            timer2[0].Image = Properties.Resources._7S_0;
            timer2[0].Location = new Point(width * size - 9 - 13, 14 + y_corr);
            this.Controls.Add(timer2[0]);

            timer3[0] = new PictureBox();
            timer3[0].Size = new Size(13, 23);
            timer3[0].Image = Properties.Resources._7S_0;
            timer3[0].Location = new Point(width * size - 9 - 13 - 13, 14 + y_corr);
            this.Controls.Add(timer3[0]);

            //Konfiguration der oberen Eckteile
            PictureBox picHL = new PictureBox();
            picHL.Size = new Size(58, 52);
            picHL.Image = Properties.Resources.head_left;
            picHL.Location = new Point(0, 0 + y_corr);
            this.Controls.Add(picHL);

            PictureBox picHR = new PictureBox();
            picHR.Size = new Size(58, 52);
            picHR.Image = Properties.Resources.head_right;
            picHR.Location = new Point(width * 16 + 20 - 58, 0 + y_corr);
            this.Controls.Add(picHR);

            //Schleife zum erstellen wiederholender Teile
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
                        picFrB.Location = new Point(x * size + 26, height * size + 52 + y_corr);
                        this.Controls.Add(picFrB);
                    }
                    //Konfiguration der oberen Rahmenteile
                    if (x < (width - 6))
                    {
                        PictureBox picHM = new PictureBox();
                        picHM.Size = new Size(16, 52);
                        picHM.Image = Properties.Resources.head_mid;
                        picHM.Location = new Point(x * size + 10 + size * 3, 0 + y_corr);
                        this.Controls.Add(picHM);
                    }
                    //Konfiguration der seitlichen Rahmenteile
                    if (x == 0)
                    {
                        PictureBox picFrR = new PictureBox();
                        picFrR.Size = new Size(10, 16);
                        picFrR.Image = Properties.Resources.frame;
                        picFrR.Location = new Point(width * size + 20 - 10, y * size + 52 + y_corr);
                        this.Controls.Add(picFrR);

                        PictureBox picFrL = new PictureBox();
                        picFrL.Size = new Size(10, 16);
                        picFrL.Image = Properties.Resources.frame;
                        picFrL.Location = new Point(0, y * size + 52 + y_corr);
                        this.Controls.Add(picFrL);
                    }
                    //Erstellen und Konfigurieren der Buttons
                    btn[x, y] = new Button();
                    btn[x, y].Name = "Button" + x + y;
                    btn[x, y].Tag = x + "." + y; //Koordinaten als Tag abspeichern
                    btn[x, y].Size = new Size(size + 1, size + 1);
                    btn[x, y].MouseDown += new MouseEventHandler(this.button_Click); //MouseDown -> ermöglicht Auswertung von Rechts-Klick
                    btn[x, y].Location = new Point(x * size + 9, y * size + 51 + y_corr);
                    btn[x, y].Image = blankBitmap;
                    btn[x, y].TabStop = false;
                    btn[x, y].FlatStyle = FlatStyle.Flat;
                    btn[x, y].FlatAppearance.BorderSize = 0;
                    btn[x, y].FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
                    this.Controls.Add(btn[x, y]);
                }
            }
            //Konfiguration der unteren Eckteile
            PictureBox picBL = new PictureBox();
            picBL.Size = new Size(26, 26);
            picBL.Image = Properties.Resources.bottom_left;
            picBL.Location = new Point(0, 52 + (height - 1) * size + y_corr);
            this.Controls.Add(picBL);

            PictureBox picBR = new PictureBox();
            picBR.Size = new Size(26, 26);
            picBR.Image = Properties.Resources.bottom_right;
            picBR.Location = new Point((width - 1) * size + 10, 52 + (height - 1) * size + y_corr);
            this.Controls.Add(picBR);

            flaggenzahl = bombenzahl;

            flag_counter(flaggenzahl);
        }

        void picEmojy_Click(object sender, MouseEventArgs e)//Spiel neu Starten
        {
            //Prüfen, welcher Modus ausgewählt ist
            if (radioButton1.Checked == true) //Beginner Modus
            {
                saveRadioBtn = 1;
                width = 8;
                height = 8;
                bombenzahl = 10;
            }
            else if (radioButton2.Checked == true) // Intermediate Modus
            {
                saveRadioBtn = 2;
                width = 16;
                height = 16;
                bombenzahl = 40;
            }
            else if (radioButton3.Checked == true) // Expert Modus
            {
                saveRadioBtn = 3;
                width = 30;
                height = 16;
                bombenzahl = 99;
            }
            else if (radioButton4.Checked == true) // Costum Modus
            {
                saveRadioBtn = 4;
                width = Convert.ToInt32(WidthTextBox.Text);
                height = Convert.ToInt32(HeightTextBox.Text);
                bombenzahl = Convert.ToInt32(MinesTextBox.Text);
            }
            for (int ix = Controls.Count - 1; ix >= 0; --ix)//Controls-Einträge im Arbeitsspeicher löschen
            {
                var tmpObj = Controls[ix];
                Controls.RemoveAt(ix);
                tmpObj.Dispose();
            }
            timer4.Stop();
            gameover = false;
            gamewin = false;
            timer = 0;
            aufgedecktefelder = 0;
            this.Controls.Clear(); //Alle Form-Objekte im Fenster löschen
            StartGame(width, height, bombenzahl); //erneutes Aufrufen der StartGame-Methode
            switch (saveRadioBtn) //Prüfen, welcher Radiobutton aktiv war
            {
                case 1:
                    radioButton1.Checked = true;
                    break;
                case 2:
                    radioButton2.Checked = true;
                    break;
                case 3:
                    radioButton3.Checked = true;
                    break;
                case 4:
                    radioButton4.Checked = true;
                    break;
            }
            WidthTextBox.Text = Convert.ToString(width);
            HeightTextBox.Text = Convert.ToString(height);
            MinesTextBox.Text = Convert.ToString(bombenzahl);
        }

        void button_Click(object sender, MouseEventArgs e)
        {
            string curtentTag = (string)((Button)sender).Tag; //auslesen des Tags
            string[] coord = curtentTag.Split('.'); //zerteilen des Tags in die Koordinaten
            int x = Convert.ToInt32(coord[0]);
            int y = Convert.ToInt32(coord[1]);

            if (e.Button == MouseButtons.Right && gameover == false && gamewin == false) //Prüfen, ob Rechts-Klick 
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
                flag_counter(flaggenzahl);
                
            }
            if (e.Button == MouseButtons.Left && gameover == false && gamewin == false) //Prüfen, ob Links-Klick
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
                            picEmojy[0].Image = Properties.Resources.emojy_sad;
                            timer4.Stop();
                        }
                    }
                }
            }
            if (width * height - bombenzahl == aufgedecktefelder)
            {
                gamewin = true; // Für Blockade der Clicks
                picEmojy[0].Image = Properties.Resources.emojy_cool;
                timer4.Stop();
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
                            if (ix == bmb_x[i] && iy == bmb_y[i])
                            {
                                if (btn[ix, iy].Image != redMineBitmap && btn[x,y].Image != flagBitmap) // Abfrage ob Bombe die Aufgedeckt werden soll die erste Bombe war bzw. ob Bombe durch Fahne deaktiviert ist
                                {
                                    btn[ix, iy].Image = mineBitmap;
                                }
                            }
                            if (ix != bmb_x[i] && iy != bmb_y[i] && btn[ix, iy].Image == flagBitmap) // Abfrage falsch gesetzte Fahne
                            {
                                btn[ix, iy].Image = crossedMineBitmap;
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
            //Zähler für aufgedeckte Felder
            aufgedecktefelder++;
        }

        public void flag_counter(int flags)//Grafischer Zähler der übrigen Flaggen
        {
            int a = flags % 10;//Aufteilen des Zählwertes in Ziffern
            counter1[0].Image = sevenSegments(a);//Ausgabe des Werts über 7-Segment-Anzeige
            flags = flags / 10;
            int b = flags % 10;
            counter2[0].Image = sevenSegments(b);
            flags = flags / 10;
            int c = flags % 10;
            counter3[0].Image = sevenSegments(c);
        }

        public System.Drawing.Image sevenSegments(int counts)//Methode zum deffinieren der Grafikelemente der 7-Segment-Anzeigen
        {
            System.Drawing.Image x;
            switch (counts)
            {
                case 1: x = Properties.Resources._7S_1;
                    break;
                case 2: x = Properties.Resources._7S_2;
                    break;
                case 3: x = Properties.Resources._7S_3;
                    break;
                case 4: x = Properties.Resources._7S_4;
                    break;
                case 5: x = Properties.Resources._7S_5;
                    break;
                case 6: x = Properties.Resources._7S_6;
                    break;
                case 7: x = Properties.Resources._7S_7;
                    break;
                case 8: x = Properties.Resources._7S_8;
                    break;
                case 9: x = Properties.Resources._7S_9;
                    break;
                default: x = Properties.Resources._7S_0;
                    break;
            }
            return (x);
        }

        private void timer4_Tick(object sender, EventArgs e)//Grafische Stoppuhr
        {
            timer++;//Zählwert
            int x = timer;
            int a = x % 10;//Aufteilen des Zählwertes in Ziffern
            timer1[0].Image = sevenSegments(a);//Ausgabe des Werts über 7-Segment-Anzeige
            x = x / 10;
            int b = x % 10;
            timer2[0].Image = sevenSegments(b);
            x = x / 10;
            int c = x % 10;
            timer3[0].Image = sevenSegments(c);
        }

        private void GameButton_Click(object sender, EventArgs e) 
        {
            panel1.Visible = !panel1.Visible;
        }

        private void OK_Button_Click(object sender, EventArgs e)
        {
            width = Convert.ToInt32(WidthTextBox.Text); // eingabe überprüfen 
            if (width > 30) width = 30;
            if (width < 8) width = 8;
            WidthTextBox.Text = width.ToString();

            height = Convert.ToInt32(HeightTextBox.Text);
            if (height > 24) height = 24;
            if (height < 8) height = 8;
            HeightTextBox.Text = height.ToString();

            bombenzahl = Convert.ToInt32(MinesTextBox.Text);
            if (bombenzahl > 99) bombenzahl = 99;
            if (bombenzahl < 10) bombenzahl = 10;
            MinesTextBox.Text = bombenzahl.ToString();

            panel1.Visible = false;

            if (picEmojy[0] != null) //Button übrprüfen ob er da ist 
                picEmojy_Click(picEmojy[0], null);  
        }
       
        private void Form1_Load(object sender, EventArgs e)
        {

        }   
    }
}
