namespace CyloAPI.Model
{
    public class OrderDetails
    {
        public int id { get; set; }
        public int userId { get; set; }
        public string address { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
    public string prodName { get; set; }
        public int cost { get; set; }
    }
}
