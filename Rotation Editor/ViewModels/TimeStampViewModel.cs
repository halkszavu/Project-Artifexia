namespace RotationEditor.ViewModel
{
	internal class TimeStampViewModel : ViewModelBase
	{
		double _timeStamp;
		public double DesiredTimestamp
		{
			get => _timeStamp;
			set
			{
				if(_timeStamp != value)
				{
					_timeStamp = value;
					OnPropertyChanged();
				}
			}
		}
	}
}
