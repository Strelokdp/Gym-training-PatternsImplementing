using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gym_sports_training.Repository.Models
{
    public class Client
    {
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        [MinLength(2), MaxLength(20, ErrorMessage = "First name cannot be longer than 20 characters.")]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        [MinLength(2), MaxLength(20, ErrorMessage = "Last name cannot be longer than 20 characters.")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Please enter valid e-mail")]
        public string EMail { get; set; }

        [Required]
        [Phone(ErrorMessage = "Please enter valid phone number")]
        public string PhoneNumber { get; set; }

        public virtual List<TrainingSession> TrainingSessions { get; set; }

        public string FullName { get { return Name + " " + LastName; } }
    }
}
