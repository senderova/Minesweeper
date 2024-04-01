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

namespace Kontrolnaya
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
        {
            Random r = new Random();
            int[,] array;
            bool[,] arr;
            int numOfBombs = 0;
            int kl = 0;
            int f_c = 0;
            int height = 10;
            int width = 10;
            public MainWindow()
            {
                InitializeComponent();
                generate_level();
                generate_bombs();
            }

            void generate_bombs()
            {
                int i = 0;
                while (i < 10)
                {
                    int x = r.Next(0, height);
                    int y = r.Next(0, width);
                    if (array[y, x] != 1)
                    {
                        array[y, x] = 1;
                        i++;
                        numOfBombs++;
                    }
                }
            }

            void generate_level()
            {
                array = new int[height, width];
                arr = new bool[height, width];
                for (int i = 0; i < height; i++)
                {
                    g.RowDefinitions.Add(new RowDefinition());
                }
                for (int i = 0; i < width; i++)
                {
                    g.ColumnDefinitions.Add(new ColumnDefinition());
                }
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        Button b = new Button();
                        b.Content = "";
                        b.Click += new RoutedEventHandler(Button_Click);
                        b.MouseRightButtonDown += new MouseButtonEventHandler(Button_MouseRightButtonDown);
                        g.Children.Add(b);
                        b.Background = Brushes.LightGray;
                        Grid.SetColumn(b, j);
                        Grid.SetRow(b, i);
                        arr[i, j] = false;
                    }
                }
            }

            private void Button_Click(object sender, RoutedEventArgs e)
            {
                Button b = (sender as Button);
                kl++;
                int i = Grid.GetRow(b);
                int j = Grid.GetColumn(b);
                int f = 0;
                if (array[i, j] == 1)
                {
                    b.Background = Brushes.White;
                    b.Content = new Image
                    {
                        Source = new BitmapImage(new Uri("Resources/unnamed.png", UriKind.Relative)),
                        VerticalAlignment = VerticalAlignment.Center
                    };
                    MessageBox.Show("Вы проиграли");
                    this.Close();
                }
                else
                {
                    b.Background = Brushes.LightGreen;
                    if (j < 9 && array[i, j + 1] == 1)
                    {
                        f++;
                    }
                    if (j > 0 && array[i, j - 1] == 1)
                    {
                        f++;
                    }
                    if (j < 9 && i < 9 && array[i + 1, j + 1] == 1)
                    {
                        f++;
                    }
                    if (j < 9 && i > 0 && array[i - 1, j + 1] == 1)
                    {
                        f++;
                    }
                    if (j > 0 && i < 9 && array[i + 1, j - 1] == 1)
                    {
                        f++;
                    }
                    if (i > 0 && j > 0 && array[i - 1, j - 1] == 1)
                    {
                        f++;
                    }
                    if (i < 9 && array[i + 1, j] == 1)
                    {
                        f++;
                    }
                    if (i > 0 && array[i - 1, j] == 1)
                    {
                        f++;
                    }
                    if (f != 0)
                    {
                        b.Content = f;
                    }
                    if (f_c == numOfBombs && kl == 100)
                    {
                        MessageBox.Show("Победа!");
                        this.Close();
                    }
                }
            }

            private void Button_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
            {
                Button b = (sender as Button);
                int i = Grid.GetRow(b);
                int j = Grid.GetColumn(b);
                if (arr[i, j] == true)
                {
                    f_c--;
                    b.Background = Brushes.LightGray;
                    kl--;
                    b.Content = "";
                    arr[i, j] = false;
                }
                else if (f_c < 10)
                {
                    kl++;
                    b.Background = Brushes.LightPink;
                    b.Content = new Image
                    {
                        Source = new BitmapImage(new Uri("Resources/Flag_256x256_32.png", UriKind.Relative)),
                        VerticalAlignment = VerticalAlignment.Center
                    };
                    f_c++;
                    if (f_c == numOfBombs && kl == 100)
                    {
                        MessageBox.Show("Победа!");
                        this.Close();
                    }
                    arr[i, j] = true;
                }
            }
        }
    }

