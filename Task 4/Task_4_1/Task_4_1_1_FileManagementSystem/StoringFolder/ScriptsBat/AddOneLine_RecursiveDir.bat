@echo off

rem dp0 - текущая директория BAT файла
cd %~dp0

cd ..\ToStore

set newLine=^&echo.

rem :~ - Возвращает подстроку, полученную из %time%, начиная с 0 символа, длиной 8 символов, т.е. из "21:52:39,68" вернет "21:52:39".
rem forfiles /M *.txt /S /C "cmd /c echo @file - %time:~0,8% >> @path"
forfiles /M *.txt /S /C "cmd /c echo @path - %time:~0,8% >> @path"