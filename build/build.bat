@echo off
cls
tools\nant\bin\NAnt.exe -buildfile:bdddoc.build %*
