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
        [JsonPropertyName("healthCenter")]
        public NominalDto HealthCenter { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("recipe")]
        public string Recipe { get; set; }
        [JsonPropertyName("doctor")]
        public NominalDto Doctor { get; set; }

    }
    public class NominalDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("area")]
        public string Area { get; set; }
        [JsonPropertyName("career")]
        public string Career { get; set; }
    }

    public class InfoReviewsDto : PatientDto
    {
        [JsonPropertyName("doctors")]
        public IEnumerable<NominalDto> Doctors { get; set; }
        [JsonPropertyName("healthCenters")]
        public IEnumerable<NominalDto> HealthCenters { get; set; }
        [JsonPropertyName("reviews")]
        public IEnumerable<ReviewDto> Reviews { get; set; }
    }


    public class DoctorsDto : PatientDto
    {
        [JsonPropertyName("doctors")]
        public List<ChatDoctorDto> Doctors { get; set; }
    }

    public class ChatDoctorDto : NominalDto
    {
        [JsonPropertyName("chats")]
        public List<ChatDto> Chats { get; set; }
    }

    public class ChatDto
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }
        [JsonPropertyName("me")]
        public bool Me { get; set; }
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

    }


    public class ProfileDto : PatientDto
    {
        [JsonPropertyName("kindreds")]
        public IEnumerable<KindredDto> Kindreds { get; set; }
    }

    public class KindredDto
    {
        [JsonPropertyName("level")]
        public string Level { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

}
