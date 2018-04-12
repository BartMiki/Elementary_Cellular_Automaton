using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CellularAutomata
{
    public class CanvasHandler
    {
        private readonly Canvas _handledCanvas;
        private readonly int _cellSize;

        public CanvasHandler(Canvas handledCanvas, int cellSize)
        {
            _handledCanvas = handledCanvas;
            _cellSize = cellSize;
        }

        public void PrintIteration(bool[] cells, int iteration)
        {
            for (var index = 0; index < cells.Length; index++)
            {
                if (cells[index])
                {
                    PrintCell(index,iteration);
                }
            }
        }

        private void PrintCell(int x, int y)
        {
            int absoluteX = x * _cellSize;
            int absoluteY = y * _cellSize;

            Rectangle cell = new Rectangle();
            cell.Height = cell.Width = _cellSize;
            cell.Fill = Brushes.Black;

            _handledCanvas.Children.Add(cell);
            Canvas.SetLeft(cell,absoluteX);
            Canvas.SetTop(cell,absoluteY);
        }
    }
}
