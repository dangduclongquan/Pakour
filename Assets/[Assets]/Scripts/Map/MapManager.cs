// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using System.IO;
// using Photon.Pun;
// using Photon.Realtime;

// // DEPRECIATED WARNING. DO NOT USE
// // TODO: MAKE A NEW MAP GEN COMPLETELY
// public class MapManager : MonoBehaviourPunCallbacks
// {
//     public Transform startlocation;
//     [SerializeField] NavMeshBaker NavMeshBaker;

//     [SerializeField] Transform startarea;
//     [SerializeField] Transform directory;

//     [SerializeField] GameObject straight;
//     [SerializeField] GameObject turn;
//     [SerializeField] GameObject inter3;
//     [SerializeField] GameObject inter4;
//     [SerializeField] GameObject[] rooms;
//     [SerializeField] int _gridsize;
//     public int maxmodulecount;
//     public float internallinkchance;
//     public int iterationcount;

//     public int gridsize { get { return _gridsize; } }

//     public SortedList<Vector2, dangduclongquanCLASS.Module> modulesschematic { get; private set; }
//     public SortedList<Vector2, GameObject> instancedmodules { get; private set; }

//     Queue<dangduclongquanCLASS.Module> queue;

//     PhotonView photonview;
//     private void Awake()
//     {
//         photonview = GetComponent<PhotonView>();
//     }

//     public void BuildRandomMap()
//     {
//         if (!PhotonNetwork.IsMasterClient) return;

//         List<string> roomnames = new List<string>();
//         foreach (GameObject room in rooms)
//         {
//             roomnames.Add(room.gameObject.name);
//         }

//         SortedList<Vector2, dangduclongquanCLASS.Module> randommodules = dangduclongquanCLASS.GetRandomMap2(maxmodulecount, internallinkchance, iterationcount, roomnames);

//         List<string> modulenames = new List<string>();
//         List<Vector2> coordinates = new List<Vector2>();
//         List<bool> u = new List<bool>();
//         List<bool> d = new List<bool>();
//         List<bool> l = new List<bool>();
//         List<bool> r = new List<bool>();

//         foreach (dangduclongquanCLASS.Module module in randommodules.Values)
//         {
//             modulenames.Add(module.name);
//             coordinates.Add(module.coordinate);
//             u.Add(module.u);
//             d.Add(module.d);
//             l.Add(module.l);
//             r.Add(module.r);
//         }

//         photonview.RPC("UpdateMap", RpcTarget.All, (object)modulenames.ToArray(), (object)coordinates.ToArray(), (object)u.ToArray(), (object)d.ToArray(), (object)l.ToArray(), (object)r.ToArray());
//     }
//     public override void OnPlayerEnteredRoom(Player newPlayer)
//     {
//         base.OnPlayerEnteredRoom(newPlayer);
//         if(PhotonNetwork.IsMasterClient)
//         {
//             List<string> modulenames = new List<string>();
//             List<Vector2> coordinates = new List<Vector2>();
//             List<bool> u = new List<bool>();
//             List<bool> d = new List<bool>();
//             List<bool> l = new List<bool>();
//             List<bool> r = new List<bool>();

//             foreach (dangduclongquanCLASS.Module module in modulesschematic.Values)
//             {
//                 modulenames.Add(module.name);
//                 coordinates.Add(module.coordinate);
//                 u.Add(module.u);
//                 d.Add(module.d);
//                 l.Add(module.l);
//                 r.Add(module.r);
//             }

//             photonview.RPC("UpdateMap", newPlayer, (object)modulenames.ToArray(), (object)coordinates.ToArray(), (object)u.ToArray(), (object)d.ToArray(), (object)l.ToArray(), (object)r.ToArray());
//         }

//     }

//     /*
//     private void OnEnable()
//     {
//         RoomManager.GameStartingEvent += OnGameStarting;
//     }
//     private void OnDisable()
//     {
//         RoomManager.GameStartingEvent -= OnGameStarting;
//     }
//     void OnGameStarting()
//     {
//         if (!PhotonNetwork.IsMasterClient) return;

//         if (instancedmodules != null)
//         {
//             foreach (GameObject go in instancedmodules.Values)
//             {
//                 Destroy(go);
//             }
//         }
//         List<string> roomnames = new List<string>();
//         foreach (GameObject room in rooms)
//         {
//             roomnames.Add(room.gameObject.name);
//         }

//         SortedList<Vector2, dangduclongquanCLASS.Module> randommodules = dangduclongquanCLASS.GetRandomMap2(maxmodulecount, internallinkchance, iterationcount, roomnames);

//         List<string> modulenames = new List<string>();
//         List<Vector2> coordinates = new List<Vector2>();
//         List<bool> u = new List<bool>();
//         List<bool> d = new List<bool>();
//         List<bool> l = new List<bool>();
//         List<bool> r = new List<bool>();

//         foreach (dangduclongquanCLASS.Module module in randommodules.Values)
//         {
//             modulenames.Add(module.name);
//             coordinates.Add(module.coordinate);
//             u.Add(module.u);
//             d.Add(module.d);
//             l.Add(module.l);
//             r.Add(module.r);
//         }

