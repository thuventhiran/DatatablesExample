using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DatatablesExample.Models
{
  public class RowDetailModel
  {
    public DateTime? DateAdded { get; set; }
    public string MobileTel { get; set; }
    public string HomeTel { get; set; }
    public string EmailAddress { get; set; }
  }
}