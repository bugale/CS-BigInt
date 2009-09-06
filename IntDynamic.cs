using System;
using System.Linq;

namespace Math
{
    [Serializable]
    public class IntDynamic
    {
        #region Fields & Enums
            private bool _nagative = false;
            private uint[] _number;
            public enum SignShow { ShowAlways = 0, ShowOnlyNagative = 1, DontShow = 2 };
        #endregion
        #region Constructors
            public IntDynamic()
            {
                this._number = new uint[] { 0 };
            }
            public IntDynamic(ushort number)
            {
                this._number = new uint[] { number };
            }
            public IntDynamic(short number)
            {
                if (number < 0)
                {
                    this._nagative = true;
                    this._number = new uint[] { (uint)(0 - number) };
                }
                else this._number = new uint[] { (uint)number };
            }
            public IntDynamic(uint number)
            {
                this._number = new uint[] { number };
            }
            public IntDynamic(int number)
            {
                if (number < 0)
                {
                    this._nagative = true;
                    this._number = new uint[] { (uint)(0 - number) };
                }
                else this._number = new uint[] { (uint)number };
            }
            public IntDynamic(ulong number)
            {
                if ((ulong)((uint)number) == number)
                    this._number = new uint[] { (uint)number };
                else
                    this._number = new uint[] { (uint)number, (uint)(number / 4294967296) };
            }
            public IntDynamic(long number)
            {
                long Number;
                uint first;
                uint second;
                if (number < 0)
                {
                    Number = 0 - number;
                    this._nagative = true;
                }
                else
                    Number = number;
                first = (uint)(Number / 4294967296);
                second = (uint)Number;
                if (first == 0)
                    this._number = new uint[] { second };
                else
                    this._number = new uint[] { first, second };
            }
            public IntDynamic(byte number)
            {
                this._number = new uint[] { (uint)number };
            }
            public IntDynamic(sbyte number)
            {
                if (number < 0)
                {
                    this._number = new uint[] { (uint)(0 - number) };
                    this._nagative = true;
                }
                else
                    this._number = new uint[] { (uint)number };
            }
            public IntDynamic(char number)
            {
                this._number = new uint[] { (uint)number };
            }
            public IntDynamic(bool number)
            {
                this._number = new uint[] { number ? (uint)1 : (uint)0 };
            }
            public IntDynamic(uint[] number)
            {
                int length = number.Length;
                this._number = new uint[length];
                for (int counter = 0; counter < length; counter++)
                    this._number[counter] = number[counter];
                this._nagative = false;
            }
            public IntDynamic(uint[] number, bool nagative)
            {
                int length = number.Length;
                this._number = new uint[length];
                for (int counter = 0; counter < length; counter++)
                    this._number[counter] = number[counter];
                this._nagative = nagative;
            }
            public IntDynamic(string number)
            {
                try
                {
                    int index = 0;
                    int length = number.Length;
                    for (; index < length; index++)
                        if (number[index] == '-') this._nagative = !this._nagative;
                        else if (number[index] != '+') break;
                    if (index == length - 1) throw new Exception("The number is not in a correct format.");
                    this._number = UInt.Convert.Parse(number.Substring(index, length - index).TrimStart('0'));
                }
                catch
                {
                    throw new Exception("The number is not in a correct format.");
                }
            }
            public IntDynamic(IntDynamic number)
            {
                int length = number.Array.Length;
                this._number = new uint[length];
                for (int counter = 0; counter < length; counter++)
                    this._number[counter] = number.Array[counter];
                this._nagative = number.Nagative;
            }
            public IntDynamic(UIntDynamic number)
            {
                int length = number.Array.Length;
                this._number = new uint[length];
                for (int counter = 0; counter < length; counter++)
                    this._number[counter] = number.Array[counter];
            }
        #endregion
        #region Methods
            #region Convertions
                static public IntDynamic Parse(bool number)
                {
                    return new IntDynamic(number);
                }
                static public IntDynamic Parse(byte number)
                {
                    return new IntDynamic(number);
                }
                static public IntDynamic Parse(sbyte number)
                {
                    return new IntDynamic(number);
                }
                static public IntDynamic Parse(char number)
                {
                    return new IntDynamic(number);
                }
                static public IntDynamic Parse(ushort number)
                {
                    return new IntDynamic(number);
                }
                static public IntDynamic Parse(short number)
                {
                    return new IntDynamic(number);
                }
                static public IntDynamic Parse(uint number)
                {
                    return new IntDynamic(number);
                }
                static public IntDynamic Parse(int number)
                {
                    return new IntDynamic(number);
                }
                static public IntDynamic Parse(ulong number)
                {
                    return new IntDynamic(number);
                }
                static public IntDynamic Parse(long number)
                {
                    return new IntDynamic(number);
                }
                static public IntDynamic Parse(string number)
                {
                    return new IntDynamic(number);
                }
                static public IntDynamic Parse(uint[] number)
                {
                    return new IntDynamic(number);
                }
                static public IntDynamic Parse(uint[] number, bool nagative)
                {
                    return new IntDynamic(number, nagative);
                }
                static public bool TryParse(string number, out IntDynamic result)
                {
                    try
                    {
                        result = new IntDynamic(number);
                        return true;
                    }
                    catch
                    {
                        result = new IntDynamic();
                        return false;
                    }
                }
                public override string ToString()
                {
                    return this.ToString(SignShow.ShowOnlyNagative);
                }
                public string ToString(SignShow ShowSign)
                {
                    if (ShowSign == SignShow.DontShow)
                        return UInt.Convert.ToString(this._number);
                    else if (ShowSign == SignShow.ShowAlways)
                        return (this._nagative ? "-" : "+") + UInt.Convert.ToString(this._number);
                    else
                        if (this._nagative)
                            return "-" + UInt.Convert.ToString(this._number);
                        else
                            return UInt.Convert.ToString(this._number);
                }
            #endregion
            public override bool Equals(object obj)
            {
                return this == (IntDynamic)obj;
            }
            public override int GetHashCode()
            {
                return base.GetHashCode();
            }
        #endregion
        #region Properties
            public uint[] Array
            {
                get
                {
                    return this._number;
                }
                set
                {
                    this._number = value;
                }
            }
            public bool Nagative
            {
                get
                {
                    return this._nagative;
                }
                set
                {
                    this._nagative = value;
                }
            }
        #endregion
        #region Static Operations
            #region Binary Operations
                static public IntDynamic Addition(IntDynamic a, IntDynamic b)
                {
                    if (a.Nagative)
                        return Subtraction(b, new IntDynamic(a) { Nagative = false });
                    if (b.Nagative)
                        return Subtraction(a, new IntDynamic(b) { Nagative = false });
                    return new IntDynamic(UInt.UIntOperations.Addition(a.Array, b.Array), false);
                }
                static public IntDynamic Incremention(IntDynamic number)
                {
                    if (number.Nagative)
                        if (number.Array.Length == 1 && number.Array[0] == 1)
                            return new IntDynamic(0);
                        else
                            return new IntDynamic(UInt.UIntOperations.Decrement(number.Array), true);
                    return new IntDynamic(UInt.UIntOperations.Increment(number.Array), false);
                }
                static public IntDynamic Subtraction(IntDynamic a, IntDynamic b)
                {
                    if (b.Nagative)
                        return Addition(a, new IntDynamic(b) { Nagative = false });
                    if (a.Nagative)
                        return new IntDynamic(UInt.UIntOperations.Addition(a.Array, b.Array), true);
                    if (a >= b)
                        return new IntDynamic(UInt.UIntOperations.Subtraction(a.Array, b.Array), false);
                    else
                        return new IntDynamic(UInt.UIntOperations.Subtraction(b.Array, a.Array), true);
                }
                static public IntDynamic Decremention(IntDynamic number)
                {
                    if (number.Array.Length == 1 && number.Array[0] == 0)
                        return new IntDynamic(-1);
                    if (number.Nagative)
                        return new IntDynamic(UInt.UIntOperations.Increment(number.Array), true);
                    return new IntDynamic(UInt.UIntOperations.Decrement(number.Array), false);
                }
                static public IntDynamic Multiplication(IntDynamic a, IntDynamic b)
                {
                    return new IntDynamic(UInt.UIntOperations.Multiplication(a.Array, b.Array),
                        a.Nagative ^ b.Nagative);
                }
                static public IntDynamic Division(IntDynamic a, IntDynamic b)
                {
                    if (b.Array.Length == 1 && b.Array[0] == 0)
                        throw new Exception("Division by zero.");
                    if (b.Array.Length == 1 && b.Array[0] == 1)
                        return new IntDynamic(a);
                    if (a.Array.Length == 1 && a.Array[0] == 1)
                        return new IntDynamic(new uint[] { 0 },
                        a.Nagative ^ b.Nagative);
                    if (a.Array.Length == 1 && a.Array[0] == 0)
                        return new IntDynamic(new uint[] { 0 },
                        a.Nagative ^ b.Nagative);
                    if (UInt.UIntOperations.Equal(a.Array, b.Array))
                        return new IntDynamic(new uint[] { 1 },
                        a.Nagative ^ b.Nagative);
                    return new IntDynamic(UInt.UIntOperations.Division(a.Array, b.Array),
                        a.Nagative ^ b.Nagative);
                }
                static public IntDynamic Division(IntDynamic a, IntDynamic b, out IntDynamic modulo)
                {
                    if (b.Array.Length == 1 && b.Array[0] == 0)
                    {
                        modulo = new IntDynamic(0);
                        throw new Exception("Division by zero.");
                    }
                    if (b.Array.Length == 1 && b.Array[0] == 1)
                    {
                        modulo = new IntDynamic(0);
                        return new IntDynamic(a.Array,
                            a.Nagative ^ b.Nagative);
                    }
                    if (a.Array.Length == 1 && a.Array[0] == 1)
                    {
                        modulo = new IntDynamic(1);
                        return new IntDynamic(new uint[] { 0 },
                            a.Nagative ^ b.Nagative);
                    }
                    if (a.Array.Length == 1 && a.Array[0] == 0)
                    {
                        modulo = new IntDynamic(0);
                        return new IntDynamic(new uint[] { 0 },
                            a.Nagative ^ b.Nagative);
                    }
                    if (UInt.UIntOperations.Equal(a.Array, b.Array))
                    {
                        modulo = new IntDynamic(0);
                        return new IntDynamic(new uint[] { 1 },
                            a.Nagative ^ b.Nagative);
                    }
                    uint[] Modulo;
                    IntDynamic result = new IntDynamic(UInt.UIntOperations.Division(a.Array, b.Array, out Modulo),
                            a.Nagative ^ b.Nagative);
                    modulo = new IntDynamic(Modulo, false);
                    return result;
                }
                static public IntDynamic Modulo(IntDynamic a, IntDynamic b)
                {
                    if (b.Array.Length == 1 && b.Array[0] == 0)
                        throw new Exception("Division by zero.");
                    if (b.Array.Length == 1 && b.Array[0] == 1)
                        return new IntDynamic();
                    if (a.Array.Length == 1 && a.Array[0] == 1)
                        return new IntDynamic(1);
                    if (a.Array.Length == 1 && a.Array[0] == 0)
                        return new IntDynamic();
                    if (UInt.UIntOperations.Equal(a.Array, b.Array))
                        return new IntDynamic();
                    return new IntDynamic(UInt.UIntOperations.Modulo(a.Array, b.Array), false);
                }
                static public IntDynamic Power(IntDynamic a, IntDynamic b)
                {
                    if (b.Nagative)
                        if (b.Array.Length == 1 && b.Array[0] == 1)
                            if (a.Array.Length == 1 && a.Array[0] == 1)
                                if (a.Nagative)
                                    return new IntDynamic(-1);
                                else
                                    return new IntDynamic(1);
                            else
                                return new IntDynamic(0);
                        else
                            return new IntDynamic(0);
                    if (a.Nagative)
                        return new IntDynamic(UInt.UIntOperations.Power(a.Array, b.Array), (b.Array[0] & 1) == 0);
                    else
                        return new IntDynamic(UInt.UIntOperations.Power(a.Array, b.Array), false);
                }
                static public IntDynamic Root(IntDynamic a, IntDynamic b)
                {
                    if (a.Array.Length == 1 && a.Array[0] == 0)
                        if (!b.Nagative && b.Array.Length == 1 && b.Array[0] == 1)
                            return new IntDynamic(b);
                        else
                            throw new Exception("Arithmetic operation resulted with no answer");
                    if (a.Nagative)
                        if (b.Nagative)
                            if (b.Array.Length == 1 && b.Array[0] == 1)
                                if ((a.Array[0] & 1) == 1)
                                    return new IntDynamic(-1);
                                else
                                    throw new Exception("Arithmetic operation resulted with no answer");
                            else
                                return new IntDynamic(0);
                        else
                            if (b.Array.Length == 1 && b.Array[0] == 1)
                                return new IntDynamic(1);
                            else
                                return new IntDynamic(0);
                    if (b.Nagative)
                        if ((a.Array[0] & 1) == 0)
                            throw new Exception("Arithmetic operation resulted with no answer");
                        else
                            return new IntDynamic(UInt.UIntOperations.Root(a.Array, b.Array), true);
                    return new IntDynamic(UInt.UIntOperations.Root(a.Array, b.Array), false);
                }
            #endregion
            #region Bitwise Operations
                static public IntDynamic Not(IntDynamic number)
                {
                    return new IntDynamic(UInt.UIntOperations.Not(number.Array), number.Nagative);
                }
                static public IntDynamic And(IntDynamic a, IntDynamic b)
                {
                    return new IntDynamic(UInt.UIntOperations.And(a.Array, b.Array), a.Nagative & b.Nagative);
                }
                static public IntDynamic Or(IntDynamic a, IntDynamic b)
                {
                    return new IntDynamic(UInt.UIntOperations.Or(a.Array, b.Array), a.Nagative | b.Nagative);
                }
                static public IntDynamic Nand(IntDynamic a, IntDynamic b)
                {
                    return new IntDynamic(UInt.UIntOperations.Nand(a.Array, b.Array), !(a.Nagative & b.Nagative));
                }
                static public IntDynamic Nor(IntDynamic a, IntDynamic b)
                {
                    return new IntDynamic(UInt.UIntOperations.Nor(a.Array, b.Array), !(a.Nagative | b.Nagative));
                }
                static public IntDynamic Xor(IntDynamic a, IntDynamic b)
                {
                    return new IntDynamic(UInt.UIntOperations.Xor(a.Array, b.Array), a.Nagative ^ b.Nagative);
                }
                static public IntDynamic Xnor(IntDynamic a, IntDynamic b)
                {
                    return new IntDynamic(UInt.UIntOperations.Xnor(a.Array, b.Array), !(a.Nagative ^ b.Nagative));
                }
                static public IntDynamic LeftShift(IntDynamic a, IntDynamic b)
                {
                    if (b.Nagative)
                        return new IntDynamic(UInt.UIntOperations.RightShift(a.Array, b.Array), a.Nagative);
                    return new IntDynamic(UInt.UIntOperations.LeftShift(a.Array, b.Array), a.Nagative);
                }
                static public IntDynamic RightShift(IntDynamic a, IntDynamic b)
                {
                    if (b.Nagative)
                        return new IntDynamic(UInt.UIntOperations.LeftShift(a.Array, b.Array), a.Nagative);
                    return new IntDynamic(UInt.UIntOperations.RightShift(a.Array, b.Array), a.Nagative);
                }
                static public IntDynamic LeftShiftCircular(IntDynamic a, IntDynamic b)
                {
                    if (b.Nagative)
                        return new IntDynamic(UInt.UIntOperations.RightShiftCircular(a.Array, b.Array), a.Nagative);
                    return new IntDynamic(UInt.UIntOperations.LeftShiftCircular(a.Array, b.Array), a.Nagative);
                }
                static public IntDynamic RightShiftCircular(IntDynamic a, IntDynamic b)
                {
                    if (b.Nagative)
                        return new IntDynamic(UInt.UIntOperations.LeftShiftCircular(a.Array, b.Array), a.Nagative);
                    return new IntDynamic(UInt.UIntOperations.RightShiftCircular(a.Array, b.Array), a.Nagative);
                }
            #endregion
            #region Unary Operations
                static public IntDynamic Factorial(IntDynamic number)
                {
                    return new IntDynamic(UInt.UIntOperations.Factorial(number.Array), number.Nagative && (number.Array[0] & 1) == 1);
                }
                static public IntDynamic ReverseUints(IntDynamic number)
                {
                    return new IntDynamic(number.Array.Reverse().ToArray(), number.Nagative);
                }
                static public IntDynamic ReverseBits(IntDynamic number)
                {
                    return new IntDynamic(UInt.UIntOperations.ReverseBinary(number.Array), number.Nagative);
                }
            #endregion
        #endregion
        #region Operators
            #region + Operator
                static public IntDynamic operator +(IntDynamic a, IntDynamic b)
                {
                    return Addition(a, b);
                }
                static public IntDynamic operator +(IntDynamic a, string b)
                {
                    return a + (IntDynamic)b;
                }
                static public IntDynamic operator +(IntDynamic a, ulong b)
                {
                    return a + (IntDynamic)b;
                }
                static public IntDynamic operator +(IntDynamic a, long b)
                {
                    return a + (IntDynamic)b;
                }
                static public IntDynamic operator +(IntDynamic a, uint b)
                {
                    return a + (IntDynamic)b;
                }
                static public IntDynamic operator +(IntDynamic a, int b)
                {
                    return a + (IntDynamic)b;
                }
                static public IntDynamic operator +(IntDynamic a, ushort b)
                {
                    return a + (IntDynamic)b;
                }
                static public IntDynamic operator +(IntDynamic a, short b)
                {
                    return a + (IntDynamic)b;
                }
                static public IntDynamic operator +(IntDynamic a, char b)
                {
                    return a + (IntDynamic)b;
                }
                static public IntDynamic operator +(IntDynamic a, byte b)
                {
                    return a + (IntDynamic)b;
                }
                static public IntDynamic operator +(IntDynamic a, sbyte b)
                {
                    return a + (IntDynamic)b;
                }
                static public IntDynamic operator +(IntDynamic a, bool b)
                {
                    return a + (IntDynamic)b;
                }
                static public IntDynamic operator +(string a, IntDynamic b)
                {
                    return (IntDynamic)a + b;
                }
                static public IntDynamic operator +(ulong a, IntDynamic b)
                {
                    return (IntDynamic)a + b;
                }
                static public IntDynamic operator +(long a, IntDynamic b)
                {
                    return (IntDynamic)a + b;
                }
                static public IntDynamic operator +(uint a, IntDynamic b)
                {
                    return (IntDynamic)a + b;
                }
                static public IntDynamic operator +(int a, IntDynamic b)
                {
                    return (IntDynamic)a + b;
                }
                static public IntDynamic operator +(ushort a, IntDynamic b)
                {
                    return (IntDynamic)a + b;
                }
                static public IntDynamic operator +(short a, IntDynamic b)
                {
                    return (IntDynamic)a + b;
                }
                static public IntDynamic operator +(char a, IntDynamic b)
                {
                    return (IntDynamic)a + b;
                }
                static public IntDynamic operator +(byte a, IntDynamic b)
                {
                    return (IntDynamic)a + b;
                }
                static public IntDynamic operator +(sbyte a, IntDynamic b)
                {
                    return (IntDynamic)a + b;
                }
                static public IntDynamic operator +(bool a, IntDynamic b)
                {
                    return (IntDynamic)a + b;
                }
                static public IntDynamic operator ++(IntDynamic number)
                {
                    return Incremention(number);
                }
            #endregion
            #region - Operator
                static public IntDynamic operator -(IntDynamic a, IntDynamic b)
                {
                    return Subtraction(a, b);
                }
                static public IntDynamic operator -(IntDynamic a, string b)
                {
                    return a - (IntDynamic)b;
                }
                static public IntDynamic operator -(IntDynamic a, ulong b)
                {
                    return a - (IntDynamic)b;
                }
                static public IntDynamic operator -(IntDynamic a, long b)
                {
                    return a - (IntDynamic)b;
                }
                static public IntDynamic operator -(IntDynamic a, uint b)
                {
                    return a - (IntDynamic)b;
                }
                static public IntDynamic operator -(IntDynamic a, int b)
                {
                    return a - (IntDynamic)b;
                }
                static public IntDynamic operator -(IntDynamic a, ushort b)
                {
                    return a - (IntDynamic)b;
                }
                static public IntDynamic operator -(IntDynamic a, short b)
                {
                    return a - (IntDynamic)b;
                }
                static public IntDynamic operator -(IntDynamic a, char b)
                {
                    return a - (IntDynamic)b;
                }
                static public IntDynamic operator -(IntDynamic a, byte b)
                {
                    return a - (IntDynamic)b;
                }
                static public IntDynamic operator -(IntDynamic a, sbyte b)
                {
                    return a - (IntDynamic)b;
                }
                static public IntDynamic operator -(IntDynamic a, bool b)
                {
                    return a - (IntDynamic)b;
                }
                static public IntDynamic operator -(string a, IntDynamic b)
                {
                    return (IntDynamic)a - b;
                }
                static public IntDynamic operator -(ulong a, IntDynamic b)
                {
                    return (IntDynamic)a - b;
                }
                static public IntDynamic operator -(long a, IntDynamic b)
                {
                    return (IntDynamic)a - b;
                }
                static public IntDynamic operator -(uint a, IntDynamic b)
                {
                    return (IntDynamic)a - b;
                }
                static public IntDynamic operator -(int a, IntDynamic b)
                {
                    return (IntDynamic)a - b;
                }
                static public IntDynamic operator -(ushort a, IntDynamic b)
                {
                    return (IntDynamic)a - b;
                }
                static public IntDynamic operator -(short a, IntDynamic b)
                {
                    return (IntDynamic)a - b;
                }
                static public IntDynamic operator -(char a, IntDynamic b)
                {
                    return (IntDynamic)a - b;
                }
                static public IntDynamic operator -(byte a, IntDynamic b)
                {
                    return (IntDynamic)a - b;
                }
                static public IntDynamic operator -(sbyte a, IntDynamic b)
                {
                    return (IntDynamic)a - b;
                }
                static public IntDynamic operator -(bool a, IntDynamic b)
                {
                    return (IntDynamic)a - b;
                }
                static public IntDynamic operator --(IntDynamic number)
                {
                    return Decremention(number);
                }
                #endregion
            #region * Operator
                static public IntDynamic operator *(IntDynamic a, IntDynamic b)
                {
                    return Multiplication(a, b);
                }
                static public IntDynamic operator *(IntDynamic a, string b)
                {
                    return a * (IntDynamic)b;
                }
                static public IntDynamic operator *(IntDynamic a, ulong b)
                {
                    return a * (IntDynamic)b;
                }
                static public IntDynamic operator *(IntDynamic a, long b)
                {
                    return a * (IntDynamic)b;
                }
                static public IntDynamic operator *(IntDynamic a, uint b)
                {
                    return a * (IntDynamic)b;
                }
                static public IntDynamic operator *(IntDynamic a, int b)
                {
                    return a * (IntDynamic)b;
                }
                static public IntDynamic operator *(IntDynamic a, ushort b)
                {
                    return a * (IntDynamic)b;
                }
                static public IntDynamic operator *(IntDynamic a, short b)
                {
                    return a * (IntDynamic)b;
                }
                static public IntDynamic operator *(IntDynamic a, char b)
                {
                    return a * (IntDynamic)b;
                }
                static public IntDynamic operator *(IntDynamic a, byte b)
                {
                    return a * (IntDynamic)b;
                }
                static public IntDynamic operator *(IntDynamic a, sbyte b)
                {
                    return a * (IntDynamic)b;
                }
                static public IntDynamic operator *(IntDynamic a, bool b)
                {
                    return a * (IntDynamic)b;
                }
                static public IntDynamic operator *(string a, IntDynamic b)
                {
                    return (IntDynamic)a * b;
                }
                static public IntDynamic operator *(ulong a, IntDynamic b)
                {
                    return (IntDynamic)a * b;
                }
                static public IntDynamic operator *(long a, IntDynamic b)
                {
                    return (IntDynamic)a * b;
                }
                static public IntDynamic operator *(uint a, IntDynamic b)
                {
                    return (IntDynamic)a * b;
                }
                static public IntDynamic operator *(int a, IntDynamic b)
                {
                    return (IntDynamic)a * b;
                }
                static public IntDynamic operator *(ushort a, IntDynamic b)
                {
                    return (IntDynamic)a * b;
                }
                static public IntDynamic operator *(short a, IntDynamic b)
                {
                    return (IntDynamic)a * b;
                }
                static public IntDynamic operator *(char a, IntDynamic b)
                {
                    return (IntDynamic)a * b;
                }
                static public IntDynamic operator *(byte a, IntDynamic b)
                {
                    return (IntDynamic)a * b;
                }
                static public IntDynamic operator *(sbyte a, IntDynamic b)
                {
                    return (IntDynamic)a * b;
                }
                static public IntDynamic operator *(bool a, IntDynamic b)
                {
                    return (IntDynamic)a * b;
                }
            #endregion
            #region / Operator
                static public IntDynamic operator /(IntDynamic a, IntDynamic b)
                {
                    return Division(a, b);
                }
                static public IntDynamic operator /(IntDynamic a, string b)
                {
                    return a / (IntDynamic)b;
                }
                static public IntDynamic operator /(IntDynamic a, ulong b)
                {
                    return a / (IntDynamic)b;
                }
                static public IntDynamic operator /(IntDynamic a, long b)
                {
                    return a / (IntDynamic)b;
                }
                static public IntDynamic operator /(IntDynamic a, uint b)
                {
                    return a / (IntDynamic)b;
                }
                static public IntDynamic operator /(IntDynamic a, int b)
                {
                    return a / (IntDynamic)b;
                }
                static public IntDynamic operator /(IntDynamic a, ushort b)
                {
                    return a / (IntDynamic)b;
                }
                static public IntDynamic operator /(IntDynamic a, short b)
                {
                    return a / (IntDynamic)b;
                }
                static public IntDynamic operator /(IntDynamic a, char b)
                {
                    return a / (IntDynamic)b;
                }
                static public IntDynamic operator /(IntDynamic a, byte b)
                {
                    return a / (IntDynamic)b;
                }
                static public IntDynamic operator /(IntDynamic a, sbyte b)
                {
                    return a / (IntDynamic)b;
                }
                static public IntDynamic operator /(IntDynamic a, bool b)
                {
                    return a / (IntDynamic)b;
                }
                static public IntDynamic operator /(string a, IntDynamic b)
                {
                    return (IntDynamic)a / b;
                }
                static public IntDynamic operator /(ulong a, IntDynamic b)
                {
                    return (IntDynamic)a / b;
                }
                static public IntDynamic operator /(long a, IntDynamic b)
                {
                    return (IntDynamic)a / b;
                }
                static public IntDynamic operator /(uint a, IntDynamic b)
                {
                    return (IntDynamic)a / b;
                }
                static public IntDynamic operator /(int a, IntDynamic b)
                {
                    return (IntDynamic)a / b;
                }
                static public IntDynamic operator /(ushort a, IntDynamic b)
                {
                    return (IntDynamic)a / b;
                }
                static public IntDynamic operator /(short a, IntDynamic b)
                {
                    return (IntDynamic)a / b;
                }
                static public IntDynamic operator /(char a, IntDynamic b)
                {
                    return (IntDynamic)a / b;
                }
                static public IntDynamic operator /(byte a, IntDynamic b)
                {
                    return (IntDynamic)a / b;
                }
                static public IntDynamic operator /(sbyte a, IntDynamic b)
                {
                    return (IntDynamic)a / b;
                }
                static public IntDynamic operator /(bool a, IntDynamic b)
                {
                    return (IntDynamic)a / b;
                }
            #endregion
            #region % Operator
                static public IntDynamic operator %(IntDynamic a, IntDynamic b)
                {
                    return Modulo(a, b);
                }
                static public IntDynamic operator %(IntDynamic a, string b)
                {
                    return a % (IntDynamic)b;
                }
                static public IntDynamic operator %(IntDynamic a, ulong b)
                {
                    return a % (IntDynamic)b;
                }
                static public IntDynamic operator %(IntDynamic a, long b)
                {
                    return a % (IntDynamic)b;
                }
                static public IntDynamic operator %(IntDynamic a, uint b)
                {
                    return a % (IntDynamic)b;
                }
                static public IntDynamic operator %(IntDynamic a, int b)
                {
                    return a % (IntDynamic)b;
                }
                static public IntDynamic operator %(IntDynamic a, ushort b)
                {
                    return a % (IntDynamic)b;
                }
                static public IntDynamic operator %(IntDynamic a, short b)
                {
                    return a % (IntDynamic)b;
                }
                static public IntDynamic operator %(IntDynamic a, char b)
                {
                    return a % (IntDynamic)b;
                }
                static public IntDynamic operator %(IntDynamic a, byte b)
                {
                    return a % (IntDynamic)b;
                }
                static public IntDynamic operator %(IntDynamic a, sbyte b)
                {
                    return a % (IntDynamic)b;
                }
                static public IntDynamic operator %(IntDynamic a, bool b)
                {
                    return a % (IntDynamic)b;
                }
                static public IntDynamic operator %(string a, IntDynamic b)
                {
                    return (IntDynamic)a % b;
                }
                static public IntDynamic operator %(ulong a, IntDynamic b)
                {
                    return (IntDynamic)a % b;
                }
                static public IntDynamic operator %(long a, IntDynamic b)
                {
                    return (IntDynamic)a % b;
                }
                static public IntDynamic operator %(uint a, IntDynamic b)
                {
                    return (IntDynamic)a % b;
                }
                static public IntDynamic operator %(int a, IntDynamic b)
                {
                    return (IntDynamic)a % b;
                }
                static public IntDynamic operator %(ushort a, IntDynamic b)
                {
                    return (IntDynamic)a % b;
                }
                static public IntDynamic operator %(short a, IntDynamic b)
                {
                    return (IntDynamic)a % b;
                }
                static public IntDynamic operator %(char a, IntDynamic b)
                {
                    return (IntDynamic)a % b;
                }
                static public IntDynamic operator %(byte a, IntDynamic b)
                {
                    return (IntDynamic)a % b;
                }
                static public IntDynamic operator %(sbyte a, IntDynamic b)
                {
                    return (IntDynamic)a % b;
                }
                static public IntDynamic operator %(bool a, IntDynamic b)
                {
                    return (IntDynamic)a % b;
                }
            #endregion
            #region ^ Operator
                static public IntDynamic operator ^(IntDynamic a, IntDynamic b)
                {
                    return Xor(a, b);
                }
                static public IntDynamic operator ^(IntDynamic a, string b)
                {
                    return a ^ (IntDynamic)b;
                }
                static public IntDynamic operator ^(IntDynamic a, ulong b)
                {
                    return a ^ (IntDynamic)b;
                }
                static public IntDynamic operator ^(IntDynamic a, long b)
                {
                    return a ^ (IntDynamic)b;
                }
                static public IntDynamic operator ^(IntDynamic a, uint b)
                {
                    return a ^ (IntDynamic)b;
                }
                static public IntDynamic operator ^(IntDynamic a, int b)
                {
                    return a ^ (IntDynamic)b;
                }
                static public IntDynamic operator ^(IntDynamic a, ushort b)
                {
                    return a ^ (IntDynamic)b;
                }
                static public IntDynamic operator ^(IntDynamic a, short b)
                {
                    return a ^ (IntDynamic)b;
                }
                static public IntDynamic operator ^(IntDynamic a, char b)
                {
                    return a ^ (IntDynamic)b;
                }
                static public IntDynamic operator ^(IntDynamic a, byte b)
                {
                    return a ^ (IntDynamic)b;
                }
                static public IntDynamic operator ^(IntDynamic a, sbyte b)
                {
                    return a ^ (IntDynamic)b;
                }
                static public IntDynamic operator ^(IntDynamic a, bool b)
                {
                    return a ^ (IntDynamic)b;
                }
                static public IntDynamic operator ^(string a, IntDynamic b)
                {
                    return (IntDynamic)a ^ b;
                }
                static public IntDynamic operator ^(ulong a, IntDynamic b)
                {
                    return (IntDynamic)a ^ b;
                }
                static public IntDynamic operator ^(long a, IntDynamic b)
                {
                    return (IntDynamic)a ^ b;
                }
                static public IntDynamic operator ^(uint a, IntDynamic b)
                {
                    return (IntDynamic)a ^ b;
                }
                static public IntDynamic operator ^(int a, IntDynamic b)
                {
                    return (IntDynamic)a ^ b;
                }
                static public IntDynamic operator ^(ushort a, IntDynamic b)
                {
                    return (IntDynamic)a ^ b;
                }
                static public IntDynamic operator ^(short a, IntDynamic b)
                {
                    return (IntDynamic)a ^ b;
                }
                static public IntDynamic operator ^(char a, IntDynamic b)
                {
                    return (IntDynamic)a ^ b;
                }
                static public IntDynamic operator ^(byte a, IntDynamic b)
                {
                    return (IntDynamic)a ^ b;
                }
                static public IntDynamic operator ^(sbyte a, IntDynamic b)
                {
                    return (IntDynamic)a ^ b;
                }
                static public IntDynamic operator ^(bool a, IntDynamic b)
                {
                    return (IntDynamic)a ^ b;
                }
            #endregion
            #region ~ Operator
                static public IntDynamic operator ~(IntDynamic number)
                {
                    return Not(number);
                }
            #endregion
            #region | Operator
                static public IntDynamic operator |(IntDynamic a, IntDynamic b)
                {
                    return Or(a, b);
                }
                static public IntDynamic operator |(IntDynamic a, string b)
                {
                    return a | (IntDynamic)b;
                }
                static public IntDynamic operator |(IntDynamic a, ulong b)
                {
                    return a | (IntDynamic)b;
                }
                static public IntDynamic operator |(IntDynamic a, long b)
                {
                    return a | (IntDynamic)b;
                }
                static public IntDynamic operator |(IntDynamic a, uint b)
                {
                    return a | (IntDynamic)b;
                }
                static public IntDynamic operator |(IntDynamic a, int b)
                {
                    return a | (IntDynamic)b;
                }
                static public IntDynamic operator |(IntDynamic a, ushort b)
                {
                    return a | (IntDynamic)b;
                }
                static public IntDynamic operator |(IntDynamic a, short b)
                {
                    return a | (IntDynamic)b;
                }
                static public IntDynamic operator |(IntDynamic a, char b)
                {
                    return a | (IntDynamic)b;
                }
                static public IntDynamic operator |(IntDynamic a, byte b)
                {
                    return a | (IntDynamic)b;
                }
                static public IntDynamic operator |(IntDynamic a, sbyte b)
                {
                    return a | (IntDynamic)b;
                }
                static public IntDynamic operator |(IntDynamic a, bool b)
                {
                    return a | (IntDynamic)b;
                }
                static public IntDynamic operator |(string a, IntDynamic b)
                {
                    return (IntDynamic)a | b;
                }
                static public IntDynamic operator |(ulong a, IntDynamic b)
                {
                    return (IntDynamic)a | b;
                }
                static public IntDynamic operator |(long a, IntDynamic b)
                {
                    return (IntDynamic)a | b;
                }
                static public IntDynamic operator |(uint a, IntDynamic b)
                {
                    return (IntDynamic)a | b;
                }
                static public IntDynamic operator |(int a, IntDynamic b)
                {
                    return (IntDynamic)a | b;
                }
                static public IntDynamic operator |(ushort a, IntDynamic b)
                {
                    return (IntDynamic)a | b;
                }
                static public IntDynamic operator |(short a, IntDynamic b)
                {
                    return (IntDynamic)a | b;
                }
                static public IntDynamic operator |(char a, IntDynamic b)
                {
                    return (IntDynamic)a | b;
                }
                static public IntDynamic operator |(byte a, IntDynamic b)
                {
                    return (IntDynamic)a | b;
                }
                static public IntDynamic operator |(sbyte a, IntDynamic b)
                {
                    return (IntDynamic)a | b;
                }
                static public IntDynamic operator |(bool a, IntDynamic b)
                {
                    return (IntDynamic)a | b;
                }
            #endregion
            #region & Operator
                static public IntDynamic operator &(IntDynamic a, IntDynamic b)
                {
                    return And(a, b);
                }
                static public IntDynamic operator &(IntDynamic a, string b)
                {
                    return a & (IntDynamic)b;
                }
                static public IntDynamic operator &(IntDynamic a, ulong b)
                {
                    return a & (IntDynamic)b;
                }
                static public IntDynamic operator &(IntDynamic a, long b)
                {
                    return a & (IntDynamic)b;
                }
                static public IntDynamic operator &(IntDynamic a, uint b)
                {
                    return a & (IntDynamic)b;
                }
                static public IntDynamic operator &(IntDynamic a, int b)
                {
                    return a & (IntDynamic)b;
                }
                static public IntDynamic operator &(IntDynamic a, ushort b)
                {
                    return a & (IntDynamic)b;
                }
                static public IntDynamic operator &(IntDynamic a, short b)
                {
                    return a & (IntDynamic)b;
                }
                static public IntDynamic operator &(IntDynamic a, char b)
                {
                    return a & (IntDynamic)b;
                }
                static public IntDynamic operator &(IntDynamic a, byte b)
                {
                    return a & (IntDynamic)b;
                }
                static public IntDynamic operator &(IntDynamic a, sbyte b)
                {
                    return a & (IntDynamic)b;
                }
                static public IntDynamic operator &(IntDynamic a, bool b)
                {
                    return a & (IntDynamic)b;
                }
                static public IntDynamic operator &(string a, IntDynamic b)
                {
                    return (IntDynamic)a & b;
                }
                static public IntDynamic operator &(ulong a, IntDynamic b)
                {
                    return (IntDynamic)a & b;
                }
                static public IntDynamic operator &(long a, IntDynamic b)
                {
                    return (IntDynamic)a & b;
                }
                static public IntDynamic operator &(uint a, IntDynamic b)
                {
                    return (IntDynamic)a & b;
                }
                static public IntDynamic operator &(int a, IntDynamic b)
                {
                    return (IntDynamic)a & b;
                }
                static public IntDynamic operator &(ushort a, IntDynamic b)
                {
                    return (IntDynamic)a & b;
                }
                static public IntDynamic operator &(short a, IntDynamic b)
                {
                    return (IntDynamic)a & b;
                }
                static public IntDynamic operator &(char a, IntDynamic b)
                {
                    return (IntDynamic)a & b;
                }
                static public IntDynamic operator &(byte a, IntDynamic b)
                {
                    return (IntDynamic)a & b;
                }
                static public IntDynamic operator &(sbyte a, IntDynamic b)
                {
                    return (IntDynamic)a & b;
                }
                static public IntDynamic operator &(bool a, IntDynamic b)
                {
                    return (IntDynamic)a & b;
                }
            #endregion
            #region << >> Operators
                static public IntDynamic operator <<(IntDynamic a, int b)
                {
                    return LeftShift(a, (IntDynamic)b);
                }
                static public IntDynamic operator >>(IntDynamic a, int b)
                {
                    return RightShift(a, (IntDynamic)b);
                }
            #endregion
            #region > Operator
                static public bool operator >(IntDynamic a, IntDynamic b)
                {
                    if (a.Nagative)
                        if (b.Nagative)
                            return UInt.UIntOperations.Bigger(b.Array, a.Array);
                        else
                            return false;
                    if (b.Nagative)
                        return true;
                    return UInt.UIntOperations.Bigger(a.Array, b.Array);
                }
                static public bool operator >(IntDynamic a, string b)
                {
                    return a > (IntDynamic)b;
                }
                static public bool operator >(IntDynamic a, ulong b)
                {
                    return a > (IntDynamic)b;
                }
                static public bool operator >(IntDynamic a, long b)
                {
                    return a > (IntDynamic)b;
                }
                static public bool operator >(IntDynamic a, uint b)
                {
                    return a > (IntDynamic)b;
                }
                static public bool operator >(IntDynamic a, int b)
                {
                    return a > (IntDynamic)b;
                }
                static public bool operator >(IntDynamic a, ushort b)
                {
                    return a > (IntDynamic)b;
                }
                static public bool operator >(IntDynamic a, short b)
                {
                    return a > (IntDynamic)b;
                }
                static public bool operator >(IntDynamic a, char b)
                {
                    return a > (IntDynamic)b;
                }
                static public bool operator >(IntDynamic a, byte b)
                {
                    return a > (IntDynamic)b;
                }
                static public bool operator >(IntDynamic a, sbyte b)
                {
                    return a > (IntDynamic)b;
                }
                static public bool operator >(IntDynamic a, bool b)
                {
                    return a > (IntDynamic)b;
                }
                static public bool operator >(string a, IntDynamic b)
                {
                    return (IntDynamic)a > b;
                }
                static public bool operator >(ulong a, IntDynamic b)
                {
                    return (IntDynamic)a > b;
                }
                static public bool operator >(long a, IntDynamic b)
                {
                    return (IntDynamic)a > b;
                }
                static public bool operator >(uint a, IntDynamic b)
                {
                    return (IntDynamic)a > b;
                }
                static public bool operator >(int a, IntDynamic b)
                {
                    return (IntDynamic)a > b;
                }
                static public bool operator >(ushort a, IntDynamic b)
                {
                    return (IntDynamic)a > b;
                }
                static public bool operator >(short a, IntDynamic b)
                {
                    return (IntDynamic)a > b;
                }
                static public bool operator >(char a, IntDynamic b)
                {
                    return (IntDynamic)a > b;
                }
                static public bool operator >(byte a, IntDynamic b)
                {
                    return (IntDynamic)a > b;
                }
                static public bool operator >(sbyte a, IntDynamic b)
                {
                    return (IntDynamic)a > b;
                }
                static public bool operator >(bool a, IntDynamic b)
                {
                    return (IntDynamic)a > b;
                }
            #endregion
            #region < Operator
                static public bool operator <(IntDynamic a, IntDynamic b)
                {
                    if (a.Nagative)
                        if (b.Nagative)
                            return UInt.UIntOperations.Smaller(b.Array, a.Array);
                        else
                            return true;
                    if (b.Nagative)
                        return false;
                    return UInt.UIntOperations.Smaller(a.Array, b.Array);
                }
                static public bool operator <(IntDynamic a, string b)
                {
                    return a < (IntDynamic)b;
                }
                static public bool operator <(IntDynamic a, ulong b)
                {
                    return a < (IntDynamic)b;
                }
                static public bool operator <(IntDynamic a, long b)
                {
                    return a < (IntDynamic)b;
                }
                static public bool operator <(IntDynamic a, uint b)
                {
                    return a < (IntDynamic)b;
                }
                static public bool operator <(IntDynamic a, int b)
                {
                    return a < (IntDynamic)b;
                }
                static public bool operator <(IntDynamic a, ushort b)
                {
                    return a < (IntDynamic)b;
                }
                static public bool operator <(IntDynamic a, short b)
                {
                    return a < (IntDynamic)b;
                }
                static public bool operator <(IntDynamic a, char b)
                {
                    return a < (IntDynamic)b;
                }
                static public bool operator <(IntDynamic a, byte b)
                {
                    return a < (IntDynamic)b;
                }
                static public bool operator <(IntDynamic a, sbyte b)
                {
                    return a < (IntDynamic)b;
                }
                static public bool operator <(IntDynamic a, bool b)
                {
                    return a < (IntDynamic)b;
                }
                static public bool operator <(string a, IntDynamic b)
                {
                    return (IntDynamic)a < b;
                }
                static public bool operator <(ulong a, IntDynamic b)
                {
                    return (IntDynamic)a < b;
                }
                static public bool operator <(long a, IntDynamic b)
                {
                    return (IntDynamic)a < b;
                }
                static public bool operator <(uint a, IntDynamic b)
                {
                    return (IntDynamic)a < b;
                }
                static public bool operator <(int a, IntDynamic b)
                {
                    return (IntDynamic)a < b;
                }
                static public bool operator <(ushort a, IntDynamic b)
                {
                    return (IntDynamic)a < b;
                }
                static public bool operator <(short a, IntDynamic b)
                {
                    return (IntDynamic)a < b;
                }
                static public bool operator <(char a, IntDynamic b)
                {
                    return (IntDynamic)a < b;
                }
                static public bool operator <(byte a, IntDynamic b)
                {
                    return (IntDynamic)a < b;
                }
                static public bool operator <(sbyte a, IntDynamic b)
                {
                    return (IntDynamic)a < b;
                }
                static public bool operator <(bool a, IntDynamic b)
                {
                    return (IntDynamic)a < b;
                }
            #endregion
            #region >= Operator
                static public bool operator >=(IntDynamic a, IntDynamic b)
                {
                    if (a.Nagative)
                        if (b.Nagative)
                            return UInt.UIntOperations.BiggerEqual(b.Array, a.Array);
                        else
                            return false;
                    if (b.Nagative)
                        return true;
                    return UInt.UIntOperations.BiggerEqual(a.Array, b.Array);
                }
                static public bool operator >=(IntDynamic a, string b)
                {
                    return a >= (IntDynamic)b;
                }
                static public bool operator >=(IntDynamic a, ulong b)
                {
                    return a >= (IntDynamic)b;
                }
                static public bool operator >=(IntDynamic a, long b)
                {
                    return a >= (IntDynamic)b;
                }
                static public bool operator >=(IntDynamic a, uint b)
                {
                    return a >= (IntDynamic)b;
                }
                static public bool operator >=(IntDynamic a, int b)
                {
                    return a >= (IntDynamic)b;
                }
                static public bool operator >=(IntDynamic a, ushort b)
                {
                    return a >= (IntDynamic)b;
                }
                static public bool operator >=(IntDynamic a, short b)
                {
                    return a >= (IntDynamic)b;
                }
                static public bool operator >=(IntDynamic a, char b)
                {
                    return a >= (IntDynamic)b;
                }
                static public bool operator >=(IntDynamic a, byte b)
                {
                    return a >= (IntDynamic)b;
                }
                static public bool operator >=(IntDynamic a, sbyte b)
                {
                    return a >= (IntDynamic)b;
                }
                static public bool operator >=(IntDynamic a, bool b)
                {
                    return a >= (IntDynamic)b;
                }
                static public bool operator >=(string a, IntDynamic b)
                {
                    return (IntDynamic)a >= b;
                }
                static public bool operator >=(ulong a, IntDynamic b)
                {
                    return (IntDynamic)a >= b;
                }
                static public bool operator >=(long a, IntDynamic b)
                {
                    return (IntDynamic)a >= b;
                }
                static public bool operator >=(uint a, IntDynamic b)
                {
                    return (IntDynamic)a >= b;
                }
                static public bool operator >=(int a, IntDynamic b)
                {
                    return (IntDynamic)a >= b;
                }
                static public bool operator >=(ushort a, IntDynamic b)
                {
                    return (IntDynamic)a >= b;
                }
                static public bool operator >=(short a, IntDynamic b)
                {
                    return (IntDynamic)a >= b;
                }
                static public bool operator >=(char a, IntDynamic b)
                {
                    return (IntDynamic)a >= b;
                }
                static public bool operator >=(byte a, IntDynamic b)
                {
                    return (IntDynamic)a >= b;
                }
                static public bool operator >=(sbyte a, IntDynamic b)
                {
                    return (IntDynamic)a >= b;
                }
                static public bool operator >=(bool a, IntDynamic b)
                {
                    return (IntDynamic)a >= b;
                }
            #endregion
            #region <= Operator
                static public bool operator <=(IntDynamic a, IntDynamic b)
                {
                    if (a.Nagative)
                        if (b.Nagative)
                            return UInt.UIntOperations.SmallerEqual(b.Array, a.Array);
                        else
                            return true;
                    if (b.Nagative)
                        return false;
                    return UInt.UIntOperations.SmallerEqual(a.Array, b.Array);
                }
                static public bool operator <=(IntDynamic a, string b)
                {
                    return a <= (IntDynamic)b;
                }
                static public bool operator <=(IntDynamic a, ulong b)
                {
                    return a <= (IntDynamic)b;
                }
                static public bool operator <=(IntDynamic a, long b)
                {
                    return a <= (IntDynamic)b;
                }
                static public bool operator <=(IntDynamic a, uint b)
                {
                    return a <= (IntDynamic)b;
                }
                static public bool operator <=(IntDynamic a, int b)
                {
                    return a <= (IntDynamic)b;
                }
                static public bool operator <=(IntDynamic a, ushort b)
                {
                    return a <= (IntDynamic)b;
                }
                static public bool operator <=(IntDynamic a, short b)
                {
                    return a <= (IntDynamic)b;
                }
                static public bool operator <=(IntDynamic a, char b)
                {
                    return a <= (IntDynamic)b;
                }
                static public bool operator <=(IntDynamic a, byte b)
                {
                    return a <= (IntDynamic)b;
                }
                static public bool operator <=(IntDynamic a, sbyte b)
                {
                    return a <= (IntDynamic)b;
                }
                static public bool operator <=(IntDynamic a, bool b)
                {
                    return a <= (IntDynamic)b;
                }
                static public bool operator <=(string a, IntDynamic b)
                {
                    return (IntDynamic)a <= b;
                }
                static public bool operator <=(ulong a, IntDynamic b)
                {
                    return (IntDynamic)a <= b;
                }
                static public bool operator <=(long a, IntDynamic b)
                {
                    return (IntDynamic)a <= b;
                }
                static public bool operator <=(uint a, IntDynamic b)
                {
                    return (IntDynamic)a <= b;
                }
                static public bool operator <=(int a, IntDynamic b)
                {
                    return (IntDynamic)a <= b;
                }
                static public bool operator <=(ushort a, IntDynamic b)
                {
                    return (IntDynamic)a <= b;
                }
                static public bool operator <=(short a, IntDynamic b)
                {
                    return (IntDynamic)a <= b;
                }
                static public bool operator <=(char a, IntDynamic b)
                {
                    return (IntDynamic)a <= b;
                }
                static public bool operator <=(byte a, IntDynamic b)
                {
                    return (IntDynamic)a <= b;
                }
                static public bool operator <=(sbyte a, IntDynamic b)
                {
                    return (IntDynamic)a <= b;
                }
                static public bool operator <=(bool a, IntDynamic b)
                {
                    return (IntDynamic)a <= b;
                }
            #endregion
            #region == Operator
                static public bool operator ==(IntDynamic a, IntDynamic b)
                {
                    if (a.Nagative)
                        if (b.Nagative)
                            return UInt.UIntOperations.Equal(a.Array, b.Array);
                        else
                            return false;
                    if (b.Nagative)
                        return false;
                    return UInt.UIntOperations.Equal(a.Array, b.Array);
                }
                static public bool operator ==(IntDynamic a, string b)
                {
                    return a == (IntDynamic)b;
                }
                static public bool operator ==(IntDynamic a, ulong b)
                {
                    return a == (IntDynamic)b;
                }
                static public bool operator ==(IntDynamic a, long b)
                {
                    return a == (IntDynamic)b;
                }
                static public bool operator ==(IntDynamic a, uint b)
                {
                    return a == (IntDynamic)b;
                }
                static public bool operator ==(IntDynamic a, int b)
                {
                    return a == (IntDynamic)b;
                }
                static public bool operator ==(IntDynamic a, ushort b)
                {
                    return a == (IntDynamic)b;
                }
                static public bool operator ==(IntDynamic a, short b)
                {
                    return a == (IntDynamic)b;
                }
                static public bool operator ==(IntDynamic a, char b)
                {
                    return a == (IntDynamic)b;
                }
                static public bool operator ==(IntDynamic a, byte b)
                {
                    return a == (IntDynamic)b;
                }
                static public bool operator ==(IntDynamic a, sbyte b)
                {
                    return a == (IntDynamic)b;
                }
                static public bool operator ==(IntDynamic a, bool b)
                {
                    return a == (IntDynamic)b;
                }
                static public bool operator ==(string a, IntDynamic b)
                {
                    return (IntDynamic)a == b;
                }
                static public bool operator ==(ulong a, IntDynamic b)
                {
                    return (IntDynamic)a == b;
                }
                static public bool operator ==(long a, IntDynamic b)
                {
                    return (IntDynamic)a == b;
                }
                static public bool operator ==(uint a, IntDynamic b)
                {
                    return (IntDynamic)a == b;
                }
                static public bool operator ==(int a, IntDynamic b)
                {
                    return (IntDynamic)a == b;
                }
                static public bool operator ==(ushort a, IntDynamic b)
                {
                    return (IntDynamic)a == b;
                }
                static public bool operator ==(short a, IntDynamic b)
                {
                    return (IntDynamic)a == b;
                }
                static public bool operator ==(char a, IntDynamic b)
                {
                    return (IntDynamic)a == b;
                }
                static public bool operator ==(byte a, IntDynamic b)
                {
                    return (IntDynamic)a == b;
                }
                static public bool operator ==(sbyte a, IntDynamic b)
                {
                    return (IntDynamic)a == b;
                }
                static public bool operator ==(bool a, IntDynamic b)
                {
                    return (IntDynamic)a == b;
                }
            #endregion
            #region != Operator
                static public bool operator !=(IntDynamic a, IntDynamic b)
                {
                    if (a.Nagative)
                        if (b.Nagative)
                            return !UInt.UIntOperations.Equal(a.Array, b.Array);
                        else
                            return true;
                    if (b.Nagative)
                        return true;
                    return !UInt.UIntOperations.Equal(a.Array, b.Array);
                }
                static public bool operator !=(IntDynamic a, string b)
                {
                    return a != (IntDynamic)b;
                }
                static public bool operator !=(IntDynamic a, ulong b)
                {
                    return a != (IntDynamic)b;
                }
                static public bool operator !=(IntDynamic a, long b)
                {
                    return a != (IntDynamic)b;
                }
                static public bool operator !=(IntDynamic a, uint b)
                {
                    return a != (IntDynamic)b;
                }
                static public bool operator !=(IntDynamic a, int b)
                {
                    return a != (IntDynamic)b;
                }
                static public bool operator !=(IntDynamic a, ushort b)
                {
                    return a != (IntDynamic)b;
                }
                static public bool operator !=(IntDynamic a, short b)
                {
                    return a != (IntDynamic)b;
                }
                static public bool operator !=(IntDynamic a, char b)
                {
                    return a != (IntDynamic)b;
                }
                static public bool operator !=(IntDynamic a, byte b)
                {
                    return a != (IntDynamic)b;
                }
                static public bool operator !=(IntDynamic a, sbyte b)
                {
                    return a != (IntDynamic)b;
                }
                static public bool operator !=(IntDynamic a, bool b)
                {
                    return a != (IntDynamic)b;
                }
                static public bool operator !=(string a, IntDynamic b)
                {
                    return (IntDynamic)a != b;
                }
                static public bool operator !=(ulong a, IntDynamic b)
                {
                    return (IntDynamic)a != b;
                }
                static public bool operator !=(long a, IntDynamic b)
                {
                    return (IntDynamic)a != b;
                }
                static public bool operator !=(uint a, IntDynamic b)
                {
                    return (IntDynamic)a != b;
                }
                static public bool operator !=(int a, IntDynamic b)
                {
                    return (IntDynamic)a != b;
                }
                static public bool operator !=(ushort a, IntDynamic b)
                {
                    return (IntDynamic)a != b;
                }
                static public bool operator !=(short a, IntDynamic b)
                {
                    return (IntDynamic)a != b;
                }
                static public bool operator !=(char a, IntDynamic b)
                {
                    return (IntDynamic)a != b;
                }
                static public bool operator !=(byte a, IntDynamic b)
                {
                    return (IntDynamic)a != b;
                }
                static public bool operator !=(sbyte a, IntDynamic b)
                {
                    return (IntDynamic)a != b;
                }
                static public bool operator !=(bool a, IntDynamic b)
                {
                    return (IntDynamic)a != b;
                }
            #endregion
            #region Explicit Operators
                static public explicit operator IntDynamic(ulong number)
                {
                    return new IntDynamic(number);
                }
                static public explicit operator IntDynamic(long number)
                {
                    return new IntDynamic(number);
                }
                static public explicit operator IntDynamic(uint number)
                {
                    return new IntDynamic(number);
                }
                static public explicit operator IntDynamic(int number)
                {
                    return new IntDynamic(number);
                }
                static public explicit operator IntDynamic(ushort number)
                {
                    return new IntDynamic(number);
                }
                static public explicit operator IntDynamic(short number)
                {
                    return new IntDynamic(number);
                }
                static public explicit operator IntDynamic(byte number)
                {
                    return new IntDynamic(number);
                }
                static public explicit operator IntDynamic(sbyte number)
                {
                    return new IntDynamic(number);
                }
                static public explicit operator IntDynamic(char number)
                {
                    return new IntDynamic(number);
                }
                static public explicit operator IntDynamic(bool number)
                {
                    return new IntDynamic(number);
                }
                static public explicit operator IntDynamic(string number)
                {
                    return new IntDynamic(number);
                }
                static public explicit operator ulong(IntDynamic number)
                {
                    if (number.Array.Length == 1)
                        return (ulong)number.Array[0];
                    else
                        return ((ulong)number.Array[0] + (ulong)number.Array[1] * (ulong)4294967296) & ulong.MaxValue;
                }
                static public explicit operator long(IntDynamic number)
                {
                    if (number.Nagative)
                        if (number.Array.Length == 1)
                            return 0 - (long)number.Array[0];
                        else
                            return 0 - (long)(((ulong)number.Array[0] + ((ulong)number.Array[1] & int.MaxValue) * (ulong)4294967296) & (ulong)long.MaxValue);
                    else
                        if (number.Array.Length == 1)
                            return (long)number.Array[0];
                        else
                            return (long)(((ulong)number.Array[0] + ((ulong)number.Array[1] & int.MaxValue) * (ulong)4294967296) & (ulong)long.MaxValue);
                }
                static public explicit operator uint(IntDynamic number)
                {
                    return number.Array[0];
                }
                static public explicit operator int(IntDynamic number)
                {
                    if (number.Nagative)
                        return 0 - (int)(number.Array[0] & int.MaxValue);
                    else
                        return (int)(number.Array[0] & int.MaxValue);
                }
                static public explicit operator ushort(IntDynamic number)
                {
                    return (ushort)(number.Array[0] & ushort.MaxValue);
                }
                static public explicit operator short(IntDynamic number)
                {
                    if (number.Nagative)
                        return (short)((short)0 - (short)(number.Array[0] & short.MaxValue));
                    else
                        return (short)(number.Array[0] & short.MaxValue);
                }
                static public explicit operator byte(IntDynamic number)
                {
                    return (byte)(number.Array[0] & byte.MaxValue);
                }
                static public explicit operator sbyte(IntDynamic number)
                {
                    return (sbyte)(number.Array[0] & sbyte.MaxValue);
                }
                static public explicit operator char(IntDynamic number)
                {
                    return (char)(number.Array[0] & char.MaxValue);
                }
                static public explicit operator bool(IntDynamic number)
                {
                    return (number.Array[0] & 1) == 1;
                }
            #endregion
        #endregion
    }
}
