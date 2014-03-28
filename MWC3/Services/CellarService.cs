using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MWC3.Services
{
    using MWC3.DAL;
    using MWC3.Models;

    public class CellarService
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        public IEnumerable<CellarWineViewModel> GetWinesByUser(string userId)
        {
            var list = this.db.Transactions.Where(t => t.UserId == userId).ToList();
            var wineList = new List<CellarWineViewModel>();
            // filter list
            foreach (var transaction in list)
            {
                var wine = wineList.FirstOrDefault(x => x.WineId == transaction.WineId && x.Year == transaction.Year);
                // if wine does not exist in exportlist, add wine
                // else add stock
                if (wine == null)
                {
                    wineList.Add(this.Map(new CellarWineViewModel(), transaction));
                }
                else
                {
                    wine.Stock = wine.Stock + transaction.Quantity * transaction.TransactionType.Multiplier;
                }
            }

            return wineList;
        }

        private CellarWineViewModel Map(CellarWineViewModel wine, Transaction transaction)
        {
            wine.WineId = transaction.WineId;
            wine.WineName = transaction.Wine.Name;
            wine.Country = transaction.Wine.Country.Name;
            wine.CountryId = transaction.Wine.CountryId;
            wine.Region = transaction.Wine.Region.Name;
            wine.RegionId = transaction.Wine.RegionId;
            wine.Stock = transaction.Quantity * transaction.TransactionType.Multiplier;
            wine.Qualification = transaction.Wine.Qualification.Name;
            wine.QualificationId = transaction.Wine.QualificationId;
            wine.ProducerName = transaction.Wine.Business.Name;
            wine.ProducerId = transaction.Wine.BusinessId;
            wine.ProducerCity = transaction.Wine.Business.City;
            wine.Year = transaction.Year;
            wine.Alcohol = transaction.Alcohol;

            return wine;
        }
    }
}