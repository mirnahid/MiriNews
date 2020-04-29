using System;
using System.Collections.Generic;

namespace MiriNews.Web.Models
{
    public class ErrorViewModel
    {
        public ErrorViewModel()
        {
            Errors = new List<string>();
        }

        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public List<string> Errors { get; set; }

        public int Status { get; set; }
    }
}
