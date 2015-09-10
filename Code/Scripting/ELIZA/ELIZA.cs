using System;
using System.Collections.Generic;
using System.Text;

namespace L2_login
{
    class ELIZA
    {
        static private bool init = false;

        static public void Initialize()
        {
            init = true;
        }

        static public string GetReply(string query)
        {
            if (!init)
            {
                Initialize();
            }

            return "no";
        }

        static public int Get_Complexity(string c)
        {
            c = c.ToLower();

            switch(c)
            {
                case "`":
                case "~":
                    return -12;
                case "q":
                case "a":
                case "z":
                case "1":
                case "!":
                    return -10;
                case "w":
                case "s":
                case "x":
                case "2":
                case "@":
                    return -9;
                case "e":
                case "d":
                case "c":
                case "3":
                case "#":
                    return -8;
                case "r":
                case "f":
                case "v":
                case "4":
                case "$":
                    return -7;
                case "t":
                case "g":
                case "b":
                case "5":
                case "%":
                    return -6;
                case " ":
                    return 0;
                case "y":
                case "h":
                case "n":
                case "6":
                case "^":
                    return 6;
                case "u":
                case "j":
                case "m":
                case "7":
                case "&":
                    return 7;
                case "i":
                case "k":
                case ",":
                case "<":
                case "8":
                case "*":
                    return 8;
                case "o":
                case "l":
                case ".":
                case ">":
                case "9":
                case "(":
                    return 9;
                case "p":
                case ";":
                case ":":
                case "/":
                case "?":
                case "0":
                case ")":
                    return 11;
                case "[":
                case "{":
                case "'":
                case "\"":
                case "-":
                case "_":
                    return 13;
                case "=":
                case "+":
                case "]":
                case "}":
                    return 15;
                case "\\":
                case "|":
                    return 17;
            }

            return 0;
        }
    }
}
