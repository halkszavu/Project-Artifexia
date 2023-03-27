using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotationEditor
{
	public class RotationViewModel : ViewModelBase
	{
		public int PlateID { get; }
		private double timeStamp;

		public double TimeStamp 
		{ 
			get => timeStamp; 
			set 
			{
				if (timeStamp != value)
				{
					timeStamp = value;
					OnPropertyChanged();
				}
			}
		}
		private double longitude;
		public double Longitude 
		{
			get => longitude;
			set
			{
				if (longitude != value)
				{
					longitude = value;
					OnPropertyChanged();
				}
			}
		}
		private double latitude;
		public double Latitude
		{
			get => latitude;
			set
			{
				if (latitude != value)
				{
					latitude = value;
					OnPropertyChanged();
				}
			}
		}
		private double angle;
		public double Angle
		{
			get => angle;
			set
			{
				if (angle != value)
				{
					angle = value;
					OnPropertyChanged();
				}
			}
		}
		private int conjugateID;
		public int ConjugateID
		{
			get => conjugateID;
			set
			{
				if (conjugateID != value)
				{
					conjugateID = value;
					OnPropertyChanged();
				}
			}
		}
		private string comment;
		public string Comment
		{
			get => comment;
			set
			{
				if (comment != value)
				{
					comment = value;
					OnPropertyChanged();
				}
			}
		}

		public RotationViewModel(int plateId)
		{
			PlateID = plateId;
		}
	}
}