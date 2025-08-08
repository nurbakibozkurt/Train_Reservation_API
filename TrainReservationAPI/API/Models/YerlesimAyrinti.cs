namespace API.Models
{
    public class YerlesimAyrinti
    {
        public YerlesimAyrinti(string ad, int rezervasyonSayisi)
        {
            VagonAdi = ad;
            KisiSayisi = rezervasyonSayisi;
        }

        public string VagonAdi { get; set; }
        public int KisiSayisi { get; set; }
    }
}