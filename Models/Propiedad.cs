using System;

namespace ArriendoPocketApp.Models
{
    public class Propiedad
    {
        public int PropiedadID { get; set; }
        public string NombreInquilino { get; set; }
        public string AliasPropiedad { get; set; }
        public string DireccionPropiedad { get; set; }
        public int NumeroHabitaciones { get; set; }
        public decimal CanonArrendatario { get; set; }
        public bool Disponible { get; set; }
        public int MesesGarantia { get; set; }
        public int NumeroPisos { get; set; }
        public decimal AreaConstruccion { get; set; }
        public string CiudadUbicacion { get; set; }
        public DateTime FechaConstruccion { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
