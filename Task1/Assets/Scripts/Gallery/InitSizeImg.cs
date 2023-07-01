using UnityEngine;
using UnityEngine.UI;
public class InitSizeImg 
{
    public InitSizeImg(GridLayoutGroup gridLayout)
    {
      var screen = new Vector2(Screen.width, Screen.height);
        if (screen.x < 850 && screen.y < 2000)
            gridLayout.cellSize = new Vector2(Screen.width / 2, Screen.height / 2);
        else if (screen.x < 1500 && screen.y > 2500)
            gridLayout.cellSize = new Vector2(Screen.width / 4, Screen.height / 4);
        else if (screen.x > 2000 && screen.y > 2000)
            gridLayout.cellSize = new Vector2(Screen.width / 4, Screen.height / 4);
        else
            gridLayout.cellSize = new Vector2(Screen.width / 3, Screen.height / 3);

    }
}
