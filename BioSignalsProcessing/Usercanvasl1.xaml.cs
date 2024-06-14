using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace biosignals
{
    /// <summary>
    /// Interaction logic for Usercanvasl1.xaml
    /// </summary>
    public partial class Usercanvasl1 : UserControl
    {
        public Usercanvasl1()
        {
            InitializeComponent();
        }

        private void box1active(object sender, System.Windows.RoutedEventArgs e)
        {
        	text.Text = "box 1 activated";
        }

        private void box1inactive(object sender, System.Windows.RoutedEventArgs e)
        {
        	text.Text = "box 1 deactivated";
        }

        private void box2clickstate(object sender, System.Windows.RoutedEventArgs e)
        {
        	if ((bool)box2.IsChecked) text.Text = "box 2 activated";
			else text.Text = "box 2 deactivated";
        }
    }
}
