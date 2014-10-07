using System;
using System.IO;
using System.Net;

namespace UpdateChecker {
    class MainClass {

		private static bool update = false;

        public static int Main(string[] args) {
            Console.WriteLine("Hello World!");
			int result = 1;

			foreach (string arg in args) {
				if (arg.ToLower() == "--update" || arg.ToLower() == "-u") {
					update = true;
				}
				if (update && arg == "stable" || arg == "latest") {
					string zName = arg + ".zip";
					using (WebClient Client = new WebClient ())
					{
						Client.DownloadFile("https://github.com/Notulp/Pluton/raw/master/Distribution/" + zName, Path.Combine(Environment.CurrentDirectory, zName));
					}

					System.Threading.Thread.Sleep(250);
					result = ExtractRelease.Extract(zName);
					System.Threading.Thread.Sleep(250);
					if (File.Exists(zName))
						File.Delete(zName);
				}
			}

			new ServerTester();
            Console.ReadKey();

			return result;

			/* Result:
			 * 1 - All good
			 * 2 - Error while extracting the file
			 * 
			 */
        }
    }
}
