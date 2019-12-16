﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PROJECT_NAME.Sqs.Models
{
    public class Movie
    {
        public string Title { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public MovieGenre Genre { get; set; }
    }
}