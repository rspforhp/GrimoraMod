# Grimora Mod

- A giant Mod made by xXxStoner420BongMasterxXx and Arackulele, with modeling from `Pink#9824`, that builds upon the finale with Grimora's
  chessboard.

- **MOST IMPORTANT**: **!!!YOu will probably encounter some Bugs!!!**
- **IMPORTANT**: **ENABLE ENERGY DRONE IN API CONFIG IN ORDER FOR IT TO SHOW UP**
- **BEWARE**:

  - THIS WILL UNLOCK CERTAIN STORY EVENTS IN ORDER FOR THE MOD TO WORK, WHICH ALSO MEANS UNLOCKING CERTAIN ACHIEVEMENTS!
  - THIS MOD HAS NOT BEEN TESTED WITH KAYCEE'S MOD!

- In the event you are starting from a brand new save, this mod will unlock all learned abilities, mechanics, and cards
  and some Story Events like the first tutorial battle, Bones, and a few others in order for this mod to work.
- If you are not starting from a new save, this mod will check for certain story events and determine if it needs to unlock those.
- I suggest restarting your game at least once if you haven't actually gotten to the finale.

## Reporting Issues

- If you would to help report issues, please raise a thread here with as much detail as you can provide: <https://github.com/Arackulele/GrimoraMod/issues>
- You may also post in the modding discord general thread: <https://discord.com/channels/903472928883093545/914480438389661747>

ANY POSTS THAT JUST SAY 'A BUG HAPPENED AND IT BROKE' WILL BE IGNORED

## Known Issues

### Hoarder (Tutor) ability does not work with Corpse Eater (5$ USD reward for anyone who can actually figure out why this ability doesn't work in Grimora's act)

- There is no immediate reason as to why this doesn't work. The array of cards that are placed have the exact same code logic as if the card was placed manually by the player.
- The created game object could be causing the issue, but there is nothing that stands out as to what the cause is, even after debugging with prefix and postfix patches.

### Boss skull between fights doesn't reset colors

- Unfortunately this one is a bit hard to track down, as the table colors get reset after each fight. Not sure why the boss skull doesn't have the color reset.

### Rare card choosing breaks/throws exceptions

- The main culprit from what the team can tell, is that it's an issue with updating the asset bundles. The only recommendation we have now is to uninstall the mod completely, and redownload. Mod managers seem to be really finnicky.

### Grimora's dialogue is unfinished and still the one from the finale and part 1 dialogue in some cases

- Ongoing process to add new dialogue.

### Current save file is already at the finale with Grimora

- Make a backup of your save, then delete your current save. Having your current save already at the finale seems to break the mod.
- Possibly fixed in 2.6.4 update.

### Bonelord art overlaps abilites

- Bonelord does what he wants.

## Update Notes

### 2.6.6

