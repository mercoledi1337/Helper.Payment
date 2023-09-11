namespace Helper.Payments.Shared.DTO
{
    public class OfferacceptedEvent
    {
        public Guid OfferId { get; set; }
        public double Price { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public DateTime RealisationStart { get; set; }
        public DateTime? RealisationEnd { get; set; }
    }
}
