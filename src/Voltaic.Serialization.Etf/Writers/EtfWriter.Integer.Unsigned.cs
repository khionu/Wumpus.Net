﻿using System;
using System.Buffers;
using System.Buffers.Binary;
using Voltaic.Serialization.Utf8;

namespace Voltaic.Serialization.Etf
{
    public static partial class EtfWriter
    {
        public static bool TryWrite(ref ResizableMemory<byte> writer, byte value, StandardFormat standardFormat)
        {
            if (standardFormat.IsDefault)
            {
                writer.Push((byte)EtfTokenType.SmallIntegerExt);
                writer.Push(value);
            }
            else
            {
                writer.Push((byte)EtfTokenType.StringExt);
                writer.Advance(2);
                int start = writer.Length;
                if (!Utf8Writer.TryWrite(ref writer, value, standardFormat))
                    return false;
                int length = writer.Length - start;
                if (length > ushort.MaxValue)
                    return false;
                writer.Array[start - 2] = (byte)(length >> 8);
                writer.Array[start - 1] = (byte)0xFF;
            }
            return true;
        }

        public static bool TryWrite(ref ResizableMemory<byte> writer, ushort value, StandardFormat standardFormat)
        {
            if (standardFormat.IsDefault)
            {
                writer.Push((byte)EtfTokenType.IntegerExt);
                BinaryPrimitives.WriteInt32BigEndian(writer.GetSpan(4), value);
                writer.Advance(4);
            }
            else
            {
                writer.Push((byte)EtfTokenType.StringExt);
                writer.Advance(2);
                int start = writer.Length;
                if (!Utf8Writer.TryWrite(ref writer, value, standardFormat))
                    return false;
                int length = writer.Length - start;
                if (length > ushort.MaxValue)
                    return false;
                writer.Array[start - 2] = (byte)(length >> 8);
                writer.Array[start - 1] = (byte)length;
            }
            return true;
        }

        public static bool TryWrite(ref ResizableMemory<byte> writer, uint value, StandardFormat standardFormat)
        {
            if (standardFormat.IsDefault)
            {
                writer.Push((byte)EtfTokenType.SmallBigExt);
                writer.Push(8);
                writer.Push(0);
                BinaryPrimitives.WriteUInt64LittleEndian(writer.GetSpan(8), value);
                writer.Advance(8);
            }
            else
            {
                writer.Push((byte)EtfTokenType.StringExt);
                writer.Advance(2);
                int start = writer.Length;
                if (!Utf8Writer.TryWrite(ref writer, value, standardFormat))
                    return false;
                int length = writer.Length - start;
                if (length > ushort.MaxValue)
                    return false;
                writer.Array[start - 2] = (byte)(length >> 8);
                writer.Array[start - 1] = (byte)length;
            }
            return true;
        }

        public static bool TryWrite(ref ResizableMemory<byte> writer, ulong value, StandardFormat standardFormat)
        {
            if (standardFormat.IsDefault)
            {
                writer.Push((byte)EtfTokenType.SmallBigExt);
                writer.Push(8);
                writer.Push(0);
                BinaryPrimitives.WriteUInt64LittleEndian(writer.GetSpan(8), value);
                writer.Advance(8);
            }
            else
            {
                writer.Push((byte)EtfTokenType.StringExt);
                writer.Advance(2);
                int start = writer.Length;
                if (!Utf8Writer.TryWrite(ref writer, value, standardFormat))
                    return false;
                int length = writer.Length - start;
                if (length > ushort.MaxValue)
                    return false;
                writer.Array[start - 2] = (byte)(length >> 8);
                writer.Array[start - 1] = (byte)length;
            }
            return true;
        }
    }
}
