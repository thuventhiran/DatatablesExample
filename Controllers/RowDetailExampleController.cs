using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DatatablesExample.Data;
using DatatablesExample.Models;

namespace DatatablesExample.Controllers
{
  public class RowDetailExampleController : Controller
  {
    //
    // GET: /RowDetailExample/

    public ActionResult Index()
    {
      return View();
    }

    public JsonResult GetAjaxData(JQueryDataTableParamModel param)
    {
      using (var e = new ExampleEntities())
      {
        var totalRowsCount = new System.Data.Objects.ObjectParameter("TotalRowsCount", typeof(int));
        var filteredRowsCount = new System.Data.Objects.ObjectParameter("FilteredRowsCount", typeof(int));

        var data = e.pr_SearchPerson(param.sSearch,
                                        Convert.ToInt32(Request["iSortCol_0"]),
                                        Request["sSortDir_0"],
                                        param.iDisplayStart,
                                        param.iDisplayStart + param.iDisplayLength,
                                        totalRowsCount,
                                        filteredRowsCount);

        var aaData = data.Select(d => new string[] { 
                                                      string.Empty //this empty element is a dummy placeholder for the expander column
                                                      , d.FirstName
                                                      , d.LastName
                                                      , d.Nationality
                                                      , d.DateOfBirth.Value.ToString("dd MMM yyyy") }).ToArray();

        return Json(new
        {
          sEcho = param.sEcho,
          aaData = aaData,
          iTotalRecords = Convert.ToInt32(totalRowsCount.Value),
          iTotalDisplayRecords = Convert.ToInt32(filteredRowsCount.Value)
        }, JsonRequestBehavior.AllowGet);
      }
    }

    public PartialViewResult GetRowDetail(string firstName, string lastName)
    {
      using (var e = new ExampleEntities())
      {
        var data = e.pr_GetPersonDetail(firstName, lastName);

        var detail = data.FirstOrDefault();

        return PartialView("RowDetail", new RowDetailModel() {  DateAdded = detail.DateAdded, 
                                                                MobileTel = detail.MobileTel, 
                                                                HomeTel = detail.HomeTel, 
                                                                EmailAddress = detail.EmailAddress });
      }
    }

  }
}
