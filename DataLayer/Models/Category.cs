using System;

namespace DataLayer
{
    public class Category: IBaseModel
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Name { get; set; }
    }
}
