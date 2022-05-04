using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.LevelPercentViewModel
{
    public class AddLevelPercentViewModel
    {
        public int StudentId { get; set; }
        public int TestId { get; set; }
        [MaxLength(100)]
        public int TestLevel { get; set; }
        [MaxLength(100)]
        public int TestScore { get; set; }
    }
}