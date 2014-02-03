namespace MWC3.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Web.Security;

    internal sealed class Configuration : DbMigrationsConfiguration<MWC3.DAL.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "MWC3.DAL.ApplicationDbContext";
        }

        protected override void Seed(DAL.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            //Roles.CreateRole("Administrator");
            //Roles.CreateRole("User");

            //Membership.CreateUser("robert", "mkhmeegls");
            //Roles.AddUserToRole("robert", "Administrator");
        }
    }
}
