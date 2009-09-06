using System;
using System.Linq;

namespace Math.UInt
{
    internal static class UIntOperations
    {
        #region Basic Operations
            static public uint[] Increment(uint[] a)
            {
                int length = a.Length;
                uint[] result = new uint[length];
                result[0] = a[0] + 1;
                bool carry = result[0] == 0;
                int counter;
                for (counter = 1; counter < length && carry; counter++)
                {
                    result[counter] = a[counter] + 1;
                    carry = result[counter] == 0;
                }
                if (carry)
                {
                    uint[] final = new uint[length + 1];
                    for (counter = 0; counter < length; counter++)
                        final[counter] = result[counter];
                    final[length] = 1;
                    return final;
                }
                else return result;
            }
            static public uint[] Decrement(uint[] a)
            {
                int length = a.Length;
                uint[] result = new uint[length];
                result[0] = a[0] - 1;
                bool carry = result[0] == 4294967295;
                int counter;
                for (counter = 1; counter < length && carry; counter++)
                {
                    result[counter] = a[counter] - 1;
                    carry = result[counter] == 4294967295;
                }
                if (carry)
                {
                    uint[] final = new uint[length + 1];
                    for (counter = 0; counter < length; counter++)
                        final[counter] = result[counter];
                    final[length] = 1;
                    return final;
                }
                else return result;
            }
            static public uint[] Addition(uint[] a, uint[] b)
            {
                int Smaller;
                int Larger;
                ulong carry = 0;
                uint[] selected;
                if (a.Length > b.Length)
                {
                    Larger = a.Length;
                    Smaller = b.Length;
                    selected = a;
                }
                else
                {
                    Larger = b.Length;
                    Smaller = a.Length;
                    selected = b;
                }
                uint[] result = new uint[Larger + 1];
                for (int counter = 0; counter < Smaller; counter++)
                {
                    carry += (ulong)a[counter] + (ulong)b[counter];
                    result[counter] = (uint)carry;
                    carry >>= 32;
                }
                if (!(a.Length == b.Length))
                    for (int counter = Smaller; counter < Larger; counter++)
                    {
                        carry += selected[counter];
                        result[counter] = (uint)carry;
                        carry >>= 32;
                    }
                if (carry == 0)
                {
                    uint[] final = new uint[Larger];
                    for (int counter = 0; counter < Larger; counter++)
                        final[counter] = result[counter];
                    return final;
                }
                else
                {
                    result[Larger] = (uint)carry;
                    return result;
                }
            }
            static public uint[] Subtraction(uint[] a, uint[] b)
            {
                bool carry = false;
                int alength = a.Length;
                int blength = b.Length;
                uint[] result = new uint[alength];
                int counter;
                for (counter = 0; counter < blength; counter++)
                {
                    result[counter] = a[counter] - b[counter];
                    if (carry) result[counter]--;
                    carry = (carry && a[counter] == b[counter]) || (b[counter] > a[counter]);
                }
                while (carry)
                {
                    result[counter]--;
                    carry = (carry && a[counter] == b[counter]) || (b[counter] > a[counter]);
                    counter++;
                }
                while (counter < alength)
                {
                    result[counter] = a[counter];
                    counter++;
                }
                if (result[alength - 1] != 0) return result;
                for (counter = alength - 1; counter >= 0 && result[counter] == 0; counter--)
                    alength--;
                uint[] final = new uint[alength];
                for (counter = 0; counter < alength; counter++)
                    final[counter] = result[counter];
                if (final.Length == 0) return new uint[] { 0 };
                return final;
            }
            static public uint[] Multiplication(uint[] a, uint[] b)
            {
                int alength = a.Length;
                int blength = b.Length;
                int sum = alength + blength;
                if (alength == 0 || blength == 0) return new uint[] { 0 };
                if ((alength == 1 && a[0] == 0) || (blength == 1 && b[0] == 0)) return new uint[] { 0 };
                if (alength > blength)
                {
                    uint[] temp = a;
                    a = b;
                    b = temp;
                    int temp2 = alength;
                    alength = blength;
                    blength = temp2;
                }
                uint[] bOptimized = new uint[sum];
                int counter = 0;
                for (; counter < blength; counter++)
                    bOptimized[counter] = b[blength - counter - 1];
                uint[] result = new uint[sum];
                uint currentTransfer;
                int used = blength;
                int minCount;
                ulong carry;
                int counter2;
                for (counter = 0; counter < alength; counter++, used++)
                {
                    currentTransfer = a[counter];
                    if (currentTransfer != 0)
                    {
                        carry = 0;
                        minCount = System.Math.Min(used, sum);
                        for (counter2 = 0; counter2 < minCount; counter2++)
                        {
                            carry = (ulong)bOptimized[used - counter2 - 1] * (ulong)currentTransfer + (ulong)result[counter2] + carry;
                            result[counter2] = (uint)carry;
                            carry >>= 32;
                        }
                        if (used > minCount)
                        {
                            for (counter2 = minCount; counter2 < used; counter2++)
                            {
                                carry += (ulong)bOptimized[used - counter2 - 1] * (ulong)currentTransfer;
                                result[counter2] = (uint)carry;
                                carry >>= 32;
                            }
                            if (carry != 0)
                                result[counter2] = (uint)carry;
                        }
                        else
                        {
                            int carryIndex = minCount;
                            if (minCount < sum && carry != 0)
                                if ((result[minCount] += (uint)carry) < result[minCount])
                                    carry = 1;
                                else
                                    carry = 0;
                            if (carry != 0)
                                result[counter2 + 1] = 1;
                        }
                    }
                }
                if (result[sum - 1] != 0) return result;
                for (sum--; result[sum] == 0; sum--) ;
                sum++;
                uint[] final = new uint[sum];
                for (counter = 0; counter < sum; counter++)
                    final[counter] = result[counter];
                return final;
            }
            static public uint[] Division(uint[] a, uint[] b, out uint[] modulo)
            {
                uint[] current = new uint[1];
                uint[] result = new uint[1];
                while (BiggerEqual(a, current))
                {
                    current = Addition(current, b);
                    result = Increment(result);
                }
                result = Decrement(result);
                current = Subtraction(current, b);
                modulo = Subtraction(a, current);
                return result;
            }
            static public uint[] Division(uint[] a, uint[] b)
            {
                uint[] current = new uint[1];
                uint[] result = new uint[1];
                while (BiggerEqual(a, current))
                {
                    current = Addition(current, b);
                    result = Increment(result);
                }
                result = Decrement(result);
                return result;
            }
            static public uint[] Modulo(uint[] a, uint[] b)
            {
                uint[] current = new uint[1];
                while (BiggerEqual(a, current)) current = Addition(current, b);
                current = Subtraction(current, b);
                return Subtraction(a, current);
            }
            static public uint[] Power(uint[] a, uint[] b)
            {
                int alength = a.Length;
                int blength = b.Length;
                if (blength == 1) // B is less than 2^32
                {
                    if (b[0] == 0)
                        return new uint[] { 1 };
                    else if (b[0] == 1)
                    {
                        uint[] resultT = new uint[alength];
                        for (int counterT = 0; counterT < alength; counterT++)
                            resultT[counterT] = a[counterT];
                        return resultT;
                    }
                    else
                    {
                        if (alength == 1)
                            if (a[0] == 0) return new uint[] { 0 };
                            else if (a[0] == 1) return new uint[] { 1 };
                        uint temp = b[0];
                        int counter2;
                        uint[] result = new uint[alength];
                        for (counter2 = 0; counter2 < alength; counter2++)
                            result[counter2] = a[counter2];
                        while (temp != 1)
                        {
                            if (temp % 2 == 0)
                                result = Multiplication(result, result);
                            else
                                result = Multiplication(result, Multiplication(result, a));
                            temp /= 2;
                        }
                        return result;
                    }
                }
                else // B is more than 2^32
                {
                    if (alength == 1)
                        if (a[0] == 0) return new uint[] { 0 };
                        else if (a[0] == 1) return new uint[] { 1 };
                    uint[] temp = new uint[blength];
                    int counter2;
                    for (counter2 = 0; counter2 < blength; counter2++)
                        temp[counter2] = b[counter2];
                    uint[] modulo;
                    uint[] Two = new uint[] { 2 };
                    uint[] result = new uint[alength];
                    for (counter2 = 0; counter2 < alength; counter2++)
                        result[counter2] = a[counter2];
                    while (temp.Length != 1 || temp[0] != 1)
                    {
                        temp = Division(temp, Two, out modulo);
                        if (modulo[0] == 0)
                            result = Multiplication(result, result);
                        else
                            result = Multiplication(result, Multiplication(result, a));
                    }
                    return result;
                }
            }
            static public uint[] Root(uint[] a, uint[] b)
            {
                int alength = a.Length;
                int blength = b.Length;
                if (alength == 1)
                    if (a[0] == 1) 
                    {
                        uint[] result = new uint[blength];
                        for (int counter = 0; counter < blength; counter++)
                            result[counter] = b[counter];
                        return result;
                    }
                if (blength == 1)
                    if (b[0] == 0) return new uint[] { 0 };
                    else if (b[0] == 1) return new uint[] { 1 };
                if (BiggerEqual(a, b)) return new uint[] { 1 };
                uint[] smallest = new uint[] { 0 };
                uint[] largest = b;
                uint[] currentresult;
                uint[] currentmultiplication;
                uint[] Two = new uint[] { 2 };
                while (!Equal(Increment(smallest), largest))
                {
                    currentresult = Division(Addition(smallest, largest), Two);
                    currentmultiplication = Multiplication(currentresult, currentresult);
                    if (Bigger(currentmultiplication, b))
                        largest = currentresult;
                    else if (Smaller(currentmultiplication, b))
                        smallest = currentresult;
                    else if (Equal(currentmultiplication, b))
                        return currentresult;
                }
                return smallest;
            }
            static public uint[] Factorial(uint[] a)
            {
                if (a.Length == 0) return new uint[] { 1 };
                if (a.Length == 1)
                    if (a[0] == 0) return new uint[] { 1 };
                    else if (a[0] == 1) return new uint[] { 1 };
                    else if (a[0] == 2) return new uint[] { 2 };
                    else if (a[0] == 3) return new uint[] { 6 };
                    else if (a[0] == 4) return new uint[] { 24 };
                    else if (a[0] == 5) return new uint[] { 120 };
                    else if (a[0] == 6) return new uint[] { 720 };
                    else if (a[0] == 7) return new uint[] { 5040 };
                    else if (a[0] == 8) return new uint[] { 40320 };
                    else if (a[0] == 9) return new uint[] { 362880 };
                    else if (a[0] == 10) return new uint[] { 3628800 };
                    else if (a[0] == 11) return new uint[] { 39916800 };
                    else if (a[0] == 12) return new uint[] { 479001600 };
                    else
                    {
                        uint[] counter2;
                        uint[] result2 = new uint[] { 479001600 };
                        for (counter2 = new uint[] { 13 }; BiggerEqual(a, counter2); counter2[0]++)
                            result2 = Multiplication(result2, counter2);
                        return result2;
                    }
                uint[] counter;
                uint[] result = new uint[] { 479001600 };
                for (counter = new uint[] { 13 }; BiggerEqual(a, counter); counter = Increment(counter))
                    result = Multiplication(result, counter);
                return result;
            }
        #endregion
        #region Bitwise
            static public uint[] Not(uint[] a)
            {
                int alength = a.Length;
                uint[] result = new uint[alength];
                for (int counter = 0; counter < alength; counter++)
                    result[counter] = ~a[counter];
                return result;
            }
            static public uint[] Or(uint[] a, uint[] b)
            {
                int alength = a.Length;
                int blength = b.Length;
                int counter;
                if (alength > blength)
                {
                    uint[] result = new uint[alength];
                    for (counter = 0; counter < blength; counter++)
                        result[counter] = a[counter] | b[counter];
                    for (; counter < alength; counter++)
                        result[counter] = a[counter];
                    return result;
                }
                else
                {
                    uint[] result = new uint[blength];
                    for (counter = 0; counter < alength; counter++)
                        result[counter] = a[counter] | b[counter];
                    for (; counter < blength; counter++)
                        result[counter] = b[counter];
                    return result;
                }
            }
            static public uint[] And(uint[] a, uint[] b)
            {
                int alength = a.Length;
                int blength = b.Length;
                int counter;
                if (alength > blength)
                {
                    uint[] result = new uint[alength];
                    for (counter = 0; counter < blength; counter++)
                        result[counter] = a[counter] & b[counter];
                    for (; counter < alength; counter++)
                        result[counter] = 0;
                    return result;
                }
                else
                {
                    uint[] result = new uint[blength];
                    for (counter = 0; counter < alength; counter++)
                        result[counter] = a[counter] & b[counter];
                    for (; counter < blength; counter++)
                        result[counter] = 0;
                    return result;
                }
            }
            static public uint[] Nor(uint[] a, uint[] b)
            {
                int alength = a.Length;
                int blength = b.Length;
                int counter;
                if (alength > blength)
                {
                    uint[] result = new uint[alength];
                    for (counter = 0; counter < blength; counter++)
                        result[counter] = ~(a[counter] | b[counter]);
                    for (; counter < alength; counter++)
                        result[counter] = ~a[counter];
                    return result;
                }
                else
                {
                    uint[] result = new uint[blength];
                    for (counter = 0; counter < alength; counter++)
                        result[counter] = ~(a[counter] | b[counter]);
                    for (; counter < blength; counter++)
                        result[counter] = ~b[counter];
                    return result;
                }
            }
            static public uint[] Nand(uint[] a, uint[] b)
            {
                int alength = a.Length;
                int blength = b.Length;
                int counter;
                if (alength > blength)
                {
                    uint[] result = new uint[alength];
                    for (counter = 0; counter < blength; counter++)
                        result[counter] = ~(a[counter] & b[counter]);
                    for (; counter < alength; counter++)
                        result[counter] = 4294967295;
                    return result;
                }
                else
                {
                    uint[] result = new uint[blength];
                    for (counter = 0; counter < alength; counter++)
                        result[counter] = ~(a[counter] & b[counter]);
                    for (; counter < blength; counter++)
                        result[counter] = 4294967295;
                    return result;
                }
            }
            static public uint[] Xor(uint[] a, uint[] b)
            {
                int alength = a.Length;
                int blength = b.Length;
                int counter;
                if (alength > blength)
                {
                    uint[] result = new uint[alength];
                    for (counter = 0; counter < blength; counter++)
                        result[counter] = a[counter] ^ b[counter];
                    for (; counter < alength; counter++)
                        result[counter] = a[counter];
                    return result;
                }
                else
                {
                    uint[] result = new uint[blength];
                    for (counter = 0; counter < alength; counter++)
                        result[counter] = a[counter] ^ b[counter];
                    for (; counter < blength; counter++)
                        result[counter] = b[counter];
                    return result;
                }
            }
            static public uint[] Xnor(uint[] a, uint[] b)
            {
                int alength = a.Length;
                int blength = b.Length;
                int counter;
                if (alength > blength)
                {
                    uint[] result = new uint[alength];
                    for (counter = 0; counter < blength; counter++)
                        result[counter] = ~(a[counter] ^ b[counter]);
                    for (; counter < alength; counter++)
                        result[counter] = ~a[counter];
                    return result;
                }
                else
                {
                    uint[] result = new uint[blength];
                    for (counter = 0; counter < alength; counter++)
                        result[counter] = ~(a[counter] ^ b[counter]);
                    for (; counter < blength; counter++)
                        result[counter] = ~b[counter];
                    return result;
                }
            }
            static public uint[] LeftShift(uint[] a, uint[] bits)
            {
                int alength = a.Length;
                int bitslength = bits.Length;
                uint[] result;
                if (bitslength == 1)
                {
                    uint Bits = bits[0];
                    uint DivisionT = Bits / 32;
                    ulong Modulo = (ulong)Bits % 32;
                    uint counter = DivisionT;
                    ulong currentresult;
                    uint carry = 0;
                    long sum = alength + DivisionT;
                    result = new uint[sum + (Modulo == 0 ? 0 : 1)];
                    for (; counter < sum; counter++)
                        result[counter] = a[counter - DivisionT];
                    for (counter = DivisionT; counter < sum; counter++)
                    {
                        currentresult = (ulong)result[counter] << (int)Modulo;
                        result[counter] = (uint)((uint)currentresult + carry);
                        carry = (uint)(currentresult / 4294967296);
                    }
                    if (carry == 0 && Modulo != 0)
                    {
                        uint[] final = new uint[sum];
                        for (counter = DivisionT; counter < sum; counter++)
                            final[counter] = result[counter];
                        return final;
                    }
                    else
                        result[counter] = carry;
                }
                else
                {
                    uint[] Modulo;
                    uint[] DivisionT = Division(bits, new uint[] { 32 }, out Modulo);
                    if (DivisionT.Length == 1)
                    {
                        uint division = DivisionT[0];
                        uint counter = division;
                        ulong currentresult;
                        uint carry = 0;
                        long sum = alength + division;
                        result = new uint[sum + (Modulo[0] == 0 ? 0 : 1)];
                        for (; counter < sum; counter++)
                            result[counter] = a[counter - division];
                        for (counter = division; counter < sum; counter++)
                        {
                            currentresult = (ulong)result[counter] << (int)Modulo[0];
                            result[counter] = (uint)((uint)currentresult + carry);
                            carry = (uint)(currentresult / 4294967296);
                        }
                        if (carry == 0 && Modulo[0] != 0)
                        {
                            uint[] final = new uint[sum];
                            for (counter = division; counter < sum; counter++)
                                final[counter] = result[counter];
                            return final;
                        }
                        else
                            result[counter] = carry;
                    }
                    else
                        throw new OutOfMemoryException("There is not enough memory for the array.");
                }
                return result;
            }
            static public uint[] RightShift(uint[] a, uint[] bits)
            {
                int alength = a.Length;
                int bitslength = bits.Length;
                uint[] result = new uint[alength];
                if (bitslength == 1)
                {
                    uint Bits = bits[0];
                    uint DivisionT = Bits / 32;
                    ulong Modulo = (ulong)Bits % 32;
                    uint counter = (uint)alength - 1;
                    ulong currentresult;
                    uint carry = 0;
                    for (; counter >= DivisionT; counter--)
                        result[counter] = a[counter - DivisionT];
                    for (counter = (uint)alength - 1; counter >= DivisionT; counter--)
                    {
                        currentresult = (ulong)result[counter] >> (int)Modulo;
                        result[counter] = (uint)((currentresult / 4294967296) + carry);
                        carry = (uint)currentresult;
                    }
                }
                else
                {
                    uint[] Modulo;
                    uint[] DivisionT = Division(bits, new uint[] { 32 }, out Modulo);
                    if (DivisionT.Length == 1)
                    {
                        uint division = DivisionT[0]; ;
                        uint counter = (uint)alength;
                        ulong currentresult;
                        uint carry = 0;
                        for (; counter >= division; counter--)
                            result[counter] = a[counter - division];
                        for (counter = (uint)alength; counter >= division; counter--)
                        {
                            currentresult = ((ulong)result[counter] >> (int)Modulo[0]) + (uint)carry;
                            result[counter] = (uint)(currentresult / 4294967296);
                            carry = (uint)currentresult;
                        }
                    }
                }
                return result;
            }
            static public uint[] LeftShiftCircular(uint[] a, uint[] bits)
            {
                int alength = a.Length;
                int bitslength = bits.Length;
                uint[] result = new uint[alength];
                uint[] Bits = Modulo(bits, Multiplication(new uint[] { (uint)bitslength }, new uint[] { 32 }));
                uint[] modulo;
                uint[] DivisionT = Division(bits, new uint[] { 32 }, out modulo);
                uint division = DivisionT[0]; ;
                uint counter = division;
                ulong currentresult;
                uint carry = 0;
                for (; counter < alength; counter++)
                    result[counter] = a[counter - division];
                for (counter = 0; counter < division; counter++)
                    result[counter] = a[counter + division];
                for (counter = division; counter < alength; counter++)
                {
                    currentresult = ((ulong)result[counter] << (int)modulo[0]) + (uint)carry;
                    result[counter] = (uint)currentresult;
                    carry = (uint)(currentresult / 4294967296);
                }
                result[0] += carry;
                return result;
            }
            static public uint[] RightShiftCircular(uint[] a, uint[] bits)
            {
                int alength = a.Length;
                int bitslength = bits.Length;
                uint[] result = new uint[alength];
                uint[] Bits = Modulo(bits, Multiplication(new uint[] { (uint)bitslength }, new uint[] { 32 }));
                uint[] modulo;
                uint[] DivisionT = Division(bits, new uint[] { 32 }, out modulo);
                uint division = DivisionT[0]; ;
                uint counter = (uint)alength - 1;
                ulong currentresult;
                uint carry = 0;
                for (; counter >= division; counter--)
                    result[counter] = a[counter - division];
                for (counter = division; counter >= 0; counter--)
                    result[counter] = a[counter + division];
                for (counter = (uint)alength - 1; counter >= 0; counter--)
                {
                    currentresult = ((ulong)result[counter] >> (int)modulo[0]) + (uint)carry;
                    result[counter] = (uint)(currentresult / 4294967296);
                    carry = (uint)currentresult;
                }
                result[alength - 1] += carry;
                return result;
            }
        #endregion
        #region Tools
            static public bool BiggerEqual(uint[] a, uint[] b)
            {
                int alength = a.Length;
                int blength = b.Length;
                if (alength > blength) return true;
                if (alength < blength) return false;
                for (int counter = alength - 1; counter >= 0; counter--)
                {
                    if (a[counter] > b[counter]) return true;
                    if (a[counter] < b[counter]) return false;
                }
                return true;
            }
            static public bool Bigger(uint[] a, uint[] b)
            {
                int alength = a.Length;
                int blength = b.Length;
                if (alength > blength) return true;
                if (alength < blength) return false;
                for (int counter = alength - 1; counter >= 0; counter--)
                {
                    if (a[counter] > b[counter]) return true;
                    if (a[counter] < b[counter]) return false;
                }
                return false;
            }
            static public bool SmallerEqual(uint[] a, uint[] b)
            {
                int alength = a.Length;
                int blength = b.Length;
                if (alength < blength) return true;
                if (alength > blength) return false;
                for (int counter = alength - 1; counter >= 0; counter--)
                {
                    if (a[counter] < b[counter]) return true;
                    if (a[counter] > b[counter]) return false;
                }
                return true;
            }
            static public bool Smaller(uint[] a, uint[] b)
            {
                int alength = a.Length;
                int blength = b.Length;
                if (alength < blength) return true;
                if (alength > blength) return false;
                for (int counter = alength - 1; counter >= 0; counter--)
                {
                    if (a[counter] < b[counter]) return true;
                    if (a[counter] > b[counter]) return false;
                }
                return false;
            }
            static public bool Equal(uint[] a, uint[] b)
            {
                int alength = a.Length;
                int blength = b.Length;
                if (alength < blength) return false;
                if (alength > blength) return false;
                for (int counter = alength - 1; counter >= 0; counter--)
                {
                    if (a[counter] < b[counter]) return false;
                    if (a[counter] > b[counter]) return false;
                }
                return true;
            }
            static public uint[] ReverseBinary(uint[] a)
            {
                int alength = a.Length;
                uint[] result = new uint[alength];
                for (int counter = 0; counter < alength; counter++)
                    result[counter] = ReverseUint(a[alength - counter - 1]);
                return result;
            }
            static public uint ReverseUint(uint a)
            {
                uint result = 0;
                for (uint counter = 1; counter <= 2147483648; counter *= 2)
                    result += (a & counter) * (2147483648 / counter);
                return result;
            }
        #endregion
    }
}
