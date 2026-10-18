# poop scoop box of toys

`Z-52.json` is a typed MadCard deck containing ten Character cards and ten individual Chatter cards. The active destination is `%USERPROFILE%\AppData\LocalLow\Gibbing Tree\Madness Project Nexus\Mods\poop scoop box of toys`. Add the deck to that existing mod while preserving its `info.txt` and other decks. Studio's Downloads mod folder is no longer the deployment destination. The deck is intended for a compatible MadCard loader; it is not a NEM loose-character export.

The roster is Receiver, Enforcer, Breacher, Deadwire, Surveyor, Suppressor, Spark, Warden, Marshal and Director Blackout. The original MadCard stock weapon loadouts are retained. Latest saved names, stats, uniforms, VoiceSets and unique speech-bubble text are carried over from the archived story characters. NEM-only `OH_*` weapon dependencies are replaced by the original loadouts rather than left as missing references.

Most units use `N51_B`; Surveyor uses `MERC`, Suppressor uses `MERCBig`, Warden uses `Support`, and Blackout uses `N51_A`. Speech-bubble font aliases are translated to the actual `BlackOpsOne-Regular` font name. The faction label remains `Z-52`, using MCM's custom faction field. Empty array overrides use MadCard's `empty` sentinel.

This adds a roster for editing and spawning in the selected mod. `Foundry-Z52.json` also overrides the existing Arena mission **The Foundry** (`Stage_Arena / Arena_Battle1`, scene `Arena_Foundry`). It does not replace story encounters. Source/schema validation does not establish runtime compatibility with every game beta or loader.

## The Foundry

The mission keeps its identity, unlock prerequisites, scene, original music, hireling allowance, enemy caps, ten finite waves and reward values through its stock root. Its description and wave label now describe Z-52 containment trials. Enemy waves use the existing Z-52 roster and its custom chatter:

| Sequence | Waves | Enemies | Boss |
| --- | --- | --- | --- |
| Opening patrol | 3 | Receiver, Enforcer, Deadwire, Spark | None |
| Breach teams | 3 | Receiver, Enforcer, Breacher, Spark | Suppressor with an M60 |
| Command lockdown | 4 | Enforcer, Breacher, Deadwire, Surveyor, Spark, Warden, Marshal | Director Blackout with an M249 |
| Endless reinforcements | Repeating | Receiver, Enforcer, Breacher, Deadwire, Surveyor, Suppressor, Spark, Warden | Marshal and Director Blackout |

Finite sequences retain their original initial enemy budgets of 17, 25 and 35 units. Generation weights and growth distribute those budgets across the new roles; the original reward multipliers remain. Endless play starts with 36 regular units distributed across eight Z-52 roles and retains the original two-boss roster size. These are content choices, not a claim of play-tested difficulty.

Enable the mod in the game's mod list and start a fresh Foundry run after reloading mods or restarting the game. An already running arena may retain the data it loaded earlier. Remove only `Foundry-Z52.json` to return this mission to stock behavior while retaining the characters.

The native deck check recognized the combined 21-card roster/arena payload and resolved 131 references, including every wave character and boss weapon. Wave totals, all ten Z-52 roles, initial finite enemy budgets and reward preservation were checked by the generator. The game was not launched.

Validation: the native MadCard loader recognized all 20 cards; 99 explicit references resolved against the supplied database and this deck. IDs, roots and field names passed. The game was not launched.
