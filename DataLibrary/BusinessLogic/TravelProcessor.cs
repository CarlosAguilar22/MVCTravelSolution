﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DataLibrary.Models;
using DataLibrary.DataAccess;

namespace DataLibrary.BusinessLogic
{
    public class TravelProcessor : IProcessor
    {
        //public static int CreateTavel(int agencyId,string titular, DateTime startDate, DateTime endDate, double totalCost, 
        //                        string description, string notes)

        public int Create(Object travelModel)
        {
            try
            {
                using (var db = new TravelAgenciesContext())
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
 
                    db.Travel.Add(travel);
                    db.SaveChanges();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
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

    }
}
