﻿using System;
using System.Runtime.InteropServices;
using Voltaic.Serialization.Utf8;

namespace Voltaic.Serialization.Etf
{
    public static partial class EtfReader
    {
        public static bool TryReadSingle(ref ReadOnlySpan<byte> remaining, out float result, char standardFormat)
        {
            result = default;

            switch (GetTokenType(ref remaining))
            {
                case EtfTokenType.NewFloatExt:
                    {
                        if (remaining.Length < 8)
                            return false;
                        //remaining = remaining.Slice(1)
                        result = MemoryMarshal.Cast<byte, float>(remaining.Slice(1, 8))[0];
                        remaining = remaining.Slice(9);
                        return true;
                    }
                case EtfTokenType.StringExt:
                case EtfTokenType.BinaryExt:
                    {
                        if (!TryReadUtf8Bytes(ref remaining, out var bytes))
                            return false;
                        return Utf8Reader.TryReadSingle(ref remaining, out result, standardFormat);
                    }
                default:
                    return false;
            }
        }

        public static bool TryReadDouble(ref ReadOnlySpan<byte> remaining, out double result, char standardFormat)
        {
            result = default;

            switch (GetTokenType(ref remaining))
            {
                case EtfTokenType.NewFloatExt:
                    {
                        if (remaining.Length < 8)
                            return false;
                        //remaining = remaining.Slice(1)
                        result = MemoryMarshal.Cast<byte, float>(remaining.Slice(1, 8))[0];
                        remaining = remaining.Slice(9);
                        return true;
                    }
                case EtfTokenType.StringExt:
                case EtfTokenType.BinaryExt:
                    {
                        if (!TryReadUtf8Bytes(ref remaining, out var bytes))
                            return false;
                        return Utf8Reader.TryReadDouble(ref remaining, out result, standardFormat);
                    }
                default:
                    return false;
            }
        }

        public static bool TryReadDecimal(ref ReadOnlySpan<byte> remaining, out decimal result, char standardFormat)
        {
            result = default;

            switch (GetTokenType(ref remaining))
            {
                case EtfTokenType.StringExt:
                case EtfTokenType.BinaryExt:
                    {
                        if (!TryReadUtf8Bytes(ref remaining, out var bytes))
                            return false;
                        return Utf8Reader.TryReadDecimal(ref remaining, out result, standardFormat);
                    }
                default:
                    return false;
            }
        }
    }
}
