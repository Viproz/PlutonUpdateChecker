/*
 * Pluton Project
 * GSP Installer by DreTaX
 * How to run the program in cmd, on in bat: PlutonInstaller.exe PATH stable/latest
 * Example: (Watch the quotes " position) PlutonInstaller.exe "d:\Program Files (x86)\RustServer_1 " stable
 */
;#SingleInstance force
Menu, Tray, Default,
Menu, Tray, NoStandard
version = V1.0 By DreTaX 

para0 = %0%
para1 = %1%

loop %para0%
{
 szam++
  para%szam% = %A_Index%
 paralista .= %szam%
 paralista .= "`n"
}
directory = %1%
IfExist, %1%
{
	FormatTime, timee, , yyyy-MM-dd_HH-mm-ss
	ticks := A_TickCount
	FileAppend, `n`n---------------%timee%-----------------`n, %directory%\PlutonInstallerLog.txt
	IfNotExist, %directory%\RustDedicated.exe
	{
		;TrayTip, PlutonInstaller %version%, Directory is not a valid RUST folder. Exiting...
		FileAppend, Directory is not a valid RUST folder. Exiting...`n, %directory%\PlutonInstallerLog.txt
		ExitApp
	}
	vtodl = %2%
	IfEqual, vtodl, stable
	{
		IfNotExist, %directory%\Temp
		{
			FileCreateDir, %directory%\Temp
		}
		FileAppend, Downloading Stable Release....`n, %directory%\PlutonInstallerLog.txt
		;TrayTip, PlutonInstaller %version%, Downloading Stable Files...., 1
		URLDownloadToFile, https://github.com/Notulp/Pluton/raw/master/Distribution/stable.zip, %directory%\Temp\Pluton.zip
		;TrayTip, PlutonInstaller %version%, Extracting Files...., 1
		FileAppend, Extracting Files....`n, %directory%\PlutonInstallerLog.txt
		SetWorkingDir, %directory%\Temp
		sZip := A_WorkingDir . "\Pluton.zip"
		sUnz := A_WorkingDir . "\PlutonExt\"  
		Unz(sZip,sUnz)
		Sleep, 200
		FileAppend, Copying Files....`n, %directory%\PlutonInstallerLog.txt
		Loop, %A_WorkingDir%\PlutonExt\RustDedicated_Data\*.*, 1, 1
		{
			FileCopy, %A_WorkingDir%\PlutonExt\RustDedicated_Data\%A_LoopFileName%, %directory%\RustDedicated_Data\%A_LoopFileName%, 1
		}
		Sleep, 500
		FileRemoveDir, %directory%\Temp, 1
		FileAppend, Running Patcher....`n, %directory%\PlutonInstallerLog.txt
		;TrayTip, PlutonInstaller %version%, Running patcher!, 3
		Run, Pluton.Patcher.exe "--no-input", %directory%\RustDedicated_Data\Managed
		; Be sure to check the patcher's return later here
		Sleep, 500
		FileAppend, Patching Complete!`n, %directory%\PlutonInstallerLog.txt
		;TrayTip, PlutonInstaller %version%, Complete!, 3
		FormatTime, csomagolasvege, , yyyy-MM-dd_HH-mm-ss
		passt := A_TickCount - ticks
		passtsec := passt / 1000
		passtmin := passt / 60000
		FileAppend, Seconds Past: %passtsec%`n, %directory%\PlutonInstallerLog.txt
		FileAppend, Minutes Past: %passtmin%`n, %directory%\PlutonInstallerLog.txt
		FileAppend, ---------------%timee%-----------------`n`n, %directory%\PlutonInstallerLog.txt
		ExitApp
	}
	IfEqual, vtodl, latest
	{
		;TrayTip, PlutonInstaller %version%, Don't use this yet..., 3
		FileAppend, Don't use this yet...(Latest)`n, %directory%\PlutonInstallerLog.txt
		ExitApp
	}
}
else 
{
	;TrayTip, PlutonInstaller %version%, Invalid Directory. Exiting...
	FileAppend, Invalid Directory. Exiting...`n, %directory%\PlutonInstallerLog.txt
	ExitApp
}

Unz(sZip, sUnz)
{
    fso := ComObjCreate("Scripting.FileSystemObject")
    If Not fso.FolderExists(sUnz)  ;http://www.autohotkey.com/forum/viewtopic.php?p=402574
       fso.CreateFolder(sUnz)
    psh  := ComObjCreate("Shell.Application")
    zippedItems := psh.Namespace( sZip ).items().count
    psh.Namespace( sUnz ).CopyHere( psh.Namespace( sZip ).items, 4|16 )
    Loop {
        sleep 50
        unzippedItems := psh.Namespace( sUnz ).items().count
        ToolTip Unzipping in progress..
        IfEqual,zippedItems,%unzippedItems%
            break
    }
    ToolTip
}
