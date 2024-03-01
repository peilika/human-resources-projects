namespace HRUI.Models
{
    public class FenYe<T> where T : class
    {
        public IEnumerable<T> fenYes { get; set; }
        public int currentPage
        { get; set; }

        public int pageSize { get; set; }

        public int rows { get; set; }
    }
}
