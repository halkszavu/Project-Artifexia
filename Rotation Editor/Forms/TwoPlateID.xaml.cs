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
	/// Interaction logic for TwoPlateID.xaml
	/// </summary>
	public partial class TwoPlateID : BaseWindow
	{
		public List<int> PlateIDs
		{
			get => ParentWnd.Model.GetPlateIDs;
		}

		int _1selected;
		public int FirstSelectedIndex
		{
			get => _1selected;
			set
			{
				if (_1selected != value)
				{
					_1selected = value;
					OnPropertyChanged();
				}
			}
		}
		int _2selected;
		public int SecondSelectedIndex
		{
			get => _2selected;
			set
			{
				if (_2selected != value)
				{
					_2selected = value;
					OnPropertyChanged();
				}
			}
		}
		public TwoPlateID()
		{
			InitializeComponent();
		}
	}
}
