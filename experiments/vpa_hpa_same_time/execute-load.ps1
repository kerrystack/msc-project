﻿write-host "1. Moved control to correct folder"
cd C:\D\msc_project\msc-project\experiments\vpa_hpa_same_time

write-host "2. Executing load"
C:\d\utilities\k6\k6.exe run .\load_script.js

write-host "7.  Finished executing load"