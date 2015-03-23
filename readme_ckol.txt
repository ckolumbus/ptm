Changelog
=========
Changes after PTM 1.4.5

ckol.7b
  - NEW: materialized path implemented
  - NEW: Account entry added to task properties
  - NEW/IMPROVE: data base version pushed to version 1.0.3
    - Tasks.AccountID : possibility to add a specific account id to a task
    - Tasks.MatPath   : materialized path to speed up hierarchical queries

ckol.6b
  - NEW: option to disable popup notifications
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
