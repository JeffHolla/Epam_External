@echo off

color 4
echo �� �������? / Are you sure?
Pause

cls

rem dp0 - ������� ���������� BAT �����
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

rem echo break - ������� ������ � ����, �� ��� ���� ��-�� > ���������� ����������
forfiles /M *.txt /S /C "cmd /c break> @path"