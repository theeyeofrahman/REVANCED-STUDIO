# REVANCED Studio

Standalone Windows MadCard deck editor: MadCard Maker REVANCED.

See [the user guide](revanced-studio/README.md) for editing tools, game settings and workspace shortcuts.

Build on Windows with the .NET 10 SDK and .NET Framework 4.8 targeting pack:

```powershell
dotnet build revanced-studio/RevancedStudio.csproj -c Release /m:1 /nodeReuse:false /p:UseSharedCompilation=false
```

The original MadCard reference database is supplied separately under `Data/MadCard/db` beside `REVANCED Studio.exe`. Dossier remains separate and is not part of Studio. Personal projects and game data are excluded from this repository.

The original MadCard Maker credits remain in its source and interface. Bundled dependencies retain their respective licenses; Play font licensing is in `madcard-revanced/src/Fonts/OFL.txt`.

The separate Dossier app's Idea Gallery provides a searchable Kanban board with task checklists, tags, priorities, optional due dates, overdue / due-soon totals, sorting and priority filters, quick-start templates, card duplication, archiving and JSON export. Existing saved boards remain compatible; due dates are optional.
Use `revanced-studio/Build.ps1` to create a package, optionally passing `-ReferenceData` with the path to an existing Studio `Data` folder. The package includes a local music player with an audio spectrum visualizer, guided MCM character drafts, adjustable interface sizing and squircle controls.
