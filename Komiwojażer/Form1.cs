using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using System.Diagnostics.Tracing;
using System.Threading;
using System.Security.Cryptography;

namespace Komiwojażer
{
    public partial class Form1 : Form
    {
        //punkty na mapie
        List<Vertice> destinationPoints = new List<Vertice>(); //punkty docelowe wybrane przez użytkownika
        List<Vertice> crossroads = new List<Vertice>(); //skrzyżowania
        List<Street> streets = new List<Street>(); //ulice

        //ścieżka do narysowania
        List<Vertice> nearestNeighbourPath = new List<Vertice>(); 
        List<Vertice> proxyPath = new List<Vertice>();
        List<Vertice> precisePath = new List<Vertice>();

        Vertice startingPoint; //punkt startowy 
        bool selectPiontMode; //flaga oznaczająca tryb wybierania punktów

        List<int> endCrossrodsId = new List<int>() { 7, 15, 23, 31, 39, 47, 55, 63, 71, 79}; //id skrzyżowań na których kończy się ulica poziomo
        List<int> streets_positions_horizontally = new List<int>() { 43, 132, 219, 308, 397, 519, 649, 771 }; //współrzędne ulic poziomo
        List<int> streets_positions_vertically = new List<int>() { 25, 70, 115, 160, 207, 253, 298, 340, 385, 430 }; //współrzędne ulic pionowo

        List<Thread> pathDrawingThread = new List<Thread>(); //wątki używane do rysowania ścieżek
        public Form1()
        {
            InitializeComponent();
            createStreets();
            selectPiontMode = true;
        }

        private void mapa_Click(object sender, EventArgs e)
        {
            var mouseEventArgs = e as MouseEventArgs; //przypisanie pozycji kliknięcia na pictureboxsie
            if (selectPiontMode)
            {
                startingPoint = null; //wyczyszczenie punktu startowego
                int positionOfRowsToDelete = ((streets_positions_horizontally.Count - 1) * streets_positions_vertically.Count) + ((streets_positions_vertically.Count - 1) * streets_positions_horizontally.Count);
                if (streets.Count != positionOfRowsToDelete)
                {
                    streets.RemoveRange(positionOfRowsToDelete, destinationPoints.Count * 2 + 2); //usunięcie ulicy stworzonych na potrzebę punktu startowego i punktów doeclowych
                }
                if (destinationPoints.Count == 20)//warunek sprawdzający, czy użytkownik nie wybrał już 20 punktów
                {
                    MessageBox.Show("Maksymalna ilosc punktow docelowych to 20");
                }
                else
                {
                    addNewPoint(new Point(mouseEventArgs.X, mouseEventArgs.Y));
                }
            }
            else
            {
                if (destinationPoints.Count == 20)//warunek sprawdzający, czy użytkownik nie wybrał już 20 punktów
                {
                    MessageBox.Show("Maksymalna ilosc punktow docelowych to 20");
                }
                else
                {
                    addNewPoint(new Point(mouseEventArgs.X, mouseEventArgs.Y));
                }
            }
            mapa.Refresh(); // odświerzenie mapy, zeby nowo dodane puntky się na niej pojawiły
        }

        private void mapa_Paint(object sender, PaintEventArgs e)// metoda zajmująca się narysowaniem wszystkich punktów na mapie 
        {
            destinationPoints.ForEach(x => x.drawVertice(e, Color.Red));
            //crossroads.ForEach(x => x.drawVertice(e, Color.Red));
            if (startingPoint != null) //rysowanie puntu starowego
            {
                startingPoint.drawVertice(e, Color.Blue);
            }
            drawPath(this.show_NN.Checked, nearestNeighbourPath, Color.Black, e);
            drawPath(this.show_Proxy.Checked, proxyPath, Color.Green, e);
            drawPath(this.show_Precise.Checked, precisePath, Color.Orange, e);
        }
        //metoda tworząca skrzyżowania i pierwsze ulice na podstawie stworzonych skrzyżowań
        private void createStreets()
        {
            //pętla tworząca skrzyżownia
            for (int j = 0; j < streets_positions_vertically.Count; ++j)
            {
                for (int i = 0; i < streets_positions_horizontally.Count; ++i)
                {
                    crossroads.Add(new Vertice(new Point(streets_positions_horizontally[i], streets_positions_vertically[j]), crossroads.Count));
                }
            }            //pętla tworząca ulice z podziałem na poziome i pionowe 
            for (int i = 0; i < crossroads.Count - 1; ++i)
            {
                if(!endCrossrodsId.Exists(x => x == i))
                {
                    streets.Add(new Street(crossroads[i], crossroads[i + 1]));
                }
                if(i < crossroads.Count - streets_positions_horizontally.Count)
                {
                    streets.Add(new Street(crossroads[i], crossroads[i + streets_positions_horizontally.Count], false));
                } 
            }
        }

