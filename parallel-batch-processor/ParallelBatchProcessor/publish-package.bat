REM https://docs.microsoft.com/en-us/nuget/quickstart/create-and-publish-a-package-using-visual-studio-net-framework
REM nuget spec ParallelBatchProcessor.csproj
REM modify version in nuget.spec
dotnet pack --configuration Release
nuget push ./bin/Debug/ParallelBatchProcessor.2.0.2.nupkg [key] -Source https://api.nuget.org/v3/index.json