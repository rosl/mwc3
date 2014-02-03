namespace MWC3.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    using MWC3.Models;

    using WebGrease.Css.Extensions;

    public class DbInitializer : System.Data.Entity.CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            AddCountries(context);
            AddLanguageInfo(context);
            AddRegions(context);
            AddGrapes(context);
            AddGrapeColors(context);
            AddBottleTypes(context);

            //Roles.CreateRole("Administrator");
            //Roles.CreateRole("User");

            //Membership.CreateUser("robert", "mkhmeegls");
            //Roles.AddUserToRole("robert", "Administrator");
        }

        private static void AddGrapeColors(ApplicationDbContext context)
        {
            var grapeColors = new List<GrapeColor>
                              {
                                  new GrapeColor{ColorId = 1, Name = "White", LanguageCode = "en"},
                                  new GrapeColor{ColorId = 2, Name = "Red", LanguageCode = "en"},
                                  new GrapeColor{ColorId = 1, Name = "Wit", LanguageCode = "nl"},
                                  new GrapeColor{ColorId = 2, Name = "Rood", LanguageCode = "nl"},
                                  new GrapeColor{ColorId = 1, Name = "Blanc", LanguageCode = "fr"},
                                  new GrapeColor{ColorId = 2, Name = "Rouge", LanguageCode = "fr"}
                              };
            grapeColors.ForEach(g => context.GrapeColors.Add(g));
            context.SaveChanges();
        }

        private static void AddCountries(ApplicationDbContext context)
        {
            var countries = new List<Country>
                            {
                                new Country { Name = "France", AddedBy = "Application", Timestamp = DateTime.Now, LanguageCode = "en", ParentId = 0 },
                                new Country { Name = "Germany", AddedBy = "Application", Timestamp = DateTime.Now, LanguageCode = "en", ParentId = 0 },
                                new Country { Name = "The Netherlands", AddedBy = "Application", Timestamp = DateTime.Now, LanguageCode = "en", ParentId = 0 },
                                new Country { Name = "Spain", AddedBy = "Application", Timestamp = DateTime.Now, LanguageCode = "en", ParentId = 0 },
                                new Country { Name = "Italy", AddedBy = "Application", Timestamp = DateTime.Now, LanguageCode = "en", ParentId = 0 },
                                new Country { Name = "Austria", AddedBy = "Application", Timestamp = DateTime.Now, LanguageCode = "en", ParentId = 0 },
                                new Country { Name = "Switzerland", AddedBy = "Application", Timestamp = DateTime.Now, LanguageCode = "en", ParentId = 0 },
                                new Country { Name = "Argentina", AddedBy = "Application", Timestamp = DateTime.Now, LanguageCode = "en", ParentId = 0 },
                                new Country { Name = "Chile", AddedBy = "Application", Timestamp = DateTime.Now, LanguageCode = "en", ParentId = 0 },
                                new Country { Name = "Brasil", AddedBy = "Application", Timestamp = DateTime.Now, LanguageCode = "en", ParentId = 0 },
                                new Country { Name = "Slovenia", AddedBy = "Application", Timestamp = DateTime.Now, LanguageCode = "en", ParentId = 0 },
                                new Country { Name = "Bulgaria", AddedBy = "Application", Timestamp = DateTime.Now, LanguageCode = "en", ParentId = 0 }
                            };
            countries.ForEach(g => context.Countries.Add(g));
            context.SaveChanges();

            countries = new List<Country>
                            {
                                new Country { Name = "Frankrijk", AddedBy = "Application", Timestamp = DateTime.Now, LanguageCode = "nl", ParentId = context.Countries.First(c=>c.Name == "France").Id },
                                new Country { Name = "Duitsland", AddedBy = "Application", Timestamp = DateTime.Now, LanguageCode = "nl", ParentId = context.Countries.First(c=>c.Name == "Germany").Id }
                            };
            countries.ForEach(g => context.Countries.Add(g));
            context.SaveChanges();

        }

        private static void AddGrapes(ApplicationDbContext context)
        {
            var grapes = new List<Grape>
                         {
                             new Grape {AddedBy = "Application",ColorId = 1,Name = "Chardonnay",ParentId = 0,TimeStamp = DateTime.Now},
                             new Grape {AddedBy = "Application",ColorId = 1,Name = "Sauvignon Blanc",ParentId = 0,TimeStamp = DateTime.Now},
                             new Grape {AddedBy = "Application",ColorId = 1,Name = "Pinot Gris",ParentId = 0,TimeStamp = DateTime.Now},
                             new Grape {AddedBy = "Application",ColorId = 1,Name = "Pinot Blanc",ParentId = 0,TimeStamp = DateTime.Now},
                             new Grape {AddedBy = "Application",ColorId = 1,Name = "Riesling",ParentId = 0,TimeStamp = DateTime.Now},
                             new Grape {AddedBy = "Application",ColorId = 1,Name = "Viognier",ParentId = 0,TimeStamp = DateTime.Now},
                             new Grape {AddedBy = "Application",ColorId = 1,Name = "Chenin",ParentId = 0,TimeStamp = DateTime.Now},
                             new Grape {AddedBy = "Application",ColorId = 1,Name = "Gewürztraminer",ParentId = 0,TimeStamp = DateTime.Now},
                             new Grape {AddedBy = "Application",ColorId = 1,Name = "Sylvaner",ParentId = 0,TimeStamp = DateTime.Now},
                             new Grape {AddedBy = "Application",ColorId = 2,Name = "Merlot",ParentId = 0,TimeStamp = DateTime.Now},
                             new Grape {AddedBy = "Application",ColorId = 2,Name = "Cabernet Sauvignon",ParentId = 0,TimeStamp = DateTime.Now},
                             new Grape {AddedBy = "Application",ColorId = 2,Name = "Pinot Noir",ParentId = 0,TimeStamp = DateTime.Now},
                             new Grape {AddedBy = "Application",ColorId = 2,Name = "Malbec",ParentId = 0,TimeStamp = DateTime.Now},
                             new Grape {AddedBy = "Application",ColorId = 2,Name = "Carmenère",ParentId = 0,TimeStamp = DateTime.Now},
                             new Grape {AddedBy = "Application",ColorId = 2,Name = "Grenache Noir",ParentId = 0,TimeStamp = DateTime.Now},
                             new Grape {AddedBy = "Application",ColorId = 2,Name = "Barbera",ParentId = 0,TimeStamp = DateTime.Now},
                             new Grape {AddedBy = "Application",ColorId = 2,Name = "Blaufränkisch",ParentId = 0,TimeStamp = DateTime.Now},
                             new Grape {AddedBy = "Application",ColorId = 1,Name = "Sylvaner",ParentId = 0,TimeStamp = DateTime.Now},
                             new Grape {AddedBy = "Application",ColorId = 1,Name = "Müller Thurgau",ParentId = 0,TimeStamp = DateTime.Now},
                             new Grape {AddedBy = "Application",ColorId = 1,Name = "Chasselas",ParentId = 0,TimeStamp = DateTime.Now},
                             new Grape {AddedBy = "Application",ColorId = 1,Name = "Sémillon",ParentId = 0,TimeStamp = DateTime.Now},
                             new Grape {AddedBy = "Application",ColorId = 2,Name = "Cabernet Franc",ParentId = 0,TimeStamp = DateTime.Now},
                             new Grape {AddedBy = "Application",ColorId = 1,Name = "Grüner Veltliner",ParentId = 0,TimeStamp = DateTime.Now},
                             new Grape {AddedBy = "Application",ColorId = 2,Name = "Gamay",ParentId = 0,TimeStamp = DateTime.Now},
                             new Grape {AddedBy = "Application",ColorId = 2,Name = "Carignan",ParentId = 0,TimeStamp = DateTime.Now},
                             new Grape {AddedBy = "Application",ColorId = 2,Name = "Cinsault",ParentId = 0,TimeStamp = DateTime.Now},
                             new Grape {AddedBy = "Application",ColorId = 2,Name = "Pinotage",ParentId = 0,TimeStamp = DateTime.Now},
                             new Grape {AddedBy = "Application",ColorId = 2,Name = "Syrah",ParentId = 0,TimeStamp = DateTime.Now},
                             new Grape {AddedBy = "Application",ColorId = 2,Name = "Tempranillo",ParentId = 0,TimeStamp = DateTime.Now},
                             new Grape {AddedBy = "Application",ColorId = 2,Name = "Dornfelder",ParentId = 0,TimeStamp = DateTime.Now},
                             new Grape {AddedBy = "Application",ColorId = 2,Name = "Schiava Grossa",ParentId = 0,TimeStamp = DateTime.Now},
                             new Grape {AddedBy = "Application",ColorId = 2,Name = "Pinot Meunier",ParentId = 0,TimeStamp = DateTime.Now},
                             new Grape {AddedBy = "Application",ColorId = 2,Name = "Mourvèdre",ParentId = 0,TimeStamp = DateTime.Now},
                             new Grape {AddedBy = "Application",ColorId = 2,Name = "Nebbiolo",ParentId = 0,TimeStamp = DateTime.Now},
                             new Grape {AddedBy = "Application",ColorId = 2,Name = "Nero d'Avola",ParentId = 0,TimeStamp = DateTime.Now},
                             new Grape {AddedBy = "Application",ColorId = 2,Name = "Petit Verdot",ParentId = 0,TimeStamp = DateTime.Now},
                             new Grape {AddedBy = "Application",ColorId = 2,Name = "Portugieser",ParentId = 0,TimeStamp = DateTime.Now},
                             new Grape {AddedBy = "Application",ColorId = 2,Name = "Regent",ParentId = 0,TimeStamp = DateTime.Now},
                             new Grape {AddedBy = "Application",ColorId = 2,Name = "Sangiovese",ParentId = 0,TimeStamp = DateTime.Now},
                             new Grape {AddedBy = "Application",ColorId = 2,Name = "Zinfandel",ParentId = 0,TimeStamp = DateTime.Now},
                             new Grape {AddedBy = "Application",ColorId = 2,Name = "Zweigelt",ParentId = 0,TimeStamp = DateTime.Now},
                             new Grape {AddedBy = "Application",ColorId = 1,Name = "Aligoté",ParentId = 0,TimeStamp = DateTime.Now},
                             new Grape {AddedBy = "Application",ColorId = 1,Name = "Alvarinho",ParentId = 0,TimeStamp = DateTime.Now},
                             new Grape {AddedBy = "Application",ColorId = 1,Name = "Auxerrois",ParentId = 0,TimeStamp = DateTime.Now},
                             new Grape {AddedBy = "Application",ColorId = 1,Name = "Chenin Blanc",ParentId = 0,TimeStamp = DateTime.Now},
                             new Grape {AddedBy = "Application",ColorId = 1,Name = "Clairette",ParentId = 0,TimeStamp = DateTime.Now},
                             new Grape {AddedBy = "Application",ColorId = 1,Name = "Colombard",ParentId = 0,TimeStamp = DateTime.Now},
                             new Grape {AddedBy = "Application",ColorId = 1,Name = "Gros Manseng",ParentId = 0,TimeStamp = DateTime.Now},
                             new Grape {AddedBy = "Application",ColorId = 1,Name = "Marsanne",ParentId = 0,TimeStamp = DateTime.Now},
                             new Grape {AddedBy = "Application",ColorId = 1,Name = "Muscat",ParentId = 0,TimeStamp = DateTime.Now},
                             new Grape {AddedBy = "Application",ColorId = 1,Name = "Prosecco",ParentId = 0,TimeStamp = DateTime.Now},
                             new Grape {AddedBy = "Application",ColorId = 1,Name = "Scheurebe",ParentId = 0,TimeStamp = DateTime.Now},
                             new Grape {AddedBy = "Application",ColorId = 1,Name = "Trebbiano",ParentId = 0,TimeStamp = DateTime.Now},
                             new Grape {AddedBy = "Application",ColorId = 1,Name = "Verdicchio",ParentId = 0,TimeStamp = DateTime.Now},
                             new Grape {AddedBy = "Application",ColorId = 1,Name = "Barbaresco",ParentId = 0,TimeStamp = DateTime.Now}
                         };
            grapes.ForEach(g => context.Grapes.Add(g));
            context.SaveChanges();

            grapes = new List<Grape>
                     {
                         new Grape { AddedBy = "Application", ColorId = context.Grapes.First(g=>g.Name == "Pinot Gris").ColorId, Name = "Grauburgunder", ParentId = context.Grapes.First(g=>g.Name == "Pinot Gris").Id, TimeStamp = DateTime.Now },
                         new Grape { AddedBy = "Application", ColorId = context.Grapes.First(g=>g.Name == "Pinot Gris").ColorId, Name = "Pinot Grigio", ParentId = context.Grapes.First(g=>g.Name == "Pinot Gris").Id, TimeStamp = DateTime.Now },
                         new Grape { AddedBy = "Application", ColorId = context.Grapes.First(g=>g.Name == "Malbec").ColorId, Name = "Côt", ParentId = context.Grapes.First(g=>g.Name == "Malbec").Id, TimeStamp = DateTime.Now },
                         new Grape { AddedBy = "Application", ColorId = context.Grapes.First(g=>g.Name == "Pinot Blanc").ColorId, Name = "Weissburgunder", ParentId = context.Grapes.First(g=>g.Name == "Pinot Blanc").Id, TimeStamp = DateTime.Now },
                         new Grape { AddedBy = "Application", ColorId = context.Grapes.First(g=>g.Name == "Pinot Noir").ColorId, Name = "Blauburgunder", ParentId = context.Grapes.First(g=>g.Name == "Pinot Noir").Id, TimeStamp = DateTime.Now },
                         new Grape { AddedBy = "Application", ColorId = context.Grapes.First(g=>g.Name == "Malbec").ColorId, Name = "Côt", ParentId = context.Grapes.First(g=>g.Name == "Malbec").Id, TimeStamp = DateTime.Now },
                     };
            grapes.ForEach(g => context.Grapes.Add(g));
            context.SaveChanges();



        }

        private static void AddLanguageInfo(ApplicationDbContext context)
        {
            var list = new List<LanguageInfo>();

            CultureInfo[] cinfo = CultureInfo.GetCultures(CultureTypes.AllCultures);
            foreach (CultureInfo cul in cinfo)
            {
                if (cul.Name != string.Empty)
                {
                    var languageCode = string.Empty;
                    var languageName = cul.DisplayName;
                    var enabled = false;

                    if (cul.Name.Split('-').Length <= 1)
                    {
                        languageCode = cul.Name.Split('-')[0];
                    }

                    if (languageCode == "en" || languageCode == "nl")
                    {
                        enabled = true;
                    }

                    var languageInfo = new LanguageInfo { LanguageCode = languageCode, LanguageName = languageName, Enabled = enabled };
                    list.Add(languageInfo);
                }
            }

            list.Where(r => r.LanguageCode != string.Empty).ForEach(r => context.LanguageInfo.Add(r));
            context.SaveChanges();

        }

        private static void AddRegions(ApplicationDbContext context)
        {
            var regions = new List<Region>
                          {
                              new Region
                              {
                                  CountryId = context.Countries.First(c=> c.Name == "France").Id,
                                  Name = "Alsace", AddedBy = "Application", Timestamp = DateTime.Now,
                              },
                                 new Region
                              {
                                  CountryId = context.Countries.First(c=> c.Name == "France").Id,
                                  Name = "Bourgogne", AddedBy = "Application", Timestamp = DateTime.Now,
                              },
                              new Region
                              {
                                  CountryId = context.Countries.First(c=> c.Name == "France").Id,
                                  Name = "Vallée de la Loire", AddedBy = "Application", Timestamp = DateTime.Now,
                              },
                              new Region
                              {
                                  CountryId = context.Countries.First(c=> c.Name == "France").Id,
                                  Name = "Bordeaux", AddedBy = "Application", Timestamp = DateTime.Now,
                              },
                              new Region
                              {
                                  CountryId = context.Countries.First(c=> c.Name == "France").Id,
                                  Name = "Lorraine", AddedBy = "Application", Timestamp = DateTime.Now,
                              },
                              new Region
                              {
                                  CountryId = context.Countries.First(c=> c.Name == "France").Id,
                                  Name = "Champagne", AddedBy = "Application", Timestamp = DateTime.Now,
                              },
                              new Region
                              {
                                  CountryId = context.Countries.First(c=> c.Name == "France").Id,
                                  Name = "Beaujolais", AddedBy = "Application", Timestamp = DateTime.Now,
                              },
                              new Region
                              {
                                  CountryId = context.Countries.First(c=> c.Name == "France").Id,
                                  Name = "Vallée du Rhone", AddedBy = "Application", Timestamp = DateTime.Now,
                              },
                              new Region
                              {
                                  CountryId = context.Countries.First(c=> c.Name == "France").Id,
                                  Name = "Languedoc", AddedBy = "Application", Timestamp = DateTime.Now,
                              },
                              new Region
                              {
                                  CountryId = context.Countries.First(c=> c.Name == "France").Id,
                                  Name = "Provence", AddedBy = "Application", Timestamp = DateTime.Now,
                              },
                              new Region
                              {
                                  CountryId = context.Countries.First(c=> c.Name == "France").Id,
                                  Name = "Sud-Ouest", AddedBy = "Application", Timestamp = DateTime.Now,
                              },
                              new Region
                              {
                                  CountryId = context.Countries.First(c=> c.Name == "France").Id,
                                  Name = "Centre", AddedBy = "Application", Timestamp = DateTime.Now,
                              },
                              new Region
                              {
                                  CountryId = context.Countries.First(c=> c.Name == "France").Id,
                                  Name = "Savoie", AddedBy = "Application", Timestamp = DateTime.Now,
                              },
                              new Region
                              {
                                  CountryId = context.Countries.First(c=> c.Name == "France").Id,
                                  Name = "Corsica", AddedBy = "Application", Timestamp = DateTime.Now,
                              },
                              new Region
                              {
                                  CountryId = context.Countries.First(c=> c.Name == "France").Id,
                                  Name = "Jura", AddedBy = "Application", Timestamp = DateTime.Now,
                              },
                              new Region
                              {
                                  CountryId = context.Countries.First(c=> c.Name == "Germany").Id,
                                  Name = "Ahr", AddedBy = "Application", Timestamp = DateTime.Now,
                              },
                              new Region
                              {
                                  CountryId = context.Countries.First(c=> c.Name == "Germany").Id,
                                  Name = "Mittelrhein", AddedBy = "Application", Timestamp = DateTime.Now,
                              },
                              new Region
                              {
                                  CountryId = context.Countries.First(c=> c.Name == "Germany").Id,
                                  Name = "Mosel-Saar-Ruwer", AddedBy = "Application", Timestamp = DateTime.Now,
                              },
                              new Region
                              {
                                  CountryId = context.Countries.First(c=> c.Name == "Germany").Id,
                                  Name = "Nahe", AddedBy = "Application", Timestamp = DateTime.Now,
                              },
                              new Region
                              {
                                  CountryId = context.Countries.First(c=> c.Name == "Germany").Id,
                                  Name = "Rheingau", AddedBy = "Application", Timestamp = DateTime.Now,
                              },
                              new Region
                              {
                                  CountryId = context.Countries.First(c=> c.Name == "Germany").Id,
                                  Name = "Franken", AddedBy = "Application", Timestamp = DateTime.Now,
                              },
                              new Region
                              {
                                  CountryId = context.Countries.First(c=> c.Name == "Germany").Id,
                                  Name = "Hessiche Bergstrasse", AddedBy = "Application", Timestamp = DateTime.Now,
                              },
                              new Region
                              {
                                  CountryId = context.Countries.First(c=> c.Name == "Germany").Id,
                                  Name = "Rheinhessen", AddedBy = "Application", Timestamp = DateTime.Now,
                              },
                              new Region
                              {
                                  CountryId = context.Countries.First(c=> c.Name == "Germany").Id,
                                  Name = "Pfalz /Rheinpfalz", AddedBy = "Application", Timestamp = DateTime.Now,
                              },
                              new Region
                              {
                                  CountryId = context.Countries.First(c=> c.Name == "Germany").Id,
                                  Name = "Baden", AddedBy = "Application", Timestamp = DateTime.Now,
                              },
                              new Region
                              {
                                  CountryId = context.Countries.First(c=> c.Name == "Germany").Id,
                                  Name = "Württemberg", AddedBy = "Application", Timestamp = DateTime.Now,
                              },
                              new Region
                              {
                                  CountryId = context.Countries.First(c=> c.Name == "Germany").Id,
                                  Name = "Saale / Unstrut", AddedBy = "Application", Timestamp = DateTime.Now,
                              },
                              new Region
                              {
                                  CountryId = context.Countries.First(c=> c.Name == "Germany").Id,
                                  Name = "Sachsen", AddedBy = "Application", Timestamp = DateTime.Now,
                              }
                          };
            regions.ForEach(r => context.Regions.Add(r));
            context.SaveChanges();
        }

        private static void AddBottleTypes(ApplicationDbContext context)
        {
            var list = new List<BottleType>
                       {
                           new BottleType{Name = "Quart", Capacity = 200, AddedBy = "Application", Timestamp = DateTime.Now},
                           new BottleType{Name = "Demi", Capacity = 375, AddedBy = "Application", Timestamp = DateTime.Now},
                           new BottleType{Name = "Bouteille", Capacity = 750, AddedBy = "Application", Timestamp = DateTime.Now},
                           new BottleType{Name = "Magnum", Capacity = 1500, AddedBy = "Application", Timestamp = DateTime.Now},
                           new BottleType{Name = "Double magnum", Capacity = 3000, AddedBy = "Application", Timestamp = DateTime.Now},
                           new BottleType{Name = "Jéroboam (Champagne)", Capacity = 3000, AddedBy = "Application", Timestamp = DateTime.Now},
                           new BottleType{Name = "Jéroboam (Bordelais)", Capacity = 4500, AddedBy = "Application", Timestamp = DateTime.Now},
                           new BottleType{Name = "Mathusalem (Champagne)", Capacity = 6000, AddedBy = "Application", Timestamp = DateTime.Now},
                           new BottleType{Name = "Mathusalem (Bordelais)", Capacity = 9000, AddedBy = "Application", Timestamp = DateTime.Now},
                           new BottleType{Name = "Salmamzar (Champagne)", Capacity = 9000, AddedBy = "Application", Timestamp = DateTime.Now},
                           new BottleType{Name = "Balthazar", Capacity = 12000, AddedBy = "Application", Timestamp = DateTime.Now},
                           new BottleType{Name = "Nebukadnezar", Capacity = 15000, AddedBy = "Application", Timestamp = DateTime.Now},
                       };
            list.ForEach(g => context.BottleTypes.Add(g));
            context.SaveChanges();
        }
    }

}