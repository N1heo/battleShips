using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;
using Button = System.Windows.Forms.Button;

namespace battleShipsFinalProject
{
    public partial class Form1 : Form

    {
        public int[,] gameField = { {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}};
        public int[,] botsGameField = { {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}};

        

        public int cellSize = 30;
        public const int arrSize = 12;
        public string fieldLetters = "ABCDEFGHIJKLMN";
        public bool battleStarted = false;
        GroupBox chooseSizeBox = new GroupBox();
        RadioButton bigShip = new RadioButton();
        int bigShipCounter = 0;
        RadioButton mediumShip = new RadioButton();
        int mediumShipCounter = 0;
        RadioButton smallShip = new RadioButton();
        int smallShipCounter = 0;
        GroupBox chooseDirectionBox = new GroupBox();
        RadioButton horizontal = new RadioButton();
        RadioButton vertical = new RadioButton();

        NotifyIcon noShipsLeft = new NotifyIcon();


        public Button[,] playerButtons = new Button[arrSize, arrSize];
        public Button[,] botsButtons = new Button[arrSize, arrSize];

        public Bot bot;


        public Form1()
        {
            InitializeComponent();
            noShipsLeft.Visible = true;
            noShipsLeft.Icon = SystemIcons.WinLogo;
            //this.Width = arrSize * 2 * cellSize + 300;
            //this.Height = (arrSize + 3) * cellSize + 20;
            //initField();
        }

        private void startSingle(Object sender, EventArgs e)
        {
            this.Controls.Clear();
            Init();
        }

        private void Init()
        {
            battleStarted = false;
            InitField();
            bot = new Bot(botsGameField, gameField, botsButtons, playerButtons);
            botsGameField = bot.ConfigureShips();
        }

        private void InitField()
        {
            //this.Width = arrSize * cellSize + 50;
            //this.Height = (arrSize + 3) * cellSize + 20;
            chooseSizeBox.Location = new Point(arrSize * cellSize + 15, 50);
            chooseSizeBox.AutoSize = true;
            chooseSizeBox.Text = "Choose size of a ship";
            chooseSizeBox.Name = "groupBox";
            chooseSizeBox.Font = new Font("Berlin Sans FB", 11);

            this.Controls.Add(chooseSizeBox);

            bigShip.AutoSize = true;
            bigShip.Location = new Point(10, 30);
            bigShip.Text = "Big Ship: 2";
            chooseSizeBox.Controls.Add(bigShip);

            mediumShip.AutoSize = true;
            mediumShip.Location = new Point(10, 50);
            mediumShip.Text = "Medium Ship: 3";
            chooseSizeBox.Controls.Add(mediumShip);

            smallShip.AutoSize = true;
            smallShip.Location = new Point(10, 70);
            smallShip.Text = "Small Ship: 4";
            chooseSizeBox.Controls.Add(smallShip);

            chooseDirectionBox.Location = new Point(arrSize * cellSize + 15, 200);
            chooseDirectionBox.AutoSize = true;
            chooseDirectionBox.Text = "Choose direction of placement";
            chooseDirectionBox.Font = new Font("Berlin Sans FB", 11);
            this.Controls.Add(chooseDirectionBox);

            horizontal.AutoSize = true;
            horizontal.Location = new Point(10, 50);
            horizontal.Text = "Horizontal";
            chooseDirectionBox.Controls.Add(horizontal);

            vertical.AutoSize = true;
            vertical.Location = new Point(10, 80);
            vertical.Text = "Vertical";
            chooseDirectionBox.Controls.Add(vertical);

            for (int i = 0; i < arrSize; i++)
            {
                for (int j = 0; j < arrSize; j++)
                {
                    Button button = new Button();
                    button.Tag = new Tuple<int, int>(i, j);
                    button.Name = $"{i},{j}";
                    button.Location = new Point(j * cellSize, i * cellSize + 50);
                    button.Size = new Size(cellSize, cellSize);
                    button.FlatAppearance.BorderSize = 1;
                    button.FlatAppearance.BorderColor = Color.White;
                    button.FlatStyle = FlatStyle.Flat;
                    button.BackColor = Color.LightSkyBlue;
                    button.Font = new Font("Berlin Sans FB", 9);



                    if (i == 0 || j == 0)
                    {
                        button.BackColor = Color.LightGray;
                        if (i == 0 && j > 0)
                        {
                            button.Text = fieldLetters[j - 1].ToString();
                            button.BackColor = Color.LightGray;

                        }
                        else if (j == 0 && i > 0)
                        {
                            button.Text = i.ToString();
                            button.BackColor = Color.LightGray;
                        }
                    }
                    else
                    {
                        button.Click += new EventHandler(SetupShips);

                    }
                    playerButtons[i, j] = button; 
                    this.Controls.Add(button);
                }
            }
            for (int i = 0; i < arrSize; i++)
            {
                for (int j = 0; j < arrSize; j++)
                {
                    Button button = new Button();
                    button.Tag = new Tuple<int, int>(i, j);
                    button.Location = new Point(600 + j * cellSize, i * cellSize + 50);
                    button.Size = new Size(cellSize, cellSize);
                    button.FlatAppearance.BorderSize = 1;
                    button.FlatAppearance.BorderColor = Color.White;
                    button.FlatStyle = FlatStyle.Flat;
                    button.BackColor = Color.LightSkyBlue;
                    button.Font = new Font("Berlin Sans FB", 9);

                    if (i == 0 || j == 0)
                    {
                        button.BackColor = Color.LightGray;
                        if (i == 0 && j > 0)
                        {
                            button.Text = fieldLetters[j - 1].ToString();
                            button.BackColor = Color.LightGray;

                        }
                        else if (j == 0 && i > 0)
                        {
                            button.Text = i.ToString();
                            button.BackColor = Color.LightGray;
                        }
                    }
                    else
                    {
                        button.Click += new EventHandler(PlayerShoot);
                        

                    }
                    botsButtons[i,j] = button;
                    this.Controls.Add(button);
                }

                Label map1 = new Label();
                map1.Text = "Карта игрока";
                map1.Location = new Point(arrSize * cellSize / 2, arrSize * cellSize + 50);
                this.Controls.Add(map1);

                Label map2 = new Label();
                map2.Text = "Карта противника";
                map2.Location = new Point(350 + arrSize * cellSize / 2, arrSize * cellSize + 50);
                this.Controls.Add(map2);

                Button startButton = new Button();
                startButton.Text = "Начать";
                startButton.AutoSize = true;
                startButton.Click += new EventHandler(Start);
                startButton.Location = new Point(arrSize * cellSize + 20, 350);
                this.Controls.Add(startButton);


            }

        }
        private void Start(object sender, EventArgs e)
        {
            battleStarted = true;
        }
        private bool CheckNeighbors(int[,] grid, int row, int col, int targetNumber)
        {
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);

