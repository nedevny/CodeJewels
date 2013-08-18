﻿using System.Runtime.Serialization;

namespace CodeJewelApi.Models
{
    [DataContract(Name = "CodeJewel")]
    public class CodeJewelModelFull : CodeJewelModel
    {
        [DataMember(Name = "avgVote")]
        public double AverageVote { get; set; }

        [DataMember(Name = "category")]
        public string Category { get; set; }
    }
}