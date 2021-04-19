using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public bool openShop;
    public GameObject openShopOverlay;
    public GameObject openShopButton;

    public GameObject pointer;

    public bool placeFuel, placeThruster;
    public GameObject placeFuelOverlay;

    public GameObject thrusterShop, fuelShop;

    public GameObject shopClose;
    public GameObject openGroups;
    public GameObject groupsCloseButton;
    public GameObject fairing;
    public GameObject groupsButton;
    public GameObject closeShopB;
    public GameObject addGroupButton;
    public bool upDown;
    public bool openGroupsPanel, addPartInGroup;

    public GameObject questButton;

    public GameObject openQuestsOverlay;
    public GameObject closeQuestButton;

    public bool closeTenders;

    public GameObject simulateOverlay;
    public GameObject simulateButton;
    public GameObject miniGroupsOverlay;
    public float time;

    public bool wait;
    public void StartSim()
    {
        miniGroupsOverlay.SetActive(true);
        simulateOverlay.SetActive(false);
        var n = FindObjectOfType<GroupsManager>().miniHolder.GetChild(0);
        pointer.transform.position = new Vector2(n.transform.position.x, n.transform.position.y + n.GetComponent<RectTransform>().sizeDelta.y / 2f);
        pointer.transform.localEulerAngles = new Vector3(0, 0, 47.539f);
    }
    public void CloseTenders()
    {
        closeTenders = false;
        simulateOverlay.SetActive(true);
        pointer.transform.position = new Vector2(simulateButton.transform.position.x - simulateButton.GetComponent<RectTransform>().sizeDelta.x / 2f, simulateButton.transform.position.y + simulateButton.GetComponent<RectTransform>().sizeDelta.y / 2f);
        pointer.transform.localEulerAngles = new Vector3(0, 0, 47.539f);

    }
    public void OpenTenders()
    {
        openQuestsOverlay.SetActive(false);
        closeTenders = true;
    }


    public void EndConstruct()
    {
        UIManager.manager.HideUnTouch(UIManager.manager.groups); addPartInGroup = false;
        pointer.transform.position = new Vector2(questButton.transform.position.x - questButton.GetComponent<RectTransform>().sizeDelta.x / 2f, questButton.transform.position.y + questButton.GetComponent<RectTransform>().sizeDelta.y / 2f);
        pointer.transform.localEulerAngles = new Vector3(0, 0, 47.539f);
        openQuestsOverlay.SetActive(true);
    }

    public GameObject endButton;
    private void Start()
    {
        openShopOverlay.SetActive(true);
        pointer.transform.position = new Vector2(openShopButton.transform.position.x, openShopButton.transform.position.y + openShopButton.GetComponent<RectTransform>().sizeDelta.y / 2f);
        pointer.transform.localEulerAngles = new Vector3(0, 0, 47.539f);
    }
    public void AddGroup()
    {
        addPartInGroup = true;
           openGroupsPanel = false;
        pointer.transform.position = FindObjectOfType<GroupsManager>().holder.GetChild(0).GetChild(2).GetChild(2).transform.position;
        pointer.transform.localEulerAngles = new Vector3(0, 0, 47.539f);
    }

    public void OpenGroups()
    {
        openGroupsPanel = true;
        openGroups.SetActive(false);
        groupsCloseButton.SetActive(false);
    }

    public void CloseShop()
    {
        openGroups.SetActive(true);
        shopClose.SetActive(false);
        pointer.transform.position = new Vector2(groupsButton.transform.position.x + groupsButton.GetComponent<RectTransform>().sizeDelta.x / 2f, groupsButton.transform.position.y + groupsButton.GetComponent<RectTransform>().sizeDelta.y / 2f);
        pointer.transform.localEulerAngles = new Vector3(0, 0, 47.539f);

    }
    public void OpenShop()
    {
        openShop = true;
        openShopOverlay.SetActive(false);
        placeFuelOverlay.SetActive(true);
        thrusterShop.SetActive(false);
    }


    private void FixedUpdate()
    {
        if (openShop)
        {
            if (!placeFuel)
            {
                if (upDown)
                {
                    if (Vector2.Distance(pointer.transform.position, Camera.main.WorldToScreenPoint(fairing.transform.position - new Vector3(0, 1f)) ) > 0.1f)
                    {
                        pointer.transform.position = Vector2.MoveTowards(pointer.transform.position, Camera.main.WorldToScreenPoint(fairing.transform.position - new Vector3(0, 1f)), 500f * Time.deltaTime);
                    }
                    else
                    {
                        upDown = false;
                    }
                }
                else
                {
                    if (Vector2.Distance(pointer.transform.position, fuelShop.transform.position) > 0.1f)
                    {
                        pointer.transform.position = Vector2.MoveTowards(pointer.transform.position, fuelShop.transform.position, 700f * Time.deltaTime);
                    }
                    else
                    {
                        upDown = true;
                    }

                }
                if (TouchManager.selected == null)
                {
                    if (FindObjectOfType<FuelTank>() != null)
                    {
                        FindObjectOfType<FuelTank>().GetComponent<PartBuilder>().enabled = false;
                        thrusterShop.SetActive(true);
                        fuelShop.SetActive(false);
                        placeFuel = true;
                    }
                }
                return;
            }
            if (!placeThruster)
            {
                if (upDown)
                {
                    if (Vector2.Distance(pointer.transform.position, Camera.main.WorldToScreenPoint(fairing.transform.position - new Vector3(0, 1f))) > 0.1f)
                    {
                        pointer.transform.position = Vector2.MoveTowards(pointer.transform.position, Camera.main.WorldToScreenPoint(fairing.transform.position - new Vector3(0, 1f)), 500f * Time.deltaTime);
                    }
                    else
                    {
                        upDown = false;
                    }
                }
                else
                {
                    if (Vector2.Distance(pointer.transform.position, fuelShop.transform.position) > 0.1f)
                    {
                        pointer.transform.position = Vector2.MoveTowards(pointer.transform.position, fuelShop.transform.position, 700f * Time.deltaTime);
                    }
                    else
                    {
                        upDown = true;
                    }

                }

                if (TouchManager.selected == null)
                {
                    if (FindObjectOfType<Thruster>() != null)
                    {
                        FindObjectOfType<Thruster>().GetComponent<PartBuilder>().enabled = false;
                        thrusterShop.SetActive(false);
                        fuelShop.SetActive(false);
                        placeThruster = true;
                        placeFuelOverlay.SetActive(false);
                        shopClose.SetActive(true);
                        pointer.transform.position = closeShopB.transform.position;
                        pointer.transform.localEulerAngles = new Vector3(0,0,-45);
                    }
                }
                return;
            }
            if (openGroupsPanel)
            {
                pointer.transform.position = new Vector2(addGroupButton.transform.position.x + addGroupButton.GetComponent<RectTransform>().sizeDelta.x / 2f, addGroupButton.transform.position.y + addGroupButton.GetComponent<RectTransform>().sizeDelta.y / 2f);
                pointer.transform.localEulerAngles = new Vector3(0, 0, 170f);
            }
            if (addPartInGroup)
            {
                pointer.transform.position = FindObjectOfType<GroupsManager>().holder.GetChild(0).GetChild(2).GetChild(2).transform.position;
                pointer.transform.localEulerAngles = new Vector3(0, 0, 47.539f);

                if (FindObjectOfType<GroupsManager>().groups.Count == 1)
                {
                    endButton.SetActive(FindObjectOfType<GroupsManager>().groups[0].parts.Count >= 3);
                }
            }
            if (closeTenders)
            {
                pointer.transform.position = new Vector2(closeQuestButton.transform.position.x, closeQuestButton.transform.position.y + closeQuestButton.GetComponent<RectTransform>().sizeDelta.y / 2f);
                pointer.transform.localEulerAngles = new Vector3(0, 0, 170f);
            }
        }
        if (FindObjectOfType<Thruster>() != null && FindObjectOfType<Thruster>().run)
        {
            wait = true;
            pointer.SetActive(false);
            miniGroupsOverlay.SetActive(false);
        }
        if (wait == true)
        {
            time += Time.fixedDeltaTime;
            if (time > 5f)
            {
                Application.LoadLevel(0);
            }
        }
    }

}
