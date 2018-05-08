using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
//using System.Linq;
//using System;

#if UNITY_EDITOR

using UnityEditor;
#endif

//[CustomEditor (typeof(HexGrid))]

#if UNITY_EDITOR_OSX

public class EditorTools : Editor {

//	[MenuItem("Edit/Editor Tools/Rename Children")]
//	public static void CombineChildren(){
//		foreach(GameObject o in Selection.gameObjects){
//			o.GetComponent<CombineChildren>().
//		}
//	}


//	[MenuItem("Molecules/Spiral")]
//	public static void Spiral()
//	{
//		Vector3[] pts = Utils2.Spiral ();
//		for(int i=0;i<pts.Length;i++){
//			GameObject s = GameObject.CreatePrimitive (PrimitiveType.Sphere);
//			s.name = "debugg";
//			s.transform.position = pts [i];
//		}
//	}

	[MenuItem("Molecules/Twist")]
	public static void Twist()
	{
		foreach(PolymerTwist tw in FindObjectsOfType<PolymerTwist>()){
			tw.Twist ();
		}
	}

	[MenuItem("Molecules/Build Membrane")]
	public static void BuildMembrane()
	{
		foreach(Membrane m in FindObjectsOfType<Membrane> ()){
			m.BuildNow ();
		}
	}


	[MenuItem("Molecules/Build Ring")]
	public static void BuildRing()
	{
		FindObjectOfType<Ring> ().BuildNow ();
	}

	[MenuItem("Molecules/Stick To Surface Above")]
	public static void StickToSurfaceAbove()
	{
		foreach (GameObject o in Selection.gameObjects) {
			RaycastHit h = new RaycastHit ();
			Ray ray = new Ray (o.transform.position, Vector3.up);
			if (Physics.Raycast (ray, out h)) {
				o.transform.position = h.point;
			} 
		}
		FindObjectOfType<Ring> ().BuildNow ();
	}

//
//	[MenuItem("Hack/Scene Tool/Set/Translate")]
//	public static void SetSceneToolTranslate()
//	{
//		SceneToolHacker.CurrentTool = SceneToolHacker.Tool.Translate;
//	}
//	[MenuItem("Hack/Scene Tool/Set/Rotate")]
//	public static void SetSceneToolRotate()
//	{
//		SceneToolHacker.CurrentTool = SceneToolHacker.Tool.Rotate;
//	}
//	[MenuItem("Hack/Scene Tool/Set/Scale _r")]
//	public static void SetSceneToolScale()
//	{
//		SceneToolHacker.CurrentTool = SceneToolHacker.Tool.Scale;
//	}
//

	[MenuItem("Edit/Editor Tools/Combine Meshes")]
	public static void CombineMeshes(){
//		List<Mesh> meshes = new List<Mesh>();
//		foreach(MeshFilter mf in Selection.activeGameObject.GetComponentsInChildren<MeshFilter>()){
//			meshes.Add(mf.sharedMesh);
//		}
		Mesh combined = new Mesh();
		List<Mesh> meshes = new List<Mesh>();
		foreach(MeshFilter mf1 in Selection.activeGameObject.GetComponentsInChildren<MeshFilter>()){
			meshes.Add(MeshCombiner.mergedMesh(mf1.sharedMesh));
//			o.GetComponent<MeshFilter>().mesh = newMesh;
//			o.GetComponent<Renderer>().materials = new Material[1];
		}
		combined = MeshCombiner.mergedMeshes(meshes.ToArray());
		MeshFilter mf = Selection.activeGameObject.AddComponent<MeshFilter>();
		mf.mesh = combined;
		MeshRenderer r = Selection.activeGameObject.AddComponent<MeshRenderer>();
//		r.material = FindObjectOfType<EffectsManager>().spongeMaterials[0];
		r.material = new Material(Shader.Find("Standard"));

		List<Transform> toDel = new List<Transform>();
		foreach(Transform t in Selection.activeTransform){
			toDel.Add(t);
		}
		foreach(Transform t in toDel){
			DestroyImmediate(t.gameObject);
		}
//		foreach(Transform t in Selection.activeGameObject.transform){
//			DestroyImmediate(t);
//		}
//		foreach(GameObject o in Selection.activeGameObject.GetComponentsInChildren<GameObject>()){
//			
//		}
	}

