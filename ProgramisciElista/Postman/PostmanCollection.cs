using System;
using System.Collections.ObjectModel;

namespace ProgramisciElista.Postman
{
    public class PostmanCollection
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public long timestamp { get; set; }
        public Collection<PostmanRequest> requests { get; set; }
    }
}