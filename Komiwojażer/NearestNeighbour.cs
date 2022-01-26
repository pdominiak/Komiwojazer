using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komiwojażer
{
    class NearestNeighbour: Algorithm
    {
        public NearestNeighbour(Vertice startingPoint, List<Vertice> destinationPoints, List<Street> streets, int numberOfCrossroads):base(startingPoint, destinationPoints, streets, numberOfCrossroads) { }

        //autor: Bartosz Chreścionko
        public override List<Vertice> executeAlgorithm()
        {
            List<Vertice> resultRoute = new List<Vertice>(); //lista przechwująca trase 
            Vertice currentVertice = vertices[0]; //aktualnie sprawdany wierzchiłek w kodzie, na początku ustawiany na wierzchołek startowy
            resultRoute.Add(currentVertice); //dodanie aktualnego wierzchołka do listy
            currentVertice.Visited = true; //ustawienie wierzchołka na odwiedzony
            while (vertices.Exists(x => x.Visited == false)) //warunek sprawdzający, czy wszystekie wierzchołki zostały już odwiedzone 
            {//przeście przes liste z krawędziami
                foreach (Street street in edges)
                {//warunek sprawdzajączy, czy pierwszy wierzchołek krawędzi jest naszym aktualnym wierchołkiem i czy drugie wierzchołek nie był już odwiedzony
                    if (street.A == currentVertice && !street.B.Visited)
                    {
                        currentVertice = street.B; //przestawienie aktualnego wierzchołka na drugi wierzchiłek z wybranej krawędzi
                        currentVertice.Visited = true; //ustawienie wierzchołka na odwiedzony
                        resultRoute.Add(currentVertice); //dodanie aktualnego wierzchołka do listy
                        break; //wyjście z fora, w celu zabezpieczenia przed pomijaniem niektórych wierzchołków
                    }
                }
            }
            resultRoute.Add(vertices[0]); // ponowne dodanie wierzchoka startowego w celu zamknięcia trasy
            return resultRoute;
        }
    }
}
