using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RussiaCube
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
		}

		private Worker worker;

		private void labelStart_Click(object sender, EventArgs e)
		{
			labelStart.Enabled = false;
			labelPause.Enabled = true;
			labelPause.TabStop = false;
			labelContinue.Enabled = false;
			worker.InitGame();
			worker.Start();
		}

		private void labelPause_Click(object sender, EventArgs e)
		{
			labelStart.Enabled = true;
			labelPause.Enabled = false;
			labelContinue.Enabled = true;
			worker.Pause();
		}

		private void labelContinue_Click(object sender, EventArgs e)
		{
			labelStart.Enabled = false;
			labelPause.Enabled = true;
			labelContinue.Enabled = false;
			worker.Continue();
		}


		private void MainForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (!worker.IsWorking)
				return;
			if (e.KeyCode == UpKey)//顺时针移动
			{
				worker.Up();
			}
			else if (e.KeyCode == DownKey)//向下移动
			{
				worker.Down();
			}
			else if (e.KeyCode == MoveLeftKey)//向左移动
			{
				worker.Left();
			}
			else if (e.KeyCode == MoveRightKey)//向右移动
			{
				worker.Right();
			}
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			TheConfig = new Config();
			viewBox.Size = new Size(ColCount * RectPix, RowCount * RectPix);
			controlPanel.Left = viewBox.Width + 18;
			this.Size = new Size(viewBox.Width + controlPanel.Width + 49, viewBox.Height + 60);
			worker = new Worker(RowCount, ColCount, RectPix, CellBackColor, CellForeColorArray,
				Graphics.FromHwnd(viewBox.Handle), Graphics.FromHwnd(preBox.Handle), SleepTime, labelScore);
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			TheConfig.Save();
		}

		#region 配置
		private Config _config;
		private Config TheConfig
		{
			get
			{
				return _config;
			}
			set
			{
				_config = value;
				UpKey = value.UpKey;
				DownKey = value.DownKey;
				MoveLeftKey = value.MoveLeftKey;
				MoveRightKey = value.MoveRightKey;
				ColCount = value.ColCount;
				RowCount = value.RowCount;
				RectPix = value.RectPix;
				CellBackColor = value.CellBackColor;
				CellForeColorArray = value.CellForeColorArray;
				SleepTime = value.SleepTime;
			}
		}
		#endregion


		#region 私有变量与基本属性
		private Keys _upkey;
		/// <summary>
		/// 顺时针移动按键
		/// </summary>
		private Keys UpKey
		{
			get
			{
				return _upkey;
			}
			set
			{
				_upkey = value;
			}
		}

		private Keys _downkey;
		/// <summary>
		/// 向下移动按键
		/// </summary>
		public Keys DownKey
		{
			get
			{
				return _downkey;
			}
			set
			{
				_downkey = value;
			}
		}

		private Keys _moveleftkey;
		/// <summary>
		/// 向左移动按键
		/// </summary>
		public Keys MoveLeftKey
		{
			get
			{
				return _moveleftkey;
			}
			set
			{
				_moveleftkey = value;
			}
		}

		private Keys _moverightkey;
		/// <summary>
		/// 向右移动按键
		/// </summary>
		public Keys MoveRightKey
		{
			get
			{
				return _moverightkey;
			}
			set
			{
				_moverightkey = value;
			}
		}

		private int _colCount;
		/// <summary>
		/// 水平格子数
		/// </summary>
		public int ColCount
		{
			get
			{
				return _colCount;
			}
			set
			{
				_colCount = value;
			}
		}

		private int _rowCount;
		/// <summary>
		/// 竖直格子数
		/// </summary>
		public int RowCount
		{
			get
			{
				return _rowCount;
			}
			set
			{
				_rowCount = value;
			}
		}

		private int _rectPix;
		/// <summary>
		/// 方块像素
		/// </summary>
		private int RectPix
		{
			get
			{
				return _rectPix;
			}
			set
			{
				_rectPix = value;
			}
		}

		private Color _cellBackColor;
		/// <summary>
		/// 场景背景色
		/// </summary>
		private Color CellBackColor
		{
			get
			{
				return _cellBackColor;
			}
			set
			{
				_cellBackColor = value;
				viewBox.BackColor = value;
			}
		}

		private Color[] _cellForeColorArray;
		/// <summary>
		/// 砖块颜色
		/// </summary>
		private Color[] CellForeColorArray
		{
			get
			{
				return _cellForeColorArray;
			}
			set
			{
				_cellForeColorArray = value;
			}
		}

		private int _sleepTime;
		/// <summary>
		/// 间隔时间，以毫秒为单位
		/// </summary>
		public int SleepTime
		{
			get
			{
				return _sleepTime;
			}
			set
			{
				_sleepTime = value;
			}
		}

		#endregion


		/// <summary>
		/// 加载设置
		/// </summary>
		private void LoadConfig()
		{
			TheConfig = new Config();
		}
	}
}
