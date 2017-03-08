using SFML.Window;
using SFML.Graphics;
class MainClass
{
	static int Main()
	{
		var window = new RenderWindow(new VideoMode(200, 200), "test");
		window.Closed += (sender, e) => window.Close();
		while (window.IsOpen)
		{
			window.DispatchEvents();
			window.Clear();
			window.Display();
		}
		return 0;
	}
};