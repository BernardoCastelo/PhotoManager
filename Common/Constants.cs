namespace Common
{
    public static class Constants
    {
        public static class DbConstants
        {
            public const string Id = nameof(Id);
        }
        public static class Folders
        {
            public const string Main = @"D:\Photos";
            public const string Thumbnails = @"C:\PhotoThumbnails";
        }
        public static class ImageProperties
        {
            public const string ImageHeight = "Image Height";
            public const string ImageHWidth = "Image Width";
            public const string CameraMake = "Make";
            public const string CameraModel = "Model";
            public const string FNumber = "F-Number";
            public const string ISO = "ISO Speed Ratings";
            public const string Exposure = "Exposure Time";
            public const string DateTIme = "Date/Time Original";
            public const string FocalLength = "Focal Length";

            public const int ThumbWidth = 240;
            public const int ThumbHeight = 180;
            public const int ThumbMultiplier = 20;
        }
    }
}
