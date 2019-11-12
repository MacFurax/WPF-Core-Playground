
namespace Knob
{
    using System.Windows;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            knob01.setRange(0.75, 0.0, 1.0);
            knob01.setLabel("level");

            knob02.setRange(0.0, -1.0, 1.0);
            knob02.setLabel("detune");

			knob03.setRange(180.0, 0.0, 180.0);
			knob03.setLabel("cutoff");

			knob04.setRange(0.0, 0.0, 1.0);
			knob04.setLabel("reso");

            fader01.Label = "A";
            fader01.setRange(10.0, 0.001, 3000.0);
            fader02.Label = "D";
            fader02.setRange(400.0, 0.001, 3000.0);
            fader03.Label = "S";
            fader03.setRange(0.85, 0.0, 1.0);
            fader04.Label = "R";
            fader04.setRange(500.0, 0.001, 3000.0);
        }
    }
}
