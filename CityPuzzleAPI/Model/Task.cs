using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading.Tasks;

#nullable disable

namespace CityPuzzleAPI.Model
{
    [DataContract]
    public partial class Task
    {
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public int PuzzleId { get; set; }

    }
}
