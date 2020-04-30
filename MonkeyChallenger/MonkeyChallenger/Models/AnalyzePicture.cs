using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MonkeyChallenger.Models
{
    public class AnalyzePicture
    {
        public string Description { get; set; }
        public Color TypeColor { get; set; }
        public string LandmarkDescription { get; }

        public AnalyzePicture(string description,Color typeColor,string landmarkDescription)
        {
            Description = $"I see {description}";
            TypeColor = typeColor;
            LandmarkDescription = string.IsNullOrWhiteSpace(landmarkDescription) ? "" : $"And I think I recognize {landmarkDescription}";
        }
    }
}
