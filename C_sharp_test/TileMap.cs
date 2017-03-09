using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;

public class TileMap : Transformable, Drawable
{
	VertexArray m_vertices = new VertexArray();
	Texture m_tileset;
	List<Int32> _tiles = new List<Int32>();
	Vector2u _tileSize;
	Int32 _baseMin;
	Int32 _amount;
	UInt32 _speed;
	Int32 _counter = 0;
	Time _lastTime = Time.FromMilliseconds(0);
	static Clock _clock = new Clock();

	public TileMap(Int32 amount, UInt32 speed)
	{
		_amount = amount;
		_speed = speed;
	}

	public bool load (Int32 x, Int32 y, string tileset, Vector2u tileSize, List<List<Int32>> tiles, Int32 baseMin,Int32 baseMax)
	{
		m_tileset = new Texture(tileset);
		_tileSize = tileSize;
		_baseMin = baseMin;

		m_vertices.PrimitiveType = PrimitiveType.Quads;

		for (Int32 i = 0; i < tiles.Count; ++i)
			for (Int32 j = 0; j < tiles[0].Count; ++j)
			{
				if (tiles[i][j] >= baseMin && tiles[i][j] <= baseMax)
					{

						m_vertices.Append(new Vertex(new Vector2f(j * tileSize.X + x, i * tileSize.Y + y)));
						m_vertices.Append(new Vertex(new Vector2f((j + 1) * tileSize.X + x, i * tileSize.Y + y)));
						m_vertices.Append(new Vertex(new Vector2f((j + 1) * tileSize.X + x, (i + 1) * tileSize.Y + y)));
						m_vertices.Append(new Vertex(new Vector2f(j * tileSize.X + x, (i + 1) * tileSize.Y + y)));


						_tiles.Add(tiles[i][j]);
					}
		
			}
		return true;
	}

	public void update()
	{
		if (_clock.ElapsedTime.AsMilliseconds() - _lastTime.AsMilliseconds() > _speed)
		{
			for (UInt32 i = 0; i < _tiles.Count; ++i)
			{
				Int32 tileNumber = (_tiles[(int)i] - _baseMin) * (_amount + 1) + _counter;

				Int32 tu = (int)(tileNumber % (m_tileset.Size.X / _tileSize.X));
				Int32 tv = (int)(tileNumber / (m_tileset.Size.X / _tileSize.X));

				Vertex buffer;

				buffer = m_vertices[i * 4];
				buffer.TexCoords = new Vector2f(tu * _tileSize.X, tv * _tileSize.Y);
				m_vertices[i * 4] = buffer;

				buffer = m_vertices[i * 4 + 1];
				buffer.TexCoords = new Vector2f((tu + 1) * _tileSize.X, tv * _tileSize.Y);
				m_vertices[i * 4 + 1] = buffer;

				buffer = m_vertices[i * 4 + 2];
				buffer.TexCoords = new Vector2f((tu + 1) * _tileSize.X, (tv + 1) * _tileSize.Y);
				m_vertices[i * 4 + 2] = buffer;

				buffer = m_vertices[i * 4 + 3];
				buffer.TexCoords = new Vector2f(tu * _tileSize.X, (tv + 1) * _tileSize.Y);
				m_vertices[i * 4 + 3] = buffer;
			}
			_counter++;
			if (_counter > _amount)
				_counter = 0;
			_lastTime = _clock.ElapsedTime;
		}
	}

	public void setSpeed(UInt32 speed)
	{
		_speed = speed;
	}

	public void clear()
	{
		m_vertices.Clear();
	}

	public virtual void Draw(RenderTarget target, RenderStates states)
	{
		// apply the transform
		states.Transform = Transform;

		// apply the tileset texture
		states.Texture = m_tileset;

		// draw the vertex array
		target.Draw(m_vertices, states);
	}
};