        //funkcja sprawdzająca, na której ulicy znajduje się nowy punkt i dodająca go w odpowiednie miejsce
        private void addNewPoint(Point destination)
        {
            for(int i = streets.Count - 1; i >= 0 ; --i)
            {
                if (streets[i].isPointOnStreet(destination)) //warunke sprawdzający na jakiej uliczy znajduje się punkt
                {
                    if (selectPiontMode)
                    {
                        if (streets[i].IsOneWay)
                        {
                            startingPoint = new Vertice(new Point(destination.X, streets[i].A.Coordinates.Y), crossroads.Count + destinationPoints.Count); //stworzenie puntku startowego
                        }
                        else
                        {
                            startingPoint = new Vertice(new Point(streets[i].A.Coordinates.X, destination.Y), crossroads.Count + destinationPoints.Count); //stworzenie puntku startowego
                        }
                        addPointToStreetNetwork(streets[i], startingPoint);
                    }
                    else
                    {
                        if (streets[i].IsOneWay)
                        {
                            destinationPoints.Add(new Vertice(new Point(destination.X, streets[i].A.Coordinates.Y), crossroads.Count + destinationPoints.Count + 1)); // dodanie nowego punktu docelowego do listy
                        }
                        else
                        {
                            destinationPoints.Add(new Vertice(new Point(streets[i].A.Coordinates.X, destination.Y), crossroads.Count + destinationPoints.Count + 1)); // dodanie nowego punktu docelowego do listy
                        }
                        addPointToStreetNetwork(streets[i], destinationPoints[destinationPoints.Count - 1]);
                    }
                    break;
                }
            }
        }

