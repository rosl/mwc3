﻿namespace MWC3.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using MWC3.DAL;
    using MWC3.Helpers;
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
                    wineList.Add(Map(new CellarWineViewModel(), transaction));
                }
                else
                {
                    wine.Stock = wine.Stock + transaction.Quantity * transaction.TransactionType.Multiplier;
                    wine.TotalPrice = wine.TotalPrice
                                      + (transaction.Quantity * transaction.Price
                                         * transaction.TransactionType.Multiplier);
                }
            }

            foreach (var cellarWineViewModel in wineList)
            {
                if (cellarWineViewModel.TotalPrice < 0)
                {
                    cellarWineViewModel.TotalPrice = 0;
                }
            }

            return wineList;
        }

        public IEnumerable<CellarTransactionViewModel> GetTransactionsByUserId(string userId)
        {
            var list = this.db.Transactions.Where(t => t.UserId == userId).ToList();
            return list.Select(transaction => Map(new CellarTransactionViewModel(), transaction)).ToList();
        }

        public IEnumerable<CellarTransactionViewModel> GetTransactionsInByUserId(string userId)
        {
            var list = this.db.Transactions.Where(t => t.UserId == userId && t.TransactionType.Multiplier > 0).ToList();
            return this.TransLateTransaction(list.Select(transaction => Map(new CellarTransactionViewModel(), transaction)).ToList());
        }

        public IEnumerable<CellarTransactionViewModel> GetTransactionsOutByUserId(string userId)
        {
            var list = this.db.Transactions.Where(t => t.UserId == userId && t.TransactionType.Multiplier <0).ToList();
            return this.TransLateTransaction(list.Select(transaction => Map(new CellarTransactionViewModel(), transaction)).ToList());
        }

        private IEnumerable<CellarTransactionViewModel> TransLateTransaction(List<CellarTransactionViewModel> list)
        {
            // get language
            var languageCode = LanguageHelper.GetLanguageFromCookie();
            // get translations
            var languageList = this.db.TransactionTypes.Where(g => g.ParentId > 0 && g.LanguageCode.ToLower() == languageCode.ToLower()).ToList();

            // translate
            foreach (var item in languageList)
            {
                var foundList = list.Where(c => item != null && c.TransactionTypeId == item.ParentId);
                foreach (var cellarTransactionViewModel in foundList)
                {
                    cellarTransactionViewModel.TransactionTypeName = item.Name;
                }
            }
            return list;
        }

        private static CellarWineViewModel Map(CellarWineViewModel wine, Transaction transaction)
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
            wine.TotalPrice = transaction.Quantity * transaction.Price;
            
            return wine;
        }

        private static CellarTransactionViewModel Map(CellarTransactionViewModel cellarTransaction, Transaction transaction)
        {
            cellarTransaction.WineId = transaction.WineId;
            cellarTransaction.WineName = transaction.Wine.Name;
            cellarTransaction.Country = transaction.Wine.Country.Name;
            cellarTransaction.CountryId = transaction.Wine.CountryId;
            cellarTransaction.Region = transaction.Wine.Region.Name;
            cellarTransaction.RegionId = transaction.Wine.RegionId;
            cellarTransaction.Qualification = transaction.Wine.Qualification.Name;
            cellarTransaction.QualificationId = transaction.Wine.QualificationId;
            cellarTransaction.ProducerName = transaction.Wine.Business.Name;
            cellarTransaction.ProducerId = transaction.Wine.BusinessId;
            cellarTransaction.ProducerCity = transaction.Wine.Business.City;
            cellarTransaction.ProducerCountry = transaction.Wine.Business.Country.Name;
            cellarTransaction.Year = transaction.Year;
            cellarTransaction.Alcohol = transaction.Alcohol;
            cellarTransaction.BottleTypeId = transaction.BottleTypeId;
            cellarTransaction.BottleTypeName = transaction.BottleType.Name;
            cellarTransaction.DistributorId = transaction.BusinessId;
            cellarTransaction.DistributorName = transaction.Business.Name;
            if (transaction.Business.City != null)
            {
                cellarTransaction.DistributorCity = transaction.Business.City;
            }
            if (transaction.Business.Country != null)
            {
                cellarTransaction.DistributorCountry = transaction.Business.Country.Name;
            }
            cellarTransaction.Date = transaction.Date.Date;
            cellarTransaction.TransactionTypeId = transaction.TransactionTypeId;
            cellarTransaction.TransactionTypeName = transaction.TransactionType.Name;
            cellarTransaction.Quantity = transaction.Quantity;
            cellarTransaction.Price = transaction.Price;
            cellarTransaction.TotalPrice = transaction.Quantity * transaction.Price;

            return cellarTransaction;
        }
    }
}