using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Miro;

public class MiroPaint : MiroWidget
{
    CanvasGraphics graphics = null;
    [SerializeField] CanvasQuadDrawer drawer = null;
    [SerializeField] float thickness = 20;
    [SerializeField] Color color = Color.red;

    private void Awake()
    {
        graphics = new CanvasGraphics();
        graphics.curveSmoothing = true;
        graphics.smoothDotLimit = -0.8f;
        graphics.verticesDrawer = drawer;
    }

    public override void SetData(WidgetRead widget, string boardId)
    {
        base.SetData(widget, boardId);

        Debug.LogWarning(widget.points.Length);

        graphics.Clear();
        
        Vector3 old = Vector3.zero;
        Vector3 currentMid = Vector3.zero;
        Vector3 oldMid = Vector3.zero;
        for (int i = 0; i < widget.points.Length; i++)
        {
            Vector2 pt = widget.points[i];
            pt.y *= -1;
            if (i == 0)
            {
                currentMid = pt;
                old = currentMid;
                oldMid = pt;
                //graphics.MoveTo(pt, color, thickness);
            }
            else
            {
                currentMid = getMidInputCoords(old, pt);
            }
            graphics.MoveTo(currentMid, color, thickness);
            graphics.QuadraticCurveTo(old, oldMid, color, thickness);
            //graphics.QuadraticCurveTo(currentMid, old, oldMid, color, thickness);

            old = pt;
            oldMid = currentMid;
    
        }

        //Vector3[] points = new Vector3[widget.points.Length];
        //for(int i = 0; i < widget.points.Length; i++)
        //{
        //    Vector2 p = widget.points[i];
        //    points[i] = new Vector3(p.x, p.y, 0);
        //}

        //graphics.SetDefaultColor(color);
        //graphics.SetDefaultThickness(thickness);
        //graphics.DrawSpline(points);

        graphics.Render();

    }

    Vector3 getMidInputCoords(Vector3 old, Vector3 current)
    {
        //return new Vector3(
        //    (old.x + current.x) >> 1,
        //    (old.y + current.y) >> 1,
        //    0);
        //return (old + current) / 2 + Vector3.up;
        return (old + current) / 2;
    }
}
