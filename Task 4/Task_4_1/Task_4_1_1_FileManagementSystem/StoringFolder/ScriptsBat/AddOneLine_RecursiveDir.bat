@echo off

rem dp0 - ������� ���������� BAT �����
cd %~dp0

cd ..\ToStore

rem :~ - ���������� ���������, ���������� �� %time%, ������� � 0 �������, ������ 8 ��������, �.�. �� "21:52:39,68" ������ "21:52:39".
forfiles /M *.txt /S /C "cmd /c echo @file - %time:~0,8% >> @path"