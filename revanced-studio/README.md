# REVANCED Studio

REVANCED Studio is the standalone **MadCard Maker REVANCED** application for editing typed MadCard decks. Start `REVANCED Studio.exe`; it opens directly into the MCM workspace. Dossier is no longer embedded or loaded by Studio and remains a separate application.

## Studio Music and interface update

Click **Music** in the Studio header for a persistent local playlist and live 48-band audio spectrum. Add files through the built-in explorer or drag audio files onto the player. Double-click a track or press Play. Previous, Next, Pause/Resume, Stop, seeking, independent volume, shuffle and repeat (Off / Playlist / Track) are included. Closing the player hides it and keeps playback running; closing Studio releases the audio device. The playlist and preferences are stored in `%LOCALAPPDATA%/REVANCEDStudio/music.json`. Nothing plays automatically on startup. Missing or unsupported files report an error. WAV, MP3 and AIFF are supported; M4A, AAC and WMA depend on available Windows decoders. The visualizer reads the playing samples and suspends drawing/analysis while hidden.

**Appearance → Text and control size** offers Compact (100%), Comfortable (115%, default) and Large (130%). Reopen Studio to apply the setting. Shared buttons, tabs, lists, input surfaces, dropdown chrome and window corners use squircle styling. Native keyboard navigation, focus and scrolling remain available.

In MCM, select an editable deck and click **Assisted character**. Choose a role and an appearance from the loaded database, enter a name and ID, then adjust health, level, body scale, TAC bar, speech bubbles and description. The draft preview updates as you edit. **Open in full editor** opens a draft for further editing; save it to your deck when ready. There are six starting roles and all loaded character roots are available as appearances. This uses the existing assets and inheritance format.

## Workspace navigation

Studio opens directly into MadCard Maker. Editors open in document tabs inside Studio. **Close tab** or **Ctrl+W** closes the current editor; its existing save/discard/cancel behavior still applies. Closing Studio closes editor documents before the workspace home so unsaved work can be handled first.

The window starts maximized and remembers its normal size and maximized state. The MCM sidebar has a dedicated layout column, preventing it from covering toolbars, headings and the card tree. Appearance controls provide the bundled Play font, alternative installed fonts, red/graphite/olive palettes, quiet click and typing sounds, volume and animation preferences. Fonts and colors apply after restarting Studio. Sound preferences are synchronized when switching workspaces.

Studio enables Per Monitor V2 high-DPI rendering. Windows renders text and controls at the display's scaling level, and the editor refreshes its control metrics when moving between displays with different DPI settings. Restart Studio after installing this update to enable the new process setting.

Scrollbars use dark tracks and rounded accent-colored thumbs across text editors, lists, trees, dropdown lists and file browsers in both workspaces. The styling retains Windows' scrolling, thumb dragging, arrows, mouse wheel and keyboard behavior. Windows high-contrast mode retains system scrollbar rendering.

## MadCard Maker

The MCM workspace includes:

