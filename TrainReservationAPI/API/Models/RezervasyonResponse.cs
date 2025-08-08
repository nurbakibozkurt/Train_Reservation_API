namespace API.Models
{
    public class RezervasyonResponse
    {   
        public bool RezervasyonYapilabilir { get; set; }
        public List<YerlesimAyrinti>? YerlesimAyrintilari { get; set; }
    }
}
