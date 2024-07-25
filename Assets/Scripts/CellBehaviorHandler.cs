using UnityEngine;

namespace DefaultNamespace
{
    public class CellBehaviorHandler : MonoBehaviour
    {
        [SerializeField]private int xVal;
        [SerializeField]private int yVal;
        public void GetCellData(int x, int y)
        {
            xVal = x;
            yVal = y;
        }
        
        
    }
}