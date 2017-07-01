namespace Subvault_Domain.Migrations {
    using Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Subvault_Domain.Concrete.EFDbContext> {
        public Configuration() {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Subvault_Domain.Concrete.EFDbContext context) {
            //Genres
            context.Genres.RemoveRange(context.Genres);

            Genre genre1 = new Genre { Id = 28, Name = "Action" };
            Genre genre2 = new Genre { Id = 18, Name = "Drama" };
            Genre genre3 = new Genre { Id = 878, Name = "Science Fiction" };

            //People
            context.People.RemoveRange(context.People);

            Person person1 = new Director { Id = 366, Name = "James Mangold" };
            Person person2 = new CastMember { Id = 6968, Name = "Hugh Jackman" };
            Person person3 = new CastMember { Id = 2387, Name = "Patrick Stewart" };
            Person person4 = new CastMember { Id = 1464650, Name = "Dafne Keen" };
            Person person5 = new CastMember { Id = 467645, Name = "Boyd Holbrook" };
            Person person6 = new CastMember { Id = 39189, Name = "Stephen Merchant" };

            //Items
            context.Items.RemoveRange(context.Items);

            Item movie1 = new Movie {
                Id = 263115,
                Title = "Logan",
                Description = "In the near future, a weary Logan cares for an ailing Professor X in a hideout on the Mexican border. But Logan's attempts to hide from the world and his legacy are upended when a young mutant arrives, pursued by dark forces.",
                ReleaseDate = new DateTime(2017, 2, 28),
                PosterURL = "/45Y1G5FEgttPAwjTYic6czC9xCn.jpg",
                BackdropURL = "/5pAGnkFYSsFJ99ZxDIYnhQbQFXs.jpg", 
                Genres = new List<Genre> { genre1, genre2, genre3 },
                People = new List<Person> { person1, person2, person3, person4, person5, person6 }
            };
            Item movie2 = new Movie { Id = 341174, Title = "Fifty Shades Darker", PosterURL = "/aybgjbFbn6yUbsgUMnUbwc2jcWd.jpg" };
            Item movie3 = new Movie { Id = 127380, Title = "Finding Dory", PosterURL = "/z09QAf8WbZncbitewNk6lKYMZsh.jpg" };
            Item movie4 = new Movie { Id = 293167, Title = "Kong: Skull Island", PosterURL = "/aoUyphk4nwffrwlZRaOa0eijgpr.jpg" };
            Item movie5 = new Movie { Id = 135397,
                Title = "Jurassic World",
                Description = "Twenty-two years after the events of Jurassic Park, Isla Nublar now features a fully functioning dinosaur theme park, Jurassic World, as originally envisioned by John Hammond.",
                ReleaseDate = new DateTime(2015, 6, 9),
                PosterURL = "/jjBgi2r5cRt36xF6iNUEhzscEcb.jpg",
                BackdropURL = "/dkMD5qlogeRMiEixC4YNPUvax2T.jpg"
            };
            Item movie6 = new Movie { Id = 15206, Title = "The Mother of Tears", PosterURL = "/sGFOggXN12CcSXD01hSAIaEoSgs.jpg" };

            context.Items.AddOrUpdate(movie1);
            context.Items.AddOrUpdate(movie2);
            context.Items.AddOrUpdate(movie3);
            context.Items.AddOrUpdate(movie4);
            context.Items.AddOrUpdate(movie5);
            context.Items.AddOrUpdate(movie6);

            //Users
            context.Users.RemoveRange(context.Users);

            User user1 = new User { Username = "DennisvHest", HashedPassword = "AAK37VVq03vVHjMrd6EdLFnZ2fEnPPX/r43VCmqPWOMKbK5FTDzYZiLx6amni/14cA==", Salt = "wVfjvAMhS9+pDH3HFGaFOw==", EmailAdress = "dennisvhest@hotmail.nl" };

            context.Users.AddOrUpdate(user1);
        }
    }
}
