using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace EF_lab.Models
{
    public class CompactDisc
    {
        public int CompactDiscId { get; set; }
        
        [DisplayName("Title")]
        public string CompactDiscTitle { get; set; }
        
        [DisplayName("Artist")]
        public string ArtistName { get; set; }
        
        [DisplayName("Genre")]
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}
