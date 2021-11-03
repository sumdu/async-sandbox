REM nuget spec ParallelBatchProcessor.csproj
REM modify version in nuget.spec
nuget pack
nuget push ParallelBatchProcessor.1.0.0.nupkg [key] -Source https://api.nuget.org/v3/index.json