@echo off
set currDir = %CD%
RUNAS \administrator cmd "%currDir%\RegisterSybase.cmd"
