using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komiwojażer
{
    class Algorithm
    {
        protected List<Vertice> vertices = new List<Vertice>(); //lista przechowująca punkt startowy i punkt docelowy
        protected List<Street> edges = new List<Street>(); //lista przechowująca wszystkie krawędzie między wierzchołkami
        protected Floyda_Warshalla floyd; //algorytm floyda-warshalla pozwalający na szukanie najkrótszych ścieżek pomiędzy punktami w grafie

        public Algorithm(Vertice startingPoint, List<Vertice> destinationPoints, List<Street> streets, int numberOfCrossroads)
        {
            vertices.Add(startingPoint); //dodanie punktu startowego
            vertices.AddRange(destinationPoints); //dodanie punktów docelowych
            floyd = new Floyda_Warshalla(numberOfCrossroads + destinationPoints.Count + 1, streets); //załączenie algorytmu do szukania najkrótszych ścieżek 
            for (int i = 0; i < vertices.Count; ++i)
            {
                for (int j = 0; j < vertices.Count; ++j)
                {
                    if (i != j)
                    {
                        edges.Add(new Street(vertices[i], vertices[j], floyd.getRouteLength(vertices[i], vertices[j]), Street.DirectionsOfOnewayStreets.None));
                    }
                }
            }
            edges.Sort((x, y) => x.Distance.CompareTo(y.Distance));//posostowanie krawędzie w sposób rosnący
        }

        public virtual List<Vertice> executeAlgorithm()
        {
            return new List<Vertice>();
        }

        public List<int> getFullRoute()
        {
            List<int> route = new List<int>();
            List<Vertice> path = executeAlgorithm();
            for (int i = 0; i < path.Count - 1; ++i)
            {
                route.Add(path[i].ID);
                floyd.getRoute(path[i].ID, path[i + 1].ID, route);
            }
            route.Add(path[path.Count - 1].ID);
            return route;
        }
    }
}
