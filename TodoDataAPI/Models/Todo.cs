using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoDataAPI.Models
{
    public class Todo
    {
        [Key]
        public int TodoID { get; set; }

        [Required]
        
        public string Title { get; set; }

        
        public string Description { get; set; }

        public DateTime? CreatedAt { get; set; }


        public bool Done { get; set; } = false;



    }
}
