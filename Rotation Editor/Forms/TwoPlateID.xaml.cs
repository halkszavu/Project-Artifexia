using System.Collections.Generic;

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
