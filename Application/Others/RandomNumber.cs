using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Others
{
    public static class RandomNumber
    {
        public static int Random()
        {
            Random random = new Random();
            int randomId = random.Next(1000, 999999);
            return randomId;
        }
    }
}
