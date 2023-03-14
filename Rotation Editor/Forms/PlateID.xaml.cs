using System.Collections.Generic;

namespace Rotation_Editor
{
	/// <summary>
	/// Interaction logic for PlateID.xaml
	/// </summary>
	public partial class PlateID : BaseWindow
	{
		public List<int> PlateIDs
		{
			get => ParentWnd.Model.GetPlateIDs;
		}

		int _selected;
		public int SelectedIndex
		{
			get => _selected;
			set
			{
				if (_selected != value)
				{
					_selected = value;
					OnPropertyChanged();
				}
			}
		}
		public PlateID()
		{
			InitializeComponent();
		}
	}
}
