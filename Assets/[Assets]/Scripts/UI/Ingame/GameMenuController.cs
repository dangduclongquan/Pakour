// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;
// using UnityEngine.Events;
// using TMPro;
// using Photon.Pun;
// using Photon.Realtime;
// using MEC;

// public class GameMenuController : MonoBehaviourPun
// {
//     public delegate void MenuVisibilityChanged(string menusname, bool visibilitystate);
//     public static event MenuVisibilityChanged MenuVisibilityChangedEvent;

//     [SerializeField] Canvas canvas;
//     [SerializeField] GameObject playerlistitem;
//     [SerializeField] GameObject chatmessageitem;
//     [SerializeField] int maxmessagecount;
//     Transform[] menus;
//     TMP_Text[] texts;
//     Toggle[] toggles;
//     VerticalLayoutGroup[] layouts;
//     TMP_InputField[] inputs;
//     Button[] buttons;

//     TMP_Text latency;
//     TMP_Text playercount;
//     VerticalLayoutGroup playerlist;
//     VerticalLayoutGroup chathistory;

//     public KeyCode menukey;
//     public KeyCode menukey2;

//     private void Awake()
//     {
//         canvas.gameObject.SetActive(true);

//         menus = new Transform[canvas.transform.childCount];
//         for (int i = 0; i <= canvas.transform.childCount - 1; i++)
//             menus[i] = canvas.transform.GetChild(i).gameObject.transform;

//         texts = canvas.GetComponentsInChildren<TMP_Text>(true);
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
//         playerlist = layouts[Find(layouts, "Player List")];
//         chathistory = layouts[Find(layouts, "Chat History")];

//         SetMenuVisibility("Popup Menu", false);
//     }

//     // Start is called before the first frame update
//     void Start()
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
//                 Debug.LogWarning("Component number " + i + " of type " + components + "has been destroyed.");
//             }
//         Debug.LogError("Failed to find object " + gameobjectsname + " in " + components.ToString());
//         return -1;
//     }
//     void SetMenuVisibility(string menusname, bool isvisible)
//     {
//         menus[Find(menus, menusname)].gameObject.SetActive(isvisible);
//         if (menusname == "Popup Menu" && isvisible == true)
//             SortChatMessages();

//         MenuVisibilityChangedEvent(menusname, isvisible);
//     }
//     public bool GetMenuVisibility(string menusname)
//     {
//         return menus[Find(menus, menusname)].gameObject.activeInHierarchy;
//     }
//     public void SetLatency(int ms)
//     {
//         latency.text = ms.ToString() + " ms";
//     }


//     //public MenuVisibilityChangedEvent MenuVisibilityChanged = new MenuVisibilityChangedEvent();
//     // Update is called once per frame
//     void Update()
//     {
//         if (Input.GetKeyDown(menukey) || Input.GetKeyDown(menukey2))
//         {
//             if (!GetMenuVisibility("Popup Menu"))
//             {
//                 Cursor.visible = true;
//                 Cursor.lockState = CursorLockMode.Confined;
//                 SetMenuVisibility("Popup Menu", true);
//             }
//             else
//             {
//                 Cursor.visible = false;
//                 Cursor.lockState = CursorLockMode.Locked;
//                 SetMenuVisibility("Popup Menu", false);
//             }
//         }
//     }

//     public void ResetPlayerList()
//     {
//         foreach (Transform child in playerlist.transform)
//             Destroy(child.gameObject);
//     }
//     public void AddPlayerListItem(Player player)
//     {
//         GameObject instantiated = Instantiate(playerlistitem, playerlist.gameObject.transform);
//         instantiated.GetComponent<PlayerListItem>().Initiate(player);
//     }
//     public void AddChatMessageItem(string text)
//     {
//         if (chathistory.transform.childCount >= maxmessagecount)
//             Destroy(chathistory.transform.GetChild(0).gameObject);

//         GameObject instantiated= Instantiate(chatmessageitem, chathistory.transform);

//         instantiated.GetComponent<TMP_Text>().text = text;

//         SortChatMessages();
//     }
//     void SortChatMessages()
//     {
//         Timing.CallDelayed(Timing.WaitForOneFrame, () =>
//         {
//             GameObject instantiated = Instantiate(chatmessageitem, chathistory.transform);
//             instantiated.GetComponent<TMP_Text>().text = "";
//             Destroy(instantiated);
//         });
//     }

//     public void SetText(string textname, string textcontent)
//     {
//         texts[Find(texts, textname)].text = textcontent;
//     }
//     public bool GetToggleState(string togglename)
//     {
//         return toggles[Find(toggles, togglename)].isOn;
//     }
//     public void SetToggleState(string togglename, bool state)
//     {
//         toggles[Find(toggles, togglename)].isOn = state;
//     }
//     public void SetInteractables(bool isroomowner)
//     {
//         if (isroomowner)
//         {
//             toggles[Find(toggles, "Open Toggle")].interactable=true;
//             buttons[Find(buttons, "Start Button")].gameObject.SetActive(true);
//         }
//         else
//         {
//             toggles[Find(toggles, "Open Toggle")].interactable=false;
//             buttons[Find(buttons, "Start Button")].gameObject.SetActive(false);
//         }
//     }
//     public string GetInput(string inputname)
//     {
//         return inputs[Find(inputs, inputname)].text;
//     }
//     public void SetInput(string inputname, string text)
//     {
//         inputs[Find(inputs, inputname)].text = text;
//     }
//     public void SetButtonVisibility(string buttonname, bool isVisible)
//     {
//         buttons[Find(buttons, buttonname)].gameObject.SetActive(isVisible);
//     }
// }
