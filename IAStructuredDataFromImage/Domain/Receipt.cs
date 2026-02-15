namespace IAStructuredDataFromImage.Domain;

public class Receipt
{
        public string StoreName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Manager { get; set; } = string.Empty;

        public List<LineItem> Items { get; set; } = new();

        public decimal Subtotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }
}