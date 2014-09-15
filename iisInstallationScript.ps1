Param(
	[Parameter(Mandatory=$true, HelpMessage="You must provide a path/location of the solution.")]
	$Path,
	$SitePort="3313",
	$ApiPort="3314",
	$SocketsPort="3315",
	$AdminPort="3316",
	$WcfPort="3317"
)

Import-Module WebAdministration

$IisAppPoolDotNetVersion = "v4.0"
$IisAppPoolName = "bloggity"
$IisSocketsAppPoolName = "blogsockets"
$IisAdminAppPoolName = "blogadmin"
$IisWcfAppPoolName = "blogwcf"

$SiteAppName = "Bloggity"
$SiteDirectoryPath = $($Path) + "\Blog.Web\Blog.Web.Site"

$ApiAppName = "BloggityApi"
$ApiDirectoryPath = $($Path) + "\Blog.Web\Blog.Web.Api"

$SocketsAppName = "BlogSockets"
$SocketsDirectoryPath = $($Path) + "\Blog.Web\Blog.Web.Sockets"

$AdminAppName = "BlogAdmin"
$AdminDirectoryPath = $($Path) + "\Blog.Admin\Blog.Admin.Web"

$WcfBlogService = "BlogWcf"
$WcfBlogServiceDirectoryPath = $($Path) + "\Blog.Services\Blog.Services.Web"

#navigate to the app pools root
cd IIS:\AppPools\

#check if the app pool exists

Write-Host "====================================================="
Write-Host "Validating application pools"
Write-Host "====================================================="

if (!(Test-Path $IisAppPoolName -pathType container))
{
	#create the app pool
	$appPool = New-Item $IisAppPoolName
	$appPool | Set-ItemProperty -Name "managedRuntimeVersion" -Value $IisAppPoolDotNetVersion
	Write-Host "Successfully created bloggity application pool"
}

#check if the sockets app pool exists
if (!(Test-Path $IisSocketsAppPoolName -pathType container))
{
	#create the app pool
	$appPoolSockets = New-Item $IisSocketsAppPoolName
	$appPoolSockets | Set-ItemProperty -Name "managedRuntimeVersion" -Value $IisAppPoolDotNetVersion
	Write-Host "Successfully created blog sockets application pool"
}

#check if the admin app pool exists
if (!(Test-Path $IisAdminAppPoolName -pathType container))
{
	#create the app pool
	$appPoolAdmin = New-Item $IisAdminAppPoolName
	$appPoolAdmin | Set-ItemProperty -Name "managedRuntimeVersion" -Value $IisAppPoolDotNetVersion
	Write-Host "Successfully created blog admin application pool"
}

#check if the blog wcf app pool exists
if (!(Test-Path $IisWcfAppPoolName -pathType container))
{
	#create the app pool
	$appPoolAdmin = New-Item $IisWcfAppPoolName
	$appPoolAdmin | Set-ItemProperty -Name "managedRuntimeVersion" -Value $IisAppPoolDotNetVersion
	Write-Host "Successfully created blog wcf application pool"
}

#navigate to the sites root
cd IIS:\Sites\

#check if the site exists

Write-Host ""
Write-Host "====================================================="
Write-Host "Validating web sites"
Write-Host "====================================================="

if (!(Test-Path $SiteAppName -pathType container))
{
	#create the site
	$iisApp = New-Item $SiteAppName -bindings @{protocol="http";bindingInformation=":" + $SitePort +":"} -physicalPath $SiteDirectoryPath
	$iisApp | Set-ItemProperty -Name "applicationPool" -Value $IisAppPoolName
	New-WebBinding -Name $SiteAppName -IP "*" -Port 4413 -Protocol https
	Write-Host "Successfully created bloggity site"
}

if (!(Test-Path $ApiAppName -pathType container))
{
	#create the site
	$iisApp = New-Item $ApiAppName -bindings @{protocol="http";bindingInformation=":" + $ApiPort +":"} -physicalPath $ApiDirectoryPath
	$iisApp | Set-ItemProperty -Name "applicationPool" -Value $IisAppPoolName
	New-WebBinding -Name $ApiAppName -IP "*" -Port 4414 -Protocol https
	Write-Host "Successfully created bloggity api site"
}

if (!(Test-Path $SocketsAppName -pathType container))
{
	#create the site
	$iisApp = New-Item $SocketsAppName -bindings @{protocol="http";bindingInformation=":" + $SocketsPort +":"} -physicalPath $SocketsDirectoryPath
	$iisApp | Set-ItemProperty -Name "applicationPool" -Value $IisSocketsAppPoolName
	New-WebBinding -Name $SocketsAppName -IP "*" -Port 4415 -Protocol https
	Write-Host "Successfully created bloggity node.js/socket.io site"
}

if (!(Test-Path $AdminAppName -pathType container))
{
	#create the site
	$iisApp = New-Item $AdminAppName -bindings @{protocol="http";bindingInformation=":" + $AdminPort +":"} -physicalPath $AdminDirectoryPath
	$iisApp | Set-ItemProperty -Name "applicationPool" -Value $IisAdminAppPoolName
	New-WebBinding -Name $AdminAppName -IP "*" -Port 4416 -Protocol https
	Write-Host "Successfully created bloggity admin site"
}

if (!(Test-Path $WcfBlogService -pathType container))
{
	#create the site
	$iisApp = New-Item $WcfBlogService -bindings @{protocol="http";bindingInformation=":" + $WcfPort +":"} -physicalPath $WcfBlogServiceDirectoryPath
	$iisApp | Set-ItemProperty -Name "applicationPool" -Value $IisWcfAppPoolName
	New-WebBinding -Name $WcfBlogService -IP "*" -Port 4417 -Protocol https
	Write-Host "Successfully created bloggity wcf application"
}