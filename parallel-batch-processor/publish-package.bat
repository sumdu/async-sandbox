REM nuget spec ParallelBatchProcessor.csproj
REM modify version in .csproj

dotnet pack --configuration Release
nuget push ./bin/Debug/ParallelBatchProcessor.2.0.2.nupkg [key] -Source https://api.nuget.org/v3/index.json