
ThemeMe v1.0
Copyright (c)2002, Mattias Sjogren
http://www.msjogren.net/dotnet/


Overview
--------

ThemeMe is a utility for embedding a manifest resource in an executable
file. It is typically used to enable Windows XP theming for managed
executables built with Visual Studio .NET, where you can't easily 
include custom old style Win32 resources.

The advantage of having the manifest embedded as a resource in the
executable, as opposed to a separate YourApp.exe.manifest file, is
that it can't easily be tampered with, and it's one less file to
keep track of when it's time to deploy.


Usage
-----

ThemeMe is best used as a post-build tool, that runs once after each
time the executable has been built. Visual C# .NET and Visual Basic .NET
projects don't natively support adding post-build actions. Instead, you
can add a Visual C++ Makefile project, and set up the project dependencies
so the makefile project runs last. Output from ThemeMe, including any
error messages, will be displayed in the Visual Studio Output window.
This is the approach used in the included sample solution.

Another possible solution is to download the PrePostBuildRules sample
add-in provided by Microsoft at
http://msdn.microsoft.com/vstudio/downloads/automation.asp


The command line for ThemeMe should be:

ThemeMe.exe ExecutableFile [ManifestFile] [NumericResourceId]

  ExecutableFile     Path and filename of the executable file to work with.
  ManifestFile       Path and filename of manifest file to embed in
                     ExecutableFile. If missing, all current manifest resources
                     in ExecutableFile will be listed.
  NumericResourceId  A number indicating the resource ID that should be used
                     for the manifest resource. The default is 1
                     (CREATEPROCESS_MANIFEST_RESOURCE_ID). Please refer to the
                     Platform SDK documentation for details on when it's
                     appropriate to use other resource IDs.


ThemeMe uses the resource modification APIs that are only available on
Windows NT based systems, which means ThemeMe will not work on Windows 9x.



History
-------

Version 1.0, 2002-07-16
  Initial release.



Terms of use
------------

This software is provided "as is", without any guarantee made
as to its suitability or fitness for any particular use. It may
contain bugs, so use of this tool is at your own risk. I take
no responsilbity for any damage that may unintentionally be caused
through its use.



Feedback
--------

Comments and suggestions are always appreciated. Please send any
feedback to mattias@mvps.org.

If you encounter any problems, please download the latest version
from http://www.msjogren.net/dotnet/ to see if the issue as been
resolved.

