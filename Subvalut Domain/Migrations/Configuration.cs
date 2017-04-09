namespace Subvault_Domain.Migrations
{
    using Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Subvault_Domain.Concrete.EFDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Subvault_Domain.Concrete.EFDbContext context)
        {
            //Subtitles

            context.Items.RemoveRange(context.Items);

            //Movies
            Item movie1 = new Movie { Id = 263115, Title = "Logan", PosterURL = "/45Y1G5FEgttPAwjTYic6czC9xCn.jpg" };
            Item movie2 = new Movie { Id = 341174, Title = "Fifty Shades Darker", PosterURL = "/aybgjbFbn6yUbsgUMnUbwc2jcWd.jpg" };
            Item movie3 = new Movie { Id = 127380, Title = "Finding Dory", PosterURL = "/z09QAf8WbZncbitewNk6lKYMZsh.jpg" };
            Item movie4 = new Movie { Id = 293167, Title = "Kong: Skull Island", PosterURL = "/aoUyphk4nwffrwlZRaOa0eijgpr.jpg" };
            Item movie5 = new Movie { Id = 135397, Title = "Jurassic World", PosterURL = "/jjBgi2r5cRt36xF6iNUEhzscEcb.jpg" };
            Item movie6 = new Movie { Id = 15206, Title = "The Mother of Tears", PosterURL = "/sGFOggXN12CcSXD01hSAIaEoSgs.jpg" };

            context.Items.AddOrUpdate(movie1);
            context.Items.AddOrUpdate(movie2);
            context.Items.AddOrUpdate(movie3);
            context.Items.AddOrUpdate(movie4);
            context.Items.AddOrUpdate(movie5);
            context.Items.AddOrUpdate(movie6);
        }
    }
}
