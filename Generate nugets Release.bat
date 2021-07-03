@echo off

set outputDir="./../GM.NugetPackages/"
set buildConfig=Release

set list=GM.QueueService
 
for %%p in (%list%) do (

nuget pack %%p  -outputDirectory %outputDir%  -Properties Configuration=%buildConfig% -IncludeReferencedProjects
)

pause