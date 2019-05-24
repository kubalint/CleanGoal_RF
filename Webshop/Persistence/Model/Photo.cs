using System;

namespace Persistence.Model
{
    
    public class Photo
    {
        public Guid ID { get; set; }
        public string MimeType { get; set; }
        public byte[] PhotoFile { get; set; }
        
    }
}