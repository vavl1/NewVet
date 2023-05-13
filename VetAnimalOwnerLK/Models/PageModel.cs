namespace VetAnimalOwnerLK.Models
{
    public class PageModel<T>
    {
        public List<T> Objects { get; set; }
        public int PageNumber { get; private set; }
        public int TotalPages { get; private set; }

        public PageModel(int count, int pageNumber, int pageSize, List<T> objects)
        {
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            Objects = objects;
        }

        public bool HasPreviousPage
        {
            get
            {
                return (PageNumber > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageNumber < TotalPages);
            }
        }
    }
}
