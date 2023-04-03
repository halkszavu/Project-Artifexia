using RotationEditor;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using RotationModel;
using RotationEditor.ViewModel;

namespace RotationEditor
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			RotationRecontstructionModel rotationModel = new RotationRecontstructionModel();

			MainWindow = new MainWindow()
			{
				DataContext = new MainViewModel(rotationModel, rotationModel, rotationModel, rotationModel, rotationModel),
			};

			MainWindow.Show();

			base.OnStartup(e);
		}
	}
}