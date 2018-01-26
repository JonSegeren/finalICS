using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameTemplate.Dialogs;
using System.Media;

namespace GameTemplate.Screens
{
    public partial class GameScreen : UserControl
    {
        public GameScreen()
        {
            InitializeComponent();
        }

        #region required global values - DO NOT CHANGE

        //player1 button control keys - DO NOT CHANGE
        Boolean upArrowDown, downArrowDown,  spaceDown;

        //player2 button control keys - DO NOT CHANGE
        Boolean  sDown,  wDown, zDown;

        #endregion

        //TODO - Place game global variables here 
        //---------------------------------------
        int player1Lives = 3;
        int player1Score, player2Score;
        int player2Lives = 3;
        int playerSpeed = 5;
        int topLeftLives = 10;
        int bottomLeftLives = 10;
        int topRightLives = 10;
        int bottomRightLives = 10;
        const int BULLET_SPEED = 10;
        Boolean gameOver=false;
        Boolean monstUp = false;
        

        //initial starting points for black rectangle
        int drawP2X = 880;
        int drawP2Y = 290;
        int p1BulletX, p1BulletY,CPUBulletX; 
        int p2BulletX,p2BulletY, CPUBulletY; 
        int drawX = 100;
        int drawY =290;
     
        int monstX1 = 400;
        int monstX2 = 400;
        int monstX3 = 600;
        int monstX4 = 600;
        int monstX5 = 500;
        int monstX6 = 500;
        int monstX7 = 500;

        int monstY1 = 200;
        int monstY2= 400;
        int monstY3 = 200;
        int monstY4 = 400;
        int monstY5 = 500;
        int monstY6 = 300;
        int monstY7 = 100;

