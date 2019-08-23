using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MultiplexData.Models
{
    public class User : IdentityUser
    {
        public IEnumerable<SeatRun> SeatRuns { get; set; }
    }
}
