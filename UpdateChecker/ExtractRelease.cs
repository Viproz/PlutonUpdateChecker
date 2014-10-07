using System;
using System.IO;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;

namespace UpdateChecker {
	public class ExtractRelease {

		public static int Extract(string zipFrom) {
			string pathTo = Environment.CurrentDirectory;
			ZipFile zipFile = null;
			try {
				FileStream fs = File.OpenRead(Path.Combine(Environment.CurrentDirectory, zipFrom));
				zipFile = new ZipFile(fs);
				foreach (ZipEntry zEntry in zipFile) {
					if (!zEntry.IsFile) {
						continue;
					}
					string entryFileName = zEntry.Name;

					byte[] buffer = new byte[4096];
					Stream zipStream = zipFile.GetInputStream(zEntry);

					string fullZipToPath = Path.Combine(pathTo, entryFileName);
					string directoryName = Path.GetDirectoryName(fullZipToPath);
					if (!Directory.Exists(directoryName))
						Directory.CreateDirectory(directoryName);

					using (FileStream streamWriter = File.Create(fullZipToPath)) {
						StreamUtils.Copy(zipStream, streamWriter, buffer);
					}
				}
			} catch (Exception ex) {
				Console.WriteLine(ex.StackTrace);
				return 2;
			} finally {
				if (zipFile != null) {
					zipFile.IsStreamOwner = true; // Makes close also shut the underlying stream
					zipFile.Close(); // Ensure we release resources
				}
			}
			return 1;
		}

		public ExtractRelease () {}
	}
}

