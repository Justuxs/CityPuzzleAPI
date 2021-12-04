using System;
using System.Collections.Generic;

#nullable disable

namespace CityPuzzleAPI.Model
{
    public partial class Puzzle
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string About { get; set; }
        public string Quest { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string ImgAdress { get; set; }
    }
}