//         photonview.RPC("InstantiateMap", RpcTarget.All, (object)modulenames.ToArray(), (object)coordinates.ToArray(), (object)u.ToArray(), (object)d.ToArray(), (object)l.ToArray(), (object)r.ToArray());
//     }
//     */
//     [PunRPC]
//     void UpdateMap(string[] modulenames, Vector2[] coordinates, bool[] u, bool[] d, bool[] l, bool[] r, PhotonMessageInfo info)
//     {
//         if (info.Sender.IsMasterClient)
//         {
//             if (instancedmodules != null)
//             {
//                 foreach (GameObject go in instancedmodules.Values)
//                 {
//                     Destroy(go);
//                 }
//             }

//             modulesschematic = Buildup(modulenames, coordinates, u, d, l, r);
//             instancedmodules = InstantiateModules(modulesschematic, directory);
//             NavMeshBaker.BakeMavMesh();

//             //Survivor.mine.transform.position = new Vector3(modules.Keys[(modules.Count - 1) / 2].x * gridsize, -2.5f, modules.Keys[(modules.Count - 1) / 2].y * gridsize);
//             if (Survivor.mine) PhotonNetwork.DestroyPlayerObjects(Survivor.mine.controller.ActorNumber, false);
//             PhotonNetwork.Instantiate(Path.Combine("Photon Prefabs", "Survivor"), new Vector3(modulesschematic.Keys[modulesschematic.Count / 2].x * gridsize, -2.5f, modulesschematic.Keys[modulesschematic.Count / 2].y * gridsize), Quaternion.identity);
//             if (PhotonNetwork.IsMasterClient)
//                 PhotonNetwork.InstantiateRoomObject(Path.Combine("Photon Prefabs", "Unforsaken"), new Vector3(modulesschematic.Keys[modulesschematic.Count - 1].x * gridsize, 0, modulesschematic.Keys[modulesschematic.Count - 1].y * gridsize), Quaternion.identity);
//         }
//     }
//     SortedList<Vector2, GameObject> InstantiateModules(SortedList<Vector2, dangduclongquanCLASS.Module> _modules, Transform parent = null)
//     {
//         SortedList<Vector2, GameObject> imodules = new SortedList<Vector2, GameObject>(new dangduclongquanCLASS.Vector2Comparer());
//         foreach (dangduclongquanCLASS.Module module in _modules.Values)
//         {
//             GameObject go = null;

//             if (module.name != "Hallway")
//             {
//                 foreach (GameObject room in rooms)
//                 {
//                     if (room.gameObject.name == module.name)
//                     {
//                         go = room;
//                         break;
//                     }
//                 }
//             }
//             else
//             {
//                 if (module.connectioncount == 4)
//                     go = inter4;
//                 if (module.connectioncount == 3)
//                     go = inter3;
//                 if (module.connectioncount == 2 && module.l == module.r)
//                     go = straight;
//                 if (module.connectioncount == 2 && module.l != module.r)
//                     go = turn;
//             }

//             if(go) imodules.Add(module.coordinate, dangduclongquanCLASS.InstantiateWithLightmaps(go, new Vector3(module.coordinate.x * gridsize, 0, module.coordinate.y * gridsize), dangduclongquanCLASS.TranslateModuleRotation(module), parent));
//         }

//         Debug.Log("Instantiated " + _modules.Count + " modules.");
//         return imodules;
//     }

//     static void Breakdown(SortedList<Vector2, dangduclongquanCLASS.Module> _modules, out string[] modulenames, out Vector2[] coordinates, out bool[] u, out bool[] d, out bool[] l, out bool[] r)
//     {
//         List<string> _modulenames = new List<string>();
//         List<Vector2> _coordinates = new List<Vector2>();
//         List<bool> _u = new List<bool>();
//         List<bool> _d = new List<bool>();
//         List<bool> _l = new List<bool>();
//         List<bool> _r = new List<bool>();

//         foreach (dangduclongquanCLASS.Module module in _modules.Values)
//         {
//             _modulenames.Add(module.name);
//             _coordinates.Add(module.coordinate);
//             _u.Add(module.u);
//             _d.Add(module.d);
//             _l.Add(module.l);
//             _r.Add(module.r);
//         }
//         modulenames = _modulenames.ToArray();
//         coordinates = _coordinates.ToArray();
//         u = _u.ToArray();
//         d = _d.ToArray();
//         l = _l.ToArray();
//         r = _r.ToArray();
//     }
//     static SortedList<Vector2, dangduclongquanCLASS.Module> Buildup(string[] modulenames, Vector2[] coordinates, bool[] u, bool[] d, bool[] l, bool[] r)
//     {
//         SortedList<Vector2, dangduclongquanCLASS.Module> modules = new SortedList<Vector2, dangduclongquanCLASS.Module>(new dangduclongquanCLASS.Vector2Comparer());

//         for (int i = 0; i <= coordinates.Length - 1; i++)
//         {
//             dangduclongquanCLASS.Module module = new dangduclongquanCLASS.Module(coordinates[i], r[i], l[i], u[i], d[i], modulenames[i]);
//             modules.Add(coordinates[i], module);
//         }
//         return modules;
//     }
// }
