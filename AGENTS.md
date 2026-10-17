# REVANCED Studio project workflow

- Commit and push changes to `theeyeofrahman/REVANCED-STUDIO`, as requested by the user.
- Deploy future game content for **poop scoop box of toys** to `%USERPROFILE%\AppData\LocalLow\Gibbing Tree\Madness Project Nexus\Mods\poop scoop box of toys`. The user's current profile is `C:\Users\zayde`.
- Keep the maintained Z-52 deck in `content/poop scoop box of toys` as a source copy. The game Mods folder is the active destination; Studio's Downloads mod folder is no longer the deployment destination.
- Preserve existing user-authored decks and mod metadata when adding content. Check for conflicting card IDs before replacing a deck.
- Do not launch or drive the desktop app or game unless the user asks. Run builds and checks sequentially with bounded resources, following the user's previous freeze report.
