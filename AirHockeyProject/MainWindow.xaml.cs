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
using System.Windows.Threading;

namespace AirHockeyProject
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public CollisionObject Puck { get; }

        public MainWindow()
        {
            InitializeComponent();
            Puck = new CollisionObject(ellipseButton, Canvas.SetLeft, Canvas.SetTop, null);
            Puck.CreateMoving(new Point(0,0), new Point(50,50));
        }

        private void EllipseButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            GameWindow gameWindow = new GameWindow();
            gameWindow.Show();
            Close();
        }
    }
}
