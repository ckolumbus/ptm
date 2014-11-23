Changelog
=========
Changes after PTM 1.4.5

ckol.6b
  - NEW: AddIns can acces TaskSelector from now
  - IMPROVE: new GetNewCommandParam that allows to create OleDbCommands with
    parameters

ckol.5
  - DEV: added NLog support (via NuGet)
  - BUGFIX: fixed suspend behavior
  - BGUFIX: update display after suspend
  - IMPROVE: increased max DB maintenance from 30 to 60 days

ckol.4
  - replace "% Active" display on Summary page with total "Active Time"

ckol.3
  - added system suspend/resume handling. Idle times added correctly after 
    resume.
  
ckol.2
  - added enable/disable "AppsLog" loading in "TasksL Log" Tab. Significant
    speedup during load achieved.
