# Grimora Mod

- A giant Mod made by xXxStoner420BongMasterxXx and Arackulele, that builds upon the finale with Grimora's chessboard.

- **MOST IMPORTANT**: **!!!You will probably encounter some Bugs!!!**
- **BEWARE**:

  - THIS WILL UNLOCK CERTAIN STORY EVENTS IN ORDER FOR THE MOD TO WORK, WHICH ALSO MEANS UNLOCKING CERTAIN ACHIEVEMENTS!
  - In the event you are starting from a brand new save, this mod will unlock all learned abilities, mechanics, and cards
    and some Story Events like the first tutorial battle, Bones, and a few others in order for this mod to work.
  - I suggest restarting your game at least once if you haven't actually gotten to the finale.

- If you want to discuss the mod further, join our Discord server! <https://discord.gg/Xf8CBuS8a8>

## Reporting Issues

- If you would to help report issues, please raise a thread here with as much detail as you can provide: <https://github.com/Arackulele/GrimoraMod/issues>
- Bug reports can also be submitted on the Discord Server: <https://discord.gg/Xf8CBuS8a8>

## Known Issues

### Activated abilities no longer work

- The primary cause of this is whenever another ability gets added to the card. The bug lies in the `Activated Ability Fix` mod, but I'm not sure where.

### A card with Area of Effect Strike, Tri Strike, and Sniper sigils only allows 3 attacks

- Believe it or not, this is how the vanilla game code for the `Sniper` ability is handled. It doesn't base it off how many attacks you're doing, it hard codes to either 2 for `Split Strike` or 3 for `Tri Strike`.

### Current save file is already at the finale with Grimora

- Make a backup of your save, then delete your current save. Having your current save already at the finale seems to break the mod.
- Possibly fixed in 2.6.4 update.

## Update Notes

### 2.8.3

