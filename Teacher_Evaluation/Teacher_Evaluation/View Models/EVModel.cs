using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teacher_Evaluation.Models;

namespace Teacher_Evaluation.View_Models
{
    public class EVModel
    {
        public IEnumerable<Question> questions { get; set; }
        public IEnumerable<Course> courses { get; set; }
    }
}