using System.Numerics;
using System.Reflection.PortableExecutable;

namespace Schness;

class Snek
{
	readonly Queue<SnekSegment> _body = new();
	SnekSegment _head;
	int _length;

	public IEnumerable<SnekSegment> Segments => _body.Prepend(_head);

	public Point Position { get => _head.Position; }

	public Direction.Types Heading { get => _head.Heading; }

	public int Length
	{
		get => _length;
		set
		{
			while (_length > value)
				_body.Dequeue();
			_length = value;
		}
	}

	public Snek(Point position, Direction.Types heading, int length)
	{
		Reset(position, heading, length);
	}

	public void Reset(Point position, Direction.Types heading, int length)
	{
		_body.Clear();
		_head = new(position, heading);
		_length = length;
	}

	public void Move(Direction direction)
	{
		if (_body.Count >= _length)
			_body.Dequeue();

		_head.Heading = direction.Type;
		_body.Enqueue(_head);
		_head.Position += direction;
	}
}

struct SnekSegment(Point position, Direction.Types heading)
{
	public Point Position = position;
	public Direction.Types Heading = heading;
}

enum Orientation
{
	Up = 0,
	Left = 1,
	Down = 2,
	Right = 3,
}

static class Orientations
{
	public static Orientation Shift(this Orientation dir, int amount = 0)
	{
		int mod(int k, int n) { return ((k %= n) < 0) ? k + n : k; }
		return (Orientation)mod((int)dir + amount, 4);
	}

	public static Orientation Inverse(this Orientation dir)
	{
		return Shift(dir, 2);
	}

	public static Direction ToDirection(this Orientation dir)
	{
		return dir switch
		{
			Orientation.Up => Direction.Up,
			Orientation.Left => Direction.Left,
			Orientation.Right => Direction.Right,
			Orientation.Down => Direction.Down,
			_ => throw new NotImplementedException()
		};
	}
}
