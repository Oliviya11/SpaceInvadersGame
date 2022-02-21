using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public class GridPlacer
    {
        public struct Params
        {
            public int rows, cols;
            public List<Transform> transforms;
            public float distanceBetweenTransforms;
        }

        Params _params;
        float speed;
        private float distanceBetweenTransforms;

        public GridPlacer(Params p)
        {
            _params = p;
            distanceBetweenTransforms = _params.distanceBetweenTransforms;
        }

        public void Place()
        {
            int k = 0;
            Vector2 centerPadding = GetCenterPadding();
            
            for (int i = 0; i < _params.rows; ++i)
            {
                Vector3 rowPosition = new Vector3(centerPadding.x, (distanceBetweenTransforms * i) + centerPadding.y, 0f);

                for (int j = 0; j < _params.cols; ++j)
                {
                    Vector3 pos = rowPosition;
                    pos.x += distanceBetweenTransforms * j;
                    _params.transforms[k++].localPosition = pos;
                }
            }
        }

        Vector2 GetCenterPadding()
        {
            float width = distanceBetweenTransforms * (_params.cols - 1);
            float height = distanceBetweenTransforms * (_params.rows - 1);
            Vector2 centerPadding = new Vector2(-width * 0.5f, -height * 0.5f);
            return centerPadding;
        }

        public void PlaceToCell(int row, int col, Transform parent)
        {
            Vector2 centerPadding = GetCenterPadding();
            Vector3 rowPosition = new Vector3(centerPadding.x, (distanceBetweenTransforms * row) + centerPadding.y, 0f);
            Vector3 pos = rowPosition;
            pos.x += distanceBetweenTransforms * col;
            _params.transforms[0].position = parent.TransformPoint(pos);
        }
    }
}