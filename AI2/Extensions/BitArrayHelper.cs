﻿using System.Collections;
using System.Text;

namespace Extensions.BitArrays {
    public static class BitArrayHelper {

        public static BitArray Merge(this IEnumerable<BitArray> bitArrayArray) {
            if (!bitArrayArray.Any()) {
                return null;
            }

            BitArray bitArray = new(bitArrayArray.First());
            foreach (var bitA in bitArrayArray.Skip(1)) {
                bitArray.MergeWith(bitA);
            }
            return bitArray;
        }

        public static BitArray MergeWith(this BitArray bitArray, BitArray otherBitArray) {
            var totLength = bitArray.Length + otherBitArray.Length;
            var merged = new BitArray(totLength);

            for (int i = 0; i < bitArray.Length; i++) {
                merged[i] = bitArray[i];
            }

            for (int i = 0; i < otherBitArray.Length; i++) {
                merged[i + bitArray.Length] = otherBitArray[i];
            }

            return merged;
        }

        public static byte[] ToBytes(this BitArray bitArray) {
            var arrayLength = bitArray.Count / 8;

            byte[] array = new byte[arrayLength];
            bitArray.CopyTo(array, 0);

            return array;
        }

        public static void WriteBytesToMemStream(this BitArray bitArray, MemoryStream ms) {
            BinaryWriter bw = new BinaryWriter(ms);
            bw.Write(bitArray.ToBytes());
        }

        public static bool EqualTo(this BitArray bitArray, BitArray other) {
            if (bitArray.Count != other.Count) {
                return false;
            }

            for (int i = 0; i < bitArray.Count; i++) {
                if (bitArray[i] != other[i]) {
                    return false;
                }
            }

            return true;
        }

        public static BitArray FromString(string str) {
            str = str.Replace(" ", "");

            if (str.Any(c => c != '0' && c != '1')) {
                throw new Exception($"String: \"{str}\" does not represent valid bit array");
            }

            return new BitArray(str.Select(c => c == '1').ToArray());
        }

        public static string ToBitString(this BitArray bitArray) {
            if (bitArray == null)
                return null;
            StringBuilder sb = new StringBuilder(bitArray.Length);
            for (int i = 0; i < bitArray.Length; i++) {
                sb.Append(bitArray[i] ? "1" : "0");
            }

            return sb.ToString();
        }

        public static string ToPrettyBitString(this BitArray bitArray) {
            if (bitArray == null)
                return null;
            StringBuilder sb = new StringBuilder(bitArray.Length);
            for (int i = 0; i < bitArray.Length; i++) {
                if (i % 8 == 0 && i > 0)
                    sb.Append(' ');

                sb.Append(bitArray[i] ? "1" : "0");
            }

            return sb.ToString();
        }

        public static string ToBitStringWithDecAndHexLE(this BitArray bitArray) {
            if (bitArray == null)
                return null;
            StringBuilder sb = new StringBuilder(bitArray.Length);
            for (int i = 0; i < bitArray.Length; i++) {
                sb.Append(bitArray[i] ? "1" : "0");
            }

            var @int = bitArray.ToIntLE();
            sb.Append($" (0x{BitConverter.ToString(BitConverter.GetBytes(@int).Reverse().ToArray())}) ({@int})");

            return sb.ToString();
        }

        public static double CompareTo(this BitArray bitArray, BitArray other) {
            if (bitArray == null || other == null)
                return 0;

            if (bitArray.Count != other.Count) {
                return 0;
            }

            int sameBits = 0;

            for (int i = 0; i < bitArray.Count; i++) {
                if (bitArray[i] == other[i]) {
                    sameBits++;
                }
            }

            return (double)sameBits / bitArray.Length;
        }

        public static BitArray Skip(this BitArray bitArray, int skip) {
            return FromString(bitArray.ToBitString()[skip..]);
        }

        public static BitArray Take(this BitArray bitArray, int take) {
            return FromString(bitArray.ToBitString()[..take]);
        }

        public static BitArray Slice(this BitArray bitArray, int skip, int take) {
            return FromString(bitArray.ToBitString().Substring(skip, take));
        }

        public static BitArray SkipLast(this BitArray bitArray, int skip) {
            return FromString(bitArray.ToBitString().Substring(0, bitArray.Count - skip));
        }

        public static BitArray FromLongLE(long long64) {
            var ba = new BitArray(BitConverter.GetBytes(long64));
            ba.Reverse();
            return ba;
        }

        public static BitArray FromULongLE(ulong ulong64) {
            var ba = new BitArray(BitConverter.GetBytes(ulong64));
            ba.Reverse();
            return ba;
        }

        public static BitArray FromIntLE(int int32) {
            var ba = new BitArray(BitConverter.GetBytes(int32));
            ba.Reverse();
            return ba;
        }

        public static BitArray FromUIntLE(uint uint32) {
            var ba = new BitArray(BitConverter.GetBytes(uint32));
            ba.Reverse();
            return ba;
        }

        public static BitArray FromShortLE(short short16) {
            var ba = new BitArray(BitConverter.GetBytes(short16));
            ba.Reverse();
            return ba;
        }

        public static BitArray FromUShortLE(ushort ushort16) {
            var ba = new BitArray(BitConverter.GetBytes(ushort16));
            ba.Reverse();
            return ba;
        }

