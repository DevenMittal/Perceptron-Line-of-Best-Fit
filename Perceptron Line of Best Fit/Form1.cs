using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Perceptron_Line_of_Best_Fit
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Graphics gfx;
        Bitmap canvas;
        Pen drawingPen;
        List<doublePoint> points;
        Perceptron perceptron;


        private void Form1_Load(object sender, EventArgs e)
        {

            canvas = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            drawingPen = new Pen(Brushes.Black, 2);

            gfx = Graphics.FromImage(canvas);

            points= new List<doublePoint>();  
            gfx.Clear(Color.White);

            // gfx.DrawEllipse(drawingPen, 100, 100, 3, 3);
            DrawAxis();

            pictureBox1.Image = canvas;
            
            
            double[] weights = { .5, .5 };
            Random rand = new Random();
            Func<double, double, double> errorFunc = (x, y) => Math.Pow(x - y, 2);

            perceptron = new Perceptron(weights, .5, .05, rand, errorFunc);
        }

        public void DrawPoints()
        {
            gfx.Clear(Color.White);
            for (int i = 0; i < points.Count; i++)
            {
                gfx.DrawEllipse(drawingPen, (float)points[i].x + pictureBox1.Width / 2,pictureBox1.Height/2 - (float)points[i].y, 3, 3);
            }
            DrawAxis();

            gfx.DrawLine(drawingPen, new Point(-pictureBox1.Width / 2 + pictureBox1.Width / 2, (pictureBox1.Height / 2 + (int)((-perceptron.weights[0] / perceptron.weights[1]) * -pictureBox1.Width / 2 - perceptron.bias / perceptron.weights[1]))), new Point(pictureBox1.Width / 2 + pictureBox1.Width / 2, (pictureBox1.Height / 2 + (int)((-perceptron.weights[0] / perceptron.weights[1]) * pictureBox1.Width / 2 - perceptron.bias / perceptron.weights[1]))));

            pictureBox1.Image = canvas;

        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != null && textBox2.Text != null)
            {
                int xPoint = int.Parse(textBox1.Text);
                int yPoint = int.Parse(textBox2.Text);
                points.Add(new doublePoint(xPoint, yPoint));

                double[][] inputs = new double[points.Count][];
                double[] desiredOutput = new double[points.Count];
                for (int i = 0; i < points.Count; i++)
                {
                    inputs[i] = new double[1];
                    inputs[i][0] = points[i].x;
                    //inputs[i][1] = points[i].y;
                    desiredOutput[i] = points[i].y; 
                }
                perceptron.bias = .5;
                perceptron.weights[0] = .5;
                perceptron.weights[1] = .5;
                for (int i = 0; i < 100000; i++)
                {
                    perceptron.TrainWithHillClimbingGate(inputs, desiredOutput);

                }
                DrawPoints();


            }
        }

        
        public void DrawAxis()
        {
            gfx.DrawLine(drawingPen, new Point(pictureBox1.Width / 2, 0), new Point(pictureBox1.Width / 2, pictureBox1.Height));
            gfx.DrawLine(drawingPen, new Point(0, pictureBox1.Height / 2), new Point(pictureBox1.Width, pictureBox1.Height / 2));
        }
    }
}
