using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiriNews.Web.Areas.Admin.Models
{
    public class ErrorViewModel
    {
        public ErrorViewModel()
        {
            Errors = new List<string>();
        }
        public List<String> Errors { get; set; }
        public int Status { get; set; }
    }
}
