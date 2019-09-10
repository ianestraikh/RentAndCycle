using System;
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
            Assembly assembly = Assembly.GetExecutingAssembly();
            string resourceName = "RentAndCycleCodeFirst.Models.SeedData.Brand.csv";
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    CsvReader csvReader = new CsvReader(reader);
                    csvReader.Configuration.MissingFieldFound = null;
                    csvReader.Configuration.HeaderValidated = null;
                    var brands = csvReader.GetRecords<Brand>().ToList();
                    brands.ForEach(b => context.Brands.Add(b));
                    context.SaveChanges();
                }
            }
        }
    }
}