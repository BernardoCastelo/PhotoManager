using System;

namespace DataLayer.Dtos
{
    public class Filter
    {
        public string FieldName { get; set; }
        public object LowerValue { get; set; }
        public object Value { get; set; }
        public object UpperValue { get; set; }
        public bool Negate { get; set; }
    }
}
