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

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }        

        private void button_Click(object sender, RoutedEventArgs e)
        {
            string type = comboBox.Text;
            int size = int.Parse(textBox_Size.Text);
            int k = int.Parse(textBox_Value.Text);

            ResultSet result = speed(type, size, k);
            textBox_Result.Text = result.microseconds.ToString();
            if (result.found)
            {
                label4.Content = "Value found.";
            } else
            {
                label4.Content = "Value not found.";
            }
        }

        private ResultSet speed(string type, int size, int k)
        {
            //inicialização da array
            int[] array = new int[size];
            for (int i = 0; i < size; i++)
            {
                array[i] = i * 2 + 1;
            }

            bool found;
            bool endOfLoop;
            DateTime t0 = DateTime.Now;

            if (type.Equals("Linear"))
            {
                found = false;
                endOfLoop = false;
                for (int i = 0; i < size && !found && !endOfLoop; i++)
                {
                    if (array[i] == k)
                    {
                        found = true;
                    } else if (array[i] > k)
                    {
                        endOfLoop = true;
                    }
                }
            }
            else
            {
                found = false;
                endOfLoop = false;
                int a = 0, b = size - 1;
                int n;
                while (!endOfLoop && !found)
                {
                    n = (int)((b + a) / 2f);
                    if (array[n] == k)
                    {
                        found = true;
                    } else if (a == b)
                    {
                        endOfLoop = true;
                    }
                    else if (array[n] > k)
                    {
                        b = n - 1;
                    } else
                    {
                        a = n + 1;
                    }
                }
            }

            DateTime t1 = DateTime.Now;
            long miS = (t1 - t0).Duration().Ticks / 10;

            ResultSet rs = new ResultSet();
            rs.found = found;
            rs.microseconds = miS;

            return rs;
        }

        private struct ResultSet
        {
            public long microseconds;
            public bool found;
        }
    }
}
