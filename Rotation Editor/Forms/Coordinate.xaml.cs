using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Rotation_Editor
{
	/// <summary>
	/// Interaction logic for Coordinate.xaml
	/// </summary>
	public partial class Coordinate : BaseWindow
	{
		double _latitude;
		public double Latitude
		{
			get => _latitude;
			set
			{
				if (_latitude != value)
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
				if(value != _longitude)
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
		string _helpTxt;
		public string HelpText
		{
			get => _helpTxt;
			set
			{
				_helpTxt = value;
				OnPropertyChanged();
			}
		}
		public Coordinate()
		{
			InitializeComponent();
		}
	}
}
