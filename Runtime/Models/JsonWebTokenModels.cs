﻿// Copyright (c) 2021 AccelByte Inc. All Rights Reserved.
// This is licensed software from AccelByte Inc, for limitations
// and restrictions contact your company contract manager.

using System.Runtime.Serialization;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AccelByte.Models
{
    [JsonConverter( typeof( StringEnumConverter ) )]
    public enum JsonWebTokenIndex
    {
        Header,
        Payload,
        Signature
    }

    [DataContract]
    public class JsonWebTokenDefaultHeader
    {
        [DataMember] public string alg { get; set; }
        [DataMember] public string kid { get; set; }
        [DataMember] public string typ { get; set; }
    }
}
