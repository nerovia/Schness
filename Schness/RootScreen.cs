
using SadConsole.Quick;
using System.Reflection.Metadata.Ecma335;

namespace Schness;

internal class RootScreen : ScreenObject
{
	readonly ScreenSurface _surface;
	readonly Snek _snek;

	Point _mousePosition;

	public RootScreen()
	{
		_surface = new ScreenSurface(GameSettings.GAME_WIDTH, GameSettings.GAME_HEIGHT);
		_surface.WithMouse((s, e) =>
		{
			_mousePosition = e.SurfaceCellPosition;
			return false;
		});

		_snek = new((10, 10), Direction.Types.Up, 50);

		Children.Add(_surface);
	}

	public override void Update(TimeSpan delta)
	{
		base.Update(delta);

		//_mousePosition = (40, 20);

		if (_snek.Position == _mousePosition)
			return;

		var dir = new Direction[] { Direction.Up, Direction.Left, Direction.Down, Direction.Right }.MinBy(it => Point.EuclideanDistanceMagnitude(_snek.Position + it, _mousePosition));

		_snek.Move(dir);

		_surface.Clear();

		foreach (var segment in _snek.Segments)
		{
			if (_surface.Surface.Area.Contains(segment.Position))
			{
				_surface.SetBackground(segment.Position.X, segment.Position.Y, Color.Red);
			}
		}

		_surface.IsDirty = true;

	}


}