        //Graphics objects
        SolidBrush player2Brush = new SolidBrush(Color.Blue);
        SolidBrush player1Brush = new SolidBrush(Color.Red);
        SolidBrush bulletBrush = new SolidBrush(Color.MediumPurple);
        SolidBrush wallBrush = new SolidBrush(Color.Black);
        SolidBrush scoreBrush = new SolidBrush(Color.Black);
        SolidBrush monsterBrush = new SolidBrush(Color.LimeGreen);
        Font scoreFont = new Font("Courier New", 12);
       // SoundPlayer player = new SoundPlayer(Screens.GameScreen.Resources.Pew_Pew.wav);  
        //----------------------------------------
        
        
        private void GameScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                pauseGame();
            }
            // p 1
            if (e.KeyCode == Keys.W)
            {
                wDown = true;
                sDown = false;
            }
            if (e.KeyCode == Keys.S)
            {
                sDown = true;
                wDown = false;
            }
            if (e.KeyCode == Keys.Z && zDown == false)
            {
                zDown = true;
            }
            //p 2
            if (e.KeyCode == Keys.Up)
            {
                upArrowDown = true;
                downArrowDown = false;
            }
            if (e.KeyCode == Keys.Down)
            {
                downArrowDown = true;
                upArrowDown = false;
            }
            if (e.KeyCode == Keys.Space && spaceDown == false)
            {
                spaceDown = true;
            }
        } 
  
        /// <summary>
        /// All game update logic must be placed in this event method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gameTimer_Tick(object sender, EventArgs e)
        {
             


            //player.Play();
            Graphics fg = this.CreateGraphics();
            #region character movements

            // p2
            if (downArrowDown == true)
            {
                drawP2Y = drawP2Y + playerSpeed;
            }
            if (upArrowDown == true)
            {
                drawP2Y = drawP2Y - playerSpeed;
            }
            if  (spaceDown == true)
            {
                p2BulletX=drawP2X;
                p2BulletY=drawP2Y+6;
                spaceDown = false;
                //player.Play();
            }
            p2BulletX = p2BulletX - BULLET_SPEED;

            //player 1
            if (wDown == true)
            {
                drawY = drawY - playerSpeed;
            }
            if (sDown == true)
            {
                drawY = drawY + playerSpeed;
            }
            if  (zDown == true)
            {
                p1BulletX=drawX;
                p1BulletY=drawY+6;
                zDown = false;
                //player.Play();
            }
            p1BulletX=p1BulletX+BULLET_SPEED;
               


            #endregion

            #region monster movements - TO BE COMPLETED
           
            if(monstUp==false)
            {
                monstMove(1);
                
                if (monstY1 == 280)
                {
                    monstUp = true;
                }
            }
            else
            {
                monstMove(-1);
                if (monstY1 == 100)
                {
                    monstUp = false;
                }
            }

            #endregion

            #region collision detection - TO BE COMPLETED
            
           
            Rectangle p1Rec = new Rectangle(drawX, drawY, 20, 20);
            Rectangle p2Rec = new Rectangle(drawP2X, drawP2Y, 20, 20);
            Rectangle leftWall = new Rectangle(0, 0, 0, Height);
            Rectangle topWall = new Rectangle(0, 0, Width, 0);
            Rectangle rightWall = new Rectangle(Width, 0, 0, Height);
            Rectangle bottomWall = new Rectangle(0, Height, Width, 0);
            Rectangle p1BulletRec = new Rectangle(p1BulletX, p1BulletY, 8, 8);
            Rectangle p2BulletRec = new Rectangle(p2BulletX, p2BulletY, 8, 8);
            Rectangle CPUBulletRec = new Rectangle(CPUBulletX, CPUBulletY, 8, 8);
            Rectangle topLeftBarrier = new Rectangle(200, 100, 10, 100);
            Rectangle bottomLeftBarrier = new Rectangle(200, 400, 10, 100);
            Rectangle topRightBarrier = new Rectangle(Width-200, 100, 10, 100);
            Rectangle bottomRightBarrier = new Rectangle(Width-200, 400, 10, 100);
            Rectangle monst1 = new Rectangle(monstX1, monstY1, 20, 20);
            Rectangle monst2 = new Rectangle(monstX2, monstY2, 20, 20);
            Rectangle monst3 = new Rectangle(monstX3, monstY3, 20, 20);
            Rectangle monst4 = new Rectangle(monstX4, monstY4, 20, 20);
            Rectangle monst5 = new Rectangle(monstX5, monstY5, 20, 20);
            Rectangle monst6 = new Rectangle(monstX6, monstY6, 20, 20);
            Rectangle monst7 = new Rectangle(monstX7, monstY7, 20, 20);

            #region Walls 
            if (p1Rec.IntersectsWith(topWall))
            {
                drawY=0;               
            }
            else if (p1Rec.IntersectsWith(bottomWall))
            {
                drawY = 580;
            }
            //p2 walls
            if (p2Rec.IntersectsWith(topWall))
            {
                drawP2Y=0;               
            }
            else if (p2Rec.IntersectsWith(bottomWall))
            {
                drawP2Y = 580;
            }
            #endregion
           
            if (p1Rec.IntersectsWith(p2BulletRec))
            {
                player1Lives--;
                bulletReturnP2();
            }
            if (p2Rec.IntersectsWith(p1BulletRec))
            {
                player2Lives--;
                bulletReturnP1();
            }

            #region Barriers
            if (p1BulletRec.IntersectsWith(topLeftBarrier) && topLeftLives > 0)
            {
                    topLeftLives = topLeftLives - 5;
                    bulletReturnP1();   
            }
            if (p2BulletRec.IntersectsWith(topLeftBarrier) && topLeftLives > 0)
            {
                    topLeftLives = topLeftLives - 5;
                    bulletReturnP2();
            }
            if (p1BulletRec.IntersectsWith(bottomLeftBarrier) && bottomLeftLives > 0) 
            {
                    bottomLeftLives = bottomLeftLives - 5;
                    bulletReturnP1();          
            }
            if (p2BulletRec.IntersectsWith(bottomLeftBarrier) && bottomLeftLives > 0)
            {
                    bottomLeftLives = bottomLeftLives - 5;
                    bulletReturnP2();         
            }
            if (p1BulletRec.IntersectsWith(topRightBarrier) && topRightLives > 0) 
            {
                    topRightLives = topRightLives - 5;                
                    bulletReturnP1();                              
            }
            if (p2BulletRec.IntersectsWith(topRightBarrier) && topRightLives > 0)
            {
                    topRightLives = topRightLives - 5;
                    bulletReturnP2();          
            }
           
            if (p1BulletRec.IntersectsWith(bottomRightBarrier) && bottomRightLives > 0)
            {
                    bottomRightLives = bottomRightLives - 5;
                    bulletReturnP1();         
            }
            if (p2BulletRec.IntersectsWith(bottomRightBarrier) && bottomRightLives > 0)
            {
                    bottomRightLives = bottomRightLives - 5;
                    bulletReturnP2();         
            }
            #endregion
            
            // required as bullets that go ofscreen could hit offscreen monsters, extra points for p2
            if(p2BulletRec.IntersectsWith(leftWall))
            {
                bulletReturnP2();
            }

            #region monsters
            if(p1BulletRec.IntersectsWith(monst1))
            {  
                p1MonstHit();
                monstX1 = -21;
            }
            if(p2BulletRec.IntersectsWith(monst1))
            {
                p2MonstHit();
                monstX1 = -21;
            }
            if(p1BulletRec.IntersectsWith(monst2))
            {  
                p1MonstHit();
                monstX2 = -21;
            }
            if(p2BulletRec.IntersectsWith(monst2))
            {
                p2MonstHit();
                monstX2 = -21;
            }
            if(p1BulletRec.IntersectsWith(monst3))
            {  
                p1MonstHit();
                monstX3 = -21;
            }
            if(p2BulletRec.IntersectsWith(monst3))
            {
                p2MonstHit();
                monstX3 = -21;
            }
            if(p1BulletRec.IntersectsWith(monst4))
            {  
                p1MonstHit();
                monstX4 = -21;
            }
            if(p2BulletRec.IntersectsWith(monst4))
            {
                p2MonstHit();
                monstX4 = -21;
            }
             if(p1BulletRec.IntersectsWith(monst5))
            {  
                p1MonstHit();
                monstX5 = -21;
            }
            if(p2BulletRec.IntersectsWith(monst5))
            {
                p2MonstHit();
                monstX5 = -21;
            }
            if(p1BulletRec.IntersectsWith(monst6))
            {  
                p1MonstHit();
                monstX6 = -21;
            }
            if(p2BulletRec.IntersectsWith(monst6))
            {
                p2MonstHit();
                monstX6 = -21;
            }
            if(p1BulletRec.IntersectsWith(monst7))
            {  
                p1MonstHit();
                monstX7 = -21;
            }
            if(p2BulletRec.IntersectsWith(monst7))
            {
                p2MonstHit();
                monstX7 = -21;
            }
            #endregion

            #endregion


            //refresh the screen, which causes the GameScreen_Paint method to run
            Refresh();
        }

        /// <summary>
        /// Open the pause dialog box and gets Cancel or Abort result from it
        /// </summary>
        private void pauseGame()
        {
            gameTimer.Enabled = false;
            
            wDown = false;
            sDown = false;
            upArrowDown = false;
            downArrowDown = false;
            zDown = false;
            spaceDown = false;
            DialogResult result = PauseDialog.Show();

            if (result == DialogResult.Cancel)
            {
                gameTimer.Enabled = true;
            }
            if (result == DialogResult.Abort)
            {
                ScreenControl.changeScreen(this, "MenuScreen");
            }
        }
        /// <summary>
        /// All drawing, (and only drawing), to be done here
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            //bullets
            e.Graphics.FillRectangle(bulletBrush, p1BulletX, p1BulletY,8,8);
            e.Graphics.FillRectangle(bulletBrush, p2BulletX,p2BulletY, 8 ,8);
            //players
            e.Graphics.FillRectangle(player1Brush, drawX, drawY, 20, 20);
            e.Graphics.FillRectangle(player2Brush, drawP2X, drawP2Y, 20, 20);
            //walls
            e.Graphics.FillRectangle(wallBrush, 200, 100, topLeftLives, 100);
            e.Graphics.FillRectangle(wallBrush, 200, 400, bottomLeftLives, 100);
            e.Graphics.FillRectangle(wallBrush, Width-200, 100, topRightLives, 100);
            e.Graphics.FillRectangle(wallBrush,  Width-200, 400, bottomRightLives, 100);
            //monsters
            e.Graphics.FillRectangle(monsterBrush, monstX1, monstY1, 20, 20);
            e.Graphics.FillRectangle(monsterBrush, monstX2, monstY2, 20, 20);
            e.Graphics.FillRectangle(monsterBrush, monstX3, monstY3, 20, 20);
            e.Graphics.FillRectangle(monsterBrush, monstX4, monstY4, 20, 20);
            e.Graphics.FillRectangle(monsterBrush, monstX5, monstY5, 20, 20);
            e.Graphics.FillRectangle(monsterBrush, monstX6, monstY6, 20, 20);
            e.Graphics.FillRectangle(monsterBrush, monstX7, monstY7, 20, 20);
            //debug
           // e.Graphics.DrawString(player1Score+" ",scoreFont,scoreBrush,300,300);
            
            //lives
            e.Graphics.DrawString("Player 1 ", scoreFont, scoreBrush, 5, 10);
            e.Graphics.DrawString("Score " + player1Score, scoreFont, scoreBrush, 5, 30);
            if (player1Lives == 3)
            {
                e.Graphics.FillRectangle(player1Brush,70,15,10,10);
                e.Graphics.FillRectangle(player1Brush,85,15,10,10);
                e.Graphics.FillRectangle(player1Brush,100,15,10,10);
            }
            else if (player1Lives == 2)
            {
                e.Graphics.FillRectangle(player1Brush,70,15,10,10);
                e.Graphics.FillRectangle(player1Brush,85,15,10,10);
            }
            else if (player1Lives == 1)
            {
                e.Graphics.FillRectangle(player1Brush,70,15,10,10);
            }
            else if (player1Lives == 0)
            {
                gameOver = true;
            }
            e.Graphics.DrawString("Player 2 ", scoreFont, scoreBrush, Width - 120, Height - 25);
            e.Graphics.DrawString("Score " + player2Score, scoreFont, scoreBrush, Width - 119, Height - 45);
            if (player2Lives == 3)
            {
                e.Graphics.FillRectangle(player2Brush, Width-135, Height-20, 10, 10);
                e.Graphics.FillRectangle(player2Brush, Width-150, Height-20, 10, 10);
                e.Graphics.FillRectangle(player2Brush, Width-165, Height-20, 10, 10);
            }
            else if (player2Lives == 2)
            {
                e.Graphics.FillRectangle(player2Brush, Width-135, Height-20, 10, 10);
                e.Graphics.FillRectangle(player2Brush, Width-150, Height-20, 10, 10);
            }
            else if (player2Lives == 1)
            {
                e.Graphics.FillRectangle(player2Brush, Width-135, Height-20, 10, 10);
            }
            else if (player2Lives == 0)
            {
                gameOver = true;
            }
                                     
            //game over
            if (gameOver==true)
            {
                //graphics stuff
                gameTimer.Enabled=false;
            }
        }
        
        public void monstMove(int upDown)
        {
            monstY1 = monstY1 + upDown;
            monstY2 = monstY2 + upDown;
            monstY3 = monstY3 + upDown;
            monstY4 = monstY4 + upDown;
            monstY5 = monstY5 + upDown;
            monstY6 = monstY6 + upDown;
            monstY7 = monstY7 + upDown;
        }
        
        //these methods are used so that the bullets do not continue through objects
        public void bulletReturnP1()
        {
            p1BulletX = -8;
            p1BulletY = -8;
        }
        public void bulletReturnP2()
        {
            p2BulletX = 1000;
            p2BulletY = -8;
        }
        public void p1MonstHit()
        {
            player1Score++;
            bulletReturnP1();
        }
        public void p2MonstHit()
        {
            player2Score++;
            bulletReturnP2();
        }
    }
}
