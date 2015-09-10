using System;

namespace L2_login
{
            /*07
            BA 25 3D 00 //??
            00 00 00 00 //??
            7C 91 2E 00 //Buy list id ?

            2A 00 //Itemcount


            04 00 // itemType1  0-weapon/ring/earring/necklace  1-armor/shield  4-item/questitem/adena
            00 00 00 00// objectid
            2B 07 00 00 //1st item ID
            00 00 00 00
            00 00 00 00
            05 00 // itemType2  0-weapon  1-shield/armor  2-ring/earring/necklace  3-questitem  4-adena  5-item
            00 00 
            00 00 00 00 
            00 00 00 00 
            00 00
            07 00 00 00 //price
            00 00 
            00 00 00 00 
            00 00 00 00 
            00 00 00 00 
            00 00 00 00 
            00 00

             
            04 00
            00 00 00 00 
            CD 09 00 00 // 2nd item ID
            00 00 00 00
            00 00 00 00 
            05 00
            00 00 
            00 00 00 00 
            00 00 00 00 
            00 00 
            12 00 00 00 //price
            00 00 
            00 00 00 00 
            00 00 00 00 
            00 00 00 00 
            00 00 00 00 
            00 00

            04 00 
            00 00 00 00
            6B 0F 00 00 //3rd item ID
            and so on*/
    public class BuyList : Object_Base
    {
        public volatile uint ItemType1 = 0; // itemType1  0-weapon/ring/earring/necklace  1-armor/shield  4-item/questitem/adena
        public volatile uint objID = 0; //Unique ID, always 0
        public volatile uint ItemID = 0; //Item ID
        public volatile uint ItemType2 = 0; // itemType2  0-weapon  1-shield/armor  2-ring/earring/necklace  3-questitem  4-adena  5-item
        public volatile uint Price = 0; //Item price


        public void Load(ByteBuffer buff)
        {
            ItemType1 = buff.ReadUInt16(); //Type1
            objID = buff.ReadUInt32(); //Object ID
            ItemID = buff.ReadUInt32(); //Item ID
            buff.ReadUInt32(); //??
            buff.ReadUInt32(); //??
            ItemType2 = buff.ReadUInt16(); //Type2
            buff.ReadUInt16(); //??
            buff.ReadUInt32(); //??
            buff.ReadUInt32(); //??
            buff.ReadUInt16(); //??
            Price = buff.ReadUInt32(); //Item price
            buff.ReadUInt16(); //??
            buff.ReadUInt32(); //??
            buff.ReadUInt32(); //??
            buff.ReadUInt32(); //??
            buff.ReadUInt32(); //??
            buff.ReadUInt16(); //??
            //Globals.l2net_home.Add_Text("Added type1: "+ ItemType1 + " itemID: "+ ItemID + " itemType2: " + ItemType2 + " Price: " + Price, Globals.Red, TextType.BOT);
        }


    }//Buylist class end
}