	[MenuItem("Edit/Editor Tools/Triangle Count")]
	public static void TriangleCount(){
		Debug.Log("<color=#0f0>"+Selection.activeGameObject.GetComponent<MeshFilter>().mesh.triangles.Length/3f+"</color> triangles");
	}




	[MenuItem("Edit/Editor Tools/Make Group %e")]
	public static void MakeGroup() {
		EditorApplication.SaveScene();
		if (!Selection.activeGameObject) return;
		Undo.IncrementCurrentGroup();
		Undo.RegisterSceneUndo("Make Group");
		
		List<Object> toGroup = new List<Object>();
		foreach(GameObject g in Selection.gameObjects) {
			GameObject f = g;
//			while(f.GetComponent<HexGridPiece>() == null && f.transform.parent != null) {
//				f = f.transform.parent.gameObject;
//			}
			toGroup.Add(f);
		}

//		while (Selection.activeGameObject.name.Contains("(Clone)")){
//			Selection.activeGameObject.name = 
//		}
		GameObject newParent = new GameObject(Selection.activeGameObject.name + " group");
		Selection.objects = toGroup.ToArray();

		Transform root = Selection.activeTransform.parent;
		newParent.transform.parent = root;
		Vector3 center = Vector3.zero;
		foreach(GameObject g in Selection.gameObjects) {
			center += g.transform.position;
		}
		center /= Selection.gameObjects.Length;
		Vector3 middlestCenter = Vector3.zero;
		float closestDist = float.PositiveInfinity;
		foreach(GameObject g in Selection.gameObjects) {
			float d = (g.transform.position - center).sqrMagnitude;
			if(d < closestDist) {
				closestDist = d;
				middlestCenter = g.transform.position;
			}
		}
		newParent.transform.position = middlestCenter;
		newParent.transform.position = center;
		foreach(GameObject g in Selection.gameObjects) {
			g.transform.parent = newParent.transform;
		}	
		Selection.activeGameObject = newParent;

	}	
	
//	[MenuItem("Edit/Editor Tools/Ungroup %#e")]
	public static void Ungroup() {
		Undo.IncrementCurrentGroup();
		Undo.RegisterSceneUndo("Ungroup");
		List<Object> newSel = new List<Object>();
		foreach(GameObject g in Selection.gameObjects) {
			while(g.transform.childCount > 0) {
				GameObject child = g.transform.GetChild(0).gameObject;
				newSel.Add(child);
				child.transform.parent = g.transform.parent;

			}
			DestroyImmediate(g);
		}
		Selection.objects = newSel.ToArray();
	}

	[MenuItem("Edit/Editor Tools/Count Vertex")]
	public static void CountVertex() {

		// commented Debug.Log ("vertex on "+Selection.activeObject.name+": "+Selection.activeGameObject.GetComponent<MeshFilter>().sharedMesh.vertices.Length);

	}

	[MenuItem("Edit/Editor Tools/Count Collider")]
	public static void CountCollider() {
//		// commented Debug.Log (FindObjectsOfType<Collider>().Length);
//		// commented Debug.Log ("vertex on "+Selection.activeObject.name+": "+Selection.activeGameObject.GetComponent<MeshFilter>().sharedMesh.vertices.Length);
		
	}


	[MenuItem("Edit/Editor Tools/Draw Spheres On Verts")]
	public static void DrawSpheresOnVerts(){
		Mesh m = Selection.activeGameObject.GetComponent<MeshFilter>().sharedMesh;

		for (int i=0;i<m.vertices.Length;i++){
			Vector3 p = Selection.activeTransform.TransformPoint(m.vertices[i]);
			DebugSphere("vert " +i.ToString(),p).transform.parent = Selection.activeTransform;
		}
	}


	static GameObject DebugSphere(string n,Vector3 p){
		GameObject debugSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		DestroyImmediate (debugSphere.GetComponent<Collider>());
		debugSphere.transform.localScale = Vector3.one * .2f;
		debugSphere.name = n;
		debugSphere.transform.position = p;
		return debugSphere;
	}

	[MenuItem("Edit/Editor Tools/Debug CSV")]
	public static void DebugCSV() {
		string s="";
		foreach(Transform t in Selection.activeTransform){
			s += Regex.Match(t.name, @"\d+").Value +",";
//			int i=0;
//			if (int.TryParse(t.name,out i)){
//				s += i.ToString();
//			}
		}
		// commented Debug.Log (s);
	}



