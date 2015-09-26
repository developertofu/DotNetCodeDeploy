cd c:\temp
%SystemRoot%\sysnative\WindowsPowerShell\v1.0\powershell.exe -command ".\deletewebsite.ps1"
IF EXIST c:\inetpub\wwwroot\soccerteamweb rmdir /f /s /q c:\inetpub\wwwroot\soccerteamweb