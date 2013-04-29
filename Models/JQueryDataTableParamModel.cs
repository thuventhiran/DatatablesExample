

namespace DatatablesExample.Models
{
  /// <summary>
  /// A model to receive parameters from dataTables AJAX call
  /// </summary>
  public class JQueryDataTableParamModel
  {
    public string sEcho { get; set; }

    public string sSearch { get; set; }

    public int iDisplayLength { get; set; }

    public int iDisplayStart { get; set; }
  }
}