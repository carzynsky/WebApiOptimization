namespace WebApiOptimization.Application.Responses
{
    public class CategoryResponse
    {
        public int CategoryID  { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public byte[] Picture { get; set; }
    }
}
