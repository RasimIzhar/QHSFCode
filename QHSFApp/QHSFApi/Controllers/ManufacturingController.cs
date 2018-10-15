using BAL.Repository;
using DAL.DbEntities;
using System.Collections.Generic;
using System.Web.Http;
using static DAL.Model.ViewModels;

namespace QHSFApi.Controllers
{
    public class ManufacturingController : ApiController
    {
        // GET: Manufacturing
        private ManufacturingRepository ManufacturingRepository;

        public ManufacturingController()
        {
            ManufacturingRepository = new ManufacturingRepository(new vt_QSFHEntities());
        }

        [HttpGet]
        [Route("Api/Manufacturing/GetCalendarData")]
        public IEnumerable<ManufacturingOrderDetails> GetCalendarData()
        {
            IEnumerable<ManufacturingOrderDetails> Manufacturing = ManufacturingRepository.GetCalendarData();
            return Manufacturing;
        }
    }
}