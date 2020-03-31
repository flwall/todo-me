using Newtonsoft.Json;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace TodoDataAPI.Models
{
    public class User
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }


        [JsonIgnore]
        public List<Todo> Todos { get; set; } = new List<Todo>();
    }
}
