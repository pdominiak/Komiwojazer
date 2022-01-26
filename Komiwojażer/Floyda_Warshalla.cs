using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komiwojażer
{
    class Floyda_Warshalla
    {
        int[,] neighborhoodMatrix;
        int[,] routesMatrix;
        int size;
        static int _infinity = 999999999;

        public Floyda_Warshalla(int size, List<Street> streets)
        {
            this.size = size;
            neighborhoodMatrix = new int[size, size];
            fillMatrixWithNumbers(neighborhoodMatrix, _infinity);
            foreach (Street street in streets)
            {
                if (street.Direction == Street.DirectionsOfOnewayStreets.None) // wpisywanie dla ulic pionowych i dwukierunkowych
                {
                    neighborhoodMatrix[street.A.ID, street.B.ID] = (int)street.Distance;
                    neighborhoodMatrix[street.B.ID, street.A.ID] = (int)street.Distance;
                }
                else if (street.Direction == Street.DirectionsOfOnewayStreets.Right) // wpisywanie dla ulic poziomych, jednukierunkowych w prawo
                {
                    neighborhoodMatrix[street.A.ID, street.B.ID] = (int)street.Distance;
                }
                else if (street.Direction == Street.DirectionsOfOnewayStreets.Left)// wpisywanie dla ulic poziomych, jednukierunkowych w lewo
                {
                    neighborhoodMatrix[street.B.ID, street.A.ID] = (int)street.Distance;
                }
            }
            executAlgorithm();
        }

        private void executAlgorithm()
        {
            routesMatrix = new int[size, size];
            fillMatrixWithNumbers(routesMatrix, -1);
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    for (int z = 0; z < size; z++)
                    {
                        if (neighborhoodMatrix[y,x] + neighborhoodMatrix[x,z] < neighborhoodMatrix[y,z])
                        {
                            neighborhoodMatrix[y,z] = neighborhoodMatrix[y,x] + neighborhoodMatrix[x,z];
                            routesMatrix[y,z] = x;
                        }
                    }
                }      
            }       
        }

        public void writeNeighborhoodMatrixToFile(string filename)
        {
            System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(filename);
            string output = "";
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    output += neighborhoodMatrix[i, j].ToString();
                    if (j != size - 1)
                    {
                        output += "\t";
                    }
                }
                streamWriter.WriteLine(output);
                output = "";
            }
            streamWriter.Close();
        }

        private void fillMatrixWithNumbers(int[,] matrix, int number)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    matrix[i, j] = number;
                }
            }
        }

        public int getRouteLength(int i, int j)
        {
            if (i >= 0 && i < size && j >= 0 && j < size)
                return neighborhoodMatrix[i,j];
            else
                return 0;
        }

        public int getRouteLength(Vertice a, Vertice b)
        {
                return neighborhoodMatrix[a.ID, b.ID];
        }
        public void getRoute(int i, int j, List<int> route)
        {
            int k = routesMatrix[i, j];
            if (k != -1)
            {
                getRoute(i, k, route);
                route.Add(k);
                getRoute(k, j, route);
            }
        }

    }
}
