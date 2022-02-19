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
            Place();
        }

        void Place()
        {
            int k = 0;
            float width = distanceBetweenTransforms * (_params.cols - 1);
            float height = distanceBetweenTransforms * (_params.rows - 1);
            Vector2 centerPadding = new Vector2(-width * 0.5f, -height * 0.5f);
            
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
    }
}