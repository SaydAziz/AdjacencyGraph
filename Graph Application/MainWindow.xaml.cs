//Some code for calculating nodes was inspired by stackoverflow, geeksforgeeks, and reddit

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Graph_Application
{
    public partial class MainWindow : Window
    {
        private double GraphCenterX = 150;
        private double GraphCenterY = 150;
        private double GraphRadius = 100;

        private readonly Point[] nodePos;
        

        //This is wher you input the matrix as I did not have time to make a full wpf table.
        public int[,] AdjacencyMatrix =
        {
            {0, 0, 0, 0, 1, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 1, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0},
            {1, 0, 1, 0, 0, 0, 1, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 1, 0},
            {0, 0, 0, 0, 1, 0, 0, 0, 0},
        };


        public MainWindow()
        {
            InitializeComponent();    

            //array of all point positions
            nodePos = new Point[AdjacencyMatrix.GetLength(0)];

            CalculateNode();
            DrawGraph();
            DrawEdges();
        }
        private void CalculateNode()
        {
            for (int i = 0; i < AdjacencyMatrix.GetLength(0); i++)
            {
                //takes a circle (full circle in radians) and splits it into slices (amount of slices depends on matrix) 
                double angle = (2 * Math.PI) * i / AdjacencyMatrix.GetLength(0);

                //finds coordinates based on the angle
                double x = GraphCenterX + GraphRadius * Math.Cos(angle);
                double y = GraphCenterY + GraphRadius * Math.Sin(angle);
                nodePos[i] = new Point(x, y);
            }
        }
        private void DrawGraph()
        {
            for (int i = 0; i < AdjacencyMatrix.GetLength(0); i++)
            {
                DrawNode(nodePos[i]);
            }
        }
        private void DrawNode(Point pos)
        {
            //creates a circle
            Ellipse node = new Ellipse
            {
                Width = 10,
                Height = 10,
                Fill = Brushes.Black,
                Stroke = Brushes.Black,
            };

            //sets circle position based on node coordinates
            Canvas.SetLeft(node, pos.X);
            Canvas.SetTop(node, pos.Y);
            canvas.Children.Add(node);
        }

        private void DrawEdges()
        {
            for (int i = 0; i < AdjacencyMatrix.GetLength(0); i++)
            {
                for (int j = i + 1; j < AdjacencyMatrix.GetLength(1); j++)
                {

                    //itterates through matrix and if the matrix slot has a 1 in it draw an edge for the nodes in the slot
                    if (AdjacencyMatrix[i, j] == 1)
                    {
                        DrawEdge(nodePos[i], nodePos[j]);
                    }
                }
            }
        }

        private void DrawEdge(Point p1, Point p2)
        {
            Line edge = new Line
            {
                X1 = p1.X + 5,
                Y1 = p1.Y + 5,
                X2 = p2.X + 5,
                Y2 = p2.Y + 5,
                Stroke = Brushes.Blue,
            };
            canvas.Children.Add(edge);
        }
    }
}


