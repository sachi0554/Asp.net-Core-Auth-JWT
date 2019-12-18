using App.Core.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace App.Core.Model
{
    public class Game :BaseEntity 
    {
        [Required]
        public string GameName { get; set; }
        [Required]
        public string GameCode { get; set; }
        public string Game_Description { get; set; }
        public string GamePoint { get; set; }
        public string GameType { get; set; }
        public string GameOrigin { get; set; }

    }
}
