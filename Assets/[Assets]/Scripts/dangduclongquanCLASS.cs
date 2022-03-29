using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class dangduclongquanCLASS : MonoBehaviour
{
    public class Vector2Comparer : IComparer<Vector2>
    {
        public int Compare(Vector2 a, Vector2 b)
        {
            if (a.x > b.x)
                return 1;
            if (a.x < b.x)
                return -1;
            if (a.y > b.y)
                return 1;
            if (a.y < b.y)
                return -1;
            return 0;
        }
    }
    public class Vector4Comparer : IComparer<Vector4>
    {
        public int Compare(Vector4 a, Vector4 b)
        {
            if (a.x > b.x)
                return 1;
            if (a.x < b.x)
                return -1;
            if (a.y > b.y)
                return 1;
            if (a.y < b.y)
                return -1;
            if (a.z > b.z)
                return 1;
            if (a.z < b.z)
                return -1;
            if (a.w > b.w)
                return 1;
            if (a.w < b.w)
                return -1;
            return 0;
        }
    }

    public class Module
    {
        public Vector2 coordinate;
        public bool r, l, u, d;
        public string name;

        public Module(Vector2 _coordinate, bool _r = false, bool _l = false, bool _u = false, bool _d = false, string _name = "Hallway")
        {
            coordinate = _coordinate;

            r = _r;
            l = _l;
            u = _u;
            d = _d;

            name = _name;
        }

        public int connectioncount
        {
            get
            {
                int count = 0;

                if (r) count++;
                if (l) count++;
                if (u) count++;
                if (d) count++;

                return count;
            }
        }

        public int size
        {
            get
            {
                if (name == null)
                    return 0;
                else
                {
                    return System.Int32.Parse(name[0]+"");
                }
            }
        }
    }

    public static SortedList<Vector2, Module> GetRandomMap(int maxcount, int minrectsize, int maxrectsize, int minmodulecount)
    {
        SortedList<Vector2, Module> modules = new SortedList<Vector2, Module>(new Vector2Comparer());

        void Delele(Module module)
        {
            if (!modules.ContainsKey(module.coordinate)) return;

            if (module.u) modules[module.coordinate + Vector2.up].d = false;
            if (module.d) modules[module.coordinate + Vector2.down].u = false;
            if (module.l) modules[module.coordinate + Vector2.left].r = false;
            if (module.r) modules[module.coordinate + Vector2.right].l = false;

            modules.Remove(module.coordinate);
        }
        void Connect(Module module1, Module module2)
        {
            if (module1.coordinate + Vector2.right == module2.coordinate)
            {
                module1.r = true;
                module2.l = true;
                return;
            }
            if (module1.coordinate + Vector2.left == module2.coordinate)
            {
                module1.l = true;
                module2.r = true;
                return;
            }
            if (module1.coordinate + Vector2.up == module2.coordinate)
            {
                module1.u = true;
                module2.d = true;
                return;
            }
            if (module1.coordinate + Vector2.down == module2.coordinate)
            {
                module1.d = true;
                module2.u = true;
                return;
            }
            Debug.LogError("Module Connection Error.");
        }
        void AddRect(Vector2 pcoordinate, Vector2 mcoordinate)
        {
            if (pcoordinate.x <= mcoordinate.x || pcoordinate.y <= mcoordinate.y)
            {
                Debug.LogError("AddRect Error: Input not valid");
                return;
            }

            for (float i = mcoordinate.x + 1; i <= pcoordinate.x - 1; i++)
            {
                for (float j = mcoordinate.y + 1; j <= pcoordinate.y - 1; j++)
                {
                    if (modules.ContainsKey(new Vector2(i, j)))
                    {
                        Delele(modules[new Vector2(i, j)]);
                    }
                }
            }

            Vector2 coordinate;
            for (float l = mcoordinate.x; l <= pcoordinate.x - 1; l++)
            {
                coordinate = new Vector2(l, mcoordinate.y);

                if (!modules.ContainsKey(coordinate))
                    modules.Add(coordinate, new Module(coordinate));
                if (!modules.ContainsKey(coordinate + Vector2.right))
                    modules.Add(coordinate + Vector2.right, new Module(coordinate + Vector2.right));
                Connect(modules[coordinate], modules[coordinate + Vector2.right]);
            }
            for (float r = mcoordinate.x; r <= pcoordinate.x - 1; r++)
            {
                coordinate = new Vector2(r, pcoordinate.y);

                if (!modules.ContainsKey(coordinate))
                    modules.Add(coordinate, new Module(coordinate));
                if (!modules.ContainsKey(coordinate + Vector2.right))
                    modules.Add(coordinate + Vector2.right, new Module(coordinate + Vector2.right));
                Connect(modules[coordinate], modules[coordinate + Vector2.right]);
            }

            for (float ym = mcoordinate.y; ym <= pcoordinate.y - 1; ym++)
            {
                coordinate = new Vector2(mcoordinate.x, ym);

                if (!modules.ContainsKey(coordinate))
                    modules.Add(coordinate, new Module(coordinate));
                if (!modules.ContainsKey(coordinate + Vector2.up))
                    modules.Add(coordinate + Vector2.up, new Module(coordinate + Vector2.up));
                Connect(modules[coordinate], modules[coordinate + Vector2.up]);
            }
            for (float yp = mcoordinate.y; yp <= pcoordinate.y - 1; yp++)
            {
                coordinate = new Vector2(pcoordinate.x, yp);

                if (!modules.ContainsKey(coordinate))
                    modules.Add(coordinate, new Module(coordinate));
                if (!modules.ContainsKey(coordinate + Vector2.up))
                    modules.Add(coordinate + Vector2.up, new Module(coordinate + Vector2.up));
                Connect(modules[coordinate], modules[coordinate + Vector2.up]);
            }
        }

        modules.Add(Vector2.zero, new Module(Vector2.zero));
        while(modules.Count<minmodulecount)
        {
            if (modules.Count < maxcount)
            {
                int xsize = Random.Range(minrectsize, maxrectsize + 1);
                int ysize = Random.Range(minrectsize, maxrectsize + 1);

                Vector2 start = modules.Keys[Random.Range(0, modules.Count)];

                Vector2 p = Vector2.zero;

                int seg = Random.Range(0, 4);
                if (seg == 0)
                    p = start + new Vector2(Random.Range(0, xsize + 1), 0);
                if (seg == 1)
                    p = start + new Vector2(Random.Range(0, xsize + 1), ysize);
                if (seg == 2)
                    p = start + new Vector2(0, Random.Range(0, ysize + 1));
                if (seg == 3)
                    p = start + new Vector2(xsize, Random.Range(0, ysize + 1));

                AddRect(p, p - new Vector2(xsize, ysize));
            }
        }

        return modules;
    }
    public static SortedList<Vector2, Module> GetRandomMap2(int maxmodulecount, float internallinkchance, int iterationcount, List<string> roomnames)
    {
        SortedList<Vector2, Module> modules = new SortedList<Vector2, Module>(new Vector2Comparer());
        SortedSet<Vector2> occupieds = new SortedSet<Vector2>(new Vector2Comparer());
        SortedList<Vector4, bool> externallinks = new SortedList<Vector4, bool>(new Vector4Comparer());
        SortedList<Vector4, bool> internallinks = new SortedList<Vector4, bool>(new Vector4Comparer());

        List<int> randoms = new List<int>();
        for (int i = 0; i < roomnames.Count; i++)
            randoms.Add(Random.Range(1, iterationcount + 1));

        //Initiate at (0, 0)
        
        modules.Add(Vector2.zero, new Module(Vector2.zero));
        externallinks.Add(new Vector4(0, 0, 1, 0), true);
        externallinks.Add(new Vector4(0, 0, -1, 0), true);
        externallinks.Add(new Vector4(0, 0, 0, 1), true);
        externallinks.Add(new Vector4(0, 0, 0, -1), true);
        

        /*
        //Initiate at (-2, 1)r and (2, 1)l
        modules.Add(new Vector2(-2, 1), new Module(new Vector2(-2, 1), true, false, false ,false));
        occupieds.Add(new Vector2(-2, 1));
        externallinks.Add(new Vector4(-2, 1, -2, 2), true);
        externallinks.Add(new Vector4(-2, 1, -2, 0), true);
        externallinks.Add(new Vector4(-2, 1, -3, 1), true);
        modules.Add(new Vector2(2, 1), new Module(new Vector2(2, 1), false, true, false, false));
        occupieds.Add(new Vector2(2, 1));
        externallinks.Add(new Vector4(2, 1, 2, 2), true);
        externallinks.Add(new Vector4(2, 1, 2, 0), true);
        externallinks.Add(new Vector4(2, 1, 3, 1), true);

        for (int i=-1; i<=1; i++)
            for(int j=-2; j<=2; j++)
                occupieds.Add(new Vector2(i, j));
        */

        for (int j = 1; j <= iterationcount; j++)
        {
            //Add maximum number of halls
            while (modules.Count < maxmodulecount)
            {
                int random = Random.Range(0, externallinks.Count);
                bool external = true;

                if (internallinks.Count > 0 && (Random.Range(0, (int)(1/Mathf.Clamp01(internallinkchance))) == 1))
                {
                    random = Random.Range(0, internallinks.Count);
                    external = false;
                }

                Vector2 origin;
                Vector2 destination;
                if (external)
                {
                    origin = new Vector2(externallinks.Keys[random].x, externallinks.Keys[random].y);
                    destination = new Vector2(externallinks.Keys[random].z, externallinks.Keys[random].w);
                    externallinks.RemoveAt(random);
                }
                else
                {
                    origin = new Vector2(internallinks.Keys[random].x, internallinks.Keys[random].y);
                    destination = new Vector2(internallinks.Keys[random].z, internallinks.Keys[random].w);
                    internallinks.RemoveAt(random);
                }

                if (occupieds.Contains(destination))
                    continue;

                if (external)
                {
                    modules.Add(destination, new Module(destination));

                    Vector4 link = new Vector4(destination.x, destination.y, destination.x + 1, destination.y);
                    if (!modules.ContainsKey(destination + Vector2.right) && !occupieds.Contains(destination + Vector2.right)) externallinks.Add(link, true);
                    else
                    {
                        link = new Vector4(destination.x + 1, destination.y, destination.x, destination.y);
                        if (externallinks.ContainsKey(link)) { externallinks.Remove(link); internallinks.Add(link, false); }
                    }

                    link = new Vector4(destination.x, destination.y, destination.x - 1, destination.y);
                    if (!modules.ContainsKey(destination + Vector2.left) && !occupieds.Contains(destination + Vector2.left)) externallinks.Add(link, true);
                    else
                    {
                        link = new Vector4(destination.x - 1, destination.y, destination.x, destination.y);
                        if (externallinks.ContainsKey(link)) { externallinks.Remove(link); internallinks.Add(link, false); }
                    }

                    link = new Vector4(destination.x, destination.y, destination.x, destination.y + 1);
                    if (!modules.ContainsKey(destination + Vector2.up) && !occupieds.Contains(destination + Vector2.up)) externallinks.Add(link, true);
                    else
                    {
                        link = new Vector4(destination.x, destination.y + 1, destination.x, destination.y);
                        if (externallinks.ContainsKey(link)) { externallinks.Remove(link); internallinks.Add(link, false); }
                    }

                    link = new Vector4(destination.x, destination.y, destination.x, destination.y - 1);
                    if (!modules.ContainsKey(destination + Vector2.down) && !occupieds.Contains(destination + Vector2.down)) externallinks.Add(link, true);
                    else
                    {
                        link = new Vector4(destination.x, destination.y - 1, destination.x, destination.y);
                        if (externallinks.ContainsKey(link)) { externallinks.Remove(link); internallinks.Add(link, false); }
                    }
                }

                if (destination - origin == Vector2.right) { modules[origin].r = true; modules[destination].l = true; }
                if (destination - origin == Vector2.left) { modules[origin].l = true; modules[destination].r = true; }
                if (destination - origin == Vector2.up) { modules[origin].u = true; modules[destination].d = true; }
                if (destination - origin == Vector2.down) { modules[origin].d = true; modules[destination].u = true; }
            }

            //Remove deadends
            for (int i = 0; i < modules.Count; i++)
            {
                Module hall = modules.Values[i];
                if (hall.connectioncount == 1 && hall.name == "Hallway" && !occupieds.Contains(hall.coordinate))
                {
                    Vector4 link = new Vector4(hall.coordinate.x + 1, hall.coordinate.y, hall.coordinate.x, hall.coordinate.y);
                    if (hall.r) { modules[hall.coordinate + Vector2.right].l = false; internallinks.Add(link, true); }

                    link = new Vector4(hall.coordinate.x - 1, hall.coordinate.y, hall.coordinate.x, hall.coordinate.y);
                    if (hall.l) { modules[hall.coordinate + Vector2.left].r = false; internallinks.Add(link, true); }

                    link = new Vector4(hall.coordinate.x, hall.coordinate.y + 1, hall.coordinate.x, hall.coordinate.y);
                    if (hall.u) { modules[hall.coordinate + Vector2.up].d = false; internallinks.Add(link, true); }

                    link = new Vector4(hall.coordinate.x, hall.coordinate.y - 1, hall.coordinate.x, hall.coordinate.y);
                    if (hall.d) { modules[hall.coordinate + Vector2.down].u = false; internallinks.Add(link, true); }


                    link = new Vector4(hall.coordinate.x, hall.coordinate.y, hall.coordinate.x + 1, hall.coordinate.y);
                    if (externallinks.ContainsKey(link)) externallinks.Remove(link);
                    if (internallinks.ContainsKey(link)) internallinks.Remove(link);
                    link = new Vector4(hall.coordinate.x + 1, hall.coordinate.y, hall.coordinate.x, hall.coordinate.y);
                    if (internallinks.ContainsKey(link)) { internallinks.Remove(link); externallinks.Add(link, true); }

                    link = new Vector4(hall.coordinate.x, hall.coordinate.y, hall.coordinate.x - 1, hall.coordinate.y);
                    if (externallinks.ContainsKey(link)) externallinks.Remove(link);
                    if (internallinks.ContainsKey(link)) internallinks.Remove(link);
                    link = new Vector4(hall.coordinate.x - 1, hall.coordinate.y, hall.coordinate.x, hall.coordinate.y);
                    if (internallinks.ContainsKey(link)) { internallinks.Remove(link); externallinks.Add(link, true); }

                    link = new Vector4(hall.coordinate.x, hall.coordinate.y, hall.coordinate.x, hall.coordinate.y + 1);
                    if (externallinks.ContainsKey(link)) externallinks.Remove(link);
                    if (internallinks.ContainsKey(link)) internallinks.Remove(link);
                    link = new Vector4(hall.coordinate.x, hall.coordinate.y + 1, hall.coordinate.x, hall.coordinate.y);
                    if (internallinks.ContainsKey(link)) { internallinks.Remove(link); externallinks.Add(link, true); }

                    link = new Vector4(hall.coordinate.x, hall.coordinate.y, hall.coordinate.x, hall.coordinate.y - 1);
                    if (externallinks.ContainsKey(link)) externallinks.Remove(link);
                    if (internallinks.ContainsKey(link)) internallinks.Remove(link);
                    link = new Vector4(hall.coordinate.x, hall.coordinate.y - 1, hall.coordinate.x, hall.coordinate.y);
                    if (internallinks.ContainsKey(link)) { internallinks.Remove(link); externallinks.Add(link, true); }

                    modules.RemoveAt(i);
                    i = -1;
                }
            }

            //Add special modules
            for (int i = 0; i < roomnames.Count; i++)
                if (randoms[i] == j)
                {
                    int radius = 7;
                    if (roomnames[i][0] == '1') radius = 0;
                    if (roomnames[i][0] == '3') radius = 1;
                    if (roomnames[i][0] == '5') radius = 2;
                    if (roomnames[i][0] == '7') radius = 3;

                    Vector2 origin;
                    Vector2 destination;
                    Vector2 center;

                    SortedList<Vector4, bool> links = new SortedList<Vector4, bool>(new Vector4Comparer());

                    //Select usable positions
                    for (int k = 0; k < externallinks.Count; k++)
                    {
                        origin = new Vector2(externallinks.Keys[k].x, externallinks.Keys[k].y);
                        destination = new Vector2(externallinks.Keys[k].z, externallinks.Keys[k].w);
                        center = origin + (destination - origin) * (radius + 1);

                        bool obstructed = false;
                        for (float x = center.x - radius; x <= center.x + radius; x++)
                            for (float y = center.y - radius; y <= center.y + radius; y++)
                                if (occupieds.Contains(new Vector2(x, y)) || modules.ContainsKey(new Vector2(x, y)))
                                    obstructed = true;
                        if (!obstructed)
                        {
                            links.Add(externallinks.Keys[k], true);
                        }
                    }

                    //Select a random usable position
                    if (links.Count > 0)
                    {

                        int random = Random.Range(0, links.Count);
                        externallinks.Remove(links.Keys[random]);

                        origin = new Vector2(links.Keys[random].x, links.Keys[random].y);
                        destination = new Vector2(links.Keys[random].z, links.Keys[random].w);
                        center = origin + (destination - origin) * (radius + 1);


                        for (float x = center.x - radius; x <= center.x + radius; x++)
                            for (float y = center.y - radius; y <= center.y + radius; y++)
                                occupieds.Add(new Vector2(x, y));

                        modules.Add(destination, new Module(destination, false, false, false, false, roomnames[i]));
                        if (destination - origin == Vector2.right) { modules[origin].r = true; modules[destination].l = true; }
                        if (destination - origin == Vector2.left) { modules[origin].l = true; modules[destination].r = true; }
                        if (destination - origin == Vector2.up) { modules[origin].u = true; modules[destination].d = true; }
                        if (destination - origin == Vector2.down) { modules[origin].d = true; modules[destination].u = true; }

                        Debug.Log("Iteration #" + randoms[i] + ": " + roomnames[i] + "'s coordinate is " + destination);
                    }

                    randoms.RemoveAt(i);
                    roomnames.RemoveAt(i);
                    i--;
                }
        }
        Debug.Log("Random Map Created. Number of modules: " + modules.Count);
        return modules;
    }


    public static GameObject InstantiateWithLightmaps(GameObject gameobject, Vector3 position, Vector3 rotation, Transform parent = null)
    {
        GameObject instance = Instantiate(gameobject, position, Quaternion.Euler(rotation));
        if (parent != null) instance.transform.SetParent(parent);

        MeshRenderer[] meshRenderers = gameobject.GetComponentsInChildren<MeshRenderer>(true);
        MeshRenderer[] instancemeshRenderers = instance.GetComponentsInChildren<MeshRenderer>(true);
        foreach (MeshRenderer i in instancemeshRenderers)
            foreach (MeshRenderer j in meshRenderers)
            {
                if (i.gameObject.name == j.gameObject.name)
                {
                    i.lightmapIndex = j.lightmapIndex;
                    i.lightmapScaleOffset = j.lightmapScaleOffset;
                    break;
                }
            }
        return instance;
    }

    public static Vector3 TranslateModuleRotation(Module module)
    {
        //is inter4?
        if (module.connectioncount == 4)
        {
            return Vector3.zero;
        }

        //is inter3?
        if (module.l && module.r && module.d)
        {
            return Vector3.zero;
        }
        if (module.u && module.d && module.l)
        {
            return new Vector3(0, 90, 0);
        }
        if (module.l && module.r && module.u)
        {
            return new Vector3(0, 180, 0);
        }
        if (module.u && module.d && module.r)
        {
            return new Vector3(0, 270, 0);
        }

        //is straight?
        if (module.l && module.r)
        {
            return new Vector3(0, 0, 0);
        }
        if (module.u && module.d)
        {
            return new Vector3(0, 90, 0);
        }

        //is turn?
        if (module.u && module.r)
        {
            return new Vector3(0, 0, 0);
        }
        if (module.d && module.r)
        {
            return new Vector3(0, 90, 0);
        }
        if (module.d && module.l)
        {
            return new Vector3(0, 180, 0);
        }
        if (module.u && module.l)
        {
            return new Vector3(0, 270, 0);
        }
        //is room.
        if (module.u)
        {
            return new Vector3(0, 0, 0);
        }
        if (module.r)
        {
            return new Vector3(0, 90, 0);
        }
        if (module.d)
        {
            return new Vector3(0, 180, 0);
        }
        if (module.l)
        {
            return new Vector3(0, 270, 0);
        }
        return Vector3.negativeInfinity;
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
