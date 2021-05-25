using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Miro
{
    [System.Serializable]
    public class MiroSetting
    {
        public string token = "";
        public string boardId = "";
    }

    // # Board
    [System.Serializable]
    public class SharingPolicy
    {
        public string access = "private";
        public string teamAccess = "private";
    }

    [System.Serializable]
    public class CreateBoard
    {
        public string name = "";
        public string description = "";
        public SharingPolicy sharingPolicy;
    }


    // # Widget
    public enum TYPE
    {
        NONE,
        Sticker,
        Shape,
        Text,
        Line,
        Card,
        Image, // readonly
        WebScreenshot, // readonly
        Document, // readonly
        Paint, // readonly
        Preview, // readonly
        Embed, // readonly
        Mockup, // readonly
        Frame, // readonly
        Kanban, // readonly
        USM, // readonly
    }


    [System.Serializable]
    public class Widget
    {
        public string type = "sticker";
    }

    [System.Serializable]
    public class StickerWidget : Widget
    {
        public string text = "";
        public float x = 0;
        public float y = 0;
    }

    [System.Serializable]
    public class ShapeWidget : Widget
    {
        public string text = "";
        public float x = 0;
        public float y = 0;
        public ShapeStyle style = null;
    }

    [System.Serializable]
    public class CardWidget : Widget
    {
        public string title = "";
        public float x = 0;
        public float y = 0;
        public string description = "";
        public string date = "2020-01-01";
    }

    [System.Serializable]
    public class LineWidget : Widget
    {
        public WidgetId startWidget;
        public WidgetId endWidget;
    }


    [System.Serializable]
    public class WidgetId
    {
        public string id = "";
    }


    //[System.Serializable]
    //public class WidgetPaint : Widget
    //{
    //    //public new string type = "paint";
    //    public Vector2[] points;
    //    public Style style = null;
    //}
    [System.Serializable]
    public class Style
    {
        public string padding;
        public string backgroundOpacity;
        public string backgroundColor;
        public string borderColor;
        public string borderStyle;
        public string borderOpacity;
        public string borderWidth;
        public int fontSize;
        public string fontFamily;
        public string textColor;
        public string textAlign;
        public string textAlignVertical;
        public string shapeType;
    }
    [System.Serializable]
    public class ShapeStyle : Style
    {
        //public string borderColor = "#ff0000";
        //public string backgroundColor = "#ff0000";
        //public string shapeType = "octagon";
    }

    [System.Serializable]
    public class WidgetRead : Widget
    {
        public string id = "";
        public Style style;
        public float x = 0;
        public float y = 0;

        public string text = "";
        public string url = "";
        public Vector2[] points;

        public float rotation;
        public float scale;

        public float width;
        public float height;

        public string title = "";

        public LineTarget startWidget;
        public LineTarget endWidget;

        public override string ToString()
        {
            return "[" + type + "]" + System.Environment.NewLine +
                text + System.Environment.NewLine +
                title;
        }
    }

    //
    [System.Serializable]
    public class LineTarget
    {
        public string id = "";
    }
    //
    [System.Serializable]
    public class AllWidgets
    {
        public string type = "collection";
        public WidgetRead[] data;
        public int size = -1;
    }

    [System.Serializable]
    public class WidgetPos
    {
        public float x = 0;
        public float y = 0;
    }

    //

    [System.Serializable]
    public class Picture
    {
        public string type = "";
        public string imageUrl = "";
    }
}
