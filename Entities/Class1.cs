namespace Entities
{
    public abstract class BaseSearchParams
    {
        public int Total { get; set; }
        public int? ObjectsCount { get; set; }
        public int StartIndex { get; set; }
        
    }
}