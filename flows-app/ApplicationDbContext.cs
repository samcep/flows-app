using flows_app.Entities;
using Microsoft.EntityFrameworkCore;

namespace flows_app
{
    public class ApplicationDbContext : DbContext 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions) : base(dbContextOptions)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<FlowStepField>().Property(x => x.Direction).HasDefaultValue(DirectionType.Input);

        }
        public DbSet<Flow> Flows { get; set; }
        public DbSet<Step> Steps { get; set; }
        public DbSet<Field> Fields { get; set; }

        public DbSet<FlowStep> FlowSteps { get; set; }

        public DbSet<FlowStepField> FlowStepFields { get; set; }

        public DbSet<FlowStepDependency> FlowStepDependencies { get; set; }
    }
}
