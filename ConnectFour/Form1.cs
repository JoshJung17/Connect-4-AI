using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace ConnectFour
{
    public partial class Form1 : Form
    {
        public static int SelectionTime, ExpansionTime, SimulationTime, PropagationTime;
        SimpleBoard _currentBoard;
        int FPS = 2;
        bool blink;
        bool gameRunning = false;
        Searcher _searcher;
        int AITimer = 1 << 30;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            btnPlay.Hide();
            Start();
        }

        private void Start()
        {
            CurrentBoard = new SimpleBoard();
            gameRunning = true;
            Refresh();

            GameSearcher = new Searcher();

            int ptime = Environment.TickCount;
            while (gameRunning)
            {
                if (Environment.TickCount - ptime > 1000 / FPS)
                {
                    ptime = Environment.TickCount;
                    blink = !blink;
                    Refresh();
                    this.Text = "Win Probability: " + (GameSearcher.RootBoard.WinCount * 100 / GameSearcher.RootBoard.SimulationCount)
                         + " Search Size: " + GameSearcher.RootBoard.SimulationCount.ToString() + " Number of Nodes: " + GameSearcher.TreeSize.ToString() + " Depth: " + GameSearcher.TreeDepth;
                }
                Application.DoEvents();
                if (GameSearcher.TreeSize<13000000 || GameSearcher.EmptyBoards.Count>0)
                    GameSearcher.SearchOnce();
                if (GameSearcher.RootBoard.SimulationCount > 200000 && GameSearcher.RootBoard.SimulationCount - 100000 * C.THREADS > AITimer || GameSearcher.TreeSize == 13000000 && GameSearcher.EmptyBoards.Count == 0)
                {
                    int c = GameSearcher.GetMove();
                    CurrentBoard.Update(c);
                    GameSearcher.Update(c);
                    Refresh();
                    AITimer = 1 << 30;
                }

                if (CurrentBoard.FinalState && !CurrentBoard.Turn)
                {
                    MessageBox.Show("You Win!");
                    gameRunning = false;
                    btnPlay.Show();
                }
                else if (CurrentBoard.FinalState)
                {
                    MessageBox.Show("You Lose!");
                    gameRunning = false;
                    btnPlay.Show();
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (!gameRunning) return;
            if (blink)
            {
                for (int c = 0; c < C.COLUMNS; ++c)
                {
                    if (CurrentBoard.Heights[c] < C.ROWS)
                    {
                        if (CurrentBoard.Turn)
                            e.Graphics.FillRectangle(Brushes.Red, 21 + 50 * c, 40, 48, 20);
                        else
                            e.Graphics.FillRectangle(Brushes.Yellow, 21 + 50 * c, 40, 48, 20);
                        e.Graphics.DrawRectangle(Pens.Gray, 21 + 50 * c, 40, 48, 20);
                    }
                }
            }

            for (int c = 0; c <= C.COLUMNS; ++c)
                e.Graphics.DrawLine(Pens.Black, 20 + 50 * c, 70, 20 + 50 * c, 370);
            for (int r = 0; r <= C.ROWS; ++r)
                e.Graphics.DrawLine(Pens.Black, 20, 370 - 50 * r, 20+50*C.COLUMNS, 370 - 50 * r);
            if (chkShowMoves.Checked && GameSearcher != null && GameSearcher.RootBoard != null)
            {
                for (int c = 0; c < C.COLUMNS; ++c)
                {
                    if (GameSearcher.RootBoard.Children[c] != null)
                    {
                        int y = 370 - (int)(300L * GameSearcher.RootBoard.Children[c].SimulationCount / GameSearcher.RootBoard.SimulationCount);
                        e.Graphics.FillRectangle(Brushes.Blue, 21 + 50 * c, y, 49, 370 - y);
                    }
                }
            }

            for (int c = 0; c < C.COLUMNS; ++c)
            {
                for (int r = 0; r < C.ROWS; ++r)
                {
                    if (r >= CurrentBoard.Heights[c]) break;
                    if ((CurrentBoard.Discs[c] & (1 << r)) > 0)
                        e.Graphics.FillEllipse(Brushes.Red, 20 + 50 * c, 320 - 50 * r, 50, 50);
                    else
                        e.Graphics.FillEllipse(Brushes.Yellow, 20 + 50 * c, 320 - 50 * r, 50, 50);
                }
            }
        }

        private SimpleBoard CurrentBoard
        {
            get
            {
                return _currentBoard;
            }
            set
            {
                _currentBoard = value;
            }
        }

        private Searcher GameSearcher
        {
            get
            {
                return _searcher;
            }
            set
            {
                _searcher = value;
            }
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Y > 40 && e.Y < 60 && e.X > 20 && e.X < 20+C.COLUMNS*50)
            {
                CurrentBoard.Update((e.X - 20) / 50);
                GameSearcher.Update((e.X - 20) / 50);
                Refresh();

                if (chkAuto.Checked)
                {
                    AITimer = GameSearcher.RootBoard.SimulationCount;
                }
            }
        }

        private void btnAIMove_Click(object sender, EventArgs e)
        {
            int c = GameSearcher.GetMove();
            CurrentBoard.Update(c);
            GameSearcher.Update(c);
            Refresh();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            gameRunning = false;
        }
    }
}
