Param(
    [Parameter(Mandatory=$true, HelpMessage="You must provide a path/location of the solution.")]
    $Path,
    $SitePort="13",
	$ApiPort="14"
)

Import-Module WebAdministration

$IisAppPoolDotNetVersion = "v4.0"
$IisAppPoolName = "bloggity"

$SiteAppName = "Bloggity"
$SiteDirectoryPath = $($Path) + "\Blog.Web\Blog.Web.Site"

$ApiAppName = "BloggityApi"
$ApiDirectoryPath = $($Path) + "\Blog.Web\Blog.Web.Api"

$WcfBlogService = "BlogWcf"
$WcfBlogServiceDirectoryPath = $($Path) + "\Blog.Services\Blog.Services.Web"

#navigate to the app pools root
cd IIS:\AppPools\

#check if the app pool exists
if (!(Test-Path $IisAppPoolName -pathType container))
{
    #create the app pool
    $appPool = New-Item $IisAppPoolName
    $appPool | Set-ItemProperty -Name "managedRuntimeVersion" -Value $IisAppPoolDotNetVersion
	Write-Host "Successfully created Bloggity application pool"
}

#navigate to the sites root
cd IIS:\Sites\

#check if the site exists
if (!(Test-Path $SiteAppName -pathType container))
{
    #create the site
    $iisApp = New-Item $SiteAppName -bindings @{protocol="http";bindingInformation=":" + $SitePort +":"} -physicalPath $SiteDirectoryPath
    $iisApp | Set-ItemProperty -Name "applicationPool" -Value $IisAppPoolName
	New-WebBinding -Name $SiteAppName -IP "*" -Port 4433 -Protocol https
	Write-Host "Successfully created Bloggity Site"
}

if (!(Test-Path $ApiAppName -pathType container))
{
    #create the site
    $iisApp = New-Item $ApiAppName -bindings @{protocol="http";bindingInformation=":" + $ApiPort +":"} -physicalPath $ApiDirectoryPath
    $iisApp | Set-ItemProperty -Name "applicationPool" -Value $IisAppPoolName
	New-WebBinding -Name $ApiAppName -IP "*" -Port 4434 -Protocol https
	Write-Host "Successfully created Bloggity Api Site"
}

$WcfBlogServiceFullPath = "Default Web Site\" + $($WcfBlogService)
if (!(Test-Path $WcfBlogServiceFullPath -pathType container))
{
    #create the site
    $iisApp = New-WebApplication -Name $WcfBlogService -Site "Default Web Site" -PhysicalPath $WcfBlogServiceDirectoryPath -ApplicationPool $IisAppPoolName
	Write-Host "Successfully created Bloggity WCF Service"
}