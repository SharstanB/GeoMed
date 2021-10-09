using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace GeoMedHybrid
{

    public class login
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("username")]
        public string Username { get; set; }
        [JsonPropertyName("password")]
        public string Password { get; set; }
    }

    public class RegisterDto
    {
        [JsonPropertyName("username")]
        public string Username { get; set; }
        [JsonPropertyName("password")]
        public string Password { get; set; }
        [JsonPropertyName("fullName")]
        public string FullName { get; set; }
        [JsonPropertyName("area")]
        public string Area { get; set; }
        [JsonPropertyName("career")]
        public string Career { get; set; }
    }

    public class PatientDto
    {
        [JsonPropertyName("fullName")]
        public string FullName { get; set; }
        [JsonPropertyName("area")]
        public string Area { get; set; }
        [JsonPropertyName("career")]
        public string Career { get; set; }
    }
    public class HomeDto : PatientDto
    {
        [JsonPropertyName("reviews")]
        public IEnumerable<ReviewDto> Reviews { get; set; }
    }

    public class ReviewDto
    {
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }
        [JsonPropertyName("nextReviewDate")]
        public DateTime NextReviewDate { get; set; }
        [JsonPropertyName("note")]
        public string Note { get; set; }
        [JsonPropertyName("healthCenterName")]
        public string HealthCenterName { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("recipe")]
        public string Recipe { get; set; }
        [JsonPropertyName("doctorCareer")]
        public string DoctorCareer { get; set; }

    }

}
