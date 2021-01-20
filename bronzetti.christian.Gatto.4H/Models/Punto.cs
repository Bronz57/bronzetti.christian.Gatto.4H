using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bronzetti.christian.Gatto._4H.Models
{
    class Punto
    {
        int _x;
        int _y;


        public int X
        {
            get
            {
                return _x;
            }

            set

            {
                _x = value;
            }
        }

        public int Y
        {
            get
            {
                return _y;
            }

            set

            {
                _y = value;
            }
        }



        public Punto(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public Punto(string str)
        {
            string[] coordinate = str.Split(',');

            X = Convert.ToInt32(coordinate[0]);
            Y = Convert.ToInt32(coordinate[1]);
        }
    }
}
