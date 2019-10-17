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

namespace Knob
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            knob01.setRange(0.75, 0.0, 1.0);
            knob01.setLabel("LEVEL");

            knob02.setRange(0.0, -1.0, 1.0);
            knob02.setLabel("DETUNE");

			knob03.setRange(180.0, 0.0, 180.0);
			knob03.setLabel("CUTOFF");

			knob04.setRange(0.0, 0.0, 1.0);
			knob04.setLabel("RESO");
		}
    }
}
