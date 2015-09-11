@ECHO Decoding, press any key to continue
@PAUSE
l2encdec.exe -l ActionName-e.dat
l2encdec.exe -l Armorgrp.dat
l2encdec.exe -l EtcItemgrp.dat
l2encdec.exe -l Hennagrp-e.dat
l2encdec.exe -l ItemName-e.dat
l2encdec.exe -l NpcName-e.dat
l2encdec.exe -l NpcString-e.dat
l2encdec.exe -l SkillName-e.dat
l2encdec.exe -l SystemMsg-e.dat
l2encdec.exe -l Weapongrp.dat
l2encdec.exe -l ZoneName-e.dat



@ECHO Ready to disassemble, press any key to continue
@PAUSE




l2disasm.exe -d actionname-e.ddf dec-ActionName-e.dat ActionName.txt
l2disasm.exe -d armorgrp.ddf dec-Armorgrp.dat Armorgrp.txt
l2disasm.exe -d etcitemgrp.ddf dec-EtcItemgrp.dat EtcItemgrp.txt
l2disasm.exe -d hennagrp-e.ddf dec-Hennagrp-e.dat Hennagrp.txt
l2disasm.exe -d itemname-e.ddf dec-ItemName-e.dat ItemName.txt
l2disasm.exe -d npcname-e.ddf dec-NpcName-e.dat NpcName.txt
l2disasm.exe -d npcname-e.ddf dec-NpcString-e.dat NpcString.txt
l2disasm.exe -d skillname-e.ddf dec-SkillName-e.dat SkillName.txt
l2disasm.exe -d systemmsg-e.ddf dec-SystemMsg-e.dat SystemMsg.txt
l2disasm.exe -d weapongrp.ddf dec-Weapongrp.dat Weapongrp.txt
l2disasm.exe -d zonename-e.ddf dec-ZoneName-e.dat ZoneName.txt