	[MenuItem("Edit/Editor Tools/Select Parents %g")]
	public static void SelectParents() {
//		DeleteAllHGP();
		EditorApplication.SaveScene();
		List<Object> newSelection = new List<Object>();
		foreach(GameObject item in Selection.gameObjects) {
			Transform t = item.transform;
			//			Transform root = GameObject.Find(".gridRoot").transform;
			if(t.parent != null) {
				t = t.parent;
			}
			//			while(t.parent != null){// && t.GetComponent<HexGridPiece>() == null && t.GetComponent<NumberInfo>() == null && t.GetComponent<NumberStructureCreator>() == null) {
			//				t = t.parent;
			//			}
			newSelection.Add(t.gameObject);
		}
		Selection.objects = newSelection.ToArray();
	}
	
	
	[MenuItem("Edit/Editor Tools/Rotate Ninety %#u")]
	public static void RotateSixty(){
		EditorApplication.SaveScene();
		Quaternion rot = Selection.activeTransform.rotation;
		rot.eulerAngles += new Vector3(0,90,0);
		Selection.activeTransform.rotation = rot;
	}
	
	[MenuItem("Edit/Editor Tools/Rotate Sixty %u")]
	public static void RotateNegSixty(){
		EditorApplication.SaveScene();
		foreach(Transform t in Selection.transforms){
			Quaternion rot = t.rotation;
			rot.eulerAngles += new Vector3(0,60,0);
			t.rotation = rot;
		}
		
	}
	
	
//	[MenuItem("Edit/Editor Tools/Populate Button Titles")]
//	public static void PopulateLevelBuilderButtonTitles() {
//		foreach(LevelBuilderUIButton but in Resources.FindObjectsOfTypeAll<LevelBuilderUIButton>()){
//			but.GetComponent<UIHoverHelp>().title = but.levelPiecePrefab.GetComponent<UserEditableObject>().myName;
//		}
//	}
	
	[MenuItem("Edit/Editor Tools/Set Local Position To Zero %#y")]
	public static void SetLocalPositionToZero(){
		foreach(GameObject o in Selection.gameObjects){
			if (o.GetComponent<RectTransform>()){
				o.GetComponent<RectTransform>().localPosition = Vector3.zero;
			} else {
				o.transform.localPosition = Vector3.zero;
			}
		}
	}	
	
	static int posInt=0;
	[MenuItem("Edit/Editor Tools/Set Rotation To Zero2 %y")]
	public static void SetRotationToZero(){
		foreach(GameObject o in Selection.objects){
//			Debug.Log("rot zero");
			switch (posInt % 4 ){
			case 0: 
				o.transform.localRotation = Quaternion.identity;
				break;
			case 1:
				Quaternion newRot2 = new Quaternion();
				newRot2.eulerAngles=new Vector3(270,0,0);
				o.transform.localRotation = newRot2;
				break;
			case 2:
				o.transform.rotation = Quaternion.identity;
				break;
			case 3:
				Quaternion newRot = new Quaternion();
				newRot.eulerAngles=new Vector3(270,0,0);
				o.transform.rotation = newRot;
				
				break;
			default:break;
			}
		}
		
		posInt++;
	}
	
	
	
	[MenuItem("Edit/Editor Tools/Select Children %#g")]
	public static void SelectChildren() {
		//		List<Object> newSel = new List<Object>();
		//		int maxSelect = 10000;
		//		foreach(Transform selT in Selection.transforms) {
		//			foreach(Transform t in selT) {
		//				newSel.Add(t.gameObject);
		//				if(maxSelect < 0) { break; }
		//				maxSelect--;
		//			}
		//		}
		//		Selection.objects = newSel.ToArray();
		GameObject newParent = new GameObject();
		newParent.transform.parent = Selection.activeTransform.parent;
		foreach(Transform t in Selection.transforms){
			t.parent = newParent.transform;
		}
	}



	static Color l_color;
	static float l_intensity;
	static float l_bounceIntensity;
	static LightShadows l_shadows = LightShadows.Hard;
	static Color a_color;
	static float a_intensity;
//	static float 






