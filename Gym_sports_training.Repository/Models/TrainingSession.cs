using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gym_sports_training.Repository.Models
{
    public class TrainingSession
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int CoachId { get; set; }
        public DateTime TrainingTimeStart { get; set; }

        public virtual Client Client { get; set; }
        public virtual Coach Coach { get; set; }
    }
}