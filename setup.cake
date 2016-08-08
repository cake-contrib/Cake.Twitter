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
// PROJECT SPECIFIC VARIABLES
///////////////////////////////////////////////////////////////////////////////

var solutionFilePath          = "./Source/Cake.Twitter.sln";
var solutionDirectoryPath     = "./Source/Cake.Twitter";
var binDirectoryPath          = "./Source/Cake.Twitter/bin/" + configuration;
var title                     = "Cake.Twitter";
var resharperSettingsFileName = "Cake.Twitter.sln.DotSettings";
var description               = "Cake Addin that exends Cake with ability to post messages to Twitter";
var product                   = "Cake.Twitter";
var company                   = "gep13";
var copyright                 = string.Format("Copyright © gep13 and Contributors {0} - Present", DateTime.Now.Year);
var projectUrl                = new Uri("https://github.com/gep13/Cake.Twitter/");
var licenseUrl                = new Uri("https://github.com/gep13/Cake.Twitter/blob/master/LICENSE");
var releaseNotes              = new List<string>() { "https://github.com/gep13/Cake.Twitter/releases" };
var tags                      = new [] {"ReSharper", "DupFinder", "InspectCode", "Reports"};
var nugetFiles                = new [] {
                                          new NuSpecContent {Source = "Cake.Twitter.dll", Target = "tools"},
                                          new NuSpecContent {Source = "Cake.Twitter.pdb", Target = "tools"},
                                          new NuSpecContent {Source = "Cake.Twitter.xml", Target = "tools"}
                                      };

var semVersion                = "0.1.0";

#l .\Tools\gep13.DefaultBuild\Content\appveyor.cake
#l .\Tools\gep13.DefaultBuild\Content\testing.cake
#l .\Tools\gep13.DefaultBuild\Content\gitreleasemanager.cake
#l .\Tools\gep13.DefaultBuild\Content\gitter.cake
#l .\Tools\gep13.DefaultBuild\Content\gitversion.cake
#l .\Tools\gep13.DefaultBuild\Content\resharper.cake
#l .\Tools\gep13.DefaultBuild\Content\slack.cake
#l .\Tools\gep13.DefaultBuild\Content\parameters.cake
#l .\Tools\gep13.DefaultBuild\Content\build.cake
#l .\Tools\gep13.DefaultBuild\Content\paths.cake
#l .\Tools\gep13.DefaultBuild\Content\packages.cake