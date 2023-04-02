using System.Collections.Generic;
using System.Windows;

namespace RotationEditor.Views
{
	/// <summary>
	/// Interaction logic for NewPlateID.xaml
	/// </summary>
	public partial class NewPlateID : BaseWindow
	{
		public NewPlateID()
		{
			InitializeComponent();
		}

		private void btnGenerate_Click(object sender, RoutedEventArgs e)
		{
			//NewPlate = ParentWnd.Model.GeneratePlateID(ParentPlate);
			//int parentID = ParentWnd.Model.GetPlateIDs[SelectedPlateIndex];
			//NewPlate = ParentWnd.Model.GeneratePlateID(parentID);
		}
	}
}