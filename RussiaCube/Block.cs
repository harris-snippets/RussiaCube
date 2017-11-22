using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RussiaCube
{
	class Block
	{
		public Block(Point[] cells, int colorIndex)
		{
			_cells = cells;
			ColorIndex = colorIndex;
		}

		private Point[] _cells;
		public Point[] Cells
		{
			get
			{
				return _cells;
			}
			set
			{
				_cells = value;
			}
		}

		public int ColPos
		{
			get;
			set;
		}
		public int RowPos
		{
			get;
			set;
		}

		public int ColorIndex
		{
			get;
			set;
		}

		private static Random r = new Random();

		//顺时针旋转砖块 旋转公式 x1=y;y1=-x
		public void Clockwise()
		{
			for (int i = 0; i < _cells.Length; i++)
			{
				int x = _cells[i].X;
				_cells[i].X = _cells[i].Y;
				_cells[i].Y = -x;
			}
		}
		/// <summary>
		/// 逆时针旋转砖块 旋转砖块 x1=-y;y1=x;
		/// </summary>
		public void Anticlockwise()
		{
			for (int i = 0; i < _cells.Length; i++)
			{
				int x = _cells[i].X;
				_cells[i].X = -_cells[i].Y;
				_cells[i].Y = x;
			}
		}

		/// <summary>
		/// 返回一个带颜色的块
		/// </summary>
		/// <param name="ColorCount">可选择的颜色的数量</param>
		/// <returns></returns>
		public static Block GetABlock(int ColorCount)
		{
			Point[] data = AllCells[r.Next(0, AllCells.Count)];
			return new Block(data, r.Next(0, ColorCount));
		}

		private static readonly List<Point[]> AllCells = new List<Point[]>()
		{
			new Point[]{new Point(0,0),new Point(-1,0),new Point(1,0),new Point(0,-1)},
			new Point[]{new Point(0,0),new Point(-1,-1),new Point(0,-1),new Point(1,0)},
			new Point[]{new Point(0,0),new Point(-1,0),new Point(0,-1),new Point(1,-1)},
			new Point[]{new Point(0,0),new Point(-1,-1),new Point(0,-1),new Point(-1,0)}
		};
	}
}
