@echo off

rem dp0 - ������� ���������� BAT �����
cd %~dp0

cd ..\ToStore

set newLine=^&echo.

rem :~ - ���������� ���������, ���������� �� %time%, ������� � 0 �������, ������ 8 ��������, �.�. �� "21:52:39,68" ������ "21:52:39".
rem forfiles /M *.txt /S /C "cmd /c echo @file - %time:~0,8% >> @path"
forfiles /M *.txt /S /C "cmd /c echo @path - %time:~0,8% >> @path"