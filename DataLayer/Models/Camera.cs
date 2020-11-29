using System;

namespace DataLayer
{
    public class Camera: IBaseModel
    {
        public int Id { get; set; }
        public string Maker { get; set; }
        public string Model { get; set; }
    }
}
