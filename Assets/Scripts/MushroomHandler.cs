using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class MushroomHandler : MonoBehaviour
    {
        private GridSystem gS;
        private int hp = 4;
        private int xVal;
        private int yVal;
        
        private void Start()
        { 
            gS = GameObject.FindWithTag("GridSystem").gameObject.GetComponent<GridSystem>();
            xVal = GetComponentInParent<CellBehaviorHandler>().xVal;
            yVal = GetComponentInParent<CellBehaviorHandler>().yVal;
        }

        public void HasBeenHit()
        {
            hp--;
            if (hp <= 0)
            {
                hp = 4;
                gS.matrix[xVal][yVal] = 0;
                gS.score += 50;
                gS.UpdateScoreText();
            }
        }
    }
}