using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Komiwojażer
{
    class Street
    {
        public enum DirectionsOfOnewayStreets { Left, Right, None }; //skierowanie poziomych ulic jednokierunkowych

        Vertice a; //pierwszy wierzchołek ulicy
        Vertice b; //pierwszy wierzchołek ulicy
        double distance; //dystans między wierzchołkami na mapie
        bool isHorizontal; //flaga oznaczająca, czy ulica jest pionowa, czy pozioma
        DirectionsOfOnewayStreets direction = DirectionsOfOnewayStreets.None;
        public static double length_of_the_street_horizontally = 164;
        public static double length_of_the_street_vertically = 72;

        bool visited;

        public Street(Vertice x, Vertice y, bool _horizontaly = true)
        {
            this.a = x;
            this.b = y;
            this.isHorizontal = _horizontaly;
            this.visited = false;
            if(this.isHorizontal)
            {
                this.distance = length_of_the_street_horizontally;
                if(b.ID%16 < 8)
                {
                    direction = DirectionsOfOnewayStreets.Right;
                }
                else
                {
                    direction = DirectionsOfOnewayStreets.Left;
                }
            }
            else
            {
                this.distance = length_of_the_street_vertically;
            } 
        }

        public Street(Vertice x, Vertice y, double length, Street.DirectionsOfOnewayStreets dir = Street.DirectionsOfOnewayStreets.None, bool _horizontaly = true)
        {
            this.a = x;
            this.b = y;
            this.distance = length;
            this.isHorizontal = _horizontaly;
            if (this.isHorizontal)
            {
                this.direction = dir;
            }
            else
            {
                this.direction = Street.DirectionsOfOnewayStreets.None;
            }     
        }

        public bool isPointOnStreet(Point destination)
        {
            if(isHorizontal)//warunek sprawdzający, czy ulica jest pionowa, czy pozioma
            {
                if (destination.X > A.Coordinates.X && destination.X < B.Coordinates.X)
                {
                    if (destination.Y > A.Coordinates.Y - 5 && destination.Y < A.Coordinates.Y + 5)
                    {
                        return true;
                    }
                }
            }
            else
            {
                if (destination.Y > A.Coordinates.Y && destination.Y < B.Coordinates.Y)
                {
                    if (destination.X > A.Coordinates.X - 5 && destination.X < A.Coordinates.X + 5)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public Vertice A { get { return a; } }
        public Vertice B { get { return b; } }
        public double Distance { get { return distance; } }
        public bool Visited { get { return visited; } set { this.visited = value; } }

        public Street.DirectionsOfOnewayStreets Direction { get { return direction; } }
        public bool IsOneWay { get { return isHorizontal; } }
    }
}
