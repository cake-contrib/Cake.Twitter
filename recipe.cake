#load nuget:https://www.myget.org/F/cake-contrib/api/v2?package=Cake.Recipe&prerelease&version=0.3.0-unstable0403

Environment.SetVariableNames();

BuildParameters.SetParameters(context: Context,
                            buildSystem: BuildSystem,
                            sourceDirectoryPath: "./Source",
                            title: "Cake.Twitter",
                            repositoryOwner: "cake-contrib",
                            repositoryName: "Cake.Twitter",
                            appVeyorAccountName: "cakecontrib",
                            shouldRunGitVersion: true);

BuildParameters.PrintParameters(Context);

ToolSettings.SetToolSettings(context: Context,
                            dupFinderExcludePattern: new string[] {
                                BuildParameters.RootDirectoryPath + "/Source/Cake.Twitter.Tests/*.cs", BuildParameters.RootDirectoryPath + "/Source/Cake.Twitter/**/*.AssemblyInfo.cs" },
                            testCoverageFilter: "+[*]* -[xunit.*]* -[Cake.Core]* -[Cake.Testing]* -[*.Tests]* ",
                            testCoverageExcludeByAttribute: "*.ExcludeFromCodeCoverage*",
                            testCoverageExcludeByFile: "*/*Designer.cs;*/*.g.cs;*/*.g.i.cs");

Build.RunDotNetCore();