	[MenuItem("Edit/Editor Tools/Save Lighting Values")]
	public static void SaveLightingValues() {
		Light l = GameObject.Find ("Directional light").GetComponent<Light>();
		l_color = l.color;
		l_intensity = l.intensity;
		l_bounceIntensity = l.bounceIntensity;
		a_color = RenderSettings.ambientLight;
		a_intensity = RenderSettings.ambientIntensity;
	}


	[MenuItem("Edit/Editor Tools/Fix Lighting")]
	public static void FixLightingInScenes() {
		SaveLightingValues();
		foreach(EditorBuildSettingsScene scene in UnityEditor.EditorBuildSettings.scenes){
			FixLighting (scene);
		}
	}
	
	static void FixLighting(EditorBuildSettingsScene scene){
		EditorApplication.OpenScene(scene.path);
		Light l = GameObject.Find ("Directional light").GetComponent<Light>();
		l.color = l_color;
		l.intensity = l_intensity;
		l.bounceIntensity = l_bounceIntensity;
		RenderSettings.ambientLight = a_color;
		RenderSettings.ambientIntensity = a_intensity;
	}

	
	[MenuItem("Edit/Editor Tools/Clear Trees")]
	public static void ClearTrees() {
		Terrain ter = Selection.activeGameObject.GetComponent<Terrain>();
		List<TreeInstance> tree = new List<TreeInstance>(ter.terrainData.treeInstances);
		tree.Clear();
		ter.terrainData.treeInstances = tree.ToArray();
	}

	[MenuItem("Edit/Editor Tools/Toggle Foggle &f")]
	public static void ToggleFoggle() {
		RenderSettings.fog = !RenderSettings.fog;
		// commented Debug.Log ("FOG:"+RenderSettings.fog);
	}


	[MenuItem("Edit/Editor Tools/Add Backface UVs")]
	public static void AddBackfaceUVs(){
		Mesh mesh = Selection.activeGameObject.GetComponent<MeshFilter>().sharedMesh;
		int oldVertCount  = mesh.vertices.Length;
		Vector3[] vertices = mesh.vertices;
		Vector2[] uv = mesh.uv;
		Vector3[] normals = mesh.normals;
		int szV = vertices.Length;
		Vector3[] newVerts = new Vector3[szV*2];
		Vector2[] newUv = new Vector2[szV*2];
		Vector3[] newNorms = new Vector3[szV*2];
		int j=0;
		for (j=0; j< szV; j++){
			// duplicate vertices and uvs:
			newVerts[j] = newVerts[j+szV] = vertices[j];
			newUv[j] = newUv[j+szV] = uv[j];
			// copy the original normals...
			newNorms[j] = normals[j];
			// and revert the new ones
			newNorms[j+szV] = -normals[j];
		}
		int[] triangles = mesh.triangles;
		int szT = triangles.Length;
		int[] newTris = new int[szT*2]; // double the triangles
		for (int i=0; i< szT; i+=3){
			// copy the original triangle
			newTris[i] = triangles[i];
			newTris[i+1] = triangles[i+1];
			newTris[i+2] = triangles[i+2];
			// save the new reversed triangle
			j = i+szT; 
			newTris[j] = triangles[i]+szV;
			newTris[j+2] = triangles[i+1]+szV;
			newTris[j+1] = triangles[i+2]+szV;
		}
		mesh.vertices = newVerts;
		mesh.uv = newUv;
		mesh.normals = newNorms;
		mesh.triangles = newTris; // assign triangles last!
		int newVertCount  = mesh.vertices.Length;
		// commented Debug.Log ("added backface UVs to:" +Selection.activeGameObject+". Vertex count old / new: " +oldVertCount +" / " +newVertCount);
	}
	[MenuItem("Edit/Terrain Generator/Drop To Terrain")]
	static float DropToTerrain(GameObject o){
		// grab verts
		
		Mesh mesh = o.GetComponent<MeshFilter>().mesh;
		Transform t = o.transform;
		
		Vector3[] vertices = mesh.vertices;
		
		float baseHeight = 0;
		for (int i = 0; i < vertices.Length; i++){
			foreach(RaycastHit hitt in Physics.RaycastAll(new Ray(t.TransformPoint(vertices[i]),Vector3.down))){
				if (hitt.collider.name == "plane"){
					if (hitt.distance > baseHeight) baseHeight = hitt.distance;
					break;
				}
			}
		}
		
		
		
		//		for (int i = 0; i < 20; i++){
		for (int i = 0; i < vertices.Length; i++){
			
			RaycastHit[] hits;
			Vector3 raypos = t.TransformPoint(vertices[i]);
			//			// commented Debug.Log("start ray at"+raypos);
			//			DebugSphere(raypos,i+"_raypos_s"tart",t);
			hits = Physics.RaycastAll(new Ray(raypos,Vector3.down));
			foreach(RaycastHit hit in hits){
				
				if (hit.collider.transform.root != t.root) {
					//				if (hit.collider.name.Contains("plane")){
					// The distance from the vert to the ground is
					float dist = hit.distance;
					//					DebugSphere(hit.point,i+"_raypos_end",t);
					// todo unity bug hit distance sometimes gives SCALED distance? e.g. the localscale of this mesh is 5, hit distance will be x * 5. So fucking annoying. Only does it sometimes which is WORSE.
					dist = Vector3.Distance(hit.point,raypos);
					// distance from vert to center of mesh (shape)
					float diff = (vertices[i].z  - mesh.bounds.extents.z);
					// corrected dist and baseheight for scale of 5
					Vector3 result = vertices[i] - t.InverseTransformDirection(new Vector3(0,(dist-baseHeight)/5f-diff,0));
					// commented Debug.Log (i+": dist: " + dist+"; diff: " +diff +"l orig: "+vertices[i]+"; result:" + result);
					vertices[i] = result;
					
					//					vertices[i] = t.InverseTransformPoint(hit.point + diff + baseHeight);
					break;
				}
			}
		}
		mesh.vertices = vertices;
		mesh.RecalculateBounds();
		
		float minHeight = Mathf.Infinity;
		for (int i = 0; i < vertices.Length; i++){
			foreach(RaycastHit hitt in Physics.RaycastAll(new Ray(t.TransformPoint(vertices[i]),Vector3.down))){
				if (hitt.collider.name == "plane"){
					if (hitt.distance < minHeight) minHeight = hitt.distance;
					break;
				}
			}
		}
		
		//		float baseHeight = 0;
		for (int i = 0; i < vertices.Length; i++){
			foreach(RaycastHit hitt in Physics.RaycastAll(new Ray(t.TransformPoint(vertices[i]),Vector3.down))){
				if (hitt.collider.name == "plane"){
					if (hitt.distance > baseHeight) baseHeight = hitt.distance;
					break;
				}
			}
		}
		return baseHeight;
		
		
		// commented Debug.Log ("baseheight (max,min):"  +baseHeight+","+minHeight+" on "+t.name);
		return minHeight;
	}


