@echo off

color 4
echo Вы уверены? / Are you sure?
Pause

cls

rem dp0 - текущая директория BAT файла
cd %~dp0

cd ..\ToStore


color 2
echo Current Path:
pwd
echo.  
echo Current Files and Folders in Path
dir

Echo.
Echo.
Echo Last barrier!
Echo Press Any Key to continue
Pause

cls

rem echo break - выводит ничего в файл, но при этом из-за > происходит перезапись
forfiles /M *.txt /S /C "cmd /c break> @path"