using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace VentaTicketsUnicornio.Models
{
    [Table("Catalogos")]
    [DisplayColumn("Nombre")]
    public class Catalogo
    {
        public Catalogo()
        {

        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCatalogo { get; set; }

        [Required(ErrorMessage = "Debe colocar este dato")]
        [DisplayName("Pelicula")]
        public string Nombre { get; set; }

        public string Genero { get; set; }

        [Required(ErrorMessage = "Debe colocar este dato")]
        [DataType(DataType.Currency)]
        public Decimal Precio { get; set; }

        [DataType(DataType.Time)]
        public DateTime HoraInicio { get; set; }

        [DataType(DataType.Time)]
        public DateTime HoraFin { get; set; }

        [Display(Name = "Asientos Disponibles")]
        public int Asientos { get; set; }

        public ICollection<Venta> Ventas { get; set; }
    }

    public class Venta
    {
        public Venta()
        {

        }        

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Ticket No.")]
        [ScaffoldColumn(true)]
        public int IdVenta { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [ReadOnly(true)]
        public DateTime Fecha { get; set; }

        [ForeignKey("Catalogos")]
        [Display(Name ="Pelicula")]
        public int IdCatalogo { get; set; }

        [Required]
        public int Asientos { get; set; }

        [DisplayName("Tipo de Pago")]
        [DataType(DataType.Text)]
        public TipoPago TipoPago { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name ="Monto a Cobrar")]
        [ReadOnly(true)]
        public double Cobrado { get; set; }
    
        [ReadOnly(true)]
        public string Empleado { get; set; }
    
        public virtual Catalogo Catalogos { get; set; }
    }

    public enum TipoPago
    {
        Efectivo, Tarjeta
    }

    class TicketDBContext : DbContext
    {
        public TicketDBContext() : base("TicketDB")
        {
            Database.SetInitializer<TicketDBContext>(new DropCreateDatabaseIfModelChanges<TicketDBContext>());
        }

        public DbSet<Catalogo> Catalogos { get; set; }
        public DbSet<Venta> Ventas { get; set; }
    }
}