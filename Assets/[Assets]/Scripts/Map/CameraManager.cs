// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// // DEPRECIATED DO NOT USE
// public class CameraManager : MonoBehaviour
// {
//     [SerializeField] MapManager MapManager;
//     // Start is called before the first frame update
//     void Start()
//     {
        
//     }

//     Vector2 oldcoordinate;
//     private void Update()
//     {
//         if (Camera.main == null || MapManager.modulesschematic == null)
//             return;

//         Vector2 coordinate = ConvertToCoordinate(Camera.main.transform.position, MapManager.gridsize);
//         if (MapManager.modulesschematic.ContainsKey(coordinate) && coordinate != oldcoordinate)
//         {
//             UpdateMesh(coordinate);
//         }
//         oldcoordinate = coordinate;
//     }

//     public static Vector2 ConvertToCoordinate(Vector3 position, int gridsize)
//     {
//         return new Vector2(Mathf.Round(position.x / gridsize), Mathf.Round(position.z / gridsize));
//     }
//     void UpdateMesh(Vector2 coordinate)
//     {
//         foreach (GameObject instancedmodule in MapManager.instancedmodules.Values)
//         {
//             instancedmodule.SetActive(false);
//         }

//         dangduclongquanCLASS.Module module = MapManager.modulesschematic[coordinate];
//         MapManager.instancedmodules[module.coordinate].SetActive(true);

//         while (module.r && MapManager.modulesschematic.ContainsKey(module.coordinate + Vector2.right))
//         {
//             module = MapManager.modulesschematic[module.coordinate+ Vector2.right];
//             MapManager.instancedmodules[module.coordinate].SetActive(true);
//             if (module.u && MapManager.modulesschematic.ContainsKey(module.coordinate + Vector2.up)) MapManager.instancedmodules[module.coordinate+Vector2.up].SetActive(true);
//             if (module.d && MapManager.modulesschematic.ContainsKey(module.coordinate + Vector2.down)) MapManager.instancedmodules[module.coordinate + Vector2.down].SetActive(true);
//         }

//         module = MapManager.modulesschematic[coordinate];
//         while (module.l && MapManager.modulesschematic.ContainsKey(module.coordinate + Vector2.left))
//         {
//             module = MapManager.modulesschematic[module.coordinate + Vector2.left];
//             MapManager.instancedmodules[module.coordinate].SetActive(true);
//             if (module.u && MapManager.modulesschematic.ContainsKey(module.coordinate + Vector2.up)) MapManager.instancedmodules[module.coordinate + Vector2.up].SetActive(true);
//             if (module.d && MapManager.modulesschematic.ContainsKey(module.coordinate + Vector2.down)) MapManager.instancedmodules[module.coordinate + Vector2.down].SetActive(true);
//         }

//         module = MapManager.modulesschematic[coordinate];
//         while (module.u && MapManager.modulesschematic.ContainsKey(module.coordinate + Vector2.up))
//         {
//             module = MapManager.modulesschematic[module.coordinate + Vector2.up];
//             MapManager.instancedmodules[module.coordinate].SetActive(true);
//             if (module.r && MapManager.modulesschematic.ContainsKey(module.coordinate + Vector2.right)) MapManager.instancedmodules[module.coordinate + Vector2.right].SetActive(true);
//             if (module.l && MapManager.modulesschematic.ContainsKey(module.coordinate + Vector2.left)) MapManager.instancedmodules[module.coordinate + Vector2.left].SetActive(true);
//         }

//         module = MapManager.modulesschematic[coordinate];
//         while (module.d && MapManager.modulesschematic.ContainsKey(module.coordinate + Vector2.down))
//         {
//             module = MapManager.modulesschematic[module.coordinate + Vector2.down];
//             MapManager.instancedmodules[module.coordinate].SetActive(true);
//             if (module.r && MapManager.modulesschematic.ContainsKey(module.coordinate + Vector2.right)) MapManager.instancedmodules[module.coordinate + Vector2.right].SetActive(true);
//             if (module.l && MapManager.modulesschematic.ContainsKey(module.coordinate + Vector2.left)) MapManager.instancedmodules[module.coordinate + Vector2.left].SetActive(true);
//         }
//     }
// }
