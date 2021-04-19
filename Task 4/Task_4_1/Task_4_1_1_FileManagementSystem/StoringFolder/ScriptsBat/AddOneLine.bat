@echo off

rem dp0 - текущая директория BAT файла
cd %~dp0

cd ..\ToStore

for %%B in (*.txt) do (echo %%B >> %%B)
