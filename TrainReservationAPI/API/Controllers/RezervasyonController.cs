using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RezervasyonController : ControllerBase
    {
        [HttpPost]
        public RezervasyonResponse RezervasyonCheck([FromBody] RezervasyonRequest rezervasyonReq)
        {
            RezervasyonResponse response = new();
            int rezervasyonSayisi = rezervasyonReq.RezervasyonYapilacakKisiSayisi;
            List<YerlesimAyrinti> yerlesimAyrintiList = [];

            if (!rezervasyonReq.KisilerFarkliVagonlaraYerlestirilebilir)
            {
                foreach (Vagon vagon in rezervasyonReq.Tren.Vagonlar)
                {
                    if (IsVagonAvailable(vagon))
                    {
                        if (AvailableVagonCapacity(vagon) >= rezervasyonSayisi)
                        {
                            yerlesimAyrintiList.Add(new YerlesimAyrinti(vagon.Ad, rezervasyonSayisi));
                            response.RezervasyonYapilabilir = true;
                            response.YerlesimAyrintilari = yerlesimAyrintiList;
                            return response;
                        }
                    }
                }
                response.RezervasyonYapilabilir = false;
                response.YerlesimAyrintilari = [];
                return response;
            }
            else
            {
                foreach (Vagon vagon in rezervasyonReq.Tren.Vagonlar)
                {
                    if (IsVagonAvailable(vagon))
                    {
                        if (AvailableVagonCapacity(vagon) >= rezervasyonSayisi)
                        {
                            yerlesimAyrintiList.Add(new YerlesimAyrinti(vagon.Ad, rezervasyonSayisi));
                            response.RezervasyonYapilabilir = true;
                            response.YerlesimAyrintilari = yerlesimAyrintiList;
                            return response;
                        }
                        else
                        {
                            yerlesimAyrintiList.Add(new YerlesimAyrinti(vagon.Ad, AvailableVagonCapacity(vagon)));
                            rezervasyonSayisi -= AvailableVagonCapacity(vagon);
                        }
                    }
                }
                response.RezervasyonYapilabilir = false;
                response.YerlesimAyrintilari = [];
                return response;
            }
        }

        [NonAction]
        public bool IsVagonAvailable(Vagon vagon)
        {
            return (int)Math.Round((vagon.Kapasite * 70) / 100.0 , MidpointRounding.AwayFromZero) > vagon.DoluKoltukAdet;
        }

        [NonAction]
        public int AvailableVagonCapacity(Vagon vagon) 
        { 
            return (int)Math.Round((vagon.Kapasite * 70) / 100.0 , MidpointRounding.AwayFromZero) - vagon.DoluKoltukAdet;
        }

    }
}
