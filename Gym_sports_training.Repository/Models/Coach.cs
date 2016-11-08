using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Gym_sports_training.Repository.Models
{
    public enum Speciality
    {
        Fitness,
        Cardio,
        Boxing,
        MMA
    }

    public class Coach
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
        public Speciality? Speciality { get; set; }

        [Required]
        [Range(20, 200, ErrorMessage = "The price can be from 20 to 200")]
        public int Price { get; set; }

        [Range(30, 120, ErrorMessage = "The training can be from 30 up to 120 minutes")]
        public int TrainingLength { get; set; }

        [Required]
        public string Description { get; set; }

        public virtual List<TrainingSession> TrainingSessions { get; set; }

        public string FullName { get { return Name + " " + LastName; } }
    }
}