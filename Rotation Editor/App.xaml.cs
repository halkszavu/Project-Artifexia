using RotationEditor.ViewModel;
using RotationModel;
using System.Windows;

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
			ICratonService addCratonService = rotationModel;

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
					saveService,
					addCratonService),
			};

			MainWindow.Show();

			base.OnStartup(e);
		}
	}
}