	[MenuItem("Edit/Editor Tools/Terrain Material")]
	public static void TerrainMaterial() {
		
		foreach(Terrain t in FindObjectsOfType<Terrain>()){
			t.materialType = Terrain.MaterialType.BuiltInLegacyDiffuse;
		}
	}



	static void SetButtonColor(GameObject o, Color c){
		UnityEngine.UI.ColorBlock colors = new ColorBlock();
		colors.normalColor = Color.white;
		colors.highlightedColor = juneYellow;
		colors.pressedColor = new Color(0.5f,0.5f,0.5f,1);
		colors.disabledColor = Color.gray;
		colors.colorMultiplier = 1;
		o.GetComponent<Button>().colors = colors;
		foreach(Image i in o.GetComponentsInChildren<Image>()){
			i.color = Color.black;
		}
		o.GetComponentInChildren<Text>().color = Color.black;
//		o.GetComponentInChildren<Text>().fontStyle = FontStyle.Normal;
		o.GetComponent<Image>().color = c;
//		if (o.GetComponentInChildren<Image>()){
//			o.GetComponentInChildren<Image>().color = Color.black;
//			// commented Debug.Log("col:"+o.GetComponentInChildren<Image>().color);
//		}
	}

	static int colorCycle = 0;
	[MenuItem("Edit/Editor Tools/Print level json")]// _p")]
	public static void ApplyButtonPropertiesSmall(){
//		// commented Debug.Log(PlayerPrefs.GetString("tempLevelJson"));

	}

