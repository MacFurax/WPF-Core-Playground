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
			//Console.WriteLine("MyKnob01::MyKnob01");
		}

		private double _angle = 0.0f;
		private bool dialing = false;
		private Point startDial;

		private const double minAngle = -145.0;
		private const double maxAngle = 145.0;

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
		

		public event PropertyChangedEventHandler PropertyChanged;

		public void setRange(double min, double max)
		{ 

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

				startDial = e.GetPosition(this);
			}
		}
	}
}
