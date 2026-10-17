# poop scoop box of toys

`Z-52.json` is a typed MadCard deck containing ten Character cards and ten individual Chatter cards. The active destination is `%USERPROFILE%\AppData\LocalLow\Gibbing Tree\Madness Project Nexus\Mods\poop scoop box of toys`. Add the deck to that existing mod while preserving its `info.txt` and other decks. Studio's Downloads mod folder is no longer the deployment destination. The deck is intended for a compatible MadCard loader; it is not a NEM loose-character export.

The roster is Receiver, Enforcer, Breacher, Deadwire, Surveyor, Suppressor, Spark, Warden, Marshal and Director Blackout. The original MadCard stock weapon loadouts are retained. Latest saved names, stats, uniforms, VoiceSets and unique speech-bubble text are carried over from the archived story characters. NEM-only `OH_*` weapon dependencies are replaced by the original loadouts rather than left as missing references.

Most units use `N51_B`; Surveyor uses `MERC`, Suppressor uses `MERCBig`, Warden uses `Support`, and Blackout uses `N51_A`. Speech-bubble font aliases are translated to the actual `BlackOpsOne-Regular` font name. The faction label remains `Z-52`, using MCM's custom faction field. Empty array overrides use MadCard's `empty` sentinel.

This adds a roster for editing and spawning in the selected mod. It does not automatically replace story encounters. Source/schema validation does not establish runtime compatibility with every game beta or loader.

Validation: the native MadCard loader recognized all 20 cards; 99 explicit references resolved against the supplied database and this deck. IDs, roots and field names passed. The game was not launched.
