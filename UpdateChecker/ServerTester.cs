using System;
using System.Reflection;
using Mono.Cecil;
using Mono.Cecil.Cil;
using System.Diagnostics;
using System.IO;

namespace UpdateChecker {
    public class ServerTester {
        public string currentSteamBuildID;
        public string currentBuildID;
        public string currentPatcherVersion;
        public bool error = false;
        public AssemblyDefinition rustAssembly;

        public ServerTester() {
            currentPatcherVersion = AssemblyName.GetAssemblyName(Path.Combine(Util.getManagedPath(), "Pluton.Patcher.exe")).Version.ToString();
            currentSteamBuildID = "a";

            Console.WriteLine("Here");

            try {
                rustAssembly = AssemblyDefinition.ReadAssembly(Path.Combine(Util.getManagedPath(), "Assembly-CSharp.dll"));
            } catch(Exception ex) {
                error = true;
                return;
            }

            TypeDefinition buildVersionClass = rustAssembly.MainModule.GetType("BuildVersion");
            currentBuildID = buildVersionClass.GetMethod(".cctor").Body.Instructions[0x00].Operand.ToString();
        }

        public static bool patchServer() {
            ProcessStartInfo patcherProccessInfo = new ProcessStartInfo();
            patcherProccessInfo.FileName = Path.Combine(Util.getManagedPath(), "Pluton.Patcher.exe");
            patcherProccessInfo.WorkingDirectory = Util.getManagedPath();
            patcherProccessInfo.Arguments = "--no-input";

            using(Process proc = Process.Start(patcherProccessInfo)) {
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

