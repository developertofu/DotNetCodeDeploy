Import-Module WebAdministration
$iisAppPoolName = "soccerteamweb-app"
$iisAppPoolDotNetVersion = "v4.0"
$iisAppName = "soccerteamweb"
$directoryPath = "C:\inetpub\wwwroot\soccerteamweb"

#navigate to the app pools root
cd IIS:\AppPools\

#check if the app pool exists
if (!(Test-Path $iisAppPoolName -pathType container))
{
    #create the app pool
    $appPool = New-Item $iisAppPoolName
    $appPool | Set-ItemProperty -Name "managedRuntimeVersion" -Value $iisAppPoolDotNetVersion
}

#navigate to the sites root
cd IIS:\Sites\

#check if the site exists
if (Test-Path $iisAppName -pathType container)
{
    return
}

#create the site
$iisApp = New-Item $iisAppName -bindings @{protocol="http";bindingInformation=":80:" + $iisAppName} -physicalPath $directoryPath
$iisApp | Set-ItemProperty -Name "applicationPool" -Value $iisAppPoolName