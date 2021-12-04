using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

#nullable disable

namespace CityPuzzleAPI.Model
{
    [DataContract]
    public partial class RoomTask
    {
        [DataMember]
        public int RoomId { get; set; }
        [DataMember]
        public int PuzzleId { get; set; }
    }
}
