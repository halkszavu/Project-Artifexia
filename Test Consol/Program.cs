﻿using RotationModel;
using Test_Consol;

using static System.Console;

WriteLine("Initializing Test Console");
WriteLine("Parsing the second rotation test case:");
RotationModel.RotationRecontstructionModel reconstruction = Parser.ParseReconstruction(TestCases.Test2);
ReadKey();