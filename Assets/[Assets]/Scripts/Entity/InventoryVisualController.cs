// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using System;
// using MEC;

// public class InventoryVisualController : MonoBehaviour
// {
//     class Items
//     {
//         GameObject[] items;
//         int currentindex;

//         public Items(GameObject[] _items, int _count = 0)
//         {
//             items = _items;
//             foreach (GameObject item in items) item.SetActive(false);
//             currentindex = 0;

//             count = _count;
//         }

//         public int count
//         {
//             get { return currentindex; }
//             set
//             {
//                 int clampedvalue = Mathf.Clamp(value, 0, items.Length);

//                 if (currentindex >= clampedvalue)
//                 {
//                     while (currentindex > clampedvalue)
//                     {
//                         currentindex--;
//                         items[currentindex].SetActive(false);
//                     }
//                 }
//                 else
//                 {
//                     while (currentindex < clampedvalue)
//                     {
//                         items[currentindex].SetActive(true);
//                         currentindex++;
//                     }
//                 }
//             }
//         }
//     }

//     [SerializeField] GameObject[] batteries;
//     [SerializeField] GameObject[] bigbatteries;

//     Items batteryitems;
//     Items bigbatteryitems;


//     public int batterycount
//     {
//         get { return batteryitems.count; }
//         set { batteryitems.count = value; }
//     }

//     public int bigbatterycount
//     {
//         get { return bigbatteryitems.count; }
//         set { bigbatteryitems.count = value; }
//     }

//     private void Awake()
//     {
//         batteryitems = new Items(batteries);
//         bigbatteryitems = new Items(bigbatteries);

//     }


//     // Start is called before the first frame update
//     void Start()
//     {

//     }

//     // Update is called once per frame
//     void Update()
//     {
        
//     }
// }
