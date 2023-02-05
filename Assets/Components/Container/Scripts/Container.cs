using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
    public bool IsDragable;
    public string ToolTipText;
    public Recipe.DishType DishType;
    public GameObject[] FillLevels;
    public Material IngInContainer;
    public DragAndDrop drag;
    Vector3 dragStartPos;

    public Vector3 ContainerOrginalPos;

    public Recipe currentRecipe;

    [SerializeField]private bool _canCook;
    private bool _onStove;

    public bool CanCook => _canCook;
    public bool OnStove { get { return _onStove; } set { _onStove = value; } }
    public bool HasRecipe {  get { return currentRecipe != null; } }
    // Start is called before the first frame update
    void Start()
    {
        drag = gameObject.AddComponent<DragAndDrop>();
        drag.On_ExitHover += OnExitHover_Func;
        drag.On_EnterHover += OnEnterHover_Func;

        if (IsDragable)
        {
            drag.CanDrag = true;
            drag.On_DragStart += OnDragStart_Func;
            drag.On_DragEnd += OnDragEnd_Func;
        }else
            drag.CanDrag = false;
        ContainerOrginalPos = transform.position;
    }
    

    private void OnDragStart_Func()
    {
        dragStartPos = transform.position;
    }

    private void OnDragEnd_Func()
    {
        Vector2 touchPosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 1000, 1 << LayerMask.NameToLayer("Stove")))
        {
            if (hit.collider.TryGetComponent<Stove>(out Stove stove))
            {
                //transform.position = contain.PlaceHolder.position;
                //transform.position = new Vector3(
                //    transform.position.x,
                //    transform.position.y,
                //    transform.position.z);
                //transform.SetParent(hit.collider.transform);
                if (!stove.PlaceContainer(this)) { 
                    transform.position = dragStartPos;
                
                } else if (IsDragable) drag.CanDrag = true;
                return;
            }
            else if (hit.collider.TryGetComponent<ServingBowl>(out ServingBowl bowl))
            {
                bowl.FillBowl(this);
            }else if(hit.collider.TryGetComponent<TrashCan>(out TrashCan can)){
                ThrowDish();
                Debug.Log(can.name);
                can.transform.DOPunchScale(new Vector3(-0.2f,0.3f,0.4f),1);
                SoundBox.Instance.PlaySound(SoundBox.Instance.countainer.TrashCan,transform.position);
            }
        }

        transform.position = dragStartPos;

    }
    private void OnEnterHover_Func()
    {
        ToolTip.instance.Show(ToolTipText);
        Debug.Log("on enter hover");
    }
    private void OnExitHover_Func()
    {
        ToolTip.instance.Hide();
    }

    //public void PushToOrginalPos() {  
    //    transform.DOMove(ContainerOrginalPos, 0.1f);
    //    drag.CanDrag = true;
    //    _onStove = false;
    //}
    //public void PlaceOnStove(Vector3 pos) {  
    //    transform.position = pos;
    //    transform.DOPunchScale(Vector3.one,30);
    //    _onStove = true;
    //}

    public void AddIngredient(IngredientData data)
    {
        if (currentRecipe == null)
        {
            InitiateRecipe(); 
        }
        currentRecipe.AddIngredient(data);
        if(DishType == Recipe.DishType.Soup) {IngInContainer.color = data.IngredientColor;
        SoundBox.Instance.PlaySound(SoundBox.Instance.countainer.Boiling,transform.position);}
        if (DishType == Recipe.DishType.StirFry) {IngInContainer.color = data.IngredientDarkColor;
        SoundBox.Instance.PlaySound(SoundBox.Instance.countainer.Fry,transform.position);}
        if (DishType == Recipe.DishType.Salad) { SoundBox.Instance.PlaySound(SoundBox.Instance.countainer.Vegie,transform.position);}


        SelectLevel(currentRecipe.Ingredients.Count - 1);
        transform.DOPunchScale(new Vector3(0.2f,0.4f,0.4f),0.7f);
    }
    private void InitiateRecipe()
    {
        currentRecipe = new Recipe(DishType);
        //CookingPlane.SetActive(true);
        //CookingUI.Instance.ThrowButton.gameObject.SetActive(true);
        //CookingUI.Instance.ThrowButton.onClick.AddListener(ThrowDish);
    }

    public void SummitOrder()
    {
        if(currentRecipe != null)
        {
            GameController.Instance.CompleteOrder(currentRecipe);
        }
    }

    public void ThrowDish()
    {
        currentRecipe = null;
        //CookingPlane.SetActive(false);
        SelectLevel(-1);
    }

    public void SelectLevel(int x)
    {
        for (int i = 0; i < FillLevels.Length; i++)
        {
            if (i <= x)
            {
                FillLevels[i].SetActive(true);
            }else
                FillLevels[i].SetActive(false);
        }
    }


}
