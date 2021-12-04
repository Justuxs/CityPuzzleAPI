using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

#nullable disable

namespace CityPuzzleAPI.Model
{
    [DataContract]
    public partial class Participant
    {
        [DataMember]
        public int RoomId { get; set; }
        [DataMember]
        public int UserId { get; set; }
    }
}
