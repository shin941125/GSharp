﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GSharp.Graphic.Blocks;
using GSharp.Graphic.Holes;
using GSharp.Base.Cores;
using GSharp.Base.Objects;
using GSharp.Extension;
using GSharp.Base.Objects.Customs;
using GSharp.Base.Objects.Lists;
using System.Xml;
using GSharp.Graphic.Controls;

namespace GSharp.Graphic.Objects.Lists
{
    /// <summary>
    /// EmptyList.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ItemBlock : SettableObjectBlock
    {
        public GItem GItem
        {
            get
            {
                var list = ListHole.SettableObjectBlock?.GObject;
                var index = NumberHole.NumberBlock?.GObject;

                if (list == null || index == null)
                {
                    throw new ToObjectException("배열의 x번째 요소 블럭이 완성되지 않았습니다.", this);
                }

                return new GItem(list, index);
            }
        }
        
        public override List<GBase> ToGObjectList()
        {
            return new List<GBase>() { GObject };
        }

        public override List<BaseHole> HoleList
        {
            get
            {
                return _HoleList;
            }
        }

        public override GObject GObject
        {
            get
            {
                return GItem;
            }
        }

        public override GSettableObject GSettableObject
        {
            get
            {
                return GItem;
            }
        }

        private List<BaseHole> _HoleList;

        // 생성자
        public ItemBlock()
        {
            InitializeComponent();

            _HoleList = new List<BaseHole>() { ListHole, NumberHole };

            InitializeBlock();
        }

        public static BaseBlock LoadBlockFromXml(XmlElement element, BlockEditor blockEditor)
        {
            var block = new ItemBlock();

            XmlNodeList elementList = element.SelectNodes("Holes/Hole");
            block.ListHole.SettableObjectBlock = LoadBlock(elementList[0].SelectSingleNode("Block") as XmlElement, blockEditor) as SettableObjectBlock;
            block.NumberHole.BaseObjectBlock = LoadBlock(elementList[1].SelectSingleNode("Block") as XmlElement, blockEditor) as SettableObjectBlock;

            return block;
        }
    }
}