	static Color juneYellow = new Color(0.976f,0.8f,0,1);
	static void SetButtonProps(GameObject o, Sprite sprite, string sizeString, bool outline = false){
		o.transform.localScale = Vector3.one;
		o.GetComponent<Image>().sprite = sprite; 
		o.GetComponent<Image>().type = Image.Type.Sliced;

		colorCycle ++;
//		// commented Debug.Log("col cy:"+colorCycle);
		switch(colorCycle){
		case 0: SetButtonColor(o,Color.black); break;
		case 1: SetButtonColor(o, new Color(0.35f,0.35f,1,1)); break;
		case 2: SetButtonColor(o,new Color(0,.8f,0)); break; // Green
		case 3: SetButtonColor(o,juneYellow); break; // Yellow
		case 4: SetButtonColor(o,new Color(0.7f,0.7f,0.7f,1)); break;
		case 5: SetButtonColor(o,new Color(1,.4f,.9f)); break; // Pink
		case 6: SetButtonColor(o,new Color(0,0,0,0)); break; // Clear
		case 7: SetButtonColor(o,Color.white); break; // White
		case 8: colorCycle = 0; SetButtonColor(o,new Color(0.8f,0,0)); break; // Red
		default: colorCycle = 0; break;
		}
//		if (outline) {
//			SetButtonColor(o,new Color(0,0,0,0));
////			o.AddComponent<Outline>();
//
//		}
//		// commented Debug.Log(Color.gray);

		Text t = o.GetComponentInChildren<Text>();
		t.fontStyle = FontStyle.Normal;
		int buttonSizeX = 0;
		Vector2 size = Vector2.zero;
		if (sizeString == "small"){
			buttonSizeX = int.Parse((t.text.Length * 11).ToString()) + 30;
			t.fontSize = 60;

			size = new Vector2(buttonSizeX,30);
			t.transform.localScale = Vector3.one * 0.25f;
		} else if (sizeString == "big") {
			buttonSizeX = int.Parse((t.text.Length * 17).ToString()) + 30;
			t.fontSize = 60;
			t.transform.localScale = Vector3.one * 0.35f;
			size = new Vector2(buttonSizeX,45);
		}
		t.font = (Font)Resources.Load("Fonts/Durotype - Aspira-Medium");
		t.fontStyle = FontStyle.Bold;
//		t.fontSize = fontsize;
//		t.transform.localScale = Vector3.one * 0.5f;
		t.horizontalOverflow = HorizontalWrapMode.Overflow;
		t.verticalOverflow = VerticalWrapMode.Overflow;
		Outline oo = Selection.activeGameObject.GetComponent<Outline>(); 
		if (oo) DestroyImmediate(oo);
//		if (!oo) oo = Selection.activeGameObject.AddComponent<Outline>();
//		oo.effectColor = Color.white;
//		oo.effectDistance = new Vector2(2,-2);
		Selection.activeGameObject.GetComponent<RectTransform>().sizeDelta = size;
		o.transform.position += Vector3.up;
		o.transform.position += Vector3.down;
	}



//	static bool bold = false;
	[MenuItem("Edit/Editor Tools/Set Font Aspira")]
	public static void SetFontAspira() {
		
//		bold = !bold;
		foreach(GameObject o in Selection.gameObjects){
			Text t = o.GetComponent<Text>();
			if (t){
				t.font = (Font)Resources.Load("Durotype - Aspira-Medium hinted sharp (gui)");
//				t.fontStyle = bold ? FontStyle.Bold : FontStyle.Normal; 
				t.fontStyle = FontStyle.Normal;
				t.gameObject.transform.position += Vector3.one;
				t.gameObject.transform.position -= Vector3.one;
				t.transform.localScale = Vector3.one * 0.5f;
				t.fontSize = t.transform.parent.localScale.x == 1 ? 28 : 100;

			}
		}

	}

	[MenuItem("Edit/Editor Tools/Tab Swap &%t")]
	public static void TabSwap() {
//		FindObjectOfType<LevelBuilderTabManager>().SwapNextTab();
	}

//	[MenuItem("Edit/Editor Tools/Simple Replacer")]
//	public static void SimpleReplacer() {
//		foreach(SimpleReplace sr in FindObjectsOfType<SimpleReplace>()){
//			GameObject replace = (GameObject)Instantiate(sr.replacement);
//			replace.transform.SetParent(sr.transform);
//			replace.transform.localPosition = Vector3.zero;
//			replace.transform.localRotation = Quaternion.identity;
//			replace.transform.SetParent(sr.transform.parent);
//			replace.transform.localScale = sr.transform.localScale;
//			DestroyImmediate(sr.gameObject);
//		}	
//	}

