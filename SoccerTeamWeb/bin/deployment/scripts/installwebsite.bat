IF NOT EXIST c:\inetpub\wwwroot\soccerteamweb mkdir c:\inetpub\wwwroot\soccerteamweb
IF NOT EXIST c:\inetpub\wwwroot\soccerteamweb\bin mkdir c:\inetpub\wwwroot\soccerteamweb\bin
cd c:\temp
%SystemRoot%\sysnative\WindowsPowerShell\v1.0\powershell.exe -command ".\runscript.ps1"