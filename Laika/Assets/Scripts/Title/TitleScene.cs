using System.Linq;
using System.Collections.Generic;
using UnityEngine;


public enum eTitleCanvas
{
    TitleMenu,
    CharacterModeMenu,
}


public class TitleScene : SceneBaseMonoBehaviour<TitleScene>
{
    public List<Canvas> Canvases { get;set; }
    Canvas OperationCanvas { get; set; }

    private void Start()
    {
        Canvases = GetComponentsInChildren<Canvas>().ToList();
        OperationCanvas = GetComponentInChildren<TitleMenu>().gameObject.GetComponent<Canvas>();

        AudioManager.PlayBGM(ResourcesPath.Audio.BGM._InnovationAndChaos, 0);
    }

    public void ChangeOperationCancas(eTitleCanvas nextCanvas)
    {
        OperationCanvas.gameObject.SetActive(false);
        OperationCanvas = Canvases[(int)nextCanvas];
        OperationCanvas.gameObject.SetActive(true);
        OperationCanvas.GetComponentInChildren<MenuBase>().OnActivateMenu();
    }
}