	[MenuItem("Edit/Editor Tools/Replace Arial Fonts")]
	public static void ReplaceArialFonts() {
		foreach(Text t in Resources.FindObjectsOfTypeAll<Text>()){
//			if (t.font.ToString().Contains("Arial") ){ //|| t.font.ToString().Contains("OpenSans")){
//			if (t.font.ToString().Contains("Candara")){
			t.font = (Font)Resources.Load("Fonts/Durotype - Aspira-Medium hinted sharp (gui)") as Font;
			t.name += " moveme? ";
//				// commented Debug.Log("font:"+t.font+" on "+t.gameObject.name);
		}

	}

	[MenuItem("Edit/Editor Tools/Fix Font Size")]
	public static void FixFontSize() {
		foreach(Text t in Resources.FindObjectsOfTypeAll<Text>()){
			
			if (t.fontSize == 28  && t.transform.localScale.x == 0.5f){
				t.fontSize = 19;
				t.transform.localScale = Vector3.one * 0.75f;
			}
//			t.name += " moveme? ";
			//				// commented Debug.Log("font:"+t.font+" on "+t.gameObject.name);
		}

	}


	static long lastSaveTime = 0;
	[MenuItem("Edit/Editor Tools/Toggle Gameobject Active _a")]
	public static void ToggleGameObjectActive()
	{
		if (System.DateTime.Today.ToFileTimeUtc() > lastSaveTime + (long)60) {
			lastSaveTime = System.DateTime.Today.ToFileTimeUtc ();
			EditorApplication.SaveScene();
		}
		bool f = Selection.activeGameObject.activeSelf;
		foreach(GameObject o in Selection.gameObjects){
			o.SetActive(!f);
		}
	}

//	[MenuItem("Edit/Editor Tools/Set Vector3.one Scale _s")]
//	public static void SetVector3OneScale()
//	{
//		EditorApplication.SaveScene();
//		foreach(GameObject o in Selection.gameObjects){
//			o.transform.localScale = Vector3.one;
//		}
//	}

	static float scaleMod = 0.5f;
	[MenuItem("Edit/Editor Tools/Set alt Scale _#s")]
	public static void SetAltScale()
	{
		EditorApplication.SaveScene();
		foreach(GameObject o in Selection.gameObjects){
			o.transform.localScale = Vector3.one * scaleMod;
		}
		scaleMod = scaleMod == 0.5f ? 0.25f : 0.5f;
	}

	[MenuItem("Edit/Editor Tools/Clear Player Prefs")]
	public static void ClearPlayerPrefs()
	{
		PlayerPrefs.DeleteAll();
	}


	[MenuItem("Edit/Editor Tools/Find Empty Meshes")]
	public static void FindEmptyMeshes() {
		foreach(MeshFilter m in FindObjectsOfType<MeshFilter>()){
			if (m.sharedMesh == null){
				// commented Debug.Log("m:"+m.name);
			}
		}

		foreach(SkinnedMeshRenderer m in FindObjectsOfType<SkinnedMeshRenderer>()){
//			if (m.shared == null){
				// commented Debug.Log("m:"+m.name);
//			}
		}
	}


	[MenuItem("Edit/Editor Tools/Calculate Radial")]
	public static void CalculateRadial()
	{
		Transform transform = Selection.activeTransform;
		if (transform.childCount == 0)
			return;
		float MinAngle = 0;
		float MaxAngle = 360;
		float fOffsetAngle = ((MaxAngle - MinAngle)) / (transform.childCount);
		float fDistance = 80;
		float fAngle = 0;
		for (int i = 0; i < transform.childCount; i++)
		{
			RectTransform child = (RectTransform)transform.GetChild(i);
			if (child != null)
			{
				//Adding the elements to the tracker stops the user from modifiying their positions via the editor.
				Vector3 vPos = new Vector3(Mathf.Cos(fAngle * Mathf.Deg2Rad), Mathf.Sin(fAngle * Mathf.Deg2Rad), 0);
				child.localPosition = vPos * fDistance;
				//Force objects to be center aligned, this can be changed however I'd suggest you keep all of the objects with the same anchor points.
				child.anchorMin = child.anchorMax = child.pivot = new Vector2(0.5f, 0.5f);
				fAngle += fOffsetAngle;
			}
		}

	}


