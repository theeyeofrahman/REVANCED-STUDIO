# REVANCED Studio

Windows modding workspace with MadCard Maker REVANCED and Dossier Legacy in one application.

See [the user guide](revanced-studio/README.md) for editing tools, game settings and workspace shortcuts.

Build on Windows with the .NET 10 SDK and .NET Framework 4.8 targeting pack:

```powershell
dotnet build revanced-studio/RevancedStudio.csproj -c Release /m:1 /nodeReuse:false /p:UseSharedCompilation=false
```

The original reference databases are supplied separately. A distribution places them under `Data/MadCard/db` and `Data/Dossier/db` beside `REVANCED Studio.exe`. Personal projects and game data are excluded from this repository.

The original MadCard Maker and Dossier credits remain in their source and interface. Bundled dependencies retain their respective licenses; Play font licensing is in `madcard-revanced/src/Fonts/OFL.txt`.
Use `revanced-studio/Build.ps1` to create a package, optionally passing `-ReferenceData` with the path to an existing Studio `Data` folder. The package includes a local music player with an audio spectrum visualizer, guided MCM character drafts, adjustable interface sizing and squircle controls.
