using System;

namespace RotationEditor.ViewModel
{
	internal class PlateIDExsistException : Exception
	{
		public PlateIDExsistException() : base() { }

		public PlateIDExsistException(string? message) : base(message) { }

		public PlateIDExsistException(string? message, Exception innerException) : base(message, innerException) { }
	}
}