	[MenuItem("Edit/Editor Tools/Generate Terrain Skirt")]
	public static void GenerateTerrainSkirt(){
		Terrain at = Selection.activeGameObject.GetComponent<Terrain>();

	//		Terrain.activeTerrain.terrainData.GetHeights(
//		Vector3[] verts = Terrain.activeTerrain.terrainData.GetHeights(
		TerrainData myTerrainData = at.terrainData;
		int res = at.terrainData.heightmapResolution;
		float scalexz = at.terrainData.size.x;
		float scaley = at.terrainData.size.y;
		float[,] allheights = myTerrainData.GetHeights(0,0,res,res);

//		PlayerPrefs.SetString("TerBak",allheights.ToString());
		for (int i=0;i<allheights.GetLength(0);i++){
			for (int j=0;j<allheights.GetLength(1);j++){
//				// commented Debug.Log("allehgitsh "+i+","+j+": "+allheights[i,j]);
				if (i < 2 || i > allheights.GetLength(0) - 3 || j < 2 || j > allheights.GetLength(1) - 3) {
					allheights[i,j] = 0;
				}
			}
		}
		myTerrainData.SetHeights(0,0,allheights);
		return;


	}
	[MenuItem("Edit/Editor Tools/Generate Terrain Ramp #r")]
	public static void GenerateTerrainRamp(){
		GameObject o1 = Selection.activeGameObject;
		MeshFilter mf = o1.GetComponent<MeshFilter>();
		Mesh m = mf.sharedMesh;
		for (int i=0;i<m.vertices.Length;i++){
			TerrainData myTerrainData = Terrain.activeTerrain.terrainData;
			int res = Terrain.activeTerrain.terrainData.heightmapResolution;
			Vector3 hScale = Terrain.activeTerrain.terrainData.heightmapScale;
			float sizeX = Terrain.activeTerrain.terrainData.size.x;
			float sizeZ = Terrain.activeTerrain.terrainData.size.z;
//			// commented Debug.Log("res:"+res+", scale:"+hScale+", hw;"+sizeX+","+sizeZ);
			Vector3 worldpos = o1.transform.TransformPoint(m.vertices[i]);
//			// commented Debug.Log("worldpos:"+worldpos);
			int xStart = Mathf.FloorToInt(worldpos.x * res / sizeX);
			int zStart = Mathf.FloorToInt(worldpos.z * res / sizeZ);
//			// commented Debug.Log("xst:"+xStart+","+zStart);
			float[,] heights = myTerrainData.GetHeights(xStart,zStart,1,1);
//			// commented Debug.Log("xz:"+xStart+","+zStart);
//			// commented Debug.Log("heights:"+heights+", len:"+heights.GetLength(0)+","+heights.GetLength(1));
			for (int j=0;j<heights.GetLength(0);j++){
				for (int k=0;k<heights.GetLength(1);k++){
					heights[j,k] = worldpos.y / hScale.y;
				}
			}
//			float[,] heights = new float[0f,worldpos.y / myTerrainData.detailHeight];
			myTerrainData.SetHeights(xStart,zStart,heights);
//			// commented Debug.Log("height "+i+": "+height);

		}


			

	}


	[MenuItem("Edit/Editor Tools/Set Tree Billboard Dist")]
	public static void SetTreeBillboardDist()
	{
		foreach(Terrain t in FindObjectsOfType<Terrain>()){
			t.treeBillboardDistance = 500;

			t.treeCrossFadeLength = 40;
			t.detailObjectDistance = 80;

		}
			
	}
		
	[MenuItem("Edit/Editor Tools/Get Compressed Size")]
	public static void GetCompressedFileSize()
	{
		string path = AssetDatabase.GetAssetPath(Selection.activeObject);
		if (!string.IsNullOrEmpty(path))
		{
			string guid = AssetDatabase.AssetPathToGUID(path);
			string p = Path.GetFullPath(Application.dataPath + "../../Library/cache/" + guid.Substring(0, 2) + "/" + guid);
			if (File.Exists(p))
			{
				var file = new FileInfo(p);
				// commented Debug.Log("Compressed file size of " + path + ": " + file.Length + " bytes");
			}
			else
			{
				// commented Debug.Log("No file found at: " + p);
			}
		}
	}










}
#endif




