using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Komiwojażer
{
    class Vertice
    {
        Point coordinates;
        int iD;
        bool visited;
        bool outgoing;
        bool incoming;
        static int radius = 7;

        public Vertice(Point coord, int iD)
        {
            this.iD = iD;
            this.coordinates = coord;
            this.visited = false;
            this.incoming = false;
            this.outgoing = false;
        }

        ~Vertice() { }
        public Point Coordinates { get { return coordinates; } }
        public int ID { get { return iD; } }
        public bool Visited { get { return visited; } set { this.visited = value; } }
        public bool Outgoing { get { return outgoing; } set { this.outgoing = value; } }
        public bool Incoming { get { return incoming; } set { this.incoming = value; } }

        public void drawVertice(PaintEventArgs e, Color color)
        {
            SolidBrush redBrush = new SolidBrush(color);
            e.Graphics.FillEllipse(redBrush, coordinates.X - radius, coordinates.Y - radius, radius + radius, radius + radius);
        }
    }
}
