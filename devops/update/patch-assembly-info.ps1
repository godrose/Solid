$path = $args[0]
$version = $args[1]
$pattern = '\[assembly: AssemblyVersion\("(.*)"\)\]'
(Get-Content $path) | ForEach-Object{
    if($_ -match $pattern){                
        '[assembly: AssemblyVersion("{0}")]' -f $version
    } else {
        # Output line as is
        $_
    }
} | Set-Content $path
$pattern = '\[assembly: AssemblyFileVersion\("(.*)"\)\]'
(Get-Content $path) | ForEach-Object{
    if($_ -match $pattern){                
        '[assembly: AssemblyFileVersion("{0}")]' -f $version
    } else {
        # Output line as is
        $_
    }
} | Set-Content $path