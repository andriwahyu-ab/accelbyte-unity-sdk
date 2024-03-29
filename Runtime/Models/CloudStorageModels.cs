﻿// Copyright (c) 2018 - 2020 AccelByte Inc. All Rights Reserved.
// This is licensed software from AccelByte Inc, for limitations
// and restrictions contact your company contract manager.

using System;
using System.Runtime.Serialization;

namespace AccelByte.Models
{
    [DataContract]
    public class Slot
    {
        [DataMember] public string checksum { get; set; }
        [DataMember] public string customAttribute { get; set; }
        [DataMember] public DateTime dateAccessed { get; set; }
        [DataMember] public DateTime dateCreated { get; set; }
        [DataMember] public DateTime dateModified { get; set; }
        [DataMember] public string label { get; set; }
        [DataMember] public string mimeType { get; set; }
        [DataMember] public string namespaceId { get; set; }
        [DataMember] public string originalName { get; set; }
        [DataMember] public string slotId { get; set; }
        [Obsolete("Status property is deprecated on new cloudstorage service.")]
        [DataMember] public string status { get; set; }
        [DataMember] public string storedName { get; set; }
        [DataMember] public string[] tags { get; set; }
        [DataMember] public string userId { get; set; }
        
    }

    [DataContract]
    public class UpdateMedataRequest
    {
        [DataMember] public string label { get; set; }
        [DataMember] public string[] tags { get; set; }
        [DataMember] public string customAttribute { get; set; }

    }
}
