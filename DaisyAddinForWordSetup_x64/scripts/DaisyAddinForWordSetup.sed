[Version]
Class=IEXPRESS
SEDVersion=3
[Options]
PackagePurpose=InstallApp
ShowInstallProgramWindow=1
HideExtractAnimation=1
UseLongFileName=1
InsideCompressed=0
CAB_FixedSize=0
CAB_ResvCodeSigning=0
RebootMode=N
InstallPrompt=%InstallPrompt%
DisplayLicense=%DisplayLicense%
FinishMessage=%FinishMessage%
TargetName=%TargetName%
FriendlyName=%FriendlyName%
AppLaunched=%AppLaunched%
PostInstallCmd=%PostInstallCmd%
AdminQuietInstCmd=%AdminQuietInstCmd%
UserQuietInstCmd=%UserQuietInstCmd%
SourceFiles=SourceFiles
[Strings]
InstallPrompt=
DisplayLicense=
FinishMessage=
TargetName=DaisyAddinForWordSetup_x64.exe
FriendlyName=Daisy Add-In for Microsoft Word
AppLaunched=CMD /C SetupPrepare.bat
PostInstallCmd=CMD /C ECHO FINISHED
AdminQuietInstCmd=
UserQuietInstCmd=
FILE0="setup.exe"
FILE1="DaisyAddinForWordSetup_x64.msi"
FILE2="SetupPrepare.bat"
FILE3="EnableDotNet3.exe"
FILE4="extensibilityMSM.msi"
FILE5="lockbackRegKey.msi"
FILE6="office2003-kb907417sfxcab-ENU.exe"
FILE7="jre-6u10-windows-i586-p-iftw.exe"
FILE8="o2003pia.msi"
FILE9="o2007pia.msi"
FILE10="FileFormatConverters.exe"
[SourceFiles]
SourceFiles0=
SourceFiles1=..\..\scripts\
SourceFiles2=DotNetFX30\
SourceFiles3=KB908002\
SourceFiles4=JAVARUNTIME\
SourceFiles5=PIA2003\
SourceFiles6=PIA2007\
SourceFiles7=CompatibilityPack\
[SourceFiles0]
%FILE0%=
%FILE1%=
[SourceFiles1]
%FILE2%=
[SourceFiles2]
%FILE3%=
[SourceFiles3]
%FILE4%=
%FILE5%=
%FILE6%=
[SourceFiles4]
%FILE7%=
[SourceFiles5]
%FILE8%=
[SourceFiles6]
%FILE9%=
[SourceFiles7]
%FILE10%=