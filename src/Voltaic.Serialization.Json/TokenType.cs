﻿namespace Voltaic.Serialization.Json
{
    public enum TokenType : byte
    {
        None = 0,
        Null,
        Undefined,
        True,
        False,
        Number,
        String,
        StartObject,
        EndObject,
        StartArray,
        EndArray,
        ListSeparator,
        KeyValueSeparator
    }
}
