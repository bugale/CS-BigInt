using System;
using System.Linq;

namespace Math
{
    [Serializable]
    public class UIntDynamic
    {
        #region Fields
            private uint[] _number;
        #endregion
        #region Constructors
            public UIntDynamic()
            {
                this._number = new uint[] { 0 };
            }
            public UIntDynamic(ushort number)
            {
                this._number = new uint[] { number };
            }
            public UIntDynamic(short number)
            {
                this._number = new uint[] { (uint)number };
            }
            public UIntDynamic(uint number)
            {
                this._number = new uint[] { number };
            }
            public UIntDynamic(int number)
            {
                this._number = new uint[] { (uint)number };
            }
            public UIntDynamic(ulong number)
            {
                if ((ulong)((uint)number) == number)
                    this._number = new uint[] { (uint)number };
                else
                    this._number = new uint[] { (uint)number, (uint)(number / 4294967296) };
            }
            public UIntDynamic(long number)
            {
                if ((ulong)((uint)number) == (ulong)number)
                    this._number = new uint[] { (uint)number };
                else
                    this._number = new uint[] { (uint)number, (uint)((ulong)number / 4294967296) };
            }
            public UIntDynamic(byte number)
            {
                this._number = new uint[] { (uint)number };
            }
            public UIntDynamic(sbyte number)
            {
                if (number < 0)
                    this._number = new uint[] { (uint)(0 - number) };
                else
                    this._number = new uint[] { (uint)number };
            }
            public UIntDynamic(char number)
            {
                this._number = new uint[] { (uint)number };
            }
            public UIntDynamic(bool number)
            {
                this._number = new uint[] { number ? (uint)1 : (uint)0 };
            }
            public UIntDynamic(uint[] number)
            {
                int length = number.Length;
                this._number = new uint[length];
                for (int counter = 0; counter < length; counter++)
                    this._number[counter] = number[counter];
            }
            public UIntDynamic(string number)
            {
                try
                {
                    this._number = UInt.Convert.Parse(number.TrimStart('0'));
                }
                catch
                {
                    throw new Exception("The number is not in a correct format.");
                }
            }
            public UIntDynamic(UIntDynamic number)
            {
                int length = number.Array.Length;
                this._number = new uint[length];
                for (int counter = 0; counter < length; counter++)
                    this._number[counter] = number.Array[counter];
            }
            public UIntDynamic(IntDynamic number)
            {
                int length = number.Array.Length;
                this._number = new uint[length];
                for (int counter = 0; counter < length; counter++)
                    this._number[counter] = number.Array[counter];
            }
        #endregion
        #region Methods
            #region Convertions
                static public UIntDynamic Parse(bool number)
                {
                    return new UIntDynamic(number);
                }
                static public UIntDynamic Parse(byte number)
                {
                    return new UIntDynamic(number);
                }
                static public UIntDynamic Parse(sbyte number)
                {
                    return new UIntDynamic(number);
                }
                static public UIntDynamic Parse(char number)
                {
                    return new UIntDynamic(number);
                }
                static public UIntDynamic Parse(ushort number)
                {
                    return new UIntDynamic(new uint[] { number });
                }
                static public UIntDynamic Parse(short number)
                {
                    return new UIntDynamic(new uint[] { (uint)number });
                }
                static public UIntDynamic Parse(uint number)
                {
                    return new UIntDynamic(new uint[] { number });
                }
                static public UIntDynamic Parse(int number)
                {
                    return new UIntDynamic(new uint[] { (uint)number });
                }
                static public UIntDynamic Parse(ulong number)
                {
                    return new UIntDynamic(new uint[] { (uint)(number % 4294967296), (uint)(number / 4294967296) });
                }
                static public UIntDynamic Parse(long number)
                {
                    return new UIntDynamic(new uint[] { (uint)((ulong)number % 4294967296), (uint)((ulong)number / 4294967296) });
                }
                static public UIntDynamic Parse(string number)
                {
                    try
                    {
                        return new UIntDynamic(UInt.Convert.Parse(number.TrimStart('0')));
                    }
                    catch
                    {
                        throw new Exception("The number is not in a correct format.");
                    }
                }
                static public UIntDynamic Parse(uint[] number)
                {
                    return new UIntDynamic(number);
                }
                static public bool TryParse(string number, out UIntDynamic result)
                {
                    try
                    {
                        result = new UIntDynamic(UInt.Convert.Parse(number.TrimStart('0')));
                        return true;
                    }
                    catch
                    {
                        result = new UIntDynamic();
                        return false;
                    }
                }
                public override string ToString()
                {
                    return UInt.Convert.ToString(this._number);
                }
            #endregion
            public override bool Equals(object obj)
            {
                return this == (UIntDynamic)obj;
            }
            public override int GetHashCode()
            {
                return base.GetHashCode();
            }
        #endregion
        #region Properties
            internal uint[] Array
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
        #endregion
        #region Static Operations
            #region Binary Operations
                static public UIntDynamic Addition(UIntDynamic a, UIntDynamic b)
                {
                    return new UIntDynamic(UInt.UIntOperations.Addition(a.Array, b.Array));
                }
                static public UIntDynamic Incremention(UIntDynamic number)
                {
                    return new UIntDynamic(UInt.UIntOperations.Increment(number.Array));
                }
                static public UIntDynamic Subtraction(UIntDynamic a, UIntDynamic b)
                {
                    if (UInt.UIntOperations.Bigger(a.Array, b.Array))
                        throw new Exception("Unsigned Integers cannot hold nagative numbers.");
                    if (UInt.UIntOperations.Equal(a.Array, b.Array))
                        return new UIntDynamic();
                    else
                        return new UIntDynamic(UInt.UIntOperations.Subtraction(a.Array, b.Array));
                }
                static public UIntDynamic Decremention(UIntDynamic number)
                {
                    if (number.Array.Length == 1 && number.Array[0] == 0)
                        throw new Exception("Unsigned Integers cannot hold nagative numbers.");
                    return new UIntDynamic(UInt.UIntOperations.Decrement(number.Array));
                }
                static public UIntDynamic Multiplication(UIntDynamic a, UIntDynamic b)
                {
                    return new UIntDynamic(UInt.UIntOperations.Multiplication(a.Array, b.Array));
                }
                static public UIntDynamic Division(UIntDynamic a, UIntDynamic b)
                {
                    if (b.Array.Length == 1 && b.Array[0] == 0)
                        throw new Exception("Division by zero.");
                    if (b.Array.Length == 1 && b.Array[0] == 1)
                        return new UIntDynamic(a);
                    if (a.Array.Length == 1 && a.Array[0] == 1)
                        return new UIntDynamic(new uint[] { 0 });
                    if (a.Array.Length == 1 && a.Array[0] == 0)
                        return new UIntDynamic(new uint[] { 0 });
                    if (UInt.UIntOperations.Equal(a.Array, b.Array))
                        return new UIntDynamic(new uint[] { 1 });
                    return new UIntDynamic(UInt.UIntOperations.Division(a.Array, b.Array));
                }
                static public UIntDynamic Division(UIntDynamic a, UIntDynamic b, out UIntDynamic modulo)
                {
                    if (b.Array.Length == 1 && b.Array[0] == 0)
                    {
                        modulo = new UIntDynamic(0);
                        throw new Exception("Division by zero.");
                    }
                    if (b.Array.Length == 1 && b.Array[0] == 1)
                    {
                        modulo = new UIntDynamic(0);
                        return new UIntDynamic(a);
                    }
                    if (a.Array.Length == 1 && a.Array[0] == 1)
                    {
                        modulo = new UIntDynamic(1);
                        return new UIntDynamic();
                    }
                    if (a.Array.Length == 1 && a.Array[0] == 0)
                    {
                        modulo = new UIntDynamic(0);
                        return new UIntDynamic();
                    }
                    if (UInt.UIntOperations.Equal(a.Array, b.Array))
                    {
                        modulo = new UIntDynamic(0);
                        return new UIntDynamic(1);
                    }
                    uint[] Modulo;
                    UIntDynamic result = new UIntDynamic(UInt.UIntOperations.Division(a.Array, b.Array, out Modulo));
                    modulo = new UIntDynamic(Modulo);
                    return result;
                }
                static public UIntDynamic Modulo(UIntDynamic a, UIntDynamic b)
                {
                    if (b.Array.Length == 1 && b.Array[0] == 0)
                        throw new Exception("Division by zero.");
                    if (b.Array.Length == 1 && b.Array[0] == 1)
                        return new UIntDynamic();
                    if (a.Array.Length == 1 && a.Array[0] == 1)
                        return new UIntDynamic(1);
                    if (a.Array.Length == 1 && a.Array[0] == 0)
                        return new UIntDynamic();
                    if (UInt.UIntOperations.Equal(a.Array, b.Array))
                        return new UIntDynamic();
                    return new UIntDynamic(UInt.UIntOperations.Modulo(a.Array, b.Array));
                }
                static public UIntDynamic Power(UIntDynamic a, UIntDynamic b)
                {
                    return new UIntDynamic(UInt.UIntOperations.Power(a.Array, b.Array));
                }
                static public UIntDynamic Root(UIntDynamic a, UIntDynamic b)
                {
                    if (b.Array.Length == 1 && b.Array[0] == 0)
                        if (a.Array.Length == 1 && a.Array[0] == 1)
                            return new UIntDynamic(a);
                        else
                            throw new Exception("Arithmetic operation resulted with no answer");
                    return new UIntDynamic(UInt.UIntOperations.Root(a.Array, b.Array));
                }
            #endregion
            #region Bitwise Operations
                static public UIntDynamic Not(UIntDynamic number)
                {
                    return new UIntDynamic(UInt.UIntOperations.Not(number.Array));
                }
                static public UIntDynamic And(UIntDynamic a, UIntDynamic b)
                {
                    return new UIntDynamic( UInt.UIntOperations.And(a.Array, b.Array));
                }
                static public UIntDynamic Or(UIntDynamic a, UIntDynamic b)
                {
                    return new UIntDynamic(UInt.UIntOperations.Or(a.Array, b.Array));
                }
                static public UIntDynamic Nand(UIntDynamic a, UIntDynamic b)
                {
                    return new UIntDynamic(UInt.UIntOperations.Nand(a.Array, b.Array));
                }
                static public UIntDynamic Nor(UIntDynamic a, UIntDynamic b)
                {
                    return new UIntDynamic(UInt.UIntOperations.Nor(a.Array, b.Array));
                }
                static public UIntDynamic Xor(UIntDynamic a, UIntDynamic b)
                {
                    return new UIntDynamic(UInt.UIntOperations.Xor(a.Array, b.Array));
                }
                static public UIntDynamic Xnor(UIntDynamic a, UIntDynamic b)
                {
                    return new UIntDynamic(UInt.UIntOperations.Xnor(a.Array, b.Array));
                }
                static public UIntDynamic LeftShift(UIntDynamic a, UIntDynamic b)
                {
                    return new UIntDynamic(UInt.UIntOperations.LeftShift(a.Array, b.Array));
                }
                static public UIntDynamic RightShift(UIntDynamic a, UIntDynamic b)
                {
                    return new UIntDynamic(UInt.UIntOperations.RightShift(a.Array, b.Array));
                }
                static public UIntDynamic LeftShiftCircular(UIntDynamic a, UIntDynamic b)
                {
                    return new UIntDynamic(UInt.UIntOperations.LeftShiftCircular(a.Array, b.Array));
                }
                static public UIntDynamic RightShiftCircular(UIntDynamic a, UIntDynamic b)
                {
                    return new UIntDynamic(UInt.UIntOperations.RightShiftCircular(a.Array, b.Array));
                }
            #endregion
            #region Unary Operations
                static public UIntDynamic Factorial(UIntDynamic number)
                {
                    return new UIntDynamic(UInt.UIntOperations.Factorial(number.Array));
                }
                static public UIntDynamic ReverseUints(UIntDynamic number)
                {
                    return new UIntDynamic(number.Array.Reverse().ToArray());
                }
                static public UIntDynamic ReverseBits(UIntDynamic number)
                {
                    return new UIntDynamic(UInt.UIntOperations.ReverseBinary(number.Array));
                }
            #endregion
        #endregion
        #region Operators
            #region + Operator
                static public UIntDynamic operator +(UIntDynamic a, UIntDynamic b)
                {
                    return Addition(a, b);
                }
                static public UIntDynamic operator +(UIntDynamic a, string b)
                {
                    return a + (UIntDynamic)b;
                }
                static public UIntDynamic operator +(UIntDynamic a, ulong b)
                {
                    return a + (UIntDynamic)b;
                }
                static public UIntDynamic operator +(UIntDynamic a, long b)
                {
                    return a + (UIntDynamic)b;
                }
                static public UIntDynamic operator +(UIntDynamic a, uint b)
                {
                    return a + (UIntDynamic)b;
                }
                static public UIntDynamic operator +(UIntDynamic a, int b)
                {
                    return a + (UIntDynamic)b;
                }
                static public UIntDynamic operator +(UIntDynamic a, ushort b)
                {
                    return a + (UIntDynamic)b;
                }
                static public UIntDynamic operator +(UIntDynamic a, short b)
                {
                    return a + (UIntDynamic)b;
                }
                static public UIntDynamic operator +(UIntDynamic a, char b)
                {
                    return a + (UIntDynamic)b;
                }
                static public UIntDynamic operator +(UIntDynamic a, byte b)
                {
                    return a + (UIntDynamic)b;
                }
                static public UIntDynamic operator +(UIntDynamic a, sbyte b)
                {
                    return a + (UIntDynamic)b;
                }
                static public UIntDynamic operator +(UIntDynamic a, bool b)
                {
                    return a + (UIntDynamic)b;
                }
                static public UIntDynamic operator +(string a, UIntDynamic b)
                {
                    return (UIntDynamic)a + b;
                }
                static public UIntDynamic operator +(ulong a, UIntDynamic b)
                {
                    return (UIntDynamic)a + b;
                }
                static public UIntDynamic operator +(long a, UIntDynamic b)
                {
                    return (UIntDynamic)a + b;
                }
                static public UIntDynamic operator +(uint a, UIntDynamic b)
                {
                    return (UIntDynamic)a + b;
                }
                static public UIntDynamic operator +(int a, UIntDynamic b)
                {
                    return (UIntDynamic)a + b;
                }
                static public UIntDynamic operator +(ushort a, UIntDynamic b)
                {
                    return (UIntDynamic)a + b;
                }
                static public UIntDynamic operator +(short a, UIntDynamic b)
                {
                    return (UIntDynamic)a + b;
                }
                static public UIntDynamic operator +(char a, UIntDynamic b)
                {
                    return (UIntDynamic)a + b;
                }
                static public UIntDynamic operator +(byte a, UIntDynamic b)
                {
                    return (UIntDynamic)a + b;
                }
                static public UIntDynamic operator +(sbyte a, UIntDynamic b)
                {
                    return (UIntDynamic)a + b;
                }
                static public UIntDynamic operator +(bool a, UIntDynamic b)
                {
                    return (UIntDynamic)a + b;
                }
                static public UIntDynamic operator ++(UIntDynamic number)
                {
                    return Incremention(number);
                }
            #endregion
            #region - Operator
                static public UIntDynamic operator -(UIntDynamic a, UIntDynamic b)
                {
                    return Subtraction(a, b);
                }
                static public UIntDynamic operator -(UIntDynamic a, string b)
                {
                    return a - (UIntDynamic)b;
                }
                static public UIntDynamic operator -(UIntDynamic a, ulong b)
                {
                    return a - (UIntDynamic)b;
                }
                static public UIntDynamic operator -(UIntDynamic a, long b)
                {
                    return a - (UIntDynamic)b;
                }
                static public UIntDynamic operator -(UIntDynamic a, uint b)
                {
                    return a - (UIntDynamic)b;
                }
                static public UIntDynamic operator -(UIntDynamic a, int b)
                {
                    return a - (UIntDynamic)b;
                }
                static public UIntDynamic operator -(UIntDynamic a, ushort b)
                {
                    return a - (UIntDynamic)b;
                }
                static public UIntDynamic operator -(UIntDynamic a, short b)
                {
                    return a - (UIntDynamic)b;
                }
                static public UIntDynamic operator -(UIntDynamic a, char b)
                {
                    return a - (UIntDynamic)b;
                }
                static public UIntDynamic operator -(UIntDynamic a, byte b)
                {
                    return a - (UIntDynamic)b;
                }
                static public UIntDynamic operator -(UIntDynamic a, sbyte b)
                {
                    return a - (UIntDynamic)b;
                }
                static public UIntDynamic operator -(UIntDynamic a, bool b)
                {
                    return a - (UIntDynamic)b;
                }
                static public UIntDynamic operator -(string a, UIntDynamic b)
                {
                    return (UIntDynamic)a - b;
                }
                static public UIntDynamic operator -(ulong a, UIntDynamic b)
                {
                    return (UIntDynamic)a - b;
                }
                static public UIntDynamic operator -(long a, UIntDynamic b)
                {
                    return (UIntDynamic)a - b;
                }
                static public UIntDynamic operator -(uint a, UIntDynamic b)
                {
                    return (UIntDynamic)a - b;
                }
                static public UIntDynamic operator -(int a, UIntDynamic b)
                {
                    return (UIntDynamic)a - b;
                }
                static public UIntDynamic operator -(ushort a, UIntDynamic b)
                {
                    return (UIntDynamic)a - b;
                }
                static public UIntDynamic operator -(short a, UIntDynamic b)
                {
                    return (UIntDynamic)a - b;
                }
                static public UIntDynamic operator -(char a, UIntDynamic b)
                {
                    return (UIntDynamic)a - b;
                }
                static public UIntDynamic operator -(byte a, UIntDynamic b)
                {
                    return (UIntDynamic)a - b;
                }
                static public UIntDynamic operator -(sbyte a, UIntDynamic b)
                {
                    return (UIntDynamic)a - b;
                }
                static public UIntDynamic operator -(bool a, UIntDynamic b)
                {
                    return (UIntDynamic)a - b;
                }
                static public UIntDynamic operator --(UIntDynamic number)
                {
                    return Decremention(number);
                }
            #endregion
            #region * Operator
                static public UIntDynamic operator *(UIntDynamic a, UIntDynamic b)
                {
                    return Multiplication(a, b);
                }
                static public UIntDynamic operator *(UIntDynamic a, string b)
                {
                    return a * (UIntDynamic)b;
                }
                static public UIntDynamic operator *(UIntDynamic a, ulong b)
                {
                    return a * (UIntDynamic)b;
                }
                static public UIntDynamic operator *(UIntDynamic a, long b)
                {
                    return a * (UIntDynamic)b;
                }
                static public UIntDynamic operator *(UIntDynamic a, uint b)
                {
                    return a * (UIntDynamic)b;
                }
                static public UIntDynamic operator *(UIntDynamic a, int b)
                {
                    return a * (UIntDynamic)b;
                }
                static public UIntDynamic operator *(UIntDynamic a, ushort b)
                {
                    return a * (UIntDynamic)b;
                }
                static public UIntDynamic operator *(UIntDynamic a, short b)
                {
                    return a * (UIntDynamic)b;
                }
                static public UIntDynamic operator *(UIntDynamic a, char b)
                {
                    return a * (UIntDynamic)b;
                }
                static public UIntDynamic operator *(UIntDynamic a, byte b)
                {
                    return a * (UIntDynamic)b;
                }
                static public UIntDynamic operator *(UIntDynamic a, sbyte b)
                {
                    return a * (UIntDynamic)b;
                }
                static public UIntDynamic operator *(UIntDynamic a, bool b)
                {
                    return a * (UIntDynamic)b;
                }
                static public UIntDynamic operator *(string a, UIntDynamic b)
                {
                    return (UIntDynamic)a * b;
                }
                static public UIntDynamic operator *(ulong a, UIntDynamic b)
                {
                    return (UIntDynamic)a * b;
                }
                static public UIntDynamic operator *(long a, UIntDynamic b)
                {
                    return (UIntDynamic)a * b;
                }
                static public UIntDynamic operator *(uint a, UIntDynamic b)
                {
                    return (UIntDynamic)a * b;
                }
                static public UIntDynamic operator *(int a, UIntDynamic b)
                {
                    return (UIntDynamic)a * b;
                }
                static public UIntDynamic operator *(ushort a, UIntDynamic b)
                {
                    return (UIntDynamic)a * b;
                }
                static public UIntDynamic operator *(short a, UIntDynamic b)
                {
                    return (UIntDynamic)a * b;
                }
                static public UIntDynamic operator *(char a, UIntDynamic b)
                {
                    return (UIntDynamic)a * b;
                }
                static public UIntDynamic operator *(byte a, UIntDynamic b)
                {
                    return (UIntDynamic)a * b;
                }
                static public UIntDynamic operator *(sbyte a, UIntDynamic b)
                {
                    return (UIntDynamic)a * b;
                }
                static public UIntDynamic operator *(bool a, UIntDynamic b)
                {
                    return (UIntDynamic)a * b;
                }
            #endregion
            #region / Operator
                static public UIntDynamic operator /(UIntDynamic a, UIntDynamic b)
                {
                    return Division(a, b);
                }
                static public UIntDynamic operator /(UIntDynamic a, string b)
                {
                    return a / (UIntDynamic)b;
                }
                static public UIntDynamic operator /(UIntDynamic a, ulong b)
                {
                    return a / (UIntDynamic)b;
                }
                static public UIntDynamic operator /(UIntDynamic a, long b)
                {
                    return a / (UIntDynamic)b;
                }
                static public UIntDynamic operator /(UIntDynamic a, uint b)
                {
                    return a / (UIntDynamic)b;
                }
                static public UIntDynamic operator /(UIntDynamic a, int b)
                {
                    return a / (UIntDynamic)b;
                }
                static public UIntDynamic operator /(UIntDynamic a, ushort b)
                {
                    return a / (UIntDynamic)b;
                }
                static public UIntDynamic operator /(UIntDynamic a, short b)
                {
                    return a / (UIntDynamic)b;
                }
                static public UIntDynamic operator /(UIntDynamic a, char b)
                {
                    return a / (UIntDynamic)b;
                }
                static public UIntDynamic operator /(UIntDynamic a, byte b)
                {
                    return a / (UIntDynamic)b;
                }
                static public UIntDynamic operator /(UIntDynamic a, sbyte b)
                {
                    return a / (UIntDynamic)b;
                }
                static public UIntDynamic operator /(UIntDynamic a, bool b)
                {
                    return a / (UIntDynamic)b;
                }
                static public UIntDynamic operator /(string a, UIntDynamic b)
                {
                    return (UIntDynamic)a / b;
                }
                static public UIntDynamic operator /(ulong a, UIntDynamic b)
                {
                    return (UIntDynamic)a / b;
                }
                static public UIntDynamic operator /(long a, UIntDynamic b)
                {
                    return (UIntDynamic)a / b;
                }
                static public UIntDynamic operator /(uint a, UIntDynamic b)
                {
                    return (UIntDynamic)a / b;
                }
                static public UIntDynamic operator /(int a, UIntDynamic b)
                {
                    return (UIntDynamic)a / b;
                }
                static public UIntDynamic operator /(ushort a, UIntDynamic b)
                {
                    return (UIntDynamic)a / b;
                }
                static public UIntDynamic operator /(short a, UIntDynamic b)
                {
                    return (UIntDynamic)a / b;
                }
                static public UIntDynamic operator /(char a, UIntDynamic b)
                {
                    return (UIntDynamic)a / b;
                }
                static public UIntDynamic operator /(byte a, UIntDynamic b)
                {
                    return (UIntDynamic)a / b;
                }
                static public UIntDynamic operator /(sbyte a, UIntDynamic b)
                {
                    return (UIntDynamic)a / b;
                }
                static public UIntDynamic operator /(bool a, UIntDynamic b)
                {
                    return (UIntDynamic)a / b;
                }
            #endregion
            #region % Operator
                static public UIntDynamic operator %(UIntDynamic a, UIntDynamic b)
                {
                    return Modulo(a, b);
                }
                static public UIntDynamic operator %(UIntDynamic a, string b)
                {
                    return a % (UIntDynamic)b;
                }
                static public UIntDynamic operator %(UIntDynamic a, ulong b)
                {
                    return a % (UIntDynamic)b;
                }
                static public UIntDynamic operator %(UIntDynamic a, long b)
                {
                    return a % (UIntDynamic)b;
                }
                static public UIntDynamic operator %(UIntDynamic a, uint b)
                {
                    return a % (UIntDynamic)b;
                }
                static public UIntDynamic operator %(UIntDynamic a, int b)
                {
                    return a % (UIntDynamic)b;
                }
                static public UIntDynamic operator %(UIntDynamic a, ushort b)
                {
                    return a % (UIntDynamic)b;
                }
                static public UIntDynamic operator %(UIntDynamic a, short b)
                {
                    return a % (UIntDynamic)b;
                }
                static public UIntDynamic operator %(UIntDynamic a, char b)
                {
                    return a % (UIntDynamic)b;
                }
                static public UIntDynamic operator %(UIntDynamic a, byte b)
                {
                    return a % (UIntDynamic)b;
                }
                static public UIntDynamic operator %(UIntDynamic a, sbyte b)
                {
                    return a % (UIntDynamic)b;
                }
                static public UIntDynamic operator %(UIntDynamic a, bool b)
                {
                    return a % (UIntDynamic)b;
                }
                static public UIntDynamic operator %(string a, UIntDynamic b)
                {
                    return (UIntDynamic)a % b;
                }
                static public UIntDynamic operator %(ulong a, UIntDynamic b)
                {
                    return (UIntDynamic)a % b;
                }
                static public UIntDynamic operator %(long a, UIntDynamic b)
                {
                    return (UIntDynamic)a % b;
                }
                static public UIntDynamic operator %(uint a, UIntDynamic b)
                {
                    return (UIntDynamic)a % b;
                }
                static public UIntDynamic operator %(int a, UIntDynamic b)
                {
                    return (UIntDynamic)a % b;
                }
                static public UIntDynamic operator %(ushort a, UIntDynamic b)
                {
                    return (UIntDynamic)a % b;
                }
                static public UIntDynamic operator %(short a, UIntDynamic b)
                {
                    return (UIntDynamic)a % b;
                }
                static public UIntDynamic operator %(char a, UIntDynamic b)
                {
                    return (UIntDynamic)a % b;
                }
                static public UIntDynamic operator %(byte a, UIntDynamic b)
                {
                    return (UIntDynamic)a % b;
                }
                static public UIntDynamic operator %(sbyte a, UIntDynamic b)
                {
                    return (UIntDynamic)a % b;
                }
                static public UIntDynamic operator %(bool a, UIntDynamic b)
                {
                    return (UIntDynamic)a % b;
                }
            #endregion
            #region ^ Operator
                static public UIntDynamic operator ^(UIntDynamic a, UIntDynamic b)
                {
                    return Xor(a, b);
                }
                static public UIntDynamic operator ^(UIntDynamic a, string b)
                {
                    return a ^ (UIntDynamic)b;
                }
                static public UIntDynamic operator ^(UIntDynamic a, ulong b)
                {
                    return a ^ (UIntDynamic)b;
                }
                static public UIntDynamic operator ^(UIntDynamic a, long b)
                {
                    return a ^ (UIntDynamic)b;
                }
                static public UIntDynamic operator ^(UIntDynamic a, uint b)
                {
                    return a ^ (UIntDynamic)b;
                }
                static public UIntDynamic operator ^(UIntDynamic a, int b)
                {
                    return a ^ (UIntDynamic)b;
                }
                static public UIntDynamic operator ^(UIntDynamic a, ushort b)
                {
                    return a ^ (UIntDynamic)b;
                }
                static public UIntDynamic operator ^(UIntDynamic a, short b)
                {
                    return a ^ (UIntDynamic)b;
                }
                static public UIntDynamic operator ^(UIntDynamic a, char b)
                {
                    return a ^ (UIntDynamic)b;
                }
                static public UIntDynamic operator ^(UIntDynamic a, byte b)
                {
                    return a ^ (UIntDynamic)b;
                }
                static public UIntDynamic operator ^(UIntDynamic a, sbyte b)
                {
                    return a ^ (UIntDynamic)b;
                }
                static public UIntDynamic operator ^(UIntDynamic a, bool b)
                {
                    return a ^ (UIntDynamic)b;
                }
                static public UIntDynamic operator ^(string a, UIntDynamic b)
                {
                    return (UIntDynamic)a ^ b;
                }
                static public UIntDynamic operator ^(ulong a, UIntDynamic b)
                {
                    return (UIntDynamic)a ^ b;
                }
                static public UIntDynamic operator ^(long a, UIntDynamic b)
                {
                    return (UIntDynamic)a ^ b;
                }
                static public UIntDynamic operator ^(uint a, UIntDynamic b)
                {
                    return (UIntDynamic)a ^ b;
                }
                static public UIntDynamic operator ^(int a, UIntDynamic b)
                {
                    return (UIntDynamic)a ^ b;
                }
                static public UIntDynamic operator ^(ushort a, UIntDynamic b)
                {
                    return (UIntDynamic)a ^ b;
                }
                static public UIntDynamic operator ^(short a, UIntDynamic b)
                {
                    return (UIntDynamic)a ^ b;
                }
                static public UIntDynamic operator ^(char a, UIntDynamic b)
                {
                    return (UIntDynamic)a ^ b;
                }
                static public UIntDynamic operator ^(byte a, UIntDynamic b)
                {
                    return (UIntDynamic)a ^ b;
                }
                static public UIntDynamic operator ^(sbyte a, UIntDynamic b)
                {
                    return (UIntDynamic)a ^ b;
                }
                static public UIntDynamic operator ^(bool a, UIntDynamic b)
                {
                    return (UIntDynamic)a ^ b;
                }
            #endregion
            #region ~ Operator
                static public UIntDynamic operator ~(UIntDynamic number)
                {
                    return Not(number);
                }
            #endregion
            #region | Operator
                static public UIntDynamic operator |(UIntDynamic a, UIntDynamic b)
                {
                    return Or(a, b);
                }
                static public UIntDynamic operator |(UIntDynamic a, string b)
                {
                    return a | (UIntDynamic)b;
                }
                static public UIntDynamic operator |(UIntDynamic a, ulong b)
                {
                    return a | (UIntDynamic)b;
                }
                static public UIntDynamic operator |(UIntDynamic a, long b)
                {
                    return a | (UIntDynamic)b;
                }
                static public UIntDynamic operator |(UIntDynamic a, uint b)
                {
                    return a | (UIntDynamic)b;
                }
                static public UIntDynamic operator |(UIntDynamic a, int b)
                {
                    return a | (UIntDynamic)b;
                }
                static public UIntDynamic operator |(UIntDynamic a, ushort b)
                {
                    return a | (UIntDynamic)b;
                }
                static public UIntDynamic operator |(UIntDynamic a, short b)
                {
                    return a | (UIntDynamic)b;
                }
                static public UIntDynamic operator |(UIntDynamic a, char b)
                {
                    return a | (UIntDynamic)b;
                }
                static public UIntDynamic operator |(UIntDynamic a, byte b)
                {
                    return a | (UIntDynamic)b;
                }
                static public UIntDynamic operator |(UIntDynamic a, sbyte b)
                {
                    return a | (UIntDynamic)b;
                }
                static public UIntDynamic operator |(UIntDynamic a, bool b)
                {
                    return a | (UIntDynamic)b;
                }
                static public UIntDynamic operator |(string a, UIntDynamic b)
                {
                    return (UIntDynamic)a | b;
                }
                static public UIntDynamic operator |(ulong a, UIntDynamic b)
                {
                    return (UIntDynamic)a | b;
                }
                static public UIntDynamic operator |(long a, UIntDynamic b)
                {
                    return (UIntDynamic)a | b;
                }
                static public UIntDynamic operator |(uint a, UIntDynamic b)
                {
                    return (UIntDynamic)a | b;
                }
                static public UIntDynamic operator |(int a, UIntDynamic b)
                {
                    return (UIntDynamic)a | b;
                }
                static public UIntDynamic operator |(ushort a, UIntDynamic b)
                {
                    return (UIntDynamic)a | b;
                }
                static public UIntDynamic operator |(short a, UIntDynamic b)
                {
                    return (UIntDynamic)a | b;
                }
                static public UIntDynamic operator |(char a, UIntDynamic b)
                {
                    return (UIntDynamic)a | b;
                }
                static public UIntDynamic operator |(byte a, UIntDynamic b)
                {
                    return (UIntDynamic)a | b;
                }
                static public UIntDynamic operator |(sbyte a, UIntDynamic b)
                {
                    return (UIntDynamic)a | b;
                }
                static public UIntDynamic operator |(bool a, UIntDynamic b)
                {
                    return (UIntDynamic)a | b;
                }
            #endregion
            #region & Operator
                static public UIntDynamic operator &(UIntDynamic a, UIntDynamic b)
                {
                    return And(a, b);
                }
                static public UIntDynamic operator &(UIntDynamic a, string b)
                {
                    return a & (UIntDynamic)b;
                }
                static public UIntDynamic operator &(UIntDynamic a, ulong b)
                {
                    return a & (UIntDynamic)b;
                }
                static public UIntDynamic operator &(UIntDynamic a, long b)
                {
                    return a & (UIntDynamic)b;
                }
                static public UIntDynamic operator &(UIntDynamic a, uint b)
                {
                    return a & (UIntDynamic)b;
                }
                static public UIntDynamic operator &(UIntDynamic a, int b)
                {
                    return a & (UIntDynamic)b;
                }
                static public UIntDynamic operator &(UIntDynamic a, ushort b)
                {
                    return a & (UIntDynamic)b;
                }
                static public UIntDynamic operator &(UIntDynamic a, short b)
                {
                    return a & (UIntDynamic)b;
                }
                static public UIntDynamic operator &(UIntDynamic a, char b)
                {
                    return a & (UIntDynamic)b;
                }
                static public UIntDynamic operator &(UIntDynamic a, byte b)
                {
                    return a & (UIntDynamic)b;
                }
                static public UIntDynamic operator &(UIntDynamic a, sbyte b)
                {
                    return a & (UIntDynamic)b;
                }
                static public UIntDynamic operator &(UIntDynamic a, bool b)
                {
                    return a & (UIntDynamic)b;
                }
                static public UIntDynamic operator &(string a, UIntDynamic b)
                {
                    return (UIntDynamic)a & b;
                }
                static public UIntDynamic operator &(ulong a, UIntDynamic b)
                {
                    return (UIntDynamic)a & b;
                }
                static public UIntDynamic operator &(long a, UIntDynamic b)
                {
                    return (UIntDynamic)a & b;
                }
                static public UIntDynamic operator &(uint a, UIntDynamic b)
                {
                    return (UIntDynamic)a & b;
                }
                static public UIntDynamic operator &(int a, UIntDynamic b)
                {
                    return (UIntDynamic)a & b;
                }
                static public UIntDynamic operator &(ushort a, UIntDynamic b)
                {
                    return (UIntDynamic)a & b;
                }
                static public UIntDynamic operator &(short a, UIntDynamic b)
                {
                    return (UIntDynamic)a & b;
                }
                static public UIntDynamic operator &(char a, UIntDynamic b)
                {
                    return (UIntDynamic)a & b;
                }
                static public UIntDynamic operator &(byte a, UIntDynamic b)
                {
                    return (UIntDynamic)a & b;
                }
                static public UIntDynamic operator &(sbyte a, UIntDynamic b)
                {
                    return (UIntDynamic)a & b;
                }
                static public UIntDynamic operator &(bool a, UIntDynamic b)
                {
                    return (UIntDynamic)a & b;
                }
            #endregion
            #region << >> Operators
                static public UIntDynamic operator <<(UIntDynamic a, int b)
                {
                    return LeftShift(a, (UIntDynamic)b);
                }
                static public UIntDynamic operator >>(UIntDynamic a, int b)
                {
                    return RightShift(a, (UIntDynamic)b);
                }
            #endregion
            #region > Operator
                static public bool operator >(UIntDynamic a, UIntDynamic b)
                {
                    return UInt.UIntOperations.Bigger(a.Array, b.Array);
                }
                static public bool operator >(UIntDynamic a, string b)
                {
                    return a > (UIntDynamic)b;
                }
                static public bool operator >(UIntDynamic a, ulong b)
                {
                    return a > (UIntDynamic)b;
                }
                static public bool operator >(UIntDynamic a, long b)
                {
                    return a > (UIntDynamic)b;
                }
                static public bool operator >(UIntDynamic a, uint b)
                {
                    return a > (UIntDynamic)b;
                }
                static public bool operator >(UIntDynamic a, int b)
                {
                    return a > (UIntDynamic)b;
                }
                static public bool operator >(UIntDynamic a, ushort b)
                {
                    return a > (UIntDynamic)b;
                }
                static public bool operator >(UIntDynamic a, short b)
                {
                    return a > (UIntDynamic)b;
                }
                static public bool operator >(UIntDynamic a, char b)
                {
                    return a > (UIntDynamic)b;
                }
                static public bool operator >(UIntDynamic a, byte b)
                {
                    return a > (UIntDynamic)b;
                }
                static public bool operator >(UIntDynamic a, sbyte b)
                {
                    return a > (UIntDynamic)b;
                }
                static public bool operator >(UIntDynamic a, bool b)
                {
                    return a > (UIntDynamic)b;
                }
                static public bool operator >(string a, UIntDynamic b)
                {
                    return (UIntDynamic)a > b;
                }
                static public bool operator >(ulong a, UIntDynamic b)
                {
                    return (UIntDynamic)a > b;
                }
                static public bool operator >(long a, UIntDynamic b)
                {
                    return (UIntDynamic)a > b;
                }
                static public bool operator >(uint a, UIntDynamic b)
                {
                    return (UIntDynamic)a > b;
                }
                static public bool operator >(int a, UIntDynamic b)
                {
                    return (UIntDynamic)a > b;
                }
                static public bool operator >(ushort a, UIntDynamic b)
                {
                    return (UIntDynamic)a > b;
                }
                static public bool operator >(short a, UIntDynamic b)
                {
                    return (UIntDynamic)a > b;
                }
                static public bool operator >(char a, UIntDynamic b)
                {
                    return (UIntDynamic)a > b;
                }
                static public bool operator >(byte a, UIntDynamic b)
                {
                    return (UIntDynamic)a > b;
                }
                static public bool operator >(sbyte a, UIntDynamic b)
                {
                    return (UIntDynamic)a > b;
                }
                static public bool operator >(bool a, UIntDynamic b)
                {
                    return (UIntDynamic)a > b;
                }
            #endregion
            #region < Operator
                static public bool operator <(UIntDynamic a, UIntDynamic b)
                {
                    return UInt.UIntOperations.Smaller(a.Array, b.Array);
                }
                static public bool operator <(UIntDynamic a, string b)
                {
                    return a < (UIntDynamic)b;
                }
                static public bool operator <(UIntDynamic a, ulong b)
                {
                    return a < (UIntDynamic)b;
                }
                static public bool operator <(UIntDynamic a, long b)
                {
                    return a < (UIntDynamic)b;
                }
                static public bool operator <(UIntDynamic a, uint b)
                {
                    return a < (UIntDynamic)b;
                }
                static public bool operator <(UIntDynamic a, int b)
                {
                    return a < (UIntDynamic)b;
                }
                static public bool operator <(UIntDynamic a, ushort b)
                {
                    return a < (UIntDynamic)b;
                }
                static public bool operator <(UIntDynamic a, short b)
                {
                    return a < (UIntDynamic)b;
                }
                static public bool operator <(UIntDynamic a, char b)
                {
                    return a < (UIntDynamic)b;
                }
                static public bool operator <(UIntDynamic a, byte b)
                {
                    return a < (UIntDynamic)b;
                }
                static public bool operator <(UIntDynamic a, sbyte b)
                {
                    return a < (UIntDynamic)b;
                }
                static public bool operator <(UIntDynamic a, bool b)
                {
                    return a < (UIntDynamic)b;
                }
                static public bool operator <(string a, UIntDynamic b)
                {
                    return (UIntDynamic)a < b;
                }
                static public bool operator <(ulong a, UIntDynamic b)
                {
                    return (UIntDynamic)a < b;
                }
                static public bool operator <(long a, UIntDynamic b)
                {
                    return (UIntDynamic)a < b;
                }
                static public bool operator <(uint a, UIntDynamic b)
                {
                    return (UIntDynamic)a < b;
                }
                static public bool operator <(int a, UIntDynamic b)
                {
                    return (UIntDynamic)a < b;
                }
                static public bool operator <(ushort a, UIntDynamic b)
                {
                    return (UIntDynamic)a < b;
                }
                static public bool operator <(short a, UIntDynamic b)
                {
                    return (UIntDynamic)a < b;
                }
                static public bool operator <(char a, UIntDynamic b)
                {
                    return (UIntDynamic)a < b;
                }
                static public bool operator <(byte a, UIntDynamic b)
                {
                    return (UIntDynamic)a < b;
                }
                static public bool operator <(sbyte a, UIntDynamic b)
                {
                    return (UIntDynamic)a < b;
                }
                static public bool operator <(bool a, UIntDynamic b)
                {
                    return (UIntDynamic)a < b;
                }
            #endregion
            #region >= Operator
                static public bool operator >=(UIntDynamic a, UIntDynamic b)
                {
                    return UInt.UIntOperations.BiggerEqual(a.Array, b.Array);
                }
                static public bool operator >=(UIntDynamic a, string b)
                {
                    return a >= (UIntDynamic)b;
                }
                static public bool operator >=(UIntDynamic a, ulong b)
                {
                    return a >= (UIntDynamic)b;
                }
                static public bool operator >=(UIntDynamic a, long b)
                {
                    return a >= (UIntDynamic)b;
                }
                static public bool operator >=(UIntDynamic a, uint b)
                {
                    return a >= (UIntDynamic)b;
                }
                static public bool operator >=(UIntDynamic a, int b)
                {
                    return a >= (UIntDynamic)b;
                }
                static public bool operator >=(UIntDynamic a, ushort b)
                {
                    return a >= (UIntDynamic)b;
                }
                static public bool operator >=(UIntDynamic a, short b)
                {
                    return a >= (UIntDynamic)b;
                }
                static public bool operator >=(UIntDynamic a, char b)
                {
                    return a >= (UIntDynamic)b;
                }
                static public bool operator >=(UIntDynamic a, byte b)
                {
                    return a >= (UIntDynamic)b;
                }
                static public bool operator >=(UIntDynamic a, sbyte b)
                {
                    return a >= (UIntDynamic)b;
                }
                static public bool operator >=(UIntDynamic a, bool b)
                {
                    return a >= (UIntDynamic)b;
                }
                static public bool operator >=(string a, UIntDynamic b)
                {
                    return (UIntDynamic)a >= b;
                }
                static public bool operator >=(ulong a, UIntDynamic b)
                {
                    return (UIntDynamic)a >= b;
                }
                static public bool operator >=(long a, UIntDynamic b)
                {
                    return (UIntDynamic)a >= b;
                }
                static public bool operator >=(uint a, UIntDynamic b)
                {
                    return (UIntDynamic)a >= b;
                }
                static public bool operator >=(int a, UIntDynamic b)
                {
                    return (UIntDynamic)a >= b;
                }
                static public bool operator >=(ushort a, UIntDynamic b)
                {
                    return (UIntDynamic)a >= b;
                }
                static public bool operator >=(short a, UIntDynamic b)
                {
                    return (UIntDynamic)a >= b;
                }
                static public bool operator >=(char a, UIntDynamic b)
                {
                    return (UIntDynamic)a >= b;
                }
                static public bool operator >=(byte a, UIntDynamic b)
                {
                    return (UIntDynamic)a >= b;
                }
                static public bool operator >=(sbyte a, UIntDynamic b)
                {
                    return (UIntDynamic)a >= b;
                }
                static public bool operator >=(bool a, UIntDynamic b)
                {
                    return (UIntDynamic)a >= b;
                }
            #endregion
            #region <= Operator
                static public bool operator <=(UIntDynamic a, UIntDynamic b)
                {
                    return UInt.UIntOperations.SmallerEqual(a.Array, b.Array);
                }
                static public bool operator <=(UIntDynamic a, string b)
                {
                    return a <= (UIntDynamic)b;
                }
                static public bool operator <=(UIntDynamic a, ulong b)
                {
                    return a <= (UIntDynamic)b;
                }
                static public bool operator <=(UIntDynamic a, long b)
                {
                    return a <= (UIntDynamic)b;
                }
                static public bool operator <=(UIntDynamic a, uint b)
                {
                    return a <= (UIntDynamic)b;
                }
                static public bool operator <=(UIntDynamic a, int b)
                {
                    return a <= (UIntDynamic)b;
                }
                static public bool operator <=(UIntDynamic a, ushort b)
                {
                    return a <= (UIntDynamic)b;
                }
                static public bool operator <=(UIntDynamic a, short b)
                {
                    return a <= (UIntDynamic)b;
                }
                static public bool operator <=(UIntDynamic a, char b)
                {
                    return a <= (UIntDynamic)b;
                }
                static public bool operator <=(UIntDynamic a, byte b)
                {
                    return a <= (UIntDynamic)b;
                }
                static public bool operator <=(UIntDynamic a, sbyte b)
                {
                    return a <= (UIntDynamic)b;
                }
                static public bool operator <=(UIntDynamic a, bool b)
                {
                    return a <= (UIntDynamic)b;
                }
                static public bool operator <=(string a, UIntDynamic b)
                {
                    return (UIntDynamic)a <= b;
                }
                static public bool operator <=(ulong a, UIntDynamic b)
                {
                    return (UIntDynamic)a <= b;
                }
                static public bool operator <=(long a, UIntDynamic b)
                {
                    return (UIntDynamic)a <= b;
                }
                static public bool operator <=(uint a, UIntDynamic b)
                {
                    return (UIntDynamic)a <= b;
                }
                static public bool operator <=(int a, UIntDynamic b)
                {
                    return (UIntDynamic)a <= b;
                }
                static public bool operator <=(ushort a, UIntDynamic b)
                {
                    return (UIntDynamic)a <= b;
                }
                static public bool operator <=(short a, UIntDynamic b)
                {
                    return (UIntDynamic)a <= b;
                }
                static public bool operator <=(char a, UIntDynamic b)
                {
                    return (UIntDynamic)a <= b;
                }
                static public bool operator <=(byte a, UIntDynamic b)
                {
                    return (UIntDynamic)a <= b;
                }
                static public bool operator <=(sbyte a, UIntDynamic b)
                {
                    return (UIntDynamic)a <= b;
                }
                static public bool operator <=(bool a, UIntDynamic b)
                {
                    return (UIntDynamic)a <= b;
                }
            #endregion
            #region == Operator
                static public bool operator ==(UIntDynamic a, UIntDynamic b)
                {
                    return UInt.UIntOperations.Equal(a.Array, b.Array);
                }
                static public bool operator ==(UIntDynamic a, string b)
                {
                    return a == (UIntDynamic)b;
                }
                static public bool operator ==(UIntDynamic a, ulong b)
                {
                    return a == (UIntDynamic)b;
                }
                static public bool operator ==(UIntDynamic a, long b)
                {
                    return a == (UIntDynamic)b;
                }
                static public bool operator ==(UIntDynamic a, uint b)
                {
                    return a == (UIntDynamic)b;
                }
                static public bool operator ==(UIntDynamic a, int b)
                {
                    return a == (UIntDynamic)b;
                }
                static public bool operator ==(UIntDynamic a, ushort b)
                {
                    return a == (UIntDynamic)b;
                }
                static public bool operator ==(UIntDynamic a, short b)
                {
                    return a == (UIntDynamic)b;
                }
                static public bool operator ==(UIntDynamic a, char b)
                {
                    return a == (UIntDynamic)b;
                }
                static public bool operator ==(UIntDynamic a, byte b)
                {
                    return a == (UIntDynamic)b;
                }
                static public bool operator ==(UIntDynamic a, sbyte b)
                {
                    return a == (UIntDynamic)b;
                }
                static public bool operator ==(UIntDynamic a, bool b)
                {
                    return a == (UIntDynamic)b;
                }
                static public bool operator ==(string a, UIntDynamic b)
                {
                    return (UIntDynamic)a == b;
                }
                static public bool operator ==(ulong a, UIntDynamic b)
                {
                    return (UIntDynamic)a == b;
                }
                static public bool operator ==(long a, UIntDynamic b)
                {
                    return (UIntDynamic)a == b;
                }
                static public bool operator ==(uint a, UIntDynamic b)
                {
                    return (UIntDynamic)a == b;
                }
                static public bool operator ==(int a, UIntDynamic b)
                {
                    return (UIntDynamic)a == b;
                }
                static public bool operator ==(ushort a, UIntDynamic b)
                {
                    return (UIntDynamic)a == b;
                }
                static public bool operator ==(short a, UIntDynamic b)
                {
                    return (UIntDynamic)a == b;
                }
                static public bool operator ==(char a, UIntDynamic b)
                {
                    return (UIntDynamic)a == b;
                }
                static public bool operator ==(byte a, UIntDynamic b)
                {
                    return (UIntDynamic)a == b;
                }
                static public bool operator ==(sbyte a, UIntDynamic b)
                {
                    return (UIntDynamic)a == b;
                }
                static public bool operator ==(bool a, UIntDynamic b)
                {
                    return (UIntDynamic)a == b;
                }
            #endregion
            #region != Operator
                static public bool operator !=(UIntDynamic a, UIntDynamic b)
                {
                    return !UInt.UIntOperations.Equal(a.Array, b.Array);
                }
                static public bool operator !=(UIntDynamic a, string b)
                {
                    return a != (UIntDynamic)b;
                }
                static public bool operator !=(UIntDynamic a, ulong b)
                {
                    return a != (UIntDynamic)b;
                }
                static public bool operator !=(UIntDynamic a, long b)
                {
                    return a != (UIntDynamic)b;
                }
                static public bool operator !=(UIntDynamic a, uint b)
                {
                    return a != (UIntDynamic)b;
                }
                static public bool operator !=(UIntDynamic a, int b)
                {
                    return a != (UIntDynamic)b;
                }
                static public bool operator !=(UIntDynamic a, ushort b)
                {
                    return a != (UIntDynamic)b;
                }
                static public bool operator !=(UIntDynamic a, short b)
                {
                    return a != (UIntDynamic)b;
                }
                static public bool operator !=(UIntDynamic a, char b)
                {
                    return a != (UIntDynamic)b;
                }
                static public bool operator !=(UIntDynamic a, byte b)
                {
                    return a != (UIntDynamic)b;
                }
                static public bool operator !=(UIntDynamic a, sbyte b)
                {
                    return a != (UIntDynamic)b;
                }
                static public bool operator !=(UIntDynamic a, bool b)
                {
                    return a != (UIntDynamic)b;
                }
                static public bool operator !=(string a, UIntDynamic b)
                {
                    return (UIntDynamic)a != b;
                }
                static public bool operator !=(ulong a, UIntDynamic b)
                {
                    return (UIntDynamic)a != b;
                }
                static public bool operator !=(long a, UIntDynamic b)
                {
                    return (UIntDynamic)a != b;
                }
                static public bool operator !=(uint a, UIntDynamic b)
                {
                    return (UIntDynamic)a != b;
                }
                static public bool operator !=(int a, UIntDynamic b)
                {
                    return (UIntDynamic)a != b;
                }
                static public bool operator !=(ushort a, UIntDynamic b)
                {
                    return (UIntDynamic)a != b;
                }
                static public bool operator !=(short a, UIntDynamic b)
                {
                    return (UIntDynamic)a != b;
                }
                static public bool operator !=(char a, UIntDynamic b)
                {
                    return (UIntDynamic)a != b;
                }
                static public bool operator !=(byte a, UIntDynamic b)
                {
                    return (UIntDynamic)a != b;
                }
                static public bool operator !=(sbyte a, UIntDynamic b)
                {
                    return (UIntDynamic)a != b;
                }
                static public bool operator !=(bool a, UIntDynamic b)
                {
                    return (UIntDynamic)a != b;
                }
            #endregion
            #region Explicit Operators
                static public explicit operator UIntDynamic(ulong number)
                {
                    return new UIntDynamic(number);
                }
                static public explicit operator UIntDynamic(long number)
                {
                    return new UIntDynamic(number);
                }
                static public explicit operator UIntDynamic(uint number)
                {
                    return new UIntDynamic(number);
                }
                static public explicit operator UIntDynamic(int number)
                {
                    return new UIntDynamic(number);
                }
                static public explicit operator UIntDynamic(ushort number)
                {
                    return new UIntDynamic(number);
                }
                static public explicit operator UIntDynamic(short number)
                {
                    return new UIntDynamic(number);
                }
                static public explicit operator UIntDynamic(byte number)
                {
                    return new UIntDynamic(number);
                }
                static public explicit operator UIntDynamic(sbyte number)
                {
                    return new UIntDynamic(number);
                }
                static public explicit operator UIntDynamic(char number)
                {
                    return new UIntDynamic(number);
                }
                static public explicit operator UIntDynamic(bool number)
                {
                    return new UIntDynamic(number);
                }
                static public explicit operator UIntDynamic(string number)
                {
                    return new UIntDynamic(number);
                }
                static public explicit operator ulong(UIntDynamic number)
                {
                    if (number.Array.Length == 1)
                        return (ulong)number.Array[0];
                    else
                        return ((ulong)number.Array[0] + (ulong)number.Array[1] * (ulong)4294967296) & ulong.MaxValue;
                }
                static public explicit operator long(UIntDynamic number)
                {
                    if (number.Array.Length == 1)
                        return (long)number.Array[0];
                    else
                        return (long)(((ulong)number.Array[0] + ((ulong)number.Array[1] & int.MaxValue) * (ulong)4294967296) & (ulong)long.MaxValue);
                }
                static public explicit operator uint(UIntDynamic number)
                {
                    return number.Array[0];
                }
                static public explicit operator int(UIntDynamic number)
                {
                    return (int)(number.Array[0] & int.MaxValue);
                }
                static public explicit operator ushort(UIntDynamic number)
                {
                    return (ushort)(number.Array[0] & ushort.MaxValue);
                }
                static public explicit operator short(UIntDynamic number)
                {
                    return (short)(number.Array[0] & short.MaxValue);
                }
                static public explicit operator byte(UIntDynamic number)
                {
                    return (byte)(number.Array[0] & byte.MaxValue);
                }
                static public explicit operator sbyte(UIntDynamic number)
                {
                    return (sbyte)(number.Array[0] & sbyte.MaxValue);
                }
                static public explicit operator char(UIntDynamic number)
                {
                    return (char)(number.Array[0] & char.MaxValue);
                }
                static public explicit operator bool(UIntDynamic number)
                {
                    return (number.Array[0] & 1) == 1;
                }
                static public explicit operator string(UIntDynamic number)
                {
                    return number.ToString();
                }
            #endregion
        #endregion
    }
}
