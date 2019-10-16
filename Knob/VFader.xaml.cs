
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

		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged(string name)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}

		private void PushButton_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			Console.WriteLine("PushButton_MouseDown");
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
				Console.WriteLine("PushButton_MouseMove - Delta " + delta);
				FaderPos += delta;
				if (FaderPos > 0.0) FaderPos = 0.0;
				if(FaderPos < -200.0) FaderPos = -200.0;
				Console.WriteLine("PushButton_MouseMove - FaderPos " + FaderPos);
				OnPropertyChanged(null);
				_movingStartY = newY;
			}
		}

		private void PushButton_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			Console.WriteLine("PushButton_MouseUp");
			if (_moving)
			{
				_moving = false;
				Mouse.Capture(null);
			}
		}

		
	}
}
