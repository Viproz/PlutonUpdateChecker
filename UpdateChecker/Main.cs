using System;
using System.Diagnostics;

namespace UpdateChecker {
	class MainClass {
		public static int Main(string[] args) {
			Console.WriteLine("Hello World!");
			patchServer();
			Console.ReadKey();


			return 1;
		}

		public static bool patchServer() {
			using(Process proc = Process.Start("TestReturn1.exe")) {
				proc.WaitForExit();

				Console.WriteLine(proc.ExitCode.ToString());
			}
			return true;
		}

		public static bool launchServer() {
			return true;
		}
	}
}
