using Microsoft.EntityFrameworkCore;
using StatAssesment.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StatAssesment.Data
{
    public class StatAssessmentContext : DbContext
    {
        public StatAssessmentContext() : base()
        { }
        //I was going to use this to start writing to a database of what read files were but i think that may be feature creep at the moment. 
        //reading the metadata should be working I expect it to so we'll stick with it for now
        public DbSet<KomarExtract> KomartExtracts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new KomarExtractConfiguration());

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=LocalHost;Initial Catalog=Stat; Integrated Security=True;Encrypt=False;User=admin;");
        }
    }
    
}
