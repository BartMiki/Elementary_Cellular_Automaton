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

namespace CellularAutomata
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int CellSize = 20;
        private CanvasHandler _canvasHandler;

        public MainWindow()
        {
            InitializeComponent();
            _canvasHandler = new CanvasHandler(CellsCanva,CellSize);
        }

        private void RunSimulation(object sender, RoutedEventArgs e)
        {
            try
            {
                CellsCanva.Width = int.Parse(CellCountBox.Text) * CellSize;
                CellsCanva.Height = int.Parse(IterationsBox.Text) * CellSize;
            }
            catch (Exception)
            {

            }
            finally
            {
                CellsCanva.Width = 5 * CellSize;
                CellsCanva.Height = 1 * CellSize;

                _canvasHandler.PrintIteration(
                    new bool[] { true, false, false, true, true }, 0);
            }
        }
    }
}
