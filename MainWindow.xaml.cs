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

namespace LABYRINTH
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnContentRendered(EventArgs e)
        {
            base.OnContentRendered(e);
            Labyrinth labyrinth = new Labyrinth(Canvas,10);
            labyrinth.Generate();
        }

        private void GenerateButton_OnClick(object sender, RoutedEventArgs e)
        {
            Canvas.Children.Clear();
            int size, x, y;
            try
            {
                Labyrinth labyrinth;
                if (Size.Text != "")
                {  size = Int32.Parse(Size.Text);
                   labyrinth = new Labyrinth(Canvas,size);
                }
                else
                {
                    labyrinth = new Labyrinth(Canvas);
                }
                
                
                if (XEntryPoint.Text != "" && YEntryPoint.Text != "")
                {
                    x = Int32.Parse(XEntryPoint.Text);
                    y = Int32.Parse(YEntryPoint.Text);
                    labyrinth.Generate(x, y);
                }
                else
                {
                    labyrinth.Generate();
                }
                
                
                
            }
            catch(Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            
        }
    }
}
