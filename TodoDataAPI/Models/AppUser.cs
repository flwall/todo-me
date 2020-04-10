using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace TodoDataAPI.Models
{
    public class AppUser:IdentityUser
    {
        [JsonIgnore]
        public List<Todo> Todos { get; set; } = new List<Todo>();
    }
}
