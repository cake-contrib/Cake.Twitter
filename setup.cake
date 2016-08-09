///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////

var target          = Argument<string>("target", "Default");
var configuration   = Argument<string>("configuration", "Release");

///////////////////////////////////////////////////////////////////////////////
// ENVIRONMENT VARIABLE NAMES
///////////////////////////////////////////////////////////////////////////////

private static string githubUserNameVariable = "CAKE_GITHUB_USERNAME";
private static string githubPasswordVariable = "CAKE_GITHUB_PASSWORD";

///////////////////////////////////////////////////////////////////////////////
// BUILD ACTIONS
///////////////////////////////////////////////////////////////////////////////

var sendMessageToGitterRoom = true;
var sendMessageToSlackChannel = true;
var sendMessageToTwitter = true;

///////////////////////////////////////////////////////////////////////////////
// PROJECT SPECIFIC VARIABLES
///////////////////////////////////////////////////////////////////////////////

var solutionFilePath          = "./Source/Cake.Twitter.sln";
var solutionDirectoryPath     = "./Source/Cake.Twitter";
var binDirectoryPath          = "./Source/Cake.Twitter/bin/" + configuration;
var title                     = "Cake.Twitter";
var resharperSettingsFileName = "Cake.Twitter.sln.DotSettings";

///////////////////////////////////////////////////////////////////////////////
// CAKE FILES TO LOAD IN
///////////////////////////////////////////////////////////////////////////////

#l .\Tools\gep13.DefaultBuild\Content\appveyor.cake
#l .\Tools\gep13.DefaultBuild\Content\chocolatey.cake
#l .\Tools\gep13.DefaultBuild\Content\gitreleasemanager.cake
#l .\Tools\gep13.DefaultBuild\Content\gitter.cake
#l .\Tools\gep13.DefaultBuild\Content\gitversion.cake
#l .\Tools\gep13.DefaultBuild\Content\nuget.cake
#l .\Tools\gep13.DefaultBuild\Content\packages.cake
#l .\Tools\gep13.DefaultBuild\Content\parameters.cake
#l .\Tools\gep13.DefaultBuild\Content\paths.cake
#l .\Tools\gep13.DefaultBuild\Content\publish.cake
#l .\Tools\gep13.DefaultBuild\Content\resharper.cake
#l .\Tools\gep13.DefaultBuild\Content\slack.cake
#l .\Tools\gep13.DefaultBuild\Content\testing.cake
#l .\Tools\gep13.DefaultBuild\Content\twitter.cake
#l .\Tools\gep13.DefaultBuild\Content\build.cake