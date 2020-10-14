using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DrugsContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        public DrugsContext() : base("name=DrugsContext4")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //if add this row: table name is like class name, without "s" at end
            // modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //this is table name of this object
           // modelBuilder.Entity<Medicine>().ToTable("Drugs");
        }

        public DbSet<BE.Medicine> Medicines { get; set; }
        public DbSet<BE.Patient> Patients { get; set; }
        public DbSet<BE.Doctor> Doctors { get; set; }
        public DbSet<BE.Prescription> Prescriptions { get; set; }
        public DbSet<BE.CronicalDisease> CronicalDiseases { get; set; }

    }
}
