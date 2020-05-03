using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace VentaTicketsUnicornio.Models
{
    public class Empleado
    {
        public Empleado()
        {

        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdEmpleado { get; set; }
        [Required]
        [DisplayName("Empleado")]
        public string Usuario { get; set; }
        [Required]
        public string Puesto { get; set; }

        //Para las llaves foraneas.
        public ICollection<Venta> Ventas { get; set; }
    }

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

        //Para las llaves foraneas.
        public ICollection<Venta> Ventas { get; set; }
    }

    public class Venta
    {
        public Venta()
        {
            Fecha = DateTime.Now;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Ticket No.")]
        [ScaffoldColumn(true)]
        public int IdVenta { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime Fecha { get; set; }

        [Required]
        public int Asientos { get; set; }

        [Required]
        [ForeignKey("Catalogos")]
        public int IdCatalogo { get; set; }

        [DisplayName("En Efectivo")]
        public Boolean EnEfectivo { get; set; }
        [DataType(DataType.Currency)]
        public double Cobrado { get; set; }

        [Required]
        [ForeignKey("Empleados")]
        public int IdEmpleado { get; set; }

        //llaves foraneas
        public virtual Empleado Empleados { get; set; }
        public virtual Catalogo Catalogos { get; set; }
    }

    class TicketDBContext : DbContext
    {
        public TicketDBContext() : base("TicketDB")
        {
            Database.SetInitializer<TicketDBContext>(new DropCreateDatabaseIfModelChanges<TicketDBContext>());
        }

        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Catalogo> Catalogos { get; set; }
        public DbSet<Venta> Ventas { get; set; }
    }
}