using StereoKit;

public class GracinishCube {
	public float size = 0.2f;
	public float border = .01f;
	public int crossCount = 5;

	private Pose pose = Pose.Identity;
	private Material material;
	private String id;

	public GracinishCube(Pose pose) {
		id = Guid.NewGuid().ToString();
		this.pose = pose;
		material = Default.MaterialUIBox.Copy();
		material["border_size"] = border;
		material["border_affect_radius"] = .0f;
	}

	public void Step() {
		UI.HandleBegin(id, ref pose, new Bounds(Vec3.One * size));
		Mesh.Cube.Draw(material, Matrix.S(size));
		
		float crossSpacing = size / crossCount;
		for(int x=-crossCount/2; x<=crossCount/2; ++x) {
			for(int y=-crossCount/2; y<=crossCount/2; ++y) {
				for(int z=-crossCount/2; z<=crossCount/2; ++z) {
					DrawCross(crossSpacing * new Vec3(x, y, z), 0.0025f, 0.015f, Material.Unlit);
				}
			}
		}

		UI.HandleEnd();
	}

	public void DrawCross(Vec3 pos, float thickness, float size, Material material) {
		Mesh.Cube.Draw(material, Matrix.TS(pos, new Vec3(size, thickness, thickness)));
		Mesh.Cube.Draw(material, Matrix.TS(pos, new Vec3(thickness, size, thickness)));
		Mesh.Cube.Draw(material, Matrix.TS(pos, new Vec3(thickness, thickness, size)));
	}
}
