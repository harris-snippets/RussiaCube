using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RussiaCube
{
	class Config
	{
		public Config()
		{
			load();
		}

		/// <summary>
		/// 读取Xml文件中的参数配置信息，并一次赋给成员变量
		/// </summary>
		private void load()
		{
			UpKey = Keys.Up;
			DownKey = Keys.Down;
			MoveLeftKey = Keys.Left;
			MoveRightKey = Keys.Right;

			ColCount = 16;
			RowCount = 20;

			RectPix = 20;
			CellForeColorArray = new Color[] { Color.Red, Color.White, Color.Pink, Color.Yellow, Color.SeaGreen };
			CellBackColor = Color.LightBlue;

			SleepTime = 800;
		}

		/// <summary>
		/// 顺时针移动
		/// </summary>
		public Keys UpKey
		{
			get;
			set;
		}
		/// <summary>
		/// 向下移动
		/// </summary>
		public Keys DownKey
		{
			get;
			set;
		}
		/// <summary>
		/// 向左移动
		/// </summary>
		public Keys MoveLeftKey
		{
			get;
			set;
		}
		/// <summary>
		/// 向右移动
		/// </summary>
		public Keys MoveRightKey
		{
			get;
			set;
		}

		/// <summary>
		/// 列数，即活动区域一共有多少列
		/// </summary>
		public int ColCount
		{
			get;
			set;
		}
		/// <summary>
		/// 行数，即活动区域一共多少行
		/// </summary>
		public int RowCount
		{
			get;
			set;
		}

		/// <summary>
		/// 方块像素
		/// </summary>
		public int RectPix
		{
			get;
			set;
		}

		/// <summary>
		/// 场景背景色
		/// </summary>
		public Color CellBackColor
		{
			get;
			set;
		}

		/// <summary>
		/// 砖块颜色
		/// </summary>
		public Color[] CellForeColorArray
		{
			get;
			set;
		}

		/// <summary>
		/// 睡眠时间大小，以毫秒为单位
		/// </summary>
		public int SleepTime
		{
			get;
			set;
		}

		internal void Save()
		{
			//throw new NotImplementedException();
		}
	}
}
