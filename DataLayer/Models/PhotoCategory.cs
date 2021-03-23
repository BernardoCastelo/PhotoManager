namespace DataLayer
{
    public class PhotoCategory : IBaseModel
    {
        public int Id { get; set; }
        public int PhotoId { get; set; }
        public int CategoryId { get; set; }
    }
}
