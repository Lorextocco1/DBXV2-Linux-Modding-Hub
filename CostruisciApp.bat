@echo off
cd /d "%~dp0"
echo Costruzione del Launcher DBXV2 in corso...

set "COMPILER=C:\Windows\Microsoft.NET\Framework\v4.0.30319\csc.exe"

if not exist "%COMPILER%" (
    echo ERRORE: Compilatore non trovato! Assicurati di usare Proton Experimental.
    pause
    exit
)

"%COMPILER%" /target:winexe /out:DBXV2Launcher.exe /r:System.Windows.Forms.dll /r:System.Drawing.dll LauncherCode.cs

if exist "DBXV2Launcher.exe" (
    echo.
    echo SUCCESSO! DBXV2Launcher.exe creato.
) else (
    echo ERRORE: Compilazione fallita.
)
pause
