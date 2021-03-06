﻿namespace WorkOS
{
    using System.Runtime.Serialization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Constants that enumerate available <see cref="Connection"/> types.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ConnectionType
    {
        [EnumMember(Value = "ADFSSAML")]
        ADFSSAML,

        [EnumMember(Value = "AzureSAML")]
        AzureSAML,

        [EnumMember(Value = "GenericOIDC")]
        GenericOIDC,

        [EnumMember(Value = "GenericSAML")]
        GenericSAML,

        [EnumMember(Value = "GoogleOAuth")]
        GoogleOAuth,

        [EnumMember(Value = "MagicLink")]
        MagicLink,

        [EnumMember(Value = "OktaSAML")]
        OktaSAML,

        [EnumMember(Value = "OneLoginSAML")]
        OneLoginSAML,

        [EnumMember(Value = "PingFederateSAML")]
        PingFederateSAML,

        [EnumMember(Value = "PingOneSAML")]
        PingOneSAML,

        [EnumMember(Value = "VMwareSAML")]
        VMwareSAML,
    }
}