- ![Bugfix](https://i.imgur.com/CYIMfjn.png) Fixed menu card flying off the screen with one line during creation of menu card: `MenuController.Instance.cards.Add(menuCardGrimora)`

- ![Bugfix](https://i.imgur.com/CYIMfjn.png) Fixed softlock if boss skull is null as there was no null check.

- ![Bugfix](https://i.imgur.com/CYIMfjn.png) Fixed issue where `Stinky` sigil would debuff both twin giants. The vanilla code handles `Giant` cards as if it was the moon and checks all player slots, and not just the opposing slots of the `Giant` card.

- ![Bugfix](https://i.imgur.com/CYIMfjn.png) Fixed softlock after defeating Grimora and chessboard does not load.

- ![Bugfix](https://i.imgur.com/CYIMfjn.png) Fixed Grimora queueing player cards that have died during second phase.

- ![Bugfix](https://i.imgur.com/CYIMfjn.png) Fixed `Flying` & `Submerge` combo not showing the card being submerged.

- ![Bugfix](https://i.imgur.com/CYIMfjn.png) Fixed issue with a card having both `Strafe Push` and `Skin Crawler` where the card would force itself out of the other card's skin, causing a softlock. It is now no longer possible to receive the `Skin Crawler` sigil if the card has `Strafe Push` and vice versa.

- ![Feature](https://i.imgur.com/uL9uYNV.png) Added new `Ember Spirit`, `Plague Doctor`, and `Vengeful Spirit` artwork courtesy of `Cevin_2006`.

- ![Feature](https://i.imgur.com/uL9uYNV.png) Added new Sawyer Boss Skull and Hammer model courtesy of `Pink#9824`.

- ![Refactor](https://i.imgur.com/5bTRm1B.png) Removed `Bonelord` from rare card pool entirely. Wasn't really meant to be playable, but hope anyone had fun while it lasted!

- ![Refactor](https://i.imgur.com/5bTRm1B.png) Moved dialogue event `RoyalBossPreIntro` from Royal to Grimora as it fits better.

### 2.6.5 HOTFIX

- ![Bugfix](https://i.imgur.com/CYIMfjn.png) Fixed softlock when choosing cards and one of those cards has an energy cost. The problem was that I was trying to get the `PlayableCard` component from the parent, _as opposed to literally just using the CardRenderInfo object that is passed in `GravestoneRenderStatsLayer.RenderCard`_

### 2.6.4

- ![Bugfix](https://i.imgur.com/CYIMfjn.png) Fixed card's with flying appearing no longer flying when attacked or is attacking. Animation resets loop when attacking or being attacked, but it's better than no longer flying.

- ![Feature](https://i.imgur.com/uL9uYNV.png) Added initial prefab for energy cells to show on cards. Will be tweaked accordingly.

- ![Feature](https://i.imgur.com/uL9uYNV.png) Added new `Poltergeist` artwork provided by `Cevin_2006`

- ![Feature](https://i.imgur.com/uL9uYNV.png) Updated Kaycee's boss skull model courtesy of `Pink#9824`.

- ![Refactor](https://i.imgur.com/5bTRm1B.png) Potentially fixed issue with having your current save already at Grimora's finale breaking the mod. This should now allow you to continue from any point as long as you are not at the finale.

### 2.6.3

- ![Bugfix](https://i.imgur.com/CYIMfjn.png) Changed setting boss skull transform for Kaycee to hardcoded position, rotation, and scaling.

- ![Bugfix](https://i.imgur.com/CYIMfjn.png) Fixed issue with NRE being thrown if `Reset Run` button was clicked. The issue was that the getter for finding Grimora's right wrist was static.

- ![Refactor](https://i.imgur.com/5bTRm1B.png) Removed `Buff Enemy (Annoying)` and `Random Consumable` ability from Electric Chair.

### 2.6.2

- ![Bugfix](https://i.imgur.com/CYIMfjn.png) Hopefully fixed weird NRE with finding Royal's skull.

### 2.6.1

- ![Bugfix](https://i.imgur.com/CYIMfjn.png) Fixed softlock in Grimora's finale phase.

- ![Feature](https://i.imgur.com/uL9uYNV.png) Added new `Bonelord` artwork and emission courtesy of `Ryan S. Art`!

- ![Feature](https://i.imgur.com/uL9uYNV.png) Added new Kaycee boss skull courtesy of `Pink#9824`!

- ![Refactor](https://i.imgur.com/5bTRm1B.png) Separated out dialogue lines after beating Royal so it looks less jumbled.

- ![Refactor](https://i.imgur.com/5bTRm1B.png) Tweaked card's light color in Grimora fight to be a bit light so the art is more visible.

- ![Refactor](https://i.imgur.com/5bTRm1B.png) Removed ice break counter for Kaycee so that she breaks Draugrs when possible.
  - Also removed clearing the board in the final phase of Kaycee.

- ![Refactor](https://i.imgur.com/5bTRm1B.png) Cleaned up descriptions of some cards to have correct punctuation and spacing.

- ![Refactor](https://i.imgur.com/5bTRm1B.png) Corrected viewing of queue slots to be before the start of the Bonelord phase instead of after.

### 2.6.0

#### Card Changes

- ![Bugfix](https://i.imgur.com/CYIMfjn.png) Changed `Amoeba` random ability to now only choose from the list of abilities that the Electric Chair choses from. This is due to the fact that `Amoeba` can be given an ability that breaks the game in unintended ways.
  - `Amoeba` now has a modified version of RandomAbility. The ability description may not show up correctly in Deck Review, just as a heads up.

- ![Bugfix](https://i.imgur.com/CYIMfjn.png) Fixed `Screaming Skull` damage done to now correctly deal damage to the owner of the card.

- ![Feature](https://i.imgur.com/uL9uYNV.png) Added new `Wendigo` and `Wyvern` artwork courtesy of `Cevin2006™ (◕‿◕)#7971`.

#### Bugfixes/Features/Refactors

- ![Bugfix](https://i.imgur.com/CYIMfjn.png) Fixed issue with `Giant` cards in phase 2 fight of Grimora throwing exceptions when spawning. Issue was that I did not add the `Giant Strike` ability to the card pool...

- ![Bugfix](https://i.imgur.com/CYIMfjn.png) Fixed issue with Hoarder ability (Tutor) throwing an exception when attempting to pick a card from your deck. The issue was caused by the fact that in the finale, there is no GameObject to handle the Hoarder ability. Therefore, it throws an exception.

- ![Bugfix](https://i.imgur.com/CYIMfjn.png) Fixed `DrawNewHand (Handy)` ability to now correct the visual issue of cards still appearing in draw pile after drawing.

- ![Bugfix](https://i.imgur.com/CYIMfjn.png) Fixed issue with card removal node decreasing card costs in the negatives.

- ![Feature](https://i.imgur.com/uL9uYNV.png) Electric Chair sequence now uses new model provided by `Pink#9824`.
  - Moved confirm stone button closer to chair.
  - Changed camera rotation during sequence to 32.5 from 37.5.

- ![Feature](https://i.imgur.com/uL9uYNV.png) New hammer dialogue courtesy of `Mr. Etc.#3925`.

- ![Refactor](https://i.imgur.com/5bTRm1B.png) Ordered decklist alphabetically in viewing cards left in the deck.

### 2.5.10

- ![Bugfix](https://i.imgur.com/CYIMfjn.png) Fixed logic in Kaycee's upkeep phase to now look for the Draugr's correctly.

- ![Bugfix](https://i.imgur.com/CYIMfjn.png) Hopefully fixed issue with duplicate rare cards showing up in choice selection.

- ![Feature](https://i.imgur.com/uL9uYNV.png) Added toggle button for viewing cards left in the deck during battle! Auto-updates in realtime as well.

- ![Refactor](https://i.imgur.com/5bTRm1B.png) Removed `Reset Deck` button as it doesn't really get used besides for debugging purposes, and `Reset Run` will most likely be used if the player wants to restart their deck.

- ![Refactor](https://i.imgur.com/5bTRm1B.png) Card changes:
  - Changed `Revenant` to use original sprite and emission.
  - Added vanilla `Banshee` to card pool.
  - Disabled attack and health shadows as sometimes the shadows don't correlate to the number and the overlapping is ugly.

### 2.5.8

- ![Bugfix](https://i.imgur.com/CYIMfjn.png) Corrected `Double Death` rulebook name to no longer be `Handy`. Funnily enough, the game has the ability page set with the name `Handy`. Probably because it was never actually meant to be used in an other act other than 2.

- ![Bugfix](https://i.imgur.com/CYIMfjn.png) Corrected logic for determining opening hand to at least always guarantee a `Bonepile` or `Gravedigger`.

- ![Bugfix](https://i.imgur.com/CYIMfjn.png) Corrected `Erratic` ability logic to now no longer force itself onto other cards.

- ![Bugfix](https://i.imgur.com/CYIMfjn.png) Set default ice cube creature within to `Skeleton` if the card would receive Ice Cube ability from Electric Chair.

- ![Refactor](https://i.imgur.com/5bTRm1B.png) Card changes
  - Changed `Skeleton Army` ability to now create a `Skeleton` in all owner owned open slots when played.
  - Lowered `Dans Macabres` Bone cost to 5
  - Lowered `Screaming Skull` Bone Cost to 2

- ![Refactor](https://i.imgur.com/5bTRm1B.png) Electric Chair changes:
  - Removed more abilities from Electric Chair sequence:
    - Activated Sacrifice Draw Cards (True Scholar)
    - Brittle
    - Buff Gems (Gem Animator)
    - Create Bells
    - Create Dams
    - Draw Ants
    - Draw Copy on Death
    - Draw Random Card On Death
    - Drop Ruby on Death
    - Draw Vessel on Hit
    - Latch Brittle
    - Latch Death Shield
    - Latch Explode on Death
    - Shield Gems (Gem Guardian)
  - Updated dialogue.
  - Reduced number of times to electrify card to a max of 2 times.
  - Changed texture of selection slot to be a plus sign with a question mark instead of 3 question marks.

- ![Refactor](https://i.imgur.com/5bTRm1B.png) Changed `Sporedigger` description to better fit the card.

- ![Refactor](https://i.imgur.com/5bTRm1B.png) Reduced `Skeleton Army` activated bone cost from 2 to 1.

- ![Refactor](https://i.imgur.com/5bTRm1B.png) Made hammer only usable up to 3 times per battle. The reasoning for this is that the hammer use should be used when necessary, and not a large crutch.

### 2.5.7

- ![Feature](https://i.imgur.com/uL9uYNV.png) Redid all of the Chessboard layouts...again!
  - The new nodes are now introduced gradually
  - Slightly less linearity. Beat optional enemies to gain access to more nodes.

- ![Feature](https://i.imgur.com/uL9uYNV.png) Difficulty rebalance!
  - Blueprints now follow a difficulty curve.
  - Kaycee now has a new Boss Mechanics, making the first map slightly easier.
  - Royal's and Grimora's areas have been buffed to provide for a more challenging experience given the new chess pieces.
  - Rebalanced the new cards to make them more interesting and less underpowered.
  - Removed the BoneLord sigil from the Electric Chair ability pool :)

### 2.5.0

#### Cards

- **Boo Hag** - 1,1 - ![cost_bone](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone.png)![cost_bone_5](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_5.png) Sigils: Skin Crawler.

- **Danse Macabre** - 3,3 - ![cost_bone](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone.png)![cost_bone_8](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_8.png) Sigils: Alternating Strike, Erratic.

- **Dybbuk** - 0,1 - ![cost_bone](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone.png)![cost_bone_6](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_6.png) Sigils: Possessive.

- **Giant** - 2,7 - ![cost_bone](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone.png)![cost_bone_1](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_1.png)![cost_bone_5](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_5.png) Sigils: Bone King, Bifurcated Strike.

- **Project** - 1,1 - ![cost_bone](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone.png)![cost_bone_8](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_8.png) Sigils: Trifurcated Strike.

- **Ripper** - 6,6 - ![cost_bone](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone.png)![cost_bone_1](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_1.png)![cost_bone_2](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_2.png) Sigils: Brittle.

- **Screaming Skull** - 1,1 - ![cost_bone](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone.png)![cost_bone_6](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_6.png) Sigils: Area Of Effect Strike.

- **Silbon** - 3,2 - ![cost_bone](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone.png)![cost_bone_7](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_7.png) Sigils: Inverted Strike, Sprinter.

#### Sigils

- **Alternating Strike** - [creature] alternates between striking the opposing space to the left and right from it.
- **Area Of Effect Strike** - [creature] will strike it's adjacent slots, and each opposing space to the left, right, and center of it.
- **Erratic** - At the end of the owner's turn, [creature] will move in a random direction.
- **Inverted Strike** - [creature] will strike the opposing slot as if the board was flipped. A card in the far left slot will attack the opposing far right slot.
- **Possessive** - [creature] cannot be attacked from the opposing slot. The opposing slot instead attacks one of it's adjacent slots if possible.
- **Skin Crawler** - When [creature] resolves, if possible, will hide under an adjacent card providing a +1 buff. Otherwise, it perishes. Cards on the left take priority.

## Special thanks to

- LavaErrorDoggo for making a lot of the artwork,

- JulianMods (xXxStoner420BongMasterxXx) for refactoring the code.

- Cevin_2006 for additional card art,

- Bt Y#0895 for currently working on artwork for the mod,

- Kopie for being a former developer.

- Draconis17#3692 for the new energy cells game object,
