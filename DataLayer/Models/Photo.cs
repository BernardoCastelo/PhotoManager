using System;

namespace DataLayer
{
    public class Photo: IBaseModel
    {
        public int Id { get; set; }
        public DateTimeOffset? DateTaken { get; set; }
        public int? CameraId { get; set; }
        public int? CategoryId { get; set; }
        public int? FileId { get; set; }
        public int? FocalLength { get; set; }
        public int? Height { get; set; }
        public int? Iso { get; set; }
        public int? Order { get; set; }
        public int? Width { get; set; }
        public decimal? ExposureAsNumber { get; set; }
        public string Exposure { get; set; }
        public string FStop { get; set; }
        public decimal? FStopAsNumber { get; set; }
        public string Name { get; set; }
        public byte[] Thumbnail { get; set; }
    }
}
