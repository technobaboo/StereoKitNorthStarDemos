using StereoKit;

public class CapsuleHand {
	Handed handed;
	public float jointRadius = 0.008f;
	public float cylinderRadius = 0.006f;
	// public float palmRadius = 0.015f;
	public float palmRadius = 0;

	private Color color;
	private int colorIndex = 0;
	private static Dictionary<Handed, Color[]> colors = new Dictionary<Handed, Color[]>{
		{Handed.Left,  new Color[]{ new Color(0.0f, 0.0f, 1.0f), new Color(0.2f, 0.0f, 0.4f), new Color(0.0f, 0.2f, 0.2f) } },
		{Handed.Right, new Color[]{ new Color(1.0f, 0.0f, 0.0f), new Color(1.0f, 1.0f, 0.0f), new Color(1.0f, 0.5f, 0.0f) } },
	};

	private Mesh sphereMesh;
	private Mesh cylinderMesh;

	public CapsuleHand(Handed handed) {
		this.handed = handed;
		Input.HandVisible(handed, false);
		sphereMesh = Mesh.GenerateSphere(2);
		cylinderMesh = Mesh.GenerateCylinder(2, 1, Vec3.Forward);
	}

	public void Step() {
		Hand hand = Input.Hand(handed);
		if(!hand.IsTracked)
			return;
		
		if(hand.IsJustTracked) {
			colorIndex = (colorIndex + 1) % colors[handed].Length;
			color = colors[handed][colorIndex];
		}

		// Draw finger sphere joints
		for(int f=0; f<5; ++f) {
			for(int j=1; j<5; ++j) {
				DrawJoint(hand[f, j].position);
			}
		}
		DrawJoint(hand[4, 0].position);

		// Draw basic finger cylinders
		for(int f=0; f<5; ++f) {
			for(int j=1; j<4; ++j) {
				DrawCylinder(hand[f, j].position, hand[f, j+1].position);
			}
		}
		DrawCylinder(hand[0, 0].position, hand[4, 0].position);
		DrawCylinder(hand[0, 0].position, hand[1, 1].position);
		DrawCylinder(hand[4, 0].position, hand[4, 1].position);

		DrawCylinder(hand[1, 1].position, hand[2, 1].position);
		DrawCylinder(hand[2, 1].position, hand[3, 1].position);
		DrawCylinder(hand[3, 1].position, hand[4, 1].position);

		// Draw palm
		sphereMesh.Draw(Material.Default, Matrix.TS(hand.palm.position, palmRadius), color);
	}

	private void DrawJoint(Vec3 pos) {
		sphereMesh.Draw(Material.Default, Matrix.TS(pos, jointRadius), color);
	}

	private void DrawCylinder(Vec3 from, Vec3 to) {
		Vec3 pos = (from + to) / 2;
		Quat rot = Quat.LookAt(from, to);
		float length = Vec3.Distance(from, to);
		Vec3 scl = new Vec3(cylinderRadius, cylinderRadius, length);

		cylinderMesh.Draw(Material.Default, Matrix.TRS(pos, rot, scl));
	}
};