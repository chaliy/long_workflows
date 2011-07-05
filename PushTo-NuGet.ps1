# NuGet SetApiKey Your-API-Key
NuGet pack .\src\Lrw\Lrw.csproj #-Symbols

gci *.nupkg | %{
	Write-Host Push $_
	NuGet push $_
	rm $_
}