        public static BitArray FromByteLE(byte @byte) {
            var ba = new BitArray(new[] { @byte });
            ba.Reverse();
            return ba;
        }

        public static BitArray FromSByteLE(sbyte @byte) {
            var ba = new BitArray(new[] { (byte)@byte });
            ba.Reverse();
            return ba;
        }

        public static BitArray FromInt(int int32) {
            return new BitArray(BitConverter.GetBytes(int32));
        }

        public static BitArray FromUInt(uint uint32) {
            return new BitArray(BitConverter.GetBytes(uint32));
        }

        public static BitArray FromShort(short short16) {
            return new BitArray(BitConverter.GetBytes(short16));
        }

        public static BitArray FromUShort(ushort ushort16) {
            return new BitArray(BitConverter.GetBytes(ushort16));
        }

        public static BitArray FromByte(byte @byte) {
            return new BitArray(new[] { @byte });
        }

        public static BitArray FromSByte(sbyte @byte) {
            return new BitArray(new[] { (byte)@byte });
        }

        public static byte ToByte(this BitArray bitArray) {
            if (bitArray.Length != 8)
                throw new Exception("Length must be equal to 8");

            byte[] array = new byte[1];
            bitArray.CopyTo(array, 0);
            return array[0];
        }

        public static byte ToByteLE(this BitArray bitArray) {
            if (bitArray.Length != 8)
                throw new Exception("Length must be equal to 8");

            int sum = 0;
            for (int i = 7; i >= 0; i--) {
                sum += bitArray[i] ? 1 << 7 - i : 0;
            }

            return (byte)sum;
        }

        public static sbyte ToSByteLE(this BitArray bitArray) {
            if (bitArray.Length != 8)
                throw new Exception("Length must be equal to 8");

            int sum = 0;
            for (int i = 7; i >= 0; i--) {
                sum += bitArray[i] ? 1 << 7 - i : 0;
            }

            return (sbyte)sum;
        }

        public static short ToShortLE(this BitArray bitArray) {
            if (bitArray.Length != 16)
                throw new Exception("Length must be equal to 16");

            int sum = 0;
            for (int i = 15; i >= 0; i--) {
                sum += bitArray[i] ? 1 << 15 - i : 0;
            }

            return (short)sum;
        }

        public static ushort ToUShortLE(this BitArray bitArray) {
            if (bitArray.Length != 16)
                throw new Exception("Length must be equal to 16");

            int sum = 0;
            for (int i = 15; i >= 0; i--) {
                sum += bitArray[i] ? 1 << 15 - i : 0;
            }

            return (ushort)sum;
        }

        public static int ToIntLE(this BitArray bitArray) {
            if (bitArray.Length != 32)
                throw new Exception("Length must be equal to 32");

            int sum = 0;
            for (int i = 31; i >= 0; i--) {
                sum += bitArray[i] ? 1 << 31 - i : 0;
            }

            return sum;
        }

        public static uint ToUIntLE(this BitArray bitArray) {
            if (bitArray.Length != 32)
                throw new Exception("Length must be equal to 32");

            int sum = 0;
            for (int i = 31; i >= 0; i--) {
                sum += bitArray[i] ? 1 << 31 - i : 0;
            }

            return (uint)sum;
        }

        public static int GetSignedValueLE(this BitArray bitArray) {
            if (bitArray.Length > 32)
                throw new Exception("Length must be less or equal to 32");

            var lengthMinusOne = bitArray.Length - 1;
            int sum = 0;
            for (int i = lengthMinusOne; i >= 0; i--) {
                sum += bitArray[i] ? 1 << lengthMinusOne - i : 0;
            }

            return sum;
        }

        public static uint GetUnsignedValueLE(this BitArray bitArray) {
            if (bitArray.Length > 32)
                throw new Exception("Length must be less equal to 32");

            var lengthMinusOne = bitArray.Length - 1;
            int sum = 0;
            for (int i = lengthMinusOne; i >= 0; i--) {
                sum += bitArray[i] ? 1 << lengthMinusOne - i : 0;
            }

            return (uint)sum;
        }

        public static long GetSignedLongValueLE(this BitArray bitArray) {
            if (bitArray.Length > 64)
                throw new Exception("Length must be less or equal to 64");

            var lengthMinusOne = bitArray.Length - 1;
            long sum = 0;
            for (int i = lengthMinusOne; i >= 0; i--) {
                sum += bitArray[i] ? 1 << lengthMinusOne - i : 0;
            }

            return sum;
        }

        public static ulong GetUnsignedLongValueLE(this BitArray bitArray) {
            if (bitArray.Length > 64)
                throw new Exception("Length must be less equal to 64");

            var lengthMinusOne = bitArray.Length - 1;
            long sum = 0;
            for (int i = lengthMinusOne; i >= 0; i--) {
                sum += bitArray[i] ? 1 << lengthMinusOne - i : 0;
            }

            return (ulong)sum;
        }

        public static void Reverse(this BitArray array) {
            int length = array.Length;
            int mid = (length / 2);

            for (int i = 0; i < mid; i++) {
                bool bit = array[i];
                array[i] = array[length - i - 1];
                array[length - i - 1] = bit;
            }
        }
    }
}
