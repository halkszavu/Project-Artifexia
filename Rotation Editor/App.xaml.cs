using RotationEditor.ViewModel;
using RotationModel;
using RotationModel.Services;
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

			IRotModelService rotModelService = rotationModel;
			IRotFileService rotFileService = rotationModel;

			MainWindow = new MainWindow()
			{
				DataContext = new MainViewModel(
					rotModelService,
					rotFileService),
			};

			MainWindow.Show();

			base.OnStartup(e);
		}
	}
}