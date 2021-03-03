using UnityEditor;
using UnityEngine;

[CustomEditor (typeof (TerrainGenerator))]
public class MeshEditor : Editor {

    TerrainGenerator terrainGenerator;

    public override void OnInspectorGUI () {
        DrawDefaultInspector ();

        if (GUILayout.Button ("Generate Mesh")) {
            terrainGenerator.GenerateHeightMap ();
            terrainGenerator.ContructMesh();
        }

        string numIterationsString = terrainGenerator.numErosionIterations.ToString();
        if (terrainGenerator.numErosionIterations >= 1000) {
            numIterationsString = (terrainGenerator.numErosionIterations/1000) + "k";
        }

        if (GUILayout.Button ("Erode (" + numIterationsString + " iterations)")) {
            terrainGenerator.StartErosion(numIterationsString);
        }

        if (GUILayout.Button("Export"))
        {
            terrainGenerator.Export();
        }
    }

    void OnEnable () {
        terrainGenerator = (TerrainGenerator) target;
        Tools.hidden = true;
    }

    void OnDisable () {
        Tools.hidden = false;
    }
}