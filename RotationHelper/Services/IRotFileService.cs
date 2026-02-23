using System;
using System.Collections.Generic;
using System.Text;

namespace RotationModel.Services
{
	public interface IRotFileService
	{
		void Update(string fileName);
		void Save();
		void Save(string fileName);

	}
}
