using System;
using System.Collections.Generic;

#nullable disable

namespace CityPuzzleAPI.Model
{
    public partial class Room
    {
        public int Id { get; set; }
        public string RoomPin { get; set; }
        public int? Owner { get; set; }
        public int? RoomSize { get; set; }
    }
}
