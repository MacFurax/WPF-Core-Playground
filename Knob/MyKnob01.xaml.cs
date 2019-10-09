using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Knob
{
	/// <summary>
	/// Interaction logic for MyKnob01.xaml
	/// </summary>
	public partial class MyKnob01 : UserControl, INotifyPropertyChanged
	{
		public MyKnob01()
		{
			InitializeComponent();
			DataContext = this;
            setLabel(_label);
			//Console.WriteLine("MyKnob01::MyKnob01");
		}

		private double _angle = 0.0f;
		private bool dialing = false;
		private Point startDial;

		private const double minAngle = -145.0;
		private const double maxAngle = 145.0;

        private double _value = 0.0;
        private double _minValue = -1.0;
        private double _maxValue = 1.0;

        private string _label = "to set";

		public double Angle { 
			get => _angle;
			set
			{
				if (_angle != value)
				{
					_angle = value;
					OnPropertyChanged("Angle");
				}
				
			}
		}

        public double Value { get => _value; set => this._value = value; }

        public event PropertyChangedEventHandler PropertyChanged;
		public void setRange(double value, double min, double max)
		{
            _value = value;
            _minValue = min;
            _maxValue = max;

            if (_value < _minValue)
                _value = _minValue;
            else if (_value > _maxValue)
                _value = _maxValue;

            Angle = interpolate(_value, _minValue, _maxValue, minAngle, maxAngle);

            displayValue.Content = _value.ToString();
		}

        public void setLabel(string label)
        {
            _label = label;
            knobLabel.Content = _label;
        }

        private double interpolate(double sourceValue, double sourceMin, double sourceMax, double targetMin, double targetMax)
        {
            return targetMin + (targetMax-targetMin) * ((sourceValue - sourceMin) /(sourceMax- sourceMin));
        }

		protected void OnPropertyChanged(string name)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}

		private void KnobGrid_MouseDown(object sender, MouseButtonEventArgs e)
		{
			//Console.WriteLine("MyKnob01::KnobGrid_MouseDown");
			dialing = true;
			startDial = e.GetPosition(this);
			Mouse.Capture(sender as IInputElement);
		}

		private void KnobGrid_MouseUp(object sender, MouseButtonEventArgs e)
		{
			//Console.WriteLine("MyKnob01::KnobGrid_MouseUp");
			dialing = false;
			Mouse.Capture(null);
		}

		private void KnobGrid_MouseMove(object sender, MouseEventArgs e)
		{
			//Console.WriteLine("MyKnob01::KnobGrid_MouseDown");
			if (dialing)
			{
				double diff = e.GetPosition(this).X - startDial.X;
				//Console.WriteLine("MyKnob01::KnobGrid_MouseDown - Diff " + diff);

				double newAngle = _angle + diff;
				if (newAngle < minAngle)
				{
					newAngle = minAngle;
				}
				else if (newAngle > maxAngle)
				{
					newAngle = maxAngle;
				}
				Angle = newAngle;

                _value = interpolate(newAngle, minAngle, maxAngle, _minValue, _maxValue);
                displayValue.Content = _value.ToString("0.##");

                startDial = e.GetPosition(this);
			}
		}
	}
}
