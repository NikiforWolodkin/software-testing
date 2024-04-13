using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MessengerModels.Models
{
    public class BlacklistAddRequest
    {
        [JsonPropertyName("userId")]
        public Guid UserId { get; set; }
    }
}
