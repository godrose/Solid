cd ../../output
nuget push %1.%2.nupkg -Source https://api.nuget.org/v3/index.json
cd ../devops/deploy