using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServingBowl : MonoBehaviour
{
    public string ToolTipText;
    public GameObject SoupDish, FryDish, SaladDish;
    public ServeBell bell;
    DragAndDrop drag;

    private Recipe FinalRecipe;
    // Start is called before the first frame update
    void Start()
    {
        drag = gameObject.AddComponent<DragAndDrop>();
        drag.On_ExitHover += OnExitHover_Func;
        drag.On_EnterHover += OnEnterHover_Func;
        drag.On_ClickRelease += OnClickReleased_Func;
        drag.CanDrag = false;
        bell.gameObject.SetActive(false);
    }

    private void OnEnterHover_Func()
    {
        ToolTip.instance.Show(ToolTipText);
    }
    private void OnExitHover_Func()
    {
        ToolTip.instance.Hide();
    }
    private void OnClickReleased_Func()
    {
        //Debug.Log("Clicked!!!!");
        
    }

    public void FillBowl(Container con)
    {
        if (con.HasRecipe)
        {
            FinalRecipe = con.currentRecipe;
            con.ThrowDish();
            selectDish((int)FinalRecipe.Type);
            bell.gameObject.SetActive(true);
        }
    }

    private void selectDish(int r)
    {
        string selected = " ";
        switch (r)
        {
            case 0: selected = SaladDish.name;
                break;
            case 1: selected = SoupDish.name;
                break;
            case 2: selected = FryDish.name;
                break;
            default:
                break;
        }

        SaladDish.SetActive(SaladDish.name.Equals(selected));
        FryDish.SetActive(FryDish.name.Equals(selected));
        SoupDish.SetActive(SoupDish.name.Equals(selected));

    }
    public void SummitOrder()
    {
        if (FinalRecipe != null)
        {
            selectDish(-1);
            GameController.Instance.CompleteOrder(FinalRecipe);
        }
    }
}
