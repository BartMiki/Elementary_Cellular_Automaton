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

namespace ElementaryCellularAutomaton
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int CellSize = 5;
        private CanvasHandler _canvasHandler;
        private Automata _automata;

        public MainWindow()
        {
            InitializeComponent();
            _canvasHandler = new CanvasHandler(CellsCanva,CellSize);
        }

        private void RunSimulation()
        {
            CellsCanva.Children.Clear();
            ErrorMessage.Content = string.Empty;
            try
            {
                
                CellsCanva.Width = CalculateCellCanvasWidth();
                CellsCanva.Height = CalculateCellCanvasHeight();
                bool[] initCells = ParseCellCount();

                Automata.AutomataBuilder automataBuilder = new Automata.AutomataBuilder();
                _automata = automataBuilder
                    .AddCanvasHandler(_canvasHandler)
                    .AddInitialCells(initCells)
                    .AddIterationDelay(TimeSpan.FromMilliseconds(5))
                    .AddRule(byte.Parse(RuleCodeBox.Text))
                    .AddIterationsCount(int.Parse(IterationsBox.Text))
                    .ToAutomata();

                _automata.RunAutomata();
            }
            catch (Exception e)
            {
                ErrorMessage.Content = e.Message;
            }
        }

        private int CalculateCellCanvasHeight()
        {
            try
            {
                int result = int.Parse(IterationsBox.Text) * CellSize;
                if (result < 1)
                    throw new ArgumentOutOfRangeException("Iterations must be higher then 0");
                return result;
            }
            catch (FormatException e)
            {
                throw new FormatException("Iterations must be integer.");
            }
        }

        private int CalculateCellCanvasWidth()
        {
            try
            {
                int result = int.Parse(CellCountBox.Text) * CellSize;
                if (result < 1)
                    throw new ArgumentOutOfRangeException("Cell count must be higher then 0");
                return result;
            }
            catch (FormatException e)
            {
                throw new FormatException("Cell count must be integer.");
            }
        }

        private bool[] ParseCellCount()
        {
            try
            {
                int size = int.Parse(CellCountBox.Text);
                if (size < 1)
                    throw new ArgumentOutOfRangeException("Cell count must be higher then 0");
                bool[] initCells = new bool[size];
                initCells[initCells.Length / 2] = true;
                return initCells;
            }
            catch (FormatException e)
            {
                throw new FormatException("Cell count must be integer.");
            }
        }

        private void UIElement_OnKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
                RunSimulation();
        }

        private void RunButton_OnClick(object sender, RoutedEventArgs e)
        {
            RunSimulation();
        }
    }
}
