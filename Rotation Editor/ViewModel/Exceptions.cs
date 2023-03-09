﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rotation_Editor.ViewModel
{
	internal class PlateIDExsistException : Exception
	{
		public PlateIDExsistException() : base() { }

		public PlateIDExsistException(string? message) : base(message) { }

		public PlateIDExsistException(string? message, Exception innerException):base(message, innerException) { }
	}
}