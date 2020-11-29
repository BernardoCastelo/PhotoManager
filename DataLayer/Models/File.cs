using System;

namespace DataLayer
{
    public class File
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Fullpath { get; set; }
        public int FolderId { get; set; }
        public int FileTypeId { get; set; }
        public int SizeKB { get; set; }
        public DateTimeOffset Created { get; set; }
    }
}
