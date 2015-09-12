# L2 .NET
L2 .NET is an automated assistant tool (commonly referred to as 'bot') for Lineage 2 that assists a player's experience during their play. It is similar to [L2Divine](http://www.l2divine.com/) and [L2Walker](http://www.towalker.com/).

WARNING: This software is not designed to circumvent anti-bot software nor is it supported for retail versions of Lineage 2. Using this on servers where botting is not allowed is not supported or condoned in any way.

Features
---
* Out of Game Mode, allowing you to fully control your characters without being inside the normal Lineage 2 Client.
* In Game Mode, allowing you to attach to a game client and enhance the gameplay experience (best of both worlds).
* Top-Down Overview map allowing you to interact with the world.
* Combat / Buff/Heal Options to automate skill usage.
* Custom Scripting Support, including packet detection and logic chains

Agreement
---
This program is developed on and for L2J servers ONLY.
Use of this program on any other type of server is against the EULA.
This program comes with no warranty expressed or implied.
Any concerns about copyright or other challenges can be messaged to the open source contributor for review and removal.


Download
---
* Download and Extract [Latest Download Link](https://github.com/Xackery/l2net/raw/master/bin/latest/latest-2015-09-10.zip)
* Download and Install [SlimDX](http://slimdx.org/download.php) (Used for Game Window, optional)

Compiling
---
* Get [NuGet for Visual Studio C# 2015](https://visualstudiogallery.msdn.microsoft.com/5d345edc-2e2d-4a9c-b73b-d53956dc458d)
* Get [Newtonsoft.Json](https://www.nuget.org/packages/Newtonsoft.Json/) via NuGet Package Manager
* Open L2 Login.sln with Visual Studio C# 2015

# Original Changelog


Nov 27: Add: "Ignore Exit Confirmation" option in Settings menu
	 Fix: Autosweep button
	 Add: Active/Passive button in skill list
	 Add: Delete Char Button
	 Add: Auto Soulshots/Spiritshots

Nov 28: Add: "Remove All" option in donot items and npc lists

Nov 30: Add: BETA!: OOG Trade - Needs testing
	 TODO: Add text to resx files, Improve "number of items" textbox, add "received items" list.
	 ADD: BETA!: OOG Buy from normal shop - Needs testing
	 TODO: Clean code, add multilanguage support, make a better interface.

Dec 07: Buy from shop window disabled in IG mode to prevent l2net from locking up.
	 Trade window is now called with .Show() instead of .ShowDialog()

v378:

Dec 16: Fixed bug with trade crashing on certain items.
	Started work on action window.
	Add support for social action: Shy

Dec 25: Fixed inventory on Official.

Dec 29: Fixed bug with l2net crashing when using shy and charm in CT 2.2 and below
	Fixed bug preventing all items from showing in trade window.
	Trade window closes automatically if trade is cancelled.

Jan 4:  Add Security card support (IG only)
	Edit: Security card is now detected automatically, removed security card checkbox
	Fix: Bug making active skills show up when passive radiobutton is selected under certain conditions.

v379:

Jan 8: Protocol version now updates automatically when you select a different chronicle.
Jan 12: Add force-log button.
	Remove passive skills from skill-list in "Buffs/Heals" tab
Jan 15: Add key only feature: Advanced logon -> Custom enterworld packet. Store the packet in enterworld.txt if you don't want to type it manually
Jan 18: "Passive?" column in skill-list removed.
	Buttons in the OOG login window don't move on top of eachother when the l2net window is resized anymore.
Jan 24: Fix packet error for CT 2.3 and above servers when using OOG login. (request player list packet)
	Fix packet error for CT 1.5 and below servers when using OOG login. (request player list packet)

v380:
Jan 25: Fix listview_skills bug
	Fix more packet errors, OOG is now working again on L2Inc
	Disable text boxes when setting toggle/kill key

v381:
Jan 26: Fix bug with textbox making l2net crash under certain conditions

v383:
Beta 1:
Fix "Index was out of range" error when connecting to l2inc
Beta: Rest options
Beta 2:
Fix HP view in Official server (Credits to d00d for analyzing the new packet)
Add "Official server" checkbox to keep backwards compability with old CT 2.4 servers
Rest options: Rest below HP is now defined as percentage.
Beta 3:
Fix HP view for party members in official server
EnterWorld packet updated to the current G+ Official packet.
Fix -c & -protocol command line parameter.
Add "Wrong username or password" error message when logging in. (Server full and other errors still gives the old "Login fail" message)
Beta 4:
Add GET_DEC and HEXTODEC script commands.
Beta 5:
Add: "Account already in use" error message
Rest options moved to a seperate tab, MP support
Add: "Rest until" in rest options
Change: [I used social skill: pooping: 84A] to [I used social skill: Level Up] when leveling up.
Beta 6:
Fix: netping reply for CT 2.3 servers
Fix: Bug in rest options when char is dead.
Add: Follow Rest
Add: "Add to donot list" option in item list.

v384
Beta 1:
Add: "Add to donot list" option in inventory list.
Add: "Add to donot list" option in NPC list.
Add: Rest options is now saved to the config file.
Add: "Ignore Raidbosses" and "Ignore Chests" checkboxes. Credits to Inxile for finding and writing down all the NPCs and to mpj123 for the suggestion.
Add: Proper output for more login errors (server overloaded, server maintenance, wrong password (but correct username)).

Beta 2: 
Stuckcheck:
 - The bot is now able to detect if its stuck
TODO:
 - Write code to make the character do something when its stuck.
   - Done: The character now tries to free itself when stuck.
 - Parse "cannot see target" system message
 - Auto blacklist bad mobs.
   - Partially done: Mobs get blacklisted if the char can't attack it after trying to unstuck 6 times.
Change: NPC-List extended to include Type-ID
Fix: add to "do not" list now works with NPC's with the same name but different ID.
Add: Blacklist npc button in NPC list. This will make the bot ignore only the selected NPC, useful for NPC's in trees etc.
Add: Commands -> "Blacklist current target" button.
Add: Auto unstuck checkbox status is now saved to the config file.
Add: Needs testing!: "Only pick mine" option.
Add: /petattack



/*********************************************/
v385
Beta 1:
Add: Script command: BLOCK_SELF, BLOCKEX_SELF, UNBLOCK_SELF, UNBLOCKEX_SELF, CLEAR_BLOCK_SELF, CLEAR_BLOCKEX_SELF
Add: Script event: SCRIPTEVENT_SELFPACKET, SCRIPTEVENT_SELFPACKETEX
Fix: BLOCK_SELF and BLOCKEX_SELF no longer blocks INJECT and INJECTBB
Beta 2:
Fix: Maxlenght changed to 64 in server ip textboxes to support longer addresses.
Add: Script command: BLOCK_SELF_ALL, BLOCKEX_SELF_ALL
Add: Map Z-Range and Zoom-level is now saved to and loaded from interface.txt.
Fix: You no longer get autobanned on l2divinity and servers with similar protection. Credits to obce for analyzing the packets.
Beta 3:
Fix: Trade now supports 64bit integers.
Add: /trade now makes the trade window pop up.
Beta 5:
Fix: "ERROR: Packet Error: 1C Previous Packet: 62 : Object reference not set to an instance of an object." when trading
Fix: Stuck-check and blacklist no longer triggers when char is dead.
Fix: Mobs attacking you is now removed from the blacklist automatically.
Fix: Char no longer attacks party members.
Fix: Mobs in the do-not list attacking you is now getting targeted and killed.
Add: Radiobuttons for inventory (Items, Equipped, Quest)
Fix: Type2 set incorrectly when loading inventory
Add: Support for ports in loginlist.txt. Format: IP:Port or IP Port. For example 127.0.0.1:1231 or 127.0.0.1 1231
Add: "Clear Options" button in bot options.
Add: Support for FE:33 - ExSetCompassZoneCode
Add: Script command GET_ZONE. Usage: GET_ZONE variable. 
Add: Support for OOG Private store!
Fix: /Vendor no longer causes the client to crash
Beta 7:
Add: Ignore Summons checkbox, credits to Inxile for finding all the IDs
Add: -ew & -enterworld commandline switch
Fix: War state+++ in CT 2.4 servers
Fix: Party stuff in CT 2.3 servers
Add: Stuck check now checks for "cannot see target" system message.
Fix: Heal/Buff-range now loaded correctly.
Fix: Stuck-check only triggers if we got a target.
Fix: PRINT_TEXT messages changed to only show up in the "All" and "Bot" tab
Beta 9:
Change: "Move before attack" changed to "Move Smart Before Attack"
Add: "Move Before Attack" checkbox, this will use normal movement instead of move smart.


/*********************************************/
v386
Beta 1:
Add: Support for OOG security card on private servers.
Update: Anti-KS logic updated.
Add: Mobs attacking party members are now automatically removed from blacklist.
Update: Tweak unstuck code, blacklist made more aggressive.
Fix: Armor icons not showing up correctly.
Add: Advanced tab in botoptions.
Add: More stuff saved to the config file.
Add: "Pick Only" and "Attack Only" in donot tab

Beta 2:
Add: Freya buttons
Fix: inventory list in Freya
Change: Default ip changed to new NA official ip
Add: Logon window now pops up automatically when starting l2net
Fix: Freya EX Packets and enterworld (thanks d00d)
Change: Server dump text is now showed in yellow and client dump in orange.
Change: Default protocol changed to 216 to support freya.

Beta 3:
Add: Valid product key feature: Support for servers with unknown blowfish (Alpha)
Add: Support for unknown blowfish on servers that share the login and gameserver IP.
Add: Botoptions -> Advanced -> Custom Window Title
Add: Auto invite to party. Format supported: Name1,Name2,Name3 or Name1;Name2;Name3
Fix: Blacklisted mobs no longer getting targetted when "attack only" is active.
Add: Custom window title, Party loot type & Attack/Pick only items in list is now saved to the bot options.
Add: Valid product key feature: Ability to set gamekey manually (l2dc)
Change: "failed to load saved enterworld packet" removed to avoid incorrect bug reports. All this error message means is that enterworld.txt used for "custom enterworld packet" is missing or contains invalid data.
Add: Freya OOG login stuff.
Add: More login packets (OOG) fixed


/*********************************************/
v387
Beta 2:
Fix: Freya charlist.

Beta 3:
Add: Script commands:
	BOTSET AUTO_UNSTUCK_ON "<&TRUE&>"
	BOTSET MOVE_BEFORE_ATTACK_ON "<&TRUE&>"
	BOTSET MOVE_SMART_BEFORE_ATTACK_ON "<&TRUE&>"
(Fix: Everything in bot options made translatable)
Change: Load now displayed as %
Fix: Autospoil no longer causes auto-unstuck not to work
Fix: Nullpointer exception
Fix: Unknown blowfish now works for servers with non-standard gameserver port.
Add: Login->Advanced: Combobox for gameservers stored in gslist.txt. Format: Same as loginlist.txt.
Fix: Summon packet is now processed correctly
Fix: Summons are no longer getting targeted by the autofighter.

Beta 4:
Fix: Bug when loading botoptions-files with blank window title.
Update: Autofighter improved.

Beta 5:
Add: Hero tab in text window.
Add: ExQuestItemList = 0xC5
Fix: Quest items not showing up in inventory on Freya servers
Add: ID to GET_EFFECTS

/*********************************************/
v388
Beta 1:
Fix: GG reply length made dynamic in GG.txt
Fix: "Packet Error: F4 :: Object reference not set to an instance of an object." when in a party with someone far away.
Add: Support for ExVitalityUpdate = 0xA0
Add: Vitality is now displayed next to XP bar

/*********************************************/
v389
Beta 1:
Add: Bypass simple antibot used in some servers.
Fix: EXShowScreenMessage
Fix: Unable to create female dwarf OOG
Change: "Smart move before attack" disabled until its fixed
Fix: Loginserver connection now closed correctly
Fix: Potential bug causing char not to attack when using a buff (autofighter)
Add: Setup -> Disable botting if GM action. This will turn off botting if a GM performs an action on you, but it also triggers on some l2j system messages (mainly when logging in). Use with care.
Add: New soulshots IDs now appears in the auto-ss list.

/*********************************************/
v390
Beta 1:
Fix: HP view of party members when "official" is checked
Fix: NPCSay
Add: NPCString.txt (Put in data folder)

Beta 2:
Fix: High Five userinfo
Change: Official server checked by default in Freya and above servers
Fix: Skills with no icon are now showing up in the skill-list
Fix: Protocol packet High Five
Add: Enterworld High Five
Fix: OOG Create char High Five
Fix: Authlogin High Five
Update: NPCString.txt updated to High Five
Fix: High Five npc chat

Beta 3:
Fix: /evaluate in CT 2.5 and above servers
Add: Color progress bars for HP/MP/CP/XP
Add: Updated charinfo panel

/*********************************************/
v391
Beta 1:
Add: Botoptions -> Auto Blacklist. It is now possible to auto-blacklist mobs without having the char trying to unstuck first.
Add: Login -> IG -> Override Protocol. Allows you to override the game client protocol.
Fix: Antibot: Made bot harder to detect.
Fix: Autoblacklist & auto unstuck improved.
Add: Autofighter: Move before target. This will move the char close to the mob before targeting it (BETA)
Fix: Autofighter no longer spams packets in high five official server (never did in private servers)

Beta 2:
Add: Dead Logout/Return/Toggle botting
Update: Bot options design updated
Add: Advanced login settings -> custom gameserver listen port
Fix: Char not attacking if attacked when resting.

Beta 3: 
Fix: Bug causing char to run west when no mobs are in range
Fix: Bug causing char not to attack after resting then spoiling
Add: Spoil until success button.
Fix: 0x12 Announcements now show up correctly
Add: Botoptions -> Pick Up After Attack
Fix: Autosweep should now work better

Beta 4:
Add: Command line parameters: -ig -oog -official_on -official_off
Fix: Spoil until success

Beta 5:
Add: Textbox spoil mp above.
Add: Pet Window

Beta 6:
Fix: Packet error when more than 1 pet in party
Fix: Error when loading old settings file
Fix: -IG -OOG command line parameters now works for all users
Add: Pet Inventory
Add: Pet Actions
Add: Autofighter -> Pet assist

Beta 7:
Add: Support for custom sized crests/captchas
Add: Key only: OOG captcha box popup
Update: Beautify actions window
Add: Active follow attack instant - Attacks instantly without waiting for leader to attack first
Fix: Autospoil when spoiler is not leader


v392
Beta 1:
Add: Preliminary GoD support.

Beta 2: 
Fix: GoD NPC-Chat
Fix: GoD Shortcut errors
Fix: GoD Endless buff loop

Beta 5:
Add: GoD lvlexp.txt
Fix: GoD: Players with green names are no longer shows up as pk'ers on map
Fix: GoD: Autofighter stops attacking

Beta 6:
Add: Auto updater
Add: All GoD datapack files

Beta 7:
Add: Display m.accuracy, m.evasion and m.critical in gui.
Add: Botoptions -> Advanced -> Pickup Delay
Add: GG forwarding to enable OOG clients in l2off. (File -> Gameguard window)
Fix: use_skill_smart & is_ready. (Credits to Infant for spotting the error)
Add: GoD Enterworld + some other packets

Beta 8:
Fix: GoD opcode obfuscation
Add: GG forwarding for IG clients running no gg patch
Update: Datapack updated to newest GoD revision 01/11/12
Fix: New soulshots now show up in the bot options

Beta 9:
Fix: Clan stuff
Fix: -bad target- bug in autofighter
Add: Bounding polygon points can now be moved by clicking on them
Add: "Move before attack" now works better for nukers
Update: Autofighter updated to only attack mobs in polygon, even if mobs outside it targets you
Add: Bounding polygon points can now be added by clicking on map

Beta 10:
Temp Fix: Disable clan stuff again
Fix: Crash when loading bot options
Fix: Char should no longer attack summons even if they are flagged

Beta 11:
Add: Command line:   -ggip //gameguard server listen ip & gameguard client connect ip
		     -ggport // --- || --- port
                     -ggsrv //Autostart gg server
                     -ggcl //Autostart gg client
		     -secpin:123456  //sets security pin to 123456
Add: IG: Auto restart IG listener if login fails
Add: GoD Pet info stuff
Add: Pet heal support, in heal options, enter name "pet" and uncheck "need target"
Add: Summon solo attack
Add: Summon instant attack
Fix: F4 Packeterror suff
Fix: Ignore raidbosses (thanks Jeapordy)
Add: Security pin feature OOG

RC1:
Fix: Update anti-ks stuff for summoner
Fix: Increase delay when targetting mobs to prevent false autoblacklist
Add: Support for auto-securitypin IG
Add: Support for security pin from clients using old system folder
Add: Commandline: -oldclient
Add: Autolearn skills (semi automatic)
Fix: Party parsed incorrectly

RC2:
Add: Autokill threads and re-listen if server is full
Change: Autolearn skills not checked by default
Fix: Targeting
Change: Minor speed improvement when blacklisting mobs (thanks Infant)

Harmony:
New datapack etc

v393
Beta 1:
Fix: Compability update for GoD official  11.04.2012 changes
Fix: SKILL_GET_REUSE (thanks riff)
Fix: Toggle buffs now works in interface and scripting.
Add: Plunder
Add: -o, -options autoloads bot options,example: -o:c:\options.l2d
Fix: IS_INCOMBAT for pets 
Fix: IS_FLAGGED for pets
Fix: IS_RED for players and pets in GoD
Fix: IS_BLEEDING for pets
Fix: IS_POISONED for pets
Fix: IS_REDCIRLCE for pets
Fix: IS_ICE for pets
Fix: IS_WIND for pets
Fix: IS_AFRAID for pets
Fix: IS_STUNNED for pets
Fix: IS_ASLEEP for pets
Fix: IS_ROOTED for pets
Fix: IS_PARALYZED for pets
Fix: IS_PETRIFIED for pets
Fix: IS_BURNING for pets
Fix: IS_FLOATING_ROOT for pets
Fix: IS_DANCE_STUNNED for pets
Fix: IS_FIREROOT_STUN for pets
Fix: IS_STEALTH for pets
Fix: IS_IMPRISIONING_1 for pets
Fix: IS_IMPRISIONING_2 for pets
Fix: IS_ICE2 for pets
Fix: IS_EARTHQUAKE for pets
Fix: IS_INVULNERABLE for pets
Fix: IS_REAL_TARGETED for pets
Fix: IS_DEATH_MARKED for pets
Fix: IS_TERRIFIED for pets
Fix: IS_CONFUSED for pets
Fix: IS_INVINCIBLE for pets
Fix: IS_DISABLED for pets
Add: Multipet support in buffs/heals. Usage: use pet;pet1;pet2;pet3 as names
Add: Pet script variables: PET1_*, PET2_*, PET3_* 
	Example: PET1_FORM will return the form of your 2nd summon, PET_MAX_HP will return the hp of your 1st summon, PET2_X will return the X value of your 3rd pet, PET3_PER_HP will return the hp percent of your 4th pet)
	PET_NAME
	PET_TITLE
	PET_X
	PET_Y
	PET_Z
	PET_DESTX
	PET_DESTY
	PET_DESTZ
	PET_IS_MOVING
	PET_MAX_HP
	PET_MAX_MP
	PET_MAX_CP
	PET_MAX_LOAD
	PET_MAX_FED
	PET_CUR_HP
	PET_CUR_MP
	PET_CUR_CP
	PET_PER_HP
	PET_PER_MP
	PET_PER_CP
	PET_CUR_LOAD
	PET_CUR_FED
	PET_RUN_SPEED
	PET_WALK_SPEED
	PET_ATTACK_SPEED
	PET_ATTACK_SPEED_MULT
	PET_CAST_SPEED
	PET_ID
	PET_TYPE
	PET_NPCTYPE
	PET_FORM
	PET_TARGETID
	PET_RUNNING
	PET_LOOKS_DEAD
Fix: GROUP_HP not including self
Fix: GROUP_MP not including self
Fix: GROUP_CP not including self
Fix: GROUP_HP, GROUP_MP, GROUP_CP always triggering
Fix: Disconnects when need target in bot options is checked and script is running at the same time
Fix: Pet1;Pet2;Pet3 not working correctly in bot options

v394
Fix: Shortcut stuff
Fix: Anti-KS logic
Add: Auto-reply
Fix: XP-Percent
Fix: Duplicate entries in do not lists

v396
01.24.13
 - New datapack encrypter
 - New crypt keys

02.03.13
 - Fix OOG login

02.21.13
 - Fix party view when pets in party
 - Fix tab size in information panel

02.27.13
 - Fix abnormal effects for players
 - Add option to disable NPCsay

03.06.13
 - Fix abnormal effects again...


01 Apr 2013 - Added 3 pickup ban preventions... Beta
			- Scripting pickup nearest now works the same as the GUI's pickup.
			- tweaked pickup code
			- re-added pickup mode. 
			- pickup will no longer ignore/blacklist when stun/slept etc.



13 Mar 2013 - added out of combat stuff (move when no mobs are around)
			- messed with gui positioning
			- small tweak to pickup... fix stuck issues?
			- tweaks to move to location, including canceling target when dead and spoil isnt activated.
			- added leash to botoptions for move to location when no mobs			
			- Bot will KS by default, anti-ks does not work well in party.
			- party invite crash fix
			- added party in combat detection for various features... including pickup
			- Toggle Skills GUI (only) complete implementation
			- other stuff i forget...
			- pickup bug fixed, (blacklisting items that can't be reached)
			


-------------------------------------------------------------------

05 Mar 2013 - pickup 2.0 (works with AOE and works way better then the last one) 90% re-write. This should be a huge performance boost. Most likely 200%+ performance boost. 
			- fixed 2 ban related targeting issues. Highly recommend you drop whatever your bots are doing and update to this version if you are playing retail.
			- added bad npc counter in stats


01 Mar 2013 - cleaned code and optimized pickup function
			- optiomized stats page
			- added combat skills disabled when picking up
			- added stats for meshless ignored


-------------------------------------------------------------------

21 feb 2013 - fixed npc titles... again
			- added Gained adena per hour
			- added Total adena gained

22 feb 2013 - partially added items picked up to stats page

23 feb 2013 - items for stats completely fixed. Please let me know if you spot anything I missed.

24 feb 2013 - fixed crystalize from inventory
			- tweaked autopin again... (3 seconds now... that should be enough.... right?)

25 feb 2013 - fixed small gui bug

27 feb 2013 - fixed packet structure
			- added additional meshless items. This probably wasn't used to ban people, but thinking ahead helps.

28 feb 2013 - changed pickup timeout to 10 by default... please let me know what you think.
			- fixed distance bug with DONOT + Whitelist
			- revamped pickup. Please test and let me know if anything is off.
			- tweaked pickup to not attack mobs while in pickup mode.
			- Disabled cancel target on dead when spoil is activated
			- resting now happens after pickup is complete
 

-------------------------------------------------------------------

20 Feb 2013 - XP/SP per hour added, new tab added for stats
			- fixed npctitles
			- added accept rez from party
			- default Z range changed to 600


-------------------------------------------------------------------

05 Feb 2013 - Active follow distance now 200 by default
			- small tweaks for placement in bot options.

07 Feb 2013	-added auto accept party/rez from alliance/clan
			-fixed clear in botoptions to clear new checkboxes I added. Also resets the follow distance to 200.
			-fix memory leak? (obce/slothmo/mpj123)



-------------------------------------------------------------------

25 Jan 2013 - Fixed party invite count, previously 9, now 7

27 Jan 2013 - added .PER_HP
			  added .PER_MP
			  added .PER_CP
			  to scripting for npcs and players

			  Changed protocol to 488
			  added cancel on dead target (bot options)
29 Jan 2013
			  added autolearnskills to save interface and load
			  Fixed crash using autopin (now waits before responding)
			  Disabled packet transform in the content filter option, this needs work.

31 Jan 2013 - Fixed Disconnect on IG Crash (save to interface... not recommended on retail fyi)
			  Disable botting if teleported (save to interface)
			  Added a warning for Disconnect on IG crash since we no longer hardcode the value. (now shows "Check this for retail

02 Feb 2013 - Fixed invite party member
			  Also added error handling added, you wont crash anymore, and just in case, added name parsing so you wont be sending invites with blank requests...
			  added option to turn off log writing in setup menu. It will still create the file, but wont write anything to it. This is only in case you decide to turn it on at a later time.

03 Feb 2013 - Tweaked pin to wait for 1 second before entering pin to better emulate real players, and hopefully reduce the crash.
			- Added leave if leader in bot options.
			- tweaked invite party member, also put in a check for being in the gameserver before starting.

--------------------------------------------------------

17 Jan 2013 - added ExSendUIEventPacket chat handling
			- added BOTSET options to commands.txt
19 Jan 2013 - fixed auto-pin packet
			- fixed social skills (shyness and others)
24 Jan 2013 - fixed autolearn skills



3.07.2013: Fixed IS_RED
		   Fixed Karma display on Char Info Tab, Now shows absolute negative value instead of positive million.
		   (Note: Cannot use uint to display/parse karma value, breaks is_red and displays karma incorrectly)
		   Found OC overflow bug with transformations, Causes L2Net to crash when untransforming, will try to replicate at later date.
		   Updated Language Files from Karma to Reputation.
		   Modified Char_status_new_god.bmp (char status tab background image) to correct Karma heading to Reputation.
2.27.2013: 
			Updated Get_Skills to include Reuse time in sortedlist
			Files Include: Util_Get.cs, Script_Handler_L2.cs, Skill.L2c .
			Added: Sample Script - get_skills_test.l2s .
			Updated Ignore Summons List, Confirmed Sigel, Aeore Healer, Wynn Summoner - Summons in List.
			Files Include: BotOptionsScreen.cs
			Provided Community with Excel Sheet of Summon Names included in the ignore list.


volatile only on: byte, sbyte, short, ushort, int, uint, char, float, or bool, or an enumeration with a base type of byte, sbyte, short, ushort, int, or uint


"Auto Unstuck"
"Only Pick Mine"

---
v390
---
fixed error calling functions/labels/subs with lowercase names

v389
---
no work done

v388
---
updated jingjing StdLib scripts
splash cleaned up by tortur
map streams are now released after no use for 5min (allows fast resizing... but saves memory later)
map textures are now released after no use for 60min (eventually prevents memory bloat when porting a lot)
map is no longer drawn when the client is minimized
small optimization of datafile loading
listview sorters changed to insertion sort (faster and still stable)
improved map texture rendering
improved handling of loading the same file multiple times
added file list and class list to script debugger
fixed EFFECTS in Get_Party
added option in setup to disable gpu acceleration

v387
---
//changed exe to target any cpu instead of x86  (will now run in 64bit mode on x64 capable pcs)
new splash from tortur - congrats :)
new v387 datapack
switched from ManagedDX to SlimDX (l2net.exe.config no longer needed)
added file missing error handling on datapack loading
small optimization to decryption code
added error handling to keyboard device creation
new evaluator for = and IF/WHILE/LOOP conditions
type of INT/DOUBLE/STRING is maintained when assigned with value of different type (casting is performed)
fixed bug with parsing built in class operators and " "
fixed bug with evaluator not returning values of TRUE or FALSE with unary operators
added PET_PER_HP, PET_PER_MP, PET_PER_CP, TARGET_PER_HP, TARGET_PER_MP, TARGET_PER_CP
changed ArrayList and SortedList views in Debugger to show keys/indexes rather than variable names (expanding the leaf shows the name)
added script compatibility mode with v386
fixed error displaying sorted lists returned from GET_* functions in the script debugger
changed script class variable storage to sortedlist from arraylist to reduce access time and improve performance with class objects
script class member variable names automatically converted to uppercase
script file cached function,sub,label lists changed from arraylist to sortedlist for performance

v386
---
updated exe to require .Net 4.0
tweaked process encryption
changed netping to accept invalid id values (l2ex.pl)
refactored datafile loading to further decrease load times
improved script engine performance when accessing variables
improved bot option screen opening performance
added item traits: Bleeding, Poisoned, Iced, Shackled, Feared, Stunned, Slept, Muted, Rooted, Paralyzed, Petrified, Ultimate Defense
added script command: GeneratePoly [double radius] [int sides] [double offset]
setup window now set to topmost (can't be hidden behind other windows)

v385
---
updated process encryption
updated French language (ty you!)
updated Spanish language (ty Lyara!)
updated Portuguese BR language (ty cDDDe!)
added Bulgarian language (ty freeze!)
changed default application priorty to Above Normal
tweaked thread priorities to reduce network and script lag
improved script = support for accuracy when casting variables between types
using DELETE on a THREAD variable will now stop the thread from executing and remove it from memory
removed ability to target invisible npcs via the map
added support for dynamic variables prefaced with 0x to cast from hex to decimal
massive revamp of objects to decrease line count, decrease code complexity, increase performance
tweaked npc ghost cleanup code
changed FILEWRITER, FILEWRITER_APPEND and FILEREADER to only open files as L2.Net\Scripts\Files\passed_realative_file_path
fixed packetex id obfuscation
added support for encrypted scripts
refactored datafile loading to decrease load times by 15%

v384
---
new datafiles
default protocol changed to 152
added 36 new localizable terms
updated Japanese language (ty gyo!)
added shadows to map text
tweaked packet id ofuscation to allow pass through for values out of range
client packet and client packetex events now clear on script restarts
fixed bug with clientpacketex not generating events correctly
changed followrest thread to call the common action parser for sit/stand
added buff listview
refactored skill image list to reduce memory usage and increase performance
refactored item image list to increase performance
added buff/heal option: bleeding
added buff/heal option: poisoned
added buff/heal option: iced
added buff/heal option: shackled
added buff/heal option: feared
added buff/heal option: stunned
added buff/heal option: slept
added buff/heal option: muted
added buff/heal option: rooted
added buff/heal option: paralyzed
added buff/heal option: petrified
added buff/heal option: ultimate defense
massive jingjing refactor
script lines are now partially compilied when loaded to increase performance while running
removed support for dynamic script commands: you can no longer create a string set equal to some script command and pass it to jingjing via "<&some_command&>"
script function names no longer case sensitive
script sub names no longer case sensitive
improved tokenizer (better support for #$)
script includes for classes executed on file load now only run if they are before the class definition
added = support for == token
added = support for != token
added = support for < token
added = support for <= token
added = support for > token
added = support for >= token
improved support for .CLONE with ARRAYLIST types
improved support for .CLONE with SORTEDLIST types
improved support for .CLONE with STACK types
improved support for .CLONE with QUEUE types
improved do/loop,while/wend,if/else/endif,for/next,foreach/nexteach performance
added support for items dropped party targeted mobs to be considered my drops
added bot option to ignore items with no drop meshes when picking up items
fixed bug loading bot options corrupting trait attribute for saved combat settings

v383
---
updated meta data

v382
---
new datafiles
added NetPing handler
improved CT2.4 action handling
fixed death penalty and souls not working on CT2.4
tweaked freeing stack after return in jingjing scripts

v381
updated process encryption
tweaked locks on SmartTimers to resolve potential deadlocks
refactor of ColorListBoxes to resolve deadlocks
refactor of ChatBox locks and tweaks to performance
added jingjing command UDP_SEND_IP
added jingjing command UDP_SENDBB_IP

v380
new datafiles
fixed off by 4 for checksum

v379
fixed game read thread crash killing L2.Net
removed redundant encryption code

v378
default protocol changed to 146
CLS now clears only the tab currently being viewed
chat boxes now support multiline items with width resize forced on really big items only
chat boxes now resize with application resize
selecting text in chatboxes now copies the selected text to the clipboard (works for multiple lines)
//chat boxes now scroll based on the length of the longest item - redacted

v377
started support for G+
updated process encryption
default protocol changed to 148
refactored player storage to reduce thread contention
refactored pet storage to reduce thread contention
refactored bot option storage to reduce thread contention
refactored alert option storage to reduce thread contention
refactored party storage to reduce thread contention
refactored shortcut storage to reduce thread contention
refactored skill storage to reduce thread contention
changed buff/heals to use skills by name instead of shortcuts
bot options screen now refreshes skills/items properly when opened
removed redundant login code
script command SLEEP 0 advances to the next thread without sleeping the running thread
fixed potential script thread starvation in high load situations
reduced duration a script thread is allowed to run before being switched from 500ms to 1ms (reduces overall latency)
added IS_DEFINED same as VARIABLE_DEFINED
refactored virtual list accessor functions to prevent potential deadlock 
added ?? same as VARIABLE_DEFINED
added conditional ?I is true if the variable is of type integer
added conditional ?D is true if the variable is of type double
added conditional ?$ is true if the variable is of type string
revert: removed \ -> \\ for SYSTEM_CURRENTFILE
fixed excessive cpu usage using script command LOCK
added conditional ?S is true if the variable is of type sortedlist
added conditional ?A is true if the variable is of type arraylist
added conditional ?ST is true if the variable is of type stack
added conditional ?Q is true if the variable is of type queue
added conditional ?FR is true if the variable is of type filereader
added conditional ?FW is true if the variable is of type filewriter
added conditional ?B is true if the variable is of type bytebuffer
added conditional ?W is true if the variable is of type window
added conditional ?C is true if the variable is of type class object
tweaked virtual listviews to reduce excessive updates
changed npc names to default to passed value, on no value passed database object name is used
added jingjing global CHAR_IS_MOVING
added jingjing global PET_IS_MOVING
added jingjing global TARGET_IS_MOVING
jingjing scriptevent SKILL_USED TARGET_ID changed to SKILL_TARGET_ID
jingjing scriptevent OTHER_SKILL_USED TARGET_ID changed to SKILL_TARGET_ID
improved on chat tabs added by toydolls
changed chatboxes for increased performance and greatly reduced memory usage
jingjing dynamic integer values no longer need the #I preface
jingjing user created classes can now have variable names that override built in values and functions
added jingjing object type THREAD [DEFINE THREAD new_thread function_name]
added jingjing handler THREAD.STOP
added jingjing handler THREAD.START

v376
default protocol changed to 87
re-changed npc html quest chat to only work for ct2.3+
re-enabled global packet error handler
re-enabled several invoke/delegate calls
improved support for unknown system messages
improved handling of char info packets for existing chars
added ability to set login listen port independent of login server send port
added ability to override game server ip/port (valid product key only)
added ability to use socks5 proxy for login server connection (valid product key only)
added ability to use socks5 proxy for game server connection (valid product key only)
refactored oog login to remove redundant code
refactored bot -> login server connection
refactored bot -> game server connection
refactored IG game server tunnel to reduce potential delay caused by slow loading clients
tweaked jingjing thread scheduler (increases performance by 2%)
add jingjing class CONSTRUCT function (called when a class is created)
added jingjing conditional IS_LOCKED
tweaked jingjing debug snapshots to be more robust
tweaked chat box printing to help resolve gui freezes
chat box refactored to increase performance by 1500%
npc chat box refactored to increase performance (% varies greatly; generally above 1000%)
fixed bug targeting 5th member in party list
refactored npc cleanup to reduce false positives
refactored npc/player/item cleanup for performance
improved listview sorting performance
refactored npc chat to improve accuracy
rewritten udp subsystem
fixed bug with UDP_SENDBB
fixed bug receiving UDP BB packets

v375r2
fixed bug with previously used skill reuse time when opening the skill list
update text is now in gold

v375
fixed handling of negative reuse timer for skills
fixed crash on setting oop names
oop checkbox now works (previously unchecked acted like checked)
improved error handling on loading skill list
tweaked thread priorities
reduced duration of locks on sorting
player list sorter now stable
npc list sorter now stable
item list sorter now stable
inventory list sorter now stable

v374r2
fixed highly likely gui lockup after log in, teleport and 5min incrememnts
fixed inventory not clearing after sale (receive full item list packet)
fixed npcs, players, items not clearing after cleanup

v374
new data pack v374
now requires .Net 3.5
inventory listview changed to virtual listview
npc listview changed to virtual listview
item listview changed to virtual listview
player listview changed to virtual listview
changed all instances of SelectedItems to SelectedIndices
jingjing command BREAK now supports FOREACH/NEXTEACH
fixed forcing control state when using skills (now correctly uses buff with control setting)
fixed off by 1 error with ExPackets (fixes quest html corruption and screen message memory leak)
fixed bug sending the same BYTEBUFFER more than once with INJECTBB
fixed bug sending the same BYTEBUFFER more than once with INJECTBB_CLIENT
tweaked udp system to better support computers with multiple network adapters
fixed support for skill reuse packet
autofighter buff/heals will no longer be used before the skill is ready regardless of re-use time
using skills from shortcutbar, skill list, auto spoil/sweep, via command line, via scripting will no longer use before the skill is ready
adjusted several internal timers
added player class column to player list (more information available with valid product key)
fixed territory battle text
added jingjing command SKILL_GET_REUSE
re-enabled quest html text for CT2.1 and CT2.2
changed bot options to default to buff without control
all instances of ReaderWriterLock changed to ReaderWriterLockSlim for performance
improved handling of interaction with listviews (mouseover/column click/single click/double click)
clean up now performed after teleport on IG
tweaked warstate handler

v373
updated jingjing documentation
fixed bug with GET_INVENTORY locking the wrong internal list which could cause scripts to crash
forced target for npcs on attack
forced target for npcs on magic skill used
forced target for npcs on magic skill launched
refactored MagicSkillLaunched for better group buff handling
magic skill used now refreshes npc timer for anti-ghosting
magic skill launched now refreshes npc timer for anti-ghosting
added LOOKS_DEAD to NPC and Player StdLib Objects
added IN_COMBAT to NPC and Player StdLib Objects
added support for encrypted map image files
chat messsages are now cached and batch proccessed every 500ms (time stamps on messages are correct... but there may be a slight delay before they post to the chat window)
chat box refactored to increase performance by 40%
fixed smart timers called multiple times no longer delaying/preventing the actual timer firing
changed npc html quest chat to only work for ct2.3+
reduced number of columns in inventory listview
reduced number of columns in skill listview
reduced number of columns in npc listview
reduced number of columns in player listview
reordered columns in item listview
fixed l2j bug where server sending an invalid user info packet could corrupt the bot
refactored texture loading to decrease performance hit when resizing or loading new maps
jingjing command DISTANCE accurancy improved
added distance as value on mouseover for players, npcs, items
item x,y,z changed to floating point to match players and npcs and to reduce number of casts done during animating
tweaked add player and npc
added start/stop script to taskbar icon
added force log to taskbar icon
fixed bug with auto pickup that would ignore items after waiting inc delay rather than full delay
added inventory count/limit to inventory screen
added STRING.REVERSE
refactored and fixed war state handling
refactored screen message handling

v372
banned helaine key
fixed UDP_SENDBB improperly labeled Script_UDP_SENDBB
added Delete Object handler for Inventory items in case server sends object packets instead of inventory updates
changed inventory update with quanity 0 to force delete
added additional mouse over text for players
added additional mouse over text for npcs
added additional mouse over text for items
added mouse over text for inventory
added mouse over text for skills
existing accessory slot now clears when removed
fixed jingjing custom class object .CLONE (also fixes using class objects inside other classes)
extra cases added for jingjing filereader (file extensions and relative paths are now supported)
reverted ReadToken to an older version to resolve variable operators not handling (358_new_stuff.l2s runs correctly again)
jingjing classes with no set base type now presumed to inherit from NULL
fixed run speed returned with GET_PLAYERS, GET_PARTY and GET_NPCS
fixed improper casts to int64 with GET_PLAYERS
fixed improper casts to int64 with GET_PARTY
fixed improper casts to int64 with GET_NPCS
fixed improper casts to int64 with GET_INVENTORY
fixed improper casts to int64 with GET_PARTY
fixed significant number of improper casts to int64 with jingjing events

v371
added jingjing command MOVE_WAIT
added jingjing command MOVE_SMART
added bot option move before attack
added bot option move range
changed bot ai thread to move smartly to target (if selected)
tweaked bot ai thread to prevent skill usage on target acquired to buff/heal
tweaked bot ai thread function priorities and call order
tweaked bot ai restoring empty state after changing target
tweaked bot ai determining when to attack target
tweaked ig login code for ct2.3
tweaked oog login code for all chronicles
added slight delay to oog login to better simulate real client
fixed bug with pet movement clearing the wrong state when finished moving
improved close client
improved system message handling
added gold text for territory chat
added support for unknown message channels
added jingjing command BLOCK_CLIENT
added jingjing command BLOCKEX_CLIENT
added jingjing command UNBLOCK_CLIENT
added jingjing command UNBLOCKEX_CLIENT
added jingjing command CLEAR_BLOCK_CLIENT
added jingjing command CLEAR_BLOCKEX_CLIENT
added SCRIPTEVENT_CLIENTPACKET
added SCRIPTEVENT_CLIENTPACKETEX
*added SCRIPTEVENT_PARTYINVITE
*added SCRIPTEVENT_TRADEINVITE
fixed potential bug revealing hidden party member info boxes
refactored add party member
refactored remove party member
fixed potential error clicking party members in the list
tweaked party member parsing
added support for server objects
added support for quest html packets
tweaked html parser
merged animate thread and process data thread to reduce resource contention
merged alert thread and process data thread to reduce resource contention
merged cleanup thread and process data thread to reduce resource contention
adjusted buffers to decrease load time
added sound alert on white chat
added sound alert on private message
added sound alert on friend chat
added \\n resolves to \n in jingjing string parser (\n still resolves to newline)
changed default chronicle on invalid command line param to ct 2.3
refactored Force Log (part of Kill Threads)
updated Spanish translation (ty escabuchen!)



-Changed debug messages to system filter.

------------------------------------------------------------------------
-fixed bug for TARGET_NEAREST_NAME. The NPCInfo object was not setting the name correctly from the packet. Either the packet contained no data about the npc's name or it was looking in the wrong spot. Changed it so it looks up the npc name via the npcname text file instead.
-Added command line parameters.

Add this to commands.txt or w/e =P
---- New command line parameters ---
-accept (same as -a)
-blowfish (same as -b)
-protocol (same as -C)
-loginport (same as -d)
-loginip (same as -i)
-password (same as -p)
-username (same as -u)
-script (same as -s)
-iglistenport (Same as -x)
-iglistenip (same as -y)
-chronicle (same as -z)

The following command line parameters require valid keys to function:

-overidegameserver enables the gameserver overide 
-useproxylogin enables proxy server for login connections 
-userproxygame enables proxy server for game connections 
-gameserverip ip address of gameserver 
-gameserverport port of gameserver 
-socks5ip ip address of the socks proxy server 
-socks5port port of the socks proxy server 
-socks5username username for socks proxy 
-socks5password password for socks proxy

added astar.cs, astarnode.cs pathmanager.cs
added pathmanager to gamedata (with locks)
added locks to Astar object
fixed bug in wall drawing code, changed wall color so its not equal to boundsbox color
fixed some minor usability bugs in MOVE_SMART
A* pathfinding now works. Requires testing.

-Fixed bug where SYSTEM_CURRENTFILE global would dereference with \n as a newline character, thus causing scripts that exist in directories with \t \b \n etc to not work correctly
-Added MOVE_INTERRUPT command which stops a smart move from any Thread
-Added a new SET_TARGETING and CHECK_TARGETING command called PATHFINDING which allows mobs that do not have paths to not be selected for targeting.

minor bug fixes to A*


v370
*continued work on pets
updated jingjing documentation - partially (missing new 370 stuff and WINDOW stuff)
added option to ignore party members for beep on clan name
added option to ignore party members for beep on player name
commander chat color changed from red to salmon
refactored jingjing label caching
refactored jingjing sub caching
refactored jingjing function caching
fixed text prefaced with - from IG client now correctly handled by L2.Net
refactored jingjing file loading
added jingjing command /*
added jingjing command */
added jingjing command GET_SKILL_DESC1
added jingjing command GET_SKILL_DESC2
added jingjing command GET_SKILL_DESC3
added jingjing command GET_ITEM_DESC
fixed jingjing command CLAN_GET_NAME
started adding support for pets
added jingjing global PET_NAME
added jingjing global PET_TITLE
added jingjing global PET_X
added jingjing global PET_Y
added jingjing global PET_Z
added jingjing global PET_DESTX
added jingjing global PET_DESTY
added jingjing global PET_DESTZ
added jingjing global PET_MAX_HP
added jingjing global PET_MAX_MP
added jingjing global PET_MAX_CP
added jingjing global PET_MAX_LOAD
added jingjing global PET_MAX_FED
added jingjing global PET_CUR_HP
added jingjing global PET_CUR_MP
added jingjing global PET_CUR_CP
added jingjing global PET_CUR_LOAD
added jingjing global PET_CUR_FED
added jingjing global PET_RUN_SPEED
added jingjing global PET_WALK_SPEED
added jingjing global PET_ATTACK_SPEED
added jingjing global PET_ATTACK_SPEED_MULT
added jingjing global PET_CAST_SPEED
added jingjing global PET_ID
added jingjing global PET_TYPE
added jingjing global PET_NPCTYPE
added jingjing global PET_FORM
added jingjing global PET_TARGETID
added jingjing global PET_RUNNING
added jingjing global PET_LOOKS_DEAD
removed extra refresh on .SET_FILENAME for WINDOWS of type HTML
removed extra refresh on .SET_HTML for WINDOWS of type HTML
added jingjing command .REFRESH for WINDOWS of type HTML
added Thai language (ty NuNui!)

v369
*started pets
refactored chronicle code to fix and prevent future errors
added /mybirthday
added jingjing command CLAN_GET_ID
added jingjing command CLAN_GET_NAME

v368
fixed corrupted char selection list for CT2.3
fixed drop items for CT2.3
fixed crystalize items for CT2.3
fixed delete items for CT2.3
added Serverside Dump Mode
added jingjing global SYSTEM_CHRONICLE
updated Finnish translation (ty aasi888!)

v367
added CT2.3 support
resolved potential error calling subs when the line number has not been cached yet
resolved potential error calling functions when the line number has not been cached yet
resolved potential error calling labels when the line number has not been cached yet
resolved potential error calling subs in an external file where a sub of the same name exists in the current file returning the wrong value
improved system message handling
tweaked clan crest handling
updated Russian translation (ty FeTiD!)
added Vietnamese translation (ty Dude!)
added Finnish translation (ty aasi888!)

v366
improved output messages and error messages
refactored IG login for stability
fixed potential for login threads to get out of sync and fail silently (probably only possible on quad core and higher boxes)
fixed -2 protocol error
refactored GG packet handling
fixed bug with jingjing where variables with the same name but different cases can be created causing potential crashes later on
added messages about deprecated jingjing commands
fixed excessive cpu usage on login failures
improved SystemMessage error handling

v365
item quanities and enchants now update correctly in the display list
PartyMembers changed to SortedList for performance
party members now drawn in OrangeRed on the map
invincible npcs now drawn in SkyBlue on the map

v364
fixed npc chat not auto opening
fixed crash on npc chat using ' instead of " to bound links
refactored inventory updating for correctness
added /allblock
added /allunblock
added /allyinfo
added /siegestatus
added /withdraw
added /clanwarstart [name]
added /clanwarstop [name]
added /duel [name]
added /partyduel [name]
clicking on map priority changed: item > npc > player
changed self to red square
targeting self now draws a filled square

v363
adjusted timeouts
fixed combat trait ComboBox/Label weirdness
set default parameters for bot options trait/conditionals
fixed removing t-shirt doesn't update equipment display
heavily optimized clan/ally info and clan crests for performance
changed DEFINE to allow empty default values

v362
new splash/icon
npc dialog auto opens the NPC Chat tab
improved map culling
further tweaked thread priorities
tweaked all network connections for stability and speed
added command line paramter -z to set chronicle (7/8/9/10)
changed default protocol to 17
fixed excessive cpu usage mixing SLEEP and LOCK
updated Japanese translation (ty gyo!)
force upper invariant on .REMOVE for sortedlist keys
force upper invariant on .CONTAINS_KEY for sortedlist keys
moved some gui stuff around
added TIMESTAMP script variable to SCRIPTEVENT_UDPRECEIVE, SCRIPTEVENT_UDPRECEIVEBB, SCRIPTEVENT_SERVERPACKET, SCRIPTEVENT_SERVERPACKETEX
tweaked removal to relieve some resource contention
tweaked listviews for performance
refactored InCombat check
adjusted packet buffers
fixed bug with script command BOTSET ACTIVEFOLLOW_TARGET
tweaked gui colors
fixed limited blowfish key length
changed IG Login Server listener to use same port as destination login server

v361
reorganized gui
added system messages to compliment sound alerts and auto logout
added caching to map texture loading to decrease latency crossing boundaries
improved error handling on sortedlist jingjing debugger snapshots
network thread priorities adjusted to reduce latency
gui thread priority adjusted to prevent "lock ups"
opening inventory tab requests inventory update
opening skill tab requests skill list update
map is continuous
added setup option to adjust view range
added Create Char

v360
fixed bug with []
fixed = assignment to persist types for INT, DOUBLE, STRING
fixed can't use . operators needing additional parameters with = assignment
fixed STRING.SUBSTRING
fixed STRING.REPLACE
fixed STRING.REMOVE
changed HTML windows to refresh after updating
disabled priority protect
refactored targeting settings to prevent the autofighter and the script engine from stomping on each other's settings
added targeting option radio buttons to bot options (previously could only be set via scripting)
fixed auto logout options not refreshing correctly
changed messagebox max length to 32767 (messages sent to the server are cropped to 128 in length)
updated Chinese translation (ty wfrancis!)
refactored token parsing
sound alerts, logout settings, and targeting settings now save/load from bot option files
changed block/ex to allow server packets to be handled by jingjing events
added jingjing global SYSTEM_VERSION
added setup option to adjust texture filtering

v359
added /instancezone
fixed add of existing fucking up players and npcs
tweaked oog login screen
tweaked popup window anchors

v358
fixed pet names corrupting party display
tweaked anti-ghosting
fixed SKILL_GET_ID to not be case sensitive
added jingjing command BLOCK
added jingjing command BLOCKEX
added jingjing command UNBLOCK
added jingjing command UNBLOCKEX
added jingjing command CLEAR_BLOCK
added jingjing command CLEAR_BLOCKEX
added variable.CLONE
fixed jingjing class member of type ARRAYLIST being forced to static
fixed jingjing class member of type SORTEDLIST being forced to static
fixed jingjing class member of type STACK being forced to static
fixed jingjing class member of type QUEUE being forced to static
FILEREADER, FILEWRITER and WINDOW are still forced to static
fixed case issue with class member names
fixed SUB and CALL getting incorrect cached line numbers across files with duplicate names
fixed inability to call non overridden parent class functions
added THIS.BASE for calling parent class functions
fixed bug with BYTEBUFFER.INDEX
added new jingjing variable type WINDOW
added WINDOW subtype CMD
added WINDOW subtype GUI
added WINDOW subtype HTML
added WINDOW subtype GDI
added WINDOW.RUN_WINDOW
added WINDOW.CLOSE_WINDOW
added WINDOW.SET_NAME
added WINDOW.SET_FILENAME
added WINDOW.SET_HTML
added WINDOW.STANDARD_IN
added WINDOW.WAIT_CLOSE
added WINDOW.WAIT_IDLE
added support for ChangeMoveType
added support for ChangeWaitType
added jingjing global WALKING
added jingjing global RUNNING
added jingjing global SITTING
added jingjing global STANDING
added jingjing global START_FAKEDEATH
added jingjing global STOP_FAKEDEATH
added jingjing global CHAR_RUNNING
added jingjing global CHAR_SITTING
added jingjing global TARGET_RUNNING
added jingjing global TARGET_SITTING
added jingjing global ALIVE
added jingjing global DEAD
added jingjing global CHAR_LOOKS_DEAD
added jingjing global TARGET_LOOKS_DEAD
changed DUMP output to preface with DUMP instead of DEBUG
added setup option to send blank gg replies to unknown queries
added setup option to always hide npc dialog window
fixed using actions or items via buff/heals or combat settings and getting stuck in a loop
tweaked closing connections on logout
sound alert screen removed
sound alerts moved to bot options
bot options forced as top most
added close button to bot options
apply no longer closes bot options
removed eula popup
tweaked startup text
added Help -> Eula
setup moved to file
save interface moved to file
Options changed to Bot Options
tweaked kill threads to clear nearby stuff
tweaked clear self to clear clan and player info
tweaked UI packet handler
tweaked CI packet handler
added INT.GET_HEX
added DOUBLE.GET_HEX
added STRING.GET_HEX
added BYTEBUFFER.GET_HEX
added STRING.CONTAINS
added STRING.STARTSWITH
added STRING.ENDSWITH
added STRING.LASTINDEXOF
added STRING.INDEXOF
added STRING.TRIMEND
added STRING.TRIMSTART
added STRING.REMOVE
added STRING.SUBSTRING
added STRING.REPLACE
removed jingjing command MATH
removed jingjing command SET
added jingjing command =
added = operator && for INT and DOUBLE
added = operator || for INT and DOUBLE
added = operator & for INT
added = operator | for INT
added = operator << for INT (cast to INT32)
added = operator >> for INT (cast to INT32)
added = operator ~ for INT
added = operator XOR for INT
added INT.GET_HEX32
refactored Item Listview for performance and accuracy
refactored NPC Listview for performance and accuracy
refactored Player Listview for performance and accuracy
changed Inventory list to sortedlist for performance
refactored Inventory Listview for performance and accuracy
tweaked Inventory images for performance
tweaked active follow ID caching
tweaked auto accept party invite
tweaked auto accept rez
tweaked clan name based sound alerts
tweaked player name based sound alerts
added bot option oop players (list of players considered in party when targeting)
added bot option priority protection (target npcs attacking party members, oop members before targeting other npcs; ignores all targeting conditions and ignore values; works with all TARGET_NEAREST commands
changed regular npc targeting to ignore all targeting conditions and ignore values if the npc is targeting the player
removed TARGET_NEAREST_ID ability to target based on object ID; use TARGET instead
tweaked clearing nearby data (restart/teleport) for stability
tweaked clearing self data (restart) for stability
eliminated need to give a local port
fixed broken icon images in datapack
factored out and removed crest thread
updated Italian translation (ty kalup!)
refactored timers to use new SmartTimer
merged 2 image lists
refactored assigning clan crests for performance
tweaked select to generate mouse over text for players, items, npcs
tweaked GET_PARTY to support far away and unknown members
fixed bug with bot options item combobox and fewer items than previous selected index
fixed non thread safe BOTSET commands
changed auto buff/heal to not retarget if it already has the target targeted
changed to prevent using skills while dead (items were already blocked)
fixed non thread safe script events
added command /FORCELOGOUT
added auto logout bot options
changed dynamic ints to require #I preface
changed dynamic doubles to require #D preface
changed dynamic strings to require #$ preface
updated stdlib and test scripts
removed nested locks on animate
fixed party list not clearing on reset
fixed inability to login IG multiple times
fixed summoning not working
change auto accept rez to auto accept summon as well
improved error handling on delete/crystalize/drop items
added toggle botting to minimize to tray right click menu
added bot options to minimize to tray right click menu

v357
tweaked autospoil and sweep
changed sortedlist keys to be forced to upper_invariant
adjusted script error reporting to include thread id
added duplicate key error checking for SORTEDLIST.ADD
added error checking to debugger snapshots
debugging snapshots now pause jingjing if it isn't already paused; last state is restored after snapshot is complete
added additional thread safety to jingjing threads
tweaked stop script
fixed error calling functions with dynamic variables
fixed error calling subs with dynamic variables
fixed error calling jump with dynamic variables
fixed error loading classes with dynamic variables
fixed potential error with SWITCH/BREAK and dynamic variables
fixed potential error with WHILE/WEND and dynamic variables
fixed potential error with LOOP and dynamic variables
fixed potential error with FOREACH/NEXTEACH and dynamic variables
fixed potential error with FOR/NEXT and dynamic variables
fixed potential error with IF/ELSE and dynamic variables
CHAR_GET_ID now correctly checks party members
CHAR_GET_NAME now correctly checks party members
/PLAYERLOC now correctly checks party members

v356
added [] same as VARIABLE_DEFINED
fixed login crash on certain servers
added /charm
added /nick
added command line parameter -s to set script file
tweaked autofighter

v355
added buff/item/combat conditional: Charges
added buff/item/combat conditional: Souls
added buff/item/combat conditional: Death Penalty
added script variable CHAR_CHARGES
added script variable CHAR_SOULS
added script variable CHAR_DEATH_PENALTY
added script variable ENV_PATH
added IF conditional VARIABLE_DEFINED
added SORTEDLIST.CONTAINS_KEY
added BYTEBUFFER.INDEX
added bot options: buffs/heals -> Right Click -> Move Up
added bot options: items -> Right Click -> Move Up
added bot options: combat -> Right Click -> Move Up
added display party member level
refactored my_char.my_skills
tweaked anti-ghosting

v354
updated Japanese translation (ty gyo!)
updated Spanish translation (ty theonn!)
tweaked locks
fixed "bad target" on target far away party members
fixed bug with NEAREST_NPC_ID
tweaked clear on reset
script debugger shows keys for sortedlist objects
script debugger shows indexes for arraylist objects
script debugger display bytebuffer values
script debugger displays globals
script debugger displays locks
script debugger displays additional thread information
updated process encryption

v353
fixed broken OOG login
added 22 new translatable terms
fixed translated terms not set everywhere
added sortedlist and arraylist .REMOVE
added sortedlist .REMOVEAT

v352c
new splash
popup code completely removed
new map
added Map Z Range to map settings
fixed resize after KillThreads
refactored IG login
refactored OOG login
updated authlogin to support CT2.2
updated items to support CT2.2
tweaked window title
tweaked form layout
refactored to better seperate logic from forms
kill threads is now nuclear
tweaked UDP system for performance (oop heals)
added npc update support to UDP system
changed to clear npc targets on death
changed target_nearest_id to work with both type and unique id for npcs
added SCRIPTEVENT_SYSTEMMESSAGE
added MESSAGETYPE to SCRIPTEVENT_SYSTEMMESSAGE
added script command TARGET
added script command SKILL_GET_ID
added script command SKILL_GET_NAME
added script command CHECk_TARGETING
added script command DELAY (same as SLEEP)
changed script command SET_TARGETING to not be case sensitive
added BYTEBUFFER.RESET_INDEX
fixed several bugged BYTEBUFFER operations
fixed bug with NEAREST_*
added option to override invalid protocol on IG login

v351c
refactored IG login
refactored OOG login

v350c
regular n-gons rotated by 180 degrees
fixed excessive cpu usage when all script threads were sleeping
fixed accept party, accept party names, accept rez, accept rez names, ignore items not updating on bot options screen
fixed bug with . and bytebuffer objects
updated about screen
tweaked IG login delays
added additional output for -2 protocol error
refactored to better seperate logic from forms

v349c
upgraded project to VS2008
refactored to better seperate logic from forms
added regular n-gon generator to bounding polygon setup
script engine speed unlocked
tweaked script engine string parsing for performance
tweaked script engine variable access for performance
tweaked script engine . operators for performance
fixed bug with script != and string objects
fixed excessive cpu usage while scripts are paused
fixed BYTEBUFFER.WRITE_BYTE
overloaded .COUNT and .LENGTH to match each other

v348c
added script global CHAR_ID
fixed bug calling class functions without CALL

v347c
epic new splash
new datafiles to fix broken icons
added new bot options tab "Autofighter"
added Z Range to Bounding Polygon Bot Options screen
added ability to target far away party members
tweaked using auto sweep
added column click sorting to Clan Info
tweaked all column click sorters for stability and performance
fixed bug with clan names
fixed bug with ally names
optimized string comparisons
refactored script error reporting
improved error message on failed decrypt
factored out reset script
changed /SPAUSE to pause the running script instead of stop
added /SSTOP to stop the currently running script
changed script command PAUSE to make jingjing enter the paused state (instead of stopped state)
changed Pause/Resume in the script debugger to enter the paused state (instead of stopped state)
fixed script bug adding variables to sorted list
fixed bug dereferencing values in '.' operation chains
fixed bug with public class containers and .COUNT
fixed bug skipping 1 script line on function call without CALL
fixed bug releasing reader lock on party members
fixed incorrect lock on GET_PARTY
added TARGET_BLUE to SCRIPTEVENT_TARGETDIE
added queue_test.l2s

v346c
fixed defective system message parser
improved script conditional handling (IF/WHLE/LOOP)
updated if_test.l2s
added alphabetical order on items in bot options
added botoption pickup
added botoption pickup range
added botoption target
added botoption attack
changed bot target option combat to default to two
changed Always heal/buff condition to ignore dead targets
changed HP heal/buff trigger to ignore dead targets
changed MP heal/buff trigger to ignore dead targets
changed CP heal/buff trigger to ignore dead targets
added BOTSET LOOT_RANGE
added BOTSET ACTIVE_ATTACK_ON
added BOTSET ACTIVE_TARGET_ON
added BOTSET PICKUP_ON
added script global NEAEREST_PLAYER_ID
added script global NEAEREST_NPC_ID
added script global NEAEREST_ITEM_ID
added script command IGNORE_ITEM
added script command INJECT_CLIENT
added script command INJECTBB_CLIENT
added script command PLAYWAV
update CLICK_NEAREST_ITEM to use ignore state
update NEAREST_ITEM_ID to use ignore state
update NEAREST_ITEM_DIST to use ignore state
tweaked HennaInfo packet handler
tweaked Die packet handler
tweaked set target packet handler
tweaked buffstate internal handling
tweaked bufftrigger internal handling
refactored MoveStuffThread into AnimateThread and BotAIThread
refactored ushort and byte types to uint
added command /GODMODE

v345c
degraded mode 5 second skill reuse limit removed
degraded mode 5 second item reuse limit removed
improved error message for crash on startup
updated Romanian translation (ty darkwrathn!)
tweaked process names and descriptors
fixed bug with BREAK and nested RETURN, RETURNSUB
added column click sorting to Player Skills
added column click sorting to Player Inventory
tweaked USE_SHORTCUT
added script global TARGET_CAST_SPEED

v344c
updated process encryption
cleaned up crash image
added Portuguese PT translation (ty Massive!)
adjusted tab order on several screens
adjusted datafile initial capacities
refactoring of project
re-added Direct Input to toggle botting (may need to reassign your toggle key)
added script command NMESSAGE_BOX2
added script global ENV_MACHINENAME
added script global ENV_USERNAME
added support for very large INJECT packets (>1024 bytes)
added SORTEDLIST.GET_KEY VARIABLE
updated foreach.l2s in test folder
added ARRAYLIST.REVERSE
added ARRAYLIST.CLEAR
added SORTEDLIST.CLEAR
added STACK.CLEAR
added QUEUE.CLEAR
refactored script event calling
added SCRIPTEVENT_SERVERPACKET
added SCRIPTEVENT_SERVERPACKETEX
added support for accept/decline trade invite
added decline_trade_test.l2s in the test folder
added BOTSET ACCEPT_INVITE
added BOTSET ACCEPT_INVITE_ON
added BOTSET ACCEPT_REZ
added BOTSET ACCEPT_REZ_ON
added BOTSET HEAL_RANGE

v343b
updated foreach.l2s in test folder
updated class_test.l2s in the test folder
fixed bug resolving '.' and ' ' where ' ' comes before '.'
fixed stack levels not being freed completely

v342b
refactored packet id encryption for CT2
added support for Server Objects
added CT2 self packet handler
added CT2 npc packet handler
added CT2 other player packet handler
added ExShowScreenMessage packet handler
added Setup option Suppress Earthquakes
added support for tapping to fortress
added script global FORTRESS
new splash
pikachu replaced on about screen
removed MATH ADD for ARRAYLIST, SORTEDLIST, STACK, QUEUE
removed MATH READ
removed MATH WRITE
removed MATH CLOSE
removed MATH FLUSH
added function calls without CALL
added ARRAYLIST.ADD VARIABLE and ARRAYLIST.PUSH VARIABLE
added SORTEDLIST.ADD VARIABLE KEY and SORTEDLIST.PUSH VARIABLE KEY
added STACK.ADD VARIABLE and STACK.PUSH VARIABLE
added QUEUE.ADD VARIABLE and QUEUE.PUSH VARIABLE
added FILEREADER.READ VARIABLE
added FILEWRITER.WRITE VARIABLE
added FILEREADER.CLOSE
added FLIEWRITER.CLOSE
added FILEWRITER.FLUSH
added DEFINE BYTEBUFFER name INT_SIZE
added BYTEBUFFER.LENGTH
added BYTEBUFFER.TRIM_TO_INDEX
added BYTEBUFFER.WRITE_UINT16 VARIABLE
added BYTEBUFFER.WRITE_UINT32 VARIABLE
added BYTEBUFFER.WRITE_UINT64 VARIABLE
added BYTEBUFFER.WRITE_INT16 VARIABLE
added BYTEBUFFER.WRITE_INT32 VARIABLE
added BYTEBUFFER.WRITE_INT64 VARIABLE
added BYTEBUFFER.WRITE_DOUBLE VARIABLE
added BYTEBUFFER.WRITE_BYTE VARIABLE
added BYTEBUFFER.WRITE_STRING VARIABLE
added BYTEBUFFER.READ_UINT16 VARIABLE
added BYTEBUFFER.READ_UINT32 VARIABLE
added BYTEBUFFER.READ_UINT64 VARIABLE
added BYTEBUFFER.READ_INT16 VARIABLE
added BYTEBUFFER.READ_INT32 VARIABLE
added BYTEBUFFER.READ_INT64 VARIABLE
added BYTEBUFFER.READ_DOUBLE VARIABLE
added BYTEBUFFER.READ_BYTE VARIABLE
added BYTEBUFFER.READ_STRING VARIABLE
added script command INJECTBB BYTEBUFFERVARIABLE
added SCRIPTEVENT_UDPRECEIVEBB
added script command UDP_SENDBB BYTEBUFFERVARIABLE
tweaked Add_Inventory to better handles duplicates

v341b
tweaked script function calls (working on a major change to base types)
fixed bug with creating events and full paths
decreased chattiness of UDP traffic
fixed bug with UDP_SEND
added script command TARGET_SELF
added script command DELETE
added script command DELETE_GLOBAL
added SCRIPTEVENT_JOINPARTY
added SCRIPTEVENT_LEAVEPARTY
added script global CHAR_PARTY_COUNT
added script global CHAR_ATTACK_SPEED_MULT
added script global CHAR_CAST_SPEED
added script global CHAR_PATK
added script global CHAR_MATK
added script global CHAR_PDEF
added script global CHAR_MDEF
added script global CHAR_ACCURACY
added script global CHAR_EVASION
added script global CHAR_CRITICAL
added script global CHAR_STR
added script global CHAR_DEX
added script global CHAR_CON
added script global CHAR_INT
added script global CHAR_WIT
added script global CHAR_MEN
added script global CHAR_CUBIC_COUNT
added script global CHAR_PVP_COUNT
added script global CHAR_PK_COUNT
added script global CHAR_KARMA

v340b
fixed script ATTACKABLE parameter for NPCS
added script global variable SYSTEM_CURRENTFILE
fixed bug with global variable SYSTEM_THREADCOUNT
added new voice Miu
cleaned up all audio files

v339b
tweaked GUI layout
tweaked map code for performance
tweaked move stuff code for performance
added script command CANCEL_TARGET
made TARGET_NEAREST, TARGET_NEAREST_ID, TARGET_NEAREST_NAME use targetting parameters consistently
fixed map not refreshing on resize
improved DirectX handling of lost devices

v338b
added CT 1.5 option at Login
restored CT 1 functionality
fixed double encryption

v337b
fixed broken IG login on retail

v336b
fixed bug with debug and dump mode output on packets of type EXPacket
refactored MixedPacket en/decryption to support l2j key of 0
added GameGuard reply packet output when Debug Mode is enabled
added new GG.txt in the Data folder
refactored GG packet handlers
fixed bug with RequestQuestList
added TARGETER_NAME to SCRIPTEVENT_SELFTARGETED
added TARGETER_WARSTATE to SCRIPTEVENT_SELFTARGETED

v335b
added CT1.5 client id en/decryption (special thanks to lhdlp!)
fixed invalid function calls with lower case types
updated CharSelected packet handler
added script command MESSAGE_BOX
added script command NMESSAGE_BOX
added script command SEND_EMAIL
added script command TEST_PING
added script command TEST_DNS
added script command TEST_ODBC
added script command TEST_WEBSITE
added GameGuard packet output when Debug Mode is enabled

v334b
CT1.5 support
fixed bug converting \n to newline in scripting
now converts \n to newline in chat box
refactoring of buff code (min mp, timer)
fixed cut off names on map

v333b
fixed bug launching events from other files
fixed bug with SCRIPTEVENT_SKILLLAUNCHED
fixed bug with /script command
added SCRIPTEVENT_OTHERSKILLUSED
added SCRIPTEVENT_OTHERSKILLLAUNCHED
added SCRIPTEVENT_OTHERSKILLCANCELED

v332b
updated German translation (ty Herscher!)
added script command GET_INVENTORY
fixed bug with Script Event UDPReceive
added StdLib base_item.l2c
updated StdLib item.l2c
added StdLib inventory.l2c
added SCRIPTEVENT_SKILLUSED
added SCRIPTEVENT_SKILLLAUNCHED
added SCRIPTEVENT_SKILLCANCELED

v331b
fixed bug not setting target types for other players/npcs
fixed bug not clearing move target id/type on stop move
fixed bug not clearing move target id/type on stop move start combat
fixed bug allowing movement to deleted objects

v330b
fixed bug with walker style follow and moving
changed walker style follow to ignore follow to objects when active follow attack is off

v329b
tweaked active follow targeting
tweaked stop move handler
tweaked teleport handler
tweaked move stuff thread conditionals for clarity
changed if Shift is checked, clicking on the map will only target and not move

v328b
added support for clan member update packet
added support for clan member delete packet
added support for clan list delete packet
fixed bug with clan online count
fixed bug with accessing variables using dot . more than 1 level deep
tweaked move to target packet handler
tweaked move to pawn packet handler

v327b
revamped data decoder
switched exe to 32 bit only (still runs on 64 bit oses)
added additional error reporting on startup
fixed disabled audio
virtually removed script engine limiter
removed GDI+ map code
re-instated Managed DirectX map code (required March 2008 directx or newer)

v326b
fixed buffer overflow in string conversion
resolved issue with $ dynamic variables and class operator .

v325b
fixed crash on invalid server ids
added script command SCRIPT_END
added support for FOREACH BYTEBUFFER objects
added support for \n in script language
added script event ChatToBot
added script event UDPReceive
added script command UDP_SEND
added event based low level keyboard hook for bot toggle key
fixed 3 potential buffer overflows at startup
updated process encryption
changed meta data
easter/spring contest winner splash

v324b
now allows keys in lower case
updated process encryption
banned lynn burgess key

v323b
fixed npc chat - text links
fixed npc chat - buttons
fixed potential serious bug with . operator (would change variable types)
fixed inablility to call functions at multiple levels of indirection (ex: array.index.function)
now allows spaces and tabs between script command elements
added .Dist to StdLib objects
added File I/O to StdLib ojbects
updated playerlist_test.l2s to use save list to a file

v322b
fixed bug interacting with some npc links
redefined script random (for ints: min <= r <= max, for doubles: min <= r < max)
added script command GET_NPCS
added script command GET_ITEMS
added script command GET_PARTY
added script command GET_PLAYERS
added script type BYTEBUFFER (for future use)
added script command GET_ELIZA
added script command SLEEP_HUMAN_READING
added script command SLEEP_HUMAN_WRITING
edit splash
tweaked bot options screen

v321b
fixed oog l2j login
fixed bug with 0 exp
updated enter world packet
updated gg verify packet
optimized map drawing
changed direct 3d toggle to toggle bilinear interpolation (off = 50% faster map when zoomed in)
decreased memory usage on startup by 10%
tweaked hardcore crash image
new splash

v320b
fixed 2 char limit on bounding coords
adjusted buff/heal/combat mp checks
swapped order of obj_id and war state column in the player list
optimzed updating player list
added npc say support
tweaked listview column sorter
updated Greek translation (ty npittas!)
wrote custom DXT1 -> BMP conversion
optimized remove object
fixed potential crash on remove player
removed unmanaged code to scroll chat box
oog login enabled
updated process protection
removal range decreased by 50%
fixed potential crash on startup on 64 bit systems

v319b
tweaked player/npc attack speed reporting
fix crash on 64 bit windows (xp/2003/vista)
fixed error on non uppercase function variables (now case insensitive)
added ghost removal techniques to items
highly optimized ghost removal (several orders of magnitude faster and more deterministic)
new splash

v318b
fixed bug clicking on npcs in the listview
fixed bug closing windows on kill threads
added setup option white names
greatly decreased columns for player list
greatly decreased columns for npc list
greatly decreased columns for item list
added mouse over text for player list
added mouse over text for npc list
added mouse over text for item list
added /deadcount

v317b
ghost removal range decreased by 50%
added ghost removal techniques to players
added bot option ACTIVE_FOLLOW_TARGET
added Cancel Target follow when active follow target is on
fixed bug with player moving to NPC
fixed bug with player moving to item
fixed bug with NPC moving to item
nearby_player array list converted to sorted list
nearby_npc array list converted to sorted list
nearby_item array list converted to sorted list
changed auto spoil to off by default

v316b
tweaked assist on no target
tweaked npc ghost removal
fixed bug sorting players by column
tweaked splash loader
improved launch crash reporting
added launch crash image

v315b
fixed bug with adding inventory
fixed bug with adding items
fixed bug with shirts
fixed bug clicking players from the list
tweaked clicking npcs from the list
tweaked clicking items from the list

v314b
tweaked adding npcs
tweaked adding items
tweaked adding players
tweaked adding inventory
refactoring of most constructors

v231b
v313b
tweaked active follow attack targeting
tweaked active follow attack attacking
tweaked setting npc max hp, mp, cp
decreased npc remove dead at timer
tweaked kill threads
added commands -> force log
added script command FORCE_LOG

v312b
refactoring of data queue thread locks
refactoring of error handling to yield more useful errors
clicking npc listview column names will sort now
clicking item listview column names will sort now
fixed bug with setting active follow on get new char info
added clean up functions to remove old dead monsters

v311b
combat settings tweaked for performance
fixed party display corruption
fixed pop up bug with valid product key

v310b
consolidated stop move start attacking
tweaked magicskilluse packet handler
added ability to toggle npc names individually
added ability to toggle player names individually
added ability to toggle item names individually
added script BOTSET CLEAR_COMBAT
added combat settings to bot options
fixed bug with TARGET_ script variables when targeting yourself

v309b
random made more random
adjusted tab order on bot options
added bounding polygon to bot options
fixed command all channel text color
tweaked attack packet handler
tweaked magicskilllaunched packet handler

v308b
fixed small issue with the Chinatsu voice
fixed inability to buff while moving

v307b
decreased failed buff reuse timer from 20 to 5 seconds
fixed move to pawn - item
rewritten movement processing
packet error message now gives the previous packet as well
on error the text file is flushed
random sound and scripting made more random
reimplemented sound on recieve mail
added new voice Chinatsu
new splash

v306b
toggle botting now turns the script on and off
tweaked inventory info packet
added accessory to inventory window
added shirt to inventory window
updated process protection
tweaked ability to run scripts unconnected to prevent crash when running in degraded mode
added error handling to launch lineage 2
fixed bug with shortcut id/page updating buffs/heals
rewritten buff system internals
large revamp of thread locking mechanisms

v305b
adjusted war state handler
fixed in combat detection for players
tweaked movement thread locking mechanisms
fixed bug with DELETE_ITEM script command
fixed bug with DROP_ITEM script command
fixed bug with CRYSTALIZE_ITEM script command
script random adjusted to be more random
significant work done to smooth out movement

v304b
tweaked failed buff handling
fixed bug with loading player lists
added -1 way war sound alerts
fixed char info packet
now displays player war state
implemented update buff/heals button
implemented update items button

v303b
fixed bug with script command TAP_TO
npc text is now properly html decoded
fixed bug when buffing self
fixed bug with min timer on items
test fix for weirdness with Auto Follow (the running away stuff)
added support to regain active follow if the target changes id or isnt there when active follow is first set
replaced 85 instances of ToUpper with ToUpperInvariant
added bot status to Overlay window
keyed users can now run scripts without being connected to the game server
added Party Accept window now disappears if you join a party from clicking accept in the client

v302b
fixed potential bug with declaring variables with non US systems - use the invariant scheme when declaring variables; decimal = . dot; digit grouping seperator  = , comma;
added script commands FOREACH, NEXTEACH
added script command SAY_TO_CLIENT
further decreased memory usage while loading by 90%

v301b
fixed bug loading skills datafile

v300b
cleaned up splash screen
C7 support
fixed crash casued by skill names in system messages
changed direct 3d background color to white

v230b
fixed localization fuck ups

v229b
fixed Danish language
now displays protocol when connecting via IG
added command line parameters
tweaked interface slightly to better match the client

v228b
added Danish language (ty johndoe!)
decreased memory usage while loading by 50%
tweaked splash slightly
changed auto accept party/rez to work on OOG
massive overhaul of packet system in preperation for CT1

v227b
updated Spanish translation (ty Dr.TroNs!)
updated BR translation (ty cybersick!)
added Swedish translation (ty ugroks!)
tweaked xp % shown - should show 4 decimal places now
updated process protection
improved error messages on loading
tweaked loader to decrease disk io by 90%
added 24 more localizable strings
changed popup to not halt process
added auto accept party invite
added auto accept rezz
tweaked self update packet handler
tweaked player update packet handler
tweaked npc update packet handler
changed meta data
new splash

v226b
refactoring of targetless buffs to ignore the current target (target name must still be within default range of 550)
changed map - players/npcs are drawn actual size
changed map - added ability to click on players/npcs/items from the map
added mode descriptor to about screen
added degraded mode - min of 5000ms reuse timer on items
added ability to switch between Windows Sound and Direct Sound
added ability to switch between GDI+ and Direct 3D for map drawing
fixed bug with script events
tweaked event locking code
tweaked sound options screen anchors
added script thread/stack/variable basic debugger

v225a
adjusted buttons on language select (again... dunno why it broke again after i fixed it)
added alt + f for file (and some others)
fixed bug where players would turn black with show names off
fixed bug with TARGET_* where target = self
added script global CHAR_RUN_SPEED
added script global CHAR_WALK_SPEED
added script global CHAR_ATTACK_SPEED
added script global CHAR_EVAL
added script global TARGET_RUN_SPEED
added script global TARGET_WALK_SPEED
added script global TARGET_ATTACK_SPEED
added script global TARGET_EVAL
changed bot buff/heal canceled to use the MagicSkillCanceled packet instead of reading the system messages
changed - you can declare more than one VAR_START - VAR_END block per class now
added script variable THIS when calling class functions/accessing class variables
added PUBLIC class member functions
added PRIVATE class member functions
added botoption Ignore Unknown Items
refactoring of map code to decrease complexity
refactoring of script function calling to support classes
edited about screen

v224a
tweaked code for when your target dies
added botoption Auto Spoil
added botoption Spoil Crush
added botoption Auto Sweep
fixed so that registering shortcuts after init will work properly
added support for skill use delay timers (won't try to spoil early anymore)
fixed bug with DoNot items and npcs
tweaked player/npc update handler
adjusted buttons on language select
updated all menus to .net 2.0
fixed bug with minimize to tray
added version and player name to tray icon mouseover text
added Commands -> Kill Threads

v223a
refactoring of script memory management
added script variable FILEWRITER
added script variable FILEREADER
added script variable ARRAYLIST
added script variable SORTEDLIST
added script variable STACK
added script variable QUEUE
added script variable CLASS
added script variable PUBLIC
added script variable PRIVATE
added script variable PROTECTED
added script command INCLUDE
added .POP for Stacks
added .POP for Queues (Dequeue)
added .PEEK for Stacks
added .PEEK for Queues
added .int for Arraylist
added .string for Sortedlist
increased max length for Lineage 2 path
increased size of the key selection drop down box
added DoNot Items to bot options
added DoNot NPCs to bot options
added MATH ADD operator for ArrayList
added MATH ADD operator for SortedList
added MATH ADD operator for Stack (Push)
added MATH ADD operator for Queue (Enqueue)
added script command CLASS
added script command END_CLASS
added script command VAR_START
added script command VAR_END
added script command PUBLIC
added script command PRIVATE
added script command PROTECTED
added .TYPE for all variable types
added .CLASSNAME for variable types
new splash

v222a
added Lineage 2 Path as a setting in Options -> Setup
tweaked tiny packet messages
tweaked script event thread handler
plays a sound when you get mail
fixed toggle botting unchecked but enabled at program launch
added double click party member names to target them
added background color of level changes based on bot status (green = on, red = off)
added script global: SHORTCUT_ITEM
added script global: SHORTCUT_SKILL
added script global: SHORTCUT_ACTION
added script global: SHORTCUT_MACRO
added script global: SHORTCUT_RECIPE
added script command USE_SHORCUT
added script command DELETE_SHORTCUT
added script command REGISTER_SHORTCUT
added Direct Input to toggle botting (default value is Grave ` )

v221a
fixed bug checking MP with scripts
fixed broke <br1> handler
fixed bug with tat handler
fixed need target set to false should work properly now
added support for augmented items to system messages
added support for 64 bit ints to system messages
added support for TARGET_* when targeting self
added buff condition: Dead
added File -> Launch Lineage 2
added ability to disable script file i/o in options -> setup

v220a
fixed bug with script command USE_ITEM
added script command USE_ITEM_EXPLICIT
added command /GETSKILLS
tweaked status messages slightly
fixed some catches on loading (should speed up loading)
optimized number conversion by 300% (this function is called 300,000 times at startup)
optimized string parsing by 200% (this function is called 100,000 times at startup)
moved chatbox length check -> clear to before line printing

v219a
updated OOG login packets to emulate GG 1057
updating debug logging to facilitate packet changes
further updated OOG packets to make them consistent with the current C6 client
further tweaked client/server network threads to decrease latency
tweaked map location slightly
several small performance tweaks

v218a
changed broadcast packets for OOP healing to unicode for character names
refactoring of login process to improve consistency between OOG and IG, increase stability, and support RESTART
tweaked client/server network threads to decrease latency
fixed crash related to clan based sound alerts
fix bug with script command NPC_GET_ID
fix bug with script command ITEM_GET_ID
added script command INVEN_GET_ITEMID
added script command INVEN_GET_UID
added script command CHAR_GET_ID
added script command CHAR_GET_NAME
fixed potential thread lock with removing inventory
fixed potential thread lock with removing items
fixed potential thread lock with removing npcs
fixed potential thread lock with removing players
fixed several other potential thread locks
doubled default buff item/heal list size (they can grow to larger)
fixed bug with setting item ids to use via bot options (bot option files pre 218 will no longer work properly)
fixed /unstuck command
added click in tab to refresh skill list

v217a
fixed bug with accepting rez
added script command STRING.To_UPPER
added script command STRING.To_LOWER_INVARIANT
added script command STRING.To_UPPER
added script command STRING.To_LOWER_INVARIANT
added script command STRING.TRIM
added script variable type STRUCT
new splash screen

v216a
changed all instances of installed culture in 215 to the invariant culture (comma to separate digit groups, period to separate decimal)
changed <& &> to convert INT and DOUBLE using the invariant culture
changed all MATH operations to use the invariant culture
(another) major refactor of script variable parser
major refactor of the script MATH command
added script command STRING.LENGTH
added script command PRINT_DEBUG
changed /" to \" to represent a quote in a string
tweaked meta data

v215a
changed utility decimal remover function to strip of the Number Group Separator specific to the installed culture (ex: comma in USA English, period in Dutch, space in French)
changed utility decimal remover function to use the Number Decimal Separator specific to the installed culture (ex: period in English, comma in Dutch, comma in French)
changed string -> double to use culture settings
changed string -> int64 to use culture settings
changed string -> uint64 to use culture settings
changed string -> int32 to use culture settings
changed string -> uint32 to use culture settings
tweaked packet handler: SystemMessage
fixed bug with BREAK not decrementing count on internal loops
added script command SWITCH
added script command ENDSWITCH
added script command CASE
added script command DEFAULT
fixed bug introduced in v214 preventing double click (Target)

v214a
added command /target name
added command /attack (uses control/shift checkboxes)
added support for attack shortcut (uses control/shift checkboxes)
pointers to pointers can now be implemented in scripting ex: <&<&arrayname&><&index&>&>
added error checking on sound engine init
changed sound errors to not play sounds to prevent circular errors
added packet handler: PledgeShowMemberListAdd
added packet handler: PledgeStatusChanged
tweaked packet handler: PledgeShowInfoUpdate
tweaked packet handler: PledgeShowMemberListAll
tweaked packet handler: PledgeInfo
tweaked packet handler: SystemMessage
changed to support both . and , as decimal delimeters
removed support for , where 1,000 = 1000
fixed potential cross thread errors on nearby player/npc rez
fixed some issues with control and shift (the checkboxes have meaning in more functions now)
fixed bug with healing/buffing purple/red chars
refactoring of script variable engine
added support for implied variables prefaced with i, d, and $
fixed several potential typecast errors with IF evaluation
added IF string condition CONTAINS
added IF string condition STARTSWITH
added IF string condition ENDSWITH
fixed bug with MATH MOD and % operator
massive refactoring of MATH handler
added MATH operator REPLACE
added MATH operator TO_UPPER
added MATH operator TO_UPPER_INVARIANT
added MATH operator TO_LOWER
added MATH operator TO_LOWER_INVARIANT
added MATH operator SUBSTRING
added MATH operator REMOVE
added MATH operator LENGTH
added MATH operator PADLEFT
added MATH operator PADRIGHT
added MATH operator TRIM
added MATH operator TRIMSTART
added MATH operator TRIMEND
added MATH operator INDEXOF
added MATH operator LASTINDEXOF
added MATH operator READ
added MATH operator WRITE
added MATH operator CLOSE
added MATH operator FLUSH
added MATH operator GET_HEX
added script variable type FILEREADER
added script variable type FILEWRITER
added script command CRYSTALIZE_ITEM
added script command DELETE_ITEM
added script command DROP_ITEM

v213a
updated Romanian language (ty Spider!)
changed bot options to force control buff/heal for purple/red targets
fixed incosistent usage of int32 and int64 with script type int
refactoring of built in script globals
added script MATH operation: + for ADD
added script MATH operation: - for SUBTRACT
added script MATH operation: * for MULTIPLY
added script MATH operation: / for DIVIDE
added script MATH operation: ^ for POW
added script MATH operation: = for EQUAL
added script MATH operation: % for MOD
added script MATH operation: || for ABS
improved script error handling of incorrect DEFINE parameters
added script command USE_ACTION
added script variable TYPE_ERROR
added script variable TYPE_NONE
added script variable TYPE_SELF
added script variable TYPE_PLAYER
added script variable TYPE_NPC
added script variable TARGET_TYPE
added script variable SYSTEM_HANDLECOUNT
added script variable SYSTEM_MEMORYUSAGE
added script variable SYSTEM_MEMORYALLOCATED
added script variable SYSTEM_STACKHEIGHT

v212a
added Norwegian language (ty Oddi!)
added Czech language (ty kryso!)
added Slovenian language (ty kredn!)
added additional info on Force Collect
added command /assist name (players only)
added LootType internally for future use
fixed actions icons not showing up correctly on the shortcut bar
fixed bugs with using actions not working (including pet skills)
added shortcut support for: assist
added shortcut support for: partyinvite
added shortcut support for: partyleave
added shortcut support for: partydismiss
added shortcut support for: socialhello
added shortcut support for: socialvictory
added shortcut support for: socialcharge
added shortcut support for: socialyes
added shortcut support for: socialno
added shortcut support for: socialunaware
added shortcut support for: socialwaitinga
added shortcut support for: sociallaugh
added shortcut support for: socialapplause
added shortcut support for: socialdance
added shortcut support for: socialsad
added shortcut support for: commend
added shortcut support for: leaderchange
reduced induced script delay to 2 ms per thread pass (at max 500 lines run per thread per second as opposed to 166 lines total per second)
refactoring of script variables
added script command LOCK
added script command UNLOCK
tweaked bot options to enable setup through scripting
added script command BOTSET
added BOTSET command ACTIVEFOLLOW_ON
added BOTSET command ACTIVEFOLLOW_WALKERSTYLE
added BOTSET command ACTIVEFOLLOW_ATTACK
added BOTSET command ACTIVEFOLLOW_NAME
added BOTSET command ACTIVEFOLLOW_DISTANCE
added BOTSET command CLEAR_ITEMS
added BOTSET command CLEAR_BUFFS
added script variable SYSTEM_THREADCOUNT
added script variable CHANNEL_ALL
added script variable CHANNEL_SHOUT
added script variable CHANNEL_PRIVATE
added script variable CHANNEL_PARTY
added script variable CHANNEL_CLAN
added script variable CHANNEL_GM
added script variable CHANNEL_PETITION
added script variable CHANNEL_PETITIONREPLY
added script variable CHANNEL_TRADE
added script variable CHANNEL_ALLY
added script variable CHANNEL_ANNOUNCEMENT
added script variable CHANNEL_BOAT
added script variable CHANNEL_PARTYROOM
added script variable CHANNEL_PARTYCOMMANDER
added script variable CHANNEL_HERO

12.7.12
parser for packets in packet window
-------------------------------------------------------------
20.2.12
IN_POLY - new unary operator in script (check if specify uid is in pollygon)
ex.
IF IN_POLY target_id
	print_text "in poly"
endif

end_script

------------------------------rc2
12.2.12
friend chat sender name fix
12.2.12
inject fix (opcodes work now)
-------------------------------rc1
6.2.12
fix for chat event in script for h5,god
5.2.12
fix for say_to_client :h5,god
19.1.12
-fix for packet window lag (big amount pck per sec)
-save only visable packets checkbox(default endabled)
---------------------------beta 10
-------------------------- beta 9
17.1.12
Fix bug causing l2net to crash when using packet window
15.1.12
work as whitelist - in filter window
search pck name - little rework

-------------------------- beta 8
-------------------------- beta 7
5.1.12
hide client/server pck
read pck names from file only when u run pck window
search by pck name
folow new pck 
31.12.11
fix for pck window oog
-----------------------beta 6-------------------------------
29.12.11
optymalization for load pck log/ fix for search 
27.12.12
fix for listviev in packet window
23.12.11
little optymalization in load packet log func.
22.12.11
new search controls (in packet window)
21.12.11
new converter window (in packet window)
18.12.11
packet window alpha 2 (packet names credit-"JIV" from l2j forum)

11.12.11
temp fix for packets: (party packets need to be checked with pet/summon in pt)
magicskilluse
partysmallwindowupdate
partysmallwindowall

6.12.11
new comandlines parameters
-ubs enable unknow blowfish
-wasp enable work as proxy srv
-gslp - game server listen port ex (-gslp:1999)
4.12.11
new control in adv login opt:
work as proxy srv(checkbox)- alow redirect conection(gserv) only using proxifier (no need ipjack/fork anymore for blowfishless conection)



----------------------------------------------------------
new comandlines
-ewip 
will enable editing ip in enterworld packet


-ip01
-ip02
-ip03
-ip04
... ip11 .. ip22 ...
-ip44
ex. 
-ip01:125
-ip02:33

if u do not add any number to specyfic ip it will be generated randomaly


new controls for ip in enterworld - advanced login opt.



v211a
fixed bug with resizing ByteBuffers
increased packet buffer size to 64k/16k from 16k/4k for client connection
restarted UDP update listener thread
restarted UDP update sender code
improved UDP code to better handle connections and memory usage
added Dump Mode

v210a
changed audio errors to be popup boxes
added global variable TRUE
added global variable FALSE
added SCRIPTEVENT_SELFDIE
added SCRIPTEVENT_SELFREZ
added SCRIPTEVENT_SELFENTERCOMBAT
added SCRIPTEVENT_SELFLEAVECOMBAT
added SCRIPTEVENT_SELFSTOPMOVE
added SCRIPTEVENT_SELFTARGETED
added SCRIPTEVENT_SELFUNTARGETED
added SCRIPTEVENT_TARGETDIE
massive refactoring and optimization of map drawing
new splash

v209a
changed assembly names and stuff...
updated Russian translation (ty armedian!)
fixed bug with closing login connection (can dual/triple/... box IG now)
fixed crash on invalid blowfish key
added script command PLAYALARM
added script command PLAYSOUND (takes an int between 1 and 9)
changed several static values to const
added script command SET_EVENT
added script global SCRIPTEVENT_CHAT = 0
fixed resource leak in regards to returning void after threaded functions
changed sounds to play using DirectSound instead of winmm.dll
fixed resource leak with freeing sounds
changed sounds to 16bit PCM audio (the exe is a bit larger now) 

note: function names are NOT case sensitive, file names are case sensitive
note: requires at least the august 2007 version of directx, found at: http://www.microsoft.com/downloads/details.aspx?FamilyID=cb7397f3-0949-487b-9247-8fee451bf952&DisplayLang=en

v208a
tweaked skillinit packet handler
support to send unicode text
support to read unicode text
changed to use GDI+ for faster rendering
changed NPC_DIALOG to use strings (now pass the parameters inside quotes)
updated Spanish translation (ty Dr.TroNs!)
added additional localizable terms (scripting related values)
updated donate link under Help->Donate
added script targetting parameter ZRANGE - default is 500
windows changed for consistency
refactoring of script thread running
fixed crash starting script before loading

v207a
fixed gray bar at the bottom on the middle window
fixed bug on closing loaded script files
fixed bug with script threads sleeping
fixed bug with XP %
new splash

v206a
increased max timer for item usage
increased max timer for heal usage
updated default login server ip to reflect new retail ip
latest version check timeout increased to 10 seconds from 5
key check moved to after version check (should fix popups with valid keys)
refactoring of localization - resources are now embedded, should no longer corrupt machine locale
tweaked UI slightly
tweaked Force Collect to yield more useful information
changed lvlexp.txt loader to actually use the level rather than the index
added debug mode to find text for NPC chat
tweaked script startup/running
added Script command THREAD
added Script command DEFINE_GLOBAL
added Script command CALL_EXTERN
fixed Script command TARGET_NEAREST_NAME for targeting npcs
fixed spaces and/or tabs may be used between script command parameters now
added command /SPAUSE to pause the running script
added command /SRESUME to resume the paused script

v205a
refactoring of main class
fixed cross thread call - OOG server list
fixed cross thread call - OOG char list
fixed cross thread call - set char info basic
fixed cross thread call - clearing chat box
fixed cross thread call - clear dialog box
fixed cross thread call - add dialog text
fixed cross thread call - add dialog link
fixed cross thread call - show dialog box
fixed cross thread call - draw map
fixed cross thread calls - set target name/cp/mp/hp (overlay as well)
fixed cross thread calls - yes/no question box
fixed cross thread calls - clearing tool tips
fixed cross thread calls - clearing equipment
fixed cross thread call - setting equipment
fixed cross thread calls - panel dead
fixed cross thread calls - dead buttons
fixed cross thread calls - self clan
fixed cross thread calls - party info
fixed bug with not targeting players unless ATTACKABLE was set to 2
updated GG packets for login process
tweaked tiny packet handler for IG login
resolved login differences between L2J and Retail
added additional error handling to the clan/crest handler thread
fixed ShortcutInit packet handler
fixed npc dialog links

v204a
fixed bug calculating xp% on on lvl 1 chars
fixed cryption error on IG
adjusted map slightly
added command /assist

v203a
tweaked startup errors to improve thread safety on faster machines
changed error output for clarification
tweaked SelectedChar packet handler to increase stability
tweaked CharInfo packet handler to increase stability
removed legacy code to improve stability
tweaked sound playing slightly for memory
updated process encryption
fixed bugs where server id > 9
fixed error sending pms with no text
cleaned up and optimized map calculations
refactoring of clan/ally info
added script variable TARGET_CLAN
added script variable TARGET_ALLY

v202a
multithreaded loading
added support for // commands
fixed packet for nearby chars
fixed buggy char lists
new even higher rez map
tweaked map math
slight gui tweaks
tweaked npc dialog
cleaned up the splash

v201a
fixed alpha on splash screen
greatly improved loader
higher quality encrypted map
new npc dialog box
changed NPC_Dialog script command to require the link id instead of index
added additional error checking to prevent readers from crashing
new summer splash splash

v200a
.Net 2.0
dotfuscator 4.0
removed low memory check
removed redundant error checking
improved chatbox thread safety
improved whitespace removal for script commands
fixed script error on MATH commands with only 1 parameter
fixed healers unable to heal themselves based on CP/HP/MP
significant refactoring of globals
AES 256 Encoding
new mini map
newer gameguard packets for login process
C6 support


09/26/10
Updated Code/Data/Packs/Other Players/CharInfo.cs to work with Retail Freya

10/09/10
Further updates to Charinfo.

10/27/10
Updates to Player_Info (packet structure and HasExtendedEffect bool)
Updates to CharInfo (packet structure and HasExtendedEffect bool)
Updates to NpcInfo (packet structure and HasExtendedEffect bool)
Updates to GlobalData ExtendedEffects 
Updates to Script_Evaluate.cs (IS_STIGMAED unary op test)

11/02/10
Added the following unary operators
    * IS_BLEEDING
    * IS_POISONED
    * IS_REDCIRCLE
    * IS_ICE
    * IS_WIND
    * IS_AFRAID (Heroic Dread / Antharas Fear / Valakas Fear)
    * IS_STUNNED
    * IS_ASLEEP
    * IS_MUTE
    * IS_ROOTED
    * IS_PARALYZED
    * IS_PETRIFIED
    * IS_BURNING
    * IS_FLOATING_ROOT (Frintezza Paralyze)
    * IS_DANCE_STUNNED (Frintezza Raid Stun)
    * IS_FIREROOT_STUN (Valakas Burning Stun)
    * IS_STEALTH
    * IS_IMPRISIONING_1
    * IS_IMPRISIONING_2
    * IS_SOE (target is using soe or /unstuck)
    * IS_ICE2
    * IS_EARTHQUAKE (IoP screen shake)
    * IS_INVULNERABLE (Ultimate defense effects / anti magic armor)
    * IS_REGEN_VITALITY (Vitality Potion / Birthday buff)
    * IS_REAL_TARGETED
    * IS_DEATH_MARKED
    * IS_TERRIFIED (Curse Fear / Mass Fear / Sword Symphony)
    * IS_CONFUSED
    * IS_INVINCIBLE (Celestial Effects)
    * IS_AIR_STUN
    * IS_AIR_ROOT
    * IS_STIGMAED
    * IS_STAKATOROOT
    * IS_FREEZING (Freya Raid Freeze attack)
    * IS_DISABLED (combination check if not fear/stun/sleep/para/petrified/frint para/frint stun/valakas stun/antharas stun/afraid/freya stun)
    * IS_SHOP
    * IS_NOBLE
    * IS_HERO
    * IS_DUELING
    * IS_FAKEDEATH
    * IS_INVISIBLE
    * IS_INCOMBAT
    * IS_SITTING
    * IS_WALKING (slothmo already had IS_RUNNING for something else :/)
    * IS_FISHING
    * IS_DEMONSWORD
    * IS_TRANSFORMED
    * IS_AGATHON
    * IS_FINDPARTY
    * IS_USINGCUBIC
    * IS_FLAGGED
    * IS_RED
    * IS_PARTYMEMBER
    * IS_PARTYLEADER
    * IS_INPARTY
    * IS_CLANMEMBER (is the id in a clan not necessarily yours)
    * IS_LEADER
    * IS_CLANMATE (is the id in your clan)
    * IS_INSIEGE
    * IS_ATTACKER
    * IS_INALLY (is the id in an alliance not necessarily yours)
    * IS_ENEMY
    * IS_MUTUALWAR
    * IS_1SIDEWAR
    * IS_ALLYMEMBER (is the id in your alliance)
    * IS_TWAR (participating in a territory war)
11/03/10
  AddInfo.cs  - public static void Set_Relation(uint id, int relation)
  AddInfo.cs  - Extended ADD Npcinfo
  GameData.cs - public enum RelationStates : uint
  CharInfo.cs - public bool HasRelation(RelationStates test)
  CharInfo.cs - Extended Copy

11/04/10
New Binary operators:

    * IS_CLAN
    * IS_ALLY
    * IS_CLASS
    * IS_ROOTCLASS

New JingJing globals:
    * SERVER_ID
    * SERVER_KEY (obfuscation key)

11/13/10
New Binary operator:

    * IN_RANGE - returns true of ID is within N distance of the character

New JingJing global:
    * NOW (returns the current time in ticks - implicit int)

11/13/10
    * Added PartySpelled/Add_PartyBuff packet handlers

New function:
    * GET_EFFECTS SortedList "<&OBJECT_ID&>"

    Returns a list of effects on a player or party member.
    Requires new Effect.l2c

11/13/10
    * Added preliminary support for in game time via new global GAME_TIME and LOGIN_TIME.
      When a character logs in the server sets their clock via CharSelected.  From there we need to keep
      track of the difference. (experimental)

11/13/10
    * Added Global Variable ZONE (Globals.gamedata.cur_zone)

11/14/10
	* Added USE_SKILL_SMART (experimental use_skill with auto sense delay and interrupt)

11/16/10
        * Added IS_RESISTED binary operator

11/18/10
        * Fix for IS_RESISTED

11/20/10
        * Fix for IN_RANGE
        * Added .EFFECT_TIME for GET_EFFECTS initialized sortedlists
        * Added CANCEL_BUFF command 
        * Added global CHAR_CLAN, CHAR_ALLY
        * Added many item id globals for equipped items on character.

11/21/10
        * Added GROUP_HP / GROUP_MP / GROUP CP binary operators

11/23/10
	* fix for IS_INVISIBLE unary operator
	* fix IS_ENEMY typo -> IS_SIEGEENEMY

11/27/10

	* fix for SKILL_GET_REUSE when using USE_SKILL_SMART

12/04/10
	* Support for Content Filtering - IG performance tweaks

12/05/10
	* new setup option: Disconnect on IG Crash
	* new setup option: Dump Network buffer on IG Crash

12/23/10
	* new setup option: toggle botting when teleported
        
01/31/11
        * added GET_SKILLS and STDLIB\Skill.l2c (returns a list of player skills with attributes)

02/02/11
        * Added IS_READY (skill id) unary operator

02/04/11
	* Extended GET_INVENTORY and updated Inventory.l2c
	* Added prelim High5 support to the login screen.	
02/05/11
	* Sped up USE_SKILL_SMART
03/19/11
	* Added unary operator IS_ITEM_EQUIPPED <item id>
	* Added unary operator IS_AUG_EQUIPPED <aug id> (from optiondata-e.dat)
07/13/2011
        * Made IS_AFRAID cover all fear vfx.
        * IS_TERRIFIED maintained for backwards compat but depreciated.

