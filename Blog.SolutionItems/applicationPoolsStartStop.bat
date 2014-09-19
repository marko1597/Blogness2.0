IF "%1"=="start" GOTO :start
IF "%1"=="stop" GOTO :stop

:start
	c:\windows\system32\inetsrv\appcmd start apppool /apppool.name:bloggity
	c:\windows\system32\inetsrv\appcmd start apppool /apppool.name:blogsockets
	c:\windows\system32\inetsrv\appcmd start apppool /apppool.name:blogwcf
	c:\windows\system32\inetsrv\appcmd start apppool /apppool.name:blogadmin
GOTO end

:stop
	c:\windows\system32\inetsrv\appcmd stop apppool /apppool.name:bloggity
	c:\windows\system32\inetsrv\appcmd stop apppool /apppool.name:blogsockets
	c:\windows\system32\inetsrv\appcmd stop apppool /apppool.name:blogwcf
	c:\windows\system32\inetsrv\appcmd stop apppool /apppool.name:blogadmin
GOTO end

:end
	ECHO "DONE!!"