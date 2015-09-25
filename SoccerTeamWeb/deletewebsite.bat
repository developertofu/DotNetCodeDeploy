cd c:\temp
%SystemRoot%\sysnative\WindowsPowerShell\v1.0\powershell.exe -command ".\deletewebsite.ps1"
IF EXIST c:\inetpub\wwwroot\soccerteamweb del c:\inetpub\wwwroot\soccerteamweb
IF EXIST c:\inetpub\wwwroot\soccerteamweb\bin del c:\inetpub\wwwroot\soccerteamweb\bin