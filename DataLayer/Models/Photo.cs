using System;

namespace DataLayer
{
    public class Photo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public int FileId { get; set; }
        public int CameraId { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string FStop { get; set; }
        public string Exposure { get; set; }
        public int Iso { get; set; }
        public int FocalLength { get; set; }
        public DateTimeOffset DateTaken { get; set; }
    }
}
