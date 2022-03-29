// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using TMPro;
// using UnityEngine.UI;
// using UnityEngine.Events;

// public class MenuController : MonoBehaviour
// {

//     [SerializeField] Canvas canvas;
//     [SerializeField] GameObject roomlistitem;
//     Transform[] menus;
//     TMP_Text[] texts;
//     Toggle[] toggles;
//     VerticalLayoutGroup[] layouts;
//     TMP_Dropdown[] dropdowns;
//     TMP_InputField[] inputs;
//     Button[] buttons;

//     TMP_Text latency;
//     VerticalLayoutGroup roomlist;

//     private void Awake()
//     {
//         canvas.gameObject.SetActive(true);

//         menus = new Transform[canvas.transform.childCount];
//         for (int i = 0; i <= canvas.transform.childCount - 1; i++)
//             menus[i] = canvas.transform.GetChild(i).gameObject.transform;

//         texts = canvas.GetComponentsInChildren<TMP_Text>(true);
//         dropdowns = canvas.GetComponentsInChildren<TMP_Dropdown>(true);
//         layouts = canvas.GetComponentsInChildren<VerticalLayoutGroup>(true);

//         toggles = canvas.GetComponentsInChildren<Toggle>(true);
//         foreach (Toggle toggle in toggles)
//         {
//             toggle.gameObject.AddComponent<MenuInteractableController>();
//         }

//         inputs = canvas.GetComponentsInChildren<TMP_InputField>(true);
//         foreach (TMP_InputField input in inputs)
//         {
//             input.gameObject.AddComponent<MenuInteractableController>();
//         }

//         buttons = canvas.GetComponentsInChildren<Button>(true);
//         foreach (Button button in buttons)
//         {
//             button.gameObject.AddComponent<MenuInteractableController>();
//         }

//         latency = texts[Find(texts, "Latency View")];
//         roomlist = layouts[Find(layouts, "Room List")];

//         ShowMenu("Main Menu");
//     }

//     // Start is called before the first frame update
//     void Start()
//     {
        
//     }

//     // Update is called once per frame
//     void Update()
//     {

//     }

//     int Find(Component[] components, string gameobjectsname)
//     {
//         for (int i = 0; i <= components.Length - 1; i++)
//             if (components[i] != null)
//             {
//                 if (components[i].gameObject.name == gameobjectsname)
//                     return i;
//             }
//             else
//             {
//                 Debug.LogWarning("Component number " + i + " of type " + components + " has been destroyed.");
//             }
//         Debug.LogError("Failed to find object " + gameobjectsname + " in " + components.ToString());
//         return -1;
//     }
//     public void ShowMenu(string menusname)
//     {
//         foreach (Transform menu in menus)
//             menu.gameObject.SetActive(false);
//         menus[Find(menus, menusname)].gameObject.SetActive(true);
//     }
//     public string GetActiveMenu()
//     {
//         foreach (Transform menu in menus)
//             if (menu.gameObject.activeInHierarchy)
//                 return menu.gameObject.name;
//         return null;
//     }
//     public void ShowInfo(string title, string content)
//     {
//         texts[Find(texts, "Info Title")].text = title;
//         texts[Find(texts, "Info Content")].text = content;
//         ShowMenu("Info Menu");
//     }
//     public void ShowErrorMessage(string errormessage)
//     {
//         WindowController.instance.ShowErrorMessage(errormessage);
//     }

//     public void SetRegionDropdownList(List<string> regionlist)
//     {
//         List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();
//         foreach (string s in regionlist)
//             options.Add(new TMP_Dropdown.OptionData(s));

//         TMP_Dropdown dropdown = dropdowns[Find(dropdowns, "Region Dropdown")];
//         dropdown.options = options;
//     }
//     public void SetRegionDropdownValue(int value)
//     {
//         TMP_Dropdown dropdown = dropdowns[Find(dropdowns, "Region Dropdown")];
//         dropdown.value = value;
//     }
//     public string GetRegionDropdownText()
//     {
//         TMP_Dropdown dropdown = dropdowns[Find(dropdowns, "Region Dropdown")];
//         return dropdown.options[dropdown.value].text;
//     }
//     public void SetLatency(int ms)
//     {
//         latency.text = ms.ToString() + " ms";
//     }

//     public void SetRoomList(List<string> roomnames, List<int> playercount)
//     {
//         if (roomnames.Count != playercount.Count)
//         {
//             Debug.LogError("Invalid Roomlist.");
//             return;
//         }
//         foreach (Transform child in roomlist.transform)
//             Destroy(child.gameObject);
//         for (int i = 0; i <= roomnames.Count - 1; i++)
//         {
//             GameObject instantiated = Instantiate(roomlistitem, roomlist.gameObject.transform);
//             instantiated.GetComponent<RoomListItem>().Setup(roomnames[i], playercount[i]);
//         }
//     }
//     public void ResetRoomList()
//     {
//         foreach (Transform child in roomlist.transform)
//             Destroy(child.gameObject);
//     }
//     public void AddRoomListItem(string roomname, int playercount)
//     {
//         GameObject instantiated = Instantiate(roomlistitem, roomlist.gameObject.transform);
//         instantiated.GetComponent<RoomListItem>().Setup(roomname, playercount);
//     }

//     public string GetInput(string inputname)
//     {
//         return inputs[Find(inputs, inputname)].text;
//     }
//     public void SetInput(string inputname, string text)
//     {
//         inputs[Find(inputs, inputname)].text = text;
//     }
//     public bool GetToggleState(string togglename)
//     {
//         return toggles[Find(toggles, togglename)].isOn;
//     }

//     public void Exit()
//     {
//         SceneController.instance.Exit();
//     }
// }
