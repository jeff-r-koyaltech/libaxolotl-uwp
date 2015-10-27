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
mkdir %StagingPath%\lib\uap10.0


REM build Any CPU
%MSBuildPath% libaxolotl\libaxolotl.csproj /property:Configuration=Release /property:Platform="AnyCPU"
REM stage Any CPU (dll, pri, xml)
copy libaxolotl\bin\Release\libaxolotl.* %StagingPath%\lib\uap10.0

REM create NuGet package
pushd deploy\staging
%NuGetPath% pack ..\nuget\libaxolotl-uwp.nuspec -outputdirectory ..
popd


REM ============================ done ==================================


REM go back to the build dir
popd
