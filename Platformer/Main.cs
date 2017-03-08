using System;
using System.Collections.Generic;
using SFML.Window;
using SFML.Graphics;
class MainClass
{
	static int Main()
	{
		var fps = new FPS();
		var window = new RenderWindow(new VideoMode(1600, 1000), "Platformer");
		var tileMap = new TileMap(0, 0);
		var tileMap1 = new TileMap(4, 0);
		var map = new List<List<Int32>>();
		var buff = new List<Int32>();
		buff.Add(4);buff.Add(1);buff.Add(2);buff.Add(0); buff.Add(3); buff.Add(2);buff.Add(0); buff.Add(1); buff.Add(4);
		buff.Add(0); buff.Add(1); buff.Add(2);buff.Add(3); buff.Add(1); buff.Add(2);buff.Add(0); buff.Add(4); buff.Add(2);
		map.Add(buff);map.Add(buff);
		tileMap.load(0, 0, "64.png", new SFML.System.Vector2u(64, 64), map, 0, 100);
		tileMap.update();
		map = new List<List<Int32>>();
		buff = new List<Int32>();
		buff.Add(3);buff.Add(3);buff.Add(3);buff.Add(3);buff.Add(3);buff.Add(3);buff.Add(3);buff.Add(3);buff.Add(3);buff.Add(3);
		buff.Add(3); buff.Add(3); buff.Add(3); buff.Add(3); buff.Add(3); buff.Add(3); buff.Add(3); buff.Add(3); buff.Add(3); buff.Add(3);
		map.Add(buff); map.Add(buff);map.Add(buff); map.Add(buff);map.Add(buff); map.Add(buff); map.Add(buff); map.Add(buff);
		tileMap1.load(0, 256, "64.png", new SFML.System.Vector2u(64, 64), map, 0, 100);
		tileMap1.update();

		window.Closed += (sender, e) => window.Close();
		while (window.IsOpen)
		{
			window.DispatchEvents();
			Console.WriteLine(fps.getFPS());
			fps.update();
			window.Clear();
			window.Draw(tileMap);
			window.Draw(tileMap1);
			tileMap1.update();
			window.Display();
		}
		return 0;
	}
};