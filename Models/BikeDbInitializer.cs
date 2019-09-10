using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using CsvHelper;

namespace RentAndCycleCodeFirst.Models
{
    public class BikeDbInitializer : System.Data.Entity.DropCreateDatabaseAlways<BikeDbContext>
    {
        protected override void Seed(BikeDbContext context)
        {
            // Inspired by https://www.davepaquette.com/archive/2014/03/18/seeding-entity-framework-database-from-csv.aspx
            string resourceName = "RentAndCycleCodeFirst.Models.SeedData.Brand.csv";
            var brands = ReadCsv<Brand>(resourceName);
            brands.ForEach(b => context.Brands.Add(b));
            context.SaveChanges();

            resourceName = "RentAndCycleCodeFirst.Models.SeedData.Company.csv";
            var companies = ReadCsv<Company>(resourceName);
            companies.ForEach(c => context.Companies.Add(c));
            context.SaveChanges();
        }

        private List<T> ReadCsv<T>(string resourceName)
        {
            // Inspired by https://www.davepaquette.com/archive/2014/03/18/seeding-entity-framework-database-from-csv.aspx
            Assembly assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    CsvReader csvReader = new CsvReader(reader);
                    csvReader.Configuration.MissingFieldFound = null;
                    csvReader.Configuration.HeaderValidated = null;
                    csvReader.Configuration.PrepareHeaderForMatch = (string header, int index) => header.ToLower();
                    return csvReader.GetRecords<T>().ToList();
                }
            }
        }
    }
}