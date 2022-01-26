using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komiwojażer
{
    class Precise: Algorithm
    {
        List<Vertice> shortestRoute = new List<Vertice>();
        int shortestRouteLength;

        public Precise(Vertice startingPoint, List<Vertice> destinationPoints, List<Street> streets, int numberOfCrossroads): base(startingPoint, destinationPoints, streets, numberOfCrossroads)
        {
            shortestRouteLength = System.Int16.MaxValue;
        }

        public override List<Vertice> executeAlgorithm()
        {
            List<Vertice> resultRoute = new List<Vertice>(); //lista przechwująca trase
            resultRoute.Add(vertices[0]);
            treeSearch(0, resultRoute);
            return shortestRoute;
        }
        //autor: Paweł Dominiak
        void treeSearch(int length, List<Vertice> resultRoute)
        {   //Warunek sprawdzający, czy długość marszruty jest krótsza od ilości wierzchołów 
            if(resultRoute.Count < vertices.Count)
            {//Pętla przechodząca przez listę z wierzchołakmi grafu małego 
               foreach(Vertice vertice in vertices)
               { //warunke sprwdzający, czy w marszrucie znajdue sie wybrany wierzchołek 
                    if(!resultRoute.Exists(x => x == vertice))
                    {
                        resultRoute.Add(vertice); //Dodanie wybranego wierzchołka do marszurty 
                        length += floyd.getRouteLength(resultRoute[resultRoute.Count - 2], resultRoute[resultRoute.Count - 1]); //Zwiększenie długości marszruty 
                        treeSearch(length, resultRoute);//wywołąnie rekurencyjnie funkcji treesearch dla obecnaj marszruty i jej długości
                        length -= floyd.getRouteLength(resultRoute[resultRoute.Count - 2], resultRoute[resultRoute.Count - 1]); //Zminiejszenie długości marszruty 
                        resultRoute.RemoveAt(resultRoute.Count - 1); //Zdjęcie z marszruty ostatniego wierzchołka 
                    }
               }
            }
            else
            {//warunke sprawdzający czy długość aktualnej marszruty jest krótsza od dotychczasowej 
                if(length + floyd.getRouteLength(resultRoute[resultRoute.Count-1], resultRoute[0]) < shortestRouteLength)
                {
                    shortestRouteLength = length + floyd.getRouteLength(resultRoute[resultRoute.Count - 1], resultRoute[0]); //ustawienie nowej najkrótszej długości marszruty
                    shortestRoute.Clear();//wyczyszcenie listy z dotychczasową najkrótszą marszrutą
                    shortestRoute.AddRange(resultRoute); //ustawnienie nowej marszrutry na najkrótszą 
                    shortestRoute.Add(resultRoute[0]); //Dodanie wierzchołka startowego do końca marszruty
                }
            }
        }
    }
}
