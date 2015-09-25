cd c:\temp
%SystemRoot%\sysnative\WindowsPowerShell\v1.0\powershell.exe -command ".\deletewebsite.ps1"
IF EXIST c:\inetpub\wwwroot\soccerteamweb rmdir /S /Q c:\inetpub\wwwroot\soccerteamweb