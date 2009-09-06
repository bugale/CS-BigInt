using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Math.UInt
{
    static internal class Convert
    {
        static ulong[] pows10 = { 1, 10, 100, 1000, 10000, 100000, 1000000, 10000000, 100000000, 1000000000 };
        static public uint[] Parse(string number)
        {
            uint[] temp = new uint[number.Length / 9 + 1];
            int index = 0;
            string result = number;
            ulong modulo = 0;
            while (result != "0")
            {
                result = Divide(result, 4294967296, out modulo);
                temp[index] = (uint)modulo;
                index++;
            }
            uint[] returned = new uint[index];
            for (int counter = 0; counter < index; counter++)
                returned[counter] = temp[counter];
            return returned;
        }
        static public string Divide(string number, ulong divider, out ulong modulo)
        {
            int length = number.Length;
            int Mod = length % 9;
            int lengthRoundedTo9 = length - Mod;
            StringBuilder result = new StringBuilder(length);
            ulong currentUlong = 0;
            uint currentDivision = 0;
            bool Add = false;
            int counter;
            char[] chars = new char[9];
            int charCounter;
            bool isLength9Zero = lengthRoundedTo9 == 0;
            if (isLength9Zero)
            {
                currentUlong = ulong.Parse(number);
                currentDivision = (uint)(currentUlong / divider); //Try to optimise
                modulo = currentUlong % divider; //Try to optimise
                return currentDivision.ToString();
            }
            modulo = 0;
            for (counter = 0; counter < lengthRoundedTo9; counter += 9)
            {
                currentUlong = (1000000000 * modulo) + ulong.Parse(number.Substring(counter, 9));
                currentDivision = (uint)(currentUlong / divider); //Try to optimise
                modulo = currentUlong % divider; //Try to optimise
                if (!Add)
                {
                    if (currentDivision != 0)
                    {
                        result.Append(currentDivision.ToString());
                        Add = true;
                    }
                }
                else
                {
                    for (charCounter = 8; currentDivision != 0; charCounter--)
                    {
                        chars[charCounter] = (char)(48 + (currentDivision % 10));
                        currentDivision /= 10;
                    }
                    while (charCounter != -1)
                    {
                        chars[charCounter] = '0';
                        charCounter--;
                    }
                    result.Append(chars);
                }
            }
            if (lengthRoundedTo9 != length && !isLength9Zero)
            {
                currentUlong = (pows10[Mod] * modulo) + ulong.Parse(number.Substring(lengthRoundedTo9, Mod));
                currentDivision = (uint)(currentUlong / divider); //Try to optimise
                modulo = currentUlong % divider; //Try to optimise
                if (!Add)
                {
                    if (currentDivision != 0)
                    {
                        result.Append(currentDivision.ToString());
                        Add = true;
                    }
                }
                else
                {
                    char[] newchars = new char[Mod];
                    for (charCounter = Mod - 1; currentDivision != 0; charCounter--)
                    {
                        newchars[charCounter] = (char)(48 + (currentDivision % 10));
                        currentDivision /= 10;
                    }
                    while (charCounter != -1)
                    {
                        newchars[charCounter] = '0';
                        charCounter--;
                    }
                    result.Append(newchars);
                }
            }
            if (Add) return result.ToString();
            else return "0";
        }
        static public string ToString(uint[] number)
        {
            int length = number.Length;
            if (length == 0) return "0";
            if (length == 1) return number[0].ToString();
            uint[] currentResult = new uint[(length * 10) / 9 + 1];
            int counter;
            int used = 0;
            ulong modulo;
            int subcounter;
            for (counter = length - 1; counter >= 0; counter--)
            {
                modulo = 0;
                for (subcounter = 0; subcounter < used; subcounter++)
                {
                    modulo += (ulong)currentResult[subcounter] << 32;
                    currentResult[subcounter] = (uint)(modulo % 1000000000);
                    modulo /= 1000000000;
                }
                while (modulo != 0)
                {
                    used++;
                    currentResult[subcounter] = (uint)(modulo % 1000000000);
                    modulo /= 1000000000;
                    subcounter++;
                }
                modulo = number[counter];
                for (subcounter = 0; subcounter < used && modulo != 0; subcounter++)
                {
                    modulo += currentResult[subcounter];
                    currentResult[subcounter] = (uint)(modulo % 1000000000);
                    modulo /= 1000000000;
                }
                while (modulo != 0)
                {
                    used++;
                    currentResult[subcounter] = (uint)(modulo % 1000000000);
                    modulo /= 1000000000;
                    subcounter++;
                }
            }
            StringBuilder result = new StringBuilder(length * 10 + 1);
            result.Append(currentResult[used - 1].ToString());
            char[] chars = new char[9];
            int charCounter;
            for (counter = used - 2; counter >= 0; counter--)
            {
                for (charCounter = 8; currentResult[counter] != 0; charCounter--)
                {
                    chars[charCounter] = (char)(48 + (currentResult[counter] % 10));
                    currentResult[counter] /= 10;
                }
                while (charCounter != -1)
                {
                    chars[charCounter] = '0';
                    charCounter--;
                }
                result.Append(chars);
            }
            return result.ToString();
        }
    }
}
