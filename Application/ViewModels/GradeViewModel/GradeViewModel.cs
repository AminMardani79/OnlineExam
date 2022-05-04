using Domin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.GradeViewModel
{
    public class GradeViewModel
    {
        public int GradeId { get; set; }
        public string GradeName { get; set; }
        public int TestsCount { get; set; }

    }
}
