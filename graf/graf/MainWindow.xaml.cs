using QuickGraph;
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

namespace graf
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        private IBidirectionalGraph<object, IEdge<object>> _graphToVisualize;


        public IBidirectionalGraph<object, IEdge<object>> GraphToVisualize
        {
            get { return _graphToVisualize; }
        }

        public MainWindow()
        {
            CreateGraphToVisualize();
            InitializeComponent();
        }

        private void CreateGraphToVisualize()
        {
            const int baseFlourLenght = 7;

            var g = new BidirectionalGraph<object, IEdge<object>>();
            List<string> firstFlour = GetNodeVertices(g, baseFlourLenght, "A1");
            List<string> secondFlour = GetNodeVertices(g, baseFlourLenght, "B2");
            List<string> thirdFlour = GetNodeVertices(g, baseFlourLenght, "C3");
            List<string> fourthFlour = GetNodeVertices(g, baseFlourLenght, "D4");

            List<string> firstFlourAnotherPart = GetNodeVertices(g, 5, "G1");
            List<string> secondFlourAnotherPart = GetNodeVertices(g, baseFlourLenght, "E2");

            AddLineEdges(g, firstFlour);
            AddLineEdges(g, secondFlour);
            AddLineEdges(g, thirdFlour);
            AddLineEdges(g, fourthFlour);

            AddLineEdges(g, firstFlourAnotherPart);
            AddLineEdges(g, secondFlourAnotherPart);

            AddVeticesEdges(g, firstFlour, secondFlour, 5);
            AddVeticesEdges(g, secondFlour,  thirdFlour, 5);
            AddVeticesEdges(g, thirdFlour, fourthFlour, 5);

            AddVeticesEdges(g, firstFlour, firstFlourAnotherPart, 4,0);
            AddVeticesEdges(g, firstFlourAnotherPart, secondFlourAnotherPart, 3, 1);


            _graphToVisualize = g;


        }

        private static void AddVeticesEdges(BidirectionalGraph<object, IEdge<object>> g, List<string> vertices, List<string> vertices2, int VerticesNumber)
        {
            g.AddEdge(new Edge<object>(vertices[VerticesNumber], vertices2[VerticesNumber]));
            g.AddEdge(new Edge<object>(vertices2[VerticesNumber], vertices[VerticesNumber]));
        }

        private static void AddVeticesEdges(BidirectionalGraph<object, IEdge<object>> g, List<string> vertices, List<string> vertices2, int VerticesNumber1, int VerticesNumber2)
        {
            g.AddEdge(new Edge<object>(vertices[VerticesNumber1], vertices2[VerticesNumber2]));
            g.AddEdge(new Edge<object>(vertices2[VerticesNumber2], vertices[VerticesNumber1]));
        }

        private List<string> GetNodeVertices(BidirectionalGraph<object, IEdge<object>> g, int count, string mark)
        {
            List<string> vertices = new List<string>();

            for (int i = 0; i < count; i++)
            {
                vertices.Add($"{i.ToString()} {mark}");
                g.AddVertex(vertices[i]);
            }

            return vertices;
        }
        private void AddLineEdges(BidirectionalGraph<object, IEdge<object>> g, List<string> vertices)
        {
            AddEdges(g, vertices);
            vertices.Reverse();
            AddEdges(g, vertices);
            vertices.Reverse();
        }

        private static void AddEdges(BidirectionalGraph<object, IEdge<object>> g, List<string> vertices)
        {
            for (int i = 0; i < vertices.Count-1; i++)
            {
                int firstVertices = i;
                int secondVertices = i + 1;
                g.AddEdge(new Edge<object>(vertices[firstVertices], vertices[secondVertices]));
            }
        }
    }
}
