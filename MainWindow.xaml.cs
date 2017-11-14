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
        private Labyrinth _labyrinth;
        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnContentRendered(EventArgs e)
        {
            base.OnContentRendered(e);
            this._labyrinth = new Labyrinth(Canvas,10);
            this._labyrinth.Generate();
        }

        private void GenerateButton_OnClick(object sender, RoutedEventArgs e)
        {
            Canvas.Children.Clear();
            int size, x, y;
            try
            {
                if (Size.Text != "")
                {  size = Int32.Parse(Size.Text);
                   this._labyrinth = new Labyrinth(Canvas,size);
                }
                else
                {
                    this._labyrinth = new Labyrinth(Canvas);
                }
                
                
                if (XGenerationPointt.Text != "" && YGenerationPoint.Text != "")
                {
                    x = Int32.Parse(XGenerationPointt.Text);
                    y = Int32.Parse(YGenerationPoint.Text);
                    this._labyrinth.Generate(x, y);
                }
                else
                {
                    this._labyrinth.Generate();
                }
                
                this.UpdateExtremities();

            }
            catch(Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            
        }

        private void ResolveButton_OnClick(object sender, RoutedEventArgs e)
        {
            this._labyrinth.Resolve();
        }

        private void UpdateExtremities()
        {
            try
            {
                if (XExitPoint.Text != "" && YExitPoint.Text != "")
                {
                    int x = Int32.Parse(XExitPoint.Text);
                    int y = Int32.Parse(YExitPoint.Text);
                    this._labyrinth.SetExitPoint(x, y);
                }

                if (XEntryPoint.Text != "" && YEntryPoint.Text != "")
                {
                    int x = Int32.Parse(XEntryPoint.Text);
                    int y = Int32.Parse(YEntryPoint.Text);
                    this._labyrinth.SetEntryPoint(x, y);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
        private void UpdateButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.UpdateExtremities();
        }
    }
}