            // Check all neighbors (up, down, left, right, upRight, etc..)
            int[] dx = { -1, -1, +1, +1,  0, -1,  0, +1};
            int[] dy = { -1, +1, -1, +1, -1 , 0, +1,  0};

            for (int i = 0; i < dx.Length; i++)
            {
                int newRow = row + dx[i];
                int newCol = col + dy[i];

                // Check if the neighbor is equal to the target number or 0
                if (grid[newRow, newCol] != targetNumber && grid[newRow, newCol] != 0)
                {
                    return false;
                }
            }

            // All neighbors are equal to the target number or 0
            return true;
        }

        private void CreateShip(string name, ref int shipCounter, ref RadioButton radioBtn, int size, int amount, int rowPlus, int colPlus, ref Button pressedButton, ref int checker)
        {
            Tuple<int, int> coord = pressedButton.Tag as Tuple<int, int>;
            int row = coord.Item1;
            int col = coord.Item2;
            bool goOn = true;
            if (shipCounter < amount)
            {
                for (int i = 0; i < size; i++)
                {
                    if (arrSize - checker < size || gameField[row + (i * rowPlus), col + (i * colPlus)] != 0 || !CheckNeighbors(gameField, row + (i * rowPlus), col + (i * colPlus), 0))
                    {
                        goOn = false;
                    }
                }
                if (goOn) {

                    pressedButton.BackColor = Color.DarkBlue;
                    pressedButton.Text = $"{size}";
                    pressedButton.ForeColor = Color.White;
                    gameField[row, col] = 1;

                    for (int i = 1; i <= size - 1; i++)
                    {
                        Control[] controls = this.Controls.Find($"{row + (i * rowPlus)},{col + (i * colPlus)}", false);
                        Button adjButton = controls[0] as Button;
                        adjButton.Text = $"{size}";
                        adjButton.BackColor = Color.DarkBlue;
                        adjButton.ForeColor = Color.White;
                        gameField[row + (i * rowPlus), col + (i * colPlus)] = 1;
                    }
                    shipCounter++;
                    radioBtn.Text = $"{name}: {amount - shipCounter}";
                }
                else
                {
                    noShipsLeft.ShowBalloonTip(2000, "Error", $"{"Can't place there"} ({DateTime.Now:h:mm:ss tt})", ToolTipIcon.Warning);
                }
            }
            else
            {
                noShipsLeft.ShowBalloonTip(2000, "Error", $" No {"No Ships Of This Size Left"} ({DateTime.Now:h:mm:ss tt})", ToolTipIcon.Warning);
            }
            
        }
        public void PlayerShoot(object sender, EventArgs e)
        {

            Button pressedButton = sender as Button;
            bool playerTurn = Shoot(botsGameField, pressedButton);
            if (!playerTurn)
                bot.Shoot();

            if (!CheckIfMapIsNotEmpty())
            {
                this.Controls.Clear();
                Init();
            }
        }
        public bool CheckIfMapIsNotEmpty()
        {
            bool isEmpty1 = true;
            bool isEmpty2 = true;
            for (int i = 1; i < arrSize; i++)
            {
                for (int j = 1; j < arrSize; j++)
                {
                    if (gameField[i, j] != 0)
                        isEmpty1 = false;
                    if (botsGameField[i, j] != 0)
                        isEmpty2 = false;
                }
            }
            if (isEmpty1 || isEmpty2)
                return false;
            else return true;
        }

        public bool Shoot(int[,] map, Button pressedButton)
        {
            bool hit = false;
            if (battleStarted)
            {
                
                Tuple<int, int> coord = pressedButton.Tag as Tuple<int, int>;
                int row = coord.Item1;
                int col = coord.Item2;
                if (map[row, col] != 0)
                {
                    Console.WriteLine("color Blue");
                    hit = true;
                    map[row, col] = 0;
                    pressedButton.BackColor = Color.Blue;
                    pressedButton.Text = "X";
                }
                else
                {
                    Console.WriteLine("color black");
                    hit = false;

                    pressedButton.BackColor = Color.Black;
                }
            }
            return hit;
        }

        private void SetupShips(object sender, EventArgs e)
        {
            for (int p = 0; p < 13; p++)
            {
                for (int s = 0; s < 13; s++)
                {
                    Console.Write(gameField[p, s]);

                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Button pressedButton = sender as Button;
            if (!battleStarted)
            {
                Tuple<int, int> coord = pressedButton.Tag as Tuple<int, int>;
                int row = coord.Item1;
                int col = coord.Item2;
                

                if (gameField[row, col] == 0)
                {
                    
                    if (vertical.Checked)
                    {
                        if (bigShip.Checked)
                        {
                            CreateShip("Big Ship", ref bigShipCounter, ref bigShip, 4, 2, 1, 0, ref pressedButton, ref row);
                        }
                        else if (mediumShip.Checked)
                        {
                            CreateShip("Medium Ship", ref mediumShipCounter, ref mediumShip, 3, 3, 1, 0, ref pressedButton, ref row);
                        }
                        else if (smallShip.Checked)
                        {
                            CreateShip("Small Ship", ref smallShipCounter, ref smallShip, 2, 4, 1, 0, ref pressedButton, ref row);
                        }

                    } 
                    else if (horizontal.Checked)
                    {
                        if (bigShip.Checked)
                        {
                            CreateShip("Big Ship", ref bigShipCounter, ref bigShip, 4, 2, 0, 1, ref pressedButton, ref col);
                        }
                        else if (mediumShip.Checked)
                        {
                            CreateShip("Medium Ship", ref mediumShipCounter, ref mediumShip, 3, 3, 0, 1, ref pressedButton, ref col);
                        }
                        else if (smallShip.Checked)
                        {
                            CreateShip("Small Ship", ref smallShipCounter, ref smallShip, 2, 4, 0, 1, ref pressedButton, ref col);
                        }
                    }
                }
                else
                {
                    //gameField[row, col] = 0;
                    //pressedButton.BackColor = Color.LightSkyBlue;
                }
                
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void exitButton_MouseEnter(object sender, EventArgs e)
        {
            exitButton.BackColor = Color.LightSeaGreen;
        }

        private void exitButton_MouseLeave(object sender, EventArgs e)
        {
            exitButton.BackColor = Color.LightSlateGray;
        }

        private void startButton_MouseEnter(object sender, EventArgs e)
        {
            startButton.BackColor = Color.LightSeaGreen;
        }

        private void startButton_MouseLeave(object sender, EventArgs e)
        {
            startButton.BackColor = Color.LightSlateGray;
        }
    }
}
