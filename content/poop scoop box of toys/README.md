# poop scoop box of toys

`Z-52.json` is a typed MadCard deck containing ten Character cards and ten individual Chatter cards. The active destination is `%USERPROFILE%\AppData\LocalLow\Gibbing Tree\Madness Project Nexus\Mods\poop scoop box of toys`. Add the deck to that existing mod while preserving its `info.txt` and other decks. Studio's Downloads mod folder is no longer the deployment destination. The deck is intended for a compatible MadCard loader; it is not a NEM loose-character export.

The roster is Receiver, Enforcer, Breacher, Deadwire, Surveyor, Suppressor, Spark, Warden, Marshal and Director Blackout. The original MadCard stock weapon loadouts are retained. Latest saved names, stats, uniforms, VoiceSets and unique speech-bubble text are carried over from the archived story characters. NEM-only `OH_*` weapon dependencies are replaced by the original loadouts rather than left as missing references.

Most units use `N51_B`; Surveyor uses `MERC`, Suppressor uses `MERCBig`, Warden uses `Support`, and Blackout uses `N51_A`. Speech-bubble font aliases are translated to the actual `BlackOpsOne-Regular` font name. The faction label remains `Z-52`, using MCM's custom faction field. Empty array overrides use MadCard's `empty` sentinel.

This adds a roster for editing and spawning in the selected mod. `Foundry-Z52.json` also overrides the existing Arena mission **The Foundry** (`Stage_Arena / Arena_Battle1`, scene `Arena_Foundry`). It does not replace story encounters. Source/schema validation does not establish runtime compatibility with every game beta or loader.

## The Foundry

The mission keeps its identity, unlock prerequisites, scene, original music, hireling allowance, enemy caps, ten finite waves and reward values through its stock root. Its description and wave label now describe Z-52 containment trials. Enemy waves use ten weaker, Foundry-specific character cards (`Z52_Foundry_*`) included in `Foundry-Z52.json`. They keep the original Z-52 appearances, voices, weapons and custom chatter. The full-strength roster in `Z-52.json` is unchanged.

The Foundry profiles reduce inherited late-game combat skills, attributes, armour, health and TAC bars. All have one Basic corpus block. Explicit skills, traits and special attacks are cleared, then appropriate base/skilled/agent skill groups are assigned. Dual-wield perks are removed. Spark has no flashbangs in this mission. Both finite and endless sequences use these weaker profiles.

| Foundry unit | Level | Health | Head / body armour | TAC |
| --- | --- | --- | --- | --- |
| Receiver | 4 | 4 | 0 / 1 | 0 |
| Enforcer | 5 | 5 | 1 / 2 | 0 |
| Breacher | 6 | 8 | 2 / 3 | 0 |
| Deadwire | 6 | 5 | 0 / 1 | 0 |
| Surveyor | 8 | 5 | 0 / 1 | 0 |
| Spark | 5 | 4 | 0 / 1 | 0 |
| Warden | 9 | 12 | 2 / 3 | 0 |
| Marshal | 9 | 9 | 1 / 2 | 0 |
| Suppressor | 10 | 24 | 3 / 4 | 0 |
| Director Blackout | 12 | 30 | 3 / 4 | 0.5 |

Wave composition:

| Sequence | Waves | Enemies | Boss |
| --- | --- | --- | --- |
| Opening patrol | 3 | Receiver, Enforcer, Deadwire, Spark | None |
| Breach teams | 3 | Receiver, Enforcer, Breacher, Spark | Suppressor with an M60 |
| Command lockdown | 4 | Enforcer, Breacher, Deadwire, Surveyor, Spark, Warden, Marshal | Director Blackout with an M249 |
| Endless reinforcements | Repeating | Receiver, Enforcer, Breacher, Deadwire, Surveyor, Suppressor, Spark, Warden | Marshal and Director Blackout |

Finite sequences retain their original initial enemy budgets of 17, 25 and 35 units. Generation weights and growth distribute those budgets across the new roles; the original reward multipliers remain. Endless play starts with 36 regular units distributed across eight Z-52 roles and retains the original two-boss roster size. These are content choices, not a claim of play-tested difficulty.

Enable the mod in the game's mod list and start a fresh Foundry run after reloading mods or restarting the game. An already running arena may retain the data it loaded earlier. Remove only `Foundry-Z52.json` to return this mission to stock behavior while retaining the characters.

The native deck check recognized the combined 31-card payload (20 original roster/chatter cards, ten Foundry variants and the arena override) and resolved 234 references, including every wave character and boss weapon. Every spawn points to a Foundry variant. Wave totals, all ten Z-52 roles, initial finite enemy budgets and reward preservation were checked by the generator. A comparison against the installed data also confirmed lower health for every variant and preserved appearances, voices, chatter, primary weapons, generation weights and rewards. The game was not launched; difficulty still needs a real playthrough.

Validation: the native MadCard loader recognized all 20 cards; 99 explicit references resolved against the supplied database and this deck. IDs, roots and field names passed. The game was not launched.
