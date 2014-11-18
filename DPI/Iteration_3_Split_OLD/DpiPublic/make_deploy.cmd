md Deploy
md Deploy\Dpi.Central.Web
xcopy Dpi.Central.Web Deploy\Dpi.Central.Web /S /Y
cd Deploy\Dpi.Central.Web\bin
del *.pdb
cd ..
del Controls /Q
del db /Q
del Docs /Q
rd Controls /Q
rd db /Q
rd Docs /Q
del *.cs
del *.resx
del *.csproj
del *.webinfo

cd ..\..\

md Deploy\Dpi.Central.Web.Account 
xcopy Dpi.Central.Web.Account Deploy\Dpi.Central.Web.Account /S /Y
cd Deploy\Dpi.Central.Web.Account\bin
del *.pdb
cd ..
del Controls /Q
rd Controls /Q
del *.cs
del *.resx
del *.csproj
del *.webinfo

cd account
del *.cs
del *.resx

cd payment
del *.cs
del *.resx

del _sgbak /Q
rd _sgbak /Q

