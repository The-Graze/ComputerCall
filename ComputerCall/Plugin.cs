using BepInEx;
using System;
using UnityEngine;
using Utilla;
using ComputerInterface;
using System.Collections.Generic;
using Bepinject;
using UnityEngine.InputSystem;
using System.Collections;

namespace ComputerCall
{
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [BepInDependency("tonimacaroni.computerinterface")]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        bool setup;
        void Start()
        { Utilla.Events.GameInitialized += OnGameInitialized; Zenjector.Install<MainInstal>().OnProject(); setup = false; }
        void OnGameInitialized(object sender, EventArgs e)
        {
            CallMan.instance = gameObject.AddComponent<CallMan>();
            CallMan.instance.lvlparent.Add(GameObject.Find("LocalObjects_Prefab").transform.GetChild(11).gameObject);
            CallMan.instance.lvlparent.Add(GameObject.Find("LocalObjects_Prefab").transform.GetChild(9).gameObject);
            CallMan.instance.lvlparent.Add(GameObject.Find("LocalObjects_Prefab").transform.GetChild(7).gameObject);
            CallMan.instance.lvlparent.Add(GameObject.Find("LocalObjects_Prefab").transform.GetChild(4).gameObject);
            CallMan.instance.lvlparent.Add(GameObject.Find("LocalObjects_Prefab").transform.GetChild(6).gameObject); 
            setup = false;
            Invoke("Oddinitfix", 5);
        }

        void Oddinitfix()
        {
            setup = false;
        }

        void Update()
        {
            if(!setup && FindObjectOfType<CustomComputer>()._initialized)
            {
                AddStuffToScreen(FindObjectOfType<CustomComputer>());
                setup = true;
            }
        }

        public void AddStuffToScreen(CustomComputer customComputer)
        {
            foreach (CustomScreenInfo screen in customComputer._customScreenInfos)
            {
                RenderTexture ren = new RenderTexture(1000, 1000, 16, RenderTextureFormat.ARGB32);
                GameObject scr = GameObject.CreatePrimitive(PrimitiveType.Cube);
                scr.AddComponent<Camera>();
                CallMan.instance.screens.Add(scr);
                CallMan.instance.video.Add(ren);
                scr.transform.SetParent(screen.Renderer.transform, false);
                scr.transform.localScale = new Vector3(-0.0185f, 0.02f, 0.001f);
                scr.transform.localRotation = Quaternion.Euler(90, 0, 0);
                string tempn = scr.transform.parent.parent.parent.name;
                scr.name = tempn;
                ren.name = scr.name;
            }
        }
    }

    class CallMan : MonoBehaviour
    {
        public List<GameObject> screens = new List<GameObject>();
        public List<RenderTexture> video = new List<RenderTexture>();
        public List<GameObject> lvlparent = new List<GameObject>();
        public static volatile CallMan instance;
        bool incall;
        void Update()
        {
            foreach (GameObject g in screens)
            {
                if (!incall)
                {
                    g.SetActive(false);
                }
                else g.SetActive(true);
            }
        }
        public IEnumerator StartCall(GameObject a, RenderTexture b, int lvl, int time)
        {
            yield return new WaitForSeconds(time);
            foreach (GameObject g in screens)
            {
                g.GetComponent<Renderer>().material.mainTexture = b;
                if (g != screens[lvl])
                {
                    g.GetComponent<Camera>().enabled = false;
                }
                else g.GetComponent<Camera>().enabled = true;
            }
            a.GetComponent<Camera>().targetTexture = b;
            lvlparent[lvl].SetActive(true);
            incall = true;
        }

        public void EndCall()
        {
            foreach (GameObject g in screens)
            {
                g.GetComponent<Renderer>().material.mainTexture = null;
            }
            incall = false;
        }
    }
}