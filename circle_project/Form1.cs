using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace circle_project
{
    public partial class Form1 : Form
    {
        Pen pen = new Pen(Brushes.Black);
        SolidBrush redBrush = new SolidBrush(Color.Red);

        public int x = 300 , y = 300, width = 300, height = 300, deltaX, deltaY;
        bool isActive = false, dragging = false, resize = false;
        //active state handlers
        Rectangle topRect, leftRect, bottomRect, rightRect, mainRect;
        int flagNum = 0; 
       

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if(isActive && intersect(e))
            {
                var rec1 = new System.Drawing.Rectangle(e.X, e.Y, 10, 10);

                if (rightRect.IntersectsWith(rec1))
                {
                    this.resize = true;
                    this.dragging = false;
                    flagNum = 4;

                }
                else if (topRect.IntersectsWith(rec1))
                {
                    this.resize = true;
                    this.dragging = false;
                    flagNum = 1;
                }
                else if (rec1.IntersectsWith(leftRect))
                {
                    this.resize = true;
                    this.dragging = false;
                    flagNum = 2;
                }
                else if (rec1.IntersectsWith(bottomRect))
                {
                    this.resize = true;
                    this.dragging = false;
                    flagNum = 3;
                }
                else
                {
                    this.dragging = true;
                    
                }
                deltaX = e.Location.X - this.x;
                deltaY = e.Location.Y - this.y;
            }
        }
  
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.dragging)
            {
                this.x = e.Location.X - deltaX;
                this.y = e.Location.Y - deltaY;
                this.Invalidate();
            }
            if (this.resize)
            {


                switch (flagNum)
                {
                    case 1: height = e.Location.Y - deltaY;
                        break;
                    case 2: width = -(e.Location.X + deltaX);
                        break;
                    case 3:
                        height = e.Location.Y + deltaY;
                        break;
                    case 4: width = e.Location.X - deltaX;//right resize DONE
                        break;
                }

                Invalidate();
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            this.dragging = false;
            this.resize = false;
        }

        public Form1()
        {
            InitializeComponent();
        }
        
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            pen.Width = 6.0f;//
            mainRect = new Rectangle(x, y, width, height);
            g.DrawEllipse(pen, mainRect);

            if (isActive)
            {
                //mid circle
                g.FillEllipse(redBrush, x + width / 2 - 10, y + height / 2 - 10, 20, 20);
                //circles on the edges
                //set the rectangles 
                topRect = new Rectangle(x + width / 2 - 10, y - 10, 20, 20);
                leftRect = new Rectangle(x - 10, y + height / 2 - 10, 20, 20);
                bottomRect = new Rectangle(x + width / 2 - 10, y + height - 10, 20, 20);
                rightRect = new Rectangle(x + width - 10, y + height / 2 - 10, 20, 20);

                g.FillEllipse(redBrush, topRect);//upper edge 
                g.FillEllipse(redBrush, leftRect);//left edge 
                g.FillEllipse(redBrush, bottomRect);//bottom edge                                             
                g.FillEllipse(redBrush, rightRect);//right edge     
            }
        }
       public bool intersect(MouseEventArgs mouse)
        {
            var rec1 = new System.Drawing.Rectangle(this.x, this.y, this.width, this.height);
            var rec2 = new System.Drawing.Rectangle(mouse.X, mouse.Y, 10, 10);
            return rec1.IntersectsWith(rec2);
        }
       
        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (intersect(e))
            {
                this.isActive = true;
                this.Invalidate();
            }
            else
            {
                this.isActive = false;
                this.Invalidate();
            }
          
        }
       
    }
}