        private void addPointToStreetNetwork(Street street, Vertice newPoint)//funkcja dodająca ulice z nowymi punktami
        {
            double distance;
            double streetLengthInPixels;
            if (street.IsOneWay)//warunke sprawdzający, czy ulica jest jednokierunkowa
            {

                streetLengthInPixels = Math.Abs(street.B.Coordinates.X - street.A.Coordinates.X);
                if (street.Direction == Street.DirectionsOfOnewayStreets.Left)
                { // dodanie nowych sudo-ulicy, które przechowują odległości nowego punktu do najbliższych skrzyżowań 
                    distance = (Math.Abs(Math.Abs(newPoint.Coordinates.X - street.A.Coordinates.X) - streetLengthInPixels) / streetLengthInPixels) * Street.length_of_the_street_horizontally;
                    streets.Add(new Street(street.A, newPoint, distance, Street.DirectionsOfOnewayStreets.Left));
                    distance = (Math.Abs(Math.Abs(newPoint.Coordinates.X - street.A.Coordinates.X) - streetLengthInPixels) / streetLengthInPixels) * Street.length_of_the_street_horizontally;
                    streets.Add(new Street(newPoint, street.B, distance, Street.DirectionsOfOnewayStreets.Left));
                }
                else
                {
                    distance = (Math.Abs(Math.Abs(newPoint.Coordinates.X - street.A.Coordinates.X) - streetLengthInPixels) / streetLengthInPixels) * Street.length_of_the_street_horizontally;
                    streets.Add(new Street(street.A, newPoint, distance, Street.DirectionsOfOnewayStreets.Right));
                    distance = (Math.Abs(Math.Abs(newPoint.Coordinates.X - street.B.Coordinates.X) - streetLengthInPixels) / streetLengthInPixels) * Street.length_of_the_street_horizontally;
                    streets.Add(new Street(newPoint, street.B, distance, Street.DirectionsOfOnewayStreets.Right));
                }
            }
            else
            {
                streetLengthInPixels = Math.Abs(street.B.Coordinates.Y - street.A.Coordinates.Y);
                distance = (Math.Abs(Math.Abs(newPoint.Coordinates.Y - street.A.Coordinates.Y) - streetLengthInPixels) / streetLengthInPixels) * Street.length_of_the_street_vertically;
                streets.Add(new Street(street.A, newPoint, distance, Street.DirectionsOfOnewayStreets.None, false));
                distance = (Math.Abs(Math.Abs(newPoint.Coordinates.Y - street.B.Coordinates.Y) - streetLengthInPixels) / streetLengthInPixels) * Street.length_of_the_street_vertically;
                streets.Add(new Street(street.B, newPoint, distance, Street.DirectionsOfOnewayStreets.None, false));
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(startingPoint!=null) //warunke sprawdzający, czy punkt startowy został wybrany
            {
                if (destinationPoints.Count > 2) // warunke sprawdzający, czy użytkownik wybrał przynajmniej 3 punkty docelowe
                {
                    pathDrawingThread.ForEach(x => x.Abort());
                    pathDrawingThread.Clear();
                    setPointsToNotVisited();
                    nearestNeighbourPath.Clear(); //wyczyszczenie ściezki
                    precisePath.Clear();
                    proxyPath.Clear();
                    pathDrawingThread.Add(new Thread(() => createPath(nearestNeighbourPath, new NearestNeighbour(startingPoint, destinationPoints, streets, crossroads.Count), this.show_NN_path_length)));
                    pathDrawingThread.Add(new Thread(() => createPath(proxyPath, new Proxy(startingPoint, destinationPoints, streets, crossroads.Count), this.show_proxy_path_length)));
                    if(destinationPoints.Count < 9)
                    {
                        pathDrawingThread.Add(new Thread(() => createPath(precisePath, new Precise(startingPoint, destinationPoints, streets, crossroads.Count), this.show_precise_path_length)));
                    }
                    else
                    {
                        MessageBox.Show("Algorytm precise, działą tylko do 8 punktów docelowych");
                    }
                    pathDrawingThread.ForEach(x => x.Start());
                }
                else
                {
                    MessageBox.Show("Musisz wybrać przynajmniej 3 punkty docelowe");
                }
            }
            else
            {
                MessageBox.Show("Brak wybranego punktu startowego");
            }   
        }

        private void createPath(List<Vertice> path, Algorithm algorithm, TextBox length)
        {
            Floyda_Warshalla floyd = new Floyda_Warshalla(crossroads.Count + destinationPoints.Count + 1, streets);
            int pathlength = 0;
            List<Vertice> allVertices = new List<Vertice>();//lista wszystkich punktów
            allVertices.AddRange(crossroads);
            allVertices.Add(startingPoint);
            allVertices.AddRange(destinationPoints);
            List<int> route = algorithm.getFullRoute(); //pobrnie ścieżki z id kolejnych wierzchołków ścieżki
            for (int i = 0; i < route.Count; ++i)//pętla przechodząca przez liste z ID kolejnych wierzchołków ze ścieżki
            {
                path.Add(allVertices[route[i]]); // dodawanie wierzchołów o odpowiednim ID do ścieżki
                if (i > 0)
                {
                    pathlength += floyd.getRouteLength(route[i - 1], route[i]);
                }
                Thread.Sleep(500);
                this.mapa.Invoke((MethodInvoker)delegate { mapa.Refresh(); });
                length.Invoke((MethodInvoker)delegate { length.Text = pathlength.ToString(); });
            }
        }

        private void setPointsToNotVisited()
        {
            foreach(Vertice point in destinationPoints)
            {
                point.Visited = false;
                point.Outgoing = false;
                point.Incoming = false;
            }
            startingPoint.Visited = false;
            startingPoint.Outgoing = false;
            startingPoint.Incoming = false;
        }

        private void drawPath(bool isCheckboxChecked, List<Vertice> path, Color pathColor, PaintEventArgs e)
        {
            if (isCheckboxChecked && path.Count > 1) //rysowanie ścieżki
            {
                Pen pen = new Pen(pathColor, 3); // wybranie koloru ścieżki
                for (int i = 0; i < path.Count - 1; ++i)
                {
                    e.Graphics.DrawLine(pen, path[i].Coordinates, path[i + 1].Coordinates);
                }
                path[path.Count - 1].drawVertice(e, pathColor);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int positionOfRowsToDelete = ((streets_positions_horizontally.Count - 1) * streets_positions_vertically.Count) + ((streets_positions_vertically.Count - 1) * streets_positions_horizontally.Count);
            if(streets.Count != positionOfRowsToDelete)
            {
                streets.RemoveRange(positionOfRowsToDelete, destinationPoints.Count * 2 + 2); //usunięcie ulicy stworzonych na potrzebę punktu startowego i punktów doeclowych
            }
            destinationPoints.Clear(); //wyczyszczenie punktów decelowych
            nearestNeighbourPath.Clear(); //wyczyszczenie ściezki
            precisePath.Clear();
            proxyPath.Clear();
            startingPoint = null; //wyczyszczenie punktu startowego
            if (nearestNeighbourPath != null)
            {
                pathDrawingThread.ForEach(x => x.Abort());
            }
            mapa.Refresh(); //odświerzenie mapy  
        }

        private void selsect_start_position_button_Click(object sender, EventArgs e)
        {
            selectPiontMode = true;
        }

        private void select_destination_button_Click(object sender, EventArgs e)
        {
            if(startingPoint==null)
            {
                MessageBox.Show("Najpier wybierz punkt startowy");
            }
            else
            {
                selectPiontMode = false;
            }

        }

        private void show_NN_CheckedChanged(object sender, EventArgs e)
        {
            mapa.Refresh();
        }

        private void show_Proxy_CheckedChanged(object sender, EventArgs e)
        {
            mapa.Refresh();
        }

        private void show_Precise_CheckedChanged(object sender, EventArgs e)
        {
            mapa.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pathDrawingThread.ForEach(x => x.Abort());
            Application.Exit();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
