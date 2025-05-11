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

           modelBuilder.Entity<FlowStepDependency>()
                .HasOne(d => d.FlowStep)
                .WithMany(s => s.DependedBy)
                .HasForeignKey(d => d.FlowStepId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FlowStepDependency>()
                .HasOne(d => d.DependsOnFlowStep)
                .WithMany()
                .HasForeignKey(d => d.DependsOnFlowStepId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Field>().HasData(
                new Field { Id = "F-0001", Name = "Primer nombre" },
                new Field { Id = "F-0002", Name = "Segundo nombre" },
                new Field { Id = "F-0003", Name = "Primer apellido" },
                new Field { Id = "F-0004", Name = "Segundo apellido" },
                new Field { Id = "F-0005", Name = "Tipo de documento" },
                new Field { Id = "F-0006", Name = "Número de documento" }
            );

            modelBuilder.Entity<Step>().HasData(
                new Step { Id = "STP-0001", Name = "Registro de usuario" },
                new Step { Id = "STP-0002", Name = "Formulario de datos personales" },
                new Step { Id = "STP-0003", Name = "Confirmación de correo" }
            );

            modelBuilder.Entity<Flow>().HasData(
                new Flow { Id = "FLW-0001", Name = "Solicitud de producto financiero" }
            );

            modelBuilder.Entity<FlowStep>().HasData(
                new FlowStep
                {
                    Id = "FST-0001",
                    FlowId = "FLW-0001",
                    StepId = "STP-0001",
                    Order = 1
                },
                new FlowStep
                {
                    Id = "FST-0002",
                    FlowId = "FLW-0001",
                    StepId = "STP-0002",
                    Order = 2
                },
                new FlowStep
                {
                    Id = "FST-0003",
                    FlowId = "FLW-0001",
                    StepId = "STP-0003",
                    Order = 3
                }
            );
            modelBuilder.Entity<FlowStepField>().HasData(
                new FlowStepField { Id = "FSF-0001", FlowStepId = "FST-0001", FieldId = "F-0005", Direction = DirectionType.Input },
                new FlowStepField { Id = "FSF-0002", FlowStepId = "FST-0001", FieldId = "F-0006", Direction = DirectionType.Input },

                new FlowStepField { Id = "FSF-0003", FlowStepId = "FST-0002", FieldId = "F-0001", Direction = DirectionType.Input },
                new FlowStepField { Id = "FSF-0004", FlowStepId = "FST-0002", FieldId = "F-0003", Direction = DirectionType.Input },

                new FlowStepField { Id = "FSF-0005", FlowStepId = "FST-0003", FieldId = "F-0001", Direction = DirectionType.Output }
            );
            modelBuilder.Entity<FlowStepDependency>().HasData(
                new FlowStepDependency { Id = "1", FlowStepId = "FST-0002", DependsOnFlowStepId = "FST-0001" },
                new FlowStepDependency { Id = "2" ,  FlowStepId = "FST-0003", DependsOnFlowStepId = "FST-0002" }
            );

        }
        public DbSet<Flow> Flows { get; set; }
        public DbSet<Step> Steps { get; set; }
        public DbSet<Field> Fields { get; set; }

        public DbSet<FlowStep> FlowSteps { get; set; }

        public DbSet<FlowStepField> FlowStepFields { get; set; }

        public DbSet<FlowStepDependency> FlowStepDependencies { get; set; }
    }
}
