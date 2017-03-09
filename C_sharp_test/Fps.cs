using System;
using SFML.System;

class FPS
{
	UInt32 mFrame;
	UInt32 mFps;
	Clock mClock = new Clock();

	public FPS() { mFrame = 0; mFps = 0;}

	public void update()
	{ 
		if (mClock.ElapsedTime.AsSeconds() >= 1.0)
		{
			mFps = mFrame;
			mFrame = 0;
			mClock.Restart();
		}

		++mFrame;
	}

	public UInt32 getFPS() { return mFps; }

};

