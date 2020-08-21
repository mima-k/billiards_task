using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;

namespace billiards_sim
{
    public partial class Form1 : Form
    {
        Vector ballPos;
        Vector ballSpeed;
        int ballRadius;
        Rectangle rect1;
        int count=0;
        int w = 5;
        int h = 3;
        double tan1 = Math.Tan(Math.PI*45/180.0);
        double tan2 = 7/2;
        Timer timer = new Timer();
        

        public Form1()
        {
            InitializeComponent();
            //this.FormBorderStyle = FormBorderStyle.FixedSingle;

            this.ballPos = new Vector(20, 70);                  //初期位置
            this.ballSpeed = new Vector(ballSpeed.Y*3/2,5);
            this.ballRadius = 10;                               //半径
            this.rect1 = new Rectangle(10, 60, w*100, h*100);

            
            timer.Interval = 30;
            timer.Tick += new EventHandler(Update);

            
            
        }

        private void Update(object sender, EventArgs e)
        {
            
            // ボールの移動
            ballPos += ballSpeed;

            // 左右の壁でのバウンド
            if (ballPos.X + ballRadius  > 10+w * 100 || ballPos.X - ballRadius < 10)
            {
                ballSpeed.X *= -1;
                count++;
                label1.Text = string.Format("衝突回数: {0:d}", count);
            }


            // 上下の壁でバウンド
            if (ballPos.Y - ballRadius < 60 || ballPos.Y + ballRadius*2  > 60+ this.ballRadius+h * 100)
            {
                ballSpeed.Y *= -1;
                count++;
                label1.Text = string.Format("衝突回数: {0:d}", count);
            }

            //左上穴に落ちる
            if (ballPos.X < 10 + this.ballRadius * 2 - 5 && ballPos.Y < 60 + this.ballRadius * 2 - 5 && count!=0)
            {
                timer.Stop();
            }

            //右上穴に落ちる
            if (ballPos.X> 10+w * 100 - this.ballRadius * 2 -5 && ballPos.Y< 60+this.ballRadius * 2 -5)
            {
                timer.Stop();
            }

            //左下穴に落ちる
            if (ballPos.X < 10 + this.ballRadius * 2 - 5 && ballPos.Y > 60 + h * 100 - this.ballRadius * 2 - 5)
            {
                timer.Stop();
            }

            //右下穴に落ちる
            if (ballPos.X > 10 + w * 100 - this.ballRadius * 2 - 5 && ballPos.Y > 60 + h * 100 - this.ballRadius * 2 - 5)
            {
                timer.Stop();
            }



            // 再描画
            Invalidate();

        }

        private void Draw(object sender, PaintEventArgs e)
        {
            SolidBrush pinkBrush = new SolidBrush(Color.HotPink);
            SolidBrush blueBrush = new SolidBrush(Color.LightBlue);
            SolidBrush blackBrush = new SolidBrush(Color.Black);

            float px = (float)this.ballPos.X - ballRadius;
            float py = (float)this.ballPos.Y - ballRadius;
           

            e.Graphics.FillRectangle(blueBrush, rect1); //ビリヤード台
            e.Graphics.FillEllipse(pinkBrush, px, py, this.ballRadius * 2, this.ballRadius * 2);    //手玉
            e.Graphics.FillEllipse(blackBrush, 10, 60, this.ballRadius * 2, this.ballRadius * 2);   //左上穴
            e.Graphics.FillEllipse(blackBrush, 10, 60+h*100-this.ballRadius*2, this.ballRadius * 2, this.ballRadius * 2);   //左下穴
            e.Graphics.FillEllipse(blackBrush, 10 + w*100-this.ballRadius*2, 60, this.ballRadius * 2, this.ballRadius * 2);   //右上穴
            e.Graphics.FillEllipse(blackBrush, 10 + w * 100 - this.ballRadius * 2, 60 + h * 100 - this.ballRadius * 2, this.ballRadius * 2, this.ballRadius * 2);   //右下穴





        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.ballPos = new Vector(20, 70);
            this.ballSpeed = new Vector(ballSpeed.Y * 3/2, 5);
            count = 0;
            label1.Text = string.Format("衝突回数: {0:d}", count);
            timer.Start();
        }


        private void label1_Click(object sender, EventArgs e)
        {
        }
    }
    
}