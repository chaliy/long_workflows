# NuGet SetApiKey Your-API-Key
NuGet pack .\src\LongWorkflows\LongWorkflows.csproj #-Symbols

gci *.nupkg | %{
	Write-Host Push $_
	NuGet push $_
	rm $_
}