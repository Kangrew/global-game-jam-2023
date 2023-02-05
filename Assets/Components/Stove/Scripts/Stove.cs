using DG.Tweening;
using UnityEngine;

public class Stove : MonoBehaviour
{
    public string ToolTipText;
    public Transform PlaceHolder;
    public Transform Regulator;

    DragAndDrop drag;
    private Container ContainerPlaced;
    private bool IsStoveOn = false;
    // Start is called before the first frame update
    void Start()
    {
        drag = gameObject.AddComponent<DragAndDrop>();
        drag.On_ExitHover += OnExitHover_Func;
        drag.On_EnterHover += OnEnterHover_Func;
        drag.On_ClickRelease += OnClickReleased_Func;
        drag.CanDrag = false;
    }
    private bool IsContainAvalable() => ContainerPlaced!= null;

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
        StoveOnAndOff();
    }

    public bool PlaceContainer(Container con)
    {
        if (ContainerPlaced == null)
        {
            ContainerPlaced = con;
            //ContainerPlaced.PlaceOnStove(PlaceHolder.position);
            PlaceContainerOnStove();
            return true;
        }
        else
        {
            if (!ContainerPlaced.HasRecipe)
            {
                Debug.Log(ContainerPlaced.name + " 1");
                Debug.Log(con.name + " 2");
                //ContainerPlaced.PushToOrginalPos();
                PushContainerToOrginalPos();
                if (!ContainerPlaced)
                {
                    Debug.Log("before "); 
                }
                ContainerPlaced = con;
                Debug.Log("after " + ContainerPlaced.name);

                //ContainerPlaced.PlaceOnStove(PlaceHolder.position);
                PlaceContainerOnStove();
                return true;
            }
            else
                return false;
        }
    }
    public void PlaceContainerOnStove()
    {
        ContainerPlaced.transform.position = PlaceHolder.position;
        ContainerPlaced.transform.DOPunchScale(new Vector3(0.3f,0.5f,0.3f),0.6f);
        //ContainerPlaced.transform.DOScale(1.5f, 0.5f).SetEase(Ease.InOutBounce).SetLoops(2,LoopType.Yoyo);
        ContainerPlaced.OnStove = true;
        ContainerPlaced.drag.CanDrag = true;
        SoundBox.Instance.PlaySound(SoundBox.Instance.countainer.LoadUtensil,transform.position);
    }
    private void PushContainerToOrginalPos()
    {
        ContainerPlaced.transform.DOMove(ContainerPlaced.ContainerOrginalPos, 0.1f).SetEase(Ease.OutElastic);
        //ContainerPlaced.transform.position = ContainerPlaced.ContainerOrginalPos;

        ContainerPlaced.drag.CanDrag = true;
        ContainerPlaced.OnStove = false;
        ContainerPlaced = null;
    }

    public void StoveOnAndOff()
    {
        if (!IsStoveOn && IsContainAvalable() && ContainerPlaced.CanCook)
        {
            IsStoveOn= true;
            Regulator.rotation = Quaternion.Euler(Regulator.rotation.eulerAngles.x, Regulator.rotation.eulerAngles.y, 88);
        }
        else
        {
            IsStoveOn= false;
            Regulator.rotation = Quaternion.Euler(Regulator.rotation.eulerAngles.x, Regulator.rotation.eulerAngles.y, 0);

        }
    }

    public void ResetStove()
    {
        IsStoveOn = true;
        StoveOnAndOff();
        ContainerPlaced.ThrowDish();
        PushContainerToOrginalPos();
    }
}
