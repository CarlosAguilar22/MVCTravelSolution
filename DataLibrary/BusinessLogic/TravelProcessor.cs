using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DataLibrary.Models;
using DataLibrary.DataAccess;
using System.Data.Entity;

namespace DataLibrary.BusinessLogic
{
    public class TravelProcessor : IProcessor
    {
        //public static int CreateTavel(int agencyId,string titular, DateTime startDate, DateTime endDate, double totalCost, 
        //                        string description, string notes)

        private Travel ConvertTravelModelToEntityTravel(Object travelModel)
        {
            try
            {
                //ObjectType
                Type travelModelType = travelModel.GetType();
                PropertyInfo[] propTravelModel = travelModelType.GetProperties();


                // TravelType
                Travel travel = new Travel();
                Type travelType = travel.GetType();
                PropertyInfo[] propTravel = travelType.GetProperties();

                foreach (PropertyInfo currentTravelProp in propTravel)
                {
                    PropertyInfo currentTravelModelProp = propTravelModel.Where(x => x.Name == currentTravelProp.Name
                                            && x.GetType() == currentTravelProp.GetType()).FirstOrDefault();

                    if (currentTravelModelProp != null)
                    {
                        currentTravelProp.SetValue(travel, currentTravelModelProp.GetValue(travelModel));
                    }
                }
                return travel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Create(Object travelModel)
        {
            try
            {
                using (var db = new TravelAgenciesContext())
                {
                    Travel travel = this.ConvertTravelModelToEntityTravel(travelModel);
                    db.Travel.Add(travel);
                    db.SaveChanges();
                    return 1;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Object> Load()
        {
            List<Travel> lstTravels = new List<Travel>();

            using (var db = new TravelAgenciesContext())
            {
                foreach (var row in db.Travel)
                {
                    lstTravels.Add(new Travel
                    {
                        Id = row.Id,
                        AgencyID = row.AgencyID,
                        Titular = row.Titular,
                        StartDate = row.StartDate,
                        EndDate = row.EndDate,
                        Destination = row.Destination,
                        Hotel = row.Hotel,
                        TotalCost = row.TotalCost,
                        Description = row.Description,
                        Notes = row.Notes
                    });
                }
            }
            return lstTravels.ToList<Object>();
        }

        public Object GetModelById(int Id)
        {
            Travel travel = null;
            using (var db = new TravelAgenciesContext())
            {
                travel = db.Travel.Find(Id);
            }

            return travel;
        }

        public int Edit(Object travelModel, int Id)
        {
            try
            {
                using (var db = new TravelAgenciesContext())
                {
                    // Get current Travel by Id          
                    Travel currentTravel = GetModelById(Id) as Travel;

                    // Convert the New MVC Travel to Entity Travel 
                    Travel newTravel = this.ConvertTravelModelToEntityTravel(travelModel);

                    db.Travel.Attach(newTravel);
                    db.Entry(newTravel).State = EntityState.Modified;
                    db.SaveChanges();
                    return 1;
                }
            }
            catch (Exception)
            {
                throw;
            }
            
        }
    }
}
