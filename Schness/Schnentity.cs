using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Point = System.Drawing.Point;

namespace Schness;

class SchnessGrid
{
	public int Width;
	public int Height;

	// Get the piece that occupies that cell
	public SchnessPiece this[Point pos] { get => throw new NotImplementedException(); }

	public bool IsOccupied(Point pos) => throw new NotImplementedException();
}

class SchnessPiece
{
	ISchnessBehaviour Behaviour;
}

interface ISchnessBehaviour
{
	IEnumerable<Point> GetMoves(Snek snek, SchnessGrid grid);
}


enum Step
{
	MoveForward,
	MoveLeft,
	MoveRight,
}


