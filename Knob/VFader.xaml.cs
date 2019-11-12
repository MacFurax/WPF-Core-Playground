
namespace Knob
{
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    /// <summary>
    /// Interaction logic for VFader.xaml
    /// </summary>
    public partial class VFader : UserControl, INotifyPropertyChanged
	{
        private string _label = "to set";
        private double _value, _min, _max;
		private class DataContexValues
		{
			
		}

		public VFader()
		{
			InitializeComponent();

			FaderPos = -100.0;

			DataContext = this;
		}

		DataContexValues dataContextValues = new DataContexValues();

		private bool _moving = false;
		private double _movingStartY = 0.0;
		public double FaderPos { get; set; }
        public string Label { 
            get => _label;
            set {
                if (value != _label)
                {
                    _label = value;
                    OnPropertyChanged("faderLabel");
                }
            } 
        }

        public double Value { get => _value; set => _value = value; }

        public void setRange( double value, double min, double max)
        {
            _value = value;
            _min = min;
            _max = max;

            FaderPos = interpolate(_value, _min, _max, 0.0, -195.0);
        }

        public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged(string name)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}

		private void PushButton_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			//Console.WriteLine("PushButton_MouseDown");
			Mouse.Capture(sender as IInputElement);
			_movingStartY = e.GetPosition(this).Y;
			_moving = true;
		}

		private void PushButton_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
		{
			if (_moving)
			{
				//Console.WriteLine("PushButton_MouseMove");
				double newY = e.GetPosition(this).Y;
				double delta = newY - _movingStartY;
				//Console.WriteLine("PushButton_MouseMove - Delta " + delta);
				
                FaderPos += delta;
				if (FaderPos > 0.0) FaderPos = 0.0;
				if(FaderPos < -195.0) FaderPos = -195.0;

                _value = interpolate(FaderPos, 0.0,-195.0, _min, _max);
				//Console.WriteLine("PushButton_MouseMove - FaderPos " + FaderPos);
				//Console.WriteLine("PushButton_MouseMove - _value " + _value);

				OnPropertyChanged(null);
				_movingStartY = newY;
			}
		}

		private void PushButton_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			//Console.WriteLine("PushButton_MouseUp");
			if (_moving)
			{
				_moving = false;
				Mouse.Capture(null);
			}
		}

        private double interpolate(double sourceValue, double sourceMin, double sourceMax, double targetMin, double targetMax)
        {
            return targetMin + (targetMax - targetMin) * ((sourceValue - sourceMin) / (sourceMax - sourceMin));
        }

    }
}
