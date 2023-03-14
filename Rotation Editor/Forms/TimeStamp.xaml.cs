namespace Rotation_Editor
{
	/// <summary>
	/// Interaction logic for TimeStamp.xaml
	/// </summary>
	public partial class TimeStamp : BaseWindow
	{
		double _timeStamp;
		public double DesiredTimestamp
		{
			get => _timeStamp;
			set
			{
				if (_timeStamp != value)
				{
					_timeStamp = value;
					OnPropertyChanged();
				}
			}
		}

		public TimeStamp()
		{
			InitializeComponent();
		}
	}
}
