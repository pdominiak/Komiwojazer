using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komiwojażer
{
    class Proxy: Algorithm
    {
        List<Street> shortestEdgesList = new List<Street>();
        public Proxy(Vertice startingPoint, List<Vertice> destinationPoints, List<Street> streets, int numberOfCrossroads) : base(startingPoint, destinationPoints, streets, numberOfCrossroads) { }

        public override List<Vertice> executeAlgorithm()
        {
            List<Vertice> resultRoute = new List<Vertice>(); //lista przechwująca trase
            List<Street> edgesOfCycle = new List<Street>(); //lista przechwująca trase w ulicach
            pickShortestEdges(edgesOfCycle);
            createHamiltonCycle(shortestEdgesList[0], shortestEdgesList, resultRoute);
            Vertice holder;
            resultRoute.RemoveAt(resultRoute.Count - 1);
            while (resultRoute[0].ID != vertices[0].ID)
            {
                holder = resultRoute[0];
                resultRoute.RemoveAt(0);
                resultRoute.Add(holder);
            }
            resultRoute.Add(resultRoute[0]);
            return resultRoute;
        }

        //Funkcja sprawdzająca, czy krawędź jest już w cyklu
        private bool isEdgeInRange(Street edge, List<Street> resultRoute)
        {
            foreach (Street street in resultRoute)
            {
                if (street.A == edge.B && street.B == edge.A)
                {
                    return true;
                }
            }
            return false;
        }

        //funkcja sprawdzająca, czy dodane wierzchołka nie stworzy nam wcześniej cyklu
        private bool isEdgeInCycle(Vertice vertice, Street edge, List<Street> resultRoute)
        {
            foreach (Street street in resultRoute)
            {
                if (edge.B == vertice)
                {
                    return true;
                }
                else if (edge.B == street.A)
                {
                    return isEdgeInCycle(vertice, street, resultRoute);
                }
            }
            return false;
        }

        private void pickShortestEdges(List<Street> resultRouteEdges)
        {
            if (resultRouteEdges.Count < vertices.Count)
            {
                foreach (Street street in edges)
                {
                    if (resultRouteEdges.Count == 0 && shortestEdgesList.Count != 0) break;
                    if (!resultRouteEdges.Exists(x => x == street))
                    {
                        if (street.Distance == floyd.getRouteLength(street.B.ID, street.A.ID))
                        {
                            if(isEdgeMeetingTheConditions(street, resultRouteEdges))
                            {
                                street.A.Outgoing = true;
                                street.B.Incoming = true;
                                resultRouteEdges.Add(street);
                                pickShortestEdges(resultRouteEdges);
                                street.A.Outgoing = false;
                                street.B.Incoming = false;
                                resultRouteEdges.RemoveAt(resultRouteEdges.Count - 1);
                            }
                            Street nextStreet = new Street(street.B, street.A, false);
                            if (!street.B.Outgoing && !street.A.Incoming)
                            {
                                if (!isEdgeInRange(nextStreet, resultRouteEdges))
                                {
                                    if (resultRouteEdges.Count == vertices.Count - 1 || !isEdgeInCycle(nextStreet.A, nextStreet, resultRouteEdges))
                                    {

                                        street.B.Outgoing = true;
                                        street.A.Incoming = true;
                                        resultRouteEdges.Add(nextStreet);
                                        pickShortestEdges(resultRouteEdges);
                                        street.B.Outgoing = false;
                                        street.A.Incoming = false;
                                        resultRouteEdges.RemoveAt(resultRouteEdges.Count - 1);
                                        break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (isEdgeMeetingTheConditions(street, resultRouteEdges))
                            {
                                resultRouteEdges.Add(street);
                                street.A.Outgoing = true;
                                street.B.Incoming = true;
                                pickShortestEdges(resultRouteEdges);
                                street.A.Outgoing = false;
                                street.B.Incoming = false;
                                resultRouteEdges.RemoveAt(resultRouteEdges.Count - 1);
                                break;
                            }
                        }
                    }
                }
            }
            else
            { //warunek sprawdzający, czy lista z najkrótszą marszrutą jest pusta
                if(shortestEdgesList.Count == 0)
                {
                    shortestEdgesList.AddRange(resultRouteEdges);
                } // warunek sprawdzający, czy wygenerowana marszruta jest krótsza od dotychczaswoej najkrótszej marszruty
                else if(shortestEdgesList.Sum(x => x.Distance) < resultRouteEdges.Sum(x => x.Distance))
                {
                    shortestEdgesList.Clear();
                    shortestEdgesList.AddRange(resultRouteEdges);
                }
            }    
        }

        private bool isEdgeMeetingTheConditions(Street street, List<Street> resultRouteEdges)
        {
            if (!street.A.Outgoing && !street.B.Incoming)
            {
                if (!isEdgeInRange(street, resultRouteEdges))
                {
                    if (resultRouteEdges.Count == vertices.Count - 1 || !isEdgeInCycle(street.A, street, resultRouteEdges))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void createHamiltonCycle(Street edge, List<Street> resultRouteEdges, List<Vertice> resultRoute)
        {
            edge.Visited = true;
            if (resultRoute.Count == 0)
            {
                resultRoute.Add(edge.A);
                resultRoute.Add(edge.B);
            }
            else
            {
                if (resultRoute[resultRoute.Count - 1] != edge.A)
                {
                    resultRoute.Add(edge.A);
                }
                if (resultRoute[resultRoute.Count - 1] != edge.B)
                {
                    resultRoute.Add(edge.B);
                }
            }
            foreach (Street street in resultRouteEdges)
            {
                if (!street.Visited && street.A == resultRoute[resultRoute.Count - 1])
                {
                    createHamiltonCycle(street, resultRouteEdges, resultRoute);
                }
            }
        }
    }
}
