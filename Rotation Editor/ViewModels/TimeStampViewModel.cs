using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rotation_Editor
{
	internal class TimeStampViewModel : ViewModelBase
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
	}
}
