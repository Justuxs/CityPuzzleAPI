using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace CityPuzzleAPI.Model
{
    public class CompletedPuzzle
    {
        [DataMember]
        public int CompletedPuzzleId { get; set; }
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public int PuzzleId { get; set; }
        [DataMember]
        public int Score { get; set; }
    }
}