- Mod/deck organization, inherited roots and reference suggestions.
- Twelve editable starter presets for characters, firearms, melee weapons and chatter.
- Independent card duplication with **Ctrl+D**.
- Card/deck checks, including IDs, roots, common health-track problems and loaded references.
- Read-only **Overrides**, **Resolved**, **Deck JSON** and **Changes** views.
- Single-card JSON import and typed MadCard deck export.
- Undo/redo, field search and navigation between changed fields.
- A per-project notebook with Idea, To do, In progress and Done statuses.
- Atomic file writes with `.bak` copies on replacement.
- **Tools → Bulk edit cards**: select a card category, scalar field and cards; review every change before applying. Field types and loaded references are checked. Close card tabs first to protect their drafts. **Undo last bulk edit** restores the batch unless its cards have since changed.
- **Tools → Find references**: see which loaded mod cards explicitly use the selected card, including inherited roots and nested typed references. Deck checks flag missing loaded references without treating dialogue text as an asset ID.
- **Review → Save as reusable template**: preserve a card's overrides in a personal library. **Tools → Browse saved templates** opens a new editable draft with a new ID.
- **Tools → Export saved mod as ZIP**: packages saved metadata, decks and assets with their relative paths, excludes backup/temporary files, and keeps a backup when replacing an archive. Save deck changes first; unsaved drafts are not included.
- **Tools → Reference library**: search original card IDs, names and descriptions by category, preview their JSON and create an inherited variant in the selected mod/deck. Reference cards are never modified by browsing or creating a draft.
- **Review → Chatter writer**: edit speech bubbles by event with one bubble per line and live line counts. Only changed events apply to the draft; blanking an event clears its inherited lines. Other fields such as the font and color are preserved. Apply supports the card editor's undo/redo; Cancel discards the writer's edits. The exact line `empty` is reserved by the game deck format and cannot be used as dialogue.
- **Review → Compare with inherited card**: compare the current resolved values against the inherited root. The report lists changed fields, including arrays and nested values, without treating Filename/Root/Priority metadata as gameplay differences.
- **Game settings / Launch game** in the MCM sidebar: select an executable or game folder, check it, and save optional launch arguments. The launcher uses the executable's own folder as its working directory and returns control to the editor immediately. If a folder contains multiple possible games, choose the EXE directly. Saving a path does not launch the game. This feature does not deploy mods, install loaders or switch the game version.
- **Reference library favorites**: add/remove favorite cards and filter to favorites within each category. Favorites persist outside the game database and leave the reference cards unchanged.
- **Save report**: validation, dependency and inherited-card comparison reports can be copied or saved as text, with backups on replacement.
- **Review → Copy selected field / Paste copied fields**: copy the whole selected field (including a list or compound field) between cards of the same type. Paste validates the field types and references before applying, preserves card IDs/roots, and supports undo/redo. The clipboard contains an MCM field envelope, separate from game-deck JSON.
- **Review → Reset all overrides**: return all fields to the inherited card while keeping the current card ID/root. Confirmation is required; Undo restores the previous draft.
- **Tools → Manage templates**: rename/remove saved templates, export an individual template, and import a template collection. Import checks the current database and gives duplicate template names a numbered suffix. Library writes retain a backup.

Imported overrides retain their edited values when the inherited-root selector changes during loading. Empty-array deck markers are encoded structurally, so literal `[]` text inside dialogue is preserved.

## Formats and game versions

MCM uses typed MadCard decks with `Filename` and `Root` fields. Its reference database lives under `Data/MadCard`. Exports belong inside a valid MadCard mod folder with its mod metadata and are not compatible with every MPN version or loader.

Keep the **Data** folder and component DLLs beside Studio. Reference snapshots include the original supplied databases; no external assets or game assemblies are downloaded.

## Notes and settings

Studio's theme and window preferences are stored under `%LOCALAPPDATA%/REVANCEDStudio`. MCM notebook data stays outside game decks. Saving an export does not automatically deploy it to a game folder.

The game path and optional arguments are stored in `%LOCALAPPDATA%/MadCardMakerRevanced/game.json`; reference favorites are stored in `favorites.json` in that same folder.

## Attribution

Original MadCard Maker code, icons, documentation, reference data and embedded attribution remain credited to their original creators. REVANCED Studio is an unofficial interface rewrite; it does not claim original authorship of MadCard Maker or MPN assets.

Zayden — project direction, feature requests, worldbuilding and hands-on testing. DC and Ghosted — acknowledged collaborators in the wider project; no additional authorship is invented here.

## Verification

The MCM checks cover preset roots/fields, independent duplication, non-destructive deck encoding, valid IDs, backup writes, imported drafts, undo/redo and review tabs. Studio checks load the MadCard reference database, exercise workspace/document persistence and tab closing, and verify the Dossier assembly is not referenced or packaged. These tests do not establish in-game compatibility or replace testing exports in the selected MPN/MadCard loader version.
