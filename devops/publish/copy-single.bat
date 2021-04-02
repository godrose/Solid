SET package_name=%1
SET package_version=%2
SET target=%3
robocopy ../../output/ %target% %package_name%.%package_version%.nupkg /E