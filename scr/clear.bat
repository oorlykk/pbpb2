DEL /q *.vcxproj.user *.vcxproj.filters *.sdf *.VC.db *.lst *.obj *.pdb *.exe
@rem DEL /q *.dll
RD /S /Q obj .vscode .vs

cd bin\
DEL /q *.*
cd..