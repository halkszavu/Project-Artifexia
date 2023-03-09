using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rotation_Editor
{
	public class NewPlateIDMessage : Message
	{
		int _newPlateID;
		public int NewPlateID
		{
			get => _newPlateID;
			set
			{
				if(_newPlateID != value)
				{
					_newPlateID = value;
					OnPropertyChanged();
				}
			}
		}

		int _parentPlateID;
		public int ParentPlateID
		{
			get => _parentPlateID;
			set
			{
				if(value != _parentPlateID)
				{
					_parentPlateID = value;
					OnPropertyChanged();
				}
			}
		}
	}

	public class TimeStampMessage : Message
	{
		double _timeStamp;
		public double TimeStamp
		{
			get => _timeStamp;
			set
			{
				if(value != _timeStamp)
				{
					TimeStamp = value;
					OnPropertyChanged();
				}
			}
		}
	}

	public class PlateIDMessage : Message
	{
		int _plateID;
		public int PlateID
		{
			get => _plateID;
			set
			{
				if (value != _plateID)
				{
					_plateID = value;
					OnPropertyChanged();
				}
			}
		}
	}

	public class CoordinateMessage : Message
	{
		double _latitude;
		public double Latitude 
		{
			get => _latitude;
			set
			{
				if(_latitude != value)
				{
					_latitude = value;
					OnPropertyChanged();
				}
			} 
		}
		double _longitude;
		public double Longitude 
		{
			get => _longitude;
			set
			{
				if(_longitude != value)
				{
					_longitude = value;
					OnPropertyChanged();
				}
			}
		}
		double _angle;
		public double Angle 
		{
			get => _angle;
			set
			{
				if(value != _angle)
				{
					_angle = value;
					OnPropertyChanged();
				}
			}
		}
	}
}