- ![Feature](https://i.imgur.com/uL9uYNV.png) Added config option to randomize enemy encounters! Disabled by default.

- ![Feature](https://i.imgur.com/uL9uYNV.png) Now able to view your deck and get up from the table using W and S (or respective joysticks)! No more buttons!

  - **This does however lock the player chess piece movement to the arrow keys. Joystick will no longer work moving the player piece.**

- ![Feature](https://i.imgur.com/uL9uYNV.png) Cards with the `Sentry` ability now has a custom attack animation with the awesome work courtesy of
  `JestAnotherAnimator#9226`!

  - The twin giants also have custom attacks now as well!

- ![Refactor](https://i.imgur.com/5bTRm1B.png) Removed Part 1 metacategories for rulebook abilities so they don't show up in Act 1.

#### Card Changes

- ![Feature](https://i.imgur.com/uL9uYNV.png)

  - Honestly, too many added that I don't feel like typing out except for the specifics.

- ![Feature](https://i.imgur.com/uL9uYNV.png) `Yellowbeard` now becomes `Bluebeard` if electric chaired.

- ![Refactor](https://i.imgur.com/5bTRm1B.png) `Starvation` art update by `Nevernamed`.
- ![Refactor](https://i.imgur.com/5bTRm1B.png) `The Walkers` art update by `Catboy Stinkbug`.
- ![Refactor](https://i.imgur.com/5bTRm1B.png) `Amoeba` replaced by `Apparition`. Normal 1/2, 4 Energy with Amorpheous (sigils from the electric chair).
- ![Refactor](https://i.imgur.com/5bTRm1B.png) `Corpse Maggots` replaced by `Wight`. Normal 2/1, 5 Bones with `Corpse Eater`.
- ![Refactor](https://i.imgur.com/5bTRm1B.png) `Project` now just has `Chaos Strike` ability.

##### Kaycee

- ![Feature](https://i.imgur.com/uL9uYNV.png)
  - Ice Cube: Normal, 1/1, 5 Bones with `Cold Front`.
  - Glacier: Normal, 0,4, 10 Bones with `Ice Cube`.
  - Frost Giant: Unobtainable, 2,4, 10 Bones with `Split Strike`.

##### Sawyer

- ![Feature](https://i.imgur.com/uL9uYNV.png) `Kennel` terrain card.

##### Royal

- ![Feature](https://i.imgur.com/uL9uYNV.png) `Shipreck` terrain card.

#### Ability Changes

- ![Bugfix](https://i.imgur.com/CYIMfjn.png) Fixed `Area of Effect Strike` attacking the opposing slot first, then the rest.

- ![Feature](https://i.imgur.com/uL9uYNV.png) `Skin Crawler` ability reworked again to hopefully be a lot more stable, added back to the pool!

- ![Feature](https://i.imgur.com/uL9uYNV.png)

  - Boneless: [creature] yields no bones upon death.
  - Bone Thief: When [creature] kills another card, gain 1 bone.
  - Chaos Strike: [creature] will strike each opposing space to the left, right, and center of it, randomly.
  - Cold Front: When [creature] perishes, the card opposing it is Frozen Away.
  - Haunter: When [creature] perishes, it haunts the space it died in. The first creature played on this space gains its old sigils.
    - Original code logic from `Never`, refactored and cleaned up by `BongMaster`.
  - Imbued: When a non-brittle ally card perishes, [creature] gains 1 power.
  - Loose Limb: Thematic version of `Loose Tail`.
  - Marching Dead: When [creature] is played, also play the cards in your hand that were adjacent to this card for free.
  - Puppeteer: Cards on the owner's side of the field are unaffected by Brittle.
  - Sea Legs: Renamed to `Anchored` with art courtesy of `Blind, the Bound Demon`.
  - Warren (Draw Rabbits): Received card is now `Spectrabbit`.

- ![Refactor](https://i.imgur.com/5bTRm1B.png) `Inverted Strike` now has logic to handle TriStrike.

### 2.8.2 KAYCEES MOD SUPPORT

**ALL CARDS HAVE NEW INTERNAL NAMES! YOU WILL NEED TO RESET YOUR SAVE DATA MOST LIKELY!**

- ![Bugfix](https://i.imgur.com/CYIMfjn.png) Fixed still being able to click the hammer and hear the sounds even though it is invisible.

- ![Bugfix](https://i.imgur.com/CYIMfjn.png) Fixed being able to get up during special card sequences like the Electric Chair.

- ![Bugfix](https://i.imgur.com/CYIMfjn.png) Fixed tombstones not playing the falling animation sometimes.

- ![Feature](https://i.imgur.com/uL9uYNV.png) Stat icons are now right-clickable and viewable in the rulebook!

- ![Refactor](https://i.imgur.com/5bTRm1B.png) Asset bundles are now loaded asynchronously. This just means it should get to the main menu faster.

- ![Refactor](https://i.imgur.com/5bTRm1B.png) Added specific patch for getting adjacent slots for Giant cards to get the correct adjacent slots.

- ![Refactor](https://i.imgur.com/5bTRm1B.png) Randomization for card choices and electric chair should be a bit more random now.

#### Bosses

- ![Bugfix](https://i.imgur.com/CYIMfjn.png) Fixed Royal's skull not showing up in Endless mode.

#### Ability/Card Changes

- ![Bugfix](https://i.imgur.com/CYIMfjn.png) Fixed potential softlock if a card with `Hook Line and Sinker` kills a `Leaping Trap`.

- ![Refactor](https://i.imgur.com/5bTRm1B.png) Reworded `Spirit Bearer` description to now say `energy soul` instead of `energy cell` to fit more thematically.

### 2.8.1

- ![Bugfix](https://i.imgur.com/CYIMfjn.png) Corrected issue with rulebook not showing all the abilities.

### 2.8.0

- ![Bugfix](https://i.imgur.com/CYIMfjn.png) Fixed teeth not disappearing from cards that attack their owners.

  - For example, AOE Strike and now Raider. The reason being is that teeth only get cleared if `DamageDealtThisPhase` is greater than zero. There is logic for the `CombatPhaseManager.SlotAttackSequence` patch that I made to minus the damage done to this field so that it would correctly add the damage to the scale for the respective owner.
  - Now there is logic in that patch right after subtracting the damage to call `CombatPhaseManager3D.VisualizeDamageMovingToScales` and clear `damageWeights` so that it will correctly remove the teeth from the board.

- ![Bugfix](https://i.imgur.com/CYIMfjn.png) FIXED ANNOYING BLUE LIGHT ON BOSS SKULL AFTER ROYAL'S FIGHT.

  - Apparently when the cannons are created, there is 2 spotlights created that don't get destroyed when they glitch out.

- ![Bugfix](https://i.imgur.com/CYIMfjn.png) Fixed hammer dialogue playing each battle instead of only once.

  - Problem was that I was only setting to not play the dialogue, only if you used the hammer the 3rd time.
  - If you only used it once or twice each fight, it will play each fight.

- ![Bugfix](https://i.imgur.com/CYIMfjn.png) Giant cards will now be buffed from both side of the card if the adjacent friendly cards have `Leader`.

  - For example, if Bonelord has a Bone Prince on the left and right side of it, then the total buff will be 2 instead of 1

- ![Feature](https://i.imgur.com/uL9uYNV.png) Reset button is now only available to click in the Pause Menu.

- ![Feature](https://i.imgur.com/uL9uYNV.png) Explorable crypt! Although it's mostly the building, and not a whole lot in it at the moment.

- ![Feature](https://i.imgur.com/uL9uYNV.png) Added button to get up from the chessboard!

- ![Feature](https://i.imgur.com/uL9uYNV.png) 3 more `Skeleton` cards are added to side deck.

- ![Refactor](https://i.imgur.com/5bTRm1B.png) If no chess pieces have been interacted with at all, starting position for player piece will be defaulted. Meaning, if you haven't battled or opened a chest, your position will reset back to the start position for that board.

- ![Refactor](https://i.imgur.com/5bTRm1B.png) Stinkbug will now glitch out when dying if it hasn't been clicked yet.

#### Boneyard

- ![Bugfix](https://i.imgur.com/CYIMfjn.png) Cards having 4 abilities cannot be chosen for the Boneyard event.

  - Breaks the card rendering unfortunately.

#### Electric Chair

- ![Feature](https://i.imgur.com/uL9uYNV.png) Changed campfire sounds to be more electric.

- ![Feature](https://i.imgur.com/uL9uYNV.png) Cards that have been Electric Chaired will now have a permanent blue emission.

- ![Feature](https://i.imgur.com/uL9uYNV.png) If the Electric Chair would give `Swapper` sigil, a new sigil will be randomly chosen if the card has zero attack or fewer than 3 health.

  - This helps to avoid a card committing forever sleep on first hit.

#### Bosses

- ![Refactor](https://i.imgur.com/5bTRm1B.png) Boss skull during boss fight will glitch out when dying.

##### Kaycee

- ![Bugfix](https://i.imgur.com/CYIMfjn.png) Corrected issue with Submerge being removed from all cards instead of the card that was frozen during Kaycee's fight. (Thanks Magnificus!)

- ![Feature](https://i.imgur.com/uL9uYNV.png) Cards with `Hook Line and Sinker`, `Possessive`, or `Waterborne` will now lose that ability when frozen, and regain it back once unfrozen. Causes the lane to die entirely and not be useable...

- ![Refactor](https://i.imgur.com/5bTRm1B.png) Chess figurine icicle now looks more ice-like. Ice is hard to look like ice.

##### Royal

- ![Feature](https://i.imgur.com/uL9uYNV.png) Fleshed out more of Royal's first and second phases.

- ![Feature](https://i.imgur.com/uL9uYNV.png) New phase 2 music from `Akisephila (Addie Brahem)`.

##### Grimora

- ![Bugfix](https://i.imgur.com/CYIMfjn.png) Fixed issue with Twin Giants spawning.

  - The primary issue seemed to be how the creation of the abilities were done. No more direct static calls!

- ![Feature](https://i.imgur.com/uL9uYNV.png) New Bonelord entrance!

- ![Feature](https://i.imgur.com/uL9uYNV.png) The giants now have personal names! I wouldn't make one mad though...

- ![Refactor](https://i.imgur.com/5bTRm1B.png) Will now only reanimate every other card.

- ![Refactor](https://i.imgur.com/5bTRm1B.png) `Giant Strike` reworked with a new description.

  - `[creature] will strike each opposing space. If only one creature is in the opposing spaces, this card will strike that creature twice.`

- ![Refactor](https://i.imgur.com/5bTRm1B.png) `Bonelord` is now 1 attack 20 health.

#### Ability/Card Changes

- ![Bugfix](https://i.imgur.com/CYIMfjn.png) Fixed potential softlock if two `Screaming Skull` cards are on the board and the first one dies to `Leaping Trap`.

- ![Bugfix](https://i.imgur.com/CYIMfjn.png) Fixed potential softlock if a card with `Sprinter` and `Submerge` dies from `Sentry` after moving into the new slot.

- ![Bugfix](https://i.imgur.com/CYIMfjn.png) Fixed cards with flying no longer flying after attacking.

- ![Feature](https://i.imgur.com/uL9uYNV.png) New pirate cards, regular cards, and abilities! Art courtesy `Bt Y#0895`.

  - Captain Yellowbeard: 2/2, 7 Bones with `Sea Shanty`.
  - First Mate Snag: 2/2, 7 Bones with `Hook Line And Sinker`.
  - Privateer: 1/1, 4 Bones with `Sniper`.
  - Swashbuckler: 1/2, 0 Bones with `Raider`. Not obtainable.
  - Vellum: 0/2. Spawned from `Leaping Trap` instead of a normal pelt.

  - Abilities:
    - Raider: [creature] will strike it's adjacent slots, except other Raiders. Icon courtesy of `Blind, the Bound Demon#6475`.
    - Sea Shanty: [creature] empowers each Skeleton on the owner's side of the board, providing a +1 buff their power. Icon courtesy of `Blind, the Bound Demon#6475`.
    - Hook Line And Sinker: When [creature] perishes, the creature in the opposing slot is dragged onto the owner's side of the board. Icon courtesy of `Bt Y#0895`.

- ![Feature](https://i.imgur.com/uL9uYNV.png) New art for `Disinter` and `Screeching Call` abilities, courtesy of `Blind, the Bound Demon#6475`.

- ![Feature](https://i.imgur.com/uL9uYNV.png) New art for multiple cards.

  - Drowned Soul, courtesy of `Bt Y#0895`.
  - Ghost Ship, courtesy of `Cevin2006™ (◕‿◕)#7971`.
  - Poltergeist, courtesy of `Cevin2006™ (◕‿◕)#7971`.
  - The Walkers, courtesy of `Catboy Stinkbug#4099`.
  - Zombie, courtesy of `Bt Y#0895`.

- ![Refactor](https://i.imgur.com/5bTRm1B.png) Set all cards to `Undead` temple so they don't show up in Act 1.

## Full Credits

### Misc

- Akisephila (Addie Brahem) for the Soundtrack

- Cactus (cactus#0003) for making the official Trailer

- Arackulele for Balancing, polish, and other miscellanious things

- JulianMods (xXxStoner420BongMasterxXx) for refactoring the code.

### Code

- JulianMods (xXxStoner420BongMasterxXx) for being the Main Developer

- Arackulele for additional programming

### Artists

- LavaErrorDoggo (LavaErrorDoggo#1564) for making the Original Act 2 Cards but in full Size Artwork

- Bt Y#0895 for currently working on artwork for the mod

- Cevin_2006 (Cevin2006™ (◕‿◕)#7971) for additional Card art

- Arackulele for additional Card art

- Lich Underling (Lich underling#7678) for additional Card Art

### 3D Models

- Pink (Pink#6999) for making the Boss Skull Models , currently working on a full crypt 3D Model, etc

- Catboy Stinkbug (Catboy Stinkbug#4099) for the Board Skull 3D Models

- Draconis17#3692 for the new energy cells game object.

### Dialogue

- Primordial Clok-Roo (The Primordial Clok-Roo#2156) for a ton of future Dialogue

- Bob the Nerd (BobTheNerd10#2164) for some dialogue

- Spooky Pig (Mr. Etc.#3925) for event dialogue

- Arackulele, for the original Dialogue

- JulianMods (xXxStoner420BongMasterxXx) for additional Dialogue

### Additional Credits

- Blind, the Bound Demon (Blind, the Bound Demon#6475) for Gameplay footage

- The people of the Grimora Mod Discord Server, for Ideas , voting on features and being awesome
