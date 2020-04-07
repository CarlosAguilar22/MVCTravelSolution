using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCTravels.NET_2017.Models;
using DataLibrary.BusinessLogic;
using DataLibrary.Models;
using DataLibrary.DataAccess;

namespace MVCTravels.NET_2017.Controllers
{
    public class TravelController : Controller
    {
        IProcessor travelProcessor = FactoryProcessor.CreateProcessor(ClientModelType.Travel);

        // GET: Travel
        public ActionResult Index()
        {
            ViewBag.Message = "Listado de viajes";

            List<Object> travelsDB = travelProcessor.Load();
            
            List<TravelModel> travels = new List<TravelModel>();

            foreach (var currentTravelObjDB in travelsDB)
            {
                Travel currentTravelDB = currentTravelObjDB as Travel;
                travels.Add(new TravelModel
                {
                    Id = currentTravelDB.Id, 
                    AgencyID = currentTravelDB.AgencyID,
                    Titular = currentTravelDB.Titular,
                    StartDate = currentTravelDB.StartDate,
                    EndDate = currentTravelDB.EndDate,
                    TotalCost = currentTravelDB.TotalCost,
                    Description = currentTravelDB.Description,
                    Notes = currentTravelDB.Notes
                });
            }

            return View(travels);
        }

        public ActionResult CreateTravel()
        {
            ViewBag.Message = "Generación de un nuevo viaje";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTravel(TravelModel travelModel)
        {            
            if (!ModelState.IsValid)
                return View();

            try
            {
                travelModel.AgencyID = 1;
                travelProcessor.Create(travelModel);               
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al agregar el viaje - " + ex.Message);
                return View();
            }
        }

        public ActionResult EditTravel(int Id)
        {
            ViewBag.Message = "Edición del viaje seleccionado";

            Object travelObjectDB = travelProcessor.GetModelById(Id);
            Travel selectedTravelDB = travelObjectDB as Travel;

            TravelModel travelModel = new TravelModel
            {
                Id = selectedTravelDB.Id,
                AgencyID = selectedTravelDB.AgencyID,
                Titular = selectedTravelDB.Titular,
                StartDate = selectedTravelDB.StartDate,
                EndDate = selectedTravelDB.EndDate,
                TotalCost = selectedTravelDB.TotalCost,
                Description = selectedTravelDB.Description,
                Notes = selectedTravelDB.Notes
            };

            return View(travelModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTravel(TravelModel travelModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View();

                travelProcessor.Edit(travelModel, travelModel.Id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al editar el viaje - " + ex.Message);
                return View();
            }
        }
    }
}