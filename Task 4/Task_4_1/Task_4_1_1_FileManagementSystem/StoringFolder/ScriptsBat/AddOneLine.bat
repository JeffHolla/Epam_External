@echo off

rem dp0 - ������� ���������� BAT �����
cd %~dp0

cd ..\ToStore

for %%B in (*.txt) do (echo %%B >> %%B)
