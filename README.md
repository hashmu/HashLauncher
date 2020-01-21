# HashLauncher
A simple application launcher for the task-bar.

Items add to "apps.txt" will be available to launch from the system tray icon.
The syntax for "apps.txt" is:

Label,Path[,Arguments]

For example:

  Drive,explorer.exe,C:\Program Files

Will create an option labelled "Drive" that opens explorer at the path "C:\Program Files"
