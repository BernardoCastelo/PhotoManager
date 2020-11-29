using System;

namespace DataLayer
{
    public class Folder: IBaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
    }
}
