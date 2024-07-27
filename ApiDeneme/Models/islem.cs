namespace ApiDeneme.Models
{
    public class islem
    {
        public int Id { get; set; }
        public string? Tarih { get; set; }
        public float? toplamIslem { get; set; }
        public decimal? toplamIslemFiyat { get; set; }
        public decimal? agirlikliOrtalama { get; set; }
    }
}
