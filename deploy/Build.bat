REM MSBuild EXE path
SET MSBuildPath="C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe"
SET NuGetPath="C:\Program Files (x86)\NuGet\nuget.exe"
set StagingPath=deploy\staging

REM change to the source root directory
pushd ..


REM ======================= clean =======================================

REM ensure any previously created package is deleted
del *.nupkg

REM ensure staging folder is empty
mkdir %StagingPath%
rmdir /s /q %StagingPath%
rmdir /s /q %StagingPath%

REM ======================= build =======================================

REM Build out staging NuGet folder structure
mkdir %StagingPath%
mkdir %StagingPath%\build\uap\native
REM mkdir %StagingPath%\lib\uap10.0
mkdir %StagingPath%\runtimes\win10-arm\native
mkdir %StagingPath%\runtimes\win10-x64\native
mkdir %StagingPath%\runtimes\win10-x86\native

REM Stage platform-independent files
copy deploy\nuget\libaxolotl.targets %StagingPath%\build\uap\native

REM build x86
%MSBuildPath% libaxolotl\libaxolotl.csproj /property:Configuration=Release /property:Platform=x86
REM stage x86
copy libaxolotl\bin\x86\Release\libaxolotl.dll %StagingPath%\runtimes\win10-x86\native
copy libaxolotl\bin\x86\Release\libaxolotl.pri %StagingPath%\runtimes\win10-x86\native

REM build x64
%MSBuildPath% libaxolotl\libaxolotl.csproj /property:Configuration=Release /property:Platform=x64
REM stage x64
copy libaxolotl\bin\x64\Release\libaxolotl.dll %StagingPath%\runtimes\win10-x64\native
copy libaxolotl\bin\x64\Release\libaxolotl.pri %StagingPath%\runtimes\win10-x64\native

REM build ARM
%MSBuildPath% libaxolotl\libaxolotl.csproj /property:Configuration=Release /property:Platform=ARM
REM stage ARM
copy libaxolotl\bin\ARM\Release\libaxolotl.dll %StagingPath%\runtimes\win10-arm\native
copy libaxolotl\bin\ARM\Release\libaxolotl.pri %StagingPath%\runtimes\win10-arm\native

REM create NuGet package
REM (Note: Expect 3 Assembly outside lib folder warnings, due to custom packaging technique)
pushd deploy\staging
%NuGetPath% pack ..\nuget\libaxolotl.nuspec -outputdirectory ..
popd


REM ============================ done ==================================


REM go back to the build dir
popd
