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

			IDriftcorrectionService driftcorrectionService = rotationModel;
			INewPlateService newPlateService = rotationModel;
			IStartIndependentMoveService startIndependentMoveService = rotationModel;
			IJoinIndependentService joinIndependentService = rotationModel;
			IGetPlateIDsService plateIDsService = rotationModel;
			IGetRotationsService getRotationsService = rotationModel;
			IUpdateService updateService = rotationModel;
			ISaveService saveService = rotationModel;

			MainWindow = new MainWindow()
			{
				DataContext = new MainViewModel(
					driftcorrectionService, 
					newPlateService, 
					startIndependentMoveService, 
					joinIndependentService, 
					plateIDsService, 
					getRotationsService, 
					updateService, 
					saveService),
			};

			MainWindow.Show();

			base.OnStartup(e);
		}
	}
}