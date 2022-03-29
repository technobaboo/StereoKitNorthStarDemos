using System;
using StereoKit;

SKSettings settings = new SKSettings{
	appName = "North Star Demos",
	depthMode = DepthMode.Balanced,
};

if(!SK.Initialize(settings))
	Environment.Exit(1);

CapsuleHand[] hands = {new CapsuleHand(Handed.Left), new CapsuleHand(Handed.Right)};
Renderer.SkyTex = Tex.GenCubemap(new Gradient(new GradientKey(Color.Black, 0)), Vec3.Up, 1);

GracinishCube cube = new GracinishCube(new Pose(Vec3.Forward * 0.5f, Quat.Identity));

while(SK.Step(()=>{
	hands[0].Step();
	hands[1].Step();

	cube.Step();
}));

SK.Shutdown();