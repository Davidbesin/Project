using UnityEngine;

public class ReduceVisbility : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    bool dropped = false;
    

    public CanvasGroup dropDownBar;

   public void DropOrNoDrop()
   {
        dropped = !dropped;
        Active(dropped);
   }

    void Active(bool bol)
    {
        dropDownBar.interactable= bol;
        dropDownBar.blocksRaycasts = bol;
        if (bol) dropDownBar.alpha = 1;
        else dropDownBar.alpha = 0;
    }

}


