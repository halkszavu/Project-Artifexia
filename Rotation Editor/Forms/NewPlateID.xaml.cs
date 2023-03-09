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
	/// Interaction logic for NewPlateID.xaml
	/// </summary>
	public partial class NewPlateID : BaseWindow
	{
		int _newPlate;
		public int NewPlate
		{
			get => _newPlate;
			set
			{
				if (_newPlate != value)
				{
					_newPlate = value;
					OnPropertyChanged();
				}
			}
		}
		int _parentPlate;
		public int ParentPlate
		{
			get => _parentPlate;
			set
			{
				if (_parentPlate != value)
				{
					_parentPlate = value;
					OnPropertyChanged();
				}
			}
		}

		public NewPlateID()
		{
			InitializeComponent();
		}

		private void btnGenerate_Click(object sender, RoutedEventArgs e)
		{
			NewPlate = ParentWnd.Model.GeneratePlateID(ParentPlate);
		}
